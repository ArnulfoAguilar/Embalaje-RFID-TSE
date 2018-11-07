Imports System.Data.OracleClient

Public Class Frm_Articulo_Nuevo

    Private Sub Frm_Mtn_Producto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.Close()
        con2.Close()
        combobox_retornar()
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
        Dim sql As String = " insert into ARTICULO (id_articulo, id_retorno, nombre_articulo, nomenclatura) values " & _
                            " ((seq_articulo.nextval), :id_retorno, :nombre, :nomenclatura)"
        Try
            Dim comando1 As New OracleCommand(sql, con2)

            comando1.Parameters.Add(":id_retorno", OracleType.VarChar, 30).Value = cbx_ret.SelectedValue.ToString
            comando1.Parameters.Add(":nombre", OracleType.VarChar, 50).Value = txt_nombre.Text
            comando1.Parameters.Add(":nomenclatura", OracleType.VarChar, 30).Value = txt_nomenclatura.Text

            con2.Open()
            comando1.ExecuteNonQuery()
            con2.Close()
            MessageBox.Show("Registro Guardado")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        txt_nombre.Text = ""
        txt_nomenclatura.Text = ""
        Me.Close()
    End Sub
End Class