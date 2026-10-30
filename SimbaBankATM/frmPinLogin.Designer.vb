<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPinLogin
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
        lblPinTitle = New Label()
        lblPinPrompt = New Label()
        txtPin = New TextBox()
        btnLogin = New Button()
        btnBack = New Button()
        SuspendLayout()
        ' 
        ' lblPinTitle
        ' 
        lblPinTitle.AutoSize = True
        lblPinTitle.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPinTitle.Location = New Point(169, 57)
        lblPinTitle.Name = "lblPinTitle"
        lblPinTitle.Size = New Size(239, 32)
        lblPinTitle.TabIndex = 0
        lblPinTitle.Text = "VERIFY YOUR CARD"
        ' 
        ' lblPinPrompt
        ' 
        lblPinPrompt.AutoSize = True
        lblPinPrompt.Location = New Point(181, 115)
        lblPinPrompt.Name = "lblPinPrompt"
        lblPinPrompt.Size = New Size(121, 15)
        lblPinPrompt.TabIndex = 1
        lblPinPrompt.Text = "Enter your 4-digit PIN"
        ' 
        ' txtPin
        ' 
        txtPin.Location = New Point(308, 112)
        txtPin.Name = "txtPin"
        txtPin.Size = New Size(89, 23)
        txtPin.TabIndex = 2
        txtPin.UseSystemPasswordChar = True
        ' 
        ' btnLogin
        ' 
        btnLogin.BackColor = Color.DarkCyan
        btnLogin.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnLogin.ForeColor = Color.White
        btnLogin.Location = New Point(328, 172)
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(109, 41)
        btnLogin.TabIndex = 3
        btnLogin.Text = "Log In"
        btnLogin.UseVisualStyleBackColor = False
        ' 
        ' btnBack
        ' 
        btnBack.BackColor = Color.LightSeaGreen
        btnBack.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnBack.ForeColor = Color.White
        btnBack.Location = New Point(159, 172)
        btnBack.Name = "btnBack"
        btnBack.Size = New Size(109, 41)
        btnBack.TabIndex = 4
        btnBack.Text = "Back"
        btnBack.UseVisualStyleBackColor = False
        ' 
        ' frmPinLogin
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(584, 361)
        Controls.Add(btnBack)
        Controls.Add(btnLogin)
        Controls.Add(txtPin)
        Controls.Add(lblPinPrompt)
        Controls.Add(lblPinTitle)
        Name = "frmPinLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Simba Bank - PIN Verification"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblPinTitle As Label
    Friend WithEvents lblPinPrompt As Label
    Friend WithEvents txtPin As TextBox
    Friend WithEvents btnLogin As Button
    Friend WithEvents btnBack As Button
End Class
