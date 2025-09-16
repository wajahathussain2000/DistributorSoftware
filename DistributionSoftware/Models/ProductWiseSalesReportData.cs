using System;

namespace DistributionSoftware.Models
{
    public class ProductWiseSalesReportData
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public string ProductBarcode { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? MRP { get; set; }
        public int? StockQuantity { get; set; }
        public int? ReorderLevel { get; set; }
        public bool? ProductIsActive { get; set; }

        // Sales aggregation by product
        public int TotalSalesCount { get; set; }
        public decimal TotalQuantitySold { get; set; }
        public decimal? AverageUnitPrice { get; set; }
        public decimal? MinUnitPrice { get; set; }
        public decimal? MaxUnitPrice { get; set; }
        public decimal? TotalDiscountAmount { get; set; }
        public decimal? TotalTaxAmount { get; set; }
        public decimal? TotalTaxableAmount { get; set; }
        public decimal? TotalSalesAmount { get; set; }

        // Profit calculations
        public decimal? TotalProfitAmount { get; set; }
        public decimal? AverageProfitMarginPercentage { get; set; }

        // Performance metrics
        public int? UniqueCustomersCount { get; set; }
        public int? UniqueSalesmenCount { get; set; }
        public int? UniqueInvoicesCount { get; set; }

        // Date range analysis
        public DateTime? FirstSaleDate { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public int? SalesPeriodDays { get; set; }

        // Sales frequency
        public string SalesFrequencyCategory { get; set; }

        // Sales value analysis
        public string SalesValueCategory { get; set; }

        // Quantity analysis
        public string SalesVolumeCategory { get; set; }

        // Profit analysis
        public string ProfitMarginCategory { get; set; }
    }
}
