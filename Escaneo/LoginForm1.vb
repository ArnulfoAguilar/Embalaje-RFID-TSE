Imports System.Data.OracleClient
Imports System.Data
Imports System
Imports System.Windows.Forms
Imports VB_RFID3_Host_Sample1



Public Class LoginForm1
    

    Dim dataReader As OracleDataReader
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Dim username As String = UsernameTextBox.Text
            Dim password As String = PasswordTextBox.Text
            Dim sqlConsult As String = "SELECT ID_USER, R.NOMBRE_ROL,NOMBRE_USER FROM vista_users US JOIN ROL R ON US.ID_ROL=R.ID_ROL " & _
                                    " where us.NOMBRE_USER =:username   AND us.CONTRASENIA =:password"
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
                    Ipreader = ComboUbicacion.SelectedValue.ToString
                    ROL = lector.GetString(1)
                    USUARIO = lector.GetString(2)
                Loop

                'MessageBox.Show("BIENVENIDO AL SISTEMA " + USUARIO)
                conn.Close()
                lector.Close()
                AppForm.Show()
                Me.Close()
            

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

    Private Sub LogoPictureBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoPictureBox.Click

    End Sub

    Private Sub LoginForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cargar_ubicacion()
    End Sub
    Private Sub cargar_ubicacion()
        Try

            Dim sqlConsult As String = "select NOMBRE_UBI, DIRECCION_IP from UBICACION"
            Dim dataAdapter As New OracleDataAdapter(sqlConsult, conn)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.ComboUbicacion.DataSource = DT
            ComboUbicacion.ValueMember = "DIRECCION_IP"
            ComboUbicacion.DisplayMember = "NOMBRE_UBI"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class
