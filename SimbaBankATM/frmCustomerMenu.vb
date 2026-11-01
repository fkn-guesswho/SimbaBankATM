Public Class frmCustomerMenu

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        AppData.CurrentAccount = Nothing

        frmWelcome.Show()
        Me.Hide()
    End Sub


    Private Sub btnDeposit_Click(sender As Object, e As EventArgs) Handles btnDeposit.Click
        If AppData.CurrentAccount Is Nothing Then
            MessageBox.Show("Please log in first.")
            Return
        End If

        Dim input As String = InputBox("Enter the amount to deposit (KSh):", "Deposit Money")
        If input.Trim() = "" Then Return

        Dim amount As Decimal

        If Not Decimal.TryParse(input, amount) Then
            MessageBox.Show("Please enter a valid amount.")
            Return
        End If

        If amount <= 0 Then
            MessageBox.Show("The amount must be greater than zero.")
            Return
        End If

        Dim result As OpResult = AppData.CurrentAccount.Deposit(amount)

        If result.Success Then
            DatabaseHelper.SaveAccount(AppData.CurrentAccount)
        End If

        MessageBox.Show(result.Message, "Deposit Result")

        If result.Success Then
            MessageBox.Show("New balance: KSh " &
                    AppData.CurrentAccount.Balance.ToString("N2"),
                    "Updated Balance")
        End If
    End Sub


    Private Sub lblWelcome_Click(sender As Object, e As EventArgs) Handles lblWelcome.Click

    End Sub

    Private Sub btnBalance_Click(sender As Object, e As EventArgs) Handles btnBalance.Click
        If AppData.CurrentAccount Is Nothing Then
            MessageBox.Show("Please log in first.")
            Return
        End If

        MessageBox.Show(
        "Account Holder: " & AppData.CurrentAccount.HolderName &
        vbCrLf & "Card Number: " & AppData.CurrentAccount.CardNumber &
        vbCrLf & "Available Balance: KSh " &
        AppData.CurrentAccount.Balance.ToString("N2"),
        "Account Balance"
    )
    End Sub



    Private Sub btnWithdraw_Click(sender As Object, e As EventArgs) Handles btnWithdraw.Click
        If AppData.CurrentAccount Is Nothing Then
            MessageBox.Show("Please log in first.")
            Return
        End If

        Dim input As String =
        InputBox("Enter the amount to withdraw (KSh):", "Withdraw Money")

        If input.Trim() = "" Then Return

        Dim amount As Decimal

        If Not Decimal.TryParse(input, amount) Then
            MessageBox.Show("Please enter a valid amount.")
            Return
        End If

        If amount <= 0 OrElse amount <> Decimal.Truncate(amount) Then
            MessageBox.Show("Enter a positive whole-number amount.")
            Return
        End If

        If amount > Integer.MaxValue Then
            MessageBox.Show("The amount is too large.")
            Return
        End If

        Dim withdrawalAmount As Integer = CInt(amount)

        Dim plan As Dictionary(Of Integer, Integer) =
        AppData.atm.PlanDispense(withdrawalAmount)

        If plan Is Nothing Then
            MessageBox.Show(
            "The ATM cannot dispense that exact amount with its available notes.",
            "Withdrawal Failed")
            Return
        End If

        Dim result As OpResult =
        AppData.CurrentAccount.Withdraw(amount)

        If Not result.Success Then
            MessageBox.Show(result.Message, "Withdrawal Failed")
            Return
        End If

        AppData.atm.Dispense(plan)

        DatabaseHelper.SaveAccount(AppData.CurrentAccount)
        DatabaseHelper.SaveCash(AppData.atm)

        MessageBox.Show(
        result.Message & vbCrLf &
        "Please collect your cash.",
        "Withdrawal Successful")

        MessageBox.Show(
        "Remaining balance: KSh " &
        AppData.CurrentAccount.Balance.ToString("N2") &
        vbCrLf &
        "ATM cash remaining: KSh " &
        AppData.atm.TotalCash.ToString("N2"),
        "Updated Balances")
    End Sub



    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        If AppData.CurrentAccount Is Nothing Then
            MessageBox.Show("Please log in first.")
            Return
        End If

        Dim recipientInput As String =
        InputBox("Enter the recipient's card number:", "Transfer Money")

        If recipientInput.Trim() = "" Then Return

        Dim recipient As Account = AppData.bank.FindAccount(recipientInput.Trim())

        If recipient Is Nothing Then
            MessageBox.Show("Recipient account not found.")
            Return
        End If

        If recipient.CardNumber = AppData.CurrentAccount.CardNumber Then
            MessageBox.Show("You cannot transfer money to your own account.")
            Return
        End If

        Dim amountInput As String =
        InputBox("Enter the amount to transfer (KSh):", "Transfer Money")

        If amountInput.Trim() = "" Then Return

        Dim amount As Decimal

        If Not Decimal.TryParse(amountInput, amount) OrElse amount <= 0 Then
            MessageBox.Show("Enter a valid amount greater than zero.")
            Return
        End If

        Dim result As OpResult = AppData.CurrentAccount.TransferOut(amount)

        If Not result.Success Then
            MessageBox.Show(result.Message, "Transfer Failed")
            Return
        End If

        recipient.TransferIn(amount)

        ' Save both accounts.
        DatabaseHelper.SaveAccounts(AppData.CurrentAccount, recipient)

        MessageBox.Show("Transfer successful!" &
                    vbCrLf & "Amount: KSh " & amount.ToString("N2") &
                    vbCrLf & "Recipient: " & recipient.HolderName &
                    vbCrLf & "Your new balance: KSh " &
                    AppData.CurrentAccount.Balance.ToString("N2"),
                    "Transfer Complete")
    End Sub


    Private Sub btnStatement_Click(sender As Object, e As EventArgs) Handles btnStatement.Click
        If AppData.CurrentAccount Is Nothing Then
            MessageBox.Show("Please log in first.")
            Return
        End If

        Dim acc As Account = AppData.CurrentAccount

        Dim statement As String =
            "ACCOUNT STATEMENT" & vbCrLf &
            "Holder: " & acc.HolderName & vbCrLf &
            "Card Number: " & acc.CardNumber & vbCrLf &
            "Current Balance: KSh " & acc.Balance.ToString("N2") &
            vbCrLf & vbCrLf & "RECENT TRANSACTIONS" & vbCrLf

        If acc.History.Count = 0 Then
            statement &= "No transactions recorded yet."
        Else
            Dim startIndex As Integer = Math.Max(0, acc.History.Count - 5)

            For i As Integer = acc.History.Count - 1 To startIndex Step -1
                Dim t As Transaction = acc.History(i)

                statement &= t.ToString() & vbCrLf
            Next
        End If

        MessageBox.Show(statement, "Mini Statement")
    End Sub


    Private Sub btnChangePin_Click(sender As Object, e As EventArgs) Handles btnChangePin.Click
        If AppData.CurrentAccount Is Nothing Then
            MessageBox.Show("Please log in first.")
            Return
        End If

        Dim oldPin As String =
        InputBox("Enter your current PIN:", "Change PIN")

        If oldPin = "" Then Return

        Dim newPin As String =
        InputBox("Enter your new 4-digit PIN:", "Change PIN")

        If newPin = "" Then Return

        If Not Account.IsValidPin(newPin) Then
            MessageBox.Show("Your new PIN must contain exactly 4 digits.")
            Return
        End If

        Dim confirmPin As String =
        InputBox("Confirm your new PIN:", "Change PIN")

        If confirmPin = "" Then Return

        If newPin <> confirmPin Then
            MessageBox.Show("The new PINs do not match.")
            Return
        End If

        Dim result As OpResult =
        AppData.CurrentAccount.ChangePin(oldPin, newPin)

        If result.Success Then
            DatabaseHelper.SaveAccount(AppData.CurrentAccount)
        End If

        MessageBox.Show(result.Message, "Change PIN")
    End Sub

End Class