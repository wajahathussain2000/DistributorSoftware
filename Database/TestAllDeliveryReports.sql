-- Test all delivery reports to ensure they work with the seed data

PRINT 'Testing Delivery Schedule Report...';
EXEC sp_GetDeliveryScheduleReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @RouteId = NULL,
    @VehicleId = NULL,
    @SalesmanId = NULL;

PRINT 'Testing Dispatch Report...';
EXEC sp_GetDispatchReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @VehicleId = NULL,
    @RouteId = NULL;

PRINT 'Testing Pending Delivery Report...';
EXEC sp_GetPendingDeliveryReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @CustomerId = NULL;

PRINT 'Testing Vehicle Utilization Report...';
EXEC sp_GetVehicleUtilizationReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @VehicleId = NULL;

PRINT 'All tests completed successfully!';
