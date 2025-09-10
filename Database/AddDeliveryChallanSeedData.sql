-- Add comprehensive seed data for Delivery Challan system
-- Run this script in DistributionDB database

USE DistributionDB;
GO

-- Clear existing sample data
DELETE FROM DeliveryChallanItems WHERE ChallanId IN (SELECT ChallanId FROM DeliveryChallans WHERE ChallanNo LIKE 'DC%');
DELETE FROM DeliveryChallans WHERE ChallanNo LIKE 'DC%';
GO

-- Insert sample delivery challans
INSERT INTO DeliveryChallans (
    ChallanNo, SalesInvoiceId, CustomerName, CustomerAddress, ChallanDate, 
    VehicleNo, DriverName, DriverPhone, Remarks, Status, CreatedBy
) VALUES 
('DC000001', NULL, 'ABC Trading Company', '123 Business Street, Downtown, City 10001', 
 GETDATE() - 5, 'ABC-123', 'John Smith', '555-0101', 'Urgent delivery - fragile items', 'DELIVERED', 1),

('DC000002', NULL, 'XYZ Retail Store', '456 Commerce Avenue, Uptown, City 10002', 
 GETDATE() - 3, 'XYZ-456', 'Mike Johnson', '555-0102', 'Regular delivery', 'IN_TRANSIT', 1),

('DC000003', NULL, 'Global Electronics Ltd', '789 Technology Park, Tech City, City 10003', 
 GETDATE() - 1, 'GEL-789', 'Sarah Wilson', '555-0103', 'Electronics delivery - handle with care', 'CONFIRMED', 1),

('DC000004', NULL, 'Metro Supermarket', '321 Retail Plaza, Metro City, City 10004', 
 GETDATE(), 'MET-321', 'David Brown', '555-0104', 'Fresh goods delivery', 'DRAFT', 1),

('DC000005', NULL, 'Premium Fashion Store', '654 Fashion District, Style City, City 10005', 
 GETDATE() + 1, 'PFS-654', 'Lisa Davis', '555-0105', 'Fashion items - careful handling', 'SCHEDULED', 1);
GO

-- Get the inserted challan IDs for adding items
DECLARE @Challan1 INT = (SELECT ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC000001');
DECLARE @Challan2 INT = (SELECT ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC000002');
DECLARE @Challan3 INT = (SELECT ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC000003');
DECLARE @Challan4 INT = (SELECT ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC000004');
DECLARE @Challan5 INT = (SELECT ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC000005');
GO

-- Insert sample delivery challan items
INSERT INTO DeliveryChallanItems (
    ChallanId, ProductCode, ProductName, Quantity, Unit, UnitPrice, TotalAmount, CreatedBy
) VALUES 
-- Items for DC000001 (ABC Trading Company)
(@Challan1, 'PROD001', 'Premium Office Chair', 10, 'PCS', 250.00, 2500.00, 1),
(@Challan1, 'PROD002', 'Executive Desk', 5, 'PCS', 500.00, 2500.00, 1),
(@Challan1, 'PROD003', 'Filing Cabinet', 8, 'PCS', 150.00, 1200.00, 1),

-- Items for DC000002 (XYZ Retail Store)
(@Challan2, 'PROD004', 'LED Monitor 24"', 15, 'PCS', 200.00, 3000.00, 1),
(@Challan2, 'PROD005', 'Wireless Keyboard', 20, 'PCS', 50.00, 1000.00, 1),
(@Challan2, 'PROD006', 'Optical Mouse', 25, 'PCS', 25.00, 625.00, 1),

-- Items for DC000003 (Global Electronics Ltd)
(@Challan3, 'PROD007', 'Laptop Computer', 12, 'PCS', 800.00, 9600.00, 1),
(@Challan3, 'PROD008', 'Tablet Device', 8, 'PCS', 400.00, 3200.00, 1),
(@Challan3, 'PROD009', 'Smartphone', 20, 'PCS', 300.00, 6000.00, 1),

-- Items for DC000004 (Metro Supermarket)
(@Challan4, 'PROD010', 'Fresh Vegetables Box', 50, 'KG', 5.00, 250.00, 1),
(@Challan4, 'PROD011', 'Dairy Products Pack', 30, 'PCS', 8.00, 240.00, 1),
(@Challan4, 'PROD012', 'Bakery Items', 25, 'PCS', 3.00, 75.00, 1),

-- Items for DC000005 (Premium Fashion Store)
(@Challan5, 'PROD013', 'Designer Shirt', 40, 'PCS', 75.00, 3000.00, 1),
(@Challan5, 'PROD014', 'Premium Jeans', 30, 'PCS', 100.00, 3000.00, 1),
(@Challan5, 'PROD015', 'Fashion Accessories', 60, 'PCS', 25.00, 1500.00, 1);
GO

-- Update barcode images for the challans (simplified base64 encoded images)
UPDATE DeliveryChallans 
SET BarcodeImage = 'iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg=='
WHERE ChallanNo IN ('DC000001', 'DC000002', 'DC000003', 'DC000004', 'DC000005');
GO

-- Verify the data
SELECT 
    dc.ChallanNo,
    dc.CustomerName,
    dc.ChallanDate,
    dc.VehicleNo,
    dc.DriverName,
    dc.Status,
    COUNT(dci.ChallanItemId) as ItemCount,
    SUM(dci.TotalAmount) as TotalAmount
FROM DeliveryChallans dc
LEFT JOIN DeliveryChallanItems dci ON dc.ChallanId = dci.ChallanId
WHERE dc.ChallanNo LIKE 'DC%'
GROUP BY dc.ChallanId, dc.ChallanNo, dc.CustomerName, dc.ChallanDate, dc.VehicleNo, dc.DriverName, dc.Status
ORDER BY dc.ChallanNo;
GO

PRINT 'Delivery Challan seed data inserted successfully!';
PRINT 'Total Delivery Challans: ' + CAST((SELECT COUNT(*) FROM DeliveryChallans WHERE ChallanNo LIKE 'DC%') AS VARCHAR(10));
PRINT 'Total Delivery Challan Items: ' + CAST((SELECT COUNT(*) FROM DeliveryChallanItems WHERE ChallanId IN (SELECT ChallanId FROM DeliveryChallans WHERE ChallanNo LIKE 'DC%')) AS VARCHAR(10));
