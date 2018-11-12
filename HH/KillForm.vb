Imports Symbol.RFID3
Imports Symbol.RFID3.TagAccess
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace VB_RFID3Sample5
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
            Me.mainMenu1 = New MainMenu
            Me.killButton = New Button
            Me.killPwd_TB = New TextBox
            Me.tagID_TB = New TextBox
            Me.tagIDLabel = New Label
            Me.killPwdLabel = New Label
            MyBase.SuspendLayout()
            Me.killButton.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.killButton.Location = New Point(&HA8, &H53)
            Me.killButton.Name = "killButton"
            Me.killButton.Size = New Size(&H42, &H1B)
            Me.killButton.TabIndex = 4
            Me.killButton.Text = "Kill"
            AddHandler Me.killButton.Click, New EventHandler(AddressOf Me.killButton_Click)
            Me.killPwd_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.killPwd_TB.Location = New Point(&H51, &H30)
            Me.killPwd_TB.Name = "killPwd_TB"
            Me.killPwd_TB.Size = New Size(&H99, &H13)
            Me.killPwd_TB.TabIndex = 2
            Me.tagID_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagID_TB.Location = New Point(&H51, 14)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&H99, &H13)
            Me.tagID_TB.TabIndex = 1
            Me.tagIDLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagIDLabel.Location = New Point(6, &H12)
            Me.tagIDLabel.Name = "tagIDLabel"
            Me.tagIDLabel.Size = New Size(&H45, 13)
            Me.tagIDLabel.Text = "Tag ID (Hex)"
            Me.killPwdLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.killPwdLabel.Location = New Point(6, &H2D)
            Me.killPwdLabel.Name = "killPwdLabel"
            Me.killPwdLabel.Size = New Size(&H4F, &H1C)
            Me.killPwdLabel.Text = "Kill Password (Hex)"
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.killButton)
            MyBase.Controls.Add(Me.killPwd_TB)
            MyBase.Controls.Add(Me.tagID_TB)
            MyBase.Controls.Add(Me.tagIDLabel)
            MyBase.Controls.Add(Me.killPwdLabel)
            MyBase.Menu = Me.mainMenu1
            MyBase.Name = "KillForm"
            Me.Text = "Kill"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.KillForm_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub killButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If ((Not Me.m_AppForm.m_ReaderAPI Is Nothing) AndAlso Me.m_AppForm.m_IsConnected) Then
                Try
                    If (Me.tagID_TB.Text.Length = 0) Then
                        Me.m_AppForm.notifyUser("No TagID is defined", "Kill Operation")
                        Return
                    End If
                    Dim killPassword As String = Me.killPwd_TB.Text
                    If killPassword.StartsWith("0x") Then
                        killPassword = killPassword.Substring(2)
                    End If
                    Me.m_KillParams.KillPassword = UInt32.Parse(killPassword, NumberStyles.HexNumber)
                Catch ex As Exception
                    Me.m_AppForm.notifyUser(ex.Message, "Kill Operation")
                End Try
                Me.m_AppForm.m_SelectedTagID = Me.tagID_TB.Text
                Me.killButton.Enabled = False
                Me.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL)
            Else
                Me.m_AppForm.notifyUser("Please connect to a reader", "Kill Operation")
            End If
        End Sub

        Private Sub KillForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedIndices.Count > 0) Then
                Dim selectedIndex As Integer = Me.m_AppForm.inventoryList.SelectedIndices.Item(0)
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.Items.Item(selectedIndex)
                Me.tagID_TB.Text = item.Text
            End If
            Me.killPwd_TB.Text = Me.m_KillParams.KillPassword.ToString("X")
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Friend killButton As Button
        Private killPwd_TB As TextBox
        Private killPwdLabel As Label
        Private m_AppForm As AppForm
        Friend m_KillParams As KillAccessParams
        Private mainMenu1 As MainMenu
        Private tagID_TB As TextBox
        Private tagIDLabel As Label
    End Class
End Namespace

