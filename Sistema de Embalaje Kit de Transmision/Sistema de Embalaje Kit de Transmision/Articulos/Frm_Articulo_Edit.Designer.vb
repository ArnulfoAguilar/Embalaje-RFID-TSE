﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Articulo_Edit
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
        Me.btn_guardar_prd = New System.Windows.Forms.Button()
        Me.cbx_ret = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_nomenclatura = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_nombre = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_cancelar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_guardar_prd
        '
        Me.btn_guardar_prd.Location = New System.Drawing.Point(107, 216)
        Me.btn_guardar_prd.Name = "btn_guardar_prd"
        Me.btn_guardar_prd.Size = New System.Drawing.Size(75, 23)
        Me.btn_guardar_prd.TabIndex = 15
        Me.btn_guardar_prd.Text = "Guardar"
        Me.btn_guardar_prd.UseVisualStyleBackColor = True
        '
        'cbx_ret
        '
        Me.cbx_ret.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbx_ret.FormattingEnabled = True
        Me.cbx_ret.Location = New System.Drawing.Point(138, 152)
        Me.cbx_ret.Name = "cbx_ret"
        Me.cbx_ret.Size = New System.Drawing.Size(121, 21)
        Me.cbx_ret.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(23, 155)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Tipo de Envio"
        '
        'txt_nomenclatura
        '
        Me.txt_nomenclatura.Location = New System.Drawing.Point(138, 95)
        Me.txt_nomenclatura.MaxLength = 3
        Me.txt_nomenclatura.Name = "txt_nomenclatura"
        Me.txt_nomenclatura.Size = New System.Drawing.Size(59, 20)
        Me.txt_nomenclatura.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Nomenclatura"
        '
        'txt_nombre
        '
        Me.txt_nombre.Location = New System.Drawing.Point(138, 38)
        Me.txt_nombre.Name = "txt_nombre"
        Me.txt_nombre.Size = New System.Drawing.Size(100, 20)
        Me.txt_nombre.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Nombre del Articulo"
        '
        'btn_cancelar
        '
        Me.btn_cancelar.Location = New System.Drawing.Point(197, 216)
        Me.btn_cancelar.Name = "btn_cancelar"
        Me.btn_cancelar.Size = New System.Drawing.Size(75, 23)
        Me.btn_cancelar.TabIndex = 16
        Me.btn_cancelar.Text = "Cancelar"
        Me.btn_cancelar.UseVisualStyleBackColor = True
        '
        'Frm_Articulo_Edit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.btn_cancelar)
        Me.Controls.Add(Me.btn_guardar_prd)
        Me.Controls.Add(Me.cbx_ret)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_nomenclatura)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_nombre)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Frm_Articulo_Edit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Editar Articulo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_guardar_prd As System.Windows.Forms.Button
    Friend WithEvents cbx_ret As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_nomenclatura As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_nombre As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_cancelar As System.Windows.Forms.Button
End Class
