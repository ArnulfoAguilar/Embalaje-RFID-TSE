Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class RadioPowerForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.radioState_CB = New ComboBox
            Me.label1 = New Label
            MyBase.SuspendLayout()
            Me.radioState_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.radioState_CB.Items.Add("Off")
            Me.radioState_CB.Items.Add("On")
            Me.radioState_CB.Location = New Point(&H87, &H12)
            Me.radioState_CB.Name = "radioState_CB"
            Me.radioState_CB.Size = New Size(&H59, 20)
            Me.radioState_CB.TabIndex = 3
            Me.label1.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.label1.Location = New Point(&H11, 20)
            Me.label1.Name = "label1"
            Me.label1.Size = New Size(&H4E, 15)
            Me.label1.Text = "Current State"
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.radioState_CB)
            MyBase.Controls.Add(Me.label1)
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "RadioPowerForm"
            Me.Text = "Radio Power"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.RadioPowerForm_Load)
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.RadioPowerForm_Closing)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub RadioPowerForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
            Try
                If Me.m_AppForm.m_IsConnected Then
                    If Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported Then
                        Me.m_AppForm.m_ReaderAPI.Config.RadioPowerState = DirectCast(Me.radioState_CB.SelectedIndex, RADIO_POWER_STATE)
                        Me.m_AppForm.functionCallStatusLabel.Text = "Set Radio Power Successfully"
                    Else
                        Me.m_AppForm.functionCallStatusLabel.Text = "Please connect to a reader"
                    End If
                Else
                    Me.m_AppForm.functionCallStatusLabel.Text = "Please connect to a reader"
                    MyBase.Close()
                End If
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub

        Private Sub RadioPowerForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not Me.m_IsLoaded Then
                Try
                    If Me.m_AppForm.m_IsConnected Then
                        If Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported Then
                            Me.radioState_CB.SelectedIndex = CInt(Me.m_AppForm.m_ReaderAPI.Config.RadioPowerState)
                        Else
                            Me.m_AppForm.functionCallStatusLabel.Text = "Radio Power Control Not Supported"
                            MyBase.Close()
                        End If
                    Else
                        Me.m_AppForm.functionCallStatusLabel.Text = "Please connect to a reader"
                        MyBase.Close()
                    End If
                Catch ex As OperationFailureException
                    Me.m_AppForm.functionCallStatusLabel.Text = ex.Result.ToString
                    MyBase.Close()
                End Try
                Me.m_IsLoaded = True
            End If
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private label1 As Label
        Private m_AppForm As AppForm
        Friend m_IsLoaded As Boolean
        Private mainMenu1 As MainMenu
        Private radioState_CB As ComboBox
    End Class
End Namespace

