CREATE PROCEDURE [dbo].[sp_GetDeliveryScheduleReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @RouteId INT = NULL,
    @VehicleId INT = NULL,
    @SalesmanId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve delivery schedule data
    SELECT
        ds.ScheduleId,
        ds.ScheduleRef,
        ds.ScheduledDateTime,
        ds.VehicleId,
        ds.VehicleNo,
        ds.RouteId,
        ds.DriverName,
        ds.DriverContact,
        ds.Status,
        ds.DispatchDateTime,
        ds.DeliveredDateTime,
        ds.ReturnedDateTime,
        ds.Remarks,
        ds.CreatedDate,
        ds.CreatedBy,
        ds.ModifiedDate,
        ds.ModifiedBy,
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
        -- Salesman Information
        sm.SalesmanId,
        sm.SalesmanCode,
        sm.SalesmanName,
        sm.Email AS SalesmanEmail,
        sm.Phone AS SalesmanPhone,
        sm.Territory AS SalesmanTerritory,
        sm.CommissionRate,
        -- Schedule Analysis
        CASE 
            WHEN ds.Status = 'SCHEDULED' THEN 'Scheduled'
            WHEN ds.Status = 'DISPATCHED' THEN 'Dispatched'
            WHEN ds.Status = 'IN_TRANSIT' THEN 'In Transit'
            WHEN ds.Status = 'DELIVERED' THEN 'Delivered'
            WHEN ds.Status = 'RETURNED' THEN 'Returned'
            WHEN ds.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE 'Unknown Status'
        END AS StatusDescription,
        -- Time Analysis
        CASE 
            WHEN ds.DispatchDateTime IS NOT NULL AND ds.ScheduledDateTime IS NOT NULL THEN
                DATEDIFF(minute, ds.ScheduledDateTime, ds.DispatchDateTime)
            ELSE NULL
        END AS DispatchDelayMinutes,
        CASE 
            WHEN ds.DeliveredDateTime IS NOT NULL AND ds.DispatchDateTime IS NOT NULL THEN
                DATEDIFF(minute, ds.DispatchDateTime, ds.DeliveredDateTime)
            ELSE NULL
        END AS DeliveryTimeMinutes,
        CASE 
            WHEN ds.ReturnedDateTime IS NOT NULL AND ds.DeliveredDateTime IS NOT NULL THEN
                DATEDIFF(minute, ds.DeliveredDateTime, ds.ReturnedDateTime)
            ELSE NULL
        END AS ReturnTimeMinutes,
        -- Schedule Age
        DATEDIFF(day, ds.ScheduledDateTime, GETDATE()) AS ScheduleAgeDays,
        -- Delivery Items Count
        (SELECT COUNT(*) FROM DeliveryScheduleItems WHERE ScheduleId = ds.ScheduleId) AS ItemsCount,
        (SELECT COUNT(DISTINCT dc.SalesInvoiceId) FROM DeliveryScheduleItems dsi 
         INNER JOIN DeliveryChallans dc ON dsi.ChallanId = dc.ChallanId 
         WHERE dsi.ScheduleId = ds.ScheduleId) AS CustomersCount,
        -- Performance Metrics
        CASE 
            WHEN ds.Status = 'DELIVERED' AND ds.DeliveredDateTime IS NOT NULL AND ds.ScheduledDateTime IS NOT NULL THEN
                CASE 
                    WHEN ds.DeliveredDateTime <= ds.ScheduledDateTime THEN 'On Time'
                    WHEN DATEDIFF(hour, ds.ScheduledDateTime, ds.DeliveredDateTime) <= 2 THEN 'Slightly Delayed'
                    WHEN DATEDIFF(hour, ds.ScheduledDateTime, ds.DeliveredDateTime) <= 24 THEN 'Delayed'
                    ELSE 'Severely Delayed'
                END
            ELSE 'Not Delivered'
        END AS DeliveryPerformance,
        -- Route Efficiency
        CASE 
            WHEN ds.DeliveredDateTime IS NOT NULL AND ds.DispatchDateTime IS NOT NULL AND rm.Distance > 0 THEN
                rm.Distance / (DATEDIFF(hour, ds.DispatchDateTime, ds.DeliveredDateTime) + 0.1)
            ELSE NULL
        END AS RouteEfficiency,
        -- Vehicle Utilization
        CASE 
            WHEN ds.DeliveredDateTime IS NOT NULL AND ds.DispatchDateTime IS NOT NULL THEN
                DATEDIFF(hour, ds.DispatchDateTime, ds.DeliveredDateTime)
            ELSE NULL
        END AS VehicleUtilizationHours
    FROM DeliverySchedules ds
    LEFT JOIN VehicleMaster vm ON ds.VehicleId = vm.VehicleId
    LEFT JOIN RouteMaster rm ON ds.RouteId = rm.RouteId
    LEFT JOIN Salesman sm ON ds.CreatedBy = sm.SalesmanId
    WHERE ds.ScheduledDateTime >= @StartDate
      AND ds.ScheduledDateTime <= @EndDate
      AND (@RouteId IS NULL OR ds.RouteId = @RouteId)
      AND (@VehicleId IS NULL OR ds.VehicleId = @VehicleId)
      AND (@SalesmanId IS NULL OR ds.CreatedBy = @SalesmanId)
    ORDER BY ds.ScheduledDateTime DESC, ds.ScheduleRef;

    -- Summary Information
    SELECT
        COUNT(*) AS TotalSchedules,
        COUNT(DISTINCT ds.VehicleId) AS TotalVehicles,
        COUNT(DISTINCT ds.RouteId) AS TotalRoutes,
        COUNT(DISTINCT ds.CreatedBy) AS TotalSalesmen,
        -- Status Summary
        COUNT(CASE WHEN ds.Status = 'SCHEDULED' THEN 1 END) AS ScheduledCount,
        COUNT(CASE WHEN ds.Status = 'DISPATCHED' THEN 1 END) AS DispatchedCount,
        COUNT(CASE WHEN ds.Status = 'IN_TRANSIT' THEN 1 END) AS InTransitCount,
        COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END) AS DeliveredCount,
        COUNT(CASE WHEN ds.Status = 'RETURNED' THEN 1 END) AS ReturnedCount,
        COUNT(CASE WHEN ds.Status = 'CANCELLED' THEN 1 END) AS CancelledCount,
        -- Performance Summary
        COUNT(CASE WHEN ds.Status = 'DELIVERED' AND ds.DeliveredDateTime <= ds.ScheduledDateTime THEN 1 END) AS OnTimeDeliveries,
        COUNT(CASE WHEN ds.Status = 'DELIVERED' AND ds.DeliveredDateTime > ds.ScheduledDateTime THEN 1 END) AS DelayedDeliveries,
        -- Time Analysis
        AVG(CASE WHEN ds.DispatchDateTime IS NOT NULL AND ds.ScheduledDateTime IS NOT NULL THEN 
            DATEDIFF(minute, ds.ScheduledDateTime, ds.DispatchDateTime) END) AS AverageDispatchDelayMinutes,
        AVG(CASE WHEN ds.DeliveredDateTime IS NOT NULL AND ds.DispatchDateTime IS NOT NULL THEN 
            DATEDIFF(minute, ds.DispatchDateTime, ds.DeliveredDateTime) END) AS AverageDeliveryTimeMinutes,
        -- Vehicle Utilization
        SUM(CASE WHEN ds.DeliveredDateTime IS NOT NULL AND ds.DispatchDateTime IS NOT NULL THEN 
            DATEDIFF(hour, ds.DispatchDateTime, ds.DeliveredDateTime) END) AS TotalVehicleHours,
        -- Top Performers
        (SELECT TOP 1 vm.VehicleNo FROM DeliverySchedules ds2 
         INNER JOIN VehicleMaster vm ON ds2.VehicleId = vm.VehicleId 
         WHERE ds2.ScheduledDateTime >= @StartDate AND ds2.ScheduledDateTime <= @EndDate
           AND (@RouteId IS NULL OR ds2.RouteId = @RouteId)
           AND (@VehicleId IS NULL OR ds2.VehicleId = @VehicleId)
           AND (@SalesmanId IS NULL OR ds2.CreatedBy = @SalesmanId)
         GROUP BY ds2.VehicleId, vm.VehicleNo 
         ORDER BY COUNT(*) DESC) AS MostActiveVehicle,
        (SELECT TOP 1 rm.RouteName FROM DeliverySchedules ds2 
         INNER JOIN RouteMaster rm ON ds2.RouteId = rm.RouteId 
         WHERE ds2.ScheduledDateTime >= @StartDate AND ds2.ScheduledDateTime <= @EndDate
           AND (@RouteId IS NULL OR ds2.RouteId = @RouteId)
           AND (@VehicleId IS NULL OR ds2.VehicleId = @VehicleId)
           AND (@SalesmanId IS NULL OR ds2.CreatedBy = @SalesmanId)
         GROUP BY ds2.RouteId, rm.RouteName 
         ORDER BY COUNT(*) DESC) AS MostActiveRoute,
        -- Efficiency Metrics
        CASE 
            WHEN COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END) > 0 THEN
                (COUNT(CASE WHEN ds.Status = 'DELIVERED' AND ds.DeliveredDateTime <= ds.ScheduledDateTime THEN 1 END) * 100.0) / 
                COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END)
            ELSE 0
        END AS OnTimeDeliveryPercentage
    FROM DeliverySchedules ds
    WHERE ds.ScheduledDateTime >= @StartDate
      AND ds.ScheduledDateTime <= @EndDate
      AND (@RouteId IS NULL OR ds.RouteId = @RouteId)
      AND (@VehicleId IS NULL OR ds.VehicleId = @VehicleId)
      AND (@SalesmanId IS NULL OR ds.CreatedBy = @SalesmanId);
END;
