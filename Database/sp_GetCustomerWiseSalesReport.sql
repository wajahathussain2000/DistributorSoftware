CREATE PROCEDURE [dbo].[sp_GetCustomerWiseSalesReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @CustomerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve customer-wise sales data
    SELECT
        c.CustomerId,
        c.CustomerCode,
        c.CustomerName,
        c.ContactPerson,
        c.Email,
        c.Phone,
        c.Address,
        c.City,
        c.State,
        c.PostalCode,
        c.Country,
        c.DiscountPercent,
        c.CreditLimit,
        c.OutstandingBalance,
        c.PaymentTerms,
        c.TaxNumber,
        c.GSTNumber,
        c.Remarks,
        c.IsActive AS CustomerIsActive,
        -- Sales aggregation by customer
        COUNT(si.SalesInvoiceId) AS TotalInvoicesCount,
        COUNT(sid.DetailId) AS TotalLineItemsCount,
        SUM(sid.Quantity) AS TotalQuantityPurchased,
        AVG(si.TotalAmount) AS AverageInvoiceAmount,
        MIN(si.TotalAmount) AS MinInvoiceAmount,
        MAX(si.TotalAmount) AS MaxInvoiceAmount,
        SUM(si.SubTotal) AS TotalSubTotal,
        SUM(si.DiscountAmount) AS TotalDiscountAmount,
        SUM(si.TaxAmount) AS TotalTaxAmount,
        SUM(si.TotalAmount) AS TotalSalesAmount,
        SUM(si.PaidAmount) AS TotalPaidAmount,
        SUM(si.BalanceAmount) AS TotalBalanceAmount,
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
        -- Date range analysis
        MIN(si.InvoiceDate) AS FirstPurchaseDate,
        MAX(si.InvoiceDate) AS LastPurchaseDate,
        DATEDIFF(day, MIN(si.InvoiceDate), MAX(si.InvoiceDate)) AS CustomerRelationshipDays,
        -- Performance metrics
        COUNT(DISTINCT si.SalesmanId) AS UniqueSalesmenCount,
        COUNT(DISTINCT sid.ProductId) AS UniqueProductsPurchased,
        -- Customer categorization
        CASE 
            WHEN SUM(si.TotalAmount) >= 100000 THEN 'High Value Customer'
            WHEN SUM(si.TotalAmount) >= 50000 THEN 'Medium Value Customer'
            WHEN SUM(si.TotalAmount) >= 10000 THEN 'Low Value Customer'
            ELSE 'Very Low Value Customer'
        END AS CustomerValueCategory,
        -- Frequency categorization
        CASE 
            WHEN COUNT(si.SalesInvoiceId) >= 20 THEN 'High Frequency Customer'
            WHEN COUNT(si.SalesInvoiceId) >= 10 THEN 'Medium Frequency Customer'
            WHEN COUNT(si.SalesInvoiceId) >= 5 THEN 'Low Frequency Customer'
            ELSE 'Very Low Frequency Customer'
        END AS CustomerFrequencyCategory,
        -- Payment behavior categorization
        CASE 
            WHEN COUNT(si.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si.SalesInvoiceId)) >= 90 THEN 'Excellent Payer'
            WHEN COUNT(si.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si.SalesInvoiceId)) >= 70 THEN 'Good Payer'
            WHEN COUNT(si.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si.SalesInvoiceId)) >= 50 THEN 'Average Payer'
            ELSE 'Poor Payer'
        END AS PaymentBehaviorCategory,
        -- Credit utilization
        CASE 
            WHEN c.CreditLimit > 0 THEN (c.OutstandingBalance / c.CreditLimit) * 100
            ELSE 0
        END AS CreditUtilizationPercentage,
        -- Average days between purchases
        CASE 
            WHEN COUNT(si.SalesInvoiceId) > 1 THEN DATEDIFF(day, MIN(si.InvoiceDate), MAX(si.InvoiceDate)) / (COUNT(si.SalesInvoiceId) - 1)
            ELSE 0
        END AS AverageDaysBetweenPurchases
    FROM Customers c
    LEFT JOIN SalesInvoices si ON c.CustomerId = si.CustomerId
    LEFT JOIN SalesInvoiceDetails sid ON si.SalesInvoiceId = sid.SalesInvoiceId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@CustomerId IS NULL OR c.CustomerId = @CustomerId)
    GROUP BY 
        c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactPerson, c.Email, c.Phone, c.Address,
        c.City, c.State, c.PostalCode, c.Country, c.DiscountPercent, c.CreditLimit, c.OutstandingBalance,
        c.PaymentTerms, c.TaxNumber, c.GSTNumber, c.Remarks, c.IsActive
    ORDER BY TotalSalesAmount DESC, TotalInvoicesCount DESC;

    -- Summary Information
    SELECT
        COUNT(DISTINCT c.CustomerId) AS TotalActiveCustomers,
        COUNT(DISTINCT si.SalesInvoiceId) AS TotalInvoices,
        COUNT(DISTINCT sid.ProductId) AS TotalProductsSold,
        SUM(sid.Quantity) AS TotalQuantitySold,
        SUM(si.SubTotal) AS TotalSubTotal,
        SUM(si.DiscountAmount) AS TotalDiscountAmount,
        SUM(si.TaxAmount) AS TotalTaxAmount,
        SUM(si.TotalAmount) AS TotalSalesAmount,
        SUM(si.PaidAmount) AS TotalPaidAmount,
        SUM(si.BalanceAmount) AS TotalBalanceAmount,
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
        -- Customer value analysis (using subqueries to avoid nested aggregates)
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, SUM(si2.TotalAmount) as TotalAmount
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING SUM(si2.TotalAmount) >= 100000
        ) AS HighVal) AS HighValueCustomersCount,
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, SUM(si2.TotalAmount) as TotalAmount
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING SUM(si2.TotalAmount) >= 50000 AND SUM(si2.TotalAmount) < 100000
        ) AS MedVal) AS MediumValueCustomersCount,
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, SUM(si2.TotalAmount) as TotalAmount
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING SUM(si2.TotalAmount) >= 10000 AND SUM(si2.TotalAmount) < 50000
        ) AS LowVal) AS LowValueCustomersCount,
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, SUM(si2.TotalAmount) as TotalAmount
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING SUM(si2.TotalAmount) < 10000
        ) AS VeryLowVal) AS VeryLowValueCustomersCount,
        -- Frequency analysis (using subqueries to avoid nested aggregates)
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, COUNT(si2.SalesInvoiceId) as InvoiceCount
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING COUNT(si2.SalesInvoiceId) >= 20
        ) AS HighFreq) AS HighFrequencyCustomersCount,
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, COUNT(si2.SalesInvoiceId) as InvoiceCount
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING COUNT(si2.SalesInvoiceId) >= 10 AND COUNT(si2.SalesInvoiceId) < 20
        ) AS MedFreq) AS MediumFrequencyCustomersCount,
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, COUNT(si2.SalesInvoiceId) as InvoiceCount
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING COUNT(si2.SalesInvoiceId) >= 5 AND COUNT(si2.SalesInvoiceId) < 10
        ) AS LowFreq) AS LowFrequencyCustomersCount,
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, COUNT(si2.SalesInvoiceId) as InvoiceCount
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING COUNT(si2.SalesInvoiceId) < 5
        ) AS VeryLowFreq) AS VeryLowFrequencyCustomersCount,
        -- Payment behavior analysis (simplified to avoid complex nested aggregates)
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, COUNT(si2.SalesInvoiceId) as TotalInvoices, COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) as PaidInvoices
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING COUNT(si2.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si2.SalesInvoiceId)) >= 90
        ) AS ExcellentPayers) AS ExcellentPayersCount,
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, COUNT(si2.SalesInvoiceId) as TotalInvoices, COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) as PaidInvoices
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING COUNT(si2.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si2.SalesInvoiceId)) >= 70 AND (COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si2.SalesInvoiceId)) < 90
        ) AS GoodPayers) AS GoodPayersCount,
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, COUNT(si2.SalesInvoiceId) as TotalInvoices, COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) as PaidInvoices
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING COUNT(si2.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si2.SalesInvoiceId)) >= 50 AND (COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si2.SalesInvoiceId)) < 70
        ) AS AveragePayers) AS AveragePayersCount,
        (SELECT COUNT(*) FROM (
            SELECT c2.CustomerId, COUNT(si2.SalesInvoiceId) as TotalInvoices, COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) as PaidInvoices
            FROM Customers c2
            LEFT JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId
            WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate
              AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId)
            GROUP BY c2.CustomerId
            HAVING COUNT(si2.SalesInvoiceId) > 0 AND (COUNT(CASE WHEN si2.Status = 'PAID' THEN 1 ELSE NULL END) * 100.0 / COUNT(si2.SalesInvoiceId)) < 50
        ) AS PoorPayers) AS PoorPayersCount,
        -- Top performers
        (SELECT TOP 1 c2.CustomerName FROM Customers c2 INNER JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId) GROUP BY c2.CustomerId, c2.CustomerName ORDER BY SUM(si2.TotalAmount) DESC) AS TopCustomer,
        (SELECT TOP 1 c2.CustomerName FROM Customers c2 INNER JOIN SalesInvoices si2 ON c2.CustomerId = si2.CustomerId WHERE si2.InvoiceDate >= @StartDate AND si2.InvoiceDate <= @EndDate AND (@CustomerId IS NULL OR c2.CustomerId = @CustomerId) GROUP BY c2.CustomerId, c2.CustomerName ORDER BY COUNT(si2.SalesInvoiceId) DESC) AS MostFrequentCustomer,
        -- Average metrics
        AVG(si.TotalAmount) AS AverageInvoiceAmount,
        AVG(DATEDIFF(day, si.InvoiceDate, GETDATE())) AS AverageInvoiceAge
    FROM Customers c
    LEFT JOIN SalesInvoices si ON c.CustomerId = si.CustomerId
    LEFT JOIN SalesInvoiceDetails sid ON si.SalesInvoiceId = sid.SalesInvoiceId
    WHERE si.InvoiceDate >= @StartDate
      AND si.InvoiceDate <= @EndDate
      AND (@CustomerId IS NULL OR c.CustomerId = @CustomerId);
END;
