using System;

namespace DistributionSoftware.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string ExpenseCode { get; set; }
        public string Barcode { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } // For display purposes
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ReferenceNumber { get; set; }
        public string PaymentMethod { get; set; }
        // Image properties removed - we only need barcode images
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; } // For display purposes
        public DateTime? ApprovedDate { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByName { get; set; } // For display purposes
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedByName { get; set; } // For display purposes
    }
}
