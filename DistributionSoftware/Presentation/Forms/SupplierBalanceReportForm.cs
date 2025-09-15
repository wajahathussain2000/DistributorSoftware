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
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SupplierBalanceReportForm : Form
    {
        private SupplierReportRepository supplierReportRepository;

        public SupplierBalanceReportForm()
        {
            InitializeComponent();
            InitializeRepository();
            LoadInitialData();
        }

        private void InitializeRepository()
        {
            try
            {
                supplierReportRepository = new SupplierReportRepository();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing repository: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInitialData()
        {
            try
            {
                // Set default date
                dtpAsOfDate.Value = DateTime.Now;

                // Load suppliers
                LoadSuppliers();

                // Set default filters
                cmbSupplierFilter.SelectedIndex = 0; // "All Suppliers"
                chkShowOnlyOutstanding.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = supplierReportRepository.GetActiveSuppliers();
                
                cmbSupplierFilter.Items.Clear();
                cmbSupplierFilter.Items.Add(new { Text = "All Suppliers", Value = (int?)null });
                
                foreach (var supplier in suppliers)
                {
                    cmbSupplierFilter.Items.Add(new { Text = $"{supplier.SupplierCode} - {supplier.SupplierName}", Value = (int?)supplier.SupplierId });
                }
                
                cmbSupplierFilter.DisplayMember = "Text";
                cmbSupplierFilter.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DateTime asOfDate = dtpAsOfDate.Value.Date;
                int? supplierId = null;
                bool showOnlyOutstanding = chkShowOnlyOutstanding.Checked;

                if (cmbSupplierFilter.SelectedItem != null)
                {
                    var selectedSupplier = cmbSupplierFilter.SelectedItem.GetType().GetProperty("Value").GetValue(cmbSupplierFilter.SelectedItem);
                    if (selectedSupplier != null)
                        supplierId = (int?)selectedSupplier;
                }

                // Get data from repository
                var balanceData = GetSupplierBalanceData(supplierId, asOfDate, showOnlyOutstanding);

                // Set up report data source
                ReportDataSource reportDataSource = new ReportDataSource("SupplierBalanceDataSet", balanceData);

                // Clear existing data sources
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                // Set report parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("AsOfDate", asOfDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("SupplierFilter", supplierId.HasValue ? (cmbSupplierFilter?.Text ?? "Unknown Supplier") : "All Suppliers"),
                    new ReportParameter("ShowOnlyOutstanding", showOnlyOutstanding ? "Yes" : "No")
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

        private DataTable GetSupplierBalanceData(int? supplierId, DateTime asOfDate, bool showOnlyOutstanding)
        {
            DataTable dt = new DataTable();
            
            try
            {
                // Get data from repository
                var reportData = supplierReportRepository.GetSupplierBalanceReportData(supplierId, asOfDate, showOnlyOutstanding);

                // Create DataTable structure
                dt.Columns.Add("SupplierId", typeof(int));
                dt.Columns.Add("SupplierCode", typeof(string));
                dt.Columns.Add("SupplierName", typeof(string));
                dt.Columns.Add("ContactPerson", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("State", typeof(string));
                dt.Columns.Add("PaymentTermsDays", typeof(int));
                dt.Columns.Add("TotalDebits", typeof(decimal));
                dt.Columns.Add("TotalCredits", typeof(decimal));
                dt.Columns.Add("CurrentBalance", typeof(decimal));
                dt.Columns.Add("LastTransactionDate", typeof(DateTime));
                dt.Columns.Add("TransactionCount", typeof(int));
                dt.Columns.Add("DaysOutstanding", typeof(int));
                dt.Columns.Add("AgingCategory", typeof(string));
                dt.Columns.Add("RiskLevel", typeof(string));
                dt.Columns.Add("PaymentStatus", typeof(string));

                // Add data rows
                foreach (var item in reportData)
                {
                    DataRow row = dt.NewRow();
                    row["SupplierId"] = item.SupplierId;
                    row["SupplierCode"] = item.SupplierCode;
                    row["SupplierName"] = item.SupplierName;
                    row["ContactPerson"] = item.ContactPerson;
                    row["Phone"] = item.Phone;
                    row["Email"] = item.Email;
                    row["City"] = item.City;
                    row["State"] = item.State;
                    row["PaymentTermsDays"] = item.PaymentTermsDays ?? 0;
                    row["TotalDebits"] = item.TotalDebits;
                    row["TotalCredits"] = item.TotalCredits;
                    row["CurrentBalance"] = item.CurrentBalance;
                    row["LastTransactionDate"] = item.LastTransactionDate ?? DateTime.MinValue;
                    row["TransactionCount"] = item.TransactionCount;
                    row["DaysOutstanding"] = item.DaysOutstanding;
                    row["AgingCategory"] = item.AgingCategory;
                    row["RiskLevel"] = item.RiskLevel;
                    row["PaymentStatus"] = item.PaymentStatus;
                    dt.Rows.Add(row);
                }

                // If no data found, add a "No data found" row
                if (dt.Rows.Count == 0)
                {
                    DataRow noDataRow = dt.NewRow();
                    noDataRow["SupplierId"] = 0;
                    noDataRow["SupplierCode"] = "N/A";
                    noDataRow["SupplierName"] = "N/A";
                    noDataRow["ContactPerson"] = "N/A";
                    noDataRow["Phone"] = "N/A";
                    noDataRow["Email"] = "N/A";
                    noDataRow["City"] = "N/A";
                    noDataRow["State"] = "N/A";
                    noDataRow["PaymentTermsDays"] = 0;
                    noDataRow["TotalDebits"] = 0;
                    noDataRow["TotalCredits"] = 0;
                    noDataRow["CurrentBalance"] = 0;
                    noDataRow["LastTransactionDate"] = DateTime.MinValue;
                    noDataRow["TransactionCount"] = 0;
                    noDataRow["DaysOutstanding"] = 0;
                    noDataRow["AgingCategory"] = "N/A";
                    noDataRow["RiskLevel"] = "N/A";
                    noDataRow["PaymentStatus"] = "N/A";
                    dt.Rows.Add(noDataRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving supplier balance data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CreateEmptyDataTable();
            }

            return dt;
        }

        private DataTable CreateEmptyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SupplierId", typeof(int));
            dt.Columns.Add("SupplierCode", typeof(string));
            dt.Columns.Add("SupplierName", typeof(string));
            dt.Columns.Add("ContactPerson", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("PaymentTermsDays", typeof(int));
            dt.Columns.Add("TotalDebits", typeof(decimal));
            dt.Columns.Add("TotalCredits", typeof(decimal));
            dt.Columns.Add("CurrentBalance", typeof(decimal));
            dt.Columns.Add("LastTransactionDate", typeof(DateTime));
            dt.Columns.Add("TransactionCount", typeof(int));
            dt.Columns.Add("DaysOutstanding", typeof(int));
            dt.Columns.Add("AgingCategory", typeof(string));
            dt.Columns.Add("RiskLevel", typeof(string));
            dt.Columns.Add("PaymentStatus", typeof(string));
            return dt;
        }

        private void btnExportToPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer1.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate the report first before exporting.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveDialog.FileName = $"SupplierBalanceReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] pdfBytes = reportViewer1.LocalReport.Render("PDF");
                    File.WriteAllBytes(saveDialog.FileName, pdfBytes);
                    MessageBox.Show($"Report exported successfully to: {saveDialog.FileName}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer1.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate the report first before exporting.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FileName = $"SupplierBalanceReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] excelBytes = reportViewer1.LocalReport.Render("EXCELOPENXML");
                    File.WriteAllBytes(saveDialog.FileName, excelBytes);
                    MessageBox.Show($"Report exported successfully to: {saveDialog.FileName}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SupplierBalanceReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Set up the report viewer
                reportViewer1.LocalReport.ReportPath = Path.Combine(Application.StartupPath, "Reports", "SupplierBalanceReport.rdlc");
                reportViewer1.LocalReport.EnableExternalImages = true;
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report viewer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
