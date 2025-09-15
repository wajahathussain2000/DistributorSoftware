using System;

namespace DistributionSoftware.Models
{
    public class SupplierPaymentHistoryReportData
    {
        public int PaymentId { get; set; }
        public string PaymentNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string ReferenceNumber { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? TransactionId { get; set; }
        public DateTime? RelatedTransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public decimal? RunningBalance { get; set; }
        public string PaymentTypeDescription { get; set; }
        public string PaymentStatusDescription { get; set; }
        public decimal OutstandingBeforePayment { get; set; }
        public string PaymentEfficiency { get; set; }
        public int DaysToPayment { get; set; }
    }
}
