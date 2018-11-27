Imports System.Data.OracleClient
Public Class Frm_Paquete_Edit
    Dim ban As Boolean
    Dim j As Integer

    Private Sub Frm_Paquete_Edit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        combobox_evento()
        cargar()
        ban = True
        txt_art.AutoCompleteCustomSource = AutoCompletar()
        txt_art.AutoCompleteMode = AutoCompleteMode.Suggest
        txt_art.AutoCompleteSource = AutoCompleteSource.CustomSource
        cambiar(ban)
        con2.Close()

    End Sub

    Private Sub combobox_evento()
        Dim sql As String = "select id_evento, NOMBRE_EVENTO  from EVENTO order by 1"
        Try
            Dim cmd As New OracleCommand(sql, con2)
            Dim dataAdapter As New OracleDataAdapter(cmd)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.cbx_evento.DataSource = DT
            cbx_evento.ValueMember = "id_evento"
            cbx_evento.DisplayMember = "nombre_evento"
            'bandera = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            'select NOMBRE_PAQUETE, ID_EVENTO  from PAQUETE
        End Try
    End Sub

    Private Sub cargar()
        Dim sql As String = "select NOMBRE_PAQUETE, ID_EVENTO from PAQUETE where id_paquete=:id_paq"
        Dim comando As New OracleCommand(sql, con2)
        comando.Parameters.Add(":id_paq", OracleType.UInt32).Value = num
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            Do While lector.Read
                txt_nombre.Text = lector.GetString(0)
                'cbx_evento.ValueMember = lector.GetString(1)
            Loop
        End If
        con2.Close()
    End Sub

    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        Dim i As Integer = 0
        Dim sql As String = "update PAQUETE set nombre_paquete=:name, id_evento=:even where id_paquete=:id_paq"
        Dim cmd As New OracleCommand(sql, con2)
        cmd.Parameters.Add(":even", OracleType.VarChar, 5).Value = cbx_evento.SelectedValue.ToString
        cmd.Parameters.Add(":name", OracleType.VarChar, 50).Value = txt_nombre.Text
        cmd.Parameters.Add(":id_paq", OracleType.UInt32).Value = num
        Try
            con2.Open()
            cmd.ExecuteNonQuery()
            con2.Close()
            MessageBox.Show("Registro Actualizado")
            i = 1
        Catch ex As Exception
            i = 0
            MessageBox.Show(ex.ToString)
        End Try
        If i = 0 Then
            ban = True
            cambiar(ban)
        Else
            ban = False
            cambiar(ban)
            cargar_articulos()
        End If
    End Sub
    'INICIA EL CODIGO PARA AÑADIR ARTICULOS A CADA PAQUETE

    Private Sub cargar_articulos()
        Dim sql As String = "select A.ID_ARTICULO, A.NOMBRE_ARTICULO from ARTICULO a join LISTA_ART_PACK l " & _
                            " on A.ID_ARTICULO=L.ID_ARTICULO where L.ID_PAQUETE =:id_paq"
        Dim comando As New OracleCommand(sql, con2)
        comando.Parameters.Add(":id_paq", OracleType.UInt32).Value = num
        con2.Open()
        Dim dataAdapter As New OracleDataAdapter(comando)
        Dim dataSet As New DataSet
        dataAdapter.Fill(dataSet, "TipoArticulo")
        Me.dtg_art.DataSource = dataSet.Tables("TipoArticulo")
        con2.Close()
        num_registros()
    End Sub

    Private Sub num_registros()
        Dim sql As String = "select count(*) from LISTA_ART_PACK where ID_PAQUETE =:id_paq"
        Dim comando As New OracleCommand(sql, con2)
        comando.Parameters.Add(":id_paq", OracleType.UInt32).Value = num
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            lector.Read()
            lb_reg.Text = lector.GetInt32(0)
            con2.Close()
        End If
    End Sub

    Private Function comprobar_art() As Integer
        Dim i As Integer
        Dim sql As String = "select ID_ARTICULO from  ARTICULO where NOMBRE_ARTICULO = :nombre"
        Dim cmd As New OracleCommand(sql, con2)
        cmd.Parameters.Add(":nombre", OracleType.VarChar, 50).Value = txt_art.Text
        con2.Open()
        Dim lector As OracleDataReader = cmd.ExecuteReader()
        If lector.HasRows Then
            lector.Read()
            i = lector.GetInt32(0)
        Else
            i = 0
        End If
        con2.Close()
        Return i
    End Function

    Private Sub btn_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Dim i As Integer = comprobar_art()
        If i > 0 Then
            Dim sql As String = "insert into LISTA_ART_PACK values (:id_pk, :id_art)"
            Dim cmd As New OracleCommand(sql, con2)
            cmd.Parameters.Add(":id_pk", OracleType.UInt32).Value = num
            cmd.Parameters.Add(":id_art", OracleType.UInt32).Value = i
            Try
                con2.Open()
                cmd.ExecuteNonQuery()
                con2.Close()
            Catch ex As Exception
                MessageBox.Show("No se permiten articulos duplicados")
            End Try
            con2.Close()
            txt_art.Text = ""
            cargar_articulos()
        Else
            MessageBox.Show("NO EXISTE ESTE ARTICULO")
            txt_art.Text = ""
        End If

    End Sub

    ' FUNCIONES Y METODOS GENERALES
    Private Function Datos() As DataTable
        Dim dt As New DataTable()
        con2.Open()
        Dim sql As String = "Select * from articulo "
        Dim cmd As New OracleCommand(sql, con2)
        Dim dataAdapter As New OracleDataAdapter(cmd)
        dataAdapter.Fill(dt)
        Return dt
    End Function

    Private Function AutoCompletar() As AutoCompleteStringCollection
        Dim dt As DataTable = Datos()
        Dim coleccion As New AutoCompleteStringCollection()

        For Each row As DataRow In dt.Rows
            coleccion.Add(Convert.ToString(row("NOMBRE_ARTICULO")))
        Next
        Return coleccion
    End Function


    Private Sub cambiar(ByVal bnd As Boolean)
        If bnd Then
            lb_titulo.Hide()
            lb_art.Hide()
            lb_reg.Hide()
            Label3.Hide()
            txt_art.Hide()
            dtg_art.Hide()
            btn_add.Hide()
            txt_nombre.Enabled = True
            cbx_evento.Enabled = True
            btn_guardar.Enabled = True
            btn_elim.Hide()
        Else
            lb_titulo.Show()
            lb_art.Show()
            lb_reg.Show()
            Label3.Show()
            txt_art.Show()
            dtg_art.Show()
            btn_add.Show()
            btn_elim.Show()
            txt_nombre.Enabled = False
            cbx_evento.Enabled = False
            btn_guardar.Enabled = False
        End If
    End Sub

    Private Sub dtg_art_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtg_art.CellClick
        Dim i As Integer
        i = dtg_art.CurrentRow.Index
        j = dtg_art.Item(0, i).Value()
    End Sub


    Private Sub btn_elim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_elim.Click
        con2.Close()
        If j < 0 Then
            MessageBox.Show("Elija una fila para eliminar")
        Else
            Dim sql As String = "delete from LISTA_ART_PACK  where id_paquete=:ip_paq and id_articulo =:idtipo "
            Try
                Dim cmd As New OracleCommand(sql, con2)
                cmd.Parameters.Add(":id_paq", OracleType.UInt32).Value = num
                cmd.Parameters.Add(":idtipo", OracleType.UInt32).Value = j
                con2.Open()
                cmd.ExecuteNonQuery()
                con2.Close()
                MessageBox.Show("Registro eliminado")
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
            cargar_articulos()
        End If
    End Sub

    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        cambiar(ban)
        Me.Close()
    End Sub
End Class