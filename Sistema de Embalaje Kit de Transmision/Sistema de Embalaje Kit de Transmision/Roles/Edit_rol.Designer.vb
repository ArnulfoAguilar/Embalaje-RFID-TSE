<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Edit_rol
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
        Me.btn_cancelar = New System.Windows.Forms.Button()
        Me.DGView_Roles = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_rol = New System.Windows.Forms.TextBox()
        Me.btn_Ingresar = New System.Windows.Forms.Button()
        CType(Me.DGView_Roles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_cancelar
        '
        Me.btn_cancelar.Location = New System.Drawing.Point(34, 364)
        Me.btn_cancelar.Name = "btn_cancelar"
        Me.btn_cancelar.Size = New System.Drawing.Size(75, 23)
        Me.btn_cancelar.TabIndex = 11
        Me.btn_cancelar.Text = "Cancelar"
        Me.btn_cancelar.UseVisualStyleBackColor = True
        '
        'DGView_Roles
        '
        Me.DGView_Roles.AllowUserToAddRows = False
        Me.DGView_Roles.AllowUserToDeleteRows = False
        Me.DGView_Roles.AllowUserToResizeColumns = False
        Me.DGView_Roles.AllowUserToResizeRows = False
        Me.DGView_Roles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGView_Roles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView_Roles.Location = New System.Drawing.Point(34, 58)
        Me.DGView_Roles.Name = "DGView_Roles"
        Me.DGView_Roles.Size = New System.Drawing.Size(310, 226)
        Me.DGView_Roles.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 304)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Rol"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(73, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 25)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Modificar Rol"
        '
        'txt_rol
        '
        Me.txt_rol.Location = New System.Drawing.Point(34, 323)
        Me.txt_rol.Name = "txt_rol"
        Me.txt_rol.Size = New System.Drawing.Size(199, 20)
        Me.txt_rol.TabIndex = 7
        '
        'btn_Ingresar
        '
        Me.btn_Ingresar.Location = New System.Drawing.Point(158, 364)
        Me.btn_Ingresar.Name = "btn_Ingresar"
        Me.btn_Ingresar.Size = New System.Drawing.Size(75, 23)
        Me.btn_Ingresar.TabIndex = 6
        Me.btn_Ingresar.Text = "Modificar"
        Me.btn_Ingresar.UseVisualStyleBackColor = True
        '
        'Edit_rol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(356, 407)
        Me.Controls.Add(Me.btn_cancelar)
        Me.Controls.Add(Me.DGView_Roles)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_rol)
        Me.Controls.Add(Me.btn_Ingresar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Edit_rol"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Modificar Rol"
        CType(Me.DGView_Roles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_cancelar As System.Windows.Forms.Button
    Friend WithEvents DGView_Roles As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_rol As System.Windows.Forms.TextBox
    Friend WithEvents btn_Ingresar As System.Windows.Forms.Button
End Class
