<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdmin
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
        lblAdminTitle = New Label()
        btnViewAccounts = New Button()
        btnLoadCash = New Button()
        btnViewCash = New Button()
        btnAdminLogout = New Button()
        btnUnlockCard = New Button()
        btnCreateAccount = New Button()
        SuspendLayout()
        ' 
        ' lblAdminTitle
        ' 
        lblAdminTitle.AutoSize = True
        lblAdminTitle.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblAdminTitle.Location = New Point(230, 28)
        lblAdminTitle.Name = "lblAdminTitle"
        lblAdminTitle.Size = New Size(283, 32)
        lblAdminTitle.TabIndex = 0
        lblAdminTitle.Text = "ATM ADMINISTRATION"
        ' 
        ' btnViewAccounts
        ' 
        btnViewAccounts.BackColor = Color.DarkCyan
        btnViewAccounts.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnViewAccounts.ForeColor = Color.WhiteSmoke
        btnViewAccounts.Location = New Point(131, 91)
        btnViewAccounts.Name = "btnViewAccounts"
        btnViewAccounts.Size = New Size(220, 50)
        btnViewAccounts.TabIndex = 1
        btnViewAccounts.Text = "View All Accounts"
        btnViewAccounts.UseVisualStyleBackColor = False
        ' 
        ' btnLoadCash
        ' 
        btnLoadCash.BackColor = Color.DarkCyan
        btnLoadCash.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnLoadCash.ForeColor = Color.WhiteSmoke
        btnLoadCash.Location = New Point(131, 191)
        btnLoadCash.Name = "btnLoadCash"
        btnLoadCash.Size = New Size(220, 50)
        btnLoadCash.TabIndex = 2
        btnLoadCash.Text = "Load ATM Cash"
        btnLoadCash.UseVisualStyleBackColor = False
        ' 
        ' btnViewCash
        ' 
        btnViewCash.BackColor = Color.DarkCyan
        btnViewCash.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnViewCash.ForeColor = Color.WhiteSmoke
        btnViewCash.Location = New Point(412, 91)
        btnViewCash.Name = "btnViewCash"
        btnViewCash.Size = New Size(220, 50)
        btnViewCash.TabIndex = 3
        btnViewCash.Text = "View ATM Cash"
        btnViewCash.UseVisualStyleBackColor = False
        ' 
        ' btnAdminLogout
        ' 
        btnAdminLogout.BackColor = Color.LightSeaGreen
        btnAdminLogout.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnAdminLogout.ForeColor = Color.WhiteSmoke
        btnAdminLogout.Location = New Point(412, 292)
        btnAdminLogout.Name = "btnAdminLogout"
        btnAdminLogout.Size = New Size(220, 50)
        btnAdminLogout.TabIndex = 4
        btnAdminLogout.Text = "Log Out"
        btnAdminLogout.UseVisualStyleBackColor = False
        ' 
        ' btnUnlockCard
        ' 
        btnUnlockCard.BackColor = Color.DarkCyan
        btnUnlockCard.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnUnlockCard.ForeColor = Color.WhiteSmoke
        btnUnlockCard.Location = New Point(412, 191)
        btnUnlockCard.Name = "btnUnlockCard"
        btnUnlockCard.Size = New Size(220, 50)
        btnUnlockCard.TabIndex = 5
        btnUnlockCard.Text = "Unlock Card"
        btnUnlockCard.UseVisualStyleBackColor = False
        ' 
        ' btnCreateAccount
        ' 
        btnCreateAccount.BackColor = Color.DarkCyan
        btnCreateAccount.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnCreateAccount.ForeColor = Color.WhiteSmoke
        btnCreateAccount.Location = New Point(131, 292)
        btnCreateAccount.Name = "btnCreateAccount"
        btnCreateAccount.Size = New Size(220, 50)
        btnCreateAccount.TabIndex = 6
        btnCreateAccount.Text = "Create Account"
        btnCreateAccount.UseVisualStyleBackColor = False
        ' 
        ' frmAdmin
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(784, 561)
        Controls.Add(btnCreateAccount)
        Controls.Add(btnUnlockCard)
        Controls.Add(btnAdminLogout)
        Controls.Add(btnViewCash)
        Controls.Add(btnLoadCash)
        Controls.Add(btnViewAccounts)
        Controls.Add(lblAdminTitle)
        Name = "frmAdmin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Simba Bank - Administration"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblAdminTitle As Label
    Friend WithEvents btnViewAccounts As Button
    Friend WithEvents btnLoadCash As Button
    Friend WithEvents btnViewCash As Button
    Friend WithEvents btnAdminLogout As Button
    Friend WithEvents btnUnlockCard As Button
    Friend WithEvents btnCreateAccount As Button
End Class
