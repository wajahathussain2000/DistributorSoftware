using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class PurchaseLedgerForm : Form
    {
        private string connectionString;
        private ComboBox cmbSupplier;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private DataGridView dgvLedger;
        private Button btnLoad;
        private Button btnClear;
        private Button btnClose;
        private Label lblSupplier;
        private Label lblFromDate;
        private Label lblToDate;
        private GroupBox filterGroup;
        private GroupBox ledgerGroup;

        public PurchaseLedgerForm()
        {
            InitializeComponent();
            InitializeForm();
            InitializeConnection();
            LoadSuppliers();
            SetDefaultDates();
        }

        private void InitializeForm()
        {
            this.Text = "Purchase Ledger - Distribution Software";
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

            // Supplier selection
            lblSupplier = new Label();
            lblSupplier.Text = "Supplier:";
            lblSupplier.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblSupplier.ForeColor = Color.FromArgb(52, 73, 94);
            lblSupplier.Location = new Point(20, 30);
            lblSupplier.Size = new Size(80, 25);

            cmbSupplier = new ComboBox();
            cmbSupplier.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            cmbSupplier.Location = new Point(110, 28);
            cmbSupplier.Size = new Size(300, 25);
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

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
                lblSupplier, cmbSupplier, lblFromDate, dtpFromDate,
                lblToDate, dtpToDate, btnLoad, btnClear, btnClose
            });

            // Create ledger group
            ledgerGroup = new GroupBox();
            ledgerGroup.Text = "Purchase Ledger";
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

        private void LoadSuppliers()
        {
            try
            {
                string query = @"
                    SELECT SupplierId, SupplierName 
                    FROM Suppliers 
                    WHERE IsActive = 1 
                    ORDER BY SupplierName";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        var adapter = new SqlDataAdapter(command);
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        cmbSupplier.DataSource = dataTable;
                        cmbSupplier.DisplayMember = "SupplierName";
                        cmbSupplier.ValueMember = "SupplierId";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultDates()
        {
            dtpFromDate.Value = new DateTime(2025, 9, 1); // September 2025 to include actual data
            dtpToDate.Value = DateTime.Now; // Current date
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedValue == null)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadPurchaseLedger();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            cmbSupplier.SelectedIndex = -1;
            SetDefaultDates();
            dgvLedger.DataSource = null;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadPurchaseLedger()
        {
            try
            {
                int supplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
                string fromDate = dtpFromDate.Value.ToString("yyyy-MM-dd");
                string toDate = dtpToDate.Value.ToString("yyyy-MM-dd");

                System.Diagnostics.Debug.WriteLine($"Loading purchase ledger for SupplierId: {supplierId}, From: {fromDate}, To: {toDate}");

                // Create a comprehensive query that combines all purchase-related transactions
                string query = @"
                    WITH PurchaseTransactions AS (
                        -- Purchase Invoices (Debit - Money owed to supplier)
                        SELECT 
                            pi.InvoiceDate as TransactionDate,
                            'Purchase Invoice' as TransactionType,
                            pi.InvoiceNumber as ReferenceNo,
                            pi.TotalAmount as DebitAmount,
                            0.00 as CreditAmount,
                            pi.BalanceAmount as Balance,
                            'Purchase Invoice - ' + ISNULL(pi.Remarks, '') as Remarks
                        FROM PurchaseInvoices pi
                        WHERE pi.SupplierId = @SupplierId
                            AND pi.InvoiceDate >= @FromDate
                            AND pi.InvoiceDate <= @ToDate
                        
                        UNION ALL
                        
                        -- Purchase Returns (Credit - Money credited back from supplier)
                        SELECT 
                            pr.ReturnDate as TransactionDate,
                            'Purchase Return' as TransactionType,
                            pr.ReturnNo as ReferenceNo,
                            0.00 as DebitAmount,
                            pr.NetReturnAmount as CreditAmount,
                            0.00 as Balance, -- Will be calculated
                            'Purchase Return - ' + ISNULL(pr.Reason, '') as Remarks
                        FROM PurchaseReturns pr
                        WHERE pr.SupplierId = @SupplierId
                            AND pr.ReturnDate >= @FromDate
                            AND pr.ReturnDate <= @ToDate
                        
                        UNION ALL
                        
                        -- Supplier Payments (Credit - Money paid to supplier)
                        SELECT 
                            sp.PaymentDate as TransactionDate,
                            'Payment' as TransactionType,
                            sp.PaymentNumber as ReferenceNo,
                            0.00 as DebitAmount,
                            sp.PaymentAmount as CreditAmount,
                            0.00 as Balance, -- Will be calculated
                            'Payment - ' + ISNULL(sp.Notes, '') as Remarks
                        FROM SupplierPayments sp
                        WHERE sp.SupplierId = @SupplierId
                            AND sp.PaymentDate >= @FromDate
                            AND sp.PaymentDate <= @ToDate
                            AND sp.IsActive = 1
                    )
                    SELECT 
                        TransactionDate,
                        TransactionType,
                        ReferenceNo,
                        DebitAmount,
                        CreditAmount,
                        Balance,
                        Remarks
                    FROM PurchaseTransactions
                    ORDER BY TransactionDate DESC";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
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

                        System.Diagnostics.Debug.WriteLine($"Loaded {dataTable.Rows.Count} records for supplier {supplierId}");

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
                MessageBox.Show($"Error loading purchase ledger: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
