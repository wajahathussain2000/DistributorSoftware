-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Stored procedure to get sales register report data
-- Shows sales transactions with filters for customer, salesman, date range, and invoice number
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetSalesRegisterReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @CustomerId INT = NULL,
    @SalesmanId INT = NULL,
    @InvoiceNumber VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main query to get sales register data
    SELECT 
        si.SalesInvoiceId,
        si.InvoiceNumber,
        si.InvoiceDate,
        si.DueDate,
        si.CustomerId,
        c.CustomerCode,
        c.CustomerName,
        c.Phone AS CustomerPhone,
        c.Address AS CustomerAddress,
        si.SalesmanId,
        sm.SalesmanCode,
        sm.SalesmanName,
        sm.Phone AS SalesmanPhone,
        sm.Territory,
        si.SubTotal,
        si.DiscountAmount,
        si.DiscountPercentage,
        si.TaxableAmount,
        si.TaxAmount,
        si.TaxPercentage,
        si.TotalAmount,
        si.PaidAmount,
        si.BalanceAmount,
        si.ChangeAmount,
        si.PaymentMode,
        si.Status,
        si.Remarks,
        si.PrintStatus,
        si.PrintDate,
        si.CashierId,
        u.FirstName + ' ' + u.LastName AS CashierName,
        si.TransactionTime,
        si.CreatedBy,
        uc.FirstName + ' ' + uc.LastName AS CreatedByUser,
        si.CreatedDate,
        si.ModifiedBy,
        si.ModifiedDate,
        -- Calculate derived fields
        CASE 
            WHEN si.Status = 'PAID' THEN 'Paid'
            WHEN si.Status = 'DRAFT' THEN 'Draft'
            WHEN si.Status = 'CONFIRMED' THEN 'Confirmed'
            WHEN si.Status = 'DELIVERED' THEN 'Delivered'
            WHEN si.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE si.Status
        END AS StatusDescription,
        CASE 
            WHEN si.PaymentMode = 'CASH' THEN 'Cash'
            WHEN si.PaymentMode = 'CARD' THEN 'Card'
            WHEN si.PaymentMode = 'EASYPAISA' THEN 'Easypaisa'
            WHEN si.PaymentMode = 'JAZZCASH' THEN 'Jazzcash'
            WHEN si.PaymentMode = 'BANK_TRANSFER' THEN 'Bank Transfer'
            WHEN si.PaymentMode = 'CHEQUE' THEN 'Cheque'
            ELSE si.PaymentMode
        END AS PaymentModeDescription,
        -- Calculate days since invoice
        DATEDIFF(DAY, si.InvoiceDate, GETDATE()) AS DaysSinceInvoice,
        -- Calculate days until due date
        CASE 
            WHEN si.DueDate IS NOT NULL THEN DATEDIFF(DAY, GETDATE(), si.DueDate)
            ELSE NULL
        END AS DaysUntilDue,
        -- Calculate overdue status
        CASE 
            WHEN si.DueDate IS NOT NULL AND si.DueDate < GETDATE() AND si.BalanceAmount > 0 THEN 'Overdue'
            WHEN si.DueDate IS NOT NULL AND si.DueDate >= GETDATE() AND si.BalanceAmount > 0 THEN 'Current'
            WHEN si.BalanceAmount = 0 THEN 'Paid'
            ELSE 'Unknown'
        END AS OverdueStatus,
        -- Calculate payment percentage
        CASE 
            WHEN si.TotalAmount > 0 THEN (si.PaidAmount / si.TotalAmount) * 100
            ELSE 0
        END AS PaymentPercentage,
        -- Calculate net amount (after discount)
        si.SubTotal - si.DiscountAmount AS NetAmount,
        -- Calculate tax rate
        CASE 
            WHEN si.TaxableAmount > 0 THEN (si.TaxAmount / si.TaxableAmount) * 100
            ELSE 0
        END AS EffectiveTaxRate
    FROM SalesInvoices si
    LEFT JOIN Customers c ON si.CustomerId = c.CustomerId
    LEFT JOIN Salesman sm ON si.SalesmanId = sm.SalesmanId
    LEFT JOIN Users u ON si.CashierId = u.UserId
    LEFT JOIN Users uc ON si.CreatedBy = uc.UserId
    WHERE si.InvoiceDate >= @StartDate
        AND si.InvoiceDate <= @EndDate
        AND (@CustomerId IS NULL OR si.CustomerId = @CustomerId)
        AND (@SalesmanId IS NULL OR si.SalesmanId = @SalesmanId)
        AND (@InvoiceNumber IS NULL OR si.InvoiceNumber LIKE '%' + @InvoiceNumber + '%')
    ORDER BY si.InvoiceDate DESC, si.InvoiceNumber DESC;
    
    -- Return summary information
    SELECT 
        COUNT(*) AS TotalInvoices,
        COUNT(DISTINCT si.CustomerId) AS TotalCustomers,
        COUNT(DISTINCT si.SalesmanId) AS TotalSalesmen,
        SUM(si.SubTotal) AS TotalSubTotal,
        SUM(si.DiscountAmount) AS TotalDiscountAmount,
        SUM(si.TaxAmount) AS TotalTaxAmount,
        SUM(si.TotalAmount) AS TotalSalesAmount,
        SUM(si.PaidAmount) AS TotalPaidAmount,
        SUM(si.BalanceAmount) AS TotalBalanceAmount,
        SUM(si.ChangeAmount) AS TotalChangeAmount,
        -- Count by status
        SUM(CASE WHEN si.Status = 'PAID' THEN 1 ELSE 0 END) AS PaidInvoices,
        SUM(CASE WHEN si.Status = 'DRAFT' THEN 1 ELSE 0 END) AS DraftInvoices,
        SUM(CASE WHEN si.Status = 'CONFIRMED' THEN 1 ELSE 0 END) AS ConfirmedInvoices,
        SUM(CASE WHEN si.Status = 'DELIVERED' THEN 1 ELSE 0 END) AS DeliveredInvoices,
        SUM(CASE WHEN si.Status = 'CANCELLED' THEN 1 ELSE 0 END) AS CancelledInvoices,
        -- Count by payment mode
        SUM(CASE WHEN si.PaymentMode = 'CASH' THEN 1 ELSE 0 END) AS CashInvoices,
        SUM(CASE WHEN si.PaymentMode = 'CARD' THEN 1 ELSE 0 END) AS CardInvoices,
        SUM(CASE WHEN si.PaymentMode = 'EASYPAISA' THEN 1 ELSE 0 END) AS EasypaisaInvoices,
        SUM(CASE WHEN si.PaymentMode = 'JAZZCASH' THEN 1 ELSE 0 END) AS JazzcashInvoices,
        SUM(CASE WHEN si.PaymentMode = 'BANK_TRANSFER' THEN 1 ELSE 0 END) AS BankTransferInvoices,
        SUM(CASE WHEN si.PaymentMode = 'CHEQUE' THEN 1 ELSE 0 END) AS ChequeInvoices,
        -- Calculate averages
        AVG(si.TotalAmount) AS AverageInvoiceAmount,
        AVG(si.DiscountPercentage) AS AverageDiscountPercentage,
        AVG(si.TaxPercentage) AS AverageTaxPercentage,
        -- Calculate payment statistics
        SUM(CASE WHEN si.BalanceAmount = 0 THEN 1 ELSE 0 END) AS FullyPaidInvoices,
        SUM(CASE WHEN si.BalanceAmount > 0 THEN 1 ELSE 0 END) AS OutstandingInvoices,
        SUM(CASE WHEN si.DueDate IS NOT NULL AND si.DueDate < GETDATE() AND si.BalanceAmount > 0 THEN 1 ELSE 0 END) AS OverdueInvoices,
        -- Calculate totals by payment mode
        SUM(CASE WHEN si.PaymentMode = 'CASH' THEN si.TotalAmount ELSE 0 END) AS TotalCashSales,
        SUM(CASE WHEN si.PaymentMode = 'CARD' THEN si.TotalAmount ELSE 0 END) AS TotalCardSales,
        SUM(CASE WHEN si.PaymentMode = 'EASYPAISA' THEN si.TotalAmount ELSE 0 END) AS TotalEasypaisaSales,
        SUM(CASE WHEN si.PaymentMode = 'JAZZCASH' THEN si.TotalAmount ELSE 0 END) AS TotalJazzcashSales,
        SUM(CASE WHEN si.PaymentMode = 'BANK_TRANSFER' THEN si.TotalAmount ELSE 0 END) AS TotalBankTransferSales,
        SUM(CASE WHEN si.PaymentMode = 'CHEQUE' THEN si.TotalAmount ELSE 0 END) AS TotalChequeSales
    FROM SalesInvoices si
    LEFT JOIN Customers c ON si.CustomerId = c.CustomerId
    LEFT JOIN Salesman sm ON si.SalesmanId = sm.SalesmanId
    WHERE si.InvoiceDate >= @StartDate
        AND si.InvoiceDate <= @EndDate
        AND (@CustomerId IS NULL OR si.CustomerId = @CustomerId)
        AND (@SalesmanId IS NULL OR si.SalesmanId = @SalesmanId)
        AND (@InvoiceNumber IS NULL OR si.InvoiceNumber LIKE '%' + @InvoiceNumber + '%');
END
