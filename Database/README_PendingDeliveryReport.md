# Pending Delivery Report

## Overview
The Pending Delivery Report provides comprehensive information about pending deliveries, including customer details, payment status, priority levels, and delivery scheduling information.

## Filters
- **Date Range**: Start and end dates for the report period
- **Customer**: Filter by specific customer

## Key Metrics
- Challan Number and Date
- Customer Information
- Invoice Details and Payment Status
- Vehicle and Route Information
- Priority Level (High/Medium/Low)
- Items Count and Total Quantity
- Age Analysis (Challan Age in Days)

## Stored Procedure
- **Name**: `sp_GetPendingDeliveryReport`
- **Parameters**: 
  - `@StartDate` (DATETIME)
  - `@EndDate` (DATETIME)
  - `@CustomerId` (INT, optional)

## Tables Used
- `DeliveryChallans` (main table)
- `SalesInvoices` (invoice information)
- `Customers` (customer information)
- `VehicleMaster` (vehicle information)
- `RouteMaster` (route information)
- `DeliveryScheduleItems` (schedule items)
- `DeliverySchedules` (delivery schedules)

## Report Features
- Professional RDLC layout with company branding
- Filter parameters display
- Totals row with summary calculations
- PDF export functionality
- Responsive design for different screen sizes

## Usage
1. Select date range
2. Optionally filter by customer
3. Click "Generate Report" to view data
4. Use "Export PDF" to save report

## Test Scripts
Run `TestPendingDeliveryReport.sql` to test various filter combinations.
