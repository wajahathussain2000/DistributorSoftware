using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class UserMasterForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private int currentUserId = 0;
        private Button btnSave;
        private DataGridView dgvUsers;
        private bool isEditMode = false;
        private IUserService userService;

        public UserMasterForm()
        {
            InitializeComponent();
            InitializeForm();
            InitializeConnection();
            InitializeServices();
            LoadRoles();
            LoadUsersGrid();
            
            // Subscribe to form load event to clear form after all controls are initialized
            this.Load += UserMasterForm_Load;
        }

        private void UserMasterForm_Load(object sender, EventArgs e)
        {
            // Clear form after all controls are fully initialized
            System.Diagnostics.Debug.WriteLine("Form Load event fired - clearing form now");
            ClearForm();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        private void InitializeServices()
        {
            var userRepository = new UserRepository(connectionString);
            userService = new UserService(userRepository);
        }

        private void InitializeForm()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "User Master - Distribution Software";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Size = new Size(1500, 800);
            this.MinimumSize = new Size(1500, 800);

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label headerLabel = new Label
            {
                Text = "👤 User Master",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button closeBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.Width - 80, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            closeBtn.Click += (s, e) => this.Close();

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(closeBtn);

            // Main Content Panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };

            // Left Panel - User Information
            Panel leftPanel = new Panel
            {
                Size = new Size(600, 600),
                Location = new Point(20, 20),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // User Information GroupBox
            GroupBox userInfoGroup = new GroupBox
            {
                Text = "User Information",
                Size = new Size(560, 400),
                Location = new Point(20, 20),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // Username
            Label lblUsername = new Label
            {
                Text = "Username:",
                Location = new Point(20, 40),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            TextBox txtUsername = new TextBox
            {
                Name = "txtUsername",
                Location = new Point(130, 40),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Email
            Label lblEmail = new Label
            {
                Text = "Email:",
                Location = new Point(20, 80),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            TextBox txtEmail = new TextBox
            {
                Name = "txtEmail",
                Location = new Point(130, 80),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // First Name
            Label lblFirstName = new Label
            {
                Text = "First Name:",
                Location = new Point(20, 120),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            TextBox txtFirstName = new TextBox
            {
                Name = "txtFirstName",
                Location = new Point(130, 120),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Last Name
            Label lblLastName = new Label
            {
                Text = "Last Name:",
                Location = new Point(20, 160),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            TextBox txtLastName = new TextBox
            {
                Name = "txtLastName",
                Location = new Point(130, 160),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Password
            Label lblPassword = new Label
            {
                Text = "Password:",
                Location = new Point(20, 200),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            TextBox txtPassword = new TextBox
            {
                Name = "txtPassword",
                Location = new Point(130, 200),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                UseSystemPasswordChar = true
            };

            // Role
            Label lblRole = new Label
            {
                Text = "Role:",
                Location = new Point(20, 240),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            ComboBox cmbRole = new ComboBox
            {
                Name = "cmbRole",
                Location = new Point(130, 240),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Is Active CheckBox
            CheckBox chkIsActive = new CheckBox
            {
                Name = "chkIsActive",
                Text = "Active User",
                Location = new Point(20, 280),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Checked = true
            };

            // Is Admin CheckBox
            CheckBox chkIsAdmin = new CheckBox
            {
                Name = "chkIsAdmin",
                Text = "Administrator",
                Location = new Point(150, 280),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            // Add controls to User Information GroupBox
            userInfoGroup.Controls.AddRange(new Control[] {
                lblUsername, txtUsername, lblEmail, txtEmail,
                lblFirstName, txtFirstName, lblLastName, txtLastName,
                lblPassword, txtPassword, lblRole, cmbRole,
                chkIsActive, chkIsAdmin
            });

            // Actions GroupBox
            GroupBox actionsGroup = new GroupBox
            {
                Text = "Actions",
                Size = new Size(560, 120),
                Location = new Point(20, 440),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            btnSave = new Button
            {
                Text = "Save",
                Size = new Size(100, 35),
                Location = new Point(20, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.Click += BtnSave_Click;

            Button btnClear = new Button
            {
                Text = "Clear",
                Size = new Size(100, 35),
                Location = new Point(140, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClear.Click += BtnClear_Click;

            Button btnDelete = new Button
            {
                Text = "Delete",
                Size = new Size(100, 35),
                Location = new Point(260, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDelete.Click += BtnDelete_Click;

            actionsGroup.Controls.AddRange(new Control[] { btnSave, btnClear, btnDelete });

            leftPanel.Controls.AddRange(new Control[] { userInfoGroup, actionsGroup });

            // Right Panel - Users List
            Panel rightPanel = new Panel
            {
                Size = new Size(800, 600),
                Location = new Point(640, 20),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblUsersList = new Label
            {
                Text = "Users List",
                Location = new Point(20, 20),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            dgvUsers = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(760, 520),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Segoe UI", 9)
            };
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;

            rightPanel.Controls.AddRange(new Control[] { lblUsersList, dgvUsers });

            contentPanel.Controls.AddRange(new Control[] { leftPanel, rightPanel });

            this.Controls.AddRange(new Control[] { headerPanel, contentPanel });

            this.ResumeLayout();
        }

        private async void LoadRoles()
        {
            try
            {
                var roles = await userService.GetAllRolesAsync();
                var cmbRole = this.FindControlRecursive<ComboBox>("cmbRole");
                
                if (cmbRole != null)
                {
                    cmbRole.DataSource = roles;
                    cmbRole.DisplayMember = "RoleName";
                    cmbRole.ValueMember = "RoleId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadUsersGrid()
        {
            try
            {
                var users = await userService.GetAllUsersAsync();
                
                dgvUsers.DataSource = users;
                dgvUsers.Columns["UserId"].Visible = false;
                dgvUsers.Columns["PasswordHash"].Visible = false;
                dgvUsers.Columns["RoleId"].Visible = false;
                
                // Set column headers
                if (dgvUsers.Columns["Username"] != null) dgvUsers.Columns["Username"].HeaderText = "Username";
                if (dgvUsers.Columns["Email"] != null) dgvUsers.Columns["Email"].HeaderText = "Email";
                if (dgvUsers.Columns["FirstName"] != null) dgvUsers.Columns["FirstName"].HeaderText = "First Name";
                if (dgvUsers.Columns["LastName"] != null) dgvUsers.Columns["LastName"].HeaderText = "Last Name";
                if (dgvUsers.Columns["RoleName"] != null) dgvUsers.Columns["RoleName"].HeaderText = "Role";
                if (dgvUsers.Columns["IsActive"] != null) dgvUsers.Columns["IsActive"].HeaderText = "Active";
                if (dgvUsers.Columns["IsAdmin"] != null) dgvUsers.Columns["IsAdmin"].HeaderText = "Admin";
                if (dgvUsers.Columns["LastLoginDate"] != null) dgvUsers.Columns["LastLoginDate"].HeaderText = "Last Login";
                if (dgvUsers.Columns["CreatedDate"] != null) dgvUsers.Columns["CreatedDate"].HeaderText = "Created Date";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                var selectedUser = dgvUsers.SelectedRows[0].DataBoundItem as User;
                if (selectedUser != null)
                {
                    LoadUserToForm(selectedUser);
                }
            }
        }

        private void LoadUserToForm(User user)
        {
            currentUserId = user.UserId;
            isEditMode = true;
            btnSave.Text = "Update";

            var txtUsername = this.FindControlRecursive<TextBox>("txtUsername");
            var txtEmail = this.FindControlRecursive<TextBox>("txtEmail");
            var txtFirstName = this.FindControlRecursive<TextBox>("txtFirstName");
            var txtLastName = this.FindControlRecursive<TextBox>("txtLastName");
            var txtPassword = this.FindControlRecursive<TextBox>("txtPassword");
            var cmbRole = this.FindControlRecursive<ComboBox>("cmbRole");
            var chkIsActive = this.FindControlRecursive<CheckBox>("chkIsActive");
            var chkIsAdmin = this.FindControlRecursive<CheckBox>("chkIsAdmin");

            if (txtUsername != null) txtUsername.Text = user.Username;
            if (txtEmail != null) txtEmail.Text = user.Email;
            if (txtFirstName != null) txtFirstName.Text = user.FirstName ?? "";
            if (txtLastName != null) txtLastName.Text = user.LastName ?? "";
            if (txtPassword != null) txtPassword.Text = ""; // Don't show password
            if (cmbRole != null) cmbRole.SelectedValue = user.RoleId;
            if (chkIsActive != null) chkIsActive.Checked = user.IsActive;
            if (chkIsAdmin != null) chkIsAdmin.Checked = user.IsAdmin;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var txtUsername = this.FindControlRecursive<TextBox>("txtUsername");
                var txtEmail = this.FindControlRecursive<TextBox>("txtEmail");
                var txtFirstName = this.FindControlRecursive<TextBox>("txtFirstName");
                var txtLastName = this.FindControlRecursive<TextBox>("txtLastName");
                var txtPassword = this.FindControlRecursive<TextBox>("txtPassword");
                var cmbRole = this.FindControlRecursive<ComboBox>("cmbRole");
                var chkIsActive = this.FindControlRecursive<CheckBox>("chkIsActive");
                var chkIsAdmin = this.FindControlRecursive<CheckBox>("chkIsAdmin");

                // Validation
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Username and Email are required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!isEditMode && string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password is required for new users!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var user = new User
                {
                    UserId = currentUserId,
                    Username = txtUsername.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    PasswordHash = txtPassword.Text.Trim(),
                    IsActive = chkIsActive.Checked,
                    IsAdmin = chkIsAdmin.Checked,
                    RoleId = cmbRole.SelectedValue != null ? Convert.ToInt32(cmbRole.SelectedValue) : 0
                };

                bool success;
                if (isEditMode)
                {
                    success = await userService.UpdateUserAsync(user);
                    if (success && !string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        await userService.ChangeUserPasswordAsync(user.UserId, txtPassword.Text);
                    }
                }
                else
                {
                    success = await userService.CreateUserAsync(user);
                }

                if (success)
                {
                    MessageBox.Show($"User {(isEditMode ? "updated" : "created")} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadUsersGrid();
                }
                else
                {
                    MessageBox.Show($"Failed to {(isEditMode ? "update" : "create")} user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentUserId == 0)
            {
                MessageBox.Show("Please select a user to delete!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = await userService.DeleteUserAsync(currentUserId);
                    if (success)
                    {
                        MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadUsersGrid();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            currentUserId = 0;
            isEditMode = false;
            btnSave.Text = "Save";

            var txtUsername = this.FindControlRecursive<TextBox>("txtUsername");
            var txtEmail = this.FindControlRecursive<TextBox>("txtEmail");
            var txtFirstName = this.FindControlRecursive<TextBox>("txtFirstName");
            var txtLastName = this.FindControlRecursive<TextBox>("txtLastName");
            var txtPassword = this.FindControlRecursive<TextBox>("txtPassword");
            var cmbRole = this.FindControlRecursive<ComboBox>("cmbRole");
            var chkIsActive = this.FindControlRecursive<CheckBox>("chkIsActive");
            var chkIsAdmin = this.FindControlRecursive<CheckBox>("chkIsAdmin");

            if (txtUsername != null) txtUsername.Text = "";
            if (txtEmail != null) txtEmail.Text = "";
            if (txtFirstName != null) txtFirstName.Text = "";
            if (txtLastName != null) txtLastName.Text = "";
            if (txtPassword != null) txtPassword.Text = "";
            if (cmbRole != null) cmbRole.SelectedIndex = -1;
            if (chkIsActive != null) chkIsActive.Checked = true;
            if (chkIsAdmin != null) chkIsAdmin.Checked = false;

            dgvUsers.ClearSelection();
        }
    }

    // Extension method to find controls recursively
    public static class ControlExtensions
    {
        public static T FindControlRecursive<T>(this Control parent, string name) where T : Control
        {
            if (parent.Name == name && parent is T)
                return parent as T;

            foreach (Control control in parent.Controls)
            {
                var found = control.FindControlRecursive<T>(name);
                if (found != null)
                    return found;
            }

            return null;
        }
    }
}
