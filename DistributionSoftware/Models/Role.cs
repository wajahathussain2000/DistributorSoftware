using System;
using System.Collections.Generic;
using System.Linq; // Added for .Any()

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a user role within the system with associated permissions
    /// </summary>
    public class Role
    {
        #region Properties
        
        /// <summary>
        /// Unique identifier for the role
        /// </summary>
        public int RoleId { get; set; }
        
        /// <summary>
        /// Name of the role (e.g., Admin, Manager, Salesman)
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// Detailed description of the role and its responsibilities
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Indicates whether the role is currently active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Date when the role was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Collection of users assigned to this role
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
        
        /// <summary>
        /// Collection of permissions granted to this role
        /// </summary>
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public Role()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            Users = new HashSet<User>();
            RolePermissions = new HashSet<RolePermission>();
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Checks if the role has a specific permission
        /// </summary>
        /// <param name="permissionCode">The permission code to check</param>
        /// <returns>True if the role has the permission, false otherwise</returns>
        public bool HasPermission(string permissionCode)
        {
            if (RolePermissions == null) return false;
            
            return RolePermissions.Any(rp => 
                rp.IsGranted && 
                rp.Permission != null && 
                rp.Permission.PermissionCode == permissionCode &&
                rp.Permission.IsActive);
        }
        
        #endregion
    }
}

