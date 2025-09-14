namespace DistributionSoftware.Presentation.Forms
{
    partial class JournalVoucherForm
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
            this.dgvJournalVouchers = new System.Windows.Forms.DataGridView();
            this.pnlVoucherDetails = new System.Windows.Forms.Panel();
            this.pnlVoucherInfo = new System.Windows.Forms.GroupBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.lblReference = new System.Windows.Forms.Label();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.lblNarration = new System.Windows.Forms.Label();
            this.dtpVoucherDate = new System.Windows.Forms.DateTimePicker();
            this.lblVoucherDate = new System.Windows.Forms.Label();
            this.txtVoucherNumber = new System.Windows.Forms.TextBox();
            this.lblVoucherNumber = new System.Windows.Forms.Label();
            this.pnlDetails = new System.Windows.Forms.GroupBox();
            this.lblDifference = new System.Windows.Forms.Label();
            this.lblTotalCredit = new System.Windows.Forms.Label();
            this.lblTotalDebit = new System.Windows.Forms.Label();
            this.btnRemoveDetail = new System.Windows.Forms.Button();
            this.btnAddDetail = new System.Windows.Forms.Button();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.pnlDetailForm = new System.Windows.Forms.GroupBox();
            this.txtDetailNarration = new System.Windows.Forms.TextBox();
            this.lblDetailNarration = new System.Windows.Forms.Label();
            this.numCreditAmount = new System.Windows.Forms.NumericUpDown();
            this.lblCreditAmount = new System.Windows.Forms.Label();
            this.numDebitAmount = new System.Windows.Forms.NumericUpDown();
            this.lblDebitAmount = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournalVouchers)).BeginInit();
            this.pnlVoucherDetails.SuspendLayout();
            this.pnlVoucherInfo.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.pnlDetailForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCreditAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDebitAmount)).BeginInit();
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
            this.lblTitle.Text = "Journal Vouchers";
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
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlVoucherList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlVoucherDetails);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 650);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlVoucherList
            // 
            this.pnlVoucherList.Controls.Add(this.lblVoucherList);
            this.pnlVoucherList.Controls.Add(this.dgvJournalVouchers);
            this.pnlVoucherList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVoucherList.Location = new System.Drawing.Point(0, 0);
            this.pnlVoucherList.Name = "pnlVoucherList";
            this.pnlVoucherList.Padding = new System.Windows.Forms.Padding(10);
            this.pnlVoucherList.Size = new System.Drawing.Size(400, 650);
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
            this.lblVoucherList.Text = "Journal Vouchers";
            // 
            // dgvJournalVouchers
            // 
            this.dgvJournalVouchers.AllowUserToAddRows = false;
            this.dgvJournalVouchers.AllowUserToDeleteRows = false;
            this.dgvJournalVouchers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJournalVouchers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJournalVouchers.Location = new System.Drawing.Point(10, 40);
            this.dgvJournalVouchers.Name = "dgvJournalVouchers";
            this.dgvJournalVouchers.ReadOnly = true;
            this.dgvJournalVouchers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJournalVouchers.Size = new System.Drawing.Size(380, 600);
            this.dgvJournalVouchers.TabIndex = 1;
            this.dgvJournalVouchers.SelectionChanged += new System.EventHandler(this.DgvJournalVouchers_SelectionChanged);
            // 
            // pnlVoucherDetails
            // 
            this.pnlVoucherDetails.Controls.Add(this.pnlVoucherInfo);
            this.pnlVoucherDetails.Controls.Add(this.pnlDetails);
            this.pnlVoucherDetails.Controls.Add(this.pnlDetailForm);
            this.pnlVoucherDetails.Controls.Add(this.pnlActions);
            this.pnlVoucherDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVoucherDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlVoucherDetails.Name = "pnlVoucherDetails";
            this.pnlVoucherDetails.Padding = new System.Windows.Forms.Padding(10);
            this.pnlVoucherDetails.Size = new System.Drawing.Size(796, 650);
            this.pnlVoucherDetails.TabIndex = 0;
            // 
            // pnlVoucherInfo
            // 
            this.pnlVoucherInfo.Controls.Add(this.txtReference);
            this.pnlVoucherInfo.Controls.Add(this.lblReference);
            this.pnlVoucherInfo.Controls.Add(this.txtNarration);
            this.pnlVoucherInfo.Controls.Add(this.lblNarration);
            this.pnlVoucherInfo.Controls.Add(this.dtpVoucherDate);
            this.pnlVoucherInfo.Controls.Add(this.lblVoucherDate);
            this.pnlVoucherInfo.Controls.Add(this.txtVoucherNumber);
            this.pnlVoucherInfo.Controls.Add(this.lblVoucherNumber);
            this.pnlVoucherInfo.Location = new System.Drawing.Point(10, 10);
            this.pnlVoucherInfo.Name = "pnlVoucherInfo";
            this.pnlVoucherInfo.Size = new System.Drawing.Size(776, 120);
            this.pnlVoucherInfo.TabIndex = 0;
            this.pnlVoucherInfo.TabStop = false;
            this.pnlVoucherInfo.Text = "Voucher Information";
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(400, 80);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(200, 25);
            this.txtReference.TabIndex = 7;
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(300, 83);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(65, 17);
            this.lblReference.TabIndex = 6;
            this.lblReference.Text = "Reference:";
            // 
            // txtNarration
            // 
            this.txtNarration.Location = new System.Drawing.Point(100, 80);
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(200, 25);
            this.txtNarration.TabIndex = 5;
            // 
            // lblNarration
            // 
            this.lblNarration.AutoSize = true;
            this.lblNarration.Location = new System.Drawing.Point(20, 83);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Size = new System.Drawing.Size(65, 17);
            this.lblNarration.TabIndex = 4;
            this.lblNarration.Text = "Narration:";
            // 
            // dtpVoucherDate
            // 
            this.dtpVoucherDate.Location = new System.Drawing.Point(400, 40);
            this.dtpVoucherDate.Name = "dtpVoucherDate";
            this.dtpVoucherDate.Size = new System.Drawing.Size(200, 25);
            this.dtpVoucherDate.TabIndex = 3;
            // 
            // lblVoucherDate
            // 
            this.lblVoucherDate.AutoSize = true;
            this.lblVoucherDate.Location = new System.Drawing.Point(300, 43);
            this.lblVoucherDate.Name = "lblVoucherDate";
            this.lblVoucherDate.Size = new System.Drawing.Size(85, 17);
            this.lblVoucherDate.TabIndex = 2;
            this.lblVoucherDate.Text = "Voucher Date:";
            // 
            // txtVoucherNumber
            // 
            this.txtVoucherNumber.Location = new System.Drawing.Point(100, 40);
            this.txtVoucherNumber.Name = "txtVoucherNumber";
            this.txtVoucherNumber.Size = new System.Drawing.Size(200, 25);
            this.txtVoucherNumber.TabIndex = 1;
            // 
            // lblVoucherNumber
            // 
            this.lblVoucherNumber.AutoSize = true;
            this.lblVoucherNumber.Location = new System.Drawing.Point(20, 43);
            this.lblVoucherNumber.Name = "lblVoucherNumber";
            this.lblVoucherNumber.Size = new System.Drawing.Size(105, 17);
            this.lblVoucherNumber.TabIndex = 0;
            this.lblVoucherNumber.Text = "Voucher Number:";
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.lblDifference);
            this.pnlDetails.Controls.Add(this.lblTotalCredit);
            this.pnlDetails.Controls.Add(this.lblTotalDebit);
            this.pnlDetails.Controls.Add(this.btnRemoveDetail);
            this.pnlDetails.Controls.Add(this.btnAddDetail);
            this.pnlDetails.Controls.Add(this.dgvDetails);
            this.pnlDetails.Location = new System.Drawing.Point(10, 140);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(776, 300);
            this.pnlDetails.TabIndex = 1;
            this.pnlDetails.TabStop = false;
            this.pnlDetails.Text = "Journal Voucher Details";
            // 
            // lblDifference
            // 
            this.lblDifference.AutoSize = true;
            this.lblDifference.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDifference.Location = new System.Drawing.Point(400, 270);
            this.lblDifference.Name = "lblDifference";
            this.lblDifference.Size = new System.Drawing.Size(85, 19);
            this.lblDifference.TabIndex = 5;
            this.lblDifference.Text = "Difference:";
            // 
            // lblTotalCredit
            // 
            this.lblTotalCredit.AutoSize = true;
            this.lblTotalCredit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalCredit.Location = new System.Drawing.Point(200, 270);
            this.lblTotalCredit.Name = "lblTotalCredit";
            this.lblTotalCredit.Size = new System.Drawing.Size(90, 19);
            this.lblTotalCredit.TabIndex = 4;
            this.lblTotalCredit.Text = "Total Credit:";
            // 
            // lblTotalDebit
            // 
            this.lblTotalDebit.AutoSize = true;
            this.lblTotalDebit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalDebit.Location = new System.Drawing.Point(20, 270);
            this.lblTotalDebit.Name = "lblTotalDebit";
            this.lblTotalDebit.Size = new System.Drawing.Size(85, 19);
            this.lblTotalDebit.TabIndex = 3;
            this.lblTotalDebit.Text = "Total Debit:";
            // 
            // btnRemoveDetail
            // 
            this.btnRemoveDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRemoveDetail.FlatAppearance.BorderSize = 0;
            this.btnRemoveDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveDetail.ForeColor = System.Drawing.Color.White;
            this.btnRemoveDetail.Location = new System.Drawing.Point(600, 240);
            this.btnRemoveDetail.Name = "btnRemoveDetail";
            this.btnRemoveDetail.Size = new System.Drawing.Size(80, 30);
            this.btnRemoveDetail.TabIndex = 2;
            this.btnRemoveDetail.Text = "Remove";
            this.btnRemoveDetail.UseVisualStyleBackColor = false;
            this.btnRemoveDetail.Click += new System.EventHandler(this.BtnRemoveDetail_Click);
            // 
            // btnAddDetail
            // 
            this.btnAddDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddDetail.FlatAppearance.BorderSize = 0;
            this.btnAddDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDetail.ForeColor = System.Drawing.Color.White;
            this.btnAddDetail.Location = new System.Drawing.Point(500, 240);
            this.btnAddDetail.Name = "btnAddDetail";
            this.btnAddDetail.Size = new System.Drawing.Size(80, 30);
            this.btnAddDetail.TabIndex = 1;
            this.btnAddDetail.Text = "Add Detail";
            this.btnAddDetail.UseVisualStyleBackColor = false;
            this.btnAddDetail.Click += new System.EventHandler(this.BtnAddDetail_Click);
            // 
            // dgvDetails
            // 
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Location = new System.Drawing.Point(20, 30);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetails.Size = new System.Drawing.Size(740, 200);
            this.dgvDetails.TabIndex = 0;
            // 
            // pnlDetailForm
            // 
            this.pnlDetailForm.Controls.Add(this.txtDetailNarration);
            this.pnlDetailForm.Controls.Add(this.lblDetailNarration);
            this.pnlDetailForm.Controls.Add(this.numCreditAmount);
            this.pnlDetailForm.Controls.Add(this.lblCreditAmount);
            this.pnlDetailForm.Controls.Add(this.numDebitAmount);
            this.pnlDetailForm.Controls.Add(this.lblDebitAmount);
            this.pnlDetailForm.Controls.Add(this.cmbAccount);
            this.pnlDetailForm.Controls.Add(this.lblAccount);
            this.pnlDetailForm.Location = new System.Drawing.Point(10, 450);
            this.pnlDetailForm.Name = "pnlDetailForm";
            this.pnlDetailForm.Size = new System.Drawing.Size(776, 100);
            this.pnlDetailForm.TabIndex = 2;
            this.pnlDetailForm.TabStop = false;
            this.pnlDetailForm.Text = "Add Detail";
            // 
            // txtDetailNarration
            // 
            this.txtDetailNarration.Location = new System.Drawing.Point(500, 60);
            this.txtDetailNarration.Name = "txtDetailNarration";
            this.txtDetailNarration.Size = new System.Drawing.Size(200, 25);
            this.txtDetailNarration.TabIndex = 7;
            // 
            // lblDetailNarration
            // 
            this.lblDetailNarration.AutoSize = true;
            this.lblDetailNarration.Location = new System.Drawing.Point(400, 63);
            this.lblDetailNarration.Name = "lblDetailNarration";
            this.lblDetailNarration.Size = new System.Drawing.Size(65, 17);
            this.lblDetailNarration.TabIndex = 6;
            this.lblDetailNarration.Text = "Narration:";
            // 
            // numCreditAmount
            // 
            this.numCreditAmount.DecimalPlaces = 2;
            this.numCreditAmount.Location = new System.Drawing.Point(300, 60);
            this.numCreditAmount.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numCreditAmount.Name = "numCreditAmount";
            this.numCreditAmount.Size = new System.Drawing.Size(80, 25);
            this.numCreditAmount.TabIndex = 5;
            // 
            // lblCreditAmount
            // 
            this.lblCreditAmount.AutoSize = true;
            this.lblCreditAmount.Location = new System.Drawing.Point(200, 63);
            this.lblCreditAmount.Name = "lblCreditAmount";
            this.lblCreditAmount.Size = new System.Drawing.Size(95, 17);
            this.lblCreditAmount.TabIndex = 4;
            this.lblCreditAmount.Text = "Credit Amount:";
            // 
            // numDebitAmount
            // 
            this.numDebitAmount.DecimalPlaces = 2;
            this.numDebitAmount.Location = new System.Drawing.Point(100, 60);
            this.numDebitAmount.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numDebitAmount.Name = "numDebitAmount";
            this.numDebitAmount.Size = new System.Drawing.Size(80, 25);
            this.numDebitAmount.TabIndex = 3;
            // 
            // lblDebitAmount
            // 
            this.lblDebitAmount.AutoSize = true;
            this.lblDebitAmount.Location = new System.Drawing.Point(20, 63);
            this.lblDebitAmount.Name = "lblDebitAmount";
            this.lblDebitAmount.Size = new System.Drawing.Size(85, 17);
            this.lblDebitAmount.TabIndex = 2;
            this.lblDebitAmount.Text = "Debit Amount:";
            // 
            // cmbAccount
            // 
            this.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(100, 30);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(300, 25);
            this.cmbAccount.TabIndex = 1;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Location = new System.Drawing.Point(20, 33);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(55, 17);
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
            this.pnlActions.Location = new System.Drawing.Point(10, 560);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(776, 80);
            this.pnlActions.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(400, 25);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 35);
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
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(300, 25);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 35);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(20, 25);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 35);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(200, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(120, 25);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // JournalVoucherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "JournalVoucherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Journal Vouchers";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlVoucherList.ResumeLayout(false);
            this.pnlVoucherList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournalVouchers)).EndInit();
            this.pnlVoucherDetails.ResumeLayout(false);
            this.pnlVoucherInfo.ResumeLayout(false);
            this.pnlVoucherInfo.PerformLayout();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.pnlDetailForm.ResumeLayout(false);
            this.pnlDetailForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCreditAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDebitAmount)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvJournalVouchers;
        private System.Windows.Forms.Panel pnlVoucherDetails;
        private System.Windows.Forms.GroupBox pnlVoucherInfo;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.Label lblNarration;
        private System.Windows.Forms.DateTimePicker dtpVoucherDate;
        private System.Windows.Forms.Label lblVoucherDate;
        private System.Windows.Forms.TextBox txtVoucherNumber;
        private System.Windows.Forms.Label lblVoucherNumber;
        private System.Windows.Forms.GroupBox pnlDetails;
        private System.Windows.Forms.Label lblDifference;
        private System.Windows.Forms.Label lblTotalCredit;
        private System.Windows.Forms.Label lblTotalDebit;
        private System.Windows.Forms.Button btnRemoveDetail;
        private System.Windows.Forms.Button btnAddDetail;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.GroupBox pnlDetailForm;
        private System.Windows.Forms.TextBox txtDetailNarration;
        private System.Windows.Forms.Label lblDetailNarration;
        private System.Windows.Forms.NumericUpDown numCreditAmount;
        private System.Windows.Forms.Label lblCreditAmount;
        private System.Windows.Forms.NumericUpDown numDebitAmount;
        private System.Windows.Forms.Label lblDebitAmount;
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
