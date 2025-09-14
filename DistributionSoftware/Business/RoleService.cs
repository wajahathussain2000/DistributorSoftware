using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public int CreateRole(Role role)
        {
            try
            {
                if (!ValidateRole(role))
                    throw new InvalidOperationException("Role validation failed");

                return _roleRepository.Create(role);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                throw;
            }
        }

        public bool UpdateRole(Role role)
        {
            try
            {
                if (!ValidateRole(role))
                    throw new InvalidOperationException("Role validation failed");

                return _roleRepository.Update(role);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                throw;
            }
        }

        public bool DeleteRole(int roleId)
        {
            try
            {
                return _roleRepository.Delete(roleId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                throw;
            }
        }

        public Role GetRoleById(int roleId)
        {
            try
            {
                return _roleRepository.GetById(roleId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                throw;
            }
        }

        public Role GetRoleByName(string roleName)
        {
            try
            {
                return _roleRepository.GetByName(roleName);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                throw;
            }
        }

        public List<Role> GetAllRoles()
        {
            try
            {
                return _roleRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                throw;
            }
        }

        public List<Role> GetActiveRoles()
        {
            try
            {
                return _roleRepository.GetActive();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                throw;
            }
        }

        public bool ValidateRole(Role role)
        {
            return GetValidationErrors(role).Length == 0;
        }

        public string[] GetValidationErrors(Role role)
        {
            var errors = new List<string>();

            if (role == null)
            {
                errors.Add("Role cannot be null");
                return errors.ToArray();
            }

            if (string.IsNullOrWhiteSpace(role.RoleName))
                errors.Add("Role name is required");

            if (role.RoleName != null && role.RoleName.Length > 100)
                errors.Add("Role name cannot exceed 100 characters");

            if (role.Description != null && role.Description.Length > 500)
                errors.Add("Description cannot exceed 500 characters");

            return errors.ToArray();
        }

        public bool IsRoleNameExists(string roleName, int? excludeId = null)
        {
            try
            {
                var existingRole = GetRoleByName(roleName);
                return existingRole != null && existingRole.RoleId != excludeId;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                return false;
            }
        }

        public List<Role> GetRoleReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            try
            {
                return _roleRepository.GetReport(startDate, endDate, isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                throw;
            }
        }

        public int GetRoleCount(bool? isActive)
        {
            try
            {
                return _roleRepository.GetCount(isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in RoleService", ex);
                throw;
            }
        }
    }
}
