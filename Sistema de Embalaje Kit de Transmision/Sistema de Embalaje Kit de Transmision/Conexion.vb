Imports System.Data.OracleClient

Module Conexion

    Dim cadena As String = "Data Source=172.16.1.16:1521/RFID; User id=EMBALAJE; password=embalaje01"
    Dim cadena2 As String = "Data Source=LOCALHOST:1521/orcl; User id=Despacho02; password=marmol01"

    Public con As New OracleConnection(cadena)
    Public con2 As New OracleConnection(cadena2)

    Public num As Integer
    Public user_global As Integer
    Public rol_global As Integer
End Module



