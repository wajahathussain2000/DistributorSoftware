using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class PricingCalculationService : IPricingCalculationService
    {
        private readonly IPricingRuleRepository _pricingRuleRepository;
        private readonly IDiscountRuleRepository _discountRuleRepository;
        private readonly IProductRepository _productRepository;

        public PricingCalculationService(IPricingRuleRepository pricingRuleRepository, IDiscountRuleRepository discountRuleRepository, IProductRepository productRepository)
        {
            _pricingRuleRepository = pricingRuleRepository;
            _discountRuleRepository = discountRuleRepository;
            _productRepository = productRepository;
        }

        public decimal CalculatePrice(int productId, int? customerId = null, decimal quantity = 1)
        {
            try
            {
                var product = _productRepository.GetById(productId);
                if (product == null) return 0;

                var basePrice = product.Price;
                var pricingRule = GetApplicablePricingRule(productId, customerId, quantity);
                
                if (pricingRule != null)
                {
                    return ApplyPricingRule(pricingRule, basePrice, quantity);
                }

                return basePrice;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
                return 0;
            }
        }

        public decimal ApplyPricingRule(PricingRule rule, decimal basePrice, decimal quantity = 1)
        {
            try
            {
                if (rule == null || !rule.IsActive) return basePrice;

                switch (rule.PricingType.ToUpper())
                {
                    case "FIXED_PRICE":
                        return rule.BaseValue;
                    case "PERCENTAGE_MARKUP":
                        return basePrice * (1 + rule.BaseValue / 100);
                    case "PERCENTAGE_MARGIN":
                        return basePrice / (1 - rule.BaseValue / 100);
                    case "QUANTITY_BREAK":
                        if (rule.MinQuantity.HasValue && quantity >= rule.MinQuantity.Value)
                            return rule.BaseValue;
                        return basePrice;
                    default:
                        return basePrice;
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
                return basePrice;
            }
        }

        public PricingRule GetApplicablePricingRule(int productId, int? customerId = null, decimal quantity = 1)
        {
            try
            {
                var rules = _pricingRuleRepository.GetActive();
                
                // Filter by product and customer
                var applicableRules = rules.Where(r => 
                    r.IsActive && 
                    (r.ProductId == productId || r.ProductId == null) &&
                    (r.CustomerId == customerId || r.CustomerId == null) &&
                    (r.EffectiveFrom == null || r.EffectiveFrom <= DateTime.Now) &&
                    (r.EffectiveTo == null || r.EffectiveTo >= DateTime.Now) &&
                    (r.MinQuantity == null || r.MinQuantity <= quantity) &&
                    (r.MaxQuantity == null || r.MaxQuantity >= quantity)
                ).OrderByDescending(r => r.Priority);

                // Return the highest priority rule
                return applicableRules.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
                return null;
            }
        }

        public decimal CalculateDiscount(int productId, int? customerId = null, decimal quantity = 1)
        {
            try
            {
                var discountRule = GetApplicableDiscountRule(productId, customerId, quantity);
                
                if (discountRule != null)
                {
                    var product = _productRepository.GetById(productId);
                    if (product != null)
                    {
                        return ApplyDiscountRule(discountRule, product.Price, quantity);
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
                return 0;
            }
        }

        public decimal ApplyDiscountRule(DiscountRule rule, decimal amount, decimal quantity = 1)
        {
            try
            {
                if (rule == null || !rule.IsActive) return 0;

                // Check minimum quantity requirement
                if (rule.MinQuantity.HasValue && quantity < rule.MinQuantity.Value)
                    return 0;

                // Check maximum quantity requirement
                if (rule.MaxQuantity.HasValue && quantity > rule.MaxQuantity.Value)
                    return 0;

                decimal discountAmount = 0;

                switch (rule.DiscountType.ToUpper())
                {
                    case "PERCENTAGE":
                        discountAmount = amount * (rule.DiscountValue / 100);
                        break;
                    case "FIXED_AMOUNT":
                        discountAmount = rule.DiscountValue;
                        break;
                    case "QUANTITY_BREAK":
                        discountAmount = amount * (rule.DiscountValue / 100);
                        break;
                    default:
                        return 0;
                }

                // Apply maximum discount cap
                if (rule.MaxDiscountAmount.HasValue && discountAmount > rule.MaxDiscountAmount.Value)
                    discountAmount = rule.MaxDiscountAmount.Value;

                return Math.Min(discountAmount, amount); // Cannot discount more than the original amount
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
                return 0;
            }
        }

        public DiscountRule GetApplicableDiscountRule(int productId, int? customerId = null, decimal quantity = 1)
        {
            try
            {
                var rules = _discountRuleRepository.GetActive();
                System.Diagnostics.Debug.WriteLine($"Found {rules.Count} active discount rules");
                
                // Filter by product and customer
                var applicableRules = rules.Where(r => 
                    r.IsActive && 
                    (r.ProductId == productId || r.ProductId == null) &&
                    (r.CustomerId == customerId || r.CustomerId == null) &&
                    (r.EffectiveFrom == null || r.EffectiveFrom <= DateTime.Now) &&
                    (r.EffectiveTo == null || r.EffectiveTo >= DateTime.Now) &&
                    (r.MinQuantity == null || r.MinQuantity <= quantity) &&
                    (r.MaxQuantity == null || r.MaxQuantity >= quantity)
                ).OrderByDescending(r => r.Priority);

                var applicableRule = applicableRules.FirstOrDefault();
                System.Diagnostics.Debug.WriteLine($"Applicable rule: {applicableRule?.RuleName} (Priority: {applicableRule?.Priority})");
                
                // Return the highest priority rule
                return applicableRule;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
                return null;
            }
        }

        public void ApplyPricingToSalesInvoice(SalesInvoice invoice)
        {
            try
            {
                if (invoice == null || invoice.Items == null) return;

                foreach (var item in invoice.Items)
                {
                    ApplyPricingToSalesInvoiceDetail(item);
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
            }
        }

        public void ApplyPricingToSalesInvoiceDetail(SalesInvoiceDetail detail)
        {
            try
            {
                if (detail == null) return;

                var pricingRule = GetApplicablePricingRule(detail.ProductId, null, detail.Quantity);
                
                if (pricingRule != null)
                {
                    detail.PricingRuleId = pricingRule.PricingRuleId;
                    detail.UnitPrice = ApplyPricingRule(pricingRule, detail.UnitPrice, detail.Quantity);
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
            }
        }

        public void ApplyDiscountToSalesInvoice(SalesInvoice invoice)
        {
            try
            {
                if (invoice == null || invoice.Items == null) return;

                foreach (var item in invoice.Items)
                {
                    ApplyDiscountToSalesInvoiceDetail(item);
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
            }
        }

        public void ApplyDiscountToSalesInvoiceDetail(SalesInvoiceDetail detail)
        {
            try
            {
                if (detail == null) return;

                var discountRule = GetApplicableDiscountRule(detail.ProductId, null, detail.Quantity);
                
                if (discountRule != null)
                {
                    detail.DiscountRuleId = discountRule.DiscountRuleId;
                    detail.DiscountAmount = ApplyDiscountRule(discountRule, detail.UnitPrice * detail.Quantity, detail.Quantity);
                    detail.DiscountPercentage = (detail.DiscountAmount / (detail.UnitPrice * detail.Quantity)) * 100;
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
            }
        }

        public bool ValidatePricingRule(PricingRule rule)
        {
            try
            {
                if (rule == null) return false;
                if (rule.BaseValue < 0) return false;
                if (rule.EffectiveFrom.HasValue && rule.EffectiveTo.HasValue && rule.EffectiveFrom > rule.EffectiveTo) return false;
                if (rule.MinQuantity.HasValue && rule.MaxQuantity.HasValue && rule.MinQuantity > rule.MaxQuantity) return false;
                return true;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
                return false;
            }
        }

        public bool ValidateDiscountRule(DiscountRule rule)
        {
            try
            {
                if (rule == null) return false;
                if (rule.DiscountValue < 0) return false;
                if (rule.EffectiveFrom.HasValue && rule.EffectiveTo.HasValue && rule.EffectiveFrom > rule.EffectiveTo) return false;
                if (rule.MinQuantity.HasValue && rule.MaxQuantity.HasValue && rule.MinQuantity > rule.MaxQuantity) return false;
                if (rule.MinQuantity.HasValue && rule.MinQuantity.Value < 0) return false;
                return true;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in PricingCalculationService", ex);
                return false;
            }
        }
    }
}
