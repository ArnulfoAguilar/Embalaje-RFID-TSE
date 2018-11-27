Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms

Namespace VB_RFID3_Host_Sample1
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
            Dim arrReferencedAssmbNames As AssemblyName() = Assembly.GetExecutingAssembly.GetReferencedAssemblies
            Dim strAssmbName As AssemblyName
            For Each strAssmbName In arrReferencedAssmbNames
                If (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) = "Symbol.RFID3.Host") Then
                    Return strAssmbName.Version.ToString
                End If
            Next
            Return Nothing
        End Function

        Private Sub HelpForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Not Nothing Is Me.m_AppForm.m_ReaderAPI) Then
                Me.dllVersionLabel.Text = (Me.dllVersionLabel.Text & "C-Dll: " & Me.m_AppForm.m_ReaderAPI.VersionInfo.Version)
                Me.dllVersionLabel.Text = (Me.dllVersionLabel.Text & ", .NET-Dll: " & Me.GetSymbolDotNetDllVersion)
            End If
        End Sub

        Private Sub InitializeComponent()
            Me.button1 = New Button
            Me.versionLabel = New Label
            Me.copyRightLabel = New Label
            Me.dllVersionLabel = New Label
            MyBase.SuspendLayout()
            Me.button1.DialogResult = DialogResult.OK
            Me.button1.Location = New Point(&HE3, &H48)
            Me.button1.Name = "button1"
            Me.button1.Size = New Size(&H4B, &H17)
            Me.button1.TabIndex = 0
            Me.button1.Text = "OK"
            Me.button1.UseVisualStyleBackColor = True
            Me.versionLabel.AutoSize = True
            Me.versionLabel.Location = New Point(&H1C, &H11)
            Me.versionLabel.Name = "versionLabel"
            Me.versionLabel.Size = New Size(&H88, 13)
            Me.versionLabel.TabIndex = 1
            Me.versionLabel.Text = "VB_RFID3_Host_Sample1 "
            Me.copyRightLabel.AutoSize = True
            Me.copyRightLabel.Location = New Point(&H1C, &H3D)
            Me.copyRightLabel.Name = "copyRightLabel"
            Me.copyRightLabel.Size = New Size(&H5E, 13)
            Me.copyRightLabel.TabIndex = 2
            Me.copyRightLabel.Text = "Copyright (C) 2010"
            Me.dllVersionLabel.AutoSize = True
            Me.dllVersionLabel.Location = New Point(&H1C, &H27)
            Me.dllVersionLabel.Name = "dllVersionLabel"
            Me.dllVersionLabel.Size = New Size(0, 13)
            Me.dllVersionLabel.TabIndex = 3
            MyBase.AcceptButton = Me.button1
            MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Font
            MyBase.ClientSize = New Size(&H13A, &H6B)
            MyBase.Controls.Add(Me.dllVersionLabel)
            MyBase.Controls.Add(Me.copyRightLabel)
            MyBase.Controls.Add(Me.versionLabel)
            MyBase.Controls.Add(Me.button1)
            MyBase.FormBorderStyle = FormBorderStyle.FixedDialog
            MyBase.MaximizeBox = False
            MyBase.MinimizeBox = False
            MyBase.Name = "HelpForm"
            MyBase.StartPosition = FormStartPosition.CenterParent
            Me.Text = "About VB_RFID3_Host_Sample1"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.HelpForm_Load)
            MyBase.ResumeLayout(False)
            MyBase.PerformLayout()
        End Sub


        ' Fields
        Private button1 As Button
        Private components As IContainer = Nothing
        Private copyRightLabel As Label
        Private dllVersionLabel As Label
        Private m_AppForm As AppForm
        Friend versionLabel As Label
    End Class
End Namespace

