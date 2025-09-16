using DistributionSoftware.Common;
using System;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    /// <summary>
    /// Routes users to appropriate dashboards based on their role and permissions
    /// </summary>
    public static class DashboardRouter
    {
        #region Dashboard Routing
        
        /// <summary>
        /// Routes the current user to the appropriate dashboard based on their role
        /// </summary>
        /// <param name="parentForm">The parent form to close after routing</param>
        public static void RouteToDashboard(Form parentForm)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"RouteToDashboard called. User logged in: {UserSession.IsLoggedIn}");
                System.Diagnostics.Debug.WriteLine($"Current user: {UserSession.GetDisplayName()}, Role: {UserSession.CurrentUserRole}");
                System.Diagnostics.Debug.WriteLine($"User ID: {UserSession.CurrentUserId}, Session start time: {UserSession.SessionStartTime}");
                
                if (!UserSession.IsLoggedIn)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: No user session found in RouteToDashboard!");
                    MessageBox.Show("No user session found. Please log in again.", "Session Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"Current user role: {UserSession.CurrentUserRole}");
                
                Form dashboardForm = null;
                try
                {
                    dashboardForm = GetDashboardForm();
                    System.Diagnostics.Debug.WriteLine($"Dashboard form created: {dashboardForm?.GetType().Name ?? "NULL"}");
                }
                catch (Exception createEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Error creating dashboard form: {createEx.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {createEx.StackTrace}");
                    MessageBox.Show($"Error creating dashboard: {createEx.Message}", "Dashboard Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (dashboardForm != null)
                {
                    System.Diagnostics.Debug.WriteLine("Dashboard form created successfully");
                    
                    // Don't close the parent form immediately - let the dashboard handle it
                    // parentForm?.Close();
                    
                    System.Diagnostics.Debug.WriteLine("Showing dashboard form");
                    
                    try
                    {
                        // Show the appropriate dashboard on UI thread
                        if (dashboardForm.InvokeRequired)
                        {
                            dashboardForm.BeginInvoke(new Action(() => {
                                try
                                {
                                    System.Diagnostics.Debug.WriteLine("Showing dashboard with Show() method");
                                    dashboardForm.Show();
                                    System.Diagnostics.Debug.WriteLine("Dashboard form shown successfully");
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine($"Error showing dashboard: {ex.Message}");
                                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                                    MessageBox.Show($"Error displaying dashboard: {ex.Message}\n\nPlease contact administrator.", 
                                        "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }));
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Showing dashboard with Show() method");
                            dashboardForm.Show();
                            System.Diagnostics.Debug.WriteLine("Dashboard form shown successfully");
                        }
                    }
                    catch (Exception showEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error showing dashboard: {showEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"Stack trace: {showEx.StackTrace}");
                        MessageBox.Show($"Error displaying dashboard: {showEx.Message}\n\nPlease contact administrator.", 
                            "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Failed to create dashboard form");
                    MessageBox.Show("Unable to determine appropriate dashboard for your role. Please contact administrator.", 
                        "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in RouteToDashboard: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // Try to show a basic dashboard as fallback
                try
                {
                    var fallbackForm = new Form
                    {
                        Text = "Dashboard - Error Recovery",
                        Size = new System.Drawing.Size(800, 600),
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    
                    var label = new Label
                    {
                        Text = "ERROR RECOVERY DASHBOARD\n\nThere was an error routing to your dashboard.\n\n" +
                               "Please contact your system administrator.\n\n" +
                               "Error Details:\n" + ex.Message,
                        Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill
                    };
                    
                    fallbackForm.Controls.Add(label);
                    
                    if (parentForm != null)
                        parentForm.Close();
                    
                    fallbackForm.ShowDialog();
                }
                catch (Exception fallbackEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Fallback dashboard also failed: {fallbackEx.Message}");
                    MessageBox.Show($"Critical error: {ex.Message}\n\nApplication will now close.", 
                        "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        /// <summary>
        /// Gets the appropriate dashboard form based on user role and permissions
        /// </summary>
        /// <returns>The dashboard form to display</returns>
        private static Form GetDashboardForm()
        {
            try
            {
                string userRole = UserSession.CurrentUserRole;
                System.Diagnostics.Debug.WriteLine($"Getting dashboard for role: {userRole}");
                
                Form result = null;
                
                switch (userRole.ToLower())
                {
                    case "admin":
                        System.Diagnostics.Debug.WriteLine("Creating Admin Dashboard");
                        result = GetAdminDashboard();
                        break;
                        
                    case "manager":
                        System.Diagnostics.Debug.WriteLine("Creating Manager Dashboard");
                        result = GetManagerDashboard();
                        break;
                        
                    case "salesman":
                    case "sales":
                        System.Diagnostics.Debug.WriteLine("Creating Sales Dashboard");
                        result = GetSalesmanDashboard();
                        break;
                        
                    case "storekeeper":
                        System.Diagnostics.Debug.WriteLine("Creating Inventory Dashboard");
                        result = GetStorekeeperDashboard();
                        break;
                        
                    case "accountant":
                        System.Diagnostics.Debug.WriteLine("Creating Financial Dashboard");
                        result = GetAccountantDashboard();
                        break;
                        
                    default:
                        System.Diagnostics.Debug.WriteLine($"Unknown role '{userRole}', creating Basic Dashboard");
                        // Fallback to basic dashboard
                        result = GetBasicDashboard();
                        break;
                }
                
                System.Diagnostics.Debug.WriteLine($"Dashboard creation result: {result?.GetType().Name ?? "NULL"}");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetDashboardForm: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // Return basic dashboard as fallback
                return GetBasicDashboard();
            }
        }
        
        #endregion
        
        #region Role-Specific Dashboards
        
        /// <summary>
        /// Gets the admin dashboard with full access
        /// </summary>
        /// <returns>Admin dashboard form</returns>
        private static Form GetAdminDashboard()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Creating AdminDashboardRedesigned instance...");
                var dashboard = new AdminDashboardRedesigned();
                System.Diagnostics.Debug.WriteLine("AdminDashboardRedesigned created successfully");
                return dashboard;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating AdminDashboardRedesigned: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // Show detailed error to user
                MessageBox.Show($"Error creating Admin Dashboard: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Dashboard Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Fallback to test dashboard if admin dashboard fails
                return new TestDashboard();
            }
        }
        
        /// <summary>
        /// Gets the manager dashboard with management access
        /// </summary>
        /// <returns>Manager dashboard form</returns>
        private static Form GetManagerDashboard()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Creating ManagerDashboard instance...");
                var dashboard = new ManagerDashboard();
                System.Diagnostics.Debug.WriteLine("ManagerDashboard created successfully");
                return dashboard;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating ManagerDashboard: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return GetBasicDashboard();
            }
        }
        
        /// <summary>
        /// Gets the salesman dashboard with sales access
        /// </summary>
        /// <returns>Salesman dashboard form</returns>
        private static Form GetSalesmanDashboard()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Creating SalesmanDashboard instance...");
                var dashboard = new SalesmanDashboard();
                System.Diagnostics.Debug.WriteLine("SalesmanDashboard created successfully");
                return dashboard;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating SalesmanDashboard: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return GetBasicDashboard();
            }
        }
        
        /// <summary>
        /// Gets the storekeeper dashboard with inventory access
        /// </summary>
        /// <returns>Storekeeper dashboard form</returns>
        private static Form GetStorekeeperDashboard()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Creating StorekeeperDashboard instance...");
                var dashboard = new StorekeeperDashboard();
                System.Diagnostics.Debug.WriteLine("StorekeeperDashboard created successfully");
                return dashboard;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating StorekeeperDashboard: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return GetBasicDashboard();
            }
        }
        
        /// <summary>
        /// Gets the accountant dashboard with financial access
        /// </summary>
        /// <returns>Accountant dashboard form</returns>
        private static Form GetAccountantDashboard()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Creating AccountantDashboard instance...");
                var dashboard = new AccountantDashboard();
                System.Diagnostics.Debug.WriteLine("AccountantDashboard created successfully");
                return dashboard;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating AccountantDashboard: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                return GetBasicDashboard();
            }
        }
        
        /// <summary>
        /// Gets a basic dashboard for users with unknown roles
        /// </summary>
        /// <returns>Basic dashboard form</returns>
        private static Form GetBasicDashboard()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Creating BasicDashboard instance...");
                var dashboard = new BasicDashboard();
                System.Diagnostics.Debug.WriteLine("BasicDashboard created successfully");
                return dashboard;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating BasicDashboard: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // Fallback to a simple form if all else fails
                System.Diagnostics.Debug.WriteLine("Creating fallback basic form...");
                var form = new Form
                {
                    Text = $"Dashboard - {UserSession.GetDisplayName()}",
                    Size = new System.Drawing.Size(800, 500),
                    StartPosition = FormStartPosition.CenterScreen
                };
                
                var label = new Label
                {
                    Text = "BASIC DASHBOARD\n\nLimited access granted\n\n" +
                           "Please contact administrator for proper role assignment.",
                    Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                
                form.Controls.Add(label);
                System.Diagnostics.Debug.WriteLine("Fallback basic form created successfully");
                return form;
            }
        }
        
        #endregion
        
        #region Permission-Based Access Control
        
        /// <summary>
        /// Checks if the current user can access a specific feature
        /// </summary>
        /// <param name="featureName">The feature name to check</param>
        /// <returns>True if access is allowed, false otherwise</returns>
        public static bool CanAccessFeature(string featureName)
        {
            if (!UserSession.IsLoggedIn)
                return false;
                
            // Admin has access to everything
            if (UserSession.IsAdmin)
                return true;
                
            // Check specific feature permissions
            switch (featureName.ToLower())
            {
                case "user_management":
                    return UserSession.CanManageUsers;
                    
                case "sales":
                    return UserSession.CanAccessSales;
                    
                case "inventory":
                    return UserSession.CanAccessInventory;
                    
                case "financial":
                    return UserSession.CanAccessFinancial;
                    
                case "reports":
                    return UserSession.HasAnyPermission("REPORT_VIEW", "REPORT_GENERATE");
                    
                default:
                    return false;
            }
        }
        
        /// <summary>
        /// Shows an access denied message
        /// </summary>
        /// <param name="featureName">The feature that was denied</param>
        public static void ShowAccessDenied(string featureName)
        {
            MessageBox.Show($"Access denied to {featureName}. You don't have the required permissions.", 
                "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        #endregion
    }
}

