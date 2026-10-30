
Option Strict On
Option Explicit On

Public Module AppData
    Public ReadOnly bank As New Bank()
    Public ReadOnly atm As New CashDispenser()
    Public CurrentAccount As Account = Nothing
End Module

