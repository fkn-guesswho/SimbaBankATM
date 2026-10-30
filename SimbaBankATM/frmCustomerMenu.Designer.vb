<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerMenu
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
        lblWelcome = New Label()
        btnBalance = New Button()
        btnDeposit = New Button()
        btnWithdraw = New Button()
        btnTransfer = New Button()
        btnStatement = New Button()
        btnChangePin = New Button()
        btnLogout = New Button()
        SuspendLayout()
        ' 
        ' lblWelcome
        ' 
        lblWelcome.AutoSize = True
        lblWelcome.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0)
        lblWelcome.Location = New Point(243, 28)
        lblWelcome.Name = "lblWelcome"
        lblWelcome.Size = New Size(326, 32)
        lblWelcome.TabIndex = 0
        lblWelcome.Text = "WELCOME TO SIMBA BANK"
        ' 
        ' btnBalance
        ' 
        btnBalance.BackColor = Color.DarkCyan
        btnBalance.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnBalance.ForeColor = Color.White
        btnBalance.Location = New Point(140, 99)
        btnBalance.Name = "btnBalance"
        btnBalance.Size = New Size(220, 50)
        btnBalance.TabIndex = 1
        btnBalance.Text = "Check Balance"
        btnBalance.UseVisualStyleBackColor = False
        ' 
        ' btnDeposit
        ' 
        btnDeposit.BackColor = Color.DarkCyan
        btnDeposit.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnDeposit.ForeColor = Color.White
        btnDeposit.Location = New Point(140, 192)
        btnDeposit.Name = "btnDeposit"
        btnDeposit.Size = New Size(220, 50)
        btnDeposit.TabIndex = 2
        btnDeposit.Text = "Deposit Money"
        btnDeposit.UseVisualStyleBackColor = False
        ' 
        ' btnWithdraw
        ' 
        btnWithdraw.BackColor = Color.DarkCyan
        btnWithdraw.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnWithdraw.ForeColor = Color.White
        btnWithdraw.Location = New Point(140, 288)
        btnWithdraw.Name = "btnWithdraw"
        btnWithdraw.Size = New Size(220, 50)
        btnWithdraw.TabIndex = 3
        btnWithdraw.Text = "Withdraw Money"
        btnWithdraw.UseVisualStyleBackColor = False
        ' 
        ' btnTransfer
        ' 
        btnTransfer.BackColor = Color.DarkCyan
        btnTransfer.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnTransfer.ForeColor = Color.White
        btnTransfer.Location = New Point(459, 99)
        btnTransfer.Name = "btnTransfer"
        btnTransfer.Size = New Size(220, 50)
        btnTransfer.TabIndex = 4
        btnTransfer.Text = "Transfer Money"
        btnTransfer.UseVisualStyleBackColor = False
        ' 
        ' btnStatement
        ' 
        btnStatement.BackColor = Color.DarkCyan
        btnStatement.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnStatement.ForeColor = Color.White
        btnStatement.Location = New Point(459, 192)
        btnStatement.Name = "btnStatement"
        btnStatement.Size = New Size(220, 50)
        btnStatement.TabIndex = 5
        btnStatement.Text = "Mini Statement"
        btnStatement.UseVisualStyleBackColor = False
        ' 
        ' btnChangePin
        ' 
        btnChangePin.BackColor = Color.DarkCyan
        btnChangePin.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnChangePin.ForeColor = Color.White
        btnChangePin.Location = New Point(459, 288)
        btnChangePin.Name = "btnChangePin"
        btnChangePin.Size = New Size(220, 50)
        btnChangePin.TabIndex = 6
        btnChangePin.Text = "Change Pin"
        btnChangePin.UseVisualStyleBackColor = False
        ' 
        ' btnLogout
        ' 
        btnLogout.BackColor = Color.LightSeaGreen
        btnLogout.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold)
        btnLogout.ForeColor = Color.White
        btnLogout.Location = New Point(305, 402)
        btnLogout.Name = "btnLogout"
        btnLogout.Size = New Size(220, 57)
        btnLogout.TabIndex = 7
        btnLogout.Text = "Log Out"
        btnLogout.UseVisualStyleBackColor = False
        ' 
        ' frmCustomerMenu
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(784, 561)
        Controls.Add(btnLogout)
        Controls.Add(btnChangePin)
        Controls.Add(btnStatement)
        Controls.Add(btnTransfer)
        Controls.Add(btnWithdraw)
        Controls.Add(btnDeposit)
        Controls.Add(btnBalance)
        Controls.Add(lblWelcome)
        Name = "frmCustomerMenu"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Simba Bank - Customer Menu"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblWelcome As Label
    Friend WithEvents btnBalance As Button
    Friend WithEvents btnDeposit As Button
    Friend WithEvents btnWithdraw As Button
    Friend WithEvents btnTransfer As Button
    Friend WithEvents btnStatement As Button
    Friend WithEvents btnChangePin As Button
    Friend WithEvents btnLogout As Button
End Class
