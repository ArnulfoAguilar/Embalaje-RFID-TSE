Imports System.Data.OracleClient
Imports System.Data
Imports System.Windows.Forms
Imports System
Imports VB_RFID3_Host_Sample1

Public Class editUser
    Dim cadena As String = "server = ORCL; User id = Arnulfo; Password = Ar13004; Unicode =True"
    Dim conn As New OracleConnection(cadena)
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            
            Dim sqlConsult As String = "select * from usuario where nombre = :nombre"
            Dim comando As New OracleCommand(sqlConsult, conn)
            comando.Parameters.Add(":nombre", OracleType.Char, 20).Value = ComboBox1.Text
            Dim lector As OracleDataReader = Nothing

            conn.Open()

            lector = comando.ExecuteReader()

            ' Si el lector tiene alguna fila, es porque al menos existe
            ' un registro con los datos especificados, por tanto, podemos
            ' decir que la validación ha sido satisfactoria.
            '

            If lector.HasRows Then
                Do While lector.Read
                    TextBox1.Text = lector.GetString(0)
                    TextBox2.Text = lector.GetString(1)
                    TextBox3.Text = lector.GetString(2)
                    TextBox4.Text = lector.GetString(3)
                    TextBox5.Text = lector.GetString(4)
                    TextBox6.Text = lector.GetString(5)
                Loop
                conn.Close()
                lector.Close()
            Else
                MessageBox.Show("No existe ese nombre de Usuario")
                conn.Close()
                lector.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    
    Private Sub editUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim sqlConsult As String = "Select nombre from usuario"
            Dim dataAdapter As New OracleDataAdapter(sqlConsult, conn)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.ComboBox1.DataSource = DT
            ComboBox1.DisplayMember = "nombre"
            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Hide()
        appForm.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try

            Dim sqlConsulta As String = "Delete from usuario where nombre= :nombre"
            conn.Open()
            Dim comando As New OracleCommand(sqlConsulta, conn)
            comando.Parameters.Add(":nombre", OracleType.Char, 50).Value = TextBox2.Text
            comando.ExecuteNonQuery()
            conn.Close()
            MessageBox.Show("USUARIO ELIMINADO")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            Try

                Dim sqlConsult As String = "Select nombre from usuario"
                Dim dataAdapter As New OracleDataAdapter(sqlConsult, conn)
                Dim DT As New DataTable
                dataAdapter.Fill(DT)
                Me.ComboBox1.DataSource = DT
                ComboBox1.DisplayMember = "nombre"
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim sqlConsulta As String = "update usuario set ID_ROL=:rol, ID_USUARIO=:id_usuario, NOMBRE=:nombre, USERNAME=:username, PASSWORD=:password, IPREADER=:ipreader where nombre=:nombreAntiguo"
            conn.Open()
            Dim comando As New OracleCommand(sqlConsulta, conn)
            comando.Parameters.Add(":id_usuario", OracleType.Char, 50).Value = TextBox1.Text
            comando.Parameters.Add(":nombre", OracleType.Char, 50).Value = TextBox2.Text
            comando.Parameters.Add(":username", OracleType.Char, 50).Value = TextBox3.Text
            comando.Parameters.Add(":password", OracleType.Char, 50).Value = TextBox4.Text
            comando.Parameters.Add(":rol", OracleType.Char, 50).Value = TextBox5.Text
            comando.Parameters.Add(":ipreader", OracleType.Char, 16).Value = TextBox6.Text
            comando.Parameters.Add(":nombreAntiguo", OracleType.Char, 50).Value = ComboBox1.Text
            comando.ExecuteNonQuery()
            conn.Close()
            MessageBox.Show("Usuario insertado")
            Me.Close()
            AppForm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class