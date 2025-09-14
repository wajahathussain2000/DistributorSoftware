using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IPricingCalculationService
    {
        // Pricing Calculation Methods
        decimal CalculatePrice(int productId, int? customerId = null, decimal quantity = 1);
        decimal ApplyPricingRule(PricingRule rule, decimal basePrice, decimal quantity = 1);
        PricingRule GetApplicablePricingRule(int productId, int? customerId = null, decimal quantity = 1);
        
        // Discount Calculation Methods
        decimal CalculateDiscount(int productId, int? customerId = null, decimal quantity = 1);
        decimal ApplyDiscountRule(DiscountRule rule, decimal amount, decimal quantity = 1);
        DiscountRule GetApplicableDiscountRule(int productId, int? customerId = null, decimal quantity = 1);
        
        // Automatic Application
        void ApplyPricingToSalesInvoice(SalesInvoice invoice);
        void ApplyPricingToSalesInvoiceDetail(SalesInvoiceDetail detail);
        void ApplyDiscountToSalesInvoice(SalesInvoice invoice);
        void ApplyDiscountToSalesInvoiceDetail(SalesInvoiceDetail detail);
        
        // Validation
        bool ValidatePricingRule(PricingRule rule);
        bool ValidateDiscountRule(DiscountRule rule);
    }
}
