USE [DistributionDB]
GO

-- Quick test data insertion for Stock Report
-- This script ensures there's basic data for testing the report

-- Check if we have any products
IF NOT EXISTS (SELECT 1 FROM Products WHERE IsActive = 1)
BEGIN
    PRINT 'No products found. Inserting test data...'
    
    -- Insert test categories if they don't exist
    IF NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryName = 'Test Category')
    BEGIN
        INSERT INTO Categories (CategoryName, Description, IsActive, CreatedDate, CreatedBy)
        VALUES ('Test Category', 'Test category for reports', 1, GETDATE(), 1)
    END
    
    -- Insert test brands if they don't exist
    IF NOT EXISTS (SELECT 1 FROM Brands WHERE BrandName = 'Test Brand')
    BEGIN
        INSERT INTO Brands (BrandName, Description, IsActive, CreatedDate, CreatedBy)
        VALUES ('Test Brand', 'Test brand for reports', 1, GETDATE(), 1)
    END
    
    -- Insert test units if they don't exist
    IF NOT EXISTS (SELECT 1 FROM Units WHERE UnitName = 'Piece')
    BEGIN
        INSERT INTO Units (UnitName, UnitCode, Description, IsActive, CreatedDate)
        VALUES ('Piece', 'PCS', 'Individual items', 1, GETDATE())
    END
    
    -- Insert test warehouses if they don't exist
    IF NOT EXISTS (SELECT 1 FROM Warehouses WHERE WarehouseName = 'Main Warehouse')
    BEGIN
        INSERT INTO Warehouses (WarehouseName, Location, ContactPerson, ContactPhone, IsActive, CreatedDate, CreatedBy)
        VALUES ('Main Warehouse', 'Test Location', 'Test Person', '123-456-7890', 1, GETDATE(), 1)
    END
    
    -- Insert test products
    INSERT INTO Products (ProductCode, ProductName, Description, Category, UnitPrice, StockQuantity, ReorderLevel, IsActive, CreatedDate, BrandId, CategoryId, UnitId, WarehouseId, BatchNumber, ExpiryDate)
    VALUES 
    ('TEST001', 'Test Product 1', 'Test product for report generation', 'Test Category', 99.99, 50, 10, 1, GETDATE(), 1, 1, 1, 1, 'BATCH001', '2025-12-31'),
    ('TEST002', 'Test Product 2', 'Another test product', 'Test Category', 149.99, 25, 5, 1, GETDATE(), 1, 1, 1, 1, 'BATCH002', '2025-12-31'),
    ('TEST003', 'Test Product 3', 'Third test product', 'Test Category', 199.99, 15, 8, 1, GETDATE(), 1, 1, 1, 1, 'BATCH003', '2025-12-31')
    
    PRINT 'Test products inserted successfully'
END
ELSE
BEGIN
    PRINT 'Products already exist in database'
END

-- Check if we have stock records
IF NOT EXISTS (SELECT 1 FROM Stock)
BEGIN
    PRINT 'No stock records found. Inserting test stock data...'
    
    -- Insert stock records for existing products
    INSERT INTO Stock (ProductId, WarehouseId, Quantity, BatchNumber, ExpiryDate, LastUpdated, UpdatedBy)
    SELECT 
        p.ProductId,
        1, -- Main Warehouse
        p.StockQuantity,
        p.BatchNumber,
        p.ExpiryDate,
        GETDATE(),
        1
    FROM Products p
    WHERE p.IsActive = 1
    
    PRINT 'Test stock records inserted successfully'
END
ELSE
BEGIN
    PRINT 'Stock records already exist in database'
END

-- Show summary
SELECT 
    'Products' AS TableName,
    COUNT(*) AS RecordCount
FROM Products 
WHERE IsActive = 1

UNION ALL

SELECT 
    'Stock' AS TableName,
    COUNT(*) AS RecordCount
FROM Stock

UNION ALL

SELECT 
    'Categories' AS TableName,
    COUNT(*) AS RecordCount
FROM Categories 
WHERE IsActive = 1

UNION ALL

SELECT 
    'Brands' AS TableName,
    COUNT(*) AS RecordCount
FROM Brands 
WHERE IsActive = 1

UNION ALL

SELECT 
    'Warehouses' AS TableName,
    COUNT(*) AS RecordCount
FROM Warehouses 
WHERE IsActive = 1

PRINT 'Database test data setup completed!'
