-- Test script for Pending Delivery Report
-- This script tests the sp_GetPendingDeliveryReport stored procedure

-- Test 1: All pending deliveries for the last 30 days
EXEC sp_GetPendingDeliveryReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @CustomerId = NULL;

-- Test 2: Filter by specific customer
EXEC sp_GetPendingDeliveryReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @CustomerId = 1;

-- Test 3: Recent pending deliveries (last 7 days)
EXEC sp_GetPendingDeliveryReport 
    @StartDate = '2025-01-24',
    @EndDate = '2025-01-31',
    @CustomerId = NULL;
