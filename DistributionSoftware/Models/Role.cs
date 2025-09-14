using System;
using System.Collections.Generic;
using System.Linq;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents user roles for access control
    /// </summary>
    public class Role
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the role
        /// </summary>
        public int RoleId { get; set; }
        
        /// <summary>
        /// Role name
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// Role description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Whether this role is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Whether this is a system role (cannot be deleted)
        /// </summary>
        public bool IsSystemRole { get; set; }
        
        /// <summary>
        /// Role priority (higher number = higher priority)
        /// </summary>
        public int Priority { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the role was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the role
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the role
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the role was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the role
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the role
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Users assigned to this role
        /// </summary>
        public List<User> Users { get; set; }
        
        /// <summary>
        /// Permissions assigned to this role
        /// </summary>
        public List<Permission> Permissions { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public Role()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            IsSystemRole = false;
            Priority = 100;
            Users = new List<User>();
            Permissions = new List<Permission>();
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the role
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(RoleName)) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(RoleName))
                errors.Add("Role name is required");
            
            if (RoleName.Length > 50)
                errors.Add("Role name cannot exceed 50 characters");
            
            return errors.ToArray();
        }
        
        /// <summary>
        /// Checks if the role has a specific permission
        /// </summary>
        /// <param name="permissionCode">Permission code to check</param>
        /// <returns>True if role has permission</returns>
        public bool HasPermission(string permissionCode)
        {
            return Permissions.Any(p => p.PermissionCode == permissionCode && p.IsActive);
        }
        
        /// <summary>
        /// Adds a permission to the role
        /// </summary>
        /// <param name="permission">Permission to add</param>
        public void AddPermission(Permission permission)
        {
            if (permission != null && !HasPermission(permission.PermissionCode))
            {
                Permissions.Add(permission);
            }
        }
        
        /// <summary>
        /// Removes a permission from the role
        /// </summary>
        /// <param name="permissionCode">Permission code to remove</param>
        public void RemovePermission(string permissionCode)
        {
            var permission = Permissions.FirstOrDefault(p => p.PermissionCode == permissionCode);
            if (permission != null)
            {
                Permissions.Remove(permission);
            }
        }
        
        /// <summary>
        /// Gets the number of users assigned to this role
        /// </summary>
        /// <returns>User count</returns>
        public int GetUserCount()
        {
            return Users?.Count(p => p.IsActive) ?? 0;
        }
        
        /// <summary>
        /// Gets the number of permissions assigned to this role
        /// </summary>
        /// <returns>Permission count</returns>
        public int GetPermissionCount()
        {
            return Permissions?.Count(p => p.IsActive) ?? 0;
        }
        
        #endregion
    }
}