using DistributionSoftware.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Interface for permission-related business logic operations
    /// </summary>
    public interface IPermissionService
    {
        #region Permission Management
        
        /// <summary>
        /// Retrieves all active permissions from the system
        /// </summary>
        /// <returns>Collection of active permissions</returns>
        Task<IEnumerable<Permission>> GetAllActivePermissionsAsync();
        
        /// <summary>
        /// Retrieves permissions for a specific module
        /// </summary>
        /// <param name="module">The module name to filter by</param>
        /// <returns>Collection of permissions for the specified module</returns>
        Task<IEnumerable<Permission>> GetPermissionsByModuleAsync(string module);
        
        /// <summary>
        /// Retrieves a permission by its unique code
        /// </summary>
        /// <param name="permissionCode">The permission code to search for</param>
        /// <returns>The permission if found, null otherwise</returns>
        Task<Permission> GetPermissionByCodeAsync(string permissionCode);
        
        /// <summary>
        /// Creates a new permission in the system
        /// </summary>
        /// <param name="permission">The permission to create</param>
        /// <param name="createdByUserId">The ID of the user creating the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> CreatePermissionAsync(Permission permission, int createdByUserId);
        
        /// <summary>
        /// Updates an existing permission in the system
        /// </summary>
        /// <param name="permission">The permission to update</param>
        /// <param name="updatedByUserId">The ID of the user updating the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> UpdatePermissionAsync(Permission permission, int updatedByUserId);
        
        /// <summary>
        /// Deactivates a permission (soft delete)
        /// </summary>
        /// <param name="permissionId">The ID of the permission to deactivate</param>
        /// <param name="deactivatedByUserId">The ID of the user deactivating the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> DeactivatePermissionAsync(int permissionId, int deactivatedByUserId);
        
        #endregion
        
        #region Role Permission Management
        
        /// <summary>
        /// Retrieves all permissions for a specific role
        /// </summary>
        /// <param name="roleId">The role ID to get permissions for</param>
        /// <returns>Collection of permissions for the specified role</returns>
        Task<IEnumerable<Permission>> GetPermissionsByRoleAsync(int roleId);
        
        /// <summary>
        /// Assigns a permission to a role
        /// </summary>
        /// <param name="roleId">The role ID</param>
        /// <param name="permissionId">The permission ID</param>
        /// <param name="isGranted">Whether the permission is granted or denied</param>
        /// <param name="assignedByUserId">The ID of the user assigning the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId, bool isGranted, int assignedByUserId);
        
        /// <summary>
        /// Removes a permission from a role
        /// </summary>
        /// <param name="roleId">The role ID</param>
        /// <param name="permissionId">The permission ID</param>
        /// <param name="removedByUserId">The ID of the user removing the permission</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId, int removedByUserId);
        
        /// <summary>
        /// Checks if a role has a specific permission
        /// </summary>
        /// <param name="roleId">The role ID to check</param>
        /// <param name="permissionCode">The permission code to check for</param>
        /// <returns>True if the role has the permission, false otherwise</returns>
        Task<bool> RoleHasPermissionAsync(int roleId, string permissionCode);
        
        #endregion
        
        #region User Permission Validation
        
        /// <summary>
        /// Checks if a user has a specific permission through their role
        /// </summary>
        /// <param name="userId">The user ID to check</param>
        /// <param name="permissionCode">The permission code to check for</param>
        /// <returns>True if the user has the permission, false otherwise</returns>
        Task<bool> UserHasPermissionAsync(int userId, string permissionCode);
        
        /// <summary>
        /// Gets all permissions for a specific user
        /// </summary>
        /// <param name="userId">The user ID to get permissions for</param>
        /// <returns>Collection of permissions for the specified user</returns>
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId);
        
        #endregion
    }
}




































