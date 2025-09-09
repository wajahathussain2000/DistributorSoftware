-- Fix Sales Issues Script
-- This script fixes the remaining issues with the sales system

USE [DistributionDB]
GO

-- Create Walk-in Customer if it doesn't exist
IF NOT EXISTS (SELECT * FROM Customers WHERE CustomerCode = 'WALKIN')
BEGIN
    INSERT INTO Customers (
        CustomerCode, CompanyName, ContactName, Email, Phone, Address, 
        City, State, PostalCode, Country, IsActive, CreatedDate
    ) VALUES (
        'WALKIN', 'Walk-in Customer', 'Walk-in Customer', 'walkin@company.com', 
        '', 'Walk-in Customer', 'Karachi', 'Sindh', '75000', 'Pakistan', 
        1, GETDATE()
    )
    PRINT 'Walk-in Customer created successfully!'
END
ELSE
BEGIN
    PRINT 'Walk-in Customer already exists!'
END
GO

-- Add ReservedQuantity column to Products table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'ReservedQuantity')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [ReservedQuantity] [decimal](18, 2) NOT NULL DEFAULT ((0))
    PRINT 'ReservedQuantity column added to Products table!'
END
ELSE
BEGIN
    PRINT 'ReservedQuantity column already exists in Products table!'
END
GO

-- Add SalePrice column to Products table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'SalePrice')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [SalePrice] [decimal](18, 2) NOT NULL DEFAULT ((0))
    PRINT 'SalePrice column added to Products table!'
END
ELSE
BEGIN
    PRINT 'SalePrice column already exists in Products table!'
END
GO

-- Update existing products to have SalePrice = UnitPrice if SalePrice is 0
UPDATE Products 
SET SalePrice = UnitPrice 
WHERE SalePrice = 0 OR SalePrice IS NULL
GO

-- Add some sample products for testing
IF NOT EXISTS (SELECT * FROM Products WHERE ProductCode = 'P001')
BEGIN
    INSERT INTO Products (
        ProductCode, ProductName, Description, Category, UnitPrice, SalePrice,
        StockQuantity, ReorderLevel, IsActive, CreatedDate
    ) VALUES 
    ('P001', 'Sample Product A', 'Sample product for testing', 'General', 1000.00, 1000.00, 100, 10, 1, GETDATE()),
    ('P002', 'Sample Product B', 'Sample product for testing', 'General', 2000.00, 2000.00, 50, 10, 1, GETDATE()),
    ('P003', 'Sample Product C', 'Sample product for testing', 'General', 1500.00, 1500.00, 75, 10, 1, GETDATE()),
    ('P004', 'Sample Product D', 'Sample product for testing', 'General', 3000.00, 3000.00, 25, 10, 1, GETDATE())
    
    PRINT 'Sample products created successfully!'
END
ELSE
BEGIN
    PRINT 'Sample products already exist!'
END
GO

-- Verify the tables structure
PRINT '=== VERIFICATION ==='
PRINT 'SalesInvoices table columns:'
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'SalesInvoices' 
ORDER BY ORDINAL_POSITION

PRINT 'SalesInvoiceDetails table columns:'
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'SalesInvoiceDetails' 
ORDER BY ORDINAL_POSITION

PRINT 'SalesPayments table columns:'
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'SalesPayments' 
ORDER BY ORDINAL_POSITION

PRINT 'Products table columns:'
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Products' 
ORDER BY ORDINAL_POSITION

PRINT '=== FIXES COMPLETED ==='
PRINT 'All sales system issues have been resolved!'
PRINT 'Ready for testing!'
