using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class ItemLedgerReportForm : Form
    {
        private ProductRepository productRepository;
        private WarehouseRepository warehouseRepository;
        private StockMovementRepository stockMovementRepository;

        public ItemLedgerReportForm()
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
                warehouseRepository = new WarehouseRepository();
                stockMovementRepository = new StockMovementRepository();
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

                // Load warehouses
                LoadWarehouses();

                // Set default filters
                cmbProductFilter.SelectedIndex = 0; // "All Products"
                cmbWarehouseFilter.SelectedIndex = 0; // "All Warehouses"
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

        private void LoadWarehouses()
        {
            try
            {
                var warehouses = warehouseRepository.GetAllWarehouses();
                cmbWarehouseFilter.Items.Clear();
                cmbWarehouseFilter.Items.Add("All Warehouses");
                
                foreach (var warehouse in warehouses)
                {
                    cmbWarehouseFilter.Items.Add(warehouse.WarehouseName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string warehouseFilter = cmbWarehouseFilter.SelectedItem?.ToString() ?? "All Warehouses";

                // Get data from repository
                var itemLedgerData = GetItemLedgerData(startDate, endDate, productFilter, warehouseFilter);

                // Set up report data source
                ReportDataSource reportDataSource = new ReportDataSource("ItemLedgerDataSet", itemLedgerData);

                // Clear existing data sources
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                // Set report parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", startDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("EndDate", endDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("ProductFilter", productFilter),
                    new ReportParameter("WarehouseFilter", warehouseFilter)
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

        private DataTable GetItemLedgerData(DateTime startDate, DateTime endDate, string productFilter, string warehouseFilter)
        {
            try
            {
                // Parse filters to get IDs
                int? productId = null;
                int? warehouseId = null;

                // Parse product filter
                if (!string.IsNullOrEmpty(productFilter) && productFilter != "All Products")
                {
                    var productCode = productFilter.Split('-')[0].Trim();
                    var product = productRepository.GetProductByCode(productCode);
                    if (product != null)
                        productId = product.ProductId;
                }

                // Parse warehouse filter
                if (!string.IsNullOrEmpty(warehouseFilter) && warehouseFilter != "All Warehouses")
                {
                    var warehouses = warehouseRepository.GetAllWarehouses();
                    var warehouse = warehouses.FirstOrDefault(w => w.WarehouseName == warehouseFilter);
                    if (warehouse != null)
                        warehouseId = warehouse.WarehouseId;
                }

                // Get real stock movement data from repository
                var stockMovements = stockMovementRepository.GetStockMovements(startDate, endDate, productId, warehouseId, null, null);

                // Create DataTable
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("TransactionId", typeof(int));
                dataTable.Columns.Add("TransactionDate", typeof(DateTime));
                dataTable.Columns.Add("ProductCode", typeof(string));
                dataTable.Columns.Add("ProductName", typeof(string));
                dataTable.Columns.Add("WarehouseName", typeof(string));
                dataTable.Columns.Add("TransactionType", typeof(string));
                dataTable.Columns.Add("ReferenceType", typeof(string));
                dataTable.Columns.Add("ReferenceNumber", typeof(string));
                dataTable.Columns.Add("InQuantity", typeof(decimal));
                dataTable.Columns.Add("OutQuantity", typeof(decimal));
                dataTable.Columns.Add("BalanceQuantity", typeof(decimal));
                dataTable.Columns.Add("UnitName", typeof(string));
                dataTable.Columns.Add("UnitPrice", typeof(decimal));
                dataTable.Columns.Add("TotalValue", typeof(decimal));
                dataTable.Columns.Add("BatchNumber", typeof(string));
                dataTable.Columns.Add("ExpiryDate", typeof(DateTime));
                dataTable.Columns.Add("Remarks", typeof(string));

                // Calculate running balance
                decimal runningBalance = 0;

                foreach (var movement in stockMovements.OrderBy(m => m.MovementDate).ThenBy(m => m.MovementId))
                {
                    decimal inQuantity = 0;
                    decimal outQuantity = 0;

                    if (movement.MovementType == "IN")
                    {
                        inQuantity = movement.Quantity;
                        runningBalance += movement.Quantity;
                    }
                    else if (movement.MovementType == "OUT")
                    {
                        outQuantity = movement.Quantity;
                        runningBalance -= movement.Quantity;
                    }

                    dataTable.Rows.Add(
                        movement.MovementId,
                        movement.MovementDate,
                        movement.ProductCode ?? "N/A",
                        movement.ProductName ?? "Unknown Product",
                        movement.WarehouseName ?? "Unknown Warehouse",
                        movement.MovementType ?? "N/A",
                        movement.ReferenceType ?? "N/A",
                        movement.ReferenceNumber ?? "N/A",
                        inQuantity,
                        outQuantity,
                        runningBalance,
                        "PCS", // TODO: Get from Unit table
                        movement.UnitPrice,
                        movement.TotalValue,
                        movement.BatchNumber ?? "N/A",
                        movement.ExpiryDate ?? DateTime.MinValue,
                        movement.Remarks ?? ""
                    );
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting item ledger data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                saveFileDialog.FileName = $"ItemLedgerReport_{DateTime.Now:yyyyMMdd_HHmmss}";

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

        private void ItemLedgerReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Set the report path
                reportViewer1.LocalReport.ReportPath = System.IO.Path.Combine(Application.StartupPath, "Reports", "ItemLedgerReport.rdlc");
                
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
