Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports Symbol.RFID3.TagAccess

Namespace VB_RFID3Sample6
    Public Class WriteForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm, ByVal isBlockWrite As Boolean)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
            Me.m_IsBlockWrite = isBlockWrite
            If Me.m_IsBlockWrite Then
                Me.Text = "Block Write Tags"
            Else
                Me.Text = "Write Tags"
            End If
            Me.m_WriteParams = New WriteAccessParams
            Me.m_WriteParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER
            Me.m_WriteParams.AccessPassword = 0
            Me.m_WriteParams.ByteOffset = 0
            Me.m_WriteParams.WriteDataLength = 4
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
            Me.data_TB = New TextBox
            Me.label2 = New Label
            Me.Password_TB = New TextBox
            Me.label1 = New Label
            Me.accessFilterButton = New Button
            Me.writeButton = New Button
            Me.length_TB = New TextBox
            Me.lengthLabel = New Label
            Me.tagID_TB = New TextBox
            Me.tagMaskLabel = New Label
            Me.offset_TB = New TextBox
            Me.offsetLabel = New Label
            Me.memBank_CB = New ComboBox
            Me.memBankLabel1 = New Label
            MyBase.SuspendLayout()
            Me.data_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.data_TB.Location = New Point(&H51, &H68)
            Me.data_TB.Multiline = True
            Me.data_TB.Name = "data_TB"
            Me.data_TB.Size = New Size(&H9C, 50)
            Me.data_TB.TabIndex = 6
            Me.label2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label2.Location = New Point(3, &H71)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H48, 13)
            Me.label2.Text = "Data (Hex)"
            Me.Password_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.Password_TB.Location = New Point(&H51, &H1C)
            Me.Password_TB.Name = "Password_TB"
            Me.Password_TB.Size = New Size(&H9B, &H13)
            Me.Password_TB.TabIndex = 2
            Me.label1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label1.Location = New Point(3, &H20)
            Me.label1.Name = "label1"
            Me.label1.Size = New Size(&H55, 13)
            Me.label1.Text = "Password (Hex)"
            Me.accessFilterButton.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.accessFilterButton.Location = New Point(&H51, 160)
            Me.accessFilterButton.Name = "accessFilterButton"
            Me.accessFilterButton.Size = New Size(&H4B, &H17)
            Me.accessFilterButton.TabIndex = 7
            Me.accessFilterButton.Text = "Access Filter"
            AddHandler Me.accessFilterButton.Click, New EventHandler(AddressOf Me.accessFilterButton_Click)
            Me.writeButton.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.writeButton.Location = New Point(&HA3, &H9F)
            Me.writeButton.Name = "writeButton"
            Me.writeButton.Size = New Size(70, &H17)
            Me.writeButton.TabIndex = 8
            Me.writeButton.Text = "Write"
            AddHandler Me.writeButton.Click, New EventHandler(AddressOf Me.writeButton_Click)
            Me.length_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.length_TB.Location = New Point(&HBB, &H4F)
            Me.length_TB.Name = "length_TB"
            Me.length_TB.Size = New Size(50, &H13)
            Me.length_TB.TabIndex = 5
            Me.lengthLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.lengthLabel.Location = New Point(&H92, &H4C)
            Me.lengthLabel.Name = "lengthLabel"
            Me.lengthLabel.Size = New Size(40, &H22)
            Me.lengthLabel.Text = "Length (Bytes)"
            Me.tagID_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagID_TB.Location = New Point(&H51, 3)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&H9B, &H13)
            Me.tagID_TB.TabIndex = 1
            AddHandler Me.tagID_TB.TextChanged, New EventHandler(AddressOf Me.tagID_TB_TextChanged)
            Me.tagMaskLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMaskLabel.Location = New Point(3, 9)
            Me.tagMaskLabel.Name = "tagMaskLabel"
            Me.tagMaskLabel.Size = New Size(&H48, 13)
            Me.tagMaskLabel.Text = "Tag ID (Hex)"
            Me.offset_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offset_TB.Location = New Point(&H51, &H4F)
            Me.offset_TB.Name = "offset_TB"
            Me.offset_TB.Size = New Size(50, &H13)
            Me.offset_TB.TabIndex = 4
            Me.offsetLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offsetLabel.Location = New Point(3, &H53)
            Me.offsetLabel.Name = "offsetLabel"
            Me.offsetLabel.Size = New Size(&H48, 13)
            Me.offsetLabel.Text = "Offset (Bytes)"
            Me.memBank_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBank_CB.ForeColor = Color.Navy
            Me.memBank_CB.Items.Add("RESERVED")
            Me.memBank_CB.Items.Add("EPC")
            Me.memBank_CB.Items.Add("TID")
            Me.memBank_CB.Items.Add("USER")
            Me.memBank_CB.Location = New Point(&H51, &H35)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New Size(110, 20)
            Me.memBank_CB.TabIndex = 3
            Me.memBankLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBankLabel1.Location = New Point(3, &H39)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.Text = "Memory Bank"
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.data_TB)
            MyBase.Controls.Add(Me.label2)
            MyBase.Controls.Add(Me.Password_TB)
            MyBase.Controls.Add(Me.label1)
            MyBase.Controls.Add(Me.accessFilterButton)
            MyBase.Controls.Add(Me.writeButton)
            MyBase.Controls.Add(Me.length_TB)
            MyBase.Controls.Add(Me.lengthLabel)
            MyBase.Controls.Add(Me.tagID_TB)
            MyBase.Controls.Add(Me.tagMaskLabel)
            MyBase.Controls.Add(Me.offset_TB)
            MyBase.Controls.Add(Me.offsetLabel)
            MyBase.Controls.Add(Me.memBank_CB)
            MyBase.Controls.Add(Me.memBankLabel1)
            MyBase.Menu = Me.mainMenu1
            MyBase.Name = "WriteForm"
            Me.Text = "Write/Block Write"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.WriteForm_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub tagID_TB_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Not ((Me.tagID_TB.Text.Length <> 0) OrElse Me.accessFilterButton.Enabled) Then
                Me.accessFilterButton.Enabled = True
            ElseIf ((Me.tagID_TB.Text.Length > 0) AndAlso Me.accessFilterButton.Enabled) Then
                Me.accessFilterButton.Enabled = False
            End If
        End Sub

        Private Sub writeButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim length As UInt16 = UInt16.Parse(Me.length_TB.Text)
                If ((Me.tagID_TB.Text.Length = 0) AndAlso (Me.m_AppForm.m_AccessFilterForm.getFilter Is Nothing)) Then
                    Me.m_AppForm.notifyUser("No TagID or Access Filter is defined", "Write/BlockWrite Operation")
                ElseIf ((length * 2) > Me.data_TB.Text.Length) Then
                    Me.m_AppForm.notifyUser("Data length has to be a word size (2X)", "Write/BlockWrite Operation")
                ElseIf ((length Mod 2) <> 0) Then
                    Me.m_AppForm.notifyUser("Data length has to be a even number", "Write/BlockWrite Operation")
                Else
                    Dim accessPassword As String = Me.Password_TB.Text
                    Me.m_WriteParams.AccessPassword = 0
                    If (accessPassword.Length > 0) Then
                        If accessPassword.StartsWith("0x") Then
                            accessPassword = accessPassword.Substring(2, (accessPassword.Length - 2))
                        End If
                        Me.m_WriteParams.AccessPassword = UInt32.Parse(accessPassword, NumberStyles.HexNumber)
                    End If
                    Me.m_WriteParams.MemoryBank = DirectCast(Me.memBank_CB.SelectedIndex, MEMORY_BANK)
                    Me.m_WriteParams.ByteOffset = UInt16.Parse(Me.offset_TB.Text)
                    Me.m_WriteParams.WriteDataLength = length
                    Dim writeData As Byte() = New Byte(Me.m_WriteParams.WriteDataLength - 1) {}
                    Dim index As Integer = 0
                    Do While (index < Me.m_WriteParams.WriteDataLength)
                        writeData(index) = Byte.Parse(Me.data_TB.Text.Substring((index * 2), 2), NumberStyles.HexNumber)
                        writeData((index + 1)) = Byte.Parse(Me.data_TB.Text.Substring(((index + 1) * 2), 2), NumberStyles.HexNumber)
                        index = (index + 2)
                    Loop
                    Me.m_WriteParams.WriteData = writeData
                    Me.m_AppForm.m_SelectedTagID = Me.tagID_TB.Text
                    If Me.m_IsBlockWrite Then
                        Me.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE)
                    Else
                        Me.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE)
                    End If
                    Me.writeButton.Enabled = False
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Write/BlockWrite Operation")
            End Try
        End Sub

        Private Sub WriteForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedIndices.Count > 0) Then
                Dim selectedIndex As Integer = Me.m_AppForm.inventoryList.SelectedIndices.Item(0)
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.Items.Item(selectedIndex)
                Me.tagID_TB.Text = item.Text
            Else
                Me.tagID_TB.Text = ""
            End If
            Me.memBank_CB.SelectedIndex = CInt(Me.m_WriteParams.MemoryBank)
            Me.Password_TB.Text = Me.m_WriteParams.AccessPassword.ToString("X")
            Me.offset_TB.Text = Me.m_WriteParams.ByteOffset.ToString
            Me.length_TB.Text = Me.m_WriteParams.WriteDataLength.ToString
        End Sub


        ' Fields
        Private accessFilterButton As Button
        Private components As IContainer = Nothing
        Private data_TB As TextBox
        Private label1 As Label
        Private label2 As Label
        Friend length_TB As TextBox
        Private lengthLabel As Label
        Private m_AppForm As AppForm
        Private m_IsBlockWrite As Boolean
        Friend m_WriteParams As WriteAccessParams
        Private mainMenu1 As MainMenu
        Friend memBank_CB As ComboBox
        Private memBankLabel1 As Label
        Friend offset_TB As TextBox
        Private offsetLabel As Label
        Private Password_TB As TextBox
        Private tagID_TB As TextBox
        Private tagMaskLabel As Label
        Friend writeButton As Button
    End Class
End Namespace

