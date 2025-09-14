using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class TaxCalculationService : ITaxCalculationService
    {
        private readonly ITaxCategoryRepository _taxCategoryRepository;
        private readonly ITaxRateRepository _taxRateRepository;

        public TaxCalculationService(ITaxCategoryRepository taxCategoryRepository, ITaxRateRepository taxRateRepository)
        {
            _taxCategoryRepository = taxCategoryRepository;
            _taxRateRepository = taxRateRepository;
        }

        public decimal CalculateTaxAmount(decimal taxableAmount, int taxCategoryId)
        {
            try
            {
                var taxRate = GetEffectiveTaxRate(taxCategoryId);
                return taxRate != null ? taxableAmount * (taxRate.RatePercentage / 100) : 0;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
                return 0;
            }
        }

        public decimal CalculateTaxAmount(decimal taxableAmount, string taxCategoryCode)
        {
            try
            {
                var taxRate = GetEffectiveTaxRate(taxCategoryCode);
                return taxRate != null ? taxableAmount * (taxRate.RatePercentage / 100) : 0;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
                return 0;
            }
        }

        public TaxRate GetEffectiveTaxRate(int taxCategoryId, DateTime? effectiveDate = null)
        {
            try
            {
                var effectiveDateValue = effectiveDate ?? DateTime.Now;
                return _taxRateRepository.GetEffectiveRate(taxCategoryId, effectiveDateValue);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
                return null;
            }
        }

        public TaxRate GetEffectiveTaxRate(string taxCategoryCode, DateTime? effectiveDate = null)
        {
            try
            {
                var taxCategory = GetTaxCategoryByCode(taxCategoryCode);
                if (taxCategory == null) return null;
                
                return GetEffectiveTaxRate(taxCategory.TaxCategoryId, effectiveDate);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
                return null;
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
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
                return new List<TaxCategory>();
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
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
                return null;
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
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
                return null;
            }
        }

        public void ApplyTaxToSalesInvoice(SalesInvoice invoice)
        {
            try
            {
                if (invoice == null) return;

                // Set default tax category if not set
                if (!invoice.TaxCategoryId.HasValue)
                {
                    var defaultTaxCategory = GetTaxCategoryByCode("GST");
                    if (defaultTaxCategory != null)
                    {
                        invoice.TaxCategoryId = defaultTaxCategory.TaxCategoryId;
                    }
                }

                // Calculate tax for the invoice
                if (invoice.TaxCategoryId.HasValue)
                {
                    var taxRate = GetEffectiveTaxRate(invoice.TaxCategoryId.Value);
                    if (taxRate != null)
                    {
                        invoice.TaxPercentage = taxRate.RatePercentage;
                        invoice.TaxAmount = invoice.TaxableAmount * (taxRate.RatePercentage / 100);
                        invoice.TotalAmount = invoice.TaxableAmount + invoice.TaxAmount;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
            }
        }

        public void ApplyTaxToSalesInvoiceDetail(SalesInvoiceDetail detail)
        {
            try
            {
                if (detail == null) return;

                // Set default tax category if not set
                if (!detail.TaxCategoryId.HasValue)
                {
                    var defaultTaxCategory = GetTaxCategoryByCode("GST");
                    if (defaultTaxCategory != null)
                    {
                        detail.TaxCategoryId = defaultTaxCategory.TaxCategoryId;
                    }
                }

                // Calculate tax for the detail
                if (detail.TaxCategoryId.HasValue)
                {
                    var taxRate = GetEffectiveTaxRate(detail.TaxCategoryId.Value);
                    if (taxRate != null)
                    {
                        detail.TaxPercentage = taxRate.RatePercentage;
                        detail.TaxAmount = detail.TaxableAmount * (taxRate.RatePercentage / 100);
                        detail.LineTotal = detail.TaxableAmount + detail.TaxAmount;
                        detail.TotalAmount = detail.LineTotal;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
            }
        }

        public void ApplyTaxToSalesReturn(SalesReturn salesReturn)
        {
            try
            {
                if (salesReturn == null) return;

                // Set default tax category if not set
                if (!salesReturn.TaxCategoryId.HasValue)
                {
                    var defaultTaxCategory = GetTaxCategoryByCode("GST");
                    if (defaultTaxCategory != null)
                    {
                        salesReturn.TaxCategoryId = defaultTaxCategory.TaxCategoryId;
                    }
                }

                // Calculate tax for the return
                if (salesReturn.TaxCategoryId.HasValue)
                {
                    var taxRate = GetEffectiveTaxRate(salesReturn.TaxCategoryId.Value);
                    if (taxRate != null)
                    {
                        salesReturn.TaxPercentage = taxRate.RatePercentage;
                        salesReturn.TaxAmount = salesReturn.TaxableAmount * (taxRate.RatePercentage / 100);
                        salesReturn.TotalAmount = salesReturn.TaxableAmount + salesReturn.TaxAmount;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
            }
        }

        public void ApplyTaxToSalesReturnItem(SalesReturnItem item)
        {
            try
            {
                if (item == null) return;

                // Set default tax category if not set
                if (!item.TaxCategoryId.HasValue)
                {
                    var defaultTaxCategory = GetTaxCategoryByCode("GST");
                    if (defaultTaxCategory != null)
                    {
                        item.TaxCategoryId = defaultTaxCategory.TaxCategoryId;
                    }
                }

                // Calculate tax for the item
                if (item.TaxCategoryId.HasValue)
                {
                    var taxRate = GetEffectiveTaxRate(item.TaxCategoryId.Value);
                    if (taxRate != null)
                    {
                        item.TaxPercentage = taxRate.RatePercentage;
                        item.TaxAmount = item.TaxableAmount * (taxRate.RatePercentage / 100);
                        item.LineTotal = item.TaxableAmount + item.TaxAmount;
                        item.TotalAmount = item.LineTotal;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
            }
        }

        public bool ValidateTaxCategory(int taxCategoryId)
        {
            try
            {
                var taxCategory = GetTaxCategoryById(taxCategoryId);
                return taxCategory != null && taxCategory.IsActive;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
                return false;
            }
        }

        public bool ValidateTaxCategory(string taxCategoryCode)
        {
            try
            {
                var taxCategory = GetTaxCategoryByCode(taxCategoryCode);
                return taxCategory != null && taxCategory.IsActive;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in TaxCalculationService", ex);
                return false;
            }
        }
    }
}
