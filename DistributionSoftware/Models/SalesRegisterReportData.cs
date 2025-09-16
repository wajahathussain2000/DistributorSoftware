using System;

namespace DistributionSoftware.Models
{
    public class SalesRegisterReportData
    {
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
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public string PaymentMode { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public bool PrintStatus { get; set; }
        public DateTime? PrintDate { get; set; }
        public int? CashierId { get; set; }
        public string CashierName { get; set; }
        public DateTime TransactionTime { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        // Computed Fields
        public string StatusDescription { get; set; }
        public string PaymentModeDescription { get; set; }
        public int DaysSinceInvoice { get; set; }
        public int? DaysUntilDue { get; set; }
        public string OverdueStatus { get; set; }
        public decimal PaymentPercentage { get; set; }
        public decimal NetAmount { get; set; }
        public decimal EffectiveTaxRate { get; set; }
    }
}
