<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Paquete_Nuevo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Paquete_Nuevo))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_nombre = New System.Windows.Forms.TextBox()
        Me.cbx_evento = New System.Windows.Forms.ComboBox()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.lb_titulo = New System.Windows.Forms.Label()
        Me.lb_art = New System.Windows.Forms.Label()
        Me.txt_art = New System.Windows.Forms.TextBox()
        Me.btn_add = New System.Windows.Forms.Button()
        Me.dtg_art = New System.Windows.Forms.DataGridView()
        Me.lb_reg = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dtg_art, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(45, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre del Paquete"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(45, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Elija el Evento"
        '
        'txt_nombre
        '
        Me.txt_nombre.Location = New System.Drawing.Point(158, 36)
        Me.txt_nombre.Name = "txt_nombre"
        Me.txt_nombre.Size = New System.Drawing.Size(209, 20)
        Me.txt_nombre.TabIndex = 2
        '
        'cbx_evento
        '
        Me.cbx_evento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_evento.FormattingEnabled = True
        Me.cbx_evento.Location = New System.Drawing.Point(158, 74)
        Me.cbx_evento.Name = "cbx_evento"
        Me.cbx_evento.Size = New System.Drawing.Size(209, 21)
        Me.cbx_evento.TabIndex = 3
        '
        'btn_guardar
        '
        Me.btn_guardar.Image = CType(resources.GetObject("btn_guardar.Image"), System.Drawing.Image)
        Me.btn_guardar.Location = New System.Drawing.Point(556, 23)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(83, 81)
        Me.btn_guardar.TabIndex = 4
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'lb_titulo
        '
        Me.lb_titulo.AutoSize = True
        Me.lb_titulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_titulo.Location = New System.Drawing.Point(142, 145)
        Me.lb_titulo.Name = "lb_titulo"
        Me.lb_titulo.Size = New System.Drawing.Size(379, 25)
        Me.lb_titulo.TabIndex = 5
        Me.lb_titulo.Text = "AGREGAR ARTICULOS AL PAQUETE"
        '
        'lb_art
        '
        Me.lb_art.AutoSize = True
        Me.lb_art.Location = New System.Drawing.Point(62, 209)
        Me.lb_art.Name = "lb_art"
        Me.lb_art.Size = New System.Drawing.Size(42, 13)
        Me.lb_art.TabIndex = 6
        Me.lb_art.Text = "Articulo"
        '
        'txt_art
        '
        Me.txt_art.Location = New System.Drawing.Point(158, 201)
        Me.txt_art.Name = "txt_art"
        Me.txt_art.Size = New System.Drawing.Size(209, 20)
        Me.txt_art.TabIndex = 7
        '
        'btn_add
        '
        Me.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_add.Image = CType(resources.GetObject("btn_add.Image"), System.Drawing.Image)
        Me.btn_add.Location = New System.Drawing.Point(556, 173)
        Me.btn_add.Name = "btn_add"
        Me.btn_add.Size = New System.Drawing.Size(83, 83)
        Me.btn_add.TabIndex = 8
        Me.btn_add.Text = "Añadir"
        Me.btn_add.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_add.UseVisualStyleBackColor = True
        '
        'dtg_art
        '
        Me.dtg_art.AllowUserToAddRows = False
        Me.dtg_art.AllowUserToDeleteRows = False
        Me.dtg_art.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dtg_art.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtg_art.Enabled = False
        Me.dtg_art.Location = New System.Drawing.Point(65, 241)
        Me.dtg_art.Name = "dtg_art"
        Me.dtg_art.Size = New System.Drawing.Size(467, 213)
        Me.dtg_art.TabIndex = 9
        '
        'lb_reg
        '
        Me.lb_reg.AutoSize = True
        Me.lb_reg.Location = New System.Drawing.Point(192, 454)
        Me.lb_reg.Name = "lb_reg"
        Me.lb_reg.Size = New System.Drawing.Size(46, 13)
        Me.lb_reg.TabIndex = 15
        Me.lb_reg.Text = "registros"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(62, 454)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Registros Encontrados"
        '
        'Frm_Paquete_Nuevo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 476)
        Me.Controls.Add(Me.lb_reg)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtg_art)
        Me.Controls.Add(Me.btn_add)
        Me.Controls.Add(Me.txt_art)
        Me.Controls.Add(Me.lb_art)
        Me.Controls.Add(Me.lb_titulo)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.cbx_evento)
        Me.Controls.Add(Me.txt_nombre)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_Paquete_Nuevo"
        Me.Text = "Frm_Paquete_Nuevo"
        CType(Me.dtg_art, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_nombre As System.Windows.Forms.TextBox
    Friend WithEvents cbx_evento As System.Windows.Forms.ComboBox
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
    Friend WithEvents lb_titulo As System.Windows.Forms.Label
    Friend WithEvents lb_art As System.Windows.Forms.Label
    Friend WithEvents txt_art As System.Windows.Forms.TextBox
    Friend WithEvents btn_add As System.Windows.Forms.Button
    Friend WithEvents dtg_art As System.Windows.Forms.DataGridView
    Friend WithEvents lb_reg As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
