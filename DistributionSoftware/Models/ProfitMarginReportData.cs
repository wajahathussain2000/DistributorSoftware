using System;

namespace DistributionSoftware.Models
{
    public class ProfitMarginReportData
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public int? ReorderLevel { get; set; }
        public int? StockQuantity { get; set; }
        public decimal? Quantity { get; set; }
        public bool? ProductIsActive { get; set; }

        // Sales aggregation
        public int TotalSalesCount { get; set; }
        public decimal? TotalQuantitySold { get; set; }
        public decimal? TotalSalesAmount { get; set; }
        public decimal? TotalCostAmount { get; set; }
        public decimal? TotalProfitAmount { get; set; }

        // Profit margin calculations
        public decimal? ProfitMarginPercentage { get; set; }
        public decimal? MarkupPercentage { get; set; }

        // Average calculations
        public decimal? AverageSellingPrice { get; set; }
        public decimal? AverageCostPrice { get; set; }
        public decimal? AverageProfitPerUnit { get; set; }

        // Price analysis
        public decimal? MinSellingPrice { get; set; }
        public decimal? MaxSellingPrice { get; set; }
        public decimal? MinCostPrice { get; set; }
        public decimal? MaxCostPrice { get; set; }

        // Sales frequency
        public int? InvoiceFrequency { get; set; }
        public int? CustomerFrequency { get; set; }

        // Date analysis
        public DateTime? FirstSaleDate { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public int? SalesPeriodDays { get; set; }

        // Performance categorization
        public string ProfitCategory { get; set; }
        public string MarginCategory { get; set; }
        public string VolumeCategory { get; set; }
        public string StockStatus { get; set; }
        public decimal? ProfitabilityIndex { get; set; }
        public decimal? RevenueContributionPercentage { get; set; }
    }
}
