using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WinForms;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class StockReportForm : Form
    {
        private string connectionString;
        private ReportViewer reportViewer;

        public StockReportForm()
        {
            InitializeComponent();
            
            try
            {
                InitializeConnection();
                InitializeReportViewer();
                LoadFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Stock Report Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Don't close the form here, let it show with error handling
            }
        }

        private void InitializeConnection()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["DistributionConnection"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Database connection string not found. Please check your configuration.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Test the connection
                TestDatabaseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing database connection: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        
        private void TestDatabaseConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    System.Diagnostics.Debug.WriteLine("Database connection successful");
                    
                    // Test if we have data in key tables
                    string testQuery = "SELECT COUNT(*) FROM Products WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(testQuery, conn))
                    {
                        int productCount = (int)cmd.ExecuteScalar();
                        System.Diagnostics.Debug.WriteLine($"Active products count: {productCount}");
                        
                        if (productCount == 0)
                        {
                            MessageBox.Show("No products found in database. Please run the seed data script to populate the database.", "No Data Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    
                    // Test stock data
                    string stockQuery = "SELECT COUNT(*) FROM Stock";
                    using (SqlCommand cmd = new SqlCommand(stockQuery, conn))
                    {
                        int stockCount = (int)cmd.ExecuteScalar();
                        System.Diagnostics.Debug.WriteLine($"Stock records count: {stockCount}");
                    }
                    
                    // Test categories
                    string categoryQuery = "SELECT COUNT(*) FROM Categories WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(categoryQuery, conn))
                    {
                        int categoryCount = (int)cmd.ExecuteScalar();
                        System.Diagnostics.Debug.WriteLine($"Active categories count: {categoryCount}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection test failed: {ex.Message}\n\nPlease ensure:\n1. SQL Server is running\n2. Database 'DistributionDB' exists\n3. Connection string is correct", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeReportViewer()
        {
            try
            {
                reportViewer = new ReportViewer();
                reportViewer.Location = new Point(0, 120); // Position below filters panel
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 120);
                reportViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                reportViewer.ProcessingMode = ProcessingMode.Local;
                
                // Improve ReportViewer appearance and alignment
                reportViewer.BackColor = Color.White;
                reportViewer.BorderStyle = BorderStyle.None;
                reportViewer.Dock = DockStyle.None; // Use manual positioning instead of dock
                reportViewer.ZoomMode = ZoomMode.PageWidth; // Auto-fit to page width
                reportViewer.ShowZoomControl = true;
                reportViewer.ShowPrintButton = true;
                reportViewer.ShowExportButton = true;
                reportViewer.ShowRefreshButton = true;
                reportViewer.ShowFindControls = true;
                reportViewer.ShowPageNavigationControls = true;
                reportViewer.ShowStopButton = false;
                reportViewer.ShowProgress = true;
                
                // Set the report path
                string reportPath = FindReportPath();
                if (!string.IsNullOrEmpty(reportPath))
                {
                    reportViewer.LocalReport.ReportPath = reportPath;
                    System.Diagnostics.Debug.WriteLine($"Report path set to: {reportPath}");
                }
                else
                {
                    MessageBox.Show("Could not find the RDLC report file. Please ensure SimpleStockReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    System.Diagnostics.Debug.WriteLine("RDLC report file not found");
                }
                
                this.Controls.Add(reportViewer);
                
                System.Diagnostics.Debug.WriteLine("ReportViewer initialized successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing ReportViewer: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"ReportViewer initialization error: {ex}");
            }
        }
        
        private string FindReportPath()
        {
            // Try multiple possible paths for the RDLC file
            string[] possiblePaths = {
                Path.Combine(Application.StartupPath, "Reports", "SimpleStockReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "Reports", "SimpleStockReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "Reports", "SimpleStockReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "Reports", "SimpleStockReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "SimpleStockReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "Reports", "SimpleStockReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Reports", "SimpleStockReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Reports", "SimpleStockReport.rdlc"),
                // Try absolute path from project structure
                Path.Combine(Application.StartupPath, "..", "..", "..", "DistributionSoftware", "Reports", "SimpleStockReport.rdlc")
            };
            
            System.Diagnostics.Debug.WriteLine($"Application.StartupPath: {Application.StartupPath}");
            System.Diagnostics.Debug.WriteLine($"Directory.GetCurrentDirectory(): {Directory.GetCurrentDirectory()}");
            
            foreach (string path in possiblePaths)
            {
                System.Diagnostics.Debug.WriteLine($"Checking path: {path}");
                if (File.Exists(path))
                {
                    System.Diagnostics.Debug.WriteLine($"Found report at: {path}");
                    return path;
                }
            }
            
            System.Diagnostics.Debug.WriteLine("Report file not found in any expected location");
            return null;
        }
        
        private bool ValidateReportSetup()
        {
            try
            {
                // Check if ReportViewer is initialized
                if (reportViewer == null)
                {
                    MessageBox.Show("Report viewer is not initialized. Please restart the application.", "Report Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                // Check if report path is set
                if (string.IsNullOrEmpty(reportViewer.LocalReport.ReportPath))
                {
                    MessageBox.Show("Report path is not set. Please ensure the RDLC file is accessible.", "Report Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                // Check if report file exists
                if (!File.Exists(reportViewer.LocalReport.ReportPath))
                {
                    MessageBox.Show($"Report file not found at: {reportViewer.LocalReport.ReportPath}\n\nPlease ensure SimpleStockReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                // Check if report is properly loaded
                if (reportViewer.LocalReport.DataSources == null)
                {
                    MessageBox.Show("Report data sources are not initialized properly.", "Report Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                // Ensure report is properly initialized
                EnsureReportInitialized();
                
                System.Diagnostics.Debug.WriteLine("Report setup validation passed");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating report setup: {ex.Message}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Report setup validation error: {ex}");
                return false;
            }
        }
        
        private void EnsureReportInitialized()
        {
            try
            {
                // Force the report to initialize by calling GetDefaultPageSettings
                var pageSettings = reportViewer.LocalReport.GetDefaultPageSettings();
                System.Diagnostics.Debug.WriteLine("Report initialized successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Report initialization error: {ex.Message}");
                // Don't throw here, just log the error
            }
        }

        private void LoadFilters()
        {
            try
            {
                // Load Products
                LoadProducts();
                
                // Load Categories
                LoadCategories();
                
                // Load Brands
                LoadBrands();
                
                // Load Warehouses
                LoadWarehouses();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filter data: {ex.Message}", "Data Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ProductId, ProductName FROM Products WHERE IsActive = 1 ORDER BY ProductName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    // Add "All Products" option at the top
                    DataRow allRow = dt.NewRow();
                    allRow["ProductId"] = -1;
                    allRow["ProductName"] = "All Products";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbProduct.DataSource = dt;
                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductId";
                    cmbProduct.SelectedIndex = 0; // Select "All Products" by default
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT CategoryId, CategoryName FROM Categories WHERE IsActive = 1 ORDER BY CategoryName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    // Add "All Categories" option at the top
                    DataRow allRow = dt.NewRow();
                    allRow["CategoryId"] = -1;
                    allRow["CategoryName"] = "All Categories";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbCategory.DataSource = dt;
                    cmbCategory.DisplayMember = "CategoryName";
                    cmbCategory.ValueMember = "CategoryId";
                    cmbCategory.SelectedIndex = 0; // Select "All Categories" by default
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBrands()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT BrandId, BrandName FROM Brands WHERE IsActive = 1 ORDER BY BrandName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    // Add "All Brands" option at the top
                    DataRow allRow = dt.NewRow();
                    allRow["BrandId"] = -1;
                    allRow["BrandName"] = "All Brands";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbBrand.DataSource = dt;
                    cmbBrand.DisplayMember = "BrandName";
                    cmbBrand.ValueMember = "BrandId";
                    cmbBrand.SelectedIndex = 0; // Select "All Brands" by default
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading brands: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWarehouses()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT WarehouseId, WarehouseName FROM Warehouses WHERE IsActive = 1 ORDER BY WarehouseName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    // Add "All Warehouses" option at the top
                    DataRow allRow = dt.NewRow();
                    allRow["WarehouseId"] = -1;
                    allRow["WarehouseName"] = "All Warehouses";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbWarehouse.DataSource = dt;
                    cmbWarehouse.DisplayMember = "WarehouseName";
                    cmbWarehouse.ValueMember = "WarehouseId";
                    cmbWarehouse.SelectedIndex = 0; // Select "All Warehouses" by default
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading warehouses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer == null)
                {
                    MessageBox.Show("Report viewer is not initialized properly.", "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Test database connection and data availability first
                if (!TestReportDataAvailability())
                {
                    MessageBox.Show("Database connection or data availability issues detected. Please check the database setup.", "Data Availability Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get filter values (ignore "All" options with ID = -1)
                int? productId = cmbProduct.SelectedValue != null && (int)cmbProduct.SelectedValue != -1 ? (int?)cmbProduct.SelectedValue : null;
                int? categoryId = cmbCategory.SelectedValue != null && (int)cmbCategory.SelectedValue != -1 ? (int?)cmbCategory.SelectedValue : null;
                int? brandId = cmbBrand.SelectedValue != null && (int)cmbBrand.SelectedValue != -1 ? (int?)cmbBrand.SelectedValue : null;
                int? warehouseId = cmbWarehouse.SelectedValue != null && (int)cmbWarehouse.SelectedValue != -1 ? (int?)cmbWarehouse.SelectedValue : null;
                DateTime? reportDate = dtpReportDate.Value.Date;

                // Get report data
                DataTable reportData = GetStockReportData(productId, categoryId, brandId, warehouseId, reportDate);

                // Always show the report, even if no data
                if (reportData == null)
                {
                    reportData = CreateEmptyDataTable();
                }

                // Debug: Show data info
                System.Diagnostics.Debug.WriteLine($"Report data rows: {reportData.Rows.Count}");
                System.Diagnostics.Debug.WriteLine($"Report data columns: {reportData.Columns.Count}");
                if (reportData.Rows.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"First row sample: {string.Join(", ", reportData.Rows[0].ItemArray.Take(3))}");
                }
                
                // Ensure we always have data structure even if empty
                if (reportData.Rows.Count == 0)
                {
                    // Add a "No data found" row
                    DataRow noDataRow = reportData.NewRow();
                    noDataRow["ProductCode"] = "No data found";
                    noDataRow["ProductName"] = "No products match the selected criteria";
                    noDataRow["CategoryName"] = "N/A";
                    noDataRow["BrandName"] = "N/A";
                    noDataRow["UnitName"] = "N/A";
                    noDataRow["UnitPrice"] = 0;
                    noDataRow["StockQuantity"] = 0;
                    noDataRow["WarehouseName"] = "N/A";
                    noDataRow["ReorderLevel"] = 0;
                    noDataRow["BatchNumber"] = "N/A";
                    noDataRow["ExpiryDate"] = DBNull.Value;
                    reportData.Rows.Add(noDataRow);
                }

                // Validate report setup before proceeding
                if (!ValidateReportSetup())
                {
                    return;
                }
                
                // Clear existing data sources first
                reportViewer.LocalReport.DataSources.Clear();
                
                // Set up report data source first
                ReportDataSource reportDataSource = new ReportDataSource("StockDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                
                // Set report parameters AFTER data source is added
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("ReportDate", reportDate?.ToString("dd-MM-yyyy") ?? DateTime.Now.ToString("dd-MM-yyyy")),
                    new ReportParameter("ProductFilter", productId.HasValue ? (cmbProduct?.Text ?? "Unknown Product") : "All Products"),
                    new ReportParameter("CategoryFilter", categoryId.HasValue ? (cmbCategory?.Text ?? "Unknown Category") : "All Categories"),
                    new ReportParameter("BrandFilter", brandId.HasValue ? (cmbBrand?.Text ?? "Unknown Brand") : "All Brands"),
                    new ReportParameter("WarehouseFilter", warehouseId.HasValue ? (cmbWarehouse?.Text ?? "Unknown Warehouse") : "All Warehouses")
                };
                
                try
                {
                    reportViewer.LocalReport.SetParameters(parameters);
                    System.Diagnostics.Debug.WriteLine("Parameters set successfully");
                }
                catch (Exception paramEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Parameter setting error: {paramEx.Message}");
                    // Continue without parameters if they fail
                }
                
                // Debug information
                System.Diagnostics.Debug.WriteLine($"Report path: {reportViewer.LocalReport.ReportPath}");
                System.Diagnostics.Debug.WriteLine($"Data source count: {reportViewer.LocalReport.DataSources.Count}");
                System.Diagnostics.Debug.WriteLine($"Data rows: {reportData.Rows.Count}");
                System.Diagnostics.Debug.WriteLine($"Data columns: {reportData.Columns.Count}");
                
                // Refresh the report
                reportViewer.RefreshReport();
                
                // Improve report positioning after refresh
                ImproveReportPositioning();
                
                // Update form title with filter information
                string reportDateStr = reportDate?.ToString("dd-MM-yyyy") ?? DateTime.Now.ToString("dd-MM-yyyy");
                string productFilterStr = productId.HasValue ? (cmbProduct?.Text ?? "Unknown Product") : "All Products";
                string categoryFilterStr = categoryId.HasValue ? (cmbCategory?.Text ?? "Unknown Category") : "All Categories";
                string brandFilterStr = brandId.HasValue ? (cmbBrand?.Text ?? "Unknown Brand") : "All Brands";
                string warehouseFilterStr = warehouseId.HasValue ? (cmbWarehouse?.Text ?? "Unknown Warehouse") : "All Warehouses";
                
                this.Text = $"Stock Report - {reportDateStr} | {productFilterStr} | {categoryFilterStr} | {brandFilterStr} | {warehouseFilterStr}";
                
                System.Diagnostics.Debug.WriteLine($"Report generated successfully with {reportData.Rows.Count} rows");
                
                MessageBox.Show($"Report generated successfully!\n\nFound {reportData.Rows.Count} records matching your criteria.", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error generating report: {ex.Message}\n\n" +
                                   $"Technical Details:\n" +
                                   $"- Exception Type: {ex.GetType().Name}\n" +
                                   $"- Stack Trace: {ex.StackTrace}\n\n" +
                                   $"Please check:\n" +
                                   $"1. Database connection\n" +
                                   $"2. Data availability\n" +
                                   $"3. Filter selections\n" +
                                   $"4. RDLC file accessibility\n\n" +
                                   $"Contact administrator if problem persists.";
                
                MessageBox.Show(errorMessage, "Report Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Report generation error: {ex}");
            }
        }

        private DataTable GetStockReportData(int? productId, int? categoryId, int? brandId, int? warehouseId, DateTime? reportDate)
        {
            DataTable dt = new DataTable();
            
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Database connection is not available.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return CreateEmptyDataTable();
                }
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT ");
                    query.AppendLine("    p.ProductCode, ");
                    query.AppendLine("    p.ProductName, ");
                    query.AppendLine("    ISNULL(c.CategoryName, ISNULL(p.Category, 'N/A')) AS CategoryName, ");
                    query.AppendLine("    ISNULL(b.BrandName, 'N/A') AS BrandName, ");
                    query.AppendLine("    ISNULL(u.UnitName, 'N/A') AS UnitName, ");
                    query.AppendLine("    p.UnitPrice, ");
                    query.AppendLine("    ISNULL(s.Quantity, p.StockQuantity) AS StockQuantity, ");
                    query.AppendLine("    ISNULL(w.WarehouseName, ISNULL(w2.WarehouseName, 'Main Warehouse')) AS WarehouseName, ");
                    query.AppendLine("    ISNULL(p.ReorderLevel, 0) AS ReorderLevel, ");
                    query.AppendLine("    ISNULL(s.BatchNumber, p.BatchNumber) AS BatchNumber, ");
                    query.AppendLine("    ISNULL(s.ExpiryDate, p.ExpiryDate) AS ExpiryDate ");
                    query.AppendLine("FROM Products p ");
                    query.AppendLine("LEFT JOIN Categories c ON p.CategoryId = c.CategoryId AND c.IsActive = 1 ");
                    query.AppendLine("LEFT JOIN Brands b ON p.BrandId = b.BrandId AND b.IsActive = 1 ");
                    query.AppendLine("LEFT JOIN Units u ON p.UnitId = u.UnitId AND u.IsActive = 1 ");
                    query.AppendLine("LEFT JOIN Stock s ON p.ProductId = s.ProductId ");
                    query.AppendLine("LEFT JOIN Warehouses w ON s.WarehouseId = w.WarehouseId AND w.IsActive = 1 ");
                    query.AppendLine("LEFT JOIN Warehouses w2 ON p.WarehouseId = w2.WarehouseId AND w2.IsActive = 1 ");
                    query.AppendLine("WHERE p.IsActive = 1 ");

                    // Add filters
                    if (productId.HasValue)
                        query.AppendLine("AND p.ProductId = @ProductId ");
                    
                    if (categoryId.HasValue)
                        query.AppendLine("AND p.CategoryId = @CategoryId ");
                    
                    if (brandId.HasValue)
                        query.AppendLine("AND p.BrandId = @BrandId ");
                    
                    if (warehouseId.HasValue)
                        query.AppendLine("AND s.WarehouseId = @WarehouseId ");

                    query.AppendLine("ORDER BY p.ProductName, w.WarehouseName");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                    
                    if (productId.HasValue)
                        cmd.Parameters.AddWithValue("@ProductId", productId.Value);
                    
                    if (categoryId.HasValue)
                        cmd.Parameters.AddWithValue("@CategoryId", categoryId.Value);
                    
                    if (brandId.HasValue)
                        cmd.Parameters.AddWithValue("@BrandId", brandId.Value);
                    
                    if (warehouseId.HasValue)
                        cmd.Parameters.AddWithValue("@WarehouseId", warehouseId.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    
                    System.Diagnostics.Debug.WriteLine($"SQL Query executed successfully. Rows returned: {dt.Rows.Count}");
                    
                    // If no data returned, try a simpler query
                    if (dt.Rows.Count == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("No data from complex query, trying simple query...");
                        dt = GetSimpleStockData(productId, categoryId, brandId, warehouseId, reportDate);
                    }
                }
                
                // If no data found, add a "No data found" row
                if (dt.Rows.Count == 0)
                {
                    dt = CreateEmptyDataTable();
                    DataRow noDataRow = dt.NewRow();
                    noDataRow["ProductCode"] = "N/A";
                    noDataRow["ProductName"] = "No data found for the selected criteria";
                    noDataRow["CategoryName"] = "N/A";
                    noDataRow["BrandName"] = "N/A";
                    noDataRow["UnitName"] = "N/A";
                    noDataRow["UnitPrice"] = 0;
                    noDataRow["StockQuantity"] = 0;
                    noDataRow["WarehouseName"] = "N/A";
                    noDataRow["ReorderLevel"] = 0;
                    noDataRow["BatchNumber"] = "N/A";
                    noDataRow["ExpiryDate"] = DBNull.Value;
                    dt.Rows.Add(noDataRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving report data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CreateEmptyDataTable();
            }

            return dt;
        }
        
        private DataTable CreateEmptyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductCode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Columns.Add("BrandName", typeof(string));
            dt.Columns.Add("UnitName", typeof(string));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("StockQuantity", typeof(int));
            dt.Columns.Add("WarehouseName", typeof(string));
            dt.Columns.Add("ReorderLevel", typeof(int));
            dt.Columns.Add("BatchNumber", typeof(string));
            dt.Columns.Add("ExpiryDate", typeof(DateTime));
            return dt;
        }
        
        private DataTable GetSimpleStockData(int? productId, int? categoryId, int? brandId, int? warehouseId, DateTime? reportDate)
        {
            DataTable dt = CreateEmptyDataTable();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Simple query that just gets products with basic info
                    string simpleQuery = @"
                        SELECT 
                            p.ProductCode,
                            p.ProductName,
                            ISNULL(p.Category, 'N/A') AS CategoryName,
                            'N/A' AS BrandName,
                            'N/A' AS UnitName,
                            p.UnitPrice,
                            p.StockQuantity,
                            'Main Warehouse' AS WarehouseName,
                            p.ReorderLevel,
                            ISNULL(p.BatchNumber, 'N/A') AS BatchNumber,
                            p.ExpiryDate
                        FROM Products p
                        WHERE p.IsActive = 1";
                    
                    List<string> conditions = new List<string>();
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    
                    if (productId.HasValue)
                    {
                        conditions.Add("p.ProductId = @ProductId");
                        parameters.Add(new SqlParameter("@ProductId", productId.Value));
                    }
                    
                    if (categoryId.HasValue)
                    {
                        conditions.Add("p.CategoryId = @CategoryId");
                        parameters.Add(new SqlParameter("@CategoryId", categoryId.Value));
                    }
                    
                    if (brandId.HasValue)
                    {
                        conditions.Add("p.BrandId = @BrandId");
                        parameters.Add(new SqlParameter("@BrandId", brandId.Value));
                    }
                    
                    if (warehouseId.HasValue)
                    {
                        conditions.Add("p.WarehouseId = @WarehouseId");
                        parameters.Add(new SqlParameter("@WarehouseId", warehouseId.Value));
                    }
                    
                    if (conditions.Count > 0)
                    {
                        simpleQuery += " AND " + string.Join(" AND ", conditions);
                    }
                    
                    simpleQuery += " ORDER BY p.ProductName";
                    
                    using (SqlCommand cmd = new SqlCommand(simpleQuery, conn))
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.Add(param);
                        }
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                        
                        System.Diagnostics.Debug.WriteLine($"Simple query executed successfully. Rows returned: {dt.Rows.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Simple query failed: {ex.Message}");
                // Return empty table with structure
            }
            
            return dt;
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer == null || reportViewer.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate a report first before exporting.", "No Report Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF files (*.pdf)|*.pdf|Excel files (*.xls)|*.xls|Word files (*.doc)|*.doc";
                saveDialog.FileName = "StockReport_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export using ReportViewer's built-in export functionality
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] reportBytes = reportViewer.LocalReport.Render(
                        saveDialog.FileName.EndsWith(".pdf") ? "PDF" : 
                        saveDialog.FileName.EndsWith(".xls") ? "Excel" : "Word",
                        null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    System.IO.File.WriteAllBytes(saveDialog.FileName, reportBytes);
                    MessageBox.Show("Report exported successfully to " + saveDialog.FileName, "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting report: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            // Adjust ReportViewer size and position when form is resized
            if (reportViewer != null)
            {
                // Ensure ReportViewer stays below the filters panel (120px height)
                int filtersPanelHeight = 120;
                reportViewer.Location = new Point(0, filtersPanelHeight);
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - filtersPanelHeight);
                
                // Ensure minimum size
                if (reportViewer.Size.Width < 400)
                    reportViewer.Size = new Size(400, reportViewer.Size.Height);
                if (reportViewer.Size.Height < 300)
                    reportViewer.Size = new Size(reportViewer.Size.Width, 300);
            }
        }
        
        private void ImproveReportPositioning()
        {
            try
            {
                if (reportViewer != null)
                {
                    // Set optimal zoom mode for better alignment
                    reportViewer.ZoomMode = ZoomMode.PageWidth;
                    
                    // Ensure the report is properly centered
                    reportViewer.RefreshReport();
                    
                    System.Diagnostics.Debug.WriteLine("Report positioning improved");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error improving report positioning: {ex.Message}");
            }
        }
        
        
        
        private bool TestReportDataAvailability()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Test if Products table exists and has data
                    string testQuery = "SELECT COUNT(*) FROM Products WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(testQuery, conn))
                    {
                        int count = (int)cmd.ExecuteScalar();
                        System.Diagnostics.Debug.WriteLine($"Products available for report: {count}");
                        
                        if (count == 0)
                        {
                            System.Diagnostics.Debug.WriteLine("No products found in database");
                            return false;
                        }
                    }
                    
                    // Test if we can execute a simple query
                    string simpleTestQuery = "SELECT TOP 1 ProductCode, ProductName FROM Products WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(simpleTestQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                System.Diagnostics.Debug.WriteLine($"Test query successful. Sample product: {reader["ProductName"]}");
                                return true;
                            }
                        }
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Data availability test failed: {ex.Message}");
                return false;
            }
        }
        
    }
}
