Imports System.Data.OracleClient
Public Class Add_Rol

    Private Sub btn_Ingresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Ingresar.Click
        Try
            Dim sqlConsulta As String = "INSERT INTO ROL (ID_ROL,NOMBRE_ROL) VALUES ((SEQ_ROL.nextval),:ROL)"
            Dim comando1 As New OracleCommand(sqlConsulta, con)
            comando1.Parameters.Add(":ROL", OracleType.VarChar, 30).Value = TXT_NOMBRE.Text
            con.Open()
            comando1.ExecuteNonQuery()
            con.Close()
            CargarRoles()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Add_Rol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CargarRoles()
    End Sub
    Private Sub CargarRoles()
        Try
            Dim sqlConsult As String = " select ID_ROL, NOMBRE_ROL from rol"
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

    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Rol.Show()
        Me.Close()
    End Sub
End Class