Imports System.Data.OracleClient
Public Class Frm_login

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ingresar.Click
        Dim contra As String = txt_pass.Text
        Dim user As String = txt_user.Text
        Dim pasar As Integer

        pasar = 0
        If user = "" Then
            MessageBox.Show("Ingrese su Usuario")
        Else
            If contra = "" Then
                MessageBox.Show("Ingrese la contraseña")
            Else
                Dim sql As String = "select ID_USER ,ID_ROL from USUARIOS where nombre_user =:useri " & _
                                    " and CONTRASENIA = fn_valor_encrypt(:pass)"
                '"select ID_USER ,ID_ROL from USUARIOS where nombre_user =:useri and CONTRASENIA = fn_valor_encrypt(:pass);
                Dim cmd As New OracleCommand(sql, con)
                cmd.Parameters.Add(":useri", OracleType.VarChar, 50).Value = user
                cmd.Parameters.Add(":pass", OracleType.VarChar, 50).Value = contra
                Try
                    con.Open()
                    Dim lector As OracleDataReader = cmd.ExecuteReader()
                    If lector.HasRows Then
                        lector.Read()
                        user_global = lector.GetInt32(0)
                        rol_global = lector.GetInt32(1)
                        pasar = 1
                    End If
                    con.Close()
                Catch ex As Exception
                    MessageBox.Show("Verifique Usuario y contraseña")
                End Try
            End If
        End If

        If pasar = 1 Then
            Frm_principal.Show()
            Me.Close()
        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_salir.Click
        Me.Close()
    End Sub

End Class
