Imports System.ComponentModel
Public Class frmPinLogin
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property CardNumber As Integer

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        frmCardLogin.txtCardNumber.Clear()
        txtPin.Clear()

        frmCardLogin.Show()
        Me.Hide()
    End Sub



    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim acc As Account = AppData.bank.FindAccount(CardNumber.ToString())

        If acc Is Nothing Then
            MessageBox.Show("Card not found. Please check your card number.")
            Return
        End If

        If acc.IsLocked Then
            MessageBox.Show("This card is locked. Please contact the bank.")
            Return
        End If

        If Not Account.IsValidPin(txtPin.Text.Trim()) Then
            MessageBox.Show("Enter a valid 4-digit PIN.")
            txtPin.Clear()
            txtPin.Focus()
            Return
        End If

        Dim pinCorrect As Boolean = acc.VerifyPin(txtPin.Text.Trim())

        DatabaseHelper.SaveAccount(acc)

        If pinCorrect Then
            AppData.CurrentAccount = acc
            frmCustomerMenu.Show()
            Me.Hide()
        Else
            MessageBox.Show("Incorrect PIN. Attempts remaining: " &
                        (Account.MaxPinAttempts - acc.FailedAttempts).ToString())

            txtPin.Clear()
            txtPin.Focus()

            If acc.IsLocked Then
                MessageBox.Show("Your card has been locked after 3 incorrect attempts.")
            End If
        End If
    End Sub

End Class