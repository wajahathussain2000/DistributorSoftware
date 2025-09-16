-- Test Invoice-wise Report Stored Procedure
-- This script tests the sp_GetInvoiceWiseReport stored procedure with various parameters

PRINT 'Testing Invoice-wise Report Stored Procedure...'
PRINT '==============================================='

-- Test 1: All invoices for current year
PRINT 'Test 1: All invoices for current year'
EXEC sp_GetInvoiceWiseReport 
    @StartDate = '2025-01-01', 
    @EndDate = '2025-12-31', 
    @InvoiceNumber = NULL

PRINT ''

-- Test 2: Specific invoice number search
PRINT 'Test 2: Search for invoices containing "INV"'
EXEC sp_GetInvoiceWiseReport 
    @StartDate = '2025-01-01', 
    @EndDate = '2025-12-31', 
    @InvoiceNumber = 'INV'

PRINT ''

-- Test 3: Recent invoices (last 30 days)
PRINT 'Test 3: Recent invoices (last 30 days)'
EXEC sp_GetInvoiceWiseReport 
    @StartDate = DATEADD(day, -30, GETDATE()), 
    @EndDate = GETDATE(), 
    @InvoiceNumber = NULL

PRINT ''

-- Test 4: Specific date range
PRINT 'Test 4: Specific date range (August 2025)'
EXEC sp_GetInvoiceWiseReport 
    @StartDate = '2025-08-01', 
    @EndDate = '2025-08-31', 
    @InvoiceNumber = NULL

PRINT ''
PRINT 'Invoice-wise Report testing completed successfully!'
