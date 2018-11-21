Imports System.Data.OracleClient
Public Class Frm_Articulo

    Private Sub Articulos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        num = -1
        lb_reg.Text = ""
        btn_elim_reg.Hide()
        btn_cancel.Hide()
        cargar_articulos()
    End Sub


    Private Sub cargar_articulos() '(ByVal sql As String)
        Dim sql As String = "select ART.ID_articulo, ART.NOMBRE_ARTICULO, ART.NOMENCLATURA, RET.NOMBRE_RETORNO " & _
                            "from ARTICULO art join RETORNAR ret on ART.ID_RETORNO=RET.ID_RETORNO order by art.id_articulo"
        Dim comando As New OracleCommand(sql, con2)
        Dim lector As OracleDataReader = Nothing
        con2.Open()
        Dim dataAdapter As New OracleDataAdapter(comando)
        Dim dataSet As New DataSet
        dataAdapter.Fill(dataSet, "TipoArticulo")
        Me.dtg_Articulo.DataSource = dataSet.Tables("TipoArticulo")
        con2.Close()
        num_registros()
    End Sub

    Private Sub num_registros()
        Dim sql As String = "select count(*) num_articulos from ARTICULO"
        Dim comando As New OracleCommand(sql, con2)
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            lector.Read()
            lb_reg.Text = lector.GetInt32(0)
            'Do While lector.Read
            '    lb_reg.Text = lector.GetInt16(0)
            'Loop()
            con2.Close()
        End If
    End Sub

    Private Sub btn_nuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_nuevo.Click
        con2.Close()
        Frm_Articulo_Nuevo.ShowDialog()
        cargar_articulos()
    End Sub


    Private Sub btn_editar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_editar.Click
        con2.Close()
        'MessageBox.Show(num.ToString)
        If num < 0 Then
            MessageBox.Show("Elija una fila para editar")
        Else
            Frm_Articulo_Edit.ShowDialog()
        End If
        cargar_articulos()
        num = -1
    End Sub

    Private Sub dtg_Articulo_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtg_Articulo.CellClick
        Dim i As Integer
        i = dtg_Articulo.CurrentRow.Index
        num = dtg_Articulo.Item(0, i).Value()
        'MessageBox.Show(num.ToString)
    End Sub

    Private Sub btn_elim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_elim.Click
        con2.Close()
        If num < 0 Then
            MessageBox.Show("Elija una fila para eliminar")
        Else
            btn_elim_reg.Show()
            btn_cancel.Show()
            btn_elim.Enabled = False
            btn_nuevo.Enabled = False
            btn_editar.Enabled = False
        End If
    End Sub

    Private Sub btn_elim_reg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_elim_reg.Click
        Dim sql As String = "delete from articulo where id_articulo =:idtipo "
        ' MessageBox.Show(num.ToString)
        Try
            Dim cmd As New OracleCommand(sql, con2)
            cmd.Parameters.Add(":idtipo", OracleType.VarChar, 30).Value = num.ToString
            con2.Open()
            cmd.ExecuteNonQuery()
            con2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        num = -1
        btn_elim.Enabled = True
        cargar_articulos()
        btn_elim_reg.Hide()
        btn_cancel.Hide()
    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        num = -1
        btn_elim.Enabled = True
        btn_nuevo.Enabled = True
        btn_editar.Enabled = True
        btn_elim_reg.Hide()
        btn_cancel.Hide()
    End Sub

    Private Sub InicioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Frm_principal.Show()
        Me.Close()
    End Sub

    Private Sub EscaneoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Shell("C:\Embalaje-RFID-TSE\Escaneo\bin\x86\Debug\TSE2018.exe", 1)
    End Sub

    Private Sub DespachoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Shell("C:\Embalaje-RFID-TSE\Despacho\bin\x86\Debug\TSE2018.exe", 1)
    End Sub

    Private Sub RecepcionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Shell("C:\Users\TSE\Desktop\SISTEMA EMBALAJE  KIT DE TRANSMISION 2019\RECEPCION\RECEPCION\bin\Debug\RECEPCION.EXE", 1)
    End Sub

    Private Sub CrearUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Crear_Usuario.Show()
        Me.Close()
    End Sub

    Private Sub ModificarUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Edit_User.Show()
        Me.Close()
    End Sub

    Private Sub EventoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Frm_Evento.Show()
        Me.Close()
    End Sub

    Private Sub PaqueteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Frm_Paquete.Show()
        Me.Close()
    End Sub

    Private Sub CrearUbicacionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        add_ubicaciones.Show()
        Me.Close()
    End Sub

    Private Sub EditarUbicacionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Edit_Ubicacion.Show()
        Me.Close()
    End Sub

    Private Sub CrearRolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Add_Rol.Show()
        Me.Close()
    End Sub

    Private Sub ActualizarRolToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Edit_rol.Show()
        Me.Close()
    End Sub

    Private Sub ImprimirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Imprimir.ShowDialog()
    End Sub
End Class