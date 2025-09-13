namespace DistributionSoftware.Presentation.Forms
{
    partial class SalesReturnForm
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
            this.pnlReturnInfo = new System.Windows.Forms.Panel();
            this.lblTransactionTime = new System.Windows.Forms.Label();
            this.txtTransactionTime = new System.Windows.Forms.TextBox();
            this.lblCashier = new System.Windows.Forms.Label();
            this.txtCashier = new System.Windows.Forms.TextBox();
            this.lblReturnDate = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.lblReturnNumber = new System.Windows.Forms.Label();
            this.txtReturnNumber = new System.Windows.Forms.TextBox();
            this.lblReturnBarcode = new System.Windows.Forms.Label();
            this.txtReturnBarcode = new System.Windows.Forms.TextBox();
            this.btnGenerateBarcode = new System.Windows.Forms.Button();
            this.pnlBarcodeInfo = new System.Windows.Forms.Panel();
            this.lblBarcodeTitle = new System.Windows.Forms.Label();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.lblBarcodeValue = new System.Windows.Forms.Label();
            this.pnlInvoiceSelection = new System.Windows.Forms.Panel();
            this.btnLoadInvoice = new System.Windows.Forms.Button();
            this.lblInvoiceNumber = new System.Windows.Forms.Label();
            this.cmbInvoiceNumber = new System.Windows.Forms.ComboBox();
            this.pnlOriginalInvoiceDetails = new System.Windows.Forms.Panel();
            this.lblOriginalInvoiceTitle = new System.Windows.Forms.Label();
            this.lblOriginalInvoiceNumber = new System.Windows.Forms.Label();
            this.txtOriginalInvoiceNumber = new System.Windows.Forms.TextBox();
            this.lblOriginalInvoiceDate = new System.Windows.Forms.Label();
            this.txtOriginalInvoiceDate = new System.Windows.Forms.TextBox();
            this.lblOriginalInvoiceTotal = new System.Windows.Forms.Label();
            this.txtOriginalInvoiceTotal = new System.Windows.Forms.TextBox();
            this.lblOriginalInvoiceStatus = new System.Windows.Forms.Label();
            this.txtOriginalInvoiceStatus = new System.Windows.Forms.TextBox();
            this.pnlCustomerInfo = new System.Windows.Forms.Panel();
            this.lblCustomerAddress = new System.Windows.Forms.Label();
            this.txtCustomerAddress = new System.Windows.Forms.TextBox();
            this.lblCustomerPhone = new System.Windows.Forms.Label();
            this.txtCustomerPhone = new System.Windows.Forms.TextBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.pnlProductInfo = new System.Windows.Forms.Panel();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.pnlItems = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.lblTaxAmount = new System.Windows.Forms.Label();
            this.txtTaxAmount = new System.Windows.Forms.TextBox();
            this.lblTaxableAmount = new System.Windows.Forms.Label();
            this.txtTaxableAmount = new System.Windows.Forms.TextBox();
            this.lblDiscountAmount = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new System.Windows.Forms.TextBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.cmbReturnReason = new System.Windows.Forms.ComboBox();
            this.lblReturnReason = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlReturnInfo.SuspendLayout();
            this.pnlBarcodeInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.pnlInvoiceSelection.SuspendLayout();
            this.pnlOriginalInvoiceDetails.SuspendLayout();
            this.pnlCustomerInfo.SuspendLayout();
            this.pnlProductInfo.SuspendLayout();
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
            this.lblTitle.Text = "Sales Return - POS";
            // 
            // pnlReturnInfo
            // 
            this.pnlReturnInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlReturnInfo.Controls.Add(this.lblTransactionTime);
            this.pnlReturnInfo.Controls.Add(this.txtTransactionTime);
            this.pnlReturnInfo.Controls.Add(this.lblCashier);
            this.pnlReturnInfo.Controls.Add(this.txtCashier);
            this.pnlReturnInfo.Controls.Add(this.lblReturnDate);
            this.pnlReturnInfo.Controls.Add(this.dtpReturnDate);
            this.pnlReturnInfo.Controls.Add(this.lblReturnNumber);
            this.pnlReturnInfo.Controls.Add(this.txtReturnNumber);
            this.pnlReturnInfo.Controls.Add(this.lblReturnBarcode);
            this.pnlReturnInfo.Controls.Add(this.txtReturnBarcode);
            this.pnlReturnInfo.Controls.Add(this.btnGenerateBarcode);
            this.pnlReturnInfo.Location = new System.Drawing.Point(20, 80);
            this.pnlReturnInfo.Name = "pnlReturnInfo";
            this.pnlReturnInfo.Size = new System.Drawing.Size(600, 100);
            this.pnlReturnInfo.TabIndex = 1;
            // 
            // lblTransactionTime
            // 
            this.lblTransactionTime.AutoSize = true;
            this.lblTransactionTime.Location = new System.Drawing.Point(600, 15);
            this.lblTransactionTime.Name = "lblTransactionTime";
            this.lblTransactionTime.Size = new System.Drawing.Size(134, 20);
            this.lblTransactionTime.TabIndex = 6;
            this.lblTransactionTime.Text = "Transaction Time:";
            // 
            // txtTransactionTime
            // 
            this.txtTransactionTime.Location = new System.Drawing.Point(740, 12);
            this.txtTransactionTime.Name = "txtTransactionTime";
            this.txtTransactionTime.ReadOnly = true;
            this.txtTransactionTime.Size = new System.Drawing.Size(100, 26);
            this.txtTransactionTime.TabIndex = 7;
            // 
            // lblCashier
            // 
            this.lblCashier.AutoSize = true;
            this.lblCashier.Location = new System.Drawing.Point(10, 50);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(67, 20);
            this.lblCashier.TabIndex = 4;
            this.lblCashier.Text = "Cashier:";
            // 
            // txtCashier
            // 
            this.txtCashier.Location = new System.Drawing.Point(80, 47);
            this.txtCashier.Name = "txtCashier";
            this.txtCashier.ReadOnly = true;
            this.txtCashier.Size = new System.Drawing.Size(120, 26);
            this.txtCashier.TabIndex = 5;
            // 
            // lblReturnDate
            // 
            this.lblReturnDate.AutoSize = true;
            this.lblReturnDate.Location = new System.Drawing.Point(600, 50);
            this.lblReturnDate.Name = "lblReturnDate";
            this.lblReturnDate.Size = new System.Drawing.Size(101, 20);
            this.lblReturnDate.TabIndex = 2;
            this.lblReturnDate.Text = "Return Date:";
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.CustomFormat = "dddd, MMMM dd, yyyy";
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReturnDate.Location = new System.Drawing.Point(710, 47);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(200, 26);
            this.dtpReturnDate.TabIndex = 3;
            // 
            // lblReturnNumber
            // 
            this.lblReturnNumber.AutoSize = true;
            this.lblReturnNumber.Location = new System.Drawing.Point(10, 15);
            this.lblReturnNumber.Name = "lblReturnNumber";
            this.lblReturnNumber.Size = new System.Drawing.Size(122, 20);
            this.lblReturnNumber.TabIndex = 0;
            this.lblReturnNumber.Text = "Return Number:";
            // 
            // txtReturnNumber
            // 
            this.txtReturnNumber.Location = new System.Drawing.Point(100, 12);
            this.txtReturnNumber.Name = "txtReturnNumber";
            this.txtReturnNumber.ReadOnly = true;
            this.txtReturnNumber.Size = new System.Drawing.Size(120, 26);
            this.txtReturnNumber.TabIndex = 1;
            // 
            // lblReturnBarcode
            // 
            this.lblReturnBarcode.AutoSize = true;
            this.lblReturnBarcode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReturnBarcode.Location = new System.Drawing.Point(240, 15);
            this.lblReturnBarcode.Name = "lblReturnBarcode";
            this.lblReturnBarcode.Size = new System.Drawing.Size(87, 25);
            this.lblReturnBarcode.TabIndex = 2;
            this.lblReturnBarcode.Text = "Barcode:";
            // 
            // txtReturnBarcode
            // 
            this.txtReturnBarcode.Location = new System.Drawing.Point(320, 12);
            this.txtReturnBarcode.Name = "txtReturnBarcode";
            this.txtReturnBarcode.ReadOnly = true;
            this.txtReturnBarcode.Size = new System.Drawing.Size(150, 26);
            this.txtReturnBarcode.TabIndex = 3;
            // 
            // btnGenerateBarcode
            // 
            this.btnGenerateBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnGenerateBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateBarcode.ForeColor = System.Drawing.Color.White;
            this.btnGenerateBarcode.Location = new System.Drawing.Point(480, 10);
            this.btnGenerateBarcode.Name = "btnGenerateBarcode";
            this.btnGenerateBarcode.Size = new System.Drawing.Size(100, 30);
            this.btnGenerateBarcode.TabIndex = 4;
            this.btnGenerateBarcode.Text = "Generate";
            this.btnGenerateBarcode.UseVisualStyleBackColor = false;
            this.btnGenerateBarcode.Click += new System.EventHandler(this.BtnGenerateBarcode_Click);
            // 
            // pnlBarcodeInfo
            // 
            this.pnlBarcodeInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBarcodeInfo.Controls.Add(this.lblBarcodeTitle);
            this.pnlBarcodeInfo.Controls.Add(this.picBarcode);
            this.pnlBarcodeInfo.Controls.Add(this.lblBarcodeValue);
            this.pnlBarcodeInfo.Location = new System.Drawing.Point(640, 80);
            this.pnlBarcodeInfo.Name = "pnlBarcodeInfo";
            this.pnlBarcodeInfo.Size = new System.Drawing.Size(580, 150);
            this.pnlBarcodeInfo.TabIndex = 9;
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
            // lblBarcodeValue
            // 
            this.lblBarcodeValue.AutoSize = true;
            this.lblBarcodeValue.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblBarcodeValue.Location = new System.Drawing.Point(220, 55);
            this.lblBarcodeValue.Name = "lblBarcodeValue";
            this.lblBarcodeValue.Size = new System.Drawing.Size(0, 21);
            this.lblBarcodeValue.TabIndex = 2;
            this.lblBarcodeValue.Visible = false;
            // 
            // pnlInvoiceSelection
            // 
            this.pnlInvoiceSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInvoiceSelection.Controls.Add(this.btnLoadInvoice);
            this.pnlInvoiceSelection.Controls.Add(this.lblInvoiceNumber);
            this.pnlInvoiceSelection.Controls.Add(this.cmbInvoiceNumber);
            this.pnlInvoiceSelection.Location = new System.Drawing.Point(20, 190);
            this.pnlInvoiceSelection.Name = "pnlInvoiceSelection";
            this.pnlInvoiceSelection.Size = new System.Drawing.Size(600, 60);
            this.pnlInvoiceSelection.TabIndex = 2;
            // 
            // btnLoadInvoice
            // 
            this.btnLoadInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnLoadInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadInvoice.ForeColor = System.Drawing.Color.White;
            this.btnLoadInvoice.Location = new System.Drawing.Point(500, 15);
            this.btnLoadInvoice.Name = "btnLoadInvoice";
            this.btnLoadInvoice.Size = new System.Drawing.Size(80, 30);
            this.btnLoadInvoice.TabIndex = 2;
            this.btnLoadInvoice.Text = "Load";
            this.btnLoadInvoice.UseVisualStyleBackColor = false;
            this.btnLoadInvoice.Click += new System.EventHandler(this.BtnLoadInvoice_Click);
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.AutoSize = true;
            this.lblInvoiceNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceNumber.Location = new System.Drawing.Point(10, 20);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(95, 25);
            this.lblInvoiceNumber.TabIndex = 1;
            this.lblInvoiceNumber.Text = "Invoice #:";
            // 
            // cmbInvoiceNumber
            // 
            this.cmbInvoiceNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvoiceNumber.FormattingEnabled = true;
            this.cmbInvoiceNumber.Location = new System.Drawing.Point(140, 17);
            this.cmbInvoiceNumber.Name = "cmbInvoiceNumber";
            this.cmbInvoiceNumber.Size = new System.Drawing.Size(350, 28);
            this.cmbInvoiceNumber.TabIndex = 0;
            // 
            // pnlOriginalInvoiceDetails
            // 
            this.pnlOriginalInvoiceDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOriginalInvoiceDetails.Controls.Add(this.lblOriginalInvoiceTitle);
            this.pnlOriginalInvoiceDetails.Controls.Add(this.lblOriginalInvoiceNumber);
            this.pnlOriginalInvoiceDetails.Controls.Add(this.txtOriginalInvoiceNumber);
            this.pnlOriginalInvoiceDetails.Controls.Add(this.lblOriginalInvoiceDate);
            this.pnlOriginalInvoiceDetails.Controls.Add(this.txtOriginalInvoiceDate);
            this.pnlOriginalInvoiceDetails.Controls.Add(this.lblOriginalInvoiceTotal);
            this.pnlOriginalInvoiceDetails.Controls.Add(this.txtOriginalInvoiceTotal);
            this.pnlOriginalInvoiceDetails.Controls.Add(this.lblOriginalInvoiceStatus);
            this.pnlOriginalInvoiceDetails.Controls.Add(this.txtOriginalInvoiceStatus);
            this.pnlOriginalInvoiceDetails.Location = new System.Drawing.Point(640, 266);
            this.pnlOriginalInvoiceDetails.Name = "pnlOriginalInvoiceDetails";
            this.pnlOriginalInvoiceDetails.Size = new System.Drawing.Size(580, 100);
            this.pnlOriginalInvoiceDetails.TabIndex = 3;
            this.pnlOriginalInvoiceDetails.Visible = false;
            // 
            // lblOriginalInvoiceTitle
            // 
            this.lblOriginalInvoiceTitle.AutoSize = true;
            this.lblOriginalInvoiceTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOriginalInvoiceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblOriginalInvoiceTitle.Location = new System.Drawing.Point(10, 10);
            this.lblOriginalInvoiceTitle.Name = "lblOriginalInvoiceTitle";
            this.lblOriginalInvoiceTitle.Size = new System.Drawing.Size(234, 28);
            this.lblOriginalInvoiceTitle.TabIndex = 0;
            this.lblOriginalInvoiceTitle.Text = "Original Invoice Details";
            // 
            // lblOriginalInvoiceNumber
            // 
            this.lblOriginalInvoiceNumber.AutoSize = true;
            this.lblOriginalInvoiceNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOriginalInvoiceNumber.Location = new System.Drawing.Point(10, 40);
            this.lblOriginalInvoiceNumber.Name = "lblOriginalInvoiceNumber";
            this.lblOriginalInvoiceNumber.Size = new System.Drawing.Size(95, 25);
            this.lblOriginalInvoiceNumber.TabIndex = 1;
            this.lblOriginalInvoiceNumber.Text = "Invoice #:";
            // 
            // txtOriginalInvoiceNumber
            // 
            this.txtOriginalInvoiceNumber.Location = new System.Drawing.Point(100, 37);
            this.txtOriginalInvoiceNumber.Name = "txtOriginalInvoiceNumber";
            this.txtOriginalInvoiceNumber.ReadOnly = true;
            this.txtOriginalInvoiceNumber.Size = new System.Drawing.Size(120, 26);
            this.txtOriginalInvoiceNumber.TabIndex = 2;
            // 
            // lblOriginalInvoiceDate
            // 
            this.lblOriginalInvoiceDate.AutoSize = true;
            this.lblOriginalInvoiceDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOriginalInvoiceDate.Location = new System.Drawing.Point(240, 40);
            this.lblOriginalInvoiceDate.Name = "lblOriginalInvoiceDate";
            this.lblOriginalInvoiceDate.Size = new System.Drawing.Size(57, 25);
            this.lblOriginalInvoiceDate.TabIndex = 3;
            this.lblOriginalInvoiceDate.Text = "Date:";
            // 
            // txtOriginalInvoiceDate
            // 
            this.txtOriginalInvoiceDate.Location = new System.Drawing.Point(300, 37);
            this.txtOriginalInvoiceDate.Name = "txtOriginalInvoiceDate";
            this.txtOriginalInvoiceDate.ReadOnly = true;
            this.txtOriginalInvoiceDate.Size = new System.Drawing.Size(100, 26);
            this.txtOriginalInvoiceDate.TabIndex = 4;
            // 
            // lblOriginalInvoiceTotal
            // 
            this.lblOriginalInvoiceTotal.AutoSize = true;
            this.lblOriginalInvoiceTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOriginalInvoiceTotal.Location = new System.Drawing.Point(420, 40);
            this.lblOriginalInvoiceTotal.Name = "lblOriginalInvoiceTotal";
            this.lblOriginalInvoiceTotal.Size = new System.Drawing.Size(59, 25);
            this.lblOriginalInvoiceTotal.TabIndex = 5;
            this.lblOriginalInvoiceTotal.Text = "Total:";
            // 
            // txtOriginalInvoiceTotal
            // 
            this.txtOriginalInvoiceTotal.Location = new System.Drawing.Point(480, 37);
            this.txtOriginalInvoiceTotal.Name = "txtOriginalInvoiceTotal";
            this.txtOriginalInvoiceTotal.ReadOnly = true;
            this.txtOriginalInvoiceTotal.Size = new System.Drawing.Size(100, 26);
            this.txtOriginalInvoiceTotal.TabIndex = 6;
            // 
            // lblOriginalInvoiceStatus
            // 
            this.lblOriginalInvoiceStatus.AutoSize = true;
            this.lblOriginalInvoiceStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOriginalInvoiceStatus.Location = new System.Drawing.Point(10, 70);
            this.lblOriginalInvoiceStatus.Name = "lblOriginalInvoiceStatus";
            this.lblOriginalInvoiceStatus.Size = new System.Drawing.Size(70, 25);
            this.lblOriginalInvoiceStatus.TabIndex = 7;
            this.lblOriginalInvoiceStatus.Text = "Status:";
            // 
            // txtOriginalInvoiceStatus
            // 
            this.txtOriginalInvoiceStatus.Location = new System.Drawing.Point(80, 67);
            this.txtOriginalInvoiceStatus.Name = "txtOriginalInvoiceStatus";
            this.txtOriginalInvoiceStatus.ReadOnly = true;
            this.txtOriginalInvoiceStatus.Size = new System.Drawing.Size(100, 26);
            this.txtOriginalInvoiceStatus.TabIndex = 8;
            // 
            // pnlCustomerInfo
            // 
            this.pnlCustomerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomerInfo.Controls.Add(this.lblCustomerAddress);
            this.pnlCustomerInfo.Controls.Add(this.txtCustomerAddress);
            this.pnlCustomerInfo.Controls.Add(this.lblCustomerPhone);
            this.pnlCustomerInfo.Controls.Add(this.txtCustomerPhone);
            this.pnlCustomerInfo.Controls.Add(this.lblCustomerName);
            this.pnlCustomerInfo.Controls.Add(this.txtCustomerName);
            this.pnlCustomerInfo.Controls.Add(this.lblCustomer);
            this.pnlCustomerInfo.Controls.Add(this.cmbCustomer);
            this.pnlCustomerInfo.Location = new System.Drawing.Point(20, 266);
            this.pnlCustomerInfo.Name = "pnlCustomerInfo";
            this.pnlCustomerInfo.Size = new System.Drawing.Size(580, 118);
            this.pnlCustomerInfo.TabIndex = 4;
            // 
            // lblCustomerAddress
            // 
            this.lblCustomerAddress.AutoSize = true;
            this.lblCustomerAddress.Location = new System.Drawing.Point(248, 51);
            this.lblCustomerAddress.Name = "lblCustomerAddress";
            this.lblCustomerAddress.Size = new System.Drawing.Size(72, 20);
            this.lblCustomerAddress.TabIndex = 6;
            this.lblCustomerAddress.Text = "Address:";
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.Location = new System.Drawing.Point(333, 47);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.ReadOnly = true;
            this.txtCustomerAddress.Size = new System.Drawing.Size(228, 26);
            this.txtCustomerAddress.TabIndex = 7;
            // 
            // lblCustomerPhone
            // 
            this.lblCustomerPhone.AutoSize = true;
            this.lblCustomerPhone.Location = new System.Drawing.Point(10, 54);
            this.lblCustomerPhone.Name = "lblCustomerPhone";
            this.lblCustomerPhone.Size = new System.Drawing.Size(59, 20);
            this.lblCustomerPhone.TabIndex = 4;
            this.lblCustomerPhone.Text = "Phone:";
            // 
            // txtCustomerPhone
            // 
            this.txtCustomerPhone.Location = new System.Drawing.Point(70, 50);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.ReadOnly = true;
            this.txtCustomerPhone.Size = new System.Drawing.Size(120, 26);
            this.txtCustomerPhone.TabIndex = 5;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(315, 15);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(55, 20);
            this.lblCustomerName.TabIndex = 2;
            this.lblCustomerName.Text = "Name:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(385, 12);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(176, 26);
            this.txtCustomerName.TabIndex = 3;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(7, 15);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(82, 20);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Customer:";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(88, 12);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(200, 28);
            this.cmbCustomer.TabIndex = 1;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.CmbCustomer_SelectedIndexChanged);
            // 
            // pnlProductInfo
            // 
            this.pnlProductInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProductInfo.Controls.Add(this.btnAddItem);
            this.pnlProductInfo.Controls.Add(this.lblStock);
            this.pnlProductInfo.Controls.Add(this.lblUnitPrice);
            this.pnlProductInfo.Controls.Add(this.txtUnitPrice);
            this.pnlProductInfo.Controls.Add(this.lblQuantity);
            this.pnlProductInfo.Controls.Add(this.txtQuantity);
            this.pnlProductInfo.Controls.Add(this.lblProduct);
            this.pnlProductInfo.Controls.Add(this.cmbProduct);
            this.pnlProductInfo.Location = new System.Drawing.Point(20, 390);
            this.pnlProductInfo.Name = "pnlProductInfo";
            this.pnlProductInfo.Size = new System.Drawing.Size(600, 80);
            this.pnlProductInfo.TabIndex = 5;
            this.pnlProductInfo.Visible = false;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Location = new System.Drawing.Point(516, 46);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(80, 30);
            this.btnAddItem.TabIndex = 7;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = false;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(202, 45);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(54, 20);
            this.lblStock.TabIndex = 6;
            this.lblStock.Text = "Stock:";
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Location = new System.Drawing.Point(10, 45);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(81, 20);
            this.lblUnitPrice.TabIndex = 4;
            this.lblUnitPrice.Text = "Unit Price:";
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(92, 42);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(100, 26);
            this.txtUnitPrice.TabIndex = 5;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(290, 15);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(72, 20);
            this.lblQuantity.TabIndex = 2;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(369, 12);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(80, 26);
            this.txtQuantity.TabIndex = 3;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(10, 15);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(68, 20);
            this.lblProduct.TabIndex = 8;
            this.lblProduct.Text = "Product:";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(88, 12);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(200, 28);
            this.cmbProduct.TabIndex = 1;
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveItem.Location = new System.Drawing.Point(540, 10);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(80, 30);
            this.btnRemoveItem.TabIndex = 8;
            this.btnRemoveItem.Text = "Remove";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.BtnRemoveItem_Click);
            // 
            // pnlItems
            // 
            this.pnlItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlItems.Controls.Add(this.dgvItems);
            this.pnlItems.Location = new System.Drawing.Point(20, 390);
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
            this.colSelect,
            this.colProductCode,
            this.colProductName,
            this.colQuantity,
            this.colUnitPrice,
            this.colDiscount,
            this.colTax,
            this.colLineTotal});
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(0, 0);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersWidth = 62;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(1198, 198);
            this.dgvItems.TabIndex = 0;
            this.dgvItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellContentClick);
            // 
            // colSelect
            // 
            this.colSelect.HeaderText = "Select";
            this.colSelect.MinimumWidth = 8;
            this.colSelect.Name = "colSelect";
            this.colSelect.ReadOnly = true;
            this.colSelect.Width = 60;
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
            this.colProductName.Width = 200;
            // 
            // colQuantity
            // 
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.MinimumWidth = 8;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            this.colQuantity.Width = 80;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.HeaderText = "Unit Price";
            this.colUnitPrice.MinimumWidth = 8;
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.ReadOnly = true;
            this.colUnitPrice.Width = 150;
            // 
            // colDiscount
            // 
            this.colDiscount.HeaderText = "Discount";
            this.colDiscount.MinimumWidth = 8;
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            this.colDiscount.Width = 80;
            // 
            // colTax
            // 
            this.colTax.HeaderText = "Tax";
            this.colTax.MinimumWidth = 8;
            this.colTax.Name = "colTax";
            this.colTax.ReadOnly = true;
            this.colTax.Width = 80;
            // 
            // colLineTotal
            // 
            this.colLineTotal.HeaderText = "Line Total";
            this.colLineTotal.MinimumWidth = 8;
            this.colLineTotal.Name = "colLineTotal";
            this.colLineTotal.ReadOnly = true;
            this.colLineTotal.Width = 150;
            // 
            // pnlTotals
            // 
            this.pnlTotals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotals.Controls.Add(this.lblTotalAmount);
            this.pnlTotals.Controls.Add(this.txtTotalAmount);
            this.pnlTotals.Controls.Add(this.lblTaxAmount);
            this.pnlTotals.Controls.Add(this.txtTaxAmount);
            this.pnlTotals.Controls.Add(this.lblTaxableAmount);
            this.pnlTotals.Controls.Add(this.txtTaxableAmount);
            this.pnlTotals.Controls.Add(this.lblDiscountAmount);
            this.pnlTotals.Controls.Add(this.txtDiscountAmount);
            this.pnlTotals.Controls.Add(this.lblSubtotal);
            this.pnlTotals.Controls.Add(this.txtSubtotal);
            this.pnlTotals.Location = new System.Drawing.Point(20, 600);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(1200, 100);
            this.pnlTotals.TabIndex = 7;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.Location = new System.Drawing.Point(10, 135);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(146, 28);
            this.lblTotalAmount.TabIndex = 8;
            this.lblTotalAmount.Text = "Total Amount:";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtTotalAmount.Location = new System.Drawing.Point(200, 132);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(120, 34);
            this.txtTotalAmount.TabIndex = 9;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTaxAmount
            // 
            this.lblTaxAmount.AutoSize = true;
            this.lblTaxAmount.Location = new System.Drawing.Point(10, 105);
            this.lblTaxAmount.Name = "lblTaxAmount";
            this.lblTaxAmount.Size = new System.Drawing.Size(98, 20);
            this.lblTaxAmount.TabIndex = 6;
            this.lblTaxAmount.Text = "Tax Amount:";
            // 
            // txtTaxAmount
            // 
            this.txtTaxAmount.Location = new System.Drawing.Point(200, 102);
            this.txtTaxAmount.Name = "txtTaxAmount";
            this.txtTaxAmount.ReadOnly = true;
            this.txtTaxAmount.Size = new System.Drawing.Size(120, 26);
            this.txtTaxAmount.TabIndex = 7;
            this.txtTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTaxableAmount
            // 
            this.lblTaxableAmount.AutoSize = true;
            this.lblTaxableAmount.Location = new System.Drawing.Point(10, 75);
            this.lblTaxableAmount.Name = "lblTaxableAmount";
            this.lblTaxableAmount.Size = new System.Drawing.Size(128, 20);
            this.lblTaxableAmount.TabIndex = 4;
            this.lblTaxableAmount.Text = "Taxable Amount:";
            // 
            // txtTaxableAmount
            // 
            this.txtTaxableAmount.Location = new System.Drawing.Point(200, 72);
            this.txtTaxableAmount.Name = "txtTaxableAmount";
            this.txtTaxableAmount.ReadOnly = true;
            this.txtTaxableAmount.Size = new System.Drawing.Size(120, 26);
            this.txtTaxableAmount.TabIndex = 5;
            this.txtTaxableAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDiscountAmount
            // 
            this.lblDiscountAmount.AutoSize = true;
            this.lblDiscountAmount.Location = new System.Drawing.Point(10, 45);
            this.lblDiscountAmount.Name = "lblDiscountAmount";
            this.lblDiscountAmount.Size = new System.Drawing.Size(136, 20);
            this.lblDiscountAmount.TabIndex = 2;
            this.lblDiscountAmount.Text = "Discount Amount:";
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.Location = new System.Drawing.Point(200, 42);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.ReadOnly = true;
            this.txtDiscountAmount.Size = new System.Drawing.Size(120, 26);
            this.txtDiscountAmount.TabIndex = 3;
            this.txtDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(10, 15);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(73, 20);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Location = new System.Drawing.Point(200, 12);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(120, 26);
            this.txtSubtotal.TabIndex = 1;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pnlActions
            // 
            this.pnlActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActions.Controls.Add(this.cmbReturnReason);
            this.pnlActions.Controls.Add(this.lblReturnReason);
            this.pnlActions.Controls.Add(this.txtRemarks);
            this.pnlActions.Controls.Add(this.lblRemarks);
            this.pnlActions.Controls.Add(this.btnCancel);
            this.pnlActions.Controls.Add(this.btnSave);
            this.pnlActions.Location = new System.Drawing.Point(20, 710);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1200, 80);
            this.pnlActions.TabIndex = 8;
            // 
            // cmbReturnReason
            // 
            this.cmbReturnReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReturnReason.FormattingEnabled = true;
            this.cmbReturnReason.Location = new System.Drawing.Point(96, 45);
            this.cmbReturnReason.Name = "cmbReturnReason";
            this.cmbReturnReason.Size = new System.Drawing.Size(200, 28);
            this.cmbReturnReason.TabIndex = 5;
            // 
            // lblReturnReason
            // 
            this.lblReturnReason.AutoSize = true;
            this.lblReturnReason.Location = new System.Drawing.Point(10, 48);
            this.lblReturnReason.Name = "lblReturnReason";
            this.lblReturnReason.Size = new System.Drawing.Size(69, 20);
            this.lblReturnReason.TabIndex = 4;
            this.lblReturnReason.Text = "Reason:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(96, 12);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(200, 26);
            this.txtRemarks.TabIndex = 3;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(10, 15);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(77, 20);
            this.lblRemarks.TabIndex = 2;
            this.lblRemarks.Text = "Remarks:";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1140, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1000, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Return";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // SalesReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1300, 800);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlTotals);
            this.Controls.Add(this.pnlItems);
            this.Controls.Add(this.pnlProductInfo);
            this.Controls.Add(this.pnlCustomerInfo);
            this.Controls.Add(this.pnlInvoiceSelection);
            this.Controls.Add(this.pnlBarcodeInfo);
            this.Controls.Add(this.pnlReturnInfo);
            this.Controls.Add(this.pnlOriginalInvoiceDetails);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1300, 600);
            this.Name = "SalesReturnForm";
            this.Text = "Sales Return - POS System";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalesReturnForm_FormClosing);
            this.Load += new System.EventHandler(this.SalesReturnForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlReturnInfo.ResumeLayout(false);
            this.pnlReturnInfo.PerformLayout();
            this.pnlBarcodeInfo.ResumeLayout(false);
            this.pnlBarcodeInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.pnlInvoiceSelection.ResumeLayout(false);
            this.pnlInvoiceSelection.PerformLayout();
            this.pnlOriginalInvoiceDetails.ResumeLayout(false);
            this.pnlOriginalInvoiceDetails.PerformLayout();
            this.pnlCustomerInfo.ResumeLayout(false);
            this.pnlCustomerInfo.PerformLayout();
            this.pnlProductInfo.ResumeLayout(false);
            this.pnlProductInfo.PerformLayout();
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
        private System.Windows.Forms.Panel pnlReturnInfo;
        private System.Windows.Forms.Label lblReturnNumber;
        private System.Windows.Forms.TextBox txtReturnNumber;
        private System.Windows.Forms.Label lblReturnDate;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.Label lblCashier;
        private System.Windows.Forms.TextBox txtCashier;
        private System.Windows.Forms.Label lblTransactionTime;
        private System.Windows.Forms.TextBox txtTransactionTime;
        private System.Windows.Forms.Panel pnlBarcodeInfo;
        private System.Windows.Forms.Label lblBarcodeTitle;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.Label lblBarcodeValue;
        private System.Windows.Forms.Panel pnlInvoiceSelection;
        private System.Windows.Forms.Button btnLoadInvoice;
        private System.Windows.Forms.Label lblInvoiceNumber;
        private System.Windows.Forms.ComboBox cmbInvoiceNumber;
        private System.Windows.Forms.Panel pnlCustomerInfo;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblCustomerPhone;
        private System.Windows.Forms.TextBox txtCustomerPhone;
        private System.Windows.Forms.Label lblCustomerAddress;
        private System.Windows.Forms.TextBox txtCustomerAddress;
        private System.Windows.Forms.Panel pnlProductInfo;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Panel pnlItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLineTotal;
        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label lblDiscountAmount;
        private System.Windows.Forms.TextBox txtDiscountAmount;
        private System.Windows.Forms.Label lblTaxableAmount;
        private System.Windows.Forms.TextBox txtTaxableAmount;
        private System.Windows.Forms.Label lblTaxAmount;
        private System.Windows.Forms.TextBox txtTaxAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblReturnReason;
        private System.Windows.Forms.ComboBox cmbReturnReason;
        private System.Windows.Forms.Label lblReturnBarcode;
        private System.Windows.Forms.TextBox txtReturnBarcode;
        private System.Windows.Forms.Button btnGenerateBarcode;
        private System.Windows.Forms.Panel pnlOriginalInvoiceDetails;
        private System.Windows.Forms.Label lblOriginalInvoiceTitle;
        private System.Windows.Forms.Label lblOriginalInvoiceNumber;
        private System.Windows.Forms.TextBox txtOriginalInvoiceNumber;
        private System.Windows.Forms.Label lblOriginalInvoiceDate;
        private System.Windows.Forms.TextBox txtOriginalInvoiceDate;
        private System.Windows.Forms.Label lblOriginalInvoiceTotal;
        private System.Windows.Forms.TextBox txtOriginalInvoiceTotal;
        private System.Windows.Forms.Label lblOriginalInvoiceStatus;
        private System.Windows.Forms.TextBox txtOriginalInvoiceStatus;
    }
}


