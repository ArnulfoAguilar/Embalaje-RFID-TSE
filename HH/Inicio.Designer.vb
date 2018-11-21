<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Inicio
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Inicio))
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.btn_aceptar = New System.Windows.Forms.Button
        Me.txt_Sede = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btn_salir = New System.Windows.Forms.Button
        Me.lbl_tema = New System.Windows.Forms.Label
        Me.lbl_error = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btn_aceptar
        '
        Me.btn_aceptar.Location = New System.Drawing.Point(23, 141)
        Me.btn_aceptar.Name = "btn_aceptar"
        Me.btn_aceptar.Size = New System.Drawing.Size(72, 20)
        Me.btn_aceptar.TabIndex = 0
        Me.btn_aceptar.Text = "Aceptar"
        '
        'txt_Sede
        '
        Me.txt_Sede.Location = New System.Drawing.Point(146, 106)
        Me.txt_Sede.Name = "txt_Sede"
        Me.txt_Sede.Size = New System.Drawing.Size(55, 21)
        Me.txt_Sede.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(3, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 20)
        Me.Label1.Text = "Ruta De Sede Logistica"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(234, 80)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'btn_salir
        '
        Me.btn_salir.Location = New System.Drawing.Point(129, 141)
        Me.btn_salir.Name = "btn_salir"
        Me.btn_salir.Size = New System.Drawing.Size(72, 20)
        Me.btn_salir.TabIndex = 4
        Me.btn_salir.Text = "Salir"
        '
        'lbl_tema
        '
        Me.lbl_tema.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_tema.Location = New System.Drawing.Point(3, 3)
        Me.lbl_tema.Name = "lbl_tema"
        Me.lbl_tema.Size = New System.Drawing.Size(140, 20)
        Me.lbl_tema.Text = "Despacho de Cajas"
        '
        'lbl_error
        '
        Me.lbl_error.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.lbl_error.Location = New System.Drawing.Point(40, 86)
        Me.lbl_error.Name = "lbl_error"
        Me.lbl_error.Size = New System.Drawing.Size(100, 20)
        Me.lbl_error.Text = "Error"
        '
        'Inicio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.Controls.Add(Me.lbl_error)
        Me.Controls.Add(Me.lbl_tema)
        Me.Controls.Add(Me.btn_salir)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_Sede)
        Me.Controls.Add(Me.btn_aceptar)
        Me.Menu = Me.mainMenu1
        Me.Name = "Inicio"
        Me.Text = "Inicio"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_aceptar As System.Windows.Forms.Button
    Friend WithEvents txt_Sede As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btn_salir As System.Windows.Forms.Button
    Friend WithEvents lbl_tema As System.Windows.Forms.Label
    Friend WithEvents lbl_error As System.Windows.Forms.Label
End Class
