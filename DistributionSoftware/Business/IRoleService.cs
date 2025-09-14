using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IRoleService
    {
        // CRUD Operations
        int CreateRole(Role role);
        bool UpdateRole(Role role);
        bool DeleteRole(int roleId);
        Role GetRoleById(int roleId);
        Role GetRoleByName(string roleName);
        List<Role> GetAllRoles();
        List<Role> GetActiveRoles();
        
        // Business Logic
        bool ValidateRole(Role role);
        string[] GetValidationErrors(Role role);
        bool IsRoleNameExists(string roleName, int? excludeId = null);
        
        // Reports
        List<Role> GetRoleReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetRoleCount(bool? isActive);
    }
}
