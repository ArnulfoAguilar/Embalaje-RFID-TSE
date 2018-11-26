Imports Microsoft.WindowsCE.Forms
Imports Symbol.RFID3
Imports Symbol.RFID3.Events
Imports System
Imports System.Data
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Windows.Forms
Imports System.Windows.Forms.ListViewItem

Namespace VB_RFID3Sample5
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
            Me.m_ReaderMgmt = New ReaderManagement
            Me.m_TagTable = New Hashtable(&H3FF)
            Me.m_AccessOpResult = New AccessOperationResult
            Me.m_IsConnected = False
            Me.m_TagTotalCount = 0

        End Sub

        Private Sub AppForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs) Handles MyBase.Closing
            Me.CloseForm()
        End Sub

        Private Sub AppForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Label_inconsistencia.Hide()
            dbug_lbl.Hide()
            Me.memBank_CB.SelectedIndex = 0
            Me.Connect("Connect")
            readButton.Focus()

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
            'Me.totalTagValueLabel.Text = ""
            'Me.readTimeValueLabel.Text = ""
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
            Me.blockEraseContextMenuItem.Enabled = Me.m_ReaderAPI.ReaderCapabilities.IsBlockEraseSupported
            Me.blockWriteContextMenuItem.Enabled = Me.m_ReaderAPI.ReaderCapabilities.IsBlockWriteSupported
        End Sub

        Friend Sub configureMenuItemsUponConnectDisconnect()
            Me.capMenuItem.Enabled = Me.m_IsConnected
            Me.configMenuItem.Enabled = Me.m_IsConnected
            Me.ResetMenuItem.Enabled = Me.m_IsConnected
            Me.accessMenuItem.Enabled = Me.m_IsConnected
            Me.readButton.Enabled = Me.m_IsConnected
            Me.memBank_CB.Enabled = Me.m_IsConnected
        End Sub

        Friend Sub configureMenuItemsUponLoginLogout()
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
                    notifyUser(ofe.VendorMessage, "Tags")
                End Try
            ElseIf status = "Disconnect" Then
                Try
                    m_ReaderAPI.Disconnect()
                Catch ofe As OperationFailureException
                    notifyUser(ofe.VendorMessage, "Connect")
                End Try

                Me.Text = "VB_RFID3Sample5"
                Me.m_ConnectionForm.connectionButton.Text = "Connect"
                If m_ConnectionForm.Visible Then
                    Me.m_ConnectionForm.Close()
                End If
                Me.readButton.Enabled = False
                Me.memBank_CB.Enabled = False
            End If
            m_IsConnected = m_ReaderAPI.IsConnected
            configureMenuItemsUponConnectDisconnect()
            configureMenuItemsBasedOnCapabilities()

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

        Private Sub helpMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles helpMenuItem.Click
            Dim helpForm As HelpForm
            helpForm = New HelpForm(Me)
            helpForm.ShowDialog()
        End Sub

        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AppForm))
            Me.appFormMenu = New System.Windows.Forms.MainMenu
            Me.connectionMenuItem = New System.Windows.Forms.MenuItem
            Me.sampleAppMenuItem = New System.Windows.Forms.MenuItem
            Me.capMenuItem = New System.Windows.Forms.MenuItem
            Me.configMenuItem = New System.Windows.Forms.MenuItem
            Me.ResetMenuItem = New System.Windows.Forms.MenuItem
            Me.operationMenuItem = New System.Windows.Forms.MenuItem
            Me.accessMenuItem = New System.Windows.Forms.MenuItem
            Me.readMenuItem = New System.Windows.Forms.MenuItem
            Me.writeMenuItem = New System.Windows.Forms.MenuItem
            Me.lockMenuItem = New System.Windows.Forms.MenuItem
            Me.killMenuItem = New System.Windows.Forms.MenuItem
            Me.blockWriteMenuItem = New System.Windows.Forms.MenuItem
            Me.blockEraseMenuItem = New System.Windows.Forms.MenuItem
            Me.menuItem1 = New System.Windows.Forms.MenuItem
            Me.helpMenuItem = New System.Windows.Forms.MenuItem
            Me.menuItem31 = New System.Windows.Forms.MenuItem
            Me.exitMenuItem = New System.Windows.Forms.MenuItem
            Me.inventoryList = New System.Windows.Forms.ListView
            Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
            Me.columnHeader8 = New System.Windows.Forms.ColumnHeader
            Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
            Me.columnHeader3 = New System.Windows.Forms.ColumnHeader
            Me.columnHeader4 = New System.Windows.Forms.ColumnHeader
            Me.columnHeader5 = New System.Windows.Forms.ColumnHeader
            Me.columnHeader6 = New System.Windows.Forms.ColumnHeader
            Me.columnHeader7 = New System.Windows.Forms.ColumnHeader
            Me.dataContextMenu = New System.Windows.Forms.ContextMenu
            Me.tagDataMenuItem = New System.Windows.Forms.MenuItem
            Me.readContextMenuItem = New System.Windows.Forms.MenuItem
            Me.writeContextMenuItem = New System.Windows.Forms.MenuItem
            Me.lockContextMenuItem = New System.Windows.Forms.MenuItem
            Me.killContextMenuItem = New System.Windows.Forms.MenuItem
            Me.blockEraseContextMenuItem = New System.Windows.Forms.MenuItem
            Me.blockWriteContextMenuItem = New System.Windows.Forms.MenuItem
            Me.menuItem3 = New System.Windows.Forms.MenuItem
            Me.clearReportContextMenuItem = New System.Windows.Forms.MenuItem
            Me.memBank_CB = New System.Windows.Forms.ComboBox
            Me.readButton = New System.Windows.Forms.Button
            Me.functionCallStatusLabel = New Microsoft.WindowsCE.Forms.Notification
            Me.Label_inconsistencia = New System.Windows.Forms.Label
            Me.Label_leida = New System.Windows.Forms.Label
            Me.dbug_lbl = New System.Windows.Forms.Label
            Me.PictureBox1 = New System.Windows.Forms.PictureBox
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
            Me.sampleAppMenuItem.Enabled = False
            Me.sampleAppMenuItem.MenuItems.Add(Me.capMenuItem)
            Me.sampleAppMenuItem.MenuItems.Add(Me.configMenuItem)
            Me.sampleAppMenuItem.MenuItems.Add(Me.operationMenuItem)
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
            Me.configMenuItem.MenuItems.Add(Me.ResetMenuItem)
            Me.configMenuItem.Text = "Config"
            '
            'ResetMenuItem
            '
            Me.ResetMenuItem.Text = "Reset To Factory Default"
            '
            'operationMenuItem
            '
            Me.operationMenuItem.MenuItems.Add(Me.accessMenuItem)
            Me.operationMenuItem.Text = "Operations"
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
            Me.inventoryList.Columns.Add(Me.columnHeader2)
            Me.inventoryList.Columns.Add(Me.columnHeader3)
            Me.inventoryList.Columns.Add(Me.columnHeader4)
            Me.inventoryList.Columns.Add(Me.columnHeader5)
            Me.inventoryList.Columns.Add(Me.columnHeader6)
            Me.inventoryList.Columns.Add(Me.columnHeader7)
            Me.inventoryList.ContextMenu = Me.dataContextMenu
            Me.inventoryList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular)
            Me.inventoryList.FullRowSelect = True
            Me.inventoryList.Location = New System.Drawing.Point(247, 15)
            Me.inventoryList.Name = "inventoryList"
            Me.inventoryList.Size = New System.Drawing.Size(10, 10)
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
            Me.columnHeader8.Text = "Ant"
            Me.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.columnHeader8.Width = 35
            '
            'columnHeader2
            '
            Me.columnHeader2.Text = "Ct"
            Me.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.columnHeader2.Width = 32
            '
            'columnHeader3
            '
            Me.columnHeader3.Text = "RSSI"
            Me.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.columnHeader3.Width = 36
            '
            'columnHeader4
            '
            Me.columnHeader4.Text = "PC Bits"
            Me.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.columnHeader4.Width = 47
            '
            'columnHeader5
            '
            Me.columnHeader5.Text = "Memory Bank Data"
            Me.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.columnHeader5.Width = 185
            '
            'columnHeader6
            '
            Me.columnHeader6.Text = "MB"
            Me.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.columnHeader6.Width = 75
            '
            'columnHeader7
            '
            Me.columnHeader7.Text = "Offset"
            Me.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.columnHeader7.Width = 46
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
            Me.memBank_CB.Location = New System.Drawing.Point(212, 15)
            Me.memBank_CB.Name = "memBank_CB"
            Me.memBank_CB.Size = New System.Drawing.Size(10, 20)
            Me.memBank_CB.TabIndex = 2
            '
            'readButton
            '
            Me.readButton.Enabled = False
            Me.readButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular)
            Me.readButton.Location = New System.Drawing.Point(212, 15)
            Me.readButton.Name = "readButton"
            Me.readButton.Size = New System.Drawing.Size(75, 20)
            Me.readButton.TabIndex = 1
            Me.readButton.Text = "Comenzar"
            '
            'functionCallStatusLabel
            '
            Me.functionCallStatusLabel.Caption = "API Call Result"
            Me.functionCallStatusLabel.Text = "Function Success"
            '
            'Label_inconsistencia
            '
            Me.Label_inconsistencia.Location = New System.Drawing.Point(166, 54)
            Me.Label_inconsistencia.Name = "Label_inconsistencia"
            Me.Label_inconsistencia.Size = New System.Drawing.Size(100, 153)
            Me.Label_inconsistencia.Text = "Inconsistencias"
            '
            'Label_leida
            '
            Me.Label_leida.BackColor = System.Drawing.Color.MidnightBlue
            Me.Label_leida.ForeColor = System.Drawing.SystemColors.ControlLight
            Me.Label_leida.Location = New System.Drawing.Point(3, 54)
            Me.Label_leida.Name = "Label_leida"
            Me.Label_leida.Size = New System.Drawing.Size(100, 153)
            Me.Label_leida.Text = "Cajas Leidas"
            '
            'dbug_lbl
            '
            Me.dbug_lbl.Location = New System.Drawing.Point(3, 207)
            Me.dbug_lbl.Name = "dbug_lbl"
            Me.dbug_lbl.Size = New System.Drawing.Size(263, 30)
            Me.dbug_lbl.Text = "Label1"
            '
            'PictureBox1
            '
            Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
            Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(114, 51)
            Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            '
            'AppForm
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
            Me.AutoScroll = True
            Me.ClientSize = New System.Drawing.Size(320, 320)
            Me.Controls.Add(Me.PictureBox1)
            Me.Controls.Add(Me.Label_leida)
            Me.Controls.Add(Me.Label_inconsistencia)
            Me.Controls.Add(Me.dbug_lbl)
            Me.Controls.Add(Me.readButton)
            Me.Controls.Add(Me.memBank_CB)
            Me.Controls.Add(Me.inventoryList)
            Me.Menu = Me.appFormMenu
            Me.MinimizeBox = False
            Me.Name = "AppForm"
            Me.Text = "Despacho Cajas"
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

        Private Sub memBank_CB_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles memBank_CB.SelectedIndexChanged
            If (Not Me.m_ReaderAPI Is Nothing) Then
                Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.DeleteAll()
                If (Me.memBank_CB.SelectedIndex > 0) Then
                    Dim op As New TagAccess.Sequence.Operation()
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

                    If tagData(nIndex).OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE OrElse (tagData(nIndex).OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ AndAlso tagData(nIndex).OpStatus = ACCESS_OPERATION_STATUS.ACCESS_SUCCESS) Then
                        Dim tag As Symbol.RFID3.TagData = tagData(nIndex)
                        Dim tagID As String = tag.TagID
                        Dim isFound As Boolean = False

                        SyncLock m_TagTable.SyncRoot
                            isFound = m_TagTable.ContainsKey(tagID)
                            If Not isFound Then
                                tagID += tag.MemoryBank.ToString()
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
                            ' Comparar Codigo RFID con base de datos
                            comprobar_RFID(item.Text)
                            SyncLock m_TagTable.SyncRoot
                                m_TagTable.Add(tagID, item)
                            End SyncLock
                        End If
                    End If
                Next
                ' 
                ' * Update front panel Tag Count while changing inventory list 
                ' 

                'Me.totalTagValueLabel.Text = (m_TagTable.Count.ToString() & "(") + m_TagTotalCount.ToString() & ")"
            End If
        End Sub

        Private Sub myUpdateStatus(ByVal eventData As StatusEventData)

            Select Case eventData.StatusEventType
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_START_EVENT
                    readButton.Text = "Parar"
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT
                    readButton.Text = "Comenzar"

                    If Me.m_DurationTriggerTime > 0 Then
                        Dim triggerDuration As TimeSpan = TimeSpan.FromMilliseconds(m_DurationTriggerTime)
                        'readTimeValueLabel.Text = (triggerDuration.Seconds & ".") + triggerDuration.Milliseconds / 100 & " Sec"
                    End If
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_START_EVENT
                    readButton.Text = "Parar"
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.ACCESS_STOP_EVENT
                    readButton.Text = "Comenzar"
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
                    Me.Connect("Disconnect")
                    Exit Select
                Case Symbol.RFID3.Events.STATUS_EVENT_TYPE.READER_EXCEPTION_EVENT
                    notifyUser(eventData.ReaderExceptionEventData.ReaderExceptionEventInfo, "Reader Exception")
                    Exit Select
                Case Else
                    Exit Select
            End Select

        End Sub

        Friend Sub notifyUser(ByVal notificationMessage As String, ByVal notificationSource As String)
            Dim result As DialogResult = MessageBox.Show(notificationMessage, notificationSource)
        End Sub

        Private Sub readButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readButton.Click
            Try
                If Me.m_IsConnected Then
                    If (Me.readButton.Text = "Comenzar") Then
                        bander_inconsistencia = 0
                        readButton.Focus()
                        Me.StartReading()
                        readButton.Focus()
                    ElseIf (Me.readButton.Text = "Parar") Then
                        bander_inconsistencia = 0
                        Label_leida.Text = "Cajas Leidas "
                        readButton.Focus()
                        Me.StopReading()
                        readButton.Focus()
                    End If
                Else
                    Me.notifyUser("Conectar a un lector", "Read Operation")
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

        Private Sub rebootMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Me.m_ReaderMgmt.Restart()
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
                End If
            Catch ex As Exception
                Me.notifyUser(ex.Message, "Reset")
            End Try
        End Sub

        Friend Sub RunAccessOps(ByVal opCommand As ACCESS_OPERATION_CODE)
            ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Me.startAccessCallback), opCommand)
        End Sub

        Private Sub sampleAppResultItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Private Sub startAccessCallback(ByVal opCommand As Object)
            Dim result As String = RFIDResults.RFID_API_SUCCESS.ToString
            Try
                Me.m_AccessOpResult.m_OpCode = DirectCast(opCommand, ACCESS_OPERATION_CODE)
                Me.m_AccessOpResult.m_Result = "Access Succeeded"
                Me.m_AccessOpResult.m_AccessCompleteEvent.Reset()
                If (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReadTag = Me.m_ReaderAPI.Actions.TagAccess.ReadWait(Me.m_SelectedTagID, Me.m_ReadForm.m_ReadParams, Nothing)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.ReadEvent(Me.m_ReadForm.m_ReadParams, Nothing, Nothing)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.WriteWait(Me.m_SelectedTagID, Me.m_WriteForm.m_WriteParams, Nothing)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.WriteEvent(Me.m_WriteForm.m_WriteParams, Nothing, Nothing)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.LockWait(Me.m_SelectedTagID, Me.m_LockForm.m_LockParams, Nothing)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.LockEvent(Me.m_LockForm.m_LockParams, Nothing, Nothing)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.KillWait(Me.m_SelectedTagID, Me.m_KillForm.m_KillParams, Nothing)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.KillEvent(Me.m_KillForm.m_KillParams, Nothing, Nothing)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.BlockWriteWait(Me.m_SelectedTagID, Me.m_BlockWriteForm.m_WriteParams, Nothing)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.BlockWriteEvent(Me.m_BlockWriteForm.m_WriteParams, Nothing, Nothing)
                    End If
                ElseIf (DirectCast(opCommand, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.BlockEraseWait(Me.m_SelectedTagID, Me.m_BlockEraseForm.m_BlockEraseParams, Nothing)
                    Else
                        Me.m_ReaderAPI.Actions.TagAccess.BlockEraseEvent(Me.m_BlockEraseForm.m_BlockEraseParams, Nothing, Nothing)
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
                    Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.PerformSequence()
                Else
                    Me.inventoryList.Items.Clear()
                    Me.m_TagTable.Clear()
                    Me.m_TagTotalCount = 0
                    Me.m_ReaderAPI.Actions.Inventory.Perform()
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
                ' Me.readTimeValueLabel.Text = String.Concat(New Object() {duration.Seconds, ".", (duration.Milliseconds / 100), " Sec"})
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

        Private Sub tagDataMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tagDataMenuItem.Click
            Dim tagDataForm As TagDataForm
            tagDataForm = New TagDataForm(Me)
            tagDataForm.ShowDialog()
        End Sub

        Private Sub updateStatus(ByVal result As String)
            If result = "Access Succeeded" Then
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
                                    newItem.SubItems.Add(m_ReadTag.AntennaID.ToString())
                                    newItem.SubItems.Add(m_ReadTag.TagSeenCount.ToString())
                                    m_TagTotalCount += m_ReadTag.TagSeenCount

                                    newItem.SubItems.Add(m_ReadTag.PeakRSSI.ToString())
                                    newItem.SubItems.Add(m_ReadTag.PC.ToString("X"))
                                    newItem.SubItems.Add(m_ReadTag.MemoryBankData)

                                    Dim memoryBank As String = m_ReadTag.MemoryBank.ToString()
                                    Dim index As Integer = memoryBank.LastIndexOf("_"c)
                                    If index <> -1 Then
                                        memoryBank = memoryBank.Substring(index + 1)
                                    End If
                                    newItem.SubItems.Add(memoryBank)
                                    newItem.SubItems.Add(m_ReadTag.MemoryBankDataOffset.ToString())

                                    inventoryList.BeginUpdate()
                                    inventoryList.Items.Add(newItem)
                                    inventoryList.EndUpdate()

                                    SyncLock m_TagTable.SyncRoot
                                        m_TagTable.Add(tagID, newItem)
                                    End SyncLock
                                Else
                                    item.SubItems(5).Text = m_ReadTag.MemoryBankData
                                    item.SubItems(7).Text = m_ReadTag.MemoryBankDataOffset.ToString()
                                End If
                            Else
                                ' Empty Memory Bank Slot 
                                item.SubItems(5).Text = m_ReadTag.MemoryBankData

                                Dim memoryBank As String = m_ReadForm.m_ReadParams.MemoryBank.ToString()
                                Dim index As Integer = memoryBank.LastIndexOf("_"c)
                                If index <> -1 Then
                                    memoryBank = memoryBank.Substring(index + 1)
                                End If
                                item.SubItems(6).Text = memoryBank
                                item.SubItems(7).Text = m_ReadTag.MemoryBankDataOffset.ToString()

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
                    End If
                    notifyUser("Read Succeed", "Access Operation")
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE Then
                    m_WriteForm.writeButton.Enabled = True
                    notifyUser("Write Succeed", "Access Operation")
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK Then
                    m_LockForm.lockButton.Enabled = True
                    notifyUser("Lock Succeed", "Access Operation")
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL Then
                    m_KillForm.killButton.Enabled = True
                    notifyUser("Kill Succeed", "Access Operation")
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_WRITE Then
                    m_BlockWriteForm.writeButton.Enabled = True
                    notifyUser("Block Write Succeed", "Access Operation")
                ElseIf m_AccessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE Then
                    m_BlockEraseForm.eraseButton.Enabled = True
                    notifyUser("Block Erase Succeed", "Access Operation")
                End If
            Else
                notifyUser(m_AccessOpResult.m_Result, "Access Operation")
            End If
            resetButtonState()
            Me.readButton.Enabled = True
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
        Private Sub comprobar_RFID(ByVal parametroConsulta As String)
            id_caja = ws.ComprobarRFID(parametroConsulta)
            'dbug_lbl.Text = id_caja.ToString + "ya entro " + parametroConsulta
            If id_caja <> 0 Then
                Dim caja_pertenece_sede As Integer
                ' Si la caja pertenece a la Sede toma el valor de 1,
                'Si no pertenece toma el valor de cero
                caja_pertenece_sede = ws.Caja_pertenezca_sede(id_caja, id_sede)
                'dbug_lbl.Text = id_caja.ToString + "Caja Pertenece"
                If caja_pertenece_sede <> 0 Then
                    Dim caja_completa As Integer
                    caja_completa = ws.comprobar_caja_completa(id_caja)
                    'dbug_lbl.Text = id_caja.ToString + "Caja Completa"
                    If caja_completa = 1 Then
                        ws.actualizar_caja(id_caja)
                        Label_leida.Text += "                              "
                        Label_leida.Text += id_caja.ToString + ","
                        'Dim item As New ListViewItem(id_caja.ToString)
                        'GridCajasEncontradas.Items.Add(item)
                    Else
                        Me.notifyUser("Hay una caja Incompleta", "Lectura")
                        'dbug_lbl.Show()
                        'dbug_lbl.Text = "Hay una  caja que no esta completa"
                    End If

                Else
                    ws.actualizar_caja_inconsistente(id_caja, id_sede)
                    Label_inconsistencia.Show()
                    If bander_inconsistencia = 0 Then
                        Label_inconsistencia.Text = " Cajas con inconsistencias "
                    End If
                    Label_inconsistencia.Text = "                         "
                    Label_inconsistencia.Text += id_caja.ToString + ","
                    Me.notifyUser("Hay una caja Inconsistente", "Lectura")
                End If
            End If
        End Sub

        ' Fields
        Private accessMenuItem As MenuItem
        Private appFormMenu As MainMenu
        Private WithEvents blockEraseContextMenuItem As MenuItem
        Private WithEvents blockEraseMenuItem As MenuItem
        Private WithEvents blockWriteContextMenuItem As MenuItem
        Private WithEvents blockWriteMenuItem As MenuItem
        Private WithEvents capMenuItem As MenuItem
        Private WithEvents clearReportContextMenuItem As MenuItem
        Private columnHeader1 As ColumnHeader
        Private columnHeader2 As ColumnHeader
        Private columnHeader3 As ColumnHeader
        Private columnHeader4 As ColumnHeader
        Private columnHeader5 As ColumnHeader
        Private columnHeader6 As ColumnHeader
        Private columnHeader7 As ColumnHeader
        Private columnHeader8 As ColumnHeader
        Private components As IContainer = Nothing
        Private configMenuItem As MenuItem
        Private WithEvents connectionMenuItem As MenuItem
        Friend dataContextMenu As ContextMenu
        Private WithEvents exitMenuItem As MenuItem
        Friend functionCallStatusLabel As Notification
        Private WithEvents helpMenuItem As MenuItem
        Friend WithEvents inventoryList As ListView
        Private WithEvents killContextMenuItem As MenuItem
        Private WithEvents killMenuItem As MenuItem
        Private WithEvents lockContextMenuItem As MenuItem
        Private WithEvents lockMenuItem As MenuItem
        Friend m_AccessOpResult As AccessOperationResult
        Friend m_BlockEraseForm As BlockEraseForm
        Friend m_BlockWriteForm As WriteForm
        Friend m_CapabilitiesForm As CapabilitiesForm
        Friend m_ConnectionForm As ConnectionForm
        Friend m_DurationTriggerTime As UInt32 = 0
        Friend m_IsConnected As Boolean
        Friend m_KillForm As KillForm
        Friend m_LockForm As LockForm
        Friend m_ReaderAPI As RFIDReader
        Friend m_ReaderMgmt As ReaderManagement
        Friend m_ReadForm As ReadForm
        Private m_ReadTag As TagData
        Friend m_SelectedTagID As String
        Private m_StartTime As TimeSpan
        Private m_TagTable As Hashtable
        Private m_TagTotalCount As UInt32
        Private m_UpdateReadHandler As UpdateRead = Nothing
        Private m_UpdateStatusHandler As updateStatusResult = Nothing
        Friend m_WriteForm As WriteForm
        Private WithEvents memBank_CB As ComboBox
        Private menuItem1 As MenuItem
        Private menuItem3 As MenuItem
        Private menuItem31 As MenuItem
        Private operationMenuItem As MenuItem
        Private WithEvents readButton As Button
        Private WithEvents readContextMenuItem As MenuItem
        Private WithEvents readMenuItem As MenuItem
        Private WithEvents ResetMenuItem As MenuItem
        Private sampleAppMenuItem As MenuItem
        Private WithEvents tagDataMenuItem As MenuItem
        Private WithEvents writeContextMenuItem As MenuItem
        Friend WithEvents Label_inconsistencia As System.Windows.Forms.Label
        Friend WithEvents Label_leida As System.Windows.Forms.Label
        Friend WithEvents dbug_lbl As System.Windows.Forms.Label
        Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
        Private WithEvents writeMenuItem As MenuItem

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

