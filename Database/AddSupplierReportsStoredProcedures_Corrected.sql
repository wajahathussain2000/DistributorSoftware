-- Add stored procedures for Comprehensive Supplier Reports
-- These procedures should be run on the DistributionDB database

USE DistributionDB;
GO

-- =============================================
-- 1. Supplier Ledger Report Stored Procedure
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetSupplierLedgerReport')
    DROP PROCEDURE sp_GetSupplierLedgerReport;
GO

CREATE PROCEDURE sp_GetSupplierLedgerReport
    @SupplierId INT = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @TransactionType NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Set default dates if not provided
    IF @StartDate IS NULL SET @StartDate = DATEADD(MONTH, -1, GETDATE());
    IF @EndDate IS NULL SET @EndDate = GETDATE();
    
    SELECT 
        st.TransactionId,
        st.TransactionDate,
        st.TransactionType,
        st.Description,
        st.DebitAmount,
        st.CreditAmount,
        st.Balance,
        st.ReferenceNumber,
        s.SupplierId,
        s.SupplierCode,
        s.SupplierName,
        s.ContactPerson,
        s.Phone,
        s.Email,
        s.City,
        s.State,
        -- Additional reference information
        CASE 
            WHEN st.TransactionType = 'Purchase' THEN pi.InvoiceNumber
            WHEN st.TransactionType = 'Payment' THEN sp.PaymentNumber
            WHEN st.TransactionType = 'Return' THEN pr.ReturnNo
            WHEN st.TransactionType = 'Debit Note' THEN sdn.DebitNoteNo
            ELSE st.ReferenceNumber
        END AS ReferenceDocument,
        -- Transaction category for reporting
        CASE 
            WHEN st.TransactionType IN ('Purchase', 'Debit Note') THEN 'Debit'
            WHEN st.TransactionType IN ('Payment', 'Return', 'Credit Note') THEN 'Credit'
            ELSE 'Adjustment'
        END AS TransactionCategory,
        -- Aging information
        DATEDIFF(DAY, st.TransactionDate, GETDATE()) AS DaysSinceTransaction,
        -- Status based on amount
        CASE 
            WHEN st.DebitAmount > 0 AND st.TransactionType IN ('Purchase', 'Debit Note') THEN 'Outstanding'
            WHEN st.CreditAmount > 0 AND st.TransactionType IN ('Payment', 'Return') THEN 'Paid'
            ELSE 'Balanced'
        END AS TransactionStatus
    FROM SupplierTransactions st
    INNER JOIN Suppliers s ON st.SupplierId = s.SupplierId
    LEFT JOIN PurchaseInvoices pi ON st.ReferenceNumber = pi.InvoiceNumber AND st.TransactionType = 'Purchase'
    LEFT JOIN SupplierPayments sp ON st.ReferenceNumber = sp.PaymentNumber AND st.TransactionType = 'Payment'
    LEFT JOIN PurchaseReturns pr ON st.ReferenceNumber = pr.ReturnNo AND st.TransactionType = 'Return'
    LEFT JOIN SupplierDebitNotes sdn ON st.ReferenceNumber = sdn.DebitNoteNo AND st.TransactionType = 'Debit Note'
    WHERE (@SupplierId IS NULL OR st.SupplierId = @SupplierId)
        AND st.TransactionDate BETWEEN @StartDate AND @EndDate
        AND (@TransactionType IS NULL OR st.TransactionType = @TransactionType)
        AND st.IsActive = 1
    ORDER BY st.TransactionDate DESC, st.TransactionId DESC;
END
GO

-- =============================================
-- 2. Supplier Balance Report Stored Procedure
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetSupplierBalanceReport')
    DROP PROCEDURE sp_GetSupplierBalanceReport;
GO

CREATE PROCEDURE sp_GetSupplierBalanceReport
    @SupplierId INT = NULL,
    @AsOfDate DATETIME = NULL,
    @ShowOnlyOutstanding BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Set default date if not provided
    IF @AsOfDate IS NULL SET @AsOfDate = GETDATE();
    
    WITH SupplierBalances AS (
        SELECT 
            s.SupplierId,
            s.SupplierCode,
            s.SupplierName,
            s.ContactPerson,
            s.Phone,
            s.Email,
            s.City,
            s.State,
            s.PaymentTermsDays,
            -- Calculate current balance
            ISNULL(SUM(CASE 
                WHEN st.TransactionType IN ('Purchase', 'Debit Note') THEN st.DebitAmount 
                ELSE 0 
            END), 0) AS TotalDebits,
            ISNULL(SUM(CASE 
                WHEN st.TransactionType IN ('Payment', 'Return', 'Credit Note') THEN st.CreditAmount 
                ELSE 0 
            END), 0) AS TotalCredits,
            -- Calculate outstanding amount
            ISNULL(SUM(CASE 
                WHEN st.TransactionType IN ('Purchase', 'Debit Note') THEN st.DebitAmount 
                ELSE 0 
            END), 0) - ISNULL(SUM(CASE 
                WHEN st.TransactionType IN ('Payment', 'Return', 'Credit Note') THEN st.CreditAmount 
                ELSE 0 
            END), 0) AS CurrentBalance,
            -- Get last transaction date
            MAX(st.TransactionDate) AS LastTransactionDate,
            -- Count transactions
            COUNT(st.TransactionId) AS TransactionCount
        FROM Suppliers s
        LEFT JOIN SupplierTransactions st ON s.SupplierId = st.SupplierId 
            AND st.TransactionDate <= @AsOfDate 
            AND st.IsActive = 1
        WHERE s.IsActive = 1
            AND (@SupplierId IS NULL OR s.SupplierId = @SupplierId)
        GROUP BY s.SupplierId, s.SupplierCode, s.SupplierName, s.ContactPerson, 
                 s.Phone, s.Email, s.City, s.State, s.PaymentTermsDays
    )
    SELECT 
        SupplierId,
        SupplierCode,
        SupplierName,
        ContactPerson,
        Phone,
        Email,
        City,
        State,
        PaymentTermsDays,
        TotalDebits,
        TotalCredits,
        CurrentBalance,
        LastTransactionDate,
        TransactionCount,
        -- Aging analysis
        CASE 
            WHEN CurrentBalance > 0 AND LastTransactionDate IS NOT NULL THEN
                DATEDIFF(DAY, LastTransactionDate, @AsOfDate)
            ELSE 0
        END AS DaysOutstanding,
        -- Aging categories
        CASE 
            WHEN CurrentBalance <= 0 THEN 'No Outstanding'
            WHEN DATEDIFF(DAY, LastTransactionDate, @AsOfDate) <= 30 THEN 'Current (0-30 days)'
            WHEN DATEDIFF(DAY, LastTransactionDate, @AsOfDate) <= 60 THEN '31-60 days'
            WHEN DATEDIFF(DAY, LastTransactionDate, @AsOfDate) <= 90 THEN '61-90 days'
            ELSE 'Over 90 days'
        END AS AgingCategory,
        -- Risk assessment
        CASE 
            WHEN CurrentBalance <= 0 THEN 'Low Risk'
            WHEN CurrentBalance > 0 AND DATEDIFF(DAY, LastTransactionDate, @AsOfDate) <= 30 THEN 'Low Risk'
            WHEN CurrentBalance > 0 AND DATEDIFF(DAY, LastTransactionDate, @AsOfDate) <= 60 THEN 'Medium Risk'
            WHEN CurrentBalance > 0 AND DATEDIFF(DAY, LastTransactionDate, @AsOfDate) <= 90 THEN 'High Risk'
            ELSE 'Critical Risk'
        END AS RiskLevel,
        -- Payment status
        CASE 
            WHEN CurrentBalance <= 0 THEN 'Paid'
            WHEN PaymentTermsDays IS NOT NULL AND DATEDIFF(DAY, LastTransactionDate, @AsOfDate) <= PaymentTermsDays THEN 'Within Terms'
            WHEN PaymentTermsDays IS NOT NULL AND DATEDIFF(DAY, LastTransactionDate, @AsOfDate) > PaymentTermsDays THEN 'Overdue'
            ELSE 'Pending'
        END AS PaymentStatus
    FROM SupplierBalances
    WHERE (@ShowOnlyOutstanding = 0 OR CurrentBalance > 0)
    ORDER BY CurrentBalance DESC, SupplierName;
END
GO

-- =============================================
-- 3. Supplier Payment History Report Stored Procedure
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetSupplierPaymentHistoryReport')
    DROP PROCEDURE sp_GetSupplierPaymentHistoryReport;
GO

CREATE PROCEDURE sp_GetSupplierPaymentHistoryReport
    @SupplierId INT = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @PaymentMethod NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Set default dates if not provided
    IF @StartDate IS NULL SET @StartDate = DATEADD(MONTH, -3, GETDATE());
    IF @EndDate IS NULL SET @EndDate = GETDATE();
    
    SELECT 
        sp.PaymentId,
        sp.PaymentNumber,
        sp.PaymentDate,
        sp.PaymentAmount,
        sp.PaymentMethod,
        sp.TransactionReference,
        sp.Notes,
        sp.Status,
        sp.CreatedDate,
        sp.CreatedBy,
        s.SupplierId,
        s.SupplierCode,
        s.SupplierName,
        s.ContactPerson,
        s.Phone,
        s.Email,
        s.City,
        s.State,
        -- Related transaction information
        st.TransactionId,
        st.TransactionDate AS RelatedTransactionDate,
        st.Description AS TransactionDescription,
        st.Balance,
        -- Payment analysis
        CASE 
            WHEN sp.PaymentMethod = 'Cash' THEN 'Cash Payment'
            WHEN sp.PaymentMethod = 'Cheque' THEN 'Cheque Payment'
            WHEN sp.PaymentMethod = 'Bank Transfer' THEN 'Bank Transfer'
            WHEN sp.PaymentMethod = 'Credit Card' THEN 'Credit Card Payment'
            ELSE 'Other Payment Method'
        END AS PaymentTypeDescription,
        -- Status analysis
        CASE 
            WHEN sp.Status = 'Completed' THEN 'Payment Completed'
            WHEN sp.Status = 'Pending' THEN 'Payment Pending'
            WHEN sp.Status = 'Failed' THEN 'Payment Failed'
            WHEN sp.Status = 'Cancelled' THEN 'Payment Cancelled'
            ELSE 'Unknown Status'
        END AS PaymentStatusDescription,
        -- Outstanding calculation
        (SELECT ISNULL(SUM(CASE 
            WHEN st2.TransactionType IN ('Purchase', 'Debit Note') THEN st2.DebitAmount 
            ELSE 0 
        END), 0) - ISNULL(SUM(CASE 
            WHEN st2.TransactionType IN ('Payment', 'Return', 'Credit Note') THEN st2.CreditAmount 
            ELSE 0 
        END), 0)
        FROM SupplierTransactions st2 
        WHERE st2.SupplierId = s.SupplierId 
            AND st2.TransactionDate <= sp.PaymentDate 
            AND st2.IsActive = 1) AS OutstandingBeforePayment,
        -- Payment efficiency
        CASE 
            WHEN sp.PaymentDate <= DATEADD(DAY, 30, st.TransactionDate) THEN 'Early Payment'
            WHEN sp.PaymentDate <= DATEADD(DAY, 60, st.TransactionDate) THEN 'On Time Payment'
            WHEN sp.PaymentDate <= DATEADD(DAY, 90, st.TransactionDate) THEN 'Late Payment'
            ELSE 'Very Late Payment'
        END AS PaymentEfficiency,
        -- Days to payment
        DATEDIFF(DAY, st.TransactionDate, sp.PaymentDate) AS DaysToPayment
    FROM SupplierPayments sp
    INNER JOIN Suppliers s ON sp.SupplierId = s.SupplierId
    LEFT JOIN SupplierTransactions st ON sp.SupplierId = st.SupplierId 
        AND st.TransactionReference = sp.TransactionReference
        AND st.TransactionType = 'Payment'
    WHERE (@SupplierId IS NULL OR sp.SupplierId = @SupplierId)
        AND sp.PaymentDate BETWEEN @StartDate AND @EndDate
        AND (@PaymentMethod IS NULL OR sp.PaymentMethod = @PaymentMethod)
        AND sp.IsActive = 1
    ORDER BY sp.PaymentDate DESC, sp.PaymentId DESC;
END
GO

-- =============================================
-- 4. Supplier Summary Statistics Stored Procedure
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetSupplierSummaryStatistics')
    DROP PROCEDURE sp_GetSupplierSummaryStatistics;
GO

CREATE PROCEDURE sp_GetSupplierSummaryStatistics
    @AsOfDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Set default date if not provided
    IF @AsOfDate IS NULL SET @AsOfDate = GETDATE();
    
    SELECT 
        -- Total suppliers
        COUNT(DISTINCT s.SupplierId) AS TotalSuppliers,
        COUNT(DISTINCT CASE WHEN s.IsActive = 1 THEN s.SupplierId END) AS ActiveSuppliers,
        COUNT(DISTINCT CASE WHEN s.IsActive = 0 THEN s.SupplierId END) AS InactiveSuppliers,
        
        -- Financial summary
        ISNULL(SUM(CASE 
            WHEN st.TransactionType IN ('Purchase', 'Debit Note') THEN st.DebitAmount 
            ELSE 0 
        END), 0) AS TotalPurchases,
        ISNULL(SUM(CASE 
            WHEN st.TransactionType IN ('Payment', 'Return', 'Credit Note') THEN st.CreditAmount 
            ELSE 0 
        END), 0) AS TotalPayments,
        ISNULL(SUM(CASE 
            WHEN st.TransactionType IN ('Purchase', 'Debit Note') THEN st.DebitAmount 
            ELSE 0 
        END), 0) - ISNULL(SUM(CASE 
            WHEN st.TransactionType IN ('Payment', 'Return', 'Credit Note') THEN st.CreditAmount 
            ELSE 0 
        END), 0) AS TotalOutstanding,
        
        -- Transaction counts
        COUNT(DISTINCT CASE WHEN st.TransactionType = 'Purchase' THEN st.TransactionId END) AS TotalPurchaseTransactions,
        COUNT(DISTINCT CASE WHEN st.TransactionType = 'Payment' THEN st.TransactionId END) AS TotalPaymentTransactions,
        COUNT(DISTINCT CASE WHEN st.TransactionType = 'Return' THEN st.TransactionId END) AS TotalReturnTransactions,
        
        -- Outstanding analysis
        COUNT(DISTINCT CASE 
            WHEN (ISNULL(SUM(CASE 
                WHEN st2.TransactionType IN ('Purchase', 'Debit Note') THEN st2.DebitAmount 
                ELSE 0 
            END), 0) - ISNULL(SUM(CASE 
                WHEN st2.TransactionType IN ('Payment', 'Return', 'Credit Note') THEN st2.CreditAmount 
                ELSE 0 
            END), 0)) > 0 THEN s.SupplierId 
        END) AS SuppliersWithOutstanding,
        
        -- Average outstanding per supplier
        CASE 
            WHEN COUNT(DISTINCT s.SupplierId) > 0 THEN
                (ISNULL(SUM(CASE 
                    WHEN st.TransactionType IN ('Purchase', 'Debit Note') THEN st.DebitAmount 
                    ELSE 0 
                END), 0) - ISNULL(SUM(CASE 
                    WHEN st.TransactionType IN ('Payment', 'Return', 'Credit Note') THEN st.CreditAmount 
                    ELSE 0 
                END), 0)) / COUNT(DISTINCT s.SupplierId)
            ELSE 0
        END AS AverageOutstandingPerSupplier,
        
        -- Date information
        @AsOfDate AS ReportDate,
        GETDATE() AS GeneratedAt
    FROM Suppliers s
    LEFT JOIN SupplierTransactions st ON s.SupplierId = st.SupplierId 
        AND st.TransactionDate <= @AsOfDate 
        AND st.IsActive = 1
    LEFT JOIN SupplierTransactions st2 ON s.SupplierId = st2.SupplierId 
        AND st2.TransactionDate <= @AsOfDate 
        AND st2.IsActive = 1;
END
GO

PRINT 'Comprehensive Supplier Reports stored procedures created successfully!';
PRINT 'Procedures created:';
PRINT '- sp_GetSupplierLedgerReport';
PRINT '- sp_GetSupplierBalanceReport';
PRINT '- sp_GetSupplierPaymentHistoryReport';
PRINT '- sp_GetSupplierSummaryStatistics';
