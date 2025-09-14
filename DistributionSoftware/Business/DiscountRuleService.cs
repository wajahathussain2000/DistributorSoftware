using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class DiscountRuleService : IDiscountRuleService
    {
        private readonly IDiscountRuleRepository _discountRuleRepository;

        public DiscountRuleService()
        {
            _discountRuleRepository = new DiscountRuleRepository();
        }

        public DiscountRuleService(IDiscountRuleRepository discountRuleRepository)
        {
            _discountRuleRepository = discountRuleRepository;
        }

        #region CRUD Operations

        public int CreateDiscountRule(DiscountRule discountRule)
        {
            try
            {
                if (!ValidateDiscountRule(discountRule))
                {
                    throw new InvalidOperationException("Invalid discount rule: " + string.Join(", ", GetValidationErrors(discountRule)));
                }

                discountRule.CreatedDate = DateTime.Now;
                return _discountRuleRepository.CreateDiscountRule(discountRule);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.CreateDiscountRule", ex);
                throw;
            }
        }

        public bool UpdateDiscountRule(DiscountRule discountRule)
        {
            try
            {
                if (!ValidateDiscountRule(discountRule))
                {
                    throw new InvalidOperationException("Invalid discount rule: " + string.Join(", ", GetValidationErrors(discountRule)));
                }

                discountRule.ModifiedDate = DateTime.Now;
                return _discountRuleRepository.UpdateDiscountRule(discountRule);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.UpdateDiscountRule", ex);
                throw;
            }
        }

        public bool DeleteDiscountRule(int discountRuleId)
        {
            try
            {
                return _discountRuleRepository.DeleteDiscountRule(discountRuleId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.DeleteDiscountRule", ex);
                throw;
            }
        }

        public DiscountRule GetDiscountRuleById(int discountRuleId)
        {
            try
            {
                return _discountRuleRepository.GetDiscountRuleById(discountRuleId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetDiscountRuleById", ex);
                throw;
            }
        }

        public List<DiscountRule> GetAllDiscountRules()
        {
            try
            {
                return _discountRuleRepository.GetAllDiscountRules();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetAllDiscountRules", ex);
                throw;
            }
        }

        public List<DiscountRule> GetActiveDiscountRules()
        {
            try
            {
                return _discountRuleRepository.GetActiveDiscountRules();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetActiveDiscountRules", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic

        public bool ValidateDiscountRule(DiscountRule discountRule)
        {
            if (discountRule == null) return false;
            
            var errors = GetValidationErrors(discountRule);
            return errors.Length == 0;
        }

        public string[] GetValidationErrors(DiscountRule discountRule)
        {
            if (discountRule == null)
                return new[] { "Discount rule cannot be null" };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(discountRule.RuleName))
                errors.Add("Rule name is required");

            if (string.IsNullOrWhiteSpace(discountRule.DiscountType))
                errors.Add("Discount type is required");

            if (discountRule.DiscountValue < 0)
                errors.Add("Discount value cannot be negative");

            if (discountRule.Priority < 0)
                errors.Add("Priority cannot be negative");

            if (discountRule.MinQuantity.HasValue && discountRule.MaxQuantity.HasValue && 
                discountRule.MinQuantity > discountRule.MaxQuantity)
                errors.Add("Minimum quantity cannot be greater than maximum quantity");

            if (discountRule.EffectiveFrom.HasValue && discountRule.EffectiveTo.HasValue && 
                discountRule.EffectiveFrom > discountRule.EffectiveTo)
                errors.Add("Effective from date cannot be greater than effective to date");

            // Validate discount type specific rules
            switch (discountRule.DiscountType)
            {
                case "PERCENTAGE":
                    if (discountRule.DiscountValue > 100)
                        errors.Add("Percentage discount cannot exceed 100%");
                    break;
                
                case "FIXED_AMOUNT":
                    if (discountRule.DiscountValue <= 0)
                        errors.Add("Fixed amount discount must be greater than zero");
                    break;
                
                case "QUANTITY_BREAK":
                    if (!discountRule.MinQuantity.HasValue && !discountRule.MaxQuantity.HasValue)
                        errors.Add("Quantity break discount requires minimum or maximum quantity");
                    break;
            }

            return errors.ToArray();
        }

        public List<DiscountRule> GetDiscountRulesForProduct(int productId)
        {
            try
            {
                return _discountRuleRepository.GetDiscountRulesForProduct(productId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetDiscountRulesForProduct", ex);
                throw;
            }
        }

        public List<DiscountRule> GetDiscountRulesForCategory(int categoryId)
        {
            try
            {
                return _discountRuleRepository.GetDiscountRulesForCategory(categoryId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetDiscountRulesForCategory", ex);
                throw;
            }
        }

        public List<DiscountRule> GetDiscountRulesForCustomer(int customerId)
        {
            try
            {
                return _discountRuleRepository.GetDiscountRulesForCustomer(customerId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetDiscountRulesForCustomer", ex);
                throw;
            }
        }

        public List<DiscountRule> GetDiscountRulesForCustomerCategory(int customerCategoryId)
        {
            try
            {
                return _discountRuleRepository.GetDiscountRulesForCustomerCategory(customerCategoryId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetDiscountRulesForCustomerCategory", ex);
                throw;
            }
        }

        public DiscountRule GetBestDiscountRule(int? productId, int? categoryId, int? customerId, int? customerCategoryId, decimal quantity = 1, decimal orderAmount = 0)
        {
            try
            {
                return _discountRuleRepository.GetBestDiscountRule(productId, categoryId, customerId, customerCategoryId, quantity, orderAmount);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetBestDiscountRule", ex);
                throw;
            }
        }

        public decimal CalculateDiscount(int? productId, int? categoryId, int? customerId, int? customerCategoryId, decimal originalAmount, decimal quantity = 1)
        {
            try
            {
                var bestRule = GetBestDiscountRule(productId, categoryId, customerId, customerCategoryId, quantity, originalAmount);
                
                if (bestRule == null)
                    return 0;

                return bestRule.CalculateDiscount(originalAmount, quantity);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.CalculateDiscount", ex);
                return 0; // Return 0 discount if calculation fails
            }
        }

        #endregion

        #region Reports

        public List<DiscountRule> GetDiscountRuleReport(DateTime? startDate, DateTime? endDate, int? productId, int? categoryId, bool? isActive)
        {
            try
            {
                return _discountRuleRepository.GetDiscountRuleReport(startDate, endDate, productId, categoryId, isActive);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetDiscountRuleReport", ex);
                throw;
            }
        }

        public int GetDiscountRuleCount(bool? isActive)
        {
            try
            {
                return _discountRuleRepository.GetDiscountRuleCount(isActive);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleService.GetDiscountRuleCount", ex);
                throw;
            }
        }

        #endregion
    }
}
