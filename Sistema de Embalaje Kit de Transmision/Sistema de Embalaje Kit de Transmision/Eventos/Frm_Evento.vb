Imports System.Data.OracleClient
Public Class Frm_Evento

    Private Sub Frm_Evento_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        num = -1
        lb_reg.Text = ""
        cargar_eventos()
        btn_elim.Hide()
    End Sub

    Private Sub num_registros()
        Dim sql As String = "select count(*) num_eventos from EVENTO"
        Dim comando As New OracleCommand(sql, con2)
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            lector.Read()
            lb_reg.Text = lector.GetInt32(0)
            con2.Close()
        End If
    End Sub

    Private Sub cargar_eventos()
        Dim sql As String = "SELECT ID_EVENTO, NOMBRE_EVENTO, FECHA, OBSERVACIONES FROM EVENTO order by 1"
        Dim comando As New OracleCommand(sql, con2)
        Dim lector As OracleDataReader = Nothing
        con2.Open()
        Dim dataAdapter As New OracleDataAdapter(comando)
        Dim dataSet As New DataSet
        dataAdapter.Fill(dataSet, "EVENTOS")
        Me.dtg_Evento.DataSource = dataSet.Tables("EVENTOS")
        con2.Close()
        num_registros()
    End Sub

    Private Sub btn_nuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_nuevo.Click
        con2.Close()
        Frm_Evento_nuevo.ShowDialog()
        cargar_eventos()
    End Sub

    Private Sub btn_editar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_editar.Click
        con2.Close()
        'MessageBox.Show(num.ToString)
        If num < 0 Then
            MessageBox.Show("Elija una fila para editar")
        Else
            Frm_Evento_Edit.ShowDialog()
        End If
        cargar_eventos()
        num = -1
    End Sub

    Private Sub dtg_Evento_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtg_Evento.CellClick
        Dim i As Integer
        i = dtg_Evento.CurrentRow.Index
        num = dtg_Evento.Item(0, i).Value()
    End Sub
End Class