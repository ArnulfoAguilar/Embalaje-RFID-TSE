Imports System.Data.OracleClient
Public Class Crear_Usuario
    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If txtNombre.Text <> "" Then
            If txtIP.Text <> "" Then
                If txtContra.Text <> "" Then
                    Try
                        Conexion.con.Close()
                        Dim SQL As String = "INSERT INTO USUARIOS (NOMBRE, CONTRA) VALUES (:NOMBRE, :PASS)"
                        'Dim SQL As String = "INSERT INTO USUARIOS (NOMBRE, CONTRA, IPREADER) VALUES (:NOMBRE, :PASS, :IPREADER)"
                        Dim comando As New OracleCommand(SQL, Conexion.con)
                        comando.Parameters.Add(":NOMBRE", OracleType.VarChar, 30).Value = txtNombre.Text
                        comando.Parameters.Add(":PASS", OracleType.VarChar, 30).Value = txtContra.Text
                        'comando.Parameters.Add(":IPREADER", OracleType.VarChar, 30).Value = txtIP.Text
                        Conexion.con.Open()
                        comando.ExecuteNonQuery()
                        Conexion.con.Close()
                        MessageBox.Show("Usuario Ingresado exitosamente")
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                        Conexion.con.Close()
                    End Try
                Else
                    MessageBox.Show("Llene todos los campos")
                End If

            Else
                MessageBox.Show("Llene todos los campos")
            End If

        Else
            MessageBox.Show("Llene todos los campos")
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        User.Show()
        Me.Close()
    End Sub

    Private Sub btn_limpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_limpiar.Click
        txtNombre.Text = ""
        txtContra.Text = ""
    End Sub
End Class