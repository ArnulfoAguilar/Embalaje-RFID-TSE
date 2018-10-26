Imports Symbol.RFID3
Imports Symbol.RFID3.TagAccess
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace VB_RFID3_Host_Sample1
    Public Class LockForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
            Me.m_LockParams = New LockAccessParams
            Me.m_LockParams.AccessPassword = 0
            Me.m_LockDataField = LOCK_DATA_FIELD.LOCK_EPC_MEMORY
            Me.m_LockPrivilege = LOCK_PRIVILEGE.LOCK_PRIVILEGE_READ_WRITE
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.lockButton = New Button
            Me.lockPrivilege_CB = New ComboBox
            Me.lockPrivilegeLabel = New Label
            Me.Password_TB = New TextBox
            Me.label2 = New Label
            Me.tagID_TB = New TextBox
            Me.tagMaskLabel = New Label
            Me.memBank_CB = New ComboBox
            Me.memBankLabel1 = New Label
            MyBase.SuspendLayout()
            Me.lockButton.Location = New Point(&HCE, &HE5)
            Me.lockButton.Name = "lockButton"
            Me.lockButton.Size = New Size(&H4B, &H1B)
            Me.lockButton.TabIndex = 6
            Me.lockButton.Text = "Lock"
            Me.lockButton.UseVisualStyleBackColor = True
            AddHandler Me.lockButton.Click, New EventHandler(AddressOf Me.lockButton_Click)
            Me.lockPrivilege_CB.ForeColor = Color.Navy
            Me.lockPrivilege_CB.FormattingEnabled = True
            Me.lockPrivilege_CB.Items.AddRange(New Object() {"None", "Read & Write", "Perma Lock", "Perma Unlock", "Unlock"})
            Me.lockPrivilege_CB.Location = New Point(90, &H91)
            Me.lockPrivilege_CB.Name = "lockPrivilege_CB"
            Me.lockPrivilege_CB.Size = New Size(&H8B, &H15)
            Me.lockPrivilege_CB.TabIndex = 4
            Me.lockPrivilegeLabel.AutoSize = True
            Me.lockPrivilegeLabel.Location = New Point(9, &H94)
            Me.lockPrivilegeLabel.Name = "lockPrivilegeLabel"
            Me.lockPrivilegeLabel.Size = New Size(&H4A, 13)
            Me.lockPrivilegeLabel.TabIndex = &H31
            Me.lockPrivilegeLabel.Text = "Lock Privilege"
            Me.Password_TB.Location = New Point(90, &H3D)
            Me.Password_TB.Name = "Password_TB"
            Me.Password_TB.Size = New Size(&HBF, 20)
            Me.Password_TB.TabIndex = 2
            Me.label2.AutoSize = True
            Me.label2.Location = New Point(11, &H3D)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H51, 13)
            Me.label2.TabIndex = &H39
            Me.label2.Text = "Password (Hex)"
            Me.tagID_TB.Location = New Point(90, &H15)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&HBF, 20)
            Me.tagID_TB.TabIndex = 1
            Me.tagMaskLabel.AutoSize = True
            Me.tagMaskLabel.Location = New Point(11, &H18)
            Me.tagMaskLabel.Name = "tagMaskLabel"
            Me.tagMaskLabel.Size = New Size(40, 13)
            Me.tagMaskLabel.TabIndex = &H38
            Me.tagMaskLabel.Text = "Tag ID (Hex)"
            Me.memBank_CB.ForeColor = Color.Navy
            Me.memBank_CB.FormattingEnabled = True
            Me.memBank_CB.Items.AddRange(New Object() {"Kill Password", "Access Password", "EPC Memory", "TID Memory", "User Memory"})
            Me.memBank_CB.Location = New Point(90, &H67)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New Size(&H8B, &H15)
            Me.memBank_CB.TabIndex = 3
            Me.memBankLabel1.AutoSize = True
            Me.memBankLabel1.Location = New Point(11, &H6A)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.TabIndex = &H37
            Me.memBankLabel1.Text = "Memory Bank"
            MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Font
            MyBase.ClientSize = New Size(&H126, &H10C)
            MyBase.Controls.Add(Me.Password_TB)
            MyBase.Controls.Add(Me.label2)
            MyBase.Controls.Add(Me.tagID_TB)
            MyBase.Controls.Add(Me.tagMaskLabel)
            MyBase.Controls.Add(Me.memBank_CB)
            MyBase.Controls.Add(Me.memBankLabel1)
            MyBase.Controls.Add(Me.lockPrivilege_CB)
            MyBase.Controls.Add(Me.lockPrivilegeLabel)
            MyBase.Controls.Add(Me.lockButton)
            MyBase.FormBorderStyle = FormBorderStyle.FixedDialog
            MyBase.MaximizeBox = False
            MyBase.MinimizeBox = False
            MyBase.Name = "LockForm"
            MyBase.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Lock Tags"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Lock_Load)
            MyBase.ResumeLayout(False)
            MyBase.PerformLayout()
        End Sub

        Private Sub Lock_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedItems.Count > 0) Then
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.SelectedItems.Item(0)
                Me.tagID_TB.Text = item.Text
            Else
                Me.tagID_TB.Text = ""
            End If
            Me.memBank_CB.SelectedIndex = CInt(Me.m_LockDataField)
            Me.lockPrivilege_CB.SelectedIndex = CInt(Me.m_LockPrivilege)
            Me.Password_TB.Text = Me.m_LockParams.AccessPassword.ToString("X")
        End Sub

        Private Sub lockButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.m_LockParams.LockPrivilege = New LOCK_PRIVILEGE(5 - 1) {}
            Try
                If (Me.tagID_TB.Text.Length = 0) Then
                    Me.m_AppForm.functionCallStatusLabel.Text = "No TagID is defined"
                    Return
                End If
                Me.m_LockParams.AccessPassword = 0
                If (Me.Password_TB.Text.Length > 0) Then
                    Dim password As String = Me.Password_TB.Text
                    If password.StartsWith("0x") Then
                        password = password.Substring(2, (password.Length - 2))
                    End If
                    Me.m_LockParams.AccessPassword = UInt32.Parse(password, NumberStyles.HexNumber)
                End If
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message.ToString
            End Try
            Me.m_LockDataField = DirectCast(Me.memBank_CB.SelectedIndex, LOCK_DATA_FIELD)
            Me.m_LockPrivilege = DirectCast(Me.lockPrivilege_CB.SelectedIndex, LOCK_PRIVILEGE)
            Me.m_LockParams.LockPrivilege(Me.memBank_CB.SelectedIndex) = DirectCast(Me.lockPrivilege_CB.SelectedIndex, LOCK_PRIVILEGE)
            Me.m_AppForm.m_SelectedTagID = Me.tagID_TB.Text
            Me.m_AppForm.accessBackgroundWorker.RunWorkerAsync(ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK)
            Me.lockButton.Enabled = False
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private label2 As Label
        Friend lockButton As Button
        Private lockPrivilege_CB As ComboBox
        Private lockPrivilegeLabel As Label
        Private m_AppForm As AppForm
        Private m_LockDataField As LOCK_DATA_FIELD
        Friend m_LockParams As LockAccessParams
        Private m_LockPrivilege As LOCK_PRIVILEGE
        Friend memBank_CB As ComboBox
        Private memBankLabel1 As Label
        Private Const NUM_MEMORY_BANKS As Integer = 5
        Private Password_TB As TextBox
        Private tagID_TB As TextBox
        Private tagMaskLabel As Label
    End Class
End Namespace

