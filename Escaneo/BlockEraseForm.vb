Imports Symbol.RFID3
Imports Symbol.RFID3.TagAccess
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace VB_RFID3_Host_Sample1
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

        Private Sub BlockErase_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedItems.Count > 0) Then
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.SelectedItems.Item(0)
                Me.tagID_TB.Text = item.Text
            Else
                Me.tagID_TB.Text = ""
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
            Try
                If (Me.tagID_TB.Text.Length = 0) Then
                    Me.m_AppForm.functionCallStatusLabel.Text = "No TagID is defined"
                ElseIf ((Integer.Parse(Me.length_TB.Text) Mod 2) <> 0) Then
                    Me.m_AppForm.functionCallStatusLabel.Text = "Data length has to be a word size (2X)"
                Else
                    Me.m_AppForm.functionCallStatusLabel.Text = ""
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
                    Me.m_AppForm.accessBackgroundWorker.RunWorkerAsync(ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE)
                    Me.eraseButton.Enabled = False
                End If
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub

        Private Sub InitializeComponent()
            Me.eraseButton = New Button
            Me.length_TB = New TextBox
            Me.lengthLabel = New Label
            Me.tagID_TB = New TextBox
            Me.tagIDLabel = New Label
            Me.offset_TB = New TextBox
            Me.offsetLabel = New Label
            Me.memBank_CB = New ComboBox
            Me.memBankLabel1 = New Label
            Me.Password_TB = New TextBox
            Me.label2 = New Label
            MyBase.SuspendLayout()
            Me.eraseButton.Location = New Point(&HCF, &HE9)
            Me.eraseButton.Name = "eraseButton"
            Me.eraseButton.Size = New Size(&H4B, &H17)
            Me.eraseButton.TabIndex = 7
            Me.eraseButton.Text = "Erase"
            Me.eraseButton.UseVisualStyleBackColor = True
            AddHandler Me.eraseButton.Click, New EventHandler(AddressOf Me.eraseButton_Click)
            Me.length_TB.Location = New Point(&HF2, &H8A)
            Me.length_TB.Name = "length_TB"
            Me.length_TB.Size = New Size(40, 20)
            Me.length_TB.TabIndex = 5
            Me.lengthLabel.AutoSize = True
            Me.lengthLabel.Location = New Point(&H8B, &H8D)
            Me.lengthLabel.Name = "lengthLabel"
            Me.lengthLabel.Size = New Size(&H65, 13)
            Me.lengthLabel.TabIndex = &H2B
            Me.lengthLabel.Text = "No. of Bytes Length"
            Me.tagID_TB.Location = New Point(90, &H13)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&HC0, 20)
            Me.tagID_TB.TabIndex = 1
            Me.tagIDLabel.AutoSize = True
            Me.tagIDLabel.Location = New Point(12, &H16)
            Me.tagIDLabel.Name = "tagIDLabel"
            Me.tagIDLabel.Size = New Size(&H44, 13)
            Me.tagIDLabel.TabIndex = &H29
            Me.tagIDLabel.Text = "Tag ID (Hex)"
            Me.offset_TB.Location = New Point(90, &H8A)
            Me.offset_TB.Name = "offset_TB"
            Me.offset_TB.Size = New Size(40, 20)
            Me.offset_TB.TabIndex = 4
            Me.offsetLabel.AutoSize = True
            Me.offsetLabel.Location = New Point(12, &H8D)
            Me.offsetLabel.Name = "offsetLabel"
            Me.offsetLabel.Size = New Size(70, 13)
            Me.offsetLabel.TabIndex = &H27
            Me.offsetLabel.Text = "Offset (Bytes)"
            Me.memBank_CB.ForeColor = Color.Navy
            Me.memBank_CB.FormattingEnabled = True
            Me.memBank_CB.Items.AddRange(New Object() {"RESERVED", "EPC", "TID", "USER"})
            Me.memBank_CB.Location = New Point(90, &H61)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New Size(110, &H15)
            Me.memBank_CB.TabIndex = 3
            Me.memBankLabel1.AutoSize = True
            Me.memBankLabel1.Location = New Point(12, 100)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.TabIndex = &H25
            Me.memBankLabel1.Text = "Memory Bank"
            Me.Password_TB.Location = New Point(90, &H3A)
            Me.Password_TB.Name = "Password_TB"
            Me.Password_TB.Size = New Size(&HC0, 20)
            Me.Password_TB.TabIndex = 2
            Me.label2.AutoSize = True
            Me.label2.Location = New Point(12, &H3D)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H51, 13)
            Me.label2.TabIndex = &H3B
            Me.label2.Text = "Password (Hex)"
            MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Font
            MyBase.ClientSize = New Size(&H126, &H10C)
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
            MyBase.FormBorderStyle = FormBorderStyle.FixedDialog
            MyBase.MaximizeBox = False
            MyBase.MinimizeBox = False
            MyBase.Name = "BlockEraseForm"
            MyBase.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Block Erase"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.BlockErase_Load)
            MyBase.ResumeLayout(False)
            MyBase.PerformLayout()
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Friend eraseButton As Button
        Private label2 As Label
        Private length_TB As TextBox
        Private lengthLabel As Label
        Private m_AppForm As AppForm
        Friend m_BlockEraseParams As BlockEraseAccessParams
        Private memBank_CB As ComboBox
        Private memBankLabel1 As Label
        Private offset_TB As TextBox
        Private offsetLabel As Label
        Private Password_TB As TextBox
        Private tagID_TB As TextBox
        Private tagIDLabel As Label
    End Class
End Namespace

