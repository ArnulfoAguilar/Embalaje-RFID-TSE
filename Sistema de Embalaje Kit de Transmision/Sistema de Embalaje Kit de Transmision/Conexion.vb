Imports System.Data.OracleClient
Public Class Conexion
    Dim cadena As String = "Data Source=172.16.1.16:1521; User id=embalaje; password=embalaje01"
    Public con As New OracleConnection(cadena)
End Class
