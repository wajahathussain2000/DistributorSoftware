-- =============================================
-- Author: Distribution Software
-- Create date: 2024
-- Description: Stored procedure to get supplier payment history report data
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetSupplierPaymentHistoryReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @SupplierId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main query to get supplier payment history
    WITH SupplierPaymentHistory AS (
        SELECT 
            st.TransactionId AS PaymentId,
            st.SupplierId,
            s.SupplierCode,
            s.SupplierName,
            s.ContactPerson,
            s.Phone,
            s.Email,
            s.Address,
            s.City,
            s.State,
            s.Country,
            s.GST,
            s.PaymentTermsDays,
            st.TransactionDate AS PaymentDate,
            st.ReferenceNumber AS PaymentNumber,
            st.CreditAmount AS PaymentAmount,
            st.PaymentMethod,
            st.BankName,
            st.TransactionReference,
            st.Description AS Notes,
            st.CreatedDate,
            st.CreatedBy,
            u.Username AS CreatedByUsername,
            u.FirstName + ' ' + ISNULL(u.LastName, '') AS CreatedByFullName,
            -- Calculate running balance
            SUM(st.CreditAmount) OVER (
                PARTITION BY st.SupplierId 
                ORDER BY st.TransactionDate, st.TransactionId 
                ROWS UNBOUNDED PRECEDING
            ) AS RunningPaymentTotal,
            -- Get invoice details if payment is linked to specific invoices
            (
                SELECT COUNT(DISTINCT pi.PurchaseInvoiceId)
                FROM PurchaseInvoices pi
                WHERE pi.SupplierId = st.SupplierId
                    AND pi.InvoiceDate <= st.TransactionDate
            ) AS TotalInvoices,
            -- Get outstanding amount before this payment
            (
                SELECT ISNULL(SUM(pi.TotalAmount), 0) - ISNULL(SUM(st2.CreditAmount), 0)
                FROM PurchaseInvoices pi
                LEFT JOIN SupplierTransactions st2 ON st2.SupplierId = pi.SupplierId 
                    AND st2.TransactionDate <= st.TransactionDate
                    AND st2.IsActive = 1
                    AND st2.TransactionType = 'Payment'
                WHERE pi.SupplierId = st.SupplierId
                    AND pi.InvoiceDate <= st.TransactionDate
            ) AS OutstandingBeforePayment,
            -- Calculate days since payment
            DATEDIFF(DAY, st.TransactionDate, @EndDate) AS DaysSincePayment,
            -- Payment status based on amount and outstanding
            CASE 
                WHEN st.CreditAmount > 0 THEN 'Payment Made'
                ELSE 'Refund/Adjustment'
            END AS PaymentStatus,
            -- Payment frequency analysis
            ROW_NUMBER() OVER (
                PARTITION BY st.SupplierId 
                ORDER BY st.TransactionDate
            ) AS PaymentSequence
        FROM SupplierTransactions st
        INNER JOIN Suppliers s ON st.SupplierId = s.SupplierId
        LEFT JOIN Users u ON TRY_CAST(st.CreatedBy AS INT) = u.UserId
        WHERE st.TransactionDate BETWEEN @StartDate AND @EndDate
            AND st.TransactionType = 'Payment'
            AND st.IsActive = 1
            AND s.IsActive = 1
            AND (@SupplierId IS NULL OR st.SupplierId = @SupplierId)
    )
    SELECT 
        PaymentId,
        SupplierId,
        SupplierCode,
        SupplierName,
        ContactPerson,
        Phone,
        Email,
        Address,
        City,
        State,
        Country,
        GST,
        PaymentTermsDays,
        PaymentDate,
        PaymentNumber,
        PaymentAmount,
        PaymentMethod,
        BankName,
        TransactionReference,
        Notes,
        CreatedDate,
        CreatedBy,
        CreatedByUsername,
        CreatedByFullName,
        RunningPaymentTotal,
        TotalInvoices,
        OutstandingBeforePayment,
        DaysSincePayment,
        PaymentStatus,
        PaymentSequence,
        -- Additional calculated fields
        CASE 
            WHEN PaymentMethod = 'Cash' THEN 'Cash Payment'
            WHEN PaymentMethod = 'Bank Transfer' THEN 'Bank Transfer'
            WHEN PaymentMethod = 'Cheque' THEN 'Cheque Payment'
            WHEN PaymentMethod = 'Online' THEN 'Online Payment'
            ELSE PaymentMethod
        END AS PaymentMethodDescription,
        -- Payment amount in words (simplified)
        CASE 
            WHEN PaymentAmount >= 100000 THEN 'Large Payment'
            WHEN PaymentAmount >= 50000 THEN 'Medium Payment'
            WHEN PaymentAmount >= 10000 THEN 'Small Payment'
            ELSE 'Minor Payment'
        END AS PaymentSizeCategory
    FROM SupplierPaymentHistory
    ORDER BY PaymentDate DESC, PaymentId DESC;
    
    -- Return summary statistics
    SELECT 
        COUNT(*) AS TotalPayments,
        COUNT(DISTINCT st.SupplierId) AS TotalSuppliers,
        SUM(st.CreditAmount) AS TotalPaymentAmount,
        AVG(st.CreditAmount) AS AveragePaymentAmount,
        MIN(st.CreditAmount) AS MinimumPayment,
        MAX(st.CreditAmount) AS MaximumPayment,
        -- Payment method breakdown
        SUM(CASE WHEN st.PaymentMethod = 'Cash' THEN st.CreditAmount ELSE 0 END) AS TotalCashPayments,
        SUM(CASE WHEN st.PaymentMethod = 'Bank Transfer' THEN st.CreditAmount ELSE 0 END) AS TotalBankTransferPayments,
        SUM(CASE WHEN st.PaymentMethod = 'Cheque' THEN st.CreditAmount ELSE 0 END) AS TotalChequePayments,
        SUM(CASE WHEN st.PaymentMethod = 'Online' THEN st.CreditAmount ELSE 0 END) AS TotalOnlinePayments,
        -- Date range analysis
        MIN(st.TransactionDate) AS FirstPaymentDate,
        MAX(st.TransactionDate) AS LastPaymentDate,
        DATEDIFF(DAY, MIN(st.TransactionDate), MAX(st.TransactionDate)) AS PaymentPeriodDays
    FROM SupplierTransactions st
    INNER JOIN Suppliers s ON st.SupplierId = s.SupplierId
    WHERE st.TransactionDate BETWEEN @StartDate AND @EndDate
        AND st.TransactionType = 'Payment'
        AND st.IsActive = 1
        AND s.IsActive = 1
        AND (@SupplierId IS NULL OR s.SupplierId = @SupplierId);
END
