Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3_Host_Sample1
    Public Class CapabilitiesForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
        End Sub

        Private Sub Capabilities_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Not (Not Me.m_AppForm.m_IsConnected OrElse Me.m_IsLoaded) Then
                    Dim cap As ReaderCapabilities = Me.m_AppForm.m_ReaderAPI.ReaderCapabilities
                    Dim capView As ListView = Me.capabilitiesView
                    capView.Items.Item(0).SubItems.Add(cap.ReaderID.ID)
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
                Else
                    Me.m_AppForm.functionCallStatusLabel.Text = "Please connect to a reader"
                End If
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
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
            Dim listViewItem1 As New ListViewItem("Reader ID")
            Dim listViewItem2 As New ListViewItem("Firmware Version")
            Dim listViewItem3 As New ListViewItem("Model Name")
            Dim listViewItem4 As New ListViewItem("No. of Antennas")
            Dim listViewItem5 As New ListViewItem("No. of GPI")
            Dim listViewItem6 As New ListViewItem("No. of GPO")
            Dim listViewItem7 As New ListViewItem("Max Ops in Access Sequence")
            Dim listViewItem8 As New ListViewItem("Max No. Of Pre-Filters")
            Dim listViewItem9 As New ListViewItem("Country Code")
            Dim listViewItem10 As New ListViewItem("Communication Standard")
            Dim listViewItem11 As New ListViewItem("UTC Clock")
            Dim listViewItem12 As New ListViewItem("Block Erase")
            Dim listViewItem13 As New ListViewItem("Block Write")
            Dim listViewItem14 As New ListViewItem("Block Permalock")
            Dim listViewItem15 As New ListViewItem("Recommission")
            Dim listViewItem16 As New ListViewItem("Write UMI")
            Dim listViewItem17 As New ListViewItem("State-aware Singulation")
            Dim listViewItem18 As New ListViewItem("Tag Event Reporting")
            Dim listViewItem19 As New ListViewItem("RSSI Filtering")
            Me.capabilitiesView = New ListView
            Me.CapabilityCol = New ColumnHeader
            Me.ValueCol = New ColumnHeader
            MyBase.SuspendLayout()
            Me.capabilitiesView.Columns.AddRange(New ColumnHeader() {Me.CapabilityCol, Me.ValueCol})
            Me.capabilitiesView.FullRowSelect = True
            Me.capabilitiesView.GridLines = True
            Me.capabilitiesView.Items.AddRange(New ListViewItem() {listViewItem1, listViewItem2, listViewItem3, listViewItem4, listViewItem5, listViewItem6, listViewItem7, listViewItem8, listViewItem9, listViewItem10, listViewItem11, listViewItem12, listViewItem13, listViewItem14, listViewItem15, listViewItem16, listViewItem17, listViewItem18, listViewItem19})
            Me.capabilitiesView.Location = New Point(12, 12)
            Me.capabilitiesView.Name = "capabilitiesView"
            Me.capabilitiesView.Size = New Size(&H139, 290)
            Me.capabilitiesView.TabIndex = 1
            Me.capabilitiesView.UseCompatibleStateImageBehavior = False
            Me.capabilitiesView.View = View.Details
            Me.CapabilityCol.Text = "Capability"
            Me.CapabilityCol.Width = &H98
            Me.ValueCol.Text = "Value"
            Me.ValueCol.Width = &H9D
            MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Font
            MyBase.ClientSize = New Size(&H151, &H13E)
            MyBase.Controls.Add(Me.capabilitiesView)
            MyBase.FormBorderStyle = FormBorderStyle.FixedDialog
            MyBase.MaximizeBox = False
            MyBase.MinimizeBox = False
            MyBase.Name = "CapabilitiesForm"
            MyBase.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Capabilities"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.Capabilities_Load)
            MyBase.ResumeLayout(False)
        End Sub


        ' Fields
        Friend capabilitiesView As ListView
        Private CapabilityCol As ColumnHeader
        Private components As IContainer = Nothing
        Private m_AppForm As AppForm
        Friend m_IsLoaded As Boolean
        Private ValueCol As ColumnHeader
    End Class
End Namespace

