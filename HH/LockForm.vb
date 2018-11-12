Imports Symbol.RFID3
Imports Symbol.RFID3.TagAccess
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace VB_RFID3Sample5
    Public Class LockForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
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
            Me.mainMenu1 = New MainMenu
            Me.Password_TB = New TextBox
            Me.label2 = New Label
            Me.tagID_TB = New TextBox
            Me.tagMaskLabel = New Label
            Me.memBank_CB = New ComboBox
            Me.memBankLabel1 = New Label
            Me.lockPrivilege_CB = New ComboBox
            Me.lockPrivilegeLabel = New Label
            Me.lockButton = New Button
            MyBase.SuspendLayout()
            Me.Password_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.Password_TB.Location = New Point(&H56, &H25)
            Me.Password_TB.Name = "Password_TB"
            Me.Password_TB.Size = New Size(&H93, &H13)
            Me.Password_TB.TabIndex = 2
            Me.label2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label2.Location = New Point(7, &H27)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H54, 13)
            Me.label2.Text = "Password (Hex)"
            Me.tagID_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagID_TB.Location = New Point(&H56, 12)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&H93, &H13)
            Me.tagID_TB.TabIndex = 1
            Me.tagMaskLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMaskLabel.Location = New Point(7, 15)
            Me.tagMaskLabel.Name = "tagMaskLabel"
            Me.tagMaskLabel.Size = New Size(&H48, 13)
            Me.tagMaskLabel.Text = "Tag ID (Hex)"
            Me.memBank_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBank_CB.ForeColor = Color.Navy
            Me.memBank_CB.Items.Add("Kill Password")
            Me.memBank_CB.Items.Add("Access Password")
            Me.memBank_CB.Items.Add("EPC Memory")
            Me.memBank_CB.Items.Add("TID Memory")
            Me.memBank_CB.Items.Add("User Memory")
            Me.memBank_CB.Location = New Point(&H56, &H3E)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New Size(&H93, 20)
            Me.memBank_CB.TabIndex = 3
            Me.memBankLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBankLabel1.Location = New Point(7, &H41)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.Text = "Memory Bank"
            Me.lockPrivilege_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.lockPrivilege_CB.ForeColor = Color.Navy
            Me.lockPrivilege_CB.Items.Add("None")
            Me.lockPrivilege_CB.Items.Add("Read & Write")
            Me.lockPrivilege_CB.Items.Add("Perma Lock")
            Me.lockPrivilege_CB.Items.Add("Perma Unlock")
            Me.lockPrivilege_CB.Items.Add("Unlock")
            Me.lockPrivilege_CB.Location = New Point(&H56, 90)
            Me.lockPrivilege_CB.Name = "lockPrivilege_CB"
            Me.lockPrivilege_CB.Size = New Size(&H93, 20)
            Me.lockPrivilege_CB.TabIndex = 4
            Me.lockPrivilegeLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.lockPrivilegeLabel.Location = New Point(7, &H5D)
            Me.lockPrivilegeLabel.Name = "lockPrivilegeLabel"
            Me.lockPrivilegeLabel.Size = New Size(&H4A, 13)
            Me.lockPrivilegeLabel.Text = "Lock Privilege"
            Me.lockButton.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.lockButton.Location = New Point(&HA7, &H74)
            Me.lockButton.Name = "lockButton"
            Me.lockButton.Size = New Size(&H42, &H1B)
            Me.lockButton.TabIndex = 6
            Me.lockButton.Text = "Lock"
            AddHandler Me.lockButton.Click, New EventHandler(AddressOf Me.lockButton_Click)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.Password_TB)
            MyBase.Controls.Add(Me.label2)
            MyBase.Controls.Add(Me.tagID_TB)
            MyBase.Controls.Add(Me.tagMaskLabel)
            MyBase.Controls.Add(Me.memBank_CB)
            MyBase.Controls.Add(Me.memBankLabel1)
            MyBase.Controls.Add(Me.lockPrivilege_CB)
            MyBase.Controls.Add(Me.lockPrivilegeLabel)
            MyBase.Controls.Add(Me.lockButton)
            MyBase.Menu = Me.mainMenu1
            MyBase.Name = "LockForm"
            Me.Text = "Lock"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.LockForm_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub lockButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.m_LockParams.LockPrivilege = New LOCK_PRIVILEGE(5 - 1) {}
            If ((Not Me.m_AppForm.m_ReaderAPI Is Nothing) AndAlso Me.m_AppForm.m_IsConnected) Then
                Try
                    If (Me.tagID_TB.Text.Length = 0) Then
                        Me.m_AppForm.notifyUser("No TagID is defined", "Lock Operation")
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
                Me.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK)
                Me.lockButton.Enabled = False
            Else
                Me.m_AppForm.notifyUser("Please connect to a reader", "Lock Operation")
            End If
        End Sub

        Private Sub LockForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedIndices.Count > 0) Then
                Dim selectedIndex As Integer = Me.m_AppForm.inventoryList.SelectedIndices.Item(0)
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.Items.Item(selectedIndex)
                Me.tagID_TB.Text = item.Text
            End If
            Me.memBank_CB.SelectedIndex = CInt(Me.m_LockDataField)
            Me.lockPrivilege_CB.SelectedIndex = CInt(Me.m_LockPrivilege)
            Me.Password_TB.Text = Me.m_LockParams.AccessPassword.ToString("X")
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
        Private mainMenu1 As MainMenu
        Friend memBank_CB As ComboBox
        Private memBankLabel1 As Label
        Private Const NUM_MEMORY_BANKS As Integer = 5
        Private Password_TB As TextBox
        Private tagID_TB As TextBox
        Private tagMaskLabel As Label
    End Class
End Namespace

