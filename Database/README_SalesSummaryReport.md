# Sales Summary Report

## Overview
The Sales Summary Report provides comprehensive analysis of sales transactions with detailed product-level information, customer insights, salesman performance, and profitability metrics. This report helps track sales performance, analyze product trends, and monitor business profitability.

## Features
- **Product Filter**: Filter sales by specific product or view all products
- **Customer Filter**: Filter sales by specific customer or view all customers
- **Salesman Filter**: Filter sales by specific salesman or view all salesmen
- **Date Range Filter**: Filter sales within a specific date range
- **Comprehensive Data**: Includes invoice details, product information, customer details, salesman information, and profitability metrics
- **Export Capability**: Export reports to PDF format
- **Summary Statistics**: Provides aggregated sales data, profitability analysis, and performance metrics

## Database Components

### Stored Procedure: `sp_GetSalesSummaryReport`
**Parameters:**
- `@StartDate` (DATETIME): Start date for the report period
- `@EndDate` (DATETIME): End date for the report period
- `@ProductId` (INT, Optional): Filter by specific product ID (NULL for all products)
- `@CustomerId` (INT, Optional): Filter by specific customer ID (NULL for all customers)
- `@SalesmanId` (INT, Optional): Filter by specific salesman ID (NULL for all salesmen)

**Returns:**
- Main dataset: Detailed sales summary information with product-level details
- Summary dataset: Aggregated statistics, profitability analysis, and performance metrics

### Data Model: `SalesSummaryReportData`
Contains all fields returned by the stored procedure including:
- Invoice identification (DetailId, SalesInvoiceId, InvoiceNumber, InvoiceDate, DueDate)
- Customer information (CustomerId, CustomerCode, CustomerName, CustomerPhone, CustomerAddress)
- Salesman information (SalesmanId, SalesmanCode, SalesmanName, SalesmanPhone, Territory)
- Product information (ProductId, ProductCode, ProductName, ProductDescription, ProductCategory, BrandId, BrandName, CategoryId, CategoryName, UnitId, UnitName, ProductBarcode)
- Sales details (Quantity, UnitPrice, TaxPercentage, TaxAmount, DiscountPercentage, DiscountAmount, TaxableAmount, TotalAmount, LineTotal, BatchNumber, ExpiryDate, LineRemarks)
- Invoice information (InvoiceSubTotal, InvoiceDiscountAmount, InvoiceTaxAmount, InvoiceTotalAmount, PaidAmount, BalanceAmount, PaymentMode, InvoiceStatus, InvoiceRemarks)
- Processing information (InvoiceCreatedDate, InvoiceCreatedBy, InvoiceCreatedByUser, InvoiceModifiedDate, InvoiceModifiedBy, InvoiceModifiedByUser, ChangeAmount, InvoiceBarcode, PrintStatus, PrintDate, CashierId, CashierName, TransactionTime)
- Calculated fields (PaymentStatus, PaymentPercentage, DaysSinceInvoice, AverageUnitPrice, ProfitMarginPercentage, ProfitAmount, SalesValueCategory, SalesQuantityCategory)

## Report Layout (RDLC)
The report displays data in a comprehensive tabular format with the following columns:
- Invoice No.
- Date
- Customer
- Salesman
- Product
- Qty
- Unit Price
- Discount
- Tax
- Total
- Status
- Profit

## User Interface
- **Form**: `SalesSummaryReportForm`
- **Filters**: Product dropdown, Customer dropdown, Salesman dropdown, Start Date, End Date
- **Actions**: Generate Report, Export PDF, Close
- **Report Viewer**: Integrated ReportViewer control for displaying the RDLC report

## Integration
- Added to the Reports dropdown menu in `AdminDashboardRedesigned`
- Accessible via: Reports → Sales Summary Report

## Usage Instructions
1. Open the Sales Summary Report from the Reports menu
2. Select filters as needed:
   - Choose a specific product or "All Products"
   - Choose a specific customer or "All Customers"
   - Choose a specific salesman or "All Salesmen"
   - Set the date range for the report period
3. Click "Generate Report" to display the report
4. Use "Export PDF" to save the report as a PDF file
5. Click "Close" to exit the report

## Key Features
- **Product Performance Analysis**: Track sales performance by individual products
- **Customer Insights**: Analyze customer purchasing patterns and preferences
- **Salesman Performance**: Monitor salesman effectiveness and territory performance
- **Profitability Analysis**: Calculate profit margins and amounts for each sale
- **Sales Value Categorization**: Classify sales as High Value, Medium Value, or Low Value
- **Sales Quantity Analysis**: Categorize sales as Bulk Sale, Medium Sale, or Small Sale
- **Comprehensive Summary**: Provides totals, averages, and top performers

## Test Data
Use `TestSalesSummaryReport.sql` to test the stored procedure with various parameter combinations.

## Technical Notes
- The report joins SalesInvoiceDetails with SalesInvoices to provide comprehensive sales data
- Product information is linked through Products, Brands, ProductCategories, and Units tables
- Profit calculations are based on the difference between sale price and purchase price
- Sales value and quantity categories are automatically calculated based on predefined thresholds
- The report handles cases where salesman information might not be available
- Empty result sets display a "No data found" message
- All monetary values are formatted to 2 decimal places
- Dates are displayed in dd/MM/yyyy format
- The report provides both detailed line-item data and summary statistics
