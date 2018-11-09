Imports System.Data.OracleClient
Public Class Edit_User
    'Variable para guardar el index donde se hizo clic 
    Dim i As Integer
    'Variable para guardar el id del usuario que se quiere editar
    Dim id As Integer

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        User.Show()
        Me.Close()
    End Sub

    Private Sub DGView_Usuarios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGView_Usuarios.CellContentClick
        'Lleno las variables para que guarde la posicion donde se hizo click y el id
        i = DGView_Usuarios.CurrentRow.Index
        id = DGView_Usuarios.Item(0, i).Value
        'Le asigno el valor del nombre del rol al textbox para que se edite 
        txt_nombre.Text = DGView_Usuarios.Item(1, i).Value
        txt_contraseña = DGView_Usuarios.Item(2, i).Value
        Combo_rol.ValueMember() = DGView_Usuarios.Item(3, i).Value
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim sqlConsulta As String = "update usuarios set Nombre_user=:Nombre, Contrasenia:pass, id_rol:rol where id_user=:id_usuario"
        Dim comando1 As New OracleCommand(sqlConsulta, con)
        comando1.Parameters.Add(":id_usuario", OracleType.Int32, 30).Value = id
        comando1.Parameters.Add(":Nombre", OracleType.VarChar, 30).Value = txt_nombre.Text
        comando1.Parameters.Add(":pass", OracleType.VarChar, 30).Value = txt_contraseña.Text
        comando1.Parameters.Add(":rol", OracleType.VarChar, 30).Value = Combo_rol.SelectedValue.ToString
        con.Open()
        comando1.ExecuteNonQuery()
        con.Close()
    End Sub

    Private Sub Edit_User_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Cargar datagrid
        Try
            Dim sqlConsult As String = " select * from USUARIOS"
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
                MessageBox.Show("No se tienen registros de Usuarios, porfavor ingrese uno.") ' + vbCrLf + "O YA SE ENTREGARON TODAS LAS BOLSAS DE LOS CENTROS DE VOTACION DE ESA RUTA")
                Crear_Usuario.Show()
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        'Cargar Combobox de roles
        Try
            Dim sqlConsult As String = "select * from Rol"
            Dim dataAdapter As New OracleDataAdapter(sqlConsult, con)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.Combo_rol.DataSource = DT
            Combo_rol.ValueMember = "ID_rol"
            Combo_rol.DisplayMember = "NOMBRE_rol"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
End Class