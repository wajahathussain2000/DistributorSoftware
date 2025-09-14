using System;

namespace DistributionSoftware.Models
{
    public class PurchasePayment
    {
        public int PaymentId { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
        public int BankAccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        
        // Additional properties for compatibility
        public string PaymentReference { get; set; }
    }
}
