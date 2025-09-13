namespace DistributionSoftware.Presentation.Forms
{
    partial class DeliveryChallanForm
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
            this.pnlChallanInfo = new System.Windows.Forms.Panel();
            this.lblChallanNumber = new System.Windows.Forms.Label();
            this.txtChallanNumber = new System.Windows.Forms.TextBox();
            this.lblChallanDate = new System.Windows.Forms.Label();
            this.dtpChallanDate = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.pnlBarcodeInfo = new System.Windows.Forms.Panel();
            this.lblBarcodeTitle = new System.Windows.Forms.Label();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.pnlSalesSelection = new System.Windows.Forms.Panel();
            this.btnLoadSalesInvoice = new System.Windows.Forms.Button();
            this.lblSalesInvoice = new System.Windows.Forms.Label();
            this.cmbSalesInvoice = new System.Windows.Forms.ComboBox();
            this.pnlCustomerInfo = new System.Windows.Forms.Panel();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblCustomerAddress = new System.Windows.Forms.Label();
            this.txtCustomerAddress = new System.Windows.Forms.TextBox();
            this.pnlTransportInfo = new System.Windows.Forms.Panel();
            this.lblVehicleNo = new System.Windows.Forms.Label();
            this.cmbVehicle = new System.Windows.Forms.ComboBox();
            this.btnAddVehicle = new System.Windows.Forms.Button();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.txtDriverName = new System.Windows.Forms.TextBox();
            this.lblDriverPhone = new System.Windows.Forms.Label();
            this.txtDriverPhone = new System.Windows.Forms.TextBox();
            this.lblRoute = new System.Windows.Forms.Label();
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.pnlItems = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlChallanInfo.SuspendLayout();
            this.pnlBarcodeInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.pnlSalesSelection.SuspendLayout();
            this.pnlCustomerInfo.SuspendLayout();
            this.pnlTransportInfo.SuspendLayout();
            this.pnlItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.pnlTotals.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1300, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(297, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Delivery Challan";
            // 
            // pnlChallanInfo
            // 
            this.pnlChallanInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChallanInfo.Controls.Add(this.lblStatus);
            this.pnlChallanInfo.Controls.Add(this.txtStatus);
            this.pnlChallanInfo.Controls.Add(this.lblChallanDate);
            this.pnlChallanInfo.Controls.Add(this.dtpChallanDate);
            this.pnlChallanInfo.Controls.Add(this.lblChallanNumber);
            this.pnlChallanInfo.Controls.Add(this.txtChallanNumber);
            this.pnlChallanInfo.Location = new System.Drawing.Point(20, 80);
            this.pnlChallanInfo.Name = "pnlChallanInfo";
            this.pnlChallanInfo.Size = new System.Drawing.Size(600, 100);
            this.pnlChallanInfo.TabIndex = 1;
            // 
            // lblChallanNumber
            // 
            this.lblChallanNumber.AutoSize = true;
            this.lblChallanNumber.Location = new System.Drawing.Point(10, 15);
            this.lblChallanNumber.Name = "lblChallanNumber";
            this.lblChallanNumber.Size = new System.Drawing.Size(122, 20);
            this.lblChallanNumber.TabIndex = 0;
            this.lblChallanNumber.Text = "Challan Number:";
            // 
            // txtChallanNumber
            // 
            this.txtChallanNumber.Location = new System.Drawing.Point(140, 15);
            this.txtChallanNumber.Name = "txtChallanNumber";
            this.txtChallanNumber.ReadOnly = true;
            this.txtChallanNumber.Size = new System.Drawing.Size(150, 26);
            this.txtChallanNumber.TabIndex = 1;
            // 
            // lblChallanDate
            // 
            this.lblChallanDate.AutoSize = true;
            this.lblChallanDate.Location = new System.Drawing.Point(10, 50);
            this.lblChallanDate.Name = "lblChallanDate";
            this.lblChallanDate.Size = new System.Drawing.Size(101, 20);
            this.lblChallanDate.TabIndex = 2;
            this.lblChallanDate.Text = "Challan Date:";
            // 
            // dtpChallanDate
            // 
            this.dtpChallanDate.CustomFormat = "dddd, MMMM dd, yyyy";
            this.dtpChallanDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChallanDate.Location = new System.Drawing.Point(120, 50);
            this.dtpChallanDate.Name = "dtpChallanDate";
            this.dtpChallanDate.Size = new System.Drawing.Size(200, 26);
            this.dtpChallanDate.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(350, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 20);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(420, 15);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(100, 26);
            this.txtStatus.TabIndex = 5;
            this.txtStatus.Text = "DRAFT";
            // 
            // pnlBarcodeInfo
            // 
            this.pnlBarcodeInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBarcodeInfo.Controls.Add(this.lblBarcodeTitle);
            this.pnlBarcodeInfo.Controls.Add(this.picBarcode);
            this.pnlBarcodeInfo.Location = new System.Drawing.Point(640, 80);
            this.pnlBarcodeInfo.Name = "pnlBarcodeInfo";
            this.pnlBarcodeInfo.Size = new System.Drawing.Size(580, 150);
            this.pnlBarcodeInfo.TabIndex = 2;
            // 
            // lblBarcodeTitle
            // 
            this.lblBarcodeTitle.AutoSize = true;
            this.lblBarcodeTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBarcodeTitle.Location = new System.Drawing.Point(10, 10);
            this.lblBarcodeTitle.Name = "lblBarcodeTitle";
            this.lblBarcodeTitle.Size = new System.Drawing.Size(89, 28);
            this.lblBarcodeTitle.TabIndex = 0;
            this.lblBarcodeTitle.Text = "Barcode";
            // 
            // picBarcode
            // 
            this.picBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBarcode.Location = new System.Drawing.Point(10, 40);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(300, 100);
            this.picBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBarcode.TabIndex = 1;
            this.picBarcode.TabStop = false;
            // 
            // pnlSalesSelection
            // 
            this.pnlSalesSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSalesSelection.Controls.Add(this.btnLoadSalesInvoice);
            this.pnlSalesSelection.Controls.Add(this.lblSalesInvoice);
            this.pnlSalesSelection.Controls.Add(this.cmbSalesInvoice);
            this.pnlSalesSelection.Location = new System.Drawing.Point(20, 190);
            this.pnlSalesSelection.Name = "pnlSalesSelection";
            this.pnlSalesSelection.Size = new System.Drawing.Size(600, 60);
            this.pnlSalesSelection.TabIndex = 3;
            // 
            // btnLoadSalesInvoice
            // 
            this.btnLoadSalesInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnLoadSalesInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadSalesInvoice.ForeColor = System.Drawing.Color.White;
            this.btnLoadSalesInvoice.Location = new System.Drawing.Point(500, 15);
            this.btnLoadSalesInvoice.Name = "btnLoadSalesInvoice";
            this.btnLoadSalesInvoice.Size = new System.Drawing.Size(80, 30);
            this.btnLoadSalesInvoice.TabIndex = 2;
            this.btnLoadSalesInvoice.Text = "Load";
            this.btnLoadSalesInvoice.UseVisualStyleBackColor = false;
            this.btnLoadSalesInvoice.Click += new System.EventHandler(this.BtnLoadSalesInvoice_Click);
            // 
            // lblSalesInvoice
            // 
            this.lblSalesInvoice.AutoSize = true;
            this.lblSalesInvoice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalesInvoice.Location = new System.Drawing.Point(10, 20);
            this.lblSalesInvoice.Name = "lblSalesInvoice";
            this.lblSalesInvoice.Size = new System.Drawing.Size(95, 25);
            this.lblSalesInvoice.TabIndex = 1;
            this.lblSalesInvoice.Text = "Sales Invoice:";
            // 
            // cmbSalesInvoice
            // 
            this.cmbSalesInvoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalesInvoice.FormattingEnabled = true;
            this.cmbSalesInvoice.Location = new System.Drawing.Point(140, 20);
            this.cmbSalesInvoice.Name = "cmbSalesInvoice";
            this.cmbSalesInvoice.Size = new System.Drawing.Size(350, 28);
            this.cmbSalesInvoice.TabIndex = 0;
            // 
            // pnlCustomerInfo
            // 
            this.pnlCustomerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomerInfo.Controls.Add(this.lblCustomerAddress);
            this.pnlCustomerInfo.Controls.Add(this.txtCustomerAddress);
            this.pnlCustomerInfo.Controls.Add(this.lblCustomerName);
            this.pnlCustomerInfo.Controls.Add(this.txtCustomerName);
            this.pnlCustomerInfo.Location = new System.Drawing.Point(20, 260);
            this.pnlCustomerInfo.Name = "pnlCustomerInfo";
            this.pnlCustomerInfo.Size = new System.Drawing.Size(600, 100);
            this.pnlCustomerInfo.TabIndex = 4;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(10, 15);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(125, 20);
            this.lblCustomerName.TabIndex = 0;
            this.lblCustomerName.Text = "Customer Name:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(140, 15);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(200, 26);
            this.txtCustomerName.TabIndex = 1;
            // 
            // lblCustomerAddress
            // 
            this.lblCustomerAddress.AutoSize = true;
            this.lblCustomerAddress.Location = new System.Drawing.Point(10, 50);
            this.lblCustomerAddress.Name = "lblCustomerAddress";
            this.lblCustomerAddress.Size = new System.Drawing.Size(72, 20);
            this.lblCustomerAddress.TabIndex = 2;
            this.lblCustomerAddress.Text = "Address:";
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.Location = new System.Drawing.Point(90, 50);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.ReadOnly = true;
            this.txtCustomerAddress.Size = new System.Drawing.Size(500, 26);
            this.txtCustomerAddress.TabIndex = 3;
            // 
            // pnlTransportInfo
            // 
            this.pnlTransportInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTransportInfo.Controls.Add(this.lblRoute);
            this.pnlTransportInfo.Controls.Add(this.cmbRoute);
            this.pnlTransportInfo.Controls.Add(this.lblDriverPhone);
            this.pnlTransportInfo.Controls.Add(this.txtDriverPhone);
            this.pnlTransportInfo.Controls.Add(this.lblDriverName);
            this.pnlTransportInfo.Controls.Add(this.txtDriverName);
            this.pnlTransportInfo.Controls.Add(this.btnAddVehicle);
            this.pnlTransportInfo.Controls.Add(this.cmbVehicle);
            this.pnlTransportInfo.Controls.Add(this.lblVehicleNo);
            this.pnlTransportInfo.Location = new System.Drawing.Point(640, 240);
            this.pnlTransportInfo.Name = "pnlTransportInfo";
            this.pnlTransportInfo.Size = new System.Drawing.Size(580, 150);
            this.pnlTransportInfo.TabIndex = 5;
            // 
            // lblVehicleNo
            // 
            this.lblVehicleNo.AutoSize = true;
            this.lblVehicleNo.Location = new System.Drawing.Point(10, 15);
            this.lblVehicleNo.Name = "lblVehicleNo";
            this.lblVehicleNo.Size = new System.Drawing.Size(89, 20);
            this.lblVehicleNo.TabIndex = 0;
            this.lblVehicleNo.Text = "Vehicle No:";
            // 
            // cmbVehicle
            // 
            this.cmbVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehicle.FormattingEnabled = true;
            this.cmbVehicle.Location = new System.Drawing.Point(100, 15);
            this.cmbVehicle.Name = "cmbVehicle";
            this.cmbVehicle.Size = new System.Drawing.Size(200, 28);
            this.cmbVehicle.TabIndex = 1;
            this.cmbVehicle.SelectedIndexChanged += new System.EventHandler(this.CmbVehicle_SelectedIndexChanged);
            // 
            // btnAddVehicle
            // 
            this.btnAddVehicle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddVehicle.FlatAppearance.BorderSize = 0;
            this.btnAddVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVehicle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnAddVehicle.ForeColor = System.Drawing.Color.White;
            this.btnAddVehicle.Location = new System.Drawing.Point(310, 12);
            this.btnAddVehicle.Name = "btnAddVehicle";
            this.btnAddVehicle.Size = new System.Drawing.Size(60, 26);
            this.btnAddVehicle.TabIndex = 2;
            this.btnAddVehicle.Text = "Add";
            this.btnAddVehicle.UseVisualStyleBackColor = false;
            this.btnAddVehicle.Click += new System.EventHandler(this.BtnAddVehicle_Click);
            // 
            // lblDriverName
            // 
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Location = new System.Drawing.Point(10, 50);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.Size = new System.Drawing.Size(102, 20);
            this.lblDriverName.TabIndex = 2;
            this.lblDriverName.Text = "Driver Name:";
            // 
            // txtDriverName
            // 
            this.txtDriverName.Location = new System.Drawing.Point(120, 47);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(200, 26);
            this.txtDriverName.TabIndex = 3;
            // 
            // lblDriverPhone
            // 
            this.lblDriverPhone.AutoSize = true;
            this.lblDriverPhone.Location = new System.Drawing.Point(350, 50);
            this.lblDriverPhone.Name = "lblDriverPhone";
            this.lblDriverPhone.Size = new System.Drawing.Size(59, 20);
            this.lblDriverPhone.TabIndex = 4;
            this.lblDriverPhone.Text = "Phone:";
            // 
            // txtDriverPhone
            // 
            this.txtDriverPhone.Location = new System.Drawing.Point(420, 47);
            this.txtDriverPhone.Name = "txtDriverPhone";
            this.txtDriverPhone.Size = new System.Drawing.Size(150, 26);
            this.txtDriverPhone.TabIndex = 5;
            // 
            // lblRoute
            // 
            this.lblRoute.AutoSize = true;
            this.lblRoute.Location = new System.Drawing.Point(10, 80);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(50, 20);
            this.lblRoute.TabIndex = 6;
            this.lblRoute.Text = "Route:";
            // 
            // cmbRoute
            // 
            this.cmbRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Location = new System.Drawing.Point(100, 77);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(200, 28);
            this.cmbRoute.TabIndex = 7;
            // 
            // pnlItems
            // 
            this.pnlItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlItems.Controls.Add(this.dgvItems);
            this.pnlItems.Location = new System.Drawing.Point(20, 370);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(1200, 200);
            this.pnlItems.TabIndex = 6;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductCode,
            this.colProductName,
            this.colQuantity,
            this.colUnit,
            this.colUnitPrice,
            this.colTotalAmount});
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(0, 0);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersWidth = 62;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(1198, 198);
            this.dgvItems.TabIndex = 0;
            // 
            // colProductCode
            // 
            this.colProductCode.HeaderText = "Product Code";
            this.colProductCode.MinimumWidth = 8;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            this.colProductCode.Width = 150;
            // 
            // colProductName
            // 
            this.colProductName.HeaderText = "Product Name";
            this.colProductName.MinimumWidth = 8;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.Width = 300;
            // 
            // colQuantity
            // 
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.MinimumWidth = 8;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            this.colQuantity.Width = 100;
            // 
            // colUnit
            // 
            this.colUnit.HeaderText = "Unit";
            this.colUnit.MinimumWidth = 8;
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            this.colUnit.Width = 80;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.HeaderText = "Unit Price";
            this.colUnitPrice.MinimumWidth = 8;
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.ReadOnly = true;
            this.colUnitPrice.Width = 150;
            // 
            // colTotalAmount
            // 
            this.colTotalAmount.HeaderText = "Total Amount";
            this.colTotalAmount.MinimumWidth = 8;
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.ReadOnly = true;
            this.colTotalAmount.Width = 150;
            // 
            // pnlTotals
            // 
            this.pnlTotals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotals.Controls.Add(this.lblTotalAmount);
            this.pnlTotals.Controls.Add(this.txtTotalAmount);
            this.pnlTotals.Location = new System.Drawing.Point(20, 580);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(600, 60);
            this.pnlTotals.TabIndex = 7;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.Location = new System.Drawing.Point(10, 20);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(146, 28);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "Total Amount:";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTotalAmount.Location = new System.Drawing.Point(200, 17);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(150, 34);
            this.txtTotalAmount.TabIndex = 1;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlActions
            // 
            this.pnlActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActions.Controls.Add(this.lblRemarks);
            this.pnlActions.Controls.Add(this.txtRemarks);
            this.pnlActions.Controls.Add(this.btnPrint);
            this.pnlActions.Controls.Add(this.btnCancel);
            this.pnlActions.Controls.Add(this.btnSave);
            this.pnlActions.Location = new System.Drawing.Point(20, 650);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1200, 80);
            this.pnlActions.TabIndex = 8;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(10, 15);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(77, 20);
            this.lblRemarks.TabIndex = 0;
            this.lblRemarks.Text = "Remarks:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(90, 12);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(400, 26);
            this.txtRemarks.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(1000, 25);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 30);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1100, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(900, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // DeliveryChallanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1300, 800);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlTotals);
            this.Controls.Add(this.pnlItems);
            this.Controls.Add(this.pnlTransportInfo);
            this.Controls.Add(this.pnlCustomerInfo);
            this.Controls.Add(this.pnlSalesSelection);
            this.Controls.Add(this.pnlBarcodeInfo);
            this.Controls.Add(this.pnlChallanInfo);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1300, 600);
            this.Name = "DeliveryChallanForm";
            this.Text = "Delivery Challan - POS System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeliveryChallanForm_FormClosing);
            this.Load += new System.EventHandler(this.DeliveryChallanForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlChallanInfo.ResumeLayout(false);
            this.pnlChallanInfo.PerformLayout();
            this.pnlBarcodeInfo.ResumeLayout(false);
            this.pnlBarcodeInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.pnlSalesSelection.ResumeLayout(false);
            this.pnlSalesSelection.PerformLayout();
            this.pnlCustomerInfo.ResumeLayout(false);
            this.pnlCustomerInfo.PerformLayout();
            this.pnlTransportInfo.ResumeLayout(false);
            this.pnlTransportInfo.PerformLayout();
            this.pnlItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.pnlTotals.ResumeLayout(false);
            this.pnlTotals.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlChallanInfo;
        private System.Windows.Forms.Label lblChallanNumber;
        private System.Windows.Forms.TextBox txtChallanNumber;
        private System.Windows.Forms.Label lblChallanDate;
        private System.Windows.Forms.DateTimePicker dtpChallanDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Panel pnlBarcodeInfo;
        private System.Windows.Forms.Label lblBarcodeTitle;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.Panel pnlSalesSelection;
        private System.Windows.Forms.Button btnLoadSalesInvoice;
        private System.Windows.Forms.Label lblSalesInvoice;
        private System.Windows.Forms.ComboBox cmbSalesInvoice;
        private System.Windows.Forms.Panel pnlCustomerInfo;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblCustomerAddress;
        private System.Windows.Forms.TextBox txtCustomerAddress;
        private System.Windows.Forms.Panel pnlTransportInfo;
        private System.Windows.Forms.Label lblVehicleNo;
        private System.Windows.Forms.ComboBox cmbVehicle;
        private System.Windows.Forms.Button btnAddVehicle;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.TextBox txtDriverName;
        private System.Windows.Forms.Label lblDriverPhone;
        private System.Windows.Forms.TextBox txtDriverPhone;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.Panel pnlItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalAmount;
        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}

