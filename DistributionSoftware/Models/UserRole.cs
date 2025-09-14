using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents the relationship between users and roles
    /// </summary>
    public class UserRole
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the user role relationship
        /// </summary>
        public int UserRoleId { get; set; }
        
        /// <summary>
        /// User ID
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Role ID
        /// </summary>
        public int RoleId { get; set; }
        
        /// <summary>
        /// Whether this user role assignment is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Assignment start date
        /// </summary>
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// Assignment end date
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// Assigned date (alias for CreatedDate)
        /// </summary>
        public DateTime AssignedDate { get => CreatedDate; set => CreatedDate = value; }
        
        /// <summary>
        /// Revoked date (alias for EndDate)
        /// </summary>
        public DateTime? RevokedDate { get => EndDate; set => EndDate = value; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the user role was assigned
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who assigned the role
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who assigned the role
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the user role was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the user role
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the user role
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// User this role is assigned to
        /// </summary>
        public User User { get; set; }
        
        /// <summary>
        /// Role assigned to the user
        /// </summary>
        public Role Role { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserRole()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the user role assignment
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (UserId <= 0) return false;
            if (RoleId <= 0) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (UserId <= 0)
                errors.Add("User is required");
            
            if (RoleId <= 0)
                errors.Add("Role is required");
            
            if (StartDate.HasValue && EndDate.HasValue && StartDate > EndDate)
                errors.Add("Start date cannot be greater than end date");
            
            return errors.ToArray();
        }
        
        /// <summary>
        /// Checks if the user role assignment is currently active
        /// </summary>
        /// <returns>True if currently active</returns>
        public bool IsCurrentlyActive()
        {
            if (!IsActive) return false;
            
            var now = DateTime.Now;
            
            if (StartDate.HasValue && now < StartDate.Value) return false;
            if (EndDate.HasValue && now > EndDate.Value) return false;
            
            return true;
        }
        
        /// <summary>
        /// Checks if the user role assignment is expired
        /// </summary>
        /// <returns>True if expired</returns>
        public bool IsExpired()
        {
            return EndDate.HasValue && DateTime.Now > EndDate.Value;
        }
        
        /// <summary>
        /// Checks if the user role assignment is scheduled for future
        /// </summary>
        /// <returns>True if scheduled for future</returns>
        public bool IsScheduledForFuture()
        {
            return StartDate.HasValue && DateTime.Now < StartDate.Value;
        }
        
        #endregion
    }
}
