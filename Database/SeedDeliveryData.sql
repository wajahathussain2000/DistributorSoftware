-- Comprehensive seed data for delivery-related tables
-- This script adds sample data to test the delivery reports

-- First, let's add some Salesman data if not exists
IF NOT EXISTS (SELECT 1 FROM Salesman WHERE SalesmanId = 1)
BEGIN
    INSERT INTO Salesman (SalesmanId, SalesmanCode, SalesmanName, Email, Phone, Territory, CommissionRate, IsActive, CreatedDate, CreatedBy)
    VALUES 
    (1, 'SM001', 'John Smith', 'john.smith@company.com', '555-0101', 'North Region', 5.0, 1, GETDATE(), 1),
    (2, 'SM002', 'Sarah Johnson', 'sarah.johnson@company.com', '555-0102', 'South Region', 4.5, 1, GETDATE(), 1),
    (3, 'SM003', 'Mike Wilson', 'mike.wilson@company.com', '555-0103', 'East Region', 5.5, 1, GETDATE(), 1);
END

-- Add some Customers if not exists
IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerId = 1)
BEGIN
    INSERT INTO Customers (CustomerId, CustomerCode, CustomerName, ContactPerson, Email, Phone, Address, City, State, PostalCode, Country, IsActive, CreatedDate, CreatedBy)
    VALUES 
    (1, 'CUST001', 'ABC Corporation', 'Jane Doe', 'jane.doe@abc.com', '555-1001', '123 Main St', 'New York', 'NY', '10001', 'USA', 1, GETDATE(), 1),
    (2, 'CUST002', 'XYZ Industries', 'Bob Smith', 'bob.smith@xyz.com', '555-1002', '456 Oak Ave', 'Los Angeles', 'CA', '90210', 'USA', 1, GETDATE(), 1),
    (3, 'CUST003', 'DEF Enterprises', 'Alice Brown', 'alice.brown@def.com', '555-1003', '789 Pine Rd', 'Chicago', 'IL', '60601', 'USA', 1, GETDATE(), 1);
END

-- Add some SalesInvoices if not exists
IF NOT EXISTS (SELECT 1 FROM SalesInvoices WHERE SalesInvoiceId = 1)
BEGIN
    INSERT INTO SalesInvoices (SalesInvoiceId, InvoiceNumber, InvoiceDate, CustomerId, TotalAmount, PaidAmount, BalanceAmount, PaymentMode, Status, CreatedDate, CreatedBy)
    VALUES 
    (1, 'INV001', '2025-01-15', 1, 5000.00, 3000.00, 2000.00, 'Cash', 'Pending', GETDATE(), 1),
    (2, 'INV002', '2025-01-16', 2, 7500.00, 7500.00, 0.00, 'Bank Transfer', 'Paid', GETDATE(), 1),
    (3, 'INV003', '2025-01-17', 3, 3200.00, 0.00, 3200.00, 'Credit', 'Pending', GETDATE(), 1);
END

-- Add more DeliverySchedules with current date range
INSERT INTO DeliverySchedules (ScheduleRef, ScheduledDateTime, VehicleId, VehicleNo, RouteId, DriverName, DriverContact, Status, DispatchDateTime, DeliveredDateTime, ReturnedDateTime, Remarks, CreatedDate, CreatedBy)
VALUES 
('DS202501001', '2025-01-15 09:00:00', 1, 'VH001', 1, 'Driver One', '555-2001', 'SCHEDULED', NULL, NULL, NULL, 'Regular delivery', GETDATE(), 1),
('DS202501002', '2025-01-16 10:30:00', 2, 'VH002', 2, 'Driver Two', '555-2002', 'DISPATCHED', '2025-01-16 10:45:00', NULL, NULL, 'Priority delivery', GETDATE(), 1),
('DS202501003', '2025-01-17 08:00:00', 3, 'VH003', 3, 'Driver Three', '555-2003', 'DELIVERED', '2025-01-17 08:15:00', '2025-01-17 12:30:00', '2025-01-17 13:00:00', 'Completed delivery', GETDATE(), 1),
('DS202501004', '2025-01-18 11:00:00', 1, 'VH001', 1, 'Driver One', '555-2001', 'IN_TRANSIT', '2025-01-18 11:15:00', NULL, NULL, 'In progress', GETDATE(), 2),
('DS202501005', '2025-01-19 14:00:00', 2, 'VH002', 2, 'Driver Two', '555-2002', 'RETURNED', '2025-01-19 14:15:00', '2025-01-19 16:45:00', '2025-01-19 17:30:00', 'Returned after delivery', GETDATE(), 2),
('DS202501006', '2025-01-20 09:30:00', 3, 'VH003', 3, 'Driver Three', '555-2003', 'SCHEDULED', NULL, NULL, NULL, 'Scheduled for tomorrow', GETDATE(), 3),
('DS202501007', '2025-01-21 13:00:00', 1, 'VH001', 1, 'Driver One', '555-2001', 'DISPATCHED', '2025-01-21 13:10:00', NULL, NULL, 'Afternoon delivery', GETDATE(), 1),
('DS202501008', '2025-01-22 07:30:00', 2, 'VH002', 2, 'Driver Two', '555-2002', 'DELIVERED', '2025-01-22 07:45:00', '2025-01-22 11:20:00', '2025-01-22 12:00:00', 'Early morning delivery', GETDATE(), 2);

-- Add DeliveryChallans
INSERT INTO DeliveryChallans (ChallanNo, SalesInvoiceId, CustomerName, CustomerAddress, ChallanDate, VehicleNo, DriverName, DriverPhone, Remarks, Status, CreatedDate, CreatedBy, VehicleId, RouteId)
VALUES 
('DC202501001', 1, 'ABC Corporation', '123 Main St, New York, NY 10001', '2025-01-15', 'VH001', 'Driver One', '555-2001', 'Regular delivery challan', 'PENDING', GETDATE(), 1, 1, 1),
('DC202501002', 2, 'XYZ Industries', '456 Oak Ave, Los Angeles, CA 90210', '2025-01-16', 'VH002', 'Driver Two', '555-2002', 'Priority delivery challan', 'SCHEDULED', GETDATE(), 1, 2, 2),
('DC202501003', 3, 'DEF Enterprises', '789 Pine Rd, Chicago, IL 60601', '2025-01-17', 'VH003', 'Driver Three', '555-2003', 'Standard delivery challan', 'DISPATCHED', GETDATE(), 1, 3, 3),
('DC202501004', 1, 'ABC Corporation', '123 Main St, New York, NY 10001', '2025-01-18', 'VH001', 'Driver One', '555-2001', 'Follow-up delivery', 'IN_TRANSIT', GETDATE(), 2, 1, 1),
('DC202501005', 2, 'XYZ Industries', '456 Oak Ave, Los Angeles, CA 90210', '2025-01-19', 'VH002', 'Driver Two', '555-2002', 'Return delivery', 'RETURNED', GETDATE(), 2, 2, 2);

-- Add DeliveryScheduleItems to link schedules with challans
INSERT INTO DeliveryScheduleItems (ScheduleId, ChallanId, CreatedDate, CreatedBy)
VALUES 
(2, 1, GETDATE(), 1),  -- DS202501001 -> DC202501001
(3, 2, GETDATE(), 1),  -- DS202501002 -> DC202501002
(4, 3, GETDATE(), 1),  -- DS202501003 -> DC202501003
(5, 4, GETDATE(), 2),  -- DS202501004 -> DC202501004
(6, 5, GETDATE(), 2);  -- DS202501005 -> DC202501005

-- Add DeliveryChallanItems
INSERT INTO DeliveryChallanItems (ChallanId, ProductId, Quantity, UnitPrice, TotalAmount, CreatedDate, CreatedBy)
VALUES 
(1, 1, 10, 50.00, 500.00, GETDATE(), 1),
(1, 2, 5, 100.00, 500.00, GETDATE(), 1),
(2, 3, 15, 75.00, 1125.00, GETDATE(), 1),
(3, 1, 8, 50.00, 400.00, GETDATE(), 1),
(4, 2, 12, 100.00, 1200.00, GETDATE(), 2),
(5, 3, 6, 75.00, 450.00, GETDATE(), 2);

PRINT 'Seed data added successfully!';
