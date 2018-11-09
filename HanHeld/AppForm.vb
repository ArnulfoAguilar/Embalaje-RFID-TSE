Imports Microsoft.WindowsCE.Forms
Imports Symbol.RFID3
Imports Symbol.RFID3.Events
Imports Symbol.RFID3.TagAccess.Sequence
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Windows.Forms
Imports System.Windows.Forms.ListViewItem
Imports System.Data.SqlServerCe
Imports System.Data

Namespace VB_RFID3Sample6
    Public Class AppForm
       
        Inherits Form
        ' Methods
        Public Sub New()
            Me.InitializeComponent()
            Me.m_UpdateReadHandler = New UpdateRead(AddressOf Me.myUpdateRead)
            Me.m_UpdateStatusHandler = New updateStatusResult(AddressOf Me.myUpdateStatus)
            Me.m_ReadTag = New TagData
            Me.m_ConnectionForm = New ConnectionForm(Me)
            Me.m_ReadForm = New ReadForm(Me)
            Me.m_AntennaInfoForm = New AntennaInfoForm(Me)
            Me.m_AntennaConfigForm = New AntennaConfigForm(Me)
            Me.m_PostFilterForm = New PostFilterForm(Me)
            Me.m_AccessFilterForm = New AccessFilterForm(Me)
            Me.m_TriggerForm = New TriggerForm(Me)
            Me.m_ReaderMgmt = New ReaderManagement
            Me.m_TagTable = New Hashtable(&H3FF)
            Me.m_AccessOpResult = New AccessOperationResult
            Me.m_IsConnected = False
            Me.m_TagTotalCount = 0
        End Sub

        Public Shared conn As New SqlCeConnection("Data Source=.\BASE_HANDHELD_ARNULFO.sdf;Password=Ar13004;Persist Security Info=True")

        Private Sub accessFiltermenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles accessFiltermenuItem.Click
            Me.m_AccessFilterForm.ShowDialog()
        End Sub

        Private Sub antennaMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles antennaMenuItem.Click
            Me.m_AntennaConfigForm.ShowDialog()
        End Sub

        Private Sub antInfoMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles antInfoMenuItem.Click
            Me.m_AntennaInfoForm.ShowDialog()
        End Sub

        Private Sub antModeMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles antModeMenuItem.Click
            If (Nothing Is Me.m_AntennaModeForm) Then
                Me.m_AntennaModeForm = New AntennaModeForm(Me)
            End If
            Me.m_AntennaModeForm.ShowDialog()
        End Sub

        Private Sub AppForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs) Handles MyBase.Closing
            Me.CloseForm()
        End Sub

        Private Sub AppForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Me.memBank_CB.SelectedIndex = 0
            Me.Connect("Connect")
        End Sub

        Private Sub autonomousMode_CB_CheckStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles autonomousMode_CB.CheckStateChanged
            If Me.m_IsConnected Then
                Me.autonomousMode_CB.Checked = (Me.autonomousMode_CB.Checked AndAlso (Me.m_IsConnected AndAlso Me.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported))
            End If
        End Sub

        Private Sub blockEraseContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles blockWriteContextMenuItem.Click
            If (Nothing Is Me.m_BlockEraseForm) Then
                Me.m_BlockEraseForm = New BlockEraseForm(Me)
            End If
            Me.m_BlockEraseForm.ShowDialog()
        End Sub

        Private Sub blockEraseMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles blockEraseMenuItem.Click
            If (Nothing Is Me.m_BlockEraseForm) Then
                Me.m_BlockEraseForm = New BlockEraseForm(Me)
            End If
            Me.m_BlockEraseForm.ShowDialog()
        End Sub

        Private Sub blockWriteContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles blockEraseContextMenuItem.Click
            If (Nothing Is Me.m_BlockWriteForm) Then
                Me.m_BlockWriteForm = New WriteForm(Me, True)
            End If
            Me.m_BlockWriteForm.ShowDialog()
        End Sub

        Private Sub blockWriteMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles blockWriteMenuItem.Click
            If (Nothing Is Me.m_BlockWriteForm) Then
                Me.m_BlockWriteForm = New WriteForm(Me, True)
            End If
            Me.m_BlockWriteForm.ShowDialog()
        End Sub

        Private Sub capMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles capMenuItem.Click
            If (Nothing Is Me.m_CapabilitiesForm) Then
                Me.m_CapabilitiesForm = New CapabilitiesForm(Me)
            End If
            Me.m_CapabilitiesForm.ShowDialog()
        End Sub

        Private Sub clearReportMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles clearReportContextMenuItem.Click
            Me.totalTagValueLabel.Text = ""
            Me.readTimeValueLabel.Text = ""
            Me.inventoryList.Items.Clear()
            Me.m_TagTable.Clear()
        End Sub

        Private Sub CloseForm()
            Try
                If Me.m_IsConnected Then
                    Me.m_ReaderAPI.Disconnect()
                End If
                Me.m_ReaderMgmt.Dispose()
                MyBase.Dispose()
            Catch ex As Exception
                Me.notifyUser(ex.Message, "Close")
            End Try
        End Sub

        Friend Sub configureMenuItemsBasedOnCapabilities()
            Me.autonomousMode_CB.Visible = Me.autonomousMode_CB.Enabled = IIf(Me.m_ReaderAPI.IsConnected, Me.m_ReaderAPI.ReaderCapabilities.IsTagEventReportingSupported, False)
            Me.radioPowerMenuItem.Enabled = IIf(Me.m_ReaderAPI.IsConnected, Me.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported, False)
            Me.gpioMenuItem.Enabled = False
            Me.m_TriggerForm.Reset()
            Me.blockEraseContextMenuItem.Enabled = IIf(Me.m_ReaderAPI.IsConnected, Me.m_ReaderAPI.ReaderCapabilities.IsBlockEraseSupported, False)
            Me.blockWriteContextMenuItem.Enabled = IIf(Me.m_ReaderAPI.IsConnected, Me.m_ReaderAPI.ReaderCapabilities.IsBlockWriteSupported, False)
        End Sub

        Friend Sub configureMenuItemsUponConnectDisconnect()
            Me.capMenuItem.Enabled = Me.m_IsConnected
            Me.configMenuItem.Enabled = Me.m_IsConnected
            Me.antennaMenuItem.Enabled = Me.m_IsConnected
            Me.rFModeMenuItem.Enabled = Me.m_IsConnected
            Me.singulationMenuItem.Enabled = Me.m_IsConnected
            Me.ResetMenuItem.Enabled = Me.m_IsConnected
            Me.tagStorageMenuItem.Enabled = Me.m_IsConnected
            Me.filterMenuItem.Enabled = Me.m_IsConnected
            Me.accessMenuItem.Enabled = Me.m_IsConnected
            Me.triggerMenuItem.Enabled = Me.m_IsConnected
            If (((Not Me.m_ReaderAPI Is Nothing) AndAlso Me.m_IsConnected) AndAlso Me.m_ReaderAPI.ReaderCapabilities.IsRadioPowerControlSupported) Then
                Me.radioPowerMenuItem.Text = IIf((Me.m_ReaderAPI.Config.RadioPowerState = RADIO_POWER_STATE.OFF), "Power On Radio", "Power Off Radio")
            Else
                Me.radioPowerMenuItem.Enabled = False
            End If
            Me.readButton.Enabled = Me.m_IsConnected
            Me.memBank_CB.Enabled = Me.m_IsConnected
        End Sub

        Friend Sub configureMenuItemsUponLoginLogout()
            Me.softwareUpdateMenuItem.Enabled = Me.m_ReaderMgmt.IsLoggedIn
            Me.readPointMenuItem.Enabled = (Me.m_ReaderMgmt.IsLoggedIn AndAlso (Me.m_ReaderType <> READER_TYPE.MC))
            Me.rebootMenuItem.Enabled = (Me.m_ReaderMgmt.IsLoggedIn AndAlso (Me.m_ReaderType <> READER_TYPE.MC))
            Me.antModeMenuItem.Enabled = (Me.m_ReaderMgmt.IsLoggedIn AndAlso (Me.m_ReaderType <> READER_TYPE.MC))
            Me.systemInfoMenuItem.Enabled = (Me.m_ReaderMgmt.IsLoggedIn AndAlso (Me.m_ReaderType <> READER_TYPE.MC))
        End Sub

        Friend Sub Connect(ByVal status As String)
            If status = "Connect" Then
                Dim port As UInteger = UInteger.Parse(m_ConnectionForm.PortText)
                m_ReaderAPI = New RFIDReader(m_ConnectionForm.IpText, port, 0)

                Try
                    m_ReaderAPI.Connect()

                    ' 
                    ' * Events Registration 
                    ' 

                    m_ReaderAPI.Events.AttachTagDataWithReadEvent = False
                    AddHandler m_ReaderAPI.Events.ReadNotify, AddressOf Events_ReadNotify
                    AddHandler m_ReaderAPI.Events.StatusNotify, AddressOf Events_StatusNotify
                    m_ReaderAPI.Events.NotifyBufferFullWarningEvent = True
                    m_ReaderAPI.Events.NotifyBufferFullEvent = True
                    m_ReaderAPI.Events.NotifyReaderDisconnectEvent = True
                    m_ReaderAPI.Events.NotifyAccessStartEvent = True
                    m_ReaderAPI.Events.NotifyAccessStopEvent = True
                    m_ReaderAPI.Events.NotifyInventoryStartEvent = True
                    m_ReaderAPI.Events.NotifyInventoryStopEvent = True
                    m_ReaderAPI.Events.NotifyReaderExceptionEvent = True

                    ' 
                    ' * Label setup 
                    ' 

                    Me.Text = m_ConnectionForm.IpText
                    Me.m_ConnectionForm.connectionButton.Text = "Disconnect"
                    If m_ConnectionForm.Visible Then
                        Me.m_ConnectionForm.Close()
                    End If
                Catch ofe As OperationFailureException
                    notifyUser(ofe.StatusDescription, "Connect")
                End Try
            ElseIf status = "Disconnect" Then
                If Not m_ReaderInitiatedDisconnectionReceived Then
                    Try
                        m_ReaderAPI.Disconnect()
                    Catch ofe As OperationFailureException
                        notifyUser(ofe.VendorMessage, "Connect")
                    End Try
                End If

                Me.Text = "VB_RFID3Sample6"
                Me.m_ConnectionForm.connectionButton.Text = "Connect"
                If m_ConnectionForm.Visible Then
                    Me.m_ConnectionForm.Close()
                End If
                Me.readButton.Enabled = False
                Me.memBank_CB.Enabled = False
            End If
            Try
                m_IsConnected = m_ReaderAPI.IsConnected
                configureMenuItemsUponConnectDisconnect()
                configureMenuItemsBasedOnCapabilities()
            Catch ex As Exception
                Me.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub

        Private Sub connectionMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles connectionMenuItem.Click
            If (Nothing Is Me.m_ConnectionForm) Then
                Me.m_ConnectionForm = New ConnectionForm(Me)
            End If
            Me.m_ConnectionForm.ShowDialog()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub Events_ReadNotify(ByVal sender As Object, ByVal args As ReadEventArgs)
            MyBase.Invoke(Me.m_UpdateReadHandler, New Object() {Nothing})
        End Sub

        Public Sub Events_StatusNotify(ByVal sender As Object, ByVal args As StatusEventArgs)
            MyBase.Invoke(Me.m_UpdateStatusHandler, New Object() {args.StatusEventData})
        End Sub

        Private Sub exitMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exitMenuItem.Click
            Me.CloseForm()
        End Sub

        Private Sub gpioMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles gpioMenuItem.Click
        End Sub

        Private Sub helpMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles helpMenuItem.Click
            Dim helpForm As HelpForm
            helpForm = New HelpForm(Me)
            helpForm.ShowDialog()
        End Sub

        Private Sub InitializeComponent()
            Me.appFormMenu = New System.Windows.Forms.MainMenu
            Me.connectionMenuItem = New System.Windows.Forms.MenuItem
            Me.sampleAppMenuItem = New System.Windows.Forms.MenuItem
            Me.capMenuItem = New System.Windows.Forms.MenuItem
            Me.configMenuItem = New System.Windows.Forms.MenuItem
            Me.tagStorageMenuItem = New System.Windows.Forms.MenuItem
            Me.menuItem4 = New System.Windows.Forms.MenuItem
            Me.antennaMenuItem = New System.Windows.Forms.MenuItem
            Me.rFModeMenuItem = New System.Windows.Forms.MenuItem
            Me.gpioMenuItem = New System.Windows.Forms.MenuItem
            Me.singulationMenuItem = New System.Windows.Forms.MenuItem
            Me.radioPowerMenuItem = New System.Windows.Forms.MenuItem
            Me.menuItem2 = New System.Windows.Forms.MenuItem
            Me.ResetMenuItem = New System.Windows.Forms.MenuItem
            Me.operationMenuItem = New System.Windows.Forms.MenuItem
            Me.antInfoMenuItem = New System.Windows.Forms.MenuItem
            Me.filterMenuItem = New System.Windows.Forms.MenuItem
            Me.preFilterMenuItem = New System.Windows.Forms.MenuItem
            Me.postFilterMenuItem = New System.Windows.Forms.MenuItem
            Me.accessFiltermenuItem = New System.Windows.Forms.MenuItem
            Me.accessMenuItem = New System.Windows.Forms.MenuItem
            Me.readMenuItem = New System.Windows.Forms.MenuItem
            Me.writeMenuItem = New System.Windows.Forms.MenuItem
            Me.lockMenuItem = New System.Windows.Forms.MenuItem
            Me.killMenuItem = New System.Windows.Forms.MenuItem
            Me.blockWriteMenuItem = New System.Windows.Forms.MenuItem
            Me.blockEraseMenuItem = New System.Windows.Forms.MenuItem
            Me.triggerMenuItem = New System.Windows.Forms.MenuItem
            Me.mgmtMenuItem = New System.Windows.Forms.MenuItem
            Me.loginMenuItem = New System.Windows.Forms.MenuItem
            Me.systemInfoMenuItem = New System.Windows.Forms.MenuItem
            Me.antModeMenuItem = New System.Windows.Forms.MenuItem
            Me.readPointMenuItem = New System.Windows.Forms.MenuItem
            Me.softwareUpdateMenuItem = New System.Windows.Forms.MenuItem
            Me.rebootMenuItem = New System.Windows.Forms.MenuItem
            Me.menuItem1 = New System.Windows.Forms.MenuItem
            Me.helpMenuItem = New System.Windows.Forms.MenuItem
            Me.menuItem31 = New System.Windows.Forms.MenuItem
            Me.exitMenuItem = New System.Windows.Forms.MenuItem
            Me.inventoryList = New System.Windows.Forms.ListView
            Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
            Me.columnHeader8 = New System.Windows.Forms.ColumnHeader
            Me.dataContextMenu = New System.Windows.Forms.ContextMenu
            Me.tagDataMenuItem = New System.Windows.Forms.MenuItem
            Me.readContextMenuItem = New System.Windows.Forms.MenuItem
            Me.writeContextMenuItem = New System.Windows.Forms.MenuItem
            Me.lockContextMenuItem = New System.Windows.Forms.MenuItem
            Me.killContextMenuItem = New System.Windows.Forms.MenuItem
            Me.blockEraseContextMenuItem = New System.Windows.Forms.MenuItem
            Me.blockWriteContextMenuItem = New System.Windows.Forms.MenuItem
            Me.menuItem5 = New System.Windows.Forms.MenuItem
            Me.locateTagContextMenuItem = New System.Windows.Forms.MenuItem
            Me.menuItem3 = New System.Windows.Forms.MenuItem
            Me.clearReportContextMenuItem = New System.Windows.Forms.MenuItem
            Me.memBank_CB = New System.Windows.Forms.ComboBox
            Me.readButton = New System.Windows.Forms.Button
            Me.readTimeLabel = New System.Windows.Forms.Label
            Me.totalTagLabel = New System.Windows.Forms.Label
            Me.totalTagValueLabel = New System.Windows.Forms.Label
            Me.readTimeValueLabel = New System.Windows.Forms.Label
            Me.functionCallStatusLabel = New Microsoft.WindowsCE.Forms.Notification
            Me.autonomousMode_CB = New System.Windows.Forms.CheckBox
            Me.SuspendLayout()
            '
            'appFormMenu
            '
            Me.appFormMenu.MenuItems.Add(Me.connectionMenuItem)
            Me.appFormMenu.MenuItems.Add(Me.sampleAppMenuItem)
            '
            'connectionMenuItem
            '
            Me.connectionMenuItem.Text = "Connection"
            '
            'sampleAppMenuItem
            '
            Me.sampleAppMenuItem.MenuItems.Add(Me.capMenuItem)
            Me.sampleAppMenuItem.MenuItems.Add(Me.configMenuItem)
            Me.sampleAppMenuItem.MenuItems.Add(Me.operationMenuItem)
            Me.sampleAppMenuItem.MenuItems.Add(Me.mgmtMenuItem)
            Me.sampleAppMenuItem.MenuItems.Add(Me.menuItem1)
            Me.sampleAppMenuItem.MenuItems.Add(Me.menuItem31)
            Me.sampleAppMenuItem.MenuItems.Add(Me.exitMenuItem)
            Me.sampleAppMenuItem.Text = "Menu"
            '
            'capMenuItem
            '
            Me.capMenuItem.Text = "Capabilities..."
            '
            'configMenuItem
            '
            Me.configMenuItem.MenuItems.Add(Me.tagStorageMenuItem)
            Me.configMenuItem.MenuItems.Add(Me.menuItem4)
            Me.configMenuItem.MenuItems.Add(Me.antennaMenuItem)
            Me.configMenuItem.MenuItems.Add(Me.rFModeMenuItem)
            Me.configMenuItem.MenuItems.Add(Me.gpioMenuItem)
            Me.configMenuItem.MenuItems.Add(Me.singulationMenuItem)
            Me.configMenuItem.MenuItems.Add(Me.radioPowerMenuItem)
            Me.configMenuItem.MenuItems.Add(Me.menuItem2)
            Me.configMenuItem.MenuItems.Add(Me.ResetMenuItem)
            Me.configMenuItem.Text = "Config"
            '
            'tagStorageMenuItem
            '
            Me.tagStorageMenuItem.Text = "&Tag Storage Settings"
            '
            'menuItem4
            '
            Me.menuItem4.Text = "-"
            '
            'antennaMenuItem
            '
            Me.antennaMenuItem.Text = "Antenna..."
            '
            'rFModeMenuItem
            '
            Me.rFModeMenuItem.Text = "RF Mode..."
            '
            'gpioMenuItem
            '
            Me.gpioMenuItem.Text = "GPIO..."
            '
            'singulationMenuItem
            '
            Me.singulationMenuItem.Text = "Singulation..."
            '
            'radioPowerMenuItem
            '
            Me.radioPowerMenuItem.Text = "Power Off Radio"
            '
            'menuItem2
            '
            Me.menuItem2.Text = "-"
            '
            'ResetMenuItem
            '
            Me.ResetMenuItem.Text = "Reset To Factory Default"
            '
            'operationMenuItem
            '
            Me.operationMenuItem.MenuItems.Add(Me.antInfoMenuItem)
            Me.operationMenuItem.MenuItems.Add(Me.filterMenuItem)
            Me.operationMenuItem.MenuItems.Add(Me.accessMenuItem)
            Me.operationMenuItem.MenuItems.Add(Me.triggerMenuItem)
            Me.operationMenuItem.Text = "Operations"
            '
            'antInfoMenuItem
            '
            Me.antInfoMenuItem.Text = "Antenna Info"
            '
            'filterMenuItem
            '
            Me.filterMenuItem.MenuItems.Add(Me.preFilterMenuItem)
            Me.filterMenuItem.MenuItems.Add(Me.postFilterMenuItem)
            Me.filterMenuItem.MenuItems.Add(Me.accessFiltermenuItem)
            Me.filterMenuItem.Text = "Filter"
            '
            'preFilterMenuItem
            '
            Me.preFilterMenuItem.Text = "Pre-Filter..."
            '
            'postFilterMenuItem
            '
            Me.postFilterMenuItem.Text = "Post-Filter..."
            '
            'accessFiltermenuItem
            '
            Me.accessFiltermenuItem.Text = "Access-Filter..."
            '
            'accessMenuItem
            '
            Me.accessMenuItem.MenuItems.Add(Me.readMenuItem)
            Me.accessMenuItem.MenuItems.Add(Me.writeMenuItem)
            Me.accessMenuItem.MenuItems.Add(Me.lockMenuItem)
            Me.accessMenuItem.MenuItems.Add(Me.killMenuItem)
            Me.accessMenuItem.MenuItems.Add(Me.blockWriteMenuItem)
            Me.accessMenuItem.MenuItems.Add(Me.blockEraseMenuItem)
            Me.accessMenuItem.Text = "Access"
            '
            'readMenuItem
            '
            Me.readMenuItem.Text = "Read"
            '
            'writeMenuItem
            '
            Me.writeMenuItem.Text = "Write"
            '
            'lockMenuItem
            '
            Me.lockMenuItem.Text = "Lock"
            '
            'killMenuItem
            '
            Me.killMenuItem.Text = "Kill"
            '
            'blockWriteMenuItem
            '
            Me.blockWriteMenuItem.Text = "Block Write"
            '
            'blockEraseMenuItem
            '
            Me.blockEraseMenuItem.Text = "Block Erase"
            '
            'triggerMenuItem
            '
            Me.triggerMenuItem.Text = "Triggers..."
            '
            'mgmtMenuItem
            '
            Me.mgmtMenuItem.MenuItems.Add(Me.loginMenuItem)
            Me.mgmtMenuItem.MenuItems.Add(Me.systemInfoMenuItem)
            Me.mgmtMenuItem.MenuItems.Add(Me.antModeMenuItem)
            Me.mgmtMenuItem.MenuItems.Add(Me.readPointMenuItem)
            Me.mgmtMenuItem.MenuItems.Add(Me.softwareUpdateMenuItem)
            Me.mgmtMenuItem.MenuItems.Add(Me.rebootMenuItem)
            Me.mgmtMenuItem.Text = "Mgmt"
            '
            'loginMenuItem
            '
            Me.loginMenuItem.Text = "Login/Logout..."
            '
            'systemInfoMenuItem
            '
            Me.systemInfoMenuItem.Enabled = False
            Me.systemInfoMenuItem.Text = "System Information"
            '
            'antModeMenuItem
            '
            Me.antModeMenuItem.Enabled = False
            Me.antModeMenuItem.Text = "Antenna Mode"
            '
            'readPointMenuItem
            '
            Me.readPointMenuItem.Enabled = False
            Me.readPointMenuItem.Text = "Read Point"
            '
            'softwareUpdateMenuItem
            '
            Me.softwareUpdateMenuItem.Enabled = False
            Me.softwareUpdateMenuItem.Text = "Software/Firmware Update"
            '
            'rebootMenuItem
            '
            Me.rebootMenuItem.Enabled = False
            Me.rebootMenuItem.Text = "Reboot..."
            '
            'menuItem1
            '
            Me.menuItem1.MenuItems.Add(Me.helpMenuItem)
            Me.menuItem1.Text = "Help"
            '
            'helpMenuItem
            '
            Me.helpMenuItem.Text = "About"
            '
            'menuItem31
            '
            Me.menuItem31.Text = "-"
            '
            'exitMenuItem
            '
            Me.exitMenuItem.Text = "Exit"
            '
            'inventoryList
            '
            Me.inventoryList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.inventoryList.Columns.Add(Me.columnHeader1)
            Me.inventoryList.Columns.Add(Me.columnHeader8)
            Me.inventoryList.ContextMenu = Me.dataContextMenu
            Me.inventoryList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular)
            Me.inventoryList.FullRowSelect = True
            Me.inventoryList.Location = New System.Drawing.Point(3, 32)
            Me.inventoryList.Name = "inventoryList"
            Me.inventoryList.Size = New System.Drawing.Size(383, 252)
            Me.inventoryList.TabIndex = 3
            Me.inventoryList.View = System.Windows.Forms.View.Details
            '
            'columnHeader1
            '
            Me.columnHeader1.Text = "EPC ID"
            Me.columnHeader1.Width = 156
            '
            'columnHeader8
            '
            Me.columnHeader8.Text = "EXISTE"
            Me.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.columnHeader8.Width = 156
            '
            'dataContextMenu
            '
            Me.dataContextMenu.MenuItems.Add(Me.tagDataMenuItem)
            Me.dataContextMenu.MenuItems.Add(Me.readContextMenuItem)
            Me.dataContextMenu.MenuItems.Add(Me.writeContextMenuItem)
            Me.dataContextMenu.MenuItems.Add(Me.lockContextMenuItem)
            Me.dataContextMenu.MenuItems.Add(Me.killContextMenuItem)
            Me.dataContextMenu.MenuItems.Add(Me.blockEraseContextMenuItem)
            Me.dataContextMenu.MenuItems.Add(Me.blockWriteContextMenuItem)
            Me.dataContextMenu.MenuItems.Add(Me.menuItem5)
            Me.dataContextMenu.MenuItems.Add(Me.locateTagContextMenuItem)
            Me.dataContextMenu.MenuItems.Add(Me.menuItem3)
            Me.dataContextMenu.MenuItems.Add(Me.clearReportContextMenuItem)
            '
            'tagDataMenuItem
            '
            Me.tagDataMenuItem.Text = "Tag Data"
            '
            'readContextMenuItem
            '
            Me.readContextMenuItem.Text = "Read"
            '
            'writeContextMenuItem
            '
            Me.writeContextMenuItem.Text = "Write"
            '
            'lockContextMenuItem
            '
            Me.lockContextMenuItem.Text = "Lock"
            '
            'killContextMenuItem
            '
            Me.killContextMenuItem.Text = "Kill"
            '
            'blockEraseContextMenuItem
            '
            Me.blockEraseContextMenuItem.Text = "Block Write"
            '
            'blockWriteContextMenuItem
            '
            Me.blockWriteContextMenuItem.Text = "Block Erase"
            '
            'menuItem5
            '
            Me.menuItem5.Text = "-"
            '
            'locateTagContextMenuItem
            '
            Me.locateTagContextMenuItem.Text = "Locate Tag"
            '
            'menuItem3
            '
            Me.menuItem3.Text = "-"
            '
            'clearReportContextMenuItem
            '
            Me.clearReportContextMenuItem.Text = "Clear Reports"
            '
            'memBank_CB
            '
            Me.memBank_CB.Enabled = False
            Me.memBank_CB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular)
            Me.memBank_CB.ForeColor = System.Drawing.Color.Navy
            Me.memBank_CB.Items.Add("NONE")
            Me.memBank_CB.Items.Add("RESERVED")
            Me.memBank_CB.Items.Add("EPC")
            Me.memBank_CB.Items.Add("TID")
            Me.memBank_CB.Items.Add("USER")
            Me.memBank_CB.Location = New System.Drawing.Point(321, 8)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New System.Drawing.Size(65, 19)
            Me.memBank_CB.TabIndex = 2
            '
            'readButton
            '
            Me.readButton.Enabled = False
            Me.readButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular)
            Me.readButton.Location = New System.Drawing.Point(3, 6)
            Me.readButton.Name = "readButton"
            Me.readButton.Size = New System.Drawing.Size(75, 20)
            Me.readButton.TabIndex = 1
            Me.readButton.Text = "Start Reading"
            '
            'readTimeLabel
            '
            Me.readTimeLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.readTimeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular)
            Me.readTimeLabel.Location = New System.Drawing.Point(218, 297)
            Me.readTimeLabel.Name = "readTimeLabel"
            Me.readTimeLabel.Size = New System.Drawing.Size(61, 16)
            Me.readTimeLabel.Text = "Read Time:"
            '
            'totalTagLabel
            '
            Me.totalTagLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.totalTagLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular)
            Me.totalTagLabel.Location = New System.Drawing.Point(91, 297)
            Me.totalTagLabel.Name = "totalTagLabel"
            Me.totalTagLabel.Size = New System.Drawing.Size(59, 16)
            Me.totalTagLabel.Text = "Total Tags: "
            '
            'totalTagValueLabel
            '
            Me.totalTagValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.totalTagValueLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular)
            Me.totalTagValueLabel.Location = New System.Drawing.Point(158, 297)
            Me.totalTagValueLabel.Name = "totalTagValueLabel"
            Me.totalTagValueLabel.Size = New System.Drawing.Size(41, 16)
            Me.totalTagValueLabel.Text = "0(0)"
            '
            'readTimeValueLabel
            '
            Me.readTimeValueLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.readTimeValueLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular)
            Me.readTimeValueLabel.Location = New System.Drawing.Point(287, 297)
            Me.readTimeValueLabel.Name = "readTimeValueLabel"
            Me.readTimeValueLabel.Size = New System.Drawing.Size(40, 12)
            Me.readTimeValueLabel.Text = "0 Sec"
            '
            'functionCallStatusLabel
            '
            Me.functionCallStatusLabel.Caption = "API Call Result"
            Me.functionCallStatusLabel.Text = "Function Success"
            '
            'autonomousMode_CB
            '
            Me.autonomousMode_CB.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
            Me.autonomousMode_CB.Location = New System.Drawing.Point(227, 7)
            Me.autonomousMode_CB.Name = "autonomousMode_CB"
            Me.autonomousMode_CB.Size = New System.Drawing.Size(88, 20)
            Me.autonomousMode_CB.TabIndex = 4
            Me.autonomousMode_CB.Text = "Tag Events"
            '
            'AppForm
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
            Me.AutoScroll = True
            Me.ClientSize = New System.Drawing.Size(416, 315)
            Me.Controls.Add(Me.readButton)
            Me.Controls.Add(Me.autonomousMode_CB)
            Me.Controls.Add(Me.readTimeValueLabel)
            Me.Controls.Add(Me.totalTagValueLabel)
            Me.Controls.Add(Me.totalTagLabel)
            Me.Controls.Add(Me.readTimeLabel)
            Me.Controls.Add(Me.memBank_CB)
            Me.Controls.Add(Me.inventoryList)
            Me.Menu = Me.appFormMenu
            Me.MinimizeBox = False
            Me.Name = "AppForm"
            Me.Text = "VB_RFID3Sample6"
            Me.ResumeLayout(False)

        End Sub

        Private Sub inventoryList_ItemActivate(ByVal sender As Object, ByVal ea As EventArgs) Handles inventoryList.ItemActivate
            Dim location As New Point
            Me.inventoryList.PointToScreen(location)
            Me.dataContextMenu.Show(Me.inventoryList, New Point((Me.inventoryList.Width / 2), (Me.inventoryList.Height / 2)))
        End Sub

        Private Sub killContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles killContextMenuItem.Click
            If (Nothing Is Me.m_KillForm) Then
                Me.m_KillForm = New KillForm(Me)
            End If
            Me.m_KillForm.ShowDialog()
        End Sub

        Private Sub killMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles killMenuItem.Click
            If (Nothing Is Me.m_KillForm) Then
                Me.m_KillForm = New KillForm(Me)
            End If
            Me.m_KillForm.ShowDialog()
        End Sub

        Private Sub locateTagContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles locateTagContextMenuItem.Click
            If (Nothing Is Me.m_LocateForm) Then
                Me.m_LocateForm = New LocateForm(Me)
            End If
            Me.m_LocateForm.ShowDialog()
        End Sub

        Private Sub lockContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lockContextMenuItem.Click
            If (Nothing Is Me.m_LockForm) Then
                Me.m_LockForm = New LockForm(Me)
            End If
            Me.m_LockForm.ShowDialog()
        End Sub

        Private Sub lockMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lockMenuItem.Click
            If (Nothing Is Me.m_LockForm) Then
                Me.m_LockForm = New LockForm(Me)
            End If
            Me.m_LockForm.ShowDialog()
        End Sub

        Private Sub loginMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles loginMenuItem.Click
            If (Nothing Is Me.m_LoginForm) Then
                Me.m_LoginForm = New LoginForm(Me)
            End If
            Me.m_LoginForm.ShowDialog()
        End Sub

        Private Sub memBank_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles memBank_CB.SelectedIndexChanged
            If (Not Me.m_ReaderAPI Is Nothing) Then
                Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.DeleteAll()
                If (Me.memBank_CB.SelectedIndex > 0) Then
                    Dim op As New Operation
                    op.AccessOperationCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ
                    op.ReadAccessParams.MemoryBank = DirectCast((Me.memBank_CB.SelectedIndex - 1), MEMORY_BANK)
                    op.ReadAccessParams.ByteCount = 0
                    op.ReadAccessParams.ByteOffset = 0
                    op.ReadAccessParams.AccessPassword = 0
                    Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.Add(op)
                End If
            End If
        End Sub

        Private Sub myUpdateRead(ByVal eventData As ReadEventData)
            Dim tagData As Symbol.RFID3.TagData() = m_ReaderAPI.Actions.GetReadTags(1000)
            If tagData IsNot Nothing Then
                For nIndex As Integer = 0 To tagData.Length - 1
                    ' 
                    ' * Display all inventories tags or tags on which 
                    ' * Read access operation was successful 
                    ' 
                    If tagData(nIndex).ContainsLocationInfo Then
                        Me.m_LocateForm.Locate_PB.Value = tagData(nIndex).LocationInfo.RelativeDistance
                    End If

                    If tagData(nIndex).OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE OrElse (tagData(nIndex).OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ AndAlso tagData(nIndex).OpStatus = ACCESS_OPERATION_STATUS.ACCESS_SUCCESS) Then
                        Dim tag As Symbol.RFID3.TagData = tagData(nIndex)
                        Dim tagID As String = tag.TagID
                        Dim isFound As Boolean = False

                        SyncLock m_TagTable.SyncRoot
                            isFound = m_TagTable.ContainsKey(tagID)
                            If Not isFound Then
                                tagID += tag.MemoryBank.ToString
                                isFound = m_TagTable.ContainsKey(tagID)
                            End If
                        End SyncLock

                        If isFound Then
                            Dim count As UInteger = 0
                            Dim item As ListViewItem = Nothing
                            SyncLock m_TagTable.SyncRoot
                                item = DirectCast(m_TagTable(tagID), ListViewItem)
                            End SyncLock
                            Try
                                count = UInteger.Parse(item.SubItems(2).Text) + tagData(nIndex).TagSeenCount
                                m_TagTotalCount += tagData(nIndex).TagSeenCount
                            Catch fe As FormatException
                                notifyUser(fe.Message, "Tags")
                                Exit Try
                            End Try
                            item.SubItems(1).Text = tag.AntennaID.ToString()
                            item.SubItems(2).Text = count.ToString()
                            item.SubItems(3).Text = tag.PeakRSSI.ToString()

                            Dim memoryBank As String = tag.MemoryBank.ToString()
                            Dim index As Integer = memoryBank.LastIndexOf("_"c)
                            If index <> -1 Then
                                memoryBank = memoryBank.Substring(index + 1)
                            End If
                            If tag.MemoryBankData.Length > 0 OrElse Not tag.MemoryBankData.Equals(item.SubItems(5).Text) Then
                                item.SubItems(5).Text = tag.MemoryBankData
                                item.SubItems(6).Text = memoryBank
                                item.SubItems(7).Text = tag.MemoryBankDataOffset.ToString()

                                SyncLock m_TagTable.SyncRoot
                                    m_TagTable.Remove(tagID)
                                    Dim newID As String = tag.TagID + tag.MemoryBank.ToString()
                                    If Not m_TagTable.Contains(newID) Then
                                        m_TagTable.Add(newID, item)
                                    End If
                                End SyncLock
                            End If
                            If tag.TagEvent <> TAG_EVENT.NONE Then
                                If tag.TagEvent = TAG_EVENT.TAG_NOT_VISIBLE Then
                                    item.BackColor = Color.LightGray
                                Else
                                    item.BackColor = Color.White
                                End If
                                inventoryList.Refresh()
                            End If
                        Else
                            Dim item As New ListViewItem(tag.TagID)
                            Dim subItem As ListViewItem.ListViewSubItem

                            subItem = New ListViewItem.ListViewSubItem()
                            subItem.Text = tag.AntennaID.ToString()
                            item.SubItems.Add(subItem)

                            subItem = New ListViewItem.ListViewSubItem()
                            subItem.Text = tag.TagSeenCount.ToString()
                            m_TagTotalCount += tag.TagSeenCount
                            item.SubItems.Add(subItem)

                            subItem = New ListViewItem.ListViewSubItem()
                            subItem.Text = tag.PeakRSSI.ToString()
                            item.SubItems.Add(subItem)

                            subItem = New ListViewItem.ListViewSubItem()
                            subItem.Text = tag.PC.ToString("X")
                            item.SubItems.Add(subItem)

                            If tag.MemoryBankData <> String.Empty Then
                                subItem = New ListViewItem.ListViewSubItem()
                                subItem.Text = tag.MemoryBankData
                                item.SubItems.Add(subItem)

                                Dim memoryBank As String = tag.MemoryBank.ToString()
                                Dim index As Integer = memoryBank.LastIndexOf("_"c)
                                If index <> -1 Then
                                    memoryBank = memoryBank.Substring(index + 1)
                                End If

                                subItem = New ListViewItem.ListViewSubItem()
                                subItem.Text = memoryBank
                                item.SubItems.Add(subItem)

                                subItem = New ListViewItem.ListViewSubItem()
                                subItem.Text = tag.MemoryBankDataOffset.ToString()
                                item.SubItems.Add(subItem)
                            Else
                                subItem = New ListViewItem.ListViewSubItem()
                                subItem.Text = ""
                                item.SubItems.Add(subItem)
                                subItem = New ListViewItem.ListViewSubItem()
                                subItem.Text = ""
                                item.SubItems.Add(subItem)
                                subItem = New ListViewItem.ListViewSubItem()
                                subItem.Text = ""
                                item.SubItems.Add(subItem)
                            End If
                            inventoryList.BeginUpdate()
                            inventoryList.Items.Add(item)
                            inventoryList.EndUpdate()

                            SyncLock m_TagTable.SyncRoot
                                m_TagTable.Add(tagID, item)
                            End SyncLock
                        End If
                    End If
                Next

                '* Update front panel Tag Count while changing inventory list 


                Me.totalTagValueLabel.Text = (m_TagTable.Count.ToString & "(") + m_TagTotalCount.ToString & ")"
            End If

        End Sub

        Private Sub myUpdateStatus(ByVal eventData As StatusEventData)
            Select Case eventData.StatusEventType
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_START_EVENT
                    readButton.Text = "Stop Reading"
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT
                    readButton.Text = "Start Reading"

                    If Me.m_DurationTriggerTime > 0 Then
                        Dim triggerDuration As TimeSpan = TimeSpan.FromMilliseconds(m_DurationTriggerTime)
                        readTimeValueLabel.Text = triggerDuration.Seconds & "." & triggerDuration.Milliseconds \ 100 & " Sec"
                    End If
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_START_EVENT
                    readButton.Text = "Stop Reading"
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_STOP_EVENT
                    readButton.Text = "Start Reading"
                    m_AccessOpResult.m_AccessCompleteEvent.[Set]()
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_WARNING_EVENT
                    notifyUser("Buffer Full Warning", "Tags")
                    myUpdateRead(Nothing)
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.BUFFER_FULL_EVENT
                    notifyUser("Buffer Full", "Tags")
                    myUpdateRead(Nothing)
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.DISCONNECTION_EVENT
                    notifyUser(eventData.DisconnectionEventData.DisconnectEventInfo.ToString(), "Disconnection")
                    m_ReaderInitiatedDisconnectionReceived = True
                    Me.Connect("Disconnect")
                    Dim disconnectionThread As New Thread(AddressOf Disconnect)
                    disconnectionThread.Start()
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.READER_EXCEPTION_EVENT
                    notifyUser(eventData.ReaderExceptionEventData.ReaderExceptionEventInfo, "Reader Exception")
                    Exit Select
                Case Else

                    Exit Select
            End Select
        End Sub
        Friend Sub Disconnect()
            Try
                m_ReaderAPI.Disconnect()
                m_ReaderInitiatedDisconnectionReceived = False
            Catch ofe As OperationFailureException
                notifyUser(ofe.VendorMessage, "DisConnect")
            End Try
        End Sub

        Friend Sub notifyUser(ByVal notificationMessage As String, ByVal notificationSource As String)
            MessageBox.Show(notificationMessage, notificationSource)
        End Sub

        Private Sub postFilterMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles postFilterMenuItem.Click
            Me.m_PostFilterForm.ShowDialog()
        End Sub

        Private Sub preFilterMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles preFilterMenuItem.Click
            If (Nothing Is Me.m_PreFilterForm) Then
                Me.m_PreFilterForm = New PreFilterForm(Me)
            End If
            Me.m_PreFilterForm.ShowDialog()
        End Sub

        Private Sub radioPowerMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles radioPowerMenuItem.Click
            Try
                If (Me.radioPowerMenuItem.Text = "Power Off Radio") Then
                    Me.m_ReaderAPI.Config.RadioPowerState = RADIO_POWER_STATE.OFF
                Else
                    Me.m_ReaderAPI.Config.RadioPowerState = RADIO_POWER_STATE.ON
                End If
                Me.radioPowerMenuItem.Text = IIf((Me.m_ReaderAPI.Config.RadioPowerState = RADIO_POWER_STATE.OFF), "Power On Radio", "Power Off Radio")
            Catch iue As InvalidUsageException
                Me.notifyUser(iue.Info, "Radio Power Control")
            Catch ofe As OperationFailureException
                Me.notifyUser(ofe.StatusDescription, "Radio Power Control")
            Catch ex As Exception
                Me.notifyUser(ex.Message, "Radio Power Control")
            End Try
        End Sub

        Private Sub readButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readButton.Click
            Try
                If Me.m_IsConnected Then
                    If (Me.readButton.Text = "Start Reading") Then
                        Me.StartReading()
                    ElseIf (Me.readButton.Text = "Stop Reading") Then
                        Me.StopReading()
                    End If
                Else
                    Me.notifyUser("Please connect to a reader", "Read Operation")
                End If
            Catch ex As OperationFailureException
                Me.notifyUser(ex.VendorMessage, "Read Operation")
            End Try
        End Sub

        Private Sub readContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readContextMenuItem.Click
            Me.m_ReadForm.ShowDialog()
        End Sub

        Private Sub readMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readMenuItem.Click
            Me.m_ReadForm.ShowDialog()
        End Sub

        Private Sub readPointMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readPointMenuItem.Click
        End Sub

        Private Sub rebootMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rebootMenuItem.Click
            Try
                Me.m_ReaderMgmt.Restart()
                Me.rebootMenuItem.Enabled = False
                Me.antModeMenuItem.Enabled = False
                Me.radioPowerMenuItem.Enabled = False
                Me.softwareUpdateMenuItem.Enabled = False
                If (Not Me.m_LoginForm Is Nothing) Then
                    Me.m_LoginForm.loginButton.Text = "Login"
                End If
            Catch failureException As OperationFailureException
                Me.notifyUser(failureException.VendorMessage, "Reboot")
            End Try
        End Sub

        Private Sub resetButtonState()
            If (Not Me.m_ReadForm Is Nothing) Then
                Me.m_ReadForm.readButton.Enabled = True
            End If
            If (Not Me.m_WriteForm Is Nothing) Then
                Me.m_WriteForm.writeButton.Enabled = True
            End If
            If (Not Me.m_LockForm Is Nothing) Then
                Me.m_LockForm.lockButton.Enabled = True
            End If
            If (Not Me.m_KillForm Is Nothing) Then
                Me.m_KillForm.killButton.Enabled = True
            End If
            If (Not Me.m_BlockWriteForm Is Nothing) Then
                Me.m_BlockWriteForm.writeButton.Enabled = True
            End If
            If (Not Me.m_BlockEraseForm Is Nothing) Then
                Me.m_BlockEraseForm.eraseButton.Enabled = True
            End If
        End Sub

        Private Sub ResetMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ResetMenuItem.Click
            Try
                If Me.m_ReaderAPI.IsConnected Then
                    Me.m_ReaderAPI.Config.ResetFactoryDefaults()
                    If (Not Me.m_TagStorageForm Is Nothing) Then
                        Me.m_TagStorageForm.Reset()
                    End If
                    If (Not Nothing Is Me.m_AntennaConfigForm) Then
                        Me.m_AntennaConfigForm.m_IsLoaded = False
                    End If
                    If (Not Nothing Is Me.m_RFModeForm) Then
                        Me.m_RFModeForm.m_IsLoaded = False
                    End If
                    If (Not Nothing Is Me.m_SingulationForm) Then
                        Me.m_SingulationForm.m_IsLoaded = False
                    End If
                End If
            Catch ex As Exception
                Me.notifyUser(ex.Message, "Reset")
            End Try
        End Sub

        Private Sub RFModeMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rFModeMenuItem.Click
            If (Nothing Is Me.m_RFModeForm) Then
                Me.m_RFModeForm = New RFModeForm(Me)
            End If
            Me.m_RFModeForm.ShowDialog()
        End Sub

        Friend Sub RunAccessOps(ByVal opCommand As ACCESS_OPERATION_CODE)
            ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Me.startAccessCallback), opCommand)
        End Sub

        Private Sub sampleAppResultItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Private Sub singulationMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles singulationMenuItem.Click
            If (Nothing Is Me.m_SingulationForm) Then
                Me.m_SingulationForm = New SingulationForm(Me)
            End If
            Me.m_SingulationForm.ShowDialog()
        End Sub

        Private Sub softwareUpdateMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles softwareUpdateMenuItem.Click
            If (Nothing Is Me.m_FirmwareUpdateForm) Then
                Me.m_FirmwareUpdateForm = New FirmwareUpdateForm(Me)
            End If
            Me.m_FirmwareUpdateForm.ShowDialog()
        End Sub

        Private Sub startAccessCallback(ByVal opCommand As Object)
            Dim result As String = RFIDResults.RFID_API_SUCCESS.ToString
            Try
                Me.m_AccessOpResult.m_OpCode = DirectCast(opCommand, ACCESS_OPERATION_CODE)
                Me.m_AccessOpResult.m_Result = "Access Succeeded"
                Me.m_AccessOpResult.m_AccessCompleteEvent.Reset()
                If (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReadTag = Me.m_ReaderAPI.Actions.TagAccess.ReadWait(Me.m_SelectedTagID, Me.m_ReadForm.m_ReadParams, Me.m_AntennaInfoForm.getInfo)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.ReadEvent(Me.m_ReadForm.m_ReadParams, Me.m_AccessFilterForm.getFilter, Me.m_AntennaInfoForm.getInfo)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.WriteWait(Me.m_SelectedTagID, Me.m_WriteForm.m_WriteParams, Me.m_AntennaInfoForm.getInfo)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.WriteEvent(Me.m_WriteForm.m_WriteParams, Me.m_AccessFilterForm.getFilter, Me.m_AntennaInfoForm.getInfo)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.LockWait(Me.m_SelectedTagID, Me.m_LockForm.m_LockParams, Me.m_AntennaInfoForm.getInfo)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.LockEvent(Me.m_LockForm.m_LockParams, Me.m_AccessFilterForm.getFilter, Me.m_AntennaInfoForm.getInfo)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.KillWait(Me.m_SelectedTagID, Me.m_KillForm.m_KillParams, Me.m_AntennaInfoForm.getInfo)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.KillEvent(Me.m_KillForm.m_KillParams, Me.m_AccessFilterForm.getFilter, Me.m_AntennaInfoForm.getInfo)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.BlockWriteWait(Me.m_SelectedTagID, Me.m_BlockWriteForm.m_WriteParams, Me.m_AntennaInfoForm.getInfo)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.BlockWriteEvent(Me.m_BlockWriteForm.m_WriteParams, Me.m_AccessFilterForm.getFilter, Me.m_AntennaInfoForm.getInfo)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.BlockEraseWait(Me.m_SelectedTagID, Me.m_BlockEraseForm.m_BlockEraseParams, Me.m_AntennaInfoForm.getInfo)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.BlockEraseEvent(Me.m_BlockEraseForm.m_BlockEraseParams, Me.m_AccessFilterForm.getFilter, Me.m_AntennaInfoForm.getInfo)
                    End If
                End If
                Me.m_AccessOpResult.m_AccessCompleteEvent.WaitOne()
            Catch iue As InvalidUsageException
                Me.m_AccessOpResult.m_Result = iue.Info
            Catch ofe As OperationFailureException
                Me.m_AccessOpResult.m_Result = IIf((ofe.VendorMessage = "None"), ofe.StatusDescription, ofe.VendorMessage)
            End Try
            MyBase.Invoke(New UpdateStatusLabel(AddressOf Me.updateStatus), New Object() {Me.m_AccessOpResult.m_Result})
        End Sub

        Private Sub StartReading()
            Try
                If (Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0) Then
                    Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.PerformSequence(Me.m_AccessFilterForm.getFilter, Me.m_TriggerForm.getTriggerInfo, Me.m_AntennaInfoForm.getInfo)
                Else
                    Me.inventoryList.Items.Clear()
                    Me.m_TagTable.Clear()
                    Me.m_TagTotalCount = 0
                    Me.m_ReaderAPI.Actions.Inventory.Perform(Me.m_PostFilterForm.getFilter, Me.m_TriggerForm.getTriggerInfo, Me.m_AntennaInfoForm.getInfo)
                End If
            Catch ioe As InvalidOperationException
                Me.notifyUser(("InvalidOperationException" & ioe.Message), "Read Operation")
            Catch ex As OperationFailureException
                Me.notifyUser(("OperationFailureException:" & ex.VendorMessage), "Read Operation")
            Catch iue As InvalidUsageException
                Me.notifyUser(("InvalidUsageException:" & iue.Info), "Read Operation")
            Catch ex As Exception
                Me.notifyUser(("Exception:" & ex.Message), "Read Operation")
            End Try
            Me.m_StartTime = TimeSpan.FromMilliseconds(CDbl(Environment.TickCount))
        End Sub

        Private Sub StopReading()
            If (Me.m_DurationTriggerTime = 0) Then
                Dim duration As TimeSpan = TimeSpan.FromMilliseconds(CDbl(Environment.TickCount)).Subtract(Me.m_StartTime).Duration
                Me.readTimeValueLabel.Text = String.Concat(New Object() {duration.Seconds, ".", (duration.Milliseconds / 100), " Sec"})
            End If
            Try
                If (Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0) Then
                    Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.StopSequence()
                Else
                    Me.m_ReaderAPI.Actions.Inventory.Stop()
                End If
            Catch ioe As InvalidOperationException
                Me.notifyUser(("InvalidOperationException" & ioe.Message), "Stop Operation")
            Catch ofe As OperationFailureException
                Me.notifyUser(("OperationFailureException:" & ofe.VendorMessage), "Stop Read")
            Catch iue As InvalidUsageException
                Me.notifyUser(("InvalidUsageException:" & iue.Info), "Stop Read")
            Catch ex As Exception
                Me.notifyUser(("Exception:" & ex.Message), "Stop Read")
            End Try
        End Sub

        Private Sub systemInfoMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles systemInfoMenuItem.Click
            If (Nothing Is Me.m_SystemInfoForm) Then
                Me.m_SystemInfoForm = New SystemInfoForm(Me)
            End If
            Me.m_SystemInfoForm.ShowDialog()
        End Sub

        Private Sub tagDataMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tagDataMenuItem.Click
            Dim tagDataForm As TagDataForm
            tagDataForm = New TagDataForm(Me)
            tagDataForm.ShowDialog()
        End Sub

        Private Sub tagStorageMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tagStorageMenuItem.Click
            If (Nothing Is Me.m_TagStorageForm) Then
                Me.m_TagStorageForm = New TagStorageForm(Me)
            End If
            Me.m_TagStorageForm.ShowDialog()
        End Sub

        Private Sub triggerMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles triggerMenuItem.Click
            Me.m_TriggerForm.ShowDialog()
        End Sub

        Private Sub updateStatus(ByVal result As String)
            Dim accessStats As String = Nothing
            If result = "Access Succeeded" Then
                If Me.m_SelectedTagID = String.Empty OrElse Me.m_SelectedTagID Is Nothing Then
                    Dim successCount As UInteger, failureCount As UInteger
                    successCount = 0
                    failureCount = 0
                    m_ReaderAPI.Actions.TagAccess.GetLastAccessResult(successCount, failureCount)
                    accessStats = ("Succeeded on " & successCount.ToString() & "tags. Failed on ") + failureCount.ToString()
                End If
                If m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ Then
                    If m_SelectedTagID <> [String].Empty Then
                        If m_ReadTag.OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ AndAlso m_ReadTag.OpStatus = ACCESS_OPERATION_STATUS.ACCESS_SUCCESS Then
                            Dim tagID As String = m_ReadTag.TagID + m_ReadTag.MemoryBank.ToString() + m_ReadTag.MemoryBankDataOffset.ToString()
                            Dim selectedIndex As Integer = Me.inventoryList.SelectedIndices(0)
                            Dim item As ListViewItem = Me.inventoryList.Items(selectedIndex)

                            If item.SubItems(5).Text.Length > 0 Then
                                Dim isFound As Boolean = False

                                ' Search or add new one 
                                SyncLock m_TagTable.SyncRoot
                                    isFound = m_TagTable.ContainsKey(tagID)
                                End SyncLock

                                If Not isFound Then
                                    Dim newItem As New ListViewItem(m_ReadTag.TagID)
                                    consultarRFID(m_ReadTag.TagID)

                                    'newItem.SubItems.Add(m_ReadTag.AntennaID.ToString())
                                    'newItem.SubItems.Add(m_ReadTag.TagSeenCount.ToString())
                                    'm_TagTotalCount += m_ReadTag.TagSeenCount
                                    'newItem.SubItems.Add(m_ReadTag.PeakRSSI.ToString())
                                    'newItem.SubItems.Add(m_ReadTag.PC.ToString("X"))
                                    'newItem.SubItems.Add(m_ReadTag.MemoryBankData)

                                    Dim memoryBank As String = m_ReadTag.MemoryBank.ToString()
                                    Dim index As Integer = memoryBank.LastIndexOf("_"c)
                                    If index <> -1 Then
                                        memoryBank = memoryBank.Substring(index + 1)
                                    End If
                                    'newItem.SubItems.Add(memoryBank)
                                    'newItem.SubItems.Add(m_ReadTag.MemoryBankDataOffset.ToString())

                                    inventoryList.BeginUpdate()
                                    inventoryList.Items.Add(newItem)
                                    inventoryList.EndUpdate()

                                    SyncLock m_TagTable.SyncRoot
                                        m_TagTable.Add(tagID, newItem)
                                    End SyncLock
                                Else
                                    'item.SubItems(5).Text = m_ReadTag.MemoryBankData
                                    'item.SubItems(7).Text = m_ReadTag.MemoryBankDataOffset.ToString()
                                End If
                            Else
                                ' Empty Memory Bank Slot 
                                'item.SubItems(5).Text = m_ReadTag.MemoryBankData

                                Dim memoryBank As String = m_ReadForm.m_ReadParams.MemoryBank.ToString()
                                Dim index As Integer = memoryBank.LastIndexOf("_"c)
                                If index <> -1 Then
                                    memoryBank = memoryBank.Substring(index + 1)
                                End If
                                'item.SubItems(6).Text = memoryBank
                                'item.SubItems(7).Text = m_ReadTag.MemoryBankDataOffset.ToString()

                                SyncLock m_TagTable.SyncRoot
                                    If m_ReadTag.TagID IsNot Nothing Then
                                        m_TagTable.Remove(m_ReadTag.TagID)
                                    End If
                                    m_TagTable.Add(tagID, item)
                                End SyncLock
                            End If
                            Me.m_ReadForm.ReadData_TB.Text = m_ReadTag.MemoryBankData
                        End If
                        notifyUser("Read Succeed", "Access Operation")
                    Else
                        notifyUser(accessStats, "Read Operation")
                    End If
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE Then
                    m_WriteForm.writeButton.Enabled = True
                    If Me.m_SelectedTagID = String.Empty OrElse Me.m_SelectedTagID Is Nothing Then
                        notifyUser(accessStats, "Write Operation")
                    Else
                        notifyUser("Write Succeed", "Access Operation")
                    End If
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK Then
                    m_LockForm.lockButton.Enabled = True
                    If Me.m_SelectedTagID = String.Empty OrElse Me.m_SelectedTagID Is Nothing Then
                        notifyUser(accessStats, "Lock Operation")
                    Else
                        notifyUser("Lock Succeed", "Access Operation")
                    End If
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL Then
                    m_KillForm.killButton.Enabled = True
                    If Me.m_SelectedTagID = String.Empty OrElse Me.m_SelectedTagID Is Nothing Then
                        notifyUser(accessStats, "Kill Operation")
                    Else
                        notifyUser("Kill Succeed", "Access Operation")
                    End If
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE Then
                    m_BlockWriteForm.writeButton.Enabled = True
                    If Me.m_SelectedTagID = String.Empty OrElse Me.m_SelectedTagID Is Nothing Then
                        notifyUser(accessStats, "Block Write Operation")
                    Else
                        notifyUser("Block Write Succeed", "Access Operation")
                    End If
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE Then
                    m_BlockEraseForm.eraseButton.Enabled = True
                    If Me.m_SelectedTagID = String.Empty OrElse Me.m_SelectedTagID Is Nothing Then
                        notifyUser(accessStats, "Write Operation")
                    Else
                        notifyUser("Block Erase Succeed", "Access Operation")
                    End If
                End If
            Else
                notifyUser(m_AccessOpResult.m_Result, "Access Operation")
            End If
            resetButtonState()
            Me.readButton.Enabled = True
        End Sub
        Private Sub consultarRFID(ByVal tag As String)
            'Dim sql As String = "select * from RFID2018 where CODIGO_RFID='" + tag + "'"
            'Dim cmd As New SqlCeCommand(sql, conln)
            'cmd.CommandType = CommandType.Text
            'cmd.ExecuteNonQuery()

        End Sub

        Private Sub writeContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles writeContextMenuItem.Click
            If (Nothing Is Me.m_WriteForm) Then
                Me.m_WriteForm = New WriteForm(Me, False)
            End If
            Me.m_WriteForm.ShowDialog()
        End Sub

        Private Sub writeMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles writeMenuItem.Click
            If (Nothing Is Me.m_WriteForm) Then
                Me.m_WriteForm = New WriteForm(Me, False)
            End If
            Me.m_WriteForm.ShowDialog()
        End Sub


        ' Fields
        Private WithEvents accessFiltermenuItem As MenuItem
        Private WithEvents accessMenuItem As MenuItem
        Private WithEvents antennaMenuItem As MenuItem
        Private WithEvents antInfoMenuItem As MenuItem
        Friend WithEvents antModeMenuItem As MenuItem
        Private appFormMenu As MainMenu
        Friend WithEvents autonomousMode_CB As CheckBox
        Private WithEvents blockEraseContextMenuItem As MenuItem
        Private WithEvents blockEraseMenuItem As MenuItem
        Private WithEvents blockWriteContextMenuItem As MenuItem
        Private WithEvents blockWriteMenuItem As MenuItem
        Private WithEvents capMenuItem As MenuItem
        Private WithEvents clearReportContextMenuItem As MenuItem
        Private columnHeader1 As ColumnHeader
        Private columnHeader8 As ColumnHeader
        Private components As IContainer = Nothing
        Private configMenuItem As MenuItem
        Private WithEvents connectionMenuItem As MenuItem
        Friend dataContextMenu As ContextMenu
        Private WithEvents exitMenuItem As MenuItem
        Private filterMenuItem As MenuItem
        Friend functionCallStatusLabel As Notification
        Private WithEvents gpioMenuItem As MenuItem
        Private WithEvents helpMenuItem As MenuItem
        Friend WithEvents inventoryList As ListView
        Private WithEvents killContextMenuItem As MenuItem
        Private WithEvents killMenuItem As MenuItem
        Private WithEvents locateTagContextMenuItem As MenuItem
        Private WithEvents lockContextMenuItem As MenuItem
        Private WithEvents lockMenuItem As MenuItem
        Private WithEvents loginMenuItem As MenuItem
        Friend m_AccessFilterForm As AccessFilterForm
        Friend m_AccessOpResult As AccessOperationResult
        Friend m_AntennaConfigForm As AntennaConfigForm
        Friend m_AntennaInfoForm As AntennaInfoForm
        Friend m_AntennaModeForm As AntennaModeForm
        Friend m_BlockEraseForm As BlockEraseForm
        Friend m_BlockWriteForm As WriteForm
        Friend m_CapabilitiesForm As CapabilitiesForm
        Friend m_ConnectionForm As ConnectionForm
        Friend m_DurationTriggerTime As UInt32
        Friend m_FirmwareUpdateForm As FirmwareUpdateForm
        Friend m_IsConnected As Boolean
        Friend m_KillForm As KillForm
        Friend m_LockForm As LockForm
        Friend m_LocateForm As LocateForm
        Friend m_LoginForm As LoginForm
        Friend m_PostFilterForm As PostFilterForm
        Friend m_PreFilterForm As PreFilterForm
        Friend m_ReaderAPI As RFIDReader
        Friend m_ReaderMgmt As ReaderManagement
        Friend m_ReaderType As READER_TYPE
        Friend m_ReadForm As ReadForm
        Private m_ReadTag As TagData
        Friend m_RFModeForm As RFModeForm
        Friend m_SelectedTagID As String
        Friend m_SingulationForm As SingulationForm
        Private m_StartTime As TimeSpan
        Friend m_SystemInfoForm As SystemInfoForm
        Friend m_TagStorageForm As TagStorageForm
        Private m_TagTable As Hashtable
        Private m_TagTotalCount As UInt32
        Friend m_TriggerForm As TriggerForm
        Private m_UpdateReadHandler As UpdateRead = Nothing
        Private m_UpdateStatusHandler As updateStatusResult = Nothing
        Friend m_WriteForm As WriteForm
        Private WithEvents memBank_CB As ComboBox
        Private WithEvents menuItem1 As MenuItem
        Private WithEvents menuItem2 As MenuItem
        Private menuItem3 As MenuItem
        Private menuItem31 As MenuItem
        Private menuItem4 As MenuItem
        Private menuItem5 As MenuItem
        Private mgmtMenuItem As MenuItem
        Private operationMenuItem As MenuItem
        Private WithEvents postFilterMenuItem As MenuItem
        Private WithEvents preFilterMenuItem As MenuItem
        Friend WithEvents radioPowerMenuItem As MenuItem
        Private WithEvents readButton As Button
        Private WithEvents readContextMenuItem As MenuItem
        Private WithEvents readMenuItem As MenuItem
        Private WithEvents readPointMenuItem As MenuItem
        Private readTimeLabel As Label
        Private readTimeValueLabel As Label
        Friend WithEvents rebootMenuItem As MenuItem
        Private WithEvents ResetMenuItem As MenuItem
        Private WithEvents rFModeMenuItem As MenuItem
        Private WithEvents sampleAppMenuItem As MenuItem
        Private WithEvents singulationMenuItem As MenuItem
        Friend WithEvents softwareUpdateMenuItem As MenuItem
        Friend WithEvents systemInfoMenuItem As MenuItem
        Private WithEvents tagDataMenuItem As MenuItem
        Private WithEvents tagStorageMenuItem As MenuItem
        Private totalTagLabel As Label
        Private totalTagValueLabel As Label
        Private WithEvents triggerMenuItem As MenuItem
        Private WithEvents writeContextMenuItem As MenuItem
        Private WithEvents writeMenuItem As MenuItem
        Private m_ReaderInitiatedDisconnectionReceived As Boolean

        ' Nested Types
        Friend Class AccessOperationResult
            ' Fields
            Public m_AccessCompleteEvent As AutoResetEvent = New AutoResetEvent(False)
            Public m_OpCode As ACCESS_OPERATION_CODE = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ
            Public m_Result As String = ""
        End Class

        Private Delegate Sub UpdateRead(ByVal eventData As ReadEventData)

        Private Delegate Sub updateStatusResult(ByVal eventData As StatusEventData)

        Private Delegate Sub UpdateStatusLabel(ByVal [text] As String)

    End Class
End Namespace

