using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class SalesInvoice
    {
        public int SalesInvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string PaymentMode { get; set; } // CASH, CARD, EASYPAISA, JAZZCASH, BANK_TRANSFER, CHEQUE
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public string Status { get; set; } // DRAFT, CONFIRMED, PAID, DELIVERED, CANCELLED
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        // Barcode and Printing
        public string Barcode { get; set; }
        public byte[] BarcodeImage { get; set; }
        public bool PrintStatus { get; set; }
        public DateTime? PrintDate { get; set; }
        public int? CashierId { get; set; }
        public string CashierName { get; set; }
        public DateTime TransactionTime { get; set; }
        
        // Navigation Properties
        public List<SalesInvoiceDetail> Items { get; set; }
        public List<SalesPayment> Payments { get; set; }
        
        public SalesInvoice()
        {
            Items = new List<SalesInvoiceDetail>();
            Payments = new List<SalesPayment>();
            InvoiceDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            TransactionTime = DateTime.Now;
            Status = "DRAFT";
            PaymentMode = "CASH";
            PrintStatus = false;
        }
    }
}
