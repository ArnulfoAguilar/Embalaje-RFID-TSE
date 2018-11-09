Imports System
Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.Resources
Imports System.Runtime.CompilerServices

Namespace VB_RFID3_Host_Sample1.Properties
    <DebuggerNonUserCode(), CompilerGenerated(), GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")> _
    Friend Class Resources
        ' Methods
        Friend Sub New()
        End Sub


        ' Properties
        Friend Shared ReadOnly Property connected() As Bitmap
            Get
                Return DirectCast(Resources.ResourceManager.GetObject("connected", Resources.resourceCulture), Bitmap)
            End Get
        End Property

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Friend Shared Property Culture() As CultureInfo
            Get
                Return Resources.resourceCulture
            End Get
            Set(ByVal value As CultureInfo)
                Resources.resourceCulture = value
            End Set
        End Property

        Friend Shared ReadOnly Property disconnected() As Bitmap
            Get
                Return DirectCast(Resources.ResourceManager.GetObject("disconnected", Resources.resourceCulture), Bitmap)
            End Get
        End Property

        Friend Shared ReadOnly Property notify() As Bitmap
            Get
                Return DirectCast(Resources.ResourceManager.GetObject("notify", Resources.resourceCulture), Bitmap)
            End Get
        End Property

        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Friend Shared ReadOnly Property ResourceManager() As ResourceManager
            Get
                If Object.ReferenceEquals(Resources.resourceMan, Nothing) Then
                    Dim temp As New ResourceManager("VB_RFID3_Host_Sample1.Properties.Resources", GetType(Resources).Assembly)
                    Resources.resourceMan = temp
                End If
                Return Resources.resourceMan
            End Get
        End Property


        ' Fields
        Private Shared resourceCulture As CultureInfo
        Private Shared resourceMan As ResourceManager
    End Class
End Namespace

