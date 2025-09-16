# Invoice-wise Report

## Overview
The Invoice-wise Report provides comprehensive analysis of individual invoices with detailed customer and salesman information, payment status, and invoice categorization.

## Features
- **Invoice Details**: Complete invoice information including number, date, amounts, and status
- **Customer Information**: Customer details, contact information, and address
- **Salesman Information**: Salesman details, territory, and commission information
- **Payment Analysis**: Payment status, overdue analysis, and collection tracking
- **Invoice Categorization**: Value-based categorization and urgency levels
- **Comprehensive Totals**: Sum calculations for all key metrics

## Filters
- **Date Range**: Start and end date for invoice filtering
- **Invoice Number**: Optional search by invoice number (supports partial matching)

## Report Sections

### Main Data Table
- Invoice Number
- Invoice Date
- Customer Name
- Salesman Name
- Total Amount
- Paid Amount
- Balance Amount
- Status
- Payment Mode

### Analysis Fields
- **Payment Status**: Fully Paid, Partially Paid, Cancelled
- **Payment Condition**: Overdue, Pending, Paid
- **Invoice Age**: Days since invoice date
- **Days Overdue**: Days past due date
- **Line Items Count**: Number of items in invoice
- **Tax Status**: Taxable/Non-Taxable
- **Discount Status**: Discounted/No Discount
- **Invoice Value Category**: High/Medium/Low/Very Low Value
- **Urgency Level**: Critical Overdue, High Priority, Overdue, Normal, Paid
- **Commission Amount**: Calculated commission for salesman

### Summary Information
- Total invoices count
- Total customers and salesmen
- Payment status summary (paid, pending, cancelled)
- Overdue analysis
- Invoice value analysis
- Payment mode analysis
- Tax and discount analysis
- Print status analysis
- Top performers (highest value invoice, most frequent customer)
- Average metrics (invoice amount, age, days overdue)

## Stored Procedure
**Name**: `sp_GetInvoiceWiseReport`

**Parameters**:
- `@StartDate` (DATETIME): Start date for filtering
- `@EndDate` (DATETIME): End date for filtering
- `@InvoiceNumber` (NVARCHAR(50)): Optional invoice number filter

## Usage
1. Open the report from the Reports dropdown menu
2. Set the date range using the date pickers
3. Optionally enter an invoice number for specific search
4. Click "Generate Report" to view the data
5. Use "Export PDF" to save the report

## Technical Details
- **Data Source**: SalesInvoices, Customers, Salesman tables
- **Report Format**: RDLC with professional styling
- **Export Options**: PDF export available
- **Performance**: Optimized queries with proper indexing

## Business Value
- **Invoice Management**: Track individual invoice performance
- **Payment Collection**: Monitor overdue invoices and collection efficiency
- **Sales Analysis**: Analyze invoice patterns and customer behavior
- **Commission Tracking**: Monitor salesman commission earnings
- **Financial Reporting**: Comprehensive invoice-based financial analysis
