Imports System.Data.OracleClient
Imports System.Data
Imports System
Imports System.Windows.Forms
Imports RECEPCION

Public Class LoginForm1
    Public Ipreader As String
    Public ROL As String
    Public USUARIO As String

    Dim cadena As String = "server = ORCL; User id = pruebas_rfid_recepcion; Password = USI123; Unicode =True"
    Dim Conexion As New OracleConnection(cadena)
    Dim cmd As OracleCommand
    Dim dataReader As OracleDataReader
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Dim username As String = UsernameTextBox.Text
            Dim password As String = PasswordTextBox.Text
            Dim sqlConsult As String = "select us.ipReader,  rl.nombre_rol, us.nombre_usuario from usuario us join rol rl on us.id_rol=rl.id_rol where us.NOMBRE_USUARIO =:username   AND us.PASSWORD =:password"
            Dim comando As New OracleCommand(sqlConsult, Conexion)
            comando.Parameters.Add(":username", OracleType.Char, 20).Value = username
            comando.Parameters.Add(":password", OracleType.Char, 20).Value = password
            Dim lector As OracleDataReader = Nothing

            '
            Conexion.Open()

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
                If ROL = "ADMIN" Or ROL = "RECEPCION" Then
                    MessageBox.Show("BIENVENIDO AL SISTEMA " + USUARIO)
                    Conexion.Close()
                    lector.Close()
                    Me.Hide()
                    Form1.Show()
                Else
                    MessageBox.Show("No cuenta con los suficientes privilegios para manipular esta aplicacion")
                    UsernameTextBox.Clear()
                    UsernameTextBox.Focus()
                    PasswordTextBox.Clear()
                    Conexion.Close()
                End If

            Else
                MessageBox.Show("EL USUARIO O LA CONTRASEÑA SON INCORRECTOS")
                Conexion.Close()
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
