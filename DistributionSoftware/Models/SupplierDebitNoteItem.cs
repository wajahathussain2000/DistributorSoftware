using System;

namespace DistributionSoftware.Models
{
    public class SupplierDebitNoteItem
    {
        public int DebitNoteItemId { get; set; }
        public int DebitNoteId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public SupplierDebitNoteItem()
        {
            CreatedDate = DateTime.Now;
            Quantity = 0;
            UnitPrice = 0;
            TaxPercentage = 0;
            TaxAmount = 0;
            DiscountPercentage = 0;
            DiscountAmount = 0;
            TotalAmount = 0;
        }
        
        // Computed properties
        public decimal LineTotal => Quantity * UnitPrice;
        public decimal TaxableAmount => LineTotal - DiscountAmount;
        
        public string DisplayText => $"{ProductCode} - {ProductName} (Qty: {Quantity})";
    }
}
