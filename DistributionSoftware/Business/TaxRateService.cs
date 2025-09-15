using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class TaxRateService : ITaxRateService
    {
        private readonly ITaxRateRepository _taxRateRepository;

        public TaxRateService(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public int CreateTaxRate(TaxRate taxRate)
        {
            try
            {
                // Auto-generate unique code if not provided
                if (string.IsNullOrWhiteSpace(taxRate.TaxRateCode))
                {
                    taxRate.TaxRateCode = GenerateUniqueTaxRateCode(taxRate.TaxRateName, taxRate.TaxPercentage);
                }

                if (!ValidateTaxRate(taxRate))
                    return 0;

                if (IsTaxRateExists(taxRate.TaxCategoryId, taxRate.TaxPercentage))
                    return 0;

                return _taxRateRepository.Create(taxRate);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.CreateTaxRate", ex);
                return 0;
            }
        }

        public bool UpdateTaxRate(TaxRate taxRate)
        {
            try
            {
                if (!ValidateTaxRate(taxRate))
                    return false;

                if (IsTaxRateExists(taxRate.TaxCategoryId, taxRate.TaxPercentage, taxRate.TaxRateId))
                    return false;

                return _taxRateRepository.Update(taxRate);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.UpdateTaxRate", ex);
                return false;
            }
        }

        public bool DeleteTaxRate(int taxRateId)
        {
            try
            {
                return _taxRateRepository.Delete(taxRateId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.DeleteTaxRate", ex);
                return false;
            }
        }

        public TaxRate GetTaxRateById(int taxRateId)
        {
            try
            {
                return _taxRateRepository.GetById(taxRateId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.GetTaxRateById", ex);
                return null;
            }
        }

        public List<TaxRate> GetAllTaxRates()
        {
            try
            {
                return _taxRateRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.GetAllTaxRates", ex);
                return new List<TaxRate>();
            }
        }

        public List<TaxRate> GetTaxRatesByCategory(int taxCategoryId)
        {
            try
            {
                return _taxRateRepository.GetByCategoryId(taxCategoryId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.GetTaxRatesByCategory", ex);
                return new List<TaxRate>();
            }
        }

        public List<TaxRate> GetActiveTaxRates()
        {
            try
            {
                return _taxRateRepository.GetActive();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.GetActiveTaxRates", ex);
                return new List<TaxRate>();
            }
        }

        public bool ValidateTaxRate(TaxRate taxRate)
        {
            if (taxRate == null)
                return false;

            if (taxRate.TaxCategoryId <= 0)
                return false;

            if (string.IsNullOrEmpty(taxRate.TaxRateCode))
                return false;

            if (string.IsNullOrEmpty(taxRate.TaxRateName))
                return false;

            if (taxRate.TaxPercentage < 0 || taxRate.TaxPercentage > 100)
                return false;

            return true;
        }

        public string[] GetValidationErrors(TaxRate taxRate)
        {
            var errors = new List<string>();

            if (taxRate == null)
            {
                errors.Add("Tax rate cannot be null");
                return errors.ToArray();
            }

            if (taxRate.TaxCategoryId <= 0)
                errors.Add("Tax category ID is required");

            if (string.IsNullOrEmpty(taxRate.TaxRateCode))
                errors.Add("Tax rate code is required");

            if (string.IsNullOrEmpty(taxRate.TaxRateName))
                errors.Add("Tax rate name is required");

            if (taxRate.TaxPercentage < 0 || taxRate.TaxPercentage > 100)
                errors.Add("Tax percentage must be between 0 and 100");

            return errors.ToArray();
        }

        public bool IsTaxRateExists(int taxCategoryId, decimal rate, int? excludeId = null)
        {
            try
            {
                return _taxRateRepository.IsRateExists(taxCategoryId, rate, excludeId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.IsTaxRateExists", ex);
                return true; // Return true to prevent creation if there's an error
            }
        }

        public List<TaxRate> GetTaxRateReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            try
            {
                return _taxRateRepository.GetReport(startDate, endDate, isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.GetTaxRateReport", ex);
                return new List<TaxRate>();
            }
        }

        public int GetTaxRateCount(bool? isActive)
        {
            try
            {
                return _taxRateRepository.GetCount(isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxRateService.GetTaxRateCount", ex);
                return 0;
            }
        }

        private string GenerateUniqueTaxRateCode(string rateName, decimal percentage)
        {
            if (string.IsNullOrWhiteSpace(rateName))
                return "TR" + DateTime.Now.ToString("yyyyMMddHHmmss");

            // Create code from rate name and percentage
            var baseCode = rateName.Replace(" ", "").ToUpper();
            if (baseCode.Length > 3)
                baseCode = baseCode.Substring(0, 3);
            else if (baseCode.Length < 2)
                baseCode = baseCode.PadRight(2, 'X');

            // Add percentage as suffix
            var percentageSuffix = percentage.ToString("0");
            var code = baseCode + percentageSuffix;

            // Ensure uniqueness
            var finalCode = code;
            var counter = 1;
            while (IsTaxRateCodeExists(finalCode))
            {
                finalCode = code + counter.ToString("00");
                counter++;
            }

            return finalCode;
        }

        private bool IsTaxRateCodeExists(string code)
        {
            try
            {
                var existingRate = _taxRateRepository.GetByCode(code);
                return existingRate != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
