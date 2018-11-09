Imports Symbol.RFID3
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class AntennaInfoForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
            Me.m_CheckBox = New ArrayList
        End Sub

        Private Sub antennaInfoButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim cb As CheckBox
            Try
                If Me.m_AppForm.m_ReaderAPI.IsConnected Then
                    Dim checkList As New ArrayList

                    For Each cb In Me.m_CheckBox
                        If cb.Checked Then
                            checkList.Add(cb.Text)
                        End If
                    Next
                    If (checkList.Count = 0) Then

                        For Each cb In Me.m_CheckBox
                            cb.Checked = True
                            Me.selectAll_CB.Checked = True
                            checkList.Add(cb.Text)
                        Next
                    End If
                    If (checkList.Count > 0) Then
                        Dim antList As UInt16() = New UInt16(checkList.Count - 1) {}
                        Dim index As Integer
                        For index = 0 To checkList.Count - 1
                            antList(index) = UInt16.Parse(checkList.Item(index).ToString)
                        Next index
                        If (Nothing Is Me.m_AntennaList) Then
                            Me.m_AntennaList = New AntennaInfo(antList)
                        Else
                            Me.m_AntennaList.AntennaID = antList
                        End If
                    End If
                Else
                    Me.m_AppForm.notifyUser("Please connect to a reader", "Write Operation")
                End If
                MyBase.Close()
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Antenna Info")
            End Try
        End Sub

        Private Sub AntennaInfoForm_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
        End Sub

        Private Sub AntennaInfoForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            Try
                If Not Me.m_IsLoaded Then
                    Dim xPos As Integer = (Me.selectAll_CB.Location.X + 80)
                    Dim yPos As Integer = (Me.selectAll_CB.Location.Y + 60)
                    Dim numAtenna As Integer = Me.m_AppForm.m_ReaderAPI.Config.Antennas.AvailableAntennas.Length
                    Dim index As Integer
                    For index = 0 To numAtenna - 1
                        If ((index > 0) AndAlso ((index Mod 2) = 0)) Then
                            xPos = 80
                            yPos = (yPos + 60)
                        End If
                        Dim cb As New CheckBox
                        Dim name As Integer = (index + 1)
                        cb.Location = New Point(xPos, yPos)
                        cb.Name = ("checkBox " & name)
                        cb.Size = New Size(80, 20)
                        cb.TabIndex = name
                        cb.Text = name.ToString
                        cb.Checked = True
                        MyBase.Controls.Add(cb)
                        Me.m_CheckBox.Add(cb)
                        xPos = (xPos + 120)
                    Next index
                    Me.selectAll_CB.Checked = True
                    Me.m_IsLoaded = True
                End If
            Catch ex As Exception
                Me.m_AppForm.notifyUser(ex.Message, "Antenna Info")
            End Try
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Public Function getInfo() As AntennaInfo
            Return Me.m_AntennaList
        End Function

        Private Sub InitializeComponent()
            Me.mainMenu1 = New MainMenu
            Me.selectAll_CB = New CheckBox
            Me.antennaInfoButton = New Button
            MyBase.SuspendLayout()
            Me.selectAll_CB.Location = New Point(3, 13)
            Me.selectAll_CB.Name = "selectAll_CB"
            Me.selectAll_CB.Size = New Size(100, &H1D)
            Me.selectAll_CB.TabIndex = 0
            Me.selectAll_CB.Text = "Select All"
            AddHandler Me.selectAll_CB.CheckStateChanged, New EventHandler(AddressOf Me.selectAll_CB_CheckStateChanged)
            Me.antennaInfoButton.Location = New Point(&HBA, &HA5)
            Me.antennaInfoButton.Name = "antennaInfoButton"
            Me.antennaInfoButton.Size = New Size(&H33, 20)
            Me.antennaInfoButton.TabIndex = &H12
            Me.antennaInfoButton.Text = "Apply"
            AddHandler Me.antennaInfoButton.Click, New EventHandler(AddressOf Me.antennaInfoButton_Click)
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.selectAll_CB)
            MyBase.Controls.Add(Me.antennaInfoButton)
            MyBase.Menu = Me.mainMenu1
            MyBase.MinimizeBox = False
            MyBase.Name = "AntennaInfoForm"
            Me.Text = "Antenna Info"
            AddHandler MyBase.Closing, New CancelEventHandler(AddressOf Me.AntennaInfoForm_Closing)
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.AntennaInfoForm_Load)
            MyBase.ResumeLayout(False)
        End Sub

        Private Sub selectAll_CB_CheckStateChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim cb As CheckBox
            For Each cb In Me.m_CheckBox
                cb.Checked = Me.selectAll_CB.Checked
            Next
        End Sub


        ' Fields
        Private antennaInfoButton As Button
        Private components As IContainer = Nothing
        Private m_AntennaList As AntennaInfo = Nothing
        Private m_AppForm As AppForm
        Private m_CheckBox As ArrayList = Nothing
        Private m_IsLoaded As Boolean = False
        Private mainMenu1 As MainMenu
        Private selectAll_CB As CheckBox
    End Class
End Namespace

