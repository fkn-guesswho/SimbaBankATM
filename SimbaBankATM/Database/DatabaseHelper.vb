Option Strict On
Option Explicit On

Imports System
Imports System.Globalization
Imports Microsoft.Data.Sqlite

Public NotInheritable Class DatabaseHelper

    Private Sub New()
    End Sub

    Private Shared ReadOnly _databasePath As String =
        System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SimbaBankATM",
            "SimbaBankATM.db"
        )

    Private Shared ReadOnly _connectionString As String =
        $"Data Source={_databasePath};Foreign Keys=True"

    Public Shared ReadOnly Property DatabasePath As String
        Get
            Return _databasePath
        End Get
    End Property

    Public Shared Function OpenConnection() As SqliteConnection
        Dim folderPath As String =
            System.IO.Path.GetDirectoryName(_databasePath)

        If Not System.IO.Directory.Exists(folderPath) Then
            System.IO.Directory.CreateDirectory(folderPath)
        End If

        Dim connection As New SqliteConnection(_connectionString)
        connection.Open()
        Return connection
    End Function

    Public Shared Sub InitializeDatabase()
        Using connection As SqliteConnection = OpenConnection()
            Using command As SqliteCommand = connection.CreateCommand()

                command.CommandText = String.Join(vbCrLf, New String() {
                    "CREATE TABLE IF NOT EXISTS Accounts (",
                    "CardNumber TEXT PRIMARY KEY,",
                    "CustomerName TEXT NOT NULL,",
                    "PinSalt BLOB NOT NULL,",
                    "PinHash BLOB NOT NULL,",
                    "Balance TEXT NOT NULL,",
                    "FailedAttempts INTEGER NOT NULL DEFAULT 0,",
                    "IsLocked INTEGER NOT NULL DEFAULT 0,",
                    "WithdrawnToday TEXT NOT NULL DEFAULT '0',",
                    "WithdrawDate TEXT NOT NULL",
                    ");",
                    "CREATE TABLE IF NOT EXISTS Transactions (",
                    "TransactionId INTEGER PRIMARY KEY AUTOINCREMENT,",
                    "CardNumber TEXT NOT NULL,",
                    "TransactionType TEXT NOT NULL,",
                    "Amount TEXT NOT NULL,",
                    "BalanceAfter TEXT NOT NULL,",
                    "TransactionDate TEXT NOT NULL,",
                    "FOREIGN KEY (CardNumber) REFERENCES Accounts(CardNumber)",
                    ");",
                    "CREATE TABLE IF NOT EXISTS ATMCash (",
                    "Denomination INTEGER PRIMARY KEY,",
                    "Quantity INTEGER NOT NULL DEFAULT 0",
                    ");",
                    "CREATE INDEX IF NOT EXISTS IX_Transactions_CardNumber",
                    "ON Transactions(CardNumber);"
                })

                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Shared Sub LoadAccounts(bank As Bank)
        Using connection As SqliteConnection = OpenConnection()

            Using command As SqliteCommand = connection.CreateCommand()
                command.CommandText =
                    "SELECT CardNumber, CustomerName, PinSalt, PinHash, " &
                    "Balance, FailedAttempts, IsLocked, WithdrawnToday, WithdrawDate " &
                    "FROM Accounts"

                Using reader As SqliteDataReader = command.ExecuteReader()
                    While reader.Read()

                        Dim cardNumber As String = reader.GetString(0)
                        Dim customerName As String = reader.GetString(1)
                        Dim pinSalt As Byte() = CType(reader("PinSalt"), Byte())
                        Dim pinHash As Byte() = CType(reader("PinHash"), Byte())

                        Dim balance As Decimal =
                            Decimal.Parse(reader.GetString(4), CultureInfo.InvariantCulture)

                        Dim failedAttempts As Integer = reader.GetInt32(5)
                        Dim isLocked As Boolean = reader.GetInt32(6) <> 0

                        Dim withdrawnToday As Decimal =
                            Decimal.Parse(reader.GetString(7), CultureInfo.InvariantCulture)

                        Dim withdrawDate As Date =
                            Date.ParseExact(
                                reader.GetString(8),
                                "yyyy-MM-dd",
                                CultureInfo.InvariantCulture
                            )

                        Dim account As New Account(
                            cardNumber,
                            customerName,
                            pinSalt,
                            pinHash,
                            balance
                        )

                        account.RestoreState(
                            balance,
                            withdrawnToday,
                            withdrawDate,
                            failedAttempts,
                            isLocked
                        )

                        bank.AddAccount(account)
                    End While
                End Using
            End Using

            For Each account As Account In bank.AllAccounts
                LoadTransactions(connection, account)
            Next

        End Using
    End Sub

    Private Shared Sub LoadTransactions(
        connection As SqliteConnection,
        account As Account
    )
        Using command As SqliteCommand = connection.CreateCommand()
            command.CommandText =
                "SELECT TransactionType, Amount, BalanceAfter, TransactionDate " &
                "FROM Transactions WHERE CardNumber = @CardNumber " &
                "ORDER BY TransactionDate, TransactionId"

            command.Parameters.AddWithValue("@CardNumber", account.CardNumber)

            Using reader As SqliteDataReader = command.ExecuteReader()
                While reader.Read()

                    Dim kind As TransactionType =
                        [Enum].Parse(Of TransactionType)(reader.GetString(0))

                    Dim amount As Decimal =
                        Decimal.Parse(reader.GetString(1), CultureInfo.InvariantCulture)

                    Dim balanceAfter As Decimal =
                        Decimal.Parse(reader.GetString(2), CultureInfo.InvariantCulture)

                    Dim timestamp As DateTime =
                        DateTime.Parse(
                            reader.GetString(3),
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.RoundtripKind
                        )

                    account.RestoreTransaction(
                        New Transaction(kind, amount, balanceAfter, timestamp)
                    )
                End While
            End Using
        End Using
    End Sub

    Public Shared Sub SaveAccount(account As Account)
        SaveAccounts(account)
    End Sub


    Public Shared Sub SaveAccounts(ParamArray accounts() As Account)

        Using connection As SqliteConnection = OpenConnection()
            connection.Open()

            Using dbTransaction As SqliteTransaction = connection.BeginTransaction()

                For Each account As Account In accounts

                    If account Is Nothing Then
                        Throw New ArgumentNullException(NameOf(accounts))
                    End If

                    SaveAccountWithinTransaction(
                    connection,
                    dbTransaction,
                    account
                )

                Next

                dbTransaction.Commit()

            End Using
        End Using

    End Sub


    Private Shared Sub SaveAccountWithinTransaction(
    connection As SqliteConnection,
    dbTransaction As SqliteTransaction,
    account As Account
)

        Dim accountSql As String =
        "INSERT INTO Accounts " &
        "(CardNumber, CustomerName, PinSalt, PinHash, Balance, " &
        "FailedAttempts, IsLocked, WithdrawnToday, WithdrawDate) " &
        "VALUES " &
        "(@card, @name, @salt, @hash, @balance, " &
        "@attempts, @locked, @withdrawn, @withdrawDate) " &
        "ON CONFLICT(CardNumber) DO UPDATE SET " &
        "CustomerName = @name, " &
        "PinSalt = @salt, " &
        "PinHash = @hash, " &
        "Balance = @balance, " &
        "FailedAttempts = @attempts, " &
        "IsLocked = @locked, " &
        "WithdrawnToday = @withdrawn, " &
        "WithdrawDate = @withdrawDate;"

        Using command As New SqliteCommand(
        accountSql, connection, dbTransaction)

            command.Parameters.AddWithValue("@card", account.CardNumber)
            command.Parameters.AddWithValue("@name", account.HolderName)
            command.Parameters.AddWithValue("@salt", account.PinSalt)
            command.Parameters.AddWithValue("@hash", account.PinHash)

            command.Parameters.AddWithValue(
            "@balance",
            account.Balance.ToString(CultureInfo.InvariantCulture))

            command.Parameters.AddWithValue("@attempts", account.FailedAttempts)
            command.Parameters.AddWithValue("@locked", If(account.IsLocked, 1, 0))

            command.Parameters.AddWithValue(
            "@withdrawn",
            account.WithdrawnToday.ToString(CultureInfo.InvariantCulture))

            command.Parameters.AddWithValue(
            "@withdrawDate",
            account.WithdrawDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))

            command.ExecuteNonQuery()

        End Using

        ' Replace this account's saved transaction history.
        Using deleteCommand As New SqliteCommand(
        "DELETE FROM Transactions WHERE CardNumber = @card;",
        connection, dbTransaction)

            deleteCommand.Parameters.AddWithValue("@card", account.CardNumber)
            deleteCommand.ExecuteNonQuery()

        End Using

        For Each item As Transaction In account.History

            Using command As New SqliteCommand(
            "INSERT INTO Transactions " &
            "(CardNumber, TransactionType, Amount, BalanceAfter, TransactionDate) " &
            "VALUES (@card, @type, @amount, @balance, @date);",
            connection, dbTransaction)

                command.Parameters.AddWithValue("@card", account.CardNumber)
                command.Parameters.AddWithValue("@type", item.Kind.ToString())

                command.Parameters.AddWithValue(
                "@amount",
                item.Amount.ToString(CultureInfo.InvariantCulture))

                command.Parameters.AddWithValue(
                "@balance",
                item.BalanceAfter.ToString(CultureInfo.InvariantCulture))

                command.Parameters.AddWithValue(
                "@date",
                item.Timestamp.ToString("O", CultureInfo.InvariantCulture))

                command.ExecuteNonQuery()

            End Using

        Next

    End Sub

    Public Shared Sub SaveCash(atm As CashDispenser)

        Using connection As SqliteConnection = OpenConnection()
            connection.Open()

            Using transaction = connection.BeginTransaction()

                Using deleteCommand As New SqliteCommand(
                    "DELETE FROM ATMCash;", connection, transaction)

                    deleteCommand.ExecuteNonQuery()
                End Using

                Dim denominations() As Integer = {100, 50, 20, 10}

                For Each denomination As Integer In denominations

                    Using command As New SqliteCommand(
                        "INSERT INTO ATMCash (Denomination, Quantity) " &
                        "VALUES (@denomination, @quantity);",
                        connection, transaction)

                        command.Parameters.AddWithValue("@denomination", denomination)
                        command.Parameters.AddWithValue("@quantity",
                            atm.NoteCount(denomination))

                        command.ExecuteNonQuery()
                    End Using

                Next

                transaction.Commit()
            End Using
        End Using

    End Sub


    Public Shared Sub LoadCash(atm As CashDispenser)

        Using connection As SqliteConnection = OpenConnection()
            connection.Open()

            Dim savedCash As New Dictionary(Of Integer, Integer)

            Using command As New SqliteCommand(
                "SELECT Denomination, Quantity FROM ATMCash;",
                connection)

                Using reader As SqliteDataReader = command.ExecuteReader()

                    While reader.Read()
                        savedCash(reader.GetInt32(0)) = reader.GetInt32(1)
                    End While

                End Using
            End Using

            If savedCash.Count = 0 Then
                SaveCash(atm)
                Return
            End If

            For Each denomination As Integer In {100, 50, 20, 10}

                If savedCash.ContainsKey(denomination) Then
                    atm.RestoreStock(denomination, savedCash(denomination))
                Else
                    atm.RestoreStock(denomination, 0)
                End If

            Next

        End Using

    End Sub

End Class