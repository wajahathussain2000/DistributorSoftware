-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Stored procedure to get customer aging report data
-- Shows customer aging analysis with filters for customer and overdue days
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAgingReport]
    @CustomerId INT = NULL,
    @OverdueDays INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main query to get customer aging data
    WITH CustomerAgingData AS (
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
            c.TaxNumber,
            c.Remarks,
            -- Calculate current balance from CustomerLedger
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' THEN ISNULL(cl.DebitAmount, 0)
                WHEN cl.TransactionType = 'Receipt' THEN -ISNULL(cl.CreditAmount, 0)
                WHEN cl.TransactionType = 'Return' THEN -ISNULL(cl.CreditAmount, 0)
                ELSE 0 
            END), 0) AS CurrentBalance,
            -- Calculate aging buckets based on invoice dates
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) <= 30 
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS Current30Days,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) BETWEEN 31 AND 60 
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS Days31To60,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) BETWEEN 61 AND 90 
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS Days61To90,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) > 90 
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS Over90Days,
            -- Calculate overdue amount (considering payment terms)
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) > ISNULL(CAST(c.PaymentTerms AS INT), 30)
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS OverdueAmount,
            -- Calculate days overdue
            MAX(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) > ISNULL(CAST(c.PaymentTerms AS INT), 30)
                THEN DATEDIFF(DAY, cl.TransactionDate, GETDATE()) - ISNULL(CAST(c.PaymentTerms AS INT), 30)
                ELSE 0 
            END) AS MaxDaysOverdue,
            -- Get last invoice date
            MAX(CASE WHEN cl.TransactionType = 'Invoice' THEN cl.TransactionDate END) AS LastInvoiceDate,
            -- Get last payment date
            MAX(CASE WHEN cl.TransactionType = 'Receipt' THEN cl.TransactionDate END) AS LastPaymentDate,
            -- Count invoices
            COUNT(CASE WHEN cl.TransactionType = 'Invoice' THEN 1 END) AS TotalInvoices,
            -- Count payments
            COUNT(CASE WHEN cl.TransactionType = 'Receipt' THEN 1 END) AS TotalPayments
        FROM Customers c
        LEFT JOIN CustomerLedger cl ON c.CustomerId = cl.CustomerId
        WHERE c.IsActive = 1
            AND (@CustomerId IS NULL OR c.CustomerId = @CustomerId)
        GROUP BY 
            c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactName, c.Phone, c.Email,
            c.Address, c.City, c.State, c.Country, c.GSTNumber, c.CreditLimit, 
            c.OutstandingBalance, c.PaymentTerms, c.IsActive, c.CreatedDate, c.ModifiedDate,
            c.TaxNumber, c.Remarks
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
        Current30Days,
        Days31To60,
        Days61To90,
        Over90Days,
        OverdueAmount,
        MaxDaysOverdue,
        LastInvoiceDate,
        LastPaymentDate,
        TotalInvoices,
        TotalPayments,
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
        -- Aging status
        CASE 
            WHEN OverdueAmount > 0 THEN 'Overdue'
            WHEN CurrentBalance > 0 THEN 'Current'
            ELSE 'Settled'
        END AS AgingStatus,
        -- Payment terms in days (default to 30 if not specified)
        ISNULL(CAST(PaymentTerms AS INT), 30) AS PaymentTermsDays,
        -- Days since last invoice
        CASE 
            WHEN LastInvoiceDate IS NOT NULL THEN DATEDIFF(DAY, LastInvoiceDate, GETDATE())
            ELSE NULL 
        END AS DaysSinceLastInvoice,
        -- Days since last payment
        CASE 
            WHEN LastPaymentDate IS NOT NULL THEN DATEDIFF(DAY, LastPaymentDate, GETDATE())
            ELSE NULL 
        END AS DaysSinceLastPayment
    FROM CustomerAgingData
    WHERE (@OverdueDays IS NULL OR MaxDaysOverdue >= @OverdueDays)
        AND CurrentBalance > 0  -- Only show customers with outstanding balances
    ORDER BY OverdueAmount DESC, CurrentBalance DESC, CustomerName;
    
    -- Return summary information
    SELECT 
        COUNT(*) AS TotalCustomers,
        SUM(CurrentBalance) AS TotalOutstandingBalance,
        SUM(CASE WHEN CurrentBalance > 0 THEN CurrentBalance ELSE 0 END) AS TotalPositiveBalance,
        SUM(CASE WHEN CurrentBalance < 0 THEN CurrentBalance ELSE 0 END) AS TotalNegativeBalance,
        SUM(CASE WHEN CreditLimit > 0 AND CurrentBalance > CreditLimit THEN 1 ELSE 0 END) AS CustomersOverLimit,
        SUM(CASE WHEN CreditLimit > 0 AND CurrentBalance > CreditLimit * 0.8 THEN 1 ELSE 0 END) AS CustomersNearLimit,
        -- Aging summary
        SUM(Current30Days) AS TotalCurrent30Days,
        SUM(Days31To60) AS TotalDays31To60,
        SUM(Days61To90) AS TotalDays61To90,
        SUM(Over90Days) AS TotalOver90Days,
        SUM(OverdueAmount) AS TotalOverdueAmount,
        -- Count customers by aging status
        SUM(CASE WHEN OverdueAmount > 0 THEN 1 ELSE 0 END) AS CustomersOverdue,
        SUM(CASE WHEN CurrentBalance > 0 AND OverdueAmount = 0 THEN 1 ELSE 0 END) AS CustomersCurrent,
        SUM(CASE WHEN CurrentBalance = 0 THEN 1 ELSE 0 END) AS CustomersSettled,
        -- Average days overdue
        AVG(CASE WHEN OverdueAmount > 0 THEN MaxDaysOverdue ELSE NULL END) AS AverageDaysOverdue,
        -- Total invoices and payments
        SUM(TotalInvoices) AS TotalInvoices,
        SUM(TotalPayments) AS TotalPayments
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
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) <= 30 
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS Current30Days,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) BETWEEN 31 AND 60 
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS Days31To60,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) BETWEEN 61 AND 90 
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS Days61To90,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) > 90 
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS Over90Days,
            ISNULL(SUM(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) > ISNULL(CAST(c.PaymentTerms AS INT), 30)
                THEN ISNULL(cl.DebitAmount, 0)
                ELSE 0 
            END), 0) AS OverdueAmount,
            MAX(CASE 
                WHEN cl.TransactionType = 'Invoice' 
                AND DATEDIFF(DAY, cl.TransactionDate, GETDATE()) > ISNULL(CAST(c.PaymentTerms AS INT), 30)
                THEN DATEDIFF(DAY, cl.TransactionDate, GETDATE()) - ISNULL(CAST(c.PaymentTerms AS INT), 30)
                ELSE 0 
            END) AS MaxDaysOverdue,
            COUNT(CASE WHEN cl.TransactionType = 'Invoice' THEN 1 END) AS TotalInvoices,
            COUNT(CASE WHEN cl.TransactionType = 'Receipt' THEN 1 END) AS TotalPayments
        FROM Customers c
        LEFT JOIN CustomerLedger cl ON c.CustomerId = cl.CustomerId
        WHERE c.IsActive = 1
            AND (@CustomerId IS NULL OR c.CustomerId = @CustomerId)
        GROUP BY c.CustomerId, c.CreditLimit
    ) AS SummaryData
    WHERE CurrentBalance > 0;
END
