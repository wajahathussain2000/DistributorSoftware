-- Test script to verify Vehicle Master integration
-- Run this after creating the VehicleMaster table

-- Test 1: Verify VehicleMaster table exists and has correct structure
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'VehicleMaster'
ORDER BY ORDINAL_POSITION;

-- Test 2: Verify sample data was inserted
SELECT * FROM VehicleMaster;

-- Test 3: Verify DeliveryChallan table has VehicleId column
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'DeliveryChallan' 
AND COLUMN_NAME = 'VehicleId';

-- Test 4: Test foreign key constraint
SELECT 
    fk.name AS ForeignKeyName,
    tp.name AS ParentTable,
    cp.name AS ParentColumn,
    tr.name AS ReferencedTable,
    cr.name AS ReferencedColumn
FROM sys.foreign_keys fk
INNER JOIN sys.tables tp ON fk.parent_object_id = tp.object_id
INNER JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
INNER JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id
INNER JOIN sys.columns cr ON fkc.referenced_column_id = cr.column_id AND fkc.referenced_object_id = cr.object_id
WHERE tp.name = 'DeliveryChallan' AND tr.name = 'VehicleMaster';

-- Test 5: Test inserting a delivery challan with vehicle reference
INSERT INTO DeliveryChallan (
    ChallanNo, ChallanDate, CustomerName, CustomerAddress, 
    VehicleId, VehicleNo, DriverName, DriverPhone, 
    Status, CreatedDate, CreatedBy
)
VALUES (
    'DC-TEST-001', GETDATE(), 'Test Customer', 'Test Address',
    1, 'TN-01-AB-1234', 'Rajesh Kumar', '9876543210',
    'DRAFT', GETDATE(), 1
);

-- Test 6: Verify the insert worked
SELECT 
    dc.ChallanId,
    dc.ChallanNo,
    dc.CustomerName,
    dc.VehicleId,
    dc.VehicleNo,
    dc.DriverName,
    vm.VehicleType,
    vm.TransporterName
FROM DeliveryChallan dc
LEFT JOIN VehicleMaster vm ON dc.VehicleId = vm.VehicleId
WHERE dc.ChallanNo = 'DC-TEST-001';

-- Test 7: Clean up test data
DELETE FROM DeliveryChallan WHERE ChallanNo = 'DC-TEST-001';

PRINT 'Vehicle Master integration test completed successfully!';
