Imports System.Data.OracleClient

Public Class Frm_Mtn_Producto

    Private Sub combobox_retornar()
        Dim sql As String = "select id_depto, nombre_depto from departamento order by 1"
        Try
            Dim cmd As New OracleCommand(sql, con2)
            Dim dataAdapter As New OracleDataAdapter(cmd)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.cbx_ret.DataSource = DT
            cbx_ret.ValueMember = "id_depto"
            cbx_ret.DisplayMember = "nombre_depto"
            'bandera = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

End Class