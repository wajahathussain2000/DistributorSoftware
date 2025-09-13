namespace DistributionSoftware.Presentation.Forms
{
    partial class SalesmanTargetForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.dgvTargets = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpStatusComments = new System.Windows.Forms.GroupBox();
            this.txtMarketConditions = new System.Windows.Forms.TextBox();
            this.lblMarketConditions = new System.Windows.Forms.Label();
            this.txtSalesmanComments = new System.Windows.Forms.TextBox();
            this.lblSalesmanComments = new System.Windows.Forms.Label();
            this.txtManagerComments = new System.Windows.Forms.TextBox();
            this.lblManagerComments = new System.Windows.Forms.Label();
            this.cmbRating = new System.Windows.Forms.ComboBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkCommissionEligible = new System.Windows.Forms.CheckBox();
            this.chkBonusEligible = new System.Windows.Forms.CheckBox();
            this.txtBonusAmount = new System.Windows.Forms.TextBox();
            this.lblBonusAmount = new System.Windows.Forms.Label();
            this.txtCommissionAmount = new System.Windows.Forms.TextBox();
            this.lblCommissionAmount = new System.Windows.Forms.Label();
            this.grpTargetValues = new System.Windows.Forms.GroupBox();
            this.txtCategoryUnit = new System.Windows.Forms.TextBox();
            this.lblCategoryUnit = new System.Windows.Forms.Label();
            this.txtCategoryRevenue = new System.Windows.Forms.TextBox();
            this.lblCategoryRevenue = new System.Windows.Forms.Label();
            this.txtProductCategory = new System.Windows.Forms.TextBox();
            this.lblProductCategory = new System.Windows.Forms.Label();
            this.txtInvoiceTarget = new System.Windows.Forms.TextBox();
            this.lblInvoiceTarget = new System.Windows.Forms.Label();
            this.txtCustomerTarget = new System.Windows.Forms.TextBox();
            this.lblCustomerTarget = new System.Windows.Forms.Label();
            this.txtUnitTarget = new System.Windows.Forms.TextBox();
            this.lblUnitTarget = new System.Windows.Forms.Label();
            this.txtRevenueTarget = new System.Windows.Forms.TextBox();
            this.lblRevenueTarget = new System.Windows.Forms.Label();
            this.grpTargetInformation = new System.Windows.Forms.GroupBox();
            this.txtPeriodName = new System.Windows.Forms.TextBox();
            this.lblPeriodName = new System.Windows.Forms.Label();
            this.cmbPeriodEndMonth = new System.Windows.Forms.ComboBox();
            this.cmbPeriodEndDay = new System.Windows.Forms.ComboBox();
            this.lblPeriodEnd = new System.Windows.Forms.Label();
            this.cmbPeriodStartMonth = new System.Windows.Forms.ComboBox();
            this.cmbPeriodStartDay = new System.Windows.Forms.ComboBox();
            this.lblPeriodStart = new System.Windows.Forms.Label();
            this.cmbTargetType = new System.Windows.Forms.ComboBox();
            this.lblTargetType = new System.Windows.Forms.Label();
            this.cmbSalesman = new System.Windows.Forms.ComboBox();
            this.lblSalesman = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTargets)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.grpStatusComments.SuspendLayout();
            this.grpTargetValues.SuspendLayout();
            this.grpTargetInformation.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1400, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Salesman Target Achievement Management";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 60);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Size = new System.Drawing.Size(1400, 900);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.dgvTargets);
            this.pnlLeft.Location = new System.Drawing.Point(10, 10);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(900, 800);
            this.pnlLeft.TabIndex = 0;
            // 
            // dgvTargets
            // 
            this.dgvTargets.AllowUserToAddRows = false;
            this.dgvTargets.AllowUserToDeleteRows = false;
            this.dgvTargets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTargets.BackgroundColor = System.Drawing.Color.White;
            this.dgvTargets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTargets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTargets.Location = new System.Drawing.Point(0, 0);
            this.dgvTargets.MultiSelect = false;
            this.dgvTargets.Name = "dgvTargets";
            this.dgvTargets.ReadOnly = true;
            this.dgvTargets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTargets.Size = new System.Drawing.Size(900, 720);
            this.dgvTargets.TabIndex = 0;
            this.dgvTargets.SelectionChanged += new System.EventHandler(this.dgvTargets_SelectionChanged);
            this.dgvTargets.DoubleClick += new System.EventHandler(this.dgvTargets_DoubleClick);
            this.dgvTargets.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTargets_CellClick);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.grpStatusComments);
            this.pnlRight.Controls.Add(this.grpTargetValues);
            this.pnlRight.Controls.Add(this.grpTargetInformation);
            this.pnlRight.Location = new System.Drawing.Point(920, 10);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(470, 800);
            this.pnlRight.TabIndex = 1;
            // 
            // grpStatusComments
            // 
            this.grpStatusComments.Controls.Add(this.txtMarketConditions);
            this.grpStatusComments.Controls.Add(this.lblMarketConditions);
            this.grpStatusComments.Controls.Add(this.txtSalesmanComments);
            this.grpStatusComments.Controls.Add(this.lblSalesmanComments);
            this.grpStatusComments.Controls.Add(this.txtManagerComments);
            this.grpStatusComments.Controls.Add(this.lblManagerComments);
            this.grpStatusComments.Controls.Add(this.cmbRating);
            this.grpStatusComments.Controls.Add(this.lblRating);
            this.grpStatusComments.Controls.Add(this.cmbStatus);
            this.grpStatusComments.Controls.Add(this.lblStatus);
            this.grpStatusComments.Controls.Add(this.chkCommissionEligible);
            this.grpStatusComments.Controls.Add(this.chkBonusEligible);
            this.grpStatusComments.Controls.Add(this.txtBonusAmount);
            this.grpStatusComments.Controls.Add(this.lblBonusAmount);
            this.grpStatusComments.Controls.Add(this.txtCommissionAmount);
            this.grpStatusComments.Controls.Add(this.lblCommissionAmount);
            this.grpStatusComments.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatusComments.Location = new System.Drawing.Point(10, 10);
            this.grpStatusComments.Name = "grpStatusComments";
            this.grpStatusComments.Size = new System.Drawing.Size(450, 230);
            this.grpStatusComments.TabIndex = 0;
            this.grpStatusComments.TabStop = false;
            this.grpStatusComments.Text = "Status Comments";
            // 
            // txtMarketConditions
            // 
            this.txtMarketConditions.Location = new System.Drawing.Point(120, 185);
            this.txtMarketConditions.Multiline = true;
            this.txtMarketConditions.Name = "txtMarketConditions";
            this.txtMarketConditions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMarketConditions.Size = new System.Drawing.Size(320, 30);
            this.txtMarketConditions.TabIndex = 11;
            // 
            // lblMarketConditions
            // 
            this.lblMarketConditions.AutoSize = true;
            this.lblMarketConditions.Location = new System.Drawing.Point(10, 188);
            this.lblMarketConditions.Name = "lblMarketConditions";
            this.lblMarketConditions.Size = new System.Drawing.Size(110, 15);
            this.lblMarketConditions.TabIndex = 10;
            this.lblMarketConditions.Text = "Market Conditions:";
            // 
            // txtSalesmanComments
            // 
            this.txtSalesmanComments.Location = new System.Drawing.Point(120, 145);
            this.txtSalesmanComments.Multiline = true;
            this.txtSalesmanComments.Name = "txtSalesmanComments";
            this.txtSalesmanComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSalesmanComments.Size = new System.Drawing.Size(320, 30);
            this.txtSalesmanComments.TabIndex = 9;
            // 
            // lblSalesmanComments
            // 
            this.lblSalesmanComments.AutoSize = true;
            this.lblSalesmanComments.Location = new System.Drawing.Point(10, 148);
            this.lblSalesmanComments.Name = "lblSalesmanComments";
            this.lblSalesmanComments.Size = new System.Drawing.Size(120, 15);
            this.lblSalesmanComments.TabIndex = 8;
            this.lblSalesmanComments.Text = "Salesman Comments:";
            // 
            // txtManagerComments
            // 
            this.txtManagerComments.Location = new System.Drawing.Point(120, 105);
            this.txtManagerComments.Multiline = true;
            this.txtManagerComments.Name = "txtManagerComments";
            this.txtManagerComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtManagerComments.Size = new System.Drawing.Size(320, 30);
            this.txtManagerComments.TabIndex = 7;
            // 
            // lblManagerComments
            // 
            this.lblManagerComments.AutoSize = true;
            this.lblManagerComments.Location = new System.Drawing.Point(10, 108);
            this.lblManagerComments.Name = "lblManagerComments";
            this.lblManagerComments.Size = new System.Drawing.Size(110, 15);
            this.lblManagerComments.TabIndex = 6;
            this.lblManagerComments.Text = "Manager Comments:";
            // 
            // cmbRating
            // 
            this.cmbRating.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRating.FormattingEnabled = true;
            this.cmbRating.Location = new System.Drawing.Point(320, 70);
            this.cmbRating.Name = "cmbRating";
            this.cmbRating.Size = new System.Drawing.Size(120, 23);
            this.cmbRating.TabIndex = 5;
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new System.Drawing.Point(270, 73);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(44, 15);
            this.lblRating.TabIndex = 4;
            this.lblRating.Text = "Rating:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(60, 70);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(120, 23);
            this.cmbStatus.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(10, 73);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 15);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:";
            // 
            // chkCommissionEligible
            // 
            this.chkCommissionEligible.AutoSize = true;
            this.chkCommissionEligible.Location = new System.Drawing.Point(200, 20);
            this.chkCommissionEligible.Name = "chkCommissionEligible";
            this.chkCommissionEligible.Size = new System.Drawing.Size(130, 19);
            this.chkCommissionEligible.TabIndex = 1;
            this.chkCommissionEligible.Text = "Commission Eligible";
            this.chkCommissionEligible.UseVisualStyleBackColor = true;
            // 
            // chkBonusEligible
            // 
            this.chkBonusEligible.AutoSize = true;
            this.chkBonusEligible.Location = new System.Drawing.Point(10, 20);
            this.chkBonusEligible.Name = "chkBonusEligible";
            this.chkBonusEligible.Size = new System.Drawing.Size(100, 19);
            this.chkBonusEligible.TabIndex = 0;
            this.chkBonusEligible.Text = "Bonus Eligible";
            this.chkBonusEligible.UseVisualStyleBackColor = true;
            // 
            // txtBonusAmount
            // 
            this.txtBonusAmount.Location = new System.Drawing.Point(100, 45);
            this.txtBonusAmount.Name = "txtBonusAmount";
            this.txtBonusAmount.Size = new System.Drawing.Size(100, 23);
            this.txtBonusAmount.TabIndex = 2;
            // 
            // lblBonusAmount
            // 
            this.lblBonusAmount.AutoSize = true;
            this.lblBonusAmount.Location = new System.Drawing.Point(10, 48);
            this.lblBonusAmount.Name = "lblBonusAmount";
            this.lblBonusAmount.Size = new System.Drawing.Size(84, 15);
            this.lblBonusAmount.TabIndex = 3;
            this.lblBonusAmount.Text = "Bonus Amount:";
            // 
            // txtCommissionAmount
            // 
            this.txtCommissionAmount.Location = new System.Drawing.Point(320, 45);
            this.txtCommissionAmount.Name = "txtCommissionAmount";
            this.txtCommissionAmount.Size = new System.Drawing.Size(100, 23);
            this.txtCommissionAmount.TabIndex = 4;
            // 
            // lblCommissionAmount
            // 
            this.lblCommissionAmount.AutoSize = true;
            this.lblCommissionAmount.Location = new System.Drawing.Point(220, 48);
            this.lblCommissionAmount.Name = "lblCommissionAmount";
            this.lblCommissionAmount.Size = new System.Drawing.Size(110, 15);
            this.lblCommissionAmount.TabIndex = 5;
            this.lblCommissionAmount.Text = "Commission Amount:";
            // 
            // grpTargetValues
            // 
            this.grpTargetValues.Controls.Add(this.txtCategoryUnit);
            this.grpTargetValues.Controls.Add(this.lblCategoryUnit);
            this.grpTargetValues.Controls.Add(this.txtCategoryRevenue);
            this.grpTargetValues.Controls.Add(this.lblCategoryRevenue);
            this.grpTargetValues.Controls.Add(this.txtProductCategory);
            this.grpTargetValues.Controls.Add(this.lblProductCategory);
            this.grpTargetValues.Controls.Add(this.txtInvoiceTarget);
            this.grpTargetValues.Controls.Add(this.lblInvoiceTarget);
            this.grpTargetValues.Controls.Add(this.txtCustomerTarget);
            this.grpTargetValues.Controls.Add(this.lblCustomerTarget);
            this.grpTargetValues.Controls.Add(this.txtUnitTarget);
            this.grpTargetValues.Controls.Add(this.lblUnitTarget);
            this.grpTargetValues.Controls.Add(this.txtRevenueTarget);
            this.grpTargetValues.Controls.Add(this.lblRevenueTarget);
            this.grpTargetValues.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTargetValues.Location = new System.Drawing.Point(10, 250);
            this.grpTargetValues.Name = "grpTargetValues";
            this.grpTargetValues.Size = new System.Drawing.Size(450, 200);
            this.grpTargetValues.TabIndex = 1;
            this.grpTargetValues.TabStop = false;
            this.grpTargetValues.Text = "Target Values";
            // 
            // txtCategoryUnit
            // 
            this.txtCategoryUnit.Location = new System.Drawing.Point(320, 160);
            this.txtCategoryUnit.Name = "txtCategoryUnit";
            this.txtCategoryUnit.Size = new System.Drawing.Size(120, 23);
            this.txtCategoryUnit.TabIndex = 13;
            // 
            // lblCategoryUnit
            // 
            this.lblCategoryUnit.AutoSize = true;
            this.lblCategoryUnit.Location = new System.Drawing.Point(230, 163);
            this.lblCategoryUnit.Name = "lblCategoryUnit";
            this.lblCategoryUnit.Size = new System.Drawing.Size(84, 15);
            this.lblCategoryUnit.TabIndex = 12;
            this.lblCategoryUnit.Text = "Category Unit";
            // 
            // txtCategoryRevenue
            // 
            this.txtCategoryRevenue.Location = new System.Drawing.Point(100, 160);
            this.txtCategoryRevenue.Name = "txtCategoryRevenue";
            this.txtCategoryRevenue.Size = new System.Drawing.Size(120, 23);
            this.txtCategoryRevenue.TabIndex = 11;
            // 
            // lblCategoryRevenue
            // 
            this.lblCategoryRevenue.AutoSize = true;
            this.lblCategoryRevenue.Location = new System.Drawing.Point(10, 163);
            this.lblCategoryRevenue.Name = "lblCategoryRevenue";
            this.lblCategoryRevenue.Size = new System.Drawing.Size(110, 15);
            this.lblCategoryRevenue.TabIndex = 10;
            this.lblCategoryRevenue.Text = "Category Revenue";
            // 
            // txtProductCategory
            // 
            this.txtProductCategory.Location = new System.Drawing.Point(120, 120);
            this.txtProductCategory.Name = "txtProductCategory";
            this.txtProductCategory.Size = new System.Drawing.Size(320, 23);
            this.txtProductCategory.TabIndex = 9;
            // 
            // lblProductCategory
            // 
            this.lblProductCategory.AutoSize = true;
            this.lblProductCategory.Location = new System.Drawing.Point(10, 123);
            this.lblProductCategory.Name = "lblProductCategory";
            this.lblProductCategory.Size = new System.Drawing.Size(100, 15);
            this.lblProductCategory.TabIndex = 8;
            this.lblProductCategory.Text = "Product Category";
            // 
            // txtInvoiceTarget
            // 
            this.txtInvoiceTarget.Location = new System.Drawing.Point(320, 80);
            this.txtInvoiceTarget.Name = "txtInvoiceTarget";
            this.txtInvoiceTarget.Size = new System.Drawing.Size(120, 23);
            this.txtInvoiceTarget.TabIndex = 7;
            // 
            // lblInvoiceTarget
            // 
            this.lblInvoiceTarget.AutoSize = true;
            this.lblInvoiceTarget.Location = new System.Drawing.Point(230, 83);
            this.lblInvoiceTarget.Name = "lblInvoiceTarget";
            this.lblInvoiceTarget.Size = new System.Drawing.Size(84, 15);
            this.lblInvoiceTarget.TabIndex = 6;
            this.lblInvoiceTarget.Text = "Invoice Target";
            // 
            // txtCustomerTarget
            // 
            this.txtCustomerTarget.Location = new System.Drawing.Point(100, 80);
            this.txtCustomerTarget.Name = "txtCustomerTarget";
            this.txtCustomerTarget.Size = new System.Drawing.Size(120, 23);
            this.txtCustomerTarget.TabIndex = 5;
            // 
            // lblCustomerTarget
            // 
            this.lblCustomerTarget.AutoSize = true;
            this.lblCustomerTarget.Location = new System.Drawing.Point(10, 83);
            this.lblCustomerTarget.Name = "lblCustomerTarget";
            this.lblCustomerTarget.Size = new System.Drawing.Size(94, 15);
            this.lblCustomerTarget.TabIndex = 4;
            this.lblCustomerTarget.Text = "Customer Target";
            // 
            // txtUnitTarget
            // 
            this.txtUnitTarget.Location = new System.Drawing.Point(320, 40);
            this.txtUnitTarget.Name = "txtUnitTarget";
            this.txtUnitTarget.Size = new System.Drawing.Size(120, 23);
            this.txtUnitTarget.TabIndex = 3;
            // 
            // lblUnitTarget
            // 
            this.lblUnitTarget.AutoSize = true;
            this.lblUnitTarget.Location = new System.Drawing.Point(230, 43);
            this.lblUnitTarget.Name = "lblUnitTarget";
            this.lblUnitTarget.Size = new System.Drawing.Size(70, 15);
            this.lblUnitTarget.TabIndex = 2;
            this.lblUnitTarget.Text = "Unit Target";
            // 
            // txtRevenueTarget
            // 
            this.txtRevenueTarget.Location = new System.Drawing.Point(100, 40);
            this.txtRevenueTarget.Name = "txtRevenueTarget";
            this.txtRevenueTarget.Size = new System.Drawing.Size(120, 23);
            this.txtRevenueTarget.TabIndex = 1;
            // 
            // lblRevenueTarget
            // 
            this.lblRevenueTarget.AutoSize = true;
            this.lblRevenueTarget.Location = new System.Drawing.Point(10, 43);
            this.lblRevenueTarget.Name = "lblRevenueTarget";
            this.lblRevenueTarget.Size = new System.Drawing.Size(90, 15);
            this.lblRevenueTarget.TabIndex = 0;
            this.lblRevenueTarget.Text = "Revenue Target";
            // 
            // grpTargetInformation
            // 
            this.grpTargetInformation.Controls.Add(this.txtPeriodName);
            this.grpTargetInformation.Controls.Add(this.lblPeriodName);
            this.grpTargetInformation.Controls.Add(this.cmbPeriodEndMonth);
            this.grpTargetInformation.Controls.Add(this.cmbPeriodEndDay);
            this.grpTargetInformation.Controls.Add(this.lblPeriodEnd);
            this.grpTargetInformation.Controls.Add(this.cmbPeriodStartMonth);
            this.grpTargetInformation.Controls.Add(this.cmbPeriodStartDay);
            this.grpTargetInformation.Controls.Add(this.lblPeriodStart);
            this.grpTargetInformation.Controls.Add(this.cmbTargetType);
            this.grpTargetInformation.Controls.Add(this.lblTargetType);
            this.grpTargetInformation.Controls.Add(this.cmbSalesman);
            this.grpTargetInformation.Controls.Add(this.lblSalesman);
            this.grpTargetInformation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTargetInformation.Location = new System.Drawing.Point(10, 460);
            this.grpTargetInformation.Name = "grpTargetInformation";
            this.grpTargetInformation.Size = new System.Drawing.Size(450, 280);
            this.grpTargetInformation.TabIndex = 2;
            this.grpTargetInformation.TabStop = false;
            this.grpTargetInformation.Text = "Target Information";
            // 
            // txtPeriodName
            // 
            this.txtPeriodName.Location = new System.Drawing.Point(100, 160);
            this.txtPeriodName.Name = "txtPeriodName";
            this.txtPeriodName.Size = new System.Drawing.Size(340, 23);
            this.txtPeriodName.TabIndex = 11;
            // 
            // lblPeriodName
            // 
            this.lblPeriodName.AutoSize = true;
            this.lblPeriodName.Location = new System.Drawing.Point(10, 163);
            this.lblPeriodName.Name = "lblPeriodName";
            this.lblPeriodName.Size = new System.Drawing.Size(80, 15);
            this.lblPeriodName.TabIndex = 10;
            this.lblPeriodName.Text = "Period Name:";
            // 
            // cmbPeriodEndMonth
            // 
            this.cmbPeriodEndMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodEndMonth.FormattingEnabled = true;
            this.cmbPeriodEndMonth.Location = new System.Drawing.Point(320, 120);
            this.cmbPeriodEndMonth.Name = "cmbPeriodEndMonth";
            this.cmbPeriodEndMonth.Size = new System.Drawing.Size(120, 23);
            this.cmbPeriodEndMonth.TabIndex = 9;
            // 
            // cmbPeriodEndDay
            // 
            this.cmbPeriodEndDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodEndDay.FormattingEnabled = true;
            this.cmbPeriodEndDay.Location = new System.Drawing.Point(200, 120);
            this.cmbPeriodEndDay.Name = "cmbPeriodEndDay";
            this.cmbPeriodEndDay.Size = new System.Drawing.Size(110, 23);
            this.cmbPeriodEndDay.TabIndex = 8;
            // 
            // lblPeriodEnd
            // 
            this.lblPeriodEnd.AutoSize = true;
            this.lblPeriodEnd.Location = new System.Drawing.Point(10, 123);
            this.lblPeriodEnd.Name = "lblPeriodEnd";
            this.lblPeriodEnd.Size = new System.Drawing.Size(70, 15);
            this.lblPeriodEnd.TabIndex = 7;
            this.lblPeriodEnd.Text = "Period End:";
            // 
            // cmbPeriodStartMonth
            // 
            this.cmbPeriodStartMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodStartMonth.FormattingEnabled = true;
            this.cmbPeriodStartMonth.Location = new System.Drawing.Point(320, 80);
            this.cmbPeriodStartMonth.Name = "cmbPeriodStartMonth";
            this.cmbPeriodStartMonth.Size = new System.Drawing.Size(120, 23);
            this.cmbPeriodStartMonth.TabIndex = 6;
            // 
            // cmbPeriodStartDay
            // 
            this.cmbPeriodStartDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodStartDay.FormattingEnabled = true;
            this.cmbPeriodStartDay.Location = new System.Drawing.Point(200, 80);
            this.cmbPeriodStartDay.Name = "cmbPeriodStartDay";
            this.cmbPeriodStartDay.Size = new System.Drawing.Size(110, 23);
            this.cmbPeriodStartDay.TabIndex = 5;
            // 
            // lblPeriodStart
            // 
            this.lblPeriodStart.AutoSize = true;
            this.lblPeriodStart.Location = new System.Drawing.Point(10, 83);
            this.lblPeriodStart.Name = "lblPeriodStart";
            this.lblPeriodStart.Size = new System.Drawing.Size(75, 15);
            this.lblPeriodStart.TabIndex = 4;
            this.lblPeriodStart.Text = "Period Start:";
            // 
            // cmbTargetType
            // 
            this.cmbTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetType.FormattingEnabled = true;
            this.cmbTargetType.Location = new System.Drawing.Point(100, 40);
            this.cmbTargetType.Name = "cmbTargetType";
            this.cmbTargetType.Size = new System.Drawing.Size(340, 23);
            this.cmbTargetType.TabIndex = 3;
            // 
            // lblTargetType
            // 
            this.lblTargetType.AutoSize = true;
            this.lblTargetType.Location = new System.Drawing.Point(10, 43);
            this.lblTargetType.Name = "lblTargetType";
            this.lblTargetType.Size = new System.Drawing.Size(75, 15);
            this.lblTargetType.TabIndex = 2;
            this.lblTargetType.Text = "Target Type:";
            // 
            // cmbSalesman
            // 
            this.cmbSalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalesman.FormattingEnabled = true;
            this.cmbSalesman.Location = new System.Drawing.Point(100, 0);
            this.cmbSalesman.Name = "cmbSalesman";
            this.cmbSalesman.Size = new System.Drawing.Size(340, 23);
            this.cmbSalesman.TabIndex = 1;
            // 
            // lblSalesman
            // 
            this.lblSalesman.AutoSize = true;
            this.lblSalesman.Location = new System.Drawing.Point(10, 3);
            this.lblSalesman.Name = "lblSalesman";
            this.lblSalesman.Size = new System.Drawing.Size(60, 15);
            this.lblSalesman.TabIndex = 0;
            this.lblSalesman.Text = "Salesman:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnExport);
            this.pnlButtons.Controls.Add(this.btnRefresh);
            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnEdit);
            this.pnlButtons.Controls.Add(this.btnNew);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 800);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1400, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(1300, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 30);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1200, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(1100, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 30);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(420, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(320, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(220, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(120, 15);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(20, 15);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 30);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // SalesmanTargetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1400, 1000);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "SalesmanTargetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Salesman Target & Achievement Management";
            this.Load += new System.EventHandler(this.SalesmanTargetForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTargets)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.grpStatusComments.ResumeLayout(false);
            this.grpStatusComments.PerformLayout();
            this.grpTargetValues.ResumeLayout(false);
            this.grpTargetValues.PerformLayout();
            this.grpTargetInformation.ResumeLayout(false);
            this.grpTargetInformation.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.DataGridView dgvTargets;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpStatusComments;
        private System.Windows.Forms.TextBox txtMarketConditions;
        private System.Windows.Forms.Label lblMarketConditions;
        private System.Windows.Forms.TextBox txtSalesmanComments;
        private System.Windows.Forms.Label lblSalesmanComments;
        private System.Windows.Forms.TextBox txtManagerComments;
        private System.Windows.Forms.Label lblManagerComments;
        private System.Windows.Forms.ComboBox cmbRating;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkCommissionEligible;
        private System.Windows.Forms.CheckBox chkBonusEligible;
        private System.Windows.Forms.TextBox txtBonusAmount;
        private System.Windows.Forms.Label lblBonusAmount;
        private System.Windows.Forms.TextBox txtCommissionAmount;
        private System.Windows.Forms.Label lblCommissionAmount;
        private System.Windows.Forms.GroupBox grpTargetValues;
        private System.Windows.Forms.TextBox txtCategoryUnit;
        private System.Windows.Forms.Label lblCategoryUnit;
        private System.Windows.Forms.TextBox txtCategoryRevenue;
        private System.Windows.Forms.Label lblCategoryRevenue;
        private System.Windows.Forms.TextBox txtProductCategory;
        private System.Windows.Forms.Label lblProductCategory;
        private System.Windows.Forms.TextBox txtInvoiceTarget;
        private System.Windows.Forms.Label lblInvoiceTarget;
        private System.Windows.Forms.TextBox txtCustomerTarget;
        private System.Windows.Forms.Label lblCustomerTarget;
        private System.Windows.Forms.TextBox txtUnitTarget;
        private System.Windows.Forms.Label lblUnitTarget;
        private System.Windows.Forms.TextBox txtRevenueTarget;
        private System.Windows.Forms.Label lblRevenueTarget;
        private System.Windows.Forms.GroupBox grpTargetInformation;
        private System.Windows.Forms.TextBox txtPeriodName;
        private System.Windows.Forms.Label lblPeriodName;
        private System.Windows.Forms.ComboBox cmbPeriodEndMonth;
        private System.Windows.Forms.ComboBox cmbPeriodEndDay;
        private System.Windows.Forms.Label lblPeriodEnd;
        private System.Windows.Forms.ComboBox cmbPeriodStartMonth;
        private System.Windows.Forms.ComboBox cmbPeriodStartDay;
        private System.Windows.Forms.Label lblPeriodStart;
        private System.Windows.Forms.ComboBox cmbTargetType;
        private System.Windows.Forms.Label lblTargetType;
        private System.Windows.Forms.ComboBox cmbSalesman;
        private System.Windows.Forms.Label lblSalesman;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
    }
}
