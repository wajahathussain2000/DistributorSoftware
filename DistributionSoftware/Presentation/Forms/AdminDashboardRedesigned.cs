using System;
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
        private PieChart customerChart;
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
        private ContextMenuStrip settingsDropdown;

        public AdminDashboardRedesigned()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
            InitializeDashboard();
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
            usersDropdown.Items.Add("User Registration Form", null, (s, e) => OpenUserRegistrationForm());
            usersDropdown.Items.Add("User Login Form", null, (s, e) => OpenUserLoginForm());
            usersDropdown.Items.Add("Role & Permissions Form", null, (s, e) => OpenRolePermissionsForm());
            usersDropdown.Items.Add("Activity Log Viewer", null, (s, e) => OpenActivityLogViewer());
            usersDropdown.Items.Add("-");
            usersDropdown.Items.Add("User Activity Report", null, (s, e) => OpenUserActivityReport());
            usersDropdown.Items.Add("Login History Report", null, (s, e) => OpenLoginHistoryReport());

            // Products Dropdown (Product & Inventory Management Module)
            productsDropdown = new ContextMenuStrip();
            productsDropdown.Font = dropdownFont;
            productsDropdown.Items.Add("Product Master", null, (s, e) => OpenProductMasterForm());
            productsDropdown.Items.Add("Categories & Brands", null, (s, e) => OpenCategoriesBrandsForm());
            productsDropdown.Items.Add("Brands Management", null, (s, e) => OpenBrandsForm());
            productsDropdown.Items.Add("Units Management", null, (s, e) => OpenUnitsForm());
            productsDropdown.Items.Add("Barcode Generator", null, (s, e) => OpenBarcodeGenerator());
            productsDropdown.Items.Add("Stock Adjustment Form", null, (s, e) => OpenStockAdjustmentForm());
            productsDropdown.Items.Add("Reorder Level Setup", null, (s, e) => OpenReorderLevelForm());
            productsDropdown.Items.Add("-");


            // Inventory Dropdown (Additional Inventory Features)
            inventoryDropdown = new ContextMenuStrip();
            inventoryDropdown.Font = dropdownFont;
            inventoryDropdown.Items.Add("Stock Transfer", null, (s, e) => MessageBox.Show("Opening Stock Transfer...", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information));
            inventoryDropdown.Items.Add("Warehouse Management", null, (s, e) => OpenWarehouseForm());
            inventoryDropdown.Items.Add("Inventory Count", null, (s, e) => MessageBox.Show("Opening Inventory Count...", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information));
            inventoryDropdown.Items.Add("-");
            inventoryDropdown.Items.Add("Inventory Valuation Report", null, (s, e) => MessageBox.Show("Opening Inventory Valuation Report...", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information));
            inventoryDropdown.Items.Add("Stock Aging Report", null, (s, e) => MessageBox.Show("Opening Stock Aging Report...", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information));

            // Sales Dropdown (Sales & Distribution Module)
            salesDropdown = new ContextMenuStrip();
            salesDropdown.Font = dropdownFont;
            salesDropdown.Items.Add("Sales Invoice Form", null, (s, e) => OpenSalesInvoiceForm());
            salesDropdown.Items.Add("Sales Return Form", null, (s, e) => OpenSalesReturnForm());
            salesDropdown.Items.Add("Delivery Challan Form", null, (s, e) => OpenDeliveryChallanForm());
            salesDropdown.Items.Add("Salesman Target & Achievement", null, (s, e) => OpenSalesmanTargetForm());
            salesDropdown.Items.Add("Pricing & Discount Setup", null, (s, e) => OpenPricingDiscountForm());
            salesDropdown.Items.Add("-");
            salesDropdown.Items.Add("Sales Register", null, (s, e) => OpenSalesRegister());
            salesDropdown.Items.Add("Sales Return Report", null, (s, e) => OpenSalesReturnReport());
            salesDropdown.Items.Add("Sales Summary Report", null, (s, e) => OpenSalesSummaryReport());
            salesDropdown.Items.Add("Product-wise Sales Report", null, (s, e) => OpenProductWiseSalesReport());
            salesDropdown.Items.Add("Customer-wise Sales Report", null, (s, e) => OpenCustomerWiseSalesReport());
            salesDropdown.Items.Add("Salesman-wise Sales Report", null, (s, e) => OpenSalesmanWiseSalesReport());
            salesDropdown.Items.Add("Invoice-wise Report", null, (s, e) => OpenInvoiceWiseReport());
            salesDropdown.Items.Add("Profit Margin Report", null, (s, e) => OpenProfitMarginReport());

            // Purchases Dropdown (Purchase Module)
            purchasesDropdown = new ContextMenuStrip();
            purchasesDropdown.Font = dropdownFont;
            purchasesDropdown.Items.Add("Purchase Invoice Entry", null, (s, e) => OpenPurchaseInvoiceForm());
            purchasesDropdown.Items.Add("Purchase Return Form", null, (s, e) => OpenPurchaseReturnForm());
            purchasesDropdown.Items.Add("GRN Form", null, (s, e) => OpenGRNForm());
            purchasesDropdown.Items.Add("Supplier Debit Note Form", null, (s, e) => OpenSupplierDebitNoteForm());
            purchasesDropdown.Items.Add("-");
            purchasesDropdown.Items.Add("Purchase Register", null, (s, e) => OpenPurchaseRegister());
            purchasesDropdown.Items.Add("Purchase Return Report", null, (s, e) => OpenPurchaseReturnReport());
            purchasesDropdown.Items.Add("Purchase Summary Report", null, (s, e) => OpenPurchaseSummaryReport());
            purchasesDropdown.Items.Add("Supplier-wise Purchase Report", null, (s, e) => OpenSupplierWisePurchaseReport());

            // Customers Dropdown (Customer Management Module)
            customersDropdown = new ContextMenuStrip();
            customersDropdown.Font = dropdownFont;
            customersDropdown.Items.Add("Customer Master", null, (s, e) => OpenCustomerMasterForm());
            customersDropdown.Items.Add("Customer Ledger", null, (s, e) => OpenCustomerLedgerForm());
            customersDropdown.Items.Add("Customer Receipts Form", null, (s, e) => OpenCustomerReceiptsForm());
            customersDropdown.Items.Add("Customer Categories", null, (s, e) => OpenCustomerCategoriesForm());
            customersDropdown.Items.Add("-");
            customersDropdown.Items.Add("Customer Ledger Report", null, (s, e) => OpenCustomerLedgerReport());
            customersDropdown.Items.Add("Customer Balance Report", null, (s, e) => OpenCustomerBalanceReport());
            customersDropdown.Items.Add("Customer Receipts Report", null, (s, e) => OpenCustomerReceiptsReport());
            customersDropdown.Items.Add("Aging Report", null, (s, e) => OpenAgingReport());

            // Suppliers Dropdown (Supplier Management Module)
            suppliersDropdown = new ContextMenuStrip();
            suppliersDropdown.Font = dropdownFont;
            suppliersDropdown.Items.Add("Supplier Master", null, (s, e) => OpenSupplierMasterForm());
            suppliersDropdown.Items.Add("Supplier Ledger", null, (s, e) => OpenSupplierLedgerForm());
            suppliersDropdown.Items.Add("Supplier Payment Form", null, (s, e) => OpenSupplierPaymentForm());
            suppliersDropdown.Items.Add("-");
            suppliersDropdown.Items.Add("Supplier Ledger Report", null, (s, e) => OpenSupplierLedgerReport());
            suppliersDropdown.Items.Add("Supplier Balance Report", null, (s, e) => OpenSupplierBalanceReport());
            suppliersDropdown.Items.Add("Supplier Payment History", null, (s, e) => OpenSupplierPaymentHistory());

            // Reports Dropdown (Reports Dashboard & Additional Reports)
            reportsDropdown = new ContextMenuStrip();
            reportsDropdown.Font = dropdownFont;
            reportsDropdown.Items.Add("Sales Trend Charts", null, (s, e) => OpenSalesTrendCharts());
            reportsDropdown.Items.Add("Purchase vs Sales Analysis", null, (s, e) => OpenPurchaseVsSalesAnalysis());
            reportsDropdown.Items.Add("Profitability Analysis", null, (s, e) => OpenProfitabilityAnalysis());
            reportsDropdown.Items.Add("Top 10 Analysis", null, (s, e) => OpenTop10Analysis());
            reportsDropdown.Items.Add("Aging Analysis Graphs", null, (s, e) => OpenAgingAnalysisGraphs());
            reportsDropdown.Items.Add("-");
            reportsDropdown.Items.Add("Delivery Schedule Report", null, (s, e) => OpenDeliveryScheduleReport());
            reportsDropdown.Items.Add("Dispatch Report", null, (s, e) => OpenDispatchReport());
            reportsDropdown.Items.Add("Pending Delivery Report", null, (s, e) => OpenPendingDeliveryReport());
            reportsDropdown.Items.Add("Vehicle Utilization Report", null, (s, e) => OpenVehicleUtilizationReport());
            reportsDropdown.Items.Add("-");


            // Settings Dropdown (Security & Backup Module)
            settingsDropdown = new ContextMenuStrip();
            settingsDropdown.Font = dropdownFont;
            settingsDropdown.Items.Add("Database Backup & Restore", null, (s, e) => OpenDatabaseBackupForm());
            settingsDropdown.Items.Add("User Access Control Settings", null, (s, e) => OpenUserAccessControlForm());
            settingsDropdown.Items.Add("Audit Trail Viewer", null, (s, e) => OpenAuditTrailViewer());
            settingsDropdown.Items.Add("-");
            settingsDropdown.Items.Add("Audit Trail Report", null, (s, e) => OpenAuditTrailReport());
            settingsDropdown.Items.Add("Backup History Report", null, (s, e) => OpenBackupHistoryReport());
            settingsDropdown.Items.Add("-");
            settingsDropdown.Items.Add("Chart of Accounts", null, (s, e) => OpenChartOfAccountsForm());
            settingsDropdown.Items.Add("Journal Voucher Form", null, (s, e) => OpenJournalVoucherForm());
            settingsDropdown.Items.Add("Payment Voucher Form", null, (s, e) => OpenPaymentVoucherForm());
            settingsDropdown.Items.Add("Receipt Voucher Form", null, (s, e) => OpenReceiptVoucherForm());
            settingsDropdown.Items.Add("Bank Reconciliation Form", null, (s, e) => OpenBankReconciliationForm());
            settingsDropdown.Items.Add("-");
            settingsDropdown.Items.Add("General Ledger Report", null, (s, e) => OpenGeneralLedgerReport());
            settingsDropdown.Items.Add("Trial Balance", null, (s, e) => OpenTrialBalanceReport());
            settingsDropdown.Items.Add("Profit & Loss Statement", null, (s, e) => OpenProfitLossReport());
            settingsDropdown.Items.Add("Balance Sheet", null, (s, e) => OpenBalanceSheetReport());
            settingsDropdown.Items.Add("Cash Flow Report", null, (s, e) => OpenCashFlowReport());
            settingsDropdown.Items.Add("Bank Reconciliation Report", null, (s, e) => OpenBankReconciliationReport());
            settingsDropdown.Items.Add("-");
            settingsDropdown.Items.Add("Expense Entry Form", null, (s, e) => OpenExpenseEntryForm());
            settingsDropdown.Items.Add("Expense Category Master", null, (s, e) => OpenExpenseCategoriesForm());
            settingsDropdown.Items.Add("Expense Report", null, (s, e) => OpenExpenseReport());
            settingsDropdown.Items.Add("Expense Summary", null, (s, e) => OpenExpenseSummaryReport());
            settingsDropdown.Items.Add("-");
            settingsDropdown.Items.Add("Tax Configuration", null, (s, e) => OpenTaxConfigurationForm());
            settingsDropdown.Items.Add("Tax Return Filing Export", null, (s, e) => OpenTaxReturnExport());
            settingsDropdown.Items.Add("Tax Summary Report", null, (s, e) => OpenTaxSummaryReport());
            settingsDropdown.Items.Add("Tax Invoice Report", null, (s, e) => OpenTaxInvoiceReport());
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
            customerChart = new PieChart
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
                    Labeler = value => $"${value:N0}",
                    Name = "Revenue ($)"
                }
            };
        }

        private void LoadInventoryChart()
        {
            var inventoryData = GetInventoryDataFromDB();
            
            var series = new PieSeries<double>
            {
                Name = "Inventory Distribution",
                Values = inventoryData.Select(x => (double)x.StockQuantity).ToArray(),
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue:N0}"
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
                    Labeler = value => $"${value:N0}",
                    Name = "Revenue ($)"
                }
            };
        }

        private void LoadCustomerChart()
        {
            var customerData = GetCustomerDataFromDB();
            
            var series = new PieSeries<double>
            {
                Name = "Customer Distribution",
                Values = customerData.Select(x => (double)x.CustomerCount).ToArray(),
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue:N0}"
            };

            customerChart.Series = new ISeries[] { series };
        }

        private void LoadTopProductsChart()
        {
            var productData = GetTopProductsDataFromDB();
            
            var series = new ColumnSeries<double>
            {
                Name = "Top Products",
                Values = productData.Select(x => (double)x.SalesQuantity).ToArray(),
                Fill = new SolidColorPaint(SKColors.Orange),
                Stroke = new SolidColorPaint(SKColors.DarkOrange) { StrokeThickness = 2 }
            };

            topProductsChart.Series = new ISeries[] { series };
            
            // Configure axes
            topProductsChart.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = productData.Select(x => x.ProductName.Length > 15 ? x.ProductName.Substring(0, 15) + "..." : x.ProductName).ToArray(),
                    Name = "Product"
                }
            };
            
            topProductsChart.YAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => $"{value:N0}",
                    Name = "Quantity Sold"
                }
            };
        }

        private void LoadPurchaseVsSalesChart()
        {
            var comparisonData = GetPurchaseVsSalesDataFromDB();
            
            var salesSeries = new LineSeries<double>
            {
                Name = "Sales",
                Values = comparisonData.Select(x => (double)x.SalesAmount).ToArray(),
                Stroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 3 },
                Fill = null,
                GeometrySize = 8
            };

            var purchaseSeries = new LineSeries<double>
            {
                Name = "Purchases",
                Values = comparisonData.Select(x => (double)x.PurchaseAmount).ToArray(),
                Stroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 3 },
                Fill = null,
                GeometrySize = 8
            };

            purchaseVsSalesChart.Series = new ISeries[] { salesSeries, purchaseSeries };
            
            // Configure axes
            purchaseVsSalesChart.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = comparisonData.Select(x => x.Month).ToArray(),
                    Name = "Month"
                }
            };
            
            purchaseVsSalesChart.YAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => $"${value:N0}",
                    Name = "Amount ($)"
                }
            };
        }

        // Database data methods
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
            catch
            {
                // Fallback to mock data if database error
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
            catch
            {
                // Fallback to mock data if database error
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
            catch
            {
                // Fallback to mock data if database error
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

        // User Management Module
        private void OpenUserRegistrationForm() { MessageBox.Show("Opening User Registration Form...", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenUserLoginForm() { MessageBox.Show("Opening User Login Form...", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenRolePermissionsForm() { MessageBox.Show("Opening Role & Permissions Form...", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenActivityLogViewer() { MessageBox.Show("Opening Activity Log Viewer...", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenUserActivityReport() { MessageBox.Show("Opening User Activity Report...", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenLoginHistoryReport() { MessageBox.Show("Opening Login History Report...", "User Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }

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
        private void OpenReorderLevelForm() { MessageBox.Show("Opening Reorder Level Form...", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information); }


        // Supplier Management Module
        private void OpenSupplierMasterForm() { MessageBox.Show("Opening Supplier Master Form...", "Supplier Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSupplierLedgerForm() { MessageBox.Show("Opening Supplier Ledger Form...", "Supplier Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSupplierPaymentForm() { MessageBox.Show("Opening Supplier Payment Form...", "Supplier Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSupplierLedgerReport() { MessageBox.Show("Opening Supplier Ledger Report...", "Supplier Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSupplierBalanceReport() { MessageBox.Show("Opening Supplier Balance Report...", "Supplier Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSupplierPaymentHistory() { MessageBox.Show("Opening Supplier Payment History...", "Supplier Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Customer Management Module
        private void OpenCustomerMasterForm() { MessageBox.Show("Opening Customer Master Form...", "Customer Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenCustomerLedgerForm() { MessageBox.Show("Opening Customer Ledger Form...", "Customer Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenCustomerReceiptsForm() { MessageBox.Show("Opening Customer Receipts Form...", "Customer Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenCustomerCategoriesForm() { MessageBox.Show("Opening Customer Categories Form...", "Customer Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenCustomerLedgerReport() { MessageBox.Show("Opening Customer Ledger Report...", "Customer Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenCustomerBalanceReport() { MessageBox.Show("Opening Customer Balance Report...", "Customer Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenCustomerReceiptsReport() { MessageBox.Show("Opening Customer Receipts Report...", "Customer Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenAgingReport() { MessageBox.Show("Opening Aging Report...", "Customer Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Purchase Module
        private void OpenPurchaseInvoiceForm() { MessageBox.Show("Opening Purchase Invoice Entry Form...", "Purchase Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenPurchaseReturnForm() { MessageBox.Show("Opening Purchase Return Form...", "Purchase Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenGRNForm() { MessageBox.Show("Opening GRN (Goods Receipt) Form...", "Purchase Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSupplierDebitNoteForm() { MessageBox.Show("Opening Supplier Debit Note Form...", "Purchase Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenPurchaseRegister() { MessageBox.Show("Opening Purchase Register...", "Purchase Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenPurchaseReturnReport() { MessageBox.Show("Opening Purchase Return Report...", "Purchase Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenPurchaseSummaryReport() { MessageBox.Show("Opening Purchase Summary Report...", "Purchase Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSupplierWisePurchaseReport() { MessageBox.Show("Opening Supplier-wise Purchase Report...", "Purchase Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Sales & Distribution Module
        private void OpenSalesInvoiceForm() { MessageBox.Show("Opening Sales Invoice Form...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSalesReturnForm() { MessageBox.Show("Opening Sales Return Form...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenDeliveryChallanForm() { MessageBox.Show("Opening Delivery Challan Form...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSalesmanTargetForm() { MessageBox.Show("Opening Salesman Target & Achievement Form...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenPricingDiscountForm() { MessageBox.Show("Opening Pricing & Discount Setup Form...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSalesRegister() { MessageBox.Show("Opening Sales Register...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSalesReturnReport() { MessageBox.Show("Opening Sales Return Report...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSalesSummaryReport() { MessageBox.Show("Opening Sales Summary Report...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenProductWiseSalesReport() { MessageBox.Show("Opening Product-wise Sales Report...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenCustomerWiseSalesReport() { MessageBox.Show("Opening Customer-wise Sales Report...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSalesmanWiseSalesReport() { MessageBox.Show("Opening Salesman-wise Sales Report...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenInvoiceWiseReport() { MessageBox.Show("Opening Invoice-wise Report...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenProfitMarginReport() { MessageBox.Show("Opening Profit Margin Report...", "Sales & Distribution", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Delivery & Logistics Module
        private void OpenVehicleMasterForm() { MessageBox.Show("Opening Vehicle Master Form...", "Delivery & Logistics", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenRouteMasterForm() { MessageBox.Show("Opening Route Master Form...", "Delivery & Logistics", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenDeliverySchedulingForm() { MessageBox.Show("Opening Delivery Scheduling Form...", "Delivery & Logistics", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenDispatchForm() { MessageBox.Show("Opening Dispatch Form...", "Delivery & Logistics", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenDeliveryConfirmationForm() { MessageBox.Show("Opening Delivery Confirmation Form...", "Delivery & Logistics", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenDeliveryScheduleReport() { MessageBox.Show("Opening Delivery Schedule Report...", "Delivery & Logistics", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenDispatchReport() { MessageBox.Show("Opening Dispatch Report...", "Delivery & Logistics", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenPendingDeliveryReport() { MessageBox.Show("Opening Pending Delivery Report...", "Delivery & Logistics", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenVehicleUtilizationReport() { MessageBox.Show("Opening Vehicle Utilization Report...", "Delivery & Logistics", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Accounts & Finance Module
        private void OpenChartOfAccountsForm() { MessageBox.Show("Opening Chart of Accounts Form...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenJournalVoucherForm() { MessageBox.Show("Opening Journal Voucher Form...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenPaymentVoucherForm() { MessageBox.Show("Opening Payment Voucher Form...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenReceiptVoucherForm() { MessageBox.Show("Opening Receipt Voucher Form...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenBankReconciliationForm() { MessageBox.Show("Opening Bank Reconciliation Form...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenGeneralLedgerReport() { MessageBox.Show("Opening General Ledger Report...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenTrialBalanceReport() { MessageBox.Show("Opening Trial Balance Report...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenProfitLossReport() { MessageBox.Show("Opening Profit & Loss Report...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenBalanceSheetReport() { MessageBox.Show("Opening Balance Sheet Report...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenCashFlowReport() { MessageBox.Show("Opening Cash Flow Report...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenBankReconciliationReport() { MessageBox.Show("Opening Bank Reconciliation Report...", "Accounts & Finance", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Expense Management Module
        private void OpenExpenseEntryForm() { MessageBox.Show("Opening Expense Entry Form...", "Expense Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenExpenseCategoriesForm() { MessageBox.Show("Opening Expense Categories Form...", "Expense Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenExpenseReport() { MessageBox.Show("Opening Expense Report...", "Expense Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenExpenseSummaryReport() { MessageBox.Show("Opening Expense Summary Report...", "Expense Management", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Returns & Refunds Module
        private void OpenPurchaseReturnEntryForm() { MessageBox.Show("Opening Purchase Return Entry Form...", "Returns & Refunds", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenSalesReturnEntryForm() { MessageBox.Show("Opening Sales Return Entry Form...", "Returns & Refunds", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenCreditDebitNoteForm() { MessageBox.Show("Opening Credit/Debit Note Entry Form...", "Returns & Refunds", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenReturnSummaryReport() { MessageBox.Show("Opening Return Summary Report...", "Returns & Refunds", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenReturnRegisterReport() { MessageBox.Show("Opening Return Register Report...", "Returns & Refunds", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Tax & Compliance Module
        private void OpenTaxConfigurationForm() { MessageBox.Show("Opening Tax Configuration Form...", "Tax & Compliance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenTaxReturnExport() { MessageBox.Show("Opening Tax Return Filing Export...", "Tax & Compliance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenTaxSummaryReport() { MessageBox.Show("Opening Tax Summary Report...", "Tax & Compliance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenTaxInvoiceReport() { MessageBox.Show("Opening Tax Invoice Report...", "Tax & Compliance", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Reports Dashboard (BI Style)
        private void OpenSalesTrendCharts() { MessageBox.Show("Opening Sales Trend Charts...", "Reports Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenPurchaseVsSalesAnalysis() { MessageBox.Show("Opening Purchase vs Sales Analysis...", "Reports Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenProfitabilityAnalysis() { MessageBox.Show("Opening Profitability Analysis...", "Reports Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenTop10Analysis() { MessageBox.Show("Opening Top 10 Analysis...", "Reports Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenAgingAnalysisGraphs() { MessageBox.Show("Opening Aging Analysis Graphs...", "Reports Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // Security & Backup Module
        private void OpenDatabaseBackupForm() { MessageBox.Show("Opening Database Backup & Restore Form...", "Security & Backup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenUserAccessControlForm() { MessageBox.Show("Opening User Access Control Form...", "Security & Backup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenAuditTrailViewer() { MessageBox.Show("Opening Audit Trail Viewer...", "Security & Backup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenAuditTrailReport() { MessageBox.Show("Opening Audit Trail Report...", "Security & Backup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void OpenBackupHistoryReport() { MessageBox.Show("Opening Backup History Report...", "Security & Backup", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        #endregion
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
}
