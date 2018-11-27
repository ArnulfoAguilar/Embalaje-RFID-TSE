<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Paquete_Edit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Paquete_Edit))
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.cbx_evento = New System.Windows.Forms.ComboBox()
        Me.txt_nombre = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lb_reg = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtg_art = New System.Windows.Forms.DataGridView()
        Me.btn_add = New System.Windows.Forms.Button()
        Me.txt_art = New System.Windows.Forms.TextBox()
        Me.lb_art = New System.Windows.Forms.Label()
        Me.lb_titulo = New System.Windows.Forms.Label()
        Me.btn_elim = New System.Windows.Forms.Button()
        Me.btn_cancelar = New System.Windows.Forms.Button()
        CType(Me.dtg_art, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_guardar
        '
        Me.btn_guardar.Image = CType(resources.GetObject("btn_guardar.Image"), System.Drawing.Image)
        Me.btn_guardar.Location = New System.Drawing.Point(553, 24)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(83, 81)
        Me.btn_guardar.TabIndex = 9
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'cbx_evento
        '
        Me.cbx_evento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_evento.FormattingEnabled = True
        Me.cbx_evento.Location = New System.Drawing.Point(155, 75)
        Me.cbx_evento.Name = "cbx_evento"
        Me.cbx_evento.Size = New System.Drawing.Size(209, 21)
        Me.cbx_evento.TabIndex = 8
        '
        'txt_nombre
        '
        Me.txt_nombre.Location = New System.Drawing.Point(155, 37)
        Me.txt_nombre.Name = "txt_nombre"
        Me.txt_nombre.Size = New System.Drawing.Size(209, 20)
        Me.txt_nombre.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(42, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Elija el Evento"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Nombre del Paquete"
        '
        'lb_reg
        '
        Me.lb_reg.AutoSize = True
        Me.lb_reg.Location = New System.Drawing.Point(189, 454)
        Me.lb_reg.Name = "lb_reg"
        Me.lb_reg.Size = New System.Drawing.Size(46, 13)
        Me.lb_reg.TabIndex = 22
        Me.lb_reg.Text = "registros"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(59, 454)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Registros Encontrados"
        '
        'dtg_art
        '
        Me.dtg_art.AllowUserToAddRows = False
        Me.dtg_art.AllowUserToDeleteRows = False
        Me.dtg_art.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dtg_art.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtg_art.Location = New System.Drawing.Point(62, 241)
        Me.dtg_art.Name = "dtg_art"
        Me.dtg_art.Size = New System.Drawing.Size(467, 213)
        Me.dtg_art.TabIndex = 20
        '
        'btn_add
        '
        Me.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_add.Image = CType(resources.GetObject("btn_add.Image"), System.Drawing.Image)
        Me.btn_add.Location = New System.Drawing.Point(553, 173)
        Me.btn_add.Name = "btn_add"
        Me.btn_add.Size = New System.Drawing.Size(83, 83)
        Me.btn_add.TabIndex = 19
        Me.btn_add.Text = "Añadir"
        Me.btn_add.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_add.UseVisualStyleBackColor = True
        '
        'txt_art
        '
        Me.txt_art.Location = New System.Drawing.Point(155, 201)
        Me.txt_art.Name = "txt_art"
        Me.txt_art.Size = New System.Drawing.Size(209, 20)
        Me.txt_art.TabIndex = 18
        '
        'lb_art
        '
        Me.lb_art.AutoSize = True
        Me.lb_art.Location = New System.Drawing.Point(59, 209)
        Me.lb_art.Name = "lb_art"
        Me.lb_art.Size = New System.Drawing.Size(42, 13)
        Me.lb_art.TabIndex = 17
        Me.lb_art.Text = "Articulo"
        '
        'lb_titulo
        '
        Me.lb_titulo.AutoSize = True
        Me.lb_titulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_titulo.Location = New System.Drawing.Point(139, 145)
        Me.lb_titulo.Name = "lb_titulo"
        Me.lb_titulo.Size = New System.Drawing.Size(379, 25)
        Me.lb_titulo.TabIndex = 16
        Me.lb_titulo.Text = "AGREGAR ARTICULOS AL PAQUETE"
        '
        'btn_elim
        '
        Me.btn_elim.Image = CType(resources.GetObject("btn_elim.Image"), System.Drawing.Image)
        Me.btn_elim.Location = New System.Drawing.Point(553, 301)
        Me.btn_elim.Name = "btn_elim"
        Me.btn_elim.Size = New System.Drawing.Size(83, 81)
        Me.btn_elim.TabIndex = 23
        Me.btn_elim.Text = "eliminar"
        Me.btn_elim.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_elim.UseVisualStyleBackColor = True
        '
        'btn_cancelar
        '
        Me.btn_cancelar.Location = New System.Drawing.Point(553, 430)
        Me.btn_cancelar.Name = "btn_cancelar"
        Me.btn_cancelar.Size = New System.Drawing.Size(83, 23)
        Me.btn_cancelar.TabIndex = 24
        Me.btn_cancelar.Text = "Salir"
        Me.btn_cancelar.UseVisualStyleBackColor = True
        '
        'Frm_Paquete_Edit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 476)
        Me.Controls.Add(Me.btn_cancelar)
        Me.Controls.Add(Me.btn_elim)
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
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Frm_Paquete_Edit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Editar Paquete"
        CType(Me.dtg_art, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
    Friend WithEvents cbx_evento As System.Windows.Forms.ComboBox
    Friend WithEvents txt_nombre As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lb_reg As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtg_art As System.Windows.Forms.DataGridView
    Friend WithEvents btn_add As System.Windows.Forms.Button
    Friend WithEvents txt_art As System.Windows.Forms.TextBox
    Friend WithEvents lb_art As System.Windows.Forms.Label
    Friend WithEvents lb_titulo As System.Windows.Forms.Label
    Friend WithEvents btn_elim As System.Windows.Forms.Button
    Friend WithEvents btn_cancelar As System.Windows.Forms.Button
End Class
