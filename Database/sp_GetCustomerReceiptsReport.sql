-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Stored procedure to get customer receipts report data
-- Shows customer receipts with filters for customer and date range
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCustomerReceiptsReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @CustomerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main query to get customer receipts data
    SELECT 
        cr.ReceiptId,
        cr.ReceiptNumber,
        cr.ReceiptDate,
        cr.CustomerId,
        cr.CustomerName,
        cr.CustomerPhone,
        cr.CustomerAddress,
        cr.Amount,
        cr.PaymentMethod,
        cr.PaymentMode,
        cr.InvoiceReference,
        cr.Description,
        cr.ReceivedBy,
        cr.Status,
        cr.Remarks,
        cr.CreatedBy,
        cr.CreatedByUser,
        cr.CreatedDate,
        cr.ModifiedBy,
        cr.ModifiedDate,
        cr.BankName,
        cr.AccountNumber,
        cr.ChequeNumber,
        cr.ChequeDate,
        cr.TransactionId,
        cr.CardNumber,
        cr.CardType,
        cr.MobileNumber,
        cr.PaymentReference,
        cr.ReceiptAccountId,
        cr.ReceivableAccountId,
        cr.JournalVoucherId,
        -- Additional customer information
        c.CustomerCode,
        c.ContactName,
        c.Email,
        c.City,
        c.State,
        c.Country,
        c.GSTNumber,
        c.CreditLimit,
        c.OutstandingBalance,
        c.PaymentTerms,
        -- Payment method description
        CASE 
            WHEN cr.PaymentMethod = 'CASH' THEN 'Cash'
            WHEN cr.PaymentMethod = 'BANK_TRANSFER' THEN 'Bank Transfer'
            WHEN cr.PaymentMethod = 'CHEQUE' THEN 'Cheque'
            WHEN cr.PaymentMethod = 'CARD' THEN 'Card Payment'
            WHEN cr.PaymentMethod = 'EASYPAISA' THEN 'Easypaisa'
            WHEN cr.PaymentMethod = 'JAZZCASH' THEN 'JazzCash'
            WHEN cr.PaymentMethod = 'OTHER' THEN 'Other'
            ELSE cr.PaymentMethod
        END AS PaymentMethodDescription,
        -- Status description
        CASE 
            WHEN cr.Status = 'PENDING' THEN 'Pending'
            WHEN cr.Status = 'COMPLETED' THEN 'Completed'
            WHEN cr.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE cr.Status
        END AS StatusDescription,
        -- Receipt type based on amount
        CASE 
            WHEN cr.Amount >= 100000 THEN 'Large Receipt'
            WHEN cr.Amount >= 50000 THEN 'Medium Receipt'
            WHEN cr.Amount >= 10000 THEN 'Small Receipt'
            ELSE 'Minor Receipt'
        END AS ReceiptSizeCategory,
        -- Days since receipt
        DATEDIFF(DAY, cr.ReceiptDate, @EndDate) AS DaysSinceReceipt,
        -- Receipt number formatted
        'RCP-' + RIGHT('000000' + CAST(cr.ReceiptId AS VARCHAR(6)), 6) AS FormattedReceiptNumber
    FROM CustomerReceipts cr
    INNER JOIN Customers c ON cr.CustomerId = c.CustomerId
    WHERE cr.ReceiptDate BETWEEN @StartDate AND @EndDate
        AND (@CustomerId IS NULL OR cr.CustomerId = @CustomerId)
        AND c.IsActive = 1
    ORDER BY cr.ReceiptDate DESC, cr.ReceiptId DESC;
    
    -- Return summary information
    SELECT 
        COUNT(*) AS TotalReceipts,
        SUM(cr.Amount) AS TotalReceiptAmount,
        AVG(cr.Amount) AS AverageReceiptAmount,
        MIN(cr.Amount) AS MinimumReceipt,
        MAX(cr.Amount) AS MaximumReceipt,
        -- Payment method breakdown
        SUM(CASE WHEN cr.PaymentMethod = 'CASH' THEN cr.Amount ELSE 0 END) AS TotalCashReceipts,
        SUM(CASE WHEN cr.PaymentMethod = 'BANK_TRANSFER' THEN cr.Amount ELSE 0 END) AS TotalBankTransferReceipts,
        SUM(CASE WHEN cr.PaymentMethod = 'CHEQUE' THEN cr.Amount ELSE 0 END) AS TotalChequeReceipts,
        SUM(CASE WHEN cr.PaymentMethod = 'CARD' THEN cr.Amount ELSE 0 END) AS TotalCardReceipts,
        SUM(CASE WHEN cr.PaymentMethod = 'EASYPAISA' THEN cr.Amount ELSE 0 END) AS TotalEasypaisaReceipts,
        SUM(CASE WHEN cr.PaymentMethod = 'JAZZCASH' THEN cr.Amount ELSE 0 END) AS TotalJazzCashReceipts,
        SUM(CASE WHEN cr.PaymentMethod = 'OTHER' THEN cr.Amount ELSE 0 END) AS TotalOtherReceipts,
        -- Status breakdown
        SUM(CASE WHEN cr.Status = 'COMPLETED' THEN cr.Amount ELSE 0 END) AS TotalCompletedReceipts,
        SUM(CASE WHEN cr.Status = 'PENDING' THEN cr.Amount ELSE 0 END) AS TotalPendingReceipts,
        SUM(CASE WHEN cr.Status = 'CANCELLED' THEN cr.Amount ELSE 0 END) AS TotalCancelledReceipts,
        -- Date range information
        MIN(cr.ReceiptDate) AS FirstReceiptDate,
        MAX(cr.ReceiptDate) AS LastReceiptDate,
        DATEDIFF(DAY, MIN(cr.ReceiptDate), MAX(cr.ReceiptDate)) AS ReceiptPeriodDays,
        -- Customer count
        COUNT(DISTINCT cr.CustomerId) AS TotalCustomers
    FROM CustomerReceipts cr
    INNER JOIN Customers c ON cr.CustomerId = c.CustomerId
    WHERE cr.ReceiptDate BETWEEN @StartDate AND @EndDate
        AND (@CustomerId IS NULL OR cr.CustomerId = @CustomerId)
        AND c.IsActive = 1;
END
