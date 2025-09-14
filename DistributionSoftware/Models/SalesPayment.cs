using System;

namespace DistributionSoftware.Models
{
    public class SalesPayment
    {
        public int PaymentId { get; set; }
        public int SalesInvoiceId { get; set; }
        public string PaymentMode { get; set; } // CASH, CARD, EASYPAISA, JAZZCASH, BANK_TRANSFER, CHEQUE
        public string CustomerName { get; set; } // For display purposes
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string TransactionId { get; set; }
        public string BankName { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string MobileNumber { get; set; }
        public string PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } // PENDING, COMPLETED, FAILED, REFUNDED
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Accounting Integration
        public int? JournalVoucherId { get; set; } // Reference to the journal voucher created for this payment
        
        public SalesPayment()
        {
            PaymentDate = DateTime.Now;
            Status = "COMPLETED";
        }
    }
}
