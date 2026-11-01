Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Security.Cryptography


Public Structure OpResult
    Public ReadOnly Success As Boolean
    Public ReadOnly Message As String

    Public Sub New(ok As Boolean, msg As String)
        Success = ok
        Message = msg
    End Sub
End Structure

' ---------- Types of transactions ----------
Public Enum TransactionType
    Deposit
    Withdrawal
    TransferOut
    TransferIn
    PinChange
End Enum

' ---------- One transaction record ----------
Public Class Transaction
    Public ReadOnly Property Timestamp As DateTime
    Public ReadOnly Property Kind As TransactionType
    Public ReadOnly Property Amount As Decimal
    Public ReadOnly Property BalanceAfter As Decimal

    Public Sub New(txKind As TransactionType, txAmount As Decimal, balanceAfterTx As Decimal)
        Timestamp = DateTime.Now
        Kind = txKind
        Amount = txAmount
        BalanceAfter = balanceAfterTx
    End Sub

    Public Sub New(
    txKind As TransactionType,
    txAmount As Decimal,
    balanceAfterTx As Decimal,
    originalTimestamp As DateTime
)
        Timestamp = originalTimestamp
        Kind = txKind
        Amount = txAmount
        BalanceAfter = balanceAfterTx
    End Sub

    Public Overrides Function ToString() As String
        Return $"{Timestamp:dd MMM yyyy HH:mm}  {Kind.ToString(),-12} {Amount,10:N2}   Balance: {BalanceAfter,10:N2}"
    End Function
End Class

' ---------- Customer account ----------
Public Class Account
    Public Const DailyWithdrawLimit As Decimal = 1000D
    Public Const MaxPinAttempts As Integer = 3

    Private _pinSalt As Byte()
    Private _pinHash As Byte()
    Private Sub SetPin(pin As String)
        _pinSalt = RandomNumberGenerator.GetBytes(16)

        _pinHash = Rfc2898DeriveBytes.Pbkdf2(
        pin,
        _pinSalt,
        100000,
        HashAlgorithmName.SHA256,
        32
    )
    End Sub

    Private Function CheckPin(pin As String) As Boolean
        If _pinSalt Is Nothing OrElse _pinHash Is Nothing Then Return False

        Dim attemptedHash As Byte() = Rfc2898DeriveBytes.Pbkdf2(
        pin,
        _pinSalt,
        100000,
        HashAlgorithmName.SHA256,
        32
    )

        Return CryptographicOperations.FixedTimeEquals(_pinHash, attemptedHash)
    End Function

    Public ReadOnly Property PinSalt As Byte()
        Get
            Return _pinSalt
        End Get
    End Property

    Public ReadOnly Property PinHash As Byte()
        Get
            Return _pinHash
        End Get
    End Property
    Private _balance As Decimal
    Private _withdrawnToday As Decimal
    Private _withdrawDate As Date = Date.Today
    Private ReadOnly _history As New List(Of Transaction)

    Public ReadOnly Property WithdrawnToday As Decimal
        Get
            ResetDailyIfNeeded()
            Return _withdrawnToday
        End Get
    End Property

    Public ReadOnly Property WithdrawDate As Date
        Get
            Return _withdrawDate
        End Get
    End Property

    Public ReadOnly Property CardNumber As String
    Public ReadOnly Property HolderName As String
    Public Property IsLocked As Boolean
    Public Property FailedAttempts As Integer

    Public ReadOnly Property Balance As Decimal
        Get
            Return _balance
        End Get
    End Property

    Public ReadOnly Property History As IReadOnlyList(Of Transaction)
        Get
            Return _history
        End Get
    End Property

    Public Sub RestoreTransaction(transaction As Transaction)
        _history.Add(transaction)
    End Sub

    Public ReadOnly Property RemainingDailyLimit As Decimal
        Get
            ResetDailyIfNeeded()
            Return DailyWithdrawLimit - _withdrawnToday
        End Get
    End Property

    Public Sub New(cardNo As String, holder As String, pin As String, openingBalance As Decimal)
        CardNumber = cardNo
        HolderName = holder
        SetPin(pin)
        _balance = openingBalance
    End Sub

    Public Sub New(
    cardNo As String,
    holder As String,
    savedSalt As Byte(),
    savedHash As Byte(),
    savedBalance As Decimal
)
        CardNumber = cardNo
        HolderName = holder
        RestorePinHash(savedSalt, savedHash)
        _balance = savedBalance
    End Sub

    Public Sub RestorePinHash(savedSalt As Byte(), savedHash As Byte())
        If savedSalt Is Nothing OrElse savedHash Is Nothing Then
            Throw New ArgumentException("PIN security data is missing.")
        End If

        If savedSalt.Length <> 16 OrElse savedHash.Length <> 32 Then
            Throw New ArgumentException("Invalid PIN security data.")
        End If

        _pinSalt = CType(savedSalt.Clone(), Byte())
        _pinHash = CType(savedHash.Clone(), Byte())
    End Sub

    Public Shared Function IsValidPin(pin As String) As Boolean
        If pin Is Nothing OrElse pin.Length <> 4 Then Return False
        For Each ch As Char In pin
            If Not Char.IsDigit(ch) Then Return False
        Next
        Return True
    End Function

    Public Function VerifyPin(pin As String) As Boolean
        If IsLocked Then Return False

        If CheckPin(pin) Then
            FailedAttempts = 0
            Return True
        End If

        FailedAttempts += 1

        If FailedAttempts >= MaxPinAttempts Then
            IsLocked = True
        End If

        Return False
    End Function

    Public Sub Unlock()
        IsLocked = False
        FailedAttempts = 0
    End Sub

    Public Sub RestoreState(
    savedBalance As Decimal,
    savedWithdrawnToday As Decimal,
    savedWithdrawDate As Date,
    savedFailedAttempts As Integer,
    savedIsLocked As Boolean
)
        _balance = savedBalance
        _withdrawnToday = savedWithdrawnToday
        _withdrawDate = savedWithdrawDate
        FailedAttempts = savedFailedAttempts
        IsLocked = savedIsLocked
    End Sub

    Private Sub ResetDailyIfNeeded()
        If _withdrawDate <> Date.Today Then
            _withdrawDate = Date.Today
            _withdrawnToday = 0D
        End If
    End Sub

    Private Sub Record(kind As TransactionType, amount As Decimal)
        _history.Add(New Transaction(kind, amount, _balance))
    End Sub

    Public Function Deposit(amount As Decimal) As OpResult
        If amount <= 0D Then Return New OpResult(False, "Amount must be greater than zero.")
        _balance += amount
        Record(TransactionType.Deposit, amount)
        Return New OpResult(True, $"Deposited {amount:N2}.")
    End Function

    Public Function Withdraw(amount As Decimal) As OpResult
        If amount <= 0D Then Return New OpResult(False, "Amount must be greater than zero.")
        ResetDailyIfNeeded()
        If amount > _balance Then Return New OpResult(False, "Insufficient funds.")
        If _withdrawnToday + amount > DailyWithdrawLimit Then
            Return New OpResult(False, $"Daily limit exceeded. You can still withdraw {RemainingDailyLimit:N2} today.")
        End If
        _balance -= amount
        _withdrawnToday += amount
        Record(TransactionType.Withdrawal, amount)
        Return New OpResult(True, $"Withdrew {amount:N2}.")
    End Function

    Public Function TransferOut(amount As Decimal) As OpResult
        If amount <= 0D Then Return New OpResult(False, "Amount must be greater than zero.")
        If amount > _balance Then Return New OpResult(False, "Insufficient funds.")
        _balance -= amount
        Record(TransactionType.TransferOut, amount)
        Return New OpResult(True, "OK")
    End Function

    Public Sub TransferIn(amount As Decimal)
        _balance += amount
        Record(TransactionType.TransferIn, amount)
    End Sub

    Public Function ChangePin(oldPin As String, newPin As String) As OpResult
        If Not CheckPin(oldPin) Then
            Return New OpResult(False, "Current PIN is incorrect.")
        End If

        If Not IsValidPin(newPin) Then
            Return New OpResult(False, "New PIN must be exactly 4 digits.")
        End If

        SetPin(newPin)
        Record(TransactionType.PinChange, 0D)

        Return New OpResult(True, "PIN changed successfully.")
    End Function
End Class

' ---------- The bank (stores all accounts) ----------
Public Class Bank
    Private ReadOnly _accounts As New Dictionary(Of String, Account)

    Public Sub New()
        ' Accounts will be loaded from the database.
    End Sub

    Public ReadOnly Property AllAccounts As IEnumerable(Of Account)
        Get
            Return _accounts.Values
        End Get
    End Property

    Public Function AddAccount(acc As Account) As Boolean
        If _accounts.ContainsKey(acc.CardNumber) Then Return False
        _accounts.Add(acc.CardNumber, acc)
        Return True
    End Function

    Public Function FindAccount(cardNumber As String) As Account
        Dim acc As Account = Nothing
        _accounts.TryGetValue(cardNumber, acc)
        Return acc
    End Function

    Public Function Transfer(source As Account, destCard As String, amount As Decimal) As OpResult
        Dim dest As Account = FindAccount(destCard)
        If dest Is Nothing Then Return New OpResult(False, "Destination account not found.")
        If dest Is source Then Return New OpResult(False, "You cannot transfer to your own account.")
        Dim r As OpResult = source.TransferOut(amount)
        If Not r.Success Then Return r
        dest.TransferIn(amount)
        Return New OpResult(True, $"Transferred {amount:N2} to {dest.HolderName}.")
    End Function
End Class

' ---------- The ATM's physical cash ----------
Public Class CashDispenser
    Private ReadOnly Denominations() As Integer = {100, 50, 20, 10}
    Private ReadOnly _stock As New Dictionary(Of Integer, Integer)

    Public Sub New()
        _stock(100) = 20
        _stock(50) = 40
        _stock(20) = 50
        _stock(10) = 100
    End Sub

    Public ReadOnly Property TotalCash As Decimal
        Get
            Dim total As Integer = 0
            For Each d As Integer In Denominations
                total += d * _stock(d)
            Next
            Return total
        End Get
    End Property

    Public Function NoteCount(denomination As Integer) As Integer
        Dim n As Integer = 0
        _stock.TryGetValue(denomination, n)
        Return n
    End Function

    Public Function IsValidDenomination(d As Integer) As Boolean
        Return _stock.ContainsKey(d)
    End Function

    Public Sub AddNotes(denomination As Integer, count As Integer)
        _stock(denomination) += count
    End Sub

    Public Sub RestoreStock(denomination As Integer, count As Integer)

        If Not _stock.ContainsKey(denomination) Then
            Throw New ArgumentException("Invalid denomination.")
        End If

        If count < 0 Then
            Throw New ArgumentOutOfRangeException(NameOf(count))
        End If

        _stock(denomination) = count

    End Sub

    ' Works out which notes to give. Returns Nothing if it is not possible.
    Public Function PlanDispense(amount As Integer) As Dictionary(Of Integer, Integer)
        Dim plan As New Dictionary(Of Integer, Integer)
        Dim remaining As Integer = amount
        For Each d As Integer In Denominations
            Dim n As Integer = Math.Min(remaining \ d, _stock(d))
            If n > 0 Then
                plan(d) = n
                remaining -= n * d
            End If
        Next
        If remaining <> 0 Then Return Nothing
        Return plan
    End Function

    Public Sub Dispense(plan As Dictionary(Of Integer, Integer))
        For Each pair As KeyValuePair(Of Integer, Integer) In plan
            _stock(pair.Key) -= pair.Value
        Next
    End Sub
End Class


