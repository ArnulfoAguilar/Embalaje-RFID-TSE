<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rol
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Rol))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_upd_rol = New System.Windows.Forms.Button()
        Me.btn_delete_rol = New System.Windows.Forms.Button()
        Me.btn_Add_Rol = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(29, 173)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(393, 236)
        Me.DataGridView1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(160, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(135, 34)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Lista de Roles"
        '
        'btn_upd_rol
        '
        Me.btn_upd_rol.Image = CType(resources.GetObject("btn_upd_rol.Image"), System.Drawing.Image)
        Me.btn_upd_rol.Location = New System.Drawing.Point(196, 89)
        Me.btn_upd_rol.Name = "btn_upd_rol"
        Me.btn_upd_rol.Size = New System.Drawing.Size(59, 60)
        Me.btn_upd_rol.TabIndex = 10
        Me.btn_upd_rol.UseVisualStyleBackColor = True
        '
        'btn_delete_rol
        '
        Me.btn_delete_rol.Image = CType(resources.GetObject("btn_delete_rol.Image"), System.Drawing.Image)
        Me.btn_delete_rol.Location = New System.Drawing.Point(350, 89)
        Me.btn_delete_rol.Name = "btn_delete_rol"
        Me.btn_delete_rol.Size = New System.Drawing.Size(57, 60)
        Me.btn_delete_rol.TabIndex = 9
        Me.btn_delete_rol.UseVisualStyleBackColor = True
        '
        'btn_Add_Rol
        '
        Me.btn_Add_Rol.Image = CType(resources.GetObject("btn_Add_Rol.Image"), System.Drawing.Image)
        Me.btn_Add_Rol.Location = New System.Drawing.Point(44, 89)
        Me.btn_Add_Rol.Name = "btn_Add_Rol"
        Me.btn_Add_Rol.Size = New System.Drawing.Size(59, 60)
        Me.btn_Add_Rol.TabIndex = 8
        Me.btn_Add_Rol.UseVisualStyleBackColor = True
        '
        'Rol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 433)
        Me.Controls.Add(Me.btn_upd_rol)
        Me.Controls.Add(Me.btn_delete_rol)
        Me.Controls.Add(Me.btn_Add_Rol)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Rol"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Rol"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_upd_rol As System.Windows.Forms.Button
    Friend WithEvents btn_delete_rol As System.Windows.Forms.Button
    Friend WithEvents btn_Add_Rol As System.Windows.Forms.Button
End Class
