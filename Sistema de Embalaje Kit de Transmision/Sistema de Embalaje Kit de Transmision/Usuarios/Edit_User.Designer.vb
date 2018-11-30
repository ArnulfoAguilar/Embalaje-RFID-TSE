<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Edit_User
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DGView_Usuarios = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txt_nombre = New System.Windows.Forms.TextBox()
        Me.txt_contraseña = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.modificar_txt = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Combo_rol = New System.Windows.Forms.ComboBox()
        CType(Me.DGView_Usuarios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(204, 490)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(79, 32)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Modificar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DGView_Usuarios
        '
        Me.DGView_Usuarios.AllowUserToAddRows = False
        Me.DGView_Usuarios.AllowUserToDeleteRows = False
        Me.DGView_Usuarios.AllowUserToResizeColumns = False
        Me.DGView_Usuarios.AllowUserToResizeRows = False
        Me.DGView_Usuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGView_Usuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView_Usuarios.Location = New System.Drawing.Point(34, 48)
        Me.DGView_Usuarios.Name = "DGView_Usuarios"
        Me.DGView_Usuarios.Size = New System.Drawing.Size(337, 296)
        Me.DGView_Usuarios.TabIndex = 1
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(292, 490)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(79, 32)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Cancelar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txt_nombre
        '
        Me.txt_nombre.Location = New System.Drawing.Point(31, 390)
        Me.txt_nombre.Name = "txt_nombre"
        Me.txt_nombre.Size = New System.Drawing.Size(168, 20)
        Me.txt_nombre.TabIndex = 3
        '
        'txt_contraseña
        '
        Me.txt_contraseña.Location = New System.Drawing.Point(34, 440)
        Me.txt_contraseña.Name = "txt_contraseña"
        Me.txt_contraseña.Size = New System.Drawing.Size(165, 20)
        Me.txt_contraseña.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 372)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Nombre"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 424)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Contraseña"
        '
        'modificar_txt
        '
        Me.modificar_txt.AutoSize = True
        Me.modificar_txt.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.modificar_txt.Location = New System.Drawing.Point(138, 9)
        Me.modificar_txt.Name = "modificar_txt"
        Me.modificar_txt.Size = New System.Drawing.Size(134, 25)
        Me.modificar_txt.TabIndex = 7
        Me.modificar_txt.Text = "Editar Usuario"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(228, 372)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Rol"
        '
        'Combo_rol
        '
        Me.Combo_rol.FormattingEnabled = True
        Me.Combo_rol.Location = New System.Drawing.Point(231, 388)
        Me.Combo_rol.Name = "Combo_rol"
        Me.Combo_rol.Size = New System.Drawing.Size(121, 21)
        Me.Combo_rol.TabIndex = 9
        '
        'Edit_User
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 534)
        Me.Controls.Add(Me.Combo_rol)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.modificar_txt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_contraseña)
        Me.Controls.Add(Me.txt_nombre)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.DGView_Usuarios)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Edit_User"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Editar Usuario"
        CType(Me.DGView_Usuarios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DGView_Usuarios As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txt_nombre As System.Windows.Forms.TextBox
    Friend WithEvents txt_contraseña As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents modificar_txt As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Combo_rol As System.Windows.Forms.ComboBox
End Class
