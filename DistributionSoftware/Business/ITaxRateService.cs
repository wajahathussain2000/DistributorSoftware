using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface ITaxRateService
    {
        // CRUD Operations
        int CreateTaxRate(TaxRate taxRate);
        bool UpdateTaxRate(TaxRate taxRate);
        bool DeleteTaxRate(int taxRateId);
        TaxRate GetTaxRateById(int taxRateId);
        List<TaxRate> GetAllTaxRates();
        List<TaxRate> GetTaxRatesByCategory(int taxCategoryId);
        List<TaxRate> GetActiveTaxRates();
        
        // Business Logic
        bool ValidateTaxRate(TaxRate taxRate);
        string[] GetValidationErrors(TaxRate taxRate);
        bool IsTaxRateExists(int taxCategoryId, decimal rate, int? excludeId = null);
        
        // Reports
        List<TaxRate> GetTaxRateReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetTaxRateCount(bool? isActive);
    }
}
