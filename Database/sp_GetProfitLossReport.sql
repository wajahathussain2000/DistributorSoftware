CREATE PROCEDURE [dbo].[sp_GetProfitLossReport]
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Profit & Loss Statement Report
    -- This report shows Revenue, Expenses, and Net Profit/Loss for a specific period
    
    WITH RevenueData AS (
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
            
            -- Period Revenue (transactions within the date range)
            ISNULL((
                SELECT SUM(jvd.CreditAmount - jvd.DebitAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ), 0) AS PeriodAmount,
            
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
          AND coa.AccountType = 'REVENUE'
    ),
    ExpenseData AS (
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
            
            -- Period Expense (transactions within the date range)
            ISNULL((
                SELECT SUM(jvd.DebitAmount - jvd.CreditAmount)
                FROM JournalVoucherDetails jvd
                INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
                WHERE jvd.AccountId = coa.AccountId
                  AND jv.VoucherDate >= @StartDate
                  AND jv.VoucherDate <= @EndDate
            ), 0) AS PeriodAmount,
            
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
          AND coa.AccountType = 'EXPENSE'
    ),
    CombinedData AS (
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
            PeriodAmount,
            TransactionCount,
            LastTransactionDate,
            AccountStatus,
            AccountDescription,
            'Revenue' AS SectionType,
            ROW_NUMBER() OVER (ORDER BY AccountCode) AS DisplayOrder
        FROM RevenueData
        WHERE PeriodAmount > 0 OR TransactionCount > 0
        
        UNION ALL
        
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
            PeriodAmount,
            TransactionCount,
            LastTransactionDate,
            AccountStatus,
            AccountDescription,
            'Expense' AS SectionType,
            ROW_NUMBER() OVER (ORDER BY AccountCode) + 1000 AS DisplayOrder
        FROM ExpenseData
        WHERE PeriodAmount > 0 OR TransactionCount > 0
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
        PeriodAmount,
        TransactionCount,
        LastTransactionDate,
        AccountStatus,
        AccountDescription,
        SectionType,
        DisplayOrder,
        
        -- Calculate totals for each section
        CASE 
            WHEN SectionType = 'Revenue' THEN PeriodAmount
            ELSE 0
        END AS RevenueAmount,
        CASE 
            WHEN SectionType = 'Expense' THEN PeriodAmount
            ELSE 0
        END AS ExpenseAmount,
        
        -- Days since last transaction
        CASE 
            WHEN LastTransactionDate IS NOT NULL THEN DATEDIFF(DAY, LastTransactionDate, @EndDate)
            ELSE NULL
        END AS DaysSinceLastTransaction,
        
        -- Account activity level
        CASE 
            WHEN TransactionCount = 0 THEN 'No Activity'
            WHEN TransactionCount <= 5 THEN 'Low Activity'
            WHEN TransactionCount <= 20 THEN 'Moderate Activity'
            ELSE 'High Activity'
        END AS ActivityLevel
    FROM CombinedData
    ORDER BY SectionType, DisplayOrder;
    
END