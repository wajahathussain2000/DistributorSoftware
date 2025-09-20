namespace DistributionSoftware.Presentation.Forms
{
    partial class ReceiptVoucherForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlVoucherList = new System.Windows.Forms.Panel();
            this.lblVoucherList = new System.Windows.Forms.Label();
            this.dgvReceiptVouchers = new System.Windows.Forms.DataGridView();
            this.pnlVoucherDetails = new System.Windows.Forms.Panel();
            this.pnlVoucherInfo = new System.Windows.Forms.GroupBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.lblReference = new System.Windows.Forms.Label();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.lblNarration = new System.Windows.Forms.Label();
            this.dtpVoucherDate = new System.Windows.Forms.DateTimePicker();
            this.lblVoucherDate = new System.Windows.Forms.Label();
            this.txtVoucherNumber = new System.Windows.Forms.TextBox();
            this.lblVoucherNumber = new System.Windows.Forms.Label();
            this.pnlReceiptDetails = new System.Windows.Forms.GroupBox();
            this.txtReceiptReference = new System.Windows.Forms.TextBox();
            this.lblReceiptReference = new System.Windows.Forms.Label();
            this.txtMobileNumber = new System.Windows.Forms.TextBox();
            this.lblMobileNumber = new System.Windows.Forms.Label();
            this.dtpChequeDate = new System.Windows.Forms.DateTimePicker();
            this.lblChequeDate = new System.Windows.Forms.Label();
            this.txtChequeNumber = new System.Windows.Forms.TextBox();
            this.lblChequeNumber = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.lblBankName = new System.Windows.Forms.Label();
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.lblTransactionId = new System.Windows.Forms.Label();
            this.txtCardType = new System.Windows.Forms.TextBox();
            this.lblCardType = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.cmbBankAccount = new System.Windows.Forms.ComboBox();
            this.lblBankAccount = new System.Windows.Forms.Label();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.lblAmount = new System.Windows.Forms.Label();
            this.cmbReceiptMode = new System.Windows.Forms.ComboBox();
            this.lblReceiptMode = new System.Windows.Forms.Label();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlVoucherList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceiptVouchers)).BeginInit();
            this.pnlVoucherDetails.SuspendLayout();
            this.pnlVoucherInfo.SuspendLayout();
            this.pnlReceiptDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(155, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Receipt Vouchers";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1150, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "×";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer1);
            this.pnlMain.Controls.Add(this.pnlActions);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 50);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 650);
            this.pnlMain.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlVoucherList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlVoucherDetails);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 600);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlVoucherList
            // 
            this.pnlVoucherList.Controls.Add(this.lblVoucherList);
            this.pnlVoucherList.Controls.Add(this.dgvReceiptVouchers);
            this.pnlVoucherList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVoucherList.Location = new System.Drawing.Point(0, 0);
            this.pnlVoucherList.Name = "pnlVoucherList";
            this.pnlVoucherList.Padding = new System.Windows.Forms.Padding(10);
            this.pnlVoucherList.Size = new System.Drawing.Size(1200, 200);
            this.pnlVoucherList.TabIndex = 0;
            // 
            // lblVoucherList
            // 
            this.lblVoucherList.AutoSize = true;
            this.lblVoucherList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblVoucherList.Location = new System.Drawing.Point(10, 10);
            this.lblVoucherList.Name = "lblVoucherList";
            this.lblVoucherList.Size = new System.Drawing.Size(120, 21);
            this.lblVoucherList.TabIndex = 0;
            this.lblVoucherList.Text = "Receipt Vouchers";
            // 
            // dgvReceiptVouchers
            // 
            this.dgvReceiptVouchers.AllowUserToAddRows = false;
            this.dgvReceiptVouchers.AllowUserToDeleteRows = false;
            this.dgvReceiptVouchers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReceiptVouchers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReceiptVouchers.BackgroundColor = System.Drawing.Color.White;
            this.dgvReceiptVouchers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceiptVouchers.Location = new System.Drawing.Point(10, 40);
            this.dgvReceiptVouchers.MultiSelect = false;
            this.dgvReceiptVouchers.Name = "dgvReceiptVouchers";
            this.dgvReceiptVouchers.ReadOnly = true;
            this.dgvReceiptVouchers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReceiptVouchers.Size = new System.Drawing.Size(1180, 150);
            this.dgvReceiptVouchers.TabIndex = 1;
            this.dgvReceiptVouchers.SelectionChanged += new System.EventHandler(this.DgvReceiptVouchers_SelectionChanged);
            // 
            // pnlVoucherDetails
            // 
            this.pnlVoucherDetails.Controls.Add(this.pnlVoucherInfo);
            this.pnlVoucherDetails.Controls.Add(this.pnlReceiptDetails);
            this.pnlVoucherDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVoucherDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlVoucherDetails.Name = "pnlVoucherDetails";
            this.pnlVoucherDetails.Padding = new System.Windows.Forms.Padding(10);
            this.pnlVoucherDetails.Size = new System.Drawing.Size(1200, 396);
            this.pnlVoucherDetails.TabIndex = 0;
            // 
            // pnlVoucherInfo
            // 
            this.pnlVoucherInfo.Controls.Add(this.txtRemarks);
            this.pnlVoucherInfo.Controls.Add(this.lblRemarks);
            this.pnlVoucherInfo.Controls.Add(this.txtReference);
            this.pnlVoucherInfo.Controls.Add(this.lblReference);
            this.pnlVoucherInfo.Controls.Add(this.txtNarration);
            this.pnlVoucherInfo.Controls.Add(this.lblNarration);
            this.pnlVoucherInfo.Controls.Add(this.dtpVoucherDate);
            this.pnlVoucherInfo.Controls.Add(this.lblVoucherDate);
            this.pnlVoucherInfo.Controls.Add(this.txtVoucherNumber);
            this.pnlVoucherInfo.Controls.Add(this.lblVoucherNumber);
            this.pnlVoucherInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVoucherInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.pnlVoucherInfo.Location = new System.Drawing.Point(10, 10);
            this.pnlVoucherInfo.Name = "pnlVoucherInfo";
            this.pnlVoucherInfo.Size = new System.Drawing.Size(1180, 120);
            this.pnlVoucherInfo.TabIndex = 0;
            this.pnlVoucherInfo.TabStop = false;
            this.pnlVoucherInfo.Text = "Voucher Information";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(600, 80);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(300, 25);
            this.txtRemarks.TabIndex = 9;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(500, 83);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(60, 19);
            this.lblRemarks.TabIndex = 8;
            this.lblRemarks.Text = "Remarks:";
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(100, 80);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(300, 25);
            this.txtReference.TabIndex = 7;
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(15, 83);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(70, 19);
            this.lblReference.TabIndex = 6;
            this.lblReference.Text = "Reference:";
            // 
            // txtNarration
            // 
            this.txtNarration.Location = new System.Drawing.Point(100, 50);
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(800, 25);
            this.txtNarration.TabIndex = 5;
            // 
            // lblNarration
            // 
            this.lblNarration.AutoSize = true;
            this.lblNarration.Location = new System.Drawing.Point(15, 53);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Size = new System.Drawing.Size(70, 19);
            this.lblNarration.TabIndex = 4;
            this.lblNarration.Text = "Narration:";
            // 
            // dtpVoucherDate
            // 
            this.dtpVoucherDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVoucherDate.Location = new System.Drawing.Point(600, 20);
            this.dtpVoucherDate.Name = "dtpVoucherDate";
            this.dtpVoucherDate.Size = new System.Drawing.Size(150, 25);
            this.dtpVoucherDate.TabIndex = 3;
            // 
            // lblVoucherDate
            // 
            this.lblVoucherDate.AutoSize = true;
            this.lblVoucherDate.Location = new System.Drawing.Point(500, 23);
            this.lblVoucherDate.Name = "lblVoucherDate";
            this.lblVoucherDate.Size = new System.Drawing.Size(95, 19);
            this.lblVoucherDate.TabIndex = 2;
            this.lblVoucherDate.Text = "Voucher Date:";
            // 
            // txtVoucherNumber
            // 
            this.txtVoucherNumber.Location = new System.Drawing.Point(100, 20);
            this.txtVoucherNumber.Name = "txtVoucherNumber";
            this.txtVoucherNumber.ReadOnly = true;
            this.txtVoucherNumber.Size = new System.Drawing.Size(300, 25);
            this.txtVoucherNumber.TabIndex = 1;
            // 
            // lblVoucherNumber
            // 
            this.lblVoucherNumber.AutoSize = true;
            this.lblVoucherNumber.Location = new System.Drawing.Point(15, 23);
            this.lblVoucherNumber.Name = "lblVoucherNumber";
            this.lblVoucherNumber.Size = new System.Drawing.Size(115, 19);
            this.lblVoucherNumber.TabIndex = 0;
            this.lblVoucherNumber.Text = "Voucher Number:";
            // 
            // pnlReceiptDetails
            // 
            this.pnlReceiptDetails.Controls.Add(this.txtReceiptReference);
            this.pnlReceiptDetails.Controls.Add(this.lblReceiptReference);
            this.pnlReceiptDetails.Controls.Add(this.txtMobileNumber);
            this.pnlReceiptDetails.Controls.Add(this.lblMobileNumber);
            this.pnlReceiptDetails.Controls.Add(this.dtpChequeDate);
            this.pnlReceiptDetails.Controls.Add(this.lblChequeDate);
            this.pnlReceiptDetails.Controls.Add(this.txtChequeNumber);
            this.pnlReceiptDetails.Controls.Add(this.lblChequeNumber);
            this.pnlReceiptDetails.Controls.Add(this.txtBankName);
            this.pnlReceiptDetails.Controls.Add(this.lblBankName);
            this.pnlReceiptDetails.Controls.Add(this.txtTransactionId);
            this.pnlReceiptDetails.Controls.Add(this.lblTransactionId);
            this.pnlReceiptDetails.Controls.Add(this.txtCardType);
            this.pnlReceiptDetails.Controls.Add(this.lblCardType);
            this.pnlReceiptDetails.Controls.Add(this.txtCardNumber);
            this.pnlReceiptDetails.Controls.Add(this.lblCardNumber);
            this.pnlReceiptDetails.Controls.Add(this.cmbBankAccount);
            this.pnlReceiptDetails.Controls.Add(this.lblBankAccount);
            this.pnlReceiptDetails.Controls.Add(this.numAmount);
            this.pnlReceiptDetails.Controls.Add(this.lblAmount);
            this.pnlReceiptDetails.Controls.Add(this.cmbReceiptMode);
            this.pnlReceiptDetails.Controls.Add(this.lblReceiptMode);
            this.pnlReceiptDetails.Controls.Add(this.cmbAccount);
            this.pnlReceiptDetails.Controls.Add(this.lblAccount);
            this.pnlReceiptDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReceiptDetails.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.pnlReceiptDetails.Location = new System.Drawing.Point(10, 130);
            this.pnlReceiptDetails.Name = "pnlReceiptDetails";
            this.pnlReceiptDetails.Size = new System.Drawing.Size(1180, 256);
            this.pnlReceiptDetails.TabIndex = 1;
            this.pnlReceiptDetails.TabStop = false;
            this.pnlReceiptDetails.Text = "Receipt Details";
            // 
            // txtReceiptReference
            // 
            this.txtReceiptReference.Location = new System.Drawing.Point(600, 200);
            this.txtReceiptReference.Name = "txtReceiptReference";
            this.txtReceiptReference.Size = new System.Drawing.Size(300, 25);
            this.txtReceiptReference.TabIndex = 23;
            // 
            // lblReceiptReference
            // 
            this.lblReceiptReference.AutoSize = true;
            this.lblReceiptReference.Location = new System.Drawing.Point(500, 203);
            this.lblReceiptReference.Name = "lblReceiptReference";
            this.lblReceiptReference.Size = new System.Drawing.Size(125, 19);
            this.lblReceiptReference.TabIndex = 22;
            this.lblReceiptReference.Text = "Receipt Reference:";
            // 
            // txtMobileNumber
            // 
            this.txtMobileNumber.Location = new System.Drawing.Point(100, 200);
            this.txtMobileNumber.Name = "txtMobileNumber";
            this.txtMobileNumber.Size = new System.Drawing.Size(300, 25);
            this.txtMobileNumber.TabIndex = 21;
            // 
            // lblMobileNumber
            // 
            this.lblMobileNumber.AutoSize = true;
            this.lblMobileNumber.Location = new System.Drawing.Point(15, 203);
            this.lblMobileNumber.Name = "lblMobileNumber";
            this.lblMobileNumber.Size = new System.Drawing.Size(110, 19);
            this.lblMobileNumber.TabIndex = 20;
            this.lblMobileNumber.Text = "Mobile Number:";
            // 
            // dtpChequeDate
            // 
            this.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpChequeDate.Location = new System.Drawing.Point(600, 170);
            this.dtpChequeDate.Name = "dtpChequeDate";
            this.dtpChequeDate.Size = new System.Drawing.Size(150, 25);
            this.dtpChequeDate.TabIndex = 19;
            // 
            // lblChequeDate
            // 
            this.lblChequeDate.AutoSize = true;
            this.lblChequeDate.Location = new System.Drawing.Point(500, 173);
            this.lblChequeDate.Name = "lblChequeDate";
            this.lblChequeDate.Size = new System.Drawing.Size(90, 19);
            this.lblChequeDate.TabIndex = 18;
            this.lblChequeDate.Text = "Cheque Date:";
            // 
            // txtChequeNumber
            // 
            this.txtChequeNumber.Location = new System.Drawing.Point(100, 170);
            this.txtChequeNumber.Name = "txtChequeNumber";
            this.txtChequeNumber.Size = new System.Drawing.Size(300, 25);
            this.txtChequeNumber.TabIndex = 17;
            // 
            // lblChequeNumber
            // 
            this.lblChequeNumber.AutoSize = true;
            this.lblChequeNumber.Location = new System.Drawing.Point(15, 173);
            this.lblChequeNumber.Name = "lblChequeNumber";
            this.lblChequeNumber.Size = new System.Drawing.Size(110, 19);
            this.lblChequeNumber.TabIndex = 16;
            this.lblChequeNumber.Text = "Cheque Number:";
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(600, 140);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(300, 25);
            this.txtBankName.TabIndex = 15;
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(500, 143);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(85, 19);
            this.lblBankName.TabIndex = 14;
            this.lblBankName.Text = "Bank Name:";
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.Location = new System.Drawing.Point(100, 140);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(300, 25);
            this.txtTransactionId.TabIndex = 13;
            // 
            // lblTransactionId
            // 
            this.lblTransactionId.AutoSize = true;
            this.lblTransactionId.Location = new System.Drawing.Point(15, 143);
            this.lblTransactionId.Name = "lblTransactionId";
            this.lblTransactionId.Size = new System.Drawing.Size(100, 19);
            this.lblTransactionId.TabIndex = 12;
            this.lblTransactionId.Text = "Transaction ID:";
            // 
            // txtCardType
            // 
            this.txtCardType.Location = new System.Drawing.Point(600, 110);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.Size = new System.Drawing.Size(300, 25);
            this.txtCardType.TabIndex = 11;
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Location = new System.Drawing.Point(500, 113);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(75, 19);
            this.lblCardType.TabIndex = 10;
            this.lblCardType.Text = "Card Type:";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(100, 110);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(300, 25);
            this.txtCardNumber.TabIndex = 9;
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Location = new System.Drawing.Point(15, 113);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(90, 19);
            this.lblCardNumber.TabIndex = 8;
            this.lblCardNumber.Text = "Card Number:";
            // 
            // cmbBankAccount
            // 
            this.cmbBankAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBankAccount.FormattingEnabled = true;
            this.cmbBankAccount.Location = new System.Drawing.Point(600, 80);
            this.cmbBankAccount.Name = "cmbBankAccount";
            this.cmbBankAccount.Size = new System.Drawing.Size(300, 25);
            this.cmbBankAccount.TabIndex = 7;
            // 
            // lblBankAccount
            // 
            this.lblBankAccount.AutoSize = true;
            this.lblBankAccount.Location = new System.Drawing.Point(500, 83);
            this.lblBankAccount.Name = "lblBankAccount";
            this.lblBankAccount.Size = new System.Drawing.Size(95, 19);
            this.lblBankAccount.TabIndex = 6;
            this.lblBankAccount.Text = "Bank Account:";
            // 
            // numAmount
            // 
            this.numAmount.DecimalPlaces = 2;
            this.numAmount.Location = new System.Drawing.Point(100, 80);
            this.numAmount.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(300, 25);
            this.numAmount.TabIndex = 5;
            this.numAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(15, 83);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(60, 19);
            this.lblAmount.TabIndex = 4;
            this.lblAmount.Text = "Amount:";
            // 
            // cmbReceiptMode
            // 
            this.cmbReceiptMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReceiptMode.FormattingEnabled = true;
            this.cmbReceiptMode.Location = new System.Drawing.Point(100, 50);
            this.cmbReceiptMode.Name = "cmbReceiptMode";
            this.cmbReceiptMode.Size = new System.Drawing.Size(300, 25);
            this.cmbReceiptMode.TabIndex = 3;
            this.cmbReceiptMode.SelectedIndexChanged += new System.EventHandler(this.CmbReceiptMode_SelectedIndexChanged);
            // 
            // lblReceiptMode
            // 
            this.lblReceiptMode.AutoSize = true;
            this.lblReceiptMode.Location = new System.Drawing.Point(15, 53);
            this.lblReceiptMode.Name = "lblReceiptMode";
            this.lblReceiptMode.Size = new System.Drawing.Size(95, 19);
            this.lblReceiptMode.TabIndex = 2;
            this.lblReceiptMode.Text = "Receipt Mode:";
            // 
            // cmbAccount
            // 
            this.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(100, 20);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(800, 25);
            this.cmbAccount.TabIndex = 1;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Location = new System.Drawing.Point(15, 23);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(60, 19);
            this.lblAccount.TabIndex = 0;
            this.lblAccount.Text = "Account:";
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnDelete);
            this.pnlActions.Controls.Add(this.btnEdit);
            this.pnlActions.Controls.Add(this.btnNew);
            this.pnlActions.Controls.Add(this.btnCancel);
            this.pnlActions.Controls.Add(this.btnSave);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 600);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1200, 50);
            this.pnlActions.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(300, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(210, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 30);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(30, 10);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 30);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(120, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(120, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // ReceiptVoucherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReceiptVoucherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Receipt Vouchers";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlVoucherList.ResumeLayout(false);
            this.pnlVoucherList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceiptVouchers)).EndInit();
            this.pnlVoucherDetails.ResumeLayout(false);
            this.pnlVoucherInfo.ResumeLayout(false);
            this.pnlVoucherInfo.PerformLayout();
            this.pnlReceiptDetails.ResumeLayout(false);
            this.pnlReceiptDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlVoucherList;
        private System.Windows.Forms.Label lblVoucherList;
        private System.Windows.Forms.DataGridView dgvReceiptVouchers;
        private System.Windows.Forms.Panel pnlVoucherDetails;
        private System.Windows.Forms.GroupBox pnlVoucherInfo;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.Label lblNarration;
        private System.Windows.Forms.DateTimePicker dtpVoucherDate;
        private System.Windows.Forms.Label lblVoucherDate;
        private System.Windows.Forms.TextBox txtVoucherNumber;
        private System.Windows.Forms.Label lblVoucherNumber;
        private System.Windows.Forms.GroupBox pnlReceiptDetails;
        private System.Windows.Forms.TextBox txtReceiptReference;
        private System.Windows.Forms.Label lblReceiptReference;
        private System.Windows.Forms.TextBox txtMobileNumber;
        private System.Windows.Forms.Label lblMobileNumber;
        private System.Windows.Forms.DateTimePicker dtpChequeDate;
        private System.Windows.Forms.Label lblChequeDate;
        private System.Windows.Forms.TextBox txtChequeNumber;
        private System.Windows.Forms.Label lblChequeNumber;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.TextBox txtTransactionId;
        private System.Windows.Forms.Label lblTransactionId;
        private System.Windows.Forms.TextBox txtCardType;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.ComboBox cmbBankAccount;
        private System.Windows.Forms.Label lblBankAccount;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.ComboBox cmbReceiptMode;
        private System.Windows.Forms.Label lblReceiptMode;
        private System.Windows.Forms.ComboBox cmbAccount;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}

