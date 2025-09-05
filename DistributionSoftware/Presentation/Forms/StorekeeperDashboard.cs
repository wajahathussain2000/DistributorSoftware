using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public class StorekeeperDashboard : BaseDashboard
    {
        private FlowLayoutPanel chartsContainer;
        private Label totalProductsLabel;
        private Label lowStockLabel;
        private Label totalWarehousesLabel;
        private Label pendingReceiptsLabel;

        public StorekeeperDashboard()
        {
            CreateNavigationMenu();
            CreateDashboardContent();
            LoadDashboardData();
        }

        protected override void CreateNavigationMenu()
        {
            // Navigation Buttons
            Button dashboardBtn = CreateNavButton("Dashboard", "📊", (s, e) => ShowDashboard());
            Button inventoryBtn = CreateNavButton("Inventory", "🏪", (s, e) => ShowInventory());
            Button productsBtn = CreateNavButton("Products", "📦", (s, e) => ShowProducts());
            Button warehousesBtn = CreateNavButton("Warehouses", "🏭", (s, e) => ShowWarehouses());
            Button stockInBtn = CreateNavButton("Stock In", "📥", (s, e) => ShowStockIn());
            Button stockOutBtn = CreateNavButton("Stock Out", "📤", (s, e) => ShowStockOut());
            Button reportsBtn = CreateNavButton("Reports", "📈", (s, e) => ShowReports());
            Button logoutBtn = CreateNavButton("Logout", "🚪", (s, e) => Logout());

            // Position buttons
            int buttonX = 20;
            dashboardBtn.Location = new Point(buttonX, 10);
            inventoryBtn.Location = new Point(buttonX + 160, 10);
            productsBtn.Location = new Point(buttonX + 320, 10);
            warehousesBtn.Location = new Point(buttonX + 480, 10);
            stockInBtn.Location = new Point(buttonX + 640, 10);
            stockOutBtn.Location = new Point(buttonX + 800, 10);
            reportsBtn.Location = new Point(buttonX + 960, 10);
            logoutBtn.Location = new Point(buttonX + 1120, 10);

            // Add controls to navigation panel
            navigationPanel.Controls.Add(dashboardBtn);
            navigationPanel.Controls.Add(inventoryBtn);
            navigationPanel.Controls.Add(productsBtn);
            navigationPanel.Controls.Add(warehousesBtn);
            navigationPanel.Controls.Add(stockInBtn);
            navigationPanel.Controls.Add(stockOutBtn);
            navigationPanel.Controls.Add(reportsBtn);
            navigationPanel.Controls.Add(logoutBtn);
        }

        protected override void CreateDashboardContent()
        {
            // Main title
            Label mainTitle = new Label
            {
                Text = "Storekeeper Dashboard Overview",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(0, 0)
            };

            // Stats Container
            FlowLayoutPanel statsContainer = new FlowLayoutPanel
            {
                Location = new Point(0, 50),
                Size = new Size(contentPanel.Width, 120),
                AutoScroll = true
            };

            // Create stat panels
            Panel productsPanel = CreateStatPanel("Total Products", "0", Color.FromArgb(52, 152, 219), "📦");
            Panel lowStockPanel = CreateStatPanel("Low Stock Items", "0", Color.FromArgb(231, 76, 60), "⚠️");
            Panel warehousesPanel = CreateStatPanel("Total Warehouses", "0", Color.FromArgb(46, 204, 113), "🏭");
            Panel receiptsPanel = CreateStatPanel("Pending Receipts", "0", Color.FromArgb(155, 89, 182), "📋");

            statsContainer.Controls.Add(productsPanel);
            statsContainer.Controls.Add(lowStockPanel);
            statsContainer.Controls.Add(warehousesPanel);
            statsContainer.Controls.Add(receiptsPanel);

            // Charts Container
            chartsContainer = new FlowLayoutPanel
            {
                Location = new Point(0, 190),
                Size = new Size(contentPanel.Width, contentPanel.Height - 190),
                AutoScroll = true
            };

            // Create chart panels
            Panel inventoryChartPanel = CreateChartPanel("📊 Inventory Status", Color.FromArgb(52, 152, 219));
            Panel stockChartPanel = CreateChartPanel("📈 Stock Movement", Color.FromArgb(46, 204, 113));
            Panel warehouseChartPanel = CreateChartPanel("🏭 Warehouse Utilization", Color.FromArgb(155, 89, 182));
            Panel alertsChartPanel = CreateChartPanel("⚠️ Stock Alerts", Color.FromArgb(230, 126, 34));

            // Add sample content to charts
            AddSampleChartContent(inventoryChartPanel, "Inventory Status Chart");
            AddSampleChartContent(stockChartPanel, "Stock Movement Chart");
            AddSampleChartContent(warehouseChartPanel, "Warehouse Utilization Chart");
            AddSampleChartContent(alertsChartPanel, "Stock Alerts Chart");

            chartsContainer.Controls.Add(inventoryChartPanel);
            chartsContainer.Controls.Add(stockChartPanel);
            chartsContainer.Controls.Add(warehouseChartPanel);
            chartsContainer.Controls.Add(alertsChartPanel);

            // Add to content panel
            contentPanel.Controls.Add(mainTitle);
            contentPanel.Controls.Add(statsContainer);
            contentPanel.Controls.Add(chartsContainer);

            // Store references for data updates
            totalProductsLabel = productsPanel.Controls[2] as Label;
            lowStockLabel = lowStockPanel.Controls[2] as Label;
            totalWarehousesLabel = warehousesPanel.Controls[2] as Label;
            pendingReceiptsLabel = receiptsPanel.Controls[2] as Label;
        }

        private Panel CreateStatPanel(string title, string value, Color color, string icon)
        {
            Panel panel = new Panel
            {
                Size = new Size(280, 100),
                BackColor = Color.White,
                Margin = new Padding(10),
                Padding = new Padding(15)
            };

            Label iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };

            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(60, 20),
                AutoSize = true
            };

            Label valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(60, 40),
                AutoSize = true
            };

            panel.Controls.Add(iconLabel);
            panel.Controls.Add(titleLabel);
            panel.Controls.Add(valueLabel);

            return panel;
        }

        private void AddSampleChartContent(Panel panel, string chartName)
        {
            Label chartLabel = new Label
            {
                Text = $"{chartName}\n\nSample chart data would be displayed here.\nThis could be a real chart using a charting library.",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(108, 117, 125),
                Location = new Point(0, 30),
                Size = new Size(panel.Width - 30, 150),
                TextAlign = ContentAlignment.TopLeft
            };

            panel.Controls.Add(chartLabel);
        }

        private void LoadDashboardData()
        {
            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.DistributionConnectionString))
                {
                    connection.Open();

                    // Get total products
                    var productQuery = "SELECT COUNT(*) FROM Products WHERE IsActive = 1";
                    using (var command = new SqlCommand(productQuery, connection))
                    {
                        int productCount = (int)command.ExecuteScalar();
                        totalProductsLabel.Text = productCount.ToString();
                    }

                    // Get low stock items
                    var lowStockQuery = @"SELECT COUNT(*) FROM Stock 
                                        WHERE Quantity <= ReorderLevel AND IsActive = 1";
                    using (var command = new SqlCommand(lowStockQuery, connection))
                    {
                        int lowStockCount = (int)command.ExecuteScalar();
                        lowStockLabel.Text = lowStockCount.ToString();
                    }

                    // Get total warehouses
                    var warehouseQuery = "SELECT COUNT(*) FROM Warehouses WHERE IsActive = 1";
                    using (var command = new SqlCommand(warehouseQuery, connection))
                    {
                        int warehouseCount = (int)command.ExecuteScalar();
                        totalWarehousesLabel.Text = warehouseCount.ToString();
                    }

                    // Get pending receipts
                    var receiptQuery = @"SELECT COUNT(*) FROM PurchaseInvoices 
                                       WHERE Status = 'Pending'";
                    using (var command = new SqlCommand(receiptQuery, connection))
                    {
                        int receiptCount = (int)command.ExecuteScalar();
                        pendingReceiptsLabel.Text = receiptCount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Navigation Methods
        private void ShowDashboard()
        {
            // Already on dashboard
        }

        private void ShowInventory()
        {
            MessageBox.Show("Inventory module would open here", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowProducts()
        {
            MessageBox.Show("Products module would open here", "Products", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowWarehouses()
        {
            MessageBox.Show("Warehouses module would open here", "Warehouses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowStockIn()
        {
            MessageBox.Show("Stock In module would open here", "Stock In", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowStockOut()
        {
            MessageBox.Show("Stock Out module would open here", "Stock Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowReports()
        {
            MessageBox.Show("Reports module would open here", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Logout()
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserSession.ClearSession();
                this.Close();
                new LoginForm().Show();
            }
        }
    }
}
