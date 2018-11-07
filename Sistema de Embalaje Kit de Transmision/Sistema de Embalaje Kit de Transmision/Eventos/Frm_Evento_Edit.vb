Imports System.Data.OracleClient
Public Class Frm_Evento_Edit

    Private Sub Frm_Evento_Edit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cargar()
    End Sub

    Private Sub cargar()
        Dim sql As String = "select nombre_evento, fecha, observaciones from EVENTO where id_evento=:id_evento"
        Dim comando As New OracleCommand(sql, con2)
        comando.Parameters.Add(":id_evento", OracleType.UInt32).Value = num
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            Do While lector.Read
                txt_nombre.Text = lector.GetString(0)
                txt_fecha.Value = lector.GetDateTime(1)
                txt_obsv.Text = lector.GetString(2)
            Loop
        End If
        con2.Close()
    End Sub


    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        Dim sql As String = "update EVENTO set nombre_evento=:name, fecha=:date, observaciones=:obsv" & _
                            " where id_evento=:id_even"
        Dim cmd As New OracleCommand(sql, con2)

        Try
            cmd.Parameters.Add(":name", OracleType.VarChar, 50).Value = txt_nombre.Text
            cmd.Parameters.Add(":fecha", OracleType.DateTime).Value = txt_fecha.Value
            cmd.Parameters.Add(":obsv", OracleType.VarChar, 255).Value = txt_obsv.Text

            con2.Open()
            cmd.ExecuteNonQuery()
            con2.Close()
            MessageBox.Show("Registro Actualizado")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        txt_nombre.Text = ""
        txt_obsv.Text = ""
        Me.Close()
    End Sub
End Class