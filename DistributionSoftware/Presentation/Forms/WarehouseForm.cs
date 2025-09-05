using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class WarehouseForm : Form
    {
        private string connectionString;

        public WarehouseForm()
        {
            InitializeComponent();
            InitializeConnection();
            LoadWarehouses();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Warehouse Management - Distribution Software";
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
                Text = "🏢 Warehouse Management",
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

            // Warehouse Input Group
            GroupBox warehouseInputGroup = new GroupBox
            {
                Text = "📝 Add New Warehouse",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1200, 350),
                Location = new Point(20, 20)
            };

            Label warehouseNameLabel = new Label { Text = "🏢 Warehouse Name:", Location = new Point(20, 60), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox warehouseNameTextBox = new TextBox { Name = "txtWarehouseName", Location = new Point(20, 85), Size = new Size(550, 30), Font = new Font("Segoe UI", 11) };

            Label locationLabel = new Label { Text = "📍 Location:", Location = new Point(590, 60), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox locationTextBox = new TextBox { Name = "txtLocation", Location = new Point(590, 85), Size = new Size(550, 30), Font = new Font("Segoe UI", 11) };

            Label contactPersonLabel = new Label { Text = "👤 Contact Person:", Location = new Point(20, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox contactPersonTextBox = new TextBox { Name = "txtContactPerson", Location = new Point(20, 175), Size = new Size(250, 30), Font = new Font("Segoe UI", 11) };

            Label contactPhoneLabel = new Label { Text = "📞 Contact Phone:", Location = new Point(290, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox contactPhoneTextBox = new TextBox { Name = "txtContactPhone", Location = new Point(290, 175), Size = new Size(250, 30), Font = new Font("Segoe UI", 11) };

            Button addWarehouseBtn = new Button
            {
                Text = "➕ Add Warehouse",
                Size = new Size(150, 40),
                Location = new Point(20, 320),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            addWarehouseBtn.Click += AddWarehouseBtn_Click;

            warehouseInputGroup.Controls.AddRange(new Control[] { 
                warehouseNameLabel, warehouseNameTextBox,
                locationLabel, locationTextBox,
                contactPersonLabel, contactPersonTextBox,
                contactPhoneLabel, contactPhoneTextBox,
                addWarehouseBtn 
            });

            // Warehouse Grid
            GroupBox warehouseGridGroup = new GroupBox
            {
                Text = "📋 Warehouses List",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1420, 400),
                Location = new Point(20, 390)
            };

            DataGridView warehousesGrid = new DataGridView
            {
                Name = "dgvWarehouses",
                Location = new Point(20, 35),
                Size = new Size(1380, 350),
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
            warehousesGrid.CellClick += WarehousesGrid_CellClick;

            warehouseGridGroup.Controls.Add(warehousesGrid);

            // Add all controls to main panel
            mainPanel.Controls.AddRange(new Control[] { 
                warehouseInputGroup, warehouseGridGroup 
            });

            // Add controls to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(mainPanel);

            this.ResumeLayout(false);
        }

        private void LoadWarehouses()
        {
            try
            {
                DataGridView dgvWarehouses = (DataGridView)this.Controls.Find("dgvWarehouses", true)[0];

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            WarehouseId,
                            WarehouseName,
                            Location,
                            ContactPerson,
                            ContactPhone,
                            IsActive,
                            CreatedDate
                        FROM Warehouses 
                        ORDER BY WarehouseName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvWarehouses.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddWarehouseBtn_Click(object sender, EventArgs e)
        {
            TextBox txtWarehouseName = (TextBox)this.Controls.Find("txtWarehouseName", true)[0];
            TextBox txtLocation = (TextBox)this.Controls.Find("txtLocation", true)[0];
            TextBox txtContactPerson = (TextBox)this.Controls.Find("txtContactPerson", true)[0];
            TextBox txtContactPhone = (TextBox)this.Controls.Find("txtContactPhone", true)[0];

            if (string.IsNullOrWhiteSpace(txtWarehouseName.Text))
            {
                MessageBox.Show("Please enter a warehouse name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Warehouses 
                        (WarehouseName, Location, ContactPerson, ContactPhone, IsActive, CreatedDate)
                        VALUES (@Name, @Location, @ContactPerson, @ContactPhone, 1, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtWarehouseName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                    cmd.Parameters.AddWithValue("@ContactPhone", txtContactPhone.Text.Trim());

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Warehouse added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadWarehouses();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding warehouse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WarehousesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementation for editing/deleting warehouses
        }

        private void ClearForm()
        {
            TextBox txtWarehouseName = (TextBox)this.Controls.Find("txtWarehouseName", true)[0];
            TextBox txtLocation = (TextBox)this.Controls.Find("txtLocation", true)[0];
            TextBox txtContactPerson = (TextBox)this.Controls.Find("txtContactPerson", true)[0];
            TextBox txtContactPhone = (TextBox)this.Controls.Find("txtContactPhone", true)[0];

            txtWarehouseName.Clear();
            txtLocation.Clear();
            txtContactPerson.Clear();
            txtContactPhone.Clear();
        }
    }
}
