CREATE PROCEDURE [dbo].[sp_GetPendingDeliveryReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @CustomerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve pending delivery data
    SELECT
        dc.ChallanId,
        dc.ChallanNo,
        dc.SalesInvoiceId,
        dc.CustomerName,
        dc.CustomerAddress,
        dc.ChallanDate,
        dc.VehicleNo,
        dc.DriverName,
        dc.DriverPhone,
        dc.Status,
        dc.Remarks,
        dc.CreatedDate,
        dc.CreatedBy,
        dc.UpdatedDate,
        dc.UpdatedBy,
        dc.VehicleId,
        dc.RouteId,
        -- Sales Invoice Information
        si.InvoiceNumber,
        si.InvoiceDate,
        si.TotalAmount,
        si.PaidAmount,
        si.BalanceAmount,
        si.PaymentMode,
        si.Status AS InvoiceStatus,
        -- Customer Information
        si.CustomerId,
        c.CustomerCode,
        c.ContactPerson,
        c.Email AS CustomerEmail,
        c.Phone AS CustomerPhone,
        c.City AS CustomerCity,
        c.State AS CustomerState,
        c.PostalCode AS CustomerPostalCode,
        c.Country AS CustomerCountry,
        -- Vehicle Information
        vm.VehicleType,
        vm.TransporterName,
        vm.IsActive AS VehicleIsActive,
        -- Route Information
        rm.RouteName,
        rm.StartLocation,
        rm.EndLocation,
        rm.Distance,
        rm.EstimatedTime,
        rm.IsActive AS RouteIsActive,
        -- Delivery Schedule Information
        ds.ScheduleId,
        ds.ScheduleRef,
        ds.ScheduledDateTime,
        ds.DispatchDateTime,
        ds.DeliveredDateTime,
        ds.ReturnedDateTime,
        ds.Status AS ScheduleStatus,
        -- Pending Analysis
        CASE 
            WHEN dc.Status = 'PENDING' THEN 'Pending Delivery'
            WHEN dc.Status = 'SCHEDULED' THEN 'Scheduled'
            WHEN dc.Status = 'DISPATCHED' THEN 'Dispatched'
            WHEN dc.Status = 'IN_TRANSIT' THEN 'In Transit'
            WHEN dc.Status = 'DELIVERED' THEN 'Delivered'
            WHEN dc.Status = 'RETURNED' THEN 'Returned'
            WHEN dc.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE 'Unknown Status'
        END AS StatusDescription,
        -- Age Analysis
        DATEDIFF(day, dc.ChallanDate, GETDATE()) AS ChallanAgeDays,
        DATEDIFF(day, si.InvoiceDate, GETDATE()) AS InvoiceAgeDays,
        CASE 
            WHEN ds.ScheduledDateTime IS NOT NULL THEN
                DATEDIFF(day, ds.ScheduledDateTime, GETDATE())
            ELSE NULL
        END AS ScheduleAgeDays,
        -- Priority Level
        CASE 
            WHEN DATEDIFF(day, dc.ChallanDate, GETDATE()) >= 7 THEN 'High Priority'
            WHEN DATEDIFF(day, dc.ChallanDate, GETDATE()) >= 3 THEN 'Medium Priority'
            ELSE 'Low Priority'
        END AS PriorityLevel,
        -- Payment Status
        CASE 
            WHEN si.BalanceAmount = 0 THEN 'Fully Paid'
            WHEN si.BalanceAmount > 0 AND si.PaidAmount > 0 THEN 'Partially Paid'
            WHEN si.BalanceAmount > 0 AND si.PaidAmount = 0 THEN 'Unpaid'
            ELSE 'Unknown Payment Status'
        END AS PaymentStatus,
        -- Delivery Items Count
        (SELECT COUNT(*) FROM DeliveryChallanItems WHERE ChallanId = dc.ChallanId) AS ItemsCount,
        (SELECT SUM(Quantity) FROM DeliveryChallanItems WHERE ChallanId = dc.ChallanId) AS TotalQuantity,
        -- Schedule Status
        CASE 
            WHEN ds.ScheduleId IS NULL THEN 'Not Scheduled'
            WHEN ds.Status = 'SCHEDULED' THEN 'Scheduled'
            WHEN ds.Status = 'DISPATCHED' THEN 'Dispatched'
            WHEN ds.Status = 'IN_TRANSIT' THEN 'In Transit'
            WHEN ds.Status = 'DELIVERED' THEN 'Delivered'
            WHEN ds.Status = 'RETURNED' THEN 'Returned'
            WHEN ds.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE 'Unknown Schedule Status'
        END AS ScheduleStatusDescription
    FROM DeliveryChallans dc
    LEFT JOIN SalesInvoices si ON dc.SalesInvoiceId = si.SalesInvoiceId
    LEFT JOIN Customers c ON si.CustomerId = c.CustomerId
    LEFT JOIN VehicleMaster vm ON dc.VehicleId = vm.VehicleId
    LEFT JOIN RouteMaster rm ON dc.RouteId = rm.RouteId
    LEFT JOIN DeliveryScheduleItems dsi ON dc.ChallanId = dsi.ChallanId
    LEFT JOIN DeliverySchedules ds ON dsi.ScheduleId = ds.ScheduleId
    WHERE dc.ChallanDate >= @StartDate
      AND dc.ChallanDate <= @EndDate
      AND (@CustomerId IS NULL OR si.CustomerId = @CustomerId)
      AND dc.Status IN ('PENDING', 'SCHEDULED', 'DISPATCHED', 'IN_TRANSIT')
    ORDER BY dc.ChallanDate DESC, dc.ChallanNo;

    -- Summary Information
    SELECT
        COUNT(*) AS TotalPendingDeliveries,
        COUNT(DISTINCT si.CustomerId) AS TotalCustomers,
        COUNT(DISTINCT dc.VehicleId) AS TotalVehicles,
        COUNT(DISTINCT dc.RouteId) AS TotalRoutes,
        -- Status Summary
        COUNT(CASE WHEN dc.Status = 'PENDING' THEN 1 END) AS PendingCount,
        COUNT(CASE WHEN dc.Status = 'SCHEDULED' THEN 1 END) AS ScheduledCount,
        COUNT(CASE WHEN dc.Status = 'DISPATCHED' THEN 1 END) AS DispatchedCount,
        COUNT(CASE WHEN dc.Status = 'IN_TRANSIT' THEN 1 END) AS InTransitCount,
        -- Priority Summary
        COUNT(CASE WHEN DATEDIFF(day, dc.ChallanDate, GETDATE()) >= 7 THEN 1 END) AS HighPriorityCount,
        COUNT(CASE WHEN DATEDIFF(day, dc.ChallanDate, GETDATE()) >= 3 AND DATEDIFF(day, dc.ChallanDate, GETDATE()) < 7 THEN 1 END) AS MediumPriorityCount,
        COUNT(CASE WHEN DATEDIFF(day, dc.ChallanDate, GETDATE()) < 3 THEN 1 END) AS LowPriorityCount,
        -- Payment Status Summary
        COUNT(CASE WHEN si.BalanceAmount = 0 THEN 1 END) AS FullyPaidCount,
        COUNT(CASE WHEN si.BalanceAmount > 0 AND si.PaidAmount > 0 THEN 1 END) AS PartiallyPaidCount,
        COUNT(CASE WHEN si.BalanceAmount > 0 AND si.PaidAmount = 0 THEN 1 END) AS UnpaidCount,
        -- Schedule Status Summary
        COUNT(CASE WHEN ds.ScheduleId IS NULL THEN 1 END) AS NotScheduledCount,
        COUNT(CASE WHEN ds.Status = 'SCHEDULED' THEN 1 END) AS ScheduledDeliveryCount,
        COUNT(CASE WHEN ds.Status = 'DISPATCHED' THEN 1 END) AS DispatchedDeliveryCount,
        COUNT(CASE WHEN ds.Status = 'IN_TRANSIT' THEN 1 END) AS InTransitDeliveryCount,
        -- Financial Summary
        SUM(si.TotalAmount) AS TotalInvoiceAmount,
        SUM(si.PaidAmount) AS TotalPaidAmount,
        SUM(si.BalanceAmount) AS TotalBalanceAmount,
        -- Items Summary (using subqueries to avoid nested aggregates)
        (SELECT COUNT(*) FROM DeliveryChallanItems dci 
         INNER JOIN DeliveryChallans dc2 ON dci.ChallanId = dc2.ChallanId
         WHERE dc2.ChallanDate >= @StartDate AND dc2.ChallanDate <= @EndDate
           AND (@CustomerId IS NULL OR dc2.SalesInvoiceId IN (SELECT SalesInvoiceId FROM SalesInvoices WHERE CustomerId = @CustomerId))
           AND dc2.Status IN ('PENDING', 'SCHEDULED', 'DISPATCHED', 'IN_TRANSIT')) AS TotalItems,
        (SELECT SUM(Quantity) FROM DeliveryChallanItems dci 
         INNER JOIN DeliveryChallans dc2 ON dci.ChallanId = dc2.ChallanId
         WHERE dc2.ChallanDate >= @StartDate AND dc2.ChallanDate <= @EndDate
           AND (@CustomerId IS NULL OR dc2.SalesInvoiceId IN (SELECT SalesInvoiceId FROM SalesInvoices WHERE CustomerId = @CustomerId))
           AND dc2.Status IN ('PENDING', 'SCHEDULED', 'DISPATCHED', 'IN_TRANSIT')) AS TotalQuantity,
        -- Age Analysis
        AVG(DATEDIFF(day, dc.ChallanDate, GETDATE())) AS AverageChallanAgeDays,
        AVG(DATEDIFF(day, si.InvoiceDate, GETDATE())) AS AverageInvoiceAgeDays,
        -- Top Performers
        (SELECT TOP 1 c.CustomerName FROM DeliveryChallans dc2 
         INNER JOIN SalesInvoices si2 ON dc2.SalesInvoiceId = si2.SalesInvoiceId
         INNER JOIN Customers c ON si2.CustomerId = c.CustomerId 
         WHERE dc2.ChallanDate >= @StartDate AND dc2.ChallanDate <= @EndDate
           AND (@CustomerId IS NULL OR si2.CustomerId = @CustomerId)
           AND dc2.Status IN ('PENDING', 'SCHEDULED', 'DISPATCHED', 'IN_TRANSIT')
         GROUP BY si2.CustomerId, c.CustomerName 
         ORDER BY COUNT(*) DESC) AS MostPendingCustomer,
        (SELECT TOP 1 vm.VehicleNo FROM DeliveryChallans dc2 
         INNER JOIN VehicleMaster vm ON dc2.VehicleId = vm.VehicleId 
         WHERE dc2.ChallanDate >= @StartDate AND dc2.ChallanDate <= @EndDate
           AND (@CustomerId IS NULL OR dc2.SalesInvoiceId IN (SELECT SalesInvoiceId FROM SalesInvoices WHERE CustomerId = @CustomerId))
           AND dc2.Status IN ('PENDING', 'SCHEDULED', 'DISPATCHED', 'IN_TRANSIT')
         GROUP BY dc2.VehicleId, vm.VehicleNo 
         ORDER BY COUNT(*) DESC) AS MostActiveVehicle
    FROM DeliveryChallans dc
    LEFT JOIN SalesInvoices si ON dc.SalesInvoiceId = si.SalesInvoiceId
    LEFT JOIN DeliveryScheduleItems dsi ON dc.ChallanId = dsi.ChallanId
    LEFT JOIN DeliverySchedules ds ON dsi.ScheduleId = ds.ScheduleId
    WHERE dc.ChallanDate >= @StartDate
      AND dc.ChallanDate <= @EndDate
      AND (@CustomerId IS NULL OR si.CustomerId = @CustomerId)
      AND dc.Status IN ('PENDING', 'SCHEDULED', 'DISPATCHED', 'IN_TRANSIT');
END;
