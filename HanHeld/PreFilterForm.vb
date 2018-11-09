Imports Symbol.RFID3
Imports Symbol.RFID3.PreFilters
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class PreFilterForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.m_isLoaded = False
            Me.InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub filter_CB1_CheckStateChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.antIDLabel1.Enabled = Me.filter_CB1.Checked
            Me.offsetLabel1.Enabled = Me.filter_CB1.Checked
            Me.memBankLabel1.Enabled = Me.filter_CB1.Checked
            Me.tagMaskLabel1.Enabled = Me.filter_CB1.Checked
            Me.filterActionLabel1.Enabled = Me.filter_CB1.Checked
            Me.actionLabel1.Enabled = Me.filter_CB1.Checked
            Me.targetLabel1.Enabled = Me.filter_CB1.Checked
            Me.antennaID_CB1.Enabled = Me.filter_CB1.Checked
            Me.memBank_CB1.Enabled = Me.filter_CB1.Checked
            Me.offset_TB1.Enabled = Me.filter_CB1.Checked
            Me.tagMask_TB1.Enabled = Me.filter_CB1.Checked
            Me.filterAction_CB1.Enabled = Me.filter_CB1.Checked
            Try
                If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                    Me.m_AppForm.m_ReaderAPI.Actions.PreFilters.DeleteAll()
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Pre-Filter")
            End Try
        End Sub

        Private Sub filter_CB2_CheckStateChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.antIDLabel2.Enabled = Me.filter_CB2.Checked
            Me.offsetLabel2.Enabled = Me.filter_CB2.Checked
            Me.memBankLabel2.Enabled = Me.filter_CB2.Checked
            Me.tagMaskLabel2.Enabled = Me.filter_CB2.Checked
            Me.filterActionLabel2.Enabled = Me.filter_CB2.Checked
            Me.actionLabel2.Enabled = Me.filter_CB2.Checked
            Me.targetLabel2.Enabled = Me.filter_CB2.Checked
            Me.antennaID_CB2.Enabled = Me.filter_CB2.Checked
            Me.memBank_CB2.Enabled = Me.filter_CB2.Checked
            Me.offset_TB2.Enabled = Me.filter_CB2.Checked
            Me.tagMask_TB2.Enabled = Me.filter_CB2.Checked
            Me.filterAction_CB2.Enabled = Me.filter_CB2.Checked
            Try
                If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                    Me.m_AppForm.m_ReaderAPI.Actions.PreFilters.DeleteAll()
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Pre-Filter")
            End Try
        End Sub

        Private Sub filterAction_CB1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Select Case Me.filterAction_CB1.SelectedIndex
                Case 0
                    Me.action_CB1.Enabled = False
                    Me.target_CB1.Enabled = False
                    Me.action_CB1.Items.Clear()
                    Me.target_CB1.Items.Clear()
                    Exit Select
                Case 1
                    Me.action_CB1.Items.Clear()
                    Me.action_CB1.Items.Add("INV A NOT INV B")
                    Me.action_CB1.Items.Add("ASRT_SL_NOT_DSRT_SL")
                    Me.action_CB1.Items.Add("INV A")
                    Me.action_CB1.Items.Add("ASRT SL")
                    Me.action_CB1.Items.Add("NOT INV B")
                    Me.action_CB1.Items.Add("NOT DSRT SL")
                    Me.action_CB1.Items.Add("INV A2BB2A NOT INV A")
                    Me.action_CB1.Items.Add("NEG SL NOT ASRT SL")
                    Me.action_CB1.Items.Add("INV B NOT INV A")
                    Me.action_CB1.Items.Add("DSRT SL NOT ASRT SL")
                    Me.action_CB1.Items.Add("INV B")
                    Me.action_CB1.Items.Add("DSRT SL")
                    Me.action_CB1.Items.Add("NOT INV A")
                    Me.action_CB1.Items.Add("NOT ASRT SL")
                    Me.action_CB1.Items.Add("NOT INV A2BB2A")
                    Me.action_CB1.Items.Add("NOT NEG SL")
                    Me.action_CB1.SelectedIndex = 0
                    Me.target_CB1.Items.Clear()
                    Me.target_CB1.Items.Add("SL")
                    Me.target_CB1.Items.Add("S0")
                    Me.target_CB1.Items.Add("S1")
                    Me.target_CB1.Items.Add("S2")
                    Me.target_CB1.Items.Add("S3")
                    Me.target_CB1.SelectedIndex = 0
                    Me.action_CB1.Enabled = True
                    Me.target_CB1.Enabled = True
                    Exit Select
                Case 2
                    Me.action_CB1.Items.Clear()
                    Me.action_CB1.Items.Add("SELECT NOT UNSELECT")
                    Me.action_CB1.Items.Add("SELECT")
                    Me.action_CB1.Items.Add("NOT UNSELECT")
                    Me.action_CB1.Items.Add("UNSELECT")
                    Me.action_CB1.Items.Add("UNSELECT NOT SELECT")
                    Me.action_CB1.Items.Add("NOT SELECT")
                    Me.action_CB1.SelectedIndex = 0
                    Me.target_CB1.Items.Clear()
                    Me.target_CB1.Enabled = False
                    Me.action_CB1.Enabled = True
                    Exit Select
            End Select
        End Sub

        Private Sub filterAction_CB2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Select Case Me.filterAction_CB2.SelectedIndex
                Case 0
                    Me.target_CB2.Enabled = False
                    Me.action_CB2.Enabled = False
                    Exit Select
                Case 1
                    Me.action_CB2.Items.Clear()
                    Me.action_CB2.Items.Add("INV A NOT INV B")
                    Me.action_CB2.Items.Add("ASRT_SL_NOT_DSRT_SL")
                    Me.action_CB2.Items.Add("INV A")
                    Me.action_CB2.Items.Add("ASRT SL")
                    Me.action_CB2.Items.Add("NOT INV B")
                    Me.action_CB2.Items.Add("NOT DSRT SL")
                    Me.action_CB2.Items.Add("INV A2BB2A NOT INV A")
                    Me.action_CB2.Items.Add("NEG SL NOT ASRT SL")
                    Me.action_CB2.Items.Add("INV B NOT INV A")
                    Me.action_CB2.Items.Add("DSRT SL NOT ASRT SL")
                    Me.action_CB2.Items.Add("INV B")
                    Me.action_CB2.Items.Add("DSRT SL")
                    Me.action_CB2.Items.Add("NOT INV A")
                    Me.action_CB2.Items.Add("NOT ASRT SL")
                    Me.action_CB2.Items.Add("NOT INV A2BB2A")
                    Me.action_CB2.Items.Add("NOT NEG SL")
                    Me.action_CB2.SelectedIndex = 0
                    Me.target_CB2.Items.Clear()
                    Me.target_CB2.Items.Add("SL")
                    Me.target_CB2.Items.Add("S0")
                    Me.target_CB2.Items.Add("S1")
                    Me.target_CB2.Items.Add("S2")
                    Me.target_CB2.Items.Add("S3")
                    Me.target_CB2.SelectedIndex = 0
                    Me.target_CB2.Enabled = True
                    Me.action_CB2.Enabled = True
                    Exit Select
                Case 2
                    Me.action_CB2.Items.Clear()
                    Me.action_CB2.Items.Add("SELECT NOT UNSELECT")
                    Me.action_CB2.Items.Add("SELECT")
                    Me.action_CB2.Items.Add("NOT UNSELECT")
                    Me.action_CB2.Items.Add("UNSELECT")
                    Me.action_CB2.Items.Add("UNSELECT NOT SELECT")
                    Me.action_CB2.Items.Add("NOT SELECT")
                    Me.action_CB2.SelectedIndex = 0
                    Me.target_CB2.Enabled = False
                    Me.action_CB2.Enabled = True
                    Exit Select
            End Select
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.tabControl1 = New TabControl
            Me.filter1_TP = New TabPage
            Me.filter_CB1 = New CheckBox
            Me.antennaID_CB1 = New ComboBox
            Me.antIDLabel1 = New Label
            Me.target_CB1 = New ComboBox
            Me.targetLabel1 = New Label
            Me.action_CB1 = New ComboBox
            Me.actionLabel1 = New Label
            Me.filterAction_CB1 = New ComboBox
            Me.filterActionLabel1 = New Label
            Me.tagMask_TB1 = New TextBox
            Me.tagMaskLabel1 = New Label
            Me.offset_TB1 = New TextBox
            Me.offsetLabel1 = New Label
            Me.memBank_CB1 = New ComboBox
            Me.memBankLabel1 = New Label
            Me.filter2_TP = New TabPage
            Me.filter_CB2 = New CheckBox
            Me.antennaID_CB2 = New ComboBox
            Me.antIDLabel2 = New Label
            Me.target_CB2 = New ComboBox
            Me.targetLabel2 = New Label
            Me.action_CB2 = New ComboBox
            Me.actionLabel2 = New Label
            Me.filterAction_CB2 = New ComboBox
            Me.filterActionLabel2 = New Label
            Me.tagMask_TB2 = New TextBox
            Me.tagMaskLabel2 = New Label
            Me.offset_TB2 = New TextBox
            Me.offsetLabel2 = New Label
            Me.memBank_CB2 = New ComboBox
            Me.memBankLabel2 = New Label
            Me.presFilterButton = New Button
            Me.tabControl1.SuspendLayout()
            Me.filter1_TP.SuspendLayout()
            Me.filter2_TP.SuspendLayout()
            MyBase.SuspendLayout()
            Me.tabControl1.Controls.Add(Me.filter1_TP)
            Me.tabControl1.Controls.Add(Me.filter2_TP)
            Me.tabControl1.Location = New Point(0, 0)
            Me.tabControl1.Name = "tabControl1"
            Me.tabControl1.SelectedIndex = 0
            Me.tabControl1.Size = New Size(240, &H9F)
            Me.tabControl1.TabIndex = 0
            Me.filter1_TP.Controls.Add(Me.filter_CB1)
            Me.filter1_TP.Controls.Add(Me.antennaID_CB1)
            Me.filter1_TP.Controls.Add(Me.antIDLabel1)
            Me.filter1_TP.Controls.Add(Me.target_CB1)
            Me.filter1_TP.Controls.Add(Me.targetLabel1)
            Me.filter1_TP.Controls.Add(Me.action_CB1)
            Me.filter1_TP.Controls.Add(Me.actionLabel1)
            Me.filter1_TP.Controls.Add(Me.filterAction_CB1)
            Me.filter1_TP.Controls.Add(Me.filterActionLabel1)
            Me.filter1_TP.Controls.Add(Me.tagMask_TB1)
            Me.filter1_TP.Controls.Add(Me.tagMaskLabel1)
            Me.filter1_TP.Controls.Add(Me.offset_TB1)
            Me.filter1_TP.Controls.Add(Me.offsetLabel1)
            Me.filter1_TP.Controls.Add(Me.memBank_CB1)
            Me.filter1_TP.Controls.Add(Me.memBankLabel1)
            Me.filter1_TP.Location = New Point(0, 0)
            Me.filter1_TP.Name = "filter1_TP"
            Me.filter1_TP.Size = New Size(240, &H88)
            Me.filter1_TP.Text = "Filter 1"
            Me.filter_CB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.filter_CB1.Location = New Point(&H94, 5)
            Me.filter_CB1.Name = "filter_CB1"
            Me.filter_CB1.Size = New Size(&H57, 20)
            Me.filter_CB1.TabIndex = 1
            Me.filter_CB1.Text = "Use Filter 1"
            AddHandler Me.filter_CB1.CheckStateChanged, New EventHandler(AddressOf Me.filter_CB1_CheckStateChanged)
            Me.antennaID_CB1.Enabled = False
            Me.antennaID_CB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antennaID_CB1.ForeColor = Color.Navy
            Me.antennaID_CB1.Location = New Point(&H52, 5)
            Me.antennaID_CB1.Name = "antennaID_CB1"
            Me.antennaID_CB1.Size = New Size(&H3E, 20)
            Me.antennaID_CB1.TabIndex = 2
            Me.antIDLabel1.Enabled = False
            Me.antIDLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antIDLabel1.Location = New Point(10, 10)
            Me.antIDLabel1.Name = "antIDLabel1"
            Me.antIDLabel1.Size = New Size(&H3D, &H13)
            Me.antIDLabel1.Text = "Antenna ID"
            Me.target_CB1.Enabled = False
            Me.target_CB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.target_CB1.ForeColor = Color.Navy
            Me.target_CB1.Items.Add("S0")
            Me.target_CB1.Items.Add("S1")
            Me.target_CB1.Items.Add("S2")
            Me.target_CB1.Items.Add("S3")
            Me.target_CB1.Location = New Point(190, &H6C)
            Me.target_CB1.Name = "target_CB1"
            Me.target_CB1.Size = New Size(&H21, 20)
            Me.target_CB1.TabIndex = 8
            Me.targetLabel1.Enabled = False
            Me.targetLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.targetLabel1.Location = New Point(&H9B, &H6F)
            Me.targetLabel1.Name = "targetLabel1"
            Me.targetLabel1.Size = New Size(&H24, 13)
            Me.targetLabel1.Text = "Target"
            Me.action_CB1.Enabled = False
            Me.action_CB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.action_CB1.ForeColor = Color.Navy
            Me.action_CB1.Location = New Point(&H2D, &H6C)
            Me.action_CB1.Name = "action_CB1"
            Me.action_CB1.Size = New Size(110, 20)
            Me.action_CB1.TabIndex = 7
            Me.actionLabel1.Enabled = False
            Me.actionLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.actionLabel1.Location = New Point(10, &H6F)
            Me.actionLabel1.Name = "actionLabel1"
            Me.actionLabel1.Size = New Size(&H25, 13)
            Me.actionLabel1.Text = "Action"
            Me.filterAction_CB1.Enabled = False
            Me.filterAction_CB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.filterAction_CB1.ForeColor = Color.Navy
            Me.filterAction_CB1.Items.Add("DEFAULT")
            Me.filterAction_CB1.Items.Add("STATE AWARE")
            Me.filterAction_CB1.Items.Add("STATE UNAWARE")
            Me.filterAction_CB1.Location = New Point(&H52, &H52)
            Me.filterAction_CB1.Name = "filterAction_CB1"
            Me.filterAction_CB1.Size = New Size(&H8D, 20)
            Me.filterAction_CB1.TabIndex = 6
            AddHandler Me.filterAction_CB1.SelectedIndexChanged, New EventHandler(AddressOf Me.filterAction_CB1_SelectedIndexChanged)
            Me.filterActionLabel1.Enabled = False
            Me.filterActionLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.filterActionLabel1.Location = New Point(10, &H55)
            Me.filterActionLabel1.Name = "filterActionLabel1"
            Me.filterActionLabel1.Size = New Size(&H3E, 13)
            Me.filterActionLabel1.Text = "Filter Action"
            Me.tagMask_TB1.Enabled = False
            Me.tagMask_TB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMask_TB1.Location = New Point(&H52, &H39)
            Me.tagMask_TB1.Name = "tagMask_TB1"
            Me.tagMask_TB1.Size = New Size(&H8D, &H13)
            Me.tagMask_TB1.TabIndex = 5
            Me.tagMaskLabel1.Enabled = False
            Me.tagMaskLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMaskLabel1.Location = New Point(10, 60)
            Me.tagMaskLabel1.Name = "tagMaskLabel1"
            Me.tagMaskLabel1.Size = New Size(&H3E, 13)
            Me.tagMaskLabel1.Text = "Tag Pattern"
            Me.offset_TB1.Enabled = False
            Me.offset_TB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offset_TB1.Location = New Point(190, &H1F)
            Me.offset_TB1.Name = "offset_TB1"
            Me.offset_TB1.Size = New Size(&H20, &H13)
            Me.offset_TB1.TabIndex = 4
            Me.offset_TB1.Text = "32"
            Me.offsetLabel1.Enabled = False
            Me.offsetLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offsetLabel1.Location = New Point(&H98, &H22)
            Me.offsetLabel1.Name = "offsetLabel1"
            Me.offsetLabel1.Size = New Size(&H23, 13)
            Me.offsetLabel1.Text = "Offset"
            Me.memBank_CB1.Enabled = False
            Me.memBank_CB1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBank_CB1.ForeColor = Color.Navy
            Me.memBank_CB1.Items.Add("EPC")
            Me.memBank_CB1.Items.Add("TID")
            Me.memBank_CB1.Items.Add("USER")
            Me.memBank_CB1.Location = New Point(&H52, &H1F)
            Me.memBank_CB1.Name = "memBank_CB1"
            Me.memBank_CB1.Size = New Size(&H3E, 20)
            Me.memBank_CB1.TabIndex = 3
            Me.memBankLabel1.Enabled = False
            Me.memBankLabel1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBankLabel1.Location = New Point(10, &H22)
            Me.memBankLabel1.Name = "memBankLabel1"
            Me.memBankLabel1.Size = New Size(&H48, 13)
            Me.memBankLabel1.Text = "Memory Bank"
            Me.filter2_TP.Controls.Add(Me.filter_CB2)
            Me.filter2_TP.Controls.Add(Me.antennaID_CB2)
            Me.filter2_TP.Controls.Add(Me.antIDLabel2)
            Me.filter2_TP.Controls.Add(Me.target_CB2)
            Me.filter2_TP.Controls.Add(Me.targetLabel2)
            Me.filter2_TP.Controls.Add(Me.action_CB2)
            Me.filter2_TP.Controls.Add(Me.actionLabel2)
            Me.filter2_TP.Controls.Add(Me.filterAction_CB2)
            Me.filter2_TP.Controls.Add(Me.filterActionLabel2)
            Me.filter2_TP.Controls.Add(Me.tagMask_TB2)
            Me.filter2_TP.Controls.Add(Me.tagMaskLabel2)
            Me.filter2_TP.Controls.Add(Me.offset_TB2)
            Me.filter2_TP.Controls.Add(Me.offsetLabel2)
            Me.filter2_TP.Controls.Add(Me.memBank_CB2)
            Me.filter2_TP.Controls.Add(Me.memBankLabel2)
            Me.filter2_TP.Location = New Point(0, 0)
            Me.filter2_TP.Name = "filter2_TP"
            Me.filter2_TP.Size = New Size(240, &H88)
            Me.filter2_TP.Text = "Filter 2"
            Me.filter_CB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.filter_CB2.Location = New Point(&H94, 5)
            Me.filter_CB2.Name = "filter_CB2"
            Me.filter_CB2.Size = New Size(&H57, 20)
            Me.filter_CB2.TabIndex = 10
            Me.filter_CB2.Text = "Use Filter 2"
            AddHandler Me.filter_CB2.CheckStateChanged, New EventHandler(AddressOf Me.filter_CB2_CheckStateChanged)
            Me.antennaID_CB2.Enabled = False
            Me.antennaID_CB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antennaID_CB2.ForeColor = Color.Navy
            Me.antennaID_CB2.Location = New Point(&H52, 5)
            Me.antennaID_CB2.Name = "antennaID_CB2"
            Me.antennaID_CB2.Size = New Size(&H3E, 20)
            Me.antennaID_CB2.TabIndex = 9
            Me.antIDLabel2.Enabled = False
            Me.antIDLabel2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antIDLabel2.Location = New Point(10, 10)
            Me.antIDLabel2.Name = "antIDLabel2"
            Me.antIDLabel2.Size = New Size(&H3D, &H13)
            Me.antIDLabel2.Text = "Antenna ID"
            Me.target_CB2.Enabled = False
            Me.target_CB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.target_CB2.ForeColor = Color.Navy
            Me.target_CB2.Items.Add("S0")
            Me.target_CB2.Items.Add("S1")
            Me.target_CB2.Items.Add("S2")
            Me.target_CB2.Items.Add("S3")
            Me.target_CB2.Location = New Point(190, &H6C)
            Me.target_CB2.Name = "target_CB2"
            Me.target_CB2.Size = New Size(&H21, 20)
            Me.target_CB2.TabIndex = &H10
            Me.targetLabel2.Enabled = False
            Me.targetLabel2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.targetLabel2.Location = New Point(&H9B, &H6F)
            Me.targetLabel2.Name = "targetLabel2"
            Me.targetLabel2.Size = New Size(&H24, 13)
            Me.targetLabel2.Text = "Target"
            Me.action_CB2.Enabled = False
            Me.action_CB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.action_CB2.ForeColor = Color.Navy
            Me.action_CB2.Location = New Point(&H2D, &H6C)
            Me.action_CB2.Name = "action_CB2"
            Me.action_CB2.Size = New Size(110, 20)
            Me.action_CB2.TabIndex = 15
            Me.actionLabel2.Enabled = False
            Me.actionLabel2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.actionLabel2.Location = New Point(10, &H6F)
            Me.actionLabel2.Name = "actionLabel2"
            Me.actionLabel2.Size = New Size(&H25, 13)
            Me.actionLabel2.Text = "Action"
            Me.filterAction_CB2.Enabled = False
            Me.filterAction_CB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.filterAction_CB2.ForeColor = Color.Navy
            Me.filterAction_CB2.Items.Add("DEFAULT")
            Me.filterAction_CB2.Items.Add("STATE AWARE")
            Me.filterAction_CB2.Items.Add("STATE UNAWARE")
            Me.filterAction_CB2.Location = New Point(&H52, &H52)
            Me.filterAction_CB2.Name = "filterAction_CB2"
            Me.filterAction_CB2.Size = New Size(&H8D, 20)
            Me.filterAction_CB2.TabIndex = 14
            AddHandler Me.filterAction_CB2.SelectedIndexChanged, New EventHandler(AddressOf Me.filterAction_CB2_SelectedIndexChanged)
            Me.filterActionLabel2.Enabled = False
            Me.filterActionLabel2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.filterActionLabel2.Location = New Point(10, &H55)
            Me.filterActionLabel2.Name = "filterActionLabel2"
            Me.filterActionLabel2.Size = New Size(&H3E, 13)
            Me.filterActionLabel2.Text = "Filter Action"
            Me.tagMask_TB2.Enabled = False
            Me.tagMask_TB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMask_TB2.Location = New Point(&H52, &H39)
            Me.tagMask_TB2.Name = "tagMask_TB2"
            Me.tagMask_TB2.Size = New Size(&H8D, &H13)
            Me.tagMask_TB2.TabIndex = 13
            Me.tagMaskLabel2.Enabled = False
            Me.tagMaskLabel2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagMaskLabel2.Location = New Point(10, 60)
            Me.tagMaskLabel2.Name = "tagMaskLabel2"
            Me.tagMaskLabel2.Size = New Size(&H3E, 13)
            Me.tagMaskLabel2.Text = "Tag Pattern"
            Me.offset_TB2.Enabled = False
            Me.offset_TB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offset_TB2.Location = New Point(190, &H1F)
            Me.offset_TB2.Name = "offset_TB2"
            Me.offset_TB2.Size = New Size(&H20, &H13)
            Me.offset_TB2.TabIndex = 12
            Me.offset_TB2.Text = "32"
            Me.offsetLabel2.Enabled = False
            Me.offsetLabel2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.offsetLabel2.Location = New Point(&H98, &H22)
            Me.offsetLabel2.Name = "offsetLabel2"
            Me.offsetLabel2.Size = New Size(&H23, 13)
            Me.offsetLabel2.Text = "Offset"
            Me.memBank_CB2.Enabled = False
            Me.memBank_CB2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBank_CB2.ForeColor = Color.Navy
            Me.memBank_CB2.Items.Add("EPC")
            Me.memBank_CB2.Items.Add("TID")
            Me.memBank_CB2.Items.Add("USER")
            Me.memBank_CB2.Location = New Point(&H52, &H1F)
            Me.memBank_CB2.Name = "memBank_CB2"
            Me.memBank_CB2.Size = New Size(&H3E, 20)
            Me.memBank_CB2.TabIndex = 11
            Me.memBankLabel2.Enabled = False
            Me.memBankLabel2.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.memBankLabel2.Location = New Point(10, &H22)
            Me.memBankLabel2.Name = "memBankLabel2"
            Me.memBankLabel2.Size = New Size(&H48, 13)
            Me.memBankLabel2.Text = "Memory Bank"
            Me.presFilterButton.Location = New Point(&HB6, &HA4)
            Me.presFilterButton.Name = "presFilterButton"
            Me.presFilterButton.Size = New Size(&H33, 20)
            Me.presFilterButton.TabIndex = &H11
            Me.presFilterButton.Text = "Apply"
            AddHandler Me.presFilterButton.Click, New EventHandler(AddressOf Me.preFilterButton_Click)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.presFilterButton)
            MyBase.Controls.Add(Me.tabControl1)
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "PreFilterForm"
            Me.Text = "PreFilter"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.PreFilterForm_Load)
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.PreFilterForm_Closing)
            Me.tabControl1.ResumeLayout(False)
            Me.filter1_TP.ResumeLayout(False)
            Me.filter2_TP.ResumeLayout(False)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub preFilterButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim index As Integer
                If Me.m_AppForm.m_IsConnected Then
                    Me.m_AppForm.m_ReaderAPI.Actions.PreFilters.DeleteAll()
                End If
                If Me.filter_CB1.Checked Then
                    Dim filter1 As New PreFilter
                    filter1.AntennaID = (Me.antennaID_CB1.SelectedIndex)
                    filter1.MemoryBank = DirectCast((Me.memBank_CB1.SelectedIndex + 1), MEMORY_BANK)
                    Dim filterMaskLength As Integer = (Me.tagMask_TB1.Text.Length / 2)
                    Dim filterMask As Byte() = New Byte(filterMaskLength - 1) {}

                    For index = 0 To filterMaskLength - 1
                        filterMask(index) = Byte.Parse(Me.tagMask_TB1.Text.Substring((index * 2), 2), NumberStyles.HexNumber)
                    Next index
                    filter1.TagPattern = filterMask
                    filter1.TagPatternBitCount = (filterMaskLength * 8)
                    filter1.BitOffset = UInt16.Parse(Me.offset_TB1.Text)
                    filter1.FilterAction = DirectCast(Me.filterAction_CB1.SelectedIndex, FILTER_ACTION)
                    If (filter1.FilterAction = FILTER_ACTION.FILTER_ACTION_STATE_AWARE) Then
                        filter1.StateAwareAction.Action = DirectCast(Convert.ToInt32(Me.action_CB1.SelectedIndex / 2), STATE_AWARE_ACTION)
                        filter1.StateAwareAction.Target = DirectCast(Me.target_CB1.SelectedIndex, TARGET)
                    ElseIf (filter1.FilterAction = FILTER_ACTION.FILTER_ACTION_STATE_UNAWARE) Then
                        filter1.StateUnawareAction.Action = DirectCast(Me.action_CB1.SelectedIndex, STATE_UNAWARE_ACTION)
                    End If
                    If Me.m_AppForm.m_IsConnected Then
                        Me.m_AppForm.m_ReaderAPI.Actions.PreFilters.Add(filter1)
                    End If
                End If
                If Me.filter_CB2.Checked Then
                    Dim filter2 As New PreFilter
                    filter2.AntennaID = Me.antennaID_CB2.SelectedIndex
                    filter2.MemoryBank = DirectCast((Me.memBank_CB2.SelectedIndex + 1), MEMORY_BANK)
                    Dim filterMaskLength2 As Integer = (Me.tagMask_TB2.Text.Length / 2)
                    Dim filterMask2 As Byte() = New Byte(filterMaskLength2 - 1) {}

                    For index = 0 To filterMaskLength2 - 1
                        filterMask2(index) = Byte.Parse(Me.tagMask_TB2.Text.Substring((index * 2), 2), NumberStyles.HexNumber)
                    Next index
                    filter2.TagPattern = filterMask2
                    filter2.TagPatternBitCount = (filterMaskLength2 * 8)
                    filter2.BitOffset = UInt16.Parse(Me.offset_TB2.Text)
                    filter2.FilterAction = DirectCast(Me.filterAction_CB2.SelectedIndex, FILTER_ACTION)
                    If (filter2.FilterAction = FILTER_ACTION.FILTER_ACTION_STATE_AWARE) Then
                        filter2.StateAwareAction.Action = DirectCast(Convert.ToInt32(Me.action_CB2.SelectedIndex / 2), STATE_AWARE_ACTION)
                        filter2.StateAwareAction.Target = DirectCast(Me.target_CB2.SelectedIndex, TARGET)
                    ElseIf (filter2.FilterAction = FILTER_ACTION.FILTER_ACTION_STATE_UNAWARE) Then
                        filter2.StateUnawareAction.Action = DirectCast(Me.action_CB2.SelectedIndex, STATE_UNAWARE_ACTION)
                    End If
                    If Me.m_AppForm.m_IsConnected Then
                        Me.m_AppForm.m_ReaderAPI.Actions.PreFilters.Add(filter2)
                    End If
                End If
                MyBase.Close()
            Catch ofe As OperationFailureException
                Me.m_AppForm.notifyUser(ofe.VendorMessage, "Pre-Filter")
            Catch iue As InvalidUsageException
                Me.m_AppForm.notifyUser(iue.Info, "Pre-Filter")
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Pre-Filter")
            End Try
        End Sub

        Private Sub PreFilterForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
        End Sub

        Private Sub PreFilterForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If (Me.m_AppForm.m_ReaderAPI.IsConnected AndAlso Not Me.m_isLoaded) Then
                    Dim antID As UInt16() = Me.m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas
                    Me.antennaID_CB1.Items.Add("0")
                    Me.antennaID_CB2.Items.Add("0")
                    Dim id As UInt16
                    For Each id In antID
                        Me.antennaID_CB1.Items.Add(id)
                        Me.antennaID_CB2.Items.Add(id)
                    Next
                    Me.antennaID_CB1.SelectedIndex = 0
                    Me.antennaID_CB2.SelectedIndex = 0
                    Me.memBank_CB1.SelectedIndex = 0
                    Me.memBank_CB2.SelectedIndex = 0
                    Me.filterAction_CB1.SelectedIndex = 0
                    Me.filterAction_CB2.SelectedIndex = 0
                    Me.m_isLoaded = True
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Pre-Filter")
            End Try
        End Sub


        ' Fields
        Private action_CB1 As ComboBox
        Private action_CB2 As ComboBox
        Private actionLabel1 As Label
        Private actionLabel2 As Label
        Private antennaID_CB1 As ComboBox
        Private antennaID_CB2 As ComboBox
        Private antIDLabel1 As Label
        Private antIDLabel2 As Label
        Private components As IContainer = Nothing
        Private filter_CB1 As CheckBox
        Private filter_CB2 As CheckBox
        Private filter1_TP As TabPage
        Private filter2_TP As TabPage
        Private filterAction_CB1 As ComboBox
        Private filterAction_CB2 As ComboBox
        Private filterActionLabel1 As Label
        Private filterActionLabel2 As Label
        Private m_AppForm As AppForm
        Private m_isLoaded As Boolean
        Private mainMenu1 As MainMenu
        Private memBank_CB1 As ComboBox
        Private memBank_CB2 As ComboBox
        Private memBankLabel1 As Label
        Private memBankLabel2 As Label
        Private offset_TB1 As TextBox
        Private offset_TB2 As TextBox
        Private offsetLabel1 As Label
        Private offsetLabel2 As Label
        Private presFilterButton As Button
        Private tabControl1 As TabControl
        Private tagMask_TB1 As TextBox
        Private tagMask_TB2 As TextBox
        Private tagMaskLabel1 As Label
        Private tagMaskLabel2 As Label
        Private target_CB1 As ComboBox
        Private target_CB2 As ComboBox
        Private targetLabel1 As Label
        Private targetLabel2 As Label
    End Class
End Namespace

