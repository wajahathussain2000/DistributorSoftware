using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class SupplierDebitNote
    {
        public int DebitNoteId { get; set; }
        public string DebitNoteNo { get; set; }
        public string DebitNoteBarcode { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public int? ReferencePurchaseId { get; set; }
        public string ReferencePurchaseNo { get; set; }
        public int? ReferenceGRNId { get; set; }
        public string ReferenceGRNNo { get; set; }
        public DateTime DebitDate { get; set; }
        public string Reason { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // DRAFT, PENDING, APPROVED, REJECTED, CANCELLED
        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public byte[] BarcodeImage { get; set; }
        
        // Navigation Properties
        public List<SupplierDebitNoteItem> Items { get; set; }
        
        public SupplierDebitNote()
        {
            Items = new List<SupplierDebitNoteItem>();
            DebitDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            Status = "DRAFT";
            SubTotal = 0;
            TaxAmount = 0;
            DiscountAmount = 0;
            TotalAmount = 0;
        }
        
        // Computed properties
        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case "DRAFT":
                        return "Draft";
                    case "PENDING":
                        return "Pending Approval";
                    case "APPROVED":
                        return "Approved";
                    case "REJECTED":
                        return "Rejected";
                    case "CANCELLED":
                        return "Cancelled";
                    default:
                        return Status;
                }
            }
        }
        
        public string DisplayText => $"{DebitNoteNo} - {SupplierName} ({DebitDate:dd/MM/yyyy})";
    }
}
