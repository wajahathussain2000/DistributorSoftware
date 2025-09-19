-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Bank Reconciliation Report Stored Procedure
-- Shows bank transactions vs book transactions for reconciliation
-- This is a critical financial control report for bank account management
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetBankReconciliationReport]
    @BankAccountId INT,
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Bank Reconciliation Report
    -- This report shows bank transactions vs book transactions for reconciliation
    
    -- Create temporary table for all transactions
    CREATE TABLE #AllTransactions (
        TransactionSource VARCHAR(10),
        TransactionType VARCHAR(20),
        TransactionDate DATE,
        Description VARCHAR(255),
        Amount DECIMAL(18,2),
        EntryType VARCHAR(10),
        SortOrder INT,
        RecordType VARCHAR(20),
        MatchStatus VARCHAR(20),
        ReconciliationStatus VARCHAR(30)
    );
    
    -- Insert bank transactions (simulated bank statement data)
    INSERT INTO #AllTransactions (TransactionSource, TransactionType, TransactionDate, Description, Amount, EntryType, SortOrder, RecordType, MatchStatus, ReconciliationStatus)
    VALUES 
        ('BANK', 'Deposit', '2025-09-01', 'Bank Deposit - Cash Sales', 15000.00, 'CREDIT', 1, 'Bank Statement', 'Unmatched', 'Outstanding Bank Item'),
        ('BANK', 'Withdrawal', '2025-09-02', 'Bank Withdrawal - Office Rent', 3000.00, 'DEBIT', 2, 'Bank Statement', 'Unmatched', 'Outstanding Bank Item'),
        ('BANK', 'Deposit', '2025-09-05', 'Bank Deposit - Service Revenue', 8500.00, 'CREDIT', 3, 'Bank Statement', 'Unmatched', 'Outstanding Bank Item'),
        ('BANK', 'Withdrawal', '2025-09-10', 'Bank Withdrawal - Advertising', 2500.00, 'DEBIT', 4, 'Bank Statement', 'Unmatched', 'Outstanding Bank Item'),
        ('BANK', 'Deposit', '2025-09-12', 'Bank Deposit - Credit Sales Collection', 8500.00, 'CREDIT', 5, 'Bank Statement', 'Unmatched', 'Outstanding Bank Item'),
        ('BANK', 'Withdrawal', '2025-09-15', 'Bank Withdrawal - Transportation', 1800.00, 'DEBIT', 6, 'Bank Statement', 'Unmatched', 'Outstanding Bank Item'),
        ('BANK', 'Deposit', '2025-09-18', 'Bank Deposit - Owner Investment', 20000.00, 'CREDIT', 7, 'Bank Statement', 'Unmatched', 'Outstanding Bank Item'),
        ('BANK', 'Withdrawal', '2025-09-19', 'Bank Withdrawal - Vehicle Maintenance', 800.00, 'DEBIT', 8, 'Bank Statement', 'Unmatched', 'Outstanding Bank Item');
    
    -- Insert book transactions from journal vouchers
    -- Only process if it's a valid bank account (not a parent account)
    IF EXISTS (SELECT 1 FROM ChartOfAccounts WHERE AccountId = @BankAccountId AND AccountLevel > 1)
    BEGIN
        INSERT INTO #AllTransactions (TransactionSource, TransactionType, TransactionDate, Description, Amount, EntryType, SortOrder, RecordType, MatchStatus, ReconciliationStatus)
        SELECT 
            'BOOK' AS TransactionSource,
            CASE 
                WHEN jvd.CreditAmount > 0 THEN 'Deposit'
                WHEN jvd.DebitAmount > 0 THEN 'Withdrawal'
                ELSE 'Transfer'
            END AS TransactionType,
            jv.VoucherDate AS TransactionDate,
            jv.Narration AS Description,
            CASE 
                WHEN jvd.CreditAmount > 0 THEN jvd.CreditAmount
                ELSE jvd.DebitAmount
            END AS Amount,
            CASE 
                WHEN jvd.CreditAmount > 0 THEN 'CREDIT'
                ELSE 'DEBIT'
            END AS EntryType,
            jv.VoucherId AS SortOrder,
            'Book Records' AS RecordType,
            'Unmatched' AS MatchStatus,
            'Outstanding Book Item' AS ReconciliationStatus
        FROM JournalVoucherDetails jvd
        INNER JOIN JournalVouchers jv ON jvd.VoucherId = jv.VoucherId
        WHERE jvd.AccountId = @BankAccountId
          AND jv.VoucherDate >= @StartDate
          AND jv.VoucherDate <= @EndDate;
    END
    
    -- Main query
    SELECT 
        TransactionSource,
        TransactionType,
        TransactionDate,
        Description,
        Amount,
        EntryType,
        RecordType,
        MatchStatus,
        ReconciliationStatus,
        SortOrder,
        -- Additional fields for reporting
        CASE 
            WHEN EntryType = 'CREDIT' THEN Amount
            ELSE 0
        END AS CreditAmount,
        CASE 
            WHEN EntryType = 'DEBIT' THEN Amount
            ELSE 0
        END AS DebitAmount,
        -- Running balance calculation
        SUM(CASE 
            WHEN EntryType = 'CREDIT' THEN Amount
            WHEN EntryType = 'DEBIT' THEN -Amount
            ELSE 0
        END) OVER (ORDER BY TransactionDate, SortOrder) AS RunningBalance,
        -- Days since transaction
        DATEDIFF(DAY, TransactionDate, @EndDate) AS DaysSinceTransaction,
        -- Transaction age category
        CASE 
            WHEN DATEDIFF(DAY, TransactionDate, @EndDate) <= 7 THEN 'Recent'
            WHEN DATEDIFF(DAY, TransactionDate, @EndDate) <= 30 THEN 'Current'
            WHEN DATEDIFF(DAY, TransactionDate, @EndDate) <= 90 THEN 'Aging'
            ELSE 'Old'
        END AS TransactionAge
    FROM #AllTransactions
    ORDER BY TransactionDate, SortOrder;
    
    -- Summary section
    SELECT 
        'SUMMARY' AS SectionType,
        COUNT(CASE WHEN TransactionSource = 'BANK' THEN 1 END) AS BankTransactionCount,
        COUNT(CASE WHEN TransactionSource = 'BOOK' THEN 1 END) AS BookTransactionCount,
        SUM(CASE WHEN TransactionSource = 'BANK' AND EntryType = 'CREDIT' THEN Amount ELSE 0 END) AS BankTotalCredits,
        SUM(CASE WHEN TransactionSource = 'BANK' AND EntryType = 'DEBIT' THEN Amount ELSE 0 END) AS BankTotalDebits,
        SUM(CASE WHEN TransactionSource = 'BOOK' AND EntryType = 'CREDIT' THEN Amount ELSE 0 END) AS BookTotalCredits,
        SUM(CASE WHEN TransactionSource = 'BOOK' AND EntryType = 'DEBIT' THEN Amount ELSE 0 END) AS BookTotalDebits,
        COUNT(CASE WHEN MatchStatus = 'Matched' THEN 1 END) AS MatchedTransactions,
        COUNT(CASE WHEN MatchStatus = 'Unmatched' THEN 1 END) AS UnmatchedTransactions,
        @StartDate AS ReportStartDate,
        @EndDate AS ReportEndDate,
        GETDATE() AS ReportGeneratedDate
    FROM #AllTransactions;
    
    -- Clean up
    DROP TABLE #AllTransactions;
    
END
