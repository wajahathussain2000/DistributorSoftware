-- Test script for Dispatch Report
-- This script tests the sp_GetDispatchReport stored procedure

-- Test 1: All dispatches for the last 30 days
EXEC sp_GetDispatchReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @VehicleId = NULL,
    @RouteId = NULL;

-- Test 2: Filter by specific vehicle
EXEC sp_GetDispatchReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @VehicleId = 1,
    @RouteId = NULL;

-- Test 3: Filter by specific route
EXEC sp_GetDispatchReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @VehicleId = NULL,
    @RouteId = 1;

-- Test 4: Combined filters
EXEC sp_GetDispatchReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @VehicleId = 1,
    @RouteId = 1;
