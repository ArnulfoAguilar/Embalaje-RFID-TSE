Imports Symbol.RFID3
Imports Symbol.RFID3.TagAccess
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace VB_RFID3_Host_Sample1
    Public Class WriteForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm, ByVal isBlockWrite As Boolean)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
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
            Me.m_WriteParams.WriteDataLength = 0
        End Sub

        Private Sub data_TB_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.Password_TB = New TextBox
            Me.label1 = New Label
            Me.writeButton = New Button
            Me.length_TB = New TextBox
            Me.lengthLabel = New Label
            Me.tagID_TB = New TextBox
            Me.tagMaskLabel = New Label
            Me.offset_TB = New TextBox
            Me.offsetLabel = New Label
            Me.memBank_CB = New ComboBox
            Me.memBankLabel1 = New Label
            Me.label2 = New Label
            Me.data_TB = New TextBox
            MyBase.SuspendLayout()
            Me.Password_TB.Location = New Point(&H5B, &H3D)
            Me.Password_TB.Name = "Password_TB"
            Me.Password_TB.Size = New Size(&HBF, 20)
            Me.Password_TB.TabIndex = 2
            Me.label1.AutoSize = True
            Me.label1.Location = New Point(12, &H3D)
            Me.label1.Name = "label1"
            Me.label1.Size = New Size(&H51, 13)
            Me.label1.TabIndex = &H25
            Me.label1.Text = "Password (Hex)"
            Me.writeButton.Location = New Point(&HCF, &H101)
            Me.writeButton.Name = "writeButton"
            Me.writeButton.Size = New Size(&H4B, &H17)
            Me.writeButton.TabIndex = 8
            Me.writeButton.Text = "Write"
            Me.writeButton.UseVisualStyleBackColor = True
            AddHandler Me.writeButton.Click, New EventHandler(AddressOf Me.writeButton_Click)
            Me.length_TB.Location = New Point(&HF2, &H93)
            Me.length_TB.Name = "length_TB"
            Me.length_TB.Size = New Size(40, 20)
            Me.length_TB.TabIndex = 5
            Me.lengthLabel.AutoSize = True
            Me.lengthLabel.Location = New Point(&H8A, 150)
            Me.lengthLabel.Name = "lengthLabel"
            Me.lengthLabel.Size = New Size(&H65, 13)
            Me.lengthLabel.TabIndex = &H22
            Me.lengthLabel.Text = "No. of Bytes Length"
            Me.tagID_TB.Location = New Point(&H5B, &H15)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&HBF, 20)
            Me.tagID_TB.TabIndex = 1
            Me.tagMaskLabel.AutoSize = True
            Me.tagMaskLabel.Location = New Point(12, &H18)
            Me.tagMaskLabel.Name = "tagMaskLabel"
            Me.tagMaskLabel.Size = New Size(&H44, 13)
            Me.tagMaskLabel.TabIndex = &H21
            Me.tagMaskLabel.Text = "Tag ID (Hex)"
            Me.offset_TB.Location = New Point(&H5B, &H93)
            Me.offset_TB.Name = "offset_TB"
            Me.offset_TB.Size = New Size(40, 20)
            Me.offset_TB.TabIndex = 4
            Me.offsetLabel.AutoSize = True
            Me.offsetLabel.Location = New Point(12, 150)
            Me.offsetLabel.Name = "offsetLabel"
            Me.offsetLabel.Size = New Size(70, 13)
            Me.offsetLabel.TabIndex = &H20
            Me.offsetLabel.Text = "Offset (Bytes)"
            Me.memBank_CB.ForeColor = Color.Navy
            Me.memBank_CB.FormattingEnabled = True
            Me.memBank_CB.Items.AddRange(New Object() {"RESERVED", "EPC", "TID", "USER"})
            Me.memBank_CB.Location = New Point(&H5B, &H67)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New Size(110, &H15)
            Me.memBank_CB.TabIndex = 3
            Me.memBankLabel1.AutoSize = True
            Me.memBankLabel1.Location = New Point(12, &H6A)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.TabIndex = &H1F
            Me.memBankLabel1.Text = "Memory Bank"
            Me.label2.AutoSize = True
            Me.label2.Location = New Point(12, &HBF)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H3A, 13)
            Me.label2.TabIndex = &H27
            Me.label2.Text = "Data (Hex)"
            Me.data_TB.Location = New Point(&H5B, &HBC)
            Me.data_TB.Multiline = True
            Me.data_TB.Name = "data_TB"
            Me.data_TB.Size = New Size(&HBF, &H3D)
            Me.data_TB.TabIndex = 6
            MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Font
            MyBase.ClientSize = New Size(&H126, &H124)
            MyBase.Controls.Add(Me.data_TB)
            MyBase.Controls.Add(Me.label2)
            MyBase.Controls.Add(Me.Password_TB)
            MyBase.Controls.Add(Me.label1)
            MyBase.Controls.Add(Me.writeButton)
            MyBase.Controls.Add(Me.length_TB)
            MyBase.Controls.Add(Me.lengthLabel)
            MyBase.Controls.Add(Me.tagID_TB)
            MyBase.Controls.Add(Me.tagMaskLabel)
            MyBase.Controls.Add(Me.offset_TB)
            MyBase.Controls.Add(Me.offsetLabel)
            MyBase.Controls.Add(Me.memBank_CB)
            MyBase.Controls.Add(Me.memBankLabel1)
            MyBase.FormBorderStyle = FormBorderStyle.FixedDialog
            MyBase.MaximizeBox = False
            MyBase.MinimizeBox = False
            MyBase.Name = "WriteForm"
            MyBase.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Write/Block Write"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Write_Load)
            MyBase.ResumeLayout(False)
            MyBase.PerformLayout()
        End Sub

        Private Sub Write_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedItems.Count > 0) Then
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.SelectedItems.Item(0)
                Me.tagID_TB.Text = item.Text
            Else
                Me.tagID_TB.Text = ""
            End If
            Me.memBank_CB.SelectedIndex = CInt(Me.m_WriteParams.MemoryBank)
            Me.Password_TB.Text = Me.m_WriteParams.AccessPassword.ToString("X")
            Me.offset_TB.Text = Me.m_WriteParams.ByteOffset.ToString
            Me.length_TB.Text = Me.m_WriteParams.WriteDataLength.ToString
        End Sub

        Private Sub writeButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim length As UInt16 = UInt16.Parse(Me.length_TB.Text)
                If (Me.tagID_TB.Text.Length = 0) Then
                    Me.m_AppForm.functionCallStatusLabel.Text = "No TagID is defined"
                ElseIf ((length * 2) > Me.data_TB.Text.Length) Then
                    Me.m_AppForm.functionCallStatusLabel.Text = "Data length has to be a word size (2X)"
                ElseIf ((length Mod 2) <> 0) Then
                    Me.m_AppForm.functionCallStatusLabel.Text = "Data length has to be a even number"
                Else
                    Me.m_AppForm.functionCallStatusLabel.Text = ""
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
                        Me.m_AppForm.accessBackgroundWorker.RunWorkerAsync(ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE)
                    Else
                        Me.m_AppForm.accessBackgroundWorker.RunWorkerAsync(ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE)
                    End If
                    Me.writeButton.Enabled = False
                End If
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private data_TB As TextBox
        Private label1 As Label
        Private label2 As Label
        Friend length_TB As TextBox
        Private lengthLabel As Label
        Private m_AppForm As AppForm
        Private m_IsBlockWrite As Boolean
        Friend m_WriteParams As WriteAccessParams
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

