using System;
using System.Drawing;
using System.Windows.Forms;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public class SimpleAdminDashboard : Form
    {
        public SimpleAdminDashboard()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("SimpleAdminDashboard constructor started");
                System.Diagnostics.Debug.WriteLine($"Session check in constructor - IsLoggedIn: {UserSession.IsLoggedIn}");
                System.Diagnostics.Debug.WriteLine($"Current user: {UserSession.GetDisplayName()}, Role: {UserSession.CurrentUserRole}");
                System.Diagnostics.Debug.WriteLine($"User ID: {UserSession.CurrentUserId}, Session start time: {UserSession.SessionStartTime}");
                
                // Validate session before creating dashboard
                if (!UserSession.IsLoggedIn)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: No user session found in SimpleAdminDashboard constructor!");
                    MessageBox.Show("No active session found. Please log in again.", "Session Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"User session found: {UserSession.GetDisplayName()}, Role: {UserSession.CurrentUserRole}");
                
                // Simple form setup
                this.Text = $"Simple Admin Dashboard - {UserSession.GetDisplayName()}";
                this.Size = new Size(800, 600);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.BackColor = Color.White;
                
                // Simple label
                var label = new Label
                {
                    Text = $"Welcome, {UserSession.GetDisplayName()}!\nRole: {UserSession.CurrentUserRole}\n\nThis is a simple test dashboard.",
                    Font = new Font("Arial", 16, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                
                this.Controls.Add(label);
                
                System.Diagnostics.Debug.WriteLine("SimpleAdminDashboard constructor completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating SimpleAdminDashboard: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error creating dashboard: {ex.Message}", "Dashboard Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
