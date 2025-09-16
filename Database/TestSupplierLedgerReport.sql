-- =============================================
-- Test Script for Supplier Ledger Report
-- Author: Distribution Software
-- Description: Test the sp_GetSupplierLedgerReport stored procedure
-- =============================================

-- First, let's check if we have suppliers and transactions
PRINT 'Checking Suppliers...'
SELECT COUNT(*) AS SupplierCount FROM Suppliers WHERE IsActive = 1;

PRINT 'Checking Supplier Transactions...'
SELECT COUNT(*) AS TransactionCount FROM SupplierTransactions;

-- Test the stored procedure with sample data
PRINT 'Testing Supplier Ledger Report...'

-- Test with a specific supplier (replace with actual SupplierId from your database)
DECLARE @TestSupplierId INT = (SELECT TOP 1 SupplierId FROM Suppliers WHERE IsActive = 1);

IF @TestSupplierId IS NOT NULL
BEGIN
    PRINT 'Testing with Supplier ID: ' + CAST(@TestSupplierId AS VARCHAR(10));
    
    -- Test the stored procedure
    EXEC sp_GetSupplierLedgerReport 
        @StartDate = '2024-01-01',
        @EndDate = '2024-12-31',
        @SupplierId = @TestSupplierId;
END
ELSE
BEGIN
    PRINT 'No active suppliers found. Please add some suppliers first.';
END

-- Test with all suppliers
PRINT 'Testing with All Suppliers...'
EXEC sp_GetSupplierLedgerReport 
    @StartDate = '2024-01-01',
    @EndDate = '2024-12-31',
    @SupplierId = NULL;

PRINT 'Test completed successfully!';
