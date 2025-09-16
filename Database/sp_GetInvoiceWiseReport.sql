CREATE PROCEDURE [dbo].[sp_GetInvoiceWiseReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @InvoiceNumber NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve invoice-wise data
    SELECT
        si.SalesInvoiceId,
        si.InvoiceNumber,
        si.InvoiceDate,
        si.DueDate,
        si.SubTotal,
        si.TaxableAmount,
        si.TaxAmount,
        si.TaxPercentage,
        si.DiscountAmount,
        si.DiscountPercentage,
        si.TotalAmount,
        si.PaidAmount,
        si.BalanceAmount,
        si.PaymentMode,
        si.Status,
        si.Remarks,
        si.PrintStatus,
        si.PrintDate,
        si.TransactionTime,
        -- Customer Information
        c.CustomerId,
        c.CustomerCode,
        c.CustomerName,
        c.ContactPerson,
        c.Email AS CustomerEmail,
        c.Phone AS CustomerPhone,
        c.Address AS CustomerAddress,
        c.City AS CustomerCity,
        c.State AS CustomerState,
        c.PostalCode AS CustomerPostalCode,
        c.Country AS CustomerCountry,
        c.TaxNumber AS CustomerTaxNumber,
        c.GSTNumber AS CustomerGSTNumber,
        -- Salesman Information
        sm.SalesmanId,
        sm.SalesmanCode,
        sm.SalesmanName,
        sm.Email AS SalesmanEmail,
        sm.Phone AS SalesmanPhone,
        sm.Territory AS SalesmanTerritory,
        sm.CommissionRate,
        -- Invoice Analysis
        CASE 
            WHEN si.Status = 'PAID' THEN 'Fully Paid'
            WHEN si.Status = 'PENDING' AND si.BalanceAmount = 0 THEN 'Fully Paid'
            WHEN si.Status = 'PENDING' AND si.BalanceAmount > 0 THEN 'Partially Paid'
            WHEN si.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE 'Unknown Status'
        END AS PaymentStatus,
        CASE 
            WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN 'Overdue'
            WHEN si.BalanceAmount > 0 AND si.DueDate >= GETDATE() THEN 'Pending'
            WHEN si.BalanceAmount = 0 THEN 'Paid'
            ELSE 'Unknown'
        END AS PaymentCondition,
        DATEDIFF(day, si.InvoiceDate, GETDATE()) AS InvoiceAge,
        DATEDIFF(day, si.DueDate, GETDATE()) AS DaysOverdue,
        -- Line Items Summary
        (SELECT COUNT(*) FROM SalesInvoiceDetails WHERE SalesInvoiceId = si.SalesInvoiceId) AS LineItemsCount,
        (SELECT SUM(Quantity) FROM SalesInvoiceDetails WHERE SalesInvoiceId = si.SalesInvoiceId) AS TotalQuantity,
        (SELECT SUM(UnitPrice * Quantity) FROM SalesInvoiceDetails WHERE SalesInvoiceId = si.SalesInvoiceId) AS LineItemsSubTotal,
        (SELECT SUM(DiscountAmount) FROM SalesInvoiceDetails WHERE SalesInvoiceId = si.SalesInvoiceId) AS LineItemsDiscount,
        -- Tax Analysis
        CASE 
            WHEN si.TaxAmount > 0 THEN 'Taxable'
            ELSE 'Non-Taxable'
        END AS TaxStatus,
        CASE 
            WHEN si.DiscountAmount > 0 THEN 'Discounted'
            ELSE 'No Discount'
        END AS DiscountStatus,
        -- Payment Analysis
        CASE 
            WHEN si.PaymentMode = 'CASH' THEN 'Cash Payment'
            WHEN si.PaymentMode = 'CREDIT' THEN 'Credit Payment'
            WHEN si.PaymentMode = 'CHEQUE' THEN 'Cheque Payment'
            WHEN si.PaymentMode = 'CARD' THEN 'Card Payment'
            WHEN si.PaymentMode = 'ONLINE' THEN 'Online Payment'
            ELSE 'Other Payment'
        END AS PaymentModeDescription,
        -- Invoice Value Category
        CASE 
            WHEN si.TotalAmount >= 50000 THEN 'High Value Invoice'
            WHEN si.TotalAmount >= 20000 THEN 'Medium Value Invoice'
            WHEN si.TotalAmount >= 5000 THEN 'Low Value Invoice'
            ELSE 'Very Low Value Invoice'
        END AS InvoiceValueCategory,
        -- Urgency Level
        CASE 
            WHEN si.BalanceAmount > 0 AND si.DueDate < DATEADD(day, -30, GETDATE()) THEN 'Critical Overdue'
            WHEN si.BalanceAmount > 0 AND si.DueDate < DATEADD(day, -15, GETDATE()) THEN 'High Priority'
            WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN 'Overdue'
            WHEN si.BalanceAmount > 0 AND si.DueDate >= GETDATE() THEN 'Normal'
            ELSE 'Paid'
        END AS UrgencyLevel,
        -- Commission Calculation
        si.TotalAmount * ISNULL(sm.CommissionRate, 0) / 100 AS CommissionAmount,
        -- Print Status
        CASE 
            WHEN si.PrintStatus = 1 THEN 'Printed'
            ELSE 'Not Printed'
        END AS PrintStatusDescription
    FROM SalesInvoices si
    LEFT JOIN Customers c ON si.CustomerId = c.CustomerId
    LEFT JOIN Salesman sm ON si.SalesmanId = sm.SalesmanId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@InvoiceNumber IS NULL OR si.InvoiceNumber LIKE '%' + @InvoiceNumber + '%')
    ORDER BY si.InvoiceDate DESC, si.TotalAmount DESC;

    -- Summary Information
    SELECT
        COUNT(*) AS TotalInvoices,
        COUNT(DISTINCT si.CustomerId) AS TotalCustomers,
        COUNT(DISTINCT si.SalesmanId) AS TotalSalesmen,
        SUM(si.SubTotal) AS TotalSubTotal,
        SUM(si.TaxAmount) AS TotalTaxAmount,
        SUM(si.DiscountAmount) AS TotalDiscountAmount,
        SUM(si.TotalAmount) AS TotalInvoiceAmount,
        SUM(si.PaidAmount) AS TotalPaidAmount,
        SUM(si.BalanceAmount) AS TotalBalanceAmount,
        -- Payment status summary
        COUNT(CASE WHEN si.Status = 'PAID' OR (si.Status = 'PENDING' AND si.BalanceAmount = 0) THEN 1 END) AS PaidInvoicesCount,
        COUNT(CASE WHEN si.Status = 'PENDING' AND si.BalanceAmount > 0 THEN 1 END) AS PendingInvoicesCount,
        COUNT(CASE WHEN si.Status = 'CANCELLED' THEN 1 END) AS CancelledInvoicesCount,
        SUM(CASE WHEN si.Status = 'PAID' OR (si.Status = 'PENDING' AND si.BalanceAmount = 0) THEN si.TotalAmount ELSE 0 END) AS PaidAmount,
        SUM(CASE WHEN si.Status = 'PENDING' AND si.BalanceAmount > 0 THEN si.TotalAmount ELSE 0 END) AS PendingAmount,
        SUM(CASE WHEN si.Status = 'CANCELLED' THEN si.TotalAmount ELSE 0 END) AS CancelledAmount,
        -- Overdue summary
        COUNT(CASE WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN 1 END) AS OverdueInvoicesCount,
        SUM(CASE WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN si.BalanceAmount ELSE 0 END) AS OverdueAmount,
        -- Invoice value analysis
        COUNT(CASE WHEN si.TotalAmount >= 50000 THEN 1 END) AS HighValueInvoicesCount,
        COUNT(CASE WHEN si.TotalAmount >= 20000 AND si.TotalAmount < 50000 THEN 1 END) AS MediumValueInvoicesCount,
        COUNT(CASE WHEN si.TotalAmount >= 5000 AND si.TotalAmount < 20000 THEN 1 END) AS LowValueInvoicesCount,
        COUNT(CASE WHEN si.TotalAmount < 5000 THEN 1 END) AS VeryLowValueInvoicesCount,
        -- Payment mode analysis
        COUNT(CASE WHEN si.PaymentMode = 'CASH' THEN 1 END) AS CashInvoicesCount,
        COUNT(CASE WHEN si.PaymentMode = 'CREDIT' THEN 1 END) AS CreditInvoicesCount,
        COUNT(CASE WHEN si.PaymentMode = 'CHEQUE' THEN 1 END) AS ChequeInvoicesCount,
        COUNT(CASE WHEN si.PaymentMode = 'CARD' THEN 1 END) AS CardInvoicesCount,
        COUNT(CASE WHEN si.PaymentMode = 'ONLINE' THEN 1 END) AS OnlineInvoicesCount,
        -- Tax analysis
        COUNT(CASE WHEN si.TaxAmount > 0 THEN 1 END) AS TaxableInvoicesCount,
        COUNT(CASE WHEN si.TaxAmount = 0 THEN 1 END) AS NonTaxableInvoicesCount,
        SUM(CASE WHEN si.TaxAmount > 0 THEN si.TaxAmount ELSE 0 END) AS TotalTaxCollected,
        -- Discount analysis
        COUNT(CASE WHEN si.DiscountAmount > 0 THEN 1 END) AS DiscountedInvoicesCount,
        COUNT(CASE WHEN si.DiscountAmount = 0 THEN 1 END) AS NonDiscountedInvoicesCount,
        SUM(si.DiscountAmount) AS TotalDiscountGiven,
        -- Print status
        COUNT(CASE WHEN si.PrintStatus = 1 THEN 1 END) AS PrintedInvoicesCount,
        COUNT(CASE WHEN si.PrintStatus = 0 OR si.PrintStatus IS NULL THEN 1 END) AS UnprintedInvoicesCount,
        -- Top performers
        (SELECT TOP 1 si2.InvoiceNumber FROM SalesInvoices si2 WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@InvoiceNumber IS NULL OR si2.InvoiceNumber LIKE '%' + @InvoiceNumber + '%') ORDER BY si2.TotalAmount DESC) AS HighestValueInvoice,
        (SELECT TOP 1 c2.CustomerName FROM SalesInvoices si2 INNER JOIN Customers c2 ON si2.CustomerId = c2.CustomerId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@InvoiceNumber IS NULL OR si2.InvoiceNumber LIKE '%' + @InvoiceNumber + '%') GROUP BY c2.CustomerId, c2.CustomerName ORDER BY COUNT(*) DESC) AS MostFrequentCustomer,
        -- Average metrics
        AVG(si.TotalAmount) AS AverageInvoiceAmount,
        AVG(DATEDIFF(day, si.InvoiceDate, GETDATE())) AS AverageInvoiceAge,
        AVG(CASE WHEN si.BalanceAmount > 0 THEN DATEDIFF(day, si.DueDate, GETDATE()) ELSE 0 END) AS AverageDaysOverdue
    FROM SalesInvoices si
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@InvoiceNumber IS NULL OR si.InvoiceNumber LIKE '%' + @InvoiceNumber + '%');
END;
