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
        'Variables
        Dim id_paquete As Integer ' variable para guardar el id de la sede y hacer consulta de la caja
        Dim id_sede As Integer 'Variable para guardar el id del paquete y hacer consulta a la caja
        Dim BANDERA_BUTTON_READ As Integer = 0
        Dim box_read As Integer ' para Guardar el ID de la caja leida y meterlo en el grid
        Dim barcode_read As String ' para Guardar el abrcode de la caja leida y meterlo en el grid

        Private WithEvents readButton As System.Windows.Forms.Button
        Friend WithEvents Label17 As System.Windows.Forms.Label
        Private WithEvents btn_stop As System.Windows.Forms.Button
        ' VARIABLE PARA ACTIVAR LA BANDERA Y HABILITAR LA LECTURA
        Dim Caja_completa As Integer = 0 'bandera para determinar si la caja esta completa y que no siga leyendo

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

        'Private Sub clearReports_CB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles clearReports_CB.CheckedChanged
        '    '.Text = "0(0)"
        '    Me.inventoryList.Items.Clear()
        '    Me.verificadosList.Items.Clear()
        '    Me.m_TagTable.Clear()
        '    Me.clearReports_CB.Checked = False
        'End Sub

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
                    If BANDERA_BUTTON_READ = 1 Then
                        Me.readButton.Enabled = True
                    End If

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
            Me.mainMenuStrip = New System.Windows.Forms.MenuStrip
            Me.configToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.connectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
            Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.inventoryList = New System.Windows.Forms.ListView
            Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
            Me.gpiLabel = New System.Windows.Forms.Label
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
            Me.verificadosList = New System.Windows.Forms.ListView
            Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
            Me.Label1 = New System.Windows.Forms.Label
            Me.txt_tags_encontrados = New System.Windows.Forms.Label
            Me.DataGrid_no_encontrados = New System.Windows.Forms.DataGridView
            Me.PictureBox1 = New System.Windows.Forms.PictureBox
            Me.DataGridENCONTRADOS = New System.Windows.Forms.DataGridView
            Me.DGViewInconsistentes = New System.Windows.Forms.DataGridView
            Me.Label14 = New System.Windows.Forms.Label
            Me.txtCantidad_inconsistentes = New System.Windows.Forms.Label
            Me.ComboSede = New System.Windows.Forms.ComboBox
            Me.Label15 = New System.Windows.Forms.Label
            Me.Label16 = New System.Windows.Forms.Label
            Me.ComboPaquete = New System.Windows.Forms.ComboBox
            Me.btn_aceptar = New System.Windows.Forms.Button
            Me.readButton = New System.Windows.Forms.Button
            Me.Label17 = New System.Windows.Forms.Label
            Me.btn_stop = New System.Windows.Forms.Button
            Me.mainMenuStrip.SuspendLayout()
            Me.statusStrip.SuspendLayout()
            Me.dataContextMenuStrip.SuspendLayout()
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
            Me.mainMenuStrip.Size = New System.Drawing.Size(1068, 24)
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
            Me.inventoryList.Size = New System.Drawing.Size(100, 0)
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
            Me.statusStrip.Location = New System.Drawing.Point(0, 545)
            Me.statusStrip.Name = "statusStrip"
            Me.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            Me.statusStrip.Size = New System.Drawing.Size(1068, 25)
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
            Me.verificadosList.Size = New System.Drawing.Size(275, 72)
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
            Me.Label1.Location = New System.Drawing.Point(371, 180)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(91, 13)
            Me.Label1.TabIndex = 29
            Me.Label1.Text = "Cajas Detectadas"
            '
            'txt_tags_encontrados
            '
            Me.txt_tags_encontrados.AutoSize = True
            Me.txt_tags_encontrados.Location = New System.Drawing.Point(468, 180)
            Me.txt_tags_encontrados.Name = "txt_tags_encontrados"
            Me.txt_tags_encontrados.Size = New System.Drawing.Size(13, 13)
            Me.txt_tags_encontrados.TabIndex = 30
            Me.txt_tags_encontrados.Text = "0"
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
            Me.PictureBox1.Location = New System.Drawing.Point(12, 41)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(314, 152)
            Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox1.TabIndex = 38
            Me.PictureBox1.TabStop = False
            '
            'DataGridENCONTRADOS
            '
            Me.DataGridENCONTRADOS.AllowUserToAddRows = False
            Me.DataGridENCONTRADOS.AllowUserToDeleteRows = False
            Me.DataGridENCONTRADOS.AllowUserToResizeColumns = False
            Me.DataGridENCONTRADOS.AllowUserToResizeRows = False
            Me.DataGridENCONTRADOS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGridENCONTRADOS.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
            Me.DataGridENCONTRADOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DataGridENCONTRADOS.DefaultCellStyle = DataGridViewCellStyle2
            Me.DataGridENCONTRADOS.Enabled = False
            Me.DataGridENCONTRADOS.Location = New System.Drawing.Point(374, 196)
            Me.DataGridENCONTRADOS.Name = "DataGridENCONTRADOS"
            Me.DataGridENCONTRADOS.ReadOnly = True
            DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DataGridENCONTRADOS.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
            Me.DataGridENCONTRADOS.Size = New System.Drawing.Size(319, 325)
            Me.DataGridENCONTRADOS.TabIndex = 41
            '
            'DGViewInconsistentes
            '
            Me.DGViewInconsistentes.AllowUserToAddRows = False
            Me.DGViewInconsistentes.AllowUserToDeleteRows = False
            Me.DGViewInconsistentes.AllowUserToResizeColumns = False
            Me.DGViewInconsistentes.AllowUserToResizeRows = False
            Me.DGViewInconsistentes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DGViewInconsistentes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
            Me.DGViewInconsistentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
            DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.DGViewInconsistentes.DefaultCellStyle = DataGridViewCellStyle5
            Me.DGViewInconsistentes.Enabled = False
            Me.DGViewInconsistentes.Location = New System.Drawing.Point(742, 196)
            Me.DGViewInconsistentes.Name = "DGViewInconsistentes"
            Me.DGViewInconsistentes.ReadOnly = True
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
            DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
            Me.DGViewInconsistentes.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
            Me.DGViewInconsistentes.Size = New System.Drawing.Size(314, 325)
            Me.DGViewInconsistentes.TabIndex = 43
            '
            'Label14
            '
            Me.Label14.AutoSize = True
            Me.Label14.Location = New System.Drawing.Point(739, 180)
            Me.Label14.Name = "Label14"
            Me.Label14.Size = New System.Drawing.Size(125, 13)
            Me.Label14.TabIndex = 42
            Me.Label14.Text = "Cajas con Inconsistencia"
            '
            'txtCantidad_inconsistentes
            '
            Me.txtCantidad_inconsistentes.AutoSize = True
            Me.txtCantidad_inconsistentes.Location = New System.Drawing.Point(870, 180)
            Me.txtCantidad_inconsistentes.Name = "txtCantidad_inconsistentes"
            Me.txtCantidad_inconsistentes.Size = New System.Drawing.Size(13, 13)
            Me.txtCantidad_inconsistentes.TabIndex = 44
            Me.txtCantidad_inconsistentes.Text = "0"
            '
            'ComboSede
            '
            Me.ComboSede.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboSede.FormattingEnabled = True
            Me.ComboSede.Location = New System.Drawing.Point(374, 113)
            Me.ComboSede.Name = "ComboSede"
            Me.ComboSede.Size = New System.Drawing.Size(156, 21)
            Me.ComboSede.TabIndex = 51
            '
            'Label15
            '
            Me.Label15.AutoSize = True
            Me.Label15.Location = New System.Drawing.Point(371, 97)
            Me.Label15.Name = "Label15"
            Me.Label15.Size = New System.Drawing.Size(77, 13)
            Me.Label15.TabIndex = 52
            Me.Label15.Text = "Sede Logistica"
            '
            'Label16
            '
            Me.Label16.AutoSize = True
            Me.Label16.Location = New System.Drawing.Point(596, 97)
            Me.Label16.Name = "Label16"
            Me.Label16.Size = New System.Drawing.Size(86, 13)
            Me.Label16.TabIndex = 54
            Me.Label16.Text = "Tipo de Paquete"
            '
            'ComboPaquete
            '
            Me.ComboPaquete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboPaquete.FormattingEnabled = True
            Me.ComboPaquete.Location = New System.Drawing.Point(599, 113)
            Me.ComboPaquete.Name = "ComboPaquete"
            Me.ComboPaquete.Size = New System.Drawing.Size(156, 21)
            Me.ComboPaquete.TabIndex = 53
            '
            'btn_aceptar
            '
            Me.btn_aceptar.Location = New System.Drawing.Point(821, 111)
            Me.btn_aceptar.Name = "btn_aceptar"
            Me.btn_aceptar.Size = New System.Drawing.Size(75, 23)
            Me.btn_aceptar.TabIndex = 56
            Me.btn_aceptar.Text = "Aceptar"
            Me.btn_aceptar.UseVisualStyleBackColor = True
            '
            'readButton
            '
            Me.readButton.BackColor = System.Drawing.SystemColors.Control
            Me.readButton.Enabled = False
            Me.readButton.Image = CType(resources.GetObject("readButton.Image"), System.Drawing.Image)
            Me.readButton.Location = New System.Drawing.Point(50, 268)
            Me.readButton.Name = "readButton"
            Me.readButton.Size = New System.Drawing.Size(64, 57)
            Me.readButton.TabIndex = 2
            Me.readButton.UseVisualStyleBackColor = False
            '
            'Label17
            '
            Me.Label17.AutoSize = True
            Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label17.Location = New System.Drawing.Point(355, 41)
            Me.Label17.Name = "Label17"
            Me.Label17.Size = New System.Drawing.Size(541, 37)
            Me.Label17.TabIndex = 55
            Me.Label17.Text = "Despacho de Cajas a Sede Logistica"
            '
            'btn_stop
            '
            Me.btn_stop.BackColor = System.Drawing.SystemColors.Control
            Me.btn_stop.Enabled = False
            Me.btn_stop.Image = CType(resources.GetObject("btn_stop.Image"), System.Drawing.Image)
            Me.btn_stop.Location = New System.Drawing.Point(185, 268)
            Me.btn_stop.Name = "btn_stop"
            Me.btn_stop.Size = New System.Drawing.Size(72, 57)
            Me.btn_stop.TabIndex = 57
            Me.btn_stop.UseVisualStyleBackColor = False
            '
            'AppForm
            '
            Me.AutoScroll = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ClientSize = New System.Drawing.Size(1068, 570)
            Me.Controls.Add(Me.btn_stop)
            Me.Controls.Add(Me.Label15)
            Me.Controls.Add(Me.ComboPaquete)
            Me.Controls.Add(Me.ComboSede)
            Me.Controls.Add(Me.btn_aceptar)
            Me.Controls.Add(Me.Label16)
            Me.Controls.Add(Me.Label17)
            Me.Controls.Add(Me.DGViewInconsistentes)
            Me.Controls.Add(Me.txtCantidad_inconsistentes)
            Me.Controls.Add(Me.Label14)
            Me.Controls.Add(Me.statusStrip)
            Me.Controls.Add(Me.DataGridENCONTRADOS)
            Me.Controls.Add(Me.PictureBox1)
            Me.Controls.Add(Me.DataGrid_no_encontrados)
            Me.Controls.Add(Me.mainMenuStrip)
            Me.Controls.Add(Me.verificadosList)
            Me.Controls.Add(Me.inventoryList)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.txt_tags_encontrados)
            Me.Controls.Add(Me.readButton)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
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
                                'totalTagValueLabel.Text = m_TagTable.Count
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
        Friend WithEvents DataGrid_no_encontrados As System.Windows.Forms.DataGridView
        Public bandera As Integer = 0
        Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
        Friend WithEvents DataGridENCONTRADOS As System.Windows.Forms.DataGridView
        Public codigo_barra_bolsa As String
        Friend WithEvents DGViewInconsistentes As System.Windows.Forms.DataGridView
        Friend WithEvents Label14 As System.Windows.Forms.Label
        Friend WithEvents txtCantidad_inconsistentes As System.Windows.Forms.Label
        Friend WithEvents ComboSede As System.Windows.Forms.ComboBox
        Friend WithEvents Label15 As System.Windows.Forms.Label
        Friend WithEvents Label16 As System.Windows.Forms.Label
        Friend WithEvents ComboPaquete As System.Windows.Forms.ComboBox
        Friend WithEvents btn_aceptar As System.Windows.Forms.Button
        Dim acta_recibida As Integer = 0

        'Funcion para actualizar el articulo cuando ya se detecto por RFID 
        Private Sub actualizar_Caja_despachada()
            Try
                Dim sqlConsulta_actualizar_articulo As String = "update caja set id_estado=2 where id_caja=:id_caja"
                Dim comando1 As New OracleCommand(sqlConsulta_actualizar_articulo, conn)
                comando1.Parameters.Add(":id_caja", OracleType.VarChar, 32).Value = id_caja
                conn.Open()
                comando1.ExecuteNonQuery()
                conn.Close()
                cargar_GRID_recibidas()
                conn.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                conn.Close()
            End Try
        End Sub


        'FUNCION PARA HACER EL UPDATE AL ARTICULO, SI SE ENCUENTRA CON EL CODIGO RFID
        'Funciona de la siguiente manera:
        'primero toma el valor leido desde la antena RFID y hace una consulta a la base de datos, si este valor no se encuentra en la base,
        'no hace nada. En cambio si el valor se encuentra en la base de datos, hace un update de el atributo id_est_art de la tabla Articulo
        'y actualiza el data grid con los articulos no recibidos
        Private Sub consulta_Articulos(ByVal parametroConsulta As String)
            ' If bandera = 0 Then

            Try
                Dim sqlConsulta_Seleccionar_caja_articulos As String = "select c.id_caja caja, c.codebar " & _
                                                                        " from detalle_caja det" & _
                                                                        " join caja c on det.id_caja=c.id_caja" & _
                                                                        " where det.RFID=:CODIGO_RFID_ARTICULO and c.id_estado = 1"
                Dim comando As New OracleCommand(sqlConsulta_Seleccionar_caja_articulos, conn)
                comando.Parameters.Add(":CODIGO_RFID_ARTICULO", OracleType.VarChar, 32).Value = parametroConsulta
                Dim lector2 As OracleDataReader = Nothing
                conn.Open()
                lector2 = comando.ExecuteReader()
                If lector2.HasRows Then
                    Do While lector2.Read
                        box_read = lector2.GetInt32(0)
                        barcode_read = lector2.GetString(1)
                    Loop
                    conn.Close()
                    verificar_caja_correcta(parametroConsulta)

                Else
                    ' MessageBox.Show("Este codigo de rfid no se encuentra en la base de datos")
                    conn.Close()
                End If
            Catch ex As Exception
                conn.Close()
                MessageBox.Show(ex.ToString)
            End Try
            'End If

            conn.Close()
        End Sub

        'FUNCION PARA CARGAR EL GRID CON TODOS LAS CAJAS NO DETECTADAS


        'FUNCION para verificar que CAJA pertenezca a la SEDE LOGISTICA correcta
        ' Sirve para comprobar que el codigo RFID leido, pertenezca a una caja de la SEDE LOGISTICA elegida
        Private Sub verificar_caja_correcta(ByVal parametroConsulta As String)
            Try
                Dim sqlConsulta_Seleccionar_caja_articulos As String = " select c.id_caja caja" & _
                                                                        " from detalle_caja det" & _
                                                                        " join caja c on det.id_caja=c.id_caja" & _
                                                                        " where det.RFID=:CODIGO_RFID_ARTICULO and c.id_sede=:sede and c.id_paquete=:paquete"

                Dim comando As New OracleCommand(sqlConsulta_Seleccionar_caja_articulos, conn)
                comando.Parameters.Add(":CODIGO_RFID_ARTICULO", OracleType.VarChar, 32).Value = parametroConsulta
                comando.Parameters.Add(":sede", OracleType.VarChar, 32).Value = id_sede
                comando.Parameters.Add(":paquete", OracleType.VarChar, 32).Value = id_paquete

                Dim lector2 As OracleDataReader = Nothing
                conn.Open()
                lector2 = comando.ExecuteReader()
                If lector2.HasRows Then
                    id_caja = lector2.GetInt32(0)
                    conn.Close()
                    actualizar_Caja_despachada()
                    txt_tags_encontrados.Text += 1

                Else
                    conn.Close()
                    ' Se actualiza la caja como inconsistente,
                    'Agregandole el ID de la sede en la que se encontro
                    actualizar_articulo_no_pertenece(parametroConsulta)

                End If
            Catch ex As Exception
                conn.Close()
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        'FUNCION para actualizar el Articulo con inconsistencia, cuando se encuentre en una CAJA distinta a la que pertenece
        Private Sub actualizar_articulo_no_pertenece(ByVal parametroConsulta As String)
            Try
                ' Se consulta si el articulo que se detecto, aun no ha pasado y no tiene inconsistencia 
                Dim sqlConsulta_Seleccionar_CAJA_ARTICULOS As String = " select c.id_caja caja " & _
                                                                        " from detalle_caja det " & _
                                                                        " join caja c on det.id_caja=c.id_caja " & _
                                                                        " where det.RFID=:CODIGO_RFID_articulo and c.id_estado <>2 " & _
                                                                        " and c.id_incon=0 "

                Dim comando As New OracleCommand(sqlConsulta_Seleccionar_CAJA_ARTICULOS, conn)
                comando.Parameters.Add(":CODIGO_RFID_articulo", OracleType.VarChar, 32).Value = parametroConsulta
                Dim lector2 As OracleDataReader = Nothing
                conn.Open()
                lector2 = comando.ExecuteReader()
                If lector2.HasRows Then
                    Do While lector2.Read
                        id_caja = lector2.GetInt32(0)
                    Loop
                    conn.Close()
                    Try
                        'MessageBox.Show("Se ha encontrado un articulo que no pertenece a esta CAJA")
                        Dim sqlConsulta_actualizar_articulo As String = "update caja set id_incon=1 where id_caja=:id_caja"
                        Dim comando1 As New OracleCommand(sqlConsulta_actualizar_articulo, conn)
                        comando1.Parameters.Add(":id_caja", OracleType.Int32, 32).Value = id_caja
                        conn.Open()
                        comando1.ExecuteNonQuery()
                        conn.Close()
                        cargar_GRID_Inconsistentes()
                        txtCantidad_inconsistentes.Text += 1
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
            DataGridENCONTRADOS.Rows.Add(box_read, barcode_read)
        End Sub

        'Funcion para cargar el Grid con los articulos que no pertenecen a la Caja que se esta ESCANEANDO
        'Primero se compara que el numero de Inconsistencia sea igual al ID de la caja
        'Despues se compara que el id del articulo sea distinto del id de la caja 
        ' Para comprobar 2 veces que sea inconsistencia
        Public Sub cargar_GRID_Inconsistentes()
            DGViewInconsistentes.Rows.Add(box_read, barcode_read)
        End Sub

        Private Sub readButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readButton.Click
            empezar()
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
        Private WithEvents readDataContextMenuItem As ToolStripMenuItem
        Private statusStrip As StatusStrip
        Private WithEvents tagDataToolStripMenuItem As ToolStripMenuItem
        Private toolStripSeparator2 As ToolStripSeparator
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

        Private Sub empezar()

            Try
                id_sede = ComboSede.SelectedValue.ToString
                id_paquete = ComboPaquete.SelectedValue.ToString
                Dim paquete As String = "select id_caja from caja where id_sede=:sede and id_paquete=:paquete and id_estado=1"
                Dim comando As New OracleCommand(paquete, conn)
                comando.Parameters.Add(":sede", OracleType.Char, 50).Value = id_sede
                comando.Parameters.Add(":paquete", OracleType.Char, 50).Value = id_paquete
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
                    conn.Close()
                    Try
                        If Me.m_IsConnected Then
                            'If (Me.readButton.Text = "Empezar") Then
                            Try
                                Me.m_ReaderAPI.Actions.Inventory.Perform(Nothing, Nothing, Nothing)
                                Me.inventoryList.Items.Clear()
                                Me.verificadosList.Items.Clear()
                                Me.m_TagTable.Clear()
                                Me.m_TagTotalCount = 0
                                'Me.readButton.Text = "Parar"
                                btn_stop.Enabled = True
                                readButton.Enabled = False
                            Catch ex As Exception
                                MessageBox.Show(ex.ToString)
                            End Try

                            'ElseIf (Me.readButton.Text = "Parar") Then
                            'parar()
                            'Caja_completa = 0
                            ' End If
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

                Else
                    MessageBox.Show(" SEDE completa, todas las cajas verificadas fueron despachadas.")
                    conn.Close()
                    lector.Close()
                    readButton.Enabled = False
                    ComboSede.Enabled = True
                    ComboPaquete.Enabled = True
                    btn_aceptar.Text = "Aceptar"
                    btn_aceptar.Focus()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        Private Sub parar()
            'totalTagValueLabel.Text = 0
            txt_tags_encontrados.Text = 0
            txtCantidad_inconsistentes.Text = 0
            DataGrid_no_encontrados.DataSource = Nothing
            DataGridENCONTRADOS.DataSource = Nothing
            'DataGViewFaltantes.DataSource = Nothing
            DGViewInconsistentes.DataSource = Nothing
            If (Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.Length > 0) Then
                Me.m_ReaderAPI.Actions.TagAccess.OperationSequence.StopSequence()

            Else
                Me.m_ReaderAPI.Actions.Inventory.Stop()
            End If
            ' Me.readButton.Text = "Empezar"


        End Sub

        Private Sub AppForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            DataGridENCONTRADOS.Enabled = False
            DataGrid_no_encontrados.Enabled = False
            readButton.Enabled = False
            CargarCombobox_sede()
            CargarCombobox_Paquete()
            DataGridENCONTRADOS.Columns.Add("id_caja", "CAJA")
            DataGridENCONTRADOS.Columns.Add("barcode", "BarCode")
            DGViewInconsistentes.Columns.Add("id_caja", "CAJA")
            DGViewInconsistentes.Columns.Add("barcode", "BarCode")
            Try
                connectBackgroundWorker.RunWorkerAsync("Connect")
                txt_tags_encontrados.Text = 0
            Catch ex As Exception
                functionCallStatusLabel.Text = ex.Message
            End Try
            btn_aceptar.Focus()
        End Sub


        Private Sub myUpdateStatus(ByVal eventData As Events.StatusEventData)

        End Sub
        Private Sub CargarCombobox_sede()
            Try
                Dim sqlConsult As String = "select ID_SEDE, NOMBRE_SEDE from SEDE_LOGISTICA"
                Dim dataAdapter As New OracleDataAdapter(sqlConsult, conn)
                Dim DT As New DataTable
                dataAdapter.Fill(DT)
                Me.ComboSede.DataSource = DT
                ComboSede.ValueMember = "ID_SEDE"
                ComboSede.DisplayMember = "NOMBRE_SEDE"
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        Private Sub CargarCombobox_Paquete()
            Try

                Dim sqlConsult As String = "select ID_PAQUETE, NOMBRE_PAQUETE from PAQUETE"
                Dim dataAdapter As New OracleDataAdapter(sqlConsult, conn)
                Dim DT As New DataTable
                dataAdapter.Fill(DT)
                Me.ComboPaquete.DataSource = DT
                ComboPaquete.ValueMember = "ID_PAQUETE"
                ComboPaquete.DisplayMember = "NOMBRE_PAQUETE"
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End Sub


        Private Sub btn_aceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_aceptar.Click
            If btn_aceptar.Text = "Aceptar" Then
                If ComboSede.Items.Count <= 0 Or ComboPaquete.Items.Count <= 0 Then
                    MessageBox.Show("Hay campos Vacios")
                Else
                    Try
                        id_sede = ComboSede.SelectedValue.ToString
                        id_paquete = ComboPaquete.SelectedValue.ToString
                        Dim paquete As String = "select id_caja from caja where id_sede=:sede and id_paquete=:paquete and id_estado=1"
                        Dim comando As New OracleCommand(paquete, conn)
                        comando.Parameters.Add(":sede", OracleType.Char, 50).Value = id_sede
                        comando.Parameters.Add(":paquete", OracleType.Char, 50).Value = id_paquete
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
                            conn.Close()

                            ComboSede.Enabled = False
                            ComboPaquete.Enabled = False
                            readButton.Enabled = True
                            btn_aceptar.Text = "Cancelar"

                        Else
                            MessageBox.Show("Sede Logistica completa, todas las cajas fueron despachadas.")
                            conn.Close()
                            lector.Close()
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If

            ElseIf btn_aceptar.Text = "Cancelar" Then
                ComboSede.Enabled = True
                ComboPaquete.Enabled = True
                readButton.Enabled = False
                btn_aceptar.Text = "Aceptar"
                'DataGrid_no_encontrados.DataSource = Nothing
                DataGridENCONTRADOS.Rows.Clear()
                DGViewInconsistentes.Rows.Clear()
            End If

        End Sub

        Private Sub btn_stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_stop.Click
            parar()
            Caja_completa = 0
            btn_stop.Enabled = False
            readButton.Enabled = True
            DataGridENCONTRADOS.Rows.Clear()
            DGViewInconsistentes.Rows.Clear()
        End Sub
    End Class

End Namespace