Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports Symbol.RFID3.TagAccess

Namespace VB_RFID3Sample5
    Public Class BlockEraseForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
            Me.m_BlockEraseParams = New BlockEraseAccessParams
            Me.m_BlockEraseParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_USER
            Me.m_BlockEraseParams.AccessPassword = 0
            Me.m_BlockEraseParams.ByteOffset = 0
            Me.m_BlockEraseParams.ByteCount = 4
        End Sub

        Private Sub BlockEraseForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedIndices.Count > 0) Then
                Dim selectedIndex As Integer = Me.m_AppForm.inventoryList.SelectedIndices.Item(0)
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.Items.Item(selectedIndex)
                Me.tagID_TB.Text = item.Text
            End If
            Me.memBank_CB.SelectedIndex = CInt(Me.m_BlockEraseParams.MemoryBank)
            Me.Password_TB.Text = Me.m_BlockEraseParams.AccessPassword.ToString("X")
            Me.offset_TB.Text = Me.m_BlockEraseParams.ByteOffset.ToString
            Me.length_TB.Text = Me.m_BlockEraseParams.ByteCount.ToString
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub eraseButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If ((Not Me.m_AppForm.m_ReaderAPI Is Nothing) AndAlso Me.m_AppForm.m_IsConnected) Then
                Try
                    Dim length As Integer = Integer.Parse(Me.length_TB.Text)
                    If (Me.tagID_TB.Text.Length = 0) Then
                        Me.m_AppForm.notifyUser("No TagID is defined", "Block Erase Operation")
                    ElseIf ((length Mod 2) <> 0) Then
                        Me.m_AppForm.notifyUser("Data length has to be a word size (2X)", "Block Erase Operation")
                    Else
                        Me.m_BlockEraseParams.MemoryBank = DirectCast(Me.memBank_CB.SelectedIndex, MEMORY_BANK)
                        Me.m_BlockEraseParams.AccessPassword = 0
                        If (Me.Password_TB.Text.Length > 0) Then
                            Dim accessPassword As String = Me.Password_TB.Text
                            If accessPassword.StartsWith("0x") Then
                                accessPassword = accessPassword.Substring(2)
                            End If
                            Me.m_BlockEraseParams.AccessPassword = UInt32.Parse(accessPassword, NumberStyles.HexNumber)
                        End If
                        Me.m_BlockEraseParams.ByteOffset = UInt16.Parse(Me.offset_TB.Text)
                        Me.m_BlockEraseParams.ByteCount = UInt16.Parse(Me.length_TB.Text)
                        Me.m_AppForm.m_SelectedTagID = Me.tagID_TB.Text
                        Me.m_AppForm.RunAccessOps(ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE)
                        Me.eraseButton.Enabled = False
                    End If
                Catch ex As Exception
                    Me.m_AppForm.notifyUser(ex.Message, "Block Erase Operation")
                End Try
            Else
                Me.m_AppForm.notifyUser("Please connect to a reader", "Block Erase Operation")
            End If
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.Password_TB = New TextBox
            Me.label2 = New Label
            Me.eraseButton = New Button
            Me.length_TB = New TextBox
            Me.lengthLabel = New Label
            Me.tagID_TB = New TextBox
            Me.tagIDLabel = New Label
            Me.offset_TB = New TextBox
            Me.offsetLabel = New Label
            Me.memBank_CB = New ComboBox
            Me.memBankLabel1 = New Label
            MyBase.SuspendLayout()
            Me.Password_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.Password_TB.Location = New Point(&H51, &H22)
            Me.Password_TB.Name = "Password_TB"
            Me.Password_TB.Size = New Size(&H98, &H13)
            Me.Password_TB.TabIndex = 2
            Me.label2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label2.Location = New Point(4, &H24)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H54, 13)
            Me.label2.Text = "Password (Hex)"
            Me.eraseButton.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.eraseButton.Location = New Point(&HA9, &H8A)
            Me.eraseButton.Name = "eraseButton"
            Me.eraseButton.Size = New Size(&H40, &H17)
            Me.eraseButton.TabIndex = 7
            Me.eraseButton.Text = "Erase"
            AddHandler Me.eraseButton.Click, New EventHandler(AddressOf Me.eraseButton_Click)
            Me.length_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.length_TB.Location = New Point(&H52, &H6D)
            Me.length_TB.Name = "length_TB"
            Me.length_TB.Size = New Size(&H48, &H13)
            Me.length_TB.TabIndex = 5
            Me.lengthLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.lengthLabel.Location = New Point(4, &H70)
            Me.lengthLabel.Name = "lengthLabel"
            Me.lengthLabel.Size = New Size(&H54, 13)
            Me.lengthLabel.Text = "Length (Bytes)"
            Me.tagID_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagID_TB.Location = New Point(&H51, 9)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&H98, &H13)
            Me.tagID_TB.TabIndex = 1
            Me.tagIDLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagIDLabel.Location = New Point(4, 12)
            Me.tagIDLabel.Name = "tagIDLabel"
            Me.tagIDLabel.Size = New Size(&H47, 13)
            Me.tagIDLabel.Text = "Tag ID (Hex)"
            Me.offset_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offset_TB.Location = New Point(&H52, &H55)
            Me.offset_TB.Name = "offset_TB"
            Me.offset_TB.Size = New Size(&H48, &H13)
            Me.offset_TB.TabIndex = 4
            Me.offsetLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offsetLabel.Location = New Point(4, &H58)
            Me.offsetLabel.Name = "offsetLabel"
            Me.offsetLabel.Size = New Size(&H48, 13)
            Me.offsetLabel.Text = "Offset (Bytes)"
            Me.memBank_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBank_CB.ForeColor = Color.Navy
            Me.memBank_CB.Items.Add("RESERVED")
            Me.memBank_CB.Items.Add("EPC")
            Me.memBank_CB.Items.Add("TID")
            Me.memBank_CB.Items.Add("USER")
            Me.memBank_CB.Location = New Point(&H52, &H3B)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New Size(110, 20)
            Me.memBank_CB.TabIndex = 3
            Me.memBankLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBankLabel1.Location = New Point(4, &H3E)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.Text = "Memory Bank"
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.Password_TB)
            MyBase.Controls.Add(Me.label2)
            MyBase.Controls.Add(Me.eraseButton)
            MyBase.Controls.Add(Me.length_TB)
            MyBase.Controls.Add(Me.lengthLabel)
            MyBase.Controls.Add(Me.tagID_TB)
            MyBase.Controls.Add(Me.tagIDLabel)
            MyBase.Controls.Add(Me.offset_TB)
            MyBase.Controls.Add(Me.offsetLabel)
            MyBase.Controls.Add(Me.memBank_CB)
            MyBase.Controls.Add(Me.memBankLabel1)
            MyBase.Menu = Me.mainMenu1
            MyBase.Name = "BlockEraseForm"
            Me.Text = "Block Erase"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.BlockEraseForm_Load)
            MyBase.ResumeLayout(False)
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Friend eraseButton As Button
        Private label2 As Label
        Private length_TB As TextBox
        Private lengthLabel As Label
        Private m_AppForm As AppForm
        Friend m_BlockEraseParams As BlockEraseAccessParams
        Private mainMenu1 As MainMenu
        Private memBank_CB As ComboBox
        Private memBankLabel1 As Label
        Private offset_TB As TextBox
        Private offsetLabel As Label
        Private Password_TB As TextBox
        Private tagID_TB As TextBox
        Private tagIDLabel As Label
    End Class
End Namespace

