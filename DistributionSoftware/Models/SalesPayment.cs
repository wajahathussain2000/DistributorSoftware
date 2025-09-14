using System;

namespace DistributionSoftware.Models
{
    public class SalesPayment
    {
        public int PaymentId { get; set; }
        public int SalesInvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
        public int BankAccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        
        // Additional properties for compatibility
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string TransactionId { get; set; }
        public string BankName { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string MobileNumber { get; set; }
        public string PaymentReference { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        
        // Additional properties for compatibility
        public string CustomerName { get; set; }
        public int? JournalVoucherId { get; set; }
    }
}