using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents tax categories for products and services
    /// </summary>
    public class TaxCategory
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the tax category
        /// </summary>
        public int TaxCategoryId { get; set; }
        
        /// <summary>
        /// Tax category code (e.g., "STD", "EXEMPT", "ZERO")
        /// </summary>
        public string TaxCategoryCode { get; set; }
        
        /// <summary>
        /// Tax category name (e.g., "Standard Rate", "Exempt", "Zero Rated")
        /// </summary>
        public string TaxCategoryName { get; set; }
        
        /// <summary>
        /// Alias for TaxCategoryName for backward compatibility
        /// </summary>
        public string CategoryName { get => TaxCategoryName; set => TaxCategoryName = value; }
        
        /// <summary>
        /// Description of the tax category
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Whether this tax category is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Whether this is a system tax category (cannot be deleted)
        /// </summary>
        public bool IsSystemCategory { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the tax category was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the tax category
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the tax category
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the tax category was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the tax category
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the tax category
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public TaxCategory()
        {
            TaxCategoryCode = string.Empty;
            TaxCategoryName = string.Empty;
            Description = string.Empty;
            IsActive = true;
            CreatedDate = DateTime.Now;
            IsSystemCategory = false;
            CreatedBy = 1;
            CreatedByName = "System User";
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the tax category
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(TaxCategoryCode)) return false;
            if (string.IsNullOrWhiteSpace(TaxCategoryName)) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(TaxCategoryCode))
                errors.Add("Tax category code is required");
            
            if (string.IsNullOrWhiteSpace(TaxCategoryName))
                errors.Add("Tax category name is required");
            
            return errors.ToArray();
        }
        
        #endregion
    }
}
