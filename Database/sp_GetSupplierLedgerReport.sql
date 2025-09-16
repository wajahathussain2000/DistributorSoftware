-- =============================================
-- Author: Distribution Software
-- Create date: 2024
-- Description: Stored procedure to get supplier ledger report data
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetSupplierLedgerReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @SupplierId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Declare variables for running balance calculation
    DECLARE @RunningBalance DECIMAL(18,2) = 0;
    
    -- Get opening balance (transactions before start date)
    SELECT @RunningBalance = ISNULL(SUM(CASE 
        WHEN TransactionType = 'Purchase' OR TransactionType = 'Return' THEN ISNULL(DebitAmount, 0)
        WHEN TransactionType = 'Payment' THEN -ISNULL(CreditAmount, 0)
        ELSE 0 
    END), 0)
    FROM SupplierTransactions 
    WHERE (@SupplierId IS NULL OR SupplierId = @SupplierId)
        AND TransactionDate < @StartDate
        AND IsActive = 1;
    
    -- Main query to get ledger transactions
    WITH SupplierLedgerData AS (
        SELECT 
            st.TransactionId,
            st.TransactionDate,
            st.TransactionType,
            st.Description,
            st.ReferenceNumber,
            ISNULL(st.DebitAmount, 0) AS DebitAmount,
            ISNULL(st.CreditAmount, 0) AS CreditAmount,
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
            ROW_NUMBER() OVER (ORDER BY st.TransactionDate, st.TransactionId) AS RowNum
        FROM SupplierTransactions st
        INNER JOIN Suppliers s ON st.SupplierId = s.SupplierId
        WHERE (@SupplierId IS NULL OR st.SupplierId = @SupplierId)
            AND st.TransactionDate BETWEEN @StartDate AND @EndDate
            AND st.IsActive = 1
            AND s.IsActive = 1
    ),
    CalculatedBalance AS (
        SELECT 
            *,
            @RunningBalance + SUM(CASE 
                WHEN TransactionType = 'Purchase' OR TransactionType = 'Return' THEN DebitAmount
                WHEN TransactionType = 'Payment' THEN -CreditAmount
                ELSE 0 
            END) OVER (ORDER BY TransactionDate, TransactionId ROWS UNBOUNDED PRECEDING) AS RunningBalance
        FROM SupplierLedgerData
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
        CASE 
            WHEN TransactionType = 'Purchase' THEN 'Purchase Invoice'
            WHEN TransactionType = 'Payment' THEN 'Payment Made'
            WHEN TransactionType = 'Return' THEN 'Purchase Return'
            ELSE TransactionType
        END AS TransactionTypeDescription
    FROM CalculatedBalance
    ORDER BY TransactionDate, TransactionId;
    
    -- Return summary information
    SELECT 
        COUNT(*) AS TotalTransactions,
        SUM(CASE WHEN TransactionType = 'Purchase' OR TransactionType = 'Return' THEN ISNULL(DebitAmount, 0) ELSE 0 END) AS TotalDebits,
        SUM(CASE WHEN TransactionType = 'Payment' THEN ISNULL(CreditAmount, 0) ELSE 0 END) AS TotalCredits,
        @RunningBalance AS OpeningBalance,
        @RunningBalance + SUM(CASE 
            WHEN TransactionType = 'Purchase' OR TransactionType = 'Return' THEN ISNULL(DebitAmount, 0)
            WHEN TransactionType = 'Payment' THEN -ISNULL(CreditAmount, 0)
            ELSE 0 
        END) AS ClosingBalance
    FROM SupplierTransactions st
    INNER JOIN Suppliers s ON st.SupplierId = s.SupplierId
    WHERE (@SupplierId IS NULL OR st.SupplierId = @SupplierId)
        AND st.TransactionDate BETWEEN @StartDate AND @EndDate
        AND st.IsActive = 1
        AND s.IsActive = 1;
END
