using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using DistributionSoftware.Resources.Icons;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;
using DistributionSoftware.Models;
using System.Net;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class LoginForm : Form
    {
        #region Private Fields
        
        private readonly IUserService _userService;
        private readonly IActivityLogRepository _activityLogRepository;
        private long _currentLoginId;
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the LoginForm class
        /// </summary>
        public LoginForm()
        {
            try
            {
                InitializeComponent();
                
                // Initialize 3-tier architecture
                var connectionString = ConfigurationManager.DistributionConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                }
                
                var userRepository = new UserRepository(connectionString);
                var activityLogRepository = new ActivityLogRepository(connectionString);
                
                _userService = new UserService(userRepository);
                _activityLogRepository = activityLogRepository;
                
                
                SetupForm();
                SetupIcons();
                SetupPlaceholders();
                
            }
            catch (Exception ex)
            {
                
                // Show error and create a minimal form
                MessageBox.Show($"Error initializing application: {ex.Message}\n\nPlease contact administrator.", 
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Create minimal form as fallback
                this.Text = "Login - Error Recovery";
                this.Size = new Size(400, 300);
                this.StartPosition = FormStartPosition.CenterScreen;
                
                var label = new Label
                {
                    Text = "ERROR RECOVERY MODE\n\nApplication failed to initialize properly.\n\nPlease contact your system administrator.",
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                
                this.Controls.Add(label);
            }
        }

        private void SetupForm()
        {
            // Set form properties
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Login - Distribution Software";
            
            // Apply modern styling
            this.BackColor = Color.FromArgb(240, 248, 255);
        }

        private void SetupIcons()
        {
            // Set email icon
            if (picEmailIcon != null)
            {
                picEmailIcon.Image = IconHelper.CreateEmailIcon();
                picEmailIcon.SizeMode = PictureBoxSizeMode.Zoom;
            }

            // Set password icon
            if (picPasswordIcon != null)
            {
                picPasswordIcon.Image = IconHelper.CreatePasswordIcon();
                picPasswordIcon.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }



        private void SetupPlaceholders()
        {
            // Hardcode test credentials for easy testing
            txtEmail.Text = "admin@distributionsoftware.com";
            txtEmail.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            txtPassword.Text = "Admin@123";
            txtPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtPassword.UseSystemPasswordChar = true;
            
            // Add a label to show available test users
            var lblTestUsers = new Label
            {
                Text = "Test Credentials (Pre-filled):\n" +
                       "• admin@distributionsoftware.com / Admin@123 ✓\n" +
                       "• manager@company.com / Manager@456\n" +
                       "• user1@company.com / User@789\n" +
                       "• sales@company.com / Sales@2024",
                Location = new Point(10, 450),
                Size = new Size(400, 80),
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = Color.Gray,
                BackColor = Color.Transparent
            };
            
            this.Controls.Add(lblTestUsers);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            btnLogin.Text = "Signing In...";
            
            try
            {
                // Authenticate user - this will handle session initialization and dashboard routing
                bool success = await AuthenticateUserAsync();
                if (!success)
                {
                    // If authentication failed, don't proceed
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "SIGN IN";
            }
        }

        private async Task<bool> AuthenticateUserAsync()
        {
            // Simple capture of textbox values
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // Basic validation
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter your email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            try
            {
                // Test database connection first
                var userRepository = new UserRepository(ConfigurationManager.DistributionConnectionString);
                var connectionTest = await userRepository.TestDatabaseConnectionAsync();
                
                // Authenticate user through 3-tier architecture
                var user = await _userService.AuthenticateUserAsync(email.Trim(), password);
                
                if (user != null)
                {
                    // Log successful login
                    await LogSuccessfulLoginAsync(user);
                    
                    // Get user permissions and initialize session
                    await InitializeUserSessionAsync(user);
                    
                    return true;
                }
                else
                {
                    // Log failed login attempt
                    await LogFailedLoginAsync(email, "Invalid credentials");
                    MessageBox.Show("Invalid email or password. Please check your credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log failed login attempt due to system error
                await LogFailedLoginAsync(email, $"System error: {ex.Message}");
                MessageBox.Show(string.Format("Database connection error: {0}\n\nPlease check your SQL Server connection.", ex.Message), "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        #endregion
        
        #region Activity Logging Methods
        
        /// <summary>
        /// Logs a successful login attempt
        /// </summary>
        /// <param name="user">The authenticated user</param>
        private async Task LogSuccessfulLoginAsync(Models.User user)
        {
            try
            {
                var ipAddress = GetClientIPAddress();
                var userAgent = GetUserAgent();
                
                // Log login attempt
                _currentLoginId = await _activityLogRepository.LogLoginAttemptAsync(
                    user.UserId, 
                    "Success", 
                    ipAddress, 
                    userAgent);
                
                // Log user activity
                await _activityLogRepository.LogUserActivityAsync(
                    user.UserId,
                    "Login",
                    $"User {user.Username} logged in successfully",
                    "Authentication",
                    ipAddress,
                    userAgent);
            }
            catch (Exception ex)
            {
                // Don't throw - logging failure shouldn't prevent login
            }
        }
        
        /// <summary>
        /// Logs a failed login attempt
        /// </summary>
        /// <param name="email">The email address used in the attempt</param>
        /// <param name="failureReason">The reason for the failure</param>
        private async Task LogFailedLoginAsync(string email, string failureReason)
        {
            try
            {
                var ipAddress = GetClientIPAddress();
                var userAgent = GetUserAgent();
                
                // For failed logins, we don't have a user ID, so we'll use a placeholder
                // In a real system, you might want to create a temporary user record or use a different approach
                await _activityLogRepository.LogUserActivityAsync(
                    0, // Placeholder user ID for failed attempts
                    "Login Failed",
                    $"Failed login attempt for email: {email}",
                    "Authentication",
                    ipAddress,
                    userAgent,
                    $"FailureReason: {failureReason}");
            }
            catch (Exception ex)
            {
                // Don't throw - logging failure shouldn't prevent proper error handling
            }
        }
        
        /// <summary>
        /// Gets the client IP address
        /// </summary>
        /// <returns>Client IP address or "Unknown" if not available</returns>
        private string GetClientIPAddress()
        {
            try
            {
                // In a Windows Forms application, this will typically return localhost
                // In a web application, you would get the actual client IP
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "127.0.0.1"; // Localhost fallback
            }
            catch
            {
                return "Unknown";
            }
        }
        
        /// <summary>
        /// Gets the user agent string
        /// </summary>
        /// <returns>User agent string or "Windows Forms Application" if not available</returns>
        private string GetUserAgent()
        {
            try
            {
                // In a Windows Forms application, we'll return a descriptive string
                return $"Windows Forms Application - {Environment.OSVersion}";
            }
            catch
            {
                return "Windows Forms Application";
            }
        }
        
        #endregion
        
        #region Session Management
        
        /// <summary>
        /// Initializes the user session with permissions and routes to appropriate dashboard
        /// </summary>
        /// <param name="user">The authenticated user</param>
        private async Task InitializeUserSessionAsync(Models.User user)
        {
            try
            {
                
                // Get user permissions from the permission service
                var permissionService = new PermissionService(
                    new PermissionRepository(ConfigurationManager.GetConnectionString("DistributionConnection")),
                    _activityLogRepository);
                
                
                var userPermissions = await permissionService.GetUserPermissionsAsync(user.UserId);
                
                
                // Initialize the user session
                UserSession.InitializeSession(user, userPermissions.ToList());
                
                
                // Double-check session is still valid after a short delay
                await Task.Delay(100); // Small delay to simulate async operations
                
                // Try to log the session initialization, but don't fail if it doesn't work
                try
                {
                    if (_activityLogRepository != null)
                    {
                        await _activityLogRepository.LogUserActivityAsync(
                            user.UserId,
                            "Session Started",
                            $"User {user.Username} logged in successfully",
                            "Authentication",
                            GetClientIPAddress(),
                            GetUserAgent());
                        
                    }
                }
                catch (Exception logEx)
                {
                    // Continue with login even if logging fails
                }
                
                
                // Route to appropriate dashboard on UI thread
                this.BeginInvoke(new Action(() => {
                    try
                    {
                        // Check session again in UI thread
                        
                        // Show success message
                        MessageBox.Show($"Welcome, {UserSession.GetDisplayName()}!\nRole: {user.RoleName}", 
                            "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Route to appropriate dashboard
                        DashboardRouter.RouteToDashboard(this);
                    }
                    catch (Exception routingEx)
                    {
                        MessageBox.Show($"Error routing to dashboard: {routingEx.Message}", "Routing Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
            }
            catch (Exception ex)
            {
                
                // Fallback: initialize session without permissions
                UserSession.InitializeSession(user, new List<Permission>());
                
                
                // Route to dashboard on UI thread
                this.BeginInvoke(new Action(() => {
                    try
                    {
                        // Show warning but still allow login
                        MessageBox.Show($"Login successful, but there was an issue loading your permissions.\n" +
                                      $"Role: {user.RoleName}\n\nPlease contact administrator if this persists.", 
                            "Login Successful with Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                        // Route to dashboard anyway
                        DashboardRouter.RouteToDashboard(this);
                    }
                    catch (Exception fallbackEx)
                    {
                        MessageBox.Show($"Error routing to dashboard: {fallbackEx.Message}", "Routing Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
            }
        }
        
        #endregion
        
        #region Event Handlers
        
        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact your system administrator to reset your password.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lnkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact your system administrator to create a new account.", "Sign Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        #endregion
    }
}
