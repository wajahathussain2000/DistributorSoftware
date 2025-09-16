# Customer-wise Sales Report

## Overview
The Customer-wise Sales Report provides comprehensive analysis of sales performance by individual customers, including payment behavior, customer categorization, and relationship analysis.

## Features

### 📊 **Report Filters**
- **Customer**: Select specific customer or "All Customers"
- **Date Range**: Start Date and End Date for analysis period

### 📈 **Report Data**
- **Customer Information**: Code, Name, Contact Details, Location
- **Sales Aggregation**: Total invoices, line items, quantities, amounts
- **Payment Analysis**: Paid, pending, cancelled, and overdue amounts
- **Customer Categorization**: 
  - Value Category (High/Medium/Low/Very Low Value Customer)
  - Frequency Category (High/Medium/Low/Very Low Frequency Customer)
  - Payment Behavior Category (Excellent/Good/Average/Poor Payer)
- **Performance Metrics**: Unique salesmen, products purchased, relationship duration
- **Credit Analysis**: Credit utilization percentage and limits

### 🎯 **Key Metrics**
- **Total Sales Amount**: Sum of all sales to the customer
- **Total Invoices Count**: Number of invoices generated
- **Payment Status**: Breakdown of paid, pending, and overdue amounts
- **Customer Value**: Automatic categorization based on sales volume
- **Payment Behavior**: Analysis of payment patterns and reliability

## Database Components

### Stored Procedure
- **Name**: `sp_GetCustomerWiseSalesReport`
- **Parameters**:
  - `@StartDate DATETIME`: Start date for analysis
  - `@EndDate DATETIME`: End date for analysis
  - `@CustomerId INT`: Optional customer ID filter (NULL for all customers)

### Data Model
- **File**: `CustomerWiseSalesReportData.cs`
- **Namespace**: `DistributionSoftware.Models`

### Report Definition
- **File**: `CustomerWiseSalesReport.rdlc`
- **Location**: `DistributionSoftware/Reports/`

### User Interface
- **Form**: `CustomerWiseSalesReportForm.cs`
- **Designer**: `CustomerWiseSalesReportForm.Designer.cs`
- **Location**: `DistributionSoftware/Presentation/Forms/`

## Usage

### Accessing the Report
1. Open the Distribution Software application
2. Navigate to **Reports** dropdown menu
3. Select **Customer-wise Sales Report**

### Using Filters
1. **Customer Filter**: Choose specific customer or "All Customers"
2. **Date Range**: Set start and end dates for analysis
3. Click **Generate Report** to view results

### Export Options
- **Export PDF**: Save report as PDF file
- **Print**: Print the report directly

## Report Sections

### Main Data Table
- Customer Code and Name
- Contact Information (Phone, City)
- Sales Metrics (Invoices, Sales Amount, Paid Amount, Balance Amount)
- Overdue Analysis
- Customer Categorization (Value and Payment Behavior)

### Summary Totals
- Total Invoices Count
- Total Sales Amount
- Total Paid Amount
- Total Balance Amount
- Total Overdue Amount

## Customer Categorization Logic

### Value Categories
- **High Value Customer**: Sales ≥ ₹100,000
- **Medium Value Customer**: Sales ≥ ₹50,000 and < ₹100,000
- **Low Value Customer**: Sales ≥ ₹10,000 and < ₹50,000
- **Very Low Value Customer**: Sales < ₹10,000

### Frequency Categories
- **High Frequency Customer**: ≥ 20 invoices
- **Medium Frequency Customer**: 10-19 invoices
- **Low Frequency Customer**: 5-9 invoices
- **Very Low Frequency Customer**: < 5 invoices

### Payment Behavior Categories
- **Excellent Payer**: ≥ 90% invoices paid
- **Good Payer**: 70-89% invoices paid
- **Average Payer**: 50-69% invoices paid
- **Poor Payer**: < 50% invoices paid

## Technical Details

### Database Tables Used
- `Customers`: Customer master data
- `SalesInvoices`: Sales invoice headers
- `SalesInvoiceDetails`: Sales invoice line items

### Key Calculations
- **Credit Utilization**: (Outstanding Balance / Credit Limit) × 100
- **Average Days Between Purchases**: Relationship duration / (Invoice count - 1)
- **Payment Percentage**: (Paid Invoices / Total Invoices) × 100

### Performance Considerations
- Uses subqueries to avoid nested aggregate functions
- Optimized for large datasets with proper indexing
- Includes comprehensive error handling

## Testing

### Test Script
- **File**: `TestCustomerWiseSalesReport.sql`
- **Location**: `Database/`

### Test Scenarios
1. All customers for current year
2. Specific customer analysis
3. Last 30 days analysis
4. Monthly analysis
5. Data availability verification

## Integration

### Dashboard Integration
- Added to Reports dropdown menu in `AdminDashboardRedesigned.cs`
- Method: `OpenCustomerWiseSalesReportForm()`

### Project Configuration
- Model included in `DistributionSoftware.csproj`
- Form files included with proper dependencies
- RDLC report included with copy to output directory

## Benefits

### Business Intelligence
- **Customer Performance Analysis**: Track sales performance by individual customers
- **Payment Behavior Insights**: Monitor payment patterns and identify risk customers
- **Customer Segmentation**: Automatic categorization for targeted marketing
- **Credit Management**: Track credit utilization and payment reliability
- **Relationship Analysis**: Understand customer relationship duration and patterns

### Decision Support
- **Credit Decisions**: Assess customer creditworthiness
- **Sales Strategy**: Identify high-value customers for focused attention
- **Collection Management**: Prioritize overdue collections
- **Customer Retention**: Identify at-risk customers based on payment behavior
- **Performance Benchmarking**: Compare customer performance across different periods

## Future Enhancements
- Customer lifetime value calculation
- Trend analysis over multiple periods
- Comparative analysis between customers
- Export to Excel functionality
- Advanced filtering options (by city, state, etc.)
- Customer ranking and scoring system
