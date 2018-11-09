Imports System.Data.OracleClient
Public Class Edit_rol

    Dim i As Integer ' Variable que guardara el indice de la fila donde se hizo clic
    Dim id As Integer ' Variable que guardara el id del rol que se modificara


    Private Sub btn_Ingresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Ingresar.Click
        Try
            Dim sqlConsulta As String = "update Rol set Nombre_rol=:Nombre where id_rol=:id_rol"
            Dim comando1 As New OracleCommand(sqlConsulta, con)
            comando1.Parameters.Add(":id_rol", OracleType.Int32, 30).Value = id
            comando1.Parameters.Add(":Nombre", OracleType.VarChar, 30).Value = txt_rol.Text
            con.Open()
            comando1.ExecuteNonQuery()
            con.Close()
            cargar_rol()
            txt_rol.Text = ""
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Rol.Show()
        Me.Close()
    End Sub

    Private Sub Edit_rol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cargar_rol()
    End Sub
    Private Sub cargar_rol()
        con.Close()
        Try
            Dim sqlConsult As String = " select ID_ROL, NOMBRE_ROL from rol"
            Dim comando As New OracleCommand(sqlConsult, con)
            Dim lector As OracleDataReader = Nothing
            con.Open()
            lector = comando.ExecuteReader()
            If lector.HasRows Then
                DGView_Roles.Refresh()
                Dim dataAdapter As New OracleDataAdapter(comando)
                Dim dataSet As New DataSet
                dataAdapter.Fill(dataSet, "rol")
                Me.DGView_Roles.DataSource = dataSet.Tables("rol")
                con.Close()
            Else
                con.Close()
                MessageBox.Show("No se tienen registros de roles, porfavor ingrese uno.")
                Add_Rol.Show()
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub DGView_Roles_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGView_Roles.CellClick
        'Lleno las variables para que guarde la posicion donde se hizo click y el id
        i = DGView_Roles.CurrentRow.Index
        id = DGView_Roles.Item(0, i).Value
        'Le asigno el valor del nombre del rol al textbox para que se edite 
        txt_rol.Text = DGView_Roles.Item(1, i).Value
    End Sub
End Class