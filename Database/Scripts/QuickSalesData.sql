USE [DistributionDB]
GO

-- Quick Sales Data Insert
-- Using existing customer IDs

-- Insert Sales Invoices
INSERT INTO SalesInvoices (InvoiceNumber, CustomerId, SalesmanId, InvoiceDate, DueDate, SubTotal, TaxAmount, DiscountAmount, TotalAmount, PaidAmount, BalanceAmount, PaymentMode, Status, Remarks, CreatedDate, CreatedBy)
VALUES 
('INV-001', 1, 1, '2024-01-15', '2024-02-15', 899.99, 89.99, 0.00, 989.98, 989.98, 0.00, 'Credit Card', 'PAID', 'Samsung Galaxy S21 sale', GETDATE(), 1),
('INV-002', 3, 1, '2024-01-16', '2024-02-16', 150.00, 15.00, 10.00, 155.00, 155.00, 0.00, 'Cash', 'PAID', 'Nike shoes sale', GETDATE(), 1),
('INV-003', 4, 1, '2024-01-17', '2024-02-17', 1299.99, 130.00, 50.00, 1379.99, 500.00, 879.99, 'Bank Transfer', 'PENDING', 'Dell laptop sale', GETDATE(), 1),
('INV-004', 5, 1, '2024-01-18', '2024-02-18', 349.99, 35.00, 0.00, 384.99, 384.99, 0.00, 'Credit Card', 'PAID', 'Sony headphones sale', GETDATE(), 1),
('INV-005', 6, 1, '2024-01-19', '2024-02-19', 49.99, 5.00, 0.00, 54.99, 54.99, 0.00, 'Cash', 'PAID', 'Programming book sale', GETDATE(), 1),
('INV-006', 7, 1, '2024-01-20', '2024-02-20', 129.99, 13.00, 0.00, 142.99, 0.00, 142.99, 'Bank Transfer', 'PENDING', 'Car battery sale', GETDATE(), 1),
('INV-007', 8, 1, '2024-01-21', '2024-02-21', 34.99, 3.50, 0.00, 38.49, 38.49, 0.00, 'Credit Card', 'PAID', 'Vitamin C serum sale', GETDATE(), 1),
('INV-008', 9, 1, '2024-01-22', '2024-02-22', 15.99, 1.60, 0.00, 17.59, 17.59, 0.00, 'Cash', 'PAID', 'Coffee beans sale', GETDATE(), 1),
('INV-009', 10, 1, '2024-01-23', '2024-02-23', 999.99, 100.00, 100.00, 999.99, 999.99, 0.00, 'Credit Card', 'PAID', 'iPhone 13 sale', GETDATE(), 1),
('INV-010', 11, 1, '2024-01-24', '2024-02-24', 1499.99, 150.00, 0.00, 1649.99, 800.00, 849.99, 'Bank Transfer', 'PENDING', 'LG TV sale', GETDATE(), 1),
('INV-011', 1, 1, '2024-01-25', '2024-02-25', 180.00, 18.00, 0.00, 198.00, 198.00, 0.00, 'Credit Card', 'PAID', 'Adidas shoes sale', GETDATE(), 1),
('INV-012', 3, 1, '2024-01-26', '2024-02-26', 19.99, 2.00, 0.00, 21.99, 21.99, 0.00, 'Cash', 'PAID', 'LED bulbs sale', GETDATE(), 1),
('INV-013', 4, 1, '2024-01-27', '2024-02-27', 39.99, 4.00, 0.00, 43.99, 43.99, 0.00, 'Credit Card', 'PAID', 'Yoga mat sale', GETDATE(), 1),
('INV-014', 5, 1, '2024-01-28', '2024-02-28', 29.99, 3.00, 0.00, 32.99, 32.99, 0.00, 'Cash', 'PAID', 'Business book sale', GETDATE(), 1),
('INV-015', 6, 1, '2024-01-29', '2024-02-29', 12.99, 1.30, 0.00, 14.29, 14.29, 0.00, 'Credit Card', 'PAID', 'Engine oil sale', GETDATE(), 1);

-- Insert Sales Invoice Details
INSERT INTO SalesInvoiceDetails (SalesInvoiceId, ProductId, Quantity, UnitPrice, TaxPercentage, TaxAmount, DiscountPercentage, DiscountAmount, TotalAmount, Remarks)
VALUES 
(1, 1, 1, 899.99, 10.00, 89.99, 0.00, 0.00, 989.98, 'Samsung Galaxy S21'),
(2, 6, 1, 150.00, 10.00, 15.00, 6.67, 10.00, 155.00, 'Nike Air Max 270'),
(3, 4, 1, 1299.99, 10.00, 130.00, 3.85, 50.00, 1379.99, 'Dell XPS 13'),
(4, 3, 1, 349.99, 10.00, 35.00, 0.00, 0.00, 384.99, 'Sony WH-1000XM4'),
(5, 16, 1, 49.99, 10.00, 5.00, 0.00, 0.00, 54.99, 'Programming Book Set'),
(6, 18, 1, 129.99, 10.00, 13.00, 0.00, 0.00, 142.99, 'Car Battery 12V'),
(7, 20, 1, 34.99, 10.00, 3.50, 0.00, 0.00, 38.49, 'Vitamin C Serum'),
(8, 22, 1, 15.99, 10.00, 1.60, 0.00, 0.00, 17.59, 'Organic Coffee Beans'),
(9, 2, 1, 999.99, 10.00, 100.00, 10.00, 100.00, 999.99, 'Apple iPhone 13'),
(10, 5, 1, 1499.99, 10.00, 150.00, 0.00, 0.00, 1649.99, 'LG OLED TV 55"'),
(11, 7, 1, 180.00, 10.00, 18.00, 0.00, 0.00, 198.00, 'Adidas Ultraboost 22'),
(12, 10, 1, 19.99, 10.00, 2.00, 0.00, 0.00, 21.99, 'Philips LED Bulb Pack'),
(13, 13, 1, 39.99, 10.00, 4.00, 0.00, 0.00, 43.99, 'Yoga Mat Premium'),
(14, 17, 1, 29.99, 10.00, 3.00, 0.00, 0.00, 32.99, 'Business Strategy Guide'),
(15, 19, 1, 12.99, 10.00, 1.30, 0.00, 0.00, 14.29, 'Engine Oil 5W-30');

-- Verification
SELECT 'Sales data inserted successfully!' as Status;
SELECT COUNT(*) as SalesInvoicesCount FROM SalesInvoices;
SELECT COUNT(*) as SalesInvoiceDetailsCount FROM SalesInvoiceDetails;

GO
