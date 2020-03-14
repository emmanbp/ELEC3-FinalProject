<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainMenu))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.playbutton = New System.Windows.Forms.Button()
        Me.settingbutton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(64, 59)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(382, 65)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "TIC - LUCK - TOE"
        '
        'playbutton
        '
        Me.playbutton.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(171, Byte), Integer))
        Me.playbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.playbutton.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.playbutton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.playbutton.Location = New System.Drawing.Point(145, 177)
        Me.playbutton.Margin = New System.Windows.Forms.Padding(2)
        Me.playbutton.Name = "playbutton"
        Me.playbutton.Size = New System.Drawing.Size(233, 58)
        Me.playbutton.TabIndex = 1
        Me.playbutton.Text = "PLAY GAME"
        Me.playbutton.UseVisualStyleBackColor = False
        '
        'settingbutton
        '
        Me.settingbutton.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(213, Byte), Integer), CType(CType(171, Byte), Integer))
        Me.settingbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.settingbutton.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.settingbutton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(117, Byte), Integer), CType(CType(131, Byte), Integer))
        Me.settingbutton.Location = New System.Drawing.Point(145, 261)
        Me.settingbutton.Margin = New System.Windows.Forms.Padding(2)
        Me.settingbutton.Name = "settingbutton"
        Me.settingbutton.Size = New System.Drawing.Size(233, 58)
        Me.settingbutton.TabIndex = 3
        Me.settingbutton.Text = "SETTINGS"
        Me.settingbutton.UseVisualStyleBackColor = False
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(193, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(523, 487)
        Me.Controls.Add(Me.settingbutton)
        Me.Controls.Add(Me.playbutton)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(93, Byte), Integer), CType(CType(121, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(539, 664)
        Me.MinimumSize = New System.Drawing.Size(539, 486)
        Me.Name = "MainMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CpE Quiz Bee: Tic - Luck - Toe"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents playbutton As Button
    Friend WithEvents settingbutton As Button
End Class
