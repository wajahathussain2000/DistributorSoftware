using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DistributionSoftware.Common
{
    /// <summary>
    /// Manages the current user session and provides access to user information and permissions
    /// </summary>
    public static class UserSession
    {
        #region Private Fields
        
        private static User _currentUser;
        private static List<Permission> _userPermissions;
        private static DateTime _sessionStartTime;
        
        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets the currently logged-in user
        /// </summary>
        public static User CurrentUser => _currentUser;
        
        /// <summary>
        /// Gets the current user's role name
        /// </summary>
        public static string CurrentUserRole => _currentUser?.RoleName ?? "Guest";
        
        /// <summary>
        /// Gets the current user's ID
        /// </summary>
        public static int CurrentUserId => _currentUser?.UserId ?? 0;
        
        /// <summary>
        /// Gets the current user's permissions
        /// </summary>
        public static List<Permission> UserPermissions => _userPermissions ?? new List<Permission>();
        
        /// <summary>
        /// Gets the session start time
        /// </summary>
        public static DateTime SessionStartTime => _sessionStartTime;
        
        /// <summary>
        /// Gets whether a user is currently logged in
        /// </summary>
        public static bool IsLoggedIn => _currentUser != null;
        
        #endregion
        
        #region Session Management
        
        /// <summary>
        /// Initializes a new user session
        /// </summary>
        /// <param name="user">The authenticated user</param>
        /// <param name="permissions">The user's permissions</param>
        public static void InitializeSession(User user, List<Permission> permissions)
        {
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            _userPermissions = permissions ?? new List<Permission>();
            _sessionStartTime = DateTime.Now;
        }
        
        /// <summary>
        /// Clears the current user session
        /// </summary>
        public static void ClearSession()
        {
            _currentUser = null;
            _userPermissions = null;
            _sessionStartTime = DateTime.MinValue;
        }
        
        #endregion
        
        #region Permission Checking
        
        /// <summary>
        /// Checks if the current user has a specific permission
        /// </summary>
        /// <param name="permissionCode">The permission code to check</param>
        /// <returns>True if the user has the permission, false otherwise</returns>
        public static bool HasPermission(string permissionCode)
        {
            if (!IsLoggedIn || _userPermissions == null)
                return false;
                
            return _userPermissions.Any(p => 
                p.PermissionCode == permissionCode && 
                p.IsActive);
        }
        
        /// <summary>
        /// Checks if the current user has any permission from a list
        /// </summary>
        /// <param name="permissionCodes">List of permission codes to check</param>
        /// <returns>True if the user has at least one permission, false otherwise</returns>
        public static bool HasAnyPermission(params string[] permissionCodes)
        {
            if (!IsLoggedIn || _userPermissions == null)
                return false;
                
            return _userPermissions.Any(p => 
                permissionCodes.Contains(p.PermissionCode) && 
                p.IsActive);
        }
        
        /// <summary>
        /// Checks if the current user has all permissions from a list
        /// </summary>
        /// <param name="permissionCodes">List of permission codes to check</param>
        /// <returns>True if the user has all permissions, false otherwise</returns>
        public static bool HasAllPermissions(params string[] permissionCodes)
        {
            if (!IsLoggedIn || _userPermissions == null)
                return false;
                
            return permissionCodes.All(code => 
                _userPermissions.Any(p => 
                    p.PermissionCode == code && 
                    p.IsActive));
        }
        
        /// <summary>
        /// Checks if the current user has access to a specific module
        /// </summary>
        /// <param name="moduleName">The module name to check</param>
        /// <returns>True if the user has access to the module, false otherwise</returns>
        public static bool HasModuleAccess(string moduleName)
        {
            if (!IsLoggedIn || _userPermissions == null)
                return false;
                
            return _userPermissions.Any(p => 
                p.Module.Equals(moduleName, StringComparison.OrdinalIgnoreCase) && 
                p.IsActive);
        }
        
        #endregion
        
        #region Role-Based Access Control
        
        /// <summary>
        /// Checks if the current user is an administrator
        /// </summary>
        /// <returns>True if the user is an admin, false otherwise</returns>
        public static bool IsAdmin => CurrentUserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        
        /// <summary>
        /// Checks if the current user is a manager or higher
        /// </summary>
        /// <returns>True if the user is a manager or admin, false otherwise</returns>
        public static bool IsManagerOrHigher => IsAdmin || CurrentUserRole.Equals("Manager", StringComparison.OrdinalIgnoreCase);
        
        /// <summary>
        /// Checks if the current user can access user management
        /// </summary>
        /// <returns>True if the user can manage users, false otherwise</returns>
        public static bool CanManageUsers => HasPermission("USER_MANAGE") || IsAdmin;
        
        /// <summary>
        /// Checks if the current user can access sales module
        /// </summary>
        /// <returns>True if the user can access sales, false otherwise</returns>
        public static bool CanAccessSales => HasModuleAccess("Sales") || IsAdmin;
        
        /// <summary>
        /// Checks if the current user can access inventory module
        /// </summary>
        /// <returns>True if the user can access inventory, false otherwise</returns>
        public static bool CanAccessInventory => HasModuleAccess("Inventory") || IsAdmin;
        
        /// <summary>
        /// Checks if the current user can access financial module
        /// </summary>
        /// <returns>True if the user can access financial, false otherwise</returns>
        public static bool CanAccessFinancial => HasModuleAccess("Financial") || IsAdmin;
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Gets the user's display name
        /// </summary>
        /// <returns>The user's full name or username if full name is not available</returns>
        public static string GetDisplayName()
        {
            if (!IsLoggedIn)
                return "Guest";
                
            if (!string.IsNullOrWhiteSpace(_currentUser.FirstName) && !string.IsNullOrWhiteSpace(_currentUser.LastName))
                return $"{_currentUser.FirstName} {_currentUser.LastName}";
                
            return _currentUser.Username ?? "Unknown User";
        }
        
        /// <summary>
        /// Gets the session duration in minutes
        /// </summary>
        /// <returns>Session duration in minutes</returns>
        public static int GetSessionDurationMinutes()
        {
            if (!IsLoggedIn)
                return 0;
                
            return (int)(DateTime.Now - _sessionStartTime).TotalMinutes;
        }
        
        #endregion
    }
}









