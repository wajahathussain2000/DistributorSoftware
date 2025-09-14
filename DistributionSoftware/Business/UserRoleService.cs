using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserRepository _userRepository;

        public UserRoleService(
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            IUserRepository userRepository)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
        }

        public bool AssignRoleToUser(int userId, int roleId)
        {
            try
            {
                if (!ValidateUserRoleAssignment(userId, roleId))
                    return false;

                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId,
                    AssignedDate = DateTime.Now,
                    IsActive = true
                };

                return _userRoleRepository.Create(userRole) > 0;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return false;
            }
        }

        public bool RevokeRoleFromUser(int userId, int roleId)
        {
            try
            {
                var userRole = _userRoleRepository.GetByUserAndRole(userId, roleId);
                if (userRole == null) return false;

                userRole.RevokedDate = DateTime.Now;
                userRole.IsActive = false;

                return _userRoleRepository.Update(userRole);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return false;
            }
        }

        public bool AssignRoleToUser(int userId, int roleId, DateTime? expiryDate)
        {
            try
            {
                if (!ValidateUserRoleAssignment(userId, roleId))
                    return false;

                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId,
                    AssignedDate = DateTime.Now,
                    RevokedDate = expiryDate,
                    IsActive = true
                };

                return _userRoleRepository.Create(userRole) > 0;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return false;
            }
        }

        public List<Role> GetUserRoles(int userId)
        {
            try
            {
                var userRoles = _userRoleRepository.GetByUserId(userId);
                var roleIds = userRoles.Where(ur => ur.IsActive).Select(ur => ur.RoleId).ToList();
                
                var roles = new List<Role>();
                foreach (var roleId in roleIds)
                {
                    var role = _roleRepository.GetById(roleId);
                    if (role != null && role.IsActive)
                        roles.Add(role);
                }

                return roles;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return new List<Role>();
            }
        }

        public List<User> GetUsersByRole(int roleId)
        {
            try
            {
                var userRoles = _userRoleRepository.GetByRoleId(roleId);
                var userIds = userRoles.Where(ur => ur.IsActive).Select(ur => ur.UserId).ToList();
                
                var users = new List<User>();
                foreach (var userId in userIds)
                {
                    var user = _userRepository.GetById(userId);
                    if (user != null && user.IsActive)
                        users.Add(user);
                }

                return users;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return new List<User>();
            }
        }

        public List<User> GetUsersByRole(string roleName)
        {
            try
            {
                var role = _roleRepository.GetByName(roleName);
                if (role == null) return new List<User>();

                return GetUsersByRole(role.RoleId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return new List<User>();
            }
        }

        public List<Permission> GetUserPermissions(int userId)
        {
            try
            {
                var userRoles = GetUserRoles(userId);
                var permissions = new List<Permission>();

                foreach (var role in userRoles)
                {
                    var rolePermissions = GetRolePermissions(role.RoleId);
                    permissions.AddRange(rolePermissions);
                }

                // Remove duplicates
                return permissions.GroupBy(p => p.PermissionId).Select(g => g.First()).ToList();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return new List<Permission>();
            }
        }

        public List<Permission> GetRolePermissions(int roleId)
        {
            try
            {
                return _permissionRepository.GetByRoleId(roleId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return new List<Permission>();
            }
        }

        public bool HasPermission(int userId, string permissionName)
        {
            try
            {
                var userPermissions = GetUserPermissions(userId);
                return userPermissions.Any(p => p.PermissionName.Equals(permissionName, StringComparison.OrdinalIgnoreCase) && p.IsActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return false;
            }
        }

        public bool HasPermission(int userId, string permissionName, string module)
        {
            try
            {
                var userPermissions = GetUserPermissions(userId);
                return userPermissions.Any(p => 
                    p.PermissionName.Equals(permissionName, StringComparison.OrdinalIgnoreCase) && 
                    p.Module.Equals(module, StringComparison.OrdinalIgnoreCase) && 
                    p.IsActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return false;
            }
        }

        public bool ValidateUserRoleAssignment(int userId, int roleId)
        {
            try
            {
                var user = _userRepository.GetById(userId);
                var role = _roleRepository.GetById(roleId);

                return user != null && user.IsActive && role != null && role.IsActive;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return false;
            }
        }

        public bool ValidatePermissionAssignment(int roleId, int permissionId)
        {
            try
            {
                var role = _roleRepository.GetById(roleId);
                var permission = _permissionRepository.GetById(permissionId);

                return role != null && role.IsActive && permission != null && permission.IsActive;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return false;
            }
        }

        public List<UserRole> GetUserRoleReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            try
            {
                return _userRoleRepository.GetReport(startDate, endDate, isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return new List<UserRole>();
            }
        }

        public List<UserRole> GetAllUserRoles()
        {
            try
            {
                return _userRoleRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return new List<UserRole>();
            }
        }

        public int GetUserRoleCount(bool? isActive)
        {
            try
            {
                return _userRoleRepository.GetCount(isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in UserRoleService", ex);
                return 0;
            }
        }
    }
}
