using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class SalesReturn
    {
        public int ReturnId { get; set; }
        public string ReturnNumber { get; set; }
        public string ReturnBarcode { get; set; }
        public int CustomerId { get; set; }
        public int? ReferenceSalesInvoiceId { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } = "PENDING";
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        
        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual SalesInvoice ReferenceSalesInvoice { get; set; }
        public virtual ICollection<SalesReturnItem> SalesReturnItems { get; set; } = new List<SalesReturnItem>();
    }
}

