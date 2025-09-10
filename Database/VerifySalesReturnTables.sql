-- Verify Sales Return Tables in DistributionDB
USE DistributionDB
GO

-- Check if SalesReturns table exists and its structure
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SalesReturns')
BEGIN
    PRINT 'SalesReturns table exists'
    SELECT COUNT(*) as RecordCount FROM SalesReturns
END
ELSE
BEGIN
    PRINT 'SalesReturns table does NOT exist'
END
GO

-- Check if SalesReturnItems table exists and its structure
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SalesReturnItems')
BEGIN
    PRINT 'SalesReturnItems table exists'
    SELECT COUNT(*) as RecordCount FROM SalesReturnItems
END
ELSE
BEGIN
    PRINT 'SalesReturnItems table does NOT exist'
END
GO

-- Check foreign key constraints
SELECT 
    fk.name as ForeignKeyName,
    tp.name as ParentTable,
    cp.name as ParentColumn,
    tr.name as ReferencedTable,
    cr.name as ReferencedColumn
FROM sys.foreign_keys fk
INNER JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
INNER JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.columns cp ON fkc.parent_object_id = cp.object_id AND fkc.parent_column_id = cp.column_id
INNER JOIN sys.columns cr ON fkc.referenced_object_id = cr.object_id AND fkc.referenced_column_id = cr.column_id
WHERE tp.name IN ('SalesReturns', 'SalesReturnItems')
GO
