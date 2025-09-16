# Salesman-wise Sales Report

## Overview
The Salesman-wise Sales Report provides comprehensive analysis of sales performance by individual salesmen, including commission tracking, territory analysis, and performance categorization.

## Features

### 📊 **Report Filters**
- **Salesman**: Select specific salesman or "All Salesmen"
- **Customer**: Select specific customer or "All Customers"
- **Date Range**: Start Date and End Date for analysis period

### 📈 **Report Data**
- **Salesman Information**: Code, Name, Territory, Commission Rate
- **Sales Aggregation**: Total invoices, line items, quantities, amounts
- **Commission Analysis**: Total commission earned based on sales
- **Payment Analysis**: Paid, pending, cancelled, and overdue amounts
- **Customer Analysis**: Unique customers count and relationship strength
- **Performance Categorization**: 
  - Performance Category (Top/High/Medium/Low/Under Performer)
  - Frequency Category (High/Medium/Low/Very Low Frequency Salesman)
  - Customer Relationship Category (Strong/Moderate/Limited/Single Customer Base)
  - Collection Efficiency Category (Excellent/Good/Average/Poor Collector)
- **Territory Analysis**: Territory status and performance
- **Commission Efficiency**: Commission efficiency percentage

### 🎯 **Key Metrics**
- **Total Sales Amount**: Sum of all sales by the salesman
- **Total Commission Earned**: Commission calculated based on sales amount and rate
- **Unique Customers Count**: Number of different customers served
- **Performance Category**: Automatic categorization based on sales volume
- **Collection Efficiency**: Analysis of payment collection patterns

## Database Components

### Stored Procedure
- **Name**: `sp_GetSalesmanWiseSalesReport`
- **Parameters**:
  - `@StartDate DATETIME`: Start date for analysis
  - `@EndDate DATETIME`: End date for analysis
  - `@SalesmanId INT`: Optional salesman ID filter (NULL for all salesmen)
  - `@CustomerId INT`: Optional customer ID filter (NULL for all customers)

### Data Model
- **File**: `SalesmanWiseSalesReportData.cs`
- **Namespace**: `DistributionSoftware.Models`

### Report Definition
- **File**: `SalesmanWiseSalesReport.rdlc`
- **Location**: `DistributionSoftware/Reports/`

### User Interface
- **Form**: `SalesmanWiseSalesReportForm.cs`
- **Designer**: `SalesmanWiseSalesReportForm.Designer.cs`
- **Location**: `DistributionSoftware/Presentation/Forms/`

## Usage

### Accessing the Report
1. Open the Distribution Software application
2. Navigate to **Reports** dropdown menu
3. Select **Salesman-wise Sales Report**

### Using Filters
1. **Salesman Filter**: Choose specific salesman or "All Salesmen"
2. **Customer Filter**: Choose specific customer or "All Customers"
3. **Date Range**: Set start and end dates for analysis
4. Click **Generate Report** to view results

### Export Options
- **Export PDF**: Save report as PDF file
- **Print**: Print the report directly

## Report Sections

### Main Data Table
- Salesman Code and Name
- Territory Information
- Sales Metrics (Invoices, Sales Amount, Commission Earned)
- Customer Analysis (Unique Customers Count)
- Performance and Collection Categories

### Summary Totals
- Total Invoices Count
- Total Sales Amount
- Total Commission Earned
- Total Unique Customers Count

## Salesman Categorization Logic

### Performance Categories
- **Top Performer**: Sales ≥ ₹200,000
- **High Performer**: Sales ≥ ₹100,000 and < ₹200,000
- **Medium Performer**: Sales ≥ ₹50,000 and < ₹100,000
- **Low Performer**: Sales ≥ ₹10,000 and < ₹50,000
- **Under Performer**: Sales < ₹10,000

### Frequency Categories
- **High Frequency Salesman**: ≥ 30 invoices
- **Medium Frequency Salesman**: 15-29 invoices
- **Low Frequency Salesman**: 5-14 invoices
- **Very Low Frequency Salesman**: < 5 invoices

### Customer Relationship Categories
- **Strong Customer Base**: ≥ 10 unique customers
- **Moderate Customer Base**: 5-9 unique customers
- **Limited Customer Base**: 2-4 unique customers
- **Single Customer Focus**: 1 unique customer

### Collection Efficiency Categories
- **Excellent Collector**: ≥ 90% invoices paid
- **Good Collector**: 70-89% invoices paid
- **Average Collector**: 50-69% invoices paid
- **Poor Collector**: < 50% invoices paid

## Technical Details

### Database Tables Used
- `Salesman`: Salesman master data
- `SalesInvoices`: Sales invoice headers
- `SalesInvoiceDetails`: Sales invoice line items

### Key Calculations
- **Commission Earned**: Sales Amount × Commission Rate / 100
- **Commission Efficiency**: (Commission Earned / Sales Amount) × 100
- **Average Days Between Sales**: Sales Period Days / Invoice Count
- **Collection Efficiency**: (Paid Invoices / Total Invoices) × 100

### Performance Considerations
- Uses subqueries to avoid nested aggregate functions
- Optimized for large datasets with proper indexing
- Includes comprehensive error handling

## Testing

### Test Script
- **File**: `TestSalesmanWiseSalesReport.sql`
- **Location**: `Database/`

### Test Scenarios
1. All salesmen for current year
2. Specific salesman analysis
3. All salesmen for specific customer
4. Specific salesman and customer combination
5. Data availability verification

## Integration

### Dashboard Integration
- Added to Reports dropdown menu in `AdminDashboardRedesigned.cs`
- Method: `OpenSalesmanWiseSalesReportForm()`

### Project Configuration
- Model included in `DistributionSoftware.csproj`
- Form files included with proper dependencies
- RDLC report included with copy to output directory

## Benefits

### Business Intelligence
- **Salesman Performance Analysis**: Track sales performance by individual salesmen
- **Commission Tracking**: Monitor commission earnings and efficiency
- **Territory Analysis**: Understand territory performance and coverage
- **Customer Relationship Management**: Analyze customer base strength
- **Collection Efficiency**: Monitor payment collection performance

### Decision Support
- **Salesman Evaluation**: Assess individual salesman performance
- **Commission Management**: Track commission payments and efficiency
- **Territory Planning**: Optimize territory assignments
- **Performance Benchmarking**: Compare salesman performance
- **Training Needs**: Identify areas for salesman improvement

## Future Enhancements
- Salesman ranking and scoring system
- Trend analysis over multiple periods
- Comparative analysis between salesmen
- Export to Excel functionality
- Advanced filtering options (by territory, commission rate, etc.)
- Salesman target vs actual performance analysis
- Customer acquisition analysis by salesman
