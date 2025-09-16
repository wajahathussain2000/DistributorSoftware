using System;

namespace DistributionSoftware.Models
{
    public class CustomerWiseSalesReportData
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? CreditLimit { get; set; }
        public decimal? OutstandingBalance { get; set; }
        public string PaymentTerms { get; set; }
        public string TaxNumber { get; set; }
        public string GSTNumber { get; set; }
        public string Remarks { get; set; }
        public bool? CustomerIsActive { get; set; }

        // Sales aggregation by customer
        public int TotalInvoicesCount { get; set; }
        public int TotalLineItemsCount { get; set; }
        public decimal? TotalQuantityPurchased { get; set; }
        public decimal? AverageInvoiceAmount { get; set; }
        public decimal? MinInvoiceAmount { get; set; }
        public decimal? MaxInvoiceAmount { get; set; }
        public decimal? TotalSubTotal { get; set; }
        public decimal? TotalDiscountAmount { get; set; }
        public decimal? TotalTaxAmount { get; set; }
        public decimal? TotalSalesAmount { get; set; }
        public decimal? TotalPaidAmount { get; set; }
        public decimal? TotalBalanceAmount { get; set; }

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

        // Date range analysis
        public DateTime? FirstPurchaseDate { get; set; }
        public DateTime? LastPurchaseDate { get; set; }
        public int? CustomerRelationshipDays { get; set; }

        // Performance metrics
        public int? UniqueSalesmenCount { get; set; }
        public int? UniqueProductsPurchased { get; set; }

        // Customer categorization
        public string CustomerValueCategory { get; set; }
        public string CustomerFrequencyCategory { get; set; }
        public string PaymentBehaviorCategory { get; set; }

        // Credit utilization
        public decimal? CreditUtilizationPercentage { get; set; }

        // Average days between purchases
        public decimal? AverageDaysBetweenPurchases { get; set; }
    }
}
