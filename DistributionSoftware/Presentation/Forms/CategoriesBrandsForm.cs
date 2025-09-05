using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class CategoriesBrandsForm : Form
    {
        private string connectionString;

        public CategoriesBrandsForm()
        {
            InitializeComponent();
            InitializeConnection();
            LoadCategories();
            LoadBrands();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Categories & Brands - Distribution Software";
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
                Height = 100,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label headerLabel = new Label
            {
                Text = "📂 Categories & Brands",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true
            };

            Button closeBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Location = new Point(1150, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            closeBtn.Click += (s, e) => this.Close();

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(closeBtn);

            // Main Content Panel
            Panel mainContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 30, 30, 30)
            };

             // Categories Section (Left Side)
             GroupBox categoryInputGroup = new GroupBox
             {
                 Text = "📝 Add New Category",
                 Font = new Font("Segoe UI", 12, FontStyle.Bold),
                 Size = new Size(580, 250),
                 Location = new Point(20, 40)
             };

             Label categoryNameLabel = new Label { Text = "🏷️ Category Name:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
             TextBox categoryNameTextBox = new TextBox { Name = "txtCategoryName", Location = new Point(20, 55), Size = new Size(530, 30), Font = new Font("Segoe UI", 11) };

             Label categoryDescLabel = new Label { Text = "📄 Description:", Location = new Point(20, 100), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
             TextBox categoryDescTextBox = new TextBox { Name = "txtCategoryDesc", Location = new Point(20, 125), Size = new Size(530, 80), Multiline = true, Font = new Font("Segoe UI", 11) };

             Button addCategoryBtn = new Button
             {
                 Text = "➕ Add Category",
                 Size = new Size(150, 40),
                 Location = new Point(20, 220),
                 BackColor = Color.FromArgb(46, 204, 113),
                 ForeColor = Color.White,
                 FlatStyle = FlatStyle.Flat,
                 Font = new Font("Segoe UI", 11, FontStyle.Bold)
             };
             addCategoryBtn.Click += AddCategoryBtn_Click;

             categoryInputGroup.Controls.AddRange(new Control[] { categoryNameLabel, categoryNameTextBox, categoryDescLabel, categoryDescTextBox, addCategoryBtn });

             // Categories Grid
             GroupBox categoryGridGroup = new GroupBox
             {
                 Text = "📋 Categories List",
                 Font = new Font("Segoe UI", 12, FontStyle.Bold),
                 Size = new Size(580, 400),
                 Location = new Point(20, 310)
             };

             DataGridView categoriesGrid = new DataGridView
             {
                 Name = "dgvCategories",
                 Location = new Point(20, 30),
                 Size = new Size(540, 350),
                 AllowUserToAddRows = false,
                 AllowUserToDeleteRows = false,
                 ReadOnly = true,
                 SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                 AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                 BackgroundColor = Color.White,
                 GridColor = Color.FromArgb(189, 195, 199),
                 Font = new Font("Segoe UI", 10),
                 ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(52, 73, 94), ForeColor = Color.White },
                 ColumnHeadersHeight = 35,
                 RowHeadersVisible = false
             };
             categoriesGrid.CellClick += CategoriesGrid_CellClick;

             categoryGridGroup.Controls.Add(categoriesGrid);

             // Brands Section (Right Side)
             GroupBox brandInputGroup = new GroupBox
             {
                 Text = "📝 Add New Brand",
                 Font = new Font("Segoe UI", 12, FontStyle.Bold),
                 Size = new Size(580, 250),
                 Location = new Point(620, 40)
             };

             Label brandNameLabel = new Label { Text = "🏷️ Brand Name:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
             TextBox brandNameTextBox = new TextBox { Name = "txtBrandName", Location = new Point(20, 55), Size = new Size(530, 30), Font = new Font("Segoe UI", 11) };

             Label brandDescLabel = new Label { Text = "📄 Description:", Location = new Point(20, 100), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
             TextBox brandDescTextBox = new TextBox { Name = "txtBrandDesc", Location = new Point(20, 125), Size = new Size(530, 80), Multiline = true, Font = new Font("Segoe UI", 11) };

             Button addBrandBtn = new Button
             {
                 Text = "➕ Add Brand",
                 Size = new Size(150, 40),
                 Location = new Point(20, 220),
                 BackColor = Color.FromArgb(46, 204, 113),
                 ForeColor = Color.White,
                 FlatStyle = FlatStyle.Flat,
                 Font = new Font("Segoe UI", 11, FontStyle.Bold)
             };
             addBrandBtn.Click += AddBrandBtn_Click;

             brandInputGroup.Controls.AddRange(new Control[] { brandNameLabel, brandNameTextBox, brandDescLabel, brandDescTextBox, addBrandBtn });

             // Brands Grid
             GroupBox brandGridGroup = new GroupBox
             {
                 Text = "📋 Brands List",
                 Font = new Font("Segoe UI", 12, FontStyle.Bold),
                 Size = new Size(580, 400),
                 Location = new Point(620, 310)
             };

             DataGridView brandsGrid = new DataGridView
             {
                 Name = "dgvBrands",
                 Location = new Point(20, 30),
                 Size = new Size(540, 350),
                 AllowUserToAddRows = false,
                 AllowUserToDeleteRows = false,
                 ReadOnly = true,
                 SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                 AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                 BackgroundColor = Color.White,
                 GridColor = Color.FromArgb(189, 195, 199),
                 Font = new Font("Segoe UI", 10),
                 ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(52, 73, 94), ForeColor = Color.White },
                 ColumnHeadersHeight = 35,
                 RowHeadersVisible = false
             };
             brandsGrid.CellClick += BrandsGrid_CellClick;

             brandGridGroup.Controls.Add(brandsGrid);

             // Add all controls to main panel
             mainContentPanel.Controls.AddRange(new Control[] { 
                 categoryInputGroup, categoryGridGroup, 
                 brandInputGroup, brandGridGroup 
             });

             // Add controls to form
             this.Controls.Add(headerPanel);
             this.Controls.Add(mainContentPanel);

            this.ResumeLayout(false);
        }

        private void LoadCategories()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CategoryID, CategoryName, Description FROM Categories ORDER BY CategoryName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    DataGridView dgvCategories = (DataGridView)this.Controls.Find("dgvCategories", true)[0];
                    dgvCategories.DataSource = dt;
                }
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT BrandID, BrandName, Description FROM Brands ORDER BY BrandName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    DataGridView dgvBrands = (DataGridView)this.Controls.Find("dgvBrands", true)[0];
                    dgvBrands.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading brands: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCategoryBtn_Click(object sender, EventArgs e)
        {
            TextBox txtCategoryName = (TextBox)this.Controls.Find("txtCategoryName", true)[0];
            TextBox txtCategoryDesc = (TextBox)this.Controls.Find("txtCategoryDesc", true)[0];

            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Please enter a category name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Categories (CategoryName, Description) VALUES (@Name, @Description)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtCategoryName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtCategoryDesc.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCategoryName.Clear();
                    txtCategoryDesc.Clear();
                    LoadCategories();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddBrandBtn_Click(object sender, EventArgs e)
        {
            TextBox txtBrandName = (TextBox)this.Controls.Find("txtBrandName", true)[0];
            TextBox txtBrandDesc = (TextBox)this.Controls.Find("txtBrandDesc", true)[0];

            if (string.IsNullOrWhiteSpace(txtBrandName.Text))
            {
                MessageBox.Show("Please enter a brand name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Brands (BrandName, Description) VALUES (@Name, @Description)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtBrandName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtBrandDesc.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Brand added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBrandName.Clear();
                    txtBrandDesc.Clear();
                    LoadBrands();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding brand: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CategoriesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementation for editing/deleting categories
        }

        private void BrandsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementation for editing/deleting brands
        }
    }
}

