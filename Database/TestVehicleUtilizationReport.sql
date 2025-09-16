-- Test script for Vehicle Utilization Report
-- This script tests the sp_GetVehicleUtilizationReport stored procedure

-- Test 1: All vehicles utilization for the last 30 days
EXEC sp_GetVehicleUtilizationReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @VehicleId = NULL;

-- Test 2: Specific vehicle utilization
EXEC sp_GetVehicleUtilizationReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @VehicleId = 1;

-- Test 3: Weekly utilization
EXEC sp_GetVehicleUtilizationReport 
    @StartDate = '2025-01-24',
    @EndDate = '2025-01-31',
    @VehicleId = NULL;
