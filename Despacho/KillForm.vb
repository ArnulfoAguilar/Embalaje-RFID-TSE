Imports Symbol.RFID3
Imports Symbol.RFID3.TagAccess
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace VB_RFID3_Host_Sample1
    Public Class KillForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
            Me.m_KillParams = New KillAccessParams
            Me.m_KillParams.KillPassword = 0
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.tagID_TB = New TextBox
            Me.tagIDLabel = New Label
            Me.killPwdLabel = New Label
            Me.killPwd_TB = New TextBox
            Me.killButton = New Button
            MyBase.SuspendLayout()
            Me.tagID_TB.Location = New Point(&H61, &H16)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&HAE, 20)
            Me.tagID_TB.TabIndex = 1
            Me.tagIDLabel.AutoSize = True
            Me.tagIDLabel.Location = New Point(&H13, &H19)
            Me.tagIDLabel.Name = "tagIDLabel"
            Me.tagIDLabel.Size = New Size(&H44, 13)
            Me.tagIDLabel.TabIndex = &H2D
            Me.tagIDLabel.Text = "Tag ID (Hex)"
            Me.killPwdLabel.Location = New Point(&H13, &H3D)
            Me.killPwdLabel.Name = "killPwdLabel"
            Me.killPwdLabel.Size = New Size(&H45, &H1D)
            Me.killPwdLabel.TabIndex = &H2B
            Me.killPwdLabel.Text = "Kill Password (Hex)"
            Me.killPwd_TB.Location = New Point(&H61, &H3F)
            Me.killPwd_TB.Name = "killPwd_TB"
            Me.killPwd_TB.Size = New Size(&HAE, 20)
            Me.killPwd_TB.TabIndex = 2
            Me.killButton.Location = New Point(&HCF, &HE5)
            Me.killButton.Name = "killButton"
            Me.killButton.Size = New Size(&H4B, &H1B)
            Me.killButton.TabIndex = 4
            Me.killButton.Text = "Kill"
            Me.killButton.UseVisualStyleBackColor = True
            AddHandler Me.killButton.Click, New EventHandler(AddressOf Me.killButton_Click)
            MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Font
            MyBase.ClientSize = New Size(&H126, &H10C)
            MyBase.Controls.Add(Me.killButton)
            MyBase.Controls.Add(Me.killPwd_TB)
            MyBase.Controls.Add(Me.tagID_TB)
            MyBase.Controls.Add(Me.tagIDLabel)
            MyBase.Controls.Add(Me.killPwdLabel)
            MyBase.FormBorderStyle = FormBorderStyle.FixedDialog
            MyBase.MaximizeBox = False
            MyBase.MinimizeBox = False
            MyBase.Name = "KillForm"
            MyBase.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Kill Tags"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Kill_Load)
            MyBase.ResumeLayout(False)
            MyBase.PerformLayout()
        End Sub

        Private Sub Kill_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedItems.Count > 0) Then
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.SelectedItems.Item(0)
                Me.tagID_TB.Text = item.Text
            Else
                Me.tagID_TB.Text = ""
            End If
            Me.killPwd_TB.Text = Me.m_KillParams.KillPassword.ToString("X")
        End Sub

        Private Sub killButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If (Me.tagID_TB.Text.Length = 0) Then
                    Me.m_AppForm.functionCallStatusLabel.Text = "No TagID is defined"
                Else
                    Me.m_KillParams.KillPassword = 0
                    If (Me.killPwd_TB.Text.Length > 0) Then
                        Dim killPassword As String = Me.killPwd_TB.Text
                        If killPassword.StartsWith("0x") Then
                            killPassword = killPassword.Substring(2)
                        End If
                        Me.m_KillParams.KillPassword = UInt32.Parse(killPassword, NumberStyles.HexNumber)
                    End If
                    Me.m_AppForm.m_SelectedTagID = Me.tagID_TB.Text
                    Me.m_AppForm.accessBackgroundWorker.RunWorkerAsync(ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL)
                    Me.killButton.Enabled = False
                End If
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Friend killButton As Button
        Private killPwd_TB As TextBox
        Private killPwdLabel As Label
        Private m_AppForm As AppForm
        Friend m_KillParams As KillAccessParams
        Private tagID_TB As TextBox
        Private tagIDLabel As Label
    End Class
End Namespace

