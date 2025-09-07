using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;
using System.Text;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class CustomerMasterForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private int currentCustomerId = 0;
        private PictureBox picBarcode;
        private PictureBox picQRCode;
        private Button btnSave;
        private DataGridView dgvCustomers;
        private bool isEditMode = false;

        public CustomerMasterForm()
        {
            InitializeComponent();
            InitializeForm();
            InitializeConnection();
            LoadCustomerCategories();
            LoadCustomersGrid();
            
            // Subscribe to form load event to clear form after all controls are initialized
            this.Load += CustomerMasterForm_Load;
        }

        private void CustomerMasterForm_Load(object sender, EventArgs e)
        {
            // Clear form after all controls are fully initialized
            System.Diagnostics.Debug.WriteLine("Form Load event fired - clearing form now");
            ClearForm();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        private void InitializeForm()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Customer Master - Distribution Software";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Size = new Size(1500, 800);
            this.MinimumSize = new Size(1500, 800);

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label headerLabel = new Label
            {
                Text = "👥 Customer Master",
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

            // Customer Information Group
            GroupBox customerGroup = new GroupBox
            {
                Text = "📋 Customer Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(750, 450),
                Location = new Point(20, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            // Customer Code
            Label codeLabel = new Label { Text = "🔢 Customer Code:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtCustomerCode = new TextBox { Name = "txtCustomerCode", Location = new Point(200, 28), Size = new Size(150, 25), ReadOnly = true, BackColor = Color.FromArgb(236, 240, 241), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Customer Name
            Label nameLabel = new Label { Text = "👤 Customer Name:", Location = new Point(20, 70), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtCustomerName = new TextBox { Name = "txtCustomerName", Location = new Point(200, 68), Size = new Size(200, 25), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Contact Person
            Label contactLabel = new Label { Text = "📞 Contact Person:", Location = new Point(20, 110), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtContactPerson = new TextBox { Name = "txtContactPerson", Location = new Point(200, 108), Size = new Size(200, 25), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Phone
            Label phoneLabel = new Label { Text = "📱 Phone:", Location = new Point(20, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtPhone = new TextBox { Name = "txtPhone", Location = new Point(150, 148), Size = new Size(150, 25), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Email
            Label emailLabel = new Label { Text = "📧 Email:", Location = new Point(20, 190), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtEmail = new TextBox { Name = "txtEmail", Location = new Point(150, 188), Size = new Size(200, 25), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Address
            Label addressLabel = new Label { Text = "🏠 Address:", Location = new Point(20, 230), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtAddress = new TextBox { Name = "txtAddress", Location = new Point(150, 228), Size = new Size(300, 60), Multiline = true, ScrollBars = ScrollBars.Vertical, Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // City
            Label cityLabel = new Label { Text = "🏙️ City:", Location = new Point(20, 310), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtCity = new TextBox { Name = "txtCity", Location = new Point(150, 308), Size = new Size(120, 25), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // State
            Label stateLabel = new Label { Text = "🗺️ State:", Location = new Point(290, 310), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtState = new TextBox { Name = "txtState", Location = new Point(400, 308), Size = new Size(120, 25), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Country
            Label countryLabel = new Label { Text = "🌍 Country:", Location = new Point(20, 350), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtCountry = new TextBox { Name = "txtCountry", Location = new Point(150, 348), Size = new Size(120, 25), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Postal Code
            Label postalLabel = new Label { Text = "📮 Postal Code:", Location = new Point(290, 350), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtPostalCode = new TextBox { Name = "txtPostalCode", Location = new Point(450, 348), Size = new Size(100, 25), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Category
            Label categoryLabel = new Label { Text = "🏷️ Category:", Location = new Point(20, 390), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            ComboBox cmbCategory = new ComboBox { Name = "cmbCategory", Location = new Point(150, 388), Size = new Size(150, 25), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Discount %
            Label discountLabel = new Label { Text = "💰 Discount %:", Location = new Point(320, 390), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            NumericUpDown nudDiscount = new NumericUpDown { Name = "nudDiscount", Location = new Point(470, 388), Size = new Size(80, 25), DecimalPlaces = 2, Maximum = 100, Minimum = 0, Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Credit Limit
            Label creditLabel = new Label { Text = "💳 Credit Limit:", Location = new Point(520, 390), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            NumericUpDown nudCreditLimit = new NumericUpDown { Name = "nudCreditLimit", Location = new Point(670, 388), Size = new Size(120, 25), DecimalPlaces = 2, Maximum = 999999999, Minimum = 0, Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Barcode and QR Code (Right side of Customer Information)
            Label barcodeLabel = new Label { Text = "📊 Barcode:", Location = new Point(500, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            
            // Barcode Display Panel
            Panel barcodePanel = new Panel
            {
                Name = "pnlBarcode",
                Location = new Point(500, 55),
                Size = new Size(180, 50),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            picBarcode = new PictureBox
            {
                Location = new Point(5, 5),
                Size = new Size(170, 40),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };
            barcodePanel.Controls.Add(picBarcode);

            // QR Code
            Label qrLabel = new Label { Text = "🔲 QR Code:", Location = new Point(500, 120), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            
            // QR Code Display Panel
            Panel qrPanel = new Panel
            {
                Name = "pnlQRCode",
                Location = new Point(500, 145),
                Size = new Size(80, 80),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            picQRCode = new PictureBox
            {
                Location = new Point(5, 5),
                Size = new Size(70, 70),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };
            qrPanel.Controls.Add(picQRCode);

            // Generate button removed - everything is automatic

            customerGroup.Controls.AddRange(new Control[] {
                codeLabel, txtCustomerCode, nameLabel, txtCustomerName, contactLabel, txtContactPerson,
                phoneLabel, txtPhone, emailLabel, txtEmail, addressLabel, txtAddress,
                cityLabel, txtCity, stateLabel, txtState, countryLabel, txtCountry, postalLabel, txtPostalCode,
                categoryLabel, cmbCategory, discountLabel, nudDiscount, creditLabel, nudCreditLimit,
                barcodeLabel, barcodePanel, qrLabel, qrPanel
            });

            // Customer List Group (Right side with more space)
            GroupBox customerListGroup = new GroupBox
            {
                Text = "📋 Customers List",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(500, 450),
                Location = new Point(790, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                BackColor = Color.FromArgb(240, 248, 255) // Light blue background
            };

            dgvCustomers = new DataGridView
            {
                Name = "dgvCustomers",
                Location = new Point(20, 30),
                Size = new Size(460, 400),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                ScrollBars = ScrollBars.Both,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                GridColor = Color.FromArgb(189, 195, 199),
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ColumnHeadersHeight = 30,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 7, FontStyle.Bold),
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    SelectionBackColor = Color.FromArgb(52, 152, 219),
                    SelectionForeColor = Color.White
                }
            };
            dgvCustomers.SelectionChanged += DgvCustomers_SelectionChanged;

            customerListGroup.Controls.Add(dgvCustomers);
            
            // Debug: Log customer list group positioning
            System.Diagnostics.Debug.WriteLine($"Customer List Group - Location: {customerListGroup.Location}, Size: {customerListGroup.Size}");
            System.Diagnostics.Debug.WriteLine($"Customer List Group - Right edge: {customerListGroup.Location.X + customerListGroup.Size.Width}");
            System.Diagnostics.Debug.WriteLine($"Form Size: {this.Size}");

            // Actions Group
            GroupBox actionsGroup = new GroupBox
            {
                Text = "⚡ Actions",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(900, 80),
                Location = new Point(20, 570),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            btnSave = new Button
            {
                Text = "💾 Save",
                Location = new Point(20, 30),
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;

            Button btnClear = new Button
            {
                Text = "🗑️ Clear",
                Location = new Point(140, 30),
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 196, 15),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClear.Click += BtnClear_Click;

            Button btnDelete = new Button
            {
                Text = "🗑️ Delete",
                Location = new Point(260, 30),
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnDelete.Click += BtnDelete_Click;

            actionsGroup.Controls.AddRange(new Control[] { btnSave, btnClear, btnDelete });

            contentPanel.Controls.AddRange(new Control[] {
                customerGroup, customerListGroup, actionsGroup
            });

            this.Controls.Add(headerPanel);
            this.Controls.Add(contentPanel);

            this.ResumeLayout(false);
        }

        private void LoadCustomerCategories()
        {
            try
            {
                ComboBox cmbCategory = FindControlRecursive(this, "cmbCategory") as ComboBox;
                if (cmbCategory == null) return;

                string query = "SELECT CategoryId, CategoryName FROM CustomerCategories WHERE IsActive = 1 ORDER BY CategoryName";
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        var dataTable = new DataTable();
                        var adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);

                        cmbCategory.DataSource = dataTable;
                        cmbCategory.DisplayMember = "CategoryName";
                        cmbCategory.ValueMember = "CategoryId";
                        cmbCategory.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateCustomerCode()
        {
            try
            {
                TextBox txtCustomerCode = FindControlRecursive(this, "txtCustomerCode") as TextBox;
                if (txtCustomerCode == null) return;

                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(CustomerCode, 5, 8) AS INT)), 0) + 1 FROM Customers WHERE CustomerCode LIKE 'CUST%'";
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        var nextNumber = Convert.ToInt32(command.ExecuteScalar());
                        txtCustomerCode.Text = $"CUST{nextNumber:D8}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating customer code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateBarcode()
        {
            try
            {
                TextBox txtCustomerCode = FindControlRecursive(this, "txtCustomerCode") as TextBox;
                if (txtCustomerCode == null) return;

                string customerCode = txtCustomerCode.Text;
                if (string.IsNullOrEmpty(customerCode))
                {
                    MessageBox.Show("Please enter a customer code first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Generate barcode image
                GenerateBarcodeImage(customerCode);
                
                // Generate QR code image
                GenerateQRCodeImage(customerCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode/QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateBarcodeImage(string barcodeText)
        {
            try
            {
                // Create barcode image like other forms
                Bitmap barcodeBitmap = new Bitmap(170, 40);
                using (Graphics g = Graphics.FromImage(barcodeBitmap))
                {
                    g.Clear(Color.White);
                    
                    // Draw barcode bars (consistent pattern like other forms)
                    Random rand = new Random(barcodeText.GetHashCode()); // Use text hash for consistent pattern
                    int barWidth = 2;
                    int x = 10;
                    
                    for (int i = 0; i < barcodeText.Length * 4 && x < 160; i++)
                    {
                        int barHeight = rand.Next(12, 30);
                        g.FillRectangle(Brushes.Black, x, 5, barWidth, barHeight);
                        x += barWidth + 1;
                    }
                }

                picBarcode.Image = barcodeBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateQRCodeImage(string qrData)
        {
            try
            {
                // Create QR code image like SupplierMasterForm
                Bitmap qrBitmap = new Bitmap(70, 70);
                using (Graphics g = Graphics.FromImage(qrBitmap))
                {
                    g.Clear(Color.White);
                    
                    // Draw QR-like pattern with squares
                    Random rand = new Random(qrData.GetHashCode());
                    
                    // Draw border
                    g.DrawRectangle(Pens.Black, 2, 2, 66, 66);
                    
                    // Draw corner squares (QR code characteristic)
                    g.FillRectangle(Brushes.Black, 5, 5, 10, 10);
                    g.FillRectangle(Brushes.Black, 55, 5, 10, 10);
                    g.FillRectangle(Brushes.Black, 5, 55, 10, 10);
                    
                    // Draw inner corner squares
                    g.FillRectangle(Brushes.White, 7, 7, 6, 6);
                    g.FillRectangle(Brushes.White, 57, 7, 6, 6);
                    g.FillRectangle(Brushes.White, 7, 57, 6, 6);
                    
                    // Draw random pattern (simulating QR code data)
                    for (int x = 18; x < 52; x += 5)
                    {
                        for (int y = 18; y < 52; y += 5)
                        {
                            if (rand.Next(0, 2) == 1)
                            {
                                g.FillRectangle(Brushes.Black, x, y, 3, 3);
                            }
                        }
                    }
                }

                picQRCode.Image = qrBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomersGrid()
        {
            try
            {
                if (dgvCustomers == null) return;

                string query = @"
                    SELECT c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactPerson, c.Phone, c.Email, 
                           c.Address, c.City, c.State, c.Country, c.PostalCode, c.DiscountPercent, c.CreditLimit,
                           c.CategoryId, cc.CategoryName
                    FROM Customers c 
                    LEFT JOIN CustomerCategories cc ON c.CategoryId = cc.CategoryId 
                    WHERE c.IsActive = 1 
                    ORDER BY c.CustomerCode";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        var dataTable = new DataTable();
                        var adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);

                        // Debug: Log the loaded data
                        System.Diagnostics.Debug.WriteLine($"Loaded {dataTable.Rows.Count} customers from database");
                        if (dataTable.Rows.Count > 0)
                        {
                            System.Diagnostics.Debug.WriteLine("Sample customer data:");
                            DataRow sampleRow = dataTable.Rows[0];
                            foreach (DataColumn column in dataTable.Columns)
                            {
                                System.Diagnostics.Debug.WriteLine($"  {column.ColumnName}: {sampleRow[column.ColumnName]} (Type: {sampleRow[column.ColumnName].GetType()})");
                            }
                        }

                        // Temporarily disable selection change event to prevent auto-population
                        dgvCustomers.SelectionChanged -= DgvCustomers_SelectionChanged;
                        
                        dgvCustomers.DataSource = dataTable;
                        
                        // Set proper column headers (using new column names)
                        if (dgvCustomers.Columns.Count > 0)
                        {
                            dgvCustomers.Columns["CustomerId"].HeaderText = "ID";
                            dgvCustomers.Columns["CustomerCode"].HeaderText = "Customer Code";
                            dgvCustomers.Columns["CustomerName"].HeaderText = "Customer Name";
                            dgvCustomers.Columns["ContactPerson"].HeaderText = "Contact Person";
                            dgvCustomers.Columns["Phone"].HeaderText = "Phone";
                            dgvCustomers.Columns["Email"].HeaderText = "Email";
                            dgvCustomers.Columns["City"].HeaderText = "City";
                            dgvCustomers.Columns["State"].HeaderText = "State";
                            dgvCustomers.Columns["DiscountPercent"].HeaderText = "Discount %";
                            dgvCustomers.Columns["CreditLimit"].HeaderText = "Credit Limit";
                            dgvCustomers.Columns["CategoryName"].HeaderText = "Category";
                        }
                        
                        dgvCustomers.AutoResizeColumns();
                        
                        // Clear any selection to prevent auto-population
                        dgvCustomers.ClearSelection();
                        
                        // Re-enable selection change event
                        dgvCustomers.SelectionChanged += DgvCustomers_SelectionChanged;
                        
                        // Debug: Show customer count in form title
                        this.Text = $"Customer Master - Distribution Software ({dataTable.Rows.Count} customers)";
                        
                        // Additional debug logging
                        System.Diagnostics.Debug.WriteLine($"Loaded {dataTable.Rows.Count} customers into grid");
                        System.Diagnostics.Debug.WriteLine($"DataGridView dgvCustomers is null: {dgvCustomers == null}");
                        System.Diagnostics.Debug.WriteLine($"DataGridView Visible: {dgvCustomers.Visible}");
                        System.Diagnostics.Debug.WriteLine($"DataGridView Location: {dgvCustomers.Location}");
                        System.Diagnostics.Debug.WriteLine($"DataGridView Size: {dgvCustomers.Size}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Generate button removed - everything is automatic

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    if (currentCustomerId == 0)
                    {
                        InsertCustomer();
                    }
                    else
                    {
                        UpdateCustomer();
                    }
                    
                    LoadCustomersGrid();
                    ClearForm();
                    string message = isEditMode ? "Customer updated successfully!" : "Customer saved successfully!";
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentCustomerId == 0)
                {
                    MessageBox.Show("Please select a customer to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DeleteCustomer();
                    LoadCustomersGrid();
                    ClearForm();
                    MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgvCustomers = sender as DataGridView;
                if (dgvCustomers.SelectedRows.Count > 0)
                {
                    DataRowView row = dgvCustomers.SelectedRows[0].DataBoundItem as DataRowView;
                    if (row != null)
                    {
                        LoadCustomerToForm(row.Row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            TextBox txtCustomerName = FindControlRecursive(this, "txtCustomerName") as TextBox;
            TextBox txtContactPerson = FindControlRecursive(this, "txtContactPerson") as TextBox;

            if (string.IsNullOrWhiteSpace(txtCustomerName?.Text))
            {
                MessageBox.Show("Customer Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName?.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContactPerson?.Text))
            {
                MessageBox.Show("Contact Person is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactPerson?.Focus();
                return false;
            }

            return true;
        }

        private void InsertCustomer()
        {
            string query = @"
                INSERT INTO Customers (CustomerCode, CustomerName, ContactPerson, Email, Phone, Address, 
                                     City, State, Country, PostalCode, DiscountPercent, CreditLimit, 
                                     CategoryId, Barcode, QRCode, IsActive, CreatedDate)
                VALUES (@CustomerCode, @CustomerName, @ContactPerson, @Email, @Phone, @Address,
                        @City, @State, @Country, @PostalCode, @DiscountPercent, @CreditLimit,
                        @CategoryId, @Barcode, @QRCode, 1, GETDATE())";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    SetParameters(command);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateCustomer()
        {
            string query = @"
                UPDATE Customers SET 
                    CustomerName = @CustomerName,
                    ContactPerson = @ContactPerson,
                    Email = @Email,
                    Phone = @Phone,
                    Address = @Address,
                    City = @City,
                    State = @State,
                    Country = @Country,
                    PostalCode = @PostalCode,
                    DiscountPercent = @DiscountPercent,
                    CreditLimit = @CreditLimit,
                    CategoryId = @CategoryId,
                    Barcode = @Barcode,
                    QRCode = @QRCode,
                    ModifiedDate = GETDATE()
                WHERE CustomerId = @CustomerId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    SetParameters(command);
                    command.Parameters.AddWithValue("@CustomerId", currentCustomerId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeleteCustomer()
        {
            string query = "UPDATE Customers SET IsActive = 0, ModifiedDate = GETDATE() WHERE CustomerId = @CustomerId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", currentCustomerId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void SetParameters(SqlCommand command)
        {
            TextBox txtCustomerCode = FindControlRecursive(this, "txtCustomerCode") as TextBox;
            TextBox txtCustomerName = FindControlRecursive(this, "txtCustomerName") as TextBox;
            TextBox txtContactPerson = FindControlRecursive(this, "txtContactPerson") as TextBox;
            TextBox txtPhone = FindControlRecursive(this, "txtPhone") as TextBox;
            TextBox txtEmail = FindControlRecursive(this, "txtEmail") as TextBox;
            TextBox txtAddress = FindControlRecursive(this, "txtAddress") as TextBox;
            TextBox txtCity = FindControlRecursive(this, "txtCity") as TextBox;
            TextBox txtState = FindControlRecursive(this, "txtState") as TextBox;
            TextBox txtCountry = FindControlRecursive(this, "txtCountry") as TextBox;
            TextBox txtPostalCode = FindControlRecursive(this, "txtPostalCode") as TextBox;
            ComboBox cmbCategory = FindControlRecursive(this, "cmbCategory") as ComboBox;
            NumericUpDown nudDiscount = FindControlRecursive(this, "nudDiscount") as NumericUpDown;
            NumericUpDown nudCreditLimit = FindControlRecursive(this, "nudCreditLimit") as NumericUpDown;
            // Use customer code for barcode and QR code
            string customerCode = txtCustomerCode?.Text ?? "";
            command.Parameters.AddWithValue("@CustomerCode", customerCode);
            command.Parameters.AddWithValue("@CustomerName", txtCustomerName?.Text ?? "");
            command.Parameters.AddWithValue("@ContactPerson", txtContactPerson?.Text ?? "");
            command.Parameters.AddWithValue("@Email", txtEmail?.Text ?? "");
            command.Parameters.AddWithValue("@Phone", txtPhone?.Text ?? "");
            command.Parameters.AddWithValue("@Address", txtAddress?.Text ?? "");
            command.Parameters.AddWithValue("@City", txtCity?.Text ?? "");
            command.Parameters.AddWithValue("@State", txtState?.Text ?? "");
            command.Parameters.AddWithValue("@Country", txtCountry?.Text ?? "");
            command.Parameters.AddWithValue("@PostalCode", txtPostalCode?.Text ?? "");
            command.Parameters.AddWithValue("@DiscountPercent", nudDiscount?.Value ?? 0);
            command.Parameters.AddWithValue("@CreditLimit", nudCreditLimit?.Value ?? 0);
            command.Parameters.AddWithValue("@CategoryId", cmbCategory?.SelectedValue ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Barcode", customerCode);
            command.Parameters.AddWithValue("@QRCode", customerCode);
        }

        private void LoadCustomerToForm(DataRow row)
        {
            try
            {
                // Debug: Log the row data
                System.Diagnostics.Debug.WriteLine("Loading customer data:");
                foreach (DataColumn column in row.Table.Columns)
                {
                    System.Diagnostics.Debug.WriteLine($"  {column.ColumnName}: {row[column.ColumnName]} (Type: {row[column.ColumnName].GetType()})");
                }
                
                System.Diagnostics.Debug.WriteLine("Available columns in query:");
                foreach (DataColumn column in row.Table.Columns)
                {
                    System.Diagnostics.Debug.WriteLine($"  - {column.ColumnName}");
                }

                currentCustomerId = Convert.ToInt32(row["CustomerId"]);

                TextBox txtCustomerCode = FindControlRecursive(this, "txtCustomerCode") as TextBox;
                TextBox txtCustomerName = FindControlRecursive(this, "txtCustomerName") as TextBox;
                TextBox txtContactPerson = FindControlRecursive(this, "txtContactPerson") as TextBox;
                TextBox txtPhone = FindControlRecursive(this, "txtPhone") as TextBox;
                TextBox txtEmail = FindControlRecursive(this, "txtEmail") as TextBox;
                TextBox txtAddress = FindControlRecursive(this, "txtAddress") as TextBox;
                TextBox txtCity = FindControlRecursive(this, "txtCity") as TextBox;
                TextBox txtState = FindControlRecursive(this, "txtState") as TextBox;
                TextBox txtCountry = FindControlRecursive(this, "txtCountry") as TextBox;
                TextBox txtPostalCode = FindControlRecursive(this, "txtPostalCode") as TextBox;
                ComboBox cmbCategory = FindControlRecursive(this, "cmbCategory") as ComboBox;
                NumericUpDown nudDiscount = FindControlRecursive(this, "nudDiscount") as NumericUpDown;
                NumericUpDown nudCreditLimit = FindControlRecursive(this, "nudCreditLimit") as NumericUpDown;

                // Safe string assignments with DBNull handling (using new column names)
                txtCustomerCode.Text = row["CustomerCode"] == DBNull.Value ? "" : row["CustomerCode"].ToString();
                txtCustomerName.Text = row["CustomerName"] == DBNull.Value ? "" : row["CustomerName"].ToString();
                txtContactPerson.Text = row["ContactPerson"] == DBNull.Value ? "" : row["ContactPerson"].ToString();
                txtPhone.Text = row["Phone"] == DBNull.Value ? "" : row["Phone"].ToString();
                txtEmail.Text = row["Email"] == DBNull.Value ? "" : row["Email"].ToString();
                txtAddress.Text = row["Address"] == DBNull.Value ? "" : row["Address"].ToString();
                txtCity.Text = row["City"] == DBNull.Value ? "" : row["City"].ToString();
                txtState.Text = row["State"] == DBNull.Value ? "" : row["State"].ToString();
                txtCountry.Text = row["Country"] == DBNull.Value ? "" : row["Country"].ToString();
                txtPostalCode.Text = row["PostalCode"] == DBNull.Value ? "" : row["PostalCode"].ToString();

                // Debug: Log loaded field values
                System.Diagnostics.Debug.WriteLine($"Loaded customer data - Name: {txtCustomerName.Text}, Contact: {txtContactPerson.Text}, Address: {txtAddress.Text}, Country: {txtCountry.Text}");

                // Generate barcode and QR code images from customer code
                string customerCode = txtCustomerCode.Text;
                if (!string.IsNullOrEmpty(customerCode))
                {
                    GenerateBarcodeImage(customerCode);
                    GenerateQRCodeImage(customerCode);
                }

                // Safe decimal assignments with DBNull handling
                nudDiscount.Value = row["DiscountPercent"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DiscountPercent"]);
                nudCreditLimit.Value = row["CreditLimit"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CreditLimit"]);

                // Handle Category selection
                if (cmbCategory != null)
                {
                    LoadCustomerCategories();
                    // Use CategoryId to find the category
                    if (row["CategoryId"] != DBNull.Value)
                    {
                        cmbCategory.SelectedValue = Convert.ToInt32(row["CategoryId"]);
                    }
                    else
                    {
                        cmbCategory.SelectedIndex = -1;
                    }
                }

                // Set edit mode and update button text
                isEditMode = true;
                if (btnSave != null)
                {
                    btnSave.Text = "✏️ Edit";
                    btnSave.BackColor = Color.FromArgb(52, 152, 219); // Blue color for edit
                }

                System.Diagnostics.Debug.WriteLine("Customer data loaded successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading customer data: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error loading customer data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("ClearForm() method called");
                currentCustomerId = 0;

                TextBox txtCustomerName = FindControlRecursive(this, "txtCustomerName") as TextBox;
                TextBox txtContactPerson = FindControlRecursive(this, "txtContactPerson") as TextBox;
                TextBox txtPhone = FindControlRecursive(this, "txtPhone") as TextBox;
                TextBox txtEmail = FindControlRecursive(this, "txtEmail") as TextBox;
                TextBox txtAddress = FindControlRecursive(this, "txtAddress") as TextBox;
                TextBox txtCity = FindControlRecursive(this, "txtCity") as TextBox;
                TextBox txtState = FindControlRecursive(this, "txtState") as TextBox;
                TextBox txtCountry = FindControlRecursive(this, "txtCountry") as TextBox;
                TextBox txtPostalCode = FindControlRecursive(this, "txtPostalCode") as TextBox;
                ComboBox cmbCategory = FindControlRecursive(this, "cmbCategory") as ComboBox;
                NumericUpDown nudDiscount = FindControlRecursive(this, "nudDiscount") as NumericUpDown;
                NumericUpDown nudCreditLimit = FindControlRecursive(this, "nudCreditLimit") as NumericUpDown;

                txtCustomerName.Clear();
                txtContactPerson.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                txtAddress.Clear();
                txtCity.Clear();
                txtState.Clear();
                txtCountry.Clear();
                txtPostalCode.Clear();
                cmbCategory.SelectedIndex = -1;
                nudDiscount.Value = 0;
                nudCreditLimit.Value = 0;

                // Reset to save mode
                isEditMode = false;
                if (btnSave != null)
                {
                    btnSave.Text = "💾 Save";
                    btnSave.BackColor = Color.FromArgb(46, 204, 113); // Green color for save
                }

                GenerateCustomerCode();
                GenerateBarcode();
                
                // Debug: Verify form is cleared
                System.Diagnostics.Debug.WriteLine("Form cleared successfully - all fields should be empty");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Name == name)
                    return control;

                Control found = FindControlRecursive(control, name);
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
