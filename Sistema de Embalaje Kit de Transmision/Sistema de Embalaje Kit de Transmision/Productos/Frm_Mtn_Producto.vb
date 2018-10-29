Imports System.Data.OracleClient

Public Class Frm_Mtn_Producto

    Private Sub Frm_Mtn_Producto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.Close()
        con2.Close()
        combobox_retornar()
    End Sub
    Private Sub combobox_retornar()
        Dim sql As String = "select * from retornar"
        Try
            Dim cmd As New OracleCommand(sql, con2)
            Dim dataAdapter As New OracleDataAdapter(cmd)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.cbx_ret.DataSource = DT
            cbx_ret.ValueMember = "id_retorno"
            cbx_ret.DisplayMember = "nombre_retorno"
            'bandera = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btn_guardar_prd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar_prd.Click
        Dim sql As String = " "

    End Sub
End Class