Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class RFModeForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Private Sub antenna_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim index As Integer = Integer.Parse(Me.antenna_CB.SelectedItem.ToString)
            Dim antRFMode As Antennas.RFMode = Nothing
            Try
                antRFMode = Me.m_AppForm.m_ReaderAPI.Config.Antennas.Item(index).GetRFMode
                If (Me.rfModeTable_CB.Items.Count > antRFMode.TableIndex) Then
                    Me.rfModeTable_CB.SelectedIndex = CInt(antRFMode.TableIndex)
                End If
            Catch ofe As OperationFailureException
                Me.m_AppForm.notifyUser(ofe.VendorMessage, "RF-Mode")
            End Try
            If (Not antRFMode Is Nothing) Then
                Me.tari_TB.Text = antRFMode.Tari.ToString
            End If
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Dim listViewItem1 As New ListViewItem
            Dim listViewItem2 As New ListViewItem
            Dim listViewItem3 As New ListViewItem
            Dim listViewItem4 As New ListViewItem
            Dim listViewItem5 As New ListViewItem
            Dim listViewItem6 As New ListViewItem
            Dim listViewItem7 As New ListViewItem
            Dim listViewItem8 As New ListViewItem
            Dim listViewItem9 As New ListViewItem
            Dim listViewItem10 As New ListViewItem
            Dim listViewItem11 As New ListViewItem
            Me.mainMenu = New MainMenu
            Me.rfModelistView = New ListView
            Me.parameterHeader = New ColumnHeader
            Me.valueHeader1 = New ColumnHeader
            Me.rfModeTable_CB = New ComboBox
            Me.rfModeTablelabel = New Label
            Me.tari_TB = New TextBox
            Me.tariValueLabel = New Label
            Me.antenna_CB = New ComboBox
            Me.antennaIDLlabel = New Label
            Me.rfModeButton = New Button
            MyBase.SuspendLayout()
            Me.rfModelistView.Anchor = (AnchorStyles.Left Or (AnchorStyles.Bottom Or AnchorStyles.Top))
            Me.rfModelistView.Columns.Add(Me.parameterHeader)
            Me.rfModelistView.Columns.Add(Me.valueHeader1)
            Me.rfModelistView.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.rfModelistView.FullRowSelect = True
            listViewItem1.Text = "Mode Identifier"
            listViewItem2.Text = "DR"
            listViewItem3.Text = "Bdr"
            listViewItem4.Text = "M"
            listViewItem5.Text = "Forward Link Modulation"
            listViewItem6.Text = "PIE"
            listViewItem7.Text = "Min Tari"
            listViewItem8.Text = "Max Tari"
            listViewItem9.Text = "Step tari"
            listViewItem10.Text = "Spectral Mask Indicator"
            listViewItem11.Text = "EPC HAG TCConformance"
            Me.rfModelistView.Items.Add(listViewItem1)
            Me.rfModelistView.Items.Add(listViewItem2)
            Me.rfModelistView.Items.Add(listViewItem3)
            Me.rfModelistView.Items.Add(listViewItem4)
            Me.rfModelistView.Items.Add(listViewItem5)
            Me.rfModelistView.Items.Add(listViewItem6)
            Me.rfModelistView.Items.Add(listViewItem7)
            Me.rfModelistView.Items.Add(listViewItem8)
            Me.rfModelistView.Items.Add(listViewItem9)
            Me.rfModelistView.Items.Add(listViewItem10)
            Me.rfModelistView.Items.Add(listViewItem11)
            Me.rfModelistView.Location = New Point(3, &H54)
            Me.rfModelistView.Name = "rfModelistView"
            Me.rfModelistView.Size = New Size(&HE8, &H47)
            Me.rfModelistView.TabIndex = 4
            Me.rfModelistView.View = View.Details
            Me.parameterHeader.Text = "Parameter"
            Me.parameterHeader.Width = 120
            Me.valueHeader1.Text = "Value"
            Me.valueHeader1.Width = &H61
            Me.rfModeTable_CB.Location = New Point(&H7F, &H38)
            Me.rfModeTable_CB.Name = "rfModeTable_CB"
            Me.rfModeTable_CB.Size = New Size(&H6C, &H16)
            Me.rfModeTable_CB.TabIndex = 2
            AddHandler Me.rfModeTable_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.rfModeTable_CB_SelectedIndexChanged)
            Me.rfModeTablelabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.rfModeTablelabel.Location = New Point(3, &H38)
            Me.rfModeTablelabel.Name = "rfModeTablelabel"
            Me.rfModeTablelabel.Size = New Size(&H5B, &H16)
            Me.rfModeTablelabel.Text = "RF Mode Table"
            Me.tari_TB.Location = New Point(&H7F, &H1D)
            Me.tari_TB.Name = "tari_TB"
            Me.tari_TB.Size = New Size(&H6C, &H15)
            Me.tari_TB.TabIndex = 3
            Me.tariValueLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tariValueLabel.Location = New Point(3, &H1D)
            Me.tariValueLabel.Name = "tariValueLabel"
            Me.tariValueLabel.Size = New Size(&H5B, &H16)
            Me.tariValueLabel.Text = "Tari Value"
            Me.antenna_CB.Location = New Point(&H7F, 3)
            Me.antenna_CB.Name = "antenna_CB"
            Me.antenna_CB.Size = New Size(&H6C, &H16)
            Me.antenna_CB.TabIndex = 1
            AddHandler Me.antenna_CB.SelectedIndexChanged, New EventHandler(AddressOf Me.antenna_CB_SelectedIndexChanged)
            Me.antennaIDLlabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.antennaIDLlabel.Location = New Point(3, 6)
            Me.antennaIDLlabel.Name = "antennaIDLlabel"
            Me.antennaIDLlabel.Size = New Size(&H5B, &H16)
            Me.antennaIDLlabel.Text = "Antenna ID"
            Me.rfModeButton.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.rfModeButton.Font = New Font("Tahoma", 9!, FontStyle.Regular)
            Me.rfModeButton.Location = New Point(&HB8, &HA1)
            Me.rfModeButton.Name = "rfModeButton"
            Me.rfModeButton.Size = New Size(&H33, 20)
            Me.rfModeButton.TabIndex = &H12
            Me.rfModeButton.Text = "Apply"
            AddHandler Me.rfModeButton.Click, New EventHandler(AddressOf Me.rfModeButton_Click)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.rfModeButton)
            MyBase.Controls.Add(Me.rfModelistView)
            MyBase.Controls.Add(Me.rfModeTable_CB)
            MyBase.Controls.Add(Me.rfModeTablelabel)
            MyBase.Controls.Add(Me.tari_TB)
            MyBase.Controls.Add(Me.tariValueLabel)
            MyBase.Controls.Add(Me.antenna_CB)
            MyBase.Controls.Add(Me.antennaIDLlabel)
            MyBase.Menu = Me.mainMenu
            MyBase.MinimizeBox = False
            MyBase.Name = "RFModeForm"
            Me.Text = "RF Mode"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.RFModeForm_Load)
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.RFModeForm_Closing)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub rfModeButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If m_AppForm.m_ReaderAPI.IsConnected Then
                    Dim index As Integer = Integer.Parse(antenna_CB.SelectedItem.ToString())
                    Dim antRFMode As Antennas.RFMode = m_AppForm.m_ReaderAPI.Config.Antennas(index).GetRFMode()
                    antRFMode.Tari = UInteger.Parse(tari_TB.Text)
                    antRFMode.TableIndex = CUInt(rfModeTable_CB.SelectedIndex)
                    m_AppForm.m_ReaderAPI.Config.Antennas(index).SetRFMode(antRFMode)
                End If
                Me.Close()
            Catch ex As InvalidUsageException
                Me.m_AppForm.notifyUser(ex.Info, "RF-Mode")
            Catch ofe As OperationFailureException
                Me.m_AppForm.notifyUser(ofe.VendorMessage, "RF-Mode")
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "RF-Mode")
            End Try
        End Sub

        Private Sub RFModeForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
        End Sub

        Private Sub RFModeForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If m_AppForm.m_ReaderAPI.IsConnected AndAlso Not m_IsLoaded Then
                    Dim antID As UShort() = m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas
                    If antID.Length > 0 Then
                        For Each id As UShort In antID
                            antenna_CB.Items.Add(id)
                        Next
                        antenna_CB.SelectedIndex = 0
                        Dim antRFMode As Antennas.RFMode = Nothing
                        Try
                            antRFMode = m_AppForm.m_ReaderAPI.Config.Antennas(antID(antenna_CB.SelectedIndex)).GetRFMode()
                        Catch ex As OperationFailureException
                            Me.m_AppForm.notifyUser(ex.VendorMessage, "RF-Mode")
                        End Try

                        Dim numberRFModes As Integer = m_AppForm.m_ReaderAPI.ReaderCapabilities.RFModes(0).Length

                        For j As Integer = 0 To numberRFModes - 1

                            rfModeTable_CB.Items.Add(j)
                        Next
                        If antRFMode IsNot Nothing Then
                            rfModeTable_CB.SelectedIndex = CInt(antRFMode.TableIndex)

                        End If
                    End If
                    m_IsLoaded = True
                End If
            Catch ofe As OperationFailureException
                Me.m_AppForm.notifyUser(ofe.VendorMessage, "RF-Mode")
            End Try
        End Sub

        Private Sub rfModeTable_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            If rfModeTable_CB.SelectedItem IsNot Nothing Then
                Dim index As Integer = rfModeTable_CB.SelectedIndex
                If index >= 0 Then
                    Dim rfTableEntry As RFModeTableEntry = m_AppForm.m_ReaderAPI.ReaderCapabilities.RFModes(0)(index)
                    For k As Integer = 0 To rfModelistView.Items.Count - 1
                        If rfModelistView.Items(k).SubItems.Count > 1 Then
                            rfModelistView.Items(k).SubItems.RemoveAt(1)
                                End If
                    Next
                    rfModelistView.Items(0).SubItems.Add(rfTableEntry.ModeIdentifier.ToString())
                    rfModelistView.Items(1).SubItems.Add(rfTableEntry.DivideRatio.ToString())
                    rfModelistView.Items(2).SubItems.Add(rfTableEntry.BdrValue.ToString())
                    rfModelistView.Items(3).SubItems.Add(rfTableEntry.Modulation.ToString())
                    rfModelistView.Items(4).SubItems.Add(rfTableEntry.ForwardLinkModulationType.ToString())
                    rfModelistView.Items(5).SubItems.Add(rfTableEntry.PieValue.ToString())
                    rfModelistView.Items(6).SubItems.Add(rfTableEntry.MinTariValue.ToString())
                    rfModelistView.Items(7).SubItems.Add(rfTableEntry.MaxTariValue.ToString())
                    rfModelistView.Items(8).SubItems.Add(rfTableEntry.StepTariValue.ToString())
                    rfModelistView.Items(9).SubItems.Add(rfTableEntry.SpectralMaskIndicator.ToString())
                    rfModelistView.Items(10).SubItems.Add(rfTableEntry.EPCHAGTCConformance.ToString())
                        End If
            End If
        End Sub


        ' Fields
        Friend antenna_CB As ComboBox
        Private antennaIDLlabel As Label
        Private components As IContainer = Nothing
        Private m_AppForm As AppForm
        Friend m_IsLoaded As Boolean
        Private mainMenu As MainMenu
        Private parameterHeader As ColumnHeader
        Private rfModeButton As Button
        Friend rfModelistView As ListView
        Friend rfModeTable_CB As ComboBox
        Private rfModeTablelabel As Label
        Friend tari_TB As TextBox
        Private tariValueLabel As Label
        Private valueHeader1 As ColumnHeader
    End Class
End Namespace

