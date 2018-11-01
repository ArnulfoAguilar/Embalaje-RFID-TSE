Imports System.Data.OracleClient
Public Class Ubicaciones

    Private Sub btn_agregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_agregar.Click
        add_ubicaciones.Show()
        Me.Close()
    End Sub

    Private Sub btn_modificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_modificar.Click
        Edit_Ubicacion.Show()
        Me.Close()
    End Sub

    Private Sub Ubicaciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                MessageBox.Show("No se tienen registros de Ubicaciones, porfavor ingrese uno.") ' + vbCrLf + "O YA SE ENTREGARON TODAS LAS BOLSAS DE LOS CENTROS DE VOTACION DE ESA RUTA")
                'DataGViewArticulos.DataSource = null
                DGView_ubicaciones.Columns.Clear()
                DGView_ubicaciones.Refresh()
                add_ubicaciones.Show()
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class