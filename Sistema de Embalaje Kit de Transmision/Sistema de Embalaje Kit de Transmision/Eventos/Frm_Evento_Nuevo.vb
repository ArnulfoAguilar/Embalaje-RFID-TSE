Imports System.Data.OracleClient
Public Class Frm_Evento_nuevo

    Private Sub Frm_Evento_nuevo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con2.Close()
    End Sub

    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        Dim sql As String = " insert into EVENTO (id_evento, nombre_evento, fecha, observaciones) values " & _
                           " ((seq_evento.nextval), :nombre, :fecha, :obsv)"
        Dim cmd As New OracleCommand(sql, con2)
        cmd.Parameters.Add(":nombre", OracleType.VarChar, 30).Value = txt_nombre.Text
        cmd.Parameters.Add(":obsv", OracleType.VarChar, 255).Value = txt_obsv.Text
        cmd.Parameters.Add(":fecha", OracleType.DateTime).Value = Convert.ToDateTime(txt_fecha.Text)

        Try
            con2.Open()
            cmd.ExecuteNonQuery()
            con2.Close()
            MessageBox.Show("Registro Guardado")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        txt_nombre.Text = ""
        txt_obsv.Text = ""
        txt_fecha.Value = Now
        Me.Close()
    End Sub

    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Frm_principal.Show()
        Me.Close()
    End Sub
End Class