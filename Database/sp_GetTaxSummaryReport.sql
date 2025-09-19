CREATE PROCEDURE [dbo].[sp_GetTaxSummaryReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @TaxType VARCHAR(50) = NULL -- 'Sales Tax', 'Purchase Tax', or NULL for all
AS
BEGIN
    SET NOCOUNT ON;

    -- Combined Sales and Purchase Tax Summary using UNION
    SELECT
        'Sales Tax' AS TaxType,
        si.SalesInvoiceId AS InvoiceId,
        si.InvoiceNumber,
        si.InvoiceDate,
        c.CustomerName,
        c.CustomerCode,
        si.TaxCategoryId,
        tc.TaxCategoryName,
        tc.TaxCategoryCode,
        si.TaxableAmount,
        si.TaxAmount,
        si.TaxPercentage,
        si.TotalAmount,
        si.SubTotal,
        si.DiscountAmount,
        si.Status,
        si.Remarks,
        si.CreatedBy,
        si.CreatedDate,
        si.ModifiedBy,
        si.ModifiedDate,
        si.TaxAccountId,
        -- Calculated fields
        CASE
            WHEN si.Status = 'PENDING' THEN 'Pending'
            WHEN si.Status = 'PAID' THEN 'Paid'
            WHEN si.Status = 'OVERDUE' THEN 'Overdue'
            WHEN si.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE si.Status
        END AS StatusDescription,
        CASE
            WHEN si.TaxAmount >= 10000 THEN 'High Tax'
            WHEN si.TaxAmount >= 5000 THEN 'Medium Tax'
            WHEN si.TaxAmount >= 1000 THEN 'Low Tax'
            ELSE 'Very Low Tax'
        END AS TaxAmountCategory,
        YEAR(si.InvoiceDate) AS TaxYear,
        MONTH(si.InvoiceDate) AS TaxMonth,
        DATENAME(month, si.InvoiceDate) AS TaxMonthName,
        -- Additional tax fields
        CASE
            WHEN si.TaxPercentage > 0 THEN 'Taxable'
            ELSE 'Non-Taxable'
        END AS TaxableStatus,
        CASE
            WHEN si.TaxableAmount > 0 AND si.TaxAmount > 0 THEN 
                CAST((si.TaxAmount / si.TaxableAmount) * 100 AS DECIMAL(5,2))
            ELSE 0
        END AS EffectiveTaxRate,
        DATEDIFF(DAY, si.InvoiceDate, GETDATE()) AS DaysSinceInvoice,
        CASE
            WHEN si.DueDate IS NOT NULL THEN DATEDIFF(DAY, si.DueDate, GETDATE())
            ELSE NULL
        END AS DaysOverdue
    FROM SalesInvoices si
    LEFT JOIN Customers c ON si.CustomerId = c.CustomerId
    LEFT JOIN TaxCategories tc ON si.TaxCategoryId = tc.TaxCategoryId
    WHERE si.InvoiceDate >= @StartDate
        AND si.InvoiceDate <= @EndDate
        AND (@TaxType IS NULL OR @TaxType = 'Sales Tax')
        AND si.TaxAmount > 0 -- Only include invoices with tax

    UNION ALL

    SELECT
        'Purchase Tax' AS TaxType,
        pi.PurchaseInvoiceId AS InvoiceId,
        pi.InvoiceNumber,
        pi.InvoiceDate,
        s.SupplierName AS CustomerName,
        s.SupplierCode AS CustomerCode,
        NULL AS TaxCategoryId,
        'Standard Tax' AS TaxCategoryName,
        'STD' AS TaxCategoryCode,
        pi.SubTotal AS TaxableAmount,
        pi.TaxAmount,
        0 AS TaxPercentage,
        pi.TotalAmount,
        pi.SubTotal,
        pi.DiscountAmount,
        pi.Status,
        pi.Remarks,
        pi.CreatedBy,
        pi.CreatedDate,
        pi.ModifiedBy,
        pi.ModifiedDate,
        pi.TaxAccountId,
        -- Calculated fields
        CASE
            WHEN pi.Status = 'PENDING' THEN 'Pending'
            WHEN pi.Status = 'PAID' THEN 'Paid'
            WHEN pi.Status = 'OVERDUE' THEN 'Overdue'
            WHEN pi.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE pi.Status
        END AS StatusDescription,
        CASE
            WHEN pi.TaxAmount >= 10000 THEN 'High Tax'
            WHEN pi.TaxAmount >= 5000 THEN 'Medium Tax'
            WHEN pi.TaxAmount >= 1000 THEN 'Low Tax'
            ELSE 'Very Low Tax'
        END AS TaxAmountCategory,
        YEAR(pi.InvoiceDate) AS TaxYear,
        MONTH(pi.InvoiceDate) AS TaxMonth,
        DATENAME(month, pi.InvoiceDate) AS TaxMonthName,
        -- Additional tax fields
        CASE
            WHEN pi.TaxAmount > 0 THEN 'Taxable'
            ELSE 'Non-Taxable'
        END AS TaxableStatus,
        CASE
            WHEN pi.SubTotal > 0 AND pi.TaxAmount > 0 THEN 
                CAST((pi.TaxAmount / pi.SubTotal) * 100 AS DECIMAL(5,2))
            ELSE 0
        END AS EffectiveTaxRate,
        DATEDIFF(DAY, pi.InvoiceDate, GETDATE()) AS DaysSinceInvoice,
        CASE
            WHEN pi.DueDate IS NOT NULL THEN DATEDIFF(DAY, pi.DueDate, GETDATE())
            ELSE NULL
        END AS DaysOverdue
    FROM PurchaseInvoices pi
    LEFT JOIN Suppliers s ON pi.SupplierId = s.SupplierId
    WHERE pi.InvoiceDate >= @StartDate
        AND pi.InvoiceDate <= @EndDate
        AND (@TaxType IS NULL OR @TaxType = 'Purchase Tax')
        AND pi.TaxAmount > 0 -- Only include invoices with tax

    ORDER BY InvoiceDate DESC, TaxType, InvoiceNumber;

    -- Overall Tax Summary Statistics
    SELECT
        COUNT(*) AS TotalTaxInvoices,
        COUNT(DISTINCT CASE WHEN TaxType = 'Sales Tax' THEN InvoiceId ELSE NULL END) AS TotalCustomers,
        COUNT(DISTINCT CASE WHEN TaxType = 'Purchase Tax' THEN InvoiceId ELSE NULL END) AS TotalSuppliers,
        COUNT(DISTINCT TaxCategoryId) AS TotalTaxCategories,
        
        SUM(CASE WHEN TaxType = 'Sales Tax' THEN TaxAmount ELSE 0 END) AS TotalSalesTaxAmount,
        SUM(CASE WHEN TaxType = 'Purchase Tax' THEN TaxAmount ELSE 0 END) AS TotalPurchaseTaxAmount,
        SUM(TaxAmount) AS TotalTaxAmount,
        
        SUM(CASE WHEN TaxType = 'Sales Tax' THEN TaxableAmount ELSE 0 END) AS TotalSalesTaxableAmount,
        SUM(CASE WHEN TaxType = 'Purchase Tax' THEN TaxableAmount ELSE 0 END) AS TotalPurchaseTaxableAmount,
        SUM(TaxableAmount) AS TotalTaxableAmount,
        
        AVG(TaxAmount) AS AverageTaxAmount,
        MIN(TaxAmount) AS MinTaxAmount,
        MAX(TaxAmount) AS MaxTaxAmount,
        
        SUM(CASE WHEN TaxType = 'Sales Tax' AND Status = 'PENDING' THEN 1 ELSE 0 END) AS PendingSalesTaxInvoices,
        SUM(CASE WHEN TaxType = 'Sales Tax' AND Status = 'PAID' THEN 1 ELSE 0 END) AS PaidSalesTaxInvoices,
        SUM(CASE WHEN TaxType = 'Sales Tax' AND Status = 'OVERDUE' THEN 1 ELSE 0 END) AS OverdueSalesTaxInvoices,
        SUM(CASE WHEN TaxType = 'Purchase Tax' AND Status = 'PENDING' THEN 1 ELSE 0 END) AS PendingPurchaseTaxInvoices,
        SUM(CASE WHEN TaxType = 'Purchase Tax' AND Status = 'PAID' THEN 1 ELSE 0 END) AS PaidPurchaseTaxInvoices,
        SUM(CASE WHEN TaxType = 'Purchase Tax' AND Status = 'OVERDUE' THEN 1 ELSE 0 END) AS OverduePurchaseTaxInvoices,
        
        SUM(CASE WHEN TaxType = 'Sales Tax' AND Status = 'PENDING' THEN TaxAmount ELSE 0 END) AS PendingSalesTaxAmount,
        SUM(CASE WHEN TaxType = 'Sales Tax' AND Status = 'PAID' THEN TaxAmount ELSE 0 END) AS PaidSalesTaxAmount,
        SUM(CASE WHEN TaxType = 'Sales Tax' AND Status = 'OVERDUE' THEN TaxAmount ELSE 0 END) AS OverdueSalesTaxAmount,
        SUM(CASE WHEN TaxType = 'Purchase Tax' AND Status = 'PENDING' THEN TaxAmount ELSE 0 END) AS PendingPurchaseTaxAmount,
        SUM(CASE WHEN TaxType = 'Purchase Tax' AND Status = 'PAID' THEN TaxAmount ELSE 0 END) AS PaidPurchaseTaxAmount,
        SUM(CASE WHEN TaxType = 'Purchase Tax' AND Status = 'OVERDUE' THEN TaxAmount ELSE 0 END) AS OverduePurchaseTaxAmount,
        
        SUM(CASE WHEN TaxAmount >= 10000 THEN 1 ELSE 0 END) AS HighTaxInvoices,
        SUM(CASE WHEN TaxAmount >= 5000 AND TaxAmount < 10000 THEN 1 ELSE 0 END) AS MediumTaxInvoices,
        SUM(CASE WHEN TaxAmount >= 1000 AND TaxAmount < 5000 THEN 1 ELSE 0 END) AS LowTaxInvoices,
        SUM(CASE WHEN TaxAmount < 1000 THEN 1 ELSE 0 END) AS VeryLowTaxInvoices,
        
        SUM(CASE WHEN TaxAmount >= 10000 THEN TaxAmount ELSE 0 END) AS TotalHighTaxAmount,
        SUM(CASE WHEN TaxAmount >= 5000 AND TaxAmount < 10000 THEN TaxAmount ELSE 0 END) AS TotalMediumTaxAmount,
        SUM(CASE WHEN TaxAmount >= 1000 AND TaxAmount < 5000 THEN TaxAmount ELSE 0 END) AS TotalLowTaxAmount,
        SUM(CASE WHEN TaxAmount < 1000 THEN TaxAmount ELSE 0 END) AS TotalVeryLowTaxAmount,
        
        -- Date range info
        @StartDate AS ReportStartDate,
        @EndDate AS ReportEndDate,
        DATEDIFF(DAY, @StartDate, @EndDate) + 1 AS ReportDays,
        
        -- Calculated fields
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN TaxType = 'Sales Tax' THEN TaxAmount ELSE 0 END) * 100.0 / SUM(TaxAmount) AS DECIMAL(5,2))
            ELSE 0 
        END AS SalesTaxPercentage,
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN TaxType = 'Purchase Tax' THEN TaxAmount ELSE 0 END) * 100.0 / SUM(TaxAmount) AS DECIMAL(5,2))
            ELSE 0 
        END AS PurchaseTaxPercentage,
        CASE 
            WHEN SUM(TaxableAmount) > 0 THEN 
                CAST(SUM(TaxAmount) * 100.0 / SUM(TaxableAmount) AS DECIMAL(5,2))
            ELSE 0 
        END AS OverallTaxRate
    FROM (
        -- Sales Tax
        SELECT 'Sales Tax' AS TaxType, si.SalesInvoiceId AS InvoiceId, si.TaxAmount, si.TaxableAmount, si.Status, si.InvoiceDate, si.TaxCategoryId
        FROM SalesInvoices si
        WHERE si.InvoiceDate >= @StartDate
            AND si.InvoiceDate <= @EndDate
            AND (@TaxType IS NULL OR @TaxType = 'Sales Tax')
            AND si.TaxAmount > 0
        
        UNION ALL
        
        -- Purchase Tax
        SELECT 'Purchase Tax' AS TaxType, pi.PurchaseInvoiceId AS InvoiceId, pi.TaxAmount, pi.SubTotal AS TaxableAmount, pi.Status, pi.InvoiceDate, NULL AS TaxCategoryId
        FROM PurchaseInvoices pi
        WHERE pi.InvoiceDate >= @StartDate
            AND pi.InvoiceDate <= @EndDate
            AND (@TaxType IS NULL OR @TaxType = 'Purchase Tax')
            AND pi.TaxAmount > 0
    ) AS CombinedTax;
END;
