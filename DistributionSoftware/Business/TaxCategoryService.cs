using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class TaxCategoryService : ITaxCategoryService
    {
        private readonly ITaxCategoryRepository _taxCategoryRepository;

        public TaxCategoryService(ITaxCategoryRepository taxCategoryRepository)
        {
            _taxCategoryRepository = taxCategoryRepository;
        }

        public int CreateTaxCategory(TaxCategory taxCategory)
        {
            try
            {
                if (!ValidateTaxCategory(taxCategory))
                    throw new InvalidOperationException("Tax category validation failed");

                return _taxCategoryRepository.Create(taxCategory);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                throw;
            }
        }

        public bool UpdateTaxCategory(TaxCategory taxCategory)
        {
            try
            {
                if (!ValidateTaxCategory(taxCategory))
                    throw new InvalidOperationException("Tax category validation failed");

                return _taxCategoryRepository.Update(taxCategory);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                throw;
            }
        }

        public bool DeleteTaxCategory(int taxCategoryId)
        {
            try
            {
                return _taxCategoryRepository.Delete(taxCategoryId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                throw;
            }
        }

        public TaxCategory GetTaxCategoryById(int taxCategoryId)
        {
            try
            {
                return _taxCategoryRepository.GetById(taxCategoryId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                throw;
            }
        }

        public TaxCategory GetTaxCategoryByCode(string taxCategoryCode)
        {
            try
            {
                return _taxCategoryRepository.GetByCode(taxCategoryCode);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                throw;
            }
        }

        public List<TaxCategory> GetAllTaxCategories()
        {
            try
            {
                return _taxCategoryRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                throw;
            }
        }

        public List<TaxCategory> GetActiveTaxCategories()
        {
            try
            {
                return _taxCategoryRepository.GetActive();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                throw;
            }
        }

        public bool ValidateTaxCategory(TaxCategory taxCategory)
        {
            return GetValidationErrors(taxCategory).Length == 0;
        }

        public string[] GetValidationErrors(TaxCategory taxCategory)
        {
            var errors = new List<string>();

            if (taxCategory == null)
            {
                errors.Add("Tax category cannot be null");
                return errors.ToArray();
            }

            if (string.IsNullOrWhiteSpace(taxCategory.CategoryName))
                errors.Add("Category name is required");

            if (taxCategory.CategoryName != null && taxCategory.CategoryName.Length > 100)
                errors.Add("Category name cannot exceed 100 characters");

            if (taxCategory.Description != null && taxCategory.Description.Length > 500)
                errors.Add("Description cannot exceed 500 characters");

            return errors.ToArray();
        }

        public bool IsTaxCategoryCodeExists(string taxCategoryCode, int? excludeId = null)
        {
            try
            {
                var existingCategory = GetTaxCategoryByCode(taxCategoryCode);
                return existingCategory != null && existingCategory.TaxCategoryId != excludeId;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                return false;
            }
        }

        public List<TaxCategory> GetTaxCategoryReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            try
            {
                return _taxCategoryRepository.GetReport(startDate, endDate, isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                throw;
            }
        }

        public int GetTaxCategoryCount(bool? isActive)
        {
            try
            {
                return _taxCategoryRepository.GetCount(isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCategoryService", ex);
                throw;
            }
        }
    }
}
