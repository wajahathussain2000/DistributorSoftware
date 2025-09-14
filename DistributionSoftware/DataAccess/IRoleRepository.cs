using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IRoleRepository
    {
        // CRUD Operations
        int Create(Role role);
        bool Update(Role role);
        bool Delete(int roleId);
        Role GetById(int roleId);
        Role GetByName(string roleName);
        List<Role> GetAll();
        List<Role> GetActive();
        
        // Reports
        List<Role> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetCount(bool? isActive);
    }
}
