Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class TriggerForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Private Sub backTag_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.backTag_CB.SelectedIndex = 1) Then
                Me.backTag_TB.Enabled = False
            End If
        End Sub

        Private Sub ClearStartGroupBox()
            Me.start_TP.Controls.Remove(Me.startDateLabel)
            Me.start_TP.Controls.Remove(Me.startDateTimePicker)
            Me.start_TP.Controls.Remove(Me.startPeriodLabel)
            Me.start_TP.Controls.Remove(Me.startperiod_TB)
            Me.start_TP.Controls.Remove(Me.startLowHigh_CB)
            Me.start_TP.Controls.Remove(Me.startHighLow_CB)
            Me.start_TP.Controls.Remove(Me.startEventLabel)
            Me.start_TP.Controls.Remove(Me.startPort_CB)
            Me.start_TP.Controls.Remove(Me.startPortLabel)
            Me.start_TP.Controls.Remove(Me.startTriggerReleased_CB)
            Me.start_TP.Controls.Remove(Me.startTriggerPressed_CB)
            Me.start_TP.Controls.Remove(Me.stopTriggerPressed_CB)
        End Sub

        Private Sub ClearStopGroupBox()
            Me.stop_TP.Controls.Remove(Me.stopDuration_TB)
            Me.stop_TP.Controls.Remove(Me.stopDurationLabel)
            Me.stop_TP.Controls.Remove(Me.stopTimeout_TB)
            Me.stop_TP.Controls.Remove(Me.stopTimeoutLabel)
            Me.stop_TP.Controls.Remove(Me.stopLowHigh_CB)
            Me.stop_TP.Controls.Remove(Me.stopHighLow_CB)
            Me.stop_TP.Controls.Remove(Me.stopEventLabel)
            Me.stop_TP.Controls.Remove(Me.stopPort_CB)
            Me.stop_TP.Controls.Remove(Me.stopPortLabel)
            Me.stop_TP.Controls.Remove(Me.stopTagObservationLabel)
            Me.stop_TP.Controls.Remove(Me.stopTagObservation_TB)
            Me.stop_TP.Controls.Remove(Me.stopTagObservTimeoutLabel)
            Me.stop_TP.Controls.Remove(Me.stopTagObservTimeout_TB)
            Me.stop_TP.Controls.Remove(Me.stopNAttemptsLabel)
            Me.stop_TP.Controls.Remove(Me.stopNAttempts_TB)
            Me.stop_TP.Controls.Remove(Me.stopNAttemptsTimeoutLabel)
            Me.stop_TP.Controls.Remove(Me.stopNAttemptsTimeout_TB)
            Me.stop_TP.Controls.Remove(Me.stopTriggerPressed_CB)
            Me.stop_TP.Controls.Remove(Me.stopTriggerReleased_CB)
            Me.stop_TP.Controls.Remove(Me.stopTriggerTimeout_TB)
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Public Function getTriggerInfo() As TriggerInfo
            If (Nothing Is Me.m_TriggerInfo) Then
                Me.m_TriggerInfo = New TriggerInfo
            End If
            Me.m_TriggerInfo.EnableTagEventReport = Me.m_AppForm.autonomousMode_CB.Checked
            Return Me.m_TriggerInfo
        End Function

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.tabControl = New TabControl
            Me.start_TP = New TabPage
            Me.stop_TP = New TabPage
            Me.stopTriggerTimeout_TB = New TextBox
            Me.stopTimeoutLabel = New Label
            Me.stopTriggerPressed_CB = New CheckBox
            Me.stopTriggerReleased_CB = New CheckBox
            Me.stopEventLabel = New Label
            Me.report_TP = New TabPage
            Me.backTag_TB = New TextBox
            Me.invisibleTag_TB = New TextBox
            Me.label3 = New Label
            Me.label2 = New Label
            Me.newTag_TB = New TextBox
            Me.label1 = New Label
            Me.backTag_CB = New ComboBox
            Me.invisibleTag_CB = New ComboBox
            Me.newTag_CB = New ComboBox
            Me.stopNAttemptsLabel = New Label
            Me.stopNAttempts_TB = New TextBox
            Me.stopNAttemptsTimeoutLabel = New Label
            Me.stopNAttemptsTimeout_TB = New TextBox
            Me.stopTimeout_TB = New TextBox
            Me.stopLowHigh_CB = New CheckBox
            Me.stopHighLow_CB = New CheckBox
            Me.stopPort_CB = New ComboBox
            Me.stopPortLabel = New Label
            Me.stopTagObservationLabel = New Label
            Me.stopTagObservation_TB = New TextBox
            Me.stopTagObservTimeoutLabel = New Label
            Me.stopTagObservTimeout_TB = New TextBox
            Me.stopTriggerType_CB = New ComboBox
            Me.stopTriggerTypeLabel = New Label
            Me.startTriggerType_CB = New ComboBox
            Me.startTriggerTypeLabel = New Label
            Me.startTriggerPressed_CB = New CheckBox
            Me.startTriggerReleased_CB = New CheckBox
            Me.startEventLabel = New Label
            Me.startLowHigh_CB = New CheckBox
            Me.startHighLow_CB = New CheckBox
            Me.startPort_CB = New ComboBox
            Me.startPortLabel = New Label
            Me.stopDurationLabel = New Label
            Me.stopDuration_TB = New TextBox
            Me.startDateLabel = New Label
            Me.startDateTimePicker = New DateTimePicker
            Me.startPeriodLabel = New Label
            Me.startperiod_TB = New TextBox
            Me.tagReportTriggerTB = New TextBox
            Me.tagReportTriggerLabel = New Label
            Me.triggerApplyButton = New Button
            Me.tabControl.SuspendLayout
            Me.stop_TP.SuspendLayout
            Me.report_TP.SuspendLayout
            MyBase.SuspendLayout
            Me.tabControl.Anchor = (AnchorStyles.Left Or (AnchorStyles.Bottom Or AnchorStyles.Top))
            Me.tabControl.Controls.Add(Me.start_TP)
            Me.tabControl.Controls.Add(Me.stop_TP)
            Me.tabControl.Controls.Add(Me.report_TP)
            Me.tabControl.Dock = DockStyle.None
            Me.tabControl.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.tabControl.Location = New Point(0, 0)
            Me.tabControl.Name = "tabControl"
            Me.tabControl.SelectedIndex = 0
            Me.tabControl.Size = New Size(240, &H9F)
            Me.tabControl.TabIndex = 0
            Me.start_TP.Location = New Point(0, 0)
            Me.start_TP.Name = "start_TP"
            Me.start_TP.Size = New Size(240, &H88)
            Me.start_TP.Text = "Start Trigger"
            Me.stop_TP.Location = New Point(0, 0)
            Me.stop_TP.Name = "stop_TP"
            Me.stop_TP.Size = New Size(240, &H88)
            Me.stop_TP.Text = "Stop Trigger"
            Me.stopTriggerTimeout_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTriggerTimeout_TB.Location = New Point(&H5F, &H47)
            Me.stopTriggerTimeout_TB.Name = "stopTriggerTimeout_TB"
            Me.stopTriggerTimeout_TB.Size = New Size(120, &H13)
            Me.stopTriggerTimeout_TB.TabIndex = &H13
            Me.stopTimeoutLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTimeoutLabel.Location = New Point(7, &H47)
            Me.stopTimeoutLabel.Name = "stopTimeoutLabel"
            Me.stopTimeoutLabel.Size = New Size(&H43, &H15)
            Me.stopTimeoutLabel.Text = "Time Out"
            Me.stopTriggerPressed_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTriggerPressed_CB.Location = New Point(&H5F, &H76)
            Me.stopTriggerPressed_CB.Name = "stopTriggerPressed_CB"
            Me.stopTriggerPressed_CB.Size = New Size(130, 20)
            Me.stopTriggerPressed_CB.TabIndex = 14
            Me.stopTriggerPressed_CB.Text = "Trigger Pressed"
            AddHandler Me.stopTriggerPressed_CB.Click, New EventHandler(AddressOf Me.stopTriggerPressed_CB_Click)
            Me.stopTriggerReleased_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTriggerReleased_CB.Location = New Point(&H5F, &H60)
            Me.stopTriggerReleased_CB.Name = "stopTriggerReleased_CB"
            Me.stopTriggerReleased_CB.Size = New Size(130, 20)
            Me.stopTriggerReleased_CB.TabIndex = 13
            Me.stopTriggerReleased_CB.Text = "Trigger Released"
            AddHandler Me.stopTriggerReleased_CB.Click, New EventHandler(AddressOf Me.stopTriggerReleased_CB_Click)
            Me.stopEventLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopEventLabel.Location = New Point(7, &H61)
            Me.stopEventLabel.Name = "stopEventLabel"
            Me.stopEventLabel.Size = New Size(&H35, 20)
            Me.stopEventLabel.Text = "Event"
            Me.report_TP.Controls.Add(Me.backTag_TB)
            Me.report_TP.Controls.Add(Me.invisibleTag_TB)
            Me.report_TP.Controls.Add(Me.label3)
            Me.report_TP.Controls.Add(Me.label2)
            Me.report_TP.Controls.Add(Me.newTag_TB)
            Me.report_TP.Controls.Add(Me.label1)
            Me.report_TP.Controls.Add(Me.backTag_CB)
            Me.report_TP.Controls.Add(Me.invisibleTag_CB)
            Me.report_TP.Controls.Add(Me.newTag_CB)
            Me.report_TP.Location = New Point(0, 0)
            Me.report_TP.Name = "report_TP"
            Me.report_TP.Size = New Size(&HE8, &H85)
            Me.report_TP.Text = "Report Trigger"
            Me.backTag_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.backTag_TB.Location = New Point(&HAE, 100)
            Me.backTag_TB.Name = "backTag_TB"
            Me.backTag_TB.Size = New Size(&H3B, &H13)
            Me.backTag_TB.TabIndex = 10
            Me.backTag_TB.Text = "500"
            Me.invisibleTag_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.invisibleTag_TB.Location = New Point(&HAE, &H3E)
            Me.invisibleTag_TB.Name = "invisibleTag_TB"
            Me.invisibleTag_TB.Size = New Size(&H3B, &H13)
            Me.invisibleTag_TB.TabIndex = 9
            Me.invisibleTag_TB.Text = "500"
            Me.label3.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.label3.Location = New Point(7, 100)
            Me.label3.Name = "label3"
            Me.label3.Size = New Size(&H52, 30)
            Me.label3.Text = "Tag back to visibility"
            Me.label2.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.label2.Location = New Point(7, &H3E)
            Me.label2.Name = "label2"
            Me.label2.Size = New Size(&H52, 20)
            Me.label2.Text = "Tag Invisible"
            Me.newTag_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.newTag_TB.Location = New Point(&HAE, &H18)
            Me.newTag_TB.Name = "newTag_TB"
            Me.newTag_TB.Size = New Size(&H3B, &H13)
            Me.newTag_TB.TabIndex = 4
            Me.newTag_TB.Text = "500"
            Me.label1.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.label1.Location = New Point(7, &H18)
            Me.label1.Name = "label1"
            Me.label1.Size = New Size(&H41, 20)
            Me.label1.Text = "New Tag"
            Me.backTag_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.backTag_CB.Items.Add("Never")
            Me.backTag_CB.Items.Add("Immediate")
            Me.backTag_CB.Items.Add("Moderated")
            Me.backTag_CB.Location = New Point(&H5F, 100)
            Me.backTag_CB.Name = "backTag_CB"
            Me.backTag_CB.Size = New Size(&H47, 20)
            Me.backTag_CB.TabIndex = 2
            AddHandler Me.backTag_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.backTag_CB_SelectedIndexChanged)
            Me.invisibleTag_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.invisibleTag_CB.Items.Add("Never")
            Me.invisibleTag_CB.Items.Add("Immediate")
            Me.invisibleTag_CB.Items.Add("Moderated")
            Me.invisibleTag_CB.Location = New Point(&H5F, &H3D)
            Me.invisibleTag_CB.Name = "invisibleTag_CB"
            Me.invisibleTag_CB.Size = New Size(&H47, 20)
            Me.invisibleTag_CB.TabIndex = 1
            AddHandler Me.invisibleTag_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.invisibleTag_CB_SelectedIndexChanged)
            Me.newTag_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.newTag_CB.Items.Add("Never")
            Me.newTag_CB.Items.Add("Immediate")
            Me.newTag_CB.Items.Add("Moderated")
            Me.newTag_CB.Location = New Point(&H5F, &H16)
            Me.newTag_CB.Name = "newTag_CB"
            Me.newTag_CB.Size = New Size(&H47, 20)
            Me.newTag_CB.TabIndex = 0
            AddHandler Me.newTag_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.newTag_CB_SelectedIndexChanged)
            Me.stopNAttemptsLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopNAttemptsLabel.Location = New Point(7, &H31)
            Me.stopNAttemptsLabel.Name = "stopNAttemptsLabel"
            Me.stopNAttemptsLabel.Size = New Size(&H52, &H24)
            Me.stopNAttemptsLabel.Text = "No. of Attempts"
            Me.stopNAttempts_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopNAttempts_TB.Location = New Point(&H5F, &H30)
            Me.stopNAttempts_TB.Name = "stopNAttempts_TB"
            Me.stopNAttempts_TB.Size = New Size(120, &H13)
            Me.stopNAttempts_TB.TabIndex = &H16
            Me.stopNAttemptsTimeoutLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopNAttemptsTimeoutLabel.Location = New Point(7, &H56)
            Me.stopNAttemptsTimeoutLabel.Name = "stopNAttemptsTimeoutLabel"
            Me.stopNAttemptsTimeoutLabel.Size = New Size(&H52, 20)
            Me.stopNAttemptsTimeoutLabel.Text = "Time Out"
            Me.stopNAttemptsTimeout_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopNAttemptsTimeout_TB.Location = New Point(&H5F, &H55)
            Me.stopNAttemptsTimeout_TB.Name = "stopNAttemptsTimeout_TB"
            Me.stopNAttemptsTimeout_TB.Size = New Size(120, &H13)
            Me.stopNAttemptsTimeout_TB.TabIndex = &H17
            Me.stopTimeout_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTimeout_TB.Location = New Point(&H5F, &H47)
            Me.stopTimeout_TB.Name = "stopTimeout_TB"
            Me.stopTimeout_TB.Size = New Size(120, &H13)
            Me.stopTimeout_TB.TabIndex = &H13
            Me.stopLowHigh_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopLowHigh_CB.Location = New Point(&H5F, &H77)
            Me.stopLowHigh_CB.Name = "stopLowHigh_CB"
            Me.stopLowHigh_CB.Size = New Size(120, 20)
            Me.stopLowHigh_CB.TabIndex = 14
            Me.stopLowHigh_CB.Text = "Low To High"
            Me.stopHighLow_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopHighLow_CB.Location = New Point(&H5F, &H61)
            Me.stopHighLow_CB.Name = "stopHighLow_CB"
            Me.stopHighLow_CB.Size = New Size(120, 20)
            Me.stopHighLow_CB.TabIndex = 13
            Me.stopHighLow_CB.Text = "High To Low"
            Me.stopPort_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopPort_CB.Location = New Point(&H5F, &H29)
            Me.stopPort_CB.Name = "stopPort_CB"
            Me.stopPort_CB.Size = New Size(120, 20)
            Me.stopPort_CB.TabIndex = 12
            Me.stopPortLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopPortLabel.Location = New Point(7, &H29)
            Me.stopPortLabel.Name = "stopPortLabel"
            Me.stopPortLabel.Size = New Size(&H43, 20)
            Me.stopPortLabel.Text = "Port"
            Me.stopTagObservationLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTagObservationLabel.Location = New Point(7, &H2F)
            Me.stopTagObservationLabel.Name = "stopTagObservationLabel"
            Me.stopTagObservationLabel.Size = New Size(&H52, &H26)
            Me.stopTagObservationLabel.Text = "Tag Observation"
            Me.stopTagObservation_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTagObservation_TB.Location = New Point(&H5F, &H30)
            Me.stopTagObservation_TB.Name = "stopTagObservation_TB"
            Me.stopTagObservation_TB.Size = New Size(120, &H13)
            Me.stopTagObservation_TB.TabIndex = 20
            Me.stopTagObservTimeoutLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTagObservTimeoutLabel.Location = New Point(7, &H56)
            Me.stopTagObservTimeoutLabel.Name = "stopTagObservTimeoutLabel"
            Me.stopTagObservTimeoutLabel.Size = New Size(&H39, 20)
            Me.stopTagObservTimeoutLabel.Text = "Time Out"
            Me.stopTagObservTimeout_TB.Location = New Point(&H5F, &H55)
            Me.stopTagObservTimeout_TB.Name = "stopTagObservTimeout_TB"
            Me.stopTagObservTimeout_TB.Size = New Size(120, &H15)
            Me.stopTagObservTimeout_TB.TabIndex = &H15
            Me.stopTriggerType_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTriggerType_CB.Items.Add("Immediate")
            Me.stopTriggerType_CB.Items.Add("Duration")
            Me.stopTriggerType_CB.Items.Add("GPI with Timeout")
            Me.stopTriggerType_CB.Items.Add("Tag Observation")
            Me.stopTriggerType_CB.Items.Add("N Attempts")
            Me.stopTriggerType_CB.Items.Add("Handheld Trigger with Timeout")
            Me.stopTriggerType_CB.Location = New Point(&H5F, 11)
            Me.stopTriggerType_CB.Name = "stopTriggerType_CB"
            Me.stopTriggerType_CB.Size = New Size(120, 20)
            Me.stopTriggerType_CB.TabIndex = 3
            AddHandler Me.stopTriggerType_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.stopTriggerType_CB_SelectedIndexChanged)
            Me.stopTriggerTypeLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopTriggerTypeLabel.Location = New Point(7, 14)
            Me.stopTriggerTypeLabel.Name = "stopTriggerTypeLabel"
            Me.stopTriggerTypeLabel.Size = New Size(&H52, &H11)
            Me.stopTriggerTypeLabel.Text = "Trigger Type"
            Me.startTriggerType_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startTriggerType_CB.Items.Add("Immediate")
            Me.startTriggerType_CB.Items.Add("Periodic")
            Me.startTriggerType_CB.Items.Add("GPI")
            Me.startTriggerType_CB.Items.Add("Handheld Trigger")
            Me.startTriggerType_CB.Location = New Point(&H5F, 11)
            Me.startTriggerType_CB.Name = "startTriggerType_CB"
            Me.startTriggerType_CB.Size = New Size(120, 20)
            Me.startTriggerType_CB.TabIndex = 3
            AddHandler Me.startTriggerType_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.startTriggerType_CB_SelectedIndexChanged)
            Me.startTriggerTypeLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startTriggerTypeLabel.Location = New Point(7, 14)
            Me.startTriggerTypeLabel.Name = "startTriggerTypeLabel"
            Me.startTriggerTypeLabel.Size = New Size(&H58, &H11)
            Me.startTriggerTypeLabel.Text = "Trigger Type"
            Me.startTriggerPressed_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startTriggerPressed_CB.Location = New Point(&H5F, 80)
            Me.startTriggerPressed_CB.Name = "startTriggerPressed_CB"
            Me.startTriggerPressed_CB.Size = New Size(130, 20)
            Me.startTriggerPressed_CB.TabIndex = 9
            Me.startTriggerPressed_CB.Text = "Trigger Pressed"
            AddHandler Me.startTriggerPressed_CB.Click, New EventHandler(AddressOf Me.startTriggerPressed_CB_Click)
            Me.startTriggerReleased_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startTriggerReleased_CB.Location = New Point(&H5F, &H36)
            Me.startTriggerReleased_CB.Name = "startTriggerReleased_CB"
            Me.startTriggerReleased_CB.Size = New Size(130, 20)
            Me.startTriggerReleased_CB.TabIndex = 8
            Me.startTriggerReleased_CB.Text = "Trigger Released"
            AddHandler Me.startTriggerReleased_CB.Click, New EventHandler(AddressOf Me.startTriggerReleased_CB_Click)
            Me.startEventLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startEventLabel.Location = New Point(7, &H36)
            Me.startEventLabel.Name = "startEventLabel"
            Me.startEventLabel.Size = New Size(&H43, 20)
            Me.startEventLabel.Text = "Event"
            Me.startLowHigh_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startLowHigh_CB.Location = New Point(&H5F, &H6A)
            Me.startLowHigh_CB.Name = "startLowHigh_CB"
            Me.startLowHigh_CB.Size = New Size(&H76, 20)
            Me.startLowHigh_CB.TabIndex = 9
            Me.startLowHigh_CB.Text = "Low To High"
            Me.startHighLow_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startHighLow_CB.Location = New Point(&H5F, 80)
            Me.startHighLow_CB.Name = "startHighLow_CB"
            Me.startHighLow_CB.Size = New Size(&H76, 20)
            Me.startHighLow_CB.TabIndex = 8
            Me.startHighLow_CB.Text = "High To Low"
            Me.startPort_CB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startPort_CB.Location = New Point(&H5F, &H34)
            Me.startPort_CB.Name = "startPort_CB"
            Me.startPort_CB.Size = New Size(120, 20)
            Me.startPort_CB.TabIndex = 6
            Me.startPortLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startPortLabel.Location = New Point(7, &H36)
            Me.startPortLabel.Name = "startPortLabel"
            Me.startPortLabel.Size = New Size(&H43, 20)
            Me.startPortLabel.Text = "Port"
            Me.stopDurationLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopDurationLabel.Location = New Point(7, 60)
            Me.stopDurationLabel.Name = "stopDurationLabel"
            Me.stopDurationLabel.Size = New Size(&H40, 20)
            Me.stopDurationLabel.Text = "Duration"
            Me.stopDuration_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.stopDuration_TB.Location = New Point(&H5F, &H3B)
            Me.stopDuration_TB.Name = "stopDuration_TB"
            Me.stopDuration_TB.Size = New Size(120, &H13)
            Me.stopDuration_TB.TabIndex = 6
            Me.startDateLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startDateLabel.Location = New Point(7, &H3F)
            Me.startDateLabel.Name = "startDateLabel"
            Me.startDateLabel.Size = New Size(&H43, 20)
            Me.startDateLabel.Text = "Start Date"
            Me.startDateTimePicker.CustomFormat = "MMM/dd/yy  hh:mm:ss tt"
            Me.startDateTimePicker.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startDateTimePicker.Format = DateTimePickerFormat.Custom
            Me.startDateTimePicker.Location = New Point(&H5F, 60)
            Me.startDateTimePicker.Name = "startDateTimePicker"
            Me.startDateTimePicker.Size = New Size(&H88, 20)
            Me.startDateTimePicker.TabIndex = 5
            Me.startDateTimePicker.Value = New DateTime(&H7D9, 3, &H18, 14, &H19, 0, 0)
            Me.startPeriodLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startPeriodLabel.Location = New Point(7, &H6A)
            Me.startPeriodLabel.Name = "startPeriodLabel"
            Me.startPeriodLabel.Size = New Size(&H38, 20)
            Me.startPeriodLabel.Text = "Period"
            Me.startperiod_TB.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.startperiod_TB.Location = New Point(&H5F, &H67)
            Me.startperiod_TB.Name = "startperiod_TB"
            Me.startperiod_TB.Size = New Size(&H76, &H13)
            Me.startperiod_TB.TabIndex = 8
            Me.tagReportTriggerTB.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.tagReportTriggerTB.Location = New Point(100, &HA4)
            Me.tagReportTriggerTB.Name = "tagReportTriggerTB"
            Me.tagReportTriggerTB.Size = New Size(&H3F, &H15)
            Me.tagReportTriggerTB.TabIndex = 6
            Me.tagReportTriggerTB.Text = "1"
            Me.tagReportTriggerLabel.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.tagReportTriggerLabel.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.tagReportTriggerLabel.Location = New Point(0, &HA6)
            Me.tagReportTriggerLabel.Name = "tagReportTriggerLabel"
            Me.tagReportTriggerLabel.Size = New Size(&H61, &H15)
            Me.tagReportTriggerLabel.Text = "Tag Report Trigger"
            Me.triggerApplyButton.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.triggerApplyButton.Font = New Font("Microsoft Sans Serif", 8!, FontStyle.Regular)
            Me.triggerApplyButton.Location = New Point(&HBA, &HA5)
            Me.triggerApplyButton.Name = "triggerApplyButton"
            Me.triggerApplyButton.Size = New Size(&H33, 20)
            Me.triggerApplyButton.TabIndex = &H12
            Me.triggerApplyButton.Text = "Apply"
            AddHandler Me.triggerApplyButton.Click, New EventHandler(AddressOf Me.triggerApplyButton_Click)
            MyBase.AutoScaleDimensions = New SizeF(96!, 96!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.triggerApplyButton)
            MyBase.Controls.Add(Me.tagReportTriggerTB)
            MyBase.Controls.Add(Me.tagReportTriggerLabel)
            MyBase.Controls.Add(Me.tabControl)
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "TriggerForm"
            Me.Text = "Trigger"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.TriggerForm_Load)
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.TriggerForm_Closing)
            Me.tabControl.ResumeLayout(False)
            Me.stop_TP.ResumeLayout(False)
            Me.report_TP.ResumeLayout(False)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub invisibleTag_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.invisibleTag_CB.SelectedIndex = 1) Then
                Me.invisibleTag_TB.Enabled = False
            End If
        End Sub

        Private Sub newTag_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.newTag_CB.SelectedIndex = 1) Then
                Me.newTag_TB.Enabled = False
            End If
        End Sub

        Public Sub Reset()
            Me.newTag_CB.SelectedIndex = 2
            Me.backTag_CB.SelectedIndex = 2
            Me.invisibleTag_CB.SelectedIndex = 2
            Me.newTag_CB.Enabled = IIf(Me.m_AppForm.m_ReaderAPI.IsConnected, Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported, False)
            Me.backTag_CB.Enabled = IIf(Me.m_AppForm.m_ReaderAPI.IsConnected, Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported, False)
            Me.invisibleTag_CB.Enabled = IIf(Me.m_AppForm.m_ReaderAPI.IsConnected, Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported, False)
            Me.newTag_TB.Enabled = IIf(Me.m_AppForm.m_ReaderAPI.IsConnected, Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported, False)
            Me.backTag_TB.Enabled = IIf(Me.m_AppForm.m_ReaderAPI.IsConnected, Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported, False)
            Me.invisibleTag_TB.Enabled = IIf(Me.m_AppForm.m_ReaderAPI.IsConnected, Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported, False)
        End Sub

        Private Sub startTriggerPressed_CB_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.startTriggerReleased_CB.Checked = False
        End Sub

        Private Sub startTriggerReleased_CB_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.startTriggerPressed_CB.Checked = False
        End Sub
        Private Sub startTriggerType_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.ClearStartGroupBox()
            If (Me.startTriggerType_CB.SelectedIndex <> 0) Then
                If (Me.startTriggerType_CB.SelectedIndex = 1) Then
                    Me.start_TP.Controls.Add(Me.startDateLabel)
                    Me.start_TP.Controls.Add(Me.startDateTimePicker)
                    Me.start_TP.Controls.Add(Me.startPeriodLabel)
                    Me.start_TP.Controls.Add(Me.startperiod_TB)
                ElseIf (Me.startTriggerType_CB.SelectedIndex = 2) Then
                    Me.start_TP.Controls.Add(Me.startLowHigh_CB)
                    Me.start_TP.Controls.Add(Me.startHighLow_CB)
                    Me.start_TP.Controls.Add(Me.startEventLabel)
                    Me.start_TP.Controls.Add(Me.startPort_CB)
                    Me.start_TP.Controls.Add(Me.startPortLabel)
                    Try
                        If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                            Dim numGPIPorts As Integer = Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.NumGPIPorts
                            Me.startPort_CB.Items.Clear()
                            Dim port As Integer = 1
                            Do While (port <= numGPIPorts)
                                Me.startPort_CB.Items.Add(port)
                                port += 1
                            Loop
                            If (numGPIPorts > 0) Then
                                Me.startPort_CB.SelectedIndex = 0
                            End If
                        End If
                    Catch ex As Exception
                        Me.m_AppForm.notifyUser(ex.Message, "Trigger Info")
                    End Try
                ElseIf (Me.startTriggerType_CB.SelectedIndex = 3) Then
                    Me.start_TP.Controls.Add(Me.startTriggerPressed_CB)
                    Me.start_TP.Controls.Add(Me.startTriggerReleased_CB)
                    Me.start_TP.Controls.Add(Me.startEventLabel)
                End If
            End If
        End Sub

        Private Sub stopTriggerPressed_CB_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.stopTriggerReleased_CB.Checked = False
        End Sub

        Private Sub stopTriggerReleased_CB_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.stopTriggerPressed_CB.Checked = False
        End Sub

        Private Sub stopTriggerType_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.ClearStopGroupBox()
            If (Me.stopTriggerType_CB.SelectedIndex <> 0) Then
                If (Me.stopTriggerType_CB.SelectedIndex = 1) Then
                    Me.stop_TP.Controls.Add(Me.stopDurationLabel)
                    Me.stop_TP.Controls.Add(Me.stopDuration_TB)
                ElseIf (Me.stopTriggerType_CB.SelectedIndex = 2) Then
                    Me.stop_TP.Controls.Add(Me.stopTimeout_TB)
                    Me.stop_TP.Controls.Add(Me.stopTimeoutLabel)
                    Me.stop_TP.Controls.Add(Me.stopLowHigh_CB)
                    Me.stop_TP.Controls.Add(Me.stopHighLow_CB)
                    Me.stop_TP.Controls.Add(Me.stopEventLabel)
                    Me.stop_TP.Controls.Add(Me.stopPort_CB)
                    Me.stop_TP.Controls.Add(Me.stopPortLabel)
                    Try
                        If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                            Dim numGPIPorts As Integer = Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.NumGPIPorts
                            Me.stopPort_CB.Items.Clear()
                            Dim port As Integer = 1
                            Do While (port <= numGPIPorts)
                                Me.stopPort_CB.Items.Add(port)
                                port += 1
                            Loop
                            If (numGPIPorts > 0) Then
                                Me.stopPort_CB.SelectedIndex = 0
                            End If
                        End If
                    Catch ex As Exception
                        Me.m_AppForm.notifyUser(ex.Message, "Trigger Info")
                    End Try
                ElseIf (Me.stopTriggerType_CB.SelectedIndex = 3) Then
                    Me.stop_TP.Controls.Add(Me.stopTagObservationLabel)
                    Me.stop_TP.Controls.Add(Me.stopTagObservation_TB)
                    Me.stop_TP.Controls.Add(Me.stopTagObservTimeoutLabel)
                    Me.stop_TP.Controls.Add(Me.stopTagObservTimeout_TB)
                ElseIf (Me.stopTriggerType_CB.SelectedIndex = 4) Then
                    Me.stop_TP.Controls.Add(Me.stopNAttemptsLabel)
                    Me.stop_TP.Controls.Add(Me.stopNAttempts_TB)
                    Me.stop_TP.Controls.Add(Me.stopNAttemptsTimeoutLabel)
                    Me.stop_TP.Controls.Add(Me.stopNAttemptsTimeout_TB)
                ElseIf (Me.stopTriggerType_CB.SelectedIndex = 5) Then
                    Me.stop_TP.Controls.Add(Me.stopTriggerTimeout_TB)
                    Me.stop_TP.Controls.Add(Me.stopTimeoutLabel)
                    Me.stop_TP.Controls.Add(Me.stopTriggerPressed_CB)
                    Me.stop_TP.Controls.Add(Me.stopTriggerReleased_CB)
                    Me.stop_TP.Controls.Add(Me.stopEventLabel)
                End If
            End If
        End Sub

        Private Sub triggerApplyButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                    If (Nothing Is Me.m_TriggerInfo) Then
                        Me.m_TriggerInfo = New TriggerInfo
                    End If
                    Me.m_AppForm.m_DurationTriggerTime = 0
                    If (Me.startTriggerType_CB.SelectedIndex = 0) Then
                        Me.m_TriggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_IMMEDIATE
                    ElseIf (Me.startTriggerType_CB.SelectedIndex = 1) Then
                        Me.m_TriggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_PERIODIC
                        Me.m_TriggerInfo.StartTrigger.Periodic.StartTime = Me.startDateTimePicker.Value
                        Me.m_TriggerInfo.StartTrigger.Periodic.Period = UInt32.Parse(Me.startperiod_TB.Text)
                    ElseIf (Me.startTriggerType_CB.SelectedIndex = 2) Then
                        Me.m_TriggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_GPI
                        Me.m_TriggerInfo.StartTrigger.GPI.PortNumber = (Me.startPort_CB.SelectedIndex + 1)
                        If (Me.startLowHigh_CB.Checked OrElse Me.startLowHigh_CB.Checked) Then
                            Me.m_TriggerInfo.StartTrigger.GPI.GPIEvent = True
                        Else
                            Me.m_TriggerInfo.StartTrigger.GPI.GPIEvent = False
                        End If
                    ElseIf (Me.startTriggerType_CB.SelectedIndex = 3) Then
                        Me.m_TriggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_HANDHELD
                        If Me.startTriggerPressed_CB.Checked Then
                            Me.m_TriggerInfo.StartTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED
                        Else
                            Me.m_TriggerInfo.StartTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED
                        End If
                    End If
                    If (Me.stopTriggerType_CB.SelectedIndex = 0) Then
                        Me.m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_IMMEDIATE
                    ElseIf (Me.stopTriggerType_CB.SelectedIndex = 1) Then
                        Me.m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_DURATION
                        Me.m_TriggerInfo.StopTrigger.Duration = UInt32.Parse(Me.stopDuration_TB.Text)
                        Me.m_AppForm.m_DurationTriggerTime = Me.m_TriggerInfo.StopTrigger.Duration
                    ElseIf (Me.stopTriggerType_CB.SelectedIndex = 2) Then
                        Me.m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_GPI_WITH_TIMEOUT
                        Me.m_TriggerInfo.StopTrigger.GPI.PortNumber = (Me.stopPort_CB.SelectedIndex + 1)
                        Me.m_TriggerInfo.StopTrigger.GPI.Timeout = UInt32.Parse(Me.stopTimeout_TB.Text)
                        If (Me.stopHighLow_CB.Checked OrElse Me.stopLowHigh_CB.Checked) Then
                            Me.m_TriggerInfo.StopTrigger.GPI.GPIEvent = True
                        Else
                            Me.m_TriggerInfo.StopTrigger.GPI.GPIEvent = False
                        End If
                    ElseIf (Me.stopTriggerType_CB.SelectedIndex = 3) Then
                        Me.m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_TAG_OBSERVATION_WITH_TIMEOUT
                        Me.m_TriggerInfo.StopTrigger.TagObservation.N = UInt16.Parse(Me.stopTagObservation_TB.Text)
                        Me.m_TriggerInfo.StopTrigger.TagObservation.Timeout = UInt32.Parse(Me.stopTagObservTimeout_TB.Text)
                    ElseIf (Me.stopTriggerType_CB.SelectedIndex = 4) Then
                        Me.m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_N_ATTEMPTS_WITH_TIMEOUT
                        Me.m_TriggerInfo.StopTrigger.NumAttempts.N = UInt16.Parse(Me.stopNAttempts_TB.Text)
                        Me.m_TriggerInfo.StopTrigger.NumAttempts.Timeout = UInt32.Parse(Me.stopNAttemptsTimeout_TB.Text)
                    ElseIf (Me.stopTriggerType_CB.SelectedIndex = 5) Then
                        Me.m_TriggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_HANDHELD_WITH_TIMEOUT
                        If Me.stopTriggerPressed_CB.Checked Then
                            Me.m_TriggerInfo.StopTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED
                        Else
                            Me.m_TriggerInfo.StopTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED
                        End If
                        Me.m_TriggerInfo.StopTrigger.Handheld.Timeout = UInt32.Parse(Me.stopTriggerTimeout_TB.Text)
                    End If
                    If Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported Then
                        Me.m_TriggerInfo.TagEventReportInfo.ReportNewTagEvent = DirectCast(Me.newTag_CB.SelectedIndex, TAG_EVENT_REPORT_TRIGGER)
                        Me.m_TriggerInfo.TagEventReportInfo.ReportTagBackToVisibilityEvent = DirectCast(Me.backTag_CB.SelectedIndex, TAG_EVENT_REPORT_TRIGGER)
                        Me.m_TriggerInfo.TagEventReportInfo.ReportTagInvisibleEvent = DirectCast(Me.invisibleTag_CB.SelectedIndex, TAG_EVENT_REPORT_TRIGGER)
                        Me.m_TriggerInfo.TagEventReportInfo.NewTagEventModeratedTimeoutMilliseconds = UInt16.Parse(Me.newTag_TB.Text)
                        Me.m_TriggerInfo.TagEventReportInfo.TagBackToVisibilityModeratedTimeoutMilliseconds = UInt16.Parse(Me.backTag_TB.Text)
                        Me.m_TriggerInfo.TagEventReportInfo.TagInvisibleEventModeratedTimeoutMilliseconds = UInt16.Parse(Me.invisibleTag_TB.Text)
                    End If
                    Me.m_TriggerInfo.TagReportTrigger = UInt32.Parse(Me.tagReportTriggerTB.Text)
                    MyBase.Close()
                Else
                    Me.m_AppForm.notifyUser("Please connect to Reader", "Trigger Info")
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Trigger Info")
            End Try
        End Sub

        Private Sub TriggerForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
        End Sub

        Private Sub TriggerForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not Me.m_IsLoaded Then
                Me.start_TP.Controls.Add(Me.startTriggerTypeLabel)
                Me.start_TP.Controls.Add(Me.startTriggerType_CB)
                Me.stop_TP.Controls.Add(Me.stopTriggerTypeLabel)
                Me.stop_TP.Controls.Add(Me.stopTriggerType_CB)
                Me.startperiod_TB.Text = "1"
                Me.tagReportTriggerTB.Text = "0"
                Me.m_IsLoaded = True
                Me.stopTriggerTimeout_TB.Text = "0"
            End If
        End Sub


        ' Fields
        Friend backTag_CB As ComboBox
        Friend backTag_TB As TextBox
        Private components As IContainer = Nothing
        Friend invisibleTag_CB As ComboBox
        Friend invisibleTag_TB As TextBox
        Private label1 As Label
        Private label2 As Label
        Private label3 As Label
        Private m_AppForm As AppForm = Nothing
        Private m_IsLoaded As Boolean
        Private m_TriggerInfo As TriggerInfo = Nothing
        Private mainMenu1 As MainMenu
        Friend newTag_CB As ComboBox
        Friend newTag_TB As TextBox
        Private report_TP As TabPage
        Friend start_TP As TabPage
        Private startDateLabel As Label
        Private startDateTimePicker As DateTimePicker
        Private startEventLabel As Label
        Private startHighLow_CB As CheckBox
        Private startLowHigh_CB As CheckBox
        Private startperiod_TB As TextBox
        Private startPeriodLabel As Label
        Private startPort_CB As ComboBox
        Private startPortLabel As Label
        Private startTriggerPressed_CB As CheckBox
        Private startTriggerReleased_CB As CheckBox
        Private startTriggerType_CB As ComboBox
        Private startTriggerTypeLabel As Label
        Friend stop_TP As TabPage
        Private stopDuration_TB As TextBox
        Private stopDurationLabel As Label
        Private stopEventLabel As Label
        Private stopHighLow_CB As CheckBox
        Private stopLowHigh_CB As CheckBox
        Private stopNAttempts_TB As TextBox
        Private stopNAttemptsLabel As Label
        Private stopNAttemptsTimeout_TB As TextBox
        Private stopNAttemptsTimeoutLabel As Label
        Private stopPort_CB As ComboBox
        Private stopPortLabel As Label
        Private stopTagObservation_TB As TextBox
        Private stopTagObservationLabel As Label
        Private stopTagObservTimeout_TB As TextBox
        Private stopTagObservTimeoutLabel As Label
        Private stopTimeout_TB As TextBox
        Private stopTimeoutLabel As Label
        Private stopTriggerPressed_CB As CheckBox
        Private stopTriggerReleased_CB As CheckBox
        Private stopTriggerTimeout_TB As TextBox
        Private stopTriggerType_CB As ComboBox
        Private stopTriggerTypeLabel As Label
        Private tabControl As TabControl
        Private tagReportTriggerLabel As Label
        Private tagReportTriggerTB As TextBox
        Private triggerApplyButton As Button
    End Class
End Namespace

