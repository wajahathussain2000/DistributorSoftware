using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class PricingDiscountSetupForm
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
            this.Text = "Pricing & Discount Setup - Distribution Software";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new Size(1400, 900);

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            this.lblFormTitle = new Label
            {
                Text = "Pricing & Discount Setup",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Mode Selection Buttons
            this.btnPricingMode = new Button
            {
                Text = "Pricing Rules",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 35),
                Location = new Point(400, 20),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };

            this.btnDiscountMode = new Button
            {
                Text = "Discount Rules",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(120, 35),
                Location = new Point(530, 20),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand
            };

            headerPanel.Controls.AddRange(new Control[] { this.lblFormTitle, this.btnPricingMode, this.btnDiscountMode });

            // Main Panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            // Tab Control
            this.tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ItemSize = new Size(150, 30),
                SizeMode = TabSizeMode.Fixed
            };

            // Pricing Rules Tab
            this.tabPricingRules = new TabPage("Pricing Rules");
            this.tabPricingRules.BackColor = Color.White;

            // Left Panel - Form
            Panel leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 450,
                BackColor = Color.White,
                Padding = new Padding(20),
                Margin = new Padding(0, 0, 10, 0)
            };

            // Form Group Box
            GroupBox formGroupBox = new GroupBox
            {
                Text = "Rule Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // Rule Name
            Label lblRuleName = new Label
            {
                Text = "Rule Name:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 40),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtRuleName = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 37),
                Size = new Size(250, 28)
            };

            // Description
            Label lblDescription = new Label
            {
                Text = "Description:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 80),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtDescription = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 77),
                Size = new Size(250, 28)
            };

            // Product Selection
            Label lblProduct = new Label
            {
                Text = "Product:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 120),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.cmbProduct = new ComboBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 117),
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Customer Selection
            Label lblCustomer = new Label
            {
                Text = "Customer:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 160),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.cmbCustomer = new ComboBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 157),
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Pricing Type
            Label lblPricingType = new Label
            {
                Text = "Pricing Type:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 200),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.cmbPricingType = new ComboBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 197),
                Size = new Size(150, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.cmbPricingType.Items.AddRange(new[] { "FIXED_PRICE", "PERCENTAGE_MARKUP", "PERCENTAGE_MARGIN", "QUANTITY_BREAK" });

            // Discount Type
            Label lblDiscountType = new Label
            {
                Text = "Discount Type:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 200),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.cmbDiscountType = new ComboBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 197),
                Size = new Size(150, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.cmbDiscountType.Items.AddRange(new[] { "PERCENTAGE", "FIXED_AMOUNT", "QUANTITY_BREAK" });

            // Base Value
            this.lblBaseValue = new Label
            {
                Text = "Base Value:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 240),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtBaseValue = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 237),
                Size = new Size(100, 28),
                Text = "0"
            };

            // Min Quantity
            this.lblMinQuantity = new Label
            {
                Text = "Min Quantity:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 280),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtMinQuantity = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 277),
                Size = new Size(100, 28)
            };

            // Max Quantity
            this.lblMaxQuantity = new Label
            {
                Text = "Max Quantity:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 320),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtMaxQuantity = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 317),
                Size = new Size(100, 28)
            };

            // Min Order Amount
            this.lblMinOrderAmount = new Label
            {
                Text = "Min Order Amount:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 360),
                Size = new Size(120, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtMinOrderAmount = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(150, 357),
                Size = new Size(100, 28)
            };

            // Max Discount Amount
            this.lblMaxDiscountAmount = new Label
            {
                Text = "Max Discount Amount:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 400),
                Size = new Size(140, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtMaxDiscountAmount = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(170, 397),
                Size = new Size(100, 28)
            };

            // Priority
            Label lblPriority = new Label
            {
                Text = "Priority:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 440),
                Size = new Size(100, 25),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.txtPriority = new TextBox
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 437),
                Size = new Size(100, 28),
                Text = "100"
            };

            // Checkboxes
            this.chkIsActive = new CheckBox
            {
                Text = "Active",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 480),
                Size = new Size(100, 25),
                Checked = true,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.chkIsPromotional = new CheckBox
            {
                Text = "Promotional",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(130, 480),
                Size = new Size(120, 25),
                Checked = false,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // Effective Dates
            this.chkEffectiveFrom = new CheckBox
            {
                Text = "Effective From:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 520),
                Size = new Size(120, 25),
                Checked = false,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.dtpEffectiveFrom = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(150, 517),
                Size = new Size(150, 28),
                Format = DateTimePickerFormat.Short
            };

            this.chkEffectiveTo = new CheckBox
            {
                Text = "Effective To:",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(20, 560),
                Size = new Size(120, 25),
                Checked = false,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.dtpEffectiveTo = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(150, 557),
                Size = new Size(150, 28),
                Format = DateTimePickerFormat.Short
            };

            // Add controls to form group box
            formGroupBox.Controls.AddRange(new Control[] {
                lblRuleName, this.txtRuleName, lblDescription, this.txtDescription,
                lblProduct, this.cmbProduct, lblCustomer, this.cmbCustomer,
                lblPricingType, this.cmbPricingType, lblDiscountType, this.cmbDiscountType,
                this.lblBaseValue, this.txtBaseValue,
                this.lblMinQuantity, this.txtMinQuantity, this.lblMaxQuantity, this.txtMaxQuantity,
                this.lblMinOrderAmount, this.txtMinOrderAmount, this.lblMaxDiscountAmount, this.txtMaxDiscountAmount,
                lblPriority, this.txtPriority, this.chkIsActive, this.chkIsPromotional,
                this.chkEffectiveFrom, this.dtpEffectiveFrom, this.chkEffectiveTo, this.dtpEffectiveTo
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
                Text = "Rules",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            this.dgvPricingRules = new DataGridView
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

            this.dgvDiscountRules = new DataGridView
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

            gridGroupBox.Controls.Add(this.dgvPricingRules);
            rightPanel.Controls.Add(gridGroupBox);

            // Add panels to tab page
            this.tabPricingRules.Controls.Add(leftPanel);
            this.tabPricingRules.Controls.Add(rightPanel);

            // Discount Rules Tab
            this.tabDiscountRules = new TabPage("Discount Rules");
            this.tabDiscountRules.BackColor = Color.White;

            // Copy the same layout for discount rules tab
            Panel leftPanelDiscount = new Panel
            {
                Dock = DockStyle.Left,
                Width = 450,
                BackColor = Color.White,
                Padding = new Padding(20),
                Margin = new Padding(0, 0, 10, 0)
            };

            Panel rightPanelDiscount = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            GroupBox gridGroupBoxDiscount = new GroupBox
            {
                Text = "Discount Rules",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            gridGroupBoxDiscount.Controls.Add(this.dgvDiscountRules);
            rightPanelDiscount.Controls.Add(gridGroupBoxDiscount);

            this.tabDiscountRules.Controls.Add(leftPanelDiscount);
            this.tabDiscountRules.Controls.Add(rightPanelDiscount);

            // Add tabs to tab control
            this.tabControl.TabPages.Add(this.tabPricingRules);
            this.tabControl.TabPages.Add(this.tabDiscountRules);

            mainPanel.Controls.Add(this.tabControl);

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
            this.btnPricingMode.Click += BtnPricingMode_Click;
            this.btnDiscountMode.Click += BtnDiscountMode_Click;
            this.dgvPricingRules.SelectionChanged += DgvPricingRules_SelectionChanged;
            this.dgvDiscountRules.SelectionChanged += DgvDiscountRules_SelectionChanged;
            this.dgvPricingRules.DataBindingComplete += DgvPricingRules_DataBindingComplete;
            this.dgvDiscountRules.DataBindingComplete += DgvDiscountRules_DataBindingComplete;

            this.ResumeLayout(false);
        }

        #endregion

        #region Controls

        private Label lblFormTitle;
        private Button btnPricingMode;
        private Button btnDiscountMode;
        private TabControl tabControl;
        private TabPage tabPricingRules;
        private TabPage tabDiscountRules;
        private TextBox txtRuleName;
        private TextBox txtDescription;
        private ComboBox cmbProduct;
        private ComboBox cmbCustomer;
        private ComboBox cmbPricingType;
        private ComboBox cmbDiscountType;
        private Label lblBaseValue;
        private TextBox txtBaseValue;
        private Label lblMinQuantity;
        private TextBox txtMinQuantity;
        private Label lblMaxQuantity;
        private TextBox txtMaxQuantity;
        private Label lblMinOrderAmount;
        private TextBox txtMinOrderAmount;
        private Label lblMaxDiscountAmount;
        private TextBox txtMaxDiscountAmount;
        private TextBox txtPriority;
        private CheckBox chkIsActive;
        private CheckBox chkIsPromotional;
        private CheckBox chkEffectiveFrom;
        private DateTimePicker dtpEffectiveFrom;
        private CheckBox chkEffectiveTo;
        private DateTimePicker dtpEffectiveTo;
        private DataGridView dgvPricingRules;
        private DataGridView dgvDiscountRules;
        private Button btnNew;
        private Button btnSave;
        private Button btnCancel;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClose;

        #endregion
    }
}
