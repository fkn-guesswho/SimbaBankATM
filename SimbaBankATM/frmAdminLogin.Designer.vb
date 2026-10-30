<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdminLogin
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
        lblPassword = New Label()
        txtAdminPassword = New TextBox()
        btnBack = New Button()
        btnLogin = New Button()
        lblAdminLogin = New Label()
        SuspendLayout()
        ' 
        ' lblPassword
        ' 
        lblPassword.AutoSize = True
        lblPassword.Location = New Point(279, 127)
        lblPassword.Name = "lblPassword"
        lblPassword.Size = New Size(96, 15)
        lblPassword.TabIndex = 0
        lblPassword.Text = "Admin Password"
        ' 
        ' txtAdminPassword
        ' 
        txtAdminPassword.Location = New Point(381, 124)
        txtAdminPassword.Name = "txtAdminPassword"
        txtAdminPassword.Size = New Size(92, 23)
        txtAdminPassword.TabIndex = 1
        txtAdminPassword.UseSystemPasswordChar = True
        ' 
        ' btnBack
        ' 
        btnBack.BackColor = Color.LightSeaGreen
        btnBack.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnBack.ForeColor = Color.White
        btnBack.Location = New Point(267, 188)
        btnBack.Name = "btnBack"
        btnBack.Size = New Size(79, 29)
        btnBack.TabIndex = 2
        btnBack.Text = "Back"
        btnBack.UseVisualStyleBackColor = False
        ' 
        ' btnLogin
        ' 
        btnLogin.BackColor = Color.DarkCyan
        btnLogin.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnLogin.ForeColor = Color.White
        btnLogin.Location = New Point(406, 188)
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(79, 29)
        btnLogin.TabIndex = 3
        btnLogin.Text = "Login"
        btnLogin.UseVisualStyleBackColor = False
        ' 
        ' lblAdminLogin
        ' 
        lblAdminLogin.AutoSize = True
        lblAdminLogin.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblAdminLogin.Location = New Point(252, 46)
        lblAdminLogin.Name = "lblAdminLogin"
        lblAdminLogin.Size = New Size(256, 32)
        lblAdminLogin.TabIndex = 4
        lblAdminLogin.Text = "SIMBA BANK ADMIN"
        ' 
        ' frmAdminLogin
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(lblAdminLogin)
        Controls.Add(btnLogin)
        Controls.Add(btnBack)
        Controls.Add(txtAdminPassword)
        Controls.Add(lblPassword)
        Name = "frmAdminLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Admin Login"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblPassword As Label
    Friend WithEvents txtAdminPassword As TextBox
    Friend WithEvents btnBack As Button
    Friend WithEvents btnLogin As Button
    Friend WithEvents lblAdminLogin As Label
End Class
