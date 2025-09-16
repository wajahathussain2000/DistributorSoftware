-- Test Sales Return Report Stored Procedure
-- This script tests the sp_GetSalesReturnReport stored procedure with various parameters

-- Test 1: Get all sales returns for the current year
PRINT 'Test 1: All Sales Returns for Current Year'
EXEC sp_GetSalesReturnReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = NULL,
    @SalesmanId = NULL;

-- Test 2: Get sales returns for a specific customer
PRINT 'Test 2: Sales Returns for Customer ID 1'
EXEC sp_GetSalesReturnReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = 1,
    @SalesmanId = NULL;

-- Test 3: Get sales returns for a specific salesman
PRINT 'Test 3: Sales Returns for Salesman ID 1'
EXEC sp_GetSalesReturnReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = NULL,
    @SalesmanId = 1;

-- Test 4: Get sales returns for specific customer and salesman
PRINT 'Test 4: Sales Returns for Customer ID 1 and Salesman ID 1'
EXEC sp_GetSalesReturnReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = 1,
    @SalesmanId = 1;

-- Test 5: Get sales returns for a specific date range
PRINT 'Test 5: Sales Returns for September 2025'
EXEC sp_GetSalesReturnReport 
    @StartDate = '2025-09-01',
    @EndDate = '2025-09-30',
    @CustomerId = NULL,
    @SalesmanId = NULL;

-- Test 6: Get sales returns with no results (future dates)
PRINT 'Test 6: Sales Returns for Future Dates (Should return empty)'
EXEC sp_GetSalesReturnReport 
    @StartDate = '2026-01-01',
    @EndDate = '2026-12-31',
    @CustomerId = NULL,
    @SalesmanId = NULL;

-- Additional verification queries
PRINT 'Verification: Check SalesReturns table structure'
SELECT TOP 3 * FROM SalesReturns;

PRINT 'Verification: Check SalesReturnItems table structure'
SELECT TOP 3 * FROM SalesReturnItems;

PRINT 'Verification: Check Customers table'
SELECT TOP 3 CustomerId, CustomerName FROM Customers;

PRINT 'Verification: Check Salesman table'
SELECT TOP 3 SalesmanId, SalesmanName FROM Salesman;

PRINT 'Verification: Check SalesInvoices table'
SELECT TOP 3 SalesInvoiceId, CustomerId, SalesmanId FROM SalesInvoices;
