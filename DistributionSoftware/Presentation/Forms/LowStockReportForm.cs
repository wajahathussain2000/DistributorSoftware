using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
using DistributionSoftware.Common;
using System.Diagnostics;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class LowStockReportForm : Form
    {
        private SqlConnection connection;
        private DataTable lowStockDataTable;
        private DataTable categoriesDataTable;
        private DataTable warehousesDataTable;

        public LowStockReportForm()
        {
            try
            {
                Debug.WriteLine("LowStockReportForm: Initializing form");
                InitializeComponent();
                InitializeConnection();
                InitializeDataTable();
                LoadCategories();
                LoadWarehouses();
                LoadLowStockData();
                Debug.WriteLine("LowStockReportForm: Form initialized successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Initialization error - {ex.Message}");
                MessageBox.Show($"Error initializing Low Stock Report Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StyleTextBox(TextBox textBox, bool isReadOnly = false)
        {
            textBox.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = isReadOnly ? Color.FromArgb(245, 245, 245) : Color.White;
            textBox.ForeColor = Color.FromArgb(44, 62, 80);
            textBox.Padding = new Padding(5);
        }

        private void StyleComboBox(ComboBox comboBox)
        {
            comboBox.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = Color.FromArgb(44, 62, 80);
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        private void StyleDateTimePicker(DateTimePicker dateTimePicker)
        {
            dateTimePicker.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dateTimePicker.BackColor = Color.White;
            dateTimePicker.ForeColor = Color.FromArgb(44, 62, 80);
            dateTimePicker.Format = DateTimePickerFormat.Short;
        }

        private void InitializeUI()
        {
            try
            {
                Debug.WriteLine("LowStockReportForm: Initializing UI");
                
                // Form settings
                this.Text = "Low Stock Report - Distribution Software";
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.BackColor = Color.FromArgb(248, 249, 250);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.MaximizeBox = true;
                this.MinimizeBox = true;
                this.MinimumSize = new Size(1200, 800);

                // Header Panel
                Panel headerPanel = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 80,
                    BackColor = Color.FromArgb(52, 73, 94)
                };

                Label headerLabel = new Label
                {
                    Text = "⚠️ Low Stock Report",
                    Font = new Font("Segoe UI", 20, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(20, 20),
                    AutoSize = true
                };

                Button closeBtn = new Button
                {
                    Text = "✕",
                    Size = new Size(40, 40),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Location = new Point(this.Width - 80, 20),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold)
                };
                closeBtn.Click += (s, e) => this.Close();

                headerPanel.Controls.Add(headerLabel);
                headerPanel.Controls.Add(closeBtn);

                // Main Content Panel
                Panel contentPanel = new Panel
                {
                    Location = new Point(0, 80), // Start below header (80px height)
                    Size = new Size(this.Width, this.Height - 80), // Fill remaining space
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                    Padding = new Padding(20),
                    AutoScroll = true
                };

                // Filters Group
                GroupBox filtersGroup = new GroupBox
                {
                    Text = "🔍 Filters",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(1160, 120),
                    Location = new Point(0, 0) // Start at top of content panel
                };

                // Product Filter
                Label productLabel = new Label { Text = "Product:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtProductFilter = new TextBox { Name = "txtProductFilter", Location = new Point(120, 28), Size = new Size(200, 25) };
                StyleTextBox(txtProductFilter);

                // Category Filter
                Label categoryLabel = new Label { Text = "Category:", Location = new Point(350, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                ComboBox cmbCategory = new ComboBox { Name = "cmbCategory", Location = new Point(430, 28), Size = new Size(150, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                StyleComboBox(cmbCategory);

                // Warehouse Filter
                Label warehouseLabel = new Label { Text = "Warehouse:", Location = new Point(600, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                ComboBox cmbWarehouse = new ComboBox { Name = "cmbWarehouse", Location = new Point(700, 28), Size = new Size(150, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                StyleComboBox(cmbWarehouse);

                // Reorder Level Filter
                Label reorderLabel = new Label { Text = "Reorder Level:", Location = new Point(870, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                ComboBox cmbReorderLevel = new ComboBox { Name = "cmbReorderLevel", Location = new Point(980, 28), Size = new Size(120, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                StyleComboBox(cmbReorderLevel);
                cmbReorderLevel.Items.AddRange(new string[] { "All", "Below Reorder", "At Reorder", "Above Reorder" });
                cmbReorderLevel.SelectedIndex = 1; // Default to "Below Reorder"

                // Action Buttons
                Button btnFilter = new Button
                {
                    Text = "🔍 Filter",
                    Location = new Point(20, 70),
                    Size = new Size(100, 30),
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                btnFilter.Click += BtnFilter_Click;

                Button btnClear = new Button
                {
                    Text = "🗑️ Clear",
                    Location = new Point(130, 70),
                    Size = new Size(100, 30),
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                btnClear.Click += BtnClear_Click;

                Button btnExport = new Button
                {
                    Text = "📊 Export",
                    Location = new Point(240, 70),
                    Size = new Size(100, 30),
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                btnExport.Click += BtnExport_Click;

                Button btnPrint = new Button
                {
                    Text = "🖨️ Print",
                    Location = new Point(350, 70),
                    Size = new Size(100, 30),
                    BackColor = Color.FromArgb(155, 89, 182),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                btnPrint.Click += BtnPrint_Click;

                filtersGroup.Controls.AddRange(new Control[] {
                    productLabel, txtProductFilter, categoryLabel, cmbCategory,
                    warehouseLabel, cmbWarehouse, reorderLabel, cmbReorderLevel,
                    btnFilter, btnClear, btnExport, btnPrint
                });

                // Report Data Group
                GroupBox reportGroup = new GroupBox
                {
                    Text = "📊 Low Stock Report",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(1160, 500),
                    Location = new Point(0, 140) // Position below filters with proper spacing
                };

                // DataGridView
                DataGridView dgvLowStock = new DataGridView
                {
                    Name = "dgvLowStock",
                    Location = new Point(20, 30),
                    Size = new Size(1120, 450),
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    RowHeadersVisible = false,
                    BackgroundColor = Color.White,
                    GridColor = Color.FromArgb(189, 195, 199),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    ColumnHeadersHeight = 35,
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        BackColor = Color.FromArgb(52, 152, 219),
                        ForeColor = Color.White,
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    },
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        SelectionBackColor = Color.FromArgb(52, 152, 219),
                        SelectionForeColor = Color.White
                    }
                };

                reportGroup.Controls.Add(dgvLowStock);

                // Summary Panel
                Panel summaryPanel = new Panel
                {
                    Size = new Size(1160, 80),
                    Location = new Point(0, 660), // Position below report group
                    BackColor = Color.FromArgb(52, 152, 219),
                    Padding = new Padding(20)
                };

                Label summaryLabel = new Label
                {
                    Name = "lblSummary",
                    Text = "Total Items: 0 | Low Stock Items: 0 | Critical Items: 0",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(20, 20),
                    AutoSize = true
                };

                summaryPanel.Controls.Add(summaryLabel);

                contentPanel.Controls.AddRange(new Control[] { filtersGroup, reportGroup, summaryPanel });
                this.Controls.Add(headerPanel);
                this.Controls.Add(contentPanel);

                Debug.WriteLine("LowStockReportForm: UI initialized successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: UI initialization error - {ex.Message}");
                throw;
            }
        }

        private void InitializeConnection()
        {
            try
            {
                Debug.WriteLine("LowStockReportForm: Initializing database connection");
                // Use the correct connection string name from App.config
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True";
                }
                connection = new SqlConnection(connectionString);
                Debug.WriteLine($"LowStockReportForm: Database connection initialized with: {connectionString}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Connection error - {ex.Message}");
                // Fallback connection string
                connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True");
            }
        }

        private void InitializeDataTable()
        {
            try
            {
                Debug.WriteLine("LowStockReportForm: Initializing data table");
                lowStockDataTable = new DataTable();
                lowStockDataTable.Columns.Add("ProductId", typeof(int));
                lowStockDataTable.Columns.Add("ProductCode", typeof(string));
                lowStockDataTable.Columns.Add("ProductName", typeof(string));
                lowStockDataTable.Columns.Add("Category", typeof(string));
                lowStockDataTable.Columns.Add("Brand", typeof(string));
                lowStockDataTable.Columns.Add("CurrentStock", typeof(decimal));
                lowStockDataTable.Columns.Add("ReorderLevel", typeof(int));
                lowStockDataTable.Columns.Add("Warehouse", typeof(string));
                lowStockDataTable.Columns.Add("UnitPrice", typeof(decimal));
                lowStockDataTable.Columns.Add("StockValue", typeof(decimal));
                lowStockDataTable.Columns.Add("Status", typeof(string));
                lowStockDataTable.Columns.Add("LastUpdated", typeof(DateTime));

                Debug.WriteLine("LowStockReportForm: Data table initialized");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Data table initialization error - {ex.Message}");
                throw;
            }
        }

        private void LoadCategories()
        {
            try
            {
                Debug.WriteLine("LowStockReportForm: Loading categories");
                string query = "SELECT CategoryId, CategoryName FROM Categories WHERE IsActive = 1 ORDER BY CategoryName";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    categoriesDataTable = new DataTable();
                    adapter.Fill(categoriesDataTable);
                }

                ComboBox cmbCategory = FindControlRecursive(this, "cmbCategory") as ComboBox;
                if (cmbCategory != null)
                {
                    cmbCategory.Items.Add("All Categories");
                    foreach (DataRow row in categoriesDataTable.Rows)
                    {
                        cmbCategory.Items.Add(row["CategoryName"].ToString());
                    }
                    cmbCategory.SelectedIndex = 0;
                }

                Debug.WriteLine($"LowStockReportForm: Loaded {categoriesDataTable.Rows.Count} categories");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Load categories error - {ex.Message}");
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWarehouses()
        {
            try
            {
                Debug.WriteLine("LowStockReportForm: Loading warehouses");
                string query = "SELECT WarehouseId, WarehouseName FROM Warehouses WHERE IsActive = 1 ORDER BY WarehouseName";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    warehousesDataTable = new DataTable();
                    adapter.Fill(warehousesDataTable);
                }

                ComboBox cmbWarehouse = FindControlRecursive(this, "cmbWarehouse") as ComboBox;
                if (cmbWarehouse != null)
                {
                    cmbWarehouse.Items.Add("All Warehouses");
                    foreach (DataRow row in warehousesDataTable.Rows)
                    {
                        cmbWarehouse.Items.Add(row["WarehouseName"].ToString());
                    }
                    cmbWarehouse.SelectedIndex = 0;
                }

                Debug.WriteLine($"LowStockReportForm: Loaded {warehousesDataTable.Rows.Count} warehouses");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Load warehouses error - {ex.Message}");
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLowStockData()
        {
            try
            {
                Debug.WriteLine("LowStockReportForm: Loading low stock data");
                
                // Test connection first
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    Debug.WriteLine("LowStockReportForm: Database connection opened");
                }
                
                // Get filter values
                TextBox txtProductFilter = FindControlRecursive(this, "txtProductFilter") as TextBox;
                ComboBox cmbCategory = FindControlRecursive(this, "cmbCategory") as ComboBox;
                ComboBox cmbWarehouse = FindControlRecursive(this, "cmbWarehouse") as ComboBox;
                ComboBox cmbReorderLevel = FindControlRecursive(this, "cmbReorderLevel") as ComboBox;

                string productFilter = txtProductFilter?.Text?.Trim() ?? "";
                string categoryFilter = cmbCategory?.SelectedItem?.ToString() ?? "All Categories";
                string warehouseFilter = cmbWarehouse?.SelectedItem?.ToString() ?? "All Warehouses";
                string reorderFilter = cmbReorderLevel?.SelectedItem?.ToString() ?? "Below Reorder";

                Debug.WriteLine($"LowStockReportForm: Filters - Product: '{productFilter}', Category: '{categoryFilter}', Warehouse: '{warehouseFilter}', Reorder: '{reorderFilter}'");

                // Build query with filters
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"
                    SELECT 
                        p.ProductId,
                        p.ProductCode,
                        p.ProductName,
                        ISNULL(c.CategoryName, 'No Category') as Category,
                        ISNULL(b.BrandName, 'No Brand') as Brand,
                        ISNULL(s.Quantity, 0) as CurrentStock,
                        p.ReorderLevel,
                        ISNULL(w.WarehouseName, 'No Warehouse') as Warehouse,
                        p.UnitPrice,
                        (ISNULL(s.Quantity, 0) * p.UnitPrice) as StockValue,
                        CASE 
                            WHEN ISNULL(s.Quantity, 0) = 0 THEN 'Out of Stock'
                            WHEN ISNULL(s.Quantity, 0) < p.ReorderLevel THEN 'Low Stock'
                            WHEN ISNULL(s.Quantity, 0) = p.ReorderLevel THEN 'At Reorder Level'
                            ELSE 'Above Reorder Level'
                        END as Status,
                        ISNULL(s.LastUpdated, p.CreatedDate) as LastUpdated,
                        GETDATE() as QueryTime
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    LEFT JOIN Stock s ON p.ProductId = s.ProductId
                    LEFT JOIN Warehouses w ON s.WarehouseId = w.WarehouseId
                    WHERE p.IsActive = 1");

                // Add filters
                if (!string.IsNullOrEmpty(productFilter))
                {
                    queryBuilder.Append($" AND (p.ProductName LIKE '%{productFilter}%' OR p.ProductCode LIKE '%{productFilter}%')");
                }

                if (categoryFilter != "All Categories")
                {
                    queryBuilder.Append($" AND c.CategoryName = '{categoryFilter}'");
                }

                if (warehouseFilter != "All Warehouses")
                {
                    queryBuilder.Append($" AND w.WarehouseName = '{warehouseFilter}'");
                }

                // Reorder level filter
                switch (reorderFilter)
                {
                    case "Below Reorder":
                        queryBuilder.Append(" AND ISNULL(s.Quantity, 0) < p.ReorderLevel");
                        break;
                    case "At Reorder":
                        queryBuilder.Append(" AND ISNULL(s.Quantity, 0) = p.ReorderLevel");
                        break;
                    case "Above Reorder":
                        queryBuilder.Append(" AND ISNULL(s.Quantity, 0) > p.ReorderLevel");
                        break;
                }

                queryBuilder.Append(" ORDER BY ISNULL(s.Quantity, 0) ASC, p.ProductName");

                string finalQuery = queryBuilder.ToString();
                Debug.WriteLine($"LowStockReportForm: Final query: {finalQuery}");

                // First, let's test with a simple query to see if we have any products at all
                string testQuery = "SELECT COUNT(*) as ProductCount FROM Products WHERE IsActive = 1";
                using (SqlCommand testCmd = new SqlCommand(testQuery, connection))
                {
                    int productCount = Convert.ToInt32(testCmd.ExecuteScalar());
                    Debug.WriteLine($"LowStockReportForm: Found {productCount} active products in database");
                    
                    // Let's also check what database we're actually connected to
                    string dbNameQuery = "SELECT DB_NAME() as DatabaseName";
                    using (SqlCommand dbCmd = new SqlCommand(dbNameQuery, connection))
                    {
                        string dbName = dbCmd.ExecuteScalar().ToString();
                        Debug.WriteLine($"LowStockReportForm: Connected to database: {dbName}");
                    }
                    
                    // Let's also check the server name
                    string serverQuery = "SELECT @@SERVERNAME as ServerName";
                    using (SqlCommand serverCmd = new SqlCommand(serverQuery, connection))
                    {
                        string serverName = serverCmd.ExecuteScalar().ToString();
                        Debug.WriteLine($"LowStockReportForm: Connected to server: {serverName}");
                    }
                    
                    if (productCount == 0)
                    {
                        MessageBox.Show("No active products found in database. Please add some products first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(finalQuery, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    Debug.WriteLine($"LowStockReportForm: Query returned {dt.Rows.Count} rows");
                    
                    // If no rows returned, try a simpler query to show all products
                    if (dt.Rows.Count == 0)
                    {
                        Debug.WriteLine("LowStockReportForm: No rows from complex query, trying simple query");
                        string simpleQuery = @"
                            SELECT 
                                p.ProductId,
                                p.ProductCode,
                                p.ProductName,
                                'No Category' as Category,
                                'No Brand' as Brand,
                                0 as CurrentStock,
                                p.ReorderLevel,
                                'No Warehouse' as Warehouse,
                                p.UnitPrice,
                                0 as StockValue,
                                'No Stock Data' as Status,
                                p.CreatedDate as LastUpdated
                            FROM Products p
                            WHERE p.IsActive = 1
                            ORDER BY p.ProductName";
                        
                        using (SqlDataAdapter simpleAdapter = new SqlDataAdapter(simpleQuery, connection))
                        {
                            dt.Clear();
                            simpleAdapter.Fill(dt);
                            Debug.WriteLine($"LowStockReportForm: Simple query returned {dt.Rows.Count} rows");
                        }
                    }

                    DataGridView dgvLowStock = FindControlRecursive(this, "dgvLowStock") as DataGridView;
                    if (dgvLowStock != null)
                    {
                        dgvLowStock.DataSource = dt;
                        dgvLowStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        
                        Debug.WriteLine($"LowStockReportForm: DataGridView updated with {dt.Rows.Count} rows");
                        
                        // Let's log the first few rows to see what data we're getting
                        for (int i = 0; i < Math.Min(3, dt.Rows.Count); i++)
                        {
                            DataRow row = dt.Rows[i];
                            Debug.WriteLine($"LowStockReportForm: Row {i}: ProductCode='{row["ProductCode"]}', ProductName='{row["ProductName"]}', CurrentStock={row["CurrentStock"]}");
                        }
                        
                        // Configure columns
                        if (dgvLowStock.Columns.Count > 0)
                        {
                            dgvLowStock.Columns["ProductId"].Visible = false;
                            dgvLowStock.Columns["ProductCode"].HeaderText = "Product Code";
                            dgvLowStock.Columns["ProductName"].HeaderText = "Product Name";
                            dgvLowStock.Columns["CurrentStock"].HeaderText = "Current Stock";
                            dgvLowStock.Columns["ReorderLevel"].HeaderText = "Reorder Level";
                            dgvLowStock.Columns["UnitPrice"].HeaderText = "Unit Price";
                            dgvLowStock.Columns["StockValue"].HeaderText = "Stock Value";
                            dgvLowStock.Columns["LastUpdated"].HeaderText = "Last Updated";

                            // Format numeric columns
                            dgvLowStock.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
                            dgvLowStock.Columns["StockValue"].DefaultCellStyle.Format = "N2";
                            dgvLowStock.Columns["LastUpdated"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                            // Color coding for status
                            foreach (DataGridViewRow row in dgvLowStock.Rows)
                            {
                                string status = row.Cells["Status"].Value?.ToString();
                                switch (status)
                                {
                                    case "Out of Stock":
                                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
                                        row.DefaultCellStyle.ForeColor = Color.FromArgb(198, 40, 40);
                                        break;
                                    case "Low Stock":
                                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225);
                                        row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 126, 34);
                                        break;
                                    case "At Reorder Level":
                                        row.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 253);
                                        row.DefaultCellStyle.ForeColor = Color.FromArgb(52, 152, 219);
                                        break;
                                }
                            }
                        }
                    }

                    // Update summary
                    UpdateSummary(dt);
                }

                Debug.WriteLine("LowStockReportForm: Low stock data loaded successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Load low stock data error - {ex.Message}");
                MessageBox.Show($"Error loading low stock data: {ex.Message}\n\nPlease check:\n1. Database connection\n2. SQL Server is running\n3. DistributionDB database exists", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Debug.WriteLine("LowStockReportForm: Database connection closed");
                }
            }
        }

        private void UpdateSummary(DataTable dataTable)
        {
            try
            {
                int totalItems = dataTable.Rows.Count;
                int lowStockItems = 0;
                int criticalItems = 0;

                foreach (DataRow row in dataTable.Rows)
                {
                    string status = row["Status"].ToString();
                    if (status == "Low Stock" || status == "At Reorder Level")
                        lowStockItems++;
                    if (status == "Out of Stock")
                        criticalItems++;
                }

                Label lblSummary = FindControlRecursive(this, "lblSummary") as Label;
                if (lblSummary != null)
                {
                    lblSummary.Text = $"Total Items: {totalItems} | Low Stock Items: {lowStockItems} | Critical Items: {criticalItems}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Update summary error - {ex.Message}");
            }
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Name == name)
                    return control;
                
                Control found = FindControlRecursive(control, name);
                if (found != null)
                    return found;
            }
            return null;
        }

        // Event Handlers
        private void BtnFilter_Click(object sender, EventArgs e)
        {
            LoadLowStockData();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtProductFilter = FindControlRecursive(this, "txtProductFilter") as TextBox;
                ComboBox cmbCategory = FindControlRecursive(this, "cmbCategory") as ComboBox;
                ComboBox cmbWarehouse = FindControlRecursive(this, "cmbWarehouse") as ComboBox;
                ComboBox cmbReorderLevel = FindControlRecursive(this, "cmbReorderLevel") as ComboBox;

                if (txtProductFilter != null) txtProductFilter.Clear();
                if (cmbCategory != null) cmbCategory.SelectedIndex = 0;
                if (cmbWarehouse != null) cmbWarehouse.SelectedIndex = 0;
                if (cmbReorderLevel != null) cmbReorderLevel.SelectedIndex = 1;

                LoadLowStockData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Clear filters error - {ex.Message}");
                MessageBox.Show($"Error clearing filters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Export functionality will be implemented in the next phase.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Export error - {ex.Message}");
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Print functionality will be implemented in the next phase.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LowStockReportForm: Print error - {ex.Message}");
                MessageBox.Show($"Error printing report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
