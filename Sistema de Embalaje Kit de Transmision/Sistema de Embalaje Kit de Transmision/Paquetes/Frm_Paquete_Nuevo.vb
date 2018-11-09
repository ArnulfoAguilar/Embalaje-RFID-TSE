Imports System.Data.OracleClient
Public Class Frm_Paquete_Nuevo
    Dim ban As Boolean
    Dim id_pack As Integer

    Private Sub Frm_Paquete_Nuevo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ban = True
        txt_art.AutoCompleteCustomSource = AutoCompletar()
        txt_art.AutoCompleteMode = AutoCompleteMode.Suggest
        txt_art.AutoCompleteSource = AutoCompleteSource.CustomSource
        combobox_evento()
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
        End Try
    End Sub

    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        Dim i As Integer = 0
        Dim sql As String = "insert into paquete (id_paquete, id_evento, nombre_paquete) values " & _
                            " ((seq_paquete.nextval), :id_evento, :nombre)"
        Dim cmd As New OracleCommand(sql, con2)
        cmd.Parameters.Add(":id_evento", OracleType.VarChar, 5).Value = cbx_evento.SelectedValue.ToString
        cmd.Parameters.Add(":nombre", OracleType.VarChar, 50).Value = txt_nombre.Text
        Try
            con2.Open()
            cmd.ExecuteNonQuery()
            con2.Close()
            MessageBox.Show("Registro Guardado")
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
            paquete_actual()
            cargar_articulos()
        End If
    End Sub
    'INICIA EL CODIGO PARA AÑADIR ARTICULOS A CADA PAQUETE

    Private Sub num_registros()
        Dim sql As String = "select count(*) from LISTA_ART_PACK where ID_PAQUETE =:id_paq"
        Dim comando As New OracleCommand(sql, con2)
        comando.Parameters.Add(":id_paq", OracleType.UInt32).Value = id_pack
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            lector.Read()
            lb_reg.Text = lector.GetInt32(0)
            con2.Close()
        End If
    End Sub

    Private Sub cargar_articulos()
        Dim sql As String = "select A.NOMBRE_ARTICULO from ARTICULO a join LISTA_ART_PACK l " & _
                            " on A.ID_ARTICULO=L.ID_ARTICULO where L.ID_PAQUETE =:id_paq"
        Dim comando As New OracleCommand(sql, con2)
        comando.Parameters.Add(":id_paq", OracleType.UInt32).Value = id_pack
        con2.Open()
        Dim dataAdapter As New OracleDataAdapter(comando)
        Dim dataSet As New DataSet
        dataAdapter.Fill(dataSet, "TipoArticulo")
        Me.dtg_art.DataSource = dataSet.Tables("TipoArticulo")
        con2.Close()
        num_registros()
    End Sub

    Private Sub paquete_actual()
        Dim sql As String = "select ID_PAQUETE, NOMBRE_PAQUETE from PAQUETE  where rownum=1 order by ID_PAQUETE desc"
        Dim comando As New OracleCommand(sql, con2)
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            lector.Read()
            id_pack = lector.GetInt32(0)
        End If
        con2.Close()
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
            cmd.Parameters.Add(":id_pk", OracleType.UInt32).Value = id_pack
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
        Else
            lb_titulo.Show()
            lb_art.Show()
            lb_reg.Show()
            Label3.Show()
            txt_art.Show()
            dtg_art.Show()
            btn_add.Show()
            txt_nombre.Enabled = False
            cbx_evento.Enabled = False
            btn_guardar.Enabled = False
        End If
    End Sub

    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Frm_principal.Show()
        Me.Close()
    End Sub
End Class