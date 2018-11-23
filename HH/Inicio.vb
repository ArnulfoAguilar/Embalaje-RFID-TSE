Imports VB_RFID3Sample5

Public Class Inicio
    Dim SedeExiste As Integer 'guardare la respuesta del Web Service, Si se encontro una Sede con ese ID
    Private Sub txt_Sede_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_Sede.KeyPress
        If Not IsNumeric(e.KeyChar) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub

    Private Sub btn_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_salir.Click
        Me.Close()
    End Sub

    Private Sub btn_aceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_aceptar.Click
        If txt_Sede.Text <> "" Then
            'Guardo en la variable, la respuesta del webservice 
            'mandando como parametro el Textbox
            SedeExiste = ws.comprobar_sede(txt_Sede.Text)
            If SedeExiste = 1 Then
                ' Verifico que hayan cajas listas para despacho en esa sede
                Dim hay_cajas As Integer
                hay_cajas = ws.CajasListasDespacho(txt_Sede.Text)
                If hay_cajas = 1 Then
                    id_sede = txt_Sede.Text
                    AppForm.Show()
                Else
                    lbl_error.Text = "Todas las cajas Verificadas de esta sede se han despachado"
                    lbl_error.Show()
                End If

            Else
                ' lbl_error.Text = "Esta  Ruta de Sede Logistica no Existe" + SedeExiste.ToString + txt_Sede.Text
                ' lbl_error.Show()
            End If
        Else
           ' lbl_error.Text = "Digite una ruta"
            lbl_error.Show()
        End If
    End Sub

    Private Sub Inicio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lbl_error.Hide()
    End Sub
End Class