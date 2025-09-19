using System;

namespace DistributionSoftware.Models
{
    public class ReturnSummaryReportData
    {
        // Return Details Fields
        public string ReturnType { get; set; } // 'Sales' or 'Purchase'
        public int ReturnId { get; set; }
        public string ReturnNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public int? ReferenceSalesInvoiceId { get; set; }
        public string ReferenceInvoiceNumber { get; set; }
        public string Reason { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public int? ProcessedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }

        // Calculated fields from SP
        public string StatusDescription { get; set; }
        public int DaysSinceReturn { get; set; }
        public int? DaysToApproval { get; set; }
        public string ApprovalStatus { get; set; }
        public string AmountCategory { get; set; }
        public int ReturnYear { get; set; }
        public int ReturnMonth { get; set; }
        public string ReturnMonthName { get; set; }

        // Summary fields (from second dataset)
        public int TotalReturns { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalSuppliers { get; set; }
        public decimal TotalSalesReturnAmount { get; set; }
        public decimal TotalPurchaseReturnAmount { get; set; }
        public decimal TotalSummaryAmount { get; set; }
        public decimal AverageAmount { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        
        public int PendingReturns { get; set; }
        public int ApprovedReturns { get; set; }
        public int ProcessedReturns { get; set; }
        public int RejectedReturns { get; set; }
        public int PurchaseReturns { get; set; }
        
        public decimal PendingAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public decimal ProcessedAmount { get; set; }
        public decimal RejectedAmount { get; set; }
        public decimal PurchaseReturnAmount { get; set; }
        
        public int HighValueReturns { get; set; }
        public int MediumValueReturns { get; set; }
        public int LowValueReturns { get; set; }
        public int VeryLowValueReturns { get; set; }
        public int HighValuePurchaseReturns { get; set; }
        public int MediumValuePurchaseReturns { get; set; }
        public int LowValuePurchaseReturns { get; set; }
        public int VeryLowValuePurchaseReturns { get; set; }
        
        public decimal TotalHighValueAmount { get; set; }
        public decimal TotalMediumValueAmount { get; set; }
        public decimal TotalLowValueAmount { get; set; }
        public decimal TotalVeryLowValueAmount { get; set; }
        public decimal TotalHighValuePurchaseAmount { get; set; }
        public decimal TotalMediumValuePurchaseAmount { get; set; }
        public decimal TotalLowValuePurchaseAmount { get; set; }
        public decimal TotalVeryLowValuePurchaseAmount { get; set; }
        
        // Report Parameters
        public DateTime ReportStartDate { get; set; }
        public DateTime ReportEndDate { get; set; }
        public int ReportDays { get; set; }
        
        // Overall Percentages
        public decimal PendingReturnPercentage { get; set; }
        public decimal ApprovedReturnPercentage { get; set; }
        public decimal ProcessedReturnPercentage { get; set; }
        public decimal RejectedReturnPercentage { get; set; }
    }
}
