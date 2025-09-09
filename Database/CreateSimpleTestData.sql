-- Simple Test Data Script for Pakistan POS System
-- This script creates minimal test data compatible with existing table structure

USE [DistributionDB]
GO

-- Create sample customers with existing table structure
IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerCode = 'CUST001')
BEGIN
    INSERT INTO Customers (
        CustomerCode, CustomerName, ContactPerson, Email, Phone, Address, City, State, 
        PostalCode, Country, IsActive, CreatedDate, CreditLimit, CategoryId
    ) VALUES (
        'CUST001', 'ABC Company Ltd.', 'Mr. Ahmed Ali', 'ahmed@abccompany.com', '+92-21-1234567', 
        '123 Main Street', 'Karachi', 'Sindh', '75000', 'Pakistan', 1, GETDATE(), 100000.00, 1
    )
END
GO

IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerCode = 'CUST002')
BEGIN
    INSERT INTO Customers (
        CustomerCode, CustomerName, ContactPerson, Email, Phone, Address, City, State, 
        PostalCode, Country, IsActive, CreatedDate, CreditLimit, CategoryId
    ) VALUES (
        'CUST002', 'XYZ Trading Co.', 'Ms. Fatima Khan', 'fatima@xyztrading.com', '+92-42-9876543', 
        '456 Business Avenue', 'Lahore', 'Punjab', '54000', 'Pakistan', 1, GETDATE(), 50000.00, 1
    )
END
GO

-- Create sample products with existing table structure
IF NOT EXISTS (SELECT 1 FROM Products WHERE ProductCode = 'PROD001')
BEGIN
    INSERT INTO Products (
        ProductCode, ProductName, CategoryId, BrandId, UnitId,
        PurchasePrice, SalePrice, MRP, Quantity, ReservedQuantity, ReorderLevel,
        WarehouseId, IsActive, CreatedDate
    ) VALUES (
        'PROD001', 'Samsung Galaxy S21', 1, 1, 1,
        80000.00, 95000.00, 100000.00, 50, 0, 10,
        1, 1, GETDATE()
    )
END
GO

IF NOT EXISTS (SELECT 1 FROM Products WHERE ProductCode = 'PROD002')
BEGIN
    INSERT INTO Products (
        ProductCode, ProductName, CategoryId, BrandId, UnitId,
        PurchasePrice, SalePrice, MRP, Quantity, ReservedQuantity, ReorderLevel,
        WarehouseId, IsActive, CreatedDate
    ) VALUES (
        'PROD002', 'iPhone 13 Pro', 1, 2, 1,
        120000.00, 140000.00, 150000.00, 25, 0, 5,
        1, 1, GETDATE()
    )
END
GO

-- Create basic lookup tables if they don't exist
IF NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryId = 1)
BEGIN
    INSERT INTO Categories (CategoryId, CategoryName, IsActive, CreatedDate)
    VALUES (1, 'Electronics', 1, GETDATE())
END
GO

IF NOT EXISTS (SELECT 1 FROM Brands WHERE BrandId = 1)
BEGIN
    INSERT INTO Brands (BrandId, BrandName, IsActive, CreatedDate)
    VALUES (1, 'Samsung', 1, GETDATE())
END
GO

IF NOT EXISTS (SELECT 1 FROM Brands WHERE BrandId = 2)
BEGIN
    INSERT INTO Brands (BrandId, BrandName, IsActive, CreatedDate)
    VALUES (2, 'Apple', 1, GETDATE())
END
GO

IF NOT EXISTS (SELECT 1 FROM Units WHERE UnitId = 1)
BEGIN
    INSERT INTO Units (UnitId, UnitName, IsActive, CreatedDate)
    VALUES (1, 'Piece', 1, GETDATE())
END
GO

IF NOT EXISTS (SELECT 1 FROM Warehouses WHERE WarehouseId = 1)
BEGIN
    INSERT INTO Warehouses (WarehouseId, WarehouseName, Address, IsActive, CreatedDate)
    VALUES (1, 'Main Warehouse', 'Main Street, Karachi', 1, GETDATE())
END
GO

PRINT 'Sample data created successfully!'
PRINT 'Database is ready for testing the debugging features.'
PRINT 'You can now run the Sales Invoice Form to see the debugging output.'
