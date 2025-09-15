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
        public string City { get; set; }
        public string State { get; set; }
        public int? PaymentTermsDays { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public int TransactionCount { get; set; }
        public int DaysOutstanding { get; set; }
        public string AgingCategory { get; set; }
        public string RiskLevel { get; set; }
        public string PaymentStatus { get; set; }
    }
}
