Imports Symbol.RFID3
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class LocateForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.m_AppForm = appForm
            Me.InitializeComponent()
            Me.Locate_PB = New MotoProgressBar
            Me.Locate_PB.Location = New Point(120, 90)
            Me.Locate_PB.Name = "Indicator_PB"
            Me.Locate_PB.Size = New Size(40, &H93)
            Me.Locate_PB.Maximum = 100
            Me.Locate_PB.Minimum = 0
            MyBase.Controls.Add(Me.Locate_PB)
            Me.Locate_PB.Value = 0
        End Sub

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.label1 = New Label
            Me.locateButton = New Button
            Me.tagID_TB = New TextBox
            MyBase.SuspendLayout()
            Me.label1.Location = New Point(&H4C, 9)
            Me.label1.Name = "label1"
            Me.label1.Size = New Size(&H52, 20)
            Me.label1.Text = "Locating tag"
            Me.locateButton.Anchor = (AnchorStyles.Left Or AnchorStyles.Bottom)
            Me.locateButton.Font = New Font("Microsoft Sans Serif", 8.25!, 0)
            Me.locateButton.Location = New Point(&H9D, &H9D)
            Me.locateButton.Name = "locateButton"
            Me.locateButton.Size = New Size(&H42, &H13)
            Me.locateButton.TabIndex = 0
            Me.locateButton.Text = "Start"
            AddHandler Me.locateButton.Click, New EventHandler(AddressOf Me.locateButton_Click)
            Me.tagID_TB.Font = New Font("Microsoft Sans Serif", 9!, FontStyle.Bold)
            Me.tagID_TB.Location = New Point(11, &H20)
            Me.tagID_TB.Name = "tagID_TB"
            Me.tagID_TB.Size = New Size(&HD4, 20)
            Me.tagID_TB.TabIndex = 3
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.tagID_TB)
            MyBase.Controls.Add(Me.locateButton)
            MyBase.Controls.Add(Me.label1)
            MyBase.MaximizeBox = False
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "LocateForm"
            Me.Text = "Locate Tag"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.LocateForm_Load)
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.LocateForm_Closing)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub locateButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            If (Me.locateButton.Text = "Start") Then
                Dim antennaList As UInt16() = New UInt16() {1}
                Dim opList As OPERATION_QUALIFER() = New OPERATION_QUALIFER() {OPERATION_QUALIFER.LOCATE_TAG}
                Dim antennaInfo As New AntennaInfo(antennaList, opList)
                Me.m_AppForm.m_ReaderAPI.Actions.TagLocationing.Perform(Me.tagID_TB.Text, antennaInfo)
                Me.locateButton.Text = "Stop"
            ElseIf (Me.locateButton.Text = "Stop") Then
                Me.m_AppForm.m_ReaderAPI.Actions.TagLocationing.Stop()
                Me.locateButton.Text = "Start"
                Me.Locate_PB.Value = 0
            End If
        End Sub

        Private Sub LocateForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
        End Sub

        Private Sub LocateForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Me.Locate_PB.Value = 0
            Dim selectedIndex As Integer = Me.m_AppForm.inventoryList.SelectedIndices.Item(0)
            Dim item As ListViewItem = Me.m_AppForm.inventoryList.Items.Item(selectedIndex)
            Me.tagID_TB.Text = item.Text
        End Sub


        ' Fields
        Private components As IContainer = Nothing
        Private label1 As Label
        Friend Locate_PB As MotoProgressBar = Nothing
        Private locateButton As Button
        Private m_AppForm As AppForm = Nothing
        Private mainMenu1 As MainMenu
        Private tagID_TB As TextBox
    End Class
End Namespace

