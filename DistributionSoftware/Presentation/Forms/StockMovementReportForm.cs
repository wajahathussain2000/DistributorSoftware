using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.Models;
using DistributionSoftware.Common;
using System.Diagnostics;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class StockMovementReportForm : Form
    {
        private IStockMovementService _stockMovementService;
        private DataTable stockMovementDataTable;
        private List<StockMovement> stockMovements;
        private StockMovementReportFilter currentFilter;

        public StockMovementReportForm()
        {
            try
            {
                Debug.WriteLine("StockMovementReportForm: Initializing form");
                InitializeComponent();
                InitializeServices();
                InitializeDataTable();
                LoadInitialData();
                Debug.WriteLine("StockMovementReportForm: Form initialized successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"StockMovementReportForm: Initialization error - {ex.Message}");
                MessageBox.Show($"Error initializing Stock Movement Report Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeServices()
        {
            _stockMovementService = new StockMovementService();
        }


        private void InitializeFilterControls()
        {
            // Create filter panel
            var filterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 200,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            // Date range controls
            var lblFromDate = new Label
            {
                Text = "From Date:",
                Location = new Point(10, 15),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            dtpFromDate = new DateTimePicker
            {
                Location = new Point(100, 12),
                Size = new Size(120, 25),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today.AddDays(-30)
            };

            var lblToDate = new Label
            {
                Text = "To Date:",
                Location = new Point(240, 15),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            dtpToDate = new DateTimePicker
            {
                Location = new Point(330, 12),
                Size = new Size(120, 25),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };

            // Product filter
            var lblProduct = new Label
            {
                Text = "Product:",
                Location = new Point(10, 50),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbProduct = new ComboBox
            {
                Location = new Point(100, 47),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Warehouse filter
            var lblWarehouse = new Label
            {
                Text = "Warehouse:",
                Location = new Point(320, 50),
                Size = new Size(80, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbWarehouse = new ComboBox
            {
                Location = new Point(410, 47),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Movement type filter
            var lblMovementType = new Label
            {
                Text = "Movement Type:",
                Location = new Point(10, 85),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbMovementType = new ComboBox
            {
                Location = new Point(120, 82),
                Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Reference type filter
            var lblReferenceType = new Label
            {
                Text = "Reference Type:",
                Location = new Point(260, 85),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            cmbReferenceType = new ComboBox
            {
                Location = new Point(370, 82),
                Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Batch number filter
            var lblBatchNumber = new Label
            {
                Text = "Batch Number:",
                Location = new Point(10, 120),
                Size = new Size(100, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            txtBatchNumber = new TextBox
            {
                Location = new Point(120, 117),
                Size = new Size(120, 25)
            };

            // Search button
            btnSearch = new Button
            {
                Text = "Search",
                Location = new Point(500, 115),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnSearch.Click += BtnSearch_Click;

            // Clear button
            btnClear = new Button
            {
                Text = "Clear",
                Location = new Point(590, 115),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnClear.Click += BtnClear_Click;

            // Add controls to filter panel
            filterPanel.Controls.AddRange(new Control[] {
                lblFromDate, dtpFromDate, lblToDate, dtpToDate,
                lblProduct, cmbProduct, lblWarehouse, cmbWarehouse,
                lblMovementType, cmbMovementType, lblReferenceType, cmbReferenceType,
                lblBatchNumber, txtBatchNumber, btnSearch, btnClear
            });

            this.Controls.Add(filterPanel);
        }

        private void InitializeDataGrid()
        {
            dgvMovements = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };

            // Style the data grid
            dgvMovements.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dgvMovements.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMovements.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvMovements.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);

            this.Controls.Add(dgvMovements);
        }

        private void InitializeButtons()
        {
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            // Export button
            btnExport = new Button
            {
                Text = "Export to Excel",
                Location = new Point(10, 15),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnExport.Click += BtnExport_Click;

            // Print button
            btnPrint = new Button
            {
                Text = "Print Report",
                Location = new Point(140, 15),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnPrint.Click += BtnPrint_Click;

            // Close button
            btnClose = new Button
            {
                Text = "Close",
                Location = new Point(270, 15),
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnClose.Click += BtnClose_Click;

            buttonPanel.Controls.AddRange(new Control[] { btnExport, btnPrint, btnClose });
            this.Controls.Add(buttonPanel);
        }

        private void InitializeDataTable()
        {
            stockMovementDataTable = new DataTable();
            stockMovementDataTable.Columns.Add("MovementId", typeof(int));
            stockMovementDataTable.Columns.Add("MovementDate", typeof(DateTime));
            stockMovementDataTable.Columns.Add("ProductCode", typeof(string));
            stockMovementDataTable.Columns.Add("ProductName", typeof(string));
            stockMovementDataTable.Columns.Add("WarehouseName", typeof(string));
            stockMovementDataTable.Columns.Add("MovementType", typeof(string));
            stockMovementDataTable.Columns.Add("Quantity", typeof(decimal));
            stockMovementDataTable.Columns.Add("ReferenceType", typeof(string));
            stockMovementDataTable.Columns.Add("ReferenceNumber", typeof(string));
            stockMovementDataTable.Columns.Add("BatchNumber", typeof(string));
            stockMovementDataTable.Columns.Add("ExpiryDate", typeof(DateTime));
            stockMovementDataTable.Columns.Add("UnitPrice", typeof(decimal));
            stockMovementDataTable.Columns.Add("TotalValue", typeof(decimal));
            stockMovementDataTable.Columns.Add("SupplierName", typeof(string));
            stockMovementDataTable.Columns.Add("CustomerName", typeof(string));
            stockMovementDataTable.Columns.Add("CreatedByUser", typeof(string));
            stockMovementDataTable.Columns.Add("Remarks", typeof(string));
        }

        private void LoadInitialData()
        {
            try
            {
                LoadProducts();
                LoadWarehouses();
                LoadMovementTypes();
                LoadReferenceTypes();
                LoadStockMovements();
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
                using (var connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.DistributionConnectionString))
                {
                    var query = "SELECT ProductId, ProductCode, ProductName FROM Products WHERE IsActive = 1 ORDER BY ProductName";
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            cmbProduct.Items.Clear();
                            cmbProduct.Items.Add(new ComboBoxItem("All Products", 0));
                            
                            while (reader.Read())
                            {
                                cmbProduct.Items.Add(new ComboBoxItem(
                                    $"{reader["ProductCode"]} - {reader["ProductName"]}", 
                                    Convert.ToInt32(reader["ProductId"])));
                            }
                        }
                    }
                }
                cmbProduct.SelectedIndex = 0;
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
                using (var connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.DistributionConnectionString))
                {
                    var query = "SELECT WarehouseId, WarehouseName FROM Warehouses WHERE IsActive = 1 ORDER BY WarehouseName";
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            cmbWarehouse.Items.Clear();
                            cmbWarehouse.Items.Add(new ComboBoxItem("All Warehouses", 0));
                            
                            while (reader.Read())
                            {
                                cmbWarehouse.Items.Add(new ComboBoxItem(
                                    reader["WarehouseName"].ToString(), 
                                    Convert.ToInt32(reader["WarehouseId"])));
                            }
                        }
                    }
                }
                cmbWarehouse.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMovementTypes()
        {
            try
            {
                var movementTypes = _stockMovementService.GetMovementTypes();
                cmbMovementType.Items.Clear();
                cmbMovementType.Items.Add("All Types");
                
                foreach (var type in movementTypes)
                {
                    cmbMovementType.Items.Add(type);
                }
                cmbMovementType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading movement types: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReferenceTypes()
        {
            try
            {
                var referenceTypes = _stockMovementService.GetReferenceTypes();
                cmbReferenceType.Items.Clear();
                cmbReferenceType.Items.Add("All Types");
                
                foreach (var type in referenceTypes)
                {
                    cmbReferenceType.Items.Add(type);
                }
                cmbReferenceType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reference types: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStockMovements()
        {
            try
            {
                currentFilter = new StockMovementReportFilter
                {
                    FromDate = dtpFromDate.Value.Date,
                    ToDate = dtpToDate.Value.Date,
                    ProductId = GetSelectedProductId(),
                    WarehouseId = GetSelectedWarehouseId(),
                    MovementType = GetSelectedMovementType(),
                    ReferenceType = GetSelectedReferenceType(),
                    BatchNumber = txtBatchNumber.Text.Trim()
                };

                stockMovements = _stockMovementService.GetStockMovementReport(currentFilter);
                
                if (stockMovements.Count == 0)
                {
                    // Check if we should create sample data
                    var result = MessageBox.Show(
                        "No stock movement data found. Would you like to create some sample data for testing?",
                        "No Data Found",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            if (_stockMovementService.CreateSampleData())
                            {
                                MessageBox.Show("Sample data created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Reload the data
                                stockMovements = _stockMovementService.GetStockMovementReport(currentFilter);
                            }
                            else
                            {
                                MessageBox.Show("Could not create sample data. Please ensure you have products and warehouses in the system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error creating sample data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                
                PopulateDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stock movements: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateDataGrid()
        {
            try
            {
                stockMovementDataTable.Clear();
                
                foreach (var movement in stockMovements)
                {
                    var row = stockMovementDataTable.NewRow();
                    row["MovementId"] = movement.MovementId;
                    row["MovementDate"] = movement.MovementDate;
                    row["ProductCode"] = movement.ProductCode;
                    row["ProductName"] = movement.ProductName;
                    row["WarehouseName"] = movement.WarehouseName;
                    row["MovementType"] = movement.MovementType;
                    row["Quantity"] = movement.Quantity;
                    row["ReferenceType"] = movement.ReferenceType ?? "";
                    row["ReferenceNumber"] = movement.ReferenceNumber ?? "";
                    row["BatchNumber"] = movement.BatchNumber ?? "";
                    row["ExpiryDate"] = movement.ExpiryDate.HasValue ? (object)movement.ExpiryDate.Value : DBNull.Value;
                    row["UnitPrice"] = movement.UnitPrice;
                    row["TotalValue"] = movement.TotalValue;
                    row["SupplierName"] = movement.SupplierName ?? "";
                    row["CustomerName"] = movement.CustomerName ?? "";
                    row["CreatedByUser"] = movement.CreatedByUser ?? "";
                    row["Remarks"] = movement.Remarks ?? "";
                    
                    stockMovementDataTable.Rows.Add(row);
                }

                dgvMovements.DataSource = stockMovementDataTable;
                
                // Format columns
                FormatDataGridColumns();
                
                // Update status
                this.Text = $"Stock Movement Report - {stockMovements.Count} records found";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating data grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridColumns()
        {
            if (dgvMovements.Columns.Count > 0)
            {
                dgvMovements.Columns["MovementId"].Visible = false;
                dgvMovements.Columns["MovementDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvMovements.Columns["Quantity"].DefaultCellStyle.Format = "N2";
                dgvMovements.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
                dgvMovements.Columns["TotalValue"].DefaultCellStyle.Format = "C2";
                dgvMovements.Columns["ExpiryDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                
                // Set column headers
                dgvMovements.Columns["MovementDate"].HeaderText = "Date";
                dgvMovements.Columns["ProductCode"].HeaderText = "Product Code";
                dgvMovements.Columns["ProductName"].HeaderText = "Product Name";
                dgvMovements.Columns["WarehouseName"].HeaderText = "Warehouse";
                dgvMovements.Columns["MovementType"].HeaderText = "Type";
                dgvMovements.Columns["ReferenceType"].HeaderText = "Ref. Type";
                dgvMovements.Columns["ReferenceNumber"].HeaderText = "Reference";
                dgvMovements.Columns["BatchNumber"].HeaderText = "Batch";
                dgvMovements.Columns["ExpiryDate"].HeaderText = "Expiry";
                dgvMovements.Columns["UnitPrice"].HeaderText = "Unit Price";
                dgvMovements.Columns["TotalValue"].HeaderText = "Total Value";
                dgvMovements.Columns["SupplierName"].HeaderText = "Supplier";
                dgvMovements.Columns["CustomerName"].HeaderText = "Customer";
                dgvMovements.Columns["CreatedByUser"].HeaderText = "Created By";
            }
        }

        private int? GetSelectedProductId()
        {
            if (cmbProduct.SelectedItem is ComboBoxItem item && item.Value > 0)
                return item.Value;
            return null;
        }

        private int? GetSelectedWarehouseId()
        {
            if (cmbWarehouse.SelectedItem is ComboBoxItem item && item.Value > 0)
                return item.Value;
            return null;
        }

        private string GetSelectedMovementType()
        {
            if (cmbMovementType.SelectedIndex > 0)
                return cmbMovementType.SelectedItem.ToString();
            return null;
        }

        private string GetSelectedReferenceType()
        {
            if (cmbReferenceType.SelectedIndex > 0)
                return cmbReferenceType.SelectedItem.ToString();
            return null;
        }


        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (stockMovements == null || stockMovements.Count == 0)
                {
                    MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv",
                    FileName = $"StockMovementReport_{DateTime.Now:yyyyMMdd_HHmmss}"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToFile(saveFileDialog.FileName);
                    MessageBox.Show("Report exported successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToFile(string fileName)
        {
            // Simple CSV export implementation
            var csv = new StringBuilder();
            
            // Add headers
            csv.AppendLine("Date,Product Code,Product Name,Warehouse,Type,Quantity,Ref. Type,Reference,Batch,Expiry,Unit Price,Total Value,Supplier,Customer,Created By,Remarks");
            
            // Add data
            foreach (var movement in stockMovements)
            {
                csv.AppendLine($"{movement.MovementDate:dd/MM/yyyy HH:mm}," +
                             $"{movement.ProductCode}," +
                             $"\"{movement.ProductName}\"," +
                             $"{movement.WarehouseName}," +
                             $"{movement.MovementType}," +
                             $"{movement.Quantity:N2}," +
                             $"{movement.ReferenceType ?? ""}," +
                             $"{movement.ReferenceNumber ?? ""}," +
                             $"{movement.BatchNumber ?? ""}," +
                             $"{(movement.ExpiryDate?.ToString("dd/MM/yyyy") ?? "")}," +
                             $"{movement.UnitPrice:C2}," +
                             $"{movement.TotalValue:C2}," +
                             $"{movement.SupplierName ?? ""}," +
                             $"{movement.CustomerName ?? ""}," +
                             $"{movement.CreatedByUser ?? ""}," +
                             $"\"{movement.Remarks ?? ""}\"");
            }
            
            System.IO.File.WriteAllText(fileName, csv.ToString());
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            // Generate report functionality
            LoadStockMovements();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // Search functionality
            LoadStockMovements();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            // Clear filters
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;
            cmbProduct.SelectedIndex = -1;
            cmbWarehouse.SelectedIndex = -1;
            cmbMovementType.SelectedIndex = -1;
            cmbReferenceType.SelectedIndex = -1;
            txtBatchNumber.Clear();
            
            // Clear data grid
            dgvMovements.DataSource = null;
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            // Print functionality
            MessageBox.Show("Print functionality not implemented yet.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }

    // Helper class for ComboBox items
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public ComboBoxItem(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
