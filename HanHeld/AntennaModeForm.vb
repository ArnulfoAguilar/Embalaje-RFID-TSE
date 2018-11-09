Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class AntennaModeForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Private Sub antennaModeButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If Me.m_IsLoaded Then
                Try
                    Me.m_AppForm.m_ReaderMgmt.AntennaMode = DirectCast(Me.antennaMode_CB.SelectedIndex, ANTENNA_MODE)
                    MyBase.Close()
                Catch ex As OperationFailureException
                    Me.m_AppForm.notifyUser(ex.VendorMessage, "Antenna Mode")
                End Try
            End If
        End Sub

        Private Sub AntennaModeForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
        End Sub

        Private Sub AntennaModeForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not Me.m_IsLoaded Then
                Try
                    Me.antennaMode_CB.SelectedIndex = CInt(Me.m_AppForm.m_ReaderMgmt.AntennaMode)
                Catch ex As OperationFailureException
                    Me.m_AppForm.notifyUser(ex.VendorMessage, "Antenna Mode")
                    MyBase.Close()
                End Try
                Me.m_IsLoaded = True
            End If
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.antennaMode_CB = New ComboBox
            Me.antennaModeLabel = New Label
            Me.rfModeButton = New Button
            MyBase.SuspendLayout()
            Me.antennaMode_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antennaMode_CB.Items.Add("BiStatic")
            Me.antennaMode_CB.Items.Add("MonoStatic")
            Me.antennaMode_CB.Location = New Point(&H89, &H20)
            Me.antennaMode_CB.Name = "antennaMode_CB"
            Me.antennaMode_CB.Size = New Size(&H59, 20)
            Me.antennaMode_CB.TabIndex = 5
            Me.antennaModeLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antennaModeLabel.Location = New Point(&H13, &H22)
            Me.antennaModeLabel.Name = "antennaModeLabel"
            Me.antennaModeLabel.Size = New Size(&H4E, 15)
            Me.antennaModeLabel.Text = "Mode"
            Me.rfModeButton.Location = New Point(&HAF, &HA5)
            Me.rfModeButton.Name = "rfModeButton"
            Me.rfModeButton.Size = New Size(&H33, 20)
            Me.rfModeButton.TabIndex = &H15
            Me.rfModeButton.Text = "Apply"
            AddHandler Me.rfModeButton.Click, New EventHandler(AddressOf Me.antennaModeButton_Click)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.rfModeButton)
            MyBase.Controls.Add(Me.antennaMode_CB)
            MyBase.Controls.Add(Me.antennaModeLabel)
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "AntennaModeForm"
            Me.Text = "Antenna Mode"
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.AntennaModeForm_Closing)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.AntennaModeForm_Load)
            MyBase.ResumeLayout(False)
        End Sub


        ' Fields
        Private antennaMode_CB As ComboBox
        Private antennaModeLabel As Label
        Private components As IContainer = Nothing
        Private m_AppForm As AppForm
        Private m_IsLoaded As Boolean
        Private mainMenu1 As MainMenu
        Private rfModeButton As Button
    End Class
End Namespace

