Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample5
    Public Class TagDataForm
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
            Dim listViewItem8 As New ListViewItem
            Me.mainMenu1 = New MainMenu
            Me.tagDataView = New ListView
            Me.itemColumnHeader = New ColumnHeader
            Me.valueColumnHeader = New ColumnHeader
            MyBase.SuspendLayout()
            Me.tagDataView.Columns.Add(Me.itemColumnHeader)
            Me.tagDataView.Columns.Add(Me.valueColumnHeader)
            Me.tagDataView.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.tagDataView.FullRowSelect = True
            listViewItem1.Text = "TagID"
            listViewItem2.Text = "Antenna"
            listViewItem3.Text = "RSSI"
            listViewItem4.Text = "PC Bits"
            listViewItem5.Text = "Memory Bank"
            listViewItem6.Text = "Data"
            listViewItem7.Text = "Offset"
            listViewItem8.Text = "Length"
            Me.tagDataView.Items.Add(listViewItem1)
            Me.tagDataView.Items.Add(listViewItem2)
            Me.tagDataView.Items.Add(listViewItem3)
            Me.tagDataView.Items.Add(listViewItem4)
            Me.tagDataView.Items.Add(listViewItem5)
            Me.tagDataView.Items.Add(listViewItem6)
            Me.tagDataView.Items.Add(listViewItem7)
            Me.tagDataView.Items.Add(listViewItem8)
            Me.tagDataView.Location = New Point(3, 3)
            Me.tagDataView.Name = "tagDataView"
            Me.tagDataView.Size = New Size(&HE9, &HB6)
            Me.tagDataView.TabIndex = 2
            Me.tagDataView.View = View.Details
            Me.itemColumnHeader.Text = "Item"
            Me.itemColumnHeader.Width = &H56
            Me.valueColumnHeader.Text = "Value"
            Me.valueColumnHeader.Width = &H8E
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.tagDataView)
            MyBase.Menu = Me.mainMenu1
            MyBase.Name = "TagDataForm"
            Me.Text = "Tag Data"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.TagDataForm_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub TagDataForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If (Me.m_AppForm.m_IsConnected AndAlso Not Me.m_IsLoaded) Then
                    If (Me.m_AppForm.inventoryList.SelectedIndices.Count > 0) Then
                        Dim index As Integer = Me.m_AppForm.inventoryList.SelectedIndices.Item(0)
                        Dim item As ListViewItem = Me.m_AppForm.inventoryList.Items.Item(index)
                        Me.tagDataView.Items.Item(0).SubItems.Add(item.SubItems.Item(0).Text)
                        If (item.SubItems.Count > 7) Then
                            Me.tagDataView.Items.Item(1).SubItems.Add(item.SubItems.Item(1).Text)
                            Me.tagDataView.Items.Item(2).SubItems.Add(item.SubItems.Item(3).Text)
                            Me.tagDataView.Items.Item(3).SubItems.Add(item.SubItems.Item(4).Text)
                            Me.tagDataView.Items.Item(4).SubItems.Add(item.SubItems.Item(6).Text)
                            Me.tagDataView.Items.Item(5).SubItems.Add(item.SubItems.Item(5).Text)
                            Me.tagDataView.Items.Item(6).SubItems.Add(item.SubItems.Item(7).Text)
                            Dim length As Integer = (item.SubItems.Item(5).Text.Length / 2)
                            Me.tagDataView.Items.Item(7).SubItems.Add(length.ToString)
                        End If
                        Me.m_IsLoaded = True
                    Else
                        Me.m_AppForm.functionCallStatusLabel.Text = "No item is selected"
                    End If
                Else
                    Me.m_AppForm.functionCallStatusLabel.Text = "Please connect to a reader"
                End If
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private itemColumnHeader As ColumnHeader
        Private m_AppForm As AppForm
        Private m_IsLoaded As Boolean
        Private mainMenu1 As MainMenu
        Private tagDataView As ListView
        Private valueColumnHeader As ColumnHeader
    End Class
End Namespace

