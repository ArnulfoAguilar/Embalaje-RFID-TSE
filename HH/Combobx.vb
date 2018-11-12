Imports System.Data

Public Class Combobx

    Private Sub Combobx_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ws As WSRFID.WebService1
        Dim DT As DataTable = ws.sedeLogistica()

        combo_sede.DataSource(DT)
    End Sub
End Class