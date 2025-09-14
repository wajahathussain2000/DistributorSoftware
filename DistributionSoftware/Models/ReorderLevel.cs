using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents reorder level settings for products
    /// </summary>
    public class ReorderLevel
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the reorder level
        /// </summary>
        public int ReorderLevelId { get; set; }
        
        /// <summary>
        /// Product ID this reorder level applies to
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// Minimum stock level before reorder alert
        /// </summary>
        public decimal MinimumLevel { get; set; }
        
        /// <summary>
        /// Maximum stock level for reorder quantity
        /// </summary>
        public decimal MaximumLevel { get; set; }
        
        /// <summary>
        /// Quantity to reorder when minimum level is reached
        /// </summary>
        public decimal ReorderQuantity { get; set; }
        
        /// <summary>
        /// Whether this reorder level is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Whether alerts are enabled for this reorder level
        /// </summary>
        public bool AlertEnabled { get; set; }
        
        /// <summary>
        /// Date when last alert was sent
        /// </summary>
        public DateTime? LastAlertDate { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the reorder level was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the reorder level
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the reorder level
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the reorder level was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the reorder level
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the reorder level
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Product this reorder level applies to
        /// </summary>
        public Product Product { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public ReorderLevel()
        {
            IsActive = true;
            AlertEnabled = true;
            CreatedDate = DateTime.Now;
            MinimumLevel = 0;
            MaximumLevel = 0;
            ReorderQuantity = 0;
        }
        
        #endregion
        
        #region Calculated Properties
        
        /// <summary>
        /// Whether the current stock level is below minimum
        /// </summary>
        public bool IsBelowMinimum => Product != null && Product.StockQuantity < MinimumLevel;
        
        /// <summary>
        /// Whether the current stock level is above maximum
        /// </summary>
        public bool IsAboveMaximum => Product != null && Product.StockQuantity > MaximumLevel;
        
        /// <summary>
        /// Days since last alert (if any)
        /// </summary>
        public int? DaysSinceLastAlert => LastAlertDate?.Date != null ? (int?)(DateTime.Now - LastAlertDate.Value).Days : null;
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the reorder level
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (ProductId <= 0) return false;
            if (MinimumLevel < 0) return false;
            if (MaximumLevel < 0) return false;
            if (ReorderQuantity < 0) return false;
            if (MinimumLevel > MaximumLevel) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (ProductId <= 0)
                errors.Add("Product is required");
            
            if (MinimumLevel < 0)
                errors.Add("Minimum level cannot be negative");
            
            if (MaximumLevel < 0)
                errors.Add("Maximum level cannot be negative");
            
            if (ReorderQuantity < 0)
                errors.Add("Reorder quantity cannot be negative");
            
            if (MinimumLevel > MaximumLevel)
                errors.Add("Minimum level cannot be greater than maximum level");
            
            return errors.ToArray();
        }
        
        #endregion
    }
}
