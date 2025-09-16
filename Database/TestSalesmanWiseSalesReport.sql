-- Test Salesman-wise Sales Report
-- This script tests the sp_GetSalesmanWiseSalesReport stored procedure

-- Test 1: All salesmen for current year
PRINT 'Test 1: All salesmen for current year'
EXEC sp_GetSalesmanWiseSalesReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @SalesmanId = NULL,
    @CustomerId = NULL

-- Test 2: Specific salesman for current year
PRINT 'Test 2: Specific salesman for current year'
EXEC sp_GetSalesmanWiseSalesReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @SalesmanId = 1,
    @CustomerId = NULL

-- Test 3: All salesmen for specific customer
PRINT 'Test 3: All salesmen for specific customer'
EXEC sp_GetSalesmanWiseSalesReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @SalesmanId = NULL,
    @CustomerId = 1

-- Test 4: Specific salesman and customer combination
PRINT 'Test 4: Specific salesman and customer combination'
EXEC sp_GetSalesmanWiseSalesReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @SalesmanId = 1,
    @CustomerId = 1

-- Test 5: Check if stored procedure exists
PRINT 'Test 5: Check if stored procedure exists'
SELECT 
    name,
    type_desc,
    create_date,
    modify_date
FROM sys.procedures 
WHERE name = 'sp_GetSalesmanWiseSalesReport'

-- Test 6: Verify data availability
PRINT 'Test 6: Verify data availability'
SELECT 
    COUNT(*) as TotalSalesmen,
    COUNT(CASE WHEN IsActive = 1 THEN 1 END) as ActiveSalesmen
FROM Salesman

SELECT 
    COUNT(*) as TotalInvoices,
    COUNT(DISTINCT SalesmanId) as SalesmenWithInvoices,
    COUNT(DISTINCT CustomerId) as CustomersWithInvoices,
    MIN(InvoiceDate) as EarliestInvoice,
    MAX(InvoiceDate) as LatestInvoice
FROM SalesInvoices
WHERE InvoiceDate >= '2025-01-01' AND InvoiceDate <= '2025-12-31'
