Imports Symbol.RFID3
Imports Symbol.RFID3.Config
Imports Symbol.RFID3.Events
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class AntennaConfigForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Private Sub antennaConfigButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If (Me.m_AppForm.m_IsConnected AndAlso Me.m_IsChanged) Then
                    Dim antID As UInt16() = Me.m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas
                    Dim antConfig As Antennas.Config = Me.m_AppForm.m_ReaderAPI.Config.Antennas.Item(antID(Me.antennaID_CB.SelectedIndex)).GetConfig
                    antConfig.ReceiveSensitivityIndex = Me.receiveSensitivity_CB.SelectedIndex
                    antConfig.TransmitPowerIndex = Me.transmitPower_CB.SelectedIndex
                    If Not Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsHoppingEnabled Then
                        antConfig.TransmitFrequencyIndex = (Me.txFreq_CB.SelectedIndex + 1)
                    Else
                        antConfig.TransmitFrequencyIndex = (Me.hopTableIndex_CB.SelectedIndex + 1)
                    End If
                    Me.m_AppForm.m_ReaderAPI.Config.Antennas.Item(antID(Me.antennaID_CB.SelectedIndex)).SetConfig(antConfig)
                Else
                    Me.m_AppForm.notifyUser("Please connect to a reader", "Antenna Config")
                End If
                MyBase.Close()
            Catch iue As InvalidUsageException
                Me.m_AppForm.notifyUser(iue.Info, "Antenna Config")
            Catch ofe As OperationFailureException
                Me.m_AppForm.notifyUser(ofe.VendorMessage, "Antenna Config")
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Antenna Config")
            End Try
        End Sub

        Private Sub AntennaConfigForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
        End Sub

        Private Sub AntennaConfigForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If (Me.m_AppForm.m_ReaderAPI.IsConnected AndAlso Not Me.m_IsLoaded) Then
                    Dim antID As UInt16() = Me.m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas
                    Dim rxValues As Integer() = Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.ReceiveSensitivityValues
                    Dim txValues As Integer() = Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.TransmitPowerLevelValues
                    If (antID.Length > 0) Then
                        Dim i As Integer
                        Dim rx As Integer
                        For Each rx In rxValues
                            Me.receiveSensitivity_CB.Items.Add(rx)
                        Next
                        Dim tx As Integer
                        For Each tx In txValues
                            Me.transmitPower_CB.Items.Add(tx)
                        Next
                        If Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsHoppingEnabled Then
                            Me.hopTableIndexLabel.Visible = True
                            Me.hopTableIndex_CB.Visible = True
                            Me.hopFrequencies_TB.Visible = True
                            Me.txFreqLabel.Visible = False
                            Me.txFreq_CB.Visible = False
                            Dim hopInfo As FrequencyHopInfo = Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.FrequencyHopInfo

                            For i = 0 To hopInfo.Length - 1
                                Me.hopTableIndex_CB.Items.Add(hopInfo.Item(i).HopTableID)
                            Next i
                        Else
                            Me.hopTableIndexLabel.Visible = False
                            Me.hopTableIndex_CB.Visible = False
                            Me.hopFrequencies_TB.Visible = False
                            Me.txFreqLabel.Visible = True
                            Me.txFreq_CB.Visible = True
                            MyBase.Controls.Add(Me.txFreq_CB)
                            Dim freq As Integer() = Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.FixedFreqValues

                            For i = 0 To freq.Length - 1
                                Me.txFreq_CB.Items.Add(freq(i).ToString)
                            Next i
                        End If
                        Dim id As UInt16
                        For Each id In antID
                            Me.antennaID_CB.Items.Add(id)
                        Next
                        Me.m_IsLoaded = True
                        MyBase.ResumeLayout(False)
                    End If
                End If
                Me.antennaID_CB.SelectedIndex = 0
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Antenna Configuration")
            End Try
            Me.m_IsChanged = False
        End Sub

        Private Sub antennaID_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.antennaID_CB.SelectedIndex <> -1) Then
                Dim antID As UInt16() = Me.m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas
                If (antID.Length > 0) Then
                    Dim antConfig As Antennas.Config = Me.m_AppForm.m_ReaderAPI.Config.Antennas.Item(antID(Me.antennaID_CB.SelectedIndex)).GetConfig
                    Me.receiveSensitivity_CB.SelectedIndex = antConfig.ReceiveSensitivityIndex
                    Me.transmitPower_CB.SelectedIndex = antConfig.TransmitPowerIndex
                    If Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsHoppingEnabled Then
                        Me.hopTableIndex_CB.SelectedIndex = (antConfig.TransmitFrequencyIndex - 1)
                    Else
                        Me.txFreq_CB.SelectedIndex = 0
                    End If
                    Me.m_IsChanged = True
                End If
            End If
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub hopTableIndex_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If (Me.m_AppForm.m_ReaderAPI.IsConnected AndAlso Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.IsHoppingEnabled) Then
                    Dim hopInfo As FrequencyHopInfo = Me.m_AppForm.m_ReaderAPI.ReaderCapabilities.FrequencyHopInfo
                    Dim index As Integer = Integer.Parse(Me.hopTableIndex_CB.SelectedItem.ToString)
                    Dim freqs As Integer() = hopInfo.Item((index - 1)).FrequencyHopValues
                    Dim hopTableFreqListMultiline As String = ""
                    Me.hopFrequencies_TB.Text = ""
                    Dim freq As Integer
                    For Each freq In freqs
                        If (hopTableFreqListMultiline <> "") Then
                            hopTableFreqListMultiline = (hopTableFreqListMultiline & ", ")
                        End If
                        hopTableFreqListMultiline = (hopTableFreqListMultiline & freq.ToString)
                    Next
                    Me.hopFrequencies_TB.Text = hopTableFreqListMultiline
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Hop Table")
            End Try
            Me.m_IsChanged = True
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu = New MainMenu
            Me.transmitPower_CB = New ComboBox
            Me.transmitPowerLabel = New Label
            Me.receiveSensitivity_CB = New ComboBox
            Me.receiveSenLabel = New Label
            Me.antennaID_CB = New ComboBox
            Me.antennaIDLlabel = New Label
            Me.txFreq_CB = New ComboBox
            Me.txFreqLabel = New Label
            Me.columnHeader1 = New ColumnHeader
            Me.hopTableIndex_CB = New ComboBox
            Me.hopTableIndexLabel = New Label
            Me.antennaConfigButton = New Button
            Me.hopFrequencies_TB = New TextBox
            MyBase.SuspendLayout()
            Me.transmitPower_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.transmitPower_CB.Location = New Point(&H87, &H39)
            Me.transmitPower_CB.Name = "transmitPower_CB"
            Me.transmitPower_CB.Size = New Size(100, 20)
            Me.transmitPower_CB.TabIndex = 3
            AddHandler Me.transmitPower_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.transmitPower_CB_SelectedIndexChanged)
            Me.transmitPowerLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.transmitPowerLabel.Location = New Point(5, &H3A)
            Me.transmitPowerLabel.Name = "transmitPowerLabel"
            Me.transmitPowerLabel.Size = New Size(&H77, &H1A)
            Me.transmitPowerLabel.Text = "Transmit Power (dBm)"
            Me.receiveSensitivity_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.receiveSensitivity_CB.Location = New Point(&H87, &H1D)
            Me.receiveSensitivity_CB.Name = "receiveSensitivity_CB"
            Me.receiveSensitivity_CB.Size = New Size(100, 20)
            Me.receiveSensitivity_CB.TabIndex = 2
            AddHandler Me.receiveSensitivity_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.receiveSensitivity_CB_SelectedIndexChanged)
            Me.receiveSenLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.receiveSenLabel.Location = New Point(5, &H20)
            Me.receiveSenLabel.Name = "receiveSenLabel"
            Me.receiveSenLabel.Size = New Size(&H7E, &H1D)
            Me.receiveSenLabel.Text = "Receive Sensitivity (dB)"
            Me.antennaID_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antennaID_CB.Location = New Point(&H87, 3)
            Me.antennaID_CB.Name = "antennaID_CB"
            Me.antennaID_CB.Size = New Size(100, 20)
            Me.antennaID_CB.TabIndex = 1
            AddHandler Me.antennaID_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.antennaID_CB_SelectedIndexChanged)
            Me.antennaIDLlabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antennaIDLlabel.Location = New Point(5, 7)
            Me.antennaIDLlabel.Name = "antennaIDLlabel"
            Me.antennaIDLlabel.Size = New Size(&H52, 13)
            Me.antennaIDLlabel.Text = "Antenna ID"
            Me.txFreq_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.txFreq_CB.Location = New Point(&H87, &H51)
            Me.txFreq_CB.Name = "txFreq_CB"
            Me.txFreq_CB.Size = New Size(&H5F, 20)
            Me.txFreq_CB.TabIndex = &H10
            Me.txFreqLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.txFreqLabel.Location = New Point(5, &H53)
            Me.txFreqLabel.Name = "txFreqLabel"
            Me.txFreqLabel.Size = New Size(&H77, 30)
            Me.txFreqLabel.Text = "Transmit Frequencies"
            Me.columnHeader1.Text = "Frequencies"
            Me.columnHeader1.Width = &H61
            Me.hopTableIndex_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.hopTableIndex_CB.Location = New Point(&H87, &H51)
            Me.hopTableIndex_CB.Name = "hopTableIndex_CB"
            Me.hopTableIndex_CB.Size = New Size(100, 20)
            Me.hopTableIndex_CB.TabIndex = 4
            AddHandler Me.hopTableIndex_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.hopTableIndex_CB_SelectedIndexChanged)
            Me.hopTableIndexLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.hopTableIndexLabel.Location = New Point(5, &H54)
            Me.hopTableIndexLabel.Name = "hopTableIndexLabel"
            Me.hopTableIndexLabel.Size = New Size(&H5B, 13)
            Me.hopTableIndexLabel.Text = "Hop Table Index"
            Me.antennaConfigButton.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.antennaConfigButton.Location = New Point(&HB8, &HA1)
            Me.antennaConfigButton.Name = "antennaConfigButton"
            Me.antennaConfigButton.Size = New Size(&H33, 20)
            Me.antennaConfigButton.TabIndex = 20
            Me.antennaConfigButton.Text = "Apply"
            AddHandler Me.antennaConfigButton.Click, New EventHandler(AddressOf Me.antennaConfigButton_Click)
            Me.hopFrequencies_TB.Anchor = (AnchorStyles.Left Or (AnchorStyles.Bottom Or AnchorStyles.Top))
            Me.hopFrequencies_TB.Location = New Point(5, &H6C)
            Me.hopFrequencies_TB.Multiline = True
            Me.hopFrequencies_TB.Name = "hopFrequencies_TB"
            Me.hopFrequencies_TB.ReadOnly = True
            Me.hopFrequencies_TB.ScrollBars = ScrollBars.Vertical
            Me.hopFrequencies_TB.Size = New Size(230, &H2F)
            Me.hopFrequencies_TB.TabIndex = &H1A
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.hopFrequencies_TB)
            MyBase.Controls.Add(Me.antennaConfigButton)
            MyBase.Controls.Add(Me.hopTableIndexLabel)
            MyBase.Controls.Add(Me.hopTableIndex_CB)
            MyBase.Controls.Add(Me.txFreqLabel)
            MyBase.Controls.Add(Me.txFreq_CB)
            MyBase.Controls.Add(Me.transmitPower_CB)
            MyBase.Controls.Add(Me.transmitPowerLabel)
            MyBase.Controls.Add(Me.receiveSensitivity_CB)
            MyBase.Controls.Add(Me.receiveSenLabel)
            MyBase.Controls.Add(Me.antennaID_CB)
            MyBase.Controls.Add(Me.antennaIDLlabel)
            MyBase.Menu = Me.mainMenu
            MyBase.MinimizeBox = False
            MyBase.Name = "AntennaConfigForm"
            Me.Text = "Antenna Config"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.AntennaConfigForm_Load)
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.AntennaConfigForm_Closing)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub receiveSensitivity_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.m_IsChanged = True
        End Sub

        Private Sub transmitPower_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.m_IsChanged = True
        End Sub

        Friend Sub updateConfig(ByVal antennaID As Integer)
            If Me.m_IsLoaded Then
            End If
        End Sub


        ' Fields
        Private antennaConfigButton As Button
        Friend antennaID_CB As ComboBox
        Private antennaIDLlabel As Label
        Private columnHeader1 As ColumnHeader
        Private components As IContainer = Nothing
        Private hopFrequencies_TB As TextBox
        Friend hopTableIndex_CB As ComboBox
        Private hopTableIndexLabel As Label
        Private m_AppForm As AppForm
        Private m_IsChanged As Boolean
        Friend m_IsLoaded As Boolean
        Private mainMenu As MainMenu
        Private receiveSenLabel As Label
        Friend receiveSensitivity_CB As ComboBox
        Friend transmitPower_CB As ComboBox
        Private transmitPowerLabel As Label
        Friend txFreq_CB As ComboBox
        Private txFreqLabel As Label
    End Class
End Namespace

