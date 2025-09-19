using System;

namespace DistributionSoftware.Models
{
    public class TaxSummaryReportData
    {
        // Tax Details Fields
        public string TaxType { get; set; } // 'Sales Tax' or 'Purchase Tax'
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public int? TaxCategoryId { get; set; }
        public string TaxCategoryName { get; set; }
        public string TaxCategoryCode { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? TaxAccountId { get; set; }

        // Calculated fields from SP
        public string StatusDescription { get; set; }
        public string TaxAmountCategory { get; set; }
        public int TaxYear { get; set; }
        public int TaxMonth { get; set; }
        public string TaxMonthName { get; set; }
        public string TaxableStatus { get; set; }
        public decimal EffectiveTaxRate { get; set; }
        public int DaysSinceInvoice { get; set; }
        public int? DaysOverdue { get; set; }

        // Summary fields (from second dataset)
        public int TotalTaxInvoices { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalTaxCategories { get; set; }
        
        public decimal TotalSalesTaxAmount { get; set; }
        public decimal TotalPurchaseTaxAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        
        public decimal TotalSalesTaxableAmount { get; set; }
        public decimal TotalPurchaseTaxableAmount { get; set; }
        public decimal TotalTaxableAmount { get; set; }
        
        public decimal AverageTaxAmount { get; set; }
        public decimal MinTaxAmount { get; set; }
        public decimal MaxTaxAmount { get; set; }
        
        public int PendingSalesTaxInvoices { get; set; }
        public int PaidSalesTaxInvoices { get; set; }
        public int OverdueSalesTaxInvoices { get; set; }
        public int PendingPurchaseTaxInvoices { get; set; }
        public int PaidPurchaseTaxInvoices { get; set; }
        public int OverduePurchaseTaxInvoices { get; set; }
        
        public decimal PendingSalesTaxAmount { get; set; }
        public decimal PaidSalesTaxAmount { get; set; }
        public decimal OverdueSalesTaxAmount { get; set; }
        public decimal PendingPurchaseTaxAmount { get; set; }
        public decimal PaidPurchaseTaxAmount { get; set; }
        public decimal OverduePurchaseTaxAmount { get; set; }
        
        public int HighTaxInvoices { get; set; }
        public int MediumTaxInvoices { get; set; }
        public int LowTaxInvoices { get; set; }
        public int VeryLowTaxInvoices { get; set; }
        
        public decimal TotalHighTaxAmount { get; set; }
        public decimal TotalMediumTaxAmount { get; set; }
        public decimal TotalLowTaxAmount { get; set; }
        public decimal TotalVeryLowTaxAmount { get; set; }
        
        // Report Parameters
        public DateTime ReportStartDate { get; set; }
        public DateTime ReportEndDate { get; set; }
        public int ReportDays { get; set; }
        
        // Overall Percentages
        public decimal SalesTaxPercentage { get; set; }
        public decimal PurchaseTaxPercentage { get; set; }
        public decimal OverallTaxRate { get; set; }
    }
}
