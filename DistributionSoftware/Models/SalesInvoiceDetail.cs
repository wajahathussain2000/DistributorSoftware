using System;

namespace DistributionSoftware.Models
{
    public class SalesInvoiceDetail
    {
        public int DetailId { get; set; }
        public int SalesInvoiceId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal LineTotal { get; set; }
        public decimal TotalAmount { get; set; } // Database column name
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Remarks { get; set; }
        
        public SalesInvoiceDetail()
        {
            DiscountAmount = 0;
            DiscountPercentage = 0;
            TaxAmount = 0;
            TaxPercentage = 0;
            TotalAmount = 0;
            LineTotal = 0;
        }
    }
}
