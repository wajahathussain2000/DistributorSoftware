# Profit Margin Report

## Overview
The Profit Margin Report provides detailed analysis of product profitability, including sales performance, profit margins, and product categorization based on profit and volume metrics.

## Features
- **Product Analysis**: Complete product information with sales aggregation
- **Profit Calculations**: Detailed profit margin and markup analysis
- **Performance Categorization**: Products categorized by profit, margin, and volume
- **Stock Analysis**: Current stock status and reorder level monitoring
- **Revenue Contribution**: Product contribution to total revenue
- **Comprehensive Totals**: Sum calculations for all key metrics

## Filters
- **Date Range**: Start and end date for sales filtering
- **Product**: Optional selection of specific product (or "All Products")

## Report Sections

### Main Data Table
- Product Code
- Product Name
- Category
- Brand
- Quantity Sold
- Sales Amount
- Profit Amount
- Margin Percentage
- Profit Category
- Margin Category

### Analysis Fields
- **Sales Aggregation**: Total sales count, quantity sold, sales amount
- **Cost Analysis**: Total cost amount and profit amount calculations
- **Profit Margin**: Percentage profit margin and markup calculations
- **Average Metrics**: Average selling price, cost price, profit per unit
- **Price Analysis**: Min/max selling and cost prices
- **Sales Frequency**: Invoice and customer frequency
- **Date Analysis**: First sale, last sale, sales period
- **Performance Categories**:
  - **Profit Category**: High/Medium/Low/Very Low Profit Product
  - **Margin Category**: High/Medium/Low/Very Low Margin Product
  - **Volume Category**: High/Medium/Low/Very Low Volume Product
- **Stock Status**: Low Stock, Overstocked, Optimal Stock
- **Profitability Index**: Combined profit and volume metric
- **Revenue Contribution**: Percentage contribution to total revenue

### Summary Information
- Total products sold count
- Total invoices and customers
- Overall profit margin and markup percentages
- Profit analysis by category (High/Medium/Low/Very Low)
- Top performers (top profit product, top volume product)
- Average metrics (selling price, cost price, profit per unit)

## Stored Procedure
**Name**: `sp_GetProfitMarginReport`

**Parameters**:
- `@StartDate` (DATETIME): Start date for filtering
- `@EndDate` (DATETIME): End date for filtering
- `@ProductId` (INT): Optional product ID filter

## Usage
1. Open the report from the Reports dropdown menu
2. Set the date range using the date pickers
3. Optionally select a specific product from the dropdown
4. Click "Generate Report" to view the data
5. Use "Export PDF" to save the report

## Technical Details
- **Data Source**: Products, ProductCategories, Brands, SalesInvoiceDetails, SalesInvoices tables
- **Report Format**: RDLC with professional styling
- **Export Options**: PDF export available
- **Performance**: Optimized queries with proper aggregation
- **Cost Calculation**: Uses 70% of selling price as estimated cost (adjustable in stored procedure)

## Business Value
- **Product Profitability**: Identify most and least profitable products
- **Margin Analysis**: Monitor profit margins and markup percentages
- **Volume vs Profit**: Balance between sales volume and profit margins
- **Stock Management**: Monitor stock levels and reorder points
- **Revenue Optimization**: Focus on high-contributing products
- **Strategic Planning**: Make informed decisions about product portfolio

## Categorization Logic

### Profit Categories
- **High Profit Product**: ≥ ₹10,000 total profit
- **Medium Profit Product**: ₹5,000 - ₹9,999 total profit
- **Low Profit Product**: ₹1,000 - ₹4,999 total profit
- **Very Low Profit Product**: < ₹1,000 total profit

### Margin Categories
- **High Margin Product**: ≥ 30% profit margin
- **Medium Margin Product**: 20% - 29% profit margin
- **Low Margin Product**: 10% - 19% profit margin
- **Very Low Margin Product**: < 10% profit margin

### Volume Categories
- **High Volume Product**: ≥ 100 units sold
- **Medium Volume Product**: 50 - 99 units sold
- **Low Volume Product**: 10 - 49 units sold
- **Very Low Volume Product**: < 10 units sold
