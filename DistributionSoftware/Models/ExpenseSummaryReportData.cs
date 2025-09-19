using System;

namespace DistributionSoftware.Models
{
    public class ExpenseSummaryReportData
    {
        // Category Summary Fields
        public int CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        
        // Expense Counts
        public int TotalExpenses { get; set; }
        public int TotalUsers { get; set; }
        
        // Amount Fields
        public decimal TotalAmount { get; set; }
        public decimal AverageAmount { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        
        // Status Counts
        public int PendingExpenses { get; set; }
        public int ApprovedExpenses { get; set; }
        public int RejectedExpenses { get; set; }
        public int PaidExpenses { get; set; }
        public int CancelledExpenses { get; set; }
        
        // Status Amounts
        public decimal PendingAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public decimal RejectedAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal CancelledAmount { get; set; }
        
        // Payment Method Counts
        public int CashExpenses { get; set; }
        public int CardExpenses { get; set; }
        public int BankTransferExpenses { get; set; }
        public int ChequeExpenses { get; set; }
        public int EasypaisaExpenses { get; set; }
        public int JazzcashExpenses { get; set; }
        
        // Payment Method Amounts
        public decimal TotalCashAmount { get; set; }
        public decimal TotalCardAmount { get; set; }
        public decimal TotalBankTransferAmount { get; set; }
        public decimal TotalChequeAmount { get; set; }
        public decimal TotalEasypaisaAmount { get; set; }
        public decimal TotalJazzcashAmount { get; set; }
        
        // Value Category Counts
        public int HighValueExpenses { get; set; }
        public int MediumValueExpenses { get; set; }
        public int LowValueExpenses { get; set; }
        public int VeryLowValueExpenses { get; set; }
        
        // Value Category Amounts
        public decimal TotalHighValueAmount { get; set; }
        public decimal TotalMediumValueAmount { get; set; }
        public decimal TotalLowValueAmount { get; set; }
        public decimal TotalVeryLowValueAmount { get; set; }
        
        // Percentages
        public decimal PendingPercentage { get; set; }
        public decimal ApprovedPercentage { get; set; }
        public decimal PaidPercentage { get; set; }
        public decimal RejectedPercentage { get; set; }
        
        // Overall Summary Fields (from second dataset)
        public int OverallTotalExpenses { get; set; }
        public int OverallTotalCategories { get; set; }
        public int OverallTotalUsers { get; set; }
        public decimal OverallTotalAmount { get; set; }
        public decimal OverallAverageAmount { get; set; }
        public decimal OverallMinAmount { get; set; }
        public decimal OverallMaxAmount { get; set; }
        
        public int OverallPendingExpenses { get; set; }
        public int OverallApprovedExpenses { get; set; }
        public int OverallRejectedExpenses { get; set; }
        public int OverallPaidExpenses { get; set; }
        public int OverallCancelledExpenses { get; set; }
        
        public decimal OverallPendingAmount { get; set; }
        public decimal OverallApprovedAmount { get; set; }
        public decimal OverallRejectedAmount { get; set; }
        public decimal OverallPaidAmount { get; set; }
        public decimal OverallCancelledAmount { get; set; }
        
        public int OverallCashExpenses { get; set; }
        public int OverallCardExpenses { get; set; }
        public int OverallBankTransferExpenses { get; set; }
        public int OverallChequeExpenses { get; set; }
        public int OverallEasypaisaExpenses { get; set; }
        public int OverallJazzcashExpenses { get; set; }
        
        public decimal OverallTotalCashAmount { get; set; }
        public decimal OverallTotalCardAmount { get; set; }
        public decimal OverallTotalBankTransferAmount { get; set; }
        public decimal OverallTotalChequeAmount { get; set; }
        public decimal OverallTotalEasypaisaAmount { get; set; }
        public decimal OverallTotalJazzcashAmount { get; set; }
        
        public int OverallHighValueExpenses { get; set; }
        public int OverallMediumValueExpenses { get; set; }
        public int OverallLowValueExpenses { get; set; }
        public int OverallVeryLowValueExpenses { get; set; }
        
        public decimal OverallTotalHighValueAmount { get; set; }
        public decimal OverallTotalMediumValueAmount { get; set; }
        public decimal OverallTotalLowValueAmount { get; set; }
        public decimal OverallTotalVeryLowValueAmount { get; set; }
        
        // Report Parameters
        public DateTime ReportStartDate { get; set; }
        public DateTime ReportEndDate { get; set; }
        public int ReportDays { get; set; }
        
        // Overall Percentages
        public decimal PendingExpensePercentage { get; set; }
        public decimal ApprovedExpensePercentage { get; set; }
        public decimal PaidExpensePercentage { get; set; }
        public decimal RejectedExpensePercentage { get; set; }
    }
}
