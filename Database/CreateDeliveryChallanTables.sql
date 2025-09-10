-- Create Delivery Challan Tables
-- Run this script in DistributionDB database

USE DistributionDB;
GO

-- Create DeliveryChallans table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DeliveryChallans' AND xtype='U')
BEGIN
    CREATE TABLE DeliveryChallans (
        ChallanId INT IDENTITY(1,1) PRIMARY KEY,
        ChallanNo NVARCHAR(50) UNIQUE NOT NULL,
        SalesInvoiceId INT NULL,
        CustomerName NVARCHAR(255) NULL,
        CustomerAddress NVARCHAR(500) NULL,
        ChallanDate DATETIME NOT NULL,
        VehicleNo NVARCHAR(50) NULL,
        DriverName NVARCHAR(100) NULL,
        DriverPhone NVARCHAR(20) NULL,
        Remarks NVARCHAR(500) NULL,
        BarcodeImage NVARCHAR(MAX) NULL,
        Status NVARCHAR(20) DEFAULT 'DRAFT',
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NOT NULL,
        UpdatedDate DATETIME NULL,
        UpdatedBy INT NULL
    );
    
    PRINT 'DeliveryChallans table created successfully';
END
ELSE
BEGIN
    PRINT 'DeliveryChallans table already exists';
END
GO

-- Create DeliveryChallanItems table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DeliveryChallanItems' AND xtype='U')
BEGIN
    CREATE TABLE DeliveryChallanItems (
        ChallanItemId INT IDENTITY(1,1) PRIMARY KEY,
        ChallanId INT NOT NULL,
        ProductId INT NULL,
        ProductCode NVARCHAR(50) NULL,
        ProductName NVARCHAR(255) NULL,
        Quantity DECIMAL(18,2) NOT NULL,
        Unit NVARCHAR(20) NULL,
        UnitPrice DECIMAL(18,2) NOT NULL,
        TotalAmount DECIMAL(18,2) NOT NULL,
        Remarks NVARCHAR(500) NULL,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NOT NULL
    );
    
    PRINT 'DeliveryChallanItems table created successfully';
END
ELSE
BEGIN
    PRINT 'DeliveryChallanItems table already exists';
END
GO

-- Add foreign key constraints
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DeliveryChallans_SalesInvoices')
BEGIN
    ALTER TABLE DeliveryChallans
    ADD CONSTRAINT FK_DeliveryChallans_SalesInvoices
    FOREIGN KEY (SalesInvoiceId) REFERENCES SalesInvoices(SalesInvoiceId);
    
    PRINT 'Foreign key FK_DeliveryChallans_SalesInvoices added successfully';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_DeliveryChallans_SalesInvoices already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DeliveryChallanItems_DeliveryChallans')
BEGIN
    ALTER TABLE DeliveryChallanItems
    ADD CONSTRAINT FK_DeliveryChallanItems_DeliveryChallans
    FOREIGN KEY (ChallanId) REFERENCES DeliveryChallans(ChallanId) ON DELETE CASCADE;
    
    PRINT 'Foreign key FK_DeliveryChallanItems_DeliveryChallans added successfully';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_DeliveryChallanItems_DeliveryChallans already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DeliveryChallanItems_Products')
BEGIN
    ALTER TABLE DeliveryChallanItems
    ADD CONSTRAINT FK_DeliveryChallanItems_Products
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId);
    
    PRINT 'Foreign key FK_DeliveryChallanItems_Products added successfully';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_DeliveryChallanItems_Products already exists';
END
GO

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DeliveryChallans_ChallanNo')
BEGIN
    CREATE INDEX IX_DeliveryChallans_ChallanNo ON DeliveryChallans(ChallanNo);
    PRINT 'Index IX_DeliveryChallans_ChallanNo created successfully';
END
ELSE
BEGIN
    PRINT 'Index IX_DeliveryChallans_ChallanNo already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DeliveryChallans_ChallanDate')
BEGIN
    CREATE INDEX IX_DeliveryChallans_ChallanDate ON DeliveryChallans(ChallanDate);
    PRINT 'Index IX_DeliveryChallans_ChallanDate created successfully';
END
ELSE
BEGIN
    PRINT 'Index IX_DeliveryChallans_ChallanDate already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DeliveryChallanItems_ChallanId')
BEGIN
    CREATE INDEX IX_DeliveryChallanItems_ChallanId ON DeliveryChallanItems(ChallanId);
    PRINT 'Index IX_DeliveryChallanItems_ChallanId created successfully';
END
ELSE
BEGIN
    PRINT 'Index IX_DeliveryChallanItems_ChallanId already exists';
END
GO

-- Insert sample data for testing
IF NOT EXISTS (SELECT * FROM DeliveryChallans WHERE ChallanNo = 'DC000001')
BEGIN
    INSERT INTO DeliveryChallans (
        ChallanNo, SalesInvoiceId, CustomerName, CustomerAddress, ChallanDate, 
        VehicleNo, DriverName, DriverPhone, Remarks, Status, CreatedBy
    ) VALUES (
        'DC000001', NULL, 'Sample Customer', '123 Sample Street, Sample City', 
        GETDATE(), 'ABC-123', 'John Driver', '123-456-7890', 'Sample delivery challan', 
        'DRAFT', 1
    );
    
    DECLARE @ChallanId INT = SCOPE_IDENTITY();
    
    INSERT INTO DeliveryChallanItems (
        ChallanId, ProductCode, ProductName, Quantity, Unit, UnitPrice, TotalAmount, CreatedBy
    ) VALUES (
        @ChallanId, 'PROD001', 'Sample Product', 10, 'PCS', 100.00, 1000.00, 1
    );
    
    PRINT 'Sample delivery challan data inserted successfully';
END
ELSE
BEGIN
    PRINT 'Sample delivery challan data already exists';
END
GO

PRINT 'Delivery Challan tables setup completed successfully!';
