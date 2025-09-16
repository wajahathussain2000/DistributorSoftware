using System;

namespace DistributionSoftware.Models
{
    public class AgingReportData
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string GSTNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal OutstandingBalance { get; set; }
        public string PaymentTerms { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string TaxNumber { get; set; }
        public string Remarks { get; set; }
        
        // Balance Information
        public decimal CurrentBalance { get; set; }
        
        // Aging Buckets
        public decimal Current30Days { get; set; }
        public decimal Days31To60 { get; set; }
        public decimal Days61To90 { get; set; }
        public decimal Over90Days { get; set; }
        public decimal OverdueAmount { get; set; }
        public int MaxDaysOverdue { get; set; }
        
        // Transaction Information
        public DateTime? LastInvoiceDate { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public int TotalInvoices { get; set; }
        public int TotalPayments { get; set; }
        
        // Computed Fields
        public decimal CreditUtilizationPercent { get; set; }
        public string BalanceStatus { get; set; }
        public string CreditStatus { get; set; }
        public string AgingStatus { get; set; }
        public int PaymentTermsDays { get; set; }
        public int? DaysSinceLastInvoice { get; set; }
        public int? DaysSinceLastPayment { get; set; }
    }
}
