Imports System.Data.OracleClient

Public Class Frm_Mtn_Articulo

    Private Sub Frm_Mtn_Articulo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' rb_busq.Checked = True
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
End Class