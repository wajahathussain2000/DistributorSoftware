using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SupplierLedgerForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private DataTable supplierLedgerData;

        // Form Controls
        private ComboBox cmbSupplier;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private ComboBox cmbTransactionType;
        private DataGridView dgvLedger;
        private Label lblCurrentBalance;
        private Label lblTotalDebits;
        private Label lblTotalCredits;
        private Button btnGenerateReport;
        private Button btnExportPDF;
        private Button btnClose;

        public SupplierLedgerForm()
        {
            try
            {
                InitializeComponent();
                InitializeConnection();
                LoadSuppliers();
                LoadTransactionTypes();
                SetDefaultDates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Supplier Ledger Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DistributionConnection"]?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection string not found.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            connection = new SqlConnection(connectionString);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Supplier Ledger - Distribution Software";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new Size(1200, 800);

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label headerLabel = new Label
            {
                Text = "📊 Supplier Ledger",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button closeBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.Width - 80, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            closeBtn.Click += (s, e) => this.Close();

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(closeBtn);

            // Main Content Panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };

            // Filter Panel
            GroupBox filterGroup = new GroupBox
            {
                Text = "🔍 Filter Options",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1400, 120),
                Location = new Point(20, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Supplier Selection
            Label lblSupplier = new Label
            {
                Text = "Supplier:",
                Location = new Point(20, 40),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            cmbSupplier = new ComboBox
            {
                Location = new Point(110, 38),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSupplier.SelectedIndexChanged += CmbSupplier_SelectedIndexChanged;

            // From Date
            Label lblFromDate = new Label
            {
                Text = "From Date:",
                Location = new Point(380, 40),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            dtpFromDate = new DateTimePicker
            {
                Location = new Point(470, 38),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10)
            };

            // To Date
            Label lblToDate = new Label
            {
                Text = "To Date:",
                Location = new Point(610, 40),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            dtpToDate = new DateTimePicker
            {
                Location = new Point(700, 38),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Transaction Type
            Label lblTransactionType = new Label
            {
                Text = "Transaction Type:",
                Location = new Point(840, 40),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            cmbTransactionType = new ComboBox
            {
                Location = new Point(970, 38),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Generate Report Button
            btnGenerateReport = new Button
            {
                Text = "📊 Generate Report",
                Size = new Size(150, 35),
                Location = new Point(1140, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnGenerateReport.Click += BtnGenerateReport_Click;

            filterGroup.Controls.AddRange(new Control[] {
                lblSupplier, cmbSupplier, lblFromDate, dtpFromDate, lblToDate, dtpToDate,
                lblTransactionType, cmbTransactionType, btnGenerateReport
            });

            // Summary Panel
            GroupBox summaryGroup = new GroupBox
            {
                Text = "📈 Summary",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1400, 80),
                Location = new Point(20, 160),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            lblCurrentBalance = new Label
            {
                Text = "Current Balance: $0.00",
                Location = new Point(20, 35),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            lblTotalDebits = new Label
            {
                Text = "Total Debits: $0.00",
                Location = new Point(250, 35),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60)
            };

            lblTotalCredits = new Label
            {
                Text = "Total Credits: $0.00",
                Location = new Point(480, 35),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113)
            };

            summaryGroup.Controls.AddRange(new Control[] { lblCurrentBalance, lblTotalDebits, lblTotalCredits });

            // Ledger Grid
            GroupBox ledgerGroup = new GroupBox
            {
                Text = "📋 Supplier Ledger",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1400, 400),
                Location = new Point(20, 260),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            dgvLedger = new DataGridView
            {
                Location = new Point(20, 30),
                Size = new Size(1360, 350),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.LightGray,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(248, 249, 250) },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    SelectionBackColor = Color.FromArgb(52, 152, 219),
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 9)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(52, 73, 94),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                }
            };

            ledgerGroup.Controls.Add(dgvLedger);

            // Action Buttons
            Panel actionPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(236, 240, 241)
            };

            btnExportPDF = new Button
            {
                Text = "📄 Export PDF",
                Size = new Size(120, 40),
                Location = new Point(20, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnExportPDF.Click += BtnExportPDF_Click;

            btnClose = new Button
            {
                Text = "❌ Close",
                Size = new Size(100, 40),
                Location = new Point(160, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClose.Click += (s, e) => this.Close();

            actionPanel.Controls.AddRange(new Control[] { btnExportPDF, btnClose });

            // Add all controls to content panel
            contentPanel.Controls.AddRange(new Control[] {
                filterGroup, summaryGroup, ledgerGroup
            });

            // Add panels to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(contentPanel);
            this.Controls.Add(actionPanel);

            this.ResumeLayout(false);
        }

        private void LoadSuppliers()
        {
            try
            {
                string query = "SELECT SupplierId, SupplierCode, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    cmbSupplier.DataSource = dt;
                    cmbSupplier.DisplayMember = "SupplierName";
                    cmbSupplier.ValueMember = "SupplierId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTransactionTypes()
        {
            cmbTransactionType.Items.AddRange(new string[] { "All", "Purchase", "Payment", "Return", "Adjustment" });
            cmbTransactionType.SelectedIndex = 0;
        }

        private void SetDefaultDates()
        {
            dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            dtpToDate.Value = DateTime.Today;
        }

        private void CmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupplier.SelectedValue != null)
            {
                LoadSupplierBalance();
            }
        }

        private void LoadSupplierBalance()
        {
            try
            {
                if (cmbSupplier.SelectedValue == null) return;

                int supplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
                string query = @"SELECT ISNULL(SUM(CASE WHEN TransactionType = 'Purchase' OR TransactionType = 'Return' THEN Amount ELSE 0 END), 0) - 
                                ISNULL(SUM(CASE WHEN TransactionType = 'Payment' THEN Amount ELSE 0 END), 0) as CurrentBalance
                                FROM SupplierTransactions WHERE SupplierId = @SupplierId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                    connection.Open();
                    decimal balance = Convert.ToDecimal(cmd.ExecuteScalar());
                    connection.Close();

                    lblCurrentBalance.Text = $"Current Balance: ${balance:F2}";
                    lblCurrentBalance.ForeColor = balance >= 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier balance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupplier.SelectedValue == null)
                {
                    MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int supplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
                string transactionType = cmbTransactionType.SelectedItem.ToString();
                
                StringBuilder query = new StringBuilder();
                query.AppendLine(@"SELECT st.TransactionDate, st.TransactionType, st.Description, 
                                 st.Amount, st.RunningBalance, st.ReferenceNumber,
                                 s.SupplierName, s.SupplierCode");
                query.AppendLine("FROM SupplierTransactions st");
                query.AppendLine("INNER JOIN Suppliers s ON st.SupplierId = s.SupplierId");
                query.AppendLine("WHERE st.SupplierId = @SupplierId");
                query.AppendLine("AND st.TransactionDate BETWEEN @FromDate AND @ToDate");

                if (transactionType != "All")
                {
                    query.AppendLine("AND st.TransactionType = @TransactionType");
                }

                query.AppendLine("ORDER BY st.TransactionDate DESC, st.TransactionId DESC");

                using (SqlCommand cmd = new SqlCommand(query.ToString(), connection))
                {
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                    cmd.Parameters.AddWithValue("@FromDate", dtpFromDate.Value.Date);
                    cmd.Parameters.AddWithValue("@ToDate", dtpToDate.Value.Date.AddDays(1).AddSeconds(-1));
                    
                    if (transactionType != "All")
                    {
                        cmd.Parameters.AddWithValue("@TransactionType", transactionType);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvLedger.DataSource = dt;
                        supplierLedgerData = dt;
                    }
                }

                CalculateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotals()
        {
            try
            {
                decimal totalDebits = 0;
                decimal totalCredits = 0;

                foreach (DataRow row in supplierLedgerData.Rows)
                {
                    decimal amount = Convert.ToDecimal(row["Amount"]);
                    string transactionType = row["TransactionType"].ToString();

                    if (transactionType == "Purchase" || transactionType == "Return")
                    {
                        totalDebits += amount;
                    }
                    else if (transactionType == "Payment")
                    {
                        totalCredits += amount;
                    }
                }

                lblTotalDebits.Text = $"Total Debits: ${totalDebits:F2}";
                lblTotalCredits.Text = $"Total Credits: ${totalCredits:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating totals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (supplierLedgerData == null || supplierLedgerData.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export. Please generate a report first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|Excel files (*.xlsx)|*.xlsx",
                    Title = "Export Supplier Ledger"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveDialog.FileName.EndsWith(".csv"))
                    {
                        ExportToCSV(supplierLedgerData, saveDialog.FileName);
                    }
                    else if (saveDialog.FileName.EndsWith(".xlsx"))
                    {
                        ExportToExcel(supplierLedgerData, saveDialog.FileName);
                    }

                    MessageBox.Show("Report exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCSV(DataTable data, string fileName)
        {
            StringBuilder csv = new StringBuilder();
            
            // Add headers
            for (int i = 0; i < data.Columns.Count; i++)
            {
                csv.Append(data.Columns[i].ColumnName);
                if (i < data.Columns.Count - 1) csv.Append(",");
            }
            csv.AppendLine();

            // Add data rows
            foreach (DataRow row in data.Rows)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    csv.Append(row[i].ToString());
                    if (i < data.Columns.Count - 1) csv.Append(",");
                }
                csv.AppendLine();
            }

            System.IO.File.WriteAllText(fileName, csv.ToString());
        }

        private void ExportToExcel(DataTable data, string fileName)
        {
            // Simple Excel export - you can enhance this with EPPlus or similar library
            ExportToCSV(data, fileName.Replace(".xlsx", ".csv"));
        }
    }
}
