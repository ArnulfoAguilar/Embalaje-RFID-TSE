Imports System.Data.OracleClient
Public Class User

    Private Sub btn_Add_User_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Add_User.Click
        Crear_Usuario.Show()
        Me.Close()
    End Sub

    Private Sub btn_upd_user_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_upd_user.Click
        Edit_User.Show()
        Me.Close()
    End Sub

    Private Sub btn_delete_user_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Delete_User.Show()
        Me.Close()
    End Sub

    Private Sub User_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim sqlConsult As String = " select ID_USER,NOMBRE_USER from USUARIOS"
            Dim comando As New OracleCommand(sqlConsult, con)
            Dim lector As OracleDataReader = Nothing
            con.Open()
            lector = comando.ExecuteReader()
            If lector.HasRows Then
                DGView_user.Refresh()
                Dim dataAdapter As New OracleDataAdapter(comando)
                Dim dataSet As New DataSet
                dataAdapter.Fill(dataSet, "usuarios")
                Me.DGView_user.DataSource = dataSet.Tables("usuarios")
                con.Close()
            Else
                con.Close()
                MessageBox.Show("No se tienen registros de Usuarios, porfavor ingrese uno.") ' + vbCrLf + "O YA SE ENTREGARON TODAS LAS BOLSAS DE LOS CENTROS DE VOTACION DE ESA RUTA")
                'DataGViewArticulos.DataSource = null
                DGView_user.Columns.Clear()
                DGView_user.Refresh()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub InicioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InicioToolStripMenuItem.Click
        Frm_principal.Show()
        Me.Close()
    End Sub
End Class