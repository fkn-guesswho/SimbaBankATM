Public Class frmWelcome
    Private Sub lblBankName_Click(sender As Object, e As EventArgs) Handles lblBankName.Click

    End Sub

    Private Sub lblTagline_Click(sender As Object, e As EventArgs) Handles lblTagline.Click

    End Sub

    Private Sub btnInsertCard_Click(sender As Object, e As EventArgs) Handles btnInsertCard.Click
        frmCardLogin.txtCardNumber.Clear()
        frmPinLogin.txtPin.Clear()

        frmCardLogin.Show()
        Me.Hide()
    End Sub

    Private Sub btnAdmin_Click(sender As Object, e As EventArgs) Handles btnAdmin.Click
        frmAdminLogin.Show()
        Me.Hide()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
