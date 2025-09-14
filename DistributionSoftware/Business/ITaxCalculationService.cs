using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface ITaxCalculationService
    {
        // Tax Calculation Methods
        decimal CalculateTaxAmount(decimal taxableAmount, int taxCategoryId);
        decimal CalculateTaxAmount(decimal taxableAmount, string taxCategoryCode);
        TaxRate GetEffectiveTaxRate(int taxCategoryId, DateTime? effectiveDate = null);
        TaxRate GetEffectiveTaxRate(string taxCategoryCode, DateTime? effectiveDate = null);
        
        // Tax Category Management
        List<TaxCategory> GetActiveTaxCategories();
        TaxCategory GetTaxCategoryById(int taxCategoryId);
        TaxCategory GetTaxCategoryByCode(string taxCategoryCode);
        
        // Automatic Tax Application
        void ApplyTaxToSalesInvoice(SalesInvoice invoice);
        void ApplyTaxToSalesInvoiceDetail(SalesInvoiceDetail detail);
        void ApplyTaxToSalesReturn(SalesReturn salesReturn);
        void ApplyTaxToSalesReturnItem(SalesReturnItem item);
        
        // Validation
        bool ValidateTaxCategory(int taxCategoryId);
        bool ValidateTaxCategory(string taxCategoryCode);
    }
}
