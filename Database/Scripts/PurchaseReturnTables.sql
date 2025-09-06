-- =============================================
-- Purchase Return Database Tables
-- =============================================

USE DistributionDB;
GO

-- =============================================
-- 1. PurchaseReturns Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PurchaseReturns' AND xtype='U')
BEGIN
    CREATE TABLE PurchaseReturns (
        PurchaseReturnId INT IDENTITY(1,1) PRIMARY KEY,
        ReturnNumber NVARCHAR(50) NOT NULL UNIQUE,
        Barcode NVARCHAR(100) NOT NULL,
        SupplierId INT NOT NULL,
        ReferencePurchaseId INT NULL,
        ReturnDate DATETIME NOT NULL DEFAULT GETDATE(),
        TaxAdjust DECIMAL(18,2) DEFAULT 0,
        DiscountAdjust DECIMAL(18,2) DEFAULT 0,
        FreightAdjust DECIMAL(18,2) DEFAULT 0,
        NetReturnAmount DECIMAL(18,2) DEFAULT 0,
        Reason NVARCHAR(500) NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT 'Draft',
        
        -- Audit fields
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL,
        
        -- Foreign key constraints
        CONSTRAINT FK_PurchaseReturns_Suppliers FOREIGN KEY (SupplierId) REFERENCES Suppliers(SupplierId),
        CONSTRAINT FK_PurchaseReturns_PurchaseInvoices FOREIGN KEY (ReferencePurchaseId) REFERENCES PurchaseInvoices(PurchaseInvoiceId),
        CONSTRAINT FK_PurchaseReturns_Users_Created FOREIGN KEY (CreatedBy) REFERENCES Users(UserId),
        CONSTRAINT FK_PurchaseReturns_Users_Modified FOREIGN KEY (ModifiedBy) REFERENCES Users(UserId),
        
        -- Check constraints
        CONSTRAINT CK_PurchaseReturns_Status CHECK (Status IN ('Draft', 'Posted', 'Cancelled')),
        CONSTRAINT CK_PurchaseReturns_NetReturnAmount CHECK (NetReturnAmount >= 0)
    );
    
    PRINT 'PurchaseReturns table created successfully';
END
ELSE
BEGIN
    PRINT 'PurchaseReturns table already exists';
END
GO

-- =============================================
-- 2. PurchaseReturnItems Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PurchaseReturnItems' AND xtype='U')
BEGIN
    CREATE TABLE PurchaseReturnItems (
        PurchaseReturnItemId INT IDENTITY(1,1) PRIMARY KEY,
        PurchaseReturnId INT NOT NULL,
        ProductId INT NOT NULL,
        Quantity DECIMAL(18,3) NOT NULL,
        UnitPrice DECIMAL(18,2) NOT NULL,
        LineTotal DECIMAL(18,2) NOT NULL,
        BatchNumber NVARCHAR(100) NULL,
        ExpiryDate DATETIME NULL,
        Notes NVARCHAR(500) NULL,
        
        -- Foreign key constraints
        CONSTRAINT FK_PurchaseReturnItems_PurchaseReturns FOREIGN KEY (PurchaseReturnId) REFERENCES PurchaseReturns(PurchaseReturnId) ON DELETE CASCADE,
        CONSTRAINT FK_PurchaseReturnItems_Products FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
        
        -- Check constraints
        CONSTRAINT CK_PurchaseReturnItems_Quantity CHECK (Quantity > 0),
        CONSTRAINT CK_PurchaseReturnItems_UnitPrice CHECK (UnitPrice >= 0),
        CONSTRAINT CK_PurchaseReturnItems_LineTotal CHECK (LineTotal >= 0)
    );
    
    PRINT 'PurchaseReturnItems table created successfully';
END
ELSE
BEGIN
    PRINT 'PurchaseReturnItems table already exists';
END
GO

-- =============================================
-- 3. Create Indexes for Performance
-- =============================================

-- Index on ReturnNumber for quick lookups
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseReturns_ReturnNumber')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX IX_PurchaseReturns_ReturnNumber 
    ON PurchaseReturns(ReturnNumber);
    PRINT 'Index IX_PurchaseReturns_ReturnNumber created successfully';
END
GO

-- Index on SupplierId for filtering
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseReturns_SupplierId')
BEGIN
    CREATE NONCLUSTERED INDEX IX_PurchaseReturns_SupplierId 
    ON PurchaseReturns(SupplierId);
    PRINT 'Index IX_PurchaseReturns_SupplierId created successfully';
END
GO

-- Index on ReturnDate for date range queries
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseReturns_ReturnDate')
BEGIN
    CREATE NONCLUSTERED INDEX IX_PurchaseReturns_ReturnDate 
    ON PurchaseReturns(ReturnDate);
    PRINT 'Index IX_PurchaseReturns_ReturnDate created successfully';
END
GO

-- Index on Status for filtering
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseReturns_Status')
BEGIN
    CREATE NONCLUSTERED INDEX IX_PurchaseReturns_Status 
    ON PurchaseReturns(Status);
    PRINT 'Index IX_PurchaseReturns_Status created successfully';
END
GO

-- Index on PurchaseReturnId in Items table
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseReturnItems_PurchaseReturnId')
BEGIN
    CREATE NONCLUSTERED INDEX IX_PurchaseReturnItems_PurchaseReturnId 
    ON PurchaseReturnItems(PurchaseReturnId);
    PRINT 'Index IX_PurchaseReturnItems_PurchaseReturnId created successfully';
END
GO

-- Index on ProductId in Items table
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseReturnItems_ProductId')
BEGIN
    CREATE NONCLUSTERED INDEX IX_PurchaseReturnItems_ProductId 
    ON PurchaseReturnItems(ProductId);
    PRINT 'Index IX_PurchaseReturnItems_ProductId created successfully';
END
GO

-- =============================================
-- 4. Create Stored Procedures
-- =============================================

-- Stored procedure to get next return number
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetNextPurchaseReturnNumber')
    DROP PROCEDURE sp_GetNextPurchaseReturnNumber;
GO

CREATE PROCEDURE sp_GetNextPurchaseReturnNumber
AS
BEGIN
    DECLARE @Today VARCHAR(8) = FORMAT(GETDATE(), 'yyyyMMdd');
    DECLARE @NextNumber INT;
    
    SELECT @NextNumber = ISNULL(MAX(CAST(SUBSTRING(ReturnNumber, 12, 5) AS INT)), 0) + 1 
    FROM PurchaseReturns 
    WHERE ReturnNumber LIKE 'PR-' + @Today + '-%';
    
    SELECT 'PR-' + @Today + '-' + RIGHT('00000' + CAST(@NextNumber AS VARCHAR), 5) AS NextReturnNumber;
END
GO

-- Stored procedure to calculate net return amount
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_CalculatePurchaseReturnAmount')
    DROP PROCEDURE sp_CalculatePurchaseReturnAmount;
GO

CREATE PROCEDURE sp_CalculatePurchaseReturnAmount
    @PurchaseReturnId INT
AS
BEGIN
    DECLARE @SubTotal DECIMAL(18,2);
    DECLARE @TaxAdjust DECIMAL(18,2);
    DECLARE @DiscountAdjust DECIMAL(18,2);
    DECLARE @FreightAdjust DECIMAL(18,2);
    DECLARE @NetAmount DECIMAL(18,2);
    
    -- Calculate subtotal from items
    SELECT @SubTotal = ISNULL(SUM(LineTotal), 0)
    FROM PurchaseReturnItems
    WHERE PurchaseReturnId = @PurchaseReturnId;
    
    -- Get adjustments
    SELECT @TaxAdjust = TaxAdjust,
           @DiscountAdjust = DiscountAdjust,
           @FreightAdjust = FreightAdjust
    FROM PurchaseReturns
    WHERE PurchaseReturnId = @PurchaseReturnId;
    
    -- Calculate net amount
    SET @NetAmount = @SubTotal + @TaxAdjust - @DiscountAdjust + @FreightAdjust;
    
    -- Update the purchase return
    UPDATE PurchaseReturns
    SET NetReturnAmount = @NetAmount
    WHERE PurchaseReturnId = @PurchaseReturnId;
    
    SELECT @NetAmount AS NetReturnAmount;
END
GO

PRINT 'Purchase Return database tables and procedures created successfully!';
