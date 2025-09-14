using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents discount rules for products and customers
    /// </summary>
    public class DiscountRule
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the discount rule
        /// </summary>
        public int DiscountRuleId { get; set; }
        
        /// <summary>
        /// Name of the discount rule
        /// </summary>
        public string RuleName { get; set; }
        
        /// <summary>
        /// Description of the discount rule
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Product ID this rule applies to (null for all products)
        /// </summary>
        public int? ProductId { get; set; }
        
        /// <summary>
        /// Category ID this rule applies to (null for all categories)
        /// </summary>
        public int? CategoryId { get; set; }
        
        /// <summary>
        /// Customer ID this rule applies to (null for all customers)
        /// </summary>
        public int? CustomerId { get; set; }
        
        /// <summary>
        /// Customer category ID this rule applies to (null for all customer categories)
        /// </summary>
        public int? CustomerCategoryId { get; set; }
        
        /// <summary>
        /// Discount type: PERCENTAGE, FIXED_AMOUNT, QUANTITY_BREAK
        /// </summary>
        public string DiscountType { get; set; }
        
        /// <summary>
        /// Start date for the rule
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// End date for the rule (null for no end date)
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// Minimum quantity for quantity-based rules
        /// </summary>
        public decimal? MinimumQuantity { get; set; }
        
        
        /// <summary>
        /// Discount value (percentage or fixed amount)
        /// </summary>
        public decimal DiscountValue { get; set; }
        
        /// <summary>
        /// Minimum quantity for quantity break discounts
        /// </summary>
        public decimal? MinQuantity { get; set; }
        
        /// <summary>
        /// Maximum quantity for quantity break discounts
        /// </summary>
        public decimal? MaxQuantity { get; set; }
        
        /// <summary>
        /// Minimum order amount required
        /// </summary>
        public decimal? MinOrderAmount { get; set; }
        
        /// <summary>
        /// Maximum discount amount (cap)
        /// </summary>
        public decimal? MaxDiscountAmount { get; set; }
        
        /// <summary>
        /// Priority order for rule application
        /// </summary>
        public int Priority { get; set; }
        
        /// <summary>
        /// Whether this rule is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Whether this is a promotional discount
        /// </summary>
        public bool IsPromotional { get; set; }
        
        /// <summary>
        /// Effective start date
        /// </summary>
        public DateTime? EffectiveFrom { get; set; }
        
        /// <summary>
        /// Effective end date
        /// </summary>
        public DateTime? EffectiveTo { get; set; }
        
        /// <summary>
        /// Usage limit per customer (null for unlimited)
        /// </summary>
        public int? UsageLimitPerCustomer { get; set; }
        
        /// <summary>
        /// Total usage limit (null for unlimited)
        /// </summary>
        public int? TotalUsageLimit { get; set; }
        
        /// <summary>
        /// Current usage count
        /// </summary>
        public int CurrentUsageCount { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the rule was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the rule
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the rule
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the rule was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the rule
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the rule
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Product this rule applies to
        /// </summary>
        public Product Product { get; set; }
        
        /// <summary>
        /// Customer this rule applies to
        /// </summary>
        public Customer Customer { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public DiscountRule()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            Priority = 100;
            DiscountType = "PERCENTAGE";
            CurrentUsageCount = 0;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the discount rule
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(RuleName)) return false;
            if (string.IsNullOrWhiteSpace(DiscountType)) return false;
            if (DiscountValue < 0) return false;
            if (Priority < 0) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(RuleName))
                errors.Add("Rule name is required");
            
            if (string.IsNullOrWhiteSpace(DiscountType))
                errors.Add("Discount type is required");
            
            if (DiscountValue < 0)
                errors.Add("Discount value cannot be negative");
            
            if (Priority < 0)
                errors.Add("Priority cannot be negative");
            
            if (MinQuantity.HasValue && MaxQuantity.HasValue && MinQuantity > MaxQuantity)
                errors.Add("Minimum quantity cannot be greater than maximum quantity");
            
            if (DiscountType == "PERCENTAGE" && DiscountValue > 100)
                errors.Add("Percentage discount cannot exceed 100%");
            
            return errors.ToArray();
        }
        
        /// <summary>
        /// Calculates the discount amount based on the rule
        /// </summary>
        /// <param name="originalAmount">Original amount to apply discount to</param>
        /// <param name="quantity">Quantity for quantity-based discounts</param>
        /// <returns>Discount amount</returns>
        public decimal CalculateDiscount(decimal originalAmount, decimal quantity = 1)
        {
            if (!IsActive) return 0;
            
            // Check effective date range
            var now = DateTime.Now;
            if (EffectiveFrom.HasValue && now < EffectiveFrom.Value) return 0;
            if (EffectiveTo.HasValue && now > EffectiveTo.Value) return 0;
            
            // Check usage limits
            if (TotalUsageLimit.HasValue && CurrentUsageCount >= TotalUsageLimit.Value) return 0;
            
            // Check minimum order amount
            if (MinOrderAmount.HasValue && originalAmount < MinOrderAmount.Value) return 0;
            
            // Check quantity range for quantity break discounts
            if (DiscountType == "QUANTITY_BREAK")
            {
                if (MinQuantity.HasValue && quantity < MinQuantity.Value) return 0;
                if (MaxQuantity.HasValue && quantity > MaxQuantity.Value) return 0;
            }
            
            decimal discountAmount = 0;
            
            switch (DiscountType)
            {
                case "PERCENTAGE":
                    discountAmount = originalAmount * (DiscountValue / 100);
                    break;
                
                case "FIXED_AMOUNT":
                    discountAmount = DiscountValue;
                    break;
                
                case "QUANTITY_BREAK":
                    discountAmount = originalAmount * (DiscountValue / 100);
                    break;
            }
            
            // Apply maximum discount cap
            if (MaxDiscountAmount.HasValue && discountAmount > MaxDiscountAmount.Value)
                discountAmount = MaxDiscountAmount.Value;
            
            return Math.Min(discountAmount, originalAmount); // Cannot discount more than the original amount
        }
        
        /// <summary>
        /// Checks if the rule can be applied
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <param name="productId">Product ID</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="orderAmount">Order amount</param>
        /// <returns>True if rule can be applied</returns>
        public bool CanApply(int? customerId, int? productId, decimal quantity, decimal orderAmount)
        {
            if (!IsActive) return false;
            
            // Check effective date range
            var now = DateTime.Now;
            if (EffectiveFrom.HasValue && now < EffectiveFrom.Value) return false;
            if (EffectiveTo.HasValue && now > EffectiveTo.Value) return false;
            
            // Check usage limits
            if (TotalUsageLimit.HasValue && CurrentUsageCount >= TotalUsageLimit.Value) return false;
            
            // Check minimum order amount
            if (MinOrderAmount.HasValue && orderAmount < MinOrderAmount.Value) return false;
            
            // Check quantity range
            if (MinQuantity.HasValue && quantity < MinQuantity.Value) return false;
            if (MaxQuantity.HasValue && quantity > MaxQuantity.Value) return false;
            
            // Check product/customer applicability
            if (ProductId.HasValue && ProductId.Value != productId) return false;
            if (CustomerId.HasValue && CustomerId.Value != customerId) return false;
            
            return true;
        }
        
        #endregion
    }
}
