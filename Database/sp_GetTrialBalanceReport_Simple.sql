-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Trial Balance Report Stored Procedure (Simplified)
-- Shows account balances grouped by account type with opening, period, and closing balances
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetTrialBalanceReport]
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main Trial Balance Query
    SELECT 
        coa.AccountId,
        coa.AccountCode,
        coa.AccountName,
        coa.AccountType,
        coa.AccountCategory,
        coa.NormalBalance,
        coa.AccountLevel,
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
        coa.Description AS AccountDescription,
        
        -- Final debit and credit balances for trial balance
        CASE 
            WHEN coa.NormalBalance = 'Debit' THEN
                CASE 
                    WHEN ISNULL((
                        SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                        FROM JournalVoucherDetails jvd
                        INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                        WHERE jvd.AccountId = coa.AccountId
                          AND jv.VoucherDate <= @EndDate
                    ), 0) >= 0 THEN ISNULL((
                        SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                        FROM JournalVoucherDetails jvd
                        INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                        WHERE jvd.AccountId = coa.AccountId
                          AND jv.VoucherDate <= @EndDate
                    ), 0)
                    ELSE 0
                END
            ELSE
                CASE 
                    WHEN ISNULL((
                        SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                        FROM JournalVoucherDetails jvd
                        INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                        WHERE jvd.AccountId = coa.AccountId
                          AND jv.VoucherDate <= @EndDate
                    ), 0) < 0 THEN ABS(ISNULL((
                        SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                        FROM JournalVoucherDetails jvd
                        INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                        WHERE jvd.AccountId = coa.AccountId
                          AND jv.VoucherDate <= @EndDate
                    ), 0))
                    ELSE 0
                END
        END AS TrialBalanceDebit,
        
        CASE 
            WHEN coa.NormalBalance = 'Debit' THEN
                CASE 
                    WHEN ISNULL((
                        SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                        FROM JournalVoucherDetails jvd
                        INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                        WHERE jvd.AccountId = coa.AccountId
                          AND jv.VoucherDate <= @EndDate
                    ), 0) < 0 THEN ABS(ISNULL((
                        SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                        FROM JournalVoucherDetails jvd
                        INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                        WHERE jvd.AccountId = coa.AccountId
                          AND jv.VoucherDate <= @EndDate
                    ), 0))
                    ELSE 0
                END
            ELSE
                CASE 
                    WHEN ISNULL((
                        SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                        FROM JournalVoucherDetails jvd
                        INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                        WHERE jvd.AccountId = coa.AccountId
                          AND jv.VoucherDate <= @EndDate
                    ), 0) >= 0 THEN ISNULL((
                        SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                        FROM JournalVoucherDetails jvd
                        INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                        WHERE jvd.AccountId = coa.AccountId
                          AND jv.VoucherDate <= @EndDate
                    ), 0)
                    ELSE 0
                END
        END AS TrialBalanceCredit,
        
        -- Additional calculated fields
        CASE 
            WHEN ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @EndDate
            ), 0) = 0 THEN 'Zero Balance'
            WHEN ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @EndDate
            ), 0) > 0 AND coa.NormalBalance = 'Debit' THEN 'Debit Balance'
            WHEN ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @EndDate
            ), 0) < 0 AND coa.NormalBalance = 'Debit' THEN 'Credit Balance'
            WHEN ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @EndDate
            ), 0) > 0 AND coa.NormalBalance = 'Credit' THEN 'Credit Balance'
            WHEN ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @EndDate
            ), 0) < 0 AND coa.NormalBalance = 'Credit' THEN 'Debit Balance'
            ELSE 'Unknown'
        END AS BalanceType,
        
        -- Days since last transaction
        CASE 
            WHEN (
                SELECT MAX(jv.VoucherDate)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @EndDate
            ) IS NOT NULL THEN DATEDIFF(DAY, (
                SELECT MAX(jv.VoucherDate)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @EndDate
            ), @EndDate)
            ELSE NULL
        END AS DaysSinceLastTransaction,
        
        -- Account activity level
        CASE 
            WHEN ISNULL((
                SELECT COUNT(jvd.DetailId)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ), 0) = 0 THEN 'No Activity'
            WHEN ISNULL((
                SELECT COUNT(jvd.DetailId)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ), 0) <= 5 THEN 'Low Activity'
            WHEN ISNULL((
                SELECT COUNT(jvd.DetailId)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ), 0) <= 20 THEN 'Medium Activity'
            ELSE 'High Activity'
        END AS ActivityLevel
        
    FROM ChartOfAccounts coa
    LEFT JOIN ChartOfAccounts parent_coa ON coa.ParentAccountId = parent_coa.AccountId
    WHERE coa.IsActive = 1
    ORDER BY 
        coa.AccountType,
        coa.AccountCategory,
        coa.AccountCode;
    
END
