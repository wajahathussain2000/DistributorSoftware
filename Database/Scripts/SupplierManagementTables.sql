-- =============================================
-- Supplier Management Database Tables
-- =============================================

USE DistributionDB;
GO

-- =============================================
-- 1. Suppliers Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Suppliers' AND xtype='U')
BEGIN
    CREATE TABLE Suppliers (
        SupplierId INT IDENTITY(1,1) PRIMARY KEY,
        SupplierCode NVARCHAR(50) NOT NULL UNIQUE,
        SupplierName NVARCHAR(200) NOT NULL,
        ContactPerson NVARCHAR(100),
        ContactNumber NVARCHAR(20),
        Email NVARCHAR(100),
        Address NVARCHAR(500),
        City NVARCHAR(100),
        State NVARCHAR(100),
        Country NVARCHAR(100) DEFAULT 'Pakistan',
        PostalCode NVARCHAR(20),
        
        -- Business Information
        GST NVARCHAR(50),
        NTN NVARCHAR(50),
        BusinessType NVARCHAR(100),
        
        -- Payment Terms
        PaymentTermsDays INT DEFAULT 30,
        PaymentTermsDate DATE,
        
        -- Barcode and QR Code
        Barcode NVARCHAR(100),
        QRCode NVARCHAR(MAX),
        
        -- Status and Audit
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy NVARCHAR(100),
        ModifiedDate DATETIME,
        ModifiedBy NVARCHAR(100),
        
        -- Additional Fields
        Notes NVARCHAR(1000),
        CreditLimit DECIMAL(18,2) DEFAULT 0,
        CurrentBalance DECIMAL(18,2) DEFAULT 0
    );
    
    PRINT 'Suppliers table created successfully';
END
ELSE
BEGIN
    PRINT 'Suppliers table already exists';
END
GO

-- =============================================
-- 2. Supplier Transactions Table (Ledger)
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SupplierTransactions' AND xtype='U')
BEGIN
    CREATE TABLE SupplierTransactions (
        TransactionId INT IDENTITY(1,1) PRIMARY KEY,
        SupplierId INT NOT NULL,
        TransactionType NVARCHAR(50) NOT NULL, -- Purchase, Payment, Return, Adjustment, Credit Note, Debit Note
        TransactionDate DATETIME NOT NULL,
        ReferenceNumber NVARCHAR(100),
        Description NVARCHAR(500),
        
        -- Amount Details
        DebitAmount DECIMAL(18,2) DEFAULT 0,
        CreditAmount DECIMAL(18,2) DEFAULT 0,
        Balance DECIMAL(18,2) DEFAULT 0,
        
        -- Payment Details (for payments)
        PaymentMethod NVARCHAR(50), -- Cash, Bank Transfer, Check, Credit Card
        BankName NVARCHAR(100),
        CheckNumber NVARCHAR(50),
        TransactionReference NVARCHAR(100),
        
        -- Purchase Details (for purchases)
        PurchaseOrderNumber NVARCHAR(100),
        InvoiceNumber NVARCHAR(100),
        InvoiceDate DATE,
        DueDate DATE,
        
        -- Status and Audit
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy NVARCHAR(100),
        ModifiedDate DATETIME,
        ModifiedBy NVARCHAR(100),
        
        -- Additional Fields
        Notes NVARCHAR(1000),
        AttachmentPath NVARCHAR(500),
        
        -- Foreign Key
        FOREIGN KEY (SupplierId) REFERENCES Suppliers(SupplierId) ON DELETE CASCADE
    );
    
    PRINT 'SupplierTransactions table created successfully';
END
ELSE
BEGIN
    PRINT 'SupplierTransactions table already exists';
END
GO

-- =============================================
-- 3. Supplier Payments Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SupplierPayments' AND xtype='U')
BEGIN
    CREATE TABLE SupplierPayments (
        PaymentId INT IDENTITY(1,1) PRIMARY KEY,
        SupplierId INT NOT NULL,
        PaymentNumber NVARCHAR(50) NOT NULL UNIQUE,
        PaymentDate DATETIME NOT NULL,
        PaymentAmount DECIMAL(18,2) NOT NULL,
        PaymentMethod NVARCHAR(50) NOT NULL,
        
        -- Payment Details
        BankName NVARCHAR(100),
        AccountNumber NVARCHAR(100),
        CheckNumber NVARCHAR(50),
        CheckDate DATE,
        TransactionReference NVARCHAR(100),
        
        -- Payment Allocation
        AllocatedAmount DECIMAL(18,2) DEFAULT 0,
        UnallocatedAmount DECIMAL(18,2) DEFAULT 0,
        
        -- Status and Audit
        Status NVARCHAR(50) DEFAULT 'Pending', -- Pending, Completed, Cancelled
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy NVARCHAR(100),
        ModifiedDate DATETIME,
        ModifiedBy NVARCHAR(100),
        
        -- Additional Fields
        Notes NVARCHAR(1000),
        ReceiptPath NVARCHAR(500),
        
        -- Foreign Key
        FOREIGN KEY (SupplierId) REFERENCES Suppliers(SupplierId) ON DELETE CASCADE
    );
    
    PRINT 'SupplierPayments table created successfully';
END
ELSE
BEGIN
    PRINT 'SupplierPayments table already exists';
END
GO

-- =============================================
-- 4. Supplier Payment Allocations Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SupplierPaymentAllocations' AND xtype='U')
BEGIN
    CREATE TABLE SupplierPaymentAllocations (
        AllocationId INT IDENTITY(1,1) PRIMARY KEY,
        PaymentId INT NOT NULL,
        TransactionId INT NOT NULL,
        AllocatedAmount DECIMAL(18,2) NOT NULL,
        AllocationDate DATETIME DEFAULT GETDATE(),
        CreatedBy NVARCHAR(100),
        
        -- Foreign Keys
        FOREIGN KEY (PaymentId) REFERENCES SupplierPayments(PaymentId) ON DELETE CASCADE,
        FOREIGN KEY (TransactionId) REFERENCES SupplierTransactions(TransactionId) ON DELETE CASCADE
    );
    
    PRINT 'SupplierPaymentAllocations table created successfully';
END
ELSE
BEGIN
    PRINT 'SupplierPaymentAllocations table already exists';
END
GO

-- =============================================
-- 5. Create Indexes for Performance
-- =============================================

-- Suppliers Indexes
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Suppliers_SupplierCode')
    CREATE INDEX IX_Suppliers_SupplierCode ON Suppliers(SupplierCode);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Suppliers_IsActive')
    CREATE INDEX IX_Suppliers_IsActive ON Suppliers(IsActive);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Suppliers_SupplierName')
    CREATE INDEX IX_Suppliers_SupplierName ON Suppliers(SupplierName);

-- SupplierTransactions Indexes
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SupplierTransactions_SupplierId')
    CREATE INDEX IX_SupplierTransactions_SupplierId ON SupplierTransactions(SupplierId);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SupplierTransactions_TransactionDate')
    CREATE INDEX IX_SupplierTransactions_TransactionDate ON SupplierTransactions(TransactionDate);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SupplierTransactions_TransactionType')
    CREATE INDEX IX_SupplierTransactions_TransactionType ON SupplierTransactions(TransactionType);

-- SupplierPayments Indexes
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SupplierPayments_SupplierId')
    CREATE INDEX IX_SupplierPayments_SupplierId ON SupplierPayments(SupplierId);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SupplierPayments_PaymentDate')
    CREATE INDEX IX_SupplierPayments_PaymentDate ON SupplierPayments(PaymentDate);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SupplierPayments_PaymentNumber')
    CREATE INDEX IX_SupplierPayments_PaymentNumber ON SupplierPayments(PaymentNumber);

PRINT 'All indexes created successfully';
GO

-- =============================================
-- 6. Create Stored Procedures
-- =============================================

-- Procedure to generate next supplier code
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GenerateSupplierCode')
    DROP PROCEDURE sp_GenerateSupplierCode;
GO

CREATE PROCEDURE sp_GenerateSupplierCode
AS
BEGIN
    DECLARE @NextCode NVARCHAR(50);
    DECLARE @MaxNumber INT;
    
    SELECT @MaxNumber = ISNULL(MAX(CAST(SUBSTRING(SupplierCode, 4, LEN(SupplierCode)) AS INT)), 0)
    FROM Suppliers
    WHERE SupplierCode LIKE 'SUP%' AND ISNUMERIC(SUBSTRING(SupplierCode, 4, LEN(SupplierCode))) = 1;
    
    SET @NextCode = 'SUP' + RIGHT('000000' + CAST(@MaxNumber + 1 AS NVARCHAR(10)), 6);
    
    SELECT @NextCode AS NextSupplierCode;
END
GO

-- Procedure to update supplier balance
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_UpdateSupplierBalance')
    DROP PROCEDURE sp_UpdateSupplierBalance;
GO

CREATE PROCEDURE sp_UpdateSupplierBalance
    @SupplierId INT
AS
BEGIN
    DECLARE @CurrentBalance DECIMAL(18,2);
    
    SELECT @CurrentBalance = ISNULL(SUM(DebitAmount - CreditAmount), 0)
    FROM SupplierTransactions
    WHERE SupplierId = @SupplierId AND IsActive = 1;
    
    UPDATE Suppliers
    SET CurrentBalance = @CurrentBalance,
        ModifiedDate = GETDATE()
    WHERE SupplierId = @SupplierId;
    
    SELECT @CurrentBalance AS CurrentBalance;
END
GO

-- Procedure to get supplier ledger
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetSupplierLedger')
    DROP PROCEDURE sp_GetSupplierLedger;
GO

CREATE PROCEDURE sp_GetSupplierLedger
    @SupplierId INT,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL
AS
BEGIN
    SET @FromDate = ISNULL(@FromDate, '1900-01-01');
    SET @ToDate = ISNULL(@ToDate, GETDATE());
    
    SELECT 
        st.TransactionId,
        st.TransactionType,
        st.TransactionDate,
        st.ReferenceNumber,
        st.Description,
        st.DebitAmount,
        st.CreditAmount,
        st.Balance,
        st.PaymentMethod,
        st.Notes,
        s.SupplierName,
        s.SupplierCode
    FROM SupplierTransactions st
    INNER JOIN Suppliers s ON st.SupplierId = s.SupplierId
    WHERE st.SupplierId = @SupplierId
        AND st.TransactionDate BETWEEN @FromDate AND @ToDate
        AND st.IsActive = 1
    ORDER BY st.TransactionDate DESC, st.TransactionId DESC;
END
GO

PRINT 'All stored procedures created successfully';
GO

-- =============================================
-- 7. Create Views for Reporting
-- =============================================

-- View for supplier summary
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_SupplierSummary')
    DROP VIEW vw_SupplierSummary;
GO

CREATE VIEW vw_SupplierSummary
AS
SELECT 
    s.SupplierId,
    s.SupplierCode,
    s.SupplierName,
    s.ContactPerson,
    s.ContactNumber,
    s.Email,
    s.City,
    s.State,
    s.GST,
    s.NTN,
    s.PaymentTermsDays,
    s.CurrentBalance,
    s.CreditLimit,
    s.IsActive,
    s.CreatedDate,
    COUNT(st.TransactionId) AS TotalTransactions,
    SUM(CASE WHEN st.TransactionType = 'Purchase' THEN st.DebitAmount ELSE 0 END) AS TotalPurchases,
    SUM(CASE WHEN st.TransactionType = 'Payment' THEN st.CreditAmount ELSE 0 END) AS TotalPayments,
    MAX(st.TransactionDate) AS LastTransactionDate
FROM Suppliers s
LEFT JOIN SupplierTransactions st ON s.SupplierId = st.SupplierId AND st.IsActive = 1
GROUP BY s.SupplierId, s.SupplierCode, s.SupplierName, s.ContactPerson, s.ContactNumber, 
         s.Email, s.City, s.State, s.GST, s.NTN, s.PaymentTermsDays, s.CurrentBalance, 
         s.CreditLimit, s.IsActive, s.CreatedDate;
GO

PRINT 'All views created successfully';
GO

PRINT '=============================================';
PRINT 'Supplier Management Database Setup Complete!';
PRINT '=============================================';
