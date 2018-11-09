Imports System.Data.OracleClient
Module Conexion
    Dim cadena As String = "Data Source=172.16.1.16:1521/RFID; User id=EMBALAJE; password=embalaje01"
    Public conn As New OracleConnection(cadena)
    Public id_caja As Integer
End Module
