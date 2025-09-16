-- =============================================
-- Test script for Customer Receipts Report
-- =============================================

-- Test the stored procedure with different parameters
PRINT 'Testing Customer Receipts Report Stored Procedure...'
PRINT '=================================================='

-- Test 1: Get all receipts for 2025
PRINT 'Test 1: All receipts for 2025'
PRINT '-----------------------------'
EXEC sp_GetCustomerReceiptsReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = NULL

PRINT ''
PRINT '=================================================='

-- Test 2: Get receipts for a specific customer (if exists)
PRINT 'Test 2: Receipts for specific customer (CustomerId = 1)'
PRINT '------------------------------------------------------'
EXEC sp_GetCustomerReceiptsReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = 1

PRINT ''
PRINT '=================================================='

-- Test 3: Get receipts for a specific date range
PRINT 'Test 3: Receipts for January 2025'
PRINT '----------------------------------'
EXEC sp_GetCustomerReceiptsReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @CustomerId = NULL

PRINT ''
PRINT '=================================================='

-- Test 4: Check if CustomerReceipts table exists and has data
PRINT 'Test 4: Check CustomerReceipts table structure and data'
PRINT '-------------------------------------------------------'

-- Check if table exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CustomerReceipts')
BEGIN
    PRINT '✓ CustomerReceipts table exists'
    
    -- Check row count
    DECLARE @ReceiptCount INT
    SELECT @ReceiptCount = COUNT(*) FROM CustomerReceipts
    PRINT '✓ CustomerReceipts table has ' + CAST(@ReceiptCount AS VARCHAR(10)) + ' records'
    
    -- Show sample data
    PRINT ''
    PRINT 'Sample CustomerReceipts data:'
    SELECT TOP 5 
        ReceiptId,
        ReceiptNumber,
        ReceiptDate,
        CustomerId,
        CustomerName,
        Amount,
        PaymentMethod,
        Status
    FROM CustomerReceipts
    ORDER BY ReceiptDate DESC
END
ELSE
BEGIN
    PRINT '✗ CustomerReceipts table does not exist'
END

PRINT ''
PRINT '=================================================='

-- Test 5: Check if Customers table exists and has data
PRINT 'Test 5: Check Customers table structure and data'
PRINT '-----------------------------------------------'

-- Check if table exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Customers')
BEGIN
    PRINT '✓ Customers table exists'
    
    -- Check row count
    DECLARE @CustomerCount INT
    SELECT @CustomerCount = COUNT(*) FROM Customers WHERE IsActive = 1
    PRINT '✓ Customers table has ' + CAST(@CustomerCount AS VARCHAR(10)) + ' active customers'
    
    -- Show sample data
    PRINT ''
    PRINT 'Sample Customers data:'
    SELECT TOP 5 
        CustomerId,
        CustomerCode,
        CustomerName,
        ContactName,
        Phone,
        IsActive
    FROM Customers
    WHERE IsActive = 1
    ORDER BY CustomerName
END
ELSE
BEGIN
    PRINT '✗ Customers table does not exist'
END

PRINT ''
PRINT '=================================================='
PRINT 'Test completed!'
PRINT '=================================================='
