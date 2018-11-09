Imports System.Data.OracleClient
Public Class Frm_Paquete

    Private Sub Frm_Paquete_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        num = -1
        lb_reg.Text = ""
        cargar_paquetes()
        btn_elim.Hide()
    End Sub

    Private Sub num_registros()
        Dim sql As String = "select count(*) num_paquetes from PAQUETE"
        Dim comando As New OracleCommand(sql, con2)
        con2.Open()
        Dim lector As OracleDataReader = comando.ExecuteReader()
        If lector.HasRows Then
            lector.Read()
            lb_reg.Text = lector.GetInt32(0)
            con2.Close()
        End If
    End Sub
    Private Sub cargar_paquetes()
        Dim sql As String = "select PAQ.ID_PAQUETE, PAQ.NOMBRE_PAQUETE ,EV.NOMBRE_EVENTO from paquete " & _
                            " paq join EVENTO ev on PAQ.ID_EVENTO=EV.ID_EVENTO order by 1"
        Dim comando As New OracleCommand(sql, con2)
        Dim lector As OracleDataReader = Nothing
        con2.Open()
        Dim dataAdapter As New OracleDataAdapter(comando)
        Dim dataSet As New DataSet
        dataAdapter.Fill(dataSet, "PAQUETES")
        Me.dtg_Paquete.DataSource = dataSet.Tables("PAQUETES")
        con2.Close()
        num_registros()
    End Sub

    Private Sub dtg_Paquete_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtg_Paquete.CellClick
        Dim i As Integer
        i = dtg_Paquete.CurrentRow.Index
        num = dtg_Paquete.Item(0, i).Value()
    End Sub

    Private Sub btn_editar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_editar.Click
        con2.Close()
        If num < 0 Then
            MessageBox.Show("Elija una fila para editar")
        Else
            Frm_Paquete_Edit.ShowDialog()
        End If
        cargar_paquetes()
        num = -1
    End Sub

    Private Sub btn_nuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_nuevo.Click
        Frm_Paquete_Nuevo.Show()
        Me.Close()
    End Sub
End Class