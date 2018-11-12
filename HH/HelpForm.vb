Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms

Namespace VB_RFID3Sample5
    Public Class HelpForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function GetSymbolDotNetDllVersion() As String
            Dim myAssembly As Assembly = Assembly.LoadFrom("Symbol.RFID3.Device.dll")
            If (Not myAssembly Is Nothing) Then
                Return myAssembly.GetName.Version.ToString
            End If
            Return Nothing
        End Function

        Private Sub HelpForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Not Nothing Is Me.m_AppForm.m_ReaderAPI) Then
                Me.dllVersionLabel.Text = (Me.dllVersionLabel.Text & "C-Dll: " & Me.m_AppForm.m_ReaderAPI.VersionInfo.Version)
                Me.dllVersionLabel.Text = (Me.dllVersionLabel.Text & ", .NET-Dll: " & Me.GetSymbolDotNetDllVersion)
            End If
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.versionLabel = New Label
            Me.copyRightLabel = New Label
            Me.dllVersionLabel = New Label
            MyBase.SuspendLayout()
            Me.versionLabel.Location = New Point(12, &H23)
            Me.versionLabel.Name = "versionLabel"
            Me.versionLabel.Size = New Size(&HC3, 20)
            Me.versionLabel.Text = "VB_RFID3Sample5"
            Me.copyRightLabel.Location = New Point(12, &H5E)
            Me.copyRightLabel.Name = "copyRightLabel"
            Me.copyRightLabel.Size = New Size(&H7B, 20)
            Me.copyRightLabel.Text = "Copyright (C) 2010"
            Me.dllVersionLabel.Location = New Point(12, &H41)
            Me.dllVersionLabel.Name = "dllVersionLabel"
            Me.dllVersionLabel.Size = New Size(&HD8, 20)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.dllVersionLabel)
            MyBase.Controls.Add(Me.copyRightLabel)
            MyBase.Controls.Add(Me.versionLabel)
            MyBase.Menu = Me.mainMenu1
            MyBase.Name = "HelpForm"
            Me.Text = "About VB_RFID3Sample5"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.HelpForm_Load)
            MyBase.ResumeLayout(False)
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private copyRightLabel As Label
        Private dllVersionLabel As Label
        Private m_AppForm As AppForm
        Private mainMenu1 As MainMenu
        Private versionLabel As Label
    End Class
End Namespace

