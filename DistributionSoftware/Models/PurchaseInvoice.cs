using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class PurchaseInvoice
    {
        public int PurchaseInvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public int SupplierId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string PaymentMode { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal ChangeAmount { get; set; }
        public string Barcode { get; set; }
        public string BarcodeImage { get; set; }
        public bool PrintStatus { get; set; }
        public DateTime? PrintDate { get; set; }
        public int? CashierId { get; set; }
        public DateTime? TransactionTime { get; set; }
        public int? PurchaseAccountId { get; set; }
        public int? CashAccountId { get; set; }
        public int? PayableAccountId { get; set; }
        public int? TaxAccountId { get; set; }
        public int? JournalVoucherId { get; set; }
        public int? TaxCategoryId { get; set; }
        public int? PricingRuleId { get; set; }
        public int? DiscountRuleId { get; set; }

        // Navigation properties
        public string SupplierName { get; set; }
        public List<PurchaseInvoiceDetail> Details { get; set; }

        public PurchaseInvoice()
        {
            Details = new List<PurchaseInvoiceDetail>();
            InvoiceDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            Status = "Pending";
            PaymentMode = "Cash";
            SubTotal = 0;
            TaxAmount = 0;
            DiscountAmount = 0;
            TotalAmount = 0;
            PaidAmount = 0;
            BalanceAmount = 0;
            TaxableAmount = 0;
            TaxPercentage = 0;
            DiscountPercentage = 0;
            ChangeAmount = 0;
            PrintStatus = false;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(InvoiceNumber) &&
                   SupplierId > 0 &&
                   InvoiceDate != default(DateTime) &&
                   TotalAmount >= 0;
        }

        public List<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(InvoiceNumber))
                errors.Add("Invoice number is required");

            if (SupplierId <= 0)
                errors.Add("Supplier is required");

            if (InvoiceDate == default(DateTime))
                errors.Add("Invoice date is required");

            if (TotalAmount < 0)
                errors.Add("Total amount cannot be negative");

            return errors;
        }

        public decimal GetNetAmount()
        {
            return TotalAmount - PaidAmount;
        }

        public bool IsFullyPaid()
        {
            return PaidAmount >= TotalAmount;
        }

        public decimal GetOutstandingAmount()
        {
            return TotalAmount - PaidAmount;
        }
    }
}
