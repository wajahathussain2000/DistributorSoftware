CREATE PROCEDURE [dbo].[sp_GetProductWiseSalesReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @ProductId INT = NULL,
    @CategoryId INT = NULL,
    @BrandId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve product-wise sales data
    SELECT
        p.ProductId,
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
        p.PurchasePrice,
        p.SalePrice,
        p.MRP,
        p.StockQuantity,
        p.ReorderLevel,
        p.IsActive AS ProductIsActive,
        -- Sales aggregation by product
        COUNT(sid.DetailId) AS TotalSalesCount,
        SUM(sid.Quantity) AS TotalQuantitySold,
        AVG(sid.UnitPrice) AS AverageUnitPrice,
        MIN(sid.UnitPrice) AS MinUnitPrice,
        MAX(sid.UnitPrice) AS MaxUnitPrice,
        SUM(sid.DiscountAmount) AS TotalDiscountAmount,
        SUM(sid.TaxAmount) AS TotalTaxAmount,
        SUM(sid.TaxableAmount) AS TotalTaxableAmount,
        SUM(sid.TotalAmount) AS TotalSalesAmount,
        -- Profit calculations
        SUM(CASE WHEN p.PurchasePrice > 0 THEN (sid.UnitPrice - p.PurchasePrice) * sid.Quantity ELSE 0 END) AS TotalProfitAmount,
        AVG(CASE WHEN p.PurchasePrice > 0 THEN ((sid.UnitPrice - p.PurchasePrice) / p.PurchasePrice) * 100 ELSE 0 END) AS AverageProfitMarginPercentage,
        -- Performance metrics
        COUNT(DISTINCT si.CustomerId) AS UniqueCustomersCount,
        COUNT(DISTINCT si.SalesmanId) AS UniqueSalesmenCount,
        COUNT(DISTINCT si.SalesInvoiceId) AS UniqueInvoicesCount,
        -- Date range analysis
        MIN(si.InvoiceDate) AS FirstSaleDate,
        MAX(si.InvoiceDate) AS LastSaleDate,
        DATEDIFF(day, MIN(si.InvoiceDate), MAX(si.InvoiceDate)) AS SalesPeriodDays,
        -- Sales frequency
        CASE 
            WHEN COUNT(sid.DetailId) >= 50 THEN 'High Frequency'
            WHEN COUNT(sid.DetailId) >= 20 THEN 'Medium Frequency'
            WHEN COUNT(sid.DetailId) >= 5 THEN 'Low Frequency'
            ELSE 'Very Low Frequency'
        END AS SalesFrequencyCategory,
        -- Sales value analysis
        CASE 
            WHEN SUM(sid.TotalAmount) >= 100000 THEN 'High Value Product'
            WHEN SUM(sid.TotalAmount) >= 50000 THEN 'Medium Value Product'
            WHEN SUM(sid.TotalAmount) >= 10000 THEN 'Low Value Product'
            ELSE 'Very Low Value Product'
        END AS SalesValueCategory,
        -- Quantity analysis
        CASE 
            WHEN SUM(sid.Quantity) >= 1000 THEN 'High Volume Product'
            WHEN SUM(sid.Quantity) >= 500 THEN 'Medium Volume Product'
            WHEN SUM(sid.Quantity) >= 100 THEN 'Low Volume Product'
            ELSE 'Very Low Volume Product'
        END AS SalesVolumeCategory,
        -- Profit analysis
        CASE 
            WHEN AVG(CASE WHEN p.PurchasePrice > 0 THEN ((sid.UnitPrice - p.PurchasePrice) / p.PurchasePrice) * 100 ELSE 0 END) >= 50 THEN 'High Profit Margin'
            WHEN AVG(CASE WHEN p.PurchasePrice > 0 THEN ((sid.UnitPrice - p.PurchasePrice) / p.PurchasePrice) * 100 ELSE 0 END) >= 25 THEN 'Medium Profit Margin'
            WHEN AVG(CASE WHEN p.PurchasePrice > 0 THEN ((sid.UnitPrice - p.PurchasePrice) / p.PurchasePrice) * 100 ELSE 0 END) >= 10 THEN 'Low Profit Margin'
            ELSE 'Very Low Profit Margin'
        END AS ProfitMarginCategory
    FROM Products p
    LEFT JOIN Brands b ON p.BrandId = b.BrandId
    LEFT JOIN ProductCategories pc ON p.CategoryId = pc.CategoryId
    LEFT JOIN Units u ON p.UnitId = u.UnitId
    LEFT JOIN SalesInvoiceDetails sid ON p.ProductId = sid.ProductId
    LEFT JOIN SalesInvoices si ON sid.SalesInvoiceId = si.SalesInvoiceId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@ProductId IS NULL OR p.ProductId = @ProductId)
      AND (@CategoryId IS NULL OR p.CategoryId = @CategoryId)
      AND (@BrandId IS NULL OR p.BrandId = @BrandId)
    GROUP BY 
        p.ProductId, p.ProductCode, p.ProductName, p.Description, p.Category,
        p.BrandId, b.BrandName, p.CategoryId, pc.CategoryName, p.UnitId, u.UnitName,
        p.Barcode, p.PurchasePrice, p.SalePrice, p.MRP, p.StockQuantity, p.ReorderLevel, p.IsActive
    ORDER BY TotalSalesAmount DESC, TotalQuantitySold DESC;

    -- Summary Information
    SELECT
        COUNT(DISTINCT p.ProductId) AS TotalProductsSold,
        COUNT(DISTINCT p.CategoryId) AS TotalCategoriesSold,
        COUNT(DISTINCT p.BrandId) AS TotalBrandsSold,
        SUM(sid.Quantity) AS TotalQuantitySold,
        SUM(sid.DiscountAmount) AS TotalDiscountAmount,
        SUM(sid.TaxAmount) AS TotalTaxAmount,
        SUM(sid.TotalAmount) AS TotalSalesAmount,
        SUM(CASE WHEN p.PurchasePrice > 0 THEN (sid.UnitPrice - p.PurchasePrice) * sid.Quantity ELSE 0 END) AS TotalProfitAmount,
        AVG(CASE WHEN p.PurchasePrice > 0 THEN ((sid.UnitPrice - p.PurchasePrice) / p.PurchasePrice) * 100 ELSE 0 END) AS AverageProfitMarginPercentage,
        COUNT(DISTINCT si.CustomerId) AS TotalUniqueCustomers,
        COUNT(DISTINCT si.SalesmanId) AS TotalUniqueSalesmen,
        COUNT(DISTINCT si.SalesInvoiceId) AS TotalUniqueInvoices,
        -- Top performers
        (SELECT TOP 1 p2.ProductName FROM Products p2 INNER JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId INNER JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@ProductId IS NULL OR p2.ProductId = @ProductId) AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId) AND (@BrandId IS NULL OR p2.BrandId = @BrandId) GROUP BY p2.ProductId, p2.ProductName ORDER BY SUM(sid2.TotalAmount) DESC) AS TopSellingProduct,
        (SELECT TOP 1 b2.BrandName FROM Brands b2 INNER JOIN Products p2 ON b2.BrandId = p2.BrandId INNER JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId INNER JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@ProductId IS NULL OR p2.ProductId = @ProductId) AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId) AND (@BrandId IS NULL OR p2.BrandId = @BrandId) GROUP BY b2.BrandId, b2.BrandName ORDER BY SUM(sid2.TotalAmount) DESC) AS TopSellingBrand,
        (SELECT TOP 1 pc2.CategoryName FROM ProductCategories pc2 INNER JOIN Products p2 ON pc2.CategoryId = p2.CategoryId INNER JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId INNER JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@ProductId IS NULL OR p2.ProductId = @ProductId) AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId) AND (@BrandId IS NULL OR p2.BrandId = @BrandId) GROUP BY pc2.CategoryId, pc2.CategoryName ORDER BY SUM(sid2.TotalAmount) DESC) AS TopSellingCategory,
        -- Performance analysis (using subqueries to avoid nested aggregates)
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, COUNT(sid2.DetailId) as SalesCount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
              AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId)
              AND (@BrandId IS NULL OR p2.BrandId = @BrandId)
            GROUP BY p2.ProductId
            HAVING COUNT(sid2.DetailId) >= 50
        ) AS HighFreq) AS HighFrequencyProductsCount,
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, COUNT(sid2.DetailId) as SalesCount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
              AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId)
              AND (@BrandId IS NULL OR p2.BrandId = @BrandId)
            GROUP BY p2.ProductId
            HAVING COUNT(sid2.DetailId) >= 20 AND COUNT(sid2.DetailId) < 50
        ) AS MedFreq) AS MediumFrequencyProductsCount,
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, COUNT(sid2.DetailId) as SalesCount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
              AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId)
              AND (@BrandId IS NULL OR p2.BrandId = @BrandId)
            GROUP BY p2.ProductId
            HAVING COUNT(sid2.DetailId) >= 5 AND COUNT(sid2.DetailId) < 20
        ) AS LowFreq) AS LowFrequencyProductsCount,
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, COUNT(sid2.DetailId) as SalesCount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
              AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId)
              AND (@BrandId IS NULL OR p2.BrandId = @BrandId)
            GROUP BY p2.ProductId
            HAVING COUNT(sid2.DetailId) < 5
        ) AS VeryLowFreq) AS VeryLowFrequencyProductsCount,
        -- Value analysis (using subqueries to avoid nested aggregates)
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, SUM(sid2.TotalAmount) as TotalAmount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
              AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId)
              AND (@BrandId IS NULL OR p2.BrandId = @BrandId)
            GROUP BY p2.ProductId
            HAVING SUM(sid2.TotalAmount) >= 100000
        ) AS HighVal) AS HighValueProductsCount,
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, SUM(sid2.TotalAmount) as TotalAmount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
              AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId)
              AND (@BrandId IS NULL OR p2.BrandId = @BrandId)
            GROUP BY p2.ProductId
            HAVING SUM(sid2.TotalAmount) >= 50000 AND SUM(sid2.TotalAmount) < 100000
        ) AS MedVal) AS MediumValueProductsCount,
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, SUM(sid2.TotalAmount) as TotalAmount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
              AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId)
              AND (@BrandId IS NULL OR p2.BrandId = @BrandId)
            GROUP BY p2.ProductId
            HAVING SUM(sid2.TotalAmount) >= 10000 AND SUM(sid2.TotalAmount) < 50000
        ) AS LowVal) AS LowValueProductsCount,
        (SELECT COUNT(*) FROM (
            SELECT p2.ProductId, SUM(sid2.TotalAmount) as TotalAmount
            FROM Products p2
            LEFT JOIN SalesInvoiceDetails sid2 ON p2.ProductId = sid2.ProductId
            LEFT JOIN SalesInvoices si2 ON sid2.SalesInvoiceId = si2.SalesInvoiceId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@ProductId IS NULL OR p2.ProductId = @ProductId)
              AND (@CategoryId IS NULL OR p2.CategoryId = @CategoryId)
              AND (@BrandId IS NULL OR p2.BrandId = @BrandId)
            GROUP BY p2.ProductId
            HAVING SUM(sid2.TotalAmount) < 10000
        ) AS VeryLowVal) AS VeryLowValueProductsCount
    FROM Products p
    LEFT JOIN SalesInvoiceDetails sid ON p.ProductId = sid.ProductId
    LEFT JOIN SalesInvoices si ON sid.SalesInvoiceId = si.SalesInvoiceId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@ProductId IS NULL OR p.ProductId = @ProductId)
      AND (@CategoryId IS NULL OR p.CategoryId = @CategoryId)
      AND (@BrandId IS NULL OR p.BrandId = @BrandId);
END;
