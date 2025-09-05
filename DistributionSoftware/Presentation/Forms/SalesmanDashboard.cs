using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public class SalesmanDashboard : BaseDashboard
    {
        private FlowLayoutPanel chartsContainer;
        private Label todaySalesLabel;
        private Label totalCommissionLabel;
        private Label totalCustomersLabel;
        private Label pendingOrdersLabel;

        public SalesmanDashboard()
        {
            CreateNavigationMenu();
            CreateDashboardContent();
            LoadDashboardData();
        }

        protected override void CreateNavigationMenu()
        {
            // Navigation Buttons
            Button dashboardBtn = CreateNavButton("Dashboard", "📊", (s, e) => ShowDashboard());
            Button salesBtn = CreateNavButton("Sales", "💰", (s, e) => ShowSales());
            Button customersBtn = CreateNavButton("Customers", "👤", (s, e) => ShowCustomers());
            Button productsBtn = CreateNavButton("Products", "📦", (s, e) => ShowProducts());
            Button ordersBtn = CreateNavButton("Orders", "📋", (s, e) => ShowOrders());
            Button leadsBtn = CreateNavButton("Leads", "🎯", (s, e) => ShowLeads());
            Button reportsBtn = CreateNavButton("Reports", "📈", (s, e) => ShowReports());
            Button logoutBtn = CreateNavButton("Logout", "🚪", (s, e) => Logout());

            // Position buttons
            int buttonX = 20;
            dashboardBtn.Location = new Point(buttonX, 10);
            salesBtn.Location = new Point(buttonX + 160, 10);
            customersBtn.Location = new Point(buttonX + 320, 10);
            productsBtn.Location = new Point(buttonX + 480, 10);
            ordersBtn.Location = new Point(buttonX + 640, 10);
            leadsBtn.Location = new Point(buttonX + 800, 10);
            reportsBtn.Location = new Point(buttonX + 960, 10);
            logoutBtn.Location = new Point(buttonX + 1120, 10);

            // Add controls to navigation panel
            navigationPanel.Controls.Add(dashboardBtn);
            navigationPanel.Controls.Add(salesBtn);
            navigationPanel.Controls.Add(customersBtn);
            navigationPanel.Controls.Add(productsBtn);
            navigationPanel.Controls.Add(ordersBtn);
            navigationPanel.Controls.Add(leadsBtn);
            navigationPanel.Controls.Add(reportsBtn);
            navigationPanel.Controls.Add(logoutBtn);
        }

        protected override void CreateDashboardContent()
        {
            // Main title
            Label mainTitle = new Label
            {
                Text = "Salesman Dashboard Overview",
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
            Panel salesPanel = CreateStatPanel("Today's Sales", "0", Color.FromArgb(52, 152, 219), "💰");
            Panel commissionPanel = CreateStatPanel("Total Commission", "$0", Color.FromArgb(46, 204, 113), "💵");
            Panel customersPanel = CreateStatPanel("My Customers", "0", Color.FromArgb(155, 89, 182), "👤");
            Panel ordersPanel = CreateStatPanel("Pending Orders", "0", Color.FromArgb(230, 126, 34), "📋");

            statsContainer.Controls.Add(salesPanel);
            statsContainer.Controls.Add(commissionPanel);
            statsContainer.Controls.Add(customersPanel);
            statsContainer.Controls.Add(ordersPanel);

            // Charts Container
            chartsContainer = new FlowLayoutPanel
            {
                Location = new Point(0, 190),
                Size = new Size(contentPanel.Width, contentPanel.Height - 190),
                AutoScroll = true
            };

            // Create chart panels
            Panel salesChartPanel = CreateChartPanel("📈 My Sales Performance", Color.FromArgb(52, 152, 219));
            Panel customerChartPanel = CreateChartPanel("👥 Customer Engagement", Color.FromArgb(46, 204, 113));
            Panel commissionChartPanel = CreateChartPanel("💵 Commission Tracking", Color.FromArgb(155, 89, 182));
            Panel leadsChartPanel = CreateChartPanel("🎯 Lead Conversion", Color.FromArgb(230, 126, 34));

            // Add sample content to charts
            AddSampleChartContent(salesChartPanel, "Sales Performance Chart");
            AddSampleChartContent(customerChartPanel, "Customer Engagement Chart");
            AddSampleChartContent(commissionChartPanel, "Commission Tracking Chart");
            AddSampleChartContent(leadsChartPanel, "Lead Conversion Chart");

            chartsContainer.Controls.Add(salesChartPanel);
            chartsContainer.Controls.Add(customerChartPanel);
            chartsContainer.Controls.Add(commissionChartPanel);
            chartsContainer.Controls.Add(leadsChartPanel);

            // Add to content panel
            contentPanel.Controls.Add(mainTitle);
            contentPanel.Controls.Add(statsContainer);
            contentPanel.Controls.Add(chartsContainer);

            // Store references for data updates
            todaySalesLabel = salesPanel.Controls[2] as Label;
            totalCommissionLabel = commissionPanel.Controls[2] as Label;
            totalCustomersLabel = customersPanel.Controls[2] as Label;
            pendingOrdersLabel = ordersPanel.Controls[2] as Label;
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

                    // Get today's sales for this salesman
                    var salesQuery = @"SELECT COUNT(*) FROM SalesInvoices 
                                     WHERE CAST(InvoiceDate AS DATE) = CAST(GETDATE() AS DATE) 
                                     AND SalesmanID = @SalesmanID";
                    using (var command = new SqlCommand(salesQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanID", UserSession.CurrentUserId);
                        int salesCount = (int)command.ExecuteScalar();
                        todaySalesLabel.Text = salesCount.ToString();
                    }

                    // Get total commission for this salesman
                    var commissionQuery = @"SELECT ISNULL(SUM(CommissionAmount), 0) FROM SalesInvoices 
                                          WHERE SalesmanID = @SalesmanID";
                    using (var command = new SqlCommand(commissionQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanID", UserSession.CurrentUserId);
                        decimal commission = (decimal)command.ExecuteScalar();
                        totalCommissionLabel.Text = $"${commission:N2}";
                    }

                    // Get total customers for this salesman
                    var customerQuery = @"SELECT COUNT(DISTINCT CustomerID) FROM SalesInvoices 
                                        WHERE SalesmanID = @SalesmanID";
                    using (var command = new SqlCommand(customerQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanID", UserSession.CurrentUserId);
                        int customerCount = (int)command.ExecuteScalar();
                        totalCustomersLabel.Text = customerCount.ToString();
                    }

                    // Get pending orders for this salesman
                    var orderQuery = @"SELECT COUNT(*) FROM SalesInvoices 
                                     WHERE SalesmanID = @SalesmanID AND Status = 'Pending'";
                    using (var command = new SqlCommand(orderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanID", UserSession.CurrentUserId);
                        int orderCount = (int)command.ExecuteScalar();
                        pendingOrdersLabel.Text = orderCount.ToString();
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

        private void ShowSales()
        {
            MessageBox.Show("Sales module would open here", "Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowCustomers()
        {
            MessageBox.Show("Customers module would open here", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowProducts()
        {
            MessageBox.Show("Products module would open here", "Products", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowOrders()
        {
            MessageBox.Show("Orders module would open here", "Orders", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowLeads()
        {
            MessageBox.Show("Leads module would open here", "Leads", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
