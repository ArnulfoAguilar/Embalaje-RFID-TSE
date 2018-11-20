<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Imprimir
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
        Me.comboArticulo = New System.Windows.Forms.ComboBox()
        Me.comboSede = New System.Windows.Forms.ComboBox()
        Me.lbl_articulo = New System.Windows.Forms.Label()
        Me.lbl_sede = New System.Windows.Forms.Label()
        Me.btn_Excel = New System.Windows.Forms.Button()
        Me.btn_cancelar = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rb_individual = New System.Windows.Forms.RadioButton()
        Me.rb_caja = New System.Windows.Forms.RadioButton()
        Me.rb_articulo = New System.Windows.Forms.RadioButton()
        Me.txt_caja = New System.Windows.Forms.TextBox()
        Me.lbl_caja = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'comboArticulo
        '
        Me.comboArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboArticulo.FormattingEnabled = True
        Me.comboArticulo.Location = New System.Drawing.Point(48, 185)
        Me.comboArticulo.Name = "comboArticulo"
        Me.comboArticulo.Size = New System.Drawing.Size(158, 21)
        Me.comboArticulo.TabIndex = 0
        '
        'comboSede
        '
        Me.comboSede.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboSede.FormattingEnabled = True
        Me.comboSede.Location = New System.Drawing.Point(291, 185)
        Me.comboSede.Name = "comboSede"
        Me.comboSede.Size = New System.Drawing.Size(156, 21)
        Me.comboSede.TabIndex = 1
        '
        'lbl_articulo
        '
        Me.lbl_articulo.AutoSize = True
        Me.lbl_articulo.Location = New System.Drawing.Point(45, 169)
        Me.lbl_articulo.Name = "lbl_articulo"
        Me.lbl_articulo.Size = New System.Drawing.Size(42, 13)
        Me.lbl_articulo.TabIndex = 2
        Me.lbl_articulo.Text = "Articulo"
        '
        'lbl_sede
        '
        Me.lbl_sede.AutoSize = True
        Me.lbl_sede.Location = New System.Drawing.Point(288, 169)
        Me.lbl_sede.Name = "lbl_sede"
        Me.lbl_sede.Size = New System.Drawing.Size(77, 13)
        Me.lbl_sede.TabIndex = 3
        Me.lbl_sede.Text = "Sede Logistica"
        '
        'btn_Excel
        '
        Me.btn_Excel.Location = New System.Drawing.Point(145, 272)
        Me.btn_Excel.Name = "btn_Excel"
        Me.btn_Excel.Size = New System.Drawing.Size(75, 48)
        Me.btn_Excel.TabIndex = 4
        Me.btn_Excel.Text = "Generar Excel"
        Me.btn_Excel.UseVisualStyleBackColor = True
        '
        'btn_cancelar
        '
        Me.btn_cancelar.Location = New System.Drawing.Point(291, 272)
        Me.btn_cancelar.Name = "btn_cancelar"
        Me.btn_cancelar.Size = New System.Drawing.Size(75, 48)
        Me.btn_cancelar.TabIndex = 5
        Me.btn_cancelar.Text = "Cancelar"
        Me.btn_cancelar.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rb_individual)
        Me.GroupBox1.Controls.Add(Me.rb_caja)
        Me.GroupBox1.Controls.Add(Me.rb_articulo)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(462, 61)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tipo de Impresion"
        '
        'rb_individual
        '
        Me.rb_individual.AutoSize = True
        Me.rb_individual.Location = New System.Drawing.Point(338, 29)
        Me.rb_individual.Name = "rb_individual"
        Me.rb_individual.Size = New System.Drawing.Size(107, 17)
        Me.rb_individual.TabIndex = 2
        Me.rb_individual.TabStop = True
        Me.rb_individual.Text = "Articulo individual"
        Me.rb_individual.UseVisualStyleBackColor = True
        '
        'rb_caja
        '
        Me.rb_caja.AutoSize = True
        Me.rb_caja.Location = New System.Drawing.Point(177, 29)
        Me.rb_caja.Name = "rb_caja"
        Me.rb_caja.Size = New System.Drawing.Size(92, 17)
        Me.rb_caja.TabIndex = 1
        Me.rb_caja.TabStop = True
        Me.rb_caja.Text = "Caja completa"
        Me.rb_caja.UseVisualStyleBackColor = True
        '
        'rb_articulo
        '
        Me.rb_articulo.AutoSize = True
        Me.rb_articulo.Location = New System.Drawing.Point(28, 29)
        Me.rb_articulo.Name = "rb_articulo"
        Me.rb_articulo.Size = New System.Drawing.Size(79, 17)
        Me.rb_articulo.TabIndex = 0
        Me.rb_articulo.TabStop = True
        Me.rb_articulo.Text = "Por Articulo"
        Me.rb_articulo.UseVisualStyleBackColor = True
        '
        'txt_caja
        '
        Me.txt_caja.Location = New System.Drawing.Point(48, 126)
        Me.txt_caja.Name = "txt_caja"
        Me.txt_caja.Size = New System.Drawing.Size(100, 20)
        Me.txt_caja.TabIndex = 7
        '
        'lbl_caja
        '
        Me.lbl_caja.AutoSize = True
        Me.lbl_caja.Location = New System.Drawing.Point(45, 110)
        Me.lbl_caja.Name = "lbl_caja"
        Me.lbl_caja.Size = New System.Drawing.Size(82, 13)
        Me.lbl_caja.TabIndex = 8
        Me.lbl_caja.Text = "Numero de caja"
        '
        'Imprimir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 332)
        Me.Controls.Add(Me.lbl_caja)
        Me.Controls.Add(Me.txt_caja)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btn_cancelar)
        Me.Controls.Add(Me.btn_Excel)
        Me.Controls.Add(Me.lbl_sede)
        Me.Controls.Add(Me.lbl_articulo)
        Me.Controls.Add(Me.comboSede)
        Me.Controls.Add(Me.comboArticulo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Imprimir"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Imprimir"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents comboArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents comboSede As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_articulo As System.Windows.Forms.Label
    Friend WithEvents lbl_sede As System.Windows.Forms.Label
    Friend WithEvents btn_Excel As System.Windows.Forms.Button
    Friend WithEvents btn_cancelar As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_individual As System.Windows.Forms.RadioButton
    Friend WithEvents rb_caja As System.Windows.Forms.RadioButton
    Friend WithEvents rb_articulo As System.Windows.Forms.RadioButton
    Friend WithEvents txt_caja As System.Windows.Forms.TextBox
    Friend WithEvents lbl_caja As System.Windows.Forms.Label
End Class
