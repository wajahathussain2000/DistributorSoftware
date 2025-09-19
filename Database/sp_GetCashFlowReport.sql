-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Cash Flow Report Stored Procedure
-- Shows Cash Inflows, Cash Outflows, and Net Cash Flow for a specific period
-- This is a critical business report that shows cash movement
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCashFlowReport]
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Cash Flow Report
    -- This report shows Cash Inflows, Cash Outflows, and Net Cash Flow for a specific period
    
    WITH CashFlowData AS (
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
            
            -- Cash Inflows (Credit transactions for cash accounts)
            ISNULL((
                SELECT SUM(jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
                  AND jvd.CreditAmount > 0
            ), 0) AS CashInflow,
            
            -- Cash Outflows (Debit transactions for cash accounts)
            ISNULL((
                SELECT SUM(jvd.DebitAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
                  AND jvd.DebitAmount > 0
            ), 0) AS CashOutflow,
            
            -- Net Cash Flow (Inflow - Outflow)
            ISNULL((
                SELECT SUM(jvd.CreditAmount - jvd.DebitAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ), 0) AS NetCashFlow,
            
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
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ) AS LastTransactionDate,
            
            -- Account status
            CASE 
                WHEN coa.IsActive = 1 THEN 'Active'
                ELSE 'Inactive'
            END AS AccountStatus,
            coa.Description AS AccountDescription
        FROM ChartOfAccounts coa
        LEFT JOIN ChartOfAccounts parent_coa ON coa.ParentAccountId = parent_coa.AccountId
        WHERE coa.IsActive = 1
          AND coa.AccountType IN ('ASSET')
          AND (coa.AccountName LIKE '%Cash%' OR coa.AccountName LIKE '%Bank%' OR coa.AccountCode LIKE '1%')
    )
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
        CashInflow,
        CashOutflow,
        NetCashFlow,
        TransactionCount,
        LastTransactionDate,
        AccountStatus,
        AccountDescription,
        
        -- Cash flow type
        CASE 
            WHEN NetCashFlow > 0 THEN 'Positive'
            WHEN NetCashFlow < 0 THEN 'Negative'
            ELSE 'Neutral'
        END AS CashFlowType,
        
        -- Activity level
        CASE 
            WHEN TransactionCount = 0 THEN 'No Activity'
            WHEN TransactionCount <= 5 THEN 'Low Activity'
            WHEN TransactionCount <= 20 THEN 'Moderate Activity'
            ELSE 'High Activity'
        END AS ActivityLevel,
        
        -- Display order for proper sorting
        CASE 
            WHEN AccountName LIKE '%Cash%' THEN 1
            WHEN AccountName LIKE '%Bank%' THEN 2
            ELSE 3
        END AS DisplayOrder,
        
        -- Days since last transaction
        CASE 
            WHEN LastTransactionDate IS NOT NULL THEN DATEDIFF(DAY, LastTransactionDate, @EndDate)
            ELSE NULL
        END AS DaysSinceLastTransaction
        
    FROM CashFlowData
    WHERE CashInflow <> 0 OR CashOutflow <> 0 OR TransactionCount > 0
    ORDER BY DisplayOrder, AccountCode;
    
END