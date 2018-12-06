Imports System.Data
Imports System.Data.OracleClient
Imports Microsoft.VisualBasic
Public Class Frm_Rep_Escaneo
    Dim band As Integer = 0
    Private Sub Frm_Rep_Escaneo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtg_unido.Hide()
        combobox_paq()
        combobox_sede()
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
                            " on C.ID_SEDE=S.ID_SEDE where C.ID_DEPTO=S.ID_DEPTO and ID_PAQUETE =:num"
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
        combobox_sede()
    End Sub
    Private Sub btn_cargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cargar.Click
        Dim sql As String = "select ID_CAJA, DEPTO, SEDE, MUNI, CENTRO, CODEBAR, RUTA from vin_report " & _
                              "where ID_SEDE=:sede and ID_PAQUETE=:paq and ID_ESTADO=2 and ID_COMPLETO=1 order by 1"
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
        rptdoc = New Rep_Escaneo
        rptdoc.SetDataSource(dt)
        Form2.CrystalReportViewer1.ReportSource = rptdoc
        Form2.ShowDialog()
        Form2.Dispose()
    End Sub
End Class
