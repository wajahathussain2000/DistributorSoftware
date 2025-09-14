using System;

namespace DistributionSoftware.Models
{
    public class StockMovement
    {
        public int MovementId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string MovementType { get; set; } // IN, OUT, ADJUSTMENT
        public decimal Quantity { get; set; }
        public string ReferenceType { get; set; } // PURCHASE, SALES, RETURN, ADJUSTMENT, etc.
        public int? ReferenceId { get; set; }
        public string ReferenceNumber { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime MovementDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByUser { get; set; }
        public string Remarks { get; set; }
        
        // Additional properties for reporting
        public decimal UnitPrice { get; set; }
        public decimal TotalValue { get; set; }
        public string SupplierName { get; set; }
        public string CustomerName { get; set; }
        
        // Accounting Integration
        public int? InventoryAccountId { get; set; } // Inventory account
        public int? JournalVoucherId { get; set; } // Reference to the journal voucher created for this movement
    }
}
