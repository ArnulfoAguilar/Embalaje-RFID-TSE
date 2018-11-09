Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class AccessFilterForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Private Sub accessFilterButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim exceptionMsg As String = "TagPatternA BitOffset:"
            Try
                If (Me.m_AppForm.m_ReaderAPI.IsConnected AndAlso Me.useAccessFilter_CB.Checked) Then
                    Dim index As Integer
                    If (Nothing Is Me.m_AccessFilter) Then
                        Me.m_AccessFilter = New AccessFilter
                    End If
                    Me.m_UseAccessFilter = Me.useAccessFilter_CB.Checked
                    Me.m_AccessFilter.MatchPattern = DirectCast(Me.matchPattern_CB.SelectedIndex, MATCH_PATTERN)
                    Me.m_AccessFilter.TagPatternA.MemoryBank = DirectCast(Me.memBank_CB1.SelectedIndex, MEMORY_BANK)
                    Me.m_AccessFilter.TagPatternA.BitOffset = UInt16.Parse(Me.offset_TB1.Text)
                    Dim maskLengthA As Integer = (Me.tagMask_TB1.Text.Length / 2)
                    Dim filterMaskA As Byte() = New Byte(maskLengthA - 1) {}
                    exceptionMsg = "TagPatternA Mask:"

                    For index = 0 To maskLengthA - 1
                        filterMaskA(index) = Byte.Parse(Me.tagMask_TB1.Text.Substring((index * 2), 2), NumberStyles.HexNumber)
                    Next index
                    Me.m_AccessFilter.TagPatternA.TagMask = filterMaskA
                    Me.m_AccessFilter.TagPatternA.TagMaskBitCount = maskLengthA * 8
                    Dim dataLengthA As Integer = (Me.MembankData_TB1.Text.Length / 2)
                    Dim memoryBankDataA As Byte() = New Byte(dataLengthA - 1) {}
                    exceptionMsg = "TagPatternA Pattern:"

                    For index = 0 To dataLengthA - 1
                        memoryBankDataA(index) = Byte.Parse(Me.MembankData_TB1.Text.Substring((index * 2), 2), NumberStyles.HexNumber)
                    Next index
                    Me.m_AccessFilter.TagPatternA.TagPattern = memoryBankDataA
                    Me.m_AccessFilter.TagPatternA.TagPatternBitCount = dataLengthA * 8
                    If (Me.m_AccessFilter.MatchPattern <> MATCH_PATTERN.A) Then
                        exceptionMsg = "TagPatternB BitOffset:"
                        Me.m_AccessFilter.TagPatternB.MemoryBank = DirectCast(Me.memBank_CB2.SelectedIndex, MEMORY_BANK)
                        Me.m_AccessFilter.TagPatternB.BitOffset = UInt16.Parse(Me.offset_TB2.Text)
                        Dim maskLengthB As Integer = (Me.tagMask_TB2.Text.Length / 2)
                        Dim filterMaskB As Byte() = New Byte(maskLengthB - 1) {}
                        exceptionMsg = "TagPatternB Mask:"
                        For index = 0 To maskLengthB - 1
                            filterMaskB(index) = Byte.Parse(Me.tagMask_TB2.Text.Substring((index * 2), 2), NumberStyles.HexNumber)
                        Next index
                        Me.m_AccessFilter.TagPatternB.TagMask = filterMaskB
                        Me.m_AccessFilter.TagPatternB.TagMaskBitCount = maskLengthB * 8
                        Dim dataLengthB As Integer = (Me.MembankData_TB2.Text.Length / 2)
                        Dim memoryBankDataB As Byte() = New Byte(dataLengthB - 1) {}
                        exceptionMsg = "TagPatternB Pattern:"
                        For index = 0 To dataLengthB - 1
                            memoryBankDataB(index) = Byte.Parse(Me.MembankData_TB2.Text.Substring((index * 2), 2), NumberStyles.HexNumber)
                        Next index
                        Me.m_AccessFilter.TagPatternB.TagPattern = memoryBankDataB
                        Me.m_AccessFilter.TagPatternB.TagPatternBitCount = dataLengthB * 8
                    End If
                ElseIf Not Me.useAccessFilter_CB.Checked Then
                    Me.m_AccessFilter = Nothing
                End If
                MyBase.Close()
            Catch ex As Exception
                Me.m_AppForm.notifyUser((exceptionMsg & ex.Message), "Access Filter")
            End Try
        End Sub

        Private Sub AccessFilterForm_Closing(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Private Sub AccessFilterForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not Me.m_IsLoaded Then
                Me.matchPattern_CB.SelectedIndex = 0
                Me.memBank_CB1.SelectedIndex = 1
                Me.memBank_CB2.SelectedIndex = 1
                Me.m_IsLoaded = True
            End If
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Public Function getFilter() As AccessFilter
            Return IIf(Me.m_UseAccessFilter, Me.m_AccessFilter, Me.m_AccessFilter)
        End Function

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.matchPattern_CB = New ComboBox
            Me.label1 = New Label
            Me.tabControl1 = New TabControl
            Me.tagPatternA_TP = New TabPage
            Me.tagMask_TB1 = New TextBox
            Me.filterActionLabel1 = New Label
            Me.MembankData_TB1 = New TextBox
            Me.tagMaskLabel1 = New Label
            Me.offset_TB1 = New TextBox
            Me.offsetLabel1 = New Label
            Me.memBank_CB1 = New ComboBox
            Me.memBankLabel1 = New Label
            Me.tagPatternB_TP = New TabPage
            Me.tagMask_TB2 = New TextBox
            Me.label2 = New Label
            Me.MembankData_TB2 = New TextBox
            Me.label3 = New Label
            Me.offset_TB2 = New TextBox
            Me.label4 = New Label
            Me.memBank_CB2 = New ComboBox
            Me.label5 = New Label
            Me.accessFilterButton = New Button
            Me.useAccessFilter_CB = New CheckBox
            Me.tabControl1.SuspendLayout()
            Me.tagPatternA_TP.SuspendLayout()
            Me.tagPatternB_TP.SuspendLayout()
            MyBase.SuspendLayout()
            Me.matchPattern_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.matchPattern_CB.Items.Add("A AND B")
            Me.matchPattern_CB.Items.Add("NOTA AND B")
            Me.matchPattern_CB.Items.Add("NOTA AND NOTB")
            Me.matchPattern_CB.Items.Add("A AND NOTB")
            Me.matchPattern_CB.Items.Add("A")
            Me.matchPattern_CB.Location = New Point(&H60, &H88)
            Me.matchPattern_CB.Name = "matchPattern_CB"
            Me.matchPattern_CB.Size = New Size(&H53, 20)
            Me.matchPattern_CB.TabIndex = &H15
            Me.label1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label1.Location = New Point(15, &H8F)
            Me.label1.Name = "label1"
            Me.label1.Size = New Size(&H4B, 13)
            Me.label1.Text = "Match Pattern"
            Me.tabControl1.Controls.Add(Me.tagPatternA_TP)
            Me.tabControl1.Controls.Add(Me.tagPatternB_TP)
            Me.tabControl1.Location = New Point(0, 0)
            Me.tabControl1.Enabled = False
            Me.tabControl1.Name = "tabControl1"
            Me.tabControl1.SelectedIndex = 0
            Me.tabControl1.Size = New Size(240, &H80)
            Me.tabControl1.TabIndex = &H17
            Me.tagPatternA_TP.Controls.Add(Me.tagMask_TB1)
            Me.tagPatternA_TP.Controls.Add(Me.filterActionLabel1)
            Me.tagPatternA_TP.Controls.Add(Me.MembankData_TB1)
            Me.tagPatternA_TP.Controls.Add(Me.tagMaskLabel1)
            Me.tagPatternA_TP.Controls.Add(Me.offset_TB1)
            Me.tagPatternA_TP.Controls.Add(Me.offsetLabel1)
            Me.tagPatternA_TP.Controls.Add(Me.memBank_CB1)
            Me.tagPatternA_TP.Controls.Add(Me.memBankLabel1)
            Me.tagPatternA_TP.Location = New Point(0, 0)
            Me.tagPatternA_TP.Name = "tagPatternA_TP"
            Me.tagPatternA_TP.Size = New Size(240, &H69)
            Me.tagPatternA_TP.Text = "Tag Pattern A"
            Me.tagMask_TB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMask_TB1.Location = New Point(&H60, &H51)
            Me.tagMask_TB1.Name = "tagMask_TB1"
            Me.tagMask_TB1.Size = New Size(&H81, &H13)
            Me.tagMask_TB1.TabIndex = 4
            Me.filterActionLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.filterActionLabel1.Location = New Point(15, &H51)
            Me.filterActionLabel1.Name = "filterActionLabel1"
            Me.filterActionLabel1.Size = New Size(&H37, 13)
            Me.filterActionLabel1.Text = "Tag Mask"
            Me.MembankData_TB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.MembankData_TB1.Location = New Point(&H60, &H39)
            Me.MembankData_TB1.Name = "MembankData_TB1"
            Me.MembankData_TB1.Size = New Size(&H81, &H13)
            Me.MembankData_TB1.TabIndex = 3
            Me.tagMaskLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMaskLabel1.Location = New Point(15, &H39)
            Me.tagMaskLabel1.Name = "tagMaskLabel1"
            Me.tagMaskLabel1.Size = New Size(&H4A, &H13)
            Me.tagMaskLabel1.Text = "Tag Pattern"
            Me.offset_TB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offset_TB1.Location = New Point(&H60, &H21)
            Me.offset_TB1.Name = "offset_TB1"
            Me.offset_TB1.Size = New Size(&H52, &H13)
            Me.offset_TB1.TabIndex = 2
            Me.offsetLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offsetLabel1.Location = New Point(15, &H21)
            Me.offsetLabel1.Name = "offsetLabel1"
            Me.offsetLabel1.Size = New Size(&H23, 13)
            Me.offsetLabel1.Text = "Offset"
            Me.memBank_CB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBank_CB1.ForeColor = Color.Navy
            Me.memBank_CB1.Items.Add("RESERVED")
            Me.memBank_CB1.Items.Add("EPC")
            Me.memBank_CB1.Items.Add("TID")
            Me.memBank_CB1.Items.Add("USER")
            Me.memBank_CB1.Location = New Point(&H60, 8)
            Me.memBank_CB1.Name = "memBank_CB1"
            Me.memBank_CB1.Size = New Size(&H52, 20)
            Me.memBank_CB1.TabIndex = 1
            Me.memBankLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBankLabel1.Location = New Point(15, 8)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.Text = "Memory Bank"
            Me.tagPatternB_TP.Controls.Add(Me.tagMask_TB2)
            Me.tagPatternB_TP.Controls.Add(Me.label2)
            Me.tagPatternB_TP.Controls.Add(Me.MembankData_TB2)
            Me.tagPatternB_TP.Controls.Add(Me.label3)
            Me.tagPatternB_TP.Controls.Add(Me.offset_TB2)
            Me.tagPatternB_TP.Controls.Add(Me.label4)
            Me.tagPatternB_TP.Controls.Add(Me.memBank_CB2)
            Me.tagPatternB_TP.Controls.Add(Me.label5)
            Me.tagPatternB_TP.Location = New Point(0, 0)
            Me.tagPatternB_TP.Name = "tagPatternB_TP"
            Me.tagPatternB_TP.Size = New Size(&HF0, &H69)
            Me.tagPatternB_TP.Text = "Tag Pattern B"
            Me.tagMask_TB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMask_TB2.Location = New Point(96, 81)
            Me.tagMask_TB2.Name = "tagMask_TB2"
            Me.tagMask_TB2.Size = New Size(&H81, &H13)
            Me.tagMask_TB2.TabIndex = 8
            Me.label2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label2.Location = New Point(15, 81)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H37, 13)
            Me.label2.Text = "Tag Mask"
            Me.MembankData_TB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.MembankData_TB2.Location = New Point(96, 57)
            Me.MembankData_TB2.Name = "MembankData_TB2"
            Me.MembankData_TB2.Size = New Size(&H81, &H13)
            Me.MembankData_TB2.TabIndex = 7
            Me.label3.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label3.Location = New Point(15, 57)
            Me.label3.Name = "label3"
            Me.label3.Size = New Size(&H4A, &H10)
            Me.label3.Text = "Tag Pattern"
            Me.offset_TB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offset_TB2.Location = New Point(96, 33)
            Me.offset_TB2.Name = "offset_TB2"
            Me.offset_TB2.Size = New Size(&H52, &H13)
            Me.offset_TB2.TabIndex = 6
            Me.label4.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label4.Location = New Point(15, 33)
            Me.label4.Name = "label4"
            Me.label4.Size = New Size(&H23, 13)
            Me.label4.Text = "Offset"
            Me.memBank_CB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBank_CB2.ForeColor = Color.Navy
            Me.memBank_CB2.Items.Add("RESERVED")
            Me.memBank_CB2.Items.Add("EPC")
            Me.memBank_CB2.Items.Add("TID")
            Me.memBank_CB2.Items.Add("USER")
            Me.memBank_CB2.Location = New Point(96, 8)
            Me.memBank_CB2.Name = "memBank_CB2"
            Me.memBank_CB2.Size = New Size(&H52, 20)
            Me.memBank_CB2.TabIndex = 5
            Me.label5.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label5.Location = New Point(15, 8)
            Me.label5.Name = "label5"
            Me.label5.Size = New Size(&H48, 13)
            Me.label5.Text = "Memory Bank"
            Me.accessFilterButton.Location = New Point(&HBA, &HA2)
            Me.accessFilterButton.Name = "accessFilterButton"
            Me.accessFilterButton.Size = New Size(&H33, 20)
            Me.accessFilterButton.TabIndex = &H2A
            Me.accessFilterButton.Text = "Apply"
            AddHandler Me.accessFilterButton.Click, New EventHandler(AddressOf Me.accessFilterButton_Click)
            Me.useAccessFilter_CB.Font = New Font("Tahoma", 8.0!, FontStyle.Regular)
            Me.useAccessFilter_CB.Location = New Point(15, &HA2)
            Me.useAccessFilter_CB.Name = "useAccessFilter_CB"
            Me.useAccessFilter_CB.Size = New Size(&H4E, 20)
            Me.useAccessFilter_CB.TabIndex = &H2C
            Me.useAccessFilter_CB.Text = "Use Filter"
            AddHandler Me.useAccessFilter_CB.Click, New EventHandler(AddressOf Me.useAccessFilter_CB_CheckStateChanged)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.useAccessFilter_CB)
            MyBase.Controls.Add(Me.accessFilterButton)
            MyBase.Controls.Add(Me.tabControl1)
            MyBase.Controls.Add(Me.matchPattern_CB)
            MyBase.Controls.Add(Me.label1)
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "AccessFilterForm"
            Me.Text = "AccessFilter"
            Me.tabControl1.ResumeLayout(False)
            Me.tagPatternA_TP.ResumeLayout(False)
            Me.tagPatternB_TP.ResumeLayout(False)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub useAccessFilter_CB_CheckStateChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.tabControl1.Enabled = Me.useAccessFilter_CB.Checked
        End Sub

        ' Fields
        Private accessFilterButton As Button
        Private components As IContainer = Nothing
        Private filterActionLabel1 As Label
        Private label1 As Label
        Private label2 As Label
        Private label3 As Label
        Private label4 As Label
        Private label5 As Label
        Private m_AccessFilter As AccessFilter = Nothing
        Private m_AppForm As AppForm
        Private m_IsLoaded As Boolean
        Private m_UseAccessFilter As Boolean = False
        Private mainMenu1 As MainMenu
        Private matchPattern_CB As ComboBox
        Private memBank_CB1 As ComboBox
        Private memBank_CB2 As ComboBox
        Private MembankData_TB1 As TextBox
        Private MembankData_TB2 As TextBox
        Private memBankLabel1 As Label
        Private offset_TB1 As TextBox
        Private offset_TB2 As TextBox
        Private offsetLabel1 As Label
        Private tabControl1 As TabControl
        Private tagMask_TB1 As TextBox
        Private tagMask_TB2 As TextBox
        Private tagMaskLabel1 As Label
        Private tagPatternA_TP As TabPage
        Private tagPatternB_TP As TabPage
        Private useAccessFilter_CB As CheckBox
    End Class
End Namespace

