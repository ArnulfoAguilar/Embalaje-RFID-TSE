<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Ubicaciones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Ubicaciones))
        Me.btn_agregar = New System.Windows.Forms.Button()
        Me.btn_modificar = New System.Windows.Forms.Button()
        Me.DGView_ubicaciones = New System.Windows.Forms.DataGridView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.InicioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EscaneoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DespachoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecepcionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MantenimientosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsuariosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CrearUsuarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModificarUsuarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarUsuarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SedesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CrearSedeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModificarSedeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarSedeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CrearProductoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModificarProductoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarProductoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CargarDatosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DGView_ubicaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_agregar
        '
        Me.btn_agregar.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.btn_agregar.Image = CType(resources.GetObject("btn_agregar.Image"), System.Drawing.Image)
        Me.btn_agregar.Location = New System.Drawing.Point(128, 95)
        Me.btn_agregar.Name = "btn_agregar"
        Me.btn_agregar.Size = New System.Drawing.Size(60, 56)
        Me.btn_agregar.TabIndex = 0
        Me.btn_agregar.UseVisualStyleBackColor = True
        '
        'btn_modificar
        '
        Me.btn_modificar.Image = CType(resources.GetObject("btn_modificar.Image"), System.Drawing.Image)
        Me.btn_modificar.Location = New System.Drawing.Point(299, 96)
        Me.btn_modificar.Name = "btn_modificar"
        Me.btn_modificar.Size = New System.Drawing.Size(58, 55)
        Me.btn_modificar.TabIndex = 1
        Me.btn_modificar.UseVisualStyleBackColor = True
        '
        'DGView_ubicaciones
        '
        Me.DGView_ubicaciones.AllowUserToAddRows = False
        Me.DGView_ubicaciones.AllowUserToDeleteRows = False
        Me.DGView_ubicaciones.AllowUserToResizeColumns = False
        Me.DGView_ubicaciones.AllowUserToResizeRows = False
        Me.DGView_ubicaciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGView_ubicaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGView_ubicaciones.Location = New System.Drawing.Point(23, 157)
        Me.DGView_ubicaciones.Name = "DGView_ubicaciones"
        Me.DGView_ubicaciones.Size = New System.Drawing.Size(400, 224)
        Me.DGView_ubicaciones.TabIndex = 3
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InicioToolStripMenuItem, Me.EscaneoToolStripMenuItem, Me.DespachoToolStripMenuItem, Me.RecepcionToolStripMenuItem, Me.MantenimientosToolStripMenuItem, Me.CargarDatosToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(459, 24)
        Me.MenuStrip1.TabIndex = 8
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'InicioToolStripMenuItem
        '
        Me.InicioToolStripMenuItem.Name = "InicioToolStripMenuItem"
        Me.InicioToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.InicioToolStripMenuItem.Text = "Inicio"
        '
        'EscaneoToolStripMenuItem
        '
        Me.EscaneoToolStripMenuItem.Name = "EscaneoToolStripMenuItem"
        Me.EscaneoToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.EscaneoToolStripMenuItem.Text = "Escaneo"
        '
        'DespachoToolStripMenuItem
        '
        Me.DespachoToolStripMenuItem.Name = "DespachoToolStripMenuItem"
        Me.DespachoToolStripMenuItem.Size = New System.Drawing.Size(71, 20)
        Me.DespachoToolStripMenuItem.Text = "Despacho"
        '
        'RecepcionToolStripMenuItem
        '
        Me.RecepcionToolStripMenuItem.Name = "RecepcionToolStripMenuItem"
        Me.RecepcionToolStripMenuItem.Size = New System.Drawing.Size(74, 20)
        Me.RecepcionToolStripMenuItem.Text = "Recepcion"
        '
        'MantenimientosToolStripMenuItem
        '
        Me.MantenimientosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UsuariosToolStripMenuItem, Me.SedesToolStripMenuItem, Me.ProductosToolStripMenuItem})
        Me.MantenimientosToolStripMenuItem.Name = "MantenimientosToolStripMenuItem"
        Me.MantenimientosToolStripMenuItem.Size = New System.Drawing.Size(106, 20)
        Me.MantenimientosToolStripMenuItem.Text = "Mantenimientos"
        '
        'UsuariosToolStripMenuItem
        '
        Me.UsuariosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CrearUsuarioToolStripMenuItem, Me.ModificarUsuarioToolStripMenuItem, Me.EliminarUsuarioToolStripMenuItem})
        Me.UsuariosToolStripMenuItem.Name = "UsuariosToolStripMenuItem"
        Me.UsuariosToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.UsuariosToolStripMenuItem.Text = "Usuarios"
        '
        'CrearUsuarioToolStripMenuItem
        '
        Me.CrearUsuarioToolStripMenuItem.Name = "CrearUsuarioToolStripMenuItem"
        Me.CrearUsuarioToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.CrearUsuarioToolStripMenuItem.Text = "Crear usuario"
        '
        'ModificarUsuarioToolStripMenuItem
        '
        Me.ModificarUsuarioToolStripMenuItem.Name = "ModificarUsuarioToolStripMenuItem"
        Me.ModificarUsuarioToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.ModificarUsuarioToolStripMenuItem.Text = "Modificar usuario"
        '
        'EliminarUsuarioToolStripMenuItem
        '
        Me.EliminarUsuarioToolStripMenuItem.Name = "EliminarUsuarioToolStripMenuItem"
        Me.EliminarUsuarioToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.EliminarUsuarioToolStripMenuItem.Text = "Eliminar usuario"
        '
        'SedesToolStripMenuItem
        '
        Me.SedesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CrearSedeToolStripMenuItem, Me.ModificarSedeToolStripMenuItem, Me.EliminarSedeToolStripMenuItem})
        Me.SedesToolStripMenuItem.Name = "SedesToolStripMenuItem"
        Me.SedesToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.SedesToolStripMenuItem.Text = "Sedes"
        '
        'CrearSedeToolStripMenuItem
        '
        Me.CrearSedeToolStripMenuItem.Name = "CrearSedeToolStripMenuItem"
        Me.CrearSedeToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.CrearSedeToolStripMenuItem.Text = "Crear Sede"
        '
        'ModificarSedeToolStripMenuItem
        '
        Me.ModificarSedeToolStripMenuItem.Name = "ModificarSedeToolStripMenuItem"
        Me.ModificarSedeToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ModificarSedeToolStripMenuItem.Text = "Modificar Sede"
        '
        'EliminarSedeToolStripMenuItem
        '
        Me.EliminarSedeToolStripMenuItem.Name = "EliminarSedeToolStripMenuItem"
        Me.EliminarSedeToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.EliminarSedeToolStripMenuItem.Text = "Eliminar Sede"
        '
        'ProductosToolStripMenuItem
        '
        Me.ProductosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CrearProductoToolStripMenuItem, Me.ModificarProductoToolStripMenuItem, Me.EliminarProductoToolStripMenuItem})
        Me.ProductosToolStripMenuItem.Name = "ProductosToolStripMenuItem"
        Me.ProductosToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.ProductosToolStripMenuItem.Text = "Productos"
        '
        'CrearProductoToolStripMenuItem
        '
        Me.CrearProductoToolStripMenuItem.Name = "CrearProductoToolStripMenuItem"
        Me.CrearProductoToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.CrearProductoToolStripMenuItem.Text = "Crear producto"
        '
        'ModificarProductoToolStripMenuItem
        '
        Me.ModificarProductoToolStripMenuItem.Name = "ModificarProductoToolStripMenuItem"
        Me.ModificarProductoToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ModificarProductoToolStripMenuItem.Text = "Modificar Producto"
        '
        'EliminarProductoToolStripMenuItem
        '
        Me.EliminarProductoToolStripMenuItem.Name = "EliminarProductoToolStripMenuItem"
        Me.EliminarProductoToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.EliminarProductoToolStripMenuItem.Text = "Eliminar Producto"
        '
        'CargarDatosToolStripMenuItem
        '
        Me.CargarDatosToolStripMenuItem.Name = "CargarDatosToolStripMenuItem"
        Me.CargarDatosToolStripMenuItem.Size = New System.Drawing.Size(87, 20)
        Me.CargarDatosToolStripMenuItem.Text = "Cargar Datos"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(167, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 25)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Ubicaciones"
        '
        'Ubicaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 396)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.DGView_ubicaciones)
        Me.Controls.Add(Me.btn_modificar)
        Me.Controls.Add(Me.btn_agregar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Ubicaciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Ubicaciones"
        CType(Me.DGView_ubicaciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_agregar As System.Windows.Forms.Button
    Friend WithEvents btn_modificar As System.Windows.Forms.Button
    Friend WithEvents DGView_ubicaciones As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents InicioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EscaneoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DespachoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RecepcionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MantenimientosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsuariosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CrearUsuarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModificarUsuarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EliminarUsuarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SedesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CrearSedeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModificarSedeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EliminarSedeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CrearProductoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModificarProductoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EliminarProductoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CargarDatosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
