-- Sample Stock Movement Data
-- This script creates sample stock movement records for testing the Stock Movement Report

-- First, ensure we have at least one product and warehouse
IF NOT EXISTS (SELECT 1 FROM Products WHERE IsActive = 1)
BEGIN
    INSERT INTO Products (ProductCode, ProductName, Description, Category, UnitPrice, StockQuantity, ReorderLevel, IsActive, CreatedDate)
    VALUES ('SAMPLE001', 'Sample Product', 'Sample product for testing', 'General', 10.00, 0, 10, 1, GETDATE())
END

IF NOT EXISTS (SELECT 1 FROM Warehouses WHERE IsActive = 1)
BEGIN
    INSERT INTO Warehouses (WarehouseName, Location, ContactPerson, ContactPhone, IsActive, CreatedDate, CreatedBy)
    VALUES ('Main Warehouse', 'Main Location', 'Warehouse Manager', '123-456-7890', 1, GETDATE(), 1)
END

-- Clear existing sample data
DELETE FROM StockMovement WHERE Remarks LIKE '%Sample%' OR Remarks LIKE '%Initial%' OR Remarks LIKE '%testing%'

-- Insert sample stock movements
INSERT INTO StockMovement (ProductId, WarehouseId, MovementType, Quantity, ReferenceType, MovementDate, CreatedBy, Remarks)
SELECT 
    p.ProductId,
    w.WarehouseId,
    'IN',
    100,
    'ADJUSTMENT',
    DATEADD(DAY, -5, GETDATE()),
    1,
    'Initial stock adjustment - Sample Data'
FROM Products p, Warehouses w
WHERE p.IsActive = 1 AND w.IsActive = 1
AND NOT EXISTS (SELECT 1 FROM StockMovement WHERE ProductId = p.ProductId AND WarehouseId = w.WarehouseId)

INSERT INTO StockMovement (ProductId, WarehouseId, MovementType, Quantity, ReferenceType, MovementDate, CreatedBy, Remarks)
SELECT 
    p.ProductId,
    w.WarehouseId,
    'OUT',
    25,
    'ADJUSTMENT',
    DATEADD(DAY, -3, GETDATE()),
    1,
    'Stock adjustment - Sample Data'
FROM Products p, Warehouses w
WHERE p.IsActive = 1 AND w.IsActive = 1

INSERT INTO StockMovement (ProductId, WarehouseId, MovementType, Quantity, ReferenceType, MovementDate, CreatedBy, Remarks)
SELECT 
    p.ProductId,
    w.WarehouseId,
    'IN',
    50,
    'ADJUSTMENT',
    DATEADD(DAY, -1, GETDATE()),
    1,
    'Stock replenishment - Sample Data'
FROM Products p, Warehouses w
WHERE p.IsActive = 1 AND w.IsActive = 1

-- Show the created sample data
SELECT 
    sm.MovementId,
    sm.MovementDate,
    p.ProductCode,
    p.ProductName,
    w.WarehouseName,
    sm.MovementType,
    sm.Quantity,
    sm.ReferenceType,
    sm.Remarks
FROM StockMovement sm
INNER JOIN Products p ON sm.ProductId = p.ProductId
INNER JOIN Warehouses w ON sm.WarehouseId = w.WarehouseId
WHERE sm.Remarks LIKE '%Sample%' OR sm.Remarks LIKE '%Initial%' OR sm.Remarks LIKE '%testing%'
ORDER BY sm.MovementDate DESC
