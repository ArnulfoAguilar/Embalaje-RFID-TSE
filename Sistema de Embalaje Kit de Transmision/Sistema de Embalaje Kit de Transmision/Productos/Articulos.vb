Imports System.Data.OracleClient
Public Class Articulos

    Private Sub Articulos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lb_reg.Text = ""
        cargar_articulos()
    End Sub


    Private Sub cargar_articulos() '(ByVal sql As String)
        Dim sql As String = "select  ART.NOMBRE_ARTICULO, ART.NOMENCLATURA, RET.NOMBRE_RETORNO " & _
                            "from TIPOARTICULO art join RETORNAR ret on ART.ID_RETORNO=RET.ID_RETORNO"
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
        Dim sql As String = "select count(*) num_articulos from TIPOARTICULO"
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
        Frm_Mtn_Producto.ShowDialog()
    End Sub
End Class