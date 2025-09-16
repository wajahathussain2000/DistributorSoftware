CREATE PROCEDURE [dbo].[sp_GetSalesReturnReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @CustomerId INT = NULL,
    @SalesmanId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve sales return data
    SELECT
        sr.ReturnId,
        sr.ReturnNumber,
        sr.ReturnBarcode,
        sr.CustomerId,
        c.CustomerCode,
        c.CustomerName,
        c.Phone AS CustomerPhone,
        c.Address AS CustomerAddress,
        sr.ReferenceSalesInvoiceId,
        si.InvoiceNumber AS ReferenceInvoiceNumber,
        si.InvoiceDate AS ReferenceInvoiceDate,
        si.SalesmanId,
        sm.SalesmanCode,
        sm.SalesmanName,
        sm.Phone AS SalesmanPhone,
        sm.Territory,
        sr.ReturnDate,
        sr.Reason,
        sr.SubTotal,
        sr.DiscountAmount,
        sr.TaxAmount,
        sr.TotalAmount,
        sr.Status,
        sr.Remarks,
        sr.CreatedDate,
        sr.CreatedBy,
        u.FirstName + ' ' + u.LastName AS CreatedByUser,
        sr.ModifiedDate,
        sr.ModifiedBy,
        um.FirstName + ' ' + um.LastName AS ModifiedByUser,
        sr.ProcessedDate,
        sr.ProcessedBy,
        up.FirstName + ' ' + up.LastName AS ProcessedByUser,
        sr.ApprovedDate,
        sr.ApprovedBy,
        ua.FirstName + ' ' + ua.LastName AS ApprovedByUser,
        sr.TaxCategoryId,
        tc.TaxCategoryName,
        -- Calculated fields
        CASE
            WHEN sr.Status = 'PROCESSED' THEN 'Processed'
            WHEN sr.Status = 'PENDING' THEN 'Pending'
            WHEN sr.Status = 'APPROVED' THEN 'Approved'
            WHEN sr.Status = 'REJECTED' THEN 'Rejected'
            ELSE 'Unknown'
        END AS StatusDescription,
        CASE
            WHEN sr.TotalAmount > 0 THEN (sr.DiscountAmount / sr.TotalAmount) * 100
            ELSE 0
        END AS DiscountPercentage,
        CASE
            WHEN sr.TotalAmount > 0 THEN (sr.TaxAmount / sr.TotalAmount) * 100
            ELSE 0
        END AS TaxPercentage,
        DATEDIFF(day, sr.ReturnDate, GETDATE()) AS DaysSinceReturn,
        DATEDIFF(day, sr.CreatedDate, sr.ProcessedDate) AS ProcessingDays,
        -- Return items summary
        (SELECT COUNT(*) FROM SalesReturnItems sri WHERE sri.ReturnId = sr.ReturnId) AS TotalItems,
        (SELECT SUM(sri.Quantity) FROM SalesReturnItems sri WHERE sri.ReturnId = sr.ReturnId) AS TotalQuantity,
        (SELECT SUM(sri.TotalAmount) FROM SalesReturnItems sri WHERE sri.ReturnId = sr.ReturnId) AS ItemsTotalAmount
    FROM SalesReturns sr
    INNER JOIN Customers c ON sr.CustomerId = c.CustomerId
    LEFT JOIN SalesInvoices si ON sr.ReferenceSalesInvoiceId = si.SalesInvoiceId
    LEFT JOIN Salesman sm ON si.SalesmanId = sm.SalesmanId
    LEFT JOIN Users u ON sr.CreatedBy = u.UserId
    LEFT JOIN Users um ON sr.ModifiedBy = um.UserId
    LEFT JOIN Users up ON sr.ProcessedBy = up.UserId
    LEFT JOIN Users ua ON sr.ApprovedBy = ua.UserId
    LEFT JOIN TaxCategories tc ON sr.TaxCategoryId = tc.TaxCategoryId
    WHERE sr.ReturnDate >= @StartDate
      AND sr.ReturnDate <= @EndDate
      AND (@CustomerId IS NULL OR sr.CustomerId = @CustomerId)
      AND (@SalesmanId IS NULL OR si.SalesmanId = @SalesmanId)
    ORDER BY sr.ReturnDate DESC, sr.ReturnNumber DESC;

    -- Summary Information
    SELECT
        COUNT(sr.ReturnId) AS TotalReturns,
        COUNT(DISTINCT sr.CustomerId) AS TotalCustomers,
        COUNT(DISTINCT si.SalesmanId) AS TotalSalesmen,
        SUM(sr.SubTotal) AS TotalSubTotal,
        SUM(sr.DiscountAmount) AS TotalDiscountAmount,
        SUM(sr.TaxAmount) AS TotalTaxAmount,
        SUM(sr.TotalAmount) AS TotalReturnAmount,
        SUM(CASE WHEN sr.Status = 'PROCESSED' THEN sr.TotalAmount ELSE 0 END) AS TotalProcessedReturns,
        SUM(CASE WHEN sr.Status = 'PENDING' THEN sr.TotalAmount ELSE 0 END) AS TotalPendingReturns,
        SUM(CASE WHEN sr.Status = 'APPROVED' THEN sr.TotalAmount ELSE 0 END) AS TotalApprovedReturns,
        SUM(CASE WHEN sr.Status = 'REJECTED' THEN sr.TotalAmount ELSE 0 END) AS TotalRejectedReturns,
        COUNT(CASE WHEN sr.Status = 'PROCESSED' THEN 1 ELSE NULL END) AS ProcessedReturnsCount,
        COUNT(CASE WHEN sr.Status = 'PENDING' THEN 1 ELSE NULL END) AS PendingReturnsCount,
        COUNT(CASE WHEN sr.Status = 'APPROVED' THEN 1 ELSE NULL END) AS ApprovedReturnsCount,
        COUNT(CASE WHEN sr.Status = 'REJECTED' THEN 1 ELSE NULL END) AS RejectedReturnsCount,
        ISNULL(AVG(CAST(DATEDIFF(day, sr.CreatedDate, sr.ProcessedDate) AS DECIMAL(18,2))), 0) AS AverageProcessingDays,
        (SELECT COUNT(*) FROM SalesReturnItems sri INNER JOIN SalesReturns sr2 ON sri.ReturnId = sr2.ReturnId WHERE sr2.ReturnDate >= @StartDate AND sr2.ReturnDate <= @EndDate AND (@CustomerId IS NULL OR sr2.CustomerId = @CustomerId) AND (@SalesmanId IS NULL OR EXISTS (SELECT 1 FROM SalesInvoices si2 WHERE si2.SalesInvoiceId = sr2.ReferenceSalesInvoiceId AND si2.SalesmanId = @SalesmanId))) AS TotalReturnItems,
        (SELECT SUM(sri.Quantity) FROM SalesReturnItems sri INNER JOIN SalesReturns sr2 ON sri.ReturnId = sr2.ReturnId WHERE sr2.ReturnDate >= @StartDate AND sr2.ReturnDate <= @EndDate AND (@CustomerId IS NULL OR sr2.CustomerId = @CustomerId) AND (@SalesmanId IS NULL OR EXISTS (SELECT 1 FROM SalesInvoices si2 WHERE si2.SalesInvoiceId = sr2.ReferenceSalesInvoiceId AND si2.SalesmanId = @SalesmanId))) AS TotalReturnQuantity
    FROM SalesReturns sr
    INNER JOIN Customers c ON sr.CustomerId = c.CustomerId
    LEFT JOIN SalesInvoices si ON sr.ReferenceSalesInvoiceId = si.SalesInvoiceId
    WHERE sr.ReturnDate >= @StartDate
      AND sr.ReturnDate <= @EndDate
      AND (@CustomerId IS NULL OR sr.CustomerId = @CustomerId)
      AND (@SalesmanId IS NULL OR si.SalesmanId = @SalesmanId);
END;
