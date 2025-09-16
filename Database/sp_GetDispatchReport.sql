CREATE PROCEDURE [dbo].[sp_GetDispatchReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @VehicleId INT = NULL,
    @RouteId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve dispatch data
    SELECT
        ds.ScheduleId,
        ds.ScheduleRef,
        ds.ScheduledDateTime,
        ds.DispatchDateTime,
        ds.VehicleId,
        ds.VehicleNo,
        ds.RouteId,
        ds.DriverName,
        ds.DriverContact,
        ds.Status,
        ds.Remarks,
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
        -- Dispatch Analysis
        CASE 
            WHEN ds.Status = 'SCHEDULED' THEN 'Scheduled'
            WHEN ds.Status = 'DISPATCHED' THEN 'Dispatched'
            WHEN ds.Status = 'IN_TRANSIT' THEN 'In Transit'
            WHEN ds.Status = 'DELIVERED' THEN 'Delivered'
            WHEN ds.Status = 'RETURNED' THEN 'Returned'
            WHEN ds.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE 'Unknown Status'
        END AS StatusDescription,
        -- Dispatch Performance
        CASE 
            WHEN ds.DispatchDateTime IS NOT NULL AND ds.ScheduledDateTime IS NOT NULL THEN
                DATEDIFF(minute, ds.ScheduledDateTime, ds.DispatchDateTime)
            ELSE NULL
        END AS DispatchDelayMinutes,
        CASE 
            WHEN ds.DispatchDateTime IS NOT NULL AND ds.ScheduledDateTime IS NOT NULL THEN
                CASE 
                    WHEN ds.DispatchDateTime <= ds.ScheduledDateTime THEN 'On Time'
                    WHEN DATEDIFF(hour, ds.ScheduledDateTime, ds.DispatchDateTime) <= 1 THEN 'Slightly Delayed'
                    WHEN DATEDIFF(hour, ds.ScheduledDateTime, ds.DispatchDateTime) <= 4 THEN 'Delayed'
                    ELSE 'Severely Delayed'
                END
            ELSE 'Not Dispatched'
        END AS DispatchPerformance,
        -- Items and Customers
        (SELECT COUNT(*) FROM DeliveryScheduleItems WHERE ScheduleId = ds.ScheduleId) AS ItemsCount,
        (SELECT COUNT(DISTINCT dc.SalesInvoiceId) FROM DeliveryScheduleItems dsi 
         INNER JOIN DeliveryChallans dc ON dsi.ChallanId = dc.ChallanId 
         WHERE dsi.ScheduleId = ds.ScheduleId) AS CustomersCount,
        -- Delivery Challans Summary
        (SELECT COUNT(DISTINCT dsi.ChallanId) FROM DeliveryScheduleItems dsi 
         WHERE dsi.ScheduleId = ds.ScheduleId) AS ChallansCount,
        -- Route Efficiency
        CASE 
            WHEN ds.DispatchDateTime IS NOT NULL AND rm.Distance > 0 THEN
                rm.Distance / (DATEDIFF(hour, ds.DispatchDateTime, GETDATE()) + 0.1)
            ELSE NULL
        END AS RouteEfficiency,
        -- Dispatch Age
        CASE 
            WHEN ds.DispatchDateTime IS NOT NULL THEN
                DATEDIFF(hour, ds.DispatchDateTime, GETDATE())
            ELSE NULL
        END AS DispatchAgeHours,
        -- Schedule Age
        DATEDIFF(day, ds.ScheduledDateTime, GETDATE()) AS ScheduleAgeDays
    FROM DeliverySchedules ds
    LEFT JOIN VehicleMaster vm ON ds.VehicleId = vm.VehicleId
    LEFT JOIN RouteMaster rm ON ds.RouteId = rm.RouteId
    WHERE ds.ScheduledDateTime >= @StartDate
      AND ds.ScheduledDateTime <= @EndDate
      AND (@VehicleId IS NULL OR ds.VehicleId = @VehicleId)
      AND (@RouteId IS NULL OR ds.RouteId = @RouteId)
    ORDER BY ds.ScheduledDateTime DESC, ds.ScheduleRef;

    -- Summary Information
    SELECT
        COUNT(*) AS TotalDispatches,
        COUNT(DISTINCT ds.VehicleId) AS TotalVehicles,
        COUNT(DISTINCT ds.RouteId) AS TotalRoutes,
        -- Status Summary
        COUNT(CASE WHEN ds.Status = 'SCHEDULED' THEN 1 END) AS ScheduledCount,
        COUNT(CASE WHEN ds.Status = 'DISPATCHED' THEN 1 END) AS DispatchedCount,
        COUNT(CASE WHEN ds.Status = 'IN_TRANSIT' THEN 1 END) AS InTransitCount,
        COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END) AS DeliveredCount,
        COUNT(CASE WHEN ds.Status = 'RETURNED' THEN 1 END) AS ReturnedCount,
        COUNT(CASE WHEN ds.Status = 'CANCELLED' THEN 1 END) AS CancelledCount,
        -- Dispatch Performance
        COUNT(CASE WHEN ds.DispatchDateTime IS NOT NULL AND ds.DispatchDateTime <= ds.ScheduledDateTime THEN 1 END) AS OnTimeDispatches,
        COUNT(CASE WHEN ds.DispatchDateTime IS NOT NULL AND ds.DispatchDateTime > ds.ScheduledDateTime THEN 1 END) AS DelayedDispatches,
        -- Items and Customers Summary (using subqueries to avoid nested aggregates)
        (SELECT COUNT(*) FROM DeliveryScheduleItems dsi 
         INNER JOIN DeliverySchedules ds2 ON dsi.ScheduleId = ds2.ScheduleId
         WHERE ds2.ScheduledDateTime >= @StartDate AND ds2.ScheduledDateTime <= @EndDate
           AND (@VehicleId IS NULL OR ds2.VehicleId = @VehicleId)
           AND (@RouteId IS NULL OR ds2.RouteId = @RouteId)) AS TotalItems,
        (SELECT COUNT(DISTINCT dc.SalesInvoiceId) FROM DeliveryScheduleItems dsi 
         INNER JOIN DeliverySchedules ds2 ON dsi.ScheduleId = ds2.ScheduleId
         INNER JOIN DeliveryChallans dc ON dsi.ChallanId = dc.ChallanId 
         WHERE ds2.ScheduledDateTime >= @StartDate AND ds2.ScheduledDateTime <= @EndDate
           AND (@VehicleId IS NULL OR ds2.VehicleId = @VehicleId)
           AND (@RouteId IS NULL OR ds2.RouteId = @RouteId)) AS TotalCustomers,
        (SELECT COUNT(DISTINCT dsi.ChallanId) FROM DeliveryScheduleItems dsi 
         INNER JOIN DeliverySchedules ds2 ON dsi.ScheduleId = ds2.ScheduleId
         WHERE ds2.ScheduledDateTime >= @StartDate AND ds2.ScheduledDateTime <= @EndDate
           AND (@VehicleId IS NULL OR ds2.VehicleId = @VehicleId)
           AND (@RouteId IS NULL OR ds2.RouteId = @RouteId)) AS TotalChallans,
        -- Time Analysis
        AVG(CASE WHEN ds.DispatchDateTime IS NOT NULL AND ds.ScheduledDateTime IS NOT NULL THEN 
            DATEDIFF(minute, ds.ScheduledDateTime, ds.DispatchDateTime) END) AS AverageDispatchDelayMinutes,
        -- Top Performers
        (SELECT TOP 1 vm.VehicleNo FROM DeliverySchedules ds2 
         INNER JOIN VehicleMaster vm ON ds2.VehicleId = vm.VehicleId 
         WHERE ds2.ScheduledDateTime >= @StartDate AND ds2.ScheduledDateTime <= @EndDate
           AND (@VehicleId IS NULL OR ds2.VehicleId = @VehicleId)
           AND (@RouteId IS NULL OR ds2.RouteId = @RouteId)
         GROUP BY ds2.VehicleId, vm.VehicleNo 
         ORDER BY COUNT(*) DESC) AS MostActiveVehicle,
        (SELECT TOP 1 rm.RouteName FROM DeliverySchedules ds2 
         INNER JOIN RouteMaster rm ON ds2.RouteId = rm.RouteId 
         WHERE ds2.ScheduledDateTime >= @StartDate AND ds2.ScheduledDateTime <= @EndDate
           AND (@VehicleId IS NULL OR ds2.VehicleId = @VehicleId)
           AND (@RouteId IS NULL OR ds2.RouteId = @RouteId)
         GROUP BY ds2.RouteId, rm.RouteName 
         ORDER BY COUNT(*) DESC) AS MostActiveRoute,
        -- Efficiency Metrics
        CASE 
            WHEN COUNT(CASE WHEN ds.DispatchDateTime IS NOT NULL THEN 1 END) > 0 THEN
                (COUNT(CASE WHEN ds.DispatchDateTime IS NOT NULL AND ds.DispatchDateTime <= ds.ScheduledDateTime THEN 1 END) * 100.0) / 
                COUNT(CASE WHEN ds.DispatchDateTime IS NOT NULL THEN 1 END)
            ELSE 0
        END AS OnTimeDispatchPercentage
    FROM DeliverySchedules ds
    WHERE ds.ScheduledDateTime >= @StartDate
      AND ds.ScheduledDateTime <= @EndDate
      AND (@VehicleId IS NULL OR ds.VehicleId = @VehicleId)
      AND (@RouteId IS NULL OR ds.RouteId = @RouteId);
END;
