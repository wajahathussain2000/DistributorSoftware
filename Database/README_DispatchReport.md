# Dispatch Report

## Overview
The Dispatch Report provides detailed information about dispatch operations, including vehicle assignments, route details, and dispatch performance metrics.

## Filters
- **Date Range**: Start and end dates for the report period
- **Vehicle**: Filter by specific vehicle
- **Route**: Filter by specific delivery route

## Key Metrics
- Schedule Reference and Status
- Vehicle and Driver Information
- Route Details and Distance
- Dispatch Times and Performance
- Items, Customers, and Challans Count
- Dispatch Age Analysis

## Stored Procedure
- **Name**: `sp_GetDispatchReport`
- **Parameters**: 
  - `@StartDate` (DATETIME)
  - `@EndDate` (DATETIME)
  - `@VehicleId` (INT, optional)
  - `@RouteId` (INT, optional)

## Tables Used
- `DeliverySchedules` (main table)
- `VehicleMaster` (vehicle information)
- `RouteMaster` (route information)
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
2. Optionally filter by vehicle or route
3. Click "Generate Report" to view data
4. Use "Export PDF" to save report

## Test Scripts
Run `TestDispatchReport.sql` to test various filter combinations.
