Public Class Cargar

    Private Sub btnImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportar.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = " Excel| *.xlsx; *.csv; *.xls "

        If ofd.ShowDialog = DialogResult.OK Then
            Dim urlArchivo As String = ofd.FileName
            Dim nombre As String = ofd.SafeFileName
        End If

    End Sub
End Class