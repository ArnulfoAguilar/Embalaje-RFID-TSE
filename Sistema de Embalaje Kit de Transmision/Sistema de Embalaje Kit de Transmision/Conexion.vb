Imports System.Data.OracleClient

Module Conexion
    Dim cadena As String = "Data Source=172.16.1.16:1521; User id=EMBALAJE; password=embalaje01"
    Dim cadena2 As String = "Data Source=LOCALHOST:1521/orcl; User id=Despacho01; password=marmol01"
    Public con As New OracleConnection(cadena)
    Public con2 As New OracleConnection(cadena2)

End Module

