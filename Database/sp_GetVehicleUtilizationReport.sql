CREATE PROCEDURE [dbo].[sp_GetVehicleUtilizationReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @VehicleId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main query to retrieve vehicle utilization data
    SELECT
        vm.VehicleId,
        vm.VehicleNo,
        vm.VehicleType,
        vm.DriverName,
        vm.DriverContact,
        vm.TransporterName,
        vm.IsActive,
        vm.CreatedDate,
        vm.CreatedBy,
        vm.ModifiedDate,
        vm.ModifiedBy,
        vm.Remarks,
        -- Utilization Statistics
        COUNT(ds.ScheduleId) AS TotalSchedules,
        COUNT(CASE WHEN ds.Status = 'SCHEDULED' THEN 1 END) AS ScheduledCount,
        COUNT(CASE WHEN ds.Status = 'DISPATCHED' THEN 1 END) AS DispatchedCount,
        COUNT(CASE WHEN ds.Status = 'IN_TRANSIT' THEN 1 END) AS InTransitCount,
        COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END) AS DeliveredCount,
        COUNT(CASE WHEN ds.Status = 'RETURNED' THEN 1 END) AS ReturnedCount,
        COUNT(CASE WHEN ds.Status = 'CANCELLED' THEN 1 END) AS CancelledCount,
        -- Time Utilization
        SUM(CASE WHEN ds.DispatchDateTime IS NOT NULL AND ds.DeliveredDateTime IS NOT NULL THEN 
            DATEDIFF(hour, ds.DispatchDateTime, ds.DeliveredDateTime) END) AS TotalWorkingHours,
        SUM(CASE WHEN ds.DispatchDateTime IS NOT NULL AND ds.ReturnedDateTime IS NOT NULL THEN 
            DATEDIFF(hour, ds.DispatchDateTime, ds.ReturnedDateTime) END) AS TotalTripHours,
        -- Distance Utilization
        SUM(rm.Distance) AS TotalDistanceCovered,
        AVG(rm.Distance) AS AverageDistancePerTrip,
        -- Route Utilization
        COUNT(DISTINCT ds.RouteId) AS UniqueRoutesUsed,
        -- Items and Customers (simplified to avoid nested aggregates)
        COUNT(ds.ScheduleId) AS TotalSchedulesCount,
        -- Performance Metrics
        CASE 
            WHEN COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END) > 0 THEN
                (COUNT(CASE WHEN ds.Status = 'DELIVERED' AND ds.DeliveredDateTime <= ds.ScheduledDateTime THEN 1 END) * 100.0) / 
                COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END)
            ELSE 0
        END AS OnTimeDeliveryPercentage,
        -- Utilization Rate
        CASE 
            WHEN DATEDIFF(day, @StartDate, @EndDate) > 0 THEN
                (COUNT(ds.ScheduleId) * 100.0) / DATEDIFF(day, @StartDate, @EndDate)
            ELSE 0
        END AS DailyUtilizationRate,
        -- Efficiency Metrics
        CASE 
            WHEN SUM(CASE WHEN ds.DispatchDateTime IS NOT NULL AND ds.DeliveredDateTime IS NOT NULL THEN 
                DATEDIFF(hour, ds.DispatchDateTime, ds.DeliveredDateTime) END) > 0 THEN
                SUM(rm.Distance) / SUM(CASE WHEN ds.DispatchDateTime IS NOT NULL AND ds.DeliveredDateTime IS NOT NULL THEN 
                    DATEDIFF(hour, ds.DispatchDateTime, ds.DeliveredDateTime) END)
            ELSE 0
        END AS AverageSpeedKmPerHour,
        -- First and Last Usage
        MIN(ds.ScheduledDateTime) AS FirstUsageDate,
        MAX(ds.ScheduledDateTime) AS LastUsageDate,
        DATEDIFF(day, MIN(ds.ScheduledDateTime), MAX(ds.ScheduledDateTime)) AS UsagePeriodDays,
        -- Status Description
        CASE 
            WHEN vm.IsActive = 1 THEN 'Active'
            ELSE 'Inactive'
        END AS VehicleStatus,
        -- Utilization Category
        CASE 
            WHEN COUNT(ds.ScheduleId) >= 20 THEN 'High Utilization'
            WHEN COUNT(ds.ScheduleId) >= 10 THEN 'Medium Utilization'
            WHEN COUNT(ds.ScheduleId) >= 5 THEN 'Low Utilization'
            WHEN COUNT(ds.ScheduleId) > 0 THEN 'Very Low Utilization'
            ELSE 'No Utilization'
        END AS UtilizationCategory,
        -- Performance Category
        CASE 
            WHEN COUNT(CASE WHEN ds.Status = 'DELIVERED' AND ds.DeliveredDateTime <= ds.ScheduledDateTime THEN 1 END) >= 
                 COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END) * 0.9 THEN 'Excellent Performance'
            WHEN COUNT(CASE WHEN ds.Status = 'DELIVERED' AND ds.DeliveredDateTime <= ds.ScheduledDateTime THEN 1 END) >= 
                 COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END) * 0.7 THEN 'Good Performance'
            WHEN COUNT(CASE WHEN ds.Status = 'DELIVERED' AND ds.DeliveredDateTime <= ds.ScheduledDateTime THEN 1 END) >= 
                 COUNT(CASE WHEN ds.Status = 'DELIVERED' THEN 1 END) * 0.5 THEN 'Average Performance'
            ELSE 'Poor Performance'
        END AS PerformanceCategory
    FROM VehicleMaster vm
    LEFT JOIN DeliverySchedules ds ON vm.VehicleId = ds.VehicleId 
        AND ds.ScheduledDateTime >= @StartDate 
        AND ds.ScheduledDateTime <= @EndDate
    LEFT JOIN RouteMaster rm ON ds.RouteId = rm.RouteId
    WHERE (@VehicleId IS NULL OR vm.VehicleId = @VehicleId)
    GROUP BY vm.VehicleId, vm.VehicleNo, vm.VehicleType, vm.DriverName, vm.DriverContact, 
             vm.TransporterName, vm.IsActive, vm.CreatedDate, vm.CreatedBy, 
             vm.ModifiedDate, vm.ModifiedBy, vm.Remarks
    ORDER BY TotalWorkingHours DESC, TotalSchedules DESC;

    -- Summary Information (simplified to avoid nested aggregates)
    SELECT
        COUNT(DISTINCT vm.VehicleId) AS TotalVehicles,
        COUNT(CASE WHEN vm.IsActive = 1 THEN 1 END) AS ActiveVehicles,
        COUNT(CASE WHEN vm.IsActive = 0 THEN 1 END) AS InactiveVehicles,
        -- Top Performers
        (SELECT TOP 1 vm2.VehicleNo FROM VehicleMaster vm2 
         INNER JOIN DeliverySchedules ds2 ON vm2.VehicleId = ds2.VehicleId 
         WHERE ds2.ScheduledDateTime >= @StartDate AND ds2.ScheduledDateTime <= @EndDate
           AND (@VehicleId IS NULL OR vm2.VehicleId = @VehicleId)
         GROUP BY vm2.VehicleId, vm2.VehicleNo 
         ORDER BY COUNT(ds2.ScheduleId) DESC) AS MostActiveVehicle
    FROM VehicleMaster vm
    WHERE (@VehicleId IS NULL OR vm.VehicleId = @VehicleId);
END;
