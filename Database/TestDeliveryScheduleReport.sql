-- Test script for Delivery Schedule Report
-- This script tests the sp_GetDeliveryScheduleReport stored procedure

-- Test 1: All schedules for the last 30 days
EXEC sp_GetDeliveryScheduleReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @RouteId = NULL,
    @VehicleId = NULL,
    @SalesmanId = NULL;

-- Test 2: Filter by specific route
EXEC sp_GetDeliveryScheduleReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @RouteId = 1,
    @VehicleId = NULL,
    @SalesmanId = NULL;

-- Test 3: Filter by specific vehicle
EXEC sp_GetDeliveryScheduleReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @RouteId = NULL,
    @VehicleId = 1,
    @SalesmanId = NULL;

-- Test 4: Filter by specific salesman
EXEC sp_GetDeliveryScheduleReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @RouteId = NULL,
    @VehicleId = NULL,
    @SalesmanId = 1;

-- Test 5: Combined filters
EXEC sp_GetDeliveryScheduleReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @RouteId = 1,
    @VehicleId = 1,
    @SalesmanId = 1;
