-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Stored procedure to get General Ledger Report data
-- Shows account transactions with filters for account and date range
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetGeneralLedgerReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @AccountId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main query to get General Ledger data
    SELECT 
        jvd.DetailId,
        jv.VoucherId,
        jv.VoucherNumber,
        jv.VoucherDate,
        jv.Reference,
        jv.Narration AS VoucherNarration,
        jv.TotalDebit AS VoucherTotalDebit,
        jv.TotalCredit AS VoucherTotalCredit,
        jv.CreatedDate AS VoucherCreatedDate,
        jv.CreatedBy AS VoucherCreatedBy,
        jv.CreatedByName AS VoucherCreatedByName,
        jv.ModifiedDate AS VoucherModifiedDate,
        jv.ModifiedBy AS VoucherModifiedBy,
        jv.ModifiedByName AS VoucherModifiedByName,
        jv.BankAccountId,
        ba.AccountNumber AS BankAccountNumber,
        ba.BankName,
        jvd.AccountId,
        coa.AccountCode,
        coa.AccountName,
        coa.AccountType,
        coa.AccountCategory,
        coa.AccountLevel,
        coa.NormalBalance,
        coa.Description AS AccountDescription,
        coa.ParentAccountId,
        parent_coa.AccountName AS ParentAccountName,
        parent_coa.AccountCode AS ParentAccountCode,
        jvd.DebitAmount,
        jvd.CreditAmount,
        jvd.Narration AS DetailNarration,
        -- Calculate running balance
        SUM(jvd.DebitAmount - jvd.CreditAmount) OVER (
            PARTITION BY jvd.AccountId 
            ORDER BY jv.VoucherDate, jvd.DetailId 
            ROWS UNBOUNDED PRECEDING
        ) AS RunningBalance,
        -- Calculate opening balance (sum of all transactions before start date)
        ISNULL((
            SELECT SUM(jvd2.DebitAmount - jvd2.CreditAmount)
            FROM JournalVoucherDetails jvd2
            INNER JOIN JournalVouchers jv2 ON jvd2.VoucherId = jv2.VoucherId
            WHERE jvd2.AccountId = jvd.AccountId
              AND jv2.VoucherDate < @StartDate
        ), 0) AS OpeningBalance,
        -- Calculate closing balance (sum of all transactions up to end date)
        ISNULL((
            SELECT SUM(jvd3.DebitAmount - jvd3.CreditAmount)
            FROM JournalVoucherDetails jvd3
            INNER JOIN JournalVouchers jv3 ON jvd3.VoucherId = jv3.VoucherId
            WHERE jvd3.AccountId = jvd.AccountId
              AND jv3.VoucherDate <= @EndDate
        ), 0) AS ClosingBalance,
        -- Transaction type
        CASE 
            WHEN jvd.DebitAmount > 0 THEN 'Debit'
            WHEN jvd.CreditAmount > 0 THEN 'Credit'
            ELSE 'Zero'
        END AS TransactionType,
        -- Balance type
        CASE 
            WHEN coa.NormalBalance = 'Debit' THEN 'Debit'
            WHEN coa.NormalBalance = 'Credit' THEN 'Credit'
            ELSE 'Unknown'
        END AS NormalBalanceType,
        -- Days since transaction
        DATEDIFF(DAY, jv.VoucherDate, GETDATE()) AS DaysSinceTransaction,
        -- Transaction amount (absolute value)
        ABS(jvd.DebitAmount - jvd.CreditAmount) AS TransactionAmount,
        -- Account status
        CASE 
            WHEN coa.IsActive = 1 THEN 'Active'
            ELSE 'Inactive'
        END AS AccountStatus
    FROM JournalVoucherDetails jvd
    INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
    INNER JOIN ChartOfAccounts coa ON jvd.AccountId = coa.AccountId
    LEFT JOIN ChartOfAccounts parent_coa ON coa.ParentAccountId = parent_coa.AccountId
    LEFT JOIN BankAccounts ba ON jv.BankAccountId = ba.BankAccountId
    WHERE jv.VoucherDate >= @StartDate
        AND jv.VoucherDate <= @EndDate
        AND (@AccountId IS NULL OR jvd.AccountId = @AccountId)
    ORDER BY jvd.AccountId, jv.VoucherDate, jvd.DetailId;
    
    -- Return summary information
    SELECT 
        COUNT(DISTINCT jvd.AccountId) AS TotalAccounts,
        COUNT(DISTINCT jv.VoucherId) AS TotalVouchers,
        COUNT(jvd.DetailId) AS TotalTransactions,
        SUM(jvd.DebitAmount) AS TotalDebitAmount,
        SUM(jvd.CreditAmount) AS TotalCreditAmount,
        SUM(jvd.DebitAmount - jvd.CreditAmount) AS NetAmount,
        -- Account type summary
        COUNT(DISTINCT CASE WHEN coa.AccountType = 'Asset' THEN jvd.AccountId END) AS AssetAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountType = 'Liability' THEN jvd.AccountId END) AS LiabilityAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountType = 'Equity' THEN jvd.AccountId END) AS EquityAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountType = 'Revenue' THEN jvd.AccountId END) AS RevenueAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountType = 'Expense' THEN jvd.AccountId END) AS ExpenseAccounts,
        -- Transaction type summary
        COUNT(CASE WHEN jvd.DebitAmount > 0 THEN 1 END) AS DebitTransactions,
        COUNT(CASE WHEN jvd.CreditAmount > 0 THEN 1 END) AS CreditTransactions,
        -- Account category summary
        COUNT(DISTINCT CASE WHEN coa.AccountCategory = 'Current Asset' THEN jvd.AccountId END) AS CurrentAssetAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountCategory = 'Fixed Asset' THEN jvd.AccountId END) AS FixedAssetAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountCategory = 'Current Liability' THEN jvd.AccountId END) AS CurrentLiabilityAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountCategory = 'Long Term Liability' THEN jvd.AccountId END) AS LongTermLiabilityAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountCategory = 'Owner Equity' THEN jvd.AccountId END) AS OwnerEquityAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountCategory = 'Revenue' THEN jvd.AccountId END) AS RevenueCategoryAccounts,
        COUNT(DISTINCT CASE WHEN coa.AccountCategory = 'Expense' THEN jvd.AccountId END) AS ExpenseCategoryAccounts,
        -- Top accounts by transaction count
        (SELECT TOP 1 coa2.AccountName 
         FROM JournalVoucherDetails jvd2 
         INNER JOIN ChartOfAccounts coa2 ON jvd2.AccountId = coa2.AccountId
         INNER JOIN JournalVouchers jv2 ON jvd2.VoucherId = jv2.VoucherId
         WHERE jv2.VoucherDate >= @StartDate AND jv2.VoucherDate <= @EndDate
           AND (@AccountId IS NULL OR jvd2.AccountId = @AccountId)
         GROUP BY coa2.AccountId, coa2.AccountName
         ORDER BY COUNT(jvd2.DetailId) DESC) AS MostActiveAccount,
        -- Top accounts by amount
        (SELECT TOP 1 coa3.AccountName 
         FROM JournalVoucherDetails jvd3 
         INNER JOIN ChartOfAccounts coa3 ON jvd3.AccountId = coa3.AccountId
         INNER JOIN JournalVouchers jv3 ON jvd3.VoucherId = jv3.VoucherId
         WHERE jv3.VoucherDate >= @StartDate AND jv3.VoucherDate <= @EndDate
           AND (@AccountId IS NULL OR jvd3.AccountId = @AccountId)
         GROUP BY coa3.AccountId, coa3.AccountName
         ORDER BY SUM(ABS(jvd3.DebitAmount - jvd3.CreditAmount)) DESC) AS HighestValueAccount,
        -- Average transaction amount
        AVG(ABS(jvd.DebitAmount - jvd.CreditAmount)) AS AverageTransactionAmount,
        -- Date range info
        MIN(jv.VoucherDate) AS EarliestTransactionDate,
        MAX(jv.VoucherDate) AS LatestTransactionDate,
        DATEDIFF(DAY, MIN(jv.VoucherDate), MAX(jv.VoucherDate)) AS ReportPeriodDays
    FROM JournalVoucherDetails jvd
    INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
    INNER JOIN ChartOfAccounts coa ON jvd.AccountId = coa.AccountId
    WHERE jv.VoucherDate >= @StartDate
        AND jv.VoucherDate <= @EndDate
        AND (@AccountId IS NULL OR jvd.AccountId = @AccountId);
END
