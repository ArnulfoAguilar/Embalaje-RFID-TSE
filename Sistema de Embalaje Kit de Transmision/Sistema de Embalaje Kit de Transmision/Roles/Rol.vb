Imports System.Data.OracleClient
Public Class Rol

    Private Sub Rol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim sqlConsult As String = " select * from rol"
            Dim comando As New OracleCommand(sqlConsult, con)
            Dim lector As OracleDataReader = Nothing
            con.Open()
            lector = comando.ExecuteReader()
            If lector.HasRows Then
                DataGridView1.Refresh()
                Dim dataAdapter As New OracleDataAdapter(comando)
                Dim dataSet As New DataSet
                dataAdapter.Fill(dataSet, "rol")
                Me.DataGridView1.DataSource = dataSet.Tables("rol")
                con.Close()
            Else
                con.Close()
                MessageBox.Show("No se tienen registros de roles, porfavor ingrese uno.") ' + vbCrLf + "O YA SE ENTREGARON TODAS LAS BOLSAS DE LOS CENTROS DE VOTACION DE ESA RUTA")
                'DataGViewArticulos.DataSource = null
                DataGridView1.Columns.Clear()
                DataGridView1.Refresh()
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class