﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Rep_Escaneo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Rep_Escaneo))
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.dtg_leido = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lb_cajas = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lb_leidas = New System.Windows.Forms.Label()
        Me.cbx_paq = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbx_sede = New System.Windows.Forms.ComboBox()
        Me.btn_cargar = New System.Windows.Forms.Button()
        Me.btn_rep = New System.Windows.Forms.Button()
        Me.dtg_unido = New System.Windows.Forms.DataGridView()
        Me.btn_listado = New System.Windows.Forms.Button()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtg_leido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtg_unido, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(5, 4)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(364, 119)
        Me.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pic.TabIndex = 13
        Me.pic.TabStop = False
        '
        'dtg_leido
        '
        Me.dtg_leido.AllowUserToAddRows = False
        Me.dtg_leido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dtg_leido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtg_leido.Location = New System.Drawing.Point(14, 141)
        Me.dtg_leido.Name = "dtg_leido"
        Me.dtg_leido.Size = New System.Drawing.Size(917, 267)
        Me.dtg_leido.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 410)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Cajas de Sede"
        '
        'lb_cajas
        '
        Me.lb_cajas.AutoSize = True
        Me.lb_cajas.Location = New System.Drawing.Point(96, 410)
        Me.lb_cajas.Name = "lb_cajas"
        Me.lb_cajas.Size = New System.Drawing.Size(29, 13)
        Me.lb_cajas.TabIndex = 16
        Me.lb_cajas.Text = "Num"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(148, 410)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Cajas Leidas"
        '
        'lb_leidas
        '
        Me.lb_leidas.AutoSize = True
        Me.lb_leidas.Location = New System.Drawing.Point(222, 410)
        Me.lb_leidas.Name = "lb_leidas"
        Me.lb_leidas.Size = New System.Drawing.Size(29, 13)
        Me.lb_leidas.TabIndex = 18
        Me.lb_leidas.Text = "Num"
        '
        'cbx_paq
        '
        Me.cbx_paq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_paq.FormattingEnabled = True
        Me.cbx_paq.Location = New System.Drawing.Point(478, 36)
        Me.cbx_paq.Name = "cbx_paq"
        Me.cbx_paq.Size = New System.Drawing.Size(151, 21)
        Me.cbx_paq.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(379, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Paquete Electoral"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(649, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Sede Logistica"
        '
        'cbx_sede
        '
        Me.cbx_sede.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_sede.FormattingEnabled = True
        Me.cbx_sede.Location = New System.Drawing.Point(748, 36)
        Me.cbx_sede.Name = "cbx_sede"
        Me.cbx_sede.Size = New System.Drawing.Size(151, 21)
        Me.cbx_sede.TabIndex = 21
        '
        'btn_cargar
        '
        Me.btn_cargar.Location = New System.Drawing.Point(748, 89)
        Me.btn_cargar.Name = "btn_cargar"
        Me.btn_cargar.Size = New System.Drawing.Size(151, 34)
        Me.btn_cargar.TabIndex = 23
        Me.btn_cargar.Text = "Buscar"
        Me.btn_cargar.UseVisualStyleBackColor = True
        '
        'btn_rep
        '
        Me.btn_rep.Location = New System.Drawing.Point(791, 414)
        Me.btn_rep.Name = "btn_rep"
        Me.btn_rep.Size = New System.Drawing.Size(108, 23)
        Me.btn_rep.TabIndex = 24
        Me.btn_rep.Text = "Acta de Entrega"
        Me.btn_rep.UseVisualStyleBackColor = True
        '
        'dtg_unido
        '
        Me.dtg_unido.AllowUserToAddRows = False
        Me.dtg_unido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtg_unido.Location = New System.Drawing.Point(142, 248)
        Me.dtg_unido.Name = "dtg_unido"
        Me.dtg_unido.Size = New System.Drawing.Size(617, 150)
        Me.dtg_unido.TabIndex = 25
        '
        'btn_listado
        '
        Me.btn_listado.Location = New System.Drawing.Point(618, 414)
        Me.btn_listado.Name = "btn_listado"
        Me.btn_listado.Size = New System.Drawing.Size(108, 23)
        Me.btn_listado.TabIndex = 26
        Me.btn_listado.Text = "Reporte Articulos"
        Me.btn_listado.UseVisualStyleBackColor = True
        '
        'Frm_Rep_Escaneo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 442)
        Me.Controls.Add(Me.btn_listado)
        Me.Controls.Add(Me.dtg_unido)
        Me.Controls.Add(Me.btn_rep)
        Me.Controls.Add(Me.btn_cargar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cbx_sede)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbx_paq)
        Me.Controls.Add(Me.lb_leidas)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lb_cajas)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtg_leido)
        Me.Controls.Add(Me.pic)
        Me.Name = "Frm_Rep_Escaneo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reportes Escaneo"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtg_leido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtg_unido, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents dtg_leido As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lb_cajas As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lb_leidas As System.Windows.Forms.Label
    Friend WithEvents cbx_paq As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbx_sede As System.Windows.Forms.ComboBox
    Friend WithEvents btn_cargar As System.Windows.Forms.Button
    Friend WithEvents btn_rep As System.Windows.Forms.Button
    Friend WithEvents dtg_unido As System.Windows.Forms.DataGridView
    Friend WithEvents btn_listado As System.Windows.Forms.Button
End Class
