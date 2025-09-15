using System;

namespace DistributionSoftware.Models
{
    public class SupplierLedgerReportData
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal RunningBalance { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ReferenceDocument { get; set; }
        public string TransactionCategory { get; set; }
        public int DaysSinceTransaction { get; set; }
        public string TransactionStatus { get; set; }
    }
}
