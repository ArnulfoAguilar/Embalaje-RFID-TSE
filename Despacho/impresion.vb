Imports System.Windows.Forms
Imports System.Data.OracleClient
Imports System.Data
Imports System
Imports System.IO
Imports System.Text



Public Class impresion

    Private Sub btn_imprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_imprimir.Click
        
        Try
            ' Procedemos a crear el archivo de texto cuyo
            ' nombre será el que se haya especificado en
            ' el control TextBox.
            '
            CrearArchivoTexto(TextBox1.Text)

        Catch ex As Exception
            ' Se ha producido un error
            MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
    Dim cadena As String = "server = ORCL; User id = Arnulfo; Password = Ar13004; Unicode =True"
    Dim conn As New OracleConnection(cadena)
    Private Sub impresion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim sqlConsult As String = "Select codigo_barra, rfid from acta"
            Dim dataAdapter As New OracleDataAdapter(sqlConsult, conn)
            Dim DT As New DataTable
            dataAdapter.Fill(DT)
            Me.ComboBox1.DataSource = DT
            ComboBox1.DisplayMember = "nombre_ruta"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    ''Funcion 
    'Function GridAExcel(ByVal ElGrid As DataGridView) As Boolean
    '    'Creamos las variables
    '    Dim exApp As New Microsoft.Office.Interop.Excel.Application
    '    Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
    '    Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
    '    Try
    '        'Añadimos el Libro al programa, y la hoja al libro
    '        exLibro = exApp.Workbooks.Add
    '        exHoja = exLibro.Worksheets.Add()
    '        ' ¿Cuantas columnas y cuantas filas?
    '        Dim NCol As Integer = ElGrid.ColumnCount
    '        Dim NRow As Integer = ElGrid.RowCount
    '        'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
    '        For i As Integer = 1 To NCol
    '            exHoja.Cells.Item(1, i) = ElGrid.Columns(i - 1).Name.ToString
    '            exHoja.Cells.Item(1, i).HorizontalAlignment = 3
    '        Next
    '        For Fila As Integer = 0 To NRow - 1
    '            For Col As Integer = 0 To NCol - 1
    '                exHoja.Cells.Item(Fila + 2, Col + 1) = ElGrid.Rows(Fila).Cells(Col).Value
    '            Next
    '        Next
    '        'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
    '        exHoja.Rows.Item(1).Font.Bold = 1
    '        exHoja.Rows.Item(1).HorizontalAlignment = 3
    '        exHoja.Columns.AutoFit()
    '        'Aplicación visible
    '        exApp.Application.Visible = True
    '        exHoja = Nothing
    '        exLibro = Nothing
    '        exApp = Nothing
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error al exportar a Excel")
    '        Return False
    '    End Try
    '    Return True
    'End Function
    'Dim i As Integer
    'Dim a As Integer
    'Dim b As Integer
   

    Friend Sub CrearArchivoTexto(ByVal sFile As String)

        If (String.IsNullOrEmpty(sFile)) Then
            MessageBox.Show("No se ha especificado el nombre del archivo de texto.")
            ' Abandonamos el procedimiento
            Return
        End If

        If (DataGViewArticulos.RowCount = 0) Then
            MessageBox.Show("El datagridview esta vacio")
            ' Abandonamos el procedimiento
            Return
        End If

        ' Si no existe la carpeta la creamos.
        '
        Dim folder As String = "C:\Carpetatxt"
        If (Not Directory.Exists(folder)) Then
            Directory.CreateDirectory(folder)
        End If

        ' Si por casualidad se ha especificado la ruta en el nombre
        ' del archivo, nos quedamos solamente con su nombre.
        '
        sFile = IO.Path.GetFileName(sFile)

        ' Concatenamos la ruta de la carpeta al nombre del archivo.
        '
        sFile = IO.Path.Combine(folder, sFile)

        ' Si existe el archivo lo eliminamos.
        '
        If (File.Exists(sFile)) Then
            IO.File.Delete(sFile)
        End If

        ' GERENANDO EL ARCHIVO 
        Using f As New IO.StreamWriter(sFile + ".txt", True)

            ' AGREGANDO LAS COLUMNAS 
            Dim col As String = ""
            ' AGREGANDO LAS FILAS 
            Dim row As String = ""
            Dim i As Integer = 0
            For Each r As DataGridViewRow In DataGViewArticulos.Rows
                For Each c As DataGridViewColumn In DataGViewArticulos.Columns
                    row = row & Convert.ToString(r.Cells(c.HeaderText).Value) & ", "
                Next
                If i < DataGViewArticulos.Rows.Count - 1 Then row &= Environment.NewLine
            Next

            'AGREGANDO LA INFORMACION 
            f.WriteLine(row)
            MessageBox.Show("El archivo ha sido creado")
        End Using

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            Dim sqlConsult As String = "SELECT det.ID_PAQUETE_ELECTORAL, ar.DESCRIPCION, det.CODIGO_BARRA, CODIGO_RFID  FROM DETALLE_ARTICULO_PAQUETE det JOIN ARTICULO ar using(ID_ARTICULO) where ar.DESCRIPCION=:descripcion"
            Dim comando As New OracleCommand(sqlConsult, conn)
            comando.Parameters.Add(":descripcion", OracleType.Char, 50).Value = ComboBox1.Text
            Dim lector As OracleDataReader = Nothing

            conn.Open()

            lector = comando.ExecuteReader()
            If lector.HasRows Then

                Dim dataAdapter As New OracleDataAdapter(comando)
                Dim dataSet As New DataSet
                dataAdapter.Fill(dataSet, "Faltantes")
                Me.DataGViewArticulos.DataSource = dataSet.Tables("Faltantes")
                conn.Close()

            Else
                conn.Close()
                MessageBox.Show("Este articulo no se encuentra en la base de datos")
                'DataGViewArticulos.DataSource = null
                DataGViewArticulos.Columns.Clear()
                DataGViewArticulos.Refresh()
               
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class







