using System;

namespace DistributionSoftware.Models
{
    public class SalesReturnItem
    {
        public int ReturnItemId { get; set; }
        public int ReturnId { get; set; }
        public int ProductId { get; set; }
        public int? OriginalInvoiceItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        public bool StockUpdated { get; set; }
        public DateTime? StockUpdatedDate { get; set; }
        public int? StockUpdatedBy { get; set; }
        
        // Additional properties for tax calculation compatibility
        public int? TaxCategoryId { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal LineTotal { get; set; }
        
        // Navigation properties
        public virtual SalesReturn SalesReturn { get; set; }
        public virtual Product Product { get; set; }
    }
}

