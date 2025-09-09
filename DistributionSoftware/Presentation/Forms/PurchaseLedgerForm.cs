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

        public PurchaseLedgerForm()
        {
            InitializeComponent();
            InitializeConnection();
            LoadSuppliers();
            SetDefaultDates();
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
