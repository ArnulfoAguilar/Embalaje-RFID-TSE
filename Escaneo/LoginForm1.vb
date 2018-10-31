Imports System.Data.OracleClient
Imports System.Data
Imports System
Imports System.Windows.Forms
Imports VB_RFID3_Host_Sample1



Public Class LoginForm1
    
    Dim cmd As OracleCommand
    Dim dataReader As OracleDataReader
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Dim username As String = UsernameTextBox.Text
            Dim password As String = PasswordTextBox.Text
            Dim sqlConsult As String = "select us.ipReader,  rl.nombre_rol, us.nombre_usuario from usuario us" & _
                                    " join rol rl on us.id_rol=rl.id_rol where us.NOMBRE_USUARIO =:username   AND us.PASSWORD =:password"
            Dim comando As New OracleCommand(sqlConsult, conn)
            comando.Parameters.Add(":username", OracleType.Char, 20).Value = username
            comando.Parameters.Add(":password", OracleType.Char, 20).Value = password
            Dim lector As OracleDataReader = Nothing

            '
            conn.Open()

            ' Ejecutamos el comando
            '
            lector = comando.ExecuteReader()

            ' Si el lector tiene alguna fila, es porque al menos existe
            ' un registro con los datos especificados, por tanto, podemos
            ' decir que la validación ha sido satisfactoria.
            '

            If lector.HasRows Then
                Do While lector.Read
                    Ipreader = lector.GetString(0)
                    ROL = lector.GetString(1)
                    USUARIO = lector.GetString(2)

                Loop
                If ROL = "ADMIN" Or ROL = "ESCANEO" Then
                    MessageBox.Show("BIENVENIDO AL SISTEMA " + USUARIO)
                    conn.Close()
                    lector.Close()
                    AppForm.Show()
                    Me.Close()
                Else
                    MessageBox.Show("No cuenta con los suficientes privilegios para manipular esta aplicacion")
                    UsernameTextBox.Clear()
                    UsernameTextBox.Focus()
                    PasswordTextBox.Clear()
                    conn.Close()
                End If

            Else
                MessageBox.Show("EL USUARIO O LA CONTRASEÑA SON INCORRECTOS")
                conn.Close()
                lector.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

End Class
