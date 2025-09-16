-- =============================================
-- Author: Distribution Software
-- Create date: 2024
-- Description: Stored procedure to get customer balance report data
-- Shows customer balances, credit limits, and transaction summaries
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCustomerBalanceReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @CustomerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main query to get customer balance data
    WITH CustomerBalanceData AS (
        SELECT 
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
            c.IsActive,
            c.CreatedDate,
            c.ModifiedDate,
            c.DiscountPercent,
            c.CategoryId,
            c.TaxNumber,
            c.Remarks,
            -- Calculate current balance from CustomerLedger
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' THEN ISNULL(cl.DebitAmount, 0)
                WHEN cl.TransactionType = 'Receipt' THEN -ISNULL(cl.CreditAmount, 0)
                WHEN cl.TransactionType = 'Return' THEN -ISNULL(cl.CreditAmount, 0)
                ELSE 0 
            END), 0) AS CurrentBalance,
            -- Calculate period transactions
            ISNULL(SUM(CASE 
                WHEN cl.TransactionDate BETWEEN @StartDate AND @EndDate 
                AND cl.TransactionType = 'Invoice' THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS PeriodSales,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionDate BETWEEN @StartDate AND @EndDate 
                AND cl.TransactionType = 'Receipt' THEN ISNULL(cl.CreditAmount, 0)
                ELSE 0 
            END), 0) AS PeriodPayments,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionDate BETWEEN @StartDate AND @EndDate 
                AND cl.TransactionType = 'Return' THEN ISNULL(cl.CreditAmount, 0)
                ELSE 0 
            END), 0) AS PeriodReturns,
            -- Count transactions
            COUNT(CASE WHEN cl.TransactionDate BETWEEN @StartDate AND @EndDate THEN 1 END) AS PeriodTransactionCount,
            COUNT(cl.LedgerId) AS TotalTransactionCount,
            -- Get opening balance (transactions before start date)
            ISNULL(SUM(CASE 
                WHEN cl.TransactionDate < @StartDate THEN
                    CASE 
                        WHEN cl.TransactionType = 'Invoice' THEN ISNULL(cl.DebitAmount, 0)
                        WHEN cl.TransactionType = 'Receipt' THEN -ISNULL(cl.CreditAmount, 0)
                        WHEN cl.TransactionType = 'Return' THEN -ISNULL(cl.CreditAmount, 0)
                        ELSE 0 
                    END
                ELSE 0 
            END), 0) AS OpeningBalance,
            -- Get last transaction date
            MAX(cl.TransactionDate) AS LastTransactionDate,
            -- Get first transaction date
            MIN(cl.TransactionDate) AS FirstTransactionDate
        FROM Customers c
        LEFT JOIN CustomerLedger cl ON c.CustomerId = cl.CustomerId
        WHERE c.IsActive = 1
            AND (@CustomerId IS NULL OR c.CustomerId = @CustomerId)
        GROUP BY 
            c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactName, c.Phone, c.Email,
            c.Address, c.City, c.State, c.Country, c.GSTNumber, c.CreditLimit, 
            c.OutstandingBalance, c.PaymentTerms, c.IsActive, c.CreatedDate, c.ModifiedDate,
            c.DiscountPercent, c.CategoryId, c.TaxNumber, c.Remarks
    )
    SELECT 
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
        CurrentBalance,
        PeriodSales,
        PeriodPayments,
        PeriodReturns,
        PeriodTransactionCount,
        TotalTransactionCount,
        OpeningBalance,
        LastTransactionDate,
        FirstTransactionDate,
        -- Calculate derived fields
        CASE 
            WHEN CreditLimit > 0 THEN CurrentBalance / CreditLimit * 100
            ELSE 0 
        END AS CreditUtilizationPercent,
        CASE 
            WHEN CurrentBalance > 0 THEN 'Outstanding'
            WHEN CurrentBalance < 0 THEN 'Credit'
            ELSE 'Settled'
        END AS BalanceStatus,
        CASE 
            WHEN CreditLimit > 0 AND CurrentBalance > CreditLimit THEN 'Over Limit'
            WHEN CreditLimit > 0 AND CurrentBalance > CreditLimit * 0.8 THEN 'Near Limit'
            ELSE 'Within Limit'
        END AS CreditStatus,
        -- Calculate net period activity
        PeriodSales - PeriodPayments - PeriodReturns AS NetPeriodActivity,
        -- Calculate closing balance
        OpeningBalance + PeriodSales - PeriodPayments - PeriodReturns AS ClosingBalance
    FROM CustomerBalanceData
    ORDER BY CurrentBalance DESC, CustomerName;
    
    -- Return summary information
    SELECT 
        COUNT(*) AS TotalCustomers,
        SUM(CurrentBalance) AS TotalOutstandingBalance,
        SUM(CASE WHEN CurrentBalance > 0 THEN CurrentBalance ELSE 0 END) AS TotalPositiveBalance,
        SUM(CASE WHEN CurrentBalance < 0 THEN CurrentBalance ELSE 0 END) AS TotalNegativeBalance,
        SUM(CASE WHEN CreditLimit > 0 AND CurrentBalance > CreditLimit THEN 1 ELSE 0 END) AS CustomersOverLimit,
        SUM(CASE WHEN CreditLimit > 0 AND CurrentBalance > CreditLimit * 0.8 THEN 1 ELSE 0 END) AS CustomersNearLimit,
        SUM(PeriodSales) AS TotalPeriodSales,
        SUM(PeriodPayments) AS TotalPeriodPayments,
        SUM(PeriodReturns) AS TotalPeriodReturns,
        SUM(PeriodTransactionCount) AS TotalPeriodTransactions
    FROM (
        SELECT 
            c.CustomerId,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' THEN ISNULL(cl.DebitAmount, 0)
                WHEN cl.TransactionType = 'Receipt' THEN -ISNULL(cl.CreditAmount, 0)
                WHEN cl.TransactionType = 'Return' THEN -ISNULL(cl.CreditAmount, 0)
                ELSE 0 
            END), 0) AS CurrentBalance,
            c.CreditLimit,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionDate BETWEEN @StartDate AND @EndDate 
                AND cl.TransactionType = 'Invoice' THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS PeriodSales,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionDate BETWEEN @StartDate AND @EndDate 
                AND cl.TransactionType = 'Receipt' THEN ISNULL(cl.CreditAmount, 0)
                ELSE 0 
            END), 0) AS PeriodPayments,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionDate BETWEEN @StartDate AND @EndDate 
                AND cl.TransactionType = 'Return' THEN ISNULL(cl.CreditAmount, 0)
                ELSE 0 
            END), 0) AS PeriodReturns,
            COUNT(CASE WHEN cl.TransactionDate BETWEEN @StartDate AND @EndDate THEN 1 END) AS PeriodTransactionCount
        FROM Customers c
        LEFT JOIN CustomerLedger cl ON c.CustomerId = cl.CustomerId
        WHERE c.IsActive = 1
            AND (@CustomerId IS NULL OR c.CustomerId = @CustomerId)
        GROUP BY c.CustomerId, c.CreditLimit
    ) AS SummaryData;
END
