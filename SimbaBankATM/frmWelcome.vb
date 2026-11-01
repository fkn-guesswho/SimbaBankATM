
Public Class frmWelcome

    Private Sub frmWelcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DatabaseHelper.InitializeDatabase()
            DatabaseHelper.LoadAccounts(AppData.bank)
            DatabaseHelper.LoadCash(AppData.atm)

        Catch ex As Exception
            MessageBox.Show(
            "Database startup failed: " & ex.Message,
            "Database Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )
        End Try
    End Sub

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
