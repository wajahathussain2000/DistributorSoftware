using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public class AccountantDashboard : BaseDashboard
    {
        private FlowLayoutPanel chartsContainer;
        private Label totalRevenueLabel;
        private Label totalExpensesLabel;
        private Label profitLabel;
        private Label pendingPaymentsLabel;

        public AccountantDashboard()
        {
            CreateNavigationMenu();
            CreateDashboardContent();
            LoadDashboardData();
        }

        protected override void CreateNavigationMenu()
        {
            // Navigation Buttons
            Button dashboardBtn = CreateNavButton("Dashboard", "📊", (s, e) => ShowDashboard());
            Button accountsBtn = CreateNavButton("Accounts", "💰", (s, e) => ShowAccounts());
            Button invoicesBtn = CreateNavButton("Invoices", "📄", (s, e) => ShowInvoices());
            Button paymentsBtn = CreateNavButton("Payments", "💳", (s, e) => ShowPayments());
            Button expensesBtn = CreateNavButton("Expenses", "💸", (s, e) => ShowExpenses());
            Button reportsBtn = CreateNavButton("Reports", "📈", (s, e) => ShowReports());
            Button ledgerBtn = CreateNavButton("Ledger", "📋", (s, e) => ShowLedger());
            Button logoutBtn = CreateNavButton("Logout", "🚪", (s, e) => Logout());

            // Position buttons
            int buttonX = 20;
            dashboardBtn.Location = new Point(buttonX, 10);
            accountsBtn.Location = new Point(buttonX + 160, 10);
            invoicesBtn.Location = new Point(buttonX + 320, 10);
            paymentsBtn.Location = new Point(buttonX + 480, 10);
            expensesBtn.Location = new Point(buttonX + 640, 10);
            reportsBtn.Location = new Point(buttonX + 800, 10);
            ledgerBtn.Location = new Point(buttonX + 1120, 10);
            logoutBtn.Location = new Point(buttonX + 1280, 10);

            // Add controls to navigation panel
            navigationPanel.Controls.Add(dashboardBtn);
            navigationPanel.Controls.Add(accountsBtn);
            navigationPanel.Controls.Add(invoicesBtn);
            navigationPanel.Controls.Add(paymentsBtn);
            navigationPanel.Controls.Add(expensesBtn);
            navigationPanel.Controls.Add(reportsBtn);
            navigationPanel.Controls.Add(ledgerBtn);
            navigationPanel.Controls.Add(logoutBtn);
        }

        protected override void CreateDashboardContent()
        {
            // Main title
            Label mainTitle = new Label
            {
                Text = "Accountant Dashboard Overview",
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
            Panel revenuePanel = CreateStatPanel("Total Revenue", "$0", Color.FromArgb(46, 204, 113), "💰");
            Panel expensesPanel = CreateStatPanel("Total Expenses", "$0", Color.FromArgb(231, 76, 60), "💸");
            Panel profitPanel = CreateStatPanel("Net Profit", "$0", Color.FromArgb(52, 152, 219), "💵");
            Panel paymentsPanel = CreateStatPanel("Pending Payments", "$0", Color.FromArgb(155, 89, 182), "📋");

            statsContainer.Controls.Add(revenuePanel);
            statsContainer.Controls.Add(expensesPanel);
            statsContainer.Controls.Add(profitPanel);
            statsContainer.Controls.Add(paymentsPanel);

            // Charts Container
            chartsContainer = new FlowLayoutPanel
            {
                Location = new Point(0, 190),
                Size = new Size(contentPanel.Width, contentPanel.Height - 190),
                AutoScroll = true
            };

            // Create chart panels
            Panel revenueChartPanel = CreateChartPanel("📈 Revenue Analysis", Color.FromArgb(46, 204, 113));
            Panel expensesChartPanel = CreateChartPanel("📊 Expense Breakdown", Color.FromArgb(231, 76, 60));
            Panel profitChartPanel = CreateChartPanel("💰 Profit Trends", Color.FromArgb(52, 152, 219));
            Panel cashFlowChartPanel = CreateChartPanel("💳 Cash Flow", Color.FromArgb(155, 89, 182));

            // Add sample content to charts
            AddSampleChartContent(revenueChartPanel, "Revenue Analysis Chart");
            AddSampleChartContent(expensesChartPanel, "Expense Breakdown Chart");
            AddSampleChartContent(profitChartPanel, "Profit Trends Chart");
            AddSampleChartContent(cashFlowChartPanel, "Cash Flow Chart");

            chartsContainer.Controls.Add(revenueChartPanel);
            chartsContainer.Controls.Add(expensesChartPanel);
            chartsContainer.Controls.Add(profitChartPanel);
            chartsContainer.Controls.Add(cashFlowChartPanel);

            // Add to content panel
            contentPanel.Controls.Add(mainTitle);
            contentPanel.Controls.Add(statsContainer);
            contentPanel.Controls.Add(chartsContainer);

            // Store references for data updates
            totalRevenueLabel = revenuePanel.Controls[2] as Label;
            totalExpensesLabel = expensesPanel.Controls[2] as Label;
            profitLabel = profitPanel.Controls[2] as Label;
            pendingPaymentsLabel = paymentsPanel.Controls[2] as Label;
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

                    // Get total revenue
                    var revenueQuery = "SELECT ISNULL(SUM(TotalAmount), 0) FROM SalesInvoices";
                    using (var command = new SqlCommand(revenueQuery, connection))
                    {
                        decimal revenue = (decimal)command.ExecuteScalar();
                        totalRevenueLabel.Text = $"${revenue:N2}";
                    }

                    // Get total expenses
                    var expensesQuery = "SELECT ISNULL(SUM(TotalAmount), 0) FROM PurchaseInvoices";
                    using (var command = new SqlCommand(expensesQuery, connection))
                    {
                        decimal expenses = (decimal)command.ExecuteScalar();
                        totalExpensesLabel.Text = $"${expenses:N2}";
                    }

                    // Calculate profit
                    var profitQuery = @"SELECT 
                                        ISNULL((SELECT SUM(TotalAmount) FROM SalesInvoices), 0) - 
                                        ISNULL((SELECT SUM(TotalAmount) FROM PurchaseInvoices), 0)";
                    using (var command = new SqlCommand(profitQuery, connection))
                    {
                        decimal profit = (decimal)command.ExecuteScalar();
                        profitLabel.Text = $"${profit:N2}";
                    }

                    // Get pending payments
                    var paymentsQuery = @"SELECT ISNULL(SUM(TotalAmount), 0) FROM SalesInvoices 
                                        WHERE Status = 'Pending'";
                    using (var command = new SqlCommand(paymentsQuery, connection))
                    {
                        decimal payments = (decimal)command.ExecuteScalar();
                        pendingPaymentsLabel.Text = $"${payments:N2}";
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

        private void ShowAccounts()
        {
            try
            {
                ChartOfAccountsForm chartOfAccountsForm = new ChartOfAccountsForm();
                chartOfAccountsForm.StartPosition = FormStartPosition.CenterParent;
                chartOfAccountsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Chart of Accounts Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowInvoices()
        {
            MessageBox.Show("Invoices module would open here", "Invoices", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowPayments()
        {
            MessageBox.Show("Payments module would open here", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowExpenses()
        {
            MessageBox.Show("Expenses module would open here", "Expenses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowReports()
        {
            MessageBox.Show("Reports module would open here", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowLedger()
        {
            try
            {
                ChartOfAccountsForm chartOfAccountsForm = new ChartOfAccountsForm();
                chartOfAccountsForm.StartPosition = FormStartPosition.CenterParent;
                chartOfAccountsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Chart of Accounts Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
