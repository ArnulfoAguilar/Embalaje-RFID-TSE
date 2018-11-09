Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace VB_RFID3Sample6
    Public Class MotoProgressBar
        Inherits UserControl
        ' Methods
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Draw3DBorder(ByVal g As Graphics)
        End Sub

        Private Sub InitializeComponent()
            MyBase.SuspendLayout()
            MyBase.AutoScaleDimensions = New SizeF(96.0!, 96.0!)
            MyBase.AutoScaleMode = AutoScaleMode.Dpi
            MyBase.Name = "MotoProgressBar"
            MyBase.Size = New Size(60, &H8B)
            MyBase.ResumeLayout(False)
        End Sub

        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim g As Graphics = e.Graphics
            Dim brush As New SolidBrush(Me.BarColor)
            Me.paintBar(g, brush)
            Me.Draw3DBorder(g)
            brush.Dispose()
            g.Dispose()
        End Sub

        Protected Overrides Sub OnResize(ByVal e As EventArgs)
            MyBase.Invalidate()
        End Sub

        Public Sub paintBar(ByVal g As Graphics, ByVal brush As Brush)
            Dim barHeight As Integer = 0
            Dim barWidth As Integer = 0
            Dim x As Integer = 0
            Dim y As Integer = MyBase.Height
            barHeight = (((Me.val - Me.min) * MyBase.Height) / (Me.max - Me.min))
            barWidth = MyBase.Width
            g.FillRectangle(brush, x, (y - barHeight), barWidth, barHeight)
        End Sub


        ' Properties
        Public Property Maximum() As Integer
            Get
                Return Me.max
            End Get
            Set(ByVal value As Integer)
                If (value < Me.min) Then
                    Me.min = value
                End If
                Me.max = value
                If (Me.val > Me.max) Then
                    Me.val = Me.max
                End If
                MyBase.Invalidate()
            End Set
        End Property

        Public Property Minimum() As Integer
            Get
                Return Me.min
            End Get
            Set(ByVal value As Integer)
                If (value < 0) Then
                    Me.min = 0
                End If
                If (value > Me.max) Then
                    Me.min = value
                    Me.min = value
                End If
                If (Me.val < Me.min) Then
                    Me.val = Me.min
                End If
                MyBase.Invalidate()
            End Set
        End Property

        Public Property ProgressBarColor() As Color
            Get
                Return Me.BarColor
            End Get
            Set(ByVal value As Color)
                Me.BarColor = value
                MyBase.Invalidate()
            End Set
        End Property

        Public Property Value() As Integer
            Get
                Return Me.val
            End Get
            Set(ByVal value As Integer)
                Dim oldValue As Integer = Me.val
                If (value < Me.min) Then
                    Me.val = Me.min
                ElseIf (value > Me.max) Then
                    Me.val = Me.max
                Else
                    Me.val = value
                End If
                MyBase.Invalidate()
            End Set
        End Property


        ' Fields
        Private BarColor As Color = Color.Blue
        Private components As IContainer = Nothing
        Private max As Integer = 100
        Private min As Integer = 0
        Private val As Integer = 0
    End Class
End Namespace

