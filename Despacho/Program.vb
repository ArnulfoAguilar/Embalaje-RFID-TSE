Imports System
Imports System.Windows.Forms

Namespace VB_RFID3_Host_Sample1
    Friend Class Program
        ' Methods
        <STAThread()> _
        Public Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New AppForm)
        End Sub

    End Class
End Namespace

