USE [DistributionDB]
GO

-- Insert Sales Invoice Details with correct SalesInvoiceId values
INSERT INTO SalesInvoiceDetails (SalesInvoiceId, ProductId, Quantity, UnitPrice, TaxPercentage, TaxAmount, DiscountPercentage, DiscountAmount, TotalAmount, Remarks)
VALUES 
(9, 1, 1, 899.99, 10.00, 89.99, 0.00, 0.00, 989.98, 'Samsung Galaxy S21'),
(10, 6, 1, 150.00, 10.00, 15.00, 6.67, 10.00, 155.00, 'Nike Air Max 270'),
(11, 4, 1, 1299.99, 10.00, 130.00, 3.85, 50.00, 1379.99, 'Dell XPS 13'),
(12, 3, 1, 349.99, 10.00, 35.00, 0.00, 0.00, 384.99, 'Sony WH-1000XM4'),
(13, 16, 1, 49.99, 10.00, 5.00, 0.00, 0.00, 54.99, 'Programming Book Set'),
(14, 18, 1, 129.99, 10.00, 13.00, 0.00, 0.00, 142.99, 'Car Battery 12V'),
(15, 20, 1, 34.99, 10.00, 3.50, 0.00, 0.00, 38.49, 'Vitamin C Serum'),
(16, 22, 1, 15.99, 10.00, 1.60, 0.00, 0.00, 17.59, 'Organic Coffee Beans'),
(17, 2, 1, 999.99, 10.00, 100.00, 10.00, 100.00, 999.99, 'Apple iPhone 13'),
(18, 5, 1, 1499.99, 10.00, 150.00, 0.00, 0.00, 1649.99, 'LG OLED TV 55"'),
(19, 7, 1, 180.00, 10.00, 18.00, 0.00, 0.00, 198.00, 'Adidas Ultraboost 22'),
(20, 10, 1, 19.99, 10.00, 2.00, 0.00, 0.00, 21.99, 'Philips LED Bulb Pack'),
(21, 13, 1, 39.99, 10.00, 4.00, 0.00, 0.00, 43.99, 'Yoga Mat Premium'),
(22, 17, 1, 29.99, 10.00, 3.00, 0.00, 0.00, 32.99, 'Business Strategy Guide'),
(23, 19, 1, 12.99, 10.00, 1.30, 0.00, 0.00, 14.29, 'Engine Oil 5W-30');

-- Verification
SELECT 'Sales invoice details inserted successfully!' as Status;
SELECT COUNT(*) as SalesInvoiceDetailsCount FROM SalesInvoiceDetails;

GO
