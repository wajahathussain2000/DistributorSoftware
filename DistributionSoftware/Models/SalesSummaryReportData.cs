using System;

namespace DistributionSoftware.Models
{
    public class SalesSummaryReportData
    {
        public int DetailId { get; set; }
        public int SalesInvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public int? SalesmanId { get; set; }
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public string SalesmanPhone { get; set; }
        public string Territory { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public string ProductBarcode { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal LineTotal { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string LineRemarks { get; set; }
        public decimal InvoiceSubTotal { get; set; }
        public decimal InvoiceDiscountAmount { get; set; }
        public decimal InvoiceTaxAmount { get; set; }
        public decimal InvoiceTotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string PaymentMode { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceRemarks { get; set; }
        public DateTime InvoiceCreatedDate { get; set; }
        public int? InvoiceCreatedBy { get; set; }
        public string InvoiceCreatedByUser { get; set; }
        public DateTime? InvoiceModifiedDate { get; set; }
        public int? InvoiceModifiedBy { get; set; }
        public string InvoiceModifiedByUser { get; set; }
        public decimal ChangeAmount { get; set; }
        public string InvoiceBarcode { get; set; }
        public bool PrintStatus { get; set; }
        public DateTime? PrintDate { get; set; }
        public int? CashierId { get; set; }
        public string CashierName { get; set; }
        public DateTime TransactionTime { get; set; }

        // Calculated fields
        public string PaymentStatus { get; set; }
        public decimal PaymentPercentage { get; set; }
        public int DaysSinceInvoice { get; set; }
        public decimal AverageUnitPrice { get; set; }
        public decimal ProfitMarginPercentage { get; set; }
        public decimal ProfitAmount { get; set; }
        public string SalesValueCategory { get; set; }
        public string SalesQuantityCategory { get; set; }
    }
}
