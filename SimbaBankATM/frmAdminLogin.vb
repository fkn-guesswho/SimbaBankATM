Public Class frmAdminLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtAdminPassword.Text = "9999" Then
            txtAdminPassword.Clear()
            frmAdmin.Show()
            Me.Hide()
        Else
            MessageBox.Show("Incorrect admin password. Access denied.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)

            txtAdminPassword.Clear()
            txtAdminPassword.Focus()
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        txtAdminPassword.Clear()
        frmWelcome.Show()
        Me.Hide()
    End Sub
End Class