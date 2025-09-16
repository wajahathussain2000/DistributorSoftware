-- Test Sales Summary Report Stored Procedure
-- This script tests the sp_GetSalesSummaryReport stored procedure with various parameters

-- Test 1: Get all sales summary for the current year
PRINT 'Test 1: All Sales Summary for Current Year'
EXEC sp_GetSalesSummaryReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @ProductId = NULL,
    @CustomerId = NULL,
    @SalesmanId = NULL;

-- Test 2: Get sales summary for a specific product
PRINT 'Test 2: Sales Summary for Product ID 1'
EXEC sp_GetSalesSummaryReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @ProductId = 1,
    @CustomerId = NULL,
    @SalesmanId = NULL;

-- Test 3: Get sales summary for a specific customer
PRINT 'Test 3: Sales Summary for Customer ID 1'
EXEC sp_GetSalesSummaryReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @ProductId = NULL,
    @CustomerId = 1,
    @SalesmanId = NULL;

-- Test 4: Get sales summary for a specific salesman
PRINT 'Test 4: Sales Summary for Salesman ID 1'
EXEC sp_GetSalesSummaryReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @ProductId = NULL,
    @CustomerId = NULL,
    @SalesmanId = 1;

-- Test 5: Get sales summary for specific product and customer
PRINT 'Test 5: Sales Summary for Product ID 1 and Customer ID 1'
EXEC sp_GetSalesSummaryReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @ProductId = 1,
    @CustomerId = 1,
    @SalesmanId = NULL;

-- Test 6: Get sales summary for a specific date range
PRINT 'Test 6: Sales Summary for September 2025'
EXEC sp_GetSalesSummaryReport 
    @StartDate = '2025-09-01',
    @EndDate = '2025-09-30',
    @ProductId = NULL,
    @CustomerId = NULL,
    @SalesmanId = NULL;

-- Test 7: Get sales summary with all filters
PRINT 'Test 7: Sales Summary with All Filters (Product 1, Customer 1, Salesman 1)'
EXEC sp_GetSalesSummaryReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @ProductId = 1,
    @CustomerId = 1,
    @SalesmanId = 1;

-- Test 8: Get sales summary with no results (future dates)
PRINT 'Test 8: Sales Summary for Future Dates (Should return empty)'
EXEC sp_GetSalesSummaryReport 
    @StartDate = '2026-01-01',
    @EndDate = '2026-12-31',
    @ProductId = NULL,
    @CustomerId = NULL,
    @SalesmanId = NULL;

-- Additional verification queries
PRINT 'Verification: Check SalesInvoices table structure'
SELECT TOP 3 SalesInvoiceId, InvoiceDate, CustomerId, SalesmanId FROM SalesInvoices;

PRINT 'Verification: Check SalesInvoiceDetails table structure'
SELECT TOP 3 DetailId, SalesInvoiceId, ProductId, ProductName, Quantity, UnitPrice, TotalAmount FROM SalesInvoiceDetails;

PRINT 'Verification: Check Products table'
SELECT TOP 3 ProductId, ProductName, ProductCode FROM Products WHERE IsActive = 1;

PRINT 'Verification: Check Customers table'
SELECT TOP 3 CustomerId, CustomerName FROM Customers WHERE IsActive = 1;

PRINT 'Verification: Check Salesman table'
SELECT TOP 3 SalesmanId, SalesmanName FROM Salesman WHERE IsActive = 1;

PRINT 'Verification: Check Brands table'
SELECT TOP 3 BrandId, BrandName FROM Brands;

PRINT 'Verification: Check ProductCategories table'
SELECT TOP 3 CategoryId, CategoryName FROM ProductCategories;

PRINT 'Verification: Check Units table'
SELECT TOP 3 UnitId, UnitName FROM Units;
