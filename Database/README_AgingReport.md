# Customer Aging Report Implementation

## Overview
This document describes the implementation of the Customer Aging Report feature for the Distribution Software system. The report follows the same pattern as the existing Customer Balance Report and Customer Receipts Report.

## Files Created

### 1. Database Files
- **`sp_GetAgingReport.sql`** - Stored procedure for retrieving customer aging data
- **`TestAgingReport.sql`** - Test script to validate the stored procedure
- **`README_AgingReport.md`** - This documentation file

### 2. Model Files
- **`AgingReportData.cs`** - Data model for customer aging report

### 3. UI Files
- **`AgingReportForm.cs`** - Main form class with business logic
- **`AgingReportForm.Designer.cs`** - Form designer file with UI controls

### 4. Report Files
- **`AgingReport.rdlc`** - RDLC report definition file

## Installation Instructions

### Step 1: Install the Stored Procedure
1. Open SQL Server Management Studio (SSMS)
2. Connect to your DistributionDB database
3. Open and execute the `sp_GetAgingReport.sql` file
4. Verify the stored procedure was created successfully

### Step 2: Test the Stored Procedure
1. Execute the `TestAgingReport.sql` file
2. Check the results to ensure the procedure works correctly
3. If no data is returned, ensure you have:
   - Active customers in the Customers table
   - Transaction data in the CustomerLedger table

### Step 3: Add the Report to Your Application
1. Copy the model file to your Models folder
2. Copy the form files to your Presentation/Forms folder
3. Copy the RDLC file to your Reports folder
4. Rebuild your solution

## Report Features

### Filters
- **Customer**: Select specific customer or "All Customers"
- **Overdue Days**: Filter by overdue days (All, 30+, 60+, 90+, 120+, 180+)

### Report Data
The report displays:
- Customer Code and Name
- Total Balance (current outstanding amount)
- Aging Buckets:
  - 0-30 Days (current invoices)
  - 31-60 Days (slightly overdue)
  - 61-90 Days (moderately overdue)
  - Over 90 Days (severely overdue)
- Overdue Amount (amount past payment terms)
- Days Overdue (maximum days overdue)
- Aging Status (Overdue, Current, Settled)

### Summary Information
- Total Customers count
- Total Outstanding Balance
- Aging bucket totals
- Total Overdue Amount
- Customers by aging status

## Database Schema Requirements

The report requires the following tables:
- **Customers** - Customer master data
- **CustomerLedger** - Transaction ledger entries

### Expected Table Structure

#### Customers Table
```sql
- CustomerId (INT, Primary Key)
- CustomerCode (VARCHAR)
- CustomerName (VARCHAR)
- ContactName (VARCHAR)
- Phone (VARCHAR)
- Email (VARCHAR)
- Address (VARCHAR)
- City (VARCHAR)
- State (VARCHAR)
- Country (VARCHAR)
- GSTNumber (VARCHAR)
- CreditLimit (DECIMAL)
- OutstandingBalance (DECIMAL)
- PaymentTerms (VARCHAR) -- Payment terms in days (default 30)
- IsActive (BIT)
```

#### CustomerLedger Table
```sql
- LedgerId (BIGINT, Primary Key)
- CustomerId (INT, Foreign Key)
- TransactionDate (DATETIME)
- TransactionType (VARCHAR) -- 'Invoice', 'Receipt', 'Return'
- ReferenceNo (VARCHAR)
- DebitAmount (DECIMAL)
- CreditAmount (DECIMAL)
- Remarks (VARCHAR)
- IsActive (BIT)
```

## Usage Instructions

### Running the Report
1. Open the Customer Aging Report form
2. Select a customer from the dropdown (or leave as "All Customers")
3. Select overdue days filter (or leave as "All")
4. Click "Generate Report"
5. The report will display in the ReportViewer control
6. Use "Export PDF" to save the report

### Report Parameters
- **CustomerFilter**: Selected customer name or "All Customers"
- **OverdueDaysFilter**: Selected overdue days filter

## Technical Details

### Stored Procedure Logic
1. Calculates current balance from CustomerLedger transactions
2. Categorizes invoices into aging buckets based on transaction date
3. Calculates overdue amounts considering customer payment terms
4. Determines aging status based on overdue amounts
5. Returns summary information including totals and counts

### Aging Calculation
- **Current Balance**: Sum of all invoice amounts minus payments and returns
- **Aging Buckets**: Based on days since invoice date
- **Overdue Amount**: Invoices past customer's payment terms
- **Payment Terms**: Default to 30 days if not specified in customer record

### Report Features
- **Professional Styling**: Dark headers, bordered cells, color-coded status
- **Responsive Design**: Auto-sizing columns, proper alignment
- **Error Handling**: Comprehensive validation and error messages
- **Export Capabilities**: PDF, Excel, Word formats
- **Performance Optimized**: Efficient SQL queries with proper indexing

## Sample Data

### Test Results
```
Customer: CUST001 - Sample Company Inc.
Total Balance: 45,000.00
0-30 Days: 15,000.00
31-60 Days: 20,000.00
61-90 Days: 10,000.00
Over 90 Days: 0.00
Overdue Amount: 30,000.00
Days Overdue: 45
Status: Overdue
```

## Benefits

### For Management
- **Cash Flow Analysis**: Track aging of outstanding receivables
- **Collection Management**: Prioritize collection efforts based on aging
- **Credit Risk Assessment**: Identify customers with aging issues
- **Financial Planning**: Better cash flow forecasting

### For Finance Team
- **Collection Strategy**: Focus on oldest outstanding amounts
- **Customer Relationship**: Monitor payment patterns and aging trends
- **Risk Management**: Identify potential bad debts early
- **Performance Metrics**: Track aging improvement over time

## Integration

The report is integrated into the main application through:
- **Reports Dropdown**: Added to the Reports menu in AdminDashboardRedesigned.cs
- **Form Integration**: Uses the same pattern as other reports
- **Database Integration**: Uses the existing CustomerLedger table structure

## Troubleshooting

### Common Issues
1. **No Data Found**: Ensure CustomerLedger table has invoice transactions
2. **Connection Error**: Check database connection string
3. **Report Not Loading**: Verify RDLC file is in Reports folder
4. **Missing Customers**: Ensure Customers table has active records

### Error Messages
- "Database connection string not found" - Check App.config
- "Report file not found" - Ensure RDLC file exists in Reports folder
- "No customers found" - Check Customers table data
- "Error retrieving report data" - Check stored procedure execution

## Aging Analysis Features

### Aging Buckets
- **0-30 Days**: Current invoices within payment terms
- **31-60 Days**: Slightly overdue invoices
- **61-90 Days**: Moderately overdue invoices
- **Over 90 Days**: Severely overdue invoices requiring immediate attention

### Status Indicators
- **Overdue**: Customer has amounts past payment terms
- **Current**: Customer has outstanding balance but within terms
- **Settled**: Customer has no outstanding balance

### Key Metrics
- **Total Outstanding**: Sum of all customer balances
- **Overdue Amount**: Amount past customer payment terms
- **Days Overdue**: Maximum days past payment terms
- **Aging Distribution**: Breakdown by aging buckets
