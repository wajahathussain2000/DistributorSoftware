using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Common;
using System.Collections.Generic;
using System.Linq;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            InitializeDashboard();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Name = "AdminDashboard";
            this.Text = "Admin Dashboard (Legacy)";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            
            this.ResumeLayout(false);
        }

        private void InitializeDashboard()
        {
            // Simple initialization for legacy dashboard
            this.Text = "Admin Dashboard (Legacy) - Use AdminDashboardRedesigned for LiveCharts";
            
            // Add a message label
            var messageLabel = new Label
            {
                Text = "This is the legacy Admin Dashboard.\n\nFor the new LiveCharts dashboard with professional charts,\nplease use AdminDashboardRedesigned instead.",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(50, 50)
            };
            
            this.Controls.Add(messageLabel);
        }

        private string GetDynamicGreeting()
        {
            var hour = DateTime.Now.Hour;
            if (hour < 12) return "Good morning!";
            if (hour < 17) return "Good afternoon!";
            return "Good evening!";
        }

        // Event handlers for designer buttons
        private void DashboardBtn_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void UsersBtn_Click(object sender, EventArgs e)
        {
            ShowUserManagement();
        }

        private void ProductsBtn_Click(object sender, EventArgs e)
        {
            ShowProducts();
        }

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            ShowInventory();
        }

        private void SalesBtn_Click(object sender, EventArgs e)
        {
            ShowSales();
        }

        private void PurchasesBtn_Click(object sender, EventArgs e)
        {
            ShowPurchases();
        }

        private void CustomersBtn_Click(object sender, EventArgs e)
        {
            ShowCustomers();
        }

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            ShowSuppliers();
        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            ShowReports();
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // Navigation methods
        private void ShowDashboard()
        {
            MessageBox.Show("Dashboard functionality - Use AdminDashboardRedesigned for LiveCharts", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowUserManagement()
        {
            MessageBox.Show("User Management functionality", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowProducts()
        {
            MessageBox.Show("Products functionality", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowInventory()
        {
            MessageBox.Show("Inventory functionality", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowSales()
        {
            MessageBox.Show("Sales functionality", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowPurchases()
        {
            MessageBox.Show("Purchases functionality", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowCustomers()
        {
            MessageBox.Show("Customers functionality", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowSuppliers()
        {
            MessageBox.Show("Suppliers functionality", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowReports()
        {
            MessageBox.Show("Reports functionality", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowSettings()
        {
            MessageBox.Show("Settings functionality", 
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
