

Public Class Crear_Usuario

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If txtNombre.Text <> "" Then
            If txtIP.Text <> "" Then
                If txtContra.Text <> "" Then


                Else
                    MessageBox.Show("Llene todos los campos")
                End If

            Else
                MessageBox.Show("Llene todos los campos")
            End If

        Else
            MessageBox.Show("Llene todos los campos")
        End If
            End If
        End If
    End Sub
End Class