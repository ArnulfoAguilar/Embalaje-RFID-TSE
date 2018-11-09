Imports System.Data.OracleClient
Public Class Crear_Usuario
    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If txtNombre.Text <> "" Then

            If txtContra.Text <> "" Then
                Try
                    Conexion.con.Close()
                    Dim SQL As String = "INSERT INTO USUARIOS (ID_USER, NOMBRE_USER, CONTRASENIA, ID_ROL) VALUES ((SEQ_USUARIO.nextval),:NOMBRE, :PASS, :ID_ROL)"
                    Dim comando As New OracleCommand(SQL, Conexion.con)
                    comando.Parameters.Add(":NOMBRE", OracleType.VarChar, 30).Value = txtNombre.Text
                    comando.Parameters.Add(":PASS", OracleType.VarChar, 30).Value = txtContra.Text
                    comando.Parameters.Add(":ID_ROL", OracleType.Int32, 30).Value = combo_rol.SelectedValue.ToString
                    Conexion.con.Open()
                    comando.ExecuteNonQuery()
                    Conexion.con.Close()
                    llenar_Grid()
                    txtNombre.Text = ""
                    txtContra.Text = ""
                    txtNombre.Focus()
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                    Conexion.con.Close()
                End Try
            Else
                MessageBox.Show("Llene todos los campos")
            End If

        Else
            MessageBox.Show("Llene todos los campos")
        End If
    End Sub
    Private Sub CargarCombobox_Rol()
        con.Close()
        Try

            Dim sqlConsult As String = "select * from Rol"
            Dim dataAdapter As New OracleDataAdapter(sqlConsult, con)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.combo_rol.DataSource = DT
            combo_rol.ValueMember = "ID_ROL"
            combo_rol.DisplayMember = "NOMBRE_ROL"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        con.Close()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        User.Show()
        Me.Close()
    End Sub

    Private Sub btn_limpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_limpiar.Click
        txtNombre.Text = ""
        txtContra.Text = ""
    End Sub

    Private Sub Crear_Usuario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenar_Grid()
        CargarCombobox_Rol()
    End Sub
    Private Sub llenar_Grid()
        Try
            Dim sqlConsult As String = " select ID_USER, NOMBRE_USER from USUARIOS"
            Dim comando As New OracleCommand(sqlConsult, con)
            Dim lector As OracleDataReader = Nothing
            con.Open()
            lector = comando.ExecuteReader()
            If lector.HasRows Then
                DGView_Usuarios.Refresh()
                Dim dataAdapter As New OracleDataAdapter(comando)
                Dim dataSet As New DataSet
                dataAdapter.Fill(dataSet, "usuarios")
                Me.DGView_Usuarios.DataSource = dataSet.Tables("usuarios")
                con.Close()
            Else
                con.Close()
                DGView_Usuarios.Columns.Clear()
                DGView_Usuarios.Refresh()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class