-- =============================================
-- Supplier Management Seed Data
-- =============================================

USE DistributionDB;
GO

-- =============================================
-- 1. Insert Sample Suppliers
-- =============================================

-- Clear existing data (if any) - only if tables exist
IF EXISTS (SELECT * FROM sysobjects WHERE name='SupplierPaymentAllocations' AND xtype='U')
    DELETE FROM SupplierPaymentAllocations;
IF EXISTS (SELECT * FROM sysobjects WHERE name='SupplierPayments' AND xtype='U')
    DELETE FROM SupplierPayments;
IF EXISTS (SELECT * FROM sysobjects WHERE name='SupplierTransactions' AND xtype='U')
    DELETE FROM SupplierTransactions;
IF EXISTS (SELECT * FROM sysobjects WHERE name='Suppliers' AND xtype='U')
    DELETE FROM Suppliers;

-- Reset identity columns - only if tables exist
IF EXISTS (SELECT * FROM sysobjects WHERE name='Suppliers' AND xtype='U')
    DBCC CHECKIDENT ('Suppliers', RESEED, 0);
IF EXISTS (SELECT * FROM sysobjects WHERE name='SupplierTransactions' AND xtype='U')
    DBCC CHECKIDENT ('SupplierTransactions', RESEED, 0);
IF EXISTS (SELECT * FROM sysobjects WHERE name='SupplierPayments' AND xtype='U')
    DBCC CHECKIDENT ('SupplierPayments', RESEED, 0);
IF EXISTS (SELECT * FROM sysobjects WHERE name='SupplierPaymentAllocations' AND xtype='U')
    DBCC CHECKIDENT ('SupplierPaymentAllocations', RESEED, 0);

-- Insert sample suppliers
INSERT INTO Suppliers (
    SupplierCode, SupplierName, ContactPerson, ContactNumber, Email, Address, City, State, 
    GST, NTN, BusinessType, PaymentTermsDays, PaymentTermsDate, Barcode, QRCode, 
    IsActive, CreatedBy, Notes, CreditLimit
) VALUES 
-- Supplier 1
('SUP000001', 'Tech Solutions Ltd', 'Ahmed Ali', '+92-300-1234567', 'ahmed@techsolutions.pk', 
 'Plot 123, Block A, Industrial Area', 'Karachi', 'Sindh', 'GST123456789', 'NTN987654321', 
 'Technology', 30, DATEADD(DAY, 30, GETDATE()), 'SUP000001-TechSolutions', 
 '{"SupplierCode":"SUP000001","SupplierName":"Tech Solutions Ltd","ContactPerson":"Ahmed Ali","ContactNumber":"+92-300-1234567","Email":"ahmed@techsolutions.pk","Address":"Plot 123, Block A, Industrial Area","City":"Karachi","State":"Sindh","GST":"GST123456789","NTN":"NTN987654321"}',
 1, 'System', 'Leading technology supplier with excellent track record', 500000.00),

-- Supplier 2
('SUP000002', 'Office Supplies Co', 'Fatima Khan', '+92-301-2345678', 'fatima@officesupplies.pk', 
 'Building 456, Main Boulevard', 'Lahore', 'Punjab', 'GST234567890', 'NTN876543210', 
 'Office Supplies', 45, DATEADD(DAY, 45, GETDATE()), 'SUP000002-OfficeSupplies', 
 '{"SupplierCode":"SUP000002","SupplierName":"Office Supplies Co","ContactPerson":"Fatima Khan","ContactNumber":"+92-301-2345678","Email":"fatima@officesupplies.pk","Address":"Building 456, Main Boulevard","City":"Lahore","State":"Punjab","GST":"GST234567890","NTN":"NTN876543210"}',
 1, 'System', 'Complete office solutions provider', 300000.00),

-- Supplier 3
('SUP000003', 'Fashion World', 'Hassan Sheikh', '+92-302-3456789', 'hassan@fashionworld.pk', 
 'Shop 789, Mall Road', 'Islamabad', 'Federal', 'GST345678901', 'NTN765432109', 
 'Fashion', 15, DATEADD(DAY, 15, GETDATE()), 'SUP000003-FashionWorld', 
 '{"SupplierCode":"SUP000003","SupplierName":"Fashion World","ContactPerson":"Hassan Sheikh","ContactNumber":"+92-302-3456789","Email":"hassan@fashionworld.pk","Address":"Shop 789, Mall Road","City":"Islamabad","State":"Federal","GST":"GST345678901","NTN":"NTN765432109"}',
 1, 'System', 'Trendy fashion and clothing supplier', 250000.00),

-- Supplier 4
('SUP000004', 'Electronics Hub', 'Ayesha Malik', '+92-303-4567890', 'ayesha@electronicshub.pk', 
 'Unit 101, Tech Park', 'Karachi', 'Sindh', 'GST456789012', 'NTN654321098', 
 'Electronics', 60, DATEADD(DAY, 60, GETDATE()), 'SUP000004-ElectronicsHub', 
 '{"SupplierCode":"SUP000004","SupplierName":"Electronics Hub","ContactPerson":"Ayesha Malik","ContactNumber":"+92-303-4567890","Email":"ayesha@electronicshub.pk","Address":"Unit 101, Tech Park","City":"Karachi","State":"Sindh","GST":"GST456789012","NTN":"NTN654321098"}',
 1, 'System', 'Premium electronics and gadgets supplier', 750000.00),

-- Supplier 5
('SUP000005', 'Home & Garden', 'Usman Qureshi', '+92-304-5678901', 'usman@homegarden.pk', 
 'Warehouse 202, Industrial Zone', 'Lahore', 'Punjab', 'GST567890123', 'NTN543210987', 
 'Home & Garden', 30, DATEADD(DAY, 30, GETDATE()), 'SUP000005-HomeGarden', 
 '{"SupplierCode":"SUP000005","SupplierName":"Home & Garden","ContactPerson":"Usman Qureshi","ContactNumber":"+92-304-5678901","Email":"usman@homegarden.pk","Address":"Warehouse 202, Industrial Zone","City":"Lahore","State":"Punjab","GST":"GST567890123","NTN":"NTN543210987"}',
 1, 'System', 'Complete home and garden solutions', 400000.00),

-- Supplier 6 (Inactive for testing)
('SUP000006', 'Old Supplier Co', 'Old Manager', '+92-305-6789012', 'old@supplier.pk', 
 'Old Address', 'Old City', 'Old State', 'GST678901234', 'NTN432109876', 
 'General', 30, DATEADD(DAY, 30, GETDATE()), 'SUP000006-OldSupplier', 
 '{"SupplierCode":"SUP000006","SupplierName":"Old Supplier Co","ContactPerson":"Old Manager","ContactNumber":"+92-305-6789012","Email":"old@supplier.pk","Address":"Old Address","City":"Old City","State":"Old State","GST":"GST678901234","NTN":"NTN432109876"}',
 0, 'System', 'Inactive supplier for testing purposes', 100000.00);

PRINT 'Sample suppliers inserted successfully';
GO

-- =============================================
-- 2. Insert Sample Supplier Transactions
-- =============================================

-- Get supplier IDs for transactions
DECLARE @Supplier1 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000001');
DECLARE @Supplier2 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000002');
DECLARE @Supplier3 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000003');
DECLARE @Supplier4 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000004');
DECLARE @Supplier5 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000005');

-- Insert sample transactions for Supplier 1 (Tech Solutions Ltd)
INSERT INTO SupplierTransactions (
    SupplierId, TransactionType, TransactionDate, ReferenceNumber, Description,
    DebitAmount, CreditAmount, Balance, PaymentMethod, PurchaseOrderNumber, 
    InvoiceNumber, InvoiceDate, DueDate, CreatedBy, Notes
) VALUES 
-- Purchases
(@Supplier1, 'Purchase', DATEADD(DAY, -60, GETDATE()), 'PO-2024-001', 'Laptop Purchase - Dell XPS 13', 
 129999.00, 0, 129999.00, NULL, 'PO-2024-001', 'INV-2024-001', DATEADD(DAY, -60, GETDATE()), DATEADD(DAY, -30, GETDATE()), 'System', 'Bulk laptop purchase for office'),
(@Supplier1, 'Purchase', DATEADD(DAY, -45, GETDATE()), 'PO-2024-002', 'Desktop Computers - HP EliteDesk', 
 250000.00, 0, 379999.00, NULL, 'PO-2024-002', 'INV-2024-002', DATEADD(DAY, -45, GETDATE()), DATEADD(DAY, -15, GETDATE()), 'System', 'Desktop computers for new employees'),
(@Supplier1, 'Purchase', DATEADD(DAY, -30, GETDATE()), 'PO-2024-003', 'Network Equipment - Switches & Routers', 
 150000.00, 0, 529999.00, NULL, 'PO-2024-003', 'INV-2024-003', DATEADD(DAY, -30, GETDATE()), GETDATE(), 'System', 'Network infrastructure upgrade'),

-- Payments
(@Supplier1, 'Payment', DATEADD(DAY, -20, GETDATE()), 'PAY-2024-001', 'Payment for Laptop Purchase', 
 0, 129999.00, 400000.00, 'Bank Transfer', NULL, NULL, NULL, NULL, 'System', 'Payment via bank transfer'),
(@Supplier1, 'Payment', DATEADD(DAY, -10, GETDATE()), 'PAY-2024-002', 'Payment for Desktop Computers', 
 0, 200000.00, 200000.00, 'Check', NULL, NULL, NULL, NULL, 'System', 'Payment via check'),

-- Returns
(@Supplier1, 'Return', DATEADD(DAY, -5, GETDATE()), 'RET-2024-001', 'Return of Defective Laptop', 
 0, 25000.00, 175000.00, NULL, NULL, NULL, NULL, NULL, 'System', 'Returned defective laptop for refund');

-- Insert sample transactions for Supplier 2 (Office Supplies Co)
INSERT INTO SupplierTransactions (
    SupplierId, TransactionType, TransactionDate, ReferenceNumber, Description,
    DebitAmount, CreditAmount, Balance, PaymentMethod, PurchaseOrderNumber, 
    InvoiceNumber, InvoiceDate, DueDate, CreatedBy, Notes
) VALUES 
-- Purchases
(@Supplier2, 'Purchase', DATEADD(DAY, -50, GETDATE()), 'PO-2024-004', 'Office Furniture - Desks & Chairs', 
 75000.00, 0, 75000.00, NULL, 'PO-2024-004', 'INV-2024-004', DATEADD(DAY, -50, GETDATE()), DATEADD(DAY, -5, GETDATE()), 'System', 'Office furniture for new branch'),
(@Supplier2, 'Purchase', DATEADD(DAY, -35, GETDATE()), 'PO-2024-005', 'Stationery Items - Bulk Order', 
 25000.00, 0, 100000.00, NULL, 'PO-2024-005', 'INV-2024-005', DATEADD(DAY, -35, GETDATE()), DATEADD(DAY, 10, GETDATE()), 'System', 'Monthly stationery supplies'),

-- Payments
(@Supplier2, 'Payment', DATEADD(DAY, -15, GETDATE()), 'PAY-2024-003', 'Payment for Office Furniture', 
 0, 75000.00, 25000.00, 'Cash', NULL, NULL, NULL, NULL, 'System', 'Cash payment for furniture');

-- Insert sample transactions for Supplier 3 (Fashion World)
INSERT INTO SupplierTransactions (
    SupplierId, TransactionType, TransactionDate, ReferenceNumber, Description,
    DebitAmount, CreditAmount, Balance, PaymentMethod, PurchaseOrderNumber, 
    InvoiceNumber, InvoiceDate, DueDate, CreatedBy, Notes
) VALUES 
-- Purchases
(@Supplier3, 'Purchase', DATEADD(DAY, -40, GETDATE()), 'PO-2024-006', 'Summer Collection - T-Shirts', 
 50000.00, 0, 50000.00, NULL, 'PO-2024-006', 'INV-2024-006', DATEADD(DAY, -40, GETDATE()), DATEADD(DAY, -25, GETDATE()), 'System', 'Summer fashion collection'),
(@Supplier3, 'Purchase', DATEADD(DAY, -25, GETDATE()), 'PO-2024-007', 'Winter Collection - Jackets', 
 80000.00, 0, 130000.00, NULL, 'PO-2024-007', 'INV-2024-007', DATEADD(DAY, -25, GETDATE()), DATEADD(DAY, -10, GETDATE()), 'System', 'Winter fashion collection'),

-- Payments
(@Supplier3, 'Payment', DATEADD(DAY, -20, GETDATE()), 'PAY-2024-004', 'Payment for Summer Collection', 
 0, 50000.00, 80000.00, 'Bank Transfer', NULL, NULL, NULL, NULL, 'System', 'Bank transfer payment');

-- Insert sample transactions for Supplier 4 (Electronics Hub)
INSERT INTO SupplierTransactions (
    SupplierId, TransactionType, TransactionDate, ReferenceNumber, Description,
    DebitAmount, CreditAmount, Balance, PaymentMethod, PurchaseOrderNumber, 
    InvoiceNumber, InvoiceDate, DueDate, CreatedBy, Notes
) VALUES 
-- Purchases
(@Supplier4, 'Purchase', DATEADD(DAY, -70, GETDATE()), 'PO-2024-008', 'Smartphones - Samsung Galaxy Series', 
 300000.00, 0, 300000.00, NULL, 'PO-2024-008', 'INV-2024-008', DATEADD(DAY, -70, GETDATE()), DATEADD(DAY, -10, GETDATE()), 'System', 'Latest smartphone models'),
(@Supplier4, 'Purchase', DATEADD(DAY, -55, GETDATE()), 'PO-2024-009', 'Tablets - iPad Pro Series', 
 200000.00, 0, 500000.00, NULL, 'PO-2024-009', 'INV-2024-009', DATEADD(DAY, -55, GETDATE()), DATEADD(DAY, 5, GETDATE()), 'System', 'Premium tablet collection'),

-- Payments
(@Supplier4, 'Payment', DATEADD(DAY, -25, GETDATE()), 'PAY-2024-005', 'Partial Payment for Smartphones', 
 0, 150000.00, 350000.00, 'Bank Transfer', NULL, NULL, NULL, NULL, 'System', 'Partial payment via bank transfer');

-- Insert sample transactions for Supplier 5 (Home & Garden)
INSERT INTO SupplierTransactions (
    SupplierId, TransactionType, TransactionDate, ReferenceNumber, Description,
    DebitAmount, CreditAmount, Balance, PaymentMethod, PurchaseOrderNumber, 
    InvoiceNumber, InvoiceDate, DueDate, CreatedBy, Notes
) VALUES 
-- Purchases
(@Supplier5, 'Purchase', DATEADD(DAY, -30, GETDATE()), 'PO-2024-010', 'Garden Tools - Complete Set', 
 35000.00, 0, 35000.00, NULL, 'PO-2024-010', 'INV-2024-010', DATEADD(DAY, -30, GETDATE()), GETDATE(), 'System', 'Professional garden tools'),
(@Supplier5, 'Purchase', DATEADD(DAY, -20, GETDATE()), 'PO-2024-011', 'Home Decor Items', 
 45000.00, 0, 80000.00, NULL, 'PO-2024-011', 'INV-2024-011', DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, 10, GETDATE()), 'System', 'Decorative items for showroom'),

-- Payments
(@Supplier5, 'Payment', DATEADD(DAY, -10, GETDATE()), 'PAY-2024-006', 'Payment for Garden Tools', 
 0, 35000.00, 45000.00, 'Cash', NULL, NULL, NULL, NULL, 'System', 'Cash payment for garden tools');

PRINT 'Sample supplier transactions inserted successfully';
GO

-- =============================================
-- 3. Insert Sample Supplier Payments
-- =============================================

-- Get supplier IDs for payments (redeclare variables after GO)
DECLARE @Supplier1 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000001');
DECLARE @Supplier2 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000002');
DECLARE @Supplier3 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000003');
DECLARE @Supplier4 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000004');
DECLARE @Supplier5 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000005');

-- Insert sample payments
INSERT INTO SupplierPayments (
    SupplierId, PaymentNumber, PaymentDate, PaymentAmount, PaymentMethod,
    BankName, AccountNumber, CheckNumber, CheckDate, TransactionReference,
    AllocatedAmount, UnallocatedAmount, Status, CreatedBy, Notes
) VALUES 
-- Payments for Supplier 1
(@Supplier1, 'PAY-2024-001', DATEADD(DAY, -20, GETDATE()), 129999.00, 'Bank Transfer',
 'Allied Bank', '1234567890123456', NULL, NULL, 'TXN-AB-2024-001',
 129999.00, 0, 'Completed', 'System', 'Payment for laptop purchase'),

(@Supplier1, 'PAY-2024-002', DATEADD(DAY, -10, GETDATE()), 200000.00, 'Check',
 'MCB Bank', '9876543210987654', 'CHK-001234', DATEADD(DAY, -10, GETDATE()), NULL,
 200000.00, 0, 'Completed', 'System', 'Payment for desktop computers'),

-- Payments for Supplier 2
(@Supplier2, 'PAY-2024-003', DATEADD(DAY, -15, GETDATE()), 75000.00, 'Cash',
 NULL, NULL, NULL, NULL, NULL,
 75000.00, 0, 'Completed', 'System', 'Cash payment for office furniture'),

-- Payments for Supplier 3
(@Supplier3, 'PAY-2024-004', DATEADD(DAY, -20, GETDATE()), 50000.00, 'Bank Transfer',
 'HBL Bank', '5555666677778888', NULL, NULL, 'TXN-HBL-2024-002',
 50000.00, 0, 'Completed', 'System', 'Payment for summer collection'),

-- Payments for Supplier 4
(@Supplier4, 'PAY-2024-005', DATEADD(DAY, -25, GETDATE()), 150000.00, 'Bank Transfer',
 'UBL Bank', '1111222233334444', NULL, NULL, 'TXN-UBL-2024-003',
 150000.00, 0, 'Completed', 'System', 'Partial payment for smartphones'),

-- Payments for Supplier 5
(@Supplier5, 'PAY-2024-006', DATEADD(DAY, -10, GETDATE()), 35000.00, 'Cash',
 NULL, NULL, NULL, NULL, NULL,
 35000.00, 0, 'Completed', 'System', 'Cash payment for garden tools');

PRINT 'Sample supplier payments inserted successfully';
GO

-- =============================================
-- 4. Insert Sample Payment Allocations
-- =============================================

-- Get transaction IDs for allocations (using correct reference numbers)
DECLARE @Txn1 INT = (SELECT TransactionId FROM SupplierTransactions WHERE ReferenceNumber = 'PO-2024-001');
DECLARE @Txn2 INT = (SELECT TransactionId FROM SupplierTransactions WHERE ReferenceNumber = 'PO-2024-002');
DECLARE @Txn3 INT = (SELECT TransactionId FROM SupplierTransactions WHERE ReferenceNumber = 'PO-2024-004');
DECLARE @Txn4 INT = (SELECT TransactionId FROM SupplierTransactions WHERE ReferenceNumber = 'PO-2024-006');
DECLARE @Txn5 INT = (SELECT TransactionId FROM SupplierTransactions WHERE ReferenceNumber = 'PO-2024-008');
DECLARE @Txn6 INT = (SELECT TransactionId FROM SupplierTransactions WHERE ReferenceNumber = 'PO-2024-010');

-- Get payment IDs for allocations
DECLARE @Pay1 INT = (SELECT PaymentId FROM SupplierPayments WHERE PaymentNumber = 'PAY-2024-001');
DECLARE @Pay2 INT = (SELECT PaymentId FROM SupplierPayments WHERE PaymentNumber = 'PAY-2024-002');
DECLARE @Pay3 INT = (SELECT PaymentId FROM SupplierPayments WHERE PaymentNumber = 'PAY-2024-003');
DECLARE @Pay4 INT = (SELECT PaymentId FROM SupplierPayments WHERE PaymentNumber = 'PAY-2024-004');
DECLARE @Pay5 INT = (SELECT PaymentId FROM SupplierPayments WHERE PaymentNumber = 'PAY-2024-005');
DECLARE @Pay6 INT = (SELECT PaymentId FROM SupplierPayments WHERE PaymentNumber = 'PAY-2024-006');

-- Insert payment allocations (only if table exists)
IF EXISTS (SELECT * FROM sysobjects WHERE name='SupplierPaymentAllocations' AND xtype='U')
BEGIN
    INSERT INTO SupplierPaymentAllocations (
        PaymentId, TransactionId, AllocatedAmount, CreatedBy
    ) VALUES 
-- Allocations for Supplier 1 payments
(@Pay1, @Txn1, 129999.00, 'System'),
(@Pay2, @Txn2, 200000.00, 'System'),

-- Allocations for Supplier 2 payments
(@Pay3, @Txn3, 75000.00, 'System'),

-- Allocations for Supplier 3 payments
(@Pay4, @Txn4, 50000.00, 'System'),

-- Allocations for Supplier 4 payments
(@Pay5, @Txn5, 150000.00, 'System'),

-- Allocations for Supplier 5 payments
(@Pay6, @Txn6, 35000.00, 'System');

    PRINT 'Sample payment allocations inserted successfully';
END
ELSE
BEGIN
    PRINT 'SupplierPaymentAllocations table does not exist - skipping payment allocations';
END
GO

-- =============================================
-- 5. Update Supplier Balances
-- =============================================

-- Get supplier IDs for balance updates (redeclare variables after GO)
DECLARE @Supplier1 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000001');
DECLARE @Supplier2 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000002');
DECLARE @Supplier3 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000003');
DECLARE @Supplier4 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000004');
DECLARE @Supplier5 INT = (SELECT SupplierId FROM Suppliers WHERE SupplierCode = 'SUP000005');

-- Update balances for all suppliers (only if stored procedure exists)
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_UpdateSupplierBalance')
BEGIN
    EXEC sp_UpdateSupplierBalance @SupplierId = @Supplier1;
    EXEC sp_UpdateSupplierBalance @SupplierId = @Supplier2;
    EXEC sp_UpdateSupplierBalance @SupplierId = @Supplier3;
    EXEC sp_UpdateSupplierBalance @SupplierId = @Supplier4;
    EXEC sp_UpdateSupplierBalance @SupplierId = @Supplier5;
    PRINT 'Supplier balances updated successfully';
END
ELSE
BEGIN
    PRINT 'sp_UpdateSupplierBalance stored procedure does not exist - skipping balance updates';
END
GO

-- =============================================
-- 6. Display Summary
-- =============================================

PRINT '=============================================';
PRINT 'Supplier Management Seed Data Summary:';
PRINT '=============================================';

SELECT 'Suppliers' AS TableName, COUNT(*) AS RecordCount FROM Suppliers
UNION ALL
SELECT 'Supplier Transactions', COUNT(*) FROM SupplierTransactions
UNION ALL
SELECT 'Supplier Payments', COUNT(*) FROM SupplierPayments
UNION ALL
SELECT 'Payment Allocations', COUNT(*) FROM SupplierPaymentAllocations;

PRINT '=============================================';
PRINT 'Seed Data Insertion Complete!';
PRINT '=============================================';

-- Display sample data
PRINT 'Sample Suppliers:';
SELECT SupplierCode, SupplierName, ContactPerson, ContactNumber, CurrentBalance, IsActive 
FROM Suppliers 
ORDER BY SupplierCode;

PRINT 'Sample Transactions Summary:';
SELECT 
    s.SupplierName,
    COUNT(st.TransactionId) AS TotalTransactions,
    SUM(st.DebitAmount) AS TotalDebits,
    SUM(st.CreditAmount) AS TotalCredits,
    s.CurrentBalance
FROM Suppliers s
LEFT JOIN SupplierTransactions st ON s.SupplierId = st.SupplierId
WHERE s.IsActive = 1
GROUP BY s.SupplierId, s.SupplierName, s.CurrentBalance
ORDER BY s.SupplierName;
