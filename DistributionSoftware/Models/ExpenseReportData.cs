using System;

namespace DistributionSoftware.Models
{
    public class ExpenseReportData
    {
        public int ExpenseId { get; set; }
        public string ExpenseNumber { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryDescription { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string PaymentMethod { get; set; }
        public string ReferenceNumber { get; set; }
        public string ReceiptNumber { get; set; }
        public string VendorName { get; set; }
        public string VendorContact { get; set; }
        public string VendorPhone { get; set; }
        public string VendorEmail { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }

        // Computed Fields
        public string StatusDescription { get; set; }
        public string PaymentMethodDescription { get; set; }
        public int DaysSinceExpense { get; set; }
        public int? DaysToApproval { get; set; }
        public string ApprovalStatus { get; set; }
        public string AmountCategory { get; set; }
        public int ExpenseYear { get; set; }
        public int ExpenseMonth { get; set; }
        public string ExpenseMonthName { get; set; }
        public string ExpenseFrequency { get; set; }
    }
}
