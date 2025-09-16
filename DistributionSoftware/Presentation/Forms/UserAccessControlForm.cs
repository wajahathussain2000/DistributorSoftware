using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class UserAccessControlForm : Form
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IPermissionService _permissionService;
        private IUserRoleService _userRoleService;
        private List<User> _users;
        private List<Role> _roles;
        private List<Permission> _permissions;
        private List<UserRole> _userRoles;

        public UserAccessControlForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
        }

        private void InitializeServices()
        {
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var userRepository = new UserRepository(connectionString);
            var roleRepository = new RoleRepository(connectionString);
            var permissionRepository = new PermissionRepository(connectionString);
            var activityLogRepository = new ActivityLogRepository(connectionString);
            var userRoleRepository = new UserRoleRepository(connectionString);
            
            _userService = new UserService(userRepository);
            _roleService = new RoleService(roleRepository);
            _permissionService = new PermissionService(permissionRepository, activityLogRepository);
            _userRoleService = new UserRoleService(userRoleRepository, roleRepository, permissionRepository, userRepository);
        }

        private void InitializeForm()
        {
            LoadUsers();
            LoadRoles();
            LoadPermissions();
            LoadUserRoles();
        }

        private void LoadUsers()
        {
            try
            {
                _users = _userService.GetAllUsers();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoles()
        {
            try
            {
                _roles = _roleService.GetAllRoles();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPermissions()
        {
            try
            {
                _permissions = _permissionService.GetAllPermissions();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading permissions: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserRoles()
        {
            try
            {
                _userRoles = _userRoleService.GetAllUserRoles();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user roles: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateGridColumns()
        {
            // UI method - will be implemented when designer files are created
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save functionality - will be implemented when designer files are created
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // New functionality - will be implemented when designer files are created
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Edit functionality - will be implemented when designer files are created
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete functionality - will be implemented when designer files are created
        }
    }
}
