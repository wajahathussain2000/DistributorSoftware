using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface ITaxCategoryService
    {
        // CRUD Operations
        int CreateTaxCategory(TaxCategory taxCategory);
        bool UpdateTaxCategory(TaxCategory taxCategory);
        bool DeleteTaxCategory(int taxCategoryId);
        TaxCategory GetTaxCategoryById(int taxCategoryId);
        TaxCategory GetTaxCategoryByCode(string taxCategoryCode);
        List<TaxCategory> GetAllTaxCategories();
        List<TaxCategory> GetActiveTaxCategories();
        
        // Business Logic
        bool ValidateTaxCategory(TaxCategory taxCategory);
        string[] GetValidationErrors(TaxCategory taxCategory);
        bool IsTaxCategoryCodeExists(string taxCategoryCode, int? excludeId = null);
        
        // Reports
        List<TaxCategory> GetTaxCategoryReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetTaxCategoryCount(bool? isActive);
    }
}
