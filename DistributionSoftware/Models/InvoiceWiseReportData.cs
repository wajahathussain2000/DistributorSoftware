using System;

namespace DistributionSoftware.Models
{
    public class InvoiceWiseReportData
    {
        public int SalesInvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TaxableAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string PaymentMode { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public bool? PrintStatus { get; set; }
        public DateTime? PrintDate { get; set; }
        public DateTime? TransactionTime { get; set; }

        // Customer Information
        public int? CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ContactPerson { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerPostalCode { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerTaxNumber { get; set; }
        public string CustomerGSTNumber { get; set; }

        // Salesman Information
        public int? SalesmanId { get; set; }
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public string SalesmanEmail { get; set; }
        public string SalesmanPhone { get; set; }
        public string SalesmanTerritory { get; set; }
        public decimal? CommissionRate { get; set; }

        // Invoice Analysis
        public string PaymentStatus { get; set; }
        public string PaymentCondition { get; set; }
        public int? InvoiceAge { get; set; }
        public int? DaysOverdue { get; set; }
        public int? LineItemsCount { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? LineItemsSubTotal { get; set; }
        public decimal? LineItemsDiscount { get; set; }
        public string TaxStatus { get; set; }
        public string DiscountStatus { get; set; }
        public string PaymentModeDescription { get; set; }
        public string InvoiceValueCategory { get; set; }
        public string UrgencyLevel { get; set; }
        public decimal? CommissionAmount { get; set; }
        public string PrintStatusDescription { get; set; }
    }
}
