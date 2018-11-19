Public Class Frm_principal

    Private Sub EscaneoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EscaneoToolStripMenuItem.Click
        Shell("C:\Embalaje-RFID-TSE\Escaneo\bin\x86\Debug\TSE2018.exe", 1)

    End Sub

    Private Sub DespachoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DespachoToolStripMenuItem.Click
        Shell("C:\Embalaje-RFID-TSE\Despacho\bin\x86\Debug\TSE2018.exe", 1)
    End Sub

    Private Sub RecepcionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecepcionToolStripMenuItem.Click
        Shell("C:\Users\TSE\Desktop\SISTEMA EMBALAJE  KIT DE TRANSMISION 2019\RECEPCION\RECEPCION\bin\Debug\RECEPCION.EXE", 1)
    End Sub

    Private Sub CrearUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrearUsuarioToolStripMenuItem.Click
        Crear_Usuario.ShowDialog()

    End Sub

    Private Sub ProductosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductosToolStripMenuItem.Click
        Frm_Articulo.ShowDialog()

    End Sub

    Private Sub EventoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventoToolStripMenuItem.Click
        Frm_Evento.ShowDialog()

    End Sub

    Private Sub PaqueteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaqueteToolStripMenuItem.Click
        Frm_Paquete.ShowDialog()

    End Sub

    Private Sub CrearRolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrearRolToolStripMenuItem.Click
        Add_Rol.ShowDialog()

    End Sub

    Private Sub ActualizarRolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActualizarRolToolStripMenuItem.Click
        Edit_rol.ShowDialog()

    End Sub

    Private Sub CrearUbicacionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrearUbicacionToolStripMenuItem.Click
        add_ubicaciones.ShowDialog()

    End Sub

    Private Sub EditarUbicacionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditarUbicacionToolStripMenuItem.Click
        Edit_Ubicacion.ShowDialog()

    End Sub

    Private Sub ModificarUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModificarUsuarioToolStripMenuItem.Click
        Edit_User.ShowDialog()

    End Sub

    Private Sub Frm_principal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If rol_global <> 0 Then
            MantenimientosToolStripMenuItem.HideDropDown()
            MantenimientosToolStripMenuItem.Enabled = False
        End If
	End Sub
    Private Sub ImprimirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImprimirToolStripMenuItem.Click
        Imprimir.ShowDialog()
    End Sub

End Class
