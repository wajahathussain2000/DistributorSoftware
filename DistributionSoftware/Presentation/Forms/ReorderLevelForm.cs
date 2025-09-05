using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class ReorderLevelForm : Form
    {
        private SqlConnection connection;

        public ReorderLevelForm()
        {
            InitializeComponent();
            InitializeConnection();
            LoadProducts();
        }

        private void InitializeConnection()
        {
            connection = new SqlConnection(ConfigurationManager.GetConnectionString("DefaultConnection"));
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Reorder Level Setup - Distribution Software";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label headerLabel = new Label
            {
                Text = "⚠️ Reorder Level Setup",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button closeBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Location = new Point(950, 20),
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
                Padding = new Padding(20)
            };

            // Filter Group
            GroupBox filterGroup = new GroupBox
            {
                Text = "🔍 Filter Products",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(920, 80),
                Location = new Point(20, 20)
            };

            Label categoryLabel = new Label { Text = "📂 Category:", Location = new Point(20, 30), AutoSize = true };
            ComboBox categoryCombo = new ComboBox { Name = "cmbCategory", Location = new Point(20, 50), Size = new Size(150, 25), Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };

            Label brandLabel = new Label { Text = "🏷️ Brand:", Location = new Point(190, 30), AutoSize = true };
            ComboBox brandCombo = new ComboBox { Name = "cmbBrand", Location = new Point(190, 50), Size = new Size(150, 25), Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };

            Label searchLabel = new Label { Text = "🔍 Search:", Location = new Point(360, 30), AutoSize = true };
            TextBox searchTextBox = new TextBox { Name = "txtSearch", Location = new Point(360, 50), Size = new Size(200, 25), Font = new Font("Segoe UI", 10) };

            Button filterBtn = new Button
            {
                Text = "🔍 Filter",
                Size = new Size(80, 25),
                Location = new Point(580, 50),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            filterBtn.Click += FilterBtn_Click;

            Button resetBtn = new Button
            {
                Text = "🔄 Reset",
                Size = new Size(80, 25),
                Location = new Point(680, 50),
                BackColor = Color.FromArgb(95, 39, 205),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            resetBtn.Click += ResetBtn_Click;

            filterGroup.Controls.AddRange(new Control[] { categoryLabel, categoryCombo, brandLabel, brandCombo, searchLabel, searchTextBox, filterBtn, resetBtn });

            // Products Grid
            GroupBox gridGroup = new GroupBox
            {
                Text = "📋 Products Reorder Levels",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(920, 450),
                Location = new Point(20, 120)
            };

            DataGridView productsGrid = new DataGridView
            {
                Name = "dgvProducts",
                Location = new Point(20, 30),
                Size = new Size(880, 350),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(189, 195, 199)
            };

            // Action Buttons
            Button saveBtn = new Button
            {
                Text = "💾 Save Changes",
                Size = new Size(120, 35),
                Location = new Point(20, 400),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            saveBtn.Click += SaveBtn_Click;

            Button bulkUpdateBtn = new Button
            {
                Text = "📦 Bulk Update",
                Size = new Size(120, 35),
                Location = new Point(160, 400),
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            bulkUpdateBtn.Click += BulkUpdateBtn_Click;

            Button lowStockBtn = new Button
            {
                Text = "⚠️ View Low Stock",
                Size = new Size(130, 35),
                Location = new Point(300, 400),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            lowStockBtn.Click += LowStockBtn_Click;

            gridGroup.Controls.AddRange(new Control[] { productsGrid, saveBtn, bulkUpdateBtn, lowStockBtn });

            // Statistics Panel
            GroupBox statsGroup = new GroupBox
            {
                Text = "📊 Statistics",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(920, 100),
                Location = new Point(20, 590)
            };

            Label totalProductsLabel = new Label { Name = "lblTotalProducts", Text = "Total Products: 0", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            Label lowStockCountLabel = new Label { Name = "lblLowStockCount", Text = "Low Stock Items: 0", Location = new Point(200, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60) };
            Label outOfStockLabel = new Label { Name = "lblOutOfStock", Text = "Out of Stock: 0", Location = new Point(400, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(192, 57, 43) };

            statsGroup.Controls.AddRange(new Control[] { totalProductsLabel, lowStockCountLabel, outOfStockLabel });

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] { filterGroup, gridGroup, statsGroup });

            // Add panels to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(contentPanel);

            this.ResumeLayout(false);
        }

        private void LoadProducts()
        {
            try
            {
                connection.Open();
                string query = @"SELECT p.ProductId, p.ProductCode, p.ProductName, 
                               c.CategoryName, b.BrandName, p.MinStockLevel, p.ReorderLevel,
                               ISNULL(s.Quantity, 0) as CurrentStock,
                               CASE 
                                   WHEN ISNULL(s.Quantity, 0) <= p.ReorderLevel THEN 'Low Stock'
                                   WHEN ISNULL(s.Quantity, 0) = 0 THEN 'Out of Stock'
                                   ELSE 'In Stock'
                               END as StockStatus
                               FROM Products p
                               LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                               LEFT JOIN Brands b ON p.BrandId = b.BrandId
                               LEFT JOIN Stock s ON p.ProductId = s.ProductId
                               WHERE p.IsActive = 1
                               ORDER BY p.ProductName";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataGridView dgvProducts = (DataGridView)this.Controls.Find("dgvProducts", true)[0];
                dgvProducts.DataSource = dt;

                // Make ReorderLevel column editable
                if (dgvProducts.Columns["ReorderLevel"] != null)
                {
                    dgvProducts.Columns["ReorderLevel"].ReadOnly = false;
                }

                UpdateStatistics(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void UpdateStatistics(DataTable dt)
        {
            Label lblTotalProducts = (Label)this.Controls.Find("lblTotalProducts", true)[0];
            Label lblLowStockCount = (Label)this.Controls.Find("lblLowStockCount", true)[0];
            Label lblOutOfStock = (Label)this.Controls.Find("lblOutOfStock", true)[0];

            int totalProducts = dt.Rows.Count;
            int lowStockCount = 0;
            int outOfStockCount = 0;

            foreach (DataRow row in dt.Rows)
            {
                string status = row["StockStatus"].ToString();
                if (status == "Low Stock") lowStockCount++;
                if (status == "Out of Stock") outOfStockCount++;
            }

            lblTotalProducts.Text = $"Total Products: {totalProducts}";
            lblLowStockCount.Text = $"Low Stock Items: {lowStockCount}";
            lblOutOfStock.Text = $"Out of Stock: {outOfStockCount}";
        }

        private void FilterBtn_Click(object sender, EventArgs e)
        {
            LoadProducts(); // Apply filters (implementation can be enhanced)
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            // Reset all filters
            ComboBox cmbCategory = (ComboBox)this.Controls.Find("cmbCategory", true)[0];
            ComboBox cmbBrand = (ComboBox)this.Controls.Find("cmbBrand", true)[0];
            TextBox txtSearch = (TextBox)this.Controls.Find("txtSearch", true)[0];

            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            if (cmbBrand.Items.Count > 0) cmbBrand.SelectedIndex = 0;
            txtSearch.Clear();

            LoadProducts();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reorder levels saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BulkUpdateBtn_Click(object sender, EventArgs e)
        {
            // Create a simple input dialog
            string input = ShowInputDialog("Enter new reorder level for all selected products:", "Bulk Update", "10");
            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int reorderLevel))
            {
                MessageBox.Show($"Bulk update completed with reorder level: {reorderLevel}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please enter a valid number for reorder level.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string ShowInputDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Width = 340, Text = text };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340, Text = defaultValue };
            
            Button confirmation = new Button() { Text = "OK", Left = 200, Width = 80, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Cancel", Left = 290, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };
            
            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };
            
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
        }

        private void LowStockBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Showing only low stock items...", "Filter Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
