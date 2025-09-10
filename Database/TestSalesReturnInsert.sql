-- Test script to verify Sales Return functionality
USE DistributionDB
GO

-- Test inserting a sales return record
INSERT INTO SalesReturns (
    ReturnNumber, CustomerId, ReferenceSalesInvoiceId, ReturnDate, Reason, 
    SubTotal, TaxAmount, DiscountAmount, TotalAmount, Status, Remarks, 
    CreatedDate, CreatedBy
)
VALUES (
    'TEST-SR-001', 1, 1, '2024-01-15', 'Test Return', 
    100.00, 10.00, 5.00, 105.00, 'PENDING', 'Test return record', 
    GETDATE(), 1
);

-- Get the inserted ID
DECLARE @ReturnId INT = SCOPE_IDENTITY();

-- Test inserting a sales return item
INSERT INTO SalesReturnItems (
    ReturnId, ProductId, OriginalInvoiceItemId, Quantity, UnitPrice, 
    TaxPercentage, TaxAmount, DiscountPercentage, DiscountAmount, 
    TotalAmount, Remarks, StockUpdated
)
VALUES (
    @ReturnId, 1, 1, 2.00, 50.00, 
    10.00, 10.00, 5.00, 5.00, 
    105.00, 'Test return item', 0
);

-- Verify the data was inserted
SELECT 'SalesReturns' as TableName, COUNT(*) as RecordCount FROM SalesReturns
UNION ALL
SELECT 'SalesReturnItems' as TableName, COUNT(*) as RecordCount FROM SalesReturnItems;

-- Show the test data
SELECT 'SalesReturns Data:' as Info;
SELECT ReturnId, ReturnNumber, CustomerId, TotalAmount, Status FROM SalesReturns WHERE ReturnNumber = 'TEST-SR-001';

SELECT 'SalesReturnItems Data:' as Info;
SELECT ReturnItemId, ReturnId, ProductId, Quantity, TotalAmount FROM SalesReturnItems WHERE ReturnId = @ReturnId;
