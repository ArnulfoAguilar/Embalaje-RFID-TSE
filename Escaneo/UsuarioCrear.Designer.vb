<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UsuarioCrear
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.username_txt = New System.Windows.Forms.Label
        Me.nombre_user_txt = New System.Windows.Forms.Label
        Me.id_txt = New System.Windows.Forms.TextBox
        Me.TextBox_nombre = New System.Windows.Forms.TextBox
        Me.TextBox_username = New System.Windows.Forms.TextBox
        Me.password_username_txt = New System.Windows.Forms.Label
        Me.TextBox_password = New System.Windows.Forms.TextBox
        Me.Aceptar_btn = New System.Windows.Forms.Button
        Me.Cancelar_btn = New System.Windows.Forms.Button
        Me.rol = New System.Windows.Forms.Label
        Me.TextBox_ipreader = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(67, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'username_txt
        '
        Me.username_txt.AutoSize = True
        Me.username_txt.Location = New System.Drawing.Point(67, 118)
        Me.username_txt.Name = "username_txt"
        Me.username_txt.Size = New System.Drawing.Size(55, 13)
        Me.username_txt.TabIndex = 1
        Me.username_txt.Text = "Username"
        '
        'nombre_user_txt
        '
        Me.nombre_user_txt.AutoSize = True
        Me.nombre_user_txt.Location = New System.Drawing.Point(67, 77)
        Me.nombre_user_txt.Name = "nombre_user_txt"
        Me.nombre_user_txt.Size = New System.Drawing.Size(44, 13)
        Me.nombre_user_txt.TabIndex = 2
        Me.nombre_user_txt.Text = "Nombre"
        '
        'id_txt
        '
        Me.id_txt.Location = New System.Drawing.Point(141, 32)
        Me.id_txt.Name = "id_txt"
        Me.id_txt.Size = New System.Drawing.Size(100, 20)
        Me.id_txt.TabIndex = 3
        '
        'TextBox_nombre
        '
        Me.TextBox_nombre.Location = New System.Drawing.Point(141, 70)
        Me.TextBox_nombre.Name = "TextBox_nombre"
        Me.TextBox_nombre.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_nombre.TabIndex = 4
        '
        'TextBox_username
        '
        Me.TextBox_username.Location = New System.Drawing.Point(141, 118)
        Me.TextBox_username.Name = "TextBox_username"
        Me.TextBox_username.Size = New System.Drawing.Size(100, 20)
        Me.TextBox_username.TabIndex = 5
        '
        'password_username_txt
        '
        Me.password_username_txt.AutoSize = True
        Me.password_username_txt.Location = New System.Drawing.Point(301, 42)
        Me.password_username_txt.Name = "password_username_txt"
        Me.password_username_txt.Size = New System.Drawing.Size(52, 13)
        Me.password_username_txt.TabIndex = 6
        Me.password_username_txt.Text = "password"
        '
        'TextBox_password
        '
        Me.TextBox_password.Location = New System.Drawing.Point(370, 35)
        Me.TextBox_password.Name = "TextBox_password"
        Me.TextBox_password.Size = New System.Drawing.Size(121, 20)
        Me.TextBox_password.TabIndex = 7
        '
        'Aceptar_btn
        '
        Me.Aceptar_btn.Location = New System.Drawing.Point(292, 222)
        Me.Aceptar_btn.Name = "Aceptar_btn"
        Me.Aceptar_btn.Size = New System.Drawing.Size(75, 23)
        Me.Aceptar_btn.TabIndex = 8
        Me.Aceptar_btn.Text = "Aceptar"
        Me.Aceptar_btn.UseVisualStyleBackColor = True
        '
        'Cancelar_btn
        '
        Me.Cancelar_btn.Location = New System.Drawing.Point(457, 222)
        Me.Cancelar_btn.Name = "Cancelar_btn"
        Me.Cancelar_btn.Size = New System.Drawing.Size(75, 23)
        Me.Cancelar_btn.TabIndex = 10
        Me.Cancelar_btn.Text = "Cancelar"
        Me.Cancelar_btn.UseVisualStyleBackColor = True
        '
        'rol
        '
        Me.rol.AutoSize = True
        Me.rol.Location = New System.Drawing.Point(301, 80)
        Me.rol.Name = "rol"
        Me.rol.Size = New System.Drawing.Size(23, 13)
        Me.rol.TabIndex = 11
        Me.rol.Text = "Rol"
        '
        'TextBox_ipreader
        '
        Me.TextBox_ipreader.Location = New System.Drawing.Point(370, 118)
        Me.TextBox_ipreader.Name = "TextBox_ipreader"
        Me.TextBox_ipreader.Size = New System.Drawing.Size(121, 20)
        Me.TextBox_ipreader.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(301, 118)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "IP Reader"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(370, 77)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 15
        '
        'UsuarioCrear
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 283)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox_ipreader)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.rol)
        Me.Controls.Add(Me.Cancelar_btn)
        Me.Controls.Add(Me.Aceptar_btn)
        Me.Controls.Add(Me.TextBox_password)
        Me.Controls.Add(Me.password_username_txt)
        Me.Controls.Add(Me.TextBox_username)
        Me.Controls.Add(Me.TextBox_nombre)
        Me.Controls.Add(Me.id_txt)
        Me.Controls.Add(Me.nombre_user_txt)
        Me.Controls.Add(Me.username_txt)
        Me.Controls.Add(Me.Label1)
        Me.Name = "UsuarioCrear"
        Me.Text = "UsuarioCrear"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents username_txt As System.Windows.Forms.Label
    Friend WithEvents nombre_user_txt As System.Windows.Forms.Label
    Friend WithEvents id_txt As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_nombre As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_username As System.Windows.Forms.TextBox
    Friend WithEvents password_username_txt As System.Windows.Forms.Label
    Friend WithEvents TextBox_password As System.Windows.Forms.TextBox
    Friend WithEvents Aceptar_btn As System.Windows.Forms.Button
    Friend WithEvents Cancelar_btn As System.Windows.Forms.Button
    Friend WithEvents rol As System.Windows.Forms.Label
    Friend WithEvents TextBox_ipreader As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
End Class
