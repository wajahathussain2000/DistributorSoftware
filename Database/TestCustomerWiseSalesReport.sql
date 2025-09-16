-- Test Customer-wise Sales Report
-- This script tests the sp_GetCustomerWiseSalesReport stored procedure

-- Test 1: All customers for current year
PRINT 'Test 1: All customers for current year'
EXEC sp_GetCustomerWiseSalesReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = NULL

-- Test 2: Specific customer for current year
PRINT 'Test 2: Specific customer for current year'
EXEC sp_GetCustomerWiseSalesReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = 1

-- Test 3: All customers for last 30 days
PRINT 'Test 3: All customers for last 30 days'
EXEC sp_GetCustomerWiseSalesReport 
    @StartDate = DATEADD(day, -30, GETDATE()),
    @EndDate = GETDATE(),
    @CustomerId = NULL

-- Test 4: All customers for a specific month
PRINT 'Test 4: All customers for January 2025'
EXEC sp_GetCustomerWiseSalesReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @CustomerId = NULL

-- Test 5: Check if stored procedure exists
PRINT 'Test 5: Check if stored procedure exists'
SELECT 
    name,
    type_desc,
    create_date,
    modify_date
FROM sys.procedures 
WHERE name = 'sp_GetCustomerWiseSalesReport'

-- Test 6: Verify data availability
PRINT 'Test 6: Verify data availability'
SELECT 
    COUNT(*) as TotalCustomers,
    COUNT(CASE WHEN IsActive = 1 THEN 1 END) as ActiveCustomers
FROM Customers

SELECT 
    COUNT(*) as TotalInvoices,
    COUNT(DISTINCT CustomerId) as CustomersWithInvoices,
    MIN(InvoiceDate) as EarliestInvoice,
    MAX(InvoiceDate) as LatestInvoice
FROM SalesInvoices
WHERE InvoiceDate >= '2025-01-01' AND InvoiceDate <= '2025-12-31'
