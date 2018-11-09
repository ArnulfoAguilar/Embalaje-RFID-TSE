Imports Symbol.RFID3
Imports Symbol.RFID3.TagAccess
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace VB_RFID3_Host_Sample1
    Public Class ReadForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
            Me.m_ReadParams = New ReadAccessParams
            Me.m_ReadParams.MemoryBank = MEMORY_BANK.MEMORY_BANK_EPC
            Me.m_ReadParams.AccessPassword = 0
            Me.m_ReadParams.ByteOffset = 0
            Me.m_ReadParams.ByteCount = 0
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.tagID_TB = New TextBox
            Me.tagMaskLabel = New Label
            Me.offset_TB = New TextBox
            Me.offsetLabel = New Label
            Me.memBank_CB = New ComboBox
            Me.memBankLabel1 = New Label
            Me.length_TB = New TextBox
            Me.lengthLabel = New Label
            Me.readButton = New Button
            Me.label1 = New Label
            Me.Password_TB = New TextBox
            Me.ReadData_TB = New TextBox
            Me.label2 = New Label
            MyBase.SuspendLayout()
            Me.tagID_TB.Location = New Point(90, 12)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&HBF, 20)
            Me.tagID_TB.TabIndex = 1
            Me.tagMaskLabel.AutoSize = True
            Me.tagMaskLabel.Location = New Point(13, 15)
            Me.tagMaskLabel.Name = "tagMaskLabel"
            Me.tagMaskLabel.Size = New Size(&H44, 13)
            Me.tagMaskLabel.TabIndex = &H13
            Me.tagMaskLabel.Text = "Tag ID (Hex)"
            Me.offset_TB.Location = New Point(90, &H86)
            Me.offset_TB.Name = "offset_TB"
            Me.offset_TB.Size = New Size(40, 20)
            Me.offset_TB.TabIndex = 4
            AddHandler Me.offset_TB.TextChanged, New EventHandler(AddressOf Me.offset_TB_TextChanged)
            Me.offsetLabel.AutoSize = True
            Me.offsetLabel.Location = New Point(13, &H89)
            Me.offsetLabel.Name = "offsetLabel"
            Me.offsetLabel.Size = New Size(70, 13)
            Me.offsetLabel.TabIndex = &H11
            Me.offsetLabel.Text = "Offset (Bytes)"
            Me.memBank_CB.ForeColor = Color.Navy
            Me.memBank_CB.FormattingEnabled = True
            Me.memBank_CB.Items.AddRange(New Object() {"RESERVED", "EPC", "TID", "USER"})
            Me.memBank_CB.Location = New Point(90, 90)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New Size(110, &H15)
            Me.memBank_CB.TabIndex = 3
            AddHandler Me.memBank_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.memBank_CB_SelectedIndexChanged)
            Me.memBankLabel1.AutoSize = True
            Me.memBankLabel1.Location = New Point(13, &H5D)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.TabIndex = 15
            Me.memBankLabel1.Text = "Memory Bank"
            Me.length_TB.Location = New Point(&HF1, &H86)
            Me.length_TB.Name = "length_TB"
            Me.length_TB.Size = New Size(40, 20)
            Me.length_TB.TabIndex = 5
            AddHandler Me.length_TB.TextChanged, New EventHandler(AddressOf Me.length_TB_TextChanged)
            Me.lengthLabel.AutoSize = True
            Me.lengthLabel.Location = New Point(&H89, &H89)
            Me.lengthLabel.Name = "lengthLabel"
            Me.lengthLabel.Size = New Size(&H65, 13)
            Me.lengthLabel.TabIndex = &H15
            Me.lengthLabel.Text = "No. of Bytes Length"
            Me.readButton.Location = New Point(&HCF, &HE9)
            Me.readButton.Name = "readButton"
            Me.readButton.Size = New Size(&H4B, &H17)
            Me.readButton.TabIndex = 7
            Me.readButton.Text = "Read"
            Me.readButton.UseVisualStyleBackColor = True
            AddHandler Me.readButton.Click, New EventHandler(AddressOf Me.readButton_Click)
            Me.label1.AutoSize = True
            Me.label1.Location = New Point(13, &H34)
            Me.label1.Name = "label1"
            Me.label1.Size = New Size(&H35, 13)
            Me.label1.TabIndex = &H19
            Me.label1.Text = "Password (Hex)"
            Me.Password_TB.Location = New Point(90, &H31)
            Me.Password_TB.Name = "Password_TB"
            Me.Password_TB.Size = New Size(&HBF, 20)
            Me.Password_TB.TabIndex = 2
            Me.ReadData_TB.Location = New Point(&H10, &HB8)
            Me.ReadData_TB.Multiline = True
            Me.ReadData_TB.Name = "ReadData_TB"
            Me.ReadData_TB.ReadOnly = True
            Me.ReadData_TB.Size = New Size(&H109, &H2B)
            Me.ReadData_TB.TabIndex = 9
            Me.label2.AutoSize = True
            Me.label2.Location = New Point(15, &HA8)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H57, 13)
            Me.label2.TabIndex = &H1B
            Me.label2.Text = "Data Read (Hex)"
            MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Font
            MyBase.ClientSize = New Size(&H126, &H10C)
            MyBase.Controls.Add(Me.label2)
            MyBase.Controls.Add(Me.ReadData_TB)
            MyBase.Controls.Add(Me.Password_TB)
            MyBase.Controls.Add(Me.label1)
            MyBase.Controls.Add(Me.readButton)
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
            MyBase.Name = "ReadForm"
            MyBase.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Read Tag (Hex)"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Read_Load)
            MyBase.ResumeLayout(False)
            MyBase.PerformLayout()
        End Sub

        Private Sub length_TB_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.length_TB.Text.Length > 0) Then
                Me.m_ReadParams.ByteCount = UInt16.Parse(Me.length_TB.Text)
            End If
        End Sub

        Private Sub memBank_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.m_ReadParams.MemoryBank = DirectCast(Me.memBank_CB.SelectedIndex, MEMORY_BANK)
        End Sub

        Private Sub offset_TB_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.offset_TB.Text.Length > 0) Then
                Me.m_ReadParams.ByteOffset = UInt16.Parse(Me.offset_TB.Text)
            End If
        End Sub

        Private Sub Read_Load(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.m_AppForm.inventoryList.SelectedItems.Count > 0) Then
                Dim item As ListViewItem = Me.m_AppForm.inventoryList.SelectedItems.Item(0)
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

        Private Sub readButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If (Me.tagID_TB.Text.Length = 0) Then
                    Me.m_AppForm.functionCallStatusLabel.Text = "No TagID is defined"
                    Return
                End If
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
                Me.m_AppForm.accessBackgroundWorker.RunWorkerAsync(ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ)
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
            End Try
            Me.readButton.Enabled = False
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private label1 As Label
        Private label2 As Label
        Friend length_TB As TextBox
        Private lengthLabel As Label
        Private m_AppForm As AppForm
        Friend m_ReadParams As ReadAccessParams = Nothing
        Friend memBank_CB As ComboBox
        Private memBankLabel1 As Label
        Friend offset_TB As TextBox
        Private offsetLabel As Label
        Friend Password_TB As TextBox
        Friend readButton As Button
        Public ReadData_TB As TextBox
        Private tagID_TB As TextBox
        Private tagMaskLabel As Label
    End Class
End Namespace

