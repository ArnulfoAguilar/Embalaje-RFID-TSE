Imports System.Data.OracleClient
Public Class Frm_Login

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim pas As Boolean = False
        If txt_user.Text = "" Then
            MessageBox.Show("Ingrese un Usuario")
        Else
            If txt_pass.Text = "" Then
                MessageBox.Show("Escriba la contraseña")
            Else
                Dim sql As String = "select * from vista_users where NOMBRE_USER=:useri and CONTRASENIA=:pass"
                Dim cmd As New OracleCommand(sql, con)
                cmd.Parameters.Add(":useri", OracleType.VarChar, 50).Value = txt_user.Text
                cmd.Parameters.Add(":pass", OracleType.VarChar, 50).Value = txt_pass.Text

                con.Open()
                Dim lector As OracleDataReader = cmd.ExecuteReader()
                If lector.HasRows Then
                    lector.Read()
                    user_global = lector.GetInt32(0)
                    rol_global = lector.GetInt32(1)
                    pas = True
                Else
                    MessageBox.Show("Error Contraseña o usuario Incorrecto")
                    txt_pass.Text = ""
                    txt_user.Text = ""
                End If
                con.Close()
            End If
        End If
        If pas Then
            Frm_principal.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

End Class
