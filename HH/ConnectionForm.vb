Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample5
    Public Class ConnectionForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
            Me.ipAddress = "127.0.0.1"
            Me.port = "5084"
        End Sub

        Private Sub connectionButton_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.ipAddress = Me.IP_TB.Text
            Me.port = Me.Port_TB.Text
            Try
                Me.m_AppForm.Connect(Me.connectionButton.Text)
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub

        Private Sub ConnectionForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not Me.m_IsLoaded Then
                Me.IP_TB.Text = Me.ipAddress
                Me.Port_TB.Text = Me.port
                Me.m_IsLoaded = True
            End If
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.readerIPLabel = New Label
            Me.connectionButton = New Button
            Me.portLabel = New Label
            Me.Port_TB = New TextBox
            Me.IP_TB = New TextBox
            Me.mainMenu1 = New MainMenu
            MyBase.SuspendLayout()
            Me.readerIPLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.readerIPLabel.Location = New Point(13, 20)
            Me.readerIPLabel.Name = "readerIPLabel"
            Me.readerIPLabel.Size = New Size(&H3A, 13)
            Me.readerIPLabel.Text = "Reader IP"
            Me.connectionButton.Location = New Point(&H69, &H6F)
            Me.connectionButton.Name = "connectionButton"
            Me.connectionButton.Size = New Size(&H5C, &H17)
            Me.connectionButton.TabIndex = 3
            Me.connectionButton.Text = "Connect"
            AddHandler Me.connectionButton.Click, New EventHandler(AddressOf Me.connectionButton_Click)
            Me.portLabel.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.portLabel.Location = New Point(13, &H3A)
            Me.portLabel.Name = "portLabel"
            Me.portLabel.Size = New Size(&H1A, 13)
            Me.portLabel.Text = "Port"
            Me.Port_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.Port_TB.Location = New Point(&H62, &H3A)
            Me.Port_TB.Name = "Port_TB"
            Me.Port_TB.Size = New Size(&H63, &H13)
            Me.Port_TB.TabIndex = 2
            Me.IP_TB.Font = New Font("Microsoft Sans Serif", 8.0!, FontStyle.Regular)
            Me.IP_TB.Location = New Point(&H62, 20)
            Me.IP_TB.Name = "IP_TB"
            Me.IP_TB.Size = New Size(&H63, &H13)
            Me.IP_TB.TabIndex = 1
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            Me.AutoScroll = True
            MyBase.ClientSize = New Size(240, &HBC)
            MyBase.Controls.Add(Me.IP_TB)
            MyBase.Controls.Add(Me.Port_TB)
            MyBase.Controls.Add(Me.connectionButton)
            MyBase.Controls.Add(Me.portLabel)
            MyBase.Controls.Add(Me.readerIPLabel)
            MyBase.Menu = Me.mainMenu1
            MyBase.Name = "ConnectionForm"
            Me.Text = "Connection"
            AddHandler MyBase.Load, New EventHandler(AddressOf Me.ConnectionForm_Load)
            MyBase.ResumeLayout(False)
        End Sub


        ' Properties
        Public ReadOnly Property IpText() As String
            Get
                Return Me.ipAddress
            End Get
        End Property

        Public ReadOnly Property PortText() As String
            Get
                Return Me.port
            End Get
        End Property


        ' Fields
        Private components As IContainer = Nothing
        Friend connectionButton As Button
        Private IP_TB As TextBox
        Private ipAddress As String
        Private m_AppForm As AppForm
        Private m_IsLoaded As Boolean
        Private mainMenu1 As MainMenu
        Private port As String
        Private Port_TB As TextBox
        Private portLabel As Label
        Private readerIPLabel As Label
    End Class
End Namespace

