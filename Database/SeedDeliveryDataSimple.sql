-- Simple seed data for delivery-related tables using existing data
-- This script adds sample data to test the delivery reports

-- Add some SalesInvoices using existing customers
INSERT INTO SalesInvoices (InvoiceNumber, InvoiceDate, CustomerId, TotalAmount, PaidAmount, BalanceAmount, PaymentMode, Status, CreatedDate, CreatedBy)
VALUES 
('INV202501001', '2025-01-15', 1, 5000.00, 3000.00, 2000.00, 'Cash', 'Pending', GETDATE(), 1),
('INV202501002', '2025-01-16', 3, 7500.00, 7500.00, 0.00, 'Bank Transfer', 'Paid', GETDATE(), 1),
('INV202501003', '2025-01-17', 4, 3200.00, 0.00, 3200.00, 'Credit', 'Pending', GETDATE(), 1),
('INV202501004', '2025-01-18', 5, 4500.00, 2000.00, 2500.00, 'Cash', 'Pending', GETDATE(), 1),
('INV202501005', '2025-01-19', 6, 2800.00, 2800.00, 0.00, 'Bank Transfer', 'Paid', GETDATE(), 1);

-- Add more DeliverySchedules with current date range
INSERT INTO DeliverySchedules (ScheduleRef, ScheduledDateTime, VehicleId, VehicleNo, RouteId, DriverName, DriverContact, Status, DispatchDateTime, DeliveredDateTime, ReturnedDateTime, Remarks, CreatedDate, CreatedBy)
VALUES 
('DS202501009', '2025-01-15 09:00:00', 1, 'VH001', 1, 'Driver One', '555-2001', 'SCHEDULED', NULL, NULL, NULL, 'Regular delivery', GETDATE(), 1),
('DS202501010', '2025-01-16 10:30:00', 2, 'VH002', 2, 'Driver Two', '555-2002', 'DISPATCHED', '2025-01-16 10:45:00', NULL, NULL, 'Priority delivery', GETDATE(), 1),
('DS202501011', '2025-01-17 08:00:00', 3, 'VH003', 3, 'Driver Three', '555-2003', 'DELIVERED', '2025-01-17 08:15:00', '2025-01-17 12:30:00', '2025-01-17 13:00:00', 'Completed delivery', GETDATE(), 1),
('DS202501012', '2025-01-18 11:00:00', 1, 'VH001', 1, 'Driver One', '555-2001', 'IN_TRANSIT', '2025-01-18 11:15:00', NULL, NULL, 'In progress', GETDATE(), 1),
('DS202501013', '2025-01-19 14:00:00', 2, 'VH002', 2, 'Driver Two', '555-2002', 'RETURNED', '2025-01-19 14:15:00', '2025-01-19 16:45:00', '2025-01-19 17:30:00', 'Returned after delivery', GETDATE(), 1);

-- Get the latest SalesInvoice IDs for foreign key references
DECLARE @InvoiceId1 INT, @InvoiceId2 INT, @InvoiceId3 INT, @InvoiceId4 INT, @InvoiceId5 INT;
SELECT @InvoiceId1 = SalesInvoiceId FROM SalesInvoices WHERE InvoiceNumber = 'INV202501001';
SELECT @InvoiceId2 = SalesInvoiceId FROM SalesInvoices WHERE InvoiceNumber = 'INV202501002';
SELECT @InvoiceId3 = SalesInvoiceId FROM SalesInvoices WHERE InvoiceNumber = 'INV202501003';
SELECT @InvoiceId4 = SalesInvoiceId FROM SalesInvoices WHERE InvoiceNumber = 'INV202501004';
SELECT @InvoiceId5 = SalesInvoiceId FROM SalesInvoices WHERE InvoiceNumber = 'INV202501005';

-- Add DeliveryChallans
INSERT INTO DeliveryChallans (ChallanNo, SalesInvoiceId, CustomerName, CustomerAddress, ChallanDate, VehicleNo, DriverName, DriverPhone, Remarks, Status, CreatedDate, CreatedBy, VehicleId, RouteId)
VALUES 
('DC202501009', @InvoiceId1, 'Sample Company Inc.', '123 Main St, New York, NY 10001', '2025-01-15', 'VH001', 'Driver One', '555-2001', 'Regular delivery challan', 'PENDING', GETDATE(), 1, 1, 1),
('DC202501010', @InvoiceId2, 'Emily Chen', '456 Oak Ave, Los Angeles, CA 90210', '2025-01-16', 'VH002', 'Driver Two', '555-2002', 'Priority delivery challan', 'SCHEDULED', GETDATE(), 1, 2, 2),
('DC202501011', @InvoiceId3, 'Home Depot Plus', '789 Pine Rd, Chicago, IL 60601', '2025-01-17', 'VH003', 'Driver Three', '555-2003', 'Standard delivery challan', 'DISPATCHED', GETDATE(), 1, 3, 3),
('DC202501012', @InvoiceId4, 'Sports World', '321 Elm St, Miami, FL 33101', '2025-01-18', 'VH001', 'Driver One', '555-2001', 'Follow-up delivery', 'IN_TRANSIT', GETDATE(), 1, 1, 1),
('DC202501013', @InvoiceId5, 'Book Haven', '654 Maple Ave, Seattle, WA 98101', '2025-01-19', 'VH002', 'Driver Two', '555-2002', 'Return delivery', 'RETURNED', GETDATE(), 1, 2, 2);

-- Get the latest Challan IDs for foreign key references
DECLARE @ChallanId1 INT, @ChallanId2 INT, @ChallanId3 INT, @ChallanId4 INT, @ChallanId5 INT;
SELECT @ChallanId1 = ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC202501009';
SELECT @ChallanId2 = ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC202501010';
SELECT @ChallanId3 = ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC202501011';
SELECT @ChallanId4 = ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC202501012';
SELECT @ChallanId5 = ChallanId FROM DeliveryChallans WHERE ChallanNo = 'DC202501013';

-- Add DeliveryScheduleItems to link schedules with challans
INSERT INTO DeliveryScheduleItems (ScheduleId, ChallanId, CreatedDate, CreatedBy)
VALUES 
(2, @ChallanId1, GETDATE(), 1),  -- DS202501009 -> DC202501009
(3, @ChallanId2, GETDATE(), 1),  -- DS202501010 -> DC202501010
(4, @ChallanId3, GETDATE(), 1),  -- DS202501011 -> DC202501011
(5, @ChallanId4, GETDATE(), 1),  -- DS202501012 -> DC202501012
(6, @ChallanId5, GETDATE(), 1);  -- DS202501013 -> DC202501013

-- Add DeliveryChallanItems using existing products
INSERT INTO DeliveryChallanItems (ChallanId, ProductId, Quantity, UnitPrice, TotalAmount, CreatedDate, CreatedBy)
VALUES 
(@ChallanId1, 1, 10, 50.00, 500.00, GETDATE(), 1),
(@ChallanId1, 3, 5, 100.00, 500.00, GETDATE(), 1),
(@ChallanId2, 4, 15, 75.00, 1125.00, GETDATE(), 1),
(@ChallanId3, 1, 8, 50.00, 400.00, GETDATE(), 1),
(@ChallanId4, 5, 12, 100.00, 1200.00, GETDATE(), 1),
(@ChallanId5, 6, 6, 75.00, 450.00, GETDATE(), 1);

PRINT 'Seed data added successfully!';
