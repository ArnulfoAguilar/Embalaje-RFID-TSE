Imports System.Data
Imports System.Data.OracleClient
Imports System.Windows.Forms
Imports System
Imports VB_RFID3_Host_Sample1

Public Class UsuarioCrear
    Dim cadena As String = "server = ORCL; User id = Arnulfo; Password = Ar13004; Unicode =True"
    Dim conn As New OracleConnection(cadena)
    Private Sub UsuarioCrear_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Try

                Dim sqlConsult As String = "Select id_rol, descripcion from rol"
                Dim dataAdapter As New OracleDataAdapter(sqlConsult, conn)
                Dim DT As New DataTable
                dataAdapter.Fill(DT)
                Me.ComboBox1.DataSource = DT
                ComboBox1.DisplayMember = "id_rol"
                conn.Close()

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                conn.Close()
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Cancelar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancelar_btn.Click
        Me.Close()
        AppForm.Show()
    End Sub

    Private Sub Aceptar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Aceptar_btn.Click
        Dim sqlConsulta As String = "insert into usuario values(:id,:nombre,:username,:password,:rol,:ipreader)"
        conn.Open()
        Dim comando As New OracleCommand(sqlConsulta, conn)
        comando.Parameters.Add(":id", OracleType.Char, 50).Value = id_txt.Text
        comando.Parameters.Add(":nombre", OracleType.Char, 50).Value = TextBox_nombre.Text
        comando.Parameters.Add(":username", OracleType.Char, 50).Value = TextBox_username.Text
        comando.Parameters.Add(":password", OracleType.Char, 50).Value = TextBox_password.Text
        comando.Parameters.Add(":rol", OracleType.Char, 50).Value = ComboBox1.Text
        comando.Parameters.Add(":ipreader", OracleType.Char, 50).Value = TextBox_ipreader.Text
        comando.ExecuteNonQuery()
        conn.Close()
        MessageBox.Show("Usuario insertado")
        Me.Close()
        AppForm.Show()

    End Sub
End Class