using System;

namespace DistributionSoftware.Models
{
    public class SupplierPaymentHistoryReportData
    {
        public int PaymentId { get; set; }
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
        public DateTime PaymentDate { get; set; }
        public string PaymentNumber { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string BankName { get; set; }
        public string TransactionReference { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByUsername { get; set; }
        public string CreatedByFullName { get; set; }
        public decimal RunningPaymentTotal { get; set; }
        public int TotalInvoices { get; set; }
        public decimal OutstandingBeforePayment { get; set; }
        public int DaysSincePayment { get; set; }
        public string PaymentStatus { get; set; }
        public int PaymentSequence { get; set; }
        public string PaymentMethodDescription { get; set; }
        public string PaymentSizeCategory { get; set; }
    }
}