using System;
using System.Drawing;
using System.Windows.Forms;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public class BasicDashboard : Form
    {
        private Timer sessionTimer;
        private Label sessionLabel;

        public BasicDashboard()
        {
            try
            {
                // Validate session before creating dashboard
                if (!UserSession.IsLoggedIn)
                {
                    MessageBox.Show("No active session found. Please log in again.", "Session Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Don't restart, just close this form
                    this.Close();
                    return;
                }

                SetupForm();
                InitializeSessionTimer();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating BasicDashboard: {ex.Message}");
                MessageBox.Show($"Error creating dashboard: {ex.Message}", "Dashboard Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void SetupForm()
        {
            this.Text = $"Basic Dashboard - {UserSession.GetDisplayName()}";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Icon = SystemIcons.Application;

            CreateDashboardLayout();
        }

        private void CreateDashboardLayout()
        {
            // Main container panel
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(245, 247, 250)
            };

            // Top header panel
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(52, 73, 94),
                Padding = new Padding(20)
            };

            // Header title
            var headerLabel = new Label
            {
                Text = "BASIC DASHBOARD",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Left,
                Width = 250
            };

            // User info panel
            var userInfoPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 250,
                BackColor = Color.Transparent
            };

            // Welcome message
            var welcomeLabel = new Label
            {
                Text = $"Welcome, {UserSession.GetDisplayName()}!",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Top,
                Height = 25
            };

            // Role info
            var roleLabel = new Label
            {
                Text = $"Role: {UserSession.CurrentUserRole}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 200, 200),
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Top,
                Height = 20
            };

            // Session duration
            sessionLabel = new Label
            {
                Text = $"Session: {UserSession.GetSessionDurationMinutes()} minutes",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(180, 180, 180),
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Top,
                Height = 20
            };

            // Logout button
            var logoutButton = new Button
            {
                Text = "LOGOUT",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(100, 35),
                Location = new Point(140, 30),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.Click += LogoutButton_Click;

            // Add controls to header
            userInfoPanel.Controls.Add(welcomeLabel);
            userInfoPanel.Controls.Add(roleLabel);
            userInfoPanel.Controls.Add(sessionLabel);
            userInfoPanel.Controls.Add(logoutButton);

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(userInfoPanel);

            // Content panel
            var contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(245, 247, 250)
            };

            // Basic stats panel
            var statsPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            CreateStatCard(statsPanel, "Available Modules", "3", Color.FromArgb(52, 73, 94), 0);
            CreateStatCard(statsPanel, "Last Login", "Today", Color.FromArgb(52, 152, 219), 1);
            CreateStatCard(statsPanel, "System Status", "Online", Color.FromArgb(46, 204, 113), 2);
            CreateStatCard(statsPanel, "Support Contact", "IT Helpdesk", Color.FromArgb(155, 89, 182), 3);

            // Modules panel
            var modulesPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(245, 247, 250)
            };

            // Create basic module buttons
            CreateModuleButton(modulesPanel, "📋 View Reports", "View basic reports and data", 0, 0, Color.FromArgb(52, 152, 219));
            CreateModuleButton(modulesPanel, "📊 Basic Analytics", "View basic analytics", 0, 1, Color.FromArgb(46, 204, 113));
            CreateModuleButton(modulesPanel, "📞 Contact Support", "Get help and support", 1, 0, Color.FromArgb(155, 89, 182));

            // Add panels to main container
            mainPanel.Controls.Add(headerPanel);
            mainPanel.Controls.Add(statsPanel);
            mainPanel.Controls.Add(modulesPanel);

            this.Controls.Add(mainPanel);
        }

        private void CreateStatCard(Panel parent, string title, string value, Color color, int index)
        {
            var card = new Panel
            {
                Size = new Size(220, 80),
                Location = new Point(index * 230 + 20, 10),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(15, 10),
                Size = new Size(190, 20)
            };

            var valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(15, 35),
                Size = new Size(190, 30)
            };

            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);
            parent.Controls.Add(card);
        }

        private void CreateModuleButton(Panel parent, string title, string description, int row, int col, Color color)
        {
            var button = new Button
            {
                Text = $"{title}\n\n{description}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(300, 120),
                Location = new Point(col * 320 + 20, row * 140 + 20),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = title,
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            button.Click += ModuleButton_Click;

            // Add hover effect
            button.MouseEnter += (s, e) => {
                button.BackColor = ControlPaint.Light(color);
            };
            button.MouseLeave += (s, e) => {
                button.BackColor = color;
            };

            parent.Controls.Add(button);
        }

        private void InitializeSessionTimer()
        {
            sessionTimer = new Timer();
            sessionTimer.Interval = 30000; // Update every 30 seconds
            sessionTimer.Tick += SessionTimer_Tick;
            sessionTimer.Start();
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            if (sessionLabel != null)
            {
                sessionLabel.Text = $"Session: {UserSession.GetSessionDurationMinutes()} minutes";
            }
        }

        private void ModuleButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is string moduleName)
            {
                var cleanModuleName = moduleName.Replace("📋", "").Replace("📊", "").Replace("📞", "").Trim();
                
                MessageBox.Show($"Opening {cleanModuleName} module...\n\nThis feature is under development.\n\n" +
                              $"User: {UserSession.GetDisplayName()}\n" +
                              $"Role: {UserSession.CurrentUserRole}\n" +
                              $"Session: {UserSession.GetSessionDurationMinutes()} minutes\n\n" +
                              $"Note: You have limited access. Contact your administrator for additional permissions.", 
                    "Module Access", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?\n\nThis will end your current session.", 
                "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Stop the session timer
                    if (sessionTimer != null)
                    {
                        sessionTimer.Stop();
                        sessionTimer.Dispose();
                    }

                    // Clear the session
                    UserSession.ClearSession();
                    
                    // Close current form
                    this.Close();
                    
                    // Show login form again
                    var loginForm = new LoginForm();
                    loginForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during logout: {ex.Message}", "Logout Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("Are you sure you want to exit the application?\n\n" +
                                           "This will close all active sessions.", 
                    "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Stop the session timer
                    if (sessionTimer != null)
                    {
                        sessionTimer.Stop();
                        sessionTimer.Dispose();
                    }

                    // Clear session
                    UserSession.ClearSession();
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            base.OnFormClosing(e);
        }
    }
}
