CREATE PROCEDURE [dbo].[sp_GetProfitMarginReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @ProductId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve profit margin data
    SELECT
        p.ProductId,
        p.ProductCode,
        p.ProductName,
        p.Description,
        p.CategoryId,
        pc.CategoryName,
        p.BrandId,
        b.BrandName,
        p.UnitPrice,
        p.PurchasePrice,
        p.SalePrice,
        p.ReorderLevel,
        p.StockQuantity,
        p.Quantity,
        p.IsActive AS ProductIsActive,
        -- Sales aggregation
        COUNT(sid.DetailId) AS TotalSalesCount,
        SUM(sid.Quantity) AS TotalQuantitySold,
        SUM(sid.UnitPrice * sid.Quantity) AS TotalSalesAmount,
        SUM(sid.UnitPrice * sid.Quantity * 0.7) AS TotalCostAmount,
        SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) AS TotalProfitAmount,
        -- Profit margin calculations
        CASE 
            WHEN SUM(sid.UnitPrice * sid.Quantity) > 0 THEN 
                (SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) / SUM(sid.UnitPrice * sid.Quantity)) * 100
            ELSE 0
        END AS ProfitMarginPercentage,
        CASE 
            WHEN SUM(sid.UnitPrice * sid.Quantity * 0.7) > 0 THEN 
                (SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) / SUM(sid.UnitPrice * sid.Quantity * 0.7)) * 100
            ELSE 0
        END AS MarkupPercentage,
        -- Average calculations
        AVG(sid.UnitPrice) AS AverageSellingPrice,
        AVG(sid.UnitPrice * 0.7) AS AverageCostPrice,
        AVG(sid.UnitPrice - (sid.UnitPrice * 0.7)) AS AverageProfitPerUnit,
        -- Price analysis
        MIN(sid.UnitPrice) AS MinSellingPrice,
        MAX(sid.UnitPrice) AS MaxSellingPrice,
        MIN(sid.UnitPrice * 0.7) AS MinCostPrice,
        MAX(sid.UnitPrice * 0.7) AS MaxCostPrice,
        -- Sales frequency
        COUNT(DISTINCT si.SalesInvoiceId) AS InvoiceFrequency,
        COUNT(DISTINCT si.CustomerId) AS CustomerFrequency,
        -- Date analysis
        MIN(si.InvoiceDate) AS FirstSaleDate,
        MAX(si.InvoiceDate) AS LastSaleDate,
        DATEDIFF(day, MIN(si.InvoiceDate), MAX(si.InvoiceDate)) AS SalesPeriodDays,
        -- Performance categorization
        CASE 
            WHEN SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) >= 10000 THEN 'High Profit Product'
            WHEN SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) >= 5000 THEN 'Medium Profit Product'
            WHEN SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) >= 1000 THEN 'Low Profit Product'
            ELSE 'Very Low Profit Product'
        END AS ProfitCategory,
        -- Margin categorization
        CASE 
            WHEN (SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) / SUM(sid.UnitPrice * sid.Quantity)) * 100 >= 30 THEN 'High Margin Product'
            WHEN (SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) / SUM(sid.UnitPrice * sid.Quantity)) * 100 >= 20 THEN 'Medium Margin Product'
            WHEN (SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) / SUM(sid.UnitPrice * sid.Quantity)) * 100 >= 10 THEN 'Low Margin Product'
            ELSE 'Very Low Margin Product'
        END AS MarginCategory,
        -- Sales volume categorization
        CASE 
            WHEN SUM(sid.Quantity) >= 100 THEN 'High Volume Product'
            WHEN SUM(sid.Quantity) >= 50 THEN 'Medium Volume Product'
            WHEN SUM(sid.Quantity) >= 10 THEN 'Low Volume Product'
            ELSE 'Very Low Volume Product'
        END AS VolumeCategory,
        -- Stock analysis
        CASE 
            WHEN p.StockQuantity <= p.ReorderLevel THEN 'Low Stock'
            WHEN p.StockQuantity >= (p.ReorderLevel * 3) THEN 'Overstocked'
            WHEN p.StockQuantity BETWEEN p.ReorderLevel AND (p.ReorderLevel * 3) THEN 'Optimal Stock'
            ELSE 'Stock Status Unknown'
        END AS StockStatus,
        -- Profitability index
        CASE 
            WHEN SUM(sid.Quantity) > 0 AND SUM(sid.UnitPrice * sid.Quantity) > 0 THEN 
                (SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) / SUM(sid.UnitPrice * sid.Quantity)) * 100 * (SUM(sid.Quantity) / 100.0)
            ELSE 0
        END AS ProfitabilityIndex,
        -- Revenue contribution
        CASE 
            WHEN (SELECT SUM(sid2.UnitPrice * sid2.Quantity) FROM SalesInvoiceDetails sid2 INNER JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate) > 0 THEN
                (SUM(sid.UnitPrice * sid.Quantity) / (SELECT SUM(sid2.UnitPrice * sid2.Quantity) FROM SalesInvoiceDetails sid2 INNER JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate)) * 100
            ELSE 0
        END AS RevenueContributionPercentage
    FROM Products p
    LEFT JOIN ProductCategories pc ON p.CategoryId = pc.CategoryId
    LEFT JOIN Brands b ON p.BrandId = b.BrandId
    LEFT JOIN SalesInvoiceDetails sid ON p.ProductId = sid.ProductId
    LEFT JOIN SalesInvoices si ON sid.SalesInvoiceId = si.SalesInvoiceId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@ProductId IS NULL OR p.ProductId = @ProductId)
    GROUP BY 
        p.ProductId, p.ProductCode, p.ProductName, p.Description, p.CategoryId, pc.CategoryName,
        p.BrandId, b.BrandName, p.UnitPrice, p.PurchasePrice, p.SalePrice, p.ReorderLevel,
        p.StockQuantity, p.Quantity, p.IsActive
    ORDER BY TotalProfitAmount DESC, ProfitMarginPercentage DESC;

    -- Summary Information
    SELECT
        COUNT(DISTINCT p.ProductId) AS TotalProductsSold,
        COUNT(DISTINCT si.SalesInvoiceId) AS TotalInvoices,
        COUNT(DISTINCT si.CustomerId) AS TotalCustomers,
        SUM(sid.Quantity) AS TotalQuantitySold,
        SUM(sid.UnitPrice * sid.Quantity) AS TotalSalesAmount,
        SUM(sid.UnitPrice * sid.Quantity * 0.7) AS TotalCostAmount,
        SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) AS TotalProfitAmount,
        -- Overall profit margin
        CASE 
            WHEN SUM(sid.UnitPrice * sid.Quantity) > 0 THEN 
                (SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) / SUM(sid.UnitPrice * sid.Quantity)) * 100
            ELSE 0
        END AS OverallProfitMarginPercentage,
        -- Overall markup
        CASE 
            WHEN SUM(sid.UnitPrice * sid.Quantity * 0.7) > 0 THEN 
                (SUM((sid.UnitPrice - (sid.UnitPrice * 0.7)) * sid.Quantity) / SUM(sid.UnitPrice * sid.Quantity * 0.7)) * 100
            ELSE 0
        END AS OverallMarkupPercentage,
        -- Profit analysis (using subqueries to avoid nested aggregates)
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) as ProfitAmount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
            GROUP BY p2.ProductId
            HAVING SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) >= 10000
        ) AS HighProfit) AS HighProfitProductsCount,
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) as ProfitAmount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
            GROUP BY p2.ProductId
            HAVING SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) >= 5000 AND SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) < 10000
        ) AS MedProfit) AS MediumProfitProductsCount,
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) as ProfitAmount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
            GROUP BY p2.ProductId
            HAVING SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) >= 1000 AND SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) < 5000
        ) AS LowProfit) AS LowProfitProductsCount,
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) as ProfitAmount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
            GROUP BY p2.ProductId
            HAVING SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) < 1000
        ) AS VeryLowProfit) AS VeryLowProfitProductsCount,
        -- Top performers
        (SELECT TOP 1 p2.ProductName FROM Products p2 INNER JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId INNER JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@ProductId IS NULL OR p2.ProductId = @ProductId) GROUP BY p2.ProductId, p2.ProductName ORDER BY SUM((sid2.UnitPrice - (sid2.UnitPrice * 0.7)) * sid2.Quantity) DESC) AS TopProfitProduct,
        (SELECT TOP 1 p2.ProductName FROM Products p2 INNER JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId INNER JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@ProductId IS NULL OR p2.ProductId = @ProductId) GROUP BY p2.ProductId, p2.ProductName ORDER BY SUM(sid2.Quantity) DESC) AS TopVolumeProduct,
        -- Average metrics
        AVG(sid.UnitPrice) AS AverageSellingPrice,
        AVG(sid.UnitPrice * 0.7) AS AverageCostPrice,
        AVG(sid.UnitPrice - (sid.UnitPrice * 0.7)) AS AverageProfitPerUnit
    FROM Products p
    LEFT JOIN SalesInvoiceDetails sid ON p.ProductId = sid.ProductId
    LEFT JOIN SalesInvoices si ON sid.SalesInvoiceId = si.SalesInvoiceId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@ProductId IS NULL OR p.ProductId = @ProductId);
END;