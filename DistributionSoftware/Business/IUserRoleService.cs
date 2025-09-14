using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IUserRoleService
    {
        // User-Role Assignment
        bool AssignRoleToUser(int userId, int roleId);
        bool RevokeRoleFromUser(int userId, int roleId);
        bool AssignRoleToUser(int userId, int roleId, DateTime? expiryDate);
        
        // Role Management
        List<Role> GetUserRoles(int userId);
        List<User> GetUsersByRole(int roleId);
        List<User> GetUsersByRole(string roleName);
        
        // Permission Management
        List<Permission> GetUserPermissions(int userId);
        List<Permission> GetRolePermissions(int roleId);
        bool HasPermission(int userId, string permissionName);
        bool HasPermission(int userId, string permissionName, string module);
        
        // Validation
        bool ValidateUserRoleAssignment(int userId, int roleId);
        bool ValidatePermissionAssignment(int roleId, int permissionId);
        
        // Reports
        List<UserRole> GetUserRoleReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        List<UserRole> GetAllUserRoles();
        int GetUserRoleCount(bool? isActive);
    }
}
