<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Mtn_Articulo
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_numcaja = New System.Windows.Forms.TextBox()
        Me.rbt_numcaja = New System.Windows.Forms.RadioButton()
        Me.rb_busq = New System.Windows.Forms.RadioButton()
        Me.cbx_depto = New System.Windows.Forms.ComboBox()
        Me.cbx_muni = New System.Windows.Forms.ComboBox()
        Me.cbx_cv = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtg_Art = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.dtg_Art, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(277, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(278, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "INGRESO DE ARTICULOS EN CAJA"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(348, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "MANUELMENTE"
        '
        'txt_numcaja
        '
        Me.txt_numcaja.Location = New System.Drawing.Point(158, 77)
        Me.txt_numcaja.MaxLength = 4
        Me.txt_numcaja.Name = "txt_numcaja"
        Me.txt_numcaja.Size = New System.Drawing.Size(53, 20)
        Me.txt_numcaja.TabIndex = 3
        '
        'rbt_numcaja
        '
        Me.rbt_numcaja.AutoSize = True
        Me.rbt_numcaja.Location = New System.Drawing.Point(51, 77)
        Me.rbt_numcaja.Name = "rbt_numcaja"
        Me.rbt_numcaja.Size = New System.Drawing.Size(101, 17)
        Me.rbt_numcaja.TabIndex = 4
        Me.rbt_numcaja.TabStop = True
        Me.rbt_numcaja.Text = "Numero de Caja"
        Me.rbt_numcaja.UseVisualStyleBackColor = True
        '
        'rb_busq
        '
        Me.rb_busq.AutoSize = True
        Me.rb_busq.Location = New System.Drawing.Point(281, 79)
        Me.rb_busq.Name = "rb_busq"
        Me.rb_busq.Size = New System.Drawing.Size(73, 17)
        Me.rb_busq.TabIndex = 5
        Me.rb_busq.TabStop = True
        Me.rb_busq.Text = "Busqueda"
        Me.rb_busq.UseVisualStyleBackColor = True
        '
        'cbx_depto
        '
        Me.cbx_depto.FormattingEnabled = True
        Me.cbx_depto.Location = New System.Drawing.Point(511, 77)
        Me.cbx_depto.Name = "cbx_depto"
        Me.cbx_depto.Size = New System.Drawing.Size(121, 21)
        Me.cbx_depto.TabIndex = 6
        '
        'cbx_muni
        '
        Me.cbx_muni.FormattingEnabled = True
        Me.cbx_muni.Location = New System.Drawing.Point(371, 114)
        Me.cbx_muni.Name = "cbx_muni"
        Me.cbx_muni.Size = New System.Drawing.Size(121, 21)
        Me.cbx_muni.TabIndex = 7
        '
        'cbx_cv
        '
        Me.cbx_cv.FormattingEnabled = True
        Me.cbx_cv.Location = New System.Drawing.Point(656, 114)
        Me.cbx_cv.Name = "cbx_cv"
        Me.cbx_cv.Size = New System.Drawing.Size(121, 21)
        Me.cbx_cv.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(425, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Departamento"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(299, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Municipio"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(552, 117)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Centro de Votacion"
        '
        'dtg_Art
        '
        Me.dtg_Art.AllowUserToAddRows = False
        Me.dtg_Art.AllowUserToDeleteRows = False
        Me.dtg_Art.AllowUserToResizeColumns = False
        Me.dtg_Art.AllowUserToResizeRows = False
        Me.dtg_Art.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dtg_Art.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtg_Art.Location = New System.Drawing.Point(51, 152)
        Me.dtg_Art.Name = "dtg_Art"
        Me.dtg_Art.Size = New System.Drawing.Size(321, 276)
        Me.dtg_Art.TabIndex = 12
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(428, 176)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 30)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Buscar Caja"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Frm_Mtn_Articulo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 486)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dtg_Art)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbx_cv)
        Me.Controls.Add(Me.cbx_muni)
        Me.Controls.Add(Me.cbx_depto)
        Me.Controls.Add(Me.rb_busq)
        Me.Controls.Add(Me.rbt_numcaja)
        Me.Controls.Add(Me.txt_numcaja)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_Mtn_Articulo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_Mtn_Articulo"
        CType(Me.dtg_Art, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_numcaja As System.Windows.Forms.TextBox
    Friend WithEvents rbt_numcaja As System.Windows.Forms.RadioButton
    Friend WithEvents rb_busq As System.Windows.Forms.RadioButton
    Friend WithEvents cbx_depto As System.Windows.Forms.ComboBox
    Friend WithEvents cbx_muni As System.Windows.Forms.ComboBox
    Friend WithEvents cbx_cv As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtg_Art As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
