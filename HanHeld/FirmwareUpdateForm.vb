Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class FirmwareUpdateForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
            Me.firmwareApplyButton.Enabled = False
        End Sub

        Private Sub browseFileButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim fileDialog As New OpenFileDialog
            If (Me.update_CB.Text = "Radio Firmware Update") Then
                fileDialog.Filter = "Firmware Files|*.a79"
            ElseIf (Me.update_CB.Text = "Radio Config Update") Then
                fileDialog.Filter = "OEM Config Files|*.dmp"
            End If
            fileDialog.InitialDirectory = "\"
            If (fileDialog.ShowDialog = DialogResult.OK) Then
                Me.location_TB.Text = fileDialog.FileName
                Me.firmwareApplyButton.Enabled = True
            Else
                Me.location_TB.Text = ""
                If String.IsNullOrEmpty(Me.location_TB.Text) Then
                    Me.firmwareApplyButton.Enabled = False
                End If
            End If
        End Sub
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub firmwareApplyButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Me.updateWorker()
            Catch ex As OperationFailureException
                Me.m_AppForm.notifyUser(ex.VendorMessage, "Software Update")
            End Try
        End Sub

        Private Sub FirmwareUpdateForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.Reset
            Me.m_IsLoaded = True
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.password_TB = New TextBox
            Me.username_TB = New TextBox
            Me.IPLabel = New Label
            Me.passwordLabel = New Label
            Me.userNameLabel = New Label
            Me.firmwareApplyButton = New Button
            Me.updateDesc_TB = New TextBox
            Me.location_TB = New TextBox
            Me.update_PB = New ProgressBar
            Me.ftp_GB = New Panel
            Me.ftp_label = New Label
            Me.update_CB = New ComboBox
            Me.browseFileButton = New Button
            Me.ftp_GB.SuspendLayout()
            MyBase.SuspendLayout()
            Me.password_TB.Enabled = False
            Me.password_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.password_TB.Location = New Point(90, 30)
            Me.password_TB.Name = "password_TB"
            Me.password_TB.Size = New Size(&H80, &H13)
            Me.password_TB.TabIndex = 1
            Me.username_TB.Enabled = False
            Me.username_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.username_TB.Location = New Point(90, 3)
            Me.username_TB.Name = "username_TB"
            Me.username_TB.Size = New Size(&H80, &H13)
            Me.username_TB.TabIndex = 1
            Me.username_TB.Text = "admin"
            Me.IPLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.IPLabel.Location = New Point(9, 90)
            Me.IPLabel.Name = "IPLabel"
            Me.IPLabel.Size = New Size(60, &H13)
            Me.IPLabel.Text = "Location"
            Me.passwordLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.passwordLabel.Location = New Point(11, &H24)
            Me.passwordLabel.Name = "passwordLabel"
            Me.passwordLabel.Size = New Size(&H35, 13)
            Me.passwordLabel.Text = "Password"
            Me.userNameLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.userNameLabel.Location = New Point(4, &H10)
            Me.userNameLabel.Name = "userNameLabel"
            Me.userNameLabel.Size = New Size(60, 13)
            Me.userNameLabel.Text = "User Name"
            Me.firmwareApplyButton.DialogResult = DialogResult.OK
            Me.firmwareApplyButton.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.firmwareApplyButton.Location = New Point(10, &H72)
            Me.firmwareApplyButton.Name = "firmwareApplyButton"
            Me.firmwareApplyButton.Size = New Size(&H4B, 20)
            Me.firmwareApplyButton.TabIndex = 4
            Me.firmwareApplyButton.Text = "Start"
            AddHandler Me.firmwareApplyButton.Click, New EventHandler(AddressOf Me.firmwareApplyButton_Click)
            Me.updateDesc_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.updateDesc_TB.Location = New Point(9, &HA6)
            Me.updateDesc_TB.Name = "updateDesc_TB"
            Me.updateDesc_TB.ReadOnly = True
            Me.updateDesc_TB.Size = New Size(&HDE, &H13)
            Me.updateDesc_TB.TabIndex = 0
            Me.location_TB.Enabled = False
            Me.location_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.location_TB.Location = New Point(&H3D, &H59)
            Me.location_TB.Name = "location_TB"
            Me.location_TB.Size = New Size(170, &H13)
            Me.location_TB.TabIndex = 2
            Me.update_PB.Location = New Point(9, 140)
            Me.update_PB.Name = "update_PB"
            Me.update_PB.Size = New Size(&HDE, 20)
            Me.ftp_GB.Controls.Add(Me.ftp_label)
            Me.ftp_GB.Controls.Add(Me.password_TB)
            Me.ftp_GB.Controls.Add(Me.username_TB)
            Me.ftp_GB.Controls.Add(Me.passwordLabel)
            Me.ftp_GB.Controls.Add(Me.userNameLabel)
            Me.ftp_GB.Location = New Point(9, 3)
            Me.ftp_GB.Name = "ftp_GB"
            Me.ftp_GB.Size = New Size(&HE4, &H35)
            Me.ftp_label.Font = New Font("Tahoma", 8.0!, FontStyle.Regular)
            Me.ftp_label.Location = New Point(11, 0)
            Me.ftp_label.Name = "ftp_label"
            Me.ftp_label.Size = New Size(&H40, 15)
            Me.ftp_label.Text = "FTP Info"
            Me.update_CB.Location = New Point(9, 60)
            Me.update_CB.Name = "update_CB"
            Me.update_CB.Size = New Size(&HDE, &H16)
            Me.update_CB.TabIndex = 9
            AddHandler Me.update_CB.SelectedValueChanged, New EventHandler(AddressOf Me.update_CB_SelectedValueChanged)
            Me.browseFileButton.Font = New Font("Tahoma", 9!, FontStyle.Regular)
            Me.browseFileButton.Location = New Point(&H9F, &H72)
            Me.browseFileButton.Name = "browseFileButton"
            Me.browseFileButton.Size = New Size(&H48, 20)
            Me.browseFileButton.TabIndex = 13
            Me.browseFileButton.Text = "Browse"
            AddHandler Me.browseFileButton.Click, New EventHandler(AddressOf Me.browseFileButton_Click)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.browseFileButton)
            MyBase.Controls.Add(Me.update_CB)
            MyBase.Controls.Add(Me.ftp_GB)
            MyBase.Controls.Add(Me.update_PB)
            MyBase.Controls.Add(Me.location_TB)
            MyBase.Controls.Add(Me.IPLabel)
            MyBase.Controls.Add(Me.firmwareApplyButton)
            MyBase.Controls.Add(Me.updateDesc_TB)
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "FirmwareUpdateForm"
            Me.Text = "Firmware Update"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.FirmwareUpdateForm_Load)
            Me.ftp_GB.ResumeLayout(False)
            MyBase.ResumeLayout(False)
        End Sub

        Friend Sub Reset()
            Me.update_CB.Items.Clear()
            Me.location_TB.Enabled = True
            If (Me.m_AppForm.m_ReaderType = READER_TYPE.MC) Then
                Me.update_CB.Items.Add("Radio Firmware Update")
                Me.update_CB.Items.Add("Radio Config Update")
                Me.update_CB.SelectedIndex = 0
                Me.ftp_GB.Enabled = False
                Me.username_TB.Enabled = False
                Me.password_TB.Enabled = False
                Me.location_TB.Text = ""
                Me.updateDesc_TB.Text = ""
            Else
                Me.update_CB.Visible = False
                Me.ftp_GB.Enabled = True
                Me.username_TB.Enabled = True
                Me.password_TB.Enabled = True
            End If
        End Sub

        Private Sub update_CB_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.update_CB.SelectedIndex <> -1) Then
                Me.location_TB.Text = ""
            End If
        End Sub
        Private Sub updateWorker()
            Me.firmwareApplyButton.Enabled = False
            Me.location_TB.Enabled = False
            Me.username_TB.Enabled = False
            Me.password_TB.Enabled = False
            Try 
                If (Me.m_AppForm.m_ReaderType = READER_TYPE.MC) Then
                    If (Me.update_CB.SelectedIndex = 0) Then
                        Me.m_AppForm.m_ReaderMgmt.RadioFirmwareUpdate.Update(Me.location_TB.Text)
                    Else
                        Me.m_AppForm.m_ReaderMgmt.RadioConfigUpdate.Update(Me.location_TB.Text)
                    End If
                Else
                    Dim info As New SoftwareUpdateInfo(Me.location_TB.Text, Me.username_TB.Text, Me.password_TB.Text)
                    Me.m_AppForm.m_ReaderMgmt.SoftwareUpdate.Update(info)
                End If
                Dim updatePercent As UInt32 = 0
                Do While (updatePercent < 100)
                    Dim updateStatus As UpdateStatus
                    If (Me.m_AppForm.m_ReaderType = READER_TYPE.MC) Then
                        If (Me.update_CB.SelectedIndex = 1) Then
                            updateStatus = Me.m_AppForm.m_ReaderMgmt.RadioFirmwareUpdate.UpdateStatus
                        Else
                            updateStatus = Me.m_AppForm.m_ReaderMgmt.RadioConfigUpdate.UpdateStatus
                        End If
                    Else
                        updateStatus = Me.m_AppForm.m_ReaderMgmt.SoftwareUpdate.UpdateStatus
                    End If
                    updatePercent = updateStatus.Percentage
                    Me.update_PB.Value = CInt(updatePercent)
                    Me.updateDesc_TB.Text = updateStatus.UpdateInfo
                    Thread.Sleep(&H3E8)
                Loop
                Me.m_AppForm.m_ReaderMgmt.Logout
                MyBase.Close
            Catch ioe As InvalidOperationException
                Me.m_AppForm.notifyUser(ioe.ToString, Me.update_CB.Text)
            Catch iue As InvalidUsageException
                Me.m_AppForm.notifyUser(iue.Info, Me.update_CB.Text)
            Catch ofe As OperationFailureException
                Me.m_AppForm.notifyUser((ofe.StatusDescription & ofe.VendorMessage), Me.update_CB.Text)
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message.ToString, Me.update_CB.Text)
            End Try
            Me.firmwareApplyButton.Enabled = True
            Me.location_TB.Enabled = True
            Me.username_TB.Enabled = True
            Me.password_TB.Enabled = True
        End Sub


        ' Fields
        Private browseFileButton As Button
        Private components As IContainer = Nothing
        Private firmwareApplyButton As Button
        Private ftp_GB As Panel
        Private ftp_label As Label
        Private IPLabel As Label
        Friend location_TB As TextBox
        Private m_AppForm As AppForm
        Private m_IsLoaded As Boolean
        Private mainMenu1 As MainMenu
        Friend password_TB As TextBox
        Private passwordLabel As Label
        Private update_CB As ComboBox
        Private update_PB As ProgressBar
        Private updateDesc_TB As TextBox
        Friend username_TB As TextBox
        Private userNameLabel As Label
    End Class
End Namespace

