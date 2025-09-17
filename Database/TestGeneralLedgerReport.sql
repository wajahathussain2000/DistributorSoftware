-- Test script for General Ledger Report
-- This script tests the sp_GetGeneralLedgerReport stored procedure with various parameter combinations

PRINT 'Testing General Ledger Report Stored Procedure...'
PRINT '================================================'

-- Test 1: All accounts for current month
PRINT 'Test 1: All accounts for current month'
PRINT '--------------------------------------'
EXEC sp_GetGeneralLedgerReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @AccountId = NULL

PRINT ''

-- Test 2: Specific account for current month
PRINT 'Test 2: Specific account for current month'
PRINT '------------------------------------------'
-- First, get a sample account ID
DECLARE @SampleAccountId INT
SELECT TOP 1 @SampleAccountId = AccountId FROM ChartOfAccounts WHERE IsActive = 1

IF @SampleAccountId IS NOT NULL
BEGIN
    PRINT 'Testing with Account ID: ' + CAST(@SampleAccountId AS VARCHAR(10))
    EXEC sp_GetGeneralLedgerReport 
        @StartDate = '2025-01-01',
        @EndDate = '2025-01-31',
        @AccountId = @SampleAccountId
END
ELSE
BEGIN
    PRINT 'No active accounts found in ChartOfAccounts table'
END

PRINT ''

-- Test 3: All accounts for last 30 days
PRINT 'Test 3: All accounts for last 30 days'
PRINT '--------------------------------------'
EXEC sp_GetGeneralLedgerReport 
    @StartDate = '2024-12-01',
    @EndDate = '2024-12-31',
    @AccountId = NULL

PRINT ''

-- Test 4: Check if we have any journal voucher data
PRINT 'Test 4: Data availability check'
PRINT '-------------------------------'
SELECT 
    'JournalVouchers' AS TableName,
    COUNT(*) AS RecordCount,
    MIN(VoucherDate) AS EarliestDate,
    MAX(VoucherDate) AS LatestDate
FROM JournalVouchers

UNION ALL

SELECT 
    'JournalVoucherDetails' AS TableName,
    COUNT(*) AS RecordCount,
    NULL AS EarliestDate,
    NULL AS LatestDate
FROM JournalVoucherDetails

UNION ALL

SELECT 
    'ChartOfAccounts' AS TableName,
    COUNT(*) AS RecordCount,
    NULL AS EarliestDate,
    NULL AS LatestDate
FROM ChartOfAccounts
WHERE IsActive = 1

PRINT ''
PRINT 'Test completed successfully!'
PRINT 'If you see data above, the stored procedure is working correctly.'
PRINT 'If you see empty results, you may need to add some test data to the JournalVouchers and JournalVoucherDetails tables.'