using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class CustomerLedgerForm : Form
    {
        private string connectionString;

        public CustomerLedgerForm()
        {
            InitializeComponent();
            InitializeConnection();
            LoadCustomers();
            SetDefaultDates();
        }


        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
        }

        private void LoadCustomers()
        {
            try
            {
                string query = @"
                    SELECT CustomerId, CustomerName 
                    FROM Customers 
                    WHERE IsActive = 1 
                    ORDER BY CustomerName";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        var adapter = new SqlDataAdapter(command);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmbCustomer.DataSource = dataTable;
                        cmbCustomer.DisplayMember = "CustomerName";
                        cmbCustomer.ValueMember = "CustomerId";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultDates()
        {
            dtpFromDate.Value = new DateTime(2025, 1, 1); // Start of 2025 to include test data
            dtpToDate.Value = DateTime.Now; // Current date
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadCustomerLedger();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            cmbCustomer.SelectedIndex = -1;
            SetDefaultDates();
            dgvLedger.DataSource = null;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadCustomerLedger()
        {
            try
            {
                int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                string fromDate = dtpFromDate.Value.ToString("yyyy-MM-dd");
                string toDate = dtpToDate.Value.ToString("yyyy-MM-dd");

                System.Diagnostics.Debug.WriteLine($"Loading ledger for CustomerId: {customerId}, From: {fromDate}, To: {toDate}");

                string query = @"
                    SELECT 
                        cl.TransactionDate,
                        cl.TransactionType,
                        cl.ReferenceNo,
                        cl.DebitAmount,
                        cl.CreditAmount,
                        cl.Balance,
                        cl.Remarks
                    FROM CustomerLedger cl
                    WHERE cl.CustomerId = @CustomerId
                        AND cl.TransactionDate >= @FromDate
                        AND cl.TransactionDate <= @ToDate
                    ORDER BY cl.TransactionDate DESC";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerId);
                        command.Parameters.AddWithValue("@FromDate", fromDate);
                        command.Parameters.AddWithValue("@ToDate", toDate);

                        var adapter = new SqlDataAdapter(command);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Configure columns
                        if (dataTable.Columns.Count > 0)
                        {
                            dataTable.Columns["TransactionDate"].ColumnName = "Transaction Date";
                            dataTable.Columns["TransactionType"].ColumnName = "Type";
                            dataTable.Columns["ReferenceNo"].ColumnName = "Reference No";
                            dataTable.Columns["DebitAmount"].ColumnName = "Debit";
                            dataTable.Columns["CreditAmount"].ColumnName = "Credit";
                            dataTable.Columns["Balance"].ColumnName = "Balance";
                            dataTable.Columns["Remarks"].ColumnName = "Remarks";

                            // Format currency columns
                            if (dataTable.Columns.Contains("Debit"))
                                dataTable.Columns["Debit"].DataType = typeof(decimal);
                            if (dataTable.Columns.Contains("Credit"))
                                dataTable.Columns["Credit"].DataType = typeof(decimal);
                            if (dataTable.Columns.Contains("Balance"))
                                dataTable.Columns["Balance"].DataType = typeof(decimal);
                        }

                        dgvLedger.DataSource = dataTable;

                        System.Diagnostics.Debug.WriteLine($"Loaded {dataTable.Rows.Count} records for customer {customerId}");

                        // Format currency columns in DataGridView
                        if (dgvLedger.Columns.Contains("Debit"))
                            dgvLedger.Columns["Debit"].DefaultCellStyle.Format = "C2";
                        if (dgvLedger.Columns.Contains("Credit"))
                            dgvLedger.Columns["Credit"].DefaultCellStyle.Format = "C2";
                        if (dgvLedger.Columns.Contains("Balance"))
                            dgvLedger.Columns["Balance"].DefaultCellStyle.Format = "C2";

                        // Format date column
                        if (dgvLedger.Columns.Contains("Transaction Date"))
                            dgvLedger.Columns["Transaction Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer ledger: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
