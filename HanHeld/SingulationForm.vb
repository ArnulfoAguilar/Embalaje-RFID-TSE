Imports Symbol.RFID3
Imports Symbol.RFID3.Antennas
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class SingulationForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Private Sub antennaID_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                    Dim antID As UInt16() = Me.m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas
                    Dim singularControl As SingulationControl = Me.m_AppForm.m_ReaderAPI.Config.Antennas.Item(antID(Me.antennaID_CB.SelectedIndex)).GetSingulationControl
                    Me.session_CB.SelectedIndex = CInt(singularControl.Session)
                    Me.tagPopulation_TB.Text = singularControl.TagPopulation.ToString
                    Me.tagTransit_TB.Text = singularControl.TagTransitTime.ToString
                    Me.stateAware_CB.Checked = singularControl.Action.PerformStateAwareSingulationAction
                    If singularControl.Action.PerformStateAwareSingulationAction Then
                        Dim action As Antennas.SingulationControl.SingulationAction = singularControl.Action
                        Me.inventoryState_CB.SelectedIndex = CInt(action.InventoryState)
                        Me.SLFlag_CB.SelectedIndex = CInt(action.SLFlag)
                    Else
                        Me.inventoryState_CB.Enabled = False
                        Me.SLFlag_CB.Enabled = False
                    End If
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Singulation Control")
            End Try
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.tagTransit_TB = New TextBox
            Me.tagtransmitLabel = New Label
            Me.tagPopulation_TB = New TextBox
            Me.tagPopulationLabel = New Label
            Me.session_CB = New ComboBox
            Me.sessionLabel = New Label
            Me.antennaID_CB = New ComboBox
            Me.antennaIDLlabel = New Label
            Me.SLFlag_CB = New ComboBox
            Me.inventoryStateLabel = New Label
            Me.SLFlagLabel = New Label
            Me.inventoryState_CB = New ComboBox
            Me.singulationButton = New Button
            Me.stateAware_CB = New CheckBox
            Me.stateAware_PB = New Panel
            Me.stateAware_PB.SuspendLayout()
            MyBase.SuspendLayout()
            Me.tagTransit_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagTransit_TB.Location = New Point(&HAE, &H47)
            Me.tagTransit_TB.Name = "tagTransit_TB"
            Me.tagTransit_TB.Size = New Size(&H3D, &H13)
            Me.tagTransit_TB.TabIndex = 4
            Me.tagtransmitLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagtransmitLabel.Location = New Point(13, &H47)
            Me.tagtransmitLabel.Name = "tagtransmitLabel"
            Me.tagtransmitLabel.Size = New Size(&H57, 13)
            Me.tagtransmitLabel.Text = "Tag Transit Time"
            Me.tagPopulation_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagPopulation_TB.Location = New Point(&HAE, &H31)
            Me.tagPopulation_TB.Name = "tagPopulation_TB"
            Me.tagPopulation_TB.Size = New Size(&H3D, &H13)
            Me.tagPopulation_TB.TabIndex = 3
            Me.tagPopulationLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagPopulationLabel.Location = New Point(14, &H31)
            Me.tagPopulationLabel.Name = "tagPopulationLabel"
            Me.tagPopulationLabel.Size = New Size(&H4F, 13)
            Me.tagPopulationLabel.Text = "Tag Population"
            Me.session_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.session_CB.Items.Add("S0")
            Me.session_CB.Items.Add("S1")
            Me.session_CB.Items.Add("S2")
            Me.session_CB.Items.Add("S3")
            Me.session_CB.Location = New Point(&HAE, &H1A)
            Me.session_CB.Name = "session_CB"
            Me.session_CB.Size = New Size(&H3D, 20)
            Me.session_CB.TabIndex = 2
            Me.sessionLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.sessionLabel.Location = New Point(14, &H1A)
            Me.sessionLabel.Name = "sessionLabel"
            Me.sessionLabel.Size = New Size(&H2C, 13)
            Me.sessionLabel.Text = "Session"
            Me.antennaID_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antennaID_CB.Location = New Point(&HAE, 3)
            Me.antennaID_CB.Name = "antennaID_CB"
            Me.antennaID_CB.Size = New Size(&H3D, 20)
            Me.antennaID_CB.TabIndex = 1
            AddHandler Me.antennaID_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.antennaID_CB_SelectedIndexChanged)
            Me.antennaIDLlabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antennaIDLlabel.Location = New Point(14, 3)
            Me.antennaIDLlabel.Name = "antennaIDLlabel"
            Me.antennaIDLlabel.Size = New Size(&H3D, 13)
            Me.antennaIDLlabel.Text = "Antenna ID"
            Me.SLFlag_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.SLFlag_CB.Items.Add("ASSERTED")
            Me.SLFlag_CB.Items.Add("DEASSERTED")
            Me.SLFlag_CB.Location = New Point(&H79, 30)
            Me.SLFlag_CB.Name = "SLFlag_CB"
            Me.SLFlag_CB.Size = New Size(&H68, 20)
            Me.SLFlag_CB.TabIndex = 6
            Me.inventoryStateLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.inventoryStateLabel.Location = New Point(5, 11)
            Me.inventoryStateLabel.Name = "inventoryStateLabel"
            Me.inventoryStateLabel.Size = New Size(&H4F, 13)
            Me.inventoryStateLabel.Text = "Inventory State"
            Me.SLFlagLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.SLFlagLabel.Location = New Point(5, &H20)
            Me.SLFlagLabel.Name = "SLFlagLabel"
            Me.SLFlagLabel.Size = New Size(&H2B, 13)
            Me.SLFlagLabel.Text = "SL Flag"
            Me.inventoryState_CB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.inventoryState_CB.Items.Add("STATE A")
            Me.inventoryState_CB.Items.Add("STATE B")
            Me.inventoryState_CB.Location = New Point(&H7A, 4)
            Me.inventoryState_CB.Name = "inventoryState_CB"
            Me.inventoryState_CB.Size = New Size(&H67, 20)
            Me.inventoryState_CB.TabIndex = 5
            Me.singulationButton.Location = New Point(&HB8, &HA1)
            Me.singulationButton.Name = "singulationButton"
            Me.singulationButton.Size = New Size(&H33, 20)
            Me.singulationButton.TabIndex = &H16
            Me.singulationButton.Text = "Apply"
            AddHandler Me.singulationButton.Click, New EventHandler(AddressOf Me.singulationButton_Click)
            Me.stateAware_CB.Font = New Font("Tahoma", 8.0!, FontStyle.Regular)
            Me.stateAware_CB.Location = New Point(10, &H57)
            Me.stateAware_CB.Name = "stateAware_CB"
            Me.stateAware_CB.Size = New Size(90, &H18)
            Me.stateAware_CB.TabIndex = 30
            Me.stateAware_CB.Text = "State Aware"
            AddHandler Me.stateAware_CB.CheckStateChanged, New EventHandler(AddressOf Me.stateAware_CB_CheckStateChanged)
            Me.stateAware_PB.Controls.Add(Me.SLFlag_CB)
            Me.stateAware_PB.Controls.Add(Me.inventoryStateLabel)
            Me.stateAware_PB.Controls.Add(Me.SLFlagLabel)
            Me.stateAware_PB.Controls.Add(Me.inventoryState_CB)
            Me.stateAware_PB.Location = New Point(10, 100)
            Me.stateAware_PB.Name = "stateAware_PB"
            Me.stateAware_PB.Size = New Size(&HE3, &H37)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.stateAware_CB)
            MyBase.Controls.Add(Me.singulationButton)
            MyBase.Controls.Add(Me.tagTransit_TB)
            MyBase.Controls.Add(Me.tagtransmitLabel)
            MyBase.Controls.Add(Me.tagPopulation_TB)
            MyBase.Controls.Add(Me.tagPopulationLabel)
            MyBase.Controls.Add(Me.session_CB)
            MyBase.Controls.Add(Me.sessionLabel)
            MyBase.Controls.Add(Me.antennaID_CB)
            MyBase.Controls.Add(Me.antennaIDLlabel)
            MyBase.Controls.Add(Me.stateAware_PB)
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "SingulationForm"
            Me.Text = "Singulation"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.SingulationForm_Load)
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.SingulationForm_Closing)
            Me.stateAware_PB.ResumeLayout(False)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub singulationButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                    Dim antID As UInt16() = Me.m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas
                    Dim singularControl As SingulationControl = Me.m_AppForm.m_ReaderAPI.Config.Antennas.Item(antID(Me.antennaID_CB.SelectedIndex)).GetSingulationControl
                    singularControl.Session = DirectCast(Me.session_CB.SelectedIndex, SESSION)
                    singularControl.TagPopulation = UInt16.Parse(Me.tagPopulation_TB.Text)
                    singularControl.TagTransitTime = UInt16.Parse(Me.tagTransit_TB.Text)
                    singularControl.Action.PerformStateAwareSingulationAction = Me.stateAware_CB.Checked
                    If singularControl.Action.PerformStateAwareSingulationAction Then
                        singularControl.Action.InventoryState = DirectCast(Me.inventoryState_CB.SelectedIndex, INVENTORY_STATE)
                        singularControl.Action.SLFlag = DirectCast(Me.SLFlag_CB.SelectedIndex, SL_FLAG)
                    End If
                    Me.m_AppForm.m_ReaderAPI.Config.Antennas.Item(antID(Me.antennaID_CB.SelectedIndex)).SetSingulationControl(singularControl)
                End If
                MyBase.Close()
            Catch ofe As OperationFailureException
                Me.m_AppForm.notifyUser(ofe.VendorMessage, "Singulation Control")
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Singulation Control")
            End Try
        End Sub

        Private Sub SingulationForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
        End Sub

        Private Sub SingulationForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If (Me.m_AppForm.m_ReaderAPI.IsConnected AndAlso Not Me.m_IsLoaded) Then
                    Dim antID As UInt16() = Me.m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas
                    If (antID.Length > 0) Then
                        Dim id As UInt16
                        For Each id In antID
                            Me.antennaID_CB.Items.Add(id)
                        Next
                        Me.antennaID_CB.SelectedIndex = 0
                    End If
                    Me.m_IsLoaded = True
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Singulation Control")
            End Try
        End Sub

        Private Sub stateAware_CB_CheckStateChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.inventoryState_CB.Enabled = Me.stateAware_CB.Checked
            Me.SLFlag_CB.Enabled = Me.stateAware_CB.Checked
            If Me.stateAware_CB.Checked Then
                Me.stateAware_PB.BackColor = Color.White
            Else
                Me.stateAware_PB.BackColor = Color.Gray
            End If
        End Sub


        ' Fields
        Private antennaID_CB As ComboBox
        Private antennaIDLlabel As Label
        Private components As IContainer = Nothing
        Private inventoryState_CB As ComboBox
        Private inventoryStateLabel As Label
        Private m_AppForm As AppForm
        Friend m_IsLoaded As Boolean
        Private mainMenu1 As MainMenu
        Private session_CB As ComboBox
        Private sessionLabel As Label
        Private singulationButton As Button
        Private SLFlag_CB As ComboBox
        Private SLFlagLabel As Label
        Private stateAware_CB As CheckBox
        Private stateAware_PB As Panel
        Private tagPopulation_TB As TextBox
        Private tagPopulationLabel As Label
        Private tagTransit_TB As TextBox
        Private tagtransmitLabel As Label
    End Class
End Namespace

