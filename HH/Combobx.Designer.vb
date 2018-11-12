<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Combobx
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.combo_sede = New System.Windows.Forms.ComboBox
        Me.combo_paquete = New System.Windows.Forms.ComboBox
        Me.btn_aceptar = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'combo_sede
        '
        Me.combo_sede.Location = New System.Drawing.Point(20, 65)
        Me.combo_sede.Name = "combo_sede"
        Me.combo_sede.Size = New System.Drawing.Size(197, 22)
        Me.combo_sede.TabIndex = 0
        '
        'combo_paquete
        '
        Me.combo_paquete.Location = New System.Drawing.Point(20, 131)
        Me.combo_paquete.Name = "combo_paquete"
        Me.combo_paquete.Size = New System.Drawing.Size(197, 22)
        Me.combo_paquete.TabIndex = 1
        '
        'btn_aceptar
        '
        Me.btn_aceptar.Location = New System.Drawing.Point(77, 192)
        Me.btn_aceptar.Name = "btn_aceptar"
        Me.btn_aceptar.Size = New System.Drawing.Size(72, 20)
        Me.btn_aceptar.TabIndex = 2
        Me.btn_aceptar.Text = "Aceptar"
        '
        'Combobx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.Controls.Add(Me.btn_aceptar)
        Me.Controls.Add(Me.combo_paquete)
        Me.Controls.Add(Me.combo_sede)
        Me.Menu = Me.mainMenu1
        Me.Name = "Combobx"
        Me.Text = "Combobx"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents combo_sede As System.Windows.Forms.ComboBox
    Friend WithEvents combo_paquete As System.Windows.Forms.ComboBox
    Friend WithEvents btn_aceptar As System.Windows.Forms.Button
End Class
