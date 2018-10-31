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
            Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
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
            Me.DGViewInconsistentes = New System.Windows.Forms.DataGridView
            Me.Label14 = New System.Windows.Forms.Label
            Me.txtCantidad_inconsistentes = New System.Windows.Forms.Label
            Me.Departamento = New System.Windows.Forms.Label
            Me.Sede = New System.Windows.Forms.Label
            Me.Municipio = New System.Windows.Forms.Label
            Me.txt_Departamento = New System.Windows.Forms.Label
            Me.txt_Sede = New System.Windows.Forms.Label
            Me.txt_Municipio = New System.Windows.Forms.Label
            Me.mainMenuStrip.SuspendLayout()
            Me.statusStrip.SuspendLayout()
            Me.dataContextMenuStrip.SuspendLayout()
            CType(Me.DataGViewFaltantes, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataGrid_no_encontrados, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DataGridENCONTRADOS, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.DGViewInconsistentes, System.ComponentModel.ISupportInitialize).BeginInit()
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
            Me.readButton.Location = New System.Drawing.Point(126, 396)
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
            Me.txt_tags_encontrados.Location = New System.Drawing.Point(761, 35)
            Me.txt_tags_encontrados.Name = "txt_tags_encontrados"
            Me.txt_tags_encontrados.Size = New System.Drawing.Size(13, 13)
            Me.txt_tags_encontrados.TabIndex = 30
            Me.txt_tags_encontrados.Text = "0"
            '
            'DataGViewFaltantes
            '
            Me.DataGViewFaltantes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGViewFaltantes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle19
            Me.DataGViewFaltantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DataGViewFaltantes.DefaultCellStyle = DataGridViewCellStyle20
            Me.DataGViewFaltantes.Location = New System.Drawing.Point(357, 59)
            Me.DataGViewFaltantes.Name = "DataGViewFaltantes"
            DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGViewFaltantes.RowHeadersDefaultCellStyle = DataGridViewCellStyle21
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
            Me.TextBox1.Location = New System.Drawing.Point(61, 358)
            Me.TextBox1.Name = "TextBox1"
            Me.TextBox1.Size = New System.Drawing.Size(244, 20)
            Me.TextBox1.TabIndex = 39
            '
            'Label13
            '
            Me.Label13.AutoSize = True
            Me.Label13.Location = New System.Drawing.Point(123, 333)
            Me.Label13.Name = "Label13"
            Me.Label13.Size = New System.Drawing.Size(136, 13)
            Me.Label13.TabIndex = 40
            Me.Label13.Text = "CODIGO DE BARRA CAJA"
            '
            'DataGridENCONTRADOS
            '
            Me.DataGridENCONTRADOS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGridENCONTRADOS.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle22
            Me.DataGridENCONTRADOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DataGridENCONTRADOS.DefaultCellStyle = DataGridViewCellStyle23
            Me.DataGridENCONTRADOS.Location = New System.Drawing.Point(648, 59)
            Me.DataGridENCONTRADOS.Name = "DataGridENCONTRADOS"
            DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGridENCONTRADOS.RowHeadersDefaultCellStyle = DataGridViewCellStyle24
            Me.DataGridENCONTRADOS.Size = New System.Drawing.Size(237, 395)
            Me.DataGridENCONTRADOS.TabIndex = 41
            '
            'DGViewInconsistentes
            '
            Me.DGViewInconsistentes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DGViewInconsistentes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle25
            Me.DGViewInconsistentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DGViewInconsistentes.DefaultCellStyle = DataGridViewCellStyle26
            Me.DGViewInconsistentes.Location = New System.Drawing.Point(905, 59)
            Me.DGViewInconsistentes.Name = "DGViewInconsistentes"
            DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DGViewInconsistentes.RowHeadersDefaultCellStyle = DataGridViewCellStyle27
            Me.DGViewInconsistentes.Size = New System.Drawing.Size(237, 395)
            Me.DGViewInconsistentes.TabIndex = 43
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
            'txtCantidad_inconsistentes
            '
            Me.txtCantidad_inconsistentes.AutoSize = True
            Me.txtCantidad_inconsistentes.Location = New System.Drawing.Point(1026, 35)
            Me.txtCantidad_inconsistentes.Name = "txtCantidad_inconsistentes"
            Me.txtCantidad_inconsistentes.Size = New System.Drawing.Size(13, 13)
            Me.txtCantidad_inconsistentes.TabIndex = 44
            Me.txtCantidad_inconsistentes.Text = "0"
            '
            'Departamento
            '
            Me.Departamento.AutoSize = True
            Me.Departamento.Location = New System.Drawing.Point(25, 232)
            Me.Departamento.Name = "Departamento"
            Me.Departamento.Size = New System.Drawing.Size(77, 13)
            Me.Departamento.TabIndex = 45
            Me.Departamento.Text = "Departamento:"
            '
            'Sede
            '
            Me.Sede.AutoSize = True
            Me.Sede.Location = New System.Drawing.Point(58, 265)
            Me.Sede.Name = "Sede"
            Me.Sede.Size = New System.Drawing.Size(35, 13)
            Me.Sede.TabIndex = 46
            Me.Sede.Text = "Sede:"
            '
            'Municipio
            '
            Me.Municipio.AutoSize = True
            Me.Municipio.Location = New System.Drawing.Point(38, 298)
            Me.Municipio.Name = "Municipio"
            Me.Municipio.Size = New System.Drawing.Size(55, 13)
            Me.Municipio.TabIndex = 47
            Me.Municipio.Text = "Municipio:"
            '
            'txt_Departamento
            '
            Me.txt_Departamento.AutoSize = True
            Me.txt_Departamento.Location = New System.Drawing.Point(105, 232)
            Me.txt_Departamento.Name = "txt_Departamento"
            Me.txt_Departamento.Size = New System.Drawing.Size(74, 13)
            Me.txt_Departamento.TabIndex = 48
            Me.txt_Departamento.Text = "Departamento"
            '
            'txt_Sede
            '
            Me.txt_Sede.AutoSize = True
            Me.txt_Sede.Location = New System.Drawing.Point(105, 265)
            Me.txt_Sede.Name = "txt_Sede"
            Me.txt_Sede.Size = New System.Drawing.Size(32, 13)
            Me.txt_Sede.TabIndex = 49
            Me.txt_Sede.Text = "Sede"
            '
            'txt_Municipio
            '
            Me.txt_Municipio.AutoSize = True
            Me.txt_Municipio.Location = New System.Drawing.Point(105, 298)
            Me.txt_Municipio.Name = "txt_Municipio"
            Me.txt_Municipio.Size = New System.Drawing.Size(52, 13)
            Me.txt_Municipio.TabIndex = 50
            Me.txt_Municipio.Text = "Municipio"
            '
            'AppForm
            '
            Me.AutoScroll = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ClientSize = New System.Drawing.Size(1235, 498)
            Me.Controls.Add(Me.txt_Municipio)
            Me.Controls.Add(Me.txt_Sede)
            Me.Controls.Add(Me.txt_Departamento)
            Me.Controls.Add(Me.Municipio)
            Me.Controls.Add(Me.Sede)
            Me.Controls.Add(Me.Departamento)
            Me.Controls.Add(Me.txtCantidad_inconsistentes)
            Me.Controls.Add(Me.DGViewInconsistentes)
            Me.Controls.Add(Me.Label14)
            Me.Controls.Add(Me.DataGViewFaltantes)
            Me.Controls.Add(Me.DataGridENCONTRADOS)
            Me.Controls.Add(Me.TextBox1)
            Me.Controls.Add(Me.Label13)
            Me.Controls.Add(Me.PictureBox1)
            Me.Controls.Add(Me.clearReports_CB)
            Me.Controls.Add(Me.statusStrip)
            Me.Controls.Add(Me.DataGrid_no_encontrados)
            Me.Controls.Add(Me.mainMenuStrip)
            Me.Controls.Add(Me.verificadosList)
            Me.Controls.Add(Me.txt_tags_encontrados)
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
            Me.Text = "Escaneo"
            Me.mainMenuStrip.ResumeLayout(False)
            Me.mainMenuStrip.PerformLayout()
            Me.statusStrip.ResumeLayout(False)
            Me.statusStrip.PerformLayout()
            Me.dataContextMenuStrip.ResumeLayout(False)
            CType(Me.DataGViewFaltantes, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DataGrid_no_encontrados, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DataGridENCONTRADOS, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.DGViewInconsistentes, System.ComponentModel.ISupportInitialize).EndInit()
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
                            consulta_Articulos(item.Text)
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
        'Dim cadena As String = "Data Source=172.16.1.16:1521/RFID; User id=EMBALAJE; password=embalaje01"
        'Dim conn As New OracleConnection(cadena)
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
        Friend WithEvents DGViewInconsistentes As System.Windows.Forms.DataGridView
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents txtCantidad_inconsistentes As System.Windows.Forms.Label
        Friend WithEvents Departamento As System.Windows.Forms.Label
        Friend WithEvents Sede As System.Windows.Forms.Label
        Friend WithEvents Municipio As System.Windows.Forms.Label
        Friend WithEvents txt_Departamento As System.Windows.Forms.Label
        Friend WithEvents txt_Sede As System.Windows.Forms.Label
        Friend WithEvents txt_Municipio As System.Windows.Forms.Label
        Dim acta_recibida As Integer = 0

        'Funcion para actualizar el articulo cuando ya se detecto por RFID 
        Private Sub actualizar_Articulo_recibido(ByVal parametroConsulta As String)
            Try

                Dim sqlConsulta_actualizar_articulo As String = "UPDATE ARTICULO SET id_est_art=1 where RFID =:CODIGO_RFID"
                Dim comando1 As New OracleCommand(sqlConsulta_actualizar_articulo, conn)
                comando1.Parameters.Add(":CODIGO_RFID", OracleType.VarChar, 32).Value = parametroConsulta
                conn.Open()
                comando1.ExecuteNonQuery()
                conn.Close()
                cargar_GRID_recibidas()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                conn.Close()
            End Try
            'Actualizar la caja con inconsistencia cuando se detecte al menos un articulo Equivocado 
            'If acta_recibida = 0 Then
            '    Try
            '        Dim sqlConsult As String = "update acta set id_catalogo_recibida=3 where barcode=:codigo_BOLSA_JRV"
            '        Dim comando1 As New OracleCommand(sqlConsult, conn)
            '        comando1.Parameters.Add(":codigo_BOLSA_JRV", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
            '        conn.Open()
            '        comando1.ExecuteNonQuery()
            '        conn.Close()

            '    Catch ex As Exception
            '        MessageBox.Show(ex.ToString)
            '    End Try
            'End If
        End Sub


        'FUNCION PARA HACER EL UPDATE AL ARTICULO, SI SE ENCUENTRA CON EL CODIGO RFID
        'Funciona de la siguiente manera:
        'primero toma el valor leido desde la antena RFID y hace una consulta a la base de datos, si este valor no se encuentra en la base,
        'no hace nada. En cambio si el valor se encuentra en la base de datos, hace un update de el atributo id_est_art de la tabla Articulo
        'y actualiza el data grid con los articulos no recibidos
        Private Sub consulta_Articulos(ByVal parametroConsulta As String)
            ' If bandera = 0 Then

            Try
                Dim sqlConsulta_Seleccionar_caja_articulos As String = "select RFID from articulo a where a.RFID=:CODIGO_RFID_ARTICULO"
                Dim comando As New OracleCommand(sqlConsulta_Seleccionar_caja_articulos, conn)
                comando.Parameters.Add(":CODIGO_RFID_ARTICULO", OracleType.VarChar, 32).Value = parametroConsulta
                Dim lector2 As OracleDataReader = Nothing
                conn.Open()
                lector2 = comando.ExecuteReader()
                If lector2.HasRows Then
                    conn.Close()
                    verificar_caja_correcta(parametroConsulta)

                Else
                    MessageBox.Show("Este codigo de rfid no se encuentra en la base de datos")
                    conn.Close()
                End If
            Catch ex As Exception
                conn.Close()
                MessageBox.Show(ex.ToString)
            End Try
            'End If

            conn.Close()
        End Sub

        'FUNCION PARA CARGAR EL GRID CON TODOS LOS ARTICULOS NO DETECTADOS
        Public Sub cargar_GRID()
            conn.Close()
            Dim sqlConsult As String = "select TP.NOMBRE_ARTICULO " & _
                                        " from articulo a join caja c on A.ID_ARTICULO=C.ID_CAJA" & _
                                        " join TIPOARTICULO tp on A.ID_TIPO=TP.ID_TIPO " & _
                                        " where C.CODEBAR=:barcode_CAJA and a.id_est_art=0" 'FALTA AGREGAR EL ESTADO DE RECIBIDO DE UN ARTICULO 
            Dim comando1 As New OracleCommand(sqlConsult, conn)
            comando1.Parameters.Add(":barcode_CAJA", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
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
                ' Si ya no se detectan articulos 
                'Se pone el Grid a cero y se actualiza la caja como COMPLETA
                DataGViewFaltantes.DataSource = Nothing
                conn.Close()
                Try
                    Dim sqlConsultaActualizar_recibida As String = "update caja c set C.ID_COMPLETO=1 where C.CODEBAR=:codigo_BARRA_CAJA"
                    Dim comandoActualizar_recibida As New OracleCommand(sqlConsultaActualizar_recibida, conn)
                    comandoActualizar_recibida.Parameters.Add(":codigo_BARRA_CAJA", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
                    conn.Open()
                    comandoActualizar_recibida.ExecuteNonQuery()
                    conn.Close()

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
                MessageBox.Show("CAJA COMPLETA")
                TextBox1.Focus()
            End If
        End Sub

        'FUNCION para verificar que el articulo pertenezca a la CAJA correcta
        ' Sirve para comprobar que el codigo RFID leido, pertenezca a la caja que se pistoleo con el codigo de barra
        Private Sub verificar_caja_correcta(ByVal parametroConsulta As String)
            Try
                Dim sqlConsulta_Seleccionar_caja_articulos As String = "select a.RFID, C.CODEBAR from articulo a join caja c  " & _
                                                                        "on A.ID_ARTICULO=C.ID_CAJA2 " & _
                                                                        "where a.RFID=:CODIGO_RFID_ARTICULO and c.codebar=:codigo_barra_caja  "
                Dim comando As New OracleCommand(sqlConsulta_Seleccionar_caja_articulos, conn)
                comando.Parameters.Add(":CODIGO_RFID_ARTICULO", OracleType.VarChar, 32).Value = parametroConsulta
                comando.Parameters.Add(":CODIGO_BARRA_caja", OracleType.VarChar, 30).Value = TextBox1.Text.ToUpper
                Dim lector2 As OracleDataReader = Nothing
                conn.Open()
                lector2 = comando.ExecuteReader()
                If lector2.HasRows Then
                    conn.Close()
                    actualizar_Articulo_recibido(parametroConsulta)
                    txt_tags_encontrados.Text += 1
                    cargar_GRID_recibidas()
                    cargar_GRID()
                    'bandera = 1
                Else
                    conn.Close()
                    ' Se actualiza el articulo como inconsistente,
                    'Agregandole el ID de la caja en la que se encontro
                    actualizar_articulo_no_pertenece(parametroConsulta)
                    txtCantidad_inconsistentes.Text += 1
                    cargar_GRID_Inconsistentes()
                End If
            Catch ex As Exception
                conn.Close()
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        'FUNCION para actualizar el Articulo con inconsistencia, cuando se encuentre en una CAJA distinta a la que pertenece
        Private Sub actualizar_articulo_no_pertenece(ByVal parametroConsulta As String)
            Try
                Dim sqlConsulta_Seleccionar_CAJA_ARTICULOS As String = "select * from Articulo " & _
                                                                    " where CODIGO_RFID=:CODIGO_RFID_articulo and id_est_art=0 and Inconsistente=0 "
                Dim comando As New OracleCommand(sqlConsulta_Seleccionar_CAJA_ARTICULOS, conn)
                comando.Parameters.Add(":CODIGO_RFID_articulo", OracleType.VarChar, 32).Value = parametroConsulta
                Dim lector2 As OracleDataReader = Nothing
                conn.Open()
                lector2 = comando.ExecuteReader()
                If lector2.HasRows Then
                    conn.Close()
                    Try
                        MessageBox.Show("Se ha encontrado un articulo que no pertenece a esta CAJA")
                        Dim sqlConsulta_actualizar_articulo As String = "update articulo set inconsistencia=:id_caja where codigo_rfid =:CODIGO_rfid"
                        Dim comando1 As New OracleCommand(sqlConsulta_actualizar_articulo, conn)
                        comando1.Parameters.Add(":CODIGO_rfid", OracleType.VarChar, 32).Value = parametroConsulta
                        comando1.Parameters.Add(":id_caja", OracleType.Int32, 32).Value = id_caja
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
        'FUNCION PARA CARGAR EL GRID CON LOS ARTICULOS RECIBIDOS
        'Se invoca esta funcion cada vezz que se ha detectado un 
        'Codigo RFID que pertenece a una caja en especifico
        Public Sub cargar_GRID_recibidas()
            Dim sqlConsult As String = "select TP.NOMBRE_ARTICULO " & _
                                        " from articulo a join caja c on A.ID_ARTICULO=C.ID_CAJA" & _
                                        " join TIPOARTICULO tp on A.ID_TIPO=TP.ID_TIPO " & _
                                        " where C.CODEBAR=::codigo_BOLSA_CAJA and a.id_est_art=1"
            Dim comando1 As New OracleCommand(sqlConsult, conn)
            comando1.Parameters.Add(":codigo_BOLSA_CAJA", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
            Dim dataAdapter As New OracleDataAdapter(comando1)
            Dim dataSet As New DataSet
            dataAdapter.Fill(dataSet, "ENCONTRADOS")
            Me.DataGridENCONTRADOS.DataSource = dataSet.Tables("ENCONTRADOS")
            conn.Close()
        End Sub

        'Funcion para cargar el Grid con los articulos que no pertenecen a la Caja que se esta ESCANEANDO
        'Primero se compara que el numero de Inconsistencia sea igual al ID de la caja
        'Despues se compara que el id del articulo sea distinto del id de la caja 
        ' Para comprobar 2 veces que sea inconsistencia
        Public Sub cargar_GRID_Inconsistentes()
            Dim sqlConsult As String = "select TP.NOMBRE_ARTICULO from articulo a join caja c on A.INCONSISTENCIA=C.ID_CAJA " & _
                                        " join TIPOARTICULO tp on TP.ID_TIPO=A.ID_TIPO " & _
                                        " where C.CODEBAR =:codigo_barra_caja and and A.ID_ARTICULO<>C.ID_CAJA"
            Dim comando1 As New OracleCommand(sqlConsult, conn)
            comando1.Parameters.Add(":codigo_barra_caja", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
            Dim dataAdapter As New OracleDataAdapter(comando1)
            Dim dataSet As New DataSet
            dataAdapter.Fill(dataSet, "Inconsistentes")
            Me.DGViewInconsistentes.DataSource = dataSet.Tables("Inconsistentes")
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
                            'HACER CONSULTA DE CAJA
                            Try

                                Dim paquete As String = "select id_caja, id_depto, id_sede, id_muni from caja where codebar=:codigo_CAJA"
                                Dim comando As New OracleCommand(paquete, conn)
                                comando.Parameters.Add(":codigo_CAJA", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
                                Dim lector As OracleDataReader = Nothing
                                '
                                conn.Open()
                                ' Ejecutamos el comando
                                '
                                lector = comando.ExecuteReader()
                                ' Si el lector tiene alguna fila, es porque al menos existe
                                ' una caja con los datos especificados, por tanto, podemos
                                ' decir que la validación ha sido satisfactoria.
                                '
                                If lector.HasRows Then
                                    'Se carga el grid con los articulos que deberia tener la caja
                                    cargar_GRID()
                                    Do While lector.Read
                                        'Le asigno valor a la variable global
                                        id_caja = lector.GetInt32(0)

                                        'Le asigno los siguientes valores a los LABEL
                                        txt_Departamento.Text = lector.GetString(1)
                                        txt_Sede.Text = lector.GetString(2)
                                        txt_Municipio.Text = lector.GetString(3)
                                    Loop
                                    conn.Close()
                                    Me.m_ReaderAPI.Actions.Inventory.Perform(Nothing, Nothing, Nothing)
                                    Me.inventoryList.Items.Clear()
                                    Me.verificadosList.Items.Clear()
                                    Me.m_TagTable.Clear()
                                    Me.m_TagTotalCount = 0
                                    Me.readButton.Text = "Parar"

                                Else
                                    MessageBox.Show("Este codigo de barra no esta en la base de datos de CAJAS.")
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
                        'Try
                        '    Dim sqlConsult As String = "update Caja set estado=2 where barcode=:codigo_BOLSA_JRV"
                        '    Dim comando1 As New OracleCommand(sqlConsult, conn)
                        '    comando1.Parameters.Add(":codigo_BOLSA_JRV", OracleType.Char, 50).Value = TextBox1.Text.ToUpper
                        '    conn.Open()
                        '    comando1.ExecuteNonQuery()
                        '    conn.Close()
                        '    TextBox1.Focus()
                        'Catch ex As Exception
                        '    MessageBox.Show(ex.ToString)
                        'End Try
                        TextBox1.Clear()
                        TextBox1.Focus()
                        totalTagValueLabel.Text = 0
                        txt_tags_encontrados.Text = 0
                        txtCantidad_inconsistentes.Text = 0
                        TextBox1.Enabled = True
                        DataGrid_no_encontrados.DataSource = Nothing
                        DataGridENCONTRADOS.DataSource = Nothing
                        DataGViewFaltantes.DataSource = Nothing
                        DGViewInconsistentes.DataSource = Nothing
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