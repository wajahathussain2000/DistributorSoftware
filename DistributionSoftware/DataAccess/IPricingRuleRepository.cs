using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IPricingRuleRepository
    {
        // CRUD Operations
        int CreatePricingRule(PricingRule pricingRule);
        bool UpdatePricingRule(PricingRule pricingRule);
        bool DeletePricingRule(int pricingRuleId);
        PricingRule GetPricingRuleById(int pricingRuleId);
        List<PricingRule> GetAllPricingRules();
        List<PricingRule> GetActivePricingRules();
        List<PricingRule> GetActive();
        
        // Business Logic
        List<PricingRule> GetPricingRulesForProduct(int productId);
        List<PricingRule> GetPricingRulesForCategory(int categoryId);
        List<PricingRule> GetPricingRulesForCustomer(int customerId);
        List<PricingRule> GetPricingRulesForCustomerCategory(int customerCategoryId);
        PricingRule GetBestPricingRule(int? productId, int? categoryId, int? customerId, int? customerCategoryId, decimal quantity = 1);
        
        // Reports
        List<PricingRule> GetPricingRuleReport(DateTime? startDate, DateTime? endDate, int? productId, int? categoryId, bool? isActive);
        int GetPricingRuleCount(bool? isActive);
    }
}
