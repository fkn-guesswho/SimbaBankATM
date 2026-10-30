<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCardLogin
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
        lblLoginTitle = New Label()
        lblCardNumber = New Label()
        txtCardNumber = New TextBox()
        btnContinue = New Button()
        SuspendLayout()
        ' 
        ' lblLoginTitle
        ' 
        lblLoginTitle.AutoSize = True
        lblLoginTitle.Font = New Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblLoginTitle.Location = New Point(172, 36)
        lblLoginTitle.Name = "lblLoginTitle"
        lblLoginTitle.Size = New Size(222, 32)
        lblLoginTitle.TabIndex = 0
        lblLoginTitle.Text = "CUSTOMER LOGIN"
        ' 
        ' lblCardNumber
        ' 
        lblCardNumber.AutoSize = True
        lblCardNumber.Location = New Point(198, 100)
        lblCardNumber.Name = "lblCardNumber"
        lblCardNumber.Size = New Size(79, 15)
        lblCardNumber.TabIndex = 1
        lblCardNumber.Text = "Card Number"
        ' 
        ' txtCardNumber
        ' 
        txtCardNumber.Location = New Point(283, 97)
        txtCardNumber.Name = "txtCardNumber"
        txtCardNumber.Size = New Size(82, 23)
        txtCardNumber.TabIndex = 2
        ' 
        ' btnContinue
        ' 
        btnContinue.BackColor = Color.DarkCyan
        btnContinue.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnContinue.ForeColor = Color.White
        btnContinue.Location = New Point(242, 153)
        btnContinue.Name = "btnContinue"
        btnContinue.Size = New Size(93, 35)
        btnContinue.TabIndex = 3
        btnContinue.Text = "Continue"
        btnContinue.UseVisualStyleBackColor = False
        ' 
        ' frmCardLogin
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(584, 361)
        Controls.Add(btnContinue)
        Controls.Add(txtCardNumber)
        Controls.Add(lblCardNumber)
        Controls.Add(lblLoginTitle)
        Name = "frmCardLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Customer Card Login - Simba Bank"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblLoginTitle As Label
    Friend WithEvents lblCardNumber As Label
    Friend WithEvents txtCardNumber As TextBox
    Friend WithEvents btnContinue As Button
End Class
