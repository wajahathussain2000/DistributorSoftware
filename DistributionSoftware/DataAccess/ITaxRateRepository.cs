using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface ITaxRateRepository
    {
        // CRUD Operations
        int Create(TaxRate taxRate);
        bool Update(TaxRate taxRate);
        bool Delete(int taxRateId);
        TaxRate GetById(int taxRateId);
        List<TaxRate> GetAll();
        List<TaxRate> GetActive();
        List<TaxRate> GetByTaxCategoryId(int taxCategoryId);
        List<TaxRate> GetByCategoryId(int taxCategoryId);
        bool IsRateExists(int taxCategoryId, decimal rate, int? excludeId = null);
        
        // Business Logic
        TaxRate GetEffectiveRate(int taxCategoryId, DateTime effectiveDate);
        TaxRate GetCurrentRate(int taxCategoryId);
        
        // Reports
        List<TaxRate> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetCount(bool? isActive);
    }
}
