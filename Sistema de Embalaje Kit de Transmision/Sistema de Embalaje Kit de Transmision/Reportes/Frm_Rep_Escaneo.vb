Imports System.Data
Imports System.Data.OracleClient
Imports Microsoft.VisualBasic
Public Class Frm_Rep_Escaneo
    Dim band As Integer = 0
    Private Sub Frm_Rep_Escaneo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtg_unido.Hide()
        combobox_paq()
        combobox_sede()
        esconder()
    End Sub
    Private Sub combobox_paq()
        Dim sql As String = "select distinct L.ID_PAQUETE, P.NOMBRE_PAQUETE  " & _
                            "from LISTA_ART_PACK l join PAQUETE p on L.ID_PAQUETE =P.ID_PAQUETE"
        Try
            Dim cmd As New OracleCommand(sql, con2)
            Dim dataAdapter As New OracleDataAdapter(cmd)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.cbx_paq.DataSource = DT
            cbx_paq.ValueMember = "id_paquete"
            cbx_paq.DisplayMember = "nombre_paquete"
            combobox_sede()
            band = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub combobox_sede()
        If band = 1 Then
            Dim sql As String = "select distinct C.ID_SEDE, S.NOMBRE_SEDE from CAJA c join SEDE_LOGISTICA s " & _
                            " on C.ID_SEDE=S.ID_SEDE where C.ID_DEPTO=S.ID_DEPTO and ID_PAQUETE =:num order by 1"
            Try
                Dim cmd As New OracleCommand(sql, con2)
                cmd.Parameters.Add("num", OracleType.UInt32).Value = cbx_paq.SelectedValue
                Dim dataAdapter As New OracleDataAdapter(cmd)
                Dim DT As New DataTable
                dataAdapter.Fill(DT)
                Me.cbx_sede.DataSource = DT
                cbx_sede.ValueMember = "id_sede"
                cbx_sede.DisplayMember = "nombre_sede"
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End If
    End Sub
    Private Sub cbx_paq_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_paq.SelectedIndexChanged
        esconder()
        dtg_leido.DataSource = Nothing
        combobox_sede()
    End Sub
    Private Sub cbx_sede_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_sede.SelectedIndexChanged
        dtg_leido.DataSource = Nothing
        esconder()
    End Sub
    Private Sub reg_existentes()
        Dim sql As String = "select count(*) from caja where id_paquete=:paq and id_sede=:sede"
        Dim comando As New OracleCommand(sql, con2)
        comando.Parameters.Add(":paq", OracleType.Int16).Value = cbx_paq.SelectedValue
        comando.Parameters.Add("sede", OracleType.UInt32).Value = cbx_sede.SelectedValue
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            lector.Read()
            lb_cajas.Text = lector.GetInt32(0)
        End If
        con2.Close()
    End Sub
    Private Sub reg_leidas()
        Dim sql As String = "select count(*) from caja where id_paquete=:paq and id_sede=:sede " & _
                            "and id_completo=1 and id_estado=2"
        Dim comando As New OracleCommand(sql, con2)
        comando.Parameters.Add(":paq", OracleType.Int16).Value = cbx_paq.SelectedValue
        comando.Parameters.Add("sede", OracleType.UInt32).Value = cbx_sede.SelectedValue
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            lector.Read()
            lb_leidas.Text = lector.GetInt32(0)
        End If
        con2.Close()
    End Sub
    Private Sub btn_cargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cargar.Click
        If IsNothing(cbx_sede.SelectedValue) Then
            MessageBox.Show("Debe elegir una sede valida o elija otro paquete electoral")
        Else
            Dim sql As String = "select * from reporte_detalle where ID_SEDE=:sede and " & _
                              "ID_PAQUETE=:paq and ID_ESTADO=2 and ID_COMPLETO=1 "
            Dim cmd As New OracleCommand(sql, con)
            cmd.Parameters.Add("paq", OracleType.UInt32).Value = cbx_paq.SelectedValue
            cmd.Parameters.Add("sede", OracleType.UInt32).Value = cbx_sede.SelectedValue
            Dim lector As OracleDataReader = Nothing
            Try
                con.Open()
                Dim dataAdapter As New OracleDataAdapter(cmd)
                Dim dataSet As New DataSet
                dataAdapter.Fill(dataSet, "CAJA")
                Me.dtg_leido.DataSource = dataSet.Tables("CAJA")
                con.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try

            'llenar el  datagrid que esta oculto

            Dim sql1 As String = "select distinct count(*) paquetes, SEDE, RUTA, NOM_EVENTO  from VIN_REPORT where ID_PAQUETE=:paq" & _
                                 " and ID_SEDE=:sede and ID_ESTADO = 2 group by SEDE, RUTA, NOM_EVENTO"
            Dim cmd1 As New OracleCommand(sql1, con)
            cmd1.Parameters.Add("paq", OracleType.UInt32).Value = cbx_paq.SelectedValue
            cmd1.Parameters.Add("sede", OracleType.UInt32).Value = cbx_sede.SelectedValue
            Dim lector1 As OracleDataReader = Nothing
            Try
                con.Open()
                Dim dataAdapter As New OracleDataAdapter(cmd1)
                Dim dataSet As New DataSet
                dataAdapter.Fill(dataSet, "CAJA")
                Me.dtg_unido.DataSource = dataSet.Tables("CAJA")
                con.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
            reg_existentes()
            reg_leidas()
            mostrar()
        End If
    End Sub
    Private Sub btn_rep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_rep.Click
        Dim dt As New DataTable
        With dt
            .Columns.Add("PAQUETES")
            .Columns.Add("SEDE")
            .Columns.Add("RUTA")
            .Columns.Add("NOM_EVENTO")
        End With
        For Each dr As DataGridViewRow In dtg_unido.Rows
            dt.Rows.Add(dr.Cells("PAQUETES").Value, dr.Cells("SEDE").Value, dr.Cells("RUTA").Value,
                        dr.Cells("NOM_EVENTO").Value)
        Next
        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New Rep_Acta
        rptdoc.SetDataSource(dt)
        Form2.CrystalReportViewer1.ReportSource = rptdoc
        Form2.ShowDialog()
        Form2.Dispose()
    End Sub

    Private Sub btn_listado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_listado.Click
        Dim dt As New DataTable
        With dt
            .Columns.Add("CAJA")
            .Columns.Add("PAQUETE_TRANSMISION")
            .Columns.Add("MALETIN")
            .Columns.Add("RTS")
            .Columns.Add("INVENTARIO")
            .Columns.Add("NOMBRE_CENTRO")
            .Columns.Add("ID_SEDE")
            .Columns.Add("NOMBRE_SEDE")
            .Columns.Add("RUTA")
            .Columns.Add("ID_PAQUETE")
            .Columns.Add("ID_ESTADO")
            .Columns.Add("ID_COMPLETO")
        End With
        For Each dr As DataGridViewRow In dtg_leido.Rows
            dt.Rows.Add(dr.Cells("CAJA").Value, dr.Cells("PAQUETE_TRANSMISION").Value, dr.Cells("MALETIN").Value,
                        dr.Cells("RTS").Value, dr.Cells("INVENTARIO").Value, dr.Cells("NOMBRE_CENTRO").Value,
                        dr.Cells("ID_SEDE").Value, dr.Cells("NOMBRE_SEDE").Value, dr.Cells("RUTA").Value,
                        dr.Cells("ID_PAQUETE").Value, dr.Cells("ID_ESTADO").Value, dr.Cells("ID_COMPLETO").Value)
        Next
        Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rptdoc = New Rep_Listado
        rptdoc.SetDataSource(dt)
        Form2.CrystalReportViewer1.ReportSource = rptdoc
        Form2.ShowDialog()
        Form2.Dispose()
    End Sub
    Private Sub esconder()
        lb_cajas.Hide()
        lb_leidas.Hide()
        btn_rep.Hide()
        btn_listado.Hide()
    End Sub
    Private Sub mostrar()
        lb_cajas.Show()
        lb_leidas.Show()
        btn_rep.Show()
        btn_listado.Show()
    End Sub
End Class
