<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Evento
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Evento))
        Me.dtg_Evento = New System.Windows.Forms.DataGridView()
        Me.btn_elim = New System.Windows.Forms.Button()
        Me.btn_editar = New System.Windows.Forms.Button()
        Me.btn_nuevo = New System.Windows.Forms.Button()
        Me.lb_reg = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dtg_Evento, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtg_Evento
        '
        Me.dtg_Evento.AllowUserToAddRows = False
        Me.dtg_Evento.AllowUserToDeleteRows = False
        Me.dtg_Evento.AllowUserToResizeColumns = False
        Me.dtg_Evento.AllowUserToResizeRows = False
        Me.dtg_Evento.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dtg_Evento.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders
        Me.dtg_Evento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtg_Evento.Location = New System.Drawing.Point(29, 130)
        Me.dtg_Evento.Name = "dtg_Evento"
        Me.dtg_Evento.ReadOnly = True
        Me.dtg_Evento.Size = New System.Drawing.Size(481, 281)
        Me.dtg_Evento.TabIndex = 0
        '
        'btn_elim
        '
        Me.btn_elim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btn_elim.Image = CType(resources.GetObject("btn_elim.Image"), System.Drawing.Image)
        Me.btn_elim.Location = New System.Drawing.Point(212, 42)
        Me.btn_elim.Name = "btn_elim"
        Me.btn_elim.Size = New System.Drawing.Size(89, 79)
        Me.btn_elim.TabIndex = 8
        Me.btn_elim.Text = "Eliminar"
        Me.btn_elim.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_elim.UseVisualStyleBackColor = True
        '
        'btn_editar
        '
        Me.btn_editar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_editar.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.btn_editar.Image = CType(resources.GetObject("btn_editar.Image"), System.Drawing.Image)
        Me.btn_editar.Location = New System.Drawing.Point(324, 42)
        Me.btn_editar.Name = "btn_editar"
        Me.btn_editar.Size = New System.Drawing.Size(89, 79)
        Me.btn_editar.TabIndex = 7
        Me.btn_editar.Text = "Editar"
        Me.btn_editar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_editar.UseVisualStyleBackColor = True
        '
        'btn_nuevo
        '
        Me.btn_nuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_nuevo.Image = CType(resources.GetObject("btn_nuevo.Image"), System.Drawing.Image)
        Me.btn_nuevo.Location = New System.Drawing.Point(100, 42)
        Me.btn_nuevo.Name = "btn_nuevo"
        Me.btn_nuevo.Size = New System.Drawing.Size(89, 79)
        Me.btn_nuevo.TabIndex = 6
        Me.btn_nuevo.Text = "Nuevo"
        Me.btn_nuevo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_nuevo.UseVisualStyleBackColor = True
        '
        'lb_reg
        '
        Me.lb_reg.AutoSize = True
        Me.lb_reg.Location = New System.Drawing.Point(156, 414)
        Me.lb_reg.Name = "lb_reg"
        Me.lb_reg.Size = New System.Drawing.Size(46, 13)
        Me.lb_reg.TabIndex = 10
        Me.lb_reg.Text = "registros"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 414)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Registros Encontrados"
        '
        'Frm_Evento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 442)
        Me.Controls.Add(Me.lb_reg)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btn_elim)
        Me.Controls.Add(Me.btn_editar)
        Me.Controls.Add(Me.btn_nuevo)
        Me.Controls.Add(Me.dtg_Evento)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Frm_Evento"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Eventos"
        CType(Me.dtg_Evento, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtg_Evento As System.Windows.Forms.DataGridView
    Friend WithEvents btn_elim As System.Windows.Forms.Button
    Friend WithEvents btn_editar As System.Windows.Forms.Button
    Friend WithEvents btn_nuevo As System.Windows.Forms.Button
    Friend WithEvents lb_reg As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
