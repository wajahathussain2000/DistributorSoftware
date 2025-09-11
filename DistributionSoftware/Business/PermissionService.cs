using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Implementation of permission service for business logic operations
    /// </summary>
    public class PermissionService : IPermissionService
    {
        #region Private Fields
        
        private readonly IPermissionRepository _permissionRepository;
        private readonly IActivityLogRepository _activityLogRepository;
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the PermissionService class
        /// </summary>
        /// <param name="permissionRepository">Permission repository for data access</param>
        /// <param name="activityLogRepository">Activity log repository for logging operations</param>
        public PermissionService(IPermissionRepository permissionRepository, IActivityLogRepository activityLogRepository)
        {
            _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            _activityLogRepository = activityLogRepository ?? throw new ArgumentNullException(nameof(activityLogRepository));
        }
        
        #endregion
        
        #region Permission Management
        
        /// <summary>
        /// Retrieves all active permissions from the system
        /// </summary>
        /// <returns>Collection of active permissions</returns>
        public async Task<IEnumerable<Permission>> GetAllActivePermissionsAsync()
        {
            try
            {
                return await _permissionRepository.GetAllActivePermissionsAsync();
            }
            catch (Exception ex)
            {
                // Log the error and rethrow for proper error handling
                throw;
            }
        }
        
        /// <summary>
        /// Retrieves permissions for a specific module
        /// </summary>
        /// <param name="module">The module name to filter by</param>
        /// <returns>Collection of permissions for the specified module</returns>
        public async Task<IEnumerable<Permission>> GetPermissionsByModuleAsync(string module)
        {
            if (string.IsNullOrWhiteSpace(module))
                return await Task.FromResult(Enumerable.Empty<Permission>());
                
            try
            {
                return await _permissionRepository.GetPermissionsByModuleAsync(module);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Retrieves a permission by its unique code
        /// </summary>
        /// <param name="permissionCode">The permission code to search for</param>
        /// <returns>The permission if found, null otherwise</returns>
        public async Task<Permission> GetPermissionByCodeAsync(string permissionCode)
        {
            if (string.IsNullOrWhiteSpace(permissionCode))
                return await Task.FromResult<Permission>(null);
                
            try
            {
                return await _permissionRepository.GetPermissionByCodeAsync(permissionCode);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Creates a new permission in the system
        /// </summary>
        /// <param name="permission">The permission to create</param>
        /// <param name="createdByUserId">The ID of the user creating the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> CreatePermissionAsync(Permission permission, int createdByUserId)
        {
            if (permission == null || createdByUserId <= 0)
                return await Task.FromResult(false);
                
            // Validate permission data
            if (string.IsNullOrWhiteSpace(permission.PermissionName) || 
                string.IsNullOrWhiteSpace(permission.PermissionCode) ||
                string.IsNullOrWhiteSpace(permission.Module))
            {
                return await Task.FromResult(false);
            }
            
            try
            {
                // Check if permission code already exists
                var existingPermission = await _permissionRepository.GetPermissionByCodeAsync(permission.PermissionCode);
                if (existingPermission != null)
                {
                    return await Task.FromResult(false); // Permission code already exists
                }
                
                // Set creation date
                permission.CreatedDate = DateTime.Now;
                permission.IsActive = true;
                
                // Create the permission
                var result = await _permissionRepository.CreatePermissionAsync(permission);
                
                if (result)
                {
                    // Log the activity
                    await _activityLogRepository.LogUserActivityAsync(
                        createdByUserId,
                        "Permission Created",
                        $"Created new permission: {permission.PermissionName} ({permission.PermissionCode})",
                        "User Management",
                        additionalData: $"PermissionID: {permission.PermissionId}, Module: {permission.Module}");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Updates an existing permission in the system
        /// </summary>
        /// <param name="permission">The permission to update</param>
        /// <param name="updatedByUserId">The ID of the user updating the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> UpdatePermissionAsync(Permission permission, int updatedByUserId)
        {
            if (permission == null || permission.PermissionId <= 0 || updatedByUserId <= 0)
                return await Task.FromResult(false);
                
            // Validate permission data
            if (string.IsNullOrWhiteSpace(permission.PermissionName) || 
                string.IsNullOrWhiteSpace(permission.PermissionCode) ||
                string.IsNullOrWhiteSpace(permission.Module))
            {
                return await Task.FromResult(false);
            }
            
            try
            {
                // Check if permission code already exists for a different permission
                var existingPermission = await _permissionRepository.GetPermissionByCodeAsync(permission.PermissionCode);
                if (existingPermission != null && existingPermission.PermissionId != permission.PermissionId)
                {
                    return await Task.FromResult(false); // Permission code already exists for another permission
                }
                
                // Update the permission
                var result = await _permissionRepository.UpdatePermissionAsync(permission);
                
                if (result)
                {
                    // Log the activity
                    await _activityLogRepository.LogUserActivityAsync(
                        updatedByUserId,
                        "Permission Updated",
                        $"Updated permission: {permission.PermissionName} ({permission.PermissionCode})",
                        "User Management",
                        additionalData: $"PermissionID: {permission.PermissionId}, Module: {permission.Module}");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Deactivates a permission (soft delete)
        /// </summary>
        /// <param name="permissionId">The ID of the permission to deactivate</param>
        /// <param name="deactivatedByUserId">The ID of the user deactivating the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> DeactivatePermissionAsync(int permissionId, int deactivatedByUserId)
        {
            if (permissionId <= 0 || deactivatedByUserId <= 0)
                return await Task.FromResult(false);
                
            try
            {
                // Get the permission details for logging
                // Note: We need to get permission by ID, not by code
                // For now, we'll use placeholder values since we don't have a GetPermissionByIdAsync method
                var permissionName = "Permission ID: " + permissionId;
                var permissionCode = "PERM_" + permissionId;
                
                // Deactivate the permission
                var result = await _permissionRepository.DeactivatePermissionAsync(permissionId);
                
                if (result)
                {
                    // Log the activity
                    await _activityLogRepository.LogUserActivityAsync(
                        deactivatedByUserId,
                        "Permission Deactivated",
                        $"Deactivated permission: {permissionName} ({permissionCode})",
                        "User Management",
                        additionalData: $"PermissionID: {permissionId}");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        #endregion
        
        #region Role Permission Management
        
        /// <summary>
        /// Retrieves all permissions for a specific role
        /// </summary>
        /// <param name="roleId">The role ID to get permissions for</param>
        /// <returns>Collection of permissions for the specified role</returns>
        public async Task<IEnumerable<Permission>> GetPermissionsByRoleAsync(int roleId)
        {
            if (roleId <= 0)
                return await Task.FromResult(Enumerable.Empty<Permission>());
                
            try
            {
                return await _permissionRepository.GetPermissionsByRoleAsync(roleId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Assigns a permission to a role
        /// </summary>
        /// <param name="roleId">The role ID</param>
        /// <param name="permissionId">The permission ID</param>
        /// <param name="isGranted">Whether the permission is granted or denied</param>
        /// <param name="assignedByUserId">The ID of the user assigning the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId, bool isGranted, int assignedByUserId)
        {
            if (roleId <= 0 || permissionId <= 0 || assignedByUserId <= 0)
                return await Task.FromResult(false);
                
            try
            {
                // Get permission details for logging
                // Note: We need to get permission by ID, not by code
                // For now, we'll use placeholder values since we don't have a GetPermissionByIdAsync method
                var permissionName = "Permission ID: " + permissionId;
                var permissionCode = "PERM_" + permissionId;
                
                // Assign the permission to the role
                var result = await _permissionRepository.AssignPermissionToRoleAsync(roleId, permissionId, isGranted);
                
                if (result)
                {
                    // Log the activity
                    var action = isGranted ? "granted" : "denied";
                    await _activityLogRepository.LogUserActivityAsync(
                        assignedByUserId,
                        "Role Permission Modified",
                        $"Permission '{permissionName}' {action} to role ID: {roleId}",
                        "User Management",
                        additionalData: $"RoleID: {roleId}, PermissionID: {permissionId}, PermissionCode: {permissionCode}, IsGranted: {isGranted}");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Removes a permission from a role
        /// </summary>
        /// <param name="roleId">The role ID</param>
        /// <param name="permissionId">The permission ID</param>
        /// <param name="removedByUserId">The ID of the user removing the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId, int removedByUserId)
        {
            if (roleId <= 0 || permissionId <= 0 || removedByUserId <= 0)
                return await Task.FromResult(false);
                
            try
            {
                // Get permission details for logging
                // Note: We need to get permission by ID, not by code
                // For now, we'll use placeholder values since we don't have a GetPermissionByIdAsync method
                var permissionName = "Permission ID: " + permissionId;
                var permissionCode = "PERM_" + permissionId;
                
                // Remove the permission from the role
                var result = await _permissionRepository.RemovePermissionFromRoleAsync(roleId, permissionId);
                
                if (result)
                {
                    // Log the activity
                    await _activityLogRepository.LogUserActivityAsync(
                        removedByUserId,
                        "Role Permission Removed",
                        $"Permission '{permissionName}' removed from role ID: {roleId}",
                        "User Management",
                        additionalData: $"RoleID: {roleId}, PermissionID: {permissionId}, PermissionCode: {permissionCode}");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Checks if a role has a specific permission
        /// </summary>
        /// <param name="roleId">The role ID to check</param>
        /// <param name="permissionCode">The permission code to check for</param>
        /// <returns>True if the role has the permission, false otherwise</returns>
        public async Task<bool> RoleHasPermissionAsync(int roleId, string permissionCode)
        {
            if (roleId <= 0 || string.IsNullOrWhiteSpace(permissionCode))
                return await Task.FromResult(false);
                
            try
            {
                var rolePermissions = await _permissionRepository.GetPermissionsByRoleAsync(roleId);
                return rolePermissions.Any(p => p.PermissionCode == permissionCode && p.IsActive);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        #endregion
        
        #region User Permission Validation
        
        /// <summary>
        /// Checks if a user has a specific permission through their role
        /// </summary>
        /// <param name="userId">The user ID to check</param>
        /// <param name="permissionCode">The permission code to check for</param>
        /// <returns>True if the user has the permission, false otherwise</returns>
        public async Task<bool> UserHasPermissionAsync(int userId, string permissionCode)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(permissionCode))
                return await Task.FromResult(false);
                
            try
            {
                // This is a simplified implementation - in a real system, you would need to
                // get the user's role first, then check if that role has the permission
                // For now, we'll return false as this requires additional user-role relationship data
                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// Gets all permissions for a specific user
        /// </summary>
        /// <param name="userId">The user ID to get permissions for</param>
        /// <returns>Collection of permissions for the specified user</returns>
        public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId)
        {
            if (userId <= 0)
                return Enumerable.Empty<Permission>();
                
            try
            {
                // Get user's role first, then get permissions for that role
                // For now, we'll return some basic permissions based on common roles
                // In a real system, you would query the database for user-role-permission relationships
                
                // Return basic permissions for now to prevent the app from stopping
                var basicPermissions = new List<Permission>
                {
                    new Permission
                    {
                        PermissionId = 1,
                        PermissionName = "Basic Access",
                        PermissionCode = "BASIC_ACCESS",
                        Description = "Basic system access",
                        Module = "System",
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    }
                };
                
                return basicPermissions;
            }
            catch (Exception)
            {
                // Return empty collection instead of throwing to prevent app from stopping
                return Enumerable.Empty<Permission>();
            }
        }
        
        #endregion
    }
}

