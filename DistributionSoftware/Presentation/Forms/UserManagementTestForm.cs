using System;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class UserManagementTestForm : Form
    {
        private IUserService userService;

        public UserManagementTestForm()
        {
            InitializeComponent();
            InitializeServices();
            TestUserManagement();
        }

        private void InitializeServices()
        {
            try
            {
                string connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
                var userRepository = new UserRepository(connectionString);
                userService = new UserService(userRepository);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing services: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void TestUserManagement()
        {
            try
            {
                // Test getting all users
                var users = await userService.GetAllUsersAsync();
                MessageBox.Show($"Found {users.Count} users in the system.", "Test Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Test getting all roles
                var roles = await userService.GetAllRolesAsync();
                MessageBox.Show($"Found {roles.Count} roles in the system.", "Test Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Test failed: {ex.Message}", "Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
