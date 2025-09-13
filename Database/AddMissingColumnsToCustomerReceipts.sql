-- Add Missing Columns to CustomerReceipts Table
-- This script adds the missing columns needed by our CustomerReceiptsForm

USE DistributionDB;
GO

-- Add missing columns
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'CustomerName')
BEGIN
    ALTER TABLE CustomerReceipts ADD CustomerName NVARCHAR(255);
    PRINT 'Added CustomerName column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'CustomerPhone')
BEGIN
    ALTER TABLE CustomerReceipts ADD CustomerPhone NVARCHAR(50);
    PRINT 'Added CustomerPhone column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'CustomerAddress')
BEGIN
    ALTER TABLE CustomerReceipts ADD CustomerAddress NVARCHAR(500);
    PRINT 'Added CustomerAddress column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'Amount')
BEGIN
    ALTER TABLE CustomerReceipts ADD Amount DECIMAL(18,2);
    PRINT 'Added Amount column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'InvoiceReference')
BEGIN
    ALTER TABLE CustomerReceipts ADD InvoiceReference NVARCHAR(100);
    PRINT 'Added InvoiceReference column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'Description')
BEGIN
    ALTER TABLE CustomerReceipts ADD Description NVARCHAR(500);
    PRINT 'Added Description column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'ReceivedBy')
BEGIN
    ALTER TABLE CustomerReceipts ADD ReceivedBy NVARCHAR(255);
    PRINT 'Added ReceivedBy column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'Status')
BEGIN
    ALTER TABLE CustomerReceipts ADD Status NVARCHAR(50) DEFAULT 'COMPLETED';
    PRINT 'Added Status column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'Remarks')
BEGIN
    ALTER TABLE CustomerReceipts ADD Remarks NVARCHAR(500);
    PRINT 'Added Remarks column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'ChequeNumber')
BEGIN
    ALTER TABLE CustomerReceipts ADD ChequeNumber NVARCHAR(100);
    PRINT 'Added ChequeNumber column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'ChequeDate')
BEGIN
    ALTER TABLE CustomerReceipts ADD ChequeDate DATETIME;
    PRINT 'Added ChequeDate column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'TransactionId')
BEGIN
    ALTER TABLE CustomerReceipts ADD TransactionId NVARCHAR(100);
    PRINT 'Added TransactionId column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'CardNumber')
BEGIN
    ALTER TABLE CustomerReceipts ADD CardNumber NVARCHAR(50);
    PRINT 'Added CardNumber column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'CardType')
BEGIN
    ALTER TABLE CustomerReceipts ADD CardType NVARCHAR(50);
    PRINT 'Added CardType column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'MobileNumber')
BEGIN
    ALTER TABLE CustomerReceipts ADD MobileNumber NVARCHAR(50);
    PRINT 'Added MobileNumber column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'PaymentReference')
BEGIN
    ALTER TABLE CustomerReceipts ADD PaymentReference NVARCHAR(100);
    PRINT 'Added PaymentReference column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'ModifiedBy')
BEGIN
    ALTER TABLE CustomerReceipts ADD ModifiedBy INT;
    PRINT 'Added ModifiedBy column.';
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CustomerReceipts' AND COLUMN_NAME = 'ModifiedDate')
BEGIN
    ALTER TABLE CustomerReceipts ADD ModifiedDate DATETIME;
    PRINT 'Added ModifiedDate column.';
END

-- Copy data from existing columns to new columns
UPDATE CustomerReceipts 
SET 
    Amount = AmountReceived,
    ChequeNumber = CheckNumber,
    ChequeDate = CheckDate,
    TransactionId = TransactionReference,
    Description = Notes,
    Status = 'COMPLETED',
    ReceivedBy = 'System Admin'
WHERE AmountReceived IS NOT NULL;

PRINT 'Copied data from existing columns to new columns.';

-- Add foreign key constraints if they don't exist
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CustomerReceipts_CustomerId')
BEGIN
    ALTER TABLE CustomerReceipts 
    ADD CONSTRAINT FK_CustomerReceipts_CustomerId 
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId);
    PRINT 'Added FK_CustomerReceipts_CustomerId constraint.';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CustomerReceipts_CreatedBy')
BEGIN
    ALTER TABLE CustomerReceipts 
    ADD CONSTRAINT FK_CustomerReceipts_CreatedBy 
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);
    PRINT 'Added FK_CustomerReceipts_CreatedBy constraint.';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CustomerReceipts_ModifiedBy')
BEGIN
    ALTER TABLE CustomerReceipts 
    ADD CONSTRAINT FK_CustomerReceipts_ModifiedBy 
    FOREIGN KEY (ModifiedBy) REFERENCES Users(UserId);
    PRINT 'Added FK_CustomerReceipts_ModifiedBy constraint.';
END

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CustomerReceipts_ReceiptNumber')
BEGIN
    CREATE INDEX IX_CustomerReceipts_ReceiptNumber ON CustomerReceipts(ReceiptNumber);
    PRINT 'Created index IX_CustomerReceipts_ReceiptNumber.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CustomerReceipts_CustomerId')
BEGIN
    CREATE INDEX IX_CustomerReceipts_CustomerId ON CustomerReceipts(CustomerId);
    PRINT 'Created index IX_CustomerReceipts_CustomerId.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CustomerReceipts_ReceiptDate')
BEGIN
    CREATE INDEX IX_CustomerReceipts_ReceiptDate ON CustomerReceipts(ReceiptDate);
    PRINT 'Created index IX_CustomerReceipts_ReceiptDate.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CustomerReceipts_Status')
BEGIN
    CREATE INDEX IX_CustomerReceipts_Status ON CustomerReceipts(Status);
    PRINT 'Created index IX_CustomerReceipts_Status.';
END

PRINT 'CustomerReceipts table setup completed successfully!';
GO
