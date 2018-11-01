Imports System.Data.OracleClient
Public Class Edit_Ubicacion
    Dim i As Integer
    Dim id As Integer
    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Ubicaciones.Show()
        Me.Close()
    End Sub

    Private Sub Edit_Ubicacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenar_Grid()
    End Sub
    Private Sub llenar_Grid()
        Try
            Dim sqlConsult As String = " select * from UBICACIONES"
            Dim comando As New OracleCommand(sqlConsult, con)
            Dim lector As OracleDataReader = Nothing
            con.Open()
            lector = comando.ExecuteReader()
            If lector.HasRows Then
                DGView_ubicaciones.Refresh()
                Dim dataAdapter As New OracleDataAdapter(comando)
                Dim dataSet As New DataSet
                dataAdapter.Fill(dataSet, "ubicaciones")
                Me.DGView_ubicaciones.DataSource = dataSet.Tables("ubicaciones")
                con.Close()
            Else
                con.Close()
                DGView_ubicaciones.Columns.Clear()
                DGView_ubicaciones.Refresh()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btn_agregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_agregar.Click
        Dim sqlConsulta As String = "update ubicaciones set Nombre=:Nombre, ip=:Ip where id_ubicaion=:id_ubicacion"
        Dim comando1 As New OracleCommand(sqlConsulta, con)
        comando1.Parameters.Add(":id_UBICACION", OracleType.Int32, 30).Value = id
        comando1.Parameters.Add(":Nombre", OracleType.VarChar, 30).Value = txtNombre.Text
        comando1.Parameters.Add(":IP", OracleType.VarChar, 30).Value = txtIP.Text
        con.Open()
        comando1.ExecuteNonQuery()
        con.Close()
    End Sub

    Private Sub DGView_ubicaciones_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGView_ubicaciones.CellContentClick
        'Lleno las variables para que guarde la posicion donde se hizo click y el id
        i = DGView_ubicaciones.CurrentRow.Index
        id = DGView_ubicaciones.Item(0, i).Value
        'Le asigno el valor del nombre del rol al textbox para que se edite 
        txtNombre.Text = DGView_ubicaciones.Item(1, i).Value
        txtIP = DGView_ubicaciones.Item(2, i).Value

    End Sub
End Class