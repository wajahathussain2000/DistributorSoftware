using System;

namespace DistributionSoftware.Models
{
    public class CustomerReceiptsReportData
    {
        public int ReceiptId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentMode { get; set; }
        public string InvoiceReference { get; set; }
        public string Description { get; set; }
        public string ReceivedBy { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string TransactionId { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string MobileNumber { get; set; }
        public string PaymentReference { get; set; }
        public int? ReceiptAccountId { get; set; }
        public int? ReceivableAccountId { get; set; }
        public int? JournalVoucherId { get; set; }
        
        // Additional customer information
        public string CustomerCode { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string GSTNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal OutstandingBalance { get; set; }
        public string PaymentTerms { get; set; }
        
        // Computed fields
        public string PaymentMethodDescription { get; set; }
        public string StatusDescription { get; set; }
        public string ReceiptSizeCategory { get; set; }
        public int DaysSinceReceipt { get; set; }
        public string FormattedReceiptNumber { get; set; }
    }
}
