Imports System.Data.OracleClient
Public Class Frm_Rep_Escaneo

    Private Sub Frm_Rep_Escaneo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        combobox_paq()
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
            'bandera = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub combobox_sede()
        Dim sql As String = "select distinct C.ID_SEDE, S.NOMBRE_SEDE from CAJA c join SEDE_LOGISTICA s " & _
                            " on C.ID_SEDE=S.ID_SEDE where C.ID_DEPTO=S.ID_DEPTO and ID_PAQUETE =:num"
        Try
            Dim cmd As New OracleCommand(sql, con2)
            cmd.Parameters.Add("num", OracleType.UInt32).Value = cbx_paq.SelectedValue
            Dim dataAdapter As New OracleDataAdapter(cmd)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.cbx_paq.DataSource = DT
            cbx_paq.ValueMember = "id_sede"
            cbx_paq.DisplayMember = "nombre_sede"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btn_cargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cargar.Click
        Dim sql As String = "select ID_CAJA, DEPTO, MUNI, CENTRO, CODEBAR from VIN_REPORT " & _
                              "where ID_SEDE=:sede and ID_PAQUETE=:paq and ID_ESTADO=2"
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
    End Sub
End Class