using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class BatchExpiryReportForm : Form
    {
        private ProductRepository productRepository;

        public BatchExpiryReportForm()
        {
            InitializeComponent();
            InitializeRepositories();
            LoadInitialData();
        }

        private void InitializeRepositories()
        {
            try
            {
                productRepository = new ProductRepository();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing repositories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInitialData()
        {
            try
            {
                // Set default dates
                dtpStartDate.Value = DateTime.Now.AddDays(-30);
                dtpEndDate.Value = DateTime.Now;

                // Load products
                LoadProducts();

                // Set default filters
                cmbProductFilter.SelectedIndex = 0; // "All Products"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = productRepository.GetAllProducts();
                cmbProductFilter.Items.Clear();
                cmbProductFilter.Items.Add("All Products");
                
                foreach (var product in products)
                {
                    cmbProductFilter.Items.Add($"{product.ProductCode} - {product.ProductName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateReport()
        {
            try
            {
                // Get filter values
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;
                string productFilter = cmbProductFilter.SelectedItem?.ToString() ?? "All Products";

                // Get data from repository
                var batchExpiryData = GetBatchExpiryData(startDate, endDate, productFilter);

                // Set up report data source
                ReportDataSource reportDataSource = new ReportDataSource("BatchExpiryDataSet", batchExpiryData);

                // Clear existing data sources
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                // Set report parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", startDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("EndDate", endDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("ProductFilter", productFilter)
                };

                reportViewer1.LocalReport.SetParameters(parameters);

                // Refresh the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetBatchExpiryData(DateTime startDate, DateTime endDate, string productFilter)
        {
            try
            {
                // Parse product filter
                int? productId = null;
                if (!string.IsNullOrEmpty(productFilter) && productFilter != "All Products")
                {
                    var productCode = productFilter.Split('-')[0].Trim();
                    var product = productRepository.GetProductByCode(productCode);
                    if (product != null)
                        productId = product.ProductId;
                }

                // Get products with expiry dates
                var products = productRepository.GetActiveProducts();
                
                // Filter by product if specified
                if (productId.HasValue)
                {
                    products = products.Where(p => p.ProductId == productId.Value).ToList();
                }

                // Filter products that have expiry dates within the date range
                var filteredProducts = products.Where(p => 
                    p.ExpiryDate.HasValue && 
                    p.ExpiryDate.Value >= startDate && 
                    p.ExpiryDate.Value <= endDate &&
                    p.StockQuantity > 0).ToList();

                // Create DataTable
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ProductId", typeof(int));
                dataTable.Columns.Add("ProductCode", typeof(string));
                dataTable.Columns.Add("ProductName", typeof(string));
                dataTable.Columns.Add("CategoryName", typeof(string));
                dataTable.Columns.Add("BrandName", typeof(string));
                dataTable.Columns.Add("WarehouseName", typeof(string));
                dataTable.Columns.Add("BatchNumber", typeof(string));
                dataTable.Columns.Add("ExpiryDate", typeof(DateTime));
                dataTable.Columns.Add("CurrentStock", typeof(decimal));
                dataTable.Columns.Add("UnitName", typeof(string));
                dataTable.Columns.Add("DaysToExpiry", typeof(int));
                dataTable.Columns.Add("ExpiryStatus", typeof(string));
                dataTable.Columns.Add("LastUpdated", typeof(DateTime));

                foreach (var product in filteredProducts)
                {
                    var daysToExpiry = (int)(product.ExpiryDate.Value - DateTime.Now).TotalDays;
                    string expiryStatus;
                    
                    if (daysToExpiry < 0)
                        expiryStatus = "Expired";
                    else if (daysToExpiry <= 30)
                        expiryStatus = "Expiring Soon";
                    else if (daysToExpiry <= 90)
                        expiryStatus = "Expiring in 3 Months";
                    else
                        expiryStatus = "Valid";

                    dataTable.Rows.Add(
                        product.ProductId,
                        product.ProductCode ?? "N/A",
                        product.ProductName ?? "Unknown Product",
                        "Category", // TODO: Get from Category table
                        "Brand",    // TODO: Get from Brand table
                        "Warehouse", // TODO: Get from Warehouse table
                        product.BatchNumber ?? "N/A",
                        product.ExpiryDate.Value,
                        product.StockQuantity,
                        "PCS", // TODO: Get from Unit table
                        daysToExpiry,
                        expiryStatus,
                        product.ModifiedDate ?? product.CreatedDate
                    );
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting batch expiry data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        private void btnExportToPDF_Click(object sender, EventArgs e)
        {
            try
            {
                ExportReport("PDF");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportReport("EXCEL");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportReport(string format)
        {
            try
            {
                if (reportViewer1.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate the report first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = format == "PDF" ? "PDF files (*.pdf)|*.pdf" : "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"BatchExpiryReport_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] reportBytes = reportViewer1.LocalReport.Render(format);
                    System.IO.File.WriteAllBytes(saveFileDialog.FileName, reportBytes);
                    MessageBox.Show($"Report exported successfully to {saveFileDialog.FileName}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BatchExpiryReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Set the report path
                reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Reports", "BatchExpiryReport.rdlc");
                
                // Set processing mode
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                
                // Refresh the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
