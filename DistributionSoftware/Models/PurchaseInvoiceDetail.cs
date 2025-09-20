using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class PurchaseInvoiceDetail
    {
        public int PurchaseInvoiceDetailId { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Remarks { get; set; }

        public PurchaseInvoiceDetail()
        {
            Quantity = 0;
            UnitPrice = 0;
            TotalPrice = 0;
            DiscountPercentage = 0;
            DiscountAmount = 0;
            TaxPercentage = 0;
            TaxAmount = 0;
            NetAmount = 0;
        }

        public bool IsValid()
        {
            return ProductId > 0 &&
                   Quantity > 0 &&
                   UnitPrice >= 0;
        }

        public List<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if (ProductId <= 0)
                errors.Add("Product is required");

            if (Quantity <= 0)
                errors.Add("Quantity must be greater than zero");

            if (UnitPrice < 0)
                errors.Add("Unit price cannot be negative");

            return errors;
        }

        public void CalculateAmounts()
        {
            TotalPrice = Quantity * UnitPrice;
            DiscountAmount = TotalPrice * (DiscountPercentage / 100);
            var taxableAmount = TotalPrice - DiscountAmount;
            TaxAmount = taxableAmount * (TaxPercentage / 100);
            NetAmount = taxableAmount + TaxAmount;
        }
    }
}
