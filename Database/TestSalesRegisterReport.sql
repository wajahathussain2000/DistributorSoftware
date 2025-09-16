-- =============================================
-- Test script for Sales Register Report
-- =============================================

-- Test the stored procedure with different parameters
PRINT 'Testing Sales Register Report Stored Procedure...'
PRINT '================================================'

-- Test 1: Get all sales invoices for current month
PRINT 'Test 1: All sales invoices for current month'
PRINT '--------------------------------------------'
EXEC sp_GetSalesRegisterReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = NULL,
    @SalesmanId = NULL,
    @InvoiceNumber = NULL

PRINT ''
PRINT '================================================'

-- Test 2: Get sales for a specific customer (if exists)
PRINT 'Test 2: Sales for specific customer (CustomerId = 1)'
PRINT '---------------------------------------------------'
EXEC sp_GetSalesRegisterReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = 1,
    @SalesmanId = NULL,
    @InvoiceNumber = NULL

PRINT ''
PRINT '================================================'

-- Test 3: Get sales for a specific salesman (if exists)
PRINT 'Test 3: Sales for specific salesman (SalesmanId = 1)'
PRINT '---------------------------------------------------'
EXEC sp_GetSalesRegisterReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = NULL,
    @SalesmanId = 1,
    @InvoiceNumber = NULL

PRINT ''
PRINT '================================================'

-- Test 4: Search for specific invoice number
PRINT 'Test 4: Search for specific invoice number (2025090027)'
PRINT '------------------------------------------------------'
EXEC sp_GetSalesRegisterReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = NULL,
    @SalesmanId = NULL,
    @InvoiceNumber = '2025090027'

PRINT ''
PRINT '================================================'

-- Test 5: Check table structure and data availability
PRINT 'Test 5: Check required tables structure and data'
PRINT '------------------------------------------------'

-- Check if SalesInvoices table exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SalesInvoices')
BEGIN
    PRINT '✓ SalesInvoices table exists'
    
    -- Check row count
    DECLARE @InvoiceCount INT
    SELECT @InvoiceCount = COUNT(*) FROM SalesInvoices
    PRINT '✓ SalesInvoices table has ' + CAST(@InvoiceCount AS VARCHAR(10)) + ' invoices'
    
    -- Show sample data
    PRINT ''
    PRINT 'Sample SalesInvoices data:'
    SELECT TOP 5 
        SalesInvoiceId,
        InvoiceNumber,
        InvoiceDate,
        CustomerId,
        SalesmanId,
        TotalAmount,
        Status,
        PaymentMode
    FROM SalesInvoices
    ORDER BY InvoiceDate DESC
END
ELSE
BEGIN
    PRINT '✗ SalesInvoices table does not exist'
END

PRINT ''
PRINT '================================================'

-- Check if Customers table exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Customers')
BEGIN
    PRINT '✓ Customers table exists'
    
    -- Check row count
    DECLARE @CustomerCount INT
    SELECT @CustomerCount = COUNT(*) FROM Customers WHERE IsActive = 1
    PRINT '✓ Customers table has ' + CAST(@CustomerCount AS VARCHAR(10)) + ' active customers'
END
ELSE
BEGIN
    PRINT '✗ Customers table does not exist'
END

PRINT ''
PRINT '================================================'

-- Check if Salesman table exists
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Salesman')
BEGIN
    PRINT '✓ Salesman table exists'
    
    -- Check row count
    DECLARE @SalesmanCount INT
    SELECT @SalesmanCount = COUNT(*) FROM Salesman WHERE IsActive = 1
    PRINT '✓ Salesman table has ' + CAST(@SalesmanCount AS VARCHAR(10)) + ' active salesmen'
    
    -- Show sample data
    PRINT ''
    PRINT 'Sample Salesman data:'
    SELECT TOP 3 
        SalesmanId,
        SalesmanCode,
        SalesmanName,
        IsActive
    FROM Salesman
    WHERE IsActive = 1
    ORDER BY SalesmanName
END
ELSE
BEGIN
    PRINT '✗ Salesman table does not exist'
END

PRINT ''
PRINT '================================================'

-- Test 6: Check sales summary statistics
PRINT 'Test 6: Sales summary statistics'
PRINT '--------------------------------'

-- Get sales statistics for current year
SELECT 
    'Current Year Sales' AS Period,
    COUNT(*) AS TotalInvoices,
    SUM(TotalAmount) AS TotalSalesAmount,
    AVG(TotalAmount) AS AverageInvoiceAmount,
    MIN(TotalAmount) AS MinInvoiceAmount,
    MAX(TotalAmount) AS MaxInvoiceAmount
FROM SalesInvoices 
WHERE YEAR(InvoiceDate) = 2025

PRINT ''
PRINT 'Sales by Status:'
SELECT 
    Status,
    COUNT(*) AS InvoiceCount,
    SUM(TotalAmount) AS TotalAmount
FROM SalesInvoices
WHERE YEAR(InvoiceDate) = 2025
GROUP BY Status
ORDER BY TotalAmount DESC

PRINT ''
PRINT 'Sales by Payment Mode:'
SELECT 
    PaymentMode,
    COUNT(*) AS InvoiceCount,
    SUM(TotalAmount) AS TotalAmount
FROM SalesInvoices
WHERE YEAR(InvoiceDate) = 2025
GROUP BY PaymentMode
ORDER BY TotalAmount DESC

PRINT ''
PRINT '================================================'
PRINT 'Test completed!'
PRINT '================================================'
