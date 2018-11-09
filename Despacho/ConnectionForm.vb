Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports VB_RFID3_Host_Sample1

Namespace VB_RFID3_Host_Sample1
    Public Class ConnectionForm
        Inherits Form
        ' Methods
        Public Sub New(ByVal appForm As AppForm)
            Me.InitializeComponent()
            Me.m_AppForm = appForm
        End Sub

        Private Sub connectionButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles connectionButton.Click
            Try
                Me.m_AppForm.connectBackgroundWorker.RunWorkerAsync(Me.connectionButton.Text)
            Catch ex As Exception
                Me.m_AppForm.functionCallStatusLabel.Text = ex.Message
            End Try
        End Sub


        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.readerIPLabel = New System.Windows.Forms.Label
            Me.portLabel = New System.Windows.Forms.Label
            Me.connectionButton = New System.Windows.Forms.Button
            Me.connectionLabel = New System.Windows.Forms.Label
            Me.port_TB = New System.Windows.Forms.TextBox
            Me.hostname_TB = New System.Windows.Forms.TextBox
            Me.SuspendLayout()
            '
            'readerIPLabel
            '
            Me.readerIPLabel.AutoSize = True
            Me.readerIPLabel.Location = New System.Drawing.Point(12, 23)
            Me.readerIPLabel.Name = "readerIPLabel"
            Me.readerIPLabel.Size = New System.Drawing.Size(113, 13)
            Me.readerIPLabel.TabIndex = 8
            Me.readerIPLabel.Text = "Host Name/Reader IP"
            '
            'portLabel
            '
            Me.portLabel.AutoSize = True
            Me.portLabel.Location = New System.Drawing.Point(12, 59)
            Me.portLabel.Name = "portLabel"
            Me.portLabel.Size = New System.Drawing.Size(26, 13)
            Me.portLabel.TabIndex = 9
            Me.portLabel.Text = "Port"
            '
            'connectionButton
            '
            Me.connectionButton.Location = New System.Drawing.Point(187, 90)
            Me.connectionButton.Name = "connectionButton"
            Me.connectionButton.Size = New System.Drawing.Size(92, 23)
            Me.connectionButton.TabIndex = 5
            Me.connectionButton.Text = "Connect"
            Me.connectionButton.UseVisualStyleBackColor = True
            '
            'connectionLabel
            '
            Me.connectionLabel.AutoSize = True
            Me.connectionLabel.Location = New System.Drawing.Point(12, 95)
            Me.connectionLabel.Name = "connectionLabel"
            Me.connectionLabel.Size = New System.Drawing.Size(0, 13)
            Me.connectionLabel.TabIndex = 14
            '
            'port_TB
            '
            Me.port_TB.Location = New System.Drawing.Point(128, 56)
            Me.port_TB.Name = "port_TB"
            Me.port_TB.Size = New System.Drawing.Size(71, 20)
            Me.port_TB.TabIndex = 4
            Me.port_TB.Text = "5084"
            '
            'hostname_TB
            '
            Me.hostname_TB.Location = New System.Drawing.Point(128, 22)
            Me.hostname_TB.Name = "hostname_TB"
            Me.hostname_TB.Size = New System.Drawing.Size(152, 20)
            Me.hostname_TB.TabIndex = 1
            Me.hostname_TB.Text = Ipreader
            '
            'ConnectionForm
            '
            Me.ClientSize = New System.Drawing.Size(288, 125)
            Me.Controls.Add(Me.hostname_TB)
            Me.Controls.Add(Me.connectionLabel)
            Me.Controls.Add(Me.port_TB)
            Me.Controls.Add(Me.connectionButton)
            Me.Controls.Add(Me.portLabel)
            Me.Controls.Add(Me.readerIPLabel)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ConnectionForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Connection Settings"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub


        ' Properties
        Public ReadOnly Property IpText() As String
            Get
                Return Me.hostname_TB.Text
            End Get
        End Property

        Public ReadOnly Property PortText() As String
            Get
                Return Me.port_TB.Text
            End Get
        End Property


        ' Fields
        Private components As IContainer = Nothing
        Friend WithEvents connectionButton As Button
        Private connectionLabel As Label
        Friend hostname_TB As TextBox
        Private m_AppForm As AppForm
        Friend port_TB As TextBox
        Private portLabel As Label
        Private readerIPLabel As Label

    End Class
End Namespace

