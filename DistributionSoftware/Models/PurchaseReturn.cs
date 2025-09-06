using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class PurchaseReturn
    {
        public int PurchaseReturnId { get; set; }
        public string ReturnNumber { get; set; }
        public string Barcode { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int? ReferencePurchaseId { get; set; }
        public string ReferencePurchaseNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TaxAdjust { get; set; }
        public decimal DiscountAdjust { get; set; }
        public decimal FreightAdjust { get; set; }
        public decimal NetReturnAmount { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        // Navigation properties
        public List<PurchaseReturnItem> Items { get; set; } = new List<PurchaseReturnItem>();
    }
}
