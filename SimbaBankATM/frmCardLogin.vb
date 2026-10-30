Public Class frmCardLogin
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblLoginTitle.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtCardNumber.TextChanged

    End Sub

    Private Sub lblCardNumber_Click(sender As Object, e As EventArgs) Handles lblCardNumber.Click

    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        If txtCardNumber.Text.Trim() = "" Then
            MessageBox.Show("Please enter your card number.")
            txtCardNumber.Focus()
            Return
        End If

        Dim cardNumber As Integer

        If Not Integer.TryParse(txtCardNumber.Text.Trim(), cardNumber) Then
            MessageBox.Show("Please enter a valid card number.")
            txtCardNumber.Clear()
            txtCardNumber.Focus()
            Return
        End If

        frmPinLogin.CardNumber = cardNumber
        frmPinLogin.Show()
        Me.Hide()
    End Sub
End Class