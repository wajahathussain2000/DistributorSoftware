using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents permissions for access control
    /// </summary>
    public class Permission
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the permission
        /// </summary>
        public int PermissionId { get; set; }
        
        /// <summary>
        /// Permission code (e.g., "USER_MANAGEMENT", "SALES_VIEW", "REPORTS_EXPORT")
        /// </summary>
        public string PermissionCode { get; set; }
        
        /// <summary>
        /// Permission name
        /// </summary>
        public string PermissionName { get; set; }
        
        /// <summary>
        /// Permission description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Permission category (e.g., "USER_MANAGEMENT", "SALES", "REPORTS", "SYSTEM")
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// Module (alias for Category)
        /// </summary>
        public string Module { get => Category; set => Category = value; }
        
        /// <summary>
        /// Permission type (VIEW, CREATE, UPDATE, DELETE, EXPORT, IMPORT, ADMIN)
        /// </summary>
        public string PermissionType { get; set; }
        
        /// <summary>
        /// Whether this permission is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Whether this is a system permission (cannot be deleted)
        /// </summary>
        public bool IsSystemPermission { get; set; }
        
        /// <summary>
        /// Permission priority (higher number = higher priority)
        /// </summary>
        public int Priority { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the permission was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the permission
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the permission
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the permission was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the permission
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the permission
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public Permission()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            IsSystemPermission = false;
            Priority = 100;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the permission
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(PermissionCode)) return false;
            if (string.IsNullOrWhiteSpace(PermissionName)) return false;
            if (string.IsNullOrWhiteSpace(Category)) return false;
            if (string.IsNullOrWhiteSpace(PermissionType)) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(PermissionCode))
                errors.Add("Permission code is required");
            
            if (string.IsNullOrWhiteSpace(PermissionName))
                errors.Add("Permission name is required");
            
            if (string.IsNullOrWhiteSpace(Category))
                errors.Add("Category is required");
            
            if (string.IsNullOrWhiteSpace(PermissionType))
                errors.Add("Permission type is required");
            
            if (PermissionCode.Length > 50)
                errors.Add("Permission code cannot exceed 50 characters");
            
            if (PermissionName.Length > 100)
                errors.Add("Permission name cannot exceed 100 characters");
            
            return errors.ToArray();
        }
        
        /// <summary>
        /// Gets the full permission display name
        /// </summary>
        /// <returns>Full permission name</returns>
        public string GetFullName()
        {
            return $"{Category} - {PermissionName} ({PermissionType})";
        }
        
        /// <summary>
        /// Checks if this permission allows viewing
        /// </summary>
        /// <returns>True if allows viewing</returns>
        public bool AllowsView()
        {
            return PermissionType == "VIEW" || PermissionType == "ADMIN" || 
                   PermissionType == "CREATE" || PermissionType == "UPDATE" || PermissionType == "DELETE";
        }
        
        /// <summary>
        /// Checks if this permission allows creating
        /// </summary>
        /// <returns>True if allows creating</returns>
        public bool AllowsCreate()
        {
            return PermissionType == "CREATE" || PermissionType == "ADMIN";
        }
        
        /// <summary>
        /// Checks if this permission allows updating
        /// </summary>
        /// <returns>True if allows updating</returns>
        public bool AllowsUpdate()
        {
            return PermissionType == "UPDATE" || PermissionType == "ADMIN";
        }
        
        /// <summary>
        /// Checks if this permission allows deleting
        /// </summary>
        /// <returns>True if allows deleting</returns>
        public bool AllowsDelete()
        {
            return PermissionType == "DELETE" || PermissionType == "ADMIN";
        }
        
        /// <summary>
        /// Checks if this permission allows exporting
        /// </summary>
        /// <returns>True if allows exporting</returns>
        public bool AllowsExport()
        {
            return PermissionType == "EXPORT" || PermissionType == "ADMIN";
        }
        
        /// <summary>
        /// Checks if this permission allows importing
        /// </summary>
        /// <returns>True if allows importing</returns>
        public bool AllowsImport()
        {
            return PermissionType == "IMPORT" || PermissionType == "ADMIN";
        }
        
        /// <summary>
        /// Checks if this is an admin permission
        /// </summary>
        /// <returns>True if admin permission</returns>
        public bool IsAdminPermission()
        {
            return PermissionType == "ADMIN";
        }
        
        #endregion
    }
}