Imports Symbol.RFID3
Imports Symbol.RFID3.Events
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Windows.Forms
Imports System.Windows.Forms.ListViewItem
Imports System.Data.OracleClient
Imports System.Data
Imports Microsoft.VisualBasic


Namespace VB_RFID3_Host_Sample1
    Public Class AppForm
        Inherits Form
        ' Methods
        Public Sub New()
            Me.InitializeComponent()
            Me.m_ReadTag = New TagData
            Me.m_UpdateStatusHandler = New UpdateStatus(AddressOf Me.myUpdateStatus)
            Me.m_UpdateReadHandler = New UpdateRead(AddressOf Me.myUpdateRead)
            Me.m_ConnectionForm = New ConnectionForm(Me)
            Me.m_ReadForm = New ReadForm(Me)
            Me.m_TagTable = New Hashtable
            Me.m_AccessOpResult = New AccessOperationResult
            Me.m_IsConnected = False
            Me.m_TagTotalCount = 0
        End Sub

        Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim helpDialog As New HelpForm(Me)
            If (helpDialog.ShowDialog(Me) = DialogResult.Yes) Then
            End If
            helpDialog.Dispose()
        End Sub

        Private Sub accessBackgroundWorker_DoWork(ByVal sender As Object, ByVal accessEvent As DoWorkEventArgs) Handles accessBackgroundWorker.DoWork
            Try
                Me.m_AccessOpResult.m_OpCode = DirectCast(accessEvent.Argument, ACCESS_OPERATION_CODE)
                Me.m_AccessOpResult.m_Result = RFIDResults.RFID_API_SUCCESS
                If (DirectCast(accessEvent.Argument, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReadTag = Me.m_ReaderAPI.Actions.TagAccess.ReadWait(Me.m_SelectedTagID, Me.m_ReadForm.m_ReadParams, Nothing)
                    Else
                        Me.functionCallStatusLabel.Text = "Enter Tag-Id"
                    End If
                ElseIf (DirectCast(accessEvent.Argument, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.WriteWait(Me.m_SelectedTagID, Me.m_WriteForm.m_WriteParams, Nothing)
                    Else
                        Me.functionCallStatusLabel.Text = "Enter Tag-Id"
                    End If
                ElseIf (DirectCast(accessEvent.Argument, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.LockWait(Me.m_SelectedTagID, Me.m_LockForm.m_LockParams, Nothing)
                    Else
                        Me.functionCallStatusLabel.Text = "Enter Tag-Id"
                    End If
                ElseIf (DirectCast(accessEvent.Argument, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.KillWait(Me.m_SelectedTagID, Me.m_KillForm.m_KillParams, Nothing)
                    End If
                ElseIf (DirectCast(accessEvent.Argument, ACCESS_OPERATION_CODE) = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE) Then
                    If (Me.m_SelectedTagID <> String.Empty) Then
                        Me.m_ReaderAPI.Actions.TagAccess.BlockEraseWait(Me.m_SelectedTagID, Me.m_BlockEraseForm.m_BlockEraseParams, Nothing)
                    Else
                        Me.functionCallStatusLabel.Text = "Enter Tag-Id"
                    End If
                End If
            Catch ofe As OperationFailureException
                Me.m_AccessOpResult.m_Result = ofe.Result
            End Try
            accessEvent.Result = Me.m_AccessOpResult
        End Sub

        Private Sub accessBackgroundWorker_ProgressChanged(ByVal sender As Object, ByVal pce As ProgressChangedEventArgs) Handles accessBackgroundWorker.ProgressChanged
        End Sub

        Private Sub accessBackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal accessEvents As RunWorkerCompletedEventArgs) Handles accessBackgroundWorker.RunWorkerCompleted
            Dim index As Integer = 0
            If accessEvents.[Error] IsNot Nothing Then
                functionCallStatusLabel.Text = accessEvents.[Error].Message
            Else
                ' Handle AccessWait Operations 
                Dim accessOpResult As AccessOperationResult = DirectCast(accessEvents.Result, AccessOperationResult)
                If accessOpResult.m_Result = RFIDResults.RFID_API_SUCCESS Then
                    If accessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ Then
                        If inventoryList.SelectedItems.Count > 0 Then
                            Dim item As ListViewItem = inventoryList.SelectedItems(0)
                            Dim tagID As String = m_ReadTag.TagID + m_ReadTag.MemoryBank.ToString() + m_ReadTag.MemoryBankDataOffset.ToString()

                            If item.SubItems(5).Text.Length > 0 Then
                                Dim isFound As Boolean = False

                                ' Search or add new one 
                                SyncLock m_TagTable.SyncRoot
                                    isFound = m_TagTable.ContainsKey(tagID)
                                End SyncLock

                                If Not isFound Then
                                    Dim newItem As New ListViewItem(m_ReadTag.TagID)
                                    Dim memoryBank As String = m_ReadTag.MemoryBank.ToString()
                                    index = memoryBank.LastIndexOf("_"c)
                                    If index <> -1 Then
                                        memoryBank = memoryBank.Substring(index + 1)
                                    End If

                                    inventoryList.BeginUpdate()
                                    inventoryList.Items.Add(newItem)
                                    inventoryList.EndUpdate()

                                    SyncLock m_TagTable.SyncRoot
                                        m_TagTable.Add(tagID, newItem)
                                    End SyncLock
                                End If
                            Else
                                ' Empty Memory Bank Slot 
                                item.SubItems(5).Text = m_ReadTag.MemoryBankData

                                Dim memoryBank As String = m_ReadForm.m_ReadParams.MemoryBank.ToString()
                                index = memoryBank.LastIndexOf("_"c)
                                If index <> -1 Then
                                    memoryBank = memoryBank.Substring(index + 1)
                                End If

                                SyncLock m_TagTable.SyncRoot
                                    If m_ReadTag.TagID IsNot Nothing Then
                                        m_TagTable.Remove(m_ReadTag.TagID)
                                    End If
                                    m_TagTable.Add(tagID, item)
                                End SyncLock
                            End If
                            Me.m_ReadForm.ReadData_TB.Text = m_ReadTag.MemoryBankData
                            functionCallStatusLabel.Text = "Read Succeed"
                        End If
                    ElseIf accessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_WRITE Then
                        functionCallStatusLabel.Text = "Write Succeed"
                    ElseIf accessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_LOCK Then
                        functionCallStatusLabel.Text = "Lock Succeed"
                    ElseIf accessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_KILL Then
                        functionCallStatusLabel.Text = "Kill Succeed"
                    ElseIf accessOpResult.m_OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_BLOCK_ERASE Then
                        functionCallStatusLabel.Text = "BlockErase Succeed"
                    End If
                Else
                    functionCallStatusLabel.Text = accessOpResult.m_Result.ToString()
                End If
                resetButtonState()
            End If
        End Sub

        Private Sub AppForm_ClientSizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.ClientSizeChanged
            Me.functionCallStatusLabel.Width = (MyBase.Width - &H4D)
        End Sub

        Private Sub AppForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
            Try
                If Me.m_IsConnected Then
                    Me.m_ReaderAPI.Disconnect()
                End If
                MyBase.Dispose()
            Catch ex As Exception
                Me.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub


        Private Sub blockEraseDataContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles blockEraseDataContextMenuItem.Click
            If (Nothing Is Me.m_BlockEraseForm) Then
                Me.m_BlockEraseForm = New BlockEraseForm(Me)
            End If
            Me.m_BlockEraseForm.ShowDialog(Me)
        End Sub

        Private Sub blockEraseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Nothing Is Me.m_BlockEraseForm) Then
                Me.m_BlockEraseForm = New BlockEraseForm(Me)
            End If
            Me.m_BlockEraseForm.ShowDialog(Me)
        End Sub

        Private Sub blockWriteDataContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles blockWriteDataContextMenuItem.Click
            If (Nothing Is Me.m_WriteForm) Then
                Me.m_WriteForm = New WriteForm(Me, True)
            End If
            Me.m_WriteForm.ShowDialog(Me)
        End Sub

        Private Sub blockWriteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Nothing Is Me.m_WriteForm) Then
                Me.m_WriteForm = New WriteForm(Me, True)
            End If
            Me.m_WriteForm.ShowDialog(Me)
        End Sub

        Private Sub capabilitiesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim capabilitiesForm As CapabilitiesForm
            capabilitiesForm = New CapabilitiesForm(Me)
            capabilitiesForm.ShowDialog(Me)
        End Sub

        Private Sub clearReports_CB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles clearReports_CB.CheckedChanged
            Me.totalTagValueLabel.Text = "0(0)"
            Me.inventoryList.Items.Clear()
            Me.verificadosList.Items.Clear()
            Me.m_TagTable.Clear()
            Me.clearReports_CB.Checked = False
        End Sub

        Private Sub clearReportsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.inventoryList.Items.Clear()
            Me.m_TagTable.Clear()
        End Sub

        Private Sub connectBackgroundWorker_DoWork(ByVal sender As Object, ByVal workEventArgs As DoWorkEventArgs) Handles connectBackgroundWorker.DoWork
            Me.connectBackgroundWorker.ReportProgress(0, workEventArgs.Argument)
            If (CStr(workEventArgs.Argument) = "Connect") Then
                Me.m_ReaderAPI = New RFIDReader(Me.m_ConnectionForm.IpText, UInt32.Parse(Me.m_ConnectionForm.PortText), 0)
                Try
                    Me.m_ReaderAPI.Connect()
                    Me.m_IsConnected = True
                    workEventArgs.Result = "Connect Succeed"
                Catch operationException As OperationFailureException
                    workEventArgs.Result = operationException.Result
                Catch ex As Exception
                    workEventArgs.Result = ex.Message
                End Try
            ElseIf (CStr(workEventArgs.Argument) = "Disconnect") Then
                Try
                    Me.m_ReaderAPI.Disconnect()
                    Me.m_IsConnected = False
                    workEventArgs.Result = "Disconnect Succeed"
                Catch ofe As OperationFailureException
                    workEventArgs.Result = ofe.Result
                End Try
            End If
        End Sub

        Private Sub connectBackgroundWorker_ProgressChanged(ByVal sender As Object, ByVal progressEventArgs As ProgressChangedEventArgs) Handles connectBackgroundWorker.ProgressChanged
            Me.m_ConnectionForm.connectionButton.Enabled = False
        End Sub

        Private Sub connectBackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal connectEventArgs As RunWorkerCompletedEventArgs) Handles connectBackgroundWorker.RunWorkerCompleted
            If (Me.m_ConnectionForm.connectionButton.Text = "Connect") Then
                If (connectEventArgs.Result.ToString = "Connect Succeed") Then
                    Me.m_ConnectionForm.connectionButton.Text = "Disconnect"
                    Me.m_ConnectionForm.hostname_TB.Enabled = False
                    Me.m_ConnectionForm.port_TB.Enabled = False
                    Me.m_ConnectionForm.Close()
                    Me.readButton.Enabled = True
                    AddHandler Me.m_ReaderAPI.Events.ReadNotify, New ReadNotifyHandler(AddressOf Me.Events_ReadNotify)
                    Me.m_ReaderAPI.Events.AttachTagDataWithReadEvent = False
                    AddHandler Me.m_ReaderAPI.Events.StatusNotify, New StatusNotifyHandler(AddressOf Me.Events_StatusNotify)
                    Me.m_ReaderAPI.Events.NotifyGPIEvent = True
                    Me.m_ReaderAPI.Events.NotifyReaderDisconnectEvent = True
                    Me.m_ReaderAPI.Events.NotifyAccessStartEvent = True
                    Me.m_ReaderAPI.Events.NotifyAccessStopEvent = True
                    Me.m_ReaderAPI.Events.NotifyInventoryStartEvent = True
                    Me.m_ReaderAPI.Events.NotifyInventoryStopEvent = True
                    Me.Text = ("Conectado a " & Me.m_ConnectionForm.IpText)
                    Me.connectionStatus.BackgroundImage = My.Resources.connected
                End If
            ElseIf ((Me.m_ConnectionForm.connectionButton.Text = "Disconnect") AndAlso (connectEventArgs.Result.ToString = "Disconnect Succeed")) Then
                Me.Text = "TSE2018"
                Me.connectionStatus.BackgroundImage = My.Resources.disconnected
                Me.m_ConnectionForm.connectionButton.Text = "Connect"
                Me.m_ConnectionForm.hostname_TB.Enabled = True
                Me.m_ConnectionForm.port_TB.Enabled = True
                Me.readButton.Enabled = False
            End If
            Me.functionCallStatusLabel.Text = connectEventArgs.Result.ToString
            Me.m_ConnectionForm.connectionButton.Enabled = True
        End Sub

        Private Sub connectionToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles connectionToolStripMenuItem.Click
            Me.m_ConnectionForm.ShowDialog(Me)
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub Events_ReadNotify(ByVal sender As Object, ByVal readEventArgs As ReadEventArgs)
            Try
                MyBase.Invoke(Me.m_UpdateReadHandler, New Object() {Nothing})
            Catch exception1 As Exception
            End Try
        End Sub

        Public Sub Events_StatusNotify(ByVal sender As Object, ByVal statusEventArgs As StatusEventArgs)
            Try
                MyBase.Invoke(Me.m_UpdateStatusHandler, New Object() {statusEventArgs.StatusEventData})
            Catch exception1 As Exception
            End Try
        End Sub

        Private Sub exitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exitToolStripMenuItem.Click
            If Me.m_IsConnected Then
                Me.m_ReaderAPI.Disconnect()
            End If
            MyBase.Dispose()
        End Sub

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AppForm))
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Me.mainMenuStrip = New System.Windows.Forms.MenuStrip
            Me.configToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.connectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
            Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.inventoryList = New System.Windows.Forms.ListView
            Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
            Me.gpiLabel = New System.Windows.Forms.Label
            Me.readButton = New System.Windows.Forms.Button
            Me.label2 = New System.Windows.Forms.Label
            Me.label3 = New System.Windows.Forms.Label
            Me.label4 = New System.Windows.Forms.Label
            Me.label5 = New System.Windows.Forms.Label
            Me.label6 = New System.Windows.Forms.Label
            Me.label7 = New System.Windows.Forms.Label
            Me.label8 = New System.Windows.Forms.Label
            Me.label9 = New System.Windows.Forms.Label
            Me.statusStrip = New System.Windows.Forms.StatusStrip
            Me.functionCallStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
            Me.connectionStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
            Me.connectionStatus = New System.Windows.Forms.ToolStripStatusLabel
            Me.gpiStateGB = New System.Windows.Forms.GroupBox
            Me.gpiNumberLabel = New System.Windows.Forms.Label
            Me.transmitPowerGB = New System.Windows.Forms.GroupBox
            Me.label12 = New System.Windows.Forms.Label
            Me.label10 = New System.Windows.Forms.Label
            Me.label11 = New System.Windows.Forms.Label
            Me.hScrollBar1 = New System.Windows.Forms.HScrollBar
            Me.dataContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tagDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.readDataContextMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.writeDataContextMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.lockDataContextMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.killDataContextMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.blockWriteDataContextMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.blockEraseDataContextMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.connectBackgroundWorker = New System.ComponentModel.BackgroundWorker
            Me.accessBackgroundWorker = New System.ComponentModel.BackgroundWorker
            Me.totalTagValueLabel = New System.Windows.Forms.Label
            Me.totalTagLabel = New System.Windows.Forms.Label
            Me.clearReports_CB = New System.Windows.Forms.CheckBox
            Me.verificadosList = New System.Windows.Forms.ListView
            Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
            Me.Label1 = New System.Windows.Forms.Label
            Me.txt_tags_encontrados = New System.Windows.Forms.Label
            Me.DataGViewFaltantes = New System.Windows.Forms.DataGridView
            Me.DataGrid_no_encontrados = New System.Windows.Forms.DataGridView
            Me.PictureBox1 = New System.Windows.Forms.PictureBox
            Me.TextBox1 = New System.Windows.Forms.TextBox
            Me.Label13 = New System.Windows.Forms.Label
            Me.DataGridENCONTRADOS = New System.Windows.Forms.DataGridView
            Me.DataGridView1 = New System.Windows.Forms.DataGridView
            Me.Label14 = New System.Windows.Forms.Label
            Me.mainMenuStrip.SuspendLayout()
            Me.statusStrip.SuspendLayout()
            Me.dataContextMenuStrip.SuspendLayout()
            CType(Me.DataGViewFaltantes, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataGrid_no_encontrados, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataGridENCONTRADOS, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'mainMenuStrip
            '
            Me.mainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.configToolStripMenuItem})
            Me.mainMenuStrip.Location = New System.Drawing.Point(0, 0)
            Me.mainMenuStrip.Name = "mainMenuStrip"
            Me.mainMenuStrip.Size = New System.Drawing.Size(1235, 24)
            Me.mainMenuStrip.TabIndex = 0
            '
            'configToolStripMenuItem
            '
            Me.configToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.connectionToolStripMenuItem, Me.toolStripSeparator2, Me.exitToolStripMenuItem})
            Me.configToolStripMenuItem.Name = "configToolStripMenuItem"
            Me.configToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
            Me.configToolStripMenuItem.Text = "Configuracion"
            '
            'connectionToolStripMenuItem
            '
            Me.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem"
            Me.connectionToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
            Me.connectionToolStripMenuItem.Text = "conectar"
            '
            'toolStripSeparator2
            '
            Me.toolStripSeparator2.Name = "toolStripSeparator2"
            Me.toolStripSeparator2.Size = New System.Drawing.Size(117, 6)
            '
            'exitToolStripMenuItem
            '
            Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
            Me.exitToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
            Me.exitToolStripMenuItem.Text = "salir"
            '
            'inventoryList
            '
            Me.inventoryList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.inventoryList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1})
            Me.inventoryList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.inventoryList.FullRowSelect = True
            Me.inventoryList.GridLines = True
            Me.inventoryList.HideSelection = False
            Me.inventoryList.Location = New System.Drawing.Point(362, 444)
            Me.inventoryList.MultiSelect = False
            Me.inventoryList.Name = "inventoryList"
            Me.inventoryList.Size = New System.Drawing.Size(10, 0)
            Me.inventoryList.TabIndex = 1
            Me.inventoryList.UseCompatibleStateImageBehavior = False
            Me.inventoryList.View = System.Windows.Forms.View.Details
            '
            'columnHeader1
            '
            Me.columnHeader1.Text = "Tags Leidos"
            Me.columnHeader1.Width = 292
            '
            'gpiLabel
            '
            Me.gpiLabel.Location = New System.Drawing.Point(0, 0)
            Me.gpiLabel.Name = "gpiLabel"
            Me.gpiLabel.Size = New System.Drawing.Size(100, 23)
            Me.gpiLabel.TabIndex = 0
            '
            'readButton
            '
            Me.readButton.Enabled = False
            Me.readButton.Location = New System.Drawing.Point(126, 323)
            Me.readButton.Name = "readButton"
            Me.readButton.Size = New System.Drawing.Size(127, 23)
            Me.readButton.TabIndex = 2
            Me.readButton.Text = "Empezar"
            Me.readButton.UseVisualStyleBackColor = True
            '
            'label2
            '
            Me.label2.Location = New System.Drawing.Point(0, 0)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(100, 23)
            Me.label2.TabIndex = 0
            '
            'label3
            '
            Me.label3.Location = New System.Drawing.Point(0, 0)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(100, 23)
            Me.label3.TabIndex = 0
            '
            'label4
            '
            Me.label4.Location = New System.Drawing.Point(0, 0)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(100, 23)
            Me.label4.TabIndex = 0
            '
            'label5
            '
            Me.label5.Location = New System.Drawing.Point(0, 0)
            Me.label5.Name = "label5"
            Me.label5.Size = New System.Drawing.Size(100, 23)
            Me.label5.TabIndex = 0
            '
            'label6
            '
            Me.label6.Location = New System.Drawing.Point(0, 0)
            Me.label6.Name = "label6"
            Me.label6.Size = New System.Drawing.Size(100, 23)
            Me.label6.TabIndex = 0
            '
            'label7
            '
            Me.label7.Location = New System.Drawing.Point(0, 0)
            Me.label7.Name = "label7"
            Me.label7.Size = New System.Drawing.Size(100, 23)
            Me.label7.TabIndex = 0
            '
            'label8
            '
            Me.label8.Location = New System.Drawing.Point(0, 0)
            Me.label8.Name = "label8"
            Me.label8.Size = New System.Drawing.Size(100, 23)
            Me.label8.TabIndex = 0
            '
            'label9
            '
            Me.label9.Location = New System.Drawing.Point(0, 0)
            Me.label9.Name = "label9"
            Me.label9.Size = New System.Drawing.Size(100, 23)
            Me.label9.TabIndex = 0
            '
            'statusStrip
            '
            Me.statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.functionCallStatusLabel, Me.connectionStatusLabel, Me.connectionStatus})
            Me.statusStrip.Location = New System.Drawing.Point(0, 473)
            Me.statusStrip.Name = "statusStrip"
            Me.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            Me.statusStrip.Size = New System.Drawing.Size(1235, 25)
            Me.statusStrip.TabIndex = 19
            Me.statusStrip.Text = "statusStrip"
            '
            'functionCallStatusLabel
            '
            Me.functionCallStatusLabel.AutoSize = False
            Me.functionCallStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
            Me.functionCallStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
            Me.functionCallStatusLabel.Margin = New System.Windows.Forms.Padding(2, 3, 0, 2)
            Me.functionCallStatusLabel.Name = "functionCallStatusLabel"
            Me.functionCallStatusLabel.Size = New System.Drawing.Size(716, 20)
            Me.functionCallStatusLabel.Text = "Ready"
            Me.functionCallStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'connectionStatusLabel
            '
            Me.connectionStatusLabel.Name = "connectionStatusLabel"
            Me.connectionStatusLabel.Size = New System.Drawing.Size(0, 20)
            '
            'connectionStatus
            '
            Me.connectionStatus.AutoSize = False
            Me.connectionStatus.BackgroundImage = CType(resources.GetObject("connectionStatus.BackgroundImage"), System.Drawing.Image)
            Me.connectionStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.connectionStatus.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                        Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
            Me.connectionStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.connectionStatus.Name = "connectionStatus"
            Me.connectionStatus.Size = New System.Drawing.Size(50, 20)
            Me.connectionStatus.Text = "Disconnected"
            '
            'gpiStateGB
            '
            Me.gpiStateGB.Location = New System.Drawing.Point(0, 0)
            Me.gpiStateGB.Name = "gpiStateGB"
            Me.gpiStateGB.Size = New System.Drawing.Size(200, 100)
            Me.gpiStateGB.TabIndex = 0
            Me.gpiStateGB.TabStop = False
            '
            'gpiNumberLabel
            '
            Me.gpiNumberLabel.Location = New System.Drawing.Point(0, 0)
            Me.gpiNumberLabel.Name = "gpiNumberLabel"
            Me.gpiNumberLabel.Size = New System.Drawing.Size(100, 23)
            Me.gpiNumberLabel.TabIndex = 0
            '
            'transmitPowerGB
            '
            Me.transmitPowerGB.Location = New System.Drawing.Point(0, 0)
            Me.transmitPowerGB.Name = "transmitPowerGB"
            Me.transmitPowerGB.Size = New System.Drawing.Size(200, 100)
            Me.transmitPowerGB.TabIndex = 0
            Me.transmitPowerGB.TabStop = False
            '
            'label12
            '
            Me.label12.Location = New System.Drawing.Point(0, 0)
            Me.label12.Name = "label12"
            Me.label12.Size = New System.Drawing.Size(100, 23)
            Me.label12.TabIndex = 0
            '
            'label10
            '
            Me.label10.Location = New System.Drawing.Point(0, 0)
            Me.label10.Name = "label10"
            Me.label10.Size = New System.Drawing.Size(100, 23)
            Me.label10.TabIndex = 0
            '
            'label11
            '
            Me.label11.Location = New System.Drawing.Point(0, 0)
            Me.label11.Name = "label11"
            Me.label11.Size = New System.Drawing.Size(100, 23)
            Me.label11.TabIndex = 0
            '
            'hScrollBar1
            '
            Me.hScrollBar1.Location = New System.Drawing.Point(13, 16)
            Me.hScrollBar1.Maximum = 2920
            Me.hScrollBar1.Minimum = 1620
            Me.hScrollBar1.Name = "hScrollBar1"
            Me.hScrollBar1.Size = New System.Drawing.Size(314, 19)
            Me.hScrollBar1.TabIndex = 0
            Me.hScrollBar1.Value = 1620
            '
            'dataContextMenuStrip
            '
            Me.dataContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tagDataToolStripMenuItem, Me.readDataContextMenuItem, Me.writeDataContextMenuItem, Me.lockDataContextMenuItem, Me.killDataContextMenuItem, Me.blockWriteDataContextMenuItem, Me.blockEraseDataContextMenuItem})
            Me.dataContextMenuStrip.Name = "dataContextMenuStrip"
            Me.dataContextMenuStrip.Size = New System.Drawing.Size(135, 158)
            '
            'tagDataToolStripMenuItem
            '
            Me.tagDataToolStripMenuItem.Name = "tagDataToolStripMenuItem"
            Me.tagDataToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
            Me.tagDataToolStripMenuItem.Text = "Tag Data"
            '
            'readDataContextMenuItem
            '
            Me.readDataContextMenuItem.Name = "readDataContextMenuItem"
            Me.readDataContextMenuItem.Size = New System.Drawing.Size(134, 22)
            Me.readDataContextMenuItem.Text = "Read"
            '
            'writeDataContextMenuItem
            '
            Me.writeDataContextMenuItem.Name = "writeDataContextMenuItem"
            Me.writeDataContextMenuItem.Size = New System.Drawing.Size(134, 22)
            Me.writeDataContextMenuItem.Text = "Write"
            '
            'lockDataContextMenuItem
            '
            Me.lockDataContextMenuItem.Name = "lockDataContextMenuItem"
            Me.lockDataContextMenuItem.Size = New System.Drawing.Size(134, 22)
            Me.lockDataContextMenuItem.Text = "Lock"
            '
            'killDataContextMenuItem
            '
            Me.killDataContextMenuItem.Name = "killDataContextMenuItem"
            Me.killDataContextMenuItem.Size = New System.Drawing.Size(134, 22)
            Me.killDataContextMenuItem.Text = "Kill"
            '
            'blockWriteDataContextMenuItem
            '
            Me.blockWriteDataContextMenuItem.Name = "blockWriteDataContextMenuItem"
            Me.blockWriteDataContextMenuItem.Size = New System.Drawing.Size(134, 22)
            Me.blockWriteDataContextMenuItem.Text = "Block Write"
            '
            'blockEraseDataContextMenuItem
            '
            Me.blockEraseDataContextMenuItem.Name = "blockEraseDataContextMenuItem"
            Me.blockEraseDataContextMenuItem.Size = New System.Drawing.Size(134, 22)
            Me.blockEraseDataContextMenuItem.Text = "Block Erase"
            '
            'connectBackgroundWorker
            '
            Me.connectBackgroundWorker.WorkerReportsProgress = True
            '
            'accessBackgroundWorker
            '
            Me.accessBackgroundWorker.WorkerReportsProgress = True
            '
            'totalTagValueLabel
            '
            Me.totalTagValueLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
            Me.totalTagValueLabel.Location = New System.Drawing.Point(422, 39)
            Me.totalTagValueLabel.Name = "totalTagValueLabel"
            Me.totalTagValueLabel.Size = New System.Drawing.Size(33, 16)
            Me.totalTagValueLabel.TabIndex = 26
            Me.totalTagValueLabel.Text = "0(0)"
            '
            'totalTagLabel
            '
            Me.totalTagLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
            Me.totalTagLabel.Location = New System.Drawing.Point(354, 39)
            Me.totalTagLabel.Name = "totalTagLabel"
            Me.totalTagLabel.Size = New System.Drawing.Size(62, 16)
            Me.totalTagLabel.TabIndex = 25
            Me.totalTagLabel.Text = "Total Articulos: "
            '
            'clearReports_CB
            '
            Me.clearReports_CB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.clearReports_CB.AutoSize = True
            Me.clearReports_CB.Location = New System.Drawing.Point(1164, 35)
            Me.clearReports_CB.Name = "clearReports_CB"
            Me.clearReports_CB.Size = New System.Drawing.Size(59, 17)
            Me.clearReports_CB.TabIndex = 27
            Me.clearReports_CB.Text = "Limpiar"
            Me.clearReports_CB.UseVisualStyleBackColor = True
            '
            'verificadosList
            '
            Me.verificadosList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.verificadosList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
            Me.verificadosList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.verificadosList.FullRowSelect = True
            Me.verificadosList.GridLines = True
            Me.verificadosList.HideSelection = False
            Me.verificadosList.Location = New System.Drawing.Point(504, 721)
            Me.verificadosList.MultiSelect = False
            Me.verificadosList.Name = "verificadosList"
            Me.verificadosList.Size = New System.Drawing.Size(442, 0)
            Me.verificadosList.TabIndex = 28
            Me.verificadosList.UseCompatibleStateImageBehavior = False
            Me.verificadosList.View = System.Windows.Forms.View.Details
            '
            'ColumnHeader2
            '
            Me.ColumnHeader2.Text = "Tags encontrados"
            Me.ColumnHeader2.Width = 292
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(645, 36)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(110, 13)
            Me.Label1.TabIndex = 29
            Me.Label1.Text = "Articulos Encontrados"
            '
            'txt_tags_encontrados
            '
            Me.txt_tags_encontrados.AutoSize = True
            Me.txt_tags_encontrados.Location = New System.Drawing.Point(872, 36)
            Me.txt_tags_encontrados.Name = "txt_tags_encontrados"
            Me.txt_tags_encontrados.Size = New System.Drawing.Size(13, 13)
            Me.txt_tags_encontrados.TabIndex = 30
            Me.txt_tags_encontrados.Text = "0"
            '
            'DataGViewFaltantes
            '
            Me.DataGViewFaltantes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGViewFaltantes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.DataGViewFaltantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DataGViewFaltantes.DefaultCellStyle = DataGridViewCellStyle2
            Me.DataGViewFaltantes.Location = New System.Drawing.Point(357, 59)
            Me.DataGViewFaltantes.Name = "DataGViewFaltantes"
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGViewFaltantes.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.DataGViewFaltantes.Size = New System.Drawing.Size(236, 395)
            Me.DataGViewFaltantes.TabIndex = 34
            '
            'DataGrid_no_encontrados
            '
            Me.DataGrid_no_encontrados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.DataGrid_no_encontrados.Location = New System.Drawing.Point(374, 444)
            Me.DataGrid_no_encontrados.Name = "DataGrid_no_encontrados"
            Me.DataGrid_no_encontrados.Size = New System.Drawing.Size(10, 10)
            Me.DataGrid_no_encontrados.TabIndex = 37
            '
            'PictureBox1
            '
            Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
            Me.PictureBox1.Location = New System.Drawing.Point(28, 59)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(314, 152)
            Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox1.TabIndex = 38
            Me.PictureBox1.TabStop = False
            '
            'TextBox1
            '
            Me.TextBox1.Location = New System.Drawing.Point(63, 282)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(244, 20)
            Me.TextBox1.TabIndex = 39
            '
            'Label13
            '
            Me.Label13.AutoSize = True
            Me.Label13.Location = New System.Drawing.Point(123, 254)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(126, 13)
            Me.Label13.TabIndex = 40
            Me.Label13.Text = "Codigo de barra de CAJA"
            '
            'DataGridENCONTRADOS
            '
            Me.DataGridENCONTRADOS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGridENCONTRADOS.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
            Me.DataGridENCONTRADOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DataGridENCONTRADOS.DefaultCellStyle = DataGridViewCellStyle5
            Me.DataGridENCONTRADOS.Location = New System.Drawing.Point(648, 59)
            Me.DataGridENCONTRADOS.Name = "DataGridENCONTRADOS"
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGridENCONTRADOS.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
            Me.DataGridENCONTRADOS.Size = New System.Drawing.Size(237, 395)
            Me.DataGridENCONTRADOS.TabIndex = 41
            '
            'DataGridView1
            '
            Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle8
            Me.DataGridView1.Location = New System.Drawing.Point(905, 59)
            Me.DataGridView1.Name = "DataGridView1"
            DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
            Me.DataGridView1.Size = New System.Drawing.Size(237, 395)
            Me.DataGridView1.TabIndex = 43
            '
            'Label14
            '
            Me.Label14.AutoSize = True
            Me.Label14.Location = New System.Drawing.Point(902, 36)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(118, 13)
            Me.Label14.TabIndex = 42
            Me.Label14.Text = "Articulos Inconsistentes"
            '
            'AppForm
            '
            Me.AutoScroll = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ClientSize = New System.Drawing.Size(1235, 498)
            Me.Controls.Add(Me.DataGridView1)
            Me.Controls.Add(Me.Label14)
            Me.Controls.Add(Me.DataGViewFaltantes)
            Me.Controls.Add(Me.Label13)
            Me.Controls.Add(Me.DataGridENCONTRADOS)
            Me.Controls.Add(Me.TextBox1)
            Me.Controls.Add(Me.PictureBox1)
            Me.Controls.Add(Me.clearReports_CB)
            Me.Controls.Add(Me.statusStrip)
            Me.Controls.Add(Me.DataGrid_no_encontrados)
            Me.Controls.Add(Me.txt_tags_encontrados)
            Me.Controls.Add(Me.mainMenuStrip)
            Me.Controls.Add(Me.verificadosList)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.totalTagValueLabel)
            Me.Controls.Add(Me.totalTagLabel)
            Me.Controls.Add(Me.inventoryList)
            Me.Controls.Add(Me.readButton)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.MinimumSize = New System.Drawing.Size(8, 250)
            Me.Name = "AppForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "ESCANEO"
            Me.mainMenuStrip.ResumeLayout(False)
            Me.mainMenuStrip.PerformLayout()
            Me.statusStrip.ResumeLayout(False)
            Me.statusStrip.PerformLayout()
            Me.dataContextMenuStrip.ResumeLayout(False)
            CType(Me.DataGViewFaltantes, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DataGrid_no_encontrados, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DataGridENCONTRADOS, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Private Sub inventoryList_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles inventoryList.MouseClick
            If (e.Button = MouseButtons.Right) Then
                Me.dataContextMenuStrip.Show(Me.inventoryList, New Point(e.X, e.Y))
            End If
        End Sub

        Private Sub killDataContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles killDataContextMenuItem.Click
            If (Nothing Is Me.m_KillForm) Then
                Me.m_KillForm = New KillForm(Me)
            End If
            Me.m_KillForm.ShowDialog(Me)
        End Sub

        Private Sub killToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Nothing Is Me.m_KillForm) Then
                Me.m_KillForm = New KillForm(Me)
            End If
            Me.m_KillForm.ShowDialog(Me)
        End Sub

        Private Sub lockDataContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lockDataContextMenuItem.Click
            If (Nothing Is Me.m_LockForm) Then
                Me.m_LockForm = New LockForm(Me)
            End If
            Me.m_LockForm.ShowDialog(Me)
        End Sub

        Private Sub lockToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Nothing Is Me.m_LockForm) Then
                Me.m_LockForm = New LockForm(Me)
            End If
            Me.m_LockForm.ShowDialog(Me)
        End Sub

        Private Sub myUpdateRead(ByVal eventData As ReadEventData)
            Dim index As Integer = 0
            Dim item As ListViewItem
            Dim tagData As Symbol.RFID3.TagData() = m_ReaderAPI.Actions.GetReadTags(1000)

            If tagData IsNot Nothing Then
                For nIndex As Integer = 0 To tagData.Length - 1
                    If tagData(nIndex).OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_NONE OrElse (tagData(nIndex).OpCode = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ AndAlso tagData(nIndex).OpStatus = ACCESS_OPERATION_STATUS.ACCESS_SUCCESS) Then
                        Dim tag As Symbol.RFID3.TagData = tagData(nIndex)
                        Dim tagID As String = tag.TagID
                        Dim isFound As Boolean = False
                        SyncLock m_TagTable.SyncRoot
                            isFound = m_TagTable.ContainsKey(tagID)
                            If Not isFound Then
                                isFound = m_TagTable.ContainsKey(tagID)
                            End If
                        End SyncLock

                        If isFound Then
                            Dim count As UInteger = 0
                            item = DirectCast(m_TagTable(tagID), ListViewItem)
                            Dim memoryBank As String = tag.MemoryBank.ToString()
                            index = memoryBank.LastIndexOf("_"c)
                            If index <> -1 Then
                                memoryBank = memoryBank.Substring(index + 1)
                            End If
                            If tag.MemoryBankData.Length > 0 AndAlso Not memoryBank.Equals(item.SubItems(5).Text) Then
                                SyncLock m_TagTable.SyncRoot
                                    m_TagTable.Remove(tagID)
                                    m_TagTable.Add(tag.TagID + tag.MemoryBank.ToString() + tag.MemoryBankDataOffset.ToString(), item)
                                End SyncLock
                            End If
                        Else
                            'Aca se agrega el nuevo item leido, Aqui agregare la comparacion con el data grid view de la base de datos
                            item = New ListViewItem(tag.TagID)
                            inventoryList.BeginUpdate()
                            inventoryList.Items.Add(item)
                            inventoryList.EndUpdate()
                            'Dim row As DataGridViewRow
                            Dim tags_encontrados As Integer = 0
                            consulta_actas(item.Text)
                            SyncLock m_TagTable.SyncRoot
                                m_TagTable.Add(tagID, item)
                                totalTagValueLabel.Text = m_TagTable.Count
                            End SyncLock

                        End If
                    End If
                Next
            End If

        End Sub

        'Conexion a bases de datos
        Dim cadena As String = "server = ORCL; User id = prue_recep_cambio; Password = usi123; Unicode =True"
        Dim conn As New OracleConnection(cadena)
        ' /Fin conexion a base de datos
        Friend WithEvents DataGViewFaltantes As System.Windows.Forms.DataGridView
        Friend WithEvents DataGrid_no_encontrados As System.Windows.Forms.DataGridView
        Public id_paquete As String
        Public bandera As Integer = 0
        Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
        Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
        Friend WithEvents Label13 As System.Windows.Forms.Label
        Friend WithEvents DataGridENCONTRADOS As System.Windows.Forms.DataGridView
        Public codigo_barra_bolsa As String
        Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Dim acta_recibida As Integer = 0

        Private Sub actualizar_folio(ByVal parametroConsulta As String)
            Try

                Dim sqlConsulta_actualizar_acta As String = "update FOLIO set id_catalogo_recibida=2 where codigo_rfid =:CODIGO_rfid"
                Dim comando1 As New OracleCommand(sqlConsulta_actualizar_acta, conn)
                comando1.Parameters.Add(":CODIGO_rfid", OracleType.VarChar, 32).Value = parametroConsulta
                conn.Open()
                comando1.ExecuteNonQuery()
                conn.Close()
                cargar_GRID_recibidas()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                conn.Close()
            End Try
            If acta_recibida = 0 Then
                Try
                    Dim sqlConsult As String = "update acta set id_catalogo_recibida=3 where barcode=:codigo_BOLSA_JRV"
                    Dim comando1 As New OracleCommand(sqlConsult, conn)
                    comando1.Parameters.Add(":codigo_BOLSA_JRV", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
                    conn.Open()
                    comando1.ExecuteNonQuery()
                    conn.Close()

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
            End If
        End Sub


        'FUNCION PARA HACER EL UPDATE AL FOLIO, SI SE ENCUENTRA CON EL CODIGO RFID
        'Funciona de la siguiente manera:
        'primero toma el valor leido desde la antena RFID y hace una consulta a la base de datos, si este valor no se encuentra en la base,
        'no hace nada. En cambio si el valor se encuentra en la base de datos, hace un update de el atributo RECIBIDA de la tabla ACTA
        'y vuelve a cargar el data grid con los campos no recibidos
        Private Sub consulta_actas(ByVal parametroConsulta As String)
            ' If bandera = 0 Then

            Try
                Dim sqlConsulta_Seleccionar_bolsas_Folios As String = "select * from folio fol join acta ac on fol.ID_ACTA = ac.ID_ACTA where  fol.CODIGO_RFID=:CODIGO_RFID_FOLIO"
                Dim comando As New OracleCommand(sqlConsulta_Seleccionar_bolsas_Folios, conn)
                comando.Parameters.Add(":CODIGO_RFID_FOLIO", OracleType.VarChar, 32).Value = parametroConsulta
                Dim lector2 As OracleDataReader = Nothing
                conn.Open()
                lector2 = comando.ExecuteReader()
                If lector2.HasRows Then
                    conn.Close()
                    verificar_jrv_correcta(parametroConsulta)

                Else
                    MessageBox.Show("Surgio un problema, Este codigo de rfid no se encuentra en la base de datos")
                    conn.Close()
                End If
            Catch ex As Exception
                conn.Close()
                MessageBox.Show(ex.ToString)
            End Try
            'End If

            conn.Close()
        End Sub

        'FUNCION PARA CARGAR EL GRID CON TODOS LOS FOLIOS NO RECIBIDOS
        Public Sub cargar_GRID()
            conn.Close()
            Dim sqlConsult As String = "select JRV,nombre_tipo_eleccion,nombre_tipo_folio " & _
                " from folio fol join acta ac on fol.id_acta=ac.id_acta " & _
                " join tipo_eleccion tipel on fol.id_tipo_eleccion=tipel.ID_TIPO_ELECION " & _
               " join tipo_folio tipol on tipol.id_tipo_folio=fol.id_tipo_folio " & _
                   " where ac.barcode=:barcode_JRV and fol.ID_CATALOGO_RECIBIDA=1"
            Dim comando1 As New OracleCommand(sqlConsult, conn)
            comando1.Parameters.Add(":barcode_JRV", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
            Dim lector As OracleDataReader = Nothing
            conn.Open()
            lector = comando1.ExecuteReader()
            If lector.HasRows Then

                Dim dataAdapter As New OracleDataAdapter(comando1)
                Dim dataSet As New DataSet
                dataAdapter.Fill(dataSet, "Faltantes")
                Me.DataGViewFaltantes.DataSource = dataSet.Tables("Faltantes")
                conn.Close()
            Else
                DataGViewFaltantes.DataSource = Nothing
                conn.Close()
                Try
                    Dim sqlConsultaActualizar_recibida As String = "update acta set id_catalogo_recibida=3 where barcode=:codigo_BOLSA_JRV"
                    Dim comandoActualizar_recibida As New OracleCommand(sqlConsultaActualizar_recibida, conn)
                    comandoActualizar_recibida.Parameters.Add(":codigo_BOLSA_JRV", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
                    conn.Open()
                    comandoActualizar_recibida.ExecuteNonQuery()
                    conn.Close()

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
                MessageBox.Show("JRV COMPLETA")
                TextBox1.Focus()
            End If
        End Sub

        'FUNCION para verificar que el folio pertenezca a la JRV
        Private Sub verificar_jrv_correcta(ByVal parametroConsulta As String)
            Try
                Dim sqlConsulta_Seleccionar_bolsas_Folios As String = "select * from folio fol join acta ac on fol.ID_ACTA = ac.ID_ACTA " & _
                                                                    " where  fol.CODIGO_RFID=:CODIGO_RFID_FOLIO and ac.Barcode=:codigo_barra_jrv  "
                Dim comando As New OracleCommand(sqlConsulta_Seleccionar_bolsas_Folios, conn)
                comando.Parameters.Add(":CODIGO_RFID_FOLIO", OracleType.VarChar, 32).Value = parametroConsulta
                comando.Parameters.Add(":CODIGO_BARRA_jrv", OracleType.VarChar, 30).Value = TextBox1.Text.ToUpper
                Dim lector2 As OracleDataReader = Nothing
                conn.Open()
                lector2 = comando.ExecuteReader()
                If lector2.HasRows Then
                    conn.Close()
                    actualizar_folio(parametroConsulta)
                    txt_tags_encontrados.Text += 1
                    cargar_GRID_recibidas()
                    cargar_GRID()
                    'bandera = 1
                Else
                    conn.Close()
                    actualizar_folio_no_pertenece(parametroConsulta)

                End If
            Catch ex As Exception
                conn.Close()
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        'FUNCION para actualizar el folio con inconsistencia, cuando se encuentre en una JRV distinta a la que pertenece
        Private Sub actualizar_folio_no_pertenece(ByVal parametroConsulta As String)
            Try
                Dim sqlConsulta_Seleccionar_bolsas_Folios As String = "select * from folio " & _
                                                                    " where  CODIGO_RFID=:CODIGO_RFID_FOLIO and id_catalogo_recibida=1  "
                Dim comando As New OracleCommand(sqlConsulta_Seleccionar_bolsas_Folios, conn)
                comando.Parameters.Add(":CODIGO_RFID_FOLIO", OracleType.VarChar, 32).Value = parametroConsulta
                Dim lector2 As OracleDataReader = Nothing
                conn.Open()
                lector2 = comando.ExecuteReader()
                If lector2.HasRows Then
                    conn.Close()
                    Try
                        MessageBox.Show("Se ha encontrado un folio que no pertenece a esta JRV")
                        Dim sqlConsulta_actualizar_acta As String = "update FOLIO set inconsistencia=2 where codigo_rfid =:CODIGO_rfid"
                        Dim comando1 As New OracleCommand(sqlConsulta_actualizar_acta, conn)
                        comando1.Parameters.Add(":CODIGO_rfid", OracleType.VarChar, 32).Value = parametroConsulta
                        conn.Open()
                        comando1.ExecuteNonQuery()
                        conn.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                        conn.Close()
                    End Try
                End If
            Catch ex As Exception
                conn.Close()
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        'FUNCION PARA CARGAR EL GRID CON LOS FOLIOS RECIBIDOS
        Public Sub cargar_GRID_recibidas()
            Dim sqlConsult As String = "select tpfol.NOMBRE_TIPO_FOLIO as FOLIO, ac.JRV,nombre_tipo_eleccion " & _
                                        " from TIPO_FOLIO tpfol join folio fol on tpfol.ID_TIPO_FOLIO=fol.ID_TIPO_FOLIO " & _
                                        " join ACTA ac on fol.ID_ACTA=ac.ID_ACTA join tipo_eleccion tipel on TIPEL.ID_TIPO_ELECION=FOL.ID_TIPO_ELECCION " & _
                                        " where AC.BARCODE=:codigo_BOLSA_JRV and fol.ID_CATALOGO_RECIBIDA=2"
            Dim comando1 As New OracleCommand(sqlConsult, conn)
            comando1.Parameters.Add(":codigo_BOLSA_JRV", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
            Dim dataAdapter As New OracleDataAdapter(comando1)
            Dim dataSet As New DataSet
            dataAdapter.Fill(dataSet, "ENCONTRADOS")
            Me.DataGridENCONTRADOS.DataSource = dataSet.Tables("ENCONTRADOS")
            conn.Close()
        End Sub


        Private Sub readButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readButton.Click
            If TextBox1.Text = "" Then
                MessageBox.Show("El campo esta vacio")
            Else
                empezar()
            End If

        End Sub

        Private Sub readDataContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readDataContextMenuItem.Click
            Me.m_ReadForm.ShowDialog(Me)
        End Sub

        Private Sub readToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Me.m_ReadForm.ShowDialog(Me)
            Catch ex As Exception
                Me.functionCallStatusLabel.Text = ("Read Form:" & ex.Message)
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
            If (Not Me.m_BlockEraseForm Is Nothing) Then
                Me.m_BlockEraseForm.eraseButton.Enabled = True
            End If
        End Sub

        Private Sub writeDataContextMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles writeDataContextMenuItem.Click
            If (Nothing Is Me.m_WriteForm) Then
                Me.m_WriteForm = New WriteForm(Me, False)
            End If
            Me.m_WriteForm.ShowDialog(Me)
        End Sub

        Private Sub writeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Nothing Is Me.m_WriteForm) Then
                Me.m_WriteForm = New WriteForm(Me, False)
            End If
            Me.m_WriteForm.ShowDialog(Me)
        End Sub


        ' Fields
        Friend WithEvents accessBackgroundWorker As BackgroundWorker
        Private WithEvents blockEraseDataContextMenuItem As ToolStripMenuItem
        Private WithEvents blockWriteDataContextMenuItem As ToolStripMenuItem
        Private WithEvents clearReports_CB As CheckBox
        Private columnHeader1 As ColumnHeader
        Private components As IContainer = Nothing
        Private configToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents connectBackgroundWorker As BackgroundWorker
        Private connectionStatus As ToolStripStatusLabel
        Private connectionStatusLabel As ToolStripStatusLabel
        Private WithEvents connectionToolStripMenuItem As ToolStripMenuItem
        Private dataContextMenuStrip As ContextMenuStrip
        Private WithEvents exitToolStripMenuItem As ToolStripMenuItem
        Friend functionCallStatusLabel As ToolStripStatusLabel
        Private gpiLabel As Label
        Private gpiNumberLabel As Label
        Private gpiStateGB As GroupBox
        Private hScrollBar1 As HScrollBar
        Friend WithEvents inventoryList As ListView
        Private WithEvents killDataContextMenuItem As ToolStripMenuItem
        Private label10 As Label
        Private label11 As Label
        Private label12 As Label
        Private label2 As Label
        Private label3 As Label
        Private label4 As Label
        Private label5 As Label
        Private label6 As Label
        Private label7 As Label
        Private label8 As Label
        Private label9 As Label
        Private WithEvents lockDataContextMenuItem As ToolStripMenuItem
        Friend m_AccessOpResult As AccessOperationResult
        Friend m_BlockEraseForm As BlockEraseForm
        Friend m_ConnectionForm As ConnectionForm
        Friend m_IsConnected As Boolean
        Friend m_KillForm As KillForm
        Friend m_LockForm As LockForm
        Friend m_ReaderAPI As RFIDReader
        Friend m_ReadForm As ReadForm
        Private m_ReadTag As TagData = Nothing
        Friend m_SelectedTagID As String = Nothing
        Private m_TagTable As Hashtable
        Private m_TagTotalCount As UInt32
        Private m_UpdateReadHandler As UpdateRead = Nothing
        Private m_UpdateStatusHandler As UpdateStatus = Nothing
        Friend m_WriteForm As WriteForm
        Private Shadows mainMenuStrip As MenuStrip
        Private WithEvents readButton As Button
        Private WithEvents readDataContextMenuItem As ToolStripMenuItem
        Private statusStrip As StatusStrip
        Private WithEvents tagDataToolStripMenuItem As ToolStripMenuItem
        Private toolStripSeparator2 As ToolStripSeparator
        Private totalTagLabel As Label
        Private totalTagValueLabel As Label
        Private transmitPowerGB As GroupBox
        Friend WithEvents verificadosList As System.Windows.Forms.ListView
        Private WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents txt_tags_encontrados As System.Windows.Forms.Label
        Private WithEvents writeDataContextMenuItem As ToolStripMenuItem

        ' Nested Types
        Friend Class AccessOperationResult
            ' Fields
            Public m_OpCode As ACCESS_OPERATION_CODE = ACCESS_OPERATION_CODE.ACCESS_OPERATION_READ
            Public m_Result As RFIDResults = RFIDResults.RFID_NO_ACCESS_IN_PROGRESS
        End Class

        Private Delegate Sub UpdateRead(ByVal eventData As ReadEventData)

        Private Delegate Sub UpdateStatus(ByVal eventData As StatusEventData)

        Private Sub ImpresionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Me.Close()
            impresion.Show()

        End Sub


        Private Sub CrearUsuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Me.Close()
            UsuarioCrear.Show()
        End Sub

        Private Sub ModificarSuarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Me.Hide()
            editUser.Show()
        End Sub

        Private Sub Textbox_codigo_paquete_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
            If (e.KeyData = Keys.Enter) Then

                empezar()

            End If
        End Sub
        Private Sub empezar()

            Try
                If Me.m_IsConnected Then
                    If (Me.readButton.Text = "Empezar") Then

                        Try
                            ''HACER CONSULTA DE ACTA
                            Try

                                Dim paquete As String = "select * from acta where barcode=:codigo_ACTA"
                                Dim comando As New OracleCommand(paquete, conn)
                                comando.Parameters.Add(":codigo_ACTA", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
                                Dim lector As OracleDataReader = Nothing
                                '
                                conn.Open()
                                ' Ejecutamos el comando
                                '
                                lector = comando.ExecuteReader()
                                ' Si el lector tiene alguna fila, es porque al menos existe
                                ' un registro con los datos especificados, por tanto, podemos
                                ' decir que la validación ha sido satisfactoria.
                                '
                                If lector.HasRows Then
                                    cargar_GRID()


                                    Me.m_ReaderAPI.Actions.Inventory.Perform(Nothing, Nothing, Nothing)
                                    Me.inventoryList.Items.Clear()
                                    Me.verificadosList.Items.Clear()
                                    Me.m_TagTable.Clear()
                                    Me.m_TagTotalCount = 0
                                    Me.readButton.Text = "Parar"

                                Else
                                    MessageBox.Show("No se tienen registros de este CODIGO de JRV en la base de datos.")
                                    conn.Close()
                                    lector.Close()
                                    TextBox1.Clear()
                                    TextBox1.Focus()
                                End If
                            Catch ex As Exception
                                MessageBox.Show(ex.ToString)
                            End Try

                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try

                    ElseIf (Me.readButton.Text = "Parar") Then
                        TextBox1.Focus()
                        Try
                            Dim sqlConsult As String = "update acta set estado=2 where barcode=:codigo_BOLSA_JRV"
                            Dim comando1 As New OracleCommand(sqlConsult, conn)
                            comando1.Parameters.Add(":codigo_BOLSA_JRV", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
                            conn.Open()
                            comando1.ExecuteNonQuery()
                            conn.Close()
                            TextBox1.Focus()
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                        TextBox1.Clear()
                        TextBox1.Focus()
                        totalTagValueLabel.Text = 0
                        txt_tags_encontrados.Text = 0
                        TextBox1.Enabled = True
                        DataGrid_no_encontrados.DataSource = Nothing
                        DataGridENCONTRADOS.DataSource = Nothing
                        DataGViewFaltantes.DataSource = Nothing
                        If (Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0) Then
                            Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.StopSequence()
                            TextBox1.Focus()
                        Else
                            Me.m_ReaderAPI.Actions.Inventory.Stop()
                        End If
                        Me.readButton.Text = "Empezar"
                        TextBox1.Focus()

                    End If
                Else
                    Me.functionCallStatusLabel.Text = "Porfavor conecte el lector"
                End If
            Catch ioe As InvalidOperationException
                Me.functionCallStatusLabel.Text = ioe.Message
            Catch iue As InvalidUsageException
                Me.functionCallStatusLabel.Text = iue.Info
            Catch ofe As OperationFailureException
                Me.functionCallStatusLabel.Text = (ofe.Result & ":" & ofe.StatusDescription)
            Catch ex As Exception
                Me.functionCallStatusLabel.Text = ex.Message
            End Try
            'End If
            TextBox1.Focus()
        End Sub

        Private Sub AppForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            DataGridENCONTRADOS.Enabled = False
            DataGrid_no_encontrados.Enabled = False
            DataGViewFaltantes.Enabled = False
            Try
                connectBackgroundWorker.RunWorkerAsync("Connect")
                txt_tags_encontrados.Text = 0
            Catch ex As Exception
                functionCallStatusLabel.Text = ex.Message
            End Try
            TextBox1.Focus()
        End Sub

        Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
            If (e.KeyData = Keys.Enter) Then
                If TextBox1.Text = "" Then
                    MessageBox.Show("El campo esta vacio")
                Else
                    TextBox1.Enabled = False
                    empezar()
                End If
            End If
        End Sub
        Private Sub myUpdateStatus(ByVal eventData As Events.StatusEventData)

        End Sub

    End Class

End Namespace