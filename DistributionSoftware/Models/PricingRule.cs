using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents pricing rules for products
    /// </summary>
    public class PricingRule
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the pricing rule
        /// </summary>
        public int PricingRuleId { get; set; }
        
        /// <summary>
        /// Name of the pricing rule
        /// </summary>
        public string RuleName { get; set; }
        
        /// <summary>
        /// Description of the pricing rule
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
        /// Pricing type: FIXED_PRICE, PERCENTAGE_MARKUP, PERCENTAGE_MARGIN, QUANTITY_BREAK
        /// </summary>
        public string PricingType { get; set; }
        
        /// <summary>
        /// Alias for PricingType for backward compatibility
        /// </summary>
        public string RuleType { get => PricingType; set => PricingType = value; }
        
        /// <summary>
        /// Base price or markup percentage
        /// </summary>
        public decimal BaseValue { get; set; }
        
        /// <summary>
        /// Alias for BaseValue for backward compatibility
        /// </summary>
        public decimal Value { get => BaseValue; set => BaseValue = value; }
        
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
        /// Minimum quantity for quantity break pricing
        /// </summary>
        public decimal? MinQuantity { get; set; }
        
        /// <summary>
        /// Maximum quantity for quantity break pricing
        /// </summary>
        public decimal? MaxQuantity { get; set; }
        
        /// <summary>
        /// Priority order for rule application
        /// </summary>
        public int Priority { get; set; }
        
        /// <summary>
        /// Whether this rule is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Effective start date
        /// </summary>
        public DateTime? EffectiveFrom { get; set; }
        
        /// <summary>
        /// Effective end date
        /// </summary>
        public DateTime? EffectiveTo { get; set; }
        
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
        public PricingRule()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            Priority = 100;
            PricingType = "FIXED_PRICE";
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the pricing rule
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(RuleName)) return false;
            if (string.IsNullOrWhiteSpace(PricingType)) return false;
            if (BaseValue < 0) return false;
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
            
            if (string.IsNullOrWhiteSpace(PricingType))
                errors.Add("Pricing type is required");
            
            if (BaseValue < 0)
                errors.Add("Base value cannot be negative");
            
            if (Priority < 0)
                errors.Add("Priority cannot be negative");
            
            if (MinQuantity.HasValue && MaxQuantity.HasValue && MinQuantity > MaxQuantity)
                errors.Add("Minimum quantity cannot be greater than maximum quantity");
            
            return errors.ToArray();
        }
        
        /// <summary>
        /// Calculates the price based on the rule
        /// </summary>
        /// <param name="basePrice">Base price to apply the rule to</param>
        /// <param name="quantity">Quantity for quantity-based pricing</param>
        /// <returns>Calculated price</returns>
        public decimal CalculatePrice(decimal basePrice, decimal quantity = 1)
        {
            if (!IsActive) return basePrice;
            
            // Check effective date range
            var now = DateTime.Now;
            if (EffectiveFrom.HasValue && now < EffectiveFrom.Value) return basePrice;
            if (EffectiveTo.HasValue && now > EffectiveTo.Value) return basePrice;
            
            // Check quantity range for quantity break pricing
            if (PricingType == "QUANTITY_BREAK")
            {
                if (MinQuantity.HasValue && quantity < MinQuantity.Value) return basePrice;
                if (MaxQuantity.HasValue && quantity > MaxQuantity.Value) return basePrice;
            }
            
            switch (PricingType)
            {
                case "FIXED_PRICE":
                    return BaseValue;
                
                case "PERCENTAGE_MARKUP":
                    return basePrice * (1 + BaseValue / 100);
                
                case "PERCENTAGE_MARGIN":
                    return basePrice / (1 - BaseValue / 100);
                
                case "QUANTITY_BREAK":
                    return BaseValue;
                
                default:
                    return basePrice;
            }
        }
        
        #endregion
    }
}
