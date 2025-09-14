using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class ReorderLevelSetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Reorder Level Setup - Distribution Software";
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
                Text = "Reorder Level Setup",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            headerPanel.Controls.Add(headerLabel);

            // Main Panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            // Left Panel - Form
            Panel leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 400,
                BackColor = Color.White,
                Padding = new Padding(20),
                Margin = new Padding(0, 0, 10, 0)
            };

            // Form Group Box
            GroupBox formGroupBox = new GroupBox
            {
                Text = "Reorder Level Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // Product Selection
            Label lblProduct = new Label
            {
                Text = "Product:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 40),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.cmbProduct = new ComboBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 37),
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Current Stock Label
            this.lblCurrentStock = new Label
            {
                Text = "Current Stock: 0",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(20, 70),
                Size = new Size(200, 20),
                ForeColor = Color.FromArgb(46, 204, 113),
                Visible = false
            };

            // Minimum Level
            Label lblMinimumLevel = new Label
            {
                Text = "Minimum Level:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 100),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtMinimumLevel = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 97),
                Size = new Size(100, 28),
                Text = "0"
            };

            // Maximum Level
            Label lblMaximumLevel = new Label
            {
                Text = "Maximum Level:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 140),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtMaximumLevel = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 137),
                Size = new Size(100, 28),
                Text = "0"
            };

            // Reorder Quantity
            Label lblReorderQuantity = new Label
            {
                Text = "Reorder Quantity:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 180),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtReorderQuantity = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 177),
                Size = new Size(100, 28),
                Text = "0"
            };

            // Checkboxes
            this.chkIsActive = new CheckBox
            {
                Text = "Active",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 220),
                Size = new Size(100, 25),
                Checked = true,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.chkAlertEnabled = new CheckBox
            {
                Text = "Enable Alerts",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 220),
                Size = new Size(120, 25),
                Checked = true,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // Add controls to form group box
            formGroupBox.Controls.AddRange(new Control[] {
                lblProduct, this.cmbProduct, this.lblCurrentStock,
                lblMinimumLevel, this.txtMinimumLevel,
                lblMaximumLevel, this.txtMaximumLevel,
                lblReorderQuantity, this.txtReorderQuantity,
                this.chkIsActive, this.chkAlertEnabled
            });

            leftPanel.Controls.Add(formGroupBox);

            // Right Panel - Data Grid
            Panel rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            // Data Grid Group Box
            GroupBox gridGroupBox = new GroupBox
            {
                Text = "Reorder Levels",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.dgvReorderLevels = new DataGridView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                GridColor = Color.FromArgb(230, 230, 230),
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    SelectionBackColor = Color.FromArgb(52, 152, 219),
                    SelectionForeColor = Color.White,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(52, 73, 94)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(52, 73, 94),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }
            };

            gridGroupBox.Controls.Add(this.dgvReorderLevels);
            rightPanel.Controls.Add(gridGroupBox);

            // Button Panel
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20, 10, 20, 10)
            };

            this.btnNew = new Button
            {
                Text = "New",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(80, 35),
                Location = new Point(0, 10),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };

            this.btnSave = new Button
            {
                Text = "Save",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(80, 35),
                Location = new Point(90, 10),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };

            this.btnCancel = new Button
            {
                Text = "Cancel",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(80, 35),
                Location = new Point(180, 10),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };

            this.btnEdit = new Button
            {
                Text = "Edit",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(80, 35),
                Location = new Point(270, 10),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };

            this.btnDelete = new Button
            {
                Text = "Delete",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(80, 35),
                Location = new Point(360, 10),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };

            this.btnClose = new Button
            {
                Text = "Close",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(80, 35),
                Location = new Point(450, 10),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };

            buttonPanel.Controls.AddRange(new Control[] {
                this.btnNew, this.btnSave, this.btnCancel, 
                this.btnEdit, this.btnDelete, this.btnClose
            });

            // Add panels to main panel
            mainPanel.Controls.Add(leftPanel);
            mainPanel.Controls.Add(rightPanel);
            mainPanel.Controls.Add(buttonPanel);

            // Add panels to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(mainPanel);

            // Event handlers
            this.btnNew.Click += BtnNew_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnCancel.Click += BtnCancel_Click;
            this.btnEdit.Click += BtnEdit_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.btnClose.Click += BtnClose_Click;
            this.dgvReorderLevels.SelectionChanged += DgvReorderLevels_SelectionChanged;
            this.dgvReorderLevels.DataBindingComplete += DgvReorderLevels_DataBindingComplete;
            this.cmbProduct.SelectedIndexChanged += CmbProduct_SelectedIndexChanged;

            this.ResumeLayout(false);
        }

        #endregion

        #region Controls

        private ComboBox cmbProduct;
        private Label lblCurrentStock;
        private TextBox txtMinimumLevel;
        private TextBox txtMaximumLevel;
        private TextBox txtReorderQuantity;
        private CheckBox chkIsActive;
        private CheckBox chkAlertEnabled;
        private DataGridView dgvReorderLevels;
        private Button btnNew;
        private Button btnSave;
        private Button btnCancel;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClose;

        #endregion
    }
}
