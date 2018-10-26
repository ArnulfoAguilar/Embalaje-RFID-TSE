Public Class CargarDatos

    Private Sub btnImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportar.Click
        'If txtExcel.Text = String.Empty Then
        'MessageBox.Show("No ha seleccionado ningun archivo")
        ' Else
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "imagenes|*.jpg; *.png"
        ofd.Title = "Abriendo Archivo"
        If ofd.ShowDialog = DialogResult.OK Then
            Dim UrlArchivo As String = ofd.FileName
            Dim nombre As String = ofd.SafeFileName

        End If


        'End If

    End Sub
End Class