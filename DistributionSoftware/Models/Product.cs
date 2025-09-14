using System;

namespace DistributionSoftware.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Price { get => SalePrice; set => SalePrice = value; }
        public decimal UnitPrice { get; set; }
        public decimal MRP { get; set; }
        public decimal Quantity { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal ReservedQuantity { get; set; }
        public decimal ReorderLevel { get; set; }
        public string Barcode { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        public Product()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            Quantity = 0;
            StockQuantity = 0;
            ReservedQuantity = 0;
            ReorderLevel = 0;
        }
    }
}
