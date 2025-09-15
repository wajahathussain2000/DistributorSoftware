using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents tax rates for different tax categories
    /// </summary>
    public class TaxRate
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the tax rate
        /// </summary>
        public int TaxRateId { get; set; }
        
        /// <summary>
        /// Tax category ID this rate applies to
        /// </summary>
        public int TaxCategoryId { get; set; }
        
        /// <summary>
        /// Tax rate name (e.g., "Sales Tax", "VAT", "GST")
        /// </summary>
        public string TaxRateName { get; set; }
        
        /// <summary>
        /// Tax rate percentage (e.g., 17.5 for 17.5%)
        /// </summary>
        public decimal TaxPercentage { get; set; }
        
        /// <summary>
        /// Alias for TaxPercentage for backward compatibility
        /// </summary>
        public decimal RatePercentage { get => TaxPercentage; set => TaxPercentage = value; }
        
        /// <summary>
        /// Tax rate code (e.g., "STD", "VAT", "GST")
        /// </summary>
        public string TaxRateCode { get; set; }
        
        /// <summary>
        /// Description of the tax rate
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Whether this tax rate is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Whether this is a system tax rate (cannot be deleted)
        /// </summary>
        public bool IsSystemRate { get; set; }
        
        /// <summary>
        /// Effective start date
        /// </summary>
        public DateTime? EffectiveFrom { get; set; }
        
        /// <summary>
        /// Effective end date
        /// </summary>
        public DateTime? EffectiveTo { get; set; }
        
        /// <summary>
        /// Whether this tax is compound (tax on tax)
        /// </summary>
        public bool IsCompound { get; set; }
        
        /// <summary>
        /// Whether this tax is included in price
        /// </summary>
        public bool IsInclusive { get; set; }
        
        /// <summary>
        /// Account ID for tax collection
        /// </summary>
        public int? TaxAccountId { get; set; }
        
        /// <summary>
        /// Account ID for tax payable
        /// </summary>
        public int? TaxPayableAccountId { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the tax rate was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the tax rate
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the tax rate
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the tax rate was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the tax rate
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the tax rate
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Tax category this rate belongs to
        /// </summary>
        public TaxCategory TaxCategory { get; set; }
        
        /// <summary>
        /// Tax category name for display purposes
        /// </summary>
        public string TaxCategoryName { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public TaxRate()
        {
            TaxRateCode = string.Empty;
            TaxRateName = string.Empty;
            Description = string.Empty;
            TaxPercentage = 0;
            IsActive = true;
            CreatedDate = DateTime.Now;
            IsSystemRate = false;
            IsCompound = false;
            IsInclusive = false;
            EffectiveFrom = DateTime.Now;
            CreatedBy = 1;
            CreatedByName = "System User";
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the tax rate
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (TaxCategoryId <= 0) return false;
            if (string.IsNullOrWhiteSpace(TaxRateName)) return false;
            if (TaxPercentage < 0) return false;
            if (string.IsNullOrWhiteSpace(TaxRateCode)) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (TaxCategoryId <= 0)
                errors.Add("Tax category is required");
            
            if (string.IsNullOrWhiteSpace(TaxRateName))
                errors.Add("Tax rate name is required");
            
            if (TaxPercentage < 0)
                errors.Add("Tax percentage cannot be negative");
            
            if (string.IsNullOrWhiteSpace(TaxRateCode))
                errors.Add("Tax rate code is required");
            
            if (TaxPercentage > 100)
                errors.Add("Tax percentage seems too high");
            
            if (EffectiveFrom.HasValue && EffectiveTo.HasValue && EffectiveFrom > EffectiveTo)
                errors.Add("Effective from date cannot be greater than effective to date");
            
            return errors.ToArray();
        }
        
        /// <summary>
        /// Calculates tax amount for a given base amount
        /// </summary>
        /// <param name="baseAmount">Base amount to calculate tax on</param>
        /// <returns>Tax amount</returns>
        public decimal CalculateTaxAmount(decimal baseAmount)
        {
            if (!IsActive) return 0;
            
            // Check effective date range
            var now = DateTime.Now;
            if (EffectiveFrom.HasValue && now < EffectiveFrom.Value) return 0;
            if (EffectiveTo.HasValue && now > EffectiveTo.Value) return 0;
            
            return baseAmount * (TaxPercentage / 100);
        }
        
        /// <summary>
        /// Calculates tax-inclusive amount
        /// </summary>
        /// <param name="baseAmount">Base amount</param>
        /// <returns>Amount including tax</returns>
        public decimal CalculateTaxInclusiveAmount(decimal baseAmount)
        {
            var taxAmount = CalculateTaxAmount(baseAmount);
            return baseAmount + taxAmount;
        }
        
        /// <summary>
        /// Calculates tax-exclusive amount from tax-inclusive amount
        /// </summary>
        /// <param name="taxInclusiveAmount">Tax-inclusive amount</param>
        /// <returns>Base amount excluding tax</returns>
        public decimal CalculateTaxExclusiveAmount(decimal taxInclusiveAmount)
        {
            if (!IsActive || TaxPercentage == 0) return taxInclusiveAmount;
            
            // Check effective date range
            var now = DateTime.Now;
            if (EffectiveFrom.HasValue && now < EffectiveFrom.Value) return taxInclusiveAmount;
            if (EffectiveTo.HasValue && now > EffectiveTo.Value) return taxInclusiveAmount;
            
            return taxInclusiveAmount / (1 + (TaxPercentage / 100));
        }
        
        /// <summary>
        /// Checks if the tax rate is currently effective
        /// </summary>
        /// <returns>True if currently effective</returns>
        public bool IsCurrentlyEffective()
        {
            if (!IsActive) return false;
            
            var now = DateTime.Now;
            if (EffectiveFrom.HasValue && now < EffectiveFrom.Value) return false;
            if (EffectiveTo.HasValue && now > EffectiveTo.Value) return false;
            
            return true;
        }
        
        #endregion
    }
}
