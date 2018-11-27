
Imports System.Data.OracleClient
Public Class Cargar
    Private Sub Cargar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        combobox_paq()
    End Sub

    Private Sub combobox_paq()
        Dim sql As String = "select distinct L.ID_PAQUETE, P.NOMBRE_PAQUETE  " & _
                            "from LISTA_ART_PACK l join PAQUETE p on L.ID_PAQUETE =P.ID_PAQUETE"
        Try
            Dim cmd As New OracleCommand(sql, con2)
            Dim dataAdapter As New OracleDataAdapter(cmd)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.cbx_paq.DataSource = DT
            cbx_paq.ValueMember = "id_paquete"
            cbx_paq.DisplayMember = "nombre_paquete"
            'bandera = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub



    Private Sub btnCargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargar.Click
        Dim ban As Integer = 0

        Dim sql_caja As String = "exec llenar_caja(:num)"
        Dim cmd As New OracleCommand()
        cmd.Connection = con2
        cmd.CommandText = sql_caja
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("num", OracleType.UInt32).Value = cbx_paq.SelectedValue
        Dim MyDA As New OracleDataAdapter(cmd)
        Dim Ds As New DataSet
        Try
            con2.Open()
            cmd.ExecuteNonQuery()
            ban = 1
            con2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        con2.Close()

        If ban = 1 Then
            Dim sql_art As String = "exec llenar_detalle_caja(:num)"
            Try
                Dim cmd1 As New OracleCommand(sql_art, con2)
                cmd1.Parameters.Add("num", OracleType.UInt32).Value = cbx_paq.SelectedValue

                con2.Open()
                cmd1.ExecuteNonQuery()
                ban = 1
                con2.Close()
            Catch ex As Exception
                MessageBox.Show(e.ToString)
            End Try
        End If
    End Sub
End Class