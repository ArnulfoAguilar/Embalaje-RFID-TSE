Imports System.Data.OracleClient
Public Class add_ubicaciones

    Private Sub btn_limpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_limpiar.Click
        txtNombre.Text = " "
        txtIp.Text = " "
    End Sub

    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Ubicaciones.Show()
        Me.Close()
    End Sub

    Private Sub add_ubicaciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenar_Grid()
    End Sub

    Private Sub llenar_Grid()
        Try
            Dim sqlConsult As String = " select * from UBICACION"
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
        If txtNombre.Text = "" Then
            MessageBox.Show("Hay campos obligatorios vacios")
        Else
            If txtIp.Text = "" Then
                MessageBox.Show("Hay campos obligatorios vacios")
            Else
                Try
                    Conexion.con.Close()
                    Dim SQL As String = "INSERT INTO UBICACION (NOMBRE, DIRECCION_IP) VALUES (:NOMBRE, :IP)"
                    'Dim SQL As String = "INSERT INTO USUARIOS (NOMBRE, CONTRA, IPREADER) VALUES (:NOMBRE, :PASS, :IPREADER)"
                    Dim comando As New OracleCommand(SQL, Conexion.con)
                    comando.Parameters.Add(":NOMBRE", OracleType.VarChar, 30).Value = txtNombre.Text
                    comando.Parameters.Add(":IP", OracleType.VarChar, 30).Value = txtIp.Text
                    'comando.Parameters.Add(":IPREADER", OracleType.VarChar, 30).Value = txtIP.Text
                    Conexion.con.Open()
                    comando.ExecuteNonQuery()
                    Conexion.con.Close()
                    MessageBox.Show("Ubicacion Ingresada exitosamente")
                    llenar_Grid()
                    txtNombre.Text = ""
                    txtIp.Text = ""
                    txtNombre.Focus()
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                    Conexion.con.Close()
                End Try
            End If
        End If
    End Sub
End Class