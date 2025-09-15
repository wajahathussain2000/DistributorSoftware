using System;

namespace DistributionSoftware.Models
{
    public class LowStockReportData
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string UnitName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal StockQuantity { get; set; }
        public string WarehouseName { get; set; }
        public decimal ReorderLevel { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string StockStatus { get; set; }
        public string Priority { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
