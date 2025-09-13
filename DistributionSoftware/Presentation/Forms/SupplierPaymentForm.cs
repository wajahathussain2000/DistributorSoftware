using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SupplierPaymentForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private int currentSupplierId = 0;
        private decimal currentBalance = 0;
        
        // Control declarations
        private ComboBox cmbSupplier;
        private TextBox txtPaymentNumber;
        private Button btnGeneratePaymentNumber;
        private DateTimePicker dtpPaymentDate;
        private TextBox txtPaymentAmount;
        private ComboBox cmbPaymentMethod;
        private TextBox txtBankName;
        private TextBox txtAccountNumber;
        private TextBox txtCheckNumber;
        private DateTimePicker dtpCheckDate;
        private TextBox txtTransactionReference;
        private TextBox txtNotes;
        private Label lblCurrentBalance;
        private DataGridView dgvOutstandingInvoices;
        private DataGridView dgvPaymentAllocations;
        private Button btnSavePayment;
        private Button btnClearPayment;
        private Button btnGenerateReceipt;
        private Button btnClose;


        public SupplierPaymentForm()
        {
            try
            {
                InitializeComponent();
                InitializeConnection();
                LoadSuppliers();
                LoadPaymentMethods();
                GeneratePaymentNumber();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Supplier Payment Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.Text = "Supplier Payment - Distribution Software";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Size = new Size(1400, 900);
            this.MinimumSize = new Size(1200, 800);
            this.AutoScroll = true;

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label headerLabel = new Label
            {
                Text = "💳 Supplier Payment",
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

            // Payment Information Group
            GroupBox paymentGroup = new GroupBox
            {
                Text = "💳 Payment Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1350, 180),
                Location = new Point(20, 100), // Moved down further from 60 to 100
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
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSupplier.SelectedIndexChanged += CmbSupplier_SelectedIndexChanged;

            // Payment Number
            Label lblPaymentNumber = new Label
            {
                Text = "Payment Number:",
                Location = new Point(430, 40),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPaymentNumber = new TextBox
            {
                Location = new Point(560, 38),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            btnGeneratePaymentNumber = new Button
            {
                Text = "🔄 Generate",
                Size = new Size(80, 25),
                Location = new Point(720, 38),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Bold)
            };
            btnGeneratePaymentNumber.Click += BtnGeneratePaymentNumber_Click;

            // Payment Date
            Label lblPaymentDate = new Label
            {
                Text = "Payment Date:",
                Location = new Point(820, 40),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            dtpPaymentDate = new DateTimePicker
            {
                Location = new Point(930, 38),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10),
                Value = DateTime.Today
            };

            // Payment Amount
            Label lblPaymentAmount = new Label
            {
                Text = "Payment Amount:",
                Location = new Point(20, 80),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPaymentAmount = new TextBox
            {
                Location = new Point(150, 78),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Payment Method
            Label lblPaymentMethod = new Label
            {
                Text = "Payment Method:",
                Location = new Point(320, 80),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            cmbPaymentMethod = new ComboBox
            {
                Location = new Point(450, 78),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPaymentMethod.SelectedIndexChanged += CmbPaymentMethod_SelectedIndexChanged;

            // Bank Name
            Label lblBankName = new Label
            {
                Text = "Bank Name:",
                Location = new Point(620, 80),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtBankName = new TextBox
            {
                Location = new Point(710, 78),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                Enabled = false
            };

            // Account Number
            Label lblAccountNumber = new Label
            {
                Text = "Account Number:",
                Location = new Point(880, 80),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtAccountNumber = new TextBox
            {
                Location = new Point(1010, 78),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10),
                Enabled = false
            };

            // Check Number
            Label lblCheckNumber = new Label
            {
                Text = "Check Number:",
                Location = new Point(20, 120),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtCheckNumber = new TextBox
            {
                Location = new Point(130, 118),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10),
                Enabled = false
            };

            // Check Date
            Label lblCheckDate = new Label
            {
                Text = "Check Date:",
                Location = new Point(270, 120),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            dtpCheckDate = new DateTimePicker
            {
                Location = new Point(360, 118),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10),
                Enabled = false
            };

            // Transaction Reference
            Label lblTransactionReference = new Label
            {
                Text = "Transaction Reference:",
                Location = new Point(500, 120),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtTransactionReference = new TextBox
            {
                Location = new Point(660, 118),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Notes
            Label lblNotes = new Label
            {
                Text = "Notes:",
                Location = new Point(880, 120),
                Size = new Size(50, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtNotes = new TextBox
            {
                Location = new Point(940, 118),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Current Balance
            lblCurrentBalance = new Label
            {
                Text = "Current Balance: $0.00",
                Location = new Point(20, 160),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            paymentGroup.Controls.AddRange(new Control[] {
                lblSupplier, cmbSupplier, lblPaymentNumber, txtPaymentNumber, btnGeneratePaymentNumber,
                lblPaymentDate, dtpPaymentDate, lblPaymentAmount, txtPaymentAmount, lblPaymentMethod, cmbPaymentMethod,
                lblBankName, txtBankName, lblAccountNumber, txtAccountNumber, lblCheckNumber, txtCheckNumber,
                lblCheckDate, dtpCheckDate, lblTransactionReference, txtTransactionReference, lblNotes, txtNotes,
                lblCurrentBalance
            });

            // Outstanding Invoices Group
            GroupBox invoicesGroup = new GroupBox
            {
                Text = "📋 Outstanding Invoices",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(650, 250),
                Location = new Point(20, 340), // Moved down further from 300 to 340 (40px more space)
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            dgvOutstandingInvoices = new DataGridView
            {
                Location = new Point(20, 30),
                Size = new Size(610, 200),
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

            invoicesGroup.Controls.Add(dgvOutstandingInvoices);

            // Payment Allocations Group
            GroupBox allocationsGroup = new GroupBox
            {
                Text = "💰 Payment Allocations",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(650, 250),
                Location = new Point(690, 340), // Moved down further from 300 to 340 (40px more space)
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            dgvPaymentAllocations = new DataGridView
            {
                Location = new Point(20, 30),
                Size = new Size(610, 200),
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

            allocationsGroup.Controls.Add(dgvPaymentAllocations);

            // Action Buttons
            Panel actionPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(236, 240, 241)
            };

            btnSavePayment = new Button
            {
                Text = "💾 Save Payment",
                Size = new Size(120, 40),
                Location = new Point(20, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSavePayment.Click += BtnSavePayment_Click;

            btnClearPayment = new Button
            {
                Text = "🔄 Clear",
                Size = new Size(100, 40),
                Location = new Point(160, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClearPayment.Click += BtnClearPayment_Click;

            btnGenerateReceipt = new Button
            {
                Text = "🧾 Generate Receipt",
                Size = new Size(150, 40),
                Location = new Point(280, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnGenerateReceipt.Click += BtnGenerateReceipt_Click;

            btnClose = new Button
            {
                Text = "❌ Close",
                Size = new Size(100, 40),
                Location = new Point(450, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClose.Click += (s, e) => this.Close();

            actionPanel.Controls.AddRange(new Control[] { btnSavePayment, btnClearPayment, btnGenerateReceipt, btnClose });

            // Add all controls to content panel
            contentPanel.Controls.AddRange(new Control[] {
                paymentGroup, invoicesGroup, allocationsGroup
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
                ComboBox cmbSupplier = (ComboBox)this.Controls.Find("cmbSupplier", true)[0];
                
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

        private void LoadPaymentMethods()
        {
            ComboBox cmbPaymentMethod = (ComboBox)this.Controls.Find("cmbPaymentMethod", true)[0];
            
            cmbPaymentMethod.Items.AddRange(new string[] { "Cash", "Bank Transfer", "Check", "Credit Card", "Other" });
            cmbPaymentMethod.SelectedIndex = 0;
        }

        private void GeneratePaymentNumber()
        {
            try
            {
                TextBox txtPaymentNumber = (TextBox)this.Controls.Find("txtPaymentNumber", true)[0];
                
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(PaymentNumber, 3, LEN(PaymentNumber)) AS INT)), 0) + 1 FROM SupplierPayments WHERE PaymentNumber LIKE 'SP%'";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int nextNumber = Convert.ToInt32(cmd.ExecuteScalar());
                    txtPaymentNumber.Text = $"SP{nextNumber:D4}";
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating payment number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbSupplier = (ComboBox)sender;
            
            if (cmbSupplier.SelectedValue != null && cmbSupplier.SelectedIndex >= 0)
            {
                try
                {
                    // Handle both DataRowView and direct integer values
                    if (cmbSupplier.SelectedValue is DataRowView dataRowView)
                    {
                        currentSupplierId = Convert.ToInt32(dataRowView["SupplierId"]);
                    }
                    else
                    {
                        currentSupplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
                    }
                    
                    LoadSupplierBalance();
                    LoadOutstandingInvoices();
                    LoadPaymentAllocations();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading supplier data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadSupplierBalance()
        {
            try
            {
                if (currentSupplierId == 0) return;

                Label lblCurrentBalance = (Label)this.Controls.Find("lblCurrentBalance", true)[0];

                string query = @"SELECT ISNULL(SUM(DebitAmount), 0) - ISNULL(SUM(CreditAmount), 0) as CurrentBalance
                                FROM SupplierTransactions WHERE SupplierId = @SupplierId AND IsActive = 1";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SupplierId", currentSupplierId);
                    connection.Open();
                    currentBalance = Convert.ToDecimal(cmd.ExecuteScalar());
                    connection.Close();

                    lblCurrentBalance.Text = $"Current Balance: ${currentBalance:F2}";
                    lblCurrentBalance.ForeColor = currentBalance >= 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier balance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOutstandingInvoices()
        {
            try
            {
                if (currentSupplierId == 0) return;

                string query = @"SELECT TransactionId, TransactionDate, Description, 
                                       CASE WHEN TransactionType = 'Purchase' THEN DebitAmount ELSE CreditAmount END as Amount, 
                                       ReferenceNumber
                                FROM SupplierTransactions 
                                WHERE SupplierId = @SupplierId 
                                AND (TransactionType = 'Purchase' OR TransactionType = 'Return')
                                AND IsActive = 1
                                ORDER BY TransactionDate DESC";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@SupplierId", currentSupplierId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvOutstandingInvoices.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading outstanding invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPaymentAllocations()
        {
            try
            {
                if (currentSupplierId == 0) return;

                string query = @"SELECT TransactionId, TransactionDate, Description, 
                                       CreditAmount as Amount, ReferenceNumber, PaymentMethod
                                FROM SupplierTransactions 
                                WHERE SupplierId = @SupplierId 
                                AND TransactionType = 'Payment'
                                AND IsActive = 1
                                ORDER BY TransactionDate DESC";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@SupplierId", currentSupplierId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPaymentAllocations.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payment allocations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbPaymentMethod = (ComboBox)sender;
            string selectedMethod = cmbPaymentMethod.SelectedItem.ToString();
            
            // Enable/disable fields based on payment method
            bool isBankTransfer = selectedMethod == "Bank Transfer";
            bool isCheck = selectedMethod == "Check";

            txtBankName.Enabled = isBankTransfer || isCheck;
            txtAccountNumber.Enabled = isBankTransfer || isCheck;
            txtCheckNumber.Enabled = isCheck;
            dtpCheckDate.Enabled = isCheck;
        }

        private void BtnGeneratePaymentNumber_Click(object sender, EventArgs e)
        {
            GeneratePaymentNumber();
        }

        private void BtnSavePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatePaymentInput())
                {
                    decimal paymentAmount = decimal.Parse(txtPaymentAmount.Text);
                    
                    // Save payment
                    string paymentQuery = @"INSERT INTO SupplierPayments (PaymentNumber, SupplierId, PaymentDate, PaymentAmount, 
                                         PaymentMethod, BankName, AccountNumber, CheckNumber, CheckDate, 
                                         TransactionReference, Notes, CreatedDate) 
                                         VALUES (@PaymentNumber, @SupplierId, @PaymentDate, @PaymentAmount, 
                                         @PaymentMethod, @BankName, @AccountNumber, @CheckNumber, @CheckDate, 
                                         @TransactionReference, @Notes, @CreatedDate)";

                    using (SqlCommand cmd = new SqlCommand(paymentQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@PaymentNumber", txtPaymentNumber.Text);
                        cmd.Parameters.AddWithValue("@SupplierId", currentSupplierId);
                        cmd.Parameters.AddWithValue("@PaymentDate", dtpPaymentDate.Value);
                        cmd.Parameters.AddWithValue("@PaymentAmount", paymentAmount);
                        cmd.Parameters.AddWithValue("@PaymentMethod", cmbPaymentMethod.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@BankName", txtBankName.Text);
                        cmd.Parameters.AddWithValue("@AccountNumber", txtAccountNumber.Text);
                        cmd.Parameters.AddWithValue("@CheckNumber", txtCheckNumber.Text);
                        cmd.Parameters.AddWithValue("@CheckDate", dtpCheckDate.Value);
                        cmd.Parameters.AddWithValue("@TransactionReference", txtTransactionReference.Text);
                        cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }

                    // Add transaction record
                    string transactionQuery = @"INSERT INTO SupplierTransactions (SupplierId, TransactionDate, TransactionType, 
                                              Description, CreditAmount, Balance, ReferenceNumber, PaymentMethod, 
                                              BankName, TransactionReference, CreatedDate, CreatedBy) 
                                              VALUES (@SupplierId, @TransactionDate, @TransactionType, @Description, 
                                              @CreditAmount, @Balance, @ReferenceNumber, @PaymentMethod, 
                                              @BankName, @TransactionReference, @CreatedDate, @CreatedBy)";

                    using (SqlCommand cmd = new SqlCommand(transactionQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@SupplierId", currentSupplierId);
                        cmd.Parameters.AddWithValue("@TransactionDate", dtpPaymentDate.Value);
                        cmd.Parameters.AddWithValue("@TransactionType", "Payment");
                        cmd.Parameters.AddWithValue("@Description", $"Payment - {txtPaymentNumber.Text}");
                        cmd.Parameters.AddWithValue("@CreditAmount", paymentAmount);
                        cmd.Parameters.AddWithValue("@Balance", currentBalance - paymentAmount);
                        cmd.Parameters.AddWithValue("@ReferenceNumber", txtTransactionReference.Text);
                        cmd.Parameters.AddWithValue("@PaymentMethod", cmbPaymentMethod.SelectedItem?.ToString());
                        cmd.Parameters.AddWithValue("@BankName", txtBankName.Text);
                        cmd.Parameters.AddWithValue("@TransactionReference", txtTransactionReference.Text);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", GetCurrentUser());

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }

                    MessageBox.Show("Payment saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearPaymentForm();
                    LoadSupplierBalance();
                    LoadOutstandingInvoices();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearPayment_Click(object sender, EventArgs e)
        {
            ClearPaymentForm();
        }

        private void BtnGenerateReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPaymentNumber.Text))
                {
                    MessageBox.Show("Please generate a payment number first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Generate receipt (you can enhance this with a proper receipt form)
                StringBuilder receipt = new StringBuilder();
                receipt.AppendLine("PAYMENT RECEIPT");
                receipt.AppendLine("==================");
                receipt.AppendLine($"Payment Number: {txtPaymentNumber.Text}");
                receipt.AppendLine($"Date: {dtpPaymentDate.Value:yyyy-MM-dd}");
                receipt.AppendLine($"Supplier: {cmbSupplier.Text}");
                receipt.AppendLine($"Amount: ${txtPaymentAmount.Text}");
                receipt.AppendLine($"Method: {cmbPaymentMethod.Text}");
                receipt.AppendLine($"Reference: {txtTransactionReference.Text}");
                receipt.AppendLine($"Notes: {txtNotes.Text}");

                MessageBox.Show(receipt.ToString(), "Payment Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearPaymentForm()
        {
            txtPaymentAmount.Clear();
            txtBankName.Clear();
            txtAccountNumber.Clear();
            txtCheckNumber.Clear();
            txtTransactionReference.Clear();
            txtNotes.Clear();
            dtpCheckDate.Value = DateTime.Today;
            GeneratePaymentNumber();
        }

        private bool ValidatePaymentInput()
        {
            if (cmbSupplier.SelectedValue == null)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPaymentAmount.Text))
            {
                MessageBox.Show("Please enter payment amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPaymentAmount.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPaymentAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid payment amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPaymentAmount.Focus();
                return false;
            }

            return true;
        }

        private string GetCurrentUser()
        {
            try
            {
                // Get current logged-in user from session or application state
                // This is a placeholder - implement based on your authentication system
                return "System"; // Default user for now
            }
            catch
            {
                return "System";
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void TxtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            // Update payment allocations
        }
    }
}
