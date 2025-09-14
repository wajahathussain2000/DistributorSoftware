using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Business
{
    public class PricingRuleService : IPricingRuleService
    {
        private readonly IPricingRuleRepository _pricingRuleRepository;

        public PricingRuleService()
        {
            _pricingRuleRepository = new PricingRuleRepository();
        }

        public PricingRuleService(IPricingRuleRepository pricingRuleRepository)
        {
            _pricingRuleRepository = pricingRuleRepository;
        }

        #region CRUD Operations

        public int CreatePricingRule(PricingRule pricingRule)
        {
            try
            {
                if (!ValidatePricingRule(pricingRule))
                {
                    throw new InvalidOperationException("Invalid pricing rule: " + string.Join(", ", GetValidationErrors(pricingRule)));
                }

                pricingRule.CreatedDate = DateTime.Now;
                return _pricingRuleRepository.CreatePricingRule(pricingRule);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.CreatePricingRule", ex);
                throw;
            }
        }

        public bool UpdatePricingRule(PricingRule pricingRule)
        {
            try
            {
                if (!ValidatePricingRule(pricingRule))
                {
                    throw new InvalidOperationException("Invalid pricing rule: " + string.Join(", ", GetValidationErrors(pricingRule)));
                }

                pricingRule.ModifiedDate = DateTime.Now;
                return _pricingRuleRepository.UpdatePricingRule(pricingRule);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.UpdatePricingRule", ex);
                throw;
            }
        }

        public bool DeletePricingRule(int pricingRuleId)
        {
            try
            {
                return _pricingRuleRepository.DeletePricingRule(pricingRuleId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.DeletePricingRule", ex);
                throw;
            }
        }

        public PricingRule GetPricingRuleById(int pricingRuleId)
        {
            try
            {
                return _pricingRuleRepository.GetPricingRuleById(pricingRuleId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetPricingRuleById", ex);
                throw;
            }
        }

        public List<PricingRule> GetAllPricingRules()
        {
            try
            {
                return _pricingRuleRepository.GetAllPricingRules();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetAllPricingRules", ex);
                throw;
            }
        }

        public List<PricingRule> GetActivePricingRules()
        {
            try
            {
                return _pricingRuleRepository.GetActivePricingRules();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetActivePricingRules", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic

        public bool ValidatePricingRule(PricingRule pricingRule)
        {
            if (pricingRule == null) return false;
            
            var errors = GetValidationErrors(pricingRule);
            return errors.Length == 0;
        }

        public string[] GetValidationErrors(PricingRule pricingRule)
        {
            if (pricingRule == null)
                return new[] { "Pricing rule cannot be null" };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(pricingRule.RuleName))
                errors.Add("Rule name is required");

            if (string.IsNullOrWhiteSpace(pricingRule.PricingType))
                errors.Add("Pricing type is required");

            if (pricingRule.BaseValue < 0)
                errors.Add("Base value cannot be negative");

            if (pricingRule.Priority < 0)
                errors.Add("Priority cannot be negative");

            if (pricingRule.MinQuantity.HasValue && pricingRule.MaxQuantity.HasValue && 
                pricingRule.MinQuantity > pricingRule.MaxQuantity)
                errors.Add("Minimum quantity cannot be greater than maximum quantity");

            if (pricingRule.EffectiveFrom.HasValue && pricingRule.EffectiveTo.HasValue && 
                pricingRule.EffectiveFrom > pricingRule.EffectiveTo)
                errors.Add("Effective from date cannot be greater than effective to date");

            // Validate pricing type specific rules
            switch (pricingRule.PricingType)
            {
                case "PERCENTAGE_MARKUP":
                case "PERCENTAGE_MARGIN":
                    if (pricingRule.BaseValue > 1000) // Allow up to 1000% markup/margin
                        errors.Add("Percentage value seems too high");
                    break;
                
                case "FIXED_PRICE":
                    if (pricingRule.BaseValue <= 0)
                        errors.Add("Fixed price must be greater than zero");
                    break;
                
                case "QUANTITY_BREAK":
                    if (!pricingRule.MinQuantity.HasValue && !pricingRule.MaxQuantity.HasValue)
                        errors.Add("Quantity break pricing requires minimum or maximum quantity");
                    break;
            }

            return errors.ToArray();
        }

        public List<PricingRule> GetPricingRulesForProduct(int productId)
        {
            try
            {
                return _pricingRuleRepository.GetPricingRulesForProduct(productId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetPricingRulesForProduct", ex);
                throw;
            }
        }

        public List<PricingRule> GetPricingRulesForCategory(int categoryId)
        {
            try
            {
                return _pricingRuleRepository.GetPricingRulesForCategory(categoryId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetPricingRulesForCategory", ex);
                throw;
            }
        }

        public List<PricingRule> GetPricingRulesForCustomer(int customerId)
        {
            try
            {
                return _pricingRuleRepository.GetPricingRulesForCustomer(customerId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetPricingRulesForCustomer", ex);
                throw;
            }
        }

        public List<PricingRule> GetPricingRulesForCustomerCategory(int customerCategoryId)
        {
            try
            {
                return _pricingRuleRepository.GetPricingRulesForCustomerCategory(customerCategoryId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetPricingRulesForCustomerCategory", ex);
                throw;
            }
        }

        public PricingRule GetBestPricingRule(int? productId, int? categoryId, int? customerId, int? customerCategoryId, decimal quantity = 1)
        {
            try
            {
                return _pricingRuleRepository.GetBestPricingRule(productId, categoryId, customerId, customerCategoryId, quantity);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetBestPricingRule", ex);
                throw;
            }
        }

        public decimal CalculatePrice(int? productId, int? categoryId, int? customerId, int? customerCategoryId, decimal basePrice, decimal quantity = 1)
        {
            try
            {
                var bestRule = GetBestPricingRule(productId, categoryId, customerId, customerCategoryId, quantity);
                
                if (bestRule == null)
                    return basePrice;

                return bestRule.CalculatePrice(basePrice, quantity);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.CalculatePrice", ex);
                return basePrice; // Return base price if calculation fails
            }
        }

        #endregion

        #region Reports

        public List<PricingRule> GetPricingRuleReport(DateTime? startDate, DateTime? endDate, int? productId, int? categoryId, bool? isActive)
        {
            try
            {
                return _pricingRuleRepository.GetPricingRuleReport(startDate, endDate, productId, categoryId, isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetPricingRuleReport", ex);
                throw;
            }
        }

        public int GetPricingRuleCount(bool? isActive)
        {
            try
            {
                return _pricingRuleRepository.GetPricingRuleCount(isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("PricingRuleService.GetPricingRuleCount", ex);
                throw;
            }
        }

        #endregion
    }
}
