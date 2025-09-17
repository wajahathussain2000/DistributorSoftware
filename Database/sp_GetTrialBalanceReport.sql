-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Comprehensive Trial Balance Report Stored Procedure
-- Shows account balances grouped by account type with opening, period, and closing balances
-- This is a critical business report that shows the financial position
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetTrialBalanceReport]
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main Trial Balance Query
    WITH AccountBalances AS (
        SELECT 
            coa.AccountId,
            coa.AccountCode,
            coa.AccountName,
            coa.AccountType,
            coa.AccountCategory,
            coa.NormalBalance,
            coa.AccountLevel,
            coa.ParentAccountId,
            parent_coa.AccountName AS ParentAccountName,
            parent_coa.AccountCode AS ParentAccountCode,
            
            -- Opening Balance (all transactions before the start date)
            ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate < @StartDate
            ), 0) AS OpeningBalance,
            
            -- Period Debit (transactions within the date range)
            ISNULL((
                SELECT SUM(jvd.DebitAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ), 0) AS PeriodDebit,
            
            -- Period Credit (transactions within the date range)
            ISNULL((
                SELECT SUM(jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ), 0) AS PeriodCredit,
            
            -- Closing Balance (opening + period transactions up to end date)
            ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @EndDate
            ), 0) AS ClosingBalance,
            
            -- Transaction count for the period
            ISNULL((
                SELECT COUNT(jvd.DetailId)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ), 0) AS TransactionCount,
            
            -- Last transaction date
            (
                SELECT MAX(jv.VoucherDate)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @EndDate
            ) AS LastTransactionDate,
            
            -- Account status
            CASE 
                WHEN coa.IsActive = 1 THEN 'Active'
                ELSE 'Inactive'
            END AS AccountStatus,
            
            -- Account description
            coa.Description AS AccountDescription
            
        FROM ChartOfAccounts coa
        LEFT JOIN ChartOfAccounts parent_coa ON coa.ParentAccountId = parent_coa.AccountId
        WHERE coa.IsActive = 1
    ),
    
    -- Calculate adjusted balances based on normal balance type
    AdjustedBalances AS (
        SELECT 
            *,
            -- For Debit Normal Balance accounts: positive balance = Debit, negative = Credit
            CASE 
                WHEN NormalBalance = 'Debit' THEN
                    CASE 
                        WHEN ClosingBalance >= 0 THEN ClosingBalance
                        ELSE 0
                    END
                ELSE 0
            END AS DebitBalance,
            
            CASE 
                WHEN NormalBalance = 'Debit' THEN
                    CASE 
                        WHEN ClosingBalance < 0 THEN ABS(ClosingBalance)
                        ELSE 0
                    END
                ELSE 0
            END AS CreditBalance,
            
            -- For Credit Normal Balance accounts: positive balance = Credit, negative = Debit
            CASE 
                WHEN NormalBalance = 'Credit' THEN
                    CASE 
                        WHEN ClosingBalance < 0 THEN ABS(ClosingBalance)
                        ELSE 0
                    END
                ELSE 0
            END AS CreditBalanceCredit,
            
            CASE 
                WHEN NormalBalance = 'Credit' THEN
                    CASE 
                        WHEN ClosingBalance >= 0 THEN ClosingBalance
                        ELSE 0
                    END
                ELSE 0
            END AS DebitBalanceCredit
            
        FROM AccountBalances
    )
    
    -- Main result set
    SELECT 
        AccountId,
        AccountCode,
        AccountName,
        AccountType,
        AccountCategory,
        NormalBalance,
        AccountLevel,
        ParentAccountName,
        ParentAccountCode,
        OpeningBalance,
        PeriodDebit,
        PeriodCredit,
        ClosingBalance,
        TransactionCount,
        LastTransactionDate,
        AccountStatus,
        AccountDescription,
        
        -- Final debit and credit balances for trial balance
        CASE 
            WHEN NormalBalance = 'Debit' THEN
                CASE 
                    WHEN ClosingBalance >= 0 THEN ClosingBalance
                    ELSE 0
                END
            ELSE
                CASE 
                    WHEN ClosingBalance < 0 THEN ABS(ClosingBalance)
                    ELSE 0
                END
        END AS TrialBalanceDebit,
        
        CASE 
            WHEN NormalBalance = 'Debit' THEN
                CASE 
                    WHEN ClosingBalance < 0 THEN ABS(ClosingBalance)
                    ELSE 0
                END
            ELSE
                CASE 
                    WHEN ClosingBalance >= 0 THEN ClosingBalance
                    ELSE 0
                END
        END AS TrialBalanceCredit,
        
        -- Additional calculated fields
        CASE 
            WHEN ClosingBalance = 0 THEN 'Zero Balance'
            WHEN ClosingBalance > 0 AND NormalBalance = 'Debit' THEN 'Debit Balance'
            WHEN ClosingBalance < 0 AND NormalBalance = 'Debit' THEN 'Credit Balance'
            WHEN ClosingBalance > 0 AND NormalBalance = 'Credit' THEN 'Credit Balance'
            WHEN ClosingBalance < 0 AND NormalBalance = 'Credit' THEN 'Debit Balance'
            ELSE 'Unknown'
        END AS BalanceType,
        
        -- Days since last transaction
        CASE 
            WHEN LastTransactionDate IS NOT NULL THEN DATEDIFF(DAY, LastTransactionDate, @EndDate)
            ELSE NULL
        END AS DaysSinceLastTransaction,
        
        -- Account activity level
        CASE 
            WHEN TransactionCount = 0 THEN 'No Activity'
            WHEN TransactionCount <= 5 THEN 'Low Activity'
            WHEN TransactionCount <= 20 THEN 'Medium Activity'
            ELSE 'High Activity'
        END AS ActivityLevel
        
    FROM AdjustedBalances
    ORDER BY 
        AccountType,
        AccountCategory,
        AccountCode;
    
    -- Summary information
    SELECT 
        -- Total counts
        COUNT(*) AS TotalAccounts,
        COUNT(CASE WHEN TrialBalanceDebit > 0 THEN 1 END) AS AccountsWithDebitBalance,
        COUNT(CASE WHEN TrialBalanceCredit > 0 THEN 1 END) AS AccountsWithCreditBalance,
        COUNT(CASE WHEN TrialBalanceDebit = 0 AND TrialBalanceCredit = 0 THEN 1 END) AS ZeroBalanceAccounts,
        
        -- Total amounts
        SUM(TrialBalanceDebit) AS TotalDebitAmount,
        SUM(TrialBalanceCredit) AS TotalCreditAmount,
        ABS(SUM(TrialBalanceDebit) - SUM(TrialBalanceCredit)) AS DifferenceAmount,
        
        -- Account type breakdown
        COUNT(CASE WHEN AccountType = 'Asset' THEN 1 END) AS AssetAccounts,
        COUNT(CASE WHEN AccountType = 'Liability' THEN 1 END) AS LiabilityAccounts,
        COUNT(CASE WHEN AccountType = 'Equity' THEN 1 END) AS EquityAccounts,
        COUNT(CASE WHEN AccountType = 'Revenue' THEN 1 END) AS RevenueAccounts,
        COUNT(CASE WHEN AccountType = 'Expense' THEN 1 END) AS ExpenseAccounts,
        
        -- Account category breakdown
        COUNT(CASE WHEN AccountCategory = 'Current Asset' THEN 1 END) AS CurrentAssetAccounts,
        COUNT(CASE WHEN AccountCategory = 'Fixed Asset' THEN 1 END) AS FixedAssetAccounts,
        COUNT(CASE WHEN AccountCategory = 'Current Liability' THEN 1 END) AS CurrentLiabilityAccounts,
        COUNT(CASE WHEN AccountCategory = 'Long Term Liability' THEN 1 END) AS LongTermLiabilityAccounts,
        COUNT(CASE WHEN AccountCategory = 'Owner Equity' THEN 1 END) AS OwnerEquityAccounts,
        
        -- Activity analysis
        COUNT(CASE WHEN TransactionCount = 0 THEN 1 END) AS InactiveAccounts,
        COUNT(CASE WHEN TransactionCount > 0 THEN 1 END) AS ActiveAccounts,
        AVG(CAST(TransactionCount AS FLOAT)) AS AverageTransactionsPerAccount,
        
        -- Balance analysis
        COUNT(CASE WHEN ClosingBalance > 0 THEN 1 END) AS PositiveBalanceAccounts,
        COUNT(CASE WHEN ClosingBalance < 0 THEN 1 END) AS NegativeBalanceAccounts,
        COUNT(CASE WHEN ClosingBalance = 0 THEN 1 END) AS ZeroBalanceAccounts,
        
        -- Report metadata
        @StartDate AS ReportStartDate,
        @EndDate AS ReportEndDate,
        GETDATE() AS ReportGeneratedDate,
        'Trial Balance Report' AS ReportType,
        
        -- Validation flags
        CASE 
            WHEN ABS(SUM(TrialBalanceDebit) - SUM(TrialBalanceCredit)) <= 0.01 THEN 'Balanced'
            ELSE 'Unbalanced'
        END AS TrialBalanceStatus,
        
        -- Top accounts by balance
        (SELECT TOP 1 AccountName FROM AdjustedBalances WHERE TrialBalanceDebit > 0 ORDER BY TrialBalanceDebit DESC) AS HighestDebitAccount,
        (SELECT TOP 1 AccountName FROM AdjustedBalances WHERE TrialBalanceCredit > 0 ORDER BY TrialBalanceCredit DESC) AS HighestCreditAccount,
        
        -- Account level analysis
        COUNT(CASE WHEN AccountLevel = 1 THEN 1 END) AS Level1Accounts,
        COUNT(CASE WHEN AccountLevel = 2 THEN 1 END) AS Level2Accounts,
        COUNT(CASE WHEN AccountLevel = 3 THEN 1 END) AS Level3Accounts,
        COUNT(CASE WHEN AccountLevel > 3 THEN 1 END) AS Level4PlusAccounts
        
    FROM (
        SELECT 
            AccountId,
            AccountCode,
            AccountName,
            AccountType,
            AccountCategory,
            NormalBalance,
            AccountLevel,
            OpeningBalance,
            PeriodDebit,
            PeriodCredit,
            ClosingBalance,
            TransactionCount,
            LastTransactionDate,
            AccountStatus,
            AccountDescription,
            
            CASE 
                WHEN NormalBalance = 'Debit' THEN
                    CASE 
                        WHEN ClosingBalance >= 0 THEN ClosingBalance
                        ELSE 0
                    END
                ELSE
                    CASE 
                        WHEN ClosingBalance < 0 THEN ABS(ClosingBalance)
                        ELSE 0
                    END
            END AS TrialBalanceDebit,
            
            CASE 
                WHEN NormalBalance = 'Debit' THEN
                    CASE 
                        WHEN ClosingBalance < 0 THEN ABS(ClosingBalance)
                        ELSE 0
                    END
                ELSE
                    CASE 
                        WHEN ClosingBalance >= 0 THEN ClosingBalance
                        ELSE 0
                    END
            END AS TrialBalanceCredit
            
        FROM AdjustedBalances
    ) AS TrialBalanceData;
    
END