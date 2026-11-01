Public Class frmAdmin
    Private Sub btnViewAccounts_Click(sender As Object, e As EventArgs) Handles btnViewAccounts.Click
        Dim accountList As String = "REGISTERED BANK ACCOUNTS" & vbCrLf &
                                "-----------------------------" & vbCrLf

        For Each acc As Account In AppData.bank.AllAccounts
            accountList &= "Name: " & acc.HolderName & vbCrLf &
                       "Card Number: " & acc.CardNumber & vbCrLf &
                       "Balance: KSh " & acc.Balance.ToString("N2") & vbCrLf &
                       "Status: " & If(acc.IsLocked, "Locked", "Active") &
                       vbCrLf & vbCrLf
        Next

        MessageBox.Show(accountList, "View Accounts")
    End Sub

    Private Sub btnViewCash_Click(sender As Object, e As EventArgs) Handles btnViewCash.Click
        Dim cashReport As String = "ATM CASH REPORT" & vbCrLf &
                               "------------------------" & vbCrLf &
                               "KSh 100 notes: " & AppData.atm.NoteCount(100) & vbCrLf &
                               "KSh 50 notes: " & AppData.atm.NoteCount(50) & vbCrLf &
                               "KSh 20 notes: " & AppData.atm.NoteCount(20) & vbCrLf &
                               "KSh 10 notes: " & AppData.atm.NoteCount(10) & vbCrLf &
                               "------------------------" & vbCrLf &
                               "Total Cash: KSh " &
                               AppData.atm.TotalCash.ToString("N2")

        MessageBox.Show(cashReport, "ATM Cash Report")
    End Sub

    Private Sub btnLoadCash_Click(sender As Object, e As EventArgs) Handles btnLoadCash.Click
        Dim denominationInput =
        InputBox("Enter note denomination (10, 20, 50, or 100):", "Load ATM Cash")

        If denominationInput.Trim = "" Then Return

        Dim denomination As Integer

        If Not Integer.TryParse(denominationInput, denomination) Then
            MessageBox.Show("Please enter a valid denomination.")
            Return
        End If

        If Not atm.IsValidDenomination(denomination) Then
            MessageBox.Show("Invalid denomination. Use 10, 20, 50, or 100.")
            Return
        End If

        Dim countInput =
        InputBox("How many KSh " & denomination & " notes do you want to add?",
                 "Load ATM Cash")

        If countInput.Trim = "" Then Return

        Dim count As Integer

        If Not Integer.TryParse(countInput, count) OrElse count <= 0 Then
            MessageBox.Show("Enter a whole number of notes greater than zero.")
            Return
        End If

        atm.AddNotes(denomination, count)
        DatabaseHelper.SaveCash(AppData.atm)

        MessageBox.Show(count & " notes of KSh " & denomination &
                    " added successfully." & vbCrLf &
                    "New ATM cash total: KSh " &
                    atm.TotalCash.ToString("N2"),
                    "Cash Loaded")
    End Sub

    Private Sub btnAdminLogout_Click(sender As Object, e As EventArgs) Handles btnAdminLogout.Click
        frmAdminLogin.txtAdminPassword.Clear()

        frmWelcome.Show()
        Me.Hide()
    End Sub

    Private Sub btnUnlockCard_Click(sender As Object, e As EventArgs) Handles btnUnlockCard.Click
        Dim cardNumber As String =
        InputBox("Enter the card number to unlock:", "Unlock Card").Trim()

        If cardNumber = "" Then Return

        Dim acc As Account = AppData.bank.FindAccount(cardNumber)

        If acc Is Nothing Then
            MessageBox.Show("Account not found. Check the card number.",
                        "Unlock Card")
            Return
        End If

        If Not acc.IsLocked Then
            MessageBox.Show("This card is not locked.",
                        "Unlock Card")
            Return
        End If

        Dim answer As DialogResult = MessageBox.Show(
        "Unlock the card belonging to " & acc.HolderName &
        " (Card: " & acc.CardNumber & ")?",
        "Confirm Unlock",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question)

        If answer = DialogResult.Yes Then
            acc.Unlock()
            DatabaseHelper.SaveAccount(acc)

            MessageBox.Show("Card unlocked successfully for " &
                        acc.HolderName & ".",
                        "Unlock Card")
        End If
    End Sub


    Private Sub btnCreateAccount_Click(sender As Object, e As EventArgs) Handles btnCreateAccount.Click
        ' Get the customer's name.
        Dim holderName As String =
            InputBox("Enter the customer's full name:", "Create Account").Trim()

        If holderName = "" Then Return

        ' Get the new card number.
        Dim cardNumber As String =
            InputBox("Enter a new card number:", "Create Account").Trim()

        If cardNumber = "" Then Return

        ' Prevent duplicate card numbers.
        If AppData.bank.FindAccount(cardNumber) IsNot Nothing Then
            MessageBox.Show("That card number already exists.",
                            "Create Account",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Return
        End If

        ' Get and validate the PIN.
        Dim pin As String =
            InputBox("Set a 4-digit PIN for the customer:", "Create Account")

        If Not Account.IsValidPin(pin) Then
            MessageBox.Show("The PIN must contain exactly 4 digits.",
                            "Create Account",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Return
        End If

        ' Get the opening balance.
        Dim balanceInput As String =
            InputBox("Enter the opening balance in KSh:", "Create Account")

        If balanceInput.Trim() = "" Then Return

        Dim openingBalance As Decimal

        If Not Decimal.TryParse(balanceInput, openingBalance) OrElse
           openingBalance < 0D Then

            MessageBox.Show("Enter a valid balance of zero or more.",
                            "Create Account",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Return
        End If

        ' Create and register the account.
        Dim newAccount As New Account(
            cardNumber,
            holderName,
            pin,
            openingBalance
        )

        If AppData.bank.AddAccount(newAccount) Then

            DatabaseHelper.SaveAccount(newAccount)

            MessageBox.Show(
                "Account created successfully!" & vbCrLf & vbCrLf &
                "Customer: " & newAccount.HolderName & vbCrLf &
                "Card Number: " & newAccount.CardNumber & vbCrLf &
                "Opening Balance: KSh " & newAccount.Balance.ToString("N2"),
                "Account Created",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)
        Else
            MessageBox.Show("Account creation failed. The card number may already exist.",
                            "Create Account",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
        End If
    End Sub

End Class