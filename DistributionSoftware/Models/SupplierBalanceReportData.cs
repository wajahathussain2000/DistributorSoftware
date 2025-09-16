using System;

namespace DistributionSoftware.Models
{
    public class SupplierBalanceReportData
    {
        public int SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string GST { get; set; }
        public int? PaymentTermsDays { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TotalPurchases { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal ClosingBalance { get; set; }
        public int TransactionCount { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public decimal NetMovement { get; set; }
        public int? DaysSinceLastTransaction { get; set; }
        public string BalanceStatus { get; set; }
    }
}
