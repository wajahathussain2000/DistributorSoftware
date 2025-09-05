using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents the relationship between user roles and system permissions
    /// </summary>
    public class RolePermission
    {
        #region Properties
        
        /// <summary>
        /// Unique identifier for the role-permission relationship
        /// </summary>
        public int RolePermissionId { get; set; }
        
        /// <summary>
        /// Reference to the role
        /// </summary>
        public int RoleId { get; set; }
        
        /// <summary>
        /// Reference to the permission
        /// </summary>
        public int PermissionId { get; set; }
        
        /// <summary>
        /// Indicates whether the permission is granted (true) or denied (false)
        /// </summary>
        public bool IsGranted { get; set; }
        
        /// <summary>
        /// Date when this role-permission relationship was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// The role associated with this permission
        /// </summary>
        public virtual Role Role { get; set; }
        
        /// <summary>
        /// The permission associated with this role
        /// </summary>
        public virtual Permission Permission { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public RolePermission()
        {
            IsGranted = true;
            CreatedDate = DateTime.Now;
        }
        
        #endregion
    }
}

