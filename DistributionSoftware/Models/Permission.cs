using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a system permission that can be assigned to user roles
    /// </summary>
    public class Permission
    {
        #region Properties
        
        /// <summary>
        /// Unique identifier for the permission
        /// </summary>
        public int PermissionId { get; set; }
        
        /// <summary>
        /// Human-readable name of the permission
        /// </summary>
        public string PermissionName { get; set; }
        
        /// <summary>
        /// Unique code identifier for the permission (e.g., USER_CREATE)
        /// </summary>
        public string PermissionCode { get; set; }
        
        /// <summary>
        /// Detailed description of what the permission allows
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Module or area of the system this permission applies to
        /// </summary>
        public string Module { get; set; }
        
        /// <summary>
        /// Indicates whether the permission is currently active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Date when the permission was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public Permission()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
        
        #endregion
    }
}

