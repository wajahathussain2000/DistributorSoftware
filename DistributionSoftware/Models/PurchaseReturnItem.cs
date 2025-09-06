using System;

namespace DistributionSoftware.Models
{
    public class PurchaseReturnItem
    {
        public int PurchaseReturnItemId { get; set; }
        public int PurchaseReturnId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Notes { get; set; }
        
        // Navigation properties
        public PurchaseReturn PurchaseReturn { get; set; }
    }
}
