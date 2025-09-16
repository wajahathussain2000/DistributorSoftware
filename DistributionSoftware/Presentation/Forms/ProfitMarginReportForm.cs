using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class ProfitMarginReportForm : Form
    {
        private ReportViewer reportViewer;
        private ComboBox cmbProduct;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnGenerateReport;
        private Button btnExportPDF;
        private Button btnClose;

        public ProfitMarginReportForm()
        {
            InitializeComponent();
            InitializeFormControls();
            InitializeReportViewer();
            LoadProducts();
            SetDefaultDates();
        }

        private void InitializeFormControls()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Profit Margin Report";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 600);
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Create filter panel
            Panel filterPanel = new Panel();
            filterPanel.Location = new Point(10, 10);
            filterPanel.Size = new Size(this.ClientSize.Width - 20, 140);
            filterPanel.BackColor = Color.White;
            filterPanel.BorderStyle = BorderStyle.FixedSingle;

            // Product filter
            Label lblProduct = new Label();
            lblProduct.Text = "Product:";
            lblProduct.Location = new Point(20, 20);
            lblProduct.Size = new Size(80, 20);
            lblProduct.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbProduct = new ComboBox();
            cmbProduct.Location = new Point(20, 40);
            cmbProduct.Size = new Size(300, 25);
            cmbProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProduct.Font = new Font("Arial", 9);

            // Start Date filter
            Label lblStartDate = new Label();
            lblStartDate.Text = "Start Date:";
            lblStartDate.Location = new Point(340, 20);
            lblStartDate.Size = new Size(80, 20);
            lblStartDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpStartDate = new DateTimePicker();
            dtpStartDate.Location = new Point(340, 40);
            dtpStartDate.Size = new Size(120, 25);
            dtpStartDate.Font = new Font("Arial", 9);
            dtpStartDate.Format = DateTimePickerFormat.Short;

            // End Date filter
            Label lblEndDate = new Label();
            lblEndDate.Text = "End Date:";
            lblEndDate.Location = new Point(480, 20);
            lblEndDate.Size = new Size(80, 20);
            lblEndDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpEndDate = new DateTimePicker();
            dtpEndDate.Location = new Point(480, 40);
            dtpEndDate.Size = new Size(120, 25);
            dtpEndDate.Font = new Font("Arial", 9);
            dtpEndDate.Format = DateTimePickerFormat.Short;

            // Buttons
            btnGenerateReport = new Button();
            btnGenerateReport.Text = "Generate Report";
            btnGenerateReport.Location = new Point(620, 35);
            btnGenerateReport.Size = new Size(120, 35);
            btnGenerateReport.BackColor = Color.FromArgb(52, 152, 219);
            btnGenerateReport.ForeColor = Color.White;
            btnGenerateReport.FlatStyle = FlatStyle.Flat;
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.Font = new Font("Arial", 9, FontStyle.Bold);
            btnGenerateReport.Click += btnGenerateReport_Click;

            btnExportPDF = new Button();
            btnExportPDF.Text = "Export PDF";
            btnExportPDF.Location = new Point(750, 35);
            btnExportPDF.Size = new Size(100, 35);
            btnExportPDF.BackColor = Color.FromArgb(46, 204, 113);
            btnExportPDF.ForeColor = Color.White;
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.FlatAppearance.BorderSize = 0;
            btnExportPDF.Font = new Font("Arial", 9, FontStyle.Bold);
            btnExportPDF.Click += btnExportPDF_Click;

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(860, 35);
            btnClose.Size = new Size(80, 35);
            btnClose.BackColor = Color.FromArgb(231, 76, 60);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Font = new Font("Arial", 9, FontStyle.Bold);
            btnClose.Click += btnClose_Click;

            // Add controls to filter panel
            filterPanel.Controls.AddRange(new Control[] {
                lblProduct, cmbProduct,
                lblStartDate, dtpStartDate,
                lblEndDate, dtpEndDate,
                btnGenerateReport, btnExportPDF, btnClose
            });

            // Add filter panel to form
            this.Controls.Add(filterPanel);

            this.ResumeLayout(false);
        }

        private void InitializeReportViewer()
        {
            try
            {
                reportViewer = new ReportViewer();
                reportViewer.Location = new Point(0, 160);
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 160);
                reportViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.BackColor = Color.White;
                reportViewer.BorderStyle = BorderStyle.None;
                reportViewer.ZoomMode = ZoomMode.PageWidth;
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                
                string reportPath = FindReportPath();
                if (!string.IsNullOrEmpty(reportPath))
                {
                    reportViewer.LocalReport.ReportPath = reportPath;
                }
                else
                {
                    MessageBox.Show("Could not find the RDLC report file. Please ensure ProfitMarginReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                this.Controls.Add(reportViewer);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing ReportViewer: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FindReportPath()
        {
            string[] possiblePaths = {
                Path.Combine(Application.StartupPath, "Reports", "ProfitMarginReport.rdlc"),
                Path.Combine(Application.StartupPath, "bin", "Debug", "Reports", "ProfitMarginReport.rdlc"),
                Path.Combine(Application.StartupPath, "bin", "Release", "Reports", "ProfitMarginReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ProfitMarginReport.rdlc")
            };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return string.Empty;
        }

        private void LoadProducts()
        {
            try
            {
                string connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ProductId, ProductName FROM Products WHERE IsActive = 1 ORDER BY ProductName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            cmbProduct.Items.Add(new { ProductId = -1, ProductName = "All Products" });
                            while (reader.Read())
                            {
                                cmbProduct.Items.Add(new { ProductId = reader.GetInt32(0), ProductName = reader.GetString(1) });
                            }
                        }
                    }
                }
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ProductId";
                cmbProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultDates()
        {
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpEndDate.Value = DateTime.Now;
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

                int? productId = cmbProduct.SelectedValue != null && (int)cmbProduct.SelectedValue != -1 ? (int?)cmbProduct.SelectedValue : null;

                DataTable reportData = GetProfitMarginReportData(dtpStartDate.Value, dtpEndDate.Value, productId);

                if (reportData == null)
                {
                    reportData = CreateEmptyDataTable();
                }

                if (reportData.Rows.Count == 0)
                {
                    DataRow noDataRow = reportData.NewRow();
                    noDataRow["ProductId"] = 0;
                    noDataRow["ProductCode"] = "N/A";
                    noDataRow["ProductName"] = "No data found";
                    noDataRow["CategoryName"] = "N/A";
                    noDataRow["BrandName"] = "N/A";
                    noDataRow["TotalQuantitySold"] = 0;
                    noDataRow["TotalSalesAmount"] = 0;
                    noDataRow["TotalProfitAmount"] = 0;
                    noDataRow["ProfitMarginPercentage"] = 0;
                    noDataRow["ProfitCategory"] = "N/A";
                    noDataRow["MarginCategory"] = "N/A";
                    reportData.Rows.Add(noDataRow);
                }

                reportViewer.LocalReport.DataSources.Clear();
                ReportDataSource reportDataSource = new ReportDataSource("ProfitMarginDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", dtpStartDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("EndDate", dtpEndDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("ProductFilter", productId.HasValue ? (cmbProduct?.Text ?? "Unknown Product") : "All Products")
                };
                
                try
                {
                    reportViewer.LocalReport.SetParameters(parameters);
                }
                catch (Exception paramEx)
                {
                    MessageBox.Show($"Error setting report parameters: {paramEx.Message}", "Parameter Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Report Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetProfitMarginReportData(DateTime startDate, DateTime endDate, int? productId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetProfitMarginReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@ProductId", productId.HasValue ? (object)productId.Value : DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving report data: {ex.Message}", "Data Retrieval Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return dataTable;
        }

        private DataTable CreateEmptyDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ProductId", typeof(int));
            dataTable.Columns.Add("ProductCode", typeof(string));
            dataTable.Columns.Add("ProductName", typeof(string));
            dataTable.Columns.Add("CategoryName", typeof(string));
            dataTable.Columns.Add("BrandName", typeof(string));
            dataTable.Columns.Add("TotalQuantitySold", typeof(decimal));
            dataTable.Columns.Add("TotalSalesAmount", typeof(decimal));
            dataTable.Columns.Add("TotalProfitAmount", typeof(decimal));
            dataTable.Columns.Add("ProfitMarginPercentage", typeof(decimal));
            dataTable.Columns.Add("ProfitCategory", typeof(string));
            dataTable.Columns.Add("MarginCategory", typeof(string));
            return dataTable;
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate the report first before exporting.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.FileName = $"ProfitMarginReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;

                    byte[] reportBytes = reportViewer.LocalReport.Render(
                        "PDF", null, out mimeType, out encoding, out fileNameExtension, out streamIds, out warnings);

                    File.WriteAllBytes(saveFileDialog.FileName, reportBytes);
                    MessageBox.Show($"Report exported successfully to: {saveFileDialog.FileName}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
