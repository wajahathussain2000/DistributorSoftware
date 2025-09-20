using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a warehouse in the distribution system
    /// </summary>
    public class Warehouse
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the warehouse
        /// </summary>
        public int WarehouseId { get; set; }
        
        /// <summary>
        /// Name of the warehouse
        /// </summary>
        public string WarehouseName { get; set; }
        
        /// <summary>
        /// Description of the warehouse
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Address of the warehouse
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// City where the warehouse is located
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// State where the warehouse is located
        /// </summary>
        public string State { get; set; }
        
        /// <summary>
        /// Postal code of the warehouse
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// Country where the warehouse is located
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Contact person for the warehouse
        /// </summary>
        public string ContactPerson { get; set; }
        
        /// <summary>
        /// Phone number of the warehouse
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Email address of the warehouse
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Whether the warehouse is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Additional remarks about the warehouse
        /// </summary>
        public string Remarks { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the warehouse was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the warehouse
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the warehouse
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the warehouse was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the warehouse
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the warehouse
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public Warehouse()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            Country = "Pakistan"; // Default country
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the warehouse data
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(WarehouseName)) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public System.Collections.Generic.List<string> GetValidationErrors()
        {
            var errors = new System.Collections.Generic.List<string>();
            
            if (string.IsNullOrWhiteSpace(WarehouseName))
                errors.Add("Warehouse Name is required");
            
            if (!string.IsNullOrWhiteSpace(WarehouseName) && WarehouseName.Length > 100)
                errors.Add("Warehouse Name cannot exceed 100 characters");
            
            if (!string.IsNullOrWhiteSpace(Description) && Description.Length > 500)
                errors.Add("Description cannot exceed 500 characters");
            
            return errors;
        }
        
        /// <summary>
        /// Gets the full address of the warehouse
        /// </summary>
        /// <returns>Full address string</returns>
        public string GetFullAddress()
        {
            var addressParts = new System.Collections.Generic.List<string>();
            
            if (!string.IsNullOrWhiteSpace(Address))
                addressParts.Add(Address);
            
            if (!string.IsNullOrWhiteSpace(City))
                addressParts.Add(City);
            
            if (!string.IsNullOrWhiteSpace(State))
                addressParts.Add(State);
            
            if (!string.IsNullOrWhiteSpace(PostalCode))
                addressParts.Add(PostalCode);
            
            if (!string.IsNullOrWhiteSpace(Country))
                addressParts.Add(Country);
            
            return string.Join(", ", addressParts);
        }
        
        #endregion
    }
}
