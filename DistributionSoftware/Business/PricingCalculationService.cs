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

                switch (rule.RuleType.ToUpper())
                {
                    case "PERCENTAGE":
                        return basePrice * (1 + rule.Value / 100);
                    case "FIXED_AMOUNT":
                        return basePrice + rule.Value;
                    case "QUANTITY_DISCOUNT":
                        if (quantity >= rule.MinimumQuantity)
                            return basePrice * (1 - rule.Value / 100);
                        return basePrice;
                    case "VOLUME_PRICING":
                        if (quantity >= rule.MinimumQuantity)
                            return basePrice * (rule.Value / 100);
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
                    (r.StartDate <= DateTime.Now) &&
                    (r.EndDate == null || r.EndDate >= DateTime.Now)
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
                if (rule.MinimumQuantity.HasValue && quantity < rule.MinimumQuantity.Value)
                    return 0;

                switch (rule.DiscountType.ToUpper())
                {
                    case "PERCENTAGE":
                        return amount * (rule.DiscountValue / 100);
                    case "FIXED_AMOUNT":
                        return rule.DiscountValue;
                    case "QUANTITY_DISCOUNT":
                        if (quantity >= rule.MinimumQuantity)
                            return amount * (rule.DiscountValue / 100);
                        return 0;
                    default:
                        return 0;
                }
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
                
                // Filter by product and customer
                var applicableRules = rules.Where(r => 
                    r.IsActive && 
                    (r.ProductId == productId || r.ProductId == null) &&
                    (r.CustomerId == customerId || r.CustomerId == null) &&
                    (r.StartDate <= DateTime.Now) &&
                    (r.EndDate == null || r.EndDate >= DateTime.Now) &&
                    (r.MinimumQuantity == null || r.MinimumQuantity <= quantity)
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
                if (rule.Value < 0) return false;
                if (rule.StartDate > rule.EndDate && rule.EndDate.HasValue) return false;
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
                if (rule.StartDate > rule.EndDate && rule.EndDate.HasValue) return false;
                if (rule.MinimumQuantity.HasValue && rule.MinimumQuantity.Value < 0) return false;
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
