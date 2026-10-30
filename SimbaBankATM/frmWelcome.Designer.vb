<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWelcome
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        pnlHeader = New Panel()
        lblTagline = New Label()
        lblBankName = New Label()
        btnInsertCard = New Button()
        btnAdmin = New Button()
        btnExit = New Button()
        pnlHeader.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlHeader
        ' 
        pnlHeader.BackColor = Color.Teal
        pnlHeader.Controls.Add(lblTagline)
        pnlHeader.Controls.Add(lblBankName)
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New Size(784, 120)
        pnlHeader.TabIndex = 0
        ' 
        ' lblTagline
        ' 
        lblTagline.AutoSize = True
        lblTagline.Font = New Font("Segoe UI", 10F)
        lblTagline.ForeColor = Color.White
        lblTagline.Location = New Point(35, 65)
        lblTagline.Name = "lblTagline"
        lblTagline.Size = New Size(162, 19)
        lblTagline.TabIndex = 1
        lblTagline.Text = "ATM BANKING SERVICES"
        ' 
        ' lblBankName
        ' 
        lblBankName.AutoSize = True
        lblBankName.Font = New Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblBankName.ForeColor = Color.White
        lblBankName.Location = New Point(26, 20)
        lblBankName.Name = "lblBankName"
        lblBankName.Size = New Size(222, 45)
        lblBankName.TabIndex = 0
        lblBankName.Text = "SIMBA BANK"
        ' 
        ' btnInsertCard
        ' 
        btnInsertCard.BackColor = Color.DarkCyan
        btnInsertCard.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnInsertCard.ForeColor = Color.Ivory
        btnInsertCard.Location = New Point(161, 174)
        btnInsertCard.Name = "btnInsertCard"
        btnInsertCard.Size = New Size(150, 62)
        btnInsertCard.TabIndex = 1
        btnInsertCard.Text = "Insert Card"
        btnInsertCard.UseVisualStyleBackColor = False
        ' 
        ' btnAdmin
        ' 
        btnAdmin.BackColor = Color.DarkCyan
        btnAdmin.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnAdmin.ForeColor = Color.White
        btnAdmin.Location = New Point(496, 174)
        btnAdmin.Name = "btnAdmin"
        btnAdmin.Size = New Size(150, 62)
        btnAdmin.TabIndex = 2
        btnAdmin.Text = "Administrator"
        btnAdmin.UseVisualStyleBackColor = False
        ' 
        ' btnExit
        ' 
        btnExit.BackColor = Color.LightSeaGreen
        btnExit.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnExit.ForeColor = Color.White
        btnExit.Location = New Point(291, 293)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(220, 62)
        btnExit.TabIndex = 3
        btnExit.Text = "Exit"
        btnExit.UseVisualStyleBackColor = False
        ' 
        ' frmWelcome
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(784, 511)
        Controls.Add(btnExit)
        Controls.Add(btnAdmin)
        Controls.Add(btnInsertCard)
        Controls.Add(pnlHeader)
        Name = "frmWelcome"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Simba Bank ATM"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblBankName As Label
    Friend WithEvents lblTagline As Label
    Friend WithEvents btnInsertCard As Button
    Friend WithEvents btnAdmin As Button
    Friend WithEvents btnExit As Button

End Class
