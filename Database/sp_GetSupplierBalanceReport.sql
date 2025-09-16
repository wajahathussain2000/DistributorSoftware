-- =============================================
-- Author: Distribution Software
-- Create date: 2024
-- Description: Stored procedure to get supplier balance report data
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetSupplierBalanceReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @SupplierId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main query to get supplier balance summary
    WITH SupplierBalanceData AS (
        SELECT 
            s.SupplierId,
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
            -- Calculate opening balance (transactions before start date)
            ISNULL((
                SELECT SUM(CASE 
                    WHEN TransactionType = 'Purchase' OR TransactionType = 'Return' THEN ISNULL(DebitAmount, 0)
                    WHEN TransactionType = 'Payment' THEN -ISNULL(CreditAmount, 0)
                    ELSE 0 
                END)
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate < @StartDate
                    AND st.IsActive = 1
            ), 0) AS OpeningBalance,
            -- Calculate total purchases in date range
            ISNULL((
                SELECT SUM(ISNULL(DebitAmount, 0))
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate BETWEEN @StartDate AND @EndDate
                    AND st.TransactionType IN ('Purchase', 'Return')
                    AND st.IsActive = 1
            ), 0) AS TotalPurchases,
            -- Calculate total payments in date range
            ISNULL((
                SELECT SUM(ISNULL(CreditAmount, 0))
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate BETWEEN @StartDate AND @EndDate
                    AND st.TransactionType = 'Payment'
                    AND st.IsActive = 1
            ), 0) AS TotalPayments,
            -- Calculate closing balance
            ISNULL((
                SELECT SUM(CASE 
                    WHEN TransactionType = 'Purchase' OR TransactionType = 'Return' THEN ISNULL(DebitAmount, 0)
                    WHEN TransactionType = 'Payment' THEN -ISNULL(CreditAmount, 0)
                    ELSE 0 
                END)
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate <= @EndDate
                    AND st.IsActive = 1
            ), 0) AS ClosingBalance,
            -- Count transactions in date range
            (
                SELECT COUNT(*)
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate BETWEEN @StartDate AND @EndDate
                    AND st.IsActive = 1
            ) AS TransactionCount,
            -- Get last transaction date
            (
                SELECT MAX(st.TransactionDate)
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate <= @EndDate
                    AND st.IsActive = 1
            ) AS LastTransactionDate
        FROM Suppliers s
        WHERE s.IsActive = 1
            AND (@SupplierId IS NULL OR s.SupplierId = @SupplierId)
    )
    SELECT 
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
        OpeningBalance,
        TotalPurchases,
        TotalPayments,
        ClosingBalance,
        TransactionCount,
        LastTransactionDate,
        -- Calculate net movement (purchases - payments)
        TotalPurchases - TotalPayments AS NetMovement,
        -- Calculate days since last transaction
        CASE 
            WHEN LastTransactionDate IS NOT NULL 
            THEN DATEDIFF(DAY, LastTransactionDate, @EndDate)
            ELSE NULL 
        END AS DaysSinceLastTransaction,
        -- Status based on balance
        CASE 
            WHEN ClosingBalance > 0 THEN 'Outstanding'
            WHEN ClosingBalance < 0 THEN 'Advance'
            ELSE 'Settled'
        END AS BalanceStatus
    FROM SupplierBalanceData
    ORDER BY ClosingBalance DESC, SupplierName;
    
    -- Return summary totals
    SELECT 
        COUNT(*) AS TotalSuppliers,
        SUM(OpeningBalance) AS TotalOpeningBalance,
        SUM(TotalPurchases) AS TotalPurchases,
        SUM(TotalPayments) AS TotalPayments,
        SUM(ClosingBalance) AS TotalClosingBalance,
        SUM(TransactionCount) AS TotalTransactions
    FROM (
        SELECT 
            s.SupplierId,
            ISNULL((
                SELECT SUM(CASE 
                    WHEN TransactionType = 'Purchase' OR TransactionType = 'Return' THEN ISNULL(DebitAmount, 0)
                    WHEN TransactionType = 'Payment' THEN -ISNULL(CreditAmount, 0)
                    ELSE 0 
                END)
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate < @StartDate
                    AND st.IsActive = 1
            ), 0) AS OpeningBalance,
            ISNULL((
                SELECT SUM(ISNULL(DebitAmount, 0))
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate BETWEEN @StartDate AND @EndDate
                    AND st.TransactionType IN ('Purchase', 'Return')
                    AND st.IsActive = 1
            ), 0) AS TotalPurchases,
            ISNULL((
                SELECT SUM(ISNULL(CreditAmount, 0))
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate BETWEEN @StartDate AND @EndDate
                    AND st.TransactionType = 'Payment'
                    AND st.IsActive = 1
            ), 0) AS TotalPayments,
            ISNULL((
                SELECT SUM(CASE 
                    WHEN TransactionType = 'Purchase' OR TransactionType = 'Return' THEN ISNULL(DebitAmount, 0)
                    WHEN TransactionType = 'Payment' THEN -ISNULL(CreditAmount, 0)
                    ELSE 0 
                END)
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate <= @EndDate
                    AND st.IsActive = 1
            ), 0) AS ClosingBalance,
            (
                SELECT COUNT(*)
                FROM SupplierTransactions st
                WHERE st.SupplierId = s.SupplierId 
                    AND st.TransactionDate BETWEEN @StartDate AND @EndDate
                    AND st.IsActive = 1
            ) AS TransactionCount
        FROM Suppliers s
        WHERE s.IsActive = 1
            AND (@SupplierId IS NULL OR s.SupplierId = @SupplierId)
    ) AS SummaryData;
END
