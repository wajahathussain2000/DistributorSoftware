using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IUserRoleRepository
    {
        // CRUD Operations
        int Create(UserRole userRole);
        bool Update(UserRole userRole);
        bool Delete(int userRoleId);
        UserRole GetById(int userRoleId);
        UserRole GetByUserAndRole(int userId, int roleId);
        List<UserRole> GetAll();
        List<UserRole> GetActive();
        
        // Query Methods
        List<UserRole> GetByUserId(int userId);
        List<UserRole> GetByRoleId(int roleId);
        
        // Reports
        List<UserRole> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetCount(bool? isActive);
    }
}
