
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
        Dim cmd As New OracleCommand()
        Try
            With cmd
                .CommandType = CommandType.StoredProcedure
                .CommandText = "llenar_caja"
                Dim p_num As New OracleParameter
                p_num.ParameterName = "paq"
                p_num.OracleType = OracleType.Int32
                p_num.Direction = ParameterDirection.Input
                p_num.Value = cbx_paq.SelectedValue
                .Parameters.Add(p_num)
                .Connection = con2
                .Connection.Open()
                .ExecuteNonQuery()
                .Connection.Close()
                .Dispose()
            End With
            MessageBox.Show("La hizo Prro")
            ban = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        con2.Close()

        If ban = 1 Then
            Dim sql_art As String = "exec llenar_detalle_caja(:num)"
            Dim cmd1 As New OracleCommand()
            Try
                With cmd1
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "llenar_detalle_caja"
                    Dim p_num1 As New OracleParameter
                    p_num1.ParameterName = "paq"
                    p_num1.OracleType = OracleType.Int32
                    p_num1.Direction = ParameterDirection.Input
                    p_num1.Value = cbx_paq.SelectedValue
                    .Parameters.Add(p_num1)
                    .Connection = con2
                    .Connection.Open()
                    .ExecuteNonQuery()
                    .Connection.Close()
                    .Dispose()
                End With
                MessageBox.Show("La hizo Prro x2")
            Catch ex As Exception
                MessageBox.Show(e.ToString)
            End Try
        End If
    End Sub
End Class