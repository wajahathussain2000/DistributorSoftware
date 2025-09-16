-- =============================================
-- Author: Distribution Software
-- Create date: 2024
-- Description: Stored procedure to get customer ledger report data with proper running balance
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCustomerLedgerReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @CustomerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Declare variables for running balance calculation
    DECLARE @RunningBalance DECIMAL(18,2) = 0;
    
    -- Get opening balance (transactions before start date)
    -- For customers: Invoice = Credit (customer owes us), Receipt = Debit (customer paid us), Return = Credit reduction
    -- Note: CreditAmount and DebitAmount columns are swapped in the database
    SELECT @RunningBalance = ISNULL(SUM(CASE 
        WHEN TransactionType = 'Invoice' THEN ISNULL(DebitAmount, 0)
        WHEN TransactionType = 'Receipt' THEN -ISNULL(CreditAmount, 0)
        WHEN TransactionType = 'Return' THEN -ISNULL(CreditAmount, 0)
        ELSE 0 
    END), 0)
    FROM CustomerLedger 
    WHERE (@CustomerId IS NULL OR CustomerId = @CustomerId)
        AND TransactionDate < @StartDate;
    
    -- Main query to get ledger transactions with proper running balance calculation
    WITH LedgerWithBalance AS (
        SELECT 
            cl.LedgerId as TransactionId,
            cl.TransactionDate,
            cl.TransactionType,
            cl.Remarks as Description,
            cl.ReferenceNo as ReferenceNumber,
            ISNULL(cl.DebitAmount, 0) AS DebitAmount,
            ISNULL(cl.CreditAmount, 0) AS CreditAmount,
            c.CustomerId,
            c.CustomerCode,
            c.CustomerName,
            c.ContactName,
            c.Phone,
            c.Email,
            c.Address,
            c.City,
            c.State,
            c.Country,
            c.GSTNumber,
            c.CreditLimit,
            c.OutstandingBalance,
            c.PaymentTerms,
            CASE 
                WHEN cl.TransactionType = 'Invoice' THEN 'Sales Invoice'
                WHEN cl.TransactionType = 'Receipt' THEN 'Payment Received'
                WHEN cl.TransactionType = 'Return' THEN 'Sales Return'
                ELSE cl.TransactionType
            END AS TransactionTypeDescription,
            -- Calculate transaction amount for balance
            -- Note: CreditAmount and DebitAmount columns are swapped in the database
            CASE 
                WHEN cl.TransactionType = 'Invoice' THEN ISNULL(cl.DebitAmount, 0)
                WHEN cl.TransactionType = 'Receipt' THEN -ISNULL(cl.CreditAmount, 0)
                WHEN cl.TransactionType = 'Return' THEN -ISNULL(cl.CreditAmount, 0)
                ELSE 0 
            END AS TransactionAmount,
            ROW_NUMBER() OVER (ORDER BY cl.TransactionDate, cl.LedgerId) AS RowNum
        FROM CustomerLedger cl
        INNER JOIN Customers c ON cl.CustomerId = c.CustomerId
        WHERE (@CustomerId IS NULL OR cl.CustomerId = @CustomerId)
            AND cl.TransactionDate BETWEEN @StartDate AND @EndDate
            AND c.IsActive = 1
    ),
    RunningBalance AS (
        SELECT 
            *,
            @RunningBalance + SUM(TransactionAmount) OVER (
                ORDER BY TransactionDate, TransactionId 
                ROWS UNBOUNDED PRECEDING
            ) AS RunningBalance
        FROM LedgerWithBalance
    )
    SELECT 
        TransactionId,
        TransactionDate,
        TransactionType,
        Description,
        ReferenceNumber,
        DebitAmount,
        CreditAmount,
        RunningBalance,
        CustomerId,
        CustomerCode,
        CustomerName,
        ContactName,
        Phone,
        Email,
        Address,
        City,
        State,
        Country,
        GSTNumber,
        CreditLimit,
        OutstandingBalance,
        PaymentTerms,
        TransactionTypeDescription
    FROM RunningBalance
    ORDER BY TransactionDate, TransactionId;
    
    -- Return summary information
    SELECT 
        COUNT(*) AS TotalTransactions,
        SUM(CASE WHEN TransactionType = 'Invoice' THEN ISNULL(DebitAmount, 0) ELSE 0 END) AS TotalCredits,
        SUM(CASE WHEN TransactionType = 'Receipt' THEN ISNULL(CreditAmount, 0) ELSE 0 END) AS TotalDebits,
        SUM(CASE WHEN TransactionType = 'Return' THEN ISNULL(CreditAmount, 0) ELSE 0 END) AS TotalReturns,
        @RunningBalance AS OpeningBalance,
        @RunningBalance + SUM(CASE 
            WHEN TransactionType = 'Invoice' THEN ISNULL(DebitAmount, 0)
            WHEN TransactionType = 'Receipt' THEN -ISNULL(CreditAmount, 0)
            WHEN TransactionType = 'Return' THEN -ISNULL(CreditAmount, 0)
            ELSE 0 
        END) AS ClosingBalance
    FROM CustomerLedger cl
    INNER JOIN Customers c ON cl.CustomerId = c.CustomerId
    WHERE (@CustomerId IS NULL OR cl.CustomerId = @CustomerId)
        AND cl.TransactionDate BETWEEN @StartDate AND @EndDate
        AND c.IsActive = 1;
END
