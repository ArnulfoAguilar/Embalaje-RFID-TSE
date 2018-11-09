Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class CapabilitiesForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Private Sub CapabilitiesForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Me.m_AppForm.m_IsConnected Then
                    If Not Me.m_IsLoaded Then
                        Dim cap As ReaderCapabilities = Me.m_AppForm.m_ReaderAPI.ReaderCapabilities
                        Dim capView As ListView = Me.capabilitiesView
                        capView.Items.Item(0).SubItems.Add(cap.ReaderID.ID.ToString)
                        capView.Items.Item(1).SubItems.Add(cap.FirwareVersion)
                        capView.Items.Item(2).SubItems.Add(cap.ModelName)
                        capView.Items.Item(3).SubItems.Add(cap.NumAntennaSupported.ToString)
                        capView.Items.Item(4).SubItems.Add(cap.NumGPIPorts.ToString)
                        capView.Items.Item(5).SubItems.Add(cap.NumGPOPorts.ToString)
                        capView.Items.Item(6).SubItems.Add(cap.MaxNumOperationsInAccessSequence.ToString)
                        capView.Items.Item(7).SubItems.Add(cap.MaxNumPreFilters.ToString)
                        capView.Items.Item(8).SubItems.Add(cap.CountryCode.ToString)
                        capView.Items.Item(9).SubItems.Add(cap.CommunicationStandard.ToString)
                        capView.Items.Item(10).SubItems.Add(cap.IsUTCClockSupported.ToString)
                        capView.Items.Item(11).SubItems.Add(cap.IsBlockEraseSupported.ToString)
                        capView.Items.Item(12).SubItems.Add(cap.IsBlockWriteSupported.ToString)
                        capView.Items.Item(13).SubItems.Add(cap.IsBlockPermalockSupported.ToString)
                        capView.Items.Item(14).SubItems.Add(cap.IsRecommissionSupported.ToString)
                        capView.Items.Item(15).SubItems.Add(cap.IsWriteUMISupported.ToString)
                        capView.Items.Item(&H10).SubItems.Add(cap.IsTagInventoryStateAwareSingulationSupported.ToString)
                        capView.Items.Item(&H11).SubItems.Add(cap.IsTagEventReportingSupported.ToString)
                        capView.Items.Item(&H12).SubItems.Add(cap.IsRssiFilterSupported.ToString)
                        Me.m_IsLoaded = True
                    End If
                Else
                    Me.m_AppForm.notifyUser("Please connect to a reader", "Capabilities")
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Capabilities")
            End Try
        End Sub

        Private Function convertCountryCodeToName(ByVal countryCode As Integer) As String
            Return countryCode.ToString
        End Function

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
            Dim listViewItem12 As New ListViewItem
            Dim listViewItem13 As New ListViewItem
            Dim listViewItem14 As New ListViewItem
            Dim listViewItem15 As New ListViewItem
            Dim listViewItem16 As New ListViewItem
            Dim listViewItem17 As New ListViewItem
            Dim listViewItem18 As New ListViewItem
            Dim listViewItem19 As New ListViewItem
            Me.mainMenu1 = New MainMenu
            Me.capabilitiesView = New ListView
            Me.CapabilityCol = New ColumnHeader
            Me.ValueCol = New ColumnHeader
            MyBase.SuspendLayout()
            Me.capabilitiesView.Anchor = (AnchorStyles.Left Or (AnchorStyles.Bottom Or AnchorStyles.Top))
            Me.capabilitiesView.Columns.Add(Me.CapabilityCol)
            Me.capabilitiesView.Columns.Add(Me.ValueCol)
            Me.capabilitiesView.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.capabilitiesView.FullRowSelect = True
            listViewItem1.Text = "Reader ID"
            listViewItem2.Text = "Firmware Version"
            listViewItem3.Text = "Model Name"
            listViewItem4.Text = "No. of Antennas"
            listViewItem5.Text = "No. of GPI"
            listViewItem6.Text = "No. of GPIO"
            listViewItem7.Text = "Max Ops in Access Sequence"
            listViewItem8.Text = "Max No. Of Pre-Filters"
            listViewItem9.Text = "Country Code"
            listViewItem10.Text = "Communication Standard"
            listViewItem11.Text = "UTC Clock"
            listViewItem12.Text = "Block Erase"
            listViewItem13.Text = "Block Write"
            listViewItem14.Text = "Block Permalock"
            listViewItem15.Text = "Recommission"
            listViewItem16.Text = "Write UMI"
            listViewItem17.Text = "State-aware Singulation"
            listViewItem18.Text = "Tag Event Reporting"
            listViewItem19.Text = "RSSI Filtering"
            Me.capabilitiesView.Items.Add(listViewItem1)
            Me.capabilitiesView.Items.Add(listViewItem2)
            Me.capabilitiesView.Items.Add(listViewItem3)
            Me.capabilitiesView.Items.Add(listViewItem4)
            Me.capabilitiesView.Items.Add(listViewItem5)
            Me.capabilitiesView.Items.Add(listViewItem6)
            Me.capabilitiesView.Items.Add(listViewItem7)
            Me.capabilitiesView.Items.Add(listViewItem8)
            Me.capabilitiesView.Items.Add(listViewItem9)
            Me.capabilitiesView.Items.Add(listViewItem10)
            Me.capabilitiesView.Items.Add(listViewItem11)
            Me.capabilitiesView.Items.Add(listViewItem12)
            Me.capabilitiesView.Items.Add(listViewItem13)
            Me.capabilitiesView.Items.Add(listViewItem14)
            Me.capabilitiesView.Items.Add(listViewItem15)
            Me.capabilitiesView.Items.Add(listViewItem16)
            Me.capabilitiesView.Items.Add(listViewItem17)
            Me.capabilitiesView.Items.Add(listViewItem18)
            Me.capabilitiesView.Items.Add(listViewItem19)
            Me.capabilitiesView.Location = New Point(3, 3)
            Me.capabilitiesView.Name = "capabilitiesView"
            Me.capabilitiesView.Size = New Size(&HEB, &HD0)
            Me.capabilitiesView.TabIndex = 2
            Me.capabilitiesView.View = View.Details
            Me.CapabilityCol.Text = "Capability"
            Me.CapabilityCol.Width = &H77
            Me.ValueCol.Text = "Value"
            Me.ValueCol.Width = 100
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HD6)
            MyBase.Controls.Add(Me.capabilitiesView)
            MyBase.Name = "CapabilitiesForm"
            Me.Text = "Capabilities"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.CapabilitiesForm_Load)
            MyBase.ResumeLayout(False)
        End Sub


        ' Fields
        Friend capabilitiesView As ListView
        Private CapabilityCol As ColumnHeader
        Private components As IContainer = Nothing
        Private m_AppForm As AppForm
        Private m_IsLoaded As Boolean
        Private mainMenu1 As MainMenu
        Private ValueCol As ColumnHeader
    End Class
End Namespace

