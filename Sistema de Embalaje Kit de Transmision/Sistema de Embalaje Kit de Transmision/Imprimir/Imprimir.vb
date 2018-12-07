Imports System.Data.OracleClient
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Imprimir


    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Frm_principal.Show()
        Me.Close()
    End Sub
    Private Sub cargar_combobox_paquetes()
        Try

            Dim sqlConsult As String = "select ID_PAQUETE, NOMBRE_PAQUETE from PAQUETE"
            Dim dataAdapter As New OracleDataAdapter(sqlConsult, con)
            Dim DT As New System.Data.DataTable
            dataAdapter.Fill(DT)
            Me.comboSede.DataSource = DT
            ComboPAQUETE.ValueMember = "ID_PAQUETE"
            ComboPAQUETE.DisplayMember = "NOMBRE_PAQUETE"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub cargar_combobox_sedes()
        Try

            Dim sqlConsult As String = "select ID_SEDE, NOMBRE_SEDE from SEDE_LOGISTICA"
            Dim dataAdapter As New OracleDataAdapter(sqlConsult, con)
            Dim DT As New System.Data.DataTable
            dataAdapter.Fill(DT)
            Me.comboSede.DataSource = DT
            comboSede.ValueMember = "ID_SEDE"
            comboSede.DisplayMember = "NOMBRE_SEDE"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub cargar_combobox_articulos()
        Try

            Dim sqlConsult As String = "select ID_ARTICULO, NOMBRE_ARTICULO from ARTICULO"
            Dim dataAdapter As New OracleDataAdapter(sqlConsult, con)
            Dim DT As New System.Data.DataTable
            dataAdapter.Fill(DT)
            Me.comboArticulo.DataSource = DT
            comboArticulo.ValueMember = "ID_ARTICULO"
            comboArticulo.DisplayMember = "NOMBRE_ARTICULO"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Imprimir_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cargar_combobox_sedes()
        cargar_combobox_articulos()
        CARGAR_COMBOBOX_PAQUETES()
        rb_articulo.Checked = True

    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_caja.KeyPress
        If Not IsNumeric(e.KeyChar) And e.KeyChar <> vbBack Then
            e.Handled = True
        End If
    End Sub
    Private Sub Checked()
        If rb_articulo.Checked Then
            comboArticulo.Enabled = True
            comboArticulo.Show()
            lbl_articulo.Show()
            comboSede.Enabled = True
            comboSede.Show()
            lbl_sede.Show()
            txt_caja.Enabled = False
            txt_caja.Hide()
            lbl_caja.Hide()

        ElseIf rb_caja.Checked Then
            comboArticulo.Enabled = False
            comboArticulo.Hide()
            lbl_articulo.Hide()
            comboSede.Enabled = False
            comboSede.Hide()
            lbl_sede.Hide()
            txt_caja.Enabled = True
            txt_caja.Show()
            lbl_caja.Show()
        ElseIf rb_individual.Checked Then
            comboArticulo.Enabled = True
            comboArticulo.Show()
            lbl_articulo.Show()
            comboSede.Enabled = False
            comboSede.Hide()
            lbl_sede.Hide()
            txt_caja.Enabled = True
            txt_caja.Show()
            lbl_caja.Show()
        End If
    End Sub

    Private Sub rb_articulo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_articulo.CheckedChanged
        Checked()
    End Sub

    Private Sub rb_caja_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_caja.CheckedChanged
        Checked()
    End Sub

    Private Sub rb_individual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_individual.CheckedChanged
        Checked()
    End Sub

    Private Sub to_Excel()
        Try
            ' Creamos todo lo necesario para un excel
            Dim appXL As Excel.Application
            Dim wbXl As Excel.Workbook
            Dim shXL As Excel.Worksheet
            Dim indice As Integer = 2
            appXL = CreateObject("Excel.Application")
            appXL.Visible = False 'Para que no se muestre mientras se crea
            wbXl = appXL.Workbooks.Add
            shXL = wbXl.ActiveSheet

            ' Añadimos las cabeceras de las columnas
            Dim formatRange As Excel.Range
            formatRange = shXL.Range("a2,b2")
            formatRange.EntireColumn.NumberFormat = "@"
            shXL.Cells(1, 1).Value = "CAJA"
            shXL.Cells(1, 2).Value = "ARTICULO"
            shXL.Cells(1, 3).Value = "MUNICIPIO"
            shXL.Cells(1, 4).Value = "SEDE"
            shXL.Cells(1, 5).Value = "DEPARTAMENTO"
            shXL.Cells(1, 6).Value = "CODIGO_BARRA"
            shXL.Cells(1, 7).Value = "RFID"

            ' Obtenemos los datos que queremos exportar desde base de datos.
            'Elegimos la consulta dependiendo de el radiobutton seleccionado
            Dim consulta As String
            If rb_articulo.Checked Then

                consulta = " SELECT  " & _
                            " C.ID_CAJA CAJA, " & _
                            " A.NOMBRE_ARTICULO ARTICULO," & _
                            " MUN.NOMBRE_MUNI MUNICIPIO," & _
                            " SED.NOMBRE_SEDE SEDE," & _
                            " DEP.NOMBRE_DEPTO DEPARTAMENTO, " & _
                            " DET.CODEBAR CODIGO_BARRA," & _
                            " DET.RFID " & _
                            " FROM DETALLE_CAJA det" & _
                            " join ARTICULO a         on A.ID_ARTICULO=DET.ID_ARTICULO" & _
                            " join caja c             on DET.ID_CAJA = C.ID_CAJA" & _
                            " join paquete p          on DET.ID_PAQUETE=P.ID_PAQUETE" & _
                            " join MUNICIPIO mun      on C.ID_MUNI=MUN.ID_MUNI" & _
                            " join SEDE_LOGISTICA sed on C.ID_SEDE=SED.ID_SEDE" & _
                            " join DEPARTAMENTO dep   on C.ID_DEPTO=DEP.ID_DEPTO" & _
                            " where" & _
                            " C.ID_PAQUETE=P.ID_PAQUETE   AND" & _
                            " MUN.ID_SEDE=SED.ID_SEDE     AND" & _
                            " SED.ID_DEPTO=DEP.ID_DEPTO   AND" & _
                            " DET.ID_ARTICULO=:ARTICULO   AND " & _
                            " C.ID_SEDE=:SEDE             AND" & _
                            " DET.ID_PAQUETE=:PAQUETE"
            ElseIf rb_caja.Checked Then
                consulta = " SELECT  " & _
                            " C.ID_CAJA CAJA, " & _
                            " A.NOMBRE_ARTICULO ARTICULO," & _
                            " MUN.NOMBRE_MUNI MUNICIPIO," & _
                            " SED.NOMBRE_SEDE SEDE," & _
                            " DEP.NOMBRE_DEPTO DEPARTAMENTO, " & _
                            " DET.CODEBAR CODIGO_BARRA," & _
                            " DET.RFID " & _
                            " FROM DETALLE_CAJA det" & _
                            " join ARTICULO a         on A.ID_ARTICULO=DET.ID_ARTICULO" & _
                            " join caja c             on DET.ID_CAJA = C.ID_CAJA" & _
                            " join paquete p          on DET.ID_PAQUETE=P.ID_PAQUETE" & _
                            " join MUNICIPIO mun      on C.ID_MUNI=MUN.ID_MUNI" & _
                            " join SEDE_LOGISTICA sed on C.ID_SEDE=SED.ID_SEDE" & _
                            " join DEPARTAMENTO dep   on C.ID_DEPTO=DEP.ID_DEPTO" & _
                            " where" & _
                            " C.ID_PAQUETE=P.ID_PAQUETE   AND" & _
                            " MUN.ID_SEDE=SED.ID_SEDE     AND" & _
                            " SED.ID_DEPTO=DEP.ID_DEPTO   AND" & _
                            " DET.ID_CAJA=:CAJA   AND " & _
                            " DET.ID_PAQUETE=:PAQUETE"
            ElseIf rb_individual.Checked Then

                consulta = " SELECT  " & _
                            " C.ID_CAJA CAJA, " & _
                            " A.NOMBRE_ARTICULO ARTICULO," & _
                            " MUN.NOMBRE_MUNI MUNICIPIO," & _
                            " SED.NOMBRE_SEDE SEDE," & _
                            " DEP.NOMBRE_DEPTO DEPARTAMENTO, " & _
                            " DET.CODEBAR CODIGO_BARRA," & _
                            " DET.RFID " & _
                            " FROM DETALLE_CAJA det" & _
                            " join ARTICULO a         on A.ID_ARTICULO=DET.ID_ARTICULO" & _
                            " join caja c             on DET.ID_CAJA = C.ID_CAJA" & _
                            " join paquete p          on DET.ID_PAQUETE=P.ID_PAQUETE" & _
                            " join MUNICIPIO mun      on C.ID_MUNI=MUN.ID_MUNI" & _
                            " join SEDE_LOGISTICA sed on C.ID_SEDE=SED.ID_SEDE" & _
                            " join DEPARTAMENTO dep   on C.ID_DEPTO=DEP.ID_DEPTO" & _
                            " where" & _
                            " C.ID_PAQUETE=P.ID_PAQUETE   AND" & _
                            " MUN.ID_SEDE=SED.ID_SEDE     AND" & _
                            " SED.ID_DEPTO=DEP.ID_DEPTO   AND" & _
                            " DET.ID_CAJA=:CAJA   AND " & _
                            " DET.ID_ARTICULO=:ARTICULO " & _
                            " DET.ID_PAQUETE=:PAQUETE"
            End If

            Dim myCommand As New OracleCommand(consulta, con)
            If rb_articulo.Checked Then
                myCommand.Parameters.Add(":ARTICULO", OracleType.Int32, 30).Value = comboArticulo.SelectedValue.ToString
                myCommand.Parameters.Add(":SEDE", OracleType.Int32, 30).Value = comboSede.SelectedValue.ToString
                myCommand.Parameters.Add(":PAQUETE", OracleType.Int32, 30).Value = ComboPAQUETE.SelectedValue.ToString
            ElseIf rb_caja.Checked Then
                myCommand.Parameters.Add(":CAJA", OracleType.VarChar, 30).Value = txt_caja.Text
                myCommand.Parameters.Add(":PAQUETE", OracleType.Int32, 30).Value = ComboPAQUETE.SelectedValue.ToString
            ElseIf rb_individual.Checked Then
                myCommand.Parameters.Add(":ID_ARTICULO", OracleType.Int32, 30).Value = comboArticulo.SelectedValue.ToString
                myCommand.Parameters.Add(":ID_CAJA", OracleType.VarChar, 30).Value = txt_caja.Text
                myCommand.Parameters.Add(":PAQUETE", OracleType.Int32, 30).Value = ComboPAQUETE.SelectedValue.ToString
            End If
            con.Open()
            Dim myReader As OracleDataReader = myCommand.ExecuteReader()
            If myReader.HasRows Then
                While myReader.Read()
                    ' Cargamos la información en el excel
                    shXL.Cells(indice, 1).Value = myReader("CAJA")
                    shXL.Cells(indice, 2).Value = myReader("ARTICULO")
                    shXL.Cells(indice, 3).Value = myReader("MUNICIPIO")
                    shXL.Cells(indice, 4).Value = myReader("SEDE")
                    shXL.Cells(indice, 5).Value = myReader("DEPARTAMENTO")
                    shXL.Cells(indice, 6).Value = myReader("CODIGO_BARRA")
                    shXL.Cells(indice, 7).Value = myReader("RFID")
                    indice += 1
                End While
                ' Cerramos el reader
                myReader.Close()
                ' Mostramos un dialog para que el usuario indique donde quiere guardar el excel
                Dim saveFileDialog1 As New SaveFileDialog()
                saveFileDialog1.Title = "Guardar documento Excel"
                saveFileDialog1.Filter = "Excel File|*.xlsx"
                saveFileDialog1.FileName = "Codigos para viñetas " + DateTime.Now.ToString("dd_MM_yyyy")
                saveFileDialog1.ShowDialog()
                ' Guardamos el excel en la ruta que ha especificado el usuario
                wbXl.SaveAs(saveFileDialog1.FileName)
                ' Cerramos el workbook
                appXL.Workbooks.Close()
                ' Eliminamos el objeto excel
                appXL.Quit()
            Else
                myReader.Close()
                con.Close()
                MessageBox.Show("No se tienen registros")
            End If


        Catch ex As Exception
            MessageBox.Show("Errorxportar los datos a excel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Cerramos la conexión y ponemos el cursor por defecto de nuevo
            con.Close()
        End Try
    End Sub

    Private Sub btn_Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Excel.Click
        to_Excel()
    End Sub
End Class