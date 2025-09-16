-- Test Profit Margin Report Stored Procedure
-- This script tests the sp_GetProfitMarginReport stored procedure with various parameters

PRINT 'Testing Profit Margin Report Stored Procedure...'
PRINT '==============================================='

-- Test 1: All products for current year
PRINT 'Test 1: All products for current year'
EXEC sp_GetProfitMarginReport 
    @StartDate = '2025-01-01', 
    @EndDate = '2025-12-31', 
    @ProductId = NULL

PRINT ''

-- Test 2: Specific product (replace with actual ProductId from your database)
PRINT 'Test 2: Specific product (ProductId = 1)'
EXEC sp_GetProfitMarginReport 
    @StartDate = '2025-01-01', 
    @EndDate = '2025-12-31', 
    @ProductId = 1

PRINT ''

-- Test 3: Recent sales (last 30 days)
PRINT 'Test 3: Recent sales (last 30 days)'
EXEC sp_GetProfitMarginReport 
    @StartDate = DATEADD(day, -30, GETDATE()), 
    @EndDate = GETDATE(), 
    @ProductId = NULL

PRINT ''

-- Test 4: Specific date range
PRINT 'Test 4: Specific date range (August 2025)'
EXEC sp_GetProfitMarginReport 
    @StartDate = '2025-08-01', 
    @EndDate = '2025-08-31', 
    @ProductId = NULL

PRINT ''
PRINT 'Profit Margin Report testing completed successfully!'
