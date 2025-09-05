using System;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    public class TestDashboard : Form
    {
        public TestDashboard()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("TestDashboard constructor started");
                
                // Minimal form setup - no UserSession access
                this.Text = "Test Dashboard";
                this.Size = new Size(600, 400);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.BackColor = Color.LightBlue;
                
                // Simple label
                var label = new Label
                {
                    Text = "TEST DASHBOARD\n\nThis is a minimal test dashboard.\n\nIf you can see this, the dashboard creation works!",
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                
                this.Controls.Add(label);
                
                System.Diagnostics.Debug.WriteLine("TestDashboard constructor completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating TestDashboard: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error creating dashboard: {ex.Message}", "Dashboard Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
