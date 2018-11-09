Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class SystemInfoForm
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
            Dim listViewItem1 As New ListViewItem
            Dim listViewItem2 As New ListViewItem
            Dim listViewItem3 As New ListViewItem
            Dim listViewItem4 As New ListViewItem
            Dim listViewItem5 As New ListViewItem
            Dim listViewItem6 As New ListViewItem
            Dim listViewItem7 As New ListViewItem
            Me.mainMenu1 = New MainMenu
            Me.systemInfoView = New ListView
            Me.SystemCol = New ColumnHeader
            Me.ValueCol = New ColumnHeader
            MyBase.SuspendLayout()
            Me.systemInfoView.Anchor = (AnchorStyles.Left Or (AnchorStyles.Bottom Or AnchorStyles.Top))
            Me.systemInfoView.Columns.Add(Me.SystemCol)
            Me.systemInfoView.Columns.Add(Me.ValueCol)
            Me.systemInfoView.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.systemInfoView.FullRowSelect = True
            listViewItem1.Text = "Reader Name"
            listViewItem2.Text = "Firmware Version"
            listViewItem3.Text = "FPGA Version"
            listViewItem4.Text = "RAM Available"
            listViewItem5.Text = "Flash Available"
            listViewItem6.Text = "Up Time"
            listViewItem7.Text = "Reader Location"
            Me.systemInfoView.Items.Add(listViewItem1)
            Me.systemInfoView.Items.Add(listViewItem2)
            Me.systemInfoView.Items.Add(listViewItem3)
            Me.systemInfoView.Items.Add(listViewItem4)
            Me.systemInfoView.Items.Add(listViewItem5)
            Me.systemInfoView.Items.Add(listViewItem6)
            Me.systemInfoView.Items.Add(listViewItem7)
            Me.systemInfoView.Location = New Point(4, 4)
            Me.systemInfoView.Name = "systemInfoView"
            Me.systemInfoView.Size = New Size(&HE9, &HCF)
            Me.systemInfoView.TabIndex = 3
            Me.systemInfoView.View = View.Details
            Me.SystemCol.Text = "System"
            Me.SystemCol.Width = &H66
            Me.ValueCol.Text = "Value"
            Me.ValueCol.Width = &H74
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HD6)
            MyBase.Controls.Add(Me.systemInfoView)
            Me.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            MyBase.Name = "SystemInfoForm"
            Me.Text = "System Information"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.SystemInfoForm_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub SystemInfoForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Me.m_AppForm.m_ReaderMgmt.IsLoggedIn Then
                    Dim info As SystemInfo = Me.m_AppForm.m_ReaderMgmt.GetSystemInfo
                    Dim infoView As ListView = Me.systemInfoView
                    infoView.Items.Item(0).SubItems.Add(info.ReaderName)
                    infoView.Items.Item(1).SubItems.Add(info.RadioFirmwareVersion)
                    infoView.Items.Item(2).SubItems.Add(info.FPGAVersion)
                    infoView.Items.Item(3).SubItems.Add((info.RAMAvailable & " bytes"))
                    infoView.Items.Item(4).SubItems.Add((info.FlashAvailable & " bytes"))
                    infoView.Items.Item(5).SubItems.Add(info.UpTime)
                    infoView.Items.Item(6).SubItems.Add(info.ReaderLocation)
                Else
                    Me.m_AppForm.notifyUser("Please login to a reader via ReaderMgmt", "System Info")
                End If
            Catch ex As OperationFailureException
                Me.m_AppForm.notifyUser(ex.VendorMessage, "System Info")
                MyBase.Close()
            End Try
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private m_AppForm As AppForm
        Private mainMenu1 As MainMenu
        Private SystemCol As ColumnHeader
        Friend systemInfoView As ListView
        Private ValueCol As ColumnHeader
    End Class
End Namespace

