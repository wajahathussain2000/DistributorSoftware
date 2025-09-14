using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IDiscountRuleRepository
    {
        // CRUD Operations
        int CreateDiscountRule(DiscountRule discountRule);
        bool UpdateDiscountRule(DiscountRule discountRule);
        bool DeleteDiscountRule(int discountRuleId);
        DiscountRule GetDiscountRuleById(int discountRuleId);
        List<DiscountRule> GetAllDiscountRules();
        List<DiscountRule> GetActiveDiscountRules();
        List<DiscountRule> GetActive();
        
        // Business Logic
        List<DiscountRule> GetDiscountRulesForProduct(int productId);
        List<DiscountRule> GetDiscountRulesForCategory(int categoryId);
        List<DiscountRule> GetDiscountRulesForCustomer(int customerId);
        List<DiscountRule> GetDiscountRulesForCustomerCategory(int customerCategoryId);
        DiscountRule GetBestDiscountRule(int? productId, int? categoryId, int? customerId, int? customerCategoryId, decimal quantity = 1, decimal orderAmount = 0);
        
        // Reports
        List<DiscountRule> GetDiscountRuleReport(DateTime? startDate, DateTime? endDate, int? productId, int? categoryId, bool? isActive);
        int GetDiscountRuleCount(bool? isActive);
    }
}
