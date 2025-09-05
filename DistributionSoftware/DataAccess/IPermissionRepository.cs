using DistributionSoftware.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.DataAccess
{
    /// <summary>
    /// Interface for permission-related data access operations
    /// </summary>
    public interface IPermissionRepository
    {
        #region Permission Management
        
        /// <summary>
        /// Retrieves all active permissions from the database
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
        /// Creates a new permission in the database
        /// </summary>
        /// <param name="permission">The permission to create</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> CreatePermissionAsync(Permission permission);
        
        /// <summary>
        /// Updates an existing permission in the database
        /// </summary>
        /// <param name="permission">The permission to update</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> UpdatePermissionAsync(Permission permission);
        
        /// <summary>
        /// Deactivates a permission (soft delete)
        /// </summary>
        /// <param name="permissionId">The ID of the permission to deactivate</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> DeactivatePermissionAsync(int permissionId);
        
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
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId, bool isGranted);
        
        /// <summary>
        /// Removes a permission from a role
        /// </summary>
        /// <param name="roleId">The role ID</param>
        /// <param name="permissionId">The permission ID</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId);
        
        #endregion
    }
}

