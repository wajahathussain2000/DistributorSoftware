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
        private ComboBox cmbCustomer;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private DataGridView dgvLedger;
        private Button btnLoad;
        private Button btnClear;
        private Button btnClose;
        private Label lblCustomer;
        private Label lblFromDate;
        private Label lblToDate;
        private GroupBox filterGroup;
        private GroupBox ledgerGroup;

        public CustomerLedgerForm()
        {
            InitializeComponent();
            InitializeForm();
            InitializeConnection();
            LoadCustomers();
            SetDefaultDates();
        }

        private void InitializeForm()
        {
            this.Text = "Customer Ledger - Distribution Software";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.ShowInTaskbar = true;

            // Create filter group
            filterGroup = new GroupBox();
            filterGroup.Text = "Filter Options";
            filterGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            filterGroup.ForeColor = Color.FromArgb(52, 73, 94);
            filterGroup.Location = new Point(20, 20);
            filterGroup.Size = new Size(1150, 120);
            filterGroup.BackColor = Color.White;

            // Customer selection
            lblCustomer = new Label();
            lblCustomer.Text = "Customer:";
            lblCustomer.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblCustomer.ForeColor = Color.FromArgb(52, 73, 94);
            lblCustomer.Location = new Point(20, 30);
            lblCustomer.Size = new Size(80, 25);

            cmbCustomer = new ComboBox();
            cmbCustomer.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            cmbCustomer.Location = new Point(110, 28);
            cmbCustomer.Size = new Size(300, 25);
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;

            // From Date
            lblFromDate = new Label();
            lblFromDate.Text = "From Date:";
            lblFromDate.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblFromDate.ForeColor = Color.FromArgb(52, 73, 94);
            lblFromDate.Location = new Point(450, 30);
            lblFromDate.Size = new Size(80, 25);

            dtpFromDate = new DateTimePicker();
            dtpFromDate.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dtpFromDate.Location = new Point(540, 28);
            dtpFromDate.Size = new Size(150, 25);
            dtpFromDate.Format = DateTimePickerFormat.Short;

            // To Date
            lblToDate = new Label();
            lblToDate.Text = "To Date:";
            lblToDate.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblToDate.ForeColor = Color.FromArgb(52, 73, 94);
            lblToDate.Location = new Point(720, 30);
            lblToDate.Size = new Size(80, 25);

            dtpToDate = new DateTimePicker();
            dtpToDate.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dtpToDate.Location = new Point(810, 28);
            dtpToDate.Size = new Size(150, 25);
            dtpToDate.Format = DateTimePickerFormat.Short;

            // Load button
            btnLoad = new Button();
            btnLoad.Text = "Load Ledger";
            btnLoad.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLoad.ForeColor = Color.White;
            btnLoad.BackColor = Color.FromArgb(52, 152, 219);
            btnLoad.Location = new Point(1000, 25);
            btnLoad.Size = new Size(120, 35);
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Click += BtnLoad_Click;

            // Clear button
            btnClear = new Button();
            btnClear.Text = "Clear";
            btnClear.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.BackColor = Color.FromArgb(241, 196, 15);
            btnClear.Location = new Point(1000, 70);
            btnClear.Size = new Size(120, 35);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Click += BtnClear_Click;

            // Close button
            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.BackColor = Color.FromArgb(231, 76, 60);
            btnClose.Location = new Point(1130, 25);
            btnClose.Size = new Size(80, 35);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Click += BtnClose_Click;

            // Add controls to filter group
            filterGroup.Controls.AddRange(new Control[] {
                lblCustomer, cmbCustomer, lblFromDate, dtpFromDate,
                lblToDate, dtpToDate, btnLoad, btnClear, btnClose
            });

            // Create ledger group
            ledgerGroup = new GroupBox();
            ledgerGroup.Text = "Customer Ledger";
            ledgerGroup.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            ledgerGroup.ForeColor = Color.FromArgb(52, 73, 94);
            ledgerGroup.Location = new Point(20, 160);
            ledgerGroup.Size = new Size(1150, 500);
            ledgerGroup.BackColor = Color.White;

            // DataGridView
            dgvLedger = new DataGridView();
            dgvLedger.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvLedger.Location = new Point(20, 30);
            dgvLedger.Size = new Size(1110, 450);
            dgvLedger.BackgroundColor = Color.White;
            dgvLedger.BorderStyle = BorderStyle.Fixed3D;
            dgvLedger.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLedger.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLedger.ReadOnly = true;
            dgvLedger.AllowUserToAddRows = false;
            dgvLedger.RowHeadersVisible = false;
            dgvLedger.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            // Add DataGridView to ledger group
            ledgerGroup.Controls.Add(dgvLedger);

            // Add groups to form
            this.Controls.AddRange(new Control[] { filterGroup, ledgerGroup });
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
