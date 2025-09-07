using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
using DistributionSoftware.Common;
using System.Diagnostics;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class PurchaseReturnForm : Form
    {
        private SqlConnection connection;
        private DataTable purchaseReturnItemsDataTable;
        private DataTable suppliersDataTable;
        private DataTable productsDataTable;
        private DataTable purchasesDataTable;
        private bool isEditMode = false;
        private int currentPurchaseReturnId = 0;
        private string currentStatus = "Draft";
        
        // Services
        private IPurchaseReturnService _purchaseReturnService;
        private IPurchaseReturnRepository _purchaseReturnRepository;
        private IPurchaseReturnItemRepository _purchaseReturnItemRepository;

        public PurchaseReturnForm()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Initializing form");
                
                InitializeUI();
                InitializeConnection();
                InitializeServices();
                InitializeDataTable();
                LoadSuppliers();
                LoadProducts();
                LoadPurchases();
                GenerateReturnNumber();
                GenerateBarcode();
                LoadPurchaseReturnList();
                SetFormReadOnly(false);
                Debug.WriteLine("PurchaseReturnForm: Form initialized successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Initialization error - {ex.Message}");
                MessageBox.Show($"Error initializing Purchase Return Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StyleTextBox(TextBox textBox, bool isReadOnly = false)
        {
            textBox.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = isReadOnly ? Color.FromArgb(245, 245, 245) : Color.White;
            textBox.ForeColor = Color.FromArgb(44, 62, 80);
            textBox.Padding = new Padding(5);
        }

        private void StyleComboBox(ComboBox comboBox)
        {
            comboBox.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = Color.FromArgb(44, 62, 80);
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        private void StyleDateTimePicker(DateTimePicker dateTimePicker)
        {
            dateTimePicker.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dateTimePicker.BackColor = Color.White;
            dateTimePicker.ForeColor = Color.FromArgb(44, 62, 80);
            dateTimePicker.Format = DateTimePickerFormat.Short;
        }

        private void InitializeUI()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Initializing UI");
                
                // Form settings
                this.Text = "Purchase Return - Distribution Software";
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
                    Text = "🔄 Purchase Return",
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

                // Purchase Return Header Group
                GroupBox headerGroup = new GroupBox
                {
                    Text = "📋 Purchase Return Header",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(900, 200),
                    Location = new Point(20, 100)
                };

                // Return Number
                Label returnNoLabel = new Label { Text = "Return No:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtReturnNumber = new TextBox { Name = "txtReturnNumber", Location = new Point(120, 28), Size = new Size(150, 25), ReadOnly = true };
                StyleTextBox(txtReturnNumber, true);

                // Barcode
                Label barcodeLabel = new Label { Text = "Barcode:", Location = new Point(300, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtBarcode = new TextBox { Name = "txtBarcode", Location = new Point(380, 28), Size = new Size(150, 25), ReadOnly = true };
                StyleTextBox(txtBarcode, true);

                // Supplier
                Label supplierLabel = new Label { Text = "Supplier:", Location = new Point(20, 70), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                ComboBox cmbSupplier = new ComboBox { Name = "cmbSupplier", Location = new Point(120, 68), Size = new Size(200, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                StyleComboBox(cmbSupplier);

                // Reference Purchase
                Label refPurchaseLabel = new Label { Text = "Reference Purchase:", Location = new Point(350, 70), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                ComboBox cmbReferencePurchase = new ComboBox { Name = "cmbReferencePurchase", Location = new Point(500, 68), Size = new Size(200, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                StyleComboBox(cmbReferencePurchase);
                cmbReferencePurchase.SelectedIndexChanged += CmbReferencePurchase_SelectedIndexChanged;

                // Return Date
                Label returnDateLabel = new Label { Text = "Return Date:", Location = new Point(20, 110), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                DateTimePicker dtpReturnDate = new DateTimePicker { Name = "dtpReturnDate", Location = new Point(120, 108), Size = new Size(150, 25), Value = DateTime.Now };
                StyleDateTimePicker(dtpReturnDate);

                // Tax Adjust
                Label taxAdjustLabel = new Label { Text = "Tax Adjust:", Location = new Point(300, 110), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtTaxAdjust = new TextBox { Name = "txtTaxAdjust", Location = new Point(380, 108), Size = new Size(100, 25), Text = "0.00" };
                StyleTextBox(txtTaxAdjust);

                // Discount Adjust
                Label discountAdjustLabel = new Label { Text = "Discount Adjust:", Location = new Point(500, 110), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtDiscountAdjust = new TextBox { Name = "txtDiscountAdjust", Location = new Point(650, 108), Size = new Size(100, 25), Text = "0.00" };
                StyleTextBox(txtDiscountAdjust);

                // Freight Adjust
                Label freightAdjustLabel = new Label { Text = "Freight Adjust:", Location = new Point(20, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtFreightAdjust = new TextBox { Name = "txtFreightAdjust", Location = new Point(120, 148), Size = new Size(100, 25), Text = "0.00" };
                StyleTextBox(txtFreightAdjust);

                // Net Return Amount
                Label netAmountLabel = new Label { Text = "Net Return Amount:", Location = new Point(250, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtNetReturnAmount = new TextBox { Name = "txtNetReturnAmount", Location = new Point(400, 148), Size = new Size(120, 25), ReadOnly = true, Text = "0.00" };
                StyleTextBox(txtNetReturnAmount, true);

                // Reason
                Label reasonLabel = new Label { Text = "Reason:", Location = new Point(550, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtReason = new TextBox { Name = "txtReason", Location = new Point(620, 148), Size = new Size(200, 25) };
                StyleTextBox(txtReason);

                // Barcode Group (Right Side)
                GroupBox barcodeGroup = new GroupBox
                {
                    Text = "📊 Barcode Information",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(480, 200),
                    Location = new Point(940, 100)
                };

                // Barcode Display Panel
                Panel barcodePanel = new Panel
                {
                    Name = "pnlBarcode",
                    Location = new Point(20, 90),
                    Size = new Size(440, 80),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White
                };

                headerGroup.Controls.AddRange(new Control[] {
                    returnNoLabel, txtReturnNumber, barcodeLabel, txtBarcode,
                    supplierLabel, cmbSupplier, refPurchaseLabel, cmbReferencePurchase,
                    returnDateLabel, dtpReturnDate, taxAdjustLabel, txtTaxAdjust,
                    discountAdjustLabel, txtDiscountAdjust, freightAdjustLabel, txtFreightAdjust,
                    netAmountLabel, txtNetReturnAmount, reasonLabel, txtReason
                });

                barcodeGroup.Controls.Add(barcodePanel);

                // Items Group
                GroupBox itemsGroup = new GroupBox
                {
                    Text = "📦 Return Items",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(1420, 300),
                    Location = new Point(20, 320)
                };

                // Product selection
                Label productLabel = new Label { Text = "Product:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                ComboBox cmbProduct = new ComboBox { Name = "cmbProduct", Location = new Point(120, 28), Size = new Size(200, 25), DropDownStyle = ComboBoxStyle.DropDownList };
                StyleComboBox(cmbProduct);
                cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;

                // Quantity
                Label quantityLabel = new Label { Text = "Quantity:", Location = new Point(350, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtQuantity = new TextBox { Name = "txtQuantity", Location = new Point(420, 28), Size = new Size(80, 25), Text = "0" };
                StyleTextBox(txtQuantity);
                txtQuantity.TextChanged += TxtQuantity_TextChanged;
                txtQuantity.Leave += TxtQuantity_Leave;

                // Available Quantity Label
                Label availableQtyLabel = new Label { Name = "lblAvailableQty", Text = "Available: 0", Location = new Point(510, 30), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Italic), ForeColor = Color.FromArgb(52, 152, 219) };

                // Unit Price
                Label unitPriceLabel = new Label { Text = "Unit Price:", Location = new Point(520, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtUnitPrice = new TextBox { Name = "txtUnitPrice", Location = new Point(600, 28), Size = new Size(80, 25), Text = "0.00" };
                StyleTextBox(txtUnitPrice);
                txtUnitPrice.TextChanged += TxtUnitPrice_TextChanged;

                // Line Total
                Label lineTotalLabel = new Label { Text = "Line Total:", Location = new Point(700, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtLineTotal = new TextBox { Name = "txtLineTotal", Location = new Point(780, 28), Size = new Size(80, 25), ReadOnly = true, Text = "0.00" };
                StyleTextBox(txtLineTotal, true);

                // Batch Number
                Label batchLabel = new Label { Text = "Batch No:", Location = new Point(20, 70), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                TextBox txtBatchNumber = new TextBox { Name = "txtBatchNumber", Location = new Point(120, 68), Size = new Size(120, 25) };
                StyleTextBox(txtBatchNumber);

                // Expiry Date
                Label expiryLabel = new Label { Text = "Expiry Date:", Location = new Point(260, 70), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                DateTimePicker dtpExpiryDate = new DateTimePicker { Name = "dtpExpiryDate", Location = new Point(350, 68), Size = new Size(120, 25), Value = DateTime.Now.AddYears(1) };
                StyleDateTimePicker(dtpExpiryDate);

                // Add Item Button
                Button btnAddItem = new Button
                {
                    Text = "➕ Add Item",
                    Location = new Point(500, 68),
                    Size = new Size(100, 30),
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                btnAddItem.Click += BtnAddItem_Click;

                // Items DataGridView
                DataGridView dgvPurchaseReturnItems = new DataGridView
                {
                    Name = "dgvPurchaseReturnItems",
                    Location = new Point(20, 110),
                    Size = new Size(1380, 150),
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = true,
                    ReadOnly = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    RowHeadersVisible = false,
                    BackgroundColor = Color.White,
                    GridColor = Color.FromArgb(189, 195, 199),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    ColumnHeadersHeight = 35,
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        BackColor = Color.FromArgb(52, 152, 219),
                        ForeColor = Color.White,
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    },
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        SelectionBackColor = Color.FromArgb(52, 152, 219),
                        SelectionForeColor = Color.White
                    }
                };
                dgvPurchaseReturnItems.UserDeletingRow += DgvPurchaseReturnItems_UserDeletingRow;
                dgvPurchaseReturnItems.RowsRemoved += DgvPurchaseReturnItems_RowsRemoved;

                itemsGroup.Controls.AddRange(new Control[] {
                    productLabel, cmbProduct, quantityLabel, txtQuantity, availableQtyLabel,
                    unitPriceLabel, txtUnitPrice, lineTotalLabel, txtLineTotal,
                    batchLabel, txtBatchNumber, expiryLabel, dtpExpiryDate,
                    btnAddItem, dgvPurchaseReturnItems
                });

                // Actions Group
                GroupBox actionsGroup = new GroupBox
                {
                    Text = "⚡ Actions",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(1420, 100),
                    Location = new Point(20, 640)
                };

                // Save Draft Button
                Button btnSaveDraft = new Button
                {
                    Text = "💾 Save Draft",
                    Location = new Point(20, 30),
                    Size = new Size(120, 40),
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                btnSaveDraft.Click += BtnSaveDraft_Click;

                // Post Button
                Button btnPost = new Button
                {
                    Text = "📤 Post",
                    Location = new Point(160, 30),
                    Size = new Size(120, 40),
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                btnPost.Click += BtnPost_Click;

                // Clear Button
                Button btnClear = new Button
                {
                    Text = "🗑️ Clear",
                    Location = new Point(300, 30),
                    Size = new Size(120, 40),
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                btnClear.Click += BtnClear_Click;

                actionsGroup.Controls.AddRange(new Control[] { btnSaveDraft, btnPost, btnClear });

                // Purchase Return List Group
                GroupBox listGroup = new GroupBox
                {
                    Text = "📋 Purchase Return List",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(1420, 300),
                    Location = new Point(20, 760)
                };

                DataGridView dgvPurchaseReturnList = new DataGridView
                {
                    Name = "dgvPurchaseReturnList",
                    Location = new Point(20, 30),
                    Size = new Size(1380, 250),
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    MultiSelect = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    RowHeadersVisible = false,
                    BackgroundColor = Color.White,
                    GridColor = Color.FromArgb(189, 195, 199),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    ColumnHeadersHeight = 35,
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        Font = new Font("Segoe UI", 8, FontStyle.Bold),
                        BackColor = Color.FromArgb(52, 152, 219),
                        ForeColor = Color.White,
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    },
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        SelectionBackColor = Color.FromArgb(52, 152, 219),
                        SelectionForeColor = Color.White
                    }
                };
                dgvPurchaseReturnList.SelectionChanged += DgvPurchaseReturnList_SelectionChanged;

                listGroup.Controls.Add(dgvPurchaseReturnList);

                contentPanel.Controls.AddRange(new Control[] { headerGroup, barcodeGroup, itemsGroup, actionsGroup, listGroup });
                this.Controls.Add(headerPanel);
                this.Controls.Add(contentPanel);

                Debug.WriteLine("PurchaseReturnForm: UI initialized successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: UI initialization error - {ex.Message}");
                throw;
            }
        }

        private void InitializeConnection()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Initializing database connection");
                connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True");
                Debug.WriteLine("PurchaseReturnForm: Database connection initialized");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Connection error - {ex.Message}");
                throw;
            }
        }

        private void InitializeServices()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Initializing services");
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True";
                _purchaseReturnRepository = new PurchaseReturnRepository(connectionString);
                _purchaseReturnItemRepository = new PurchaseReturnItemRepository(connectionString);
                _purchaseReturnService = new PurchaseReturnService(_purchaseReturnRepository, _purchaseReturnItemRepository);
                Debug.WriteLine("PurchaseReturnForm: Services initialized");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Services initialization error - {ex.Message}");
                throw;
            }
        }

        private void InitializeDataTable()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Initializing data table");
                purchaseReturnItemsDataTable = new DataTable();
                purchaseReturnItemsDataTable.Columns.Add("ProductId", typeof(int));
                purchaseReturnItemsDataTable.Columns.Add("ProductName", typeof(string));
                purchaseReturnItemsDataTable.Columns.Add("Quantity", typeof(decimal));
                purchaseReturnItemsDataTable.Columns.Add("UnitPrice", typeof(decimal));
                purchaseReturnItemsDataTable.Columns.Add("LineTotal", typeof(decimal));
                purchaseReturnItemsDataTable.Columns.Add("BatchNo", typeof(string));
                purchaseReturnItemsDataTable.Columns.Add("ExpiryDate", typeof(DateTime));

                // Find the DataGridView control
                DataGridView dgv = FindControlRecursive(this, "dgvPurchaseReturnItems") as DataGridView;
                if (dgv != null)
                {
                    dgv.DataSource = purchaseReturnItemsDataTable;
                    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv.RowHeadersVisible = false;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = true;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.MultiSelect = false;
                    dgv.ReadOnly = false;
                    dgv.ScrollBars = ScrollBars.Vertical;

                // Configure columns
                    dgv.Columns["ProductId"].Visible = false;
                    dgv.Columns["ProductName"].HeaderText = "Product";
                    dgv.Columns["ProductName"].ReadOnly = true;
                    dgv.Columns["Quantity"].HeaderText = "Quantity";
                    dgv.Columns["UnitPrice"].HeaderText = "Unit Price";
                    dgv.Columns["LineTotal"].HeaderText = "Line Total";
                    dgv.Columns["LineTotal"].ReadOnly = true;
                    dgv.Columns["BatchNo"].HeaderText = "Batch No";
                    dgv.Columns["ExpiryDate"].HeaderText = "Expiry Date";
                }
                
                Debug.WriteLine("PurchaseReturnForm: Data table initialized");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Data table initialization error - {ex.Message}");
                throw;
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Loading suppliers");
                string query = "SELECT SupplierId, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    suppliersDataTable = new DataTable();
                    adapter.Fill(suppliersDataTable);
                }

                ComboBox cmbSupplier = FindControlRecursive(this, "cmbSupplier") as ComboBox;
                if (cmbSupplier != null)
                {
                    cmbSupplier.DataSource = suppliersDataTable;
                    cmbSupplier.DisplayMember = "SupplierName";
                    cmbSupplier.ValueMember = "SupplierId";
                    cmbSupplier.SelectedIndex = -1;
                }

                Debug.WriteLine($"PurchaseReturnForm: Loaded {suppliersDataTable.Rows.Count} suppliers");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load suppliers error - {ex.Message}");
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Loading products");
                string query = "SELECT ProductId, ProductName FROM Products WHERE IsActive = 1 ORDER BY ProductName";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    productsDataTable = new DataTable();
                    adapter.Fill(productsDataTable);
                }

                ComboBox cmbProduct = FindControlRecursive(this, "cmbProduct") as ComboBox;
                if (cmbProduct != null)
                {
                    cmbProduct.DataSource = productsDataTable;
                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductId";
                    cmbProduct.SelectedIndex = -1;
                    cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
                }

                Debug.WriteLine($"PurchaseReturnForm: Loaded {productsDataTable.Rows.Count} products");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load products error - {ex.Message}");
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPurchases()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Loading purchases");
                string query = @"SELECT pi.PurchaseInvoiceId, pi.InvoiceNumber, s.SupplierName, pi.InvoiceDate, pi.TotalAmount, pi.Status
                                FROM PurchaseInvoices pi
                                INNER JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                                ORDER BY pi.InvoiceDate DESC";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    purchasesDataTable = new DataTable();
                    adapter.Fill(purchasesDataTable);
                }

                Debug.WriteLine($"PurchaseReturnForm: Found {purchasesDataTable.Rows.Count} purchases in database");

                ComboBox cmbReferencePurchase = FindControlRecursive(this, "cmbReferencePurchase") as ComboBox;
                if (cmbReferencePurchase != null)
                {
                    Debug.WriteLine("PurchaseReturnForm: Found cmbReferencePurchase control");
                    cmbReferencePurchase.DataSource = purchasesDataTable;
                    cmbReferencePurchase.DisplayMember = "InvoiceNumber";
                    cmbReferencePurchase.ValueMember = "PurchaseInvoiceId";
                    cmbReferencePurchase.SelectedIndex = -1;
                    cmbReferencePurchase.SelectedIndexChanged += CmbReferencePurchase_SelectedIndexChanged;
                    Debug.WriteLine($"PurchaseReturnForm: Bound {purchasesDataTable.Rows.Count} items to reference purchase dropdown");
                }
                else
                {
                    Debug.WriteLine("PurchaseReturnForm: ERROR - cmbReferencePurchase control not found!");
                }

                Debug.WriteLine($"PurchaseReturnForm: Loaded {purchasesDataTable.Rows.Count} purchases");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load purchases error - {ex.Message}");
                MessageBox.Show($"Error loading purchases: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void GenerateReturnNumber()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Generating return number");
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(ReturnNo, 4, LEN(ReturnNo)) AS INT)), 0) + 1 FROM PurchaseReturns WHERE ReturnNo LIKE 'PRT%'";
                
                string returnNumber = "";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int nextNumber = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                    
                    returnNumber = $"PRT{nextNumber:D6}";
                    
                    TextBox txtReturnNumber = FindControlRecursive(this, "txtReturnNumber") as TextBox;
                    if (txtReturnNumber != null)
                    {
                txtReturnNumber.Text = returnNumber;
                    }
                }

                Debug.WriteLine($"PurchaseReturnForm: Generated return number {returnNumber}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Generate return number error - {ex.Message}");
                MessageBox.Show($"Error generating return number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetNextReturnNumber()
        {
            try
            {
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(ReturnNo, 4, LEN(ReturnNo)) AS INT)), 0) + 1 FROM PurchaseReturns WHERE ReturnNo LIKE 'PRT%'";
                
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int nextNumber = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                    return $"PRT{nextNumber:D6}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Get next return number error - {ex.Message}");
                return $"PRT{DateTime.Now:yyyyMMddHHmmss}";
            }
        }

        private void GenerateBarcode()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Generating barcode");
                string barcode = $"PRT{DateTime.Now:yyyyMMddHHmmss}";
                
                TextBox txtBarcode = FindControlRecursive(this, "txtBarcode") as TextBox;
                if (txtBarcode != null)
                {
                txtBarcode.Text = barcode;
                }

                // Generate barcode image
                GenerateBarcodeImage(barcode);

                Debug.WriteLine($"PurchaseReturnForm: Generated barcode {barcode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Generate barcode error - {ex.Message}");
            }
        }

        private void GenerateBarcodeImage(string barcodeText)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnForm: Generating barcode image for {barcodeText}");
                
                // Create barcode image matching the pattern used in other forms
                Bitmap barcodeBitmap = new Bitmap(300, 80);
                using (Graphics g = Graphics.FromImage(barcodeBitmap))
                {
                    g.Clear(Color.White);
                    
                    // Draw barcode bars (consistent pattern like other forms)
                    Random rand = new Random(barcodeText.GetHashCode()); // Use text hash for consistent pattern
                    int barWidth = 2;
                    int x = 20;
                    
                    for (int i = 0; i < barcodeText.Length * 4 && x < 280; i++)
                    {
                        int barHeight = rand.Next(20, 60);
                        g.FillRectangle(Brushes.Black, x, 10, barWidth, barHeight);
                        x += barWidth + 1;
                    }
                    
                    // No text in barcode image - only lines
                }

                Panel barcodePanel = FindControlRecursive(this, "pnlBarcode") as Panel;
                if (barcodePanel != null)
                {
                    barcodePanel.BackgroundImage = barcodeBitmap;
                    barcodePanel.BackgroundImageLayout = ImageLayout.Center;
                }

                Debug.WriteLine("PurchaseReturnForm: Barcode image generated");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Barcode image generation error - {ex.Message}");
            }
        }

        private void LoadPurchaseReturnList()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Loading purchase return list");
                string query = @"SELECT pr.ReturnId, pr.ReturnNo, s.SupplierName, pr.ReturnDate, pr.NetReturnAmount, pr.Reason
                                FROM PurchaseReturns pr
                                INNER JOIN Suppliers s ON pr.SupplierId = s.SupplierId
                                ORDER BY pr.ReturnDate DESC";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    DataGridView dgvPurchaseReturnList = this.Controls.Find("dgvPurchaseReturnList", true).FirstOrDefault() as DataGridView;
                    if (dgvPurchaseReturnList != null)
                    {
                        dgvPurchaseReturnList.DataSource = dt;
                        dgvPurchaseReturnList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }

                Debug.WriteLine("PurchaseReturnForm: Purchase return list loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load purchase return list error - {ex.Message}");
                MessageBox.Show($"Error loading purchase return list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFormReadOnly(bool readOnly)
        {
            try
            {
                // Implementation for setting form controls read-only
                Debug.WriteLine($"PurchaseReturnForm: Set form read only: {readOnly}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Set form read only error - {ex.Message}");
            }
        }

        // Event Handlers
        private async void BtnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Adding item");
                
                ComboBox cmbProduct = FindControlRecursive(this, "cmbProduct") as ComboBox;
                TextBox txtQuantity = FindControlRecursive(this, "txtQuantity") as TextBox;
                TextBox txtUnitPrice = FindControlRecursive(this, "txtUnitPrice") as TextBox;
                TextBox txtBatchNumber = FindControlRecursive(this, "txtBatchNumber") as TextBox;
                DateTimePicker dtpExpiryDate = FindControlRecursive(this, "dtpExpiryDate") as DateTimePicker;

                if (cmbProduct?.SelectedValue == null)
                {
                    MessageBox.Show("Please select a product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtQuantity?.Text) || Convert.ToDecimal(txtQuantity.Text) <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal quantity = Convert.ToDecimal(txtQuantity.Text);
                decimal unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                decimal lineTotal = quantity * unitPrice;

                DataRow newRow = purchaseReturnItemsDataTable.NewRow();
                newRow["ProductId"] = cmbProduct.SelectedValue;
                newRow["ProductName"] = cmbProduct.Text;
                newRow["Quantity"] = quantity;
                newRow["UnitPrice"] = unitPrice;
                newRow["LineTotal"] = lineTotal;
                newRow["BatchNo"] = txtBatchNumber.Text;
                newRow["ExpiryDate"] = dtpExpiryDate.Value;

                purchaseReturnItemsDataTable.Rows.Add(newRow);

                // Update available quantity display
                await UpdateAvailableQuantityDisplay();

                // Clear item controls
                cmbProduct.SelectedIndex = -1;
                txtQuantity.Clear();
                txtUnitPrice.Clear();
                txtBatchNumber.Clear();
                dtpExpiryDate.Value = DateTime.Now.AddYears(1);

                CalculateNetAmount();

                Debug.WriteLine("PurchaseReturnForm: Item added successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Add item error - {ex.Message}");
                MessageBox.Show($"Error adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSaveDraft_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Saving draft");
                
                if (!ValidateForm())
                {
                    return;
                }

                var purchaseReturn = CreatePurchaseReturnFromForm();
                string returnNumber = purchaseReturn.ReturnNumber;

                var purchaseReturnId = await _purchaseReturnService.CreatePurchaseReturnAsync(purchaseReturn);
                
                if (purchaseReturnId > 0)
                {
                    MessageBox.Show($"Draft saved successfully! Return ID: {purchaseReturnId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPurchaseReturnList();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to save draft. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Save draft error - {ex.Message}");
                MessageBox.Show($"Error saving draft: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnPost_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Posting purchase return");
                
                if (!ValidateForm())
                {
                    return;
                }

                // Confirm posting
                var result = MessageBox.Show(
                    "Are you sure you want to post this purchase return?\n\nThis will:\n- Reduce inventory quantities\n- Create ledger entries\n- Mark the return as posted\n\nThis action cannot be undone.",
                    "Confirm Post",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                // First save as draft if not already saved
                var purchaseReturn = CreatePurchaseReturnFromForm();
                int purchaseReturnId;

                if (currentPurchaseReturnId == 0)
                {
                    purchaseReturnId = await _purchaseReturnService.CreatePurchaseReturnAsync(purchaseReturn);
                    if (purchaseReturnId <= 0)
                    {
                        MessageBox.Show("Failed to save purchase return before posting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    purchaseReturnId = currentPurchaseReturnId;
                }

                // Post the purchase return
                var postResult = await _purchaseReturnService.PostPurchaseReturnAsync(purchaseReturnId);
                
                if (postResult)
                {
                    MessageBox.Show("Purchase return posted successfully!\n\nInventory has been reduced and ledger entries created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPurchaseReturnList();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to post purchase return. Please check inventory levels and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Post error - {ex.Message}");
                MessageBox.Show($"Error posting purchase return: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void DgvPurchaseReturnList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv.SelectedRows.Count > 0)
                {
                    int returnId = Convert.ToInt32(dgv.SelectedRows[0].Cells["ReturnId"].Value);
                    Debug.WriteLine($"PurchaseReturnForm: Purchase return selected from list: {returnId}");
                    // Load purchase return details
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Purchase return list selection error - {ex.Message}");
            }
        }

        private async void CmbReferencePurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmbReferencePurchase = sender as ComboBox;
                ComboBox cmbProduct = FindControlRecursive(this, "cmbProduct") as ComboBox;
                
                Debug.WriteLine($"PurchaseReturnForm: Reference purchase selection changed. SelectedValue: {cmbReferencePurchase.SelectedValue}, SelectedIndex: {cmbReferencePurchase.SelectedIndex}");
                
                if (cmbReferencePurchase.SelectedValue != null && cmbProduct != null)
                {
                    int invoiceId;
                    if (cmbReferencePurchase.SelectedValue is DataRowView rowView)
                    {
                        invoiceId = Convert.ToInt32(rowView["PurchaseInvoiceId"]);
                    }
                    else
                    {
                        invoiceId = Convert.ToInt32(cmbReferencePurchase.SelectedValue);
                    }
                    Debug.WriteLine($"PurchaseReturnForm: Reference purchase selected: {invoiceId}");
                    
                    // Load products from the selected invoice using new service method
                    var products = await _purchaseReturnService.GetProductsFromInvoiceAsync(invoiceId);
                    
                    cmbProduct.DataSource = products;
                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductId";
                    cmbProduct.SelectedIndex = -1;
                    
                    Debug.WriteLine($"PurchaseReturnForm: Loaded {products.Count} products from invoice {invoiceId}");
                    
                    // Update supplier if different
                    UpdateSupplierFromPurchase(invoiceId);
                    
                    // Auto-fill all invoice details
                    AutoFillInvoiceDetails(invoiceId);
                }
                else
                {
                    Debug.WriteLine("PurchaseReturnForm: No reference purchase selected, resetting to all products");
                    // Reset to all products if no reference purchase
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Reference purchase selection error - {ex.Message}");
                MessageBox.Show($"Error loading products from invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductsFromPurchase(int purchaseId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnForm: Loading products from purchase {purchaseId}");
                
                string query = @"SELECT pid.DetailId, p.ProductId, p.ProductName, pid.UnitPrice, pid.Quantity, 
                                       pid.BatchNumber, pid.ExpiryDate, pid.TotalAmount
                                FROM Products p
                                INNER JOIN PurchaseInvoiceDetails pid ON p.ProductId = pid.ProductId
                                WHERE pid.PurchaseInvoiceId = @PurchaseId
                                AND p.IsActive = 1
                                ORDER BY p.ProductName";

                DataTable purchaseProductsDataTable;
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PurchaseId", purchaseId);
                    connection.Open();
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        purchaseProductsDataTable = new DataTable();
                        purchaseProductsDataTable.Load(reader);
                    }
                }

                Debug.WriteLine($"PurchaseReturnForm: Query executed, found {purchaseProductsDataTable.Rows.Count} products in database");

                ComboBox cmbProduct = FindControlRecursive(this, "cmbProduct") as ComboBox;
                if (cmbProduct != null)
                {
                    Debug.WriteLine("PurchaseReturnForm: Found cmbProduct control, binding data");
                    cmbProduct.DataSource = purchaseProductsDataTable;
                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductId";
                cmbProduct.SelectedIndex = -1;
                    cmbProduct.Tag = purchaseProductsDataTable; // Store for unit price lookup
                    cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;
                }
                else
                {
                    Debug.WriteLine("PurchaseReturnForm: ERROR - cmbProduct control not found!");
                }

                Debug.WriteLine($"PurchaseReturnForm: Loaded {purchaseProductsDataTable.Rows.Count} products from purchase {purchaseId}");
                
                // Note: We don't auto-populate the grid - user must manually add items
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load products from purchase error - {ex.Message}");
                Debug.WriteLine($"PurchaseReturnForm: Stack trace - {ex.StackTrace}");
                MessageBox.Show($"Error loading products from purchase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void AutoPopulateItemsGrid(DataTable purchaseProductsDataTable)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnForm: Auto-populating items grid with {purchaseProductsDataTable.Rows.Count} products");
                
                // Debug: Check DataTable structure
                Debug.WriteLine($"PurchaseReturnForm: Source DataTable columns: {string.Join(", ", purchaseProductsDataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                Debug.WriteLine($"PurchaseReturnForm: Target DataTable columns: {string.Join(", ", purchaseReturnItemsDataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                
                // Clear existing items
                purchaseReturnItemsDataTable.Clear();
                Debug.WriteLine($"PurchaseReturnForm: Cleared existing items, target table now has {purchaseReturnItemsDataTable.Rows.Count} rows");
                
                // Add each product from the invoice to the items grid
                foreach (DataRow row in purchaseProductsDataTable.Rows)
                {
                    try
                    {
                        DataRow newRow = purchaseReturnItemsDataTable.NewRow();
                        newRow["ProductId"] = row["ProductId"];
                        newRow["ProductName"] = row["ProductName"];
                        newRow["Quantity"] = row["Quantity"];
                        newRow["UnitPrice"] = row["UnitPrice"];
                        newRow["LineTotal"] = row["TotalAmount"];
                        newRow["BatchNo"] = row["BatchNumber"];
                        newRow["ExpiryDate"] = row["ExpiryDate"];
                        purchaseReturnItemsDataTable.Rows.Add(newRow);
                        
                        Debug.WriteLine($"PurchaseReturnForm: Added item - {row["ProductName"]}, Qty: {row["Quantity"]}, Price: {row["UnitPrice"]}");
                    }
                    catch (Exception rowEx)
                    {
                        Debug.WriteLine($"PurchaseReturnForm: Error adding row - {rowEx.Message}");
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnForm: After adding items, target table has {purchaseReturnItemsDataTable.Rows.Count} rows");
                
                // Refresh the DataGridView
                DataGridView dgvPurchaseReturnItems = FindControlRecursive(this, "dgvPurchaseReturnItems") as DataGridView;
                if (dgvPurchaseReturnItems != null)
                {
                    Debug.WriteLine("PurchaseReturnForm: Found dgvPurchaseReturnItems control, refreshing data source");
                    dgvPurchaseReturnItems.DataSource = purchaseReturnItemsDataTable;
                    
                    // Ensure the grid is visible and properly sized
                    dgvPurchaseReturnItems.Refresh();
                    dgvPurchaseReturnItems.AutoResizeColumns();
                    dgvPurchaseReturnItems.AutoResizeRows();
                    
                    Debug.WriteLine($"PurchaseReturnForm: DataGridView refreshed, it now shows {dgvPurchaseReturnItems.Rows.Count} rows");
                    Debug.WriteLine($"PurchaseReturnForm: DataGridView size: {dgvPurchaseReturnItems.Size}, Location: {dgvPurchaseReturnItems.Location}");
                }
                else
                {
                    Debug.WriteLine("PurchaseReturnForm: ERROR - dgvPurchaseReturnItems control not found!");
                }
                
                Debug.WriteLine($"PurchaseReturnForm: Auto-populated items grid with {purchaseReturnItemsDataTable.Rows.Count} items");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Auto-populate items grid error - {ex.Message}");
                Debug.WriteLine($"PurchaseReturnForm: Stack trace - {ex.StackTrace}");
            }
        }

        private void AutoFillInvoiceDetails(int purchaseId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnForm: Auto-filling invoice details for purchase {purchaseId}");
                
                string query = @"SELECT pi.InvoiceNumber, pi.InvoiceDate, pi.SubTotal, pi.TaxAmount, 
                                       pi.DiscountAmount, pi.FreightAmount, pi.TotalAmount, pi.Status,
                                       s.SupplierName, s.SupplierId
                                FROM PurchaseInvoices pi
                                INNER JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                                WHERE pi.PurchaseInvoiceId = @PurchaseId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PurchaseId", purchaseId);
                    connection.Open();
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Auto-fill Tax Adjust
                            TextBox txtTaxAdjust = FindControlRecursive(this, "txtTaxAdjust") as TextBox;
                            if (txtTaxAdjust != null)
                            {
                                txtTaxAdjust.Text = reader["TaxAmount"].ToString();
                            }

                            // Auto-fill Discount Adjust
                            TextBox txtDiscountAdjust = FindControlRecursive(this, "txtDiscountAdjust") as TextBox;
                            if (txtDiscountAdjust != null)
                            {
                                txtDiscountAdjust.Text = reader["DiscountAmount"].ToString();
                            }

                            // Auto-fill Freight Adjust
                            TextBox txtFreightAdjust = FindControlRecursive(this, "txtFreightAdjust") as TextBox;
                            if (txtFreightAdjust != null)
                            {
                                txtFreightAdjust.Text = reader["FreightAmount"].ToString();
                            }

                            // Auto-fill Net Return Amount (initially same as TotalAmount)
                            TextBox txtNetReturnAmount = FindControlRecursive(this, "txtNetReturnAmount") as TextBox;
                            if (txtNetReturnAmount != null)
                            {
                                txtNetReturnAmount.Text = reader["TotalAmount"].ToString();
                            }

                            // Auto-fill Return Date (set to today)
                            DateTimePicker dtpReturnDate = FindControlRecursive(this, "dtpReturnDate") as DateTimePicker;
                            if (dtpReturnDate != null)
                            {
                                dtpReturnDate.Value = DateTime.Now;
                            }

                            // Auto-fill Reason with invoice reference
                            TextBox txtReason = FindControlRecursive(this, "txtReason") as TextBox;
                            if (txtReason != null)
                            {
                                txtReason.Text = $"Return for Invoice: {reader["InvoiceNumber"]}";
                            }

                            Debug.WriteLine($"PurchaseReturnForm: Auto-filled invoice details - Tax: {reader["TaxAmount"]}, Discount: {reader["DiscountAmount"]}, Freight: {reader["FreightAmount"]}, Total: {reader["TotalAmount"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Auto-fill invoice details error - {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void UpdateSupplierFromPurchase(int purchaseId)
        {
            try
            {
                string query = @"SELECT pi.SupplierId, s.SupplierName
                                FROM PurchaseInvoices pi
                                INNER JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                                WHERE pi.PurchaseInvoiceId = @PurchaseId";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PurchaseId", purchaseId);
                    connection.Open();
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int supplierId = Convert.ToInt32(reader["SupplierId"]);
                            string supplierName = reader["SupplierName"].ToString();
                            
                            ComboBox cmbSupplier = FindControlRecursive(this, "cmbSupplier") as ComboBox;
                            if (cmbSupplier != null)
                            {
                                // Find and select the supplier
                                for (int i = 0; i < cmbSupplier.Items.Count; i++)
                                {
                                    DataRowView supplierRow = (DataRowView)cmbSupplier.Items[i];
                                    if (Convert.ToInt32(supplierRow["SupplierId"]) == supplierId)
                                    {
                                        cmbSupplier.SelectedIndex = i;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Update supplier from purchase error - {ex.Message}");
            }
        }

        private async void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmbProduct = sender as ComboBox;
                ComboBox cmbReferencePurchase = FindControlRecursive(this, "cmbReferencePurchase") as ComboBox;
                
                if (cmbProduct?.SelectedValue != null && cmbReferencePurchase?.SelectedValue != null)
                {
                    Debug.WriteLine($"PurchaseReturnForm: Product selected: {cmbProduct.Text}");
                    
                    int productId;
                    if (cmbProduct.SelectedValue is DataRowView productRowView)
                    {
                        productId = Convert.ToInt32(productRowView["ProductId"]);
                    }
                    else
                    {
                        productId = Convert.ToInt32(cmbProduct.SelectedValue);
                    }
                    
                    int invoiceId;
                    if (cmbReferencePurchase.SelectedValue is DataRowView invoiceRowView)
                    {
                        invoiceId = Convert.ToInt32(invoiceRowView["PurchaseInvoiceId"]);
                    }
                    else
                    {
                        invoiceId = Convert.ToInt32(cmbReferencePurchase.SelectedValue);
                    }
                    
                    // Get the selected product details from the invoice
                    var products = await _purchaseReturnService.GetProductsFromInvoiceAsync(invoiceId);
                    var selectedProduct = products.FirstOrDefault(p => p.ProductId == productId);
                    
                    if (selectedProduct != null)
                    {
                        // Auto-populate fields with invoice data
                        TextBox txtQuantity = FindControlRecursive(this, "txtQuantity") as TextBox;
                        TextBox txtUnitPrice = FindControlRecursive(this, "txtUnitPrice") as TextBox;
                        TextBox txtBatchNumber = FindControlRecursive(this, "txtBatchNumber") as TextBox;
                        DateTimePicker dtpExpiryDate = FindControlRecursive(this, "dtpExpiryDate") as DateTimePicker;
                        Label lblAvailableQty = FindControlRecursive(this, "lblAvailableQty") as Label;
                        
                        if (txtQuantity != null)
                        {
                            txtQuantity.Text = "0"; // Start with 0 for return quantity
                        }
                        
                        if (txtUnitPrice != null)
                        {
                            txtUnitPrice.Text = selectedProduct.UnitPrice.ToString("F2");
                        }
                        
                        if (txtBatchNumber != null)
                        {
                            txtBatchNumber.Text = selectedProduct.BatchNo ?? "";
                        }
                        
                        if (dtpExpiryDate != null)
                        {
                            dtpExpiryDate.Value = selectedProduct.ExpiryDate ?? DateTime.Now.AddYears(1);
                        }
                        
                        if (lblAvailableQty != null)
                        {
                            // Update available quantity display (considering items already in grid)
                            await UpdateAvailableQuantityDisplay();
                        }
                        
                        CalculateLineTotal();
                        Debug.WriteLine($"PurchaseReturnForm: Auto-filled product details for {selectedProduct.ProductName}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Product selection error - {ex.Message}");
                MessageBox.Show($"Error loading product details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateLineTotal();
        }

        private void TxtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateLineTotal();
        }

        private void CalculateLineTotal()
        {
            try
            {
                TextBox txtQuantity = this.Controls.Find("txtQuantity", true).FirstOrDefault() as TextBox;
                TextBox txtUnitPrice = this.Controls.Find("txtUnitPrice", true).FirstOrDefault() as TextBox;
                TextBox txtLineTotal = this.Controls.Find("txtLineTotal", true).FirstOrDefault() as TextBox;

                if (txtQuantity != null && txtUnitPrice != null && txtLineTotal != null)
                {
                    if (decimal.TryParse(txtQuantity.Text, out decimal quantity) && 
                        decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice))
                    {
                        decimal lineTotal = quantity * unitPrice;
                        txtLineTotal.Text = lineTotal.ToString("N2");
                    }
                    else
                    {
                        txtLineTotal.Text = "0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Calculate line total error - {ex.Message}");
            }
        }

        private void CalculateNetAmount()
        {
            try
            {
                decimal totalAmount = 0;
                foreach (DataRow row in purchaseReturnItemsDataTable.Rows)
                {
                    totalAmount += Convert.ToDecimal(row["LineTotal"]);
                }

                TextBox txtTaxAdjust = this.Controls.Find("txtTaxAdjust", true).FirstOrDefault() as TextBox;
                TextBox txtDiscountAdjust = this.Controls.Find("txtDiscountAdjust", true).FirstOrDefault() as TextBox;
                TextBox txtFreightAdjust = this.Controls.Find("txtFreightAdjust", true).FirstOrDefault() as TextBox;
                TextBox txtNetReturnAmount = this.Controls.Find("txtNetReturnAmount", true).FirstOrDefault() as TextBox;

                decimal taxAdjust = string.IsNullOrEmpty(txtTaxAdjust.Text) ? 0 : Convert.ToDecimal(txtTaxAdjust.Text);
                decimal discountAdjust = string.IsNullOrEmpty(txtDiscountAdjust.Text) ? 0 : Convert.ToDecimal(txtDiscountAdjust.Text);
                decimal freightAdjust = string.IsNullOrEmpty(txtFreightAdjust.Text) ? 0 : Convert.ToDecimal(txtFreightAdjust.Text);

                decimal netAmount = totalAmount + taxAdjust - discountAdjust + freightAdjust;

                if (txtNetReturnAmount != null)
                {
                    txtNetReturnAmount.Text = netAmount.ToString("N2");
                }

                Debug.WriteLine($"PurchaseReturnForm: Net amount calculated: {netAmount}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Calculate net amount error - {ex.Message}");
            }
        }

        private bool ValidateForm()
        {
            try
            {
                ComboBox cmbSupplier = this.Controls.Find("cmbSupplier", true).FirstOrDefault() as ComboBox;
                if (cmbSupplier.SelectedValue == null)
                {
                    MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                if (purchaseReturnItemsDataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Please add at least one item to return.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Validation error - {ex.Message}");
                MessageBox.Show($"Validation error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private PurchaseReturn CreatePurchaseReturnFromForm()
        {
            try
            {
                TextBox txtReturnNumber = this.Controls.Find("txtReturnNumber", true).FirstOrDefault() as TextBox;
                TextBox txtBarcode = this.Controls.Find("txtBarcode", true).FirstOrDefault() as TextBox;
                ComboBox cmbSupplier = this.Controls.Find("cmbSupplier", true).FirstOrDefault() as ComboBox;
                ComboBox cmbReferencePurchase = this.Controls.Find("cmbReferencePurchase", true).FirstOrDefault() as ComboBox;
                DateTimePicker dtpReturnDate = this.Controls.Find("dtpReturnDate", true).FirstOrDefault() as DateTimePicker;
                TextBox txtTaxAdjust = this.Controls.Find("txtTaxAdjust", true).FirstOrDefault() as TextBox;
                TextBox txtDiscountAdjust = this.Controls.Find("txtDiscountAdjust", true).FirstOrDefault() as TextBox;
                TextBox txtFreightAdjust = this.Controls.Find("txtFreightAdjust", true).FirstOrDefault() as TextBox;
                TextBox txtNetReturnAmount = this.Controls.Find("txtNetReturnAmount", true).FirstOrDefault() as TextBox;
                TextBox txtReason = this.Controls.Find("txtReason", true).FirstOrDefault() as TextBox;

                // Generate new return number if this is a new return
                string returnNumber = txtReturnNumber.Text;
                if (currentPurchaseReturnId == 0)
                {
                    returnNumber = GetNextReturnNumber();
                }

                return new PurchaseReturn
                {
                    ReturnNumber = returnNumber,
                    Barcode = txtBarcode.Text,
                    SupplierId = Convert.ToInt32(cmbSupplier.SelectedValue),
                    ReferencePurchaseId = null, // Don't set reference purchase for now due to FK constraint
                    ReturnDate = dtpReturnDate.Value,
                    TaxAdjust = string.IsNullOrEmpty(txtTaxAdjust.Text) ? 0 : Convert.ToDecimal(txtTaxAdjust.Text),
                    DiscountAdjust = string.IsNullOrEmpty(txtDiscountAdjust.Text) ? 0 : Convert.ToDecimal(txtDiscountAdjust.Text),
                    FreightAdjust = string.IsNullOrEmpty(txtFreightAdjust.Text) ? 0 : Convert.ToDecimal(txtFreightAdjust.Text),
                    NetReturnAmount = string.IsNullOrEmpty(txtNetReturnAmount.Text) ? 0 : Convert.ToDecimal(txtNetReturnAmount.Text),
                    Reason = txtReason.Text,
                    CreatedBy = UserSession.CurrentUser?.UserId ?? 1,
                    CreatedDate = DateTime.Now,
                    Items = CreatePurchaseReturnItemsFromDataTable()
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Create purchase return error - {ex.Message}");
                throw;
            }
        }

        private List<PurchaseReturnItem> CreatePurchaseReturnItemsFromDataTable()
        {
            try
            {
                List<PurchaseReturnItem> items = new List<PurchaseReturnItem>();

                foreach (DataRow row in purchaseReturnItemsDataTable.Rows)
                {
                    items.Add(new PurchaseReturnItem
                    {
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        Quantity = Convert.ToDecimal(row["Quantity"]),
                        UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                        LineTotal = Convert.ToDecimal(row["LineTotal"]),
                        BatchNo = row["BatchNo"].ToString(),
                        ExpiryDate = Convert.ToDateTime(row["ExpiryDate"])
                    });
                }

                return items;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Create purchase return items error - {ex.Message}");
                throw;
            }
        }

        private void ClearForm()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Clearing form");
                
                // Clear all text boxes
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox)
                    {
                        ((TextBox)control).Clear();
                    }
                    else if (control is ComboBox)
                    {
                        ((ComboBox)control).SelectedIndex = -1;
                    }
                }

                // Clear data table
                    purchaseReturnItemsDataTable.Clear();

                // Reset form
                GenerateReturnNumber();
                GenerateBarcode();
                SetFormReadOnly(false);

                Debug.WriteLine("PurchaseReturnForm: Form cleared");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Clear form error - {ex.Message}");
                MessageBox.Show($"Error clearing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UpdateAvailableQuantityDisplay()
        {
            try
            {
                ComboBox cmbProduct = FindControlRecursive(this, "cmbProduct") as ComboBox;
                Label lblAvailableQty = FindControlRecursive(this, "lblAvailableQty") as Label;
                
                if (cmbProduct?.SelectedValue != null && lblAvailableQty != null)
                {
                    int productId;
                    if (cmbProduct.SelectedValue is DataRowView productRowView)
                    {
                        productId = Convert.ToInt32(productRowView["ProductId"]);
                    }
                    else
                    {
                        productId = Convert.ToInt32(cmbProduct.SelectedValue);
                    }
                    
                    // Get current available stock from database
                    var availableStock = await _purchaseReturnService.GetActualAvailableStockAsync(productId);
                    
                    // Calculate quantity already in the grid for this product
                    decimal quantityInGrid = 0;
                    foreach (DataRow row in purchaseReturnItemsDataTable.Rows)
                    {
                        if (Convert.ToInt32(row["ProductId"]) == productId)
                        {
                            quantityInGrid += Convert.ToDecimal(row["Quantity"]);
                        }
                    }
                    
                    // Calculate remaining available quantity
                    decimal remainingAvailable = availableStock - quantityInGrid;
                    
                    lblAvailableQty.Text = $"Available: {remainingAvailable}";
                    
                    Debug.WriteLine($"PurchaseReturnForm: Updated available quantity - Stock: {availableStock}, In Grid: {quantityInGrid}, Remaining: {remainingAvailable}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm.UpdateAvailableQuantityDisplay: Error - {ex.Message}");
            }
        }

        private async void TxtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBox txtQuantity = sender as TextBox;
                ComboBox cmbProduct = FindControlRecursive(this, "cmbProduct") as ComboBox;
                ComboBox cmbReferencePurchase = FindControlRecursive(this, "cmbReferencePurchase") as ComboBox;
                
                if (txtQuantity != null && cmbProduct?.SelectedValue != null && cmbReferencePurchase?.SelectedValue != null)
                {
                    if (decimal.TryParse(txtQuantity.Text, out decimal returnQuantity))
                    {
                        int productId;
                        if (cmbProduct.SelectedValue is DataRowView productRowView)
                        {
                            productId = Convert.ToInt32(productRowView["ProductId"]);
                        }
                        else
                        {
                            productId = Convert.ToInt32(cmbProduct.SelectedValue);
                        }
                        
                        int invoiceId;
                        if (cmbReferencePurchase.SelectedValue is DataRowView invoiceRowView)
                        {
                            invoiceId = Convert.ToInt32(invoiceRowView["PurchaseInvoiceId"]);
                        }
                        else
                        {
                            invoiceId = Convert.ToInt32(cmbReferencePurchase.SelectedValue);
                        }
                        
                        // Validate return quantity
                        bool isValid = await _purchaseReturnService.ValidateReturnQuantityAsync(productId, invoiceId, returnQuantity);
                        
                        if (!isValid)
                        {
                            MessageBox.Show("Return quantity cannot exceed the purchased quantity from the invoice.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtQuantity.Focus();
                            txtQuantity.SelectAll();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm.TxtQuantity_Leave: Error - {ex.Message}");
                MessageBox.Show($"Error validating quantity: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DgvPurchaseReturnItems_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: User is deleting a row from the grid");
                // This event fires before the row is actually deleted
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm.DgvPurchaseReturnItems_UserDeletingRow: Error - {ex.Message}");
            }
        }

        private async void DgvPurchaseReturnItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnForm: Rows removed from grid - RowIndex: {e.RowIndex}, RowCount: {e.RowCount}");
                
                // Update available quantity display after row removal
                await UpdateAvailableQuantityDisplay();
                
                // Recalculate net amount
                CalculateNetAmount();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm.DgvPurchaseReturnItems_RowsRemoved: Error - {ex.Message}");
            }
        }
    }
}

