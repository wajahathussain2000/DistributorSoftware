USE [DistributionDB]
GO

-- Insert Sales Invoice Details with correct ProductId values
INSERT INTO SalesInvoiceDetails (SalesInvoiceId, ProductId, Quantity, UnitPrice, TaxPercentage, TaxAmount, DiscountPercentage, DiscountAmount, TotalAmount, Remarks)
VALUES 
(9, 1, 1, 899.99, 10.00, 89.99, 0.00, 0.00, 989.98, 'Sample Product 1'),
(10, 5, 1, 150.00, 10.00, 15.00, 6.67, 10.00, 155.00, 'Shoes'),
(11, 1007, 1, 1299.99, 10.00, 130.00, 3.85, 50.00, 1379.99, 'Dell XPS 13'),
(12, 1006, 1, 349.99, 10.00, 35.00, 0.00, 0.00, 384.99, 'Sony WH-1000XM4'),
(13, 1, 1, 49.99, 10.00, 5.00, 0.00, 0.00, 54.99, 'Sample Product 1'),
(14, 1, 1, 129.99, 10.00, 13.00, 0.00, 0.00, 142.99, 'Sample Product 1'),
(15, 1, 1, 34.99, 10.00, 3.50, 0.00, 0.00, 38.49, 'Sample Product 1'),
(16, 1, 1, 15.99, 10.00, 1.60, 0.00, 0.00, 17.59, 'Sample Product 1'),
(17, 3, 1, 999.99, 10.00, 100.00, 10.00, 100.00, 999.99, 'Mobile'),
(18, 1008, 1, 1499.99, 10.00, 150.00, 0.00, 0.00, 1649.99, 'LG OLED TV 55"'),
(19, 5, 1, 180.00, 10.00, 18.00, 0.00, 0.00, 198.00, 'Shoes'),
(20, 1, 1, 19.99, 10.00, 2.00, 0.00, 0.00, 21.99, 'Sample Product 1'),
(21, 1, 1, 39.99, 10.00, 4.00, 0.00, 0.00, 43.99, 'Sample Product 1'),
(22, 1, 1, 29.99, 10.00, 3.00, 0.00, 0.00, 32.99, 'Sample Product 1'),
(23, 1, 1, 12.99, 10.00, 1.30, 0.00, 0.00, 14.29, 'Sample Product 1');

-- Verification
SELECT 'Sales invoice details inserted successfully!' as Status;
SELECT COUNT(*) as SalesInvoiceDetailsCount FROM SalesInvoiceDetails;

-- Test chart data query
SELECT TOP 7 
    CAST(si.InvoiceDate AS DATE) as Date,
    SUM(si.TotalAmount) as Revenue
FROM SalesInvoices si
WHERE si.InvoiceDate >= DATEADD(day, -7, GETDATE())
GROUP BY CAST(si.InvoiceDate AS DATE)
ORDER BY Date DESC;

GO
