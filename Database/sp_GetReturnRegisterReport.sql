CREATE PROCEDURE [dbo].[sp_GetReturnRegisterReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @InvoiceNumber VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Combined Sales and Purchase Returns Register using UNION
    SELECT
        'Sales' AS ReturnType,
        sr.ReturnId,
        sr.ReturnNumber,
        sr.ReturnBarcode,
        sr.ReturnDate,
        sr.CustomerId,
        c.CustomerName,
        c.CustomerCode,
        c.Phone AS CustomerPhone,
        c.Address AS CustomerAddress,
        sr.ReferenceSalesInvoiceId,
        si.InvoiceNumber AS ReferenceInvoiceNumber,
        si.InvoiceDate AS ReferenceInvoiceDate,
        sr.Reason,
        sr.SubTotal,
        sr.TaxAmount,
        sr.DiscountAmount,
        sr.TotalAmount,
        sr.Status,
        sr.Remarks,
        sr.CreatedBy,
        sr.CreatedDate,
        sr.ModifiedBy,
        sr.ModifiedDate,
        sr.ProcessedDate,
        sr.ProcessedBy,
        sr.ApprovedDate,
        sr.ApprovedBy,
        sr.TaxCategoryId,
        -- Calculated fields
        CASE
            WHEN sr.Status = 'PENDING' THEN 'Pending Approval'
            WHEN sr.Status = 'APPROVED' THEN 'Approved'
            WHEN sr.Status = 'PROCESSED' THEN 'Processed'
            WHEN sr.Status = 'REJECTED' THEN 'Rejected'
            ELSE sr.Status
        END AS StatusDescription,
        DATEDIFF(DAY, sr.ReturnDate, GETDATE()) AS DaysSinceReturn,
        DATEDIFF(DAY, sr.ReturnDate, sr.ApprovedDate) AS DaysToApproval,
        CASE
            WHEN sr.Status = 'APPROVED' AND sr.ApprovedDate IS NOT NULL AND sr.ApprovedDate < GETDATE() THEN 'Approved'
            WHEN sr.Status = 'PENDING' AND sr.ReturnDate < GETDATE() THEN 'Overdue Approval'
            ELSE 'Current'
        END AS ApprovalStatus,
        CASE
            WHEN sr.TotalAmount >= 10000 THEN 'High Value'
            WHEN sr.TotalAmount >= 5000 THEN 'Medium Value'
            WHEN sr.TotalAmount >= 1000 THEN 'Low Value'
            ELSE 'Very Low Value'
        END AS AmountCategory,
        YEAR(sr.ReturnDate) AS ReturnYear,
        MONTH(sr.ReturnDate) AS ReturnMonth,
        DATENAME(month, sr.ReturnDate) AS ReturnMonthName,
        -- Additional register fields
        CASE
            WHEN sr.Status = 'PROCESSED' THEN 'Completed'
            WHEN sr.Status = 'APPROVED' THEN 'In Progress'
            WHEN sr.Status = 'PENDING' THEN 'Awaiting Approval'
            WHEN sr.Status = 'REJECTED' THEN 'Cancelled'
            ELSE 'Unknown'
        END AS ProcessStatus,
        CASE
            WHEN sr.ProcessedDate IS NOT NULL THEN DATEDIFF(DAY, sr.ReturnDate, sr.ProcessedDate)
            ELSE NULL
        END AS ProcessingDays,
        CASE
            WHEN sr.ApprovedDate IS NOT NULL THEN DATEDIFF(DAY, sr.ReturnDate, sr.ApprovedDate)
            ELSE NULL
        END AS ApprovalDays
    FROM SalesReturns sr
    LEFT JOIN Customers c ON sr.CustomerId = c.CustomerId
    LEFT JOIN SalesInvoices si ON sr.ReferenceSalesInvoiceId = si.SalesInvoiceId
    WHERE sr.ReturnDate >= @StartDate
        AND sr.ReturnDate <= @EndDate
        AND (@InvoiceNumber IS NULL OR si.InvoiceNumber LIKE '%' + @InvoiceNumber + '%')

    UNION ALL

    SELECT
        'Purchase' AS ReturnType,
        pr.ReturnId,
        pr.ReturnNo AS ReturnNumber,
        pr.ReturnBarcode,
        pr.ReturnDate,
        pr.SupplierId AS CustomerId, -- Using CustomerId for consistency
        s.SupplierName AS CustomerName,
        s.SupplierCode AS CustomerCode,
        s.Phone AS CustomerPhone,
        s.Address AS CustomerAddress,
        pr.ReferencePurchaseId AS ReferenceSalesInvoiceId, -- Using ReferenceSalesInvoiceId for consistency
        NULL AS ReferenceInvoiceNumber, -- Purchase returns don't have invoice numbers
        NULL AS ReferenceInvoiceDate,
        pr.Reason,
        pr.NetReturnAmount AS SubTotal,
        0 AS TaxAmount, -- Purchase returns don't have separate tax
        0 AS DiscountAmount, -- Purchase returns don't have separate discount
        pr.NetReturnAmount AS TotalAmount,
        'PROCESSED' AS Status, -- Purchase returns are typically processed
        pr.Reason AS Remarks,
        pr.CreatedBy,
        pr.CreatedDate,
        pr.ModifiedBy,
        pr.ModifiedDate,
        pr.CreatedDate AS ProcessedDate, -- Using CreatedDate as ProcessedDate
        pr.CreatedBy AS ProcessedBy, -- Using CreatedBy as ProcessedBy
        pr.CreatedDate AS ApprovedDate, -- Using CreatedDate as ApprovedDate
        pr.CreatedBy AS ApprovedBy, -- Using CreatedBy as ApprovedBy
        NULL AS TaxCategoryId,
        -- Calculated fields
        'Processed' AS StatusDescription,
        DATEDIFF(DAY, pr.ReturnDate, GETDATE()) AS DaysSinceReturn,
        0 AS DaysToApproval, -- Purchase returns don't have approval process
        'Current' AS ApprovalStatus,
        CASE
            WHEN pr.NetReturnAmount >= 10000 THEN 'High Value'
            WHEN pr.NetReturnAmount >= 5000 THEN 'Medium Value'
            WHEN pr.NetReturnAmount >= 1000 THEN 'Low Value'
            ELSE 'Very Low Value'
        END AS AmountCategory,
        YEAR(pr.ReturnDate) AS ReturnYear,
        MONTH(pr.ReturnDate) AS ReturnMonth,
        DATENAME(month, pr.ReturnDate) AS ReturnMonthName,
        -- Additional register fields
        'Completed' AS ProcessStatus,
        DATEDIFF(DAY, pr.ReturnDate, pr.CreatedDate) AS ProcessingDays,
        0 AS ApprovalDays
    FROM PurchaseReturns pr
    LEFT JOIN Suppliers s ON pr.SupplierId = s.SupplierId
    WHERE pr.ReturnDate >= @StartDate
        AND pr.ReturnDate <= @EndDate
        AND (@InvoiceNumber IS NULL OR pr.ReferencePurchaseId IS NOT NULL) -- Purchase returns don't have invoice numbers

    ORDER BY ReturnDate DESC, ReturnType, ReturnNumber;

    -- Overall Summary Statistics
    SELECT
        COUNT(*) AS TotalReturns,
        COUNT(DISTINCT CASE WHEN ReturnType = 'Sales' THEN CustomerId ELSE NULL END) AS TotalCustomers,
        COUNT(DISTINCT CASE WHEN ReturnType = 'Purchase' THEN CustomerId ELSE NULL END) AS TotalSuppliers,
        SUM(CASE WHEN ReturnType = 'Sales' THEN TotalAmount ELSE 0 END) AS TotalSalesReturnAmount,
        SUM(CASE WHEN ReturnType = 'Purchase' THEN TotalAmount ELSE 0 END) AS TotalPurchaseReturnAmount,
        SUM(TotalAmount) AS TotalSummaryAmount,
        AVG(TotalAmount) AS AverageAmount,
        MIN(TotalAmount) AS MinAmount,
        MAX(TotalAmount) AS MaxAmount,
        
        SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'PENDING' THEN 1 ELSE 0 END) AS PendingReturns,
        SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'APPROVED' THEN 1 ELSE 0 END) AS ApprovedReturns,
        SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'PROCESSED' THEN 1 ELSE 0 END) AS ProcessedReturns,
        SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'REJECTED' THEN 1 ELSE 0 END) AS RejectedReturns,
        SUM(CASE WHEN ReturnType = 'Purchase' THEN 1 ELSE 0 END) AS PurchaseReturns,
        
        SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'PENDING' THEN TotalAmount ELSE 0 END) AS PendingAmount,
        SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'APPROVED' THEN TotalAmount ELSE 0 END) AS ApprovedAmount,
        SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'PROCESSED' THEN TotalAmount ELSE 0 END) AS ProcessedAmount,
        SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'REJECTED' THEN TotalAmount ELSE 0 END) AS RejectedAmount,
        SUM(CASE WHEN ReturnType = 'Purchase' THEN TotalAmount ELSE 0 END) AS PurchaseReturnAmount,
        
        SUM(CASE WHEN ReturnType = 'Sales' AND TotalAmount >= 10000 THEN 1 ELSE 0 END) AS HighValueReturns,
        SUM(CASE WHEN ReturnType = 'Sales' AND TotalAmount >= 5000 AND TotalAmount < 10000 THEN 1 ELSE 0 END) AS MediumValueReturns,
        SUM(CASE WHEN ReturnType = 'Sales' AND TotalAmount >= 1000 AND TotalAmount < 5000 THEN 1 ELSE 0 END) AS LowValueReturns,
        SUM(CASE WHEN ReturnType = 'Sales' AND TotalAmount < 1000 THEN 1 ELSE 0 END) AS VeryLowValueReturns,
        SUM(CASE WHEN ReturnType = 'Purchase' AND TotalAmount >= 10000 THEN 1 ELSE 0 END) AS HighValuePurchaseReturns,
        SUM(CASE WHEN ReturnType = 'Purchase' AND TotalAmount >= 5000 AND TotalAmount < 10000 THEN 1 ELSE 0 END) AS MediumValuePurchaseReturns,
        SUM(CASE WHEN ReturnType = 'Purchase' AND TotalAmount >= 1000 AND TotalAmount < 5000 THEN 1 ELSE 0 END) AS LowValuePurchaseReturns,
        SUM(CASE WHEN ReturnType = 'Purchase' AND TotalAmount < 1000 THEN 1 ELSE 0 END) AS VeryLowValuePurchaseReturns,
        
        SUM(CASE WHEN ReturnType = 'Sales' AND TotalAmount >= 10000 THEN TotalAmount ELSE 0 END) AS TotalHighValueAmount,
        SUM(CASE WHEN ReturnType = 'Sales' AND TotalAmount >= 5000 AND TotalAmount < 10000 THEN TotalAmount ELSE 0 END) AS TotalMediumValueAmount,
        SUM(CASE WHEN ReturnType = 'Sales' AND TotalAmount >= 1000 AND TotalAmount < 5000 THEN TotalAmount ELSE 0 END) AS TotalLowValueAmount,
        SUM(CASE WHEN ReturnType = 'Sales' AND TotalAmount < 1000 THEN TotalAmount ELSE 0 END) AS TotalVeryLowValueAmount,
        SUM(CASE WHEN ReturnType = 'Purchase' AND TotalAmount >= 10000 THEN TotalAmount ELSE 0 END) AS TotalHighValuePurchaseAmount,
        SUM(CASE WHEN ReturnType = 'Purchase' AND TotalAmount >= 5000 AND TotalAmount < 10000 THEN TotalAmount ELSE 0 END) AS TotalMediumValuePurchaseAmount,
        SUM(CASE WHEN ReturnType = 'Purchase' AND TotalAmount >= 1000 AND TotalAmount < 5000 THEN TotalAmount ELSE 0 END) AS TotalLowValuePurchaseAmount,
        SUM(CASE WHEN ReturnType = 'Purchase' AND TotalAmount < 1000 THEN TotalAmount ELSE 0 END) AS TotalVeryLowValuePurchaseAmount,
        
        -- Date range info
        @StartDate AS ReportStartDate,
        @EndDate AS ReportEndDate,
        DATEDIFF(DAY, @StartDate, @EndDate) + 1 AS ReportDays,
        
        -- Calculated fields
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'PENDING' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
            ELSE 0 
        END AS PendingReturnPercentage,
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'APPROVED' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
            ELSE 0 
        END AS ApprovedReturnPercentage,
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'PROCESSED' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
            ELSE 0 
        END AS ProcessedReturnPercentage,
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN ReturnType = 'Sales' AND Status = 'REJECTED' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
            ELSE 0 
        END AS RejectedReturnPercentage
    FROM (
        -- Sales Returns
        SELECT 'Sales' AS ReturnType, sr.CustomerId, sr.TotalAmount, sr.Status, sr.ReturnDate
        FROM SalesReturns sr
        LEFT JOIN SalesInvoices si ON sr.ReferenceSalesInvoiceId = si.SalesInvoiceId
        WHERE sr.ReturnDate >= @StartDate
            AND sr.ReturnDate <= @EndDate
            AND (@InvoiceNumber IS NULL OR si.InvoiceNumber LIKE '%' + @InvoiceNumber + '%')
        
        UNION ALL
        
        -- Purchase Returns
        SELECT 'Purchase' AS ReturnType, pr.SupplierId AS CustomerId, pr.NetReturnAmount AS TotalAmount, 'PROCESSED' AS Status, pr.ReturnDate
        FROM PurchaseReturns pr
        WHERE pr.ReturnDate >= @StartDate
            AND pr.ReturnDate <= @EndDate
            AND (@InvoiceNumber IS NULL OR pr.ReferencePurchaseId IS NOT NULL)
    ) AS CombinedReturns;
END;
