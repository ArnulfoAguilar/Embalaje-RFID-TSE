Public Class add_ubicaciones

    Private Sub btn_limpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_limpiar.Click
        txtNombre.Text = " "
        txtIp.Text = " "
    End Sub

    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Ubicaciones.Show()
        Me.Close()
    End Sub
End Class