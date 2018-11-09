Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports Symbol.RFID3.TagAccess

Namespace VB_RFID3Sample6
    Public Class ReadForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
            Me.m_ReadParams = New ReadAccessParams
            Me.m_ReadParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC
            Me.m_ReadParams.AccessPassword = 0
            Me.m_ReadParams.ByteOffset = 0
            Me.m_ReadParams.ByteCount = 12
        End Sub

        Private Sub accessFilterButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.m_AppForm.m_AccessFilterForm.ShowDialog()
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
            Me.label1 = New Label
            Me.length_TB = New TextBox
            Me.lengthLabel = New Label
            Me.tagID_TB = New TextBox
            Me.tagMaskLabel = New Label
            Me.offset_TB = New TextBox
            Me.offsetLabel = New Label
            Me.memBank_CB = New ComboBox
            Me.memBankLabel1 = New Label
            Me.accessFilterButton = New Button
            Me.readButton = New Button
            Me.label2 = New Label
            Me.ReadData_TB = New TextBox
            MyBase.SuspendLayout()
            Me.Password_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.Password_TB.Location = New Point(&H52, &H1C)
            Me.Password_TB.Name = "Password_TB"
            Me.Password_TB.Size = New Size(&H9B, &H13)
            Me.Password_TB.TabIndex = 2
            Me.label1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label1.Location = New Point(5, &H21)
            Me.label1.Name = "label1"
            Me.label1.Size = New Size(&H57, 13)
            Me.label1.Text = "Password (Hex)"
            Me.length_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.length_TB.Location = New Point(&HBD, &H4F)
            Me.length_TB.Name = "length_TB"
            Me.length_TB.Size = New Size(&H30, &H13)
            Me.length_TB.TabIndex = 5
            Me.lengthLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.lengthLabel.Location = New Point(&H92, &H4C)
            Me.lengthLabel.Name = "lengthLabel"
            Me.lengthLabel.Size = New Size(&H2D, &H19)
            Me.lengthLabel.Text = "Length (Bytes)"
            Me.tagID_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagID_TB.Location = New Point(&H52, 3)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&H9B, &H13)
            Me.tagID_TB.TabIndex = 1
            AddHandler Me.tagID_TB.TextChanged, New EventHandler(AddressOf Me.tagMask_TB_TextChanged)
            Me.tagMaskLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMaskLabel.Location = New Point(5, 8)
            Me.tagMaskLabel.Name = "tagMaskLabel"
            Me.tagMaskLabel.Size = New Size(&H48, 13)
            Me.tagMaskLabel.Text = "Tag ID (Hex)"
            Me.offset_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offset_TB.Location = New Point(&H52, &H4F)
            Me.offset_TB.Name = "offset_TB"
            Me.offset_TB.Size = New Size(50, &H13)
            Me.offset_TB.TabIndex = 4
            Me.offsetLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offsetLabel.Location = New Point(5, &H53)
            Me.offsetLabel.Name = "offsetLabel"
            Me.offsetLabel.Size = New Size(&H47, 13)
            Me.offsetLabel.Text = "Offset (Bytes)"
            Me.memBank_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBank_CB.ForeColor = Color.Navy
            Me.memBank_CB.Items.Add("RESERVED")
            Me.memBank_CB.Items.Add("EPC")
            Me.memBank_CB.Items.Add("TID")
            Me.memBank_CB.Items.Add("USER")
            Me.memBank_CB.Location = New Point(&H52, &H35)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New Size(110, 20)
            Me.memBank_CB.TabIndex = 3
            Me.memBankLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBankLabel1.Location = New Point(5, &H39)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.Text = "Memory Bank"
            Me.accessFilterButton.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.accessFilterButton.Location = New Point(&H52, 160)
            Me.accessFilterButton.Name = "accessFilterButton"
            Me.accessFilterButton.Size = New Size(&H48, &H17)
            Me.accessFilterButton.TabIndex = 6
            Me.accessFilterButton.Text = "Access Filter"
            AddHandler Me.accessFilterButton.Click, New EventHandler(AddressOf Me.accessFilterButton_Click)
            Me.readButton.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.readButton.Location = New Point(160, 160)
            Me.readButton.Name = "readButton"
            Me.readButton.Size = New Size(&H4B, &H17)
            Me.readButton.TabIndex = 7
            Me.readButton.Text = "Read"
            AddHandler Me.readButton.Click, New EventHandler(AddressOf Me.readButton_Click)
            Me.label2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label2.Location = New Point(5, 110)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H47, &H1F)
            Me.label2.Text = "Data Read (Hex)"
            Me.ReadData_TB.Location = New Point(&H52, &H68)
            Me.ReadData_TB.Multiline = True
            Me.ReadData_TB.Name = "ReadData_TB"
            Me.ReadData_TB.ReadOnly = True
            Me.ReadData_TB.Size = New Size(&H9B, 50)
            Me.ReadData_TB.TabIndex = 15
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.ReadData_TB)
            MyBase.Controls.Add(Me.label2)
            MyBase.Controls.Add(Me.readButton)
            MyBase.Controls.Add(Me.accessFilterButton)
            MyBase.Controls.Add(Me.Password_TB)
            MyBase.Controls.Add(Me.label1)
            MyBase.Controls.Add(Me.length_TB)
            MyBase.Controls.Add(Me.lengthLabel)
            MyBase.Controls.Add(Me.tagID_TB)
            MyBase.Controls.Add(Me.tagMaskLabel)
            MyBase.Controls.Add(Me.offset_TB)
            MyBase.Controls.Add(Me.offsetLabel)
            MyBase.Controls.Add(Me.memBank_CB)
            MyBase.Controls.Add(Me.memBankLabel1)
            MyBase.Menu = Me.mainMenu1
            MyBase.Name = "ReadForm"
            Me.Text = "Read"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.ReadForm_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub readButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If ((Not Me.m_AppForm.m_ReaderAPI Is Nothing) AndAlso Me.m_AppForm.m_IsConnected) Then
                Try
                    If ((Me.tagID_TB.Text.Length = 0) AndAlso (Me.m_AppForm.m_AccessFilterForm.getFilter Is Nothing)) Then
                        Me.m_AppForm.notifyUser("No TagID or Access Filter is defined", "Read Operation")
                    Else
                        Dim accessPassword As String = Me.Password_TB.Text
                        Me.m_ReadParams.AccessPassword = 0
                        If (accessPassword.Length > 0) Then
                            If accessPassword.StartsWith("0x") Then
                                accessPassword = accessPassword.Substring(2, (accessPassword.Length - 2))
                            End If
                            Me.m_ReadParams.AccessPassword = UInt32.Parse(accessPassword, NumberStyles.HexNumber)
                        End If
                        Me.m_ReadParams.MemoryBank = DirectCast(Me.memBank_CB.SelectedIndex, MEMORY_BANK)
                        Me.m_ReadParams.ByteOffset = UInt16.Parse(Me.offset_TB.Text)
                        Me.m_ReadParams.ByteCount = UInt16.Parse(Me.length_TB.Text)
                        Me.ReadData_TB.Text = ""
                        Me.m_AppForm.m_SelectedTagID = Me.tagID_TB.Text
                        Me.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ)
                        Me.readButton.Enabled = False
                    End If
                Catch ex As Exception
                    Me.m_AppForm.notifyUser(ex.Message, "Read Operation")
                End Try
            Else
                Me.m_AppForm.notifyUser("Please connect to a reader", "Read Operation")
            End If
        End Sub

        Private Sub ReadForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedIndices.Count > 0) Then
                Dim selectedIndex As Integer = Me.m_AppForm.inventoryList.SelectedIndices.Item(0)
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.Items.Item(selectedIndex)
                Me.tagID_TB.Text = item.Text
            Else
                Me.tagID_TB.Text = ""
            End If
            Me.ReadData_TB.Text = ""
            Me.memBank_CB.SelectedIndex = CInt(Me.m_ReadParams.MemoryBank)
            Me.Password_TB.Text = Me.m_ReadParams.AccessPassword.ToString("X")
            Me.offset_TB.Text = Me.m_ReadParams.ByteOffset.ToString
            Me.length_TB.Text = Me.m_ReadParams.ByteCount.ToString
        End Sub

        Private Sub tagMask_TB_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Not ((Me.tagID_TB.Text.Length <> 0) OrElse Me.accessFilterButton.Enabled) Then
                Me.accessFilterButton.Enabled = True
            ElseIf ((Me.tagID_TB.Text.Length > 0) AndAlso Me.accessFilterButton.Enabled) Then
                Me.accessFilterButton.Enabled = False
            End If
        End Sub


        ' Fields
        Private accessFilterButton As Button
        Private components As IContainer = Nothing
        Private label1 As Label
        Private label2 As Label
        Friend length_TB As TextBox
        Private lengthLabel As Label
        Private m_AppForm As AppForm
        Friend m_ReadParams As ReadAccessParams = Nothing
        Private mainMenu1 As MainMenu
        Friend memBank_CB As ComboBox
        Private memBankLabel1 As Label
        Friend offset_TB As TextBox
        Private offsetLabel As Label
        Private Password_TB As TextBox
        Friend readButton As Button
        Friend ReadData_TB As TextBox
        Private tagID_TB As TextBox
        Private tagMaskLabel As Label
    End Class
End Namespace

