-- Integrate Bank Reconciliation with Accounting Forms
-- Add BankAccountId to JournalVouchers and related tables

USE DistributionDB;
GO

-- Add BankAccountId to JournalVouchers table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('JournalVouchers') AND name = 'BankAccountId')
BEGIN
    ALTER TABLE JournalVouchers ADD BankAccountId INT NULL;
    PRINT 'Added BankAccountId column to JournalVouchers table';
END
ELSE
BEGIN
    PRINT 'BankAccountId column already exists in JournalVouchers table';
END

-- Add BankAccountId to SalesPayments table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('SalesPayments') AND name = 'BankAccountId')
BEGIN
    ALTER TABLE SalesPayments ADD BankAccountId INT NULL;
    PRINT 'Added BankAccountId column to SalesPayments table';
END
ELSE
BEGIN
    PRINT 'BankAccountId column already exists in SalesPayments table';
END

-- Add BankAccountId to PurchasePayments table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('PurchasePayments') AND name = 'BankAccountId')
BEGIN
    ALTER TABLE PurchasePayments ADD BankAccountId INT NULL;
    PRINT 'Added BankAccountId column to PurchasePayments table';
END
ELSE
BEGIN
    PRINT 'BankAccountId column already exists in PurchasePayments table';
END

-- Add foreign key constraints
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_JournalVouchers_BankAccounts')
BEGIN
    ALTER TABLE JournalVouchers 
    ADD CONSTRAINT FK_JournalVouchers_BankAccounts 
    FOREIGN KEY (BankAccountId) REFERENCES BankAccounts(BankAccountId);
    PRINT 'Added foreign key constraint FK_JournalVouchers_BankAccounts';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_JournalVouchers_BankAccounts already exists';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesPayments_BankAccounts')
BEGIN
    ALTER TABLE SalesPayments 
    ADD CONSTRAINT FK_SalesPayments_BankAccounts 
    FOREIGN KEY (BankAccountId) REFERENCES BankAccounts(BankAccountId);
    PRINT 'Added foreign key constraint FK_SalesPayments_BankAccounts';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_SalesPayments_BankAccounts already exists';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_PurchasePayments_BankAccounts')
BEGIN
    ALTER TABLE PurchasePayments 
    ADD CONSTRAINT FK_PurchasePayments_BankAccounts 
    FOREIGN KEY (BankAccountId) REFERENCES BankAccounts(BankAccountId);
    PRINT 'Added foreign key constraint FK_PurchasePayments_BankAccounts';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_PurchasePayments_BankAccounts already exists';
END

PRINT 'Bank Reconciliation integration with Accounting forms completed successfully!';
