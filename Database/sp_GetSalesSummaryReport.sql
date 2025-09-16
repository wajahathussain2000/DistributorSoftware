CREATE PROCEDURE [dbo].[sp_GetSalesSummaryReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @ProductId INT = NULL,
    @CustomerId INT = NULL,
    @SalesmanId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve sales summary data
    SELECT
        sid.DetailId,
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
        sid.ProductId,
        p.ProductCode,
        p.ProductName,
        p.Description AS ProductDescription,
        p.Category AS ProductCategory,
        p.BrandId,
        b.BrandName,
        p.CategoryId,
        pc.CategoryName,
        p.UnitId,
        u.UnitName,
        p.Barcode AS ProductBarcode,
        sid.Quantity,
        sid.UnitPrice,
        sid.TaxPercentage,
        sid.TaxAmount,
        sid.DiscountPercentage,
        sid.DiscountAmount,
        sid.TaxableAmount,
        sid.TotalAmount,
        sid.LineTotal,
        sid.BatchNumber,
        sid.ExpiryDate,
        sid.Remarks AS LineRemarks,
        si.SubTotal AS InvoiceSubTotal,
        si.DiscountAmount AS InvoiceDiscountAmount,
        si.TaxAmount AS InvoiceTaxAmount,
        si.TotalAmount AS InvoiceTotalAmount,
        si.PaidAmount,
        si.BalanceAmount,
        si.PaymentMode,
        si.Status AS InvoiceStatus,
        si.Remarks AS InvoiceRemarks,
        si.CreatedDate AS InvoiceCreatedDate,
        si.CreatedBy AS InvoiceCreatedBy,
        u_created.FirstName + ' ' + u_created.LastName AS InvoiceCreatedByUser,
        si.ModifiedDate AS InvoiceModifiedDate,
        si.ModifiedBy AS InvoiceModifiedBy,
        u_modified.FirstName + ' ' + u_modified.LastName AS InvoiceModifiedByUser,
        si.ChangeAmount,
        si.Barcode AS InvoiceBarcode,
        si.PrintStatus,
        si.PrintDate,
        si.CashierId,
        u_cashier.FirstName + ' ' + u_cashier.LastName AS CashierName,
        si.TransactionTime,
        -- Calculated fields
        CASE
            WHEN si.Status = 'PAID' THEN 'Paid'
            WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN 'Overdue'
            WHEN si.BalanceAmount > 0 AND si.DueDate >= GETDATE() THEN 'Pending'
            ELSE 'Other'
        END AS PaymentStatus,
        CASE
            WHEN si.TotalAmount > 0 THEN (si.PaidAmount / si.TotalAmount) * 100
            ELSE 0
        END AS PaymentPercentage,
        DATEDIFF(day, si.InvoiceDate, GETDATE()) AS DaysSinceInvoice,
        -- Product performance metrics
        CASE
            WHEN sid.Quantity > 0 THEN sid.TotalAmount / sid.Quantity
            ELSE 0
        END AS AverageUnitPrice,
        CASE
            WHEN p.PurchasePrice > 0 THEN ((sid.UnitPrice - p.PurchasePrice) / p.PurchasePrice) * 100
            ELSE 0
        END AS ProfitMarginPercentage,
        CASE
            WHEN p.PurchasePrice > 0 THEN (sid.UnitPrice - p.PurchasePrice) * sid.Quantity
            ELSE 0
        END AS ProfitAmount,
        -- Sales performance indicators
        CASE
            WHEN sid.TotalAmount >= 1000 THEN 'High Value'
            WHEN sid.TotalAmount >= 500 THEN 'Medium Value'
            ELSE 'Low Value'
        END AS SalesValueCategory,
        CASE
            WHEN sid.Quantity >= 10 THEN 'Bulk Sale'
            WHEN sid.Quantity >= 5 THEN 'Medium Sale'
            ELSE 'Small Sale'
        END AS SalesQuantityCategory
    FROM SalesInvoiceDetails sid
    INNER JOIN SalesInvoices si ON sid.SalesInvoiceId = si.SalesInvoiceId
    INNER JOIN Customers c ON si.CustomerId = c.CustomerId
    LEFT JOIN Salesman sm ON si.SalesmanId = sm.SalesmanId
    LEFT JOIN Products p ON sid.ProductId = p.ProductId
    LEFT JOIN Brands b ON p.BrandId = b.BrandId
    LEFT JOIN ProductCategories pc ON p.CategoryId = pc.CategoryId
    LEFT JOIN Units u ON p.UnitId = u.UnitId
    LEFT JOIN Users u_created ON si.CreatedBy = u_created.UserId
    LEFT JOIN Users u_modified ON si.ModifiedBy = u_modified.UserId
    LEFT JOIN Users u_cashier ON si.CashierId = u_cashier.UserId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@ProductId IS NULL OR sid.ProductId = @ProductId)
      AND (@CustomerId IS NULL OR si.CustomerId = @CustomerId)
      AND (@SalesmanId IS NULL OR si.SalesmanId = @SalesmanId)
    ORDER BY si.InvoiceDate DESC, si.InvoiceNumber DESC, sid.DetailId;

    -- Summary Information
    SELECT
        COUNT(sid.DetailId) AS TotalLineItems,
        COUNT(DISTINCT si.SalesInvoiceId) AS TotalInvoices,
        COUNT(DISTINCT si.CustomerId) AS TotalCustomers,
        COUNT(DISTINCT si.SalesmanId) AS TotalSalesmen,
        COUNT(DISTINCT sid.ProductId) AS TotalProducts,
        SUM(sid.Quantity) AS TotalQuantitySold,
        SUM(sid.TaxableAmount) AS TotalSubTotal,
        SUM(sid.DiscountAmount) AS TotalDiscountAmount,
        SUM(sid.TaxAmount) AS TotalTaxAmount,
        SUM(sid.TotalAmount) AS TotalSalesAmount,
        SUM(si.PaidAmount) AS TotalPaidAmount,
        SUM(si.BalanceAmount) AS TotalBalanceAmount,
        SUM(CASE WHEN si.Status = 'PAID' THEN si.TotalAmount ELSE 0 END) AS TotalPaidSales,
        SUM(CASE WHEN si.Status = 'PENDING' THEN si.TotalAmount ELSE 0 END) AS TotalPendingSales,
        SUM(CASE WHEN si.Status = 'CANCELLED' THEN si.TotalAmount ELSE 0 END) AS TotalCancelledSales,
        SUM(CASE WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN si.BalanceAmount ELSE 0 END) AS TotalOverdueAmount,
        -- Product performance summary
        SUM(CASE WHEN p.PurchasePrice > 0 THEN (sid.UnitPrice - p.PurchasePrice) * sid.Quantity ELSE 0 END) AS TotalProfitAmount,
        AVG(CASE WHEN p.PurchasePrice > 0 THEN ((sid.UnitPrice - p.PurchasePrice) / p.PurchasePrice) * 100 ELSE 0 END) AS AverageProfitMarginPercentage,
        -- Sales value analysis
        COUNT(CASE WHEN sid.TotalAmount >= 1000 THEN 1 ELSE NULL END) AS HighValueSalesCount,
        COUNT(CASE WHEN sid.TotalAmount >= 500 AND sid.TotalAmount < 1000 THEN 1 ELSE NULL END) AS MediumValueSalesCount,
        COUNT(CASE WHEN sid.TotalAmount < 500 THEN 1 ELSE NULL END) AS LowValueSalesCount,
        -- Quantity analysis
        COUNT(CASE WHEN sid.Quantity >= 10 THEN 1 ELSE NULL END) AS BulkSalesCount,
        COUNT(CASE WHEN sid.Quantity >= 5 AND sid.Quantity < 10 THEN 1 ELSE NULL END) AS MediumSalesCount,
        COUNT(CASE WHEN sid.Quantity < 5 THEN 1 ELSE NULL END) AS SmallSalesCount,
        -- Top performers
        (SELECT TOP 1 c2.CustomerName FROM SalesInvoices si2 INNER JOIN Customers c2 ON si2.CustomerId = c2.CustomerId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@ProductId IS NULL OR EXISTS (SELECT 1 FROM SalesInvoiceDetails sid2 WHERE sid2.SalesInvoiceId = si2.SalesInvoiceId AND sid2.ProductId = @ProductId)) AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId) AND (@SalesmanId IS NULL OR si2.SalesmanId = @SalesmanId) GROUP BY c2.CustomerId, c2.CustomerName ORDER BY SUM(si2.TotalAmount) DESC) AS TopCustomer,
        (SELECT TOP 1 sm2.SalesmanName FROM SalesInvoices si2 INNER JOIN Salesman sm2 ON si2.SalesmanId = sm2.SalesmanId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@ProductId IS NULL OR EXISTS (SELECT 1 FROM SalesInvoiceDetails sid2 WHERE sid2.SalesInvoiceId = si2.SalesInvoiceId AND sid2.ProductId = @ProductId)) AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId) AND (@SalesmanId IS NULL OR si2.SalesmanId = @SalesmanId) GROUP BY sm2.SalesmanId, sm2.SalesmanName ORDER BY SUM(si2.TotalAmount) DESC) AS TopSalesman,
        (SELECT TOP 1 p2.ProductName FROM SalesInvoiceDetails sid2 INNER JOIN Products p2 ON sid2.ProductId = p2.ProductId INNER JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@ProductId IS NULL OR sid2.ProductId = @ProductId) AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId) AND (@SalesmanId IS NULL OR si2.SalesmanId = @SalesmanId) GROUP BY p2.ProductId, p2.ProductName ORDER BY SUM(sid2.TotalAmount) DESC) AS TopProduct
    FROM SalesInvoiceDetails sid
    INNER JOIN SalesInvoices si ON sid.SalesInvoiceId = si.SalesInvoiceId
    LEFT JOIN Products p ON sid.ProductId = p.ProductId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@ProductId IS NULL OR sid.ProductId = @ProductId)
      AND (@CustomerId IS NULL OR si.CustomerId = @CustomerId)
      AND (@SalesmanId IS NULL OR si.SalesmanId = @SalesmanId);
END;
