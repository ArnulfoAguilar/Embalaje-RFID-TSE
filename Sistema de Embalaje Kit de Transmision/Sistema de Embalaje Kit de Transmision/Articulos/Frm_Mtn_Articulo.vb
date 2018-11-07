Imports System.Data.OracleClient

Public Class Frm_Mtn_Articulo
    Dim bandera As Integer = 0
    Private Sub Frm_Mtn_Articulo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        rb_busq.Checked = True
        'combobox_depto()
    End Sub

    Private Sub rbt_numcaja_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbt_numcaja.CheckedChanged
        txt_numcaja.Enabled = True
        cbx_depto.Enabled = False
        cbx_muni.Enabled = False
        cbx_cv.Enabled = False
    End Sub

    Private Sub rb_busq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_busq.CheckedChanged
        txt_numcaja.Enabled = False
        cbx_depto.Enabled = True
        cbx_muni.Enabled = True
        cbx_cv.Enabled = True
    End Sub


    Private Sub combobox_depto()
        Dim sql As String = "select * from departamento"
        Try
            Dim cmd As New OracleCommand(sql, con2)
            Dim dataAdapter As New OracleDataAdapter(cmd)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.cbx_depto.DataSource = DT
            cbx_depto.ValueMember = "id_depto"
            cbx_depto.DisplayMember = "nombre_depto"
            bandera = 1
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub combobox_muni()
        If bandera = 1 Then
            Dim sql As String = "select id_muni, nombre_muni from municipio" & _
                                "where id_depto = :ID_DEPTO order by id_muni"
            Try
                Dim cmd As New OracleCommand(sql, con2)
                cmd.Parameters.Add(":ID_DEPTO", OracleType.VarChar, 30).Value = cbx_depto.SelectedValue.ToString
                Dim dataAdapter As New OracleDataAdapter(cmd)
                Dim DT As New DataTable
                dataAdapter.Fill(DT)
                Me.cbx_muni.DataSource = DT
                cbx_muni.ValueMember = "id_muni"
                cbx_muni.DisplayMember = "nombre_muni"
                bandera = 2
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub combobox_cv()
        If bandera = 2 Then
            Dim sql As String = "select id_centro, nombre_centro from centrovotacion" & _
                                "where id_depto = :ID_DEPTO and id_muni = :ID_MUNI order by id_centro"
            Try
                Dim cmd As New OracleCommand(sql, con2)
                cmd.Parameters.Add(":ID_DEPTO", OracleType.VarChar, 30).Value = cbx_depto.SelectedValue.ToString
                Dim dataAdapter As New OracleDataAdapter(cmd)
                Dim DT As New DataTable
                dataAdapter.Fill(DT)
                Me.cbx_muni.DataSource = DT
                cbx_muni.ValueMember = "id_muni"
                cbx_muni.DisplayMember = "nombre_muni"
                bandera = 2
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If rbt_numcaja.Checked = True Then
            Dim sql As String 'POR NUMERO DE CAJA
            sql = "select "
        End If
    End Sub
End Class