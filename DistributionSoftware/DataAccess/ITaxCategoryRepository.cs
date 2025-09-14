using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface ITaxCategoryRepository
    {
        // CRUD Operations
        int Create(TaxCategory taxCategory);
        bool Update(TaxCategory taxCategory);
        bool Delete(int taxCategoryId);
        TaxCategory GetById(int taxCategoryId);
        TaxCategory GetByCode(string taxCategoryCode);
        List<TaxCategory> GetAll();
        List<TaxCategory> GetActive();
        
        // Reports
        List<TaxCategory> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetCount(bool? isActive);
    }
}