Imports System.Data.OracleClient
Public Class Conexion
    Dim cadena As String = "Data Source=LOCALHOST:1521; User id=eMBALAJE; password=uSI123"
    Public con As New OracleConnection(cadena)
End Class
