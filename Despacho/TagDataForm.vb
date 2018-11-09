Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3_Host_Sample1
    Public Class TagDataForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Dim listViewItem1 As New ListViewItem("TagID")
            Dim listViewItem2 As New ListViewItem("Antenna")
            Dim listViewItem3 As New ListViewItem("RSSI")
            Dim listViewItem4 As New ListViewItem("PC Bits")
            Dim listViewItem5 As New ListViewItem("Memory Bank Data")
            Dim listViewItem6 As New ListViewItem("MB")
            Dim listViewItem7 As New ListViewItem("Offset")
            Dim listViewItem8 As New ListViewItem("Length")
            Me.tagDataView = New ListView
            Me.ItemCol = New ColumnHeader
            Me.ValueCol = New ColumnHeader
            MyBase.SuspendLayout()
            Me.tagDataView.Columns.AddRange(New ColumnHeader() {Me.ItemCol, Me.ValueCol})
            Me.tagDataView.FullRowSelect = True
            Me.tagDataView.GridLines = True
            Me.tagDataView.Items.AddRange(New ListViewItem() {listViewItem1, listViewItem2, listViewItem3, listViewItem4, listViewItem5, listViewItem6, listViewItem7, listViewItem8})
            Me.tagDataView.Location = New Point(12, 12)
            Me.tagDataView.Name = "tagDataView"
            Me.tagDataView.Size = New Size(&H137, &H8E)
            Me.tagDataView.TabIndex = 2
            Me.tagDataView.UseCompatibleStateImageBehavior = False
            Me.tagDataView.View = View.Details
            Me.ItemCol.Text = "Item"
            Me.ItemCol.Width = &H67
            Me.ValueCol.Text = "Value"
            Me.ValueCol.Width = &HC2
            MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Font
            MyBase.ClientSize = New Size(&H14F, &HA7)
            MyBase.Controls.Add(Me.tagDataView)
            MyBase.FormBorderStyle = FormBorderStyle.FixedDialog
            MyBase.MaximizeBox = False
            MyBase.MinimizeBox = False
            MyBase.Name = "TagDataForm"
            MyBase.StartPosition = FormStartPosition.CenterParent
            Me.Text = "TagData"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.TagData_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub TagData_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                    Dim item As ListViewItem = Me.m_AppForm.inventoryList.SelectedItems.Item(0)
                    Me.tagDataView.Items.Item(0).SubItems.Add(item.SubItems.Item(0).Text)
                    If (item.SubItems.Count > 6) Then
                        Me.tagDataView.Items.Item(1).SubItems.Add(item.SubItems.Item(1).Text)
                        Me.tagDataView.Items.Item(2).SubItems.Add(item.SubItems.Item(3).Text)
                        Me.tagDataView.Items.Item(3).SubItems.Add(item.SubItems.Item(4).Text)
                        Me.tagDataView.Items.Item(4).SubItems.Add(item.SubItems.Item(5).Text)
                        Me.tagDataView.Items.Item(5).SubItems.Add(item.SubItems.Item(6).Text)
                        Me.tagDataView.Items.Item(6).SubItems.Add(item.SubItems.Item(7).Text)
                        Dim length As Integer = (item.SubItems.Item(5).Text.Length / 2)
                        Me.tagDataView.Items.Item(7).SubItems.Add(length.ToString)
                    End If
                End If
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private ItemCol As ColumnHeader
        Private m_AppForm As AppForm
        Private tagDataView As ListView
        Private ValueCol As ColumnHeader
    End Class
End Namespace

