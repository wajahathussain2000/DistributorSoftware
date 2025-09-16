using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Common;
using System.Collections.Generic;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using DistributionSoftware.Business;
using DistributionSoftware.Presentation.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class AdminDashboardRedesigned : Form
    {
        private string connectionString;
        
        // LiveChartsCore controls
        private CartesianChart salesChart;
        private PieChart inventoryChart;
        private CartesianChart revenueChart;
        
        // New charts
        private CartesianChart customerChart;
        private CartesianChart topProductsChart;
        private CartesianChart purchaseVsSalesChart;

        // Dropdown menus
        private ContextMenuStrip usersDropdown;
        private ContextMenuStrip productsDropdown;
        private ContextMenuStrip inventoryDropdown;
        private ContextMenuStrip salesDropdown;
        private ContextMenuStrip purchasesDropdown;
        private ContextMenuStrip customersDropdown;
        private ContextMenuStrip suppliersDropdown;
        private ContextMenuStrip reportsDropdown;
        private ContextMenuStrip expenseDropdown;
        private ContextMenuStrip accountingDropdown;
        private ContextMenuStrip settingsDropdown;

        public AdminDashboardRedesigned()
        {
            InitializeComponent();
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DistributionConnection"]?.ConnectionString;
            
            // Test database connection
            TestDatabaseConnection();
            
            InitializeDashboard();
            SetupResponsiveLayout();
        }

        private void InitializeDashboard()
        {
            // Set up user info
            userInfoLabel.Text = $"Welcome, {UserSession.GetDisplayName()}";
            greetingLabel.Text = GetDynamicGreeting();
            
            // Start session timer
            sessionTimer.Start();
            
            // Initialize charts
            InitializeCharts();
            
            // Initialize dropdown menus
            InitializeDropdownMenus();
        }

        private void InitializeDropdownMenus()
        {
            // Set smaller font for all dropdowns
            var dropdownFont = new Font("Segoe UI", 9, FontStyle.Regular);

            // Users Dropdown (User Management Module)
            usersDropdown = new ContextMenuStrip();
            usersDropdown.Font = dropdownFont;
            usersDropdown.Items.Add("User Master", null, (s, e) => OpenUserMasterForm());
            usersDropdown.Items.Add("-");

            // Products Dropdown (Product & Inventory Management Module)
            productsDropdown = new ContextMenuStrip();
            productsDropdown.Font = dropdownFont;
            productsDropdown.Items.Add("Product Master", null, (s, e) => OpenProductMasterForm());
            productsDropdown.Items.Add("Categories & Brands", null, (s, e) => OpenCategoriesBrandsForm());
            productsDropdown.Items.Add("Brands Management", null, (s, e) => OpenBrandsForm());
            productsDropdown.Items.Add("Units Management", null, (s, e) => OpenUnitsForm());
            productsDropdown.Items.Add("Barcode Generator", null, (s, e) => OpenBarcodeGenerator());
            productsDropdown.Items.Add("Stock Adjustment Form", null, (s, e) => OpenStockAdjustmentForm());
            productsDropdown.Items.Add("Stock Movement Entry", null, (s, e) => OpenStockMovementEntryForm());
            productsDropdown.Items.Add("Reorder Level Setup", null, (s, e) => OpenReorderLevelSetupForm());
            productsDropdown.Items.Add("-");
            productsDropdown.Items.Add("Pricing & Discount Setup", null, (s, e) => OpenPricingDiscountSetupForm());
            productsDropdown.Items.Add("-");


            // Inventory Dropdown (Additional Inventory Features)
            inventoryDropdown = new ContextMenuStrip();
            inventoryDropdown.Font = dropdownFont;
            inventoryDropdown.Items.Add("Warehouse Management", null, (s, e) => OpenWarehouseForm());

            // Sales Dropdown (Sales & Distribution Module)
            salesDropdown = new ContextMenuStrip();
            salesDropdown.Font = dropdownFont;
            salesDropdown.Items.Add("Sales Invoice", null, (s, e) => OpenSalesInvoiceForm());
            salesDropdown.Items.Add("Sales Return", null, (s, e) => OpenSalesReturnForm());
            salesDropdown.Items.Add("Delivery Challan", null, (s, e) => OpenDeliveryChallanForm());
            salesDropdown.Items.Add("Delivery Planning & Dispatch", null, (s, e) => OpenDeliveryPlanningAndDispatchForm());
            salesDropdown.Items.Add("Delivery Confirmation", null, (s, e) => OpenDeliveryConfirmationForm());
            salesDropdown.Items.Add("-");
            salesDropdown.Items.Add("Salesman Target & Achievement", null, (s, e) => OpenSalesmanTargetForm());
            salesDropdown.Items.Add("-");
            salesDropdown.Items.Add("Vehicle Master", null, (s, e) => OpenVehicleMasterForm());
            salesDropdown.Items.Add("Route Master", null, (s, e) => OpenRouteMasterForm());

            // Purchases Dropdown (Purchase Module)
            purchasesDropdown = new ContextMenuStrip();
            purchasesDropdown.Font = dropdownFont;
            purchasesDropdown.Items.Add("Purchase Invoice Entry", null, (s, e) => OpenPurchaseInvoiceForm());
            purchasesDropdown.Items.Add("GRN (Goods Receipt Note)", null, (s, e) => OpenGRNForm());
            purchasesDropdown.Items.Add("Purchase Return", null, (s, e) => OpenPurchaseReturnForm());
            purchasesDropdown.Items.Add("Purchase Ledger", null, (s, e) => OpenPurchaseLedgerForm());

            // Customers Dropdown (Customer Management Module)
            customersDropdown = new ContextMenuStrip();
            customersDropdown.Font = dropdownFont;
            customersDropdown.Items.Add("Customer Master", null, (s, e) => OpenCustomerMasterForm());
            customersDropdown.Items.Add("Customer Category", null, (s, e) => OpenCustomerCategoryForm());
            customersDropdown.Items.Add("Customer Ledger", null, (s, e) => OpenCustomerLedgerForm());
            customersDropdown.Items.Add("Customer Receipts", null, (s, e) => OpenCustomerReceiptsForm());
            customersDropdown.Items.Add("-");
            customersDropdown.Items.Add("Customer Report", null, (s, e) => OpenCustomerReportForm());

            // Suppliers Dropdown (Supplier Management Module)
            suppliersDropdown = new ContextMenuStrip();
            suppliersDropdown.Font = dropdownFont;
            suppliersDropdown.Items.Add("Supplier Master", null, (s, e) => OpenSupplierMasterForm());
            suppliersDropdown.Items.Add("Supplier Ledger", null, (s, e) => OpenSupplierLedgerForm());
            suppliersDropdown.Items.Add("Supplier Payment Form", null, (s, e) => OpenSupplierPaymentForm());
            suppliersDropdown.Items.Add("Supplier Debit Note", null, (s, e) => OpenSupplierDebitNoteForm());
            suppliersDropdown.Items.Add("-");
            suppliersDropdown.Items.Add("Supplier Payment History Report", null, (s, e) => OpenSupplierPaymentHistoryReportForm());

            // Reports Dropdown (Reports Dashboard & Additional Reports)
            reportsDropdown = new ContextMenuStrip();
            reportsDropdown.Font = dropdownFont;
            // Set maximum size for scrolling (ContextMenuStrip handles scrolling automatically)
            reportsDropdown.MaximumSize = new Size(300, 400);
            reportsDropdown.Items.Add("Stock Report", null, (s, e) => OpenStockReportForm());
            reportsDropdown.Items.Add("Low Stock Report", null, (s, e) => OpenLowStockReportForm());
            reportsDropdown.Items.Add("Stock Movement Report", null, (s, e) => OpenStockMovementReportForm());
            reportsDropdown.Items.Add("Batch Expiry Report", null, (s, e) => OpenBatchExpiryReportForm());
            reportsDropdown.Items.Add("Item Ledger Report", null, (s, e) => OpenItemLedgerReportForm());
            reportsDropdown.Items.Add("-");
            reportsDropdown.Items.Add("User Activity Report", null, (s, e) => OpenUserActivityReportForm());
            reportsDropdown.Items.Add("Login History Report", null, (s, e) => OpenLoginHistoryReportForm());
            reportsDropdown.Items.Add("-");
            reportsDropdown.Items.Add("Supplier Ledger Report", null, (s, e) => OpenSupplierLedgerReportForm());
            reportsDropdown.Items.Add("Supplier Balance Report", null, (s, e) => OpenSupplierBalanceReportForm());
            reportsDropdown.Items.Add("Supplier Payment History Report", null, (s, e) => OpenSupplierPaymentHistoryReportForm());
            reportsDropdown.Items.Add("-");
            reportsDropdown.Items.Add("Customer Ledger Report", null, (s, e) => OpenCustomerLedgerReportForm());
            reportsDropdown.Items.Add("Customer Balance Report", null, (s, e) => OpenCustomerBalanceReportForm());
            reportsDropdown.Items.Add("Customer Receipts Report", null, (s, e) => OpenCustomerReceiptsReportForm());
            reportsDropdown.Items.Add("Customer Aging Report", null, (s, e) => OpenAgingReportForm());
            reportsDropdown.Items.Add("Sales Register Report", null, (s, e) => OpenSalesRegisterReportForm());
            reportsDropdown.Items.Add("Sales Return Report", null, (s, e) => OpenSalesReturnReportForm());
            reportsDropdown.Items.Add("Sales Summary Report", null, (s, e) => OpenSalesSummaryReportForm());
            reportsDropdown.Items.Add("-");


            // Expense Dropdown (Expense Management Module)
            expenseDropdown = new ContextMenuStrip();
            expenseDropdown.Font = dropdownFont;
            expenseDropdown.Items.Add("Expense Category Master", null, (s, e) => OpenExpenseCategoryMasterForm());
            expenseDropdown.Items.Add("Expense Entry", null, (s, e) => OpenExpenseEntryForm());
            expenseDropdown.Items.Add("-");
            expenseDropdown.Items.Add("Expense Reports", null, (s, e) => OpenExpenseReports());

            // Accounting Dropdown (Accounting & Finance Module)
            accountingDropdown = new ContextMenuStrip();
            accountingDropdown.Font = dropdownFont;
            accountingDropdown.Items.Add("Chart of Accounts", null, (s, e) => OpenChartOfAccountsForm());
            accountingDropdown.Items.Add("-");
            accountingDropdown.Items.Add("Journal Entries", null, (s, e) => OpenJournalEntriesForm());
            accountingDropdown.Items.Add("General Ledger", null, (s, e) => OpenGeneralLedgerForm());
            accountingDropdown.Items.Add("Trial Balance", null, (s, e) => OpenTrialBalanceForm());
            accountingDropdown.Items.Add("-");
            accountingDropdown.Items.Add("Tax Configuration", null, (s, e) => OpenTaxConfigurationForm());
            accountingDropdown.Items.Add("Bank Reconciliation", null, (s, e) => OpenBankReconciliationForm());
            accountingDropdown.Items.Add("-");
            accountingDropdown.Items.Add("Profit & Loss Statement", null, (s, e) => OpenProfitLossForm());
            accountingDropdown.Items.Add("Balance Sheet", null, (s, e) => OpenBalanceSheetForm());

            // Settings Dropdown (Master Data & Configuration Module)
            settingsDropdown = new ContextMenuStrip();
            settingsDropdown.Font = dropdownFont;
            settingsDropdown.Items.Add("Master Data Management", null, (s, e) => OpenMasterDataManagement());
            settingsDropdown.Items.Add("-");
            settingsDropdown.Items.Add("Customer Category Master", null, (s, e) => OpenCustomerCategoryForm());
            settingsDropdown.Items.Add("Vehicle Master", null, (s, e) => OpenVehicleMasterForm());
            settingsDropdown.Items.Add("Route Master", null, (s, e) => OpenRouteMasterForm());
            settingsDropdown.Items.Add("-");
            settingsDropdown.Items.Add("User Management", null, (s, e) => OpenUserMasterForm());
            settingsDropdown.Items.Add("User Access Control", null, (s, e) => OpenUserAccessControlForm());
            settingsDropdown.Items.Add("System Configuration", null, (s, e) => OpenSystemConfiguration());
            settingsDropdown.Items.Add("-");
            settingsDropdown.Items.Add("Database Backup & Restore", null, (s, e) => OpenDatabaseBackupRestoreForm());
            settingsDropdown.Items.Add("System Logs", null, (s, e) => OpenSystemLogs());
        }

        private void InitializeCharts()
        {
            // Initialize LiveChartsCore controls
            salesChart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            salesChartPanel.Controls.Add(salesChart);
            
            inventoryChart = new PieChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            inventoryChartPanel.Controls.Add(inventoryChart);
            
            revenueChart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            revenueChartPanel.Controls.Add(revenueChart);
            
            // Initialize new charts
            customerChart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            customerChartPanel.Controls.Add(customerChart);
            
            topProductsChart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            topProductsChartPanel.Controls.Add(topProductsChart);
            
            purchaseVsSalesChart = new CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            purchaseVsSalesChartPanel.Controls.Add(purchaseVsSalesChart);
            
            // Load data into charts
            LoadSalesChart();
            LoadInventoryChart();
            LoadRevenueChart();
            LoadCustomerChart();
            LoadTopProductsChart();
            LoadPurchaseVsSalesChart();
            
            // Add refresh button
            AddRefreshButton();
        }

        private void AddRefreshButton()
        {
            var refreshBtn = new Button
            {
                Text = "🔄 Refresh Data",
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Size = new Size(120, 35),
                Location = new Point(this.ClientSize.Width - 140, 20),
                Cursor = Cursors.Hand
            };
            
            refreshBtn.FlatAppearance.BorderSize = 0;
            refreshBtn.Click += RefreshBtn_Click;
            
            this.Controls.Add(refreshBtn);
            refreshBtn.BringToFront();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Show loading message
                var refreshBtn = sender as Button;
                refreshBtn.Text = "⏳ Loading...";
                refreshBtn.Enabled = false;
                
                // Refresh all charts
                LoadSalesChart();
                LoadInventoryChart();
                LoadRevenueChart();
                LoadCustomerChart();
                LoadTopProductsChart();
                LoadPurchaseVsSalesChart();
                
                // Reset button
                refreshBtn.Text = "🔄 Refresh Data";
                refreshBtn.Enabled = true;
                
                MessageBox.Show("Dashboard data refreshed successfully!", "Refresh Complete", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                var refreshBtn = sender as Button;
                refreshBtn.Text = "🔄 Refresh Data";
                refreshBtn.Enabled = true;
                
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Refresh Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddChartPlaceholder(Panel panel, string title, string message)
        {
            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(10, 10)
            };
            
            var messageLabel = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(127, 140, 141),
                AutoSize = true,
                Location = new Point(10, 40)
            };
            
            panel.Controls.Add(titleLabel);
            panel.Controls.Add(messageLabel);
        }

        private void LoadSalesChart()
        {
            var salesData = GetSalesDataFromDB();
            
            var series = new LineSeries<double>
            {
                Name = "Sales Revenue",
                Values = salesData.Select(x => (double)x.Revenue).ToArray(),
                Stroke = new SolidColorPaint(SKColors.DodgerBlue) { StrokeThickness = 3 },
                Fill = null,
                GeometrySize = 12
            };

            salesChart.Series = new ISeries[] { series };
            
            // Configure axes
            salesChart.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = salesData.Select(x => x.Date.ToString("MM/dd")).ToArray(),
                    Name = "Date"
                }
            };
            
            salesChart.YAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => $"{value:N0}",
                    Name = "Revenue"
                }
            };
        }

        private void LoadInventoryChart()
        {
            var inventoryData = GetInventoryDataFromDB();
            
            if (inventoryData.Count == 0)
            {
                AddChartPlaceholder(inventoryChartPanel, "Inventory Distribution", "No inventory data available");
                return;
            }
            
            var series = new PieSeries<double>
            {
                Name = "Inventory Distribution",
                Values = inventoryData.Select(x => (double)x.StockQuantity).ToArray(),
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue:N0}",
                Fill = new SolidColorPaint(SKColors.SteelBlue)
            };

            inventoryChart.Series = new ISeries[] { series };
        }

        private void LoadRevenueChart()
        {
            var revenueData = GetRevenueDataFromDB();
            
            var series = new ColumnSeries<double>
            {
                Name = "Monthly Revenue",
                Values = revenueData.Select(x => (double)x.Revenue).ToArray(),
                Fill = new SolidColorPaint(SKColors.Purple),
                Stroke = new SolidColorPaint(SKColors.DarkViolet) { StrokeThickness = 2 }
            };

            revenueChart.Series = new ISeries[] { series };
            
            // Configure axes
            revenueChart.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = revenueData.Select(x => x.Month).ToArray(),
                    Name = "Month"
                }
            };
            
            revenueChart.YAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => $"{value:N0}",
                    Name = "Revenue"
                }
            };
        }

        private void LoadCustomerChart()
        {
            var salesReturnsData = GetSalesReturnsDataFromDB();
            
            if (salesReturnsData.Count == 0)
            {
                AddChartPlaceholder(customerChartPanel, "Sales Returns", "No returns data available");
                return;
            }
            
            var series = new ColumnSeries<double>
            {
                Name = "Sales Returns",
                Values = salesReturnsData.Select(x => (double)x.ReturnAmount).ToArray(),
                Fill = new SolidColorPaint(SKColors.Red),
                Stroke = new SolidColorPaint(SKColors.DarkRed) { StrokeThickness = 2 }
            };

            customerChart.Series = new ISeries[] { series };
            
            // Configure axes
            customerChart.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = salesReturnsData.Select(x => x.Date.ToString("MM/dd")).ToArray(),
                    Name = "Date"
                }
            };
            
            customerChart.YAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => $"{value:N0}",
                    Name = "Return Amount"
                }
            };
        }

        private void LoadTopProductsChart()
        {
            var lowStockData = GetLowStockDataFromDB();
            
            if (lowStockData.Count == 0)
            {
                AddChartPlaceholder(topProductsChartPanel, "Stock Levels", "No stock data available");
                return;
            }
            
            var series = new ColumnSeries<double>
            {
                Name = "Current Stock Levels",
                Values = lowStockData.Select(x => (double)x.CurrentStock).ToArray(),
                Fill = new SolidColorPaint(SKColors.Orange),
                Stroke = new SolidColorPaint(SKColors.DarkOrange) { StrokeThickness = 2 }
            };

            topProductsChart.Series = new ISeries[] { series };
            
            // Configure axes
            topProductsChart.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = lowStockData.Select(x => x.ProductName.Length > 12 ? x.ProductName.Substring(0, 12) + "..." : x.ProductName).ToArray(),
                    Name = "Product"
                }
            };
            
            topProductsChart.YAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => $"{value:N0}",
                    Name = "Stock Quantity"
                }
            };
        }

        private void LoadPurchaseVsSalesChart()
        {
            var trendingData = GetTrendingProductsDataFromDB();
            
            if (trendingData.Count == 0)
            {
                AddChartPlaceholder(purchaseVsSalesChartPanel, "Trending Products", "No trending data available");
                return;
            }
            
            var series = new LineSeries<double>
            {
                Name = "Trending Products",
                Values = trendingData.Select(x => (double)x.SalesCount).ToArray(),
                Stroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 3 },
                Fill = new SolidColorPaint(SKColors.LightGreen),
                GeometrySize = 8
            };

            purchaseVsSalesChart.Series = new ISeries[] { series };
            
            // Configure axes
            purchaseVsSalesChart.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = trendingData.Select(x => x.ProductName.Length > 10 ? x.ProductName.Substring(0, 10) + "..." : x.ProductName).ToArray(),
                    Name = "Product"
                }
            };
            
            purchaseVsSalesChart.YAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => $"{value:N0}",
                    Name = "Sales Count"
                }
            };
        }

        // Database data methods
        private List<SalesReturnsData> GetSalesReturnsDataFromDB()
        {
            var data = new List<SalesReturnsData>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT TOP 7 
                            CAST(sr.ReturnDate AS DATE) as Date,
                            SUM(sr.TotalAmount) as ReturnAmount
                        FROM SalesReturns sr
                        WHERE sr.ReturnDate >= DATEADD(day, -7, GETDATE())
                        GROUP BY CAST(sr.ReturnDate AS DATE)
                        ORDER BY Date DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new SalesReturnsData
                            {
                                Date = reader.IsDBNull(0) ? DateTime.Now : reader.GetDateTime(0),
                                ReturnAmount = decimal.TryParse(reader["ReturnAmount"].ToString(), out decimal amount) ? amount : 0m
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading sales returns data: {ex.Message}");
                // Fallback to mock data
                data = MockDataGenerator.GetSalesReturnsData();
            }
            
            return data;
        }

        private List<LowStockData> GetLowStockDataFromDB()
        {
            var data = new List<LowStockData>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT TOP 6
                            p.ProductName,
                            s.Quantity as CurrentStock,
                            p.ReorderLevel
                        FROM Stock s
                        INNER JOIN Products p ON s.ProductId = p.ProductId
                        ORDER BY s.Quantity ASC";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new LowStockData
                            {
                                ProductName = reader["ProductName"].ToString(),
                                CurrentStock = int.TryParse(reader["CurrentStock"].ToString(), out int stock) ? stock : 0,
                                ReorderLevel = int.TryParse(reader["ReorderLevel"].ToString(), out int reorder) ? reorder : 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading low stock data: {ex.Message}");
                // Fallback to mock data
                data = MockDataGenerator.GetLowStockData();
            }
            
            return data;
        }

        private List<TrendingProductsData> GetTrendingProductsDataFromDB()
        {
            var data = new List<TrendingProductsData>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT TOP 6
                            p.ProductName,
                            COUNT(sid.SalesInvoiceDetailId) as SalesCount
                        FROM SalesInvoiceDetails sid
                        INNER JOIN SalesInvoices si ON sid.SalesInvoiceId = si.SalesInvoiceId
                        INNER JOIN Products p ON sid.ProductId = p.ProductId
                        WHERE si.InvoiceDate >= DATEADD(day, -30, GETDATE())
                        GROUP BY p.ProductName
                        ORDER BY SalesCount DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new TrendingProductsData
                            {
                                ProductName = reader["ProductName"].ToString(),
                                SalesCount = int.TryParse(reader["SalesCount"].ToString(), out int count) ? count : 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading trending products data: {ex.Message}");
                // Fallback to mock data
                data = MockDataGenerator.GetTrendingProductsData();
            }
            
            return data;
        }

        private List<SalesData> GetSalesDataFromDB()
        {
            var data = new List<SalesData>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT TOP 7 
                            CAST(si.InvoiceDate AS DATE) as Date,
                            SUM(si.TotalAmount) as Revenue
                        FROM SalesInvoices si
                        WHERE si.InvoiceDate >= DATEADD(day, -7, GETDATE())
                        GROUP BY CAST(si.InvoiceDate AS DATE)
                        ORDER BY Date DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new SalesData
                            {
                                Date = reader.IsDBNull(0) ? DateTime.Now : reader.GetDateTime(0),
                                Revenue = decimal.TryParse(reader["Revenue"].ToString(), out decimal revenue) ? revenue : 0m
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Fallback to mock data if database error
                System.Diagnostics.Debug.WriteLine($"Error loading sales data: {ex.Message}");
                data = GetSalesData();
            }
            
            return data;
        }

        private List<InventoryData> GetInventoryDataFromDB()
        {
            var data = new List<InventoryData>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT TOP 5
                            c.CategoryName,
                            SUM(s.Quantity) as StockQuantity
                        FROM Stock s
                        INNER JOIN Products p ON s.ProductId = p.ProductId
                        INNER JOIN Categories c ON p.CategoryId = c.CategoryId
                        GROUP BY c.CategoryName
                        ORDER BY StockQuantity DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new InventoryData
                            {
                                CategoryName = reader.GetString(0),
                                StockQuantity = int.TryParse(reader["StockQuantity"].ToString(), out int quantity) ? quantity : 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Fallback to mock data if database error
                System.Diagnostics.Debug.WriteLine($"Error loading inventory data: {ex.Message}");
                data = GetInventoryData();
            }
            
            return data;
        }

        private List<RevenueData> GetRevenueDataFromDB()
        {
            var data = new List<RevenueData>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT TOP 6
                            FORMAT(si.InvoiceDate, 'MMM') as Month,
                            SUM(si.TotalAmount) as Revenue
                        FROM SalesInvoices si
                        WHERE si.InvoiceDate >= DATEADD(month, -6, GETDATE())
                        GROUP BY FORMAT(si.InvoiceDate, 'MMM'), MONTH(si.InvoiceDate)
                        ORDER BY MONTH(si.InvoiceDate)";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new RevenueData
                            {
                                Month = reader.GetString(0),
                                Revenue = decimal.TryParse(reader["Revenue"].ToString(), out decimal revenue) ? revenue : 0m
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Fallback to mock data if database error
                System.Diagnostics.Debug.WriteLine($"Error loading revenue data: {ex.Message}");
                data = GetRevenueData();
            }
            
            return data;
        }

        private List<CustomerData> GetCustomerDataFromDB()
        {
            var data = new List<CustomerData>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT 
                            ISNULL(cc.CategoryName, 'Uncategorized') as CategoryName,
                            COUNT(c.CustomerId) as CustomerCount
                        FROM Customers c
                        LEFT JOIN CustomerCategories cc ON c.CategoryId = cc.CategoryId
                        WHERE c.IsActive = 1
                        GROUP BY cc.CategoryName
                        ORDER BY CustomerCount DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new CustomerData
                            {
                                CategoryName = reader.GetString(0),
                                CustomerCount = int.TryParse(reader["CustomerCount"].ToString(), out int count) ? count : 0
                            });
                        }
                    }
                }
            }
            catch
            {
                // Fallback to mock data if database error
                data = new List<CustomerData>
                {
                    new CustomerData { CategoryName = "Regular", CustomerCount = 45 },
                    new CustomerData { CategoryName = "Premium", CustomerCount = 28 },
                    new CustomerData { CategoryName = "Wholesale", CustomerCount = 15 },
                    new CustomerData { CategoryName = "VIP", CustomerCount = 8 }
                };
            }
            
            return data;
        }

        private List<ProductData> GetTopProductsDataFromDB()
        {
            var data = new List<ProductData>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT TOP 6
                            p.ProductName,
                            SUM(sid.Quantity) as SalesQuantity
                        FROM SalesInvoiceDetails sid
                        INNER JOIN SalesInvoices si ON sid.SalesInvoiceId = si.SalesInvoiceId
                        INNER JOIN Products p ON sid.ProductId = p.ProductId
                        WHERE si.InvoiceDate >= DATEADD(month, -1, GETDATE())
                        GROUP BY p.ProductName
                        ORDER BY SalesQuantity DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new ProductData
                            {
                                ProductName = reader.GetString(0),
                                SalesQuantity = int.TryParse(reader["SalesQuantity"].ToString(), out int salesQty) ? salesQty : 0
                            });
                        }
                    }
                }
            }
            catch
            {
                // Fallback to mock data if database error
                data = new List<ProductData>
                {
                    new ProductData { ProductName = "Laptop", SalesQuantity = 120 },
                    new ProductData { ProductName = "Smartphone", SalesQuantity = 95 },
                    new ProductData { ProductName = "Tablet", SalesQuantity = 78 },
                    new ProductData { ProductName = "Headphones", SalesQuantity = 65 },
                    new ProductData { ProductName = "Mouse", SalesQuantity = 52 },
                    new ProductData { ProductName = "Keyboard", SalesQuantity = 45 }
                };
            }
            
            return data;
        }

        private List<ComparisonData> GetPurchaseVsSalesDataFromDB()
        {
            var data = new List<ComparisonData>();
            
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT 
                            FORMAT(InvoiceDate, 'MMM') as Month,
                            SUM(CASE WHEN InvoiceDate IS NOT NULL THEN TotalAmount ELSE 0 END) as SalesAmount,
                            SUM(CASE WHEN InvoiceDate IS NOT NULL THEN TotalAmount ELSE 0 END) as PurchaseAmount
                        FROM (
                            SELECT InvoiceDate, TotalAmount FROM SalesInvoices 
                            WHERE InvoiceDate >= DATEADD(month, -6, GETDATE())
                            UNION ALL
                            SELECT InvoiceDate, TotalAmount FROM PurchaseInvoices 
                            WHERE InvoiceDate >= DATEADD(month, -6, GETDATE())
                        ) Combined
                        GROUP BY FORMAT(InvoiceDate, 'MMM'), MONTH(InvoiceDate)
                        ORDER BY MONTH(InvoiceDate)";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new ComparisonData
                            {
                                Month = reader.GetString(0),
                                SalesAmount = decimal.TryParse(reader["SalesAmount"].ToString(), out decimal sales) ? sales : 0m,
                                PurchaseAmount = decimal.TryParse(reader["PurchaseAmount"].ToString(), out decimal purchase) ? purchase : 0m
                            });
                        }
                    }
                }
            }
            catch
            {
                // Fallback to mock data if database error
                data = new List<ComparisonData>
                {
                    new ComparisonData { Month = "Jan", SalesAmount = 45000, PurchaseAmount = 38000 },
                    new ComparisonData { Month = "Feb", SalesAmount = 52000, PurchaseAmount = 42000 },
                    new ComparisonData { Month = "Mar", SalesAmount = 48000, PurchaseAmount = 41000 },
                    new ComparisonData { Month = "Apr", SalesAmount = 61000, PurchaseAmount = 52000 },
                    new ComparisonData { Month = "May", SalesAmount = 55000, PurchaseAmount = 48000 },
                    new ComparisonData { Month = "Jun", SalesAmount = 67000, PurchaseAmount = 59000 }
                };
            }
            
            return data;
        }

        // Fallback mock data methods (kept for compatibility)
        private List<SalesData> GetSalesData()
        {
            var data = new List<SalesData>();
            var random = new Random();
            
            for (int i = 0; i < 7; i++)
            {
                data.Add(new SalesData
                {
                    Date = DateTime.Now.AddDays(-6 + i),
                    Revenue = (decimal)random.Next(5000, 25000)
                });
            }
            
            return data;
        }

        private List<InventoryData> GetInventoryData()
        {
            return new List<InventoryData>
            {
                new InventoryData { CategoryName = "Electronics", StockQuantity = 150 },
                new InventoryData { CategoryName = "Clothing", StockQuantity = 300 },
                new InventoryData { CategoryName = "Books", StockQuantity = 200 },
                new InventoryData { CategoryName = "Home & Garden", StockQuantity = 100 },
                new InventoryData { CategoryName = "Sports", StockQuantity = 80 }
            };
        }

        private List<RevenueData> GetRevenueData()
        {
            return new List<RevenueData>
            {
                new RevenueData { Month = "Jan", Revenue = 45000m },
                new RevenueData { Month = "Feb", Revenue = 52000m },
                new RevenueData { Month = "Mar", Revenue = 48000m },
                new RevenueData { Month = "Apr", Revenue = 61000m },
                new RevenueData { Month = "May", Revenue = 55000m },
                new RevenueData { Month = "Jun", Revenue = 67000m }
            };
        }

        private string GetDynamicGreeting()
        {
            var hour = DateTime.Now.Hour;
            if (hour < 12) return "Good morning!";
            if (hour < 17) return "Good afternoon!";
            return "Good evening!";
        }

        // Event handlers
        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            greetingLabel.Text = GetDynamicGreeting();
        }

        private void DashboardBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dashboard clicked!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UsersBtn_Click(object sender, EventArgs e)
        {
            usersDropdown.Show(usersBtn, new Point(0, usersBtn.Height));
        }

        private void ProductsBtn_Click(object sender, EventArgs e)
        {
            productsDropdown.Show(productsBtn, new Point(0, productsBtn.Height));
        }

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            inventoryDropdown.Show(inventoryBtn, new Point(0, inventoryBtn.Height));
        }

        private void SalesBtn_Click(object sender, EventArgs e)
        {
            salesDropdown.Show(salesBtn, new Point(0, salesBtn.Height));
        }

        private void PurchasesBtn_Click(object sender, EventArgs e)
        {
            purchasesDropdown.Show(purchasesBtn, new Point(0, purchasesBtn.Height));
        }

        private void CustomersBtn_Click(object sender, EventArgs e)
        {
            customersDropdown.Show(customersBtn, new Point(0, customersBtn.Height));
        }

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            suppliersDropdown.Show(suppliersBtn, new Point(0, suppliersBtn.Height));
        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            reportsDropdown.Show(reportsBtn, new Point(0, reportsBtn.Height));
        }

        private void ExpenseBtn_Click(object sender, EventArgs e)
        {
            expenseDropdown.Show(expenseBtn, new Point(0, expenseBtn.Height));
        }

        private void AccountingBtn_Click(object sender, EventArgs e)
        {
            accountingDropdown.Show(accountingBtn, new Point(0, accountingBtn.Height));
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            settingsDropdown.Show(settingsBtn, new Point(0, settingsBtn.Height));
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #region Form Opening Methods


        // Product & Inventory Management Module
        private void OpenProductMasterForm() 
        { 
            var form = new ProductMasterForm();
            form.ShowDialog();
        }
        private void OpenCategoriesBrandsForm() 
        { 
            var form = new CategoriesBrandsForm();
            form.ShowDialog();
        }
        private void OpenUnitsForm() 
        { 
            UnitsForm unitsForm = new UnitsForm();
            unitsForm.Show();
        }

        private void OpenBrandsForm() 
        { 
            BrandsForm brandsForm = new BrandsForm();
            brandsForm.Show();
        }

        private void OpenWarehouseForm() 
        { 
            WarehouseForm warehouseForm = new WarehouseForm();
            warehouseForm.Show();
        }
        private void OpenBarcodeGenerator() 
        { 
            var form = new BarcodeGeneratorForm();
            form.ShowDialog();
        }
        private void OpenStockAdjustmentForm() 
        { 
            StockAdjustmentForm stockAdjustmentForm = new StockAdjustmentForm();
            stockAdjustmentForm.Show();
        }
        


        private void OpenSalesInvoiceForm()
        {
            try
            {
                SalesInvoiceForm salesInvoiceForm = new SalesInvoiceForm();
                salesInvoiceForm.Owner = this;
                salesInvoiceForm.ShowDialog();
                salesInvoiceForm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Sales Invoice Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenSalesReturnForm()
        {
            try
            {
                SalesReturnForm salesReturnForm = new SalesReturnForm();
                salesReturnForm.Owner = this;
                salesReturnForm.ShowDialog();
                salesReturnForm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Sales Return Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDeliveryChallanForm()
        {
            try
            {
                DeliveryChallanForm deliveryChallanForm = new DeliveryChallanForm();
                deliveryChallanForm.Owner = this;
                deliveryChallanForm.ShowDialog();
                deliveryChallanForm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Delivery Challan Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDeliveryPlanningAndDispatchForm()
        {
            try
            {
                DeliveryPlanningAndDispatchForm deliveryPlanningForm = new DeliveryPlanningAndDispatchForm();
                deliveryPlanningForm.Owner = this;
                deliveryPlanningForm.ShowDialog();
                deliveryPlanningForm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Delivery Planning & Dispatch Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDeliveryConfirmationForm()
        {
            try
            {
                DeliveryConfirmationForm deliveryConfirmationForm = new DeliveryConfirmationForm();
                deliveryConfirmationForm.Owner = this;
                deliveryConfirmationForm.ShowDialog();
                deliveryConfirmationForm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Delivery Confirmation Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OpenStockMovementEntryForm()
        {
            try
            {
                StockMovementEntryForm stockMovementEntryForm = new StockMovementEntryForm();
                stockMovementEntryForm.Owner = this;
                stockMovementEntryForm.ShowDialog();
                stockMovementEntryForm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Stock Movement Entry Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenReorderLevelSetupForm()
        {
            try
            {
                ReorderLevelSetupForm reorderLevelSetupForm = new ReorderLevelSetupForm();
                reorderLevelSetupForm.Owner = this;
                reorderLevelSetupForm.ShowDialog();
                reorderLevelSetupForm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Reorder Level Setup Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        // Supplier Management Module
        private void OpenSupplierMasterForm() 
        { 
            try
            {
                SupplierMasterForm supplierMasterForm = new SupplierMasterForm();
                supplierMasterForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Supplier Master Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void OpenGRNForm() 
        { 
            try
            {
                GRNForm grnForm = new GRNForm();
                grnForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening GRN Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void OpenSupplierLedgerForm() 
        { 
            try
            {
                SupplierLedgerForm supplierLedgerForm = new SupplierLedgerForm();
                supplierLedgerForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Supplier Ledger Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void OpenSupplierPaymentForm() 
        { 
            try
            {
                SupplierPaymentForm supplierPaymentForm = new SupplierPaymentForm();
                supplierPaymentForm.StartPosition = FormStartPosition.CenterParent;
                supplierPaymentForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Supplier Payment Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void OpenSupplierDebitNoteForm() 
        { 
            try
            {
                SupplierDebitNoteForm supplierDebitNoteForm = new SupplierDebitNoteForm();
                supplierDebitNoteForm.StartPosition = FormStartPosition.CenterParent;
                supplierDebitNoteForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Supplier Debit Note Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void OpenSalesmanTargetForm() 
        { 
            try
            {
                SalesmanTargetForm salesmanTargetForm = new SalesmanTargetForm();
                salesmanTargetForm.StartPosition = FormStartPosition.CenterScreen;
                salesmanTargetForm.Show();
                salesmanTargetForm.BringToFront();
                salesmanTargetForm.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Salesman Target Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        // Purchase Module
        private void OpenPurchaseInvoiceForm() 
        { 
            try
            {
                PurchaseInvoiceForm purchaseInvoiceForm = new PurchaseInvoiceForm();
                purchaseInvoiceForm.StartPosition = FormStartPosition.CenterParent;
                purchaseInvoiceForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Purchase Invoice Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenPurchaseReturnForm() 
        { 
            try
            {
                PurchaseReturnForm purchaseReturnForm = new PurchaseReturnForm();
                purchaseReturnForm.StartPosition = FormStartPosition.CenterParent;
                purchaseReturnForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Purchase Return Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenPurchaseLedgerForm() 
        { 
            try
            {
                PurchaseLedgerForm purchaseLedgerForm = new PurchaseLedgerForm();
                purchaseLedgerForm.StartPosition = FormStartPosition.CenterParent;
                purchaseLedgerForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Purchase Ledger Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenCustomerMasterForm() 
        { 
            try
            {
                CustomerMasterForm customerMasterForm = new CustomerMasterForm();
                customerMasterForm.StartPosition = FormStartPosition.CenterParent;
                customerMasterForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Customer Master Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenVehicleMasterForm() 
        { 
            try
            {
                VehicleMasterForm vehicleMasterForm = new VehicleMasterForm();
                vehicleMasterForm.StartPosition = FormStartPosition.CenterParent;
                vehicleMasterForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Vehicle Master Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenRouteMasterForm() 
        { 
            try
            {
                RouteMasterForm routeMasterForm = new RouteMasterForm();
                routeMasterForm.StartPosition = FormStartPosition.CenterParent;
                routeMasterForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Route Master Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenUserMasterForm() 
        { 
            try
            {
                UserMasterForm userMasterForm = new UserMasterForm();
                userMasterForm.StartPosition = FormStartPosition.CenterParent;
                userMasterForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening User Master Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OpenCustomerCategoryForm()
        {
            try
            {
                CustomerCategoryForm categoryForm = new CustomerCategoryForm();
                categoryForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Customer Category Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenExpenseCategoryMasterForm()
        {
            try
            {
                ExpenseCategoryMasterForm expenseCategoryForm = new ExpenseCategoryMasterForm();
                expenseCategoryForm.StartPosition = FormStartPosition.CenterParent;
                expenseCategoryForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Expense Category Master Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenExpenseEntryForm()
        {
            try
            {
                // Now try the actual ExpenseEntryForm
                ExpenseEntryForm expenseEntryForm = new ExpenseEntryForm();
                expenseEntryForm.StartPosition = FormStartPosition.CenterParent;
                expenseEntryForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Expense Entry Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenExpenseReports()
        {
            try
            {
                MessageBox.Show("Expense Reports functionality will be implemented soon.", "Expense Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Expense Reports: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenCustomerLedgerForm() 
        { 
            try
            {
                CustomerLedgerForm customerLedgerForm = new CustomerLedgerForm();
                customerLedgerForm.StartPosition = FormStartPosition.CenterParent;
                customerLedgerForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Customer Ledger Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenCustomerReceiptsForm() 
        { 
            try
            {
                CustomerReceiptsForm customerReceiptsForm = new CustomerReceiptsForm();
                customerReceiptsForm.StartPosition = FormStartPosition.CenterParent;
                customerReceiptsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Customer Receipts Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenCustomerReportForm() 
        { 
            try
            {
                // TODO: Implement Customer Report Form
                MessageBox.Show("Customer Report Form is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Customer Report Form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }









        // Accounting Dropdown Methods
        private void OpenChartOfAccountsForm()
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

        private void OpenJournalEntriesForm()
        {
            try
            {
                var journalVoucherForm = new JournalVoucherForm();
                journalVoucherForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Journal Entries: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenGeneralLedgerForm()
        {
            try
            {
                var generalLedgerForm = new GeneralLedgerForm();
                generalLedgerForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening General Ledger: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenTrialBalanceForm()
        {
            try
            {
                var trialBalanceForm = new TrialBalanceForm();
                trialBalanceForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Trial Balance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenProfitLossForm()
        {
            try
            {
                MessageBox.Show("Profit & Loss Statement functionality will be implemented soon.", "Profit & Loss", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Profit & Loss Statement: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenBalanceSheetForm()
        {
            try
            {
                MessageBox.Show("Balance Sheet functionality will be implemented soon.", "Balance Sheet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Balance Sheet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // New Priority Forms Methods
        private void OpenPricingDiscountSetupForm()
        {
            try
            {
                var pricingDiscountForm = new PricingDiscountSetupForm();
                pricingDiscountForm.StartPosition = FormStartPosition.CenterParent;
                pricingDiscountForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Pricing & Discount Setup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenTaxConfigurationForm()
        {
            try
            {
                var taxConfigForm = new TaxConfigurationForm();
                taxConfigForm.StartPosition = FormStartPosition.CenterParent;
                taxConfigForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Tax Configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenBankReconciliationForm()
        {
            try
            {
                var bankReconciliationForm = new BankReconciliationForm();
                bankReconciliationForm.StartPosition = FormStartPosition.CenterParent;
                bankReconciliationForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Bank Reconciliation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDatabaseBackupRestoreForm()
        {
            try
            {
                var databaseBackupForm = new DatabaseBackupRestoreForm();
                databaseBackupForm.StartPosition = FormStartPosition.CenterParent;
                databaseBackupForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Database Backup & Restore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenUserAccessControlForm()
        {
            try
            {
                var userAccessForm = new UserAccessControlForm();
                userAccessForm.StartPosition = FormStartPosition.CenterParent;
                userAccessForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening User Access Control: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Settings Dropdown Methods
        private void OpenMasterDataManagement()
        {
            try
            {
                MessageBox.Show("Master Data Management - Central hub for all master data forms.\n\n" +
                    "Available Forms:\n" +
                    "• Expense Category Master\n" +
                    "• Customer Category Master\n" +
                    "• Vehicle Master\n" +
                    "• Route Master\n" +
                    "• User Management\n\n" +
                    "Use the individual menu items to access specific forms.", 
                    "Master Data Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Master Data Management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenSystemConfiguration()
        {
            try
            {
                MessageBox.Show("System Configuration - Configure system settings, preferences, and parameters.\n\n" +
                    "This feature will include:\n" +
                    "• Company Information\n" +
                    "• System Preferences\n" +
                    "• Email Settings\n" +
                    "• Backup Configuration\n" +
                    "• Security Settings", 
                    "System Configuration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening System Configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDatabaseBackup()
        {
            try
            {
                MessageBox.Show("Database Backup - Create and manage database backups.\n\n" +
                    "This feature will include:\n" +
                    "• Full Database Backup\n" +
                    "• Incremental Backup\n" +
                    "• Backup Scheduling\n" +
                    "• Restore Operations\n" +
                    "• Backup History", 
                    "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Database Backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenSystemLogs()
        {
            try
            {
                MessageBox.Show("System Logs - View and manage system logs and audit trails.\n\n" +
                    "This feature will include:\n" +
                    "• Application Logs\n" +
                    "• Error Logs\n" +
                    "• User Activity Logs\n" +
                    "• System Events\n" +
                    "• Log Filtering and Search", 
                    "System Logs", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening System Logs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void TestDatabaseConnection()
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    System.Diagnostics.Debug.WriteLine("Connection string is null or empty!");
                    MessageBox.Show("Database connection string is not configured properly. Charts will show sample data.", 
                        "Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    System.Diagnostics.Debug.WriteLine("Database connection successful!");
                    
                    // Test if required tables exist
                    TestRequiredTables(connection);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database connection failed: {ex.Message}");
                MessageBox.Show($"Database connection failed: {ex.Message}\n\nCharts will show sample data.", 
                    "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TestRequiredTables(SqlConnection connection)
        {
            try
            {
                var requiredTables = new[] { "SalesInvoices", "SalesInvoiceDetails", "Products", "Categories", "Stock", "Customers", "PurchaseInvoices" };
                var existingTables = new List<string>();

                foreach (var table in requiredTables)
                {
                    var query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{table}'";
                    using (var command = new SqlCommand(query, connection))
                    {
                        var count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            existingTables.Add(table);
                        }
                    }
                }

                System.Diagnostics.Debug.WriteLine($"Existing tables: {string.Join(", ", existingTables)}");
                
                if (existingTables.Count < requiredTables.Length)
                {
                    var missingTables = requiredTables.Except(existingTables).ToArray();
                    System.Diagnostics.Debug.WriteLine($"Missing tables: {string.Join(", ", missingTables)}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error testing tables: {ex.Message}");
            }
        }

        private void SetupResponsiveLayout()
        {
            // Set up responsive layout
            this.Resize += AdminDashboardRedesigned_Resize;
            AdjustLayoutForScreenSize();
        }

        private void AdminDashboardRedesigned_Resize(object sender, EventArgs e)
        {
            AdjustLayoutForScreenSize();
        }

        private void AdjustLayoutForScreenSize()
        {
            try
            {
                int screenWidth = this.ClientSize.Width;
                int screenHeight = this.ClientSize.Height;

                // Calculate optimal spacing and sizes
                int buttonWidth = Math.Max(80, screenWidth / 15); // Responsive button width
                int buttonSpacing = Math.Max(5, (screenWidth - (12 * buttonWidth)) / 13); // Responsive spacing
                int chartWidth = (screenWidth - 20) / 3; // 3 columns with margins
                int chartHeight = Math.Max(200, (screenHeight - 150) / 2); // 2 rows with margins

                // Adjust button positions and sizes
                AdjustButtonLayout(buttonWidth, buttonSpacing);

                // Adjust chart panel positions and sizes
                AdjustChartLayout(chartWidth, chartHeight);

                // Refresh the form
                this.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adjusting layout: {ex.Message}");
            }
        }

        private void AdjustButtonLayout(int buttonWidth, int buttonSpacing)
        {
            int startX = 5;
            int y = 65;

            // Adjust all navigation buttons
            dashboardBtn.Location = new Point(startX, y);
            dashboardBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            usersBtn.Location = new Point(startX, y);
            usersBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            productsBtn.Location = new Point(startX, y);
            productsBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            inventoryBtn.Location = new Point(startX, y);
            inventoryBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            salesBtn.Location = new Point(startX, y);
            salesBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            purchasesBtn.Location = new Point(startX, y);
            purchasesBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            customersBtn.Location = new Point(startX, y);
            customersBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            suppliersBtn.Location = new Point(startX, y);
            suppliersBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            reportsBtn.Location = new Point(startX, y);
            reportsBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            expenseBtn.Location = new Point(startX, y);
            expenseBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            accountingBtn.Location = new Point(startX, y);
            accountingBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            settingsBtn.Location = new Point(startX, y);
            settingsBtn.Size = new Size(buttonWidth, 30);

            startX += buttonWidth + buttonSpacing;
            logoutBtn.Location = new Point(startX, y);
            logoutBtn.Size = new Size(buttonWidth, 30);
        }

        private void AdjustChartLayout(int chartWidth, int chartHeight)
        {
            int startX = 5;
            int startY = 104;
            int spacing = 10;

            // Top row charts
            salesChartPanel.Location = new Point(startX, startY);
            salesChartPanel.Size = new Size(chartWidth, chartHeight);

            startX += chartWidth + spacing;
            inventoryChartPanel.Location = new Point(startX, startY);
            inventoryChartPanel.Size = new Size(chartWidth, chartHeight);

            startX += chartWidth + spacing;
            revenueChartPanel.Location = new Point(startX, startY);
            revenueChartPanel.Size = new Size(chartWidth, chartHeight);

            // Bottom row charts
            startX = 5;
            startY += chartHeight + spacing;
            customerChartPanel.Location = new Point(startX, startY);
            customerChartPanel.Size = new Size(chartWidth, chartHeight);

            startX += chartWidth + spacing;
            topProductsChartPanel.Location = new Point(startX, startY);
            topProductsChartPanel.Size = new Size(chartWidth, chartHeight);

            startX += chartWidth + spacing;
            purchaseVsSalesChartPanel.Location = new Point(startX, startY);
            purchaseVsSalesChartPanel.Size = new Size(chartWidth, chartHeight);
        }

        // Report Form Methods
        private void OpenStockReportForm()
        {
            try
            {
                var stockReportForm = new StockReportForm();
                stockReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Stock Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenLowStockReportForm()
        {
            try
            {
                var lowStockReportForm = new LowStockReportForm();
                lowStockReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Low Stock Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenStockMovementReportForm()
        {
            try
            {
                var stockMovementReportForm = new StockMovementReportForm();
                stockMovementReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Stock Movement Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenBatchExpiryReportForm()
        {
            try
            {
                var batchExpiryReportForm = new BatchExpiryReportForm();
                batchExpiryReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Batch Expiry Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenItemLedgerReportForm()
        {
            try
            {
                var itemLedgerReportForm = new ItemLedgerReportForm();
                itemLedgerReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Item Ledger Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenUserActivityReportForm()
        {
            try
            {
                var userActivityReportForm = new UserActivityReportForm();
                userActivityReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening User Activity Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenLoginHistoryReportForm()
        {
            try
            {
                var loginHistoryReportForm = new LoginHistoryReportForm();
                loginHistoryReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Login History Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenSupplierLedgerReportForm()
        {
            try
            {
                var supplierLedgerReportForm = new SupplierLedgerReportForm();
                supplierLedgerReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Supplier Ledger Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenCustomerLedgerReportForm()
        {
            try
            {
                var customerLedgerReportForm = new CustomerLedgerReportForm();
                customerLedgerReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Customer Ledger Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenCustomerBalanceReportForm()
        {
            try
            {
                var customerBalanceReportForm = new CustomerBalanceReportForm();
                customerBalanceReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Customer Balance Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenCustomerReceiptsReportForm()
        {
            try
            {
                var customerReceiptsReportForm = new CustomerReceiptsReportForm();
                customerReceiptsReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Customer Receipts Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenAgingReportForm()
        {
            try
            {
                var agingReportForm = new AgingReportForm();
                agingReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Customer Aging Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenSalesRegisterReportForm()
        {
            try
            {
                var salesRegisterReportForm = new SalesRegisterReportForm();
                salesRegisterReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Sales Register Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenSalesReturnReportForm()
        {
            try
            {
                var salesReturnReportForm = new SalesReturnReportForm();
                salesReturnReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Sales Return Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenSalesSummaryReportForm()
        {
            try
            {
                var salesSummaryReportForm = new SalesSummaryReportForm();
                salesSummaryReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Sales Summary Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenSupplierBalanceReportForm()
        {
            try
            {
                var supplierBalanceReportForm = new SupplierBalanceReportForm();
                supplierBalanceReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Supplier Balance Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenSupplierPaymentHistoryReportForm()
        {
            try
            {
                var supplierPaymentHistoryReportForm = new SupplierPaymentHistoryReportForm();
                supplierPaymentHistoryReportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Supplier Payment History Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

    // Data classes
    public class SalesData
    {
        public DateTime Date { get; set; }
        public decimal Revenue { get; set; }
    }

    public class InventoryData
    {
        public string CategoryName { get; set; }
        public int StockQuantity { get; set; }
    }

    public class RevenueData
    {
        public string Month { get; set; }
        public decimal Revenue { get; set; }
    }

    public class CustomerData
    {
        public string CategoryName { get; set; }
        public int CustomerCount { get; set; }
    }

    public class ProductData
    {
        public string ProductName { get; set; }
        public int SalesQuantity { get; set; }
    }

    public class ComparisonData
    {
        public string Month { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal PurchaseAmount { get; set; }
    }

    public class SalesReturnsData
    {
        public DateTime Date { get; set; }
        public decimal ReturnAmount { get; set; }
    }

    public class LowStockData
    {
        public string ProductName { get; set; }
        public int CurrentStock { get; set; }
        public int ReorderLevel { get; set; }
    }

    public class TrendingProductsData
    {
        public string ProductName { get; set; }
        public int SalesCount { get; set; }
    }

    // Mock data methods for fallback
    public static class MockDataGenerator
    {
        public static List<SalesReturnsData> GetSalesReturnsData()
        {
            var data = new List<SalesReturnsData>();
            var random = new Random();
            
            for (int i = 0; i < 7; i++)
            {
                data.Add(new SalesReturnsData
                {
                    Date = DateTime.Now.AddDays(-6 + i),
                    ReturnAmount = (decimal)random.Next(500, 3000)
                });
            }
            
            return data;
        }

        public static List<LowStockData> GetLowStockData()
        {
            return new List<LowStockData>
            {
                new LowStockData { ProductName = "Laptop", CurrentStock = 5, ReorderLevel = 10 },
                new LowStockData { ProductName = "Mouse", CurrentStock = 8, ReorderLevel = 15 },
                new LowStockData { ProductName = "Keyboard", CurrentStock = 3, ReorderLevel = 12 },
                new LowStockData { ProductName = "Monitor", CurrentStock = 2, ReorderLevel = 8 },
                new LowStockData { ProductName = "Headphones", CurrentStock = 7, ReorderLevel = 20 },
                new LowStockData { ProductName = "Webcam", CurrentStock = 4, ReorderLevel = 10 }
            };
        }

        public static List<TrendingProductsData> GetTrendingProductsData()
        {
            return new List<TrendingProductsData>
            {
                new TrendingProductsData { ProductName = "Smartphone", SalesCount = 45 },
                new TrendingProductsData { ProductName = "Laptop", SalesCount = 38 },
                new TrendingProductsData { ProductName = "Tablet", SalesCount = 32 },
                new TrendingProductsData { ProductName = "Headphones", SalesCount = 28 },
                new TrendingProductsData { ProductName = "Mouse", SalesCount = 25 },
                new TrendingProductsData { ProductName = "Keyboard", SalesCount = 22 }
            };
        }
    }
}
