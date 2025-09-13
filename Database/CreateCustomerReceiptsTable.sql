-- Customer Receipts Table Creation Script
-- This script creates the CustomerReceipts table for the Distribution Software

USE DistributionDB;
GO

-- Create CustomerReceipts table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CustomerReceipts' AND xtype='U')
BEGIN
    CREATE TABLE CustomerReceipts (
        ReceiptId INT IDENTITY(1,1) PRIMARY KEY,
        ReceiptNumber NVARCHAR(50) NOT NULL UNIQUE,
        ReceiptDate DATETIME NOT NULL,
        CustomerId INT NOT NULL,
        CustomerName NVARCHAR(255),
        CustomerPhone NVARCHAR(50),
        CustomerAddress NVARCHAR(500),
        Amount DECIMAL(18,2) NOT NULL,
        PaymentMethod NVARCHAR(50) NOT NULL,
        InvoiceReference NVARCHAR(100),
        Description NVARCHAR(500),
        ReceivedBy NVARCHAR(255) NOT NULL,
        Status NVARCHAR(50) NOT NULL DEFAULT 'COMPLETED',
        Remarks NVARCHAR(500),
        
        -- Additional payment details
        BankName NVARCHAR(255),
        ChequeNumber NVARCHAR(100),
        ChequeDate DATETIME,
        TransactionId NVARCHAR(100),
        CardNumber NVARCHAR(50),
        CardType NVARCHAR(50),
        MobileNumber NVARCHAR(50),
        PaymentReference NVARCHAR(100),
        
        -- Audit fields
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT,
        ModifiedDate DATETIME,
        
        -- Foreign key constraints
        CONSTRAINT FK_CustomerReceipts_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
        CONSTRAINT FK_CustomerReceipts_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserId),
        CONSTRAINT FK_CustomerReceipts_ModifiedBy FOREIGN KEY (ModifiedBy) REFERENCES Users(UserId)
    );
    
    PRINT 'CustomerReceipts table created successfully.';
END
ELSE
BEGIN
    PRINT 'CustomerReceipts table already exists.';
END
GO

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CustomerReceipts_ReceiptNumber')
BEGIN
    CREATE INDEX IX_CustomerReceipts_ReceiptNumber ON CustomerReceipts(ReceiptNumber);
    PRINT 'Index IX_CustomerReceipts_ReceiptNumber created.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CustomerReceipts_CustomerId')
BEGIN
    CREATE INDEX IX_CustomerReceipts_CustomerId ON CustomerReceipts(CustomerId);
    PRINT 'Index IX_CustomerReceipts_CustomerId created.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CustomerReceipts_ReceiptDate')
BEGIN
    CREATE INDEX IX_CustomerReceipts_ReceiptDate ON CustomerReceipts(ReceiptDate);
    PRINT 'Index IX_CustomerReceipts_ReceiptDate created.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CustomerReceipts_Status')
BEGIN
    CREATE INDEX IX_CustomerReceipts_Status ON CustomerReceipts(Status);
    PRINT 'Index IX_CustomerReceipts_Status created.';
END
GO

-- Insert sample data for testing (optional)
IF NOT EXISTS (SELECT * FROM CustomerReceipts WHERE ReceiptNumber = 'RCP2025010001')
BEGIN
    INSERT INTO CustomerReceipts (
        ReceiptNumber, ReceiptDate, CustomerId, CustomerName, CustomerPhone, CustomerAddress,
        Amount, PaymentMethod, InvoiceReference, Description, ReceivedBy, Status, CreatedBy
    ) VALUES (
        'RCP2025010001', '2025-01-15', 1, 'Walk-in Customer', '', 'Walk-in Customer',
        1500.00, 'CASH', 'INV-2025010001', 'Payment for Invoice INV-2025010001', 'System Admin', 'COMPLETED', 1
    );
    
    PRINT 'Sample customer receipt data inserted.';
END
GO

PRINT 'Customer Receipts table setup completed successfully!';
