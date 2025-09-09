-- Test Database Connection and Data Loading
-- This script creates sample data to test the debugging features

USE [DistributionDB]
GO

-- Create sample customers if they don't exist
IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerCode = 'CUST001')
BEGIN
    INSERT INTO Customers (
        CustomerCode, CustomerName, ContactName, Phone, Mobile, Email, Address, City, State, 
        PostalCode, Country, CustomerCategoryId, CreditLimit, OutstandingBalance, PaymentTerms, 
        TaxNumber, GSTNumber, IsActive, Remarks, CreatedBy, CreatedDate
    ) VALUES (
        'CUST001', 'ABC Company Ltd.', 'Mr. Ahmed Ali', '+92-21-1234567', '0300-1234567', 
        'ahmed@abccompany.com', '123 Main Street', 'Karachi', 'Sindh', '75000', 'Pakistan', 
        1, 100000.00, 0.00, 'Net 30', 'TAX123456', 'GST123456789', 1, 'Regular customer', 1, GETDATE()
    )
END
GO

IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerCode = 'CUST002')
BEGIN
    INSERT INTO Customers (
        CustomerCode, CustomerName, ContactName, Phone, Mobile, Email, Address, City, State, 
        PostalCode, Country, CustomerCategoryId, CreditLimit, OutstandingBalance, PaymentTerms, 
        TaxNumber, GSTNumber, IsActive, Remarks, CreatedBy, CreatedDate
    ) VALUES (
        'CUST002', 'XYZ Trading Co.', 'Ms. Fatima Khan', '+92-42-9876543', '0321-9876543', 
        'fatima@xyztrading.com', '456 Business Avenue', 'Lahore', 'Punjab', '54000', 'Pakistan', 
        1, 50000.00, 0.00, 'Net 15', 'TAX789012', 'GST987654321', 1, 'New customer', 1, GETDATE()
    )
END
GO

-- Create sample products if they don't exist
IF NOT EXISTS (SELECT 1 FROM Products WHERE ProductCode = 'PROD001')
BEGIN
    INSERT INTO Products (
        ProductCode, ProductName, ProductDescription, CategoryId, BrandId, UnitId,
        PurchasePrice, SalePrice, MRP, Quantity, ReservedQuantity, ReorderLevel,
        Barcode, BatchNumber, ExpiryDate, WarehouseId, IsActive, Remarks, CreatedBy, CreatedDate
    ) VALUES (
        'PROD001', 'Samsung Galaxy S21', 'Latest smartphone model', 1, 1, 1,
        80000.00, 95000.00, 100000.00, 50, 0, 10,
        '1234567890123', 'BATCH001', DATEADD(MONTH, 12, GETDATE()), 1, 1, 'High demand product', 1, GETDATE()
    )
END
GO

IF NOT EXISTS (SELECT 1 FROM Products WHERE ProductCode = 'PROD002')
BEGIN
    INSERT INTO Products (
        ProductCode, ProductName, ProductDescription, CategoryId, BrandId, UnitId,
        PurchasePrice, SalePrice, MRP, Quantity, ReservedQuantity, ReorderLevel,
        Barcode, BatchNumber, ExpiryDate, WarehouseId, IsActive, Remarks, CreatedBy, CreatedDate
    ) VALUES (
        'PROD002', 'iPhone 13 Pro', 'Apple flagship smartphone', 1, 2, 1,
        120000.00, 140000.00, 150000.00, 25, 0, 5,
        '9876543210987', 'BATCH002', DATEADD(MONTH, 12, GETDATE()), 1, 1, 'Premium product', 1, GETDATE()
    )
END
GO

-- Create basic lookup tables if they don't exist
IF NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryId = 1)
BEGIN
    INSERT INTO Categories (CategoryId, CategoryName, IsActive, CreatedBy, CreatedDate)
    VALUES (1, 'Electronics', 1, 1, GETDATE())
END
GO

IF NOT EXISTS (SELECT 1 FROM Brands WHERE BrandId = 1)
BEGIN
    INSERT INTO Brands (BrandId, BrandName, IsActive, CreatedBy, CreatedDate)
    VALUES (1, 'Samsung', 1, 1, GETDATE())
END
GO

IF NOT EXISTS (SELECT 1 FROM Brands WHERE BrandId = 2)
BEGIN
    INSERT INTO Brands (BrandId, BrandName, IsActive, CreatedBy, CreatedDate)
    VALUES (2, 'Apple', 1, 1, GETDATE())
END
GO

IF NOT EXISTS (SELECT 1 FROM Units WHERE UnitId = 1)
BEGIN
    INSERT INTO Units (UnitId, UnitName, IsActive, CreatedBy, CreatedDate)
    VALUES (1, 'Piece', 1, 1, GETDATE())
END
GO

IF NOT EXISTS (SELECT 1 FROM Warehouses WHERE WarehouseId = 1)
BEGIN
    INSERT INTO Warehouses (WarehouseId, WarehouseName, Address, IsActive, CreatedBy, CreatedDate)
    VALUES (1, 'Main Warehouse', 'Main Street, Karachi', 1, 1, GETDATE())
END
GO

IF NOT EXISTS (SELECT 1 FROM CustomerCategories WHERE CategoryId = 1)
BEGIN
    INSERT INTO CustomerCategories (CategoryId, CategoryName, IsActive, CreatedBy, CreatedDate)
    VALUES (1, 'Corporate', 1, 1, GETDATE())
END
GO

PRINT 'Sample data created successfully!'
PRINT 'Database is ready for testing the debugging features.'
PRINT 'You can now run the Sales Invoice Form to see the debugging output.'
