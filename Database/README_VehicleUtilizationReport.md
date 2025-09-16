# Vehicle Utilization Report

## Overview
The Vehicle Utilization Report provides comprehensive analysis of vehicle usage, including utilization statistics, performance metrics, and efficiency analysis.

## Filters
- **Date Range**: Start and end dates for the report period
- **Vehicle**: Filter by specific vehicle

## Key Metrics
- Vehicle Information (Type, Driver, Transporter)
- Utilization Statistics (Total Schedules, Delivered Count)
- Time Utilization (Working Hours, Trip Hours)
- Distance Utilization (Total Distance, Average Distance per Trip)
- Route Utilization (Unique Routes Used)
- Performance Metrics (On-time Delivery Percentage)
- Utilization and Performance Categories

## Stored Procedure
- **Name**: `sp_GetVehicleUtilizationReport`
- **Parameters**: 
  - `@StartDate` (DATETIME)
  - `@EndDate` (DATETIME)
  - `@VehicleId` (INT, optional)

## Tables Used
- `VehicleMaster` (main table)
- `DeliverySchedules` (schedule information)
- `RouteMaster` (route information)

## Report Features
- Professional RDLC layout with company branding
- Filter parameters display
- Totals row with summary calculations
- PDF export functionality
- Responsive design for different screen sizes

## Usage
1. Select date range
2. Optionally filter by specific vehicle
3. Click "Generate Report" to view data
4. Use "Export PDF" to save report

## Test Scripts
Run `TestVehicleUtilizationReport.sql` to test various filter combinations.
