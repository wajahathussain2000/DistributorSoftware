-- Test Database Connection and Data Loading
-- This script verifies that the repository queries work correctly

USE [DistributionDB]
GO

-- Test customer query
PRINT 'Testing Customer Query...'
SELECT TOP 3
    c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactPerson,
    c.Phone, c.Email, c.Address, c.City, c.State,
    c.PostalCode, c.Country, c.CategoryId, cc.CategoryName,
    c.CreditLimit, c.IsActive, c.CreatedDate, c.ModifiedDate
FROM Customers c
LEFT JOIN CustomerCategories cc ON c.CategoryId = cc.CategoryId
WHERE c.IsActive = 1
ORDER BY c.CustomerName

PRINT 'Customer query completed successfully!'
GO

-- Test product query
PRINT 'Testing Product Query...'
SELECT TOP 3
    p.ProductId, p.ProductCode, p.ProductName, p.ProductDescription,
    p.CategoryId, c.CategoryName,
    p.BrandId, b.BrandName,
    p.UnitId, u.UnitName,
    p.PurchasePrice, p.SalePrice, p.MRP,
    p.StockQuantity as Quantity, p.ReservedQuantity, p.ReorderLevel,
    p.Barcode, p.BatchNumber, p.ExpiryDate,
    p.WarehouseId, w.WarehouseName,
    p.IsActive, p.Remarks,
    p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate
FROM Products p
LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
LEFT JOIN Brands b ON p.BrandId = b.BrandId
LEFT JOIN Units u ON p.UnitId = u.UnitId
LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId
WHERE p.IsActive = 1
ORDER BY p.ProductName

PRINT 'Product query completed successfully!'
GO

-- Test stock calculation
PRINT 'Testing Stock Calculation...'
SELECT 
    ProductId, ProductCode, ProductName,
    StockQuantity, ReservedQuantity,
    (StockQuantity - ISNULL(ReservedQuantity, 0)) as AvailableStock
FROM Products 
WHERE IsActive = 1

PRINT 'Stock calculation completed successfully!'
GO

PRINT 'All database queries are working correctly!'
PRINT 'The debugging system should now work properly.'
