Imports System.Data.OracleClient
Module Variables_Globales
    'Conexion a base de datos
    Dim cadena As String = "Data Source=172.16.1.16:1521/RFID; User id=EMBALAJE; password=embalaje01"
    Public conn As New OracleConnection(cadena)
    'Guardo el Id de la caja para verificar las inconsistencias
    Public id_caja As Integer
    ''Hago una variable Publica llamada IP READER que 
    'Utilizo en el ConnectionForm, para mandarle la ip de la antena automaticamente
    'Cada usuario ya debe tener establecido que antena USARA
    Public Ipreader As String
    'Datos del usuario
    Public ROL As String
    Public USUARIO As String
End Module
