Public Class User

    Private Sub btn_Add_User_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Add_User.Click
        Crear_Usuario.Show()
        Me.Close()
    End Sub

    Private Sub btn_upd_user_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_upd_user.Click
        Edit_User.Show()
        Me.Close()
    End Sub

    Private Sub btn_delete_user_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delete_user.Click
        Delete_User.Show()
        Me.Close()
    End Sub
End Class