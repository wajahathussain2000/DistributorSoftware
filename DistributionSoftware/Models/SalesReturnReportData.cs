using System;

namespace DistributionSoftware.Models
{
    public class SalesReturnReportData
    {
        public int ReturnId { get; set; }
        public string ReturnNumber { get; set; }
        public string ReturnBarcode { get; set; }
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public int? ReferenceSalesInvoiceId { get; set; }
        public string ReferenceInvoiceNumber { get; set; }
        public DateTime? ReferenceInvoiceDate { get; set; }
        public int? SalesmanId { get; set; }
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public string SalesmanPhone { get; set; }
        public string Territory { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Reason { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedByUser { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public int? ProcessedBy { get; set; }
        public string ProcessedByUser { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedByUser { get; set; }
        public int? TaxCategoryId { get; set; }
        public string TaxCategoryName { get; set; }

        // Calculated fields
        public string StatusDescription { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TaxPercentage { get; set; }
        public int DaysSinceReturn { get; set; }
        public int? ProcessingDays { get; set; }
        public int TotalItems { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? ItemsTotalAmount { get; set; }
    }
}
