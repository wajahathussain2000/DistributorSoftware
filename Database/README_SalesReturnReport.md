# Sales Return Report

## Overview
The Sales Return Report provides comprehensive information about sales returns with filtering capabilities for customers, salesmen, and date ranges. This report helps track return patterns, analyze return reasons, and monitor return processing status.

## Features
- **Customer Filter**: Filter returns by specific customer or view all customers
- **Salesman Filter**: Filter returns by specific salesman or view all salesmen  
- **Date Range Filter**: Filter returns within a specific date range
- **Comprehensive Data**: Includes return details, customer information, salesman details, reference invoice information, and processing status
- **Export Capability**: Export reports to PDF format
- **Summary Statistics**: Provides summary information including totals and counts

## Database Components

### Stored Procedure: `sp_GetSalesReturnReport`
**Parameters:**
- `@StartDate` (DATETIME): Start date for the report period
- `@EndDate` (DATETIME): End date for the report period
- `@CustomerId` (INT, Optional): Filter by specific customer ID (NULL for all customers)
- `@SalesmanId` (INT, Optional): Filter by specific salesman ID (NULL for all salesmen)

**Returns:**
- Main dataset: Detailed sales return information
- Summary dataset: Aggregated statistics and totals

### Data Model: `SalesReturnReportData`
Contains all fields returned by the stored procedure including:
- Return identification (ReturnId, ReturnNumber, ReturnBarcode)
- Customer information (CustomerId, CustomerCode, CustomerName, CustomerPhone, CustomerAddress)
- Salesman information (SalesmanId, SalesmanCode, SalesmanName, SalesmanPhone, Territory)
- Reference invoice details (ReferenceSalesInvoiceId, ReferenceInvoiceNumber, ReferenceInvoiceDate)
- Return details (ReturnDate, Reason, SubTotal, DiscountAmount, TaxAmount, TotalAmount)
- Status information (Status, StatusDescription, Remarks)
- Processing information (CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, ProcessedDate, ProcessedBy, ApprovedDate, ApprovedBy)
- Tax information (TaxCategoryId, TaxCategoryName)
- Calculated fields (DiscountPercentage, TaxPercentage, DaysSinceReturn, ProcessingDays, TotalItems, TotalQuantity, ItemsTotalAmount)

## Report Layout (RDLC)
The report displays data in a tabular format with the following columns:
- Return No.
- Date
- Customer
- Salesman
- Reference Invoice
- Reason
- Sub Total
- Discount
- Tax
- Total
- Status

## User Interface
- **Form**: `SalesReturnReportForm`
- **Filters**: Customer dropdown, Salesman dropdown, Start Date, End Date
- **Actions**: Generate Report, Export PDF, Close
- **Report Viewer**: Integrated ReportViewer control for displaying the RDLC report

## Integration
- Added to the Reports dropdown menu in `AdminDashboardRedesigned`
- Accessible via: Reports → Sales Return Report

## Usage Instructions
1. Open the Sales Return Report from the Reports menu
2. Select filters as needed:
   - Choose a specific customer or "All Customers"
   - Choose a specific salesman or "All Salesmen"
   - Set the date range for the report period
3. Click "Generate Report" to display the report
4. Use "Export PDF" to save the report as a PDF file
5. Click "Close" to exit the report

## Test Data
Use `TestSalesReturnReport.sql` to test the stored procedure with various parameter combinations.

## Technical Notes
- The report links sales returns to their reference sales invoices to get salesman information
- Status descriptions are calculated fields that provide user-friendly status text
- The report handles cases where salesman information might not be available
- Empty result sets display a "No data found" message
- All monetary values are formatted to 2 decimal places
- Dates are displayed in dd/MM/yyyy format
