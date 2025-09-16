# Delivery Schedule Report

## Overview
The Delivery Schedule Report provides comprehensive information about delivery schedules, including vehicle assignments, route details, driver information, and performance metrics.

## Filters
- **Date Range**: Start and end dates for the report period
- **Route**: Filter by specific delivery route
- **Vehicle**: Filter by specific vehicle
- **Salesman**: Filter by specific salesman who created the schedule

## Key Metrics
- Schedule Reference and Status
- Vehicle and Driver Information
- Route Details and Distance
- Dispatch and Delivery Times
- Performance Analysis (On-time delivery percentage)
- Items and Customers Count
- Vehicle Utilization Hours

## Stored Procedure
- **Name**: `sp_GetDeliveryScheduleReport`
- **Parameters**: 
  - `@StartDate` (DATETIME)
  - `@EndDate` (DATETIME)
  - `@RouteId` (INT, optional)
  - `@VehicleId` (INT, optional)
  - `@SalesmanId` (INT, optional)

## Tables Used
- `DeliverySchedules` (main table)
- `VehicleMaster` (vehicle information)
- `RouteMaster` (route information)
- `Salesman` (salesman information)
- `DeliveryScheduleItems` (schedule items)
- `DeliveryChallans` (delivery challans)

## Report Features
- Professional RDLC layout with company branding
- Filter parameters display
- Totals row with summary calculations
- PDF export functionality
- Responsive design for different screen sizes

## Usage
1. Select date range
2. Optionally filter by route, vehicle, or salesman
3. Click "Generate Report" to view data
4. Use "Export PDF" to save report

## Test Scripts
Run `TestDeliveryScheduleReport.sql` to test various filter combinations.
