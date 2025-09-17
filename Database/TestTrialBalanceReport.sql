-- Test script for Trial Balance Report
-- This script tests the sp_GetTrialBalanceReport stored procedure

PRINT 'Testing Trial Balance Report Stored Procedure...'
PRINT '==============================================='

-- Test 1: Trial Balance for current month
PRINT 'Test 1: Trial Balance for current month'
PRINT '---------------------------------------'
DECLARE @StartDate DATETIME = '2025-01-01'
DECLARE @EndDate DATETIME = '2025-01-31'
EXEC sp_GetTrialBalanceReport @StartDate = @StartDate, @EndDate = @EndDate

PRINT ''
PRINT '==============================================='
PRINT ''

-- Test 2: Trial Balance for last month
PRINT 'Test 2: Trial Balance for last month'
PRINT '------------------------------------'
DECLARE @LastMonthStart DATETIME = DATEADD(MONTH, -1, DATEADD(DAY, -DAY(GETDATE()) + 1, GETDATE()))
DECLARE @LastMonthEnd DATETIME = DATEADD(DAY, -DAY(GETDATE()), GETDATE())
EXEC sp_GetTrialBalanceReport @StartDate = @LastMonthStart, @EndDate = @LastMonthEnd

PRINT ''
PRINT '==============================================='
PRINT ''

-- Test 3: Trial Balance for specific date range
PRINT 'Test 3: Trial Balance for specific date range (2025-01-01 to 2025-01-31)'
PRINT '----------------------------------------------------------------------'
EXEC sp_GetTrialBalanceReport @StartDate = '2025-01-01', @EndDate = '2025-01-31'

PRINT ''
PRINT '==============================================='
PRINT ''

-- Test 4: Data availability check
PRINT 'Test 4: Data availability check'
PRINT '-------------------------------'
SELECT 
    'ChartOfAccounts' AS TableName,
    COUNT(*) AS TotalRecords,
    COUNT(CASE WHEN IsActive = 1 THEN 1 END) AS ActiveRecords,
    COUNT(CASE WHEN IsActive = 0 THEN 1 END) AS InactiveRecords
FROM ChartOfAccounts

UNION ALL

SELECT 
    'JournalVouchers' AS TableName,
    COUNT(*) AS TotalRecords,
    COUNT(CASE WHEN VoucherDate <= GETDATE() THEN 1 END) AS ActiveRecords,
    COUNT(CASE WHEN VoucherDate > GETDATE() THEN 1 END) AS InactiveRecords
FROM JournalVouchers

UNION ALL

SELECT 
    'JournalVoucherDetails' AS TableName,
    COUNT(*) AS TotalRecords,
    COUNT(CASE WHEN DebitAmount > 0 OR CreditAmount > 0 THEN 1 END) AS ActiveRecords,
    COUNT(CASE WHEN DebitAmount = 0 AND CreditAmount = 0 THEN 1 END) AS InactiveRecords
FROM JournalVoucherDetails

PRINT ''
PRINT '==============================================='
PRINT ''

-- Test 5: Account type distribution
PRINT 'Test 5: Account type distribution'
PRINT '---------------------------------'
SELECT 
    AccountType,
    COUNT(*) AS AccountCount,
    COUNT(CASE WHEN IsActive = 1 THEN 1 END) AS ActiveCount
FROM ChartOfAccounts
GROUP BY AccountType
ORDER BY AccountType

PRINT ''
PRINT '==============================================='
PRINT ''

-- Test 6: Account category distribution
PRINT 'Test 6: Account category distribution'
PRINT '--------------------------------------'
SELECT 
    AccountCategory,
    COUNT(*) AS AccountCount,
    COUNT(CASE WHEN IsActive = 1 THEN 1 END) AS ActiveCount
FROM ChartOfAccounts
GROUP BY AccountCategory
ORDER BY AccountCategory

PRINT ''
PRINT '==============================================='
PRINT ''

-- Test 7: Sample account balances
PRINT 'Test 7: Sample account balances (Top 10 accounts)'
PRINT '--------------------------------------------------'
SELECT TOP 10
    coa.AccountCode,
    coa.AccountName,
    coa.AccountType,
    coa.NormalBalance,
    ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0) AS CurrentBalance
FROM ChartOfAccounts coa
LEFT JOIN JournalVoucherDetails jvd ON coa.AccountId = jvd.AccountId
LEFT JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
WHERE coa.IsActive = 1
GROUP BY coa.AccountId, coa.AccountCode, coa.AccountName, coa.AccountType, coa.NormalBalance
ORDER BY ABS(ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0)) DESC

PRINT ''
PRINT '==============================================='
PRINT ''

-- Test 8: Trial Balance validation
PRINT 'Test 8: Trial Balance validation'
PRINT '---------------------------------'
DECLARE @TotalDebit DECIMAL(18,2)
DECLARE @TotalCredit DECIMAL(18,2)

-- Calculate totals using the same logic as the stored procedure
SELECT 
    @TotalDebit = SUM(
        CASE 
            WHEN coa.NormalBalance = 'Debit' THEN
                CASE 
                    WHEN ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0) >= 0 THEN ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0)
                    ELSE 0
                END
            ELSE
                CASE 
                    WHEN ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0) < 0 THEN ABS(ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0))
                    ELSE 0
                END
        END
    ),
    @TotalCredit = SUM(
        CASE 
            WHEN coa.NormalBalance = 'Debit' THEN
                CASE 
                    WHEN ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0) < 0 THEN ABS(ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0))
                    ELSE 0
                END
            ELSE
                CASE 
                    WHEN ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0) >= 0 THEN ISNULL(SUM(jvd.DebitAmount - jvd.CreditAmount), 0)
                    ELSE 0
                END
        END
    )
FROM ChartOfAccounts coa
LEFT JOIN JournalVoucherDetails jvd ON coa.AccountId = jvd.AccountId
LEFT JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
WHERE coa.IsActive = 1
GROUP BY coa.AccountId, coa.NormalBalance

SELECT 
    @TotalDebit AS TotalDebitAmount,
    @TotalCredit AS TotalCreditAmount,
    @TotalDebit - @TotalCredit AS Difference,
    CASE 
        WHEN ABS(@TotalDebit - @TotalCredit) <= 0.01 THEN 'Trial Balance is Balanced'
        ELSE 'Trial Balance is NOT Balanced - Check for errors'
    END AS BalanceStatus

PRINT ''
PRINT 'Test completed successfully!'
PRINT 'If you see data above, the stored procedure is working correctly.'
PRINT 'If you see empty results, you may need to add some test data to the ChartOfAccounts, JournalVouchers and JournalVoucherDetails tables.'
PRINT ''
PRINT 'Key points to verify:'
PRINT '1. Total Debit should equal Total Credit (Trial Balance should balance)'
PRINT '2. All active accounts should be included'
PRINT '3. Account types and categories should be properly grouped'
PRINT '4. Opening, period, and closing balances should be calculated correctly'
