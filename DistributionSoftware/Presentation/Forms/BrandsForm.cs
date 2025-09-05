using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class BrandsForm : Form
    {
        private string connectionString;

        public BrandsForm()
        {
            InitializeComponent();
            InitializeConnection();
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
            this.Text = "Brands Management - Distribution Software";
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
                Text = "🏷️ Brands Management",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button closeBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Location = new Point(1150, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            closeBtn.Click += (s, e) => this.Close();

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(closeBtn);

            // Main Content Panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Brands Input Group
            GroupBox brandsInputGroup = new GroupBox
            {
                Text = "📝 Add New Brand",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1200, 350),
                Location = new Point(20, 20)
            };

            Label brandNameLabel = new Label { Text = "🏷️ Brand Name:", Location = new Point(20, 60), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox brandNameTextBox = new TextBox { Name = "txtBrandName", Location = new Point(20, 85), Size = new Size(550, 30), Font = new Font("Segoe UI", 11) };

            Label descriptionLabel = new Label { Text = "📄 Description:", Location = new Point(20, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox descriptionTextBox = new TextBox { Name = "txtDescription", Location = new Point(20, 175), Size = new Size(1150, 100), Font = new Font("Segoe UI", 11), Multiline = true };

            Button addBrandBtn = new Button
            {
                Text = "➕ Add Brand",
                Size = new Size(150, 40),
                Location = new Point(20, 320),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            addBrandBtn.Click += AddBrandBtn_Click;

            brandsInputGroup.Controls.AddRange(new Control[] { 
                brandNameLabel, brandNameTextBox,
                descriptionLabel, descriptionTextBox,
                addBrandBtn 
            });

            // Brands Grid
            GroupBox brandsGridGroup = new GroupBox
            {
                Text = "📋 Brands List",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1420, 450),
                Location = new Point(20, 390)
            };

            DataGridView brandsGrid = new DataGridView
            {
                Name = "dgvBrands",
                Location = new Point(20, 35),
                Size = new Size(1380, 400),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(189, 195, 199),
                Font = new Font("Segoe UI", 9),
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 8, FontStyle.Bold), BackColor = Color.FromArgb(52, 73, 94), ForeColor = Color.White },
                ColumnHeadersHeight = 35,
                RowHeadersVisible = false
            };
            brandsGrid.CellClick += BrandsGrid_CellClick;

            brandsGridGroup.Controls.Add(brandsGrid);

            // Add all controls to main panel
            mainPanel.Controls.AddRange(new Control[] { 
                brandsInputGroup, brandsGridGroup 
            });

            // Add controls to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(mainPanel);

            this.ResumeLayout(false);
        }

        private void LoadBrands()
        {
            try
            {
                DataGridView dgvBrands = (DataGridView)this.Controls.Find("dgvBrands", true)[0];

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            BrandId,
                            BrandName,
                            Description,
                            IsActive,
                            CreatedDate
                        FROM Brands 
                        ORDER BY BrandName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvBrands.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading brands: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddBrandBtn_Click(object sender, EventArgs e)
        {
            TextBox txtBrandName = (TextBox)this.Controls.Find("txtBrandName", true)[0];
            TextBox txtDescription = (TextBox)this.Controls.Find("txtDescription", true)[0];

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
                    string query = @"
                        INSERT INTO Brands 
                        (BrandName, Description, IsActive, CreatedDate)
                        VALUES (@Name, @Description, 1, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtBrandName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Brand added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadBrands();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding brand: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BrandsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementation for editing/deleting brands
        }

        private void ClearForm()
        {
            TextBox txtBrandName = (TextBox)this.Controls.Find("txtBrandName", true)[0];
            TextBox txtDescription = (TextBox)this.Controls.Find("txtDescription", true)[0];

            txtBrandName.Clear();
            txtDescription.Clear();
        }
    }
}
