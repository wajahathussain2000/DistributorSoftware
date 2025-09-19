-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Balance Sheet Report Stored Procedure
-- Shows Assets, Liabilities, and Equity as of a specific date
-- This is a critical business report that shows the financial position
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetBalanceSheetReport]
    @ReportDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Balance Sheet Report
    -- This report shows Assets, Liabilities, and Equity as of a specific date
    
    WITH AssetData AS (
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
            
            -- Current Balance (all transactions up to the report date)
            ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @ReportDate
            ), 0) AS CurrentBalance,
            
            -- Previous Balance (all transactions up to the previous year end)
            ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= DATEADD(YEAR, -1, @ReportDate)
            ), 0) AS PreviousBalance,
            
            -- Transaction count for the current year
            ISNULL((
                SELECT COUNT(jvd.DetailId)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= DATEADD(YEAR, -1, @ReportDate)
                  AND jv.VoucherDate <= @ReportDate
            ), 0) AS TransactionCount,
            
            -- Last transaction date
            (
                SELECT MAX(jv.VoucherDate)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate <= @ReportDate
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
          AND coa.AccountType IN ('ASSET', 'LIABILITY', 'EQUITY')
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
        CurrentBalance,
        PreviousBalance,
        TransactionCount,
        LastTransactionDate,
        AccountStatus,
        AccountDescription,
        
        -- Calculate balance change
        CurrentBalance - PreviousBalance AS BalanceChange,
        
        -- Calculate balance change percentage
        CASE 
            WHEN PreviousBalance <> 0 THEN 
                ((CurrentBalance - PreviousBalance) / PreviousBalance) * 100
            ELSE 0
        END AS BalanceChangePercent,
        
        -- Section type based on account type
        CASE 
            WHEN AccountType = 'ASSET' THEN 'Assets'
            WHEN AccountType = 'LIABILITY' THEN 'Liabilities'
            WHEN AccountType = 'EQUITY' THEN 'Equity'
            ELSE 'Other'
        END AS SectionType,
        
        -- Display order for proper sorting
        CASE 
            WHEN AccountType = 'ASSET' THEN 1
            WHEN AccountType = 'LIABILITY' THEN 2
            WHEN AccountType = 'EQUITY' THEN 3
            ELSE 4
        END AS DisplayOrder,
        
        -- Days since last transaction
        CASE 
            WHEN LastTransactionDate IS NOT NULL THEN DATEDIFF(DAY, LastTransactionDate, @ReportDate)
            ELSE NULL
        END AS DaysSinceLastTransaction,
        
        -- Account activity level
        CASE 
            WHEN TransactionCount = 0 THEN 'No Activity'
            WHEN TransactionCount <= 5 THEN 'Low Activity'
            WHEN TransactionCount <= 20 THEN 'Moderate Activity'
            ELSE 'High Activity'
        END AS ActivityLevel,
        
        -- Balance status
        CASE 
            WHEN CurrentBalance > 0 THEN 'Positive'
            WHEN CurrentBalance < 0 THEN 'Negative'
            ELSE 'Zero'
        END AS BalanceStatus
        
    FROM AssetData
    WHERE CurrentBalance <> 0 OR PreviousBalance <> 0 OR TransactionCount > 0
    ORDER BY DisplayOrder, AccountCode;
    
END