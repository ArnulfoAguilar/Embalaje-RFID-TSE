Imports System.Data.OracleClient
Public Class Frm_Articulo_Edit

    Private Sub Frm_Articulo_Edit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        combobox_retornar()
        cargar()
    End Sub

    Private Sub cargar()
        Dim sql As String = "select NOMBRE_ARTICULO, NOMENCLATURA, ID_RETORNO from ARTICULO where id_articulo = :id_tipo"
        Dim comando As New OracleCommand(sql, con2)
        comando.Parameters.Add(":id_tipo", OracleType.UInt32).Value = num
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            Do While lector.Read
                txt_nombre.Text = lector.GetString(0)
                txt_nomenclatura.Text = lector.GetString(1)
            Loop
        End If
        con2.Close()
    End Sub

    Private Sub combobox_retornar()
        Dim sql As String = "select * from retornar order by 1 desc"
        Try
            Dim cmd As New OracleCommand(sql, con2)
            Dim dataAdapter As New OracleDataAdapter(cmd)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.cbx_ret.DataSource = DT
            cbx_ret.ValueMember = "id_retorno"
            cbx_ret.DisplayMember = "nombre_retorno"
            'bandera = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btn_guardar_prd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar_prd.Click
        Dim sql As String = "update ARTICULO set nombre_articulo=:name, nomenclatura=:nomen, " & _
                        "id_retorno=:ret where id_articulo=:idtipo"
        Try
            Dim cmd As New OracleCommand(sql, con2)
            cmd.Parameters.Add(":name", OracleType.VarChar, 50).Value = txt_nombre.Text
            cmd.Parameters.Add(":nomen", OracleType.VarChar, 30).Value = txt_nomenclatura.Text
            cmd.Parameters.Add(":ret", OracleType.UInt32).Value = cbx_ret.SelectedValue
            cmd.Parameters.Add(":idtipo", OracleType.UInt32).Value = num

            con2.Open()
            cmd.ExecuteNonQuery()
            con2.Close()
            MessageBox.Show("Registro Actualizado")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        txt_nombre.Text = ""
        txt_nomenclatura.Text = ""
        Me.Close()
    End Sub
End Class