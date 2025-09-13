using System;

namespace DistributionSoftware.Models
{
    public class CustomerReceipt
    {
        public int ReceiptId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // CASH, BANK_TRANSFER, CHEQUE, CARD, EASYPAISA, JAZZCASH, OTHER
        public string InvoiceReference { get; set; }
        public string Description { get; set; }
        public string ReceivedBy { get; set; }
        public string Status { get; set; } // PENDING, COMPLETED, CANCELLED
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        // Additional fields for enhanced functionality
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string TransactionId { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string MobileNumber { get; set; }
        public string PaymentReference { get; set; }
        
        public CustomerReceipt()
        {
            ReceiptDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            Status = "COMPLETED";
            PaymentMethod = "CASH";
        }
    }
}
