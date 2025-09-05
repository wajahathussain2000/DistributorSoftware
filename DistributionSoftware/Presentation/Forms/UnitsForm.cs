using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class UnitsForm : Form
    {
        private string connectionString;

        public UnitsForm()
        {
            InitializeComponent();
            InitializeConnection();
            LoadUnits();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Units Management - Distribution Software";
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
                Text = "📏 Units Management",
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

            // Units Input Group
            GroupBox unitsInputGroup = new GroupBox
            {
                Text = "📝 Add New Unit",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1200, 350),
                Location = new Point(20, 20)
            };

            Label unitNameLabel = new Label { Text = "📏 Unit Name:", Location = new Point(20, 60), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox unitNameTextBox = new TextBox { Name = "txtUnitName", Location = new Point(20, 85), Size = new Size(350, 30), Font = new Font("Segoe UI", 11) };

            Label unitCodeLabel = new Label { Text = "🔤 Unit Code:", Location = new Point(390, 60), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox unitCodeTextBox = new TextBox { Name = "txtUnitCode", Location = new Point(390, 85), Size = new Size(250, 30), Font = new Font("Segoe UI", 11) };

            Label descriptionLabel = new Label { Text = "📄 Description:", Location = new Point(20, 150), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox descriptionTextBox = new TextBox { Name = "txtDescription", Location = new Point(20, 175), Size = new Size(1150, 100), Font = new Font("Segoe UI", 11), Multiline = true };

            Button addUnitBtn = new Button
            {
                Text = "➕ Add Unit",
                Size = new Size(150, 40),
                Location = new Point(20, 320),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            addUnitBtn.Click += AddUnitBtn_Click;

            unitsInputGroup.Controls.AddRange(new Control[] { 
                unitNameLabel, unitNameTextBox,
                unitCodeLabel, unitCodeTextBox,
                descriptionLabel, descriptionTextBox,
                addUnitBtn 
            });

            // Units Grid
            GroupBox unitsGridGroup = new GroupBox
            {
                Text = "📋 Units List",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1420, 450),
                Location = new Point(20, 390)
            };

            DataGridView unitsGrid = new DataGridView
            {
                Name = "dgvUnits",
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
            unitsGrid.CellClick += UnitsGrid_CellClick;

            unitsGridGroup.Controls.Add(unitsGrid);

            // Add all controls to main panel
            mainPanel.Controls.AddRange(new Control[] { 
                unitsInputGroup, unitsGridGroup 
            });

            // Add controls to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(mainPanel);

            this.ResumeLayout(false);
        }

        private void LoadUnits()
        {
            try
            {
                DataGridView dgvUnits = (DataGridView)this.Controls.Find("dgvUnits", true)[0];

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            UnitId,
                            UnitName,
                            UnitCode,
                            Description,
                            IsActive,
                            CreatedDate
                        FROM Units 
                        ORDER BY UnitName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvUnits.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading units: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddUnitBtn_Click(object sender, EventArgs e)
        {
            TextBox txtUnitName = (TextBox)this.Controls.Find("txtUnitName", true)[0];
            TextBox txtUnitCode = (TextBox)this.Controls.Find("txtUnitCode", true)[0];
            TextBox txtDescription = (TextBox)this.Controls.Find("txtDescription", true)[0];

            if (string.IsNullOrWhiteSpace(txtUnitName.Text))
            {
                MessageBox.Show("Please enter a unit name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUnitCode.Text))
            {
                MessageBox.Show("Please enter a unit code.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Units 
                        (UnitName, UnitCode, Description, IsActive, CreatedDate)
                        VALUES (@Name, @Code, @Description, 1, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtUnitName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Code", txtUnitCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Unit added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadUnits();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding unit: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UnitsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementation for editing/deleting units
        }

        private void ClearForm()
        {
            TextBox txtUnitName = (TextBox)this.Controls.Find("txtUnitName", true)[0];
            TextBox txtUnitCode = (TextBox)this.Controls.Find("txtUnitCode", true)[0];
            TextBox txtDescription = (TextBox)this.Controls.Find("txtDescription", true)[0];

            txtUnitName.Clear();
            txtUnitCode.Clear();
            txtDescription.Clear();
        }
    }
}

