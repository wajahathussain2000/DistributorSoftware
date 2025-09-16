CREATE PROCEDURE [dbo].[sp_GetSalesmanWiseSalesReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @SalesmanId INT = NULL,
    @CustomerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve salesman-wise sales data
    SELECT
        sm.SalesmanId,
        sm.SalesmanCode,
        sm.SalesmanName,
        sm.Email,
        sm.Phone,
        sm.Address,
        sm.Territory,
        sm.CommissionRate,
        sm.IsActive AS SalesmanIsActive,
        -- Sales aggregation by salesman
        COUNT(si.SalesInvoiceId) AS TotalInvoicesCount,
        COUNT(sid.DetailId) AS TotalLineItemsCount,
        SUM(sid.Quantity) AS TotalQuantitySold,
        AVG(si.TotalAmount) AS AverageInvoiceAmount,
        MIN(si.TotalAmount) AS MinInvoiceAmount,
        MAX(si.TotalAmount) AS MaxInvoiceAmount,
        SUM(si.SubTotal) AS TotalSubTotal,
        SUM(si.DiscountAmount) AS TotalDiscountAmount,
        SUM(si.TaxAmount) AS TotalTaxAmount,
        SUM(si.TotalAmount) AS TotalSalesAmount,
        SUM(si.PaidAmount) AS TotalPaidAmount,
        SUM(si.BalanceAmount) AS TotalBalanceAmount,
        -- Commission calculation
        SUM(si.TotalAmount) * ISNULL(sm.CommissionRate, 0) / 100 AS TotalCommissionEarned,
        -- Payment analysis
        COUNT(CASE WHEN si.Status = 'PAID' THEN 1 ELSE NULL END) AS PaidInvoicesCount,
        COUNT(CASE WHEN si.Status = 'PENDING' THEN 1 ELSE NULL END) AS PendingInvoicesCount,
        COUNT(CASE WHEN si.Status = 'CANCELLED' THEN 1 ELSE NULL END) AS CancelledInvoicesCount,
        SUM(CASE WHEN si.Status = 'PAID' THEN si.TotalAmount ELSE 0 END) AS PaidAmount,
        SUM(CASE WHEN si.Status = 'PENDING' THEN si.TotalAmount ELSE 0 END) AS PendingAmount,
        SUM(CASE WHEN si.Status = 'CANCELLED' THEN si.TotalAmount ELSE 0 END) AS CancelledAmount,
        -- Overdue analysis
        COUNT(CASE WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN 1 ELSE NULL END) AS OverdueInvoicesCount,
        SUM(CASE WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN si.BalanceAmount ELSE 0 END) AS OverdueAmount,
        -- Customer analysis
        COUNT(DISTINCT si.CustomerId) AS UniqueCustomersCount,
        COUNT(DISTINCT sid.ProductId) AS UniqueProductsSold,
        -- Date range analysis
        MIN(si.InvoiceDate) AS FirstSaleDate,
        MAX(si.InvoiceDate) AS LastSaleDate,
        DATEDIFF(day, MIN(si.InvoiceDate), MAX(si.InvoiceDate)) AS SalesPeriodDays,
        -- Performance metrics
        CASE 
            WHEN COUNT(si.SalesInvoiceId) > 0 THEN DATEDIFF(day, MIN(si.InvoiceDate), MAX(si.InvoiceDate)) / COUNT(si.SalesInvoiceId)
            ELSE 0
        END AS AverageDaysBetweenSales,
        -- Salesman categorization
        CASE 
            WHEN SUM(si.TotalAmount) >= 200000 THEN 'Top Performer'
            WHEN SUM(si.TotalAmount) >= 100000 THEN 'High Performer'
            WHEN SUM(si.TotalAmount) >= 50000 THEN 'Medium Performer'
            WHEN SUM(si.TotalAmount) >= 10000 THEN 'Low Performer'
            ELSE 'Under Performer'
        END AS SalesmanPerformanceCategory,
        -- Frequency categorization
        CASE 
            WHEN COUNT(si.SalesInvoiceId) >= 30 THEN 'High Frequency Salesman'
            WHEN COUNT(si.SalesInvoiceId) >= 15 THEN 'Medium Frequency Salesman'
            WHEN COUNT(si.SalesInvoiceId) >= 5 THEN 'Low Frequency Salesman'
            ELSE 'Very Low Frequency Salesman'
        END AS SalesmanFrequencyCategory,
        -- Customer relationship strength
        CASE 
            WHEN COUNT(DISTINCT si.CustomerId) >= 10 THEN 'Strong Customer Base'
            WHEN COUNT(DISTINCT si.CustomerId) >= 5 THEN 'Moderate Customer Base'
            WHEN COUNT(DISTINCT si.CustomerId) >= 2 THEN 'Limited Customer Base'
            ELSE 'Single Customer Focus'
        END AS CustomerRelationshipCategory,
        -- Payment collection efficiency
        CASE 
            WHEN COUNT(si.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si.SalesInvoiceId)) >= 90 THEN 'Excellent Collector'
            WHEN COUNT(si.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si.SalesInvoiceId)) >= 70 THEN 'Good Collector'
            WHEN COUNT(si.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si.SalesInvoiceId)) >= 50 THEN 'Average Collector'
            ELSE 'Poor Collector'
        END AS CollectionEfficiencyCategory,
        -- Territory performance
        CASE 
            WHEN sm.Territory IS NOT NULL AND sm.Territory != '' THEN sm.Territory
            ELSE 'No Territory Assigned'
        END AS TerritoryStatus,
        -- Commission efficiency
        CASE 
            WHEN SUM(si.TotalAmount) > 0 AND ISNULL(sm.CommissionRate, 0) > 0 THEN (SUM(si.TotalAmount) * sm.CommissionRate / 100) / SUM(si.TotalAmount) * 100
            ELSE 0
        END AS CommissionEfficiencyPercentage
    FROM Salesman sm
    LEFT JOIN SalesInvoices si ON sm.SalesmanId = si.SalesmanId
    LEFT JOIN SalesInvoiceDetails sid ON si.SalesInvoiceId = sid.SalesInvoiceId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@SalesmanId IS NULL OR sm.SalesmanId = @SalesmanId)
      AND (@CustomerId IS NULL OR si.CustomerId = @CustomerId)
    GROUP BY 
        sm.SalesmanId, sm.SalesmanCode, sm.SalesmanName, sm.Email, sm.Phone, sm.Address,
        sm.Territory, sm.CommissionRate, sm.IsActive
    ORDER BY TotalSalesAmount DESC, TotalInvoicesCount DESC;

    -- Summary Information
    SELECT
        COUNT(DISTINCT sm.SalesmanId) AS TotalActiveSalesmen,
        COUNT(DISTINCT si.SalesInvoiceId) AS TotalInvoices,
        COUNT(DISTINCT si.CustomerId) AS TotalCustomers,
        COUNT(DISTINCT sid.ProductId) AS TotalProductsSold,
        SUM(sid.Quantity) AS TotalQuantitySold,
        SUM(si.SubTotal) AS TotalSubTotal,
        SUM(si.DiscountAmount) AS TotalDiscountAmount,
        SUM(si.TaxAmount) AS TotalTaxAmount,
        SUM(si.TotalAmount) AS TotalSalesAmount,
        SUM(si.PaidAmount) AS TotalPaidAmount,
        SUM(si.BalanceAmount) AS TotalBalanceAmount,
        SUM(si.TotalAmount * ISNULL(sm.CommissionRate, 0) / 100) AS TotalCommissionPaid,
        -- Payment status summary
        COUNT(CASE WHEN si.Status = 'PAID' THEN 1 ELSE NULL END) AS TotalPaidInvoices,
        COUNT(CASE WHEN si.Status = 'PENDING' THEN 1 ELSE NULL END) AS TotalPendingInvoices,
        COUNT(CASE WHEN si.Status = 'CANCELLED' THEN 1 ELSE NULL END) AS TotalCancelledInvoices,
        SUM(CASE WHEN si.Status = 'PAID' THEN si.TotalAmount ELSE 0 END) AS TotalPaidAmount,
        SUM(CASE WHEN si.Status = 'PENDING' THEN si.TotalAmount ELSE 0 END) AS TotalPendingAmount,
        SUM(CASE WHEN si.Status = 'CANCELLED' THEN si.TotalAmount ELSE 0 END) AS TotalCancelledAmount,
        -- Overdue summary
        COUNT(CASE WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN 1 ELSE NULL END) AS TotalOverdueInvoices,
        SUM(CASE WHEN si.BalanceAmount > 0 AND si.DueDate < GETDATE() THEN si.BalanceAmount ELSE 0 END) AS TotalOverdueAmount,
        -- Performance analysis (using subqueries to avoid nested aggregates)
        (SELECT COUNT(*) FROM (
            SELECT sm2.SalesmanId, SUM(si2.TotalAmount) as TotalAmount
            FROM Salesman sm2
            LEFT JOIN SalesInvoices si2 ON sm2.SalesmanId = si2.SalesmanId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@SalesmanId IS NULL OR sm2.SalesmanId = @SalesmanId)
              AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId)
            GROUP BY sm2.SalesmanId
            HAVING SUM(si2.TotalAmount) >= 200000
        ) AS TopPerformers) AS TopPerformersCount,
        (SELECT COUNT(*) FROM (
            SELECT sm2.SalesmanId, SUM(si2.TotalAmount) as TotalAmount
            FROM Salesman sm2
            LEFT JOIN SalesInvoices si2 ON sm2.SalesmanId = si2.SalesmanId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@SalesmanId IS NULL OR sm2.SalesmanId = @SalesmanId)
              AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId)
            GROUP BY sm2.SalesmanId
            HAVING SUM(si2.TotalAmount) >= 100000 AND SUM(si2.TotalAmount) < 200000
        ) AS HighPerformers) AS HighPerformersCount,
        (SELECT COUNT(*) FROM (
            SELECT sm2.SalesmanId, SUM(si2.TotalAmount) as TotalAmount
            FROM Salesman sm2
            LEFT JOIN SalesInvoices si2 ON sm2.SalesmanId = si2.SalesmanId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@SalesmanId IS NULL OR sm2.SalesmanId = @SalesmanId)
              AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId)
            GROUP BY sm2.SalesmanId
            HAVING SUM(si2.TotalAmount) >= 50000 AND SUM(si2.TotalAmount) < 100000
        ) AS MediumPerformers) AS MediumPerformersCount,
        (SELECT COUNT(*) FROM (
            SELECT sm2.SalesmanId, SUM(si2.TotalAmount) as TotalAmount
            FROM Salesman sm2
            LEFT JOIN SalesInvoices si2 ON sm2.SalesmanId = si2.SalesmanId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@SalesmanId IS NULL OR sm2.SalesmanId = @SalesmanId)
              AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId)
            GROUP BY sm2.SalesmanId
            HAVING SUM(si2.TotalAmount) < 50000
        ) AS LowPerformers) AS LowPerformersCount,
        -- Top performers
        (SELECT TOP 1 sm2.SalesmanName FROM Salesman sm2 INNER JOIN SalesInvoices si2 ON sm2.SalesmanId = si2.SalesmanId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@SalesmanId IS NULL OR sm2.SalesmanId = @SalesmanId) AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId) GROUP BY sm2.SalesmanId, sm2.SalesmanName ORDER BY SUM(si2.TotalAmount) DESC) AS TopSalesman,
        (SELECT TOP 1 sm2.SalesmanName FROM Salesman sm2 INNER JOIN SalesInvoices si2 ON sm2.SalesmanId = si2.SalesmanId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@SalesmanId IS NULL OR sm2.SalesmanId = @SalesmanId) AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId) GROUP BY sm2.SalesmanId, sm2.SalesmanName ORDER BY COUNT(si2.SalesInvoiceId) DESC) AS MostActiveSalesman,
        -- Average metrics
        AVG(si.TotalAmount) AS AverageInvoiceAmount,
        AVG(DATEDIFF(day, si.InvoiceDate, GETDATE())) AS AverageInvoiceAge,
        AVG(ISNULL(sm.CommissionRate, 0)) AS AverageCommissionRate
    FROM Salesman sm
    LEFT JOIN SalesInvoices si ON sm.SalesmanId = si.SalesmanId
    LEFT JOIN SalesInvoiceDetails sid ON si.SalesInvoiceId = sid.SalesInvoiceId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@SalesmanId IS NULL OR sm.SalesmanId = @SalesmanId)
      AND (@CustomerId IS NULL OR si.CustomerId = @CustomerId);
END;
