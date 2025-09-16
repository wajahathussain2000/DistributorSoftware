-- =============================================
-- Test script for Customer Aging Report
-- =============================================

-- Test the stored procedure with different parameters
PRINT 'Testing Customer Aging Report Stored Procedure...'
PRINT '================================================'

-- Test 1: Get all customers with aging data
PRINT 'Test 1: All customers aging analysis'
PRINT '-----------------------------------'
EXEC sp_GetAgingReport 
    @CustomerId = NULL,
    @OverdueDays = NULL

PRINT ''
PRINT '================================================'

-- Test 2: Get aging for a specific customer (if exists)
PRINT 'Test 2: Aging for specific customer (CustomerId = 1)'
PRINT '---------------------------------------------------'
EXEC sp_GetAgingReport 
    @CustomerId = 1,
    @OverdueDays = NULL

PRINT ''
PRINT '================================================'

-- Test 3: Get customers with 30+ days overdue
PRINT 'Test 3: Customers with 30+ days overdue'
PRINT '---------------------------------------'
EXEC sp_GetAgingReport 
    @CustomerId = NULL,
    @OverdueDays = 30

PRINT ''
PRINT '================================================'

-- Test 4: Get customers with 90+ days overdue
PRINT 'Test 4: Customers with 90+ days overdue'
PRINT '---------------------------------------'
EXEC sp_GetAgingReport 
    @CustomerId = NULL,
    @OverdueDays = 90

PRINT ''
PRINT '================================================'

-- Test 5: Check if required tables exist and have data
PRINT 'Test 5: Check required tables structure and data'
PRINT '------------------------------------------------'

-- Check if Customers table exists
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
        PaymentTerms,
        CreditLimit,
        OutstandingBalance,
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
PRINT '================================================'

-- Check if CustomerLedger table exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CustomerLedger')
BEGIN
    PRINT '✓ CustomerLedger table exists'
    
    -- Check row count
    DECLARE @LedgerCount INT
    SELECT @LedgerCount = COUNT(*) FROM CustomerLedger
    PRINT '✓ CustomerLedger table has ' + CAST(@LedgerCount AS VARCHAR(10)) + ' transactions'
    
    -- Show sample data
    PRINT ''
    PRINT 'Sample CustomerLedger data:'
    SELECT TOP 5 
        LedgerId,
        CustomerId,
        TransactionDate,
        TransactionType,
        ReferenceNo,
        DebitAmount,
        CreditAmount,
        Remarks
    FROM CustomerLedger
    ORDER BY TransactionDate DESC
END
ELSE
BEGIN
    PRINT '✗ CustomerLedger table does not exist'
END

PRINT ''
PRINT '================================================'

-- Test 6: Check aging calculation logic
PRINT 'Test 6: Aging calculation verification'
PRINT '---------------------------------------'

-- Check if we have invoice transactions
DECLARE @InvoiceCount INT
SELECT @InvoiceCount = COUNT(*) FROM CustomerLedger WHERE TransactionType = 'Invoice'
PRINT 'Total Invoice transactions: ' + CAST(@InvoiceCount AS VARCHAR(10))

-- Check if we have receipt transactions
DECLARE @ReceiptCount INT
SELECT @ReceiptCount = COUNT(*) FROM CustomerLedger WHERE TransactionType = 'Receipt'
PRINT 'Total Receipt transactions: ' + CAST(@ReceiptCount AS VARCHAR(10))

-- Check aging buckets
PRINT ''
PRINT 'Aging buckets analysis:'
SELECT 
    '0-30 Days' AS AgingBucket,
    COUNT(*) AS TransactionCount,
    SUM(DebitAmount) AS TotalAmount
FROM CustomerLedger 
WHERE TransactionType = 'Invoice' 
    AND DATEDIFF(DAY, TransactionDate, GETDATE()) <= 30

UNION ALL

SELECT 
    '31-60 Days' AS AgingBucket,
    COUNT(*) AS TransactionCount,
    SUM(DebitAmount) AS TotalAmount
FROM CustomerLedger 
WHERE TransactionType = 'Invoice' 
    AND DATEDIFF(DAY, TransactionDate, GETDATE()) BETWEEN 31 AND 60

UNION ALL

SELECT 
    '61-90 Days' AS AgingBucket,
    COUNT(*) AS TransactionCount,
    SUM(DebitAmount) AS TotalAmount
FROM CustomerLedger 
WHERE TransactionType = 'Invoice' 
    AND DATEDIFF(DAY, TransactionDate, GETDATE()) BETWEEN 61 AND 90

UNION ALL

SELECT 
    'Over 90 Days' AS AgingBucket,
    COUNT(*) AS TransactionCount,
    SUM(DebitAmount) AS TotalAmount
FROM CustomerLedger 
WHERE TransactionType = 'Invoice' 
    AND DATEDIFF(DAY, TransactionDate, GETDATE()) > 90

PRINT ''
PRINT '================================================'
PRINT 'Test completed!'
PRINT '================================================'
