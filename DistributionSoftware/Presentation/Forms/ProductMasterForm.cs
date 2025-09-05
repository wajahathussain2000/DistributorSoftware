using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class ProductMasterForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private bool isEditMode = false;
        private int currentProductId = 0;

        public ProductMasterForm()
        {
            InitializeComponent();
            InitializeConnection();
            LoadCategories();
            LoadBrands();
            LoadUnits();
            LoadWarehouses();
            GenerateBarcode();
            LoadProductsGrid(); // Load existing products
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Product Master - Distribution Software";
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
                Text = "📦 Product Master",
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

            // Product Information Group
            GroupBox productGroup = new GroupBox
            {
                Text = "📋 Product Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(900, 450),
                Location = new Point(20, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            // Product Code
            Label codeLabel = new Label { Text = "🔢 Product Code:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox codeTextBox = new TextBox { Name = "txtProductCode", Location = new Point(20, 55), Size = new Size(200, 25), Font = new Font("Segoe UI", 10), ReadOnly = true, BackColor = Color.FromArgb(240, 240, 240) };

            // Product Name
            Label nameLabel = new Label { Text = "📝 Product Name:", Location = new Point(20, 90), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox nameTextBox = new TextBox { Name = "txtProductName", Location = new Point(20, 115), Size = new Size(850, 25), Font = new Font("Segoe UI", 10) };

            // Description
            Label descLabel = new Label { Text = "📄 Description:", Location = new Point(20, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox descTextBox = new TextBox { Name = "txtDescription", Location = new Point(20, 175), Size = new Size(850, 60), Multiline = true, Font = new Font("Segoe UI", 10) };

            // Row 1: Category, Brand, Warehouse
            Label categoryLabel = new Label { Text = "📂 Category:", Location = new Point(20, 250), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            ComboBox categoryCombo = new ComboBox { Name = "cmbCategory", Location = new Point(20, 275), Size = new Size(200, 25), Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };

            Label brandLabel = new Label { Text = "🏷️ Brand:", Location = new Point(240, 250), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            ComboBox brandCombo = new ComboBox { Name = "cmbBrand", Location = new Point(240, 275), Size = new Size(200, 25), Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };

            Label warehouseLabel = new Label { Text = "🏢 Warehouse:", Location = new Point(460, 250), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            ComboBox warehouseCombo = new ComboBox { Name = "cmbWarehouse", Location = new Point(460, 275), Size = new Size(200, 25), Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };

            // Row 2: Unit, Price, Reorder Level
            Label unitLabel = new Label { Text = "📏 Unit:", Location = new Point(20, 320), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            ComboBox unitCombo = new ComboBox { Name = "cmbUnit", Location = new Point(20, 345), Size = new Size(150, 25), Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };

            Label priceLabel = new Label { Text = "💰 Price:", Location = new Point(190, 320), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox priceTextBox = new TextBox { Name = "txtPrice", Location = new Point(190, 345), Size = new Size(150, 25), Font = new Font("Segoe UI", 10) };

            Label reorderLabel = new Label { Text = "⚠️ Reorder Level:", Location = new Point(360, 320), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox reorderTextBox = new TextBox { Name = "txtReorderLevel", Location = new Point(360, 345), Size = new Size(150, 25), Font = new Font("Segoe UI", 10) };

            // Row 3: Batch Number, Expiry Date, Stock Quantity
            Label batchLabel = new Label { Text = "📦 Batch Number:", Location = new Point(20, 390), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox batchTextBox = new TextBox { Name = "txtBatchNumber", Location = new Point(20, 415), Size = new Size(150, 25), Font = new Font("Segoe UI", 10) };

            Label expiryLabel = new Label { Text = "📅 Expiry Date:", Location = new Point(190, 390), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            DateTimePicker expiryPicker = new DateTimePicker { Name = "dtpExpiryDate", Location = new Point(190, 415), Size = new Size(150, 25), Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };

            Label stockLabel = new Label { Text = "📊 Stock Quantity:", Location = new Point(360, 390), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox stockTextBox = new TextBox { Name = "txtStockQuantity", Location = new Point(360, 415), Size = new Size(150, 25), Font = new Font("Segoe UI", 10) };

            productGroup.Controls.AddRange(new Control[] { codeLabel, codeTextBox, nameLabel, nameTextBox, descLabel, descTextBox, 
                                                          categoryLabel, categoryCombo, brandLabel, brandCombo, unitLabel, unitCombo,
                                                          priceLabel, priceTextBox, reorderLabel, reorderTextBox, warehouseLabel, warehouseCombo,
                                                          batchLabel, batchTextBox, expiryLabel, expiryPicker, stockLabel, stockTextBox });

            // Barcode Group
            GroupBox barcodeGroup = new GroupBox
            {
                Text = "📊 Barcode Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(480, 200),
                Location = new Point(940, 200),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            // Barcode Text
            Label barcodeLabel = new Label { Text = "🔍 Barcode:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox barcodeTextBox = new TextBox { Name = "txtBarcode", Location = new Point(20, 55), Size = new Size(300, 25), Font = new Font("Segoe UI", 10) };

            // Generate Barcode Button
            Button generateBtn = new Button
            {
                Text = "🔄 Generate",
                Location = new Point(340, 55),
                Size = new Size(120, 25),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            generateBtn.Click += GenerateBarcode_Click;

            // Barcode Display Panel
            Panel barcodePanel = new Panel
            {
                Name = "pnlBarcode",
                Location = new Point(20, 90),
                Size = new Size(440, 80),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            barcodeGroup.Controls.AddRange(new Control[] { barcodeLabel, barcodeTextBox, generateBtn, barcodePanel });

            // Action Buttons
            Panel actionPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(236, 240, 241)
            };

            Button saveBtn = new Button
            {
                Text = "💾 Save",
                Size = new Size(120, 40),
                Location = new Point(20, 20),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            saveBtn.Click += SaveBtn_Click;

            Button deleteBtn = new Button
            {
                Text = "❌ Delete",
                Size = new Size(120, 40),
                Location = new Point(160, 20),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            deleteBtn.Click += DeleteBtn_Click;

            actionPanel.Controls.AddRange(new Control[] { saveBtn, deleteBtn });

            // Products Grid
            GroupBox gridGroup = new GroupBox
            {
                Text = "📋 Products List",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1420, 300),
                Location = new Point(20, 580),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            DataGridView productsGrid = new DataGridView
            {
                Name = "dgvProducts",
                Location = new Point(20, 35),
                Size = new Size(1380, 250),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
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
            productsGrid.CellClick += ProductsGrid_CellClick;

            gridGroup.Controls.Add(productsGrid);

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] { productGroup, barcodeGroup, gridGroup });

            // Add panels to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(contentPanel);
            this.Controls.Add(actionPanel);

            this.ResumeLayout(false);
        }

        private void LoadCategories()
        {
            try
            {
                ComboBox cmbCategory = (ComboBox)this.Controls.Find("cmbCategory", true)[0];
                cmbCategory.Items.Clear();
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CategoryName FROM Categories ORDER BY CategoryName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        cmbCategory.Items.Add(reader["CategoryName"].ToString());
                    }
                }
                
                // Add default categories if database is empty
                if (cmbCategory.Items.Count == 0)
                {
                    cmbCategory.Items.AddRange(new string[] { 
                        "Electronics", "Food & Beverages", "Clothing", "Books", 
                        "Home & Garden", "Sports", "Toys", "Health & Beauty", "Automotive" 
                    });
                }
                
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBrands()
        {
            try
            {
                ComboBox cmbBrand = (ComboBox)this.Controls.Find("cmbBrand", true)[0];
                cmbBrand.Items.Clear();
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT BrandName FROM Brands ORDER BY BrandName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        cmbBrand.Items.Add(reader["BrandName"].ToString());
                    }
                }
                
                // Add default brands if database is empty
                if (cmbBrand.Items.Count == 0)
                {
                    cmbBrand.Items.AddRange(new string[] { 
                        "Samsung", "Apple", "Nike", "Adidas", "Sony", 
                        "LG", "Dell", "HP", "Canon", "Generic" 
                    });
                }
                
                cmbBrand.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading brands: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUnits()
        {
            try
            {
                ComboBox cmbUnit = (ComboBox)this.Controls.Find("cmbUnit", true)[0];
                cmbUnit.Items.Clear();
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT UnitName FROM Units WHERE IsActive = 1 ORDER BY UnitName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        cmbUnit.Items.Add(reader["UnitName"].ToString());
                    }
                }
                
                // Add default units if database is empty
                if (cmbUnit.Items.Count == 0)
                {
                    cmbUnit.Items.AddRange(new string[] { "Piece", "Box", "Kg", "Liter", "Meter", "Pack", "Dozen", "Carton" });
                }
                
                cmbUnit.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading units: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWarehouses()
        {
            try
            {
                ComboBox cmbWarehouse = (ComboBox)this.Controls.Find("cmbWarehouse", true)[0];
                cmbWarehouse.Items.Clear();
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT WarehouseName FROM Warehouses WHERE IsActive = 1 ORDER BY WarehouseName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        cmbWarehouse.Items.Add(reader["WarehouseName"].ToString());
                    }
                }
                
                // Add default warehouses if database is empty
                if (cmbWarehouse.Items.Count == 0)
                {
                    cmbWarehouse.Items.AddRange(new string[] { 
                        "Main Warehouse", "Secondary Warehouse", "North Branch", "South Branch", 
                        "Central Storage", "Distribution Center A", "Distribution Center B" 
                    });
                }
                
                cmbWarehouse.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetBrandId(string brandName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT BrandID FROM Brands WHERE BrandName = @BrandName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BrandName", brandName);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error if needed
            }
            
            // Fallback to default mapping for backward compatibility
            switch (brandName?.ToLower())
            {
                case "samsung": return 1;
                case "apple": return 2;
                case "nike": return 3;
                case "adidas": return 4;
                case "sony": return 5;
                case "lg": return 6;
                case "dell": return 7;
                case "hp": return 8;
                case "canon": return 9;
                case "generic": return 10;
                default: return 1; // Default to Samsung
            }
        }

        private int GetCategoryId(string categoryName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CategoryID FROM Categories WHERE CategoryName = @CategoryName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error if needed
            }
            
            // Fallback to default mapping for backward compatibility
            switch (categoryName?.ToLower())
            {
                case "electronics": return 1;
                case "clothing": return 2;
                case "books": return 3;
                case "home & garden": return 4;
                case "sports": return 5;
                case "toys": return 6;
                case "automotive": return 7;
                case "health & beauty": return 8;
                default: return 1; // Default to Electronics
            }
        }

        private int GetUnitId(string unitName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT UnitId FROM Units WHERE UnitName = @UnitName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UnitName", unitName);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error if needed
            }
            
            // Fallback to default mapping for backward compatibility
            switch (unitName?.ToLower())
            {
                case "piece": return 1;
                case "box": return 2;
                case "kg": return 3;
                case "liter": return 4;
                case "meter": return 5;
                case "pack": return 6;
                case "dozen": return 7;
                case "carton": return 8;
                default: return 1; // Default to Piece
            }
        }

        private string GetBrandName(int brandId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT BrandName FROM Brands WHERE BrandId = @BrandId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BrandId", brandId);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error if needed
            }
            
            // Fallback to default mapping for backward compatibility
            switch (brandId)
            {
                case 1: return "Samsung";
                case 2: return "Apple";
                case 3: return "Nike";
                case 4: return "Adidas";
                case 5: return "Sony";
                case 6: return "LG";
                case 7: return "Dell";
                case 8: return "HP";
                case 9: return "Canon";
                case 10: return "Generic";
                default: return "Samsung"; // Default
            }
        }

        private string GetCategoryName(int categoryId)
        {
            // Reverse mapping: Category ID -> Category Name
            switch (categoryId)
            {
                case 1: return "Electronics";
                case 2: return "Clothing";
                case 3: return "Books";
                case 4: return "Home & Garden";
                case 5: return "Sports";
                case 6: return "Toys";
                case 7: return "Automotive";
                case 8: return "Health & Beauty";
                default: return "Electronics"; // Default
            }
        }

        private string GetUnitName(int unitId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT UnitName FROM Units WHERE UnitId = @UnitId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UnitId", unitId);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error if needed
            }
            
            // Fallback to default mapping for backward compatibility
            switch (unitId)
            {
                case 1: return "Piece";
                case 2: return "Box";
                case 3: return "Kg";
                case 4: return "Liter";
                case 5: return "Meter";
                case 6: return "Pack";
                case 7: return "Dozen";
                case 8: return "Carton";
                default: return "Piece"; // Default
            }
        }

        private int GetWarehouseId(string warehouseName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT WarehouseId FROM Warehouses WHERE WarehouseName = @WarehouseName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@WarehouseName", warehouseName);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error if needed
            }
            
            // Fallback to default mapping for backward compatibility
            switch (warehouseName?.ToLower())
            {
                case "main warehouse": return 1;
                case "secondary warehouse": return 2;
                case "north branch": return 3;
                case "south branch": return 4;
                case "central storage": return 5;
                case "distribution center a": return 6;
                case "distribution center b": return 7;
                default: return 1; // Default to Main Warehouse
            }
        }

        private string GetWarehouseName(int warehouseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT WarehouseName FROM Warehouses WHERE WarehouseId = @WarehouseId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@WarehouseId", warehouseId);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error if needed
            }
            
            // Fallback to default mapping for backward compatibility
            switch (warehouseId)
            {
                case 1: return "Main Warehouse";
                case 2: return "Secondary Warehouse";
                case 3: return "North Branch";
                case 4: return "South Branch";
                case 5: return "Central Storage";
                case 6: return "Distribution Center A";
                case 7: return "Distribution Center B";
                default: return "Main Warehouse"; // Default
            }
        }

        private void GenerateBarcode()
        {
            // Auto-generate Product Code if it's empty (for new products)
            TextBox codeTextBox = (TextBox)this.Controls.Find("txtProductCode", true)[0];
            if (string.IsNullOrEmpty(codeTextBox.Text) || currentProductId == 0)
            {
                codeTextBox.Text = "PRD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }

            // Auto-generate Barcode
            TextBox barcodeTextBox = (TextBox)this.Controls.Find("txtBarcode", true)[0];
            if (string.IsNullOrEmpty(barcodeTextBox.Text))
            {
                barcodeTextBox.Text = codeTextBox.Text + "_BC";
            }

            // Generate barcode image
            GenerateBarcodeImage();
        }

        private void GenerateBarcodeImage()
        {
            try
            {
                TextBox barcodeTextBox = (TextBox)this.Controls.Find("txtBarcode", true)[0];
                Panel barcodePanel = (Panel)this.Controls.Find("pnlBarcode", true)[0];
                
                if (!string.IsNullOrEmpty(barcodeTextBox.Text))
                {
                    // Create a simple barcode representation
                    Bitmap barcodeImage = CreateBarcodeImage(barcodeTextBox.Text);
                    barcodePanel.BackgroundImage = barcodeImage;
                    barcodePanel.BackgroundImageLayout = ImageLayout.Center;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Bitmap CreateBarcodeImage(string text)
        {
            Bitmap bmp = new Bitmap(400, 80);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                
                // Draw barcode bars only (no text)
                Random rand = new Random(text.GetHashCode()); // Use text hash for consistent pattern
                int barWidth = 2;
                int x = 10;
                
                for (int i = 0; i < text.Length * 4 && x < 380; i++)
                {
                    int barHeight = rand.Next(40, 70);
                    g.FillRectangle(Brushes.Black, x, 5, barWidth, barHeight);
                    x += barWidth + 1;
                }
            }
            return bmp;
        }

        private void GenerateBarcode_Click(object sender, EventArgs e)
        {
            GenerateBarcode();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Get form controls
                TextBox txtProductCode = (TextBox)this.Controls.Find("txtProductCode", true)[0];
                TextBox txtProductName = (TextBox)this.Controls.Find("txtProductName", true)[0];
                TextBox txtDescription = (TextBox)this.Controls.Find("txtDescription", true)[0];
                ComboBox cmbCategory = (ComboBox)this.Controls.Find("cmbCategory", true)[0];
                ComboBox cmbBrand = (ComboBox)this.Controls.Find("cmbBrand", true)[0];
                ComboBox cmbUnit = (ComboBox)this.Controls.Find("cmbUnit", true)[0];
                TextBox txtPrice = (TextBox)this.Controls.Find("txtPrice", true)[0];
                TextBox txtReorderLevel = (TextBox)this.Controls.Find("txtReorderLevel", true)[0];
                TextBox txtBarcode = (TextBox)this.Controls.Find("txtBarcode", true)[0];
                ComboBox cmbWarehouse = (ComboBox)this.Controls.Find("cmbWarehouse", true)[0];
                TextBox txtBatchNumber = (TextBox)this.Controls.Find("txtBatchNumber", true)[0];
                DateTimePicker dtpExpiryDate = (DateTimePicker)this.Controls.Find("dtpExpiryDate", true)[0];
                TextBox txtStockQuantity = (TextBox)this.Controls.Find("txtStockQuantity", true)[0];

                // Auto-generate Product Code if empty (shouldn't happen since it's auto-generated)
                if (string.IsNullOrWhiteSpace(txtProductCode.Text))
                {
                    txtProductCode.Text = "PRD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                }

                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Product Name is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProductName.Focus();
                    return;
                }

                // Validate and parse numeric values
                if (string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    MessageBox.Show("Price is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrice.Focus();
                    return;
                }

                decimal price = 0;
                if (!decimal.TryParse(txtPrice.Text.Trim(), out price) || price < 0)
                {
                    MessageBox.Show("Please enter a valid price (must be 0 or greater)!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrice.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtReorderLevel.Text))
                {
                    MessageBox.Show("Reorder Level is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtReorderLevel.Focus();
                    return;
                }

                int reorderLevel = 0;
                if (!int.TryParse(txtReorderLevel.Text.Trim(), out reorderLevel) || reorderLevel < 0)
                {
                    MessageBox.Show("Please enter a valid reorder level (must be 0 or greater)!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtReorderLevel.Focus();
                    return;
                }

                // Validate stock quantity
                int stockQuantity = 0;
                if (!string.IsNullOrWhiteSpace(txtStockQuantity.Text))
                {
                    if (!int.TryParse(txtStockQuantity.Text.Trim(), out stockQuantity) || stockQuantity < 0)
                    {
                        MessageBox.Show("Please enter a valid stock quantity (must be 0 or greater)!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtStockQuantity.Focus();
                        return;
                    }
                }

                // Validate dropdown selections
                if (cmbCategory.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a category!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCategory.Focus();
                    return;
                }

                if (cmbUnit.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a unit!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbUnit.Focus();
                    return;
                }

                // Validate barcode
                if (string.IsNullOrWhiteSpace(txtBarcode.Text))
                {
                    MessageBox.Show("Barcode is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBarcode.Focus();
                    return;
                }

                connection.Open();

                if (currentProductId == 0) // New product
                {
                    string insertQuery = @"
                        INSERT INTO Products (ProductCode, ProductName, Description, Category, UnitPrice, ReorderLevel, Barcode, 
                                            StockQuantity, BrandId, CategoryId, UnitId, WarehouseId, BatchNumber, ExpiryDate, CreatedDate, IsActive)
                        VALUES (@ProductCode, @ProductName, @Description, @Category, @Price, @ReorderLevel, @Barcode, 
                                @StockQuantity, @BrandId, @CategoryId, @UnitId, @WarehouseId, @BatchNumber, @ExpiryDate, GETDATE(), 1)";

                    SqlCommand cmd = new SqlCommand(insertQuery, connection);
                    cmd.Parameters.AddWithValue("@ProductCode", txtProductCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@ReorderLevel", reorderLevel);
                    cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text.Trim());
                    cmd.Parameters.AddWithValue("@StockQuantity", stockQuantity); // Use actual stock quantity
                    cmd.Parameters.AddWithValue("@BrandId", GetBrandId(cmbBrand.SelectedItem?.ToString())); // Proper brand ID mapping
                    cmd.Parameters.AddWithValue("@CategoryId", GetCategoryId(cmbCategory.SelectedItem?.ToString())); // Proper category ID mapping
                    cmd.Parameters.AddWithValue("@UnitId", GetUnitId(cmbUnit.SelectedItem?.ToString())); // Proper unit ID mapping
                    cmd.Parameters.AddWithValue("@WarehouseId", cmbWarehouse.SelectedIndex + 1); // Warehouse ID mapping
                    cmd.Parameters.AddWithValue("@BatchNumber", string.IsNullOrWhiteSpace(txtBatchNumber.Text) ? (object)DBNull.Value : txtBatchNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@ExpiryDate", dtpExpiryDate.Value.Date);

                    int result = cmd.ExecuteNonQuery();
                    
                    if (result > 0)
                    {
                        MessageBox.Show("Product saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm(); // Clear form
                        LoadProductsGrid(); // Refresh grid
                    }
                    else
                    {
                        MessageBox.Show("Failed to save product!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else // Update existing product
                {
                    string updateQuery = @"
                        UPDATE Products SET 
                            ProductCode = @ProductCode,
                            ProductName = @ProductName,
                            Description = @Description,
                            Category = @Category,
                            UnitPrice = @Price,
                            ReorderLevel = @ReorderLevel,
                            Barcode = @Barcode,
                            StockQuantity = @StockQuantity,
                            BrandId = @BrandId,
                            CategoryId = @CategoryId,
                            UnitId = @UnitId,
                            WarehouseId = @WarehouseId,
                            BatchNumber = @BatchNumber,
                            ExpiryDate = @ExpiryDate,
                            ModifiedDate = GETDATE()
                        WHERE ProductId = @ProductID";

                    SqlCommand cmd = new SqlCommand(updateQuery, connection);
                    cmd.Parameters.AddWithValue("@ProductID", currentProductId);
                    cmd.Parameters.AddWithValue("@ProductCode", txtProductCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", cmbCategory.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@ReorderLevel", reorderLevel);
                    cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text.Trim());
                    cmd.Parameters.AddWithValue("@StockQuantity", stockQuantity); // Use actual stock quantity
                    cmd.Parameters.AddWithValue("@BrandId", GetBrandId(cmbBrand.SelectedItem?.ToString())); // Proper brand ID mapping
                    cmd.Parameters.AddWithValue("@CategoryId", GetCategoryId(cmbCategory.SelectedItem?.ToString())); // Proper category ID mapping
                    cmd.Parameters.AddWithValue("@UnitId", GetUnitId(cmbUnit.SelectedItem?.ToString())); // Proper unit ID mapping
                    cmd.Parameters.AddWithValue("@WarehouseId", cmbWarehouse.SelectedIndex + 1); // Warehouse ID mapping
                    cmd.Parameters.AddWithValue("@BatchNumber", string.IsNullOrWhiteSpace(txtBatchNumber.Text) ? (object)DBNull.Value : txtBatchNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@ExpiryDate", dtpExpiryDate.Value.Date);

                    int result = cmd.ExecuteNonQuery();
                    
                    if (result > 0)
                    {
                        MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm(); // Clear form after successful update
                        LoadProductsGrid(); // Refresh grid
                    }
                    else
                    {
                        MessageBox.Show("Failed to update product!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving product: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }


        private void ClearForm()
        {
            // Clear all form fields
            TextBox txtProductCode = (TextBox)this.Controls.Find("txtProductCode", true)[0];
            TextBox txtProductName = (TextBox)this.Controls.Find("txtProductName", true)[0];
            TextBox txtDescription = (TextBox)this.Controls.Find("txtDescription", true)[0];
            ComboBox cmbCategory = (ComboBox)this.Controls.Find("cmbCategory", true)[0];
            ComboBox cmbBrand = (ComboBox)this.Controls.Find("cmbBrand", true)[0];
            ComboBox cmbUnit = (ComboBox)this.Controls.Find("cmbUnit", true)[0];
            TextBox txtPrice = (TextBox)this.Controls.Find("txtPrice", true)[0];
            TextBox txtReorderLevel = (TextBox)this.Controls.Find("txtReorderLevel", true)[0];
            TextBox txtBarcode = (TextBox)this.Controls.Find("txtBarcode", true)[0];
            ComboBox cmbWarehouse = (ComboBox)this.Controls.Find("cmbWarehouse", true)[0];
            TextBox txtBatchNumber = (TextBox)this.Controls.Find("txtBatchNumber", true)[0];
            DateTimePicker dtpExpiryDate = (DateTimePicker)this.Controls.Find("dtpExpiryDate", true)[0];
            TextBox txtStockQuantity = (TextBox)this.Controls.Find("txtStockQuantity", true)[0];

            txtProductCode.Clear();
            txtProductName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtReorderLevel.Clear();
            txtBarcode.Clear();
            txtBatchNumber.Clear();
            txtStockQuantity.Clear();

            cmbCategory.SelectedIndex = 0;
            cmbBrand.SelectedIndex = 0;
            cmbUnit.SelectedIndex = 0;
            cmbWarehouse.SelectedIndex = 0;
            dtpExpiryDate.Value = DateTime.Now.AddDays(30); // Default 30 days from now

            currentProductId = 0; // Reset to new product mode
            
            // Auto-generate new Product Code and Barcode
            GenerateBarcode();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (currentProductId > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            
                            // Soft delete - set IsActive = 0 instead of actually deleting
                            string updateQuery = @"
                                UPDATE Products 
                                SET IsActive = 0, ModifiedDate = GETDATE() 
                                WHERE ProductId = @ProductId";
                            
                            SqlCommand cmd = new SqlCommand(updateQuery, conn);
                            cmd.Parameters.AddWithValue("@ProductId", currentProductId);
                            
                            int result = cmd.ExecuteNonQuery();
                            
                            if (result > 0)
                            {
                                MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                // Clear form and refresh grid to hide the deleted product
                                ClearForm();
                                LoadProductsGrid();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete product!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting product: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadProductsGrid()
        {
            try
            {
                DataGridView dgvProducts = (DataGridView)this.Controls.Find("dgvProducts", true)[0];
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT p.ProductId, p.ProductCode, p.ProductName, p.Description, 
                               ISNULL(p.Category, '') as CategoryName, 
                               ISNULL(p.BrandId, 1) as BrandId,
                               ISNULL(p.CategoryId, 1) as CategoryId,
                               ISNULL(p.UnitId, 1) as UnitId,
                               ISNULL(p.UnitPrice, 0) as Price, 
                               p.ReorderLevel, 
                               ISNULL(p.Barcode, '') as Barcode,
                               ISNULL(p.StockQuantity, 0) as StockQuantity,
                               ISNULL(p.BatchNumber, '') as BatchNumber,
                               p.ExpiryDate,
                               ISNULL(p.WarehouseId, 0) as WarehouseId
                        FROM Products p
                        WHERE p.IsActive = 1
                        ORDER BY p.ProductName";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Add display columns for names
                    dataTable.Columns.Add("BrandName", typeof(string));
                    dataTable.Columns.Add("Unit", typeof(string));

                    // Convert IDs to names for display
                    foreach (DataRow row in dataTable.Rows)
                    {
                        int brandId = Convert.ToInt32(row["BrandId"]);
                        int unitId = Convert.ToInt32(row["UnitId"]);
                        
                        row["BrandName"] = GetBrandName(brandId);
                        row["Unit"] = GetUnitName(unitId);
                    }

                    dgvProducts.DataSource = dataTable;

                    // Format columns
                    if (dgvProducts.Columns.Count > 0)
                    {
                        dgvProducts.Columns["ProductId"].Visible = false; // Hide ID column
                        dgvProducts.Columns["BrandId"].Visible = false; // Hide brand ID
                        dgvProducts.Columns["CategoryId"].Visible = false; // Hide category ID
                        dgvProducts.Columns["UnitId"].Visible = false; // Hide unit ID
                        dgvProducts.Columns["WarehouseId"].Visible = false; // Hide warehouse ID
                        
                        dgvProducts.Columns["ProductCode"].HeaderText = "Product Code";
                        dgvProducts.Columns["ProductName"].HeaderText = "Product Name";
                        dgvProducts.Columns["Description"].HeaderText = "Description";
                        dgvProducts.Columns["CategoryName"].HeaderText = "Category";
                        dgvProducts.Columns["BrandName"].HeaderText = "Brand";
                        dgvProducts.Columns["Unit"].HeaderText = "Unit";
                        dgvProducts.Columns["Price"].HeaderText = "Price";
                        dgvProducts.Columns["ReorderLevel"].HeaderText = "Reorder Level";
                        dgvProducts.Columns["Barcode"].HeaderText = "Barcode";
                        dgvProducts.Columns["StockQuantity"].HeaderText = "Stock";
                        dgvProducts.Columns["BatchNumber"].HeaderText = "Batch";
                        dgvProducts.Columns["ExpiryDate"].HeaderText = "Expiry";

                        // Format price column (no currency symbol)
                        dgvProducts.Columns["Price"].DefaultCellStyle.Format = "N2";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridView dgvProducts = (DataGridView)sender;
                    DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

                    // Load selected product data into form
                    currentProductId = Convert.ToInt32(row.Cells["ProductId"].Value);

                    TextBox txtProductCode = (TextBox)this.Controls.Find("txtProductCode", true)[0];
                    TextBox txtProductName = (TextBox)this.Controls.Find("txtProductName", true)[0];
                    TextBox txtDescription = (TextBox)this.Controls.Find("txtDescription", true)[0];
                    ComboBox cmbCategory = (ComboBox)this.Controls.Find("cmbCategory", true)[0];
                    ComboBox cmbBrand = (ComboBox)this.Controls.Find("cmbBrand", true)[0];
                    ComboBox cmbUnit = (ComboBox)this.Controls.Find("cmbUnit", true)[0];
                    TextBox txtPrice = (TextBox)this.Controls.Find("txtPrice", true)[0];
                    TextBox txtReorderLevel = (TextBox)this.Controls.Find("txtReorderLevel", true)[0];
                    TextBox txtBarcode = (TextBox)this.Controls.Find("txtBarcode", true)[0];
                    ComboBox cmbWarehouse = (ComboBox)this.Controls.Find("cmbWarehouse", true)[0];
                    TextBox txtBatchNumber = (TextBox)this.Controls.Find("txtBatchNumber", true)[0];
                    DateTimePicker dtpExpiryDate = (DateTimePicker)this.Controls.Find("dtpExpiryDate", true)[0];
                    TextBox txtStockQuantity = (TextBox)this.Controls.Find("txtStockQuantity", true)[0];

                    txtProductCode.Text = row.Cells["ProductCode"].Value?.ToString() ?? "";
                    txtProductName.Text = row.Cells["ProductName"].Value?.ToString() ?? "";
                    txtDescription.Text = row.Cells["Description"].Value?.ToString() ?? "";
                    txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "0";
                    txtReorderLevel.Text = row.Cells["ReorderLevel"].Value?.ToString() ?? "0";
                    txtBarcode.Text = row.Cells["Barcode"].Value?.ToString() ?? "";
                    txtBatchNumber.Text = row.Cells["BatchNumber"].Value?.ToString() ?? "";
                    txtStockQuantity.Text = row.Cells["StockQuantity"].Value?.ToString() ?? "0";
                    
                    // Set expiry date
                    if (row.Cells["ExpiryDate"].Value != null && row.Cells["ExpiryDate"].Value != DBNull.Value)
                    {
                        dtpExpiryDate.Value = Convert.ToDateTime(row.Cells["ExpiryDate"].Value);
                    }
                    else
                    {
                        dtpExpiryDate.Value = DateTime.Now.AddDays(30);
                    }

                    // Set combo box selections using the display names
                    string categoryName = row.Cells["CategoryName"].Value?.ToString() ?? "";
                    string brandName = row.Cells["BrandName"].Value?.ToString() ?? "";
                    string unit = row.Cells["Unit"].Value?.ToString() ?? "";

                    for (int i = 0; i < cmbCategory.Items.Count; i++)
                    {
                        if (cmbCategory.Items[i].ToString() == categoryName)
                        {
                            cmbCategory.SelectedIndex = i;
                            break;
                        }
                    }

                    for (int i = 0; i < cmbBrand.Items.Count; i++)
                    {
                        if (cmbBrand.Items[i].ToString() == brandName)
                        {
                            cmbBrand.SelectedIndex = i;
                            break;
                        }
                    }

                    for (int i = 0; i < cmbUnit.Items.Count; i++)
                    {
                        if (cmbUnit.Items[i].ToString() == unit)
                        {
                            cmbUnit.SelectedIndex = i;
                            break;
                        }
                    }

                    // Set warehouse selection (default to first warehouse if no specific warehouse ID mapping)
                    int warehouseId = Convert.ToInt32(row.Cells["WarehouseId"].Value ?? 1);
                    if (warehouseId > 0 && warehouseId <= cmbWarehouse.Items.Count)
                    {
                        cmbWarehouse.SelectedIndex = warehouseId - 1;
                    }
                    else
                    {
                        cmbWarehouse.SelectedIndex = 0; // Default to first warehouse
                    }

                    // Generate barcode image for the loaded product
                    if (!string.IsNullOrEmpty(txtBarcode.Text))
                    {
                        GenerateBarcodeImage();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading product data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
