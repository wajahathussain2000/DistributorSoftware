using System;

namespace DistributionSoftware.Models
{
    public class SalesmanWiseSalesReportData
    {
        public int SalesmanId { get; set; }
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Territory { get; set; }
        public decimal? CommissionRate { get; set; }
        public bool? SalesmanIsActive { get; set; }

        // Sales aggregation by salesman
        public int TotalInvoicesCount { get; set; }
        public int TotalLineItemsCount { get; set; }
        public decimal? TotalQuantitySold { get; set; }
        public decimal? AverageInvoiceAmount { get; set; }
        public decimal? MinInvoiceAmount { get; set; }
        public decimal? MaxInvoiceAmount { get; set; }
        public decimal? TotalSubTotal { get; set; }
        public decimal? TotalDiscountAmount { get; set; }
        public decimal? TotalTaxAmount { get; set; }
        public decimal? TotalSalesAmount { get; set; }
        public decimal? TotalPaidAmount { get; set; }
        public decimal? TotalBalanceAmount { get; set; }

        // Commission calculation
        public decimal? TotalCommissionEarned { get; set; }

        // Payment analysis
        public int? PaidInvoicesCount { get; set; }
        public int? PendingInvoicesCount { get; set; }
        public int? CancelledInvoicesCount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? PendingAmount { get; set; }
        public decimal? CancelledAmount { get; set; }

        // Overdue analysis
        public int? OverdueInvoicesCount { get; set; }
        public decimal? OverdueAmount { get; set; }

        // Customer analysis
        public int? UniqueCustomersCount { get; set; }
        public int? UniqueProductsSold { get; set; }

        // Date range analysis
        public DateTime? FirstSaleDate { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public int? SalesPeriodDays { get; set; }

        // Performance metrics
        public decimal? AverageDaysBetweenSales { get; set; }

        // Salesman categorization
        public string SalesmanPerformanceCategory { get; set; }
        public string SalesmanFrequencyCategory { get; set; }
        public string CustomerRelationshipCategory { get; set; }
        public string CollectionEfficiencyCategory { get; set; }

        // Territory performance
        public string TerritoryStatus { get; set; }

        // Commission efficiency
        public decimal? CommissionEfficiencyPercentage { get; set; }
    }
}
