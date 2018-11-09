Public Class Frm_principal

    Private Sub EscaneoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EscaneoToolStripMenuItem.Click
        Shell("C:\Users\TSE\Desktop\SISTEMA EMBALAJE  KIT DE TRANSMISION 2019\Escaneo\bin\x86\Debug\TSE2018.exe", 1)
        Me.Close()
    End Sub

    Private Sub DespachoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DespachoToolStripMenuItem.Click

    End Sub

    Private Sub RecepcionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecepcionToolStripMenuItem.Click
        Shell("C:\Users\TSE\Desktop\SISTEMA EMBALAJE  KIT DE TRANSMISION 2019\RECEPCION\RECEPCION\bin\Debug\RECEPCION.EXE", 1)
    End Sub

    Private Sub CrearUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrearUsuarioToolStripMenuItem.Click
        Crear_Usuario.Show()
        Me.Close()
    End Sub

    Private Sub Frm_principal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub ProductosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductosToolStripMenuItem.Click
        Frm_Articulo.Show()
        Me.Close()
    End Sub

    Private Sub EventoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventoToolStripMenuItem.Click
        Frm_Evento.Show()

        Me.Close()
    End Sub
End Class
