--Database Name BankingDB
CREATE TABLE UserTable (
    UserID INT PRIMARY KEY not null IDENTITY (0,1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    City VARCHAR(50) NOT NULL,
    Password VARBINARY(255) NOT NULL
);

CREATE TABLE AccountsTable (
    AccountID INT PRIMARY KEY not null IDENTITY (0,1),
    AccountNumber AS ('PKZ' + RIGHT('000000' + CAST(AccountID AS VARCHAR(6)), 6)) PERSISTED,
    UserID INT NOT NULL,
    AccountType VARCHAR(20) NOT NULL,
    Balance DECIMAL(15, 2) NOT NULL,
    DateOpened DATE NOT NULL,
    FOREIGN KEY (UserID) REFERENCES UserTable(UserID)
);

CREATE TABLE TransactionsTable (
    TID INT PRIMARY KEY not null IDENTITY(0,1),
    AccountID INT NOT NULL,
    TransactionType VARCHAR(20) NOT NULL,
    Amount DECIMAL(15, 2) NOT NULL,
    TransactionDate DATE NOT NULL,
    Description VARCHAR(255),
    FOREIGN KEY (AccountID) REFERENCES AccountsTable(AccountID)
);

CREATE TABLE BillTable (
    BillID INT PRIMARY KEY not null IDENTITY(0,1),
    AccountID INT NOT NULL,
    BillType VARCHAR(50) NOT NULL,
    Amount DECIMAL(15, 2) NOT NULL,
    PaymentDate DATE NOT NULL,
    FOREIGN KEY (AccountID) REFERENCES AccountsTable(AccountID)
);

CREATE TABLE StatementTable (
    SID INT PRIMARY KEY NOT NULL IDENTITY (2000,1),
    [ThisAccount] INT not null,
    [TDate] DATETIME not null,
    [FromAccount] VARCHAR(6) not null,
    [ToAccount] VARCHAR(6) not null,
    [Type] VARCHAR(50) not null,
    [OldBalance] INT not null,
    [Amount] INT not null,
    [NewBalance] INT not null,
    FOREIGN KEY ([ThisAccount]) REFERENCES AccountsTable(AccountID)
   );
  
ALTER TABLE StatementTable
ALTER COLUMN FromAccount VARCHAR(10) NOT NULL;

ALTER TABLE StatementTable
ALTER COLUMN ToAccount VARCHAR(10) NOT NULL;
SELECT * FROM StatementTable
