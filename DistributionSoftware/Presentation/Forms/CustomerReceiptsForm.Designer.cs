using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class CustomerReceiptsForm
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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.headerLabel = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.receiptsListGroup = new System.Windows.Forms.GroupBox();
            this.dgvReceipts = new System.Windows.Forms.DataGridView();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.receiptGroup = new System.Windows.Forms.GroupBox();
            this.receivedByLabel = new System.Windows.Forms.Label();
            this.txtReceivedBy = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.invoiceRefLabel = new System.Windows.Forms.Label();
            this.cmbInvoiceReference = new System.Windows.Forms.ComboBox();
            this.paymentMethodLabel = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.paymentDetailsGroup = new System.Windows.Forms.GroupBox();
            this.lblBankName = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.lblAccountNumber = new System.Windows.Forms.Label();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.lblChequeNumber = new System.Windows.Forms.Label();
            this.txtChequeNumber = new System.Windows.Forms.TextBox();
            this.lblChequeDate = new System.Windows.Forms.Label();
            this.dtpChequeDate = new System.Windows.Forms.DateTimePicker();
            this.lblTransactionId = new System.Windows.Forms.Label();
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.lblCardType = new System.Windows.Forms.Label();
            this.cmbCardType = new System.Windows.Forms.ComboBox();
            this.lblMobileNumber = new System.Windows.Forms.Label();
            this.txtMobileNumber = new System.Windows.Forms.TextBox();
            this.lblPaymentReference = new System.Windows.Forms.Label();
            this.txtPaymentReference = new System.Windows.Forms.TextBox();
            this.amountLabel = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.customerLabel = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.receiptDateLabel = new System.Windows.Forms.Label();
            this.dtpReceiptDate = new System.Windows.Forms.DateTimePicker();
            this.receiptNumberLabel = new System.Windows.Forms.Label();
            this.txtReceiptNumber = new System.Windows.Forms.TextBox();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.receiptsListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).BeginInit();
            this.leftPanel.SuspendLayout();
            this.actionsGroup.SuspendLayout();
            this.receiptGroup.SuspendLayout();
            this.paymentDetailsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Controls.Add(this.closeBtn);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1284, 60);
            this.headerPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(20, 15);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(267, 32);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "💳 Customer Receipts";
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(1234, 10);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(40, 40);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "×";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.Controls.Add(this.receiptsListGroup);
            this.mainPanel.Controls.Add(this.leftPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 60);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1284, 641);
            this.mainPanel.TabIndex = 1;
            // 
            // receiptsListGroup
            // 
            this.receiptsListGroup.Controls.Add(this.dgvReceipts);
            this.receiptsListGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.receiptsListGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.receiptsListGroup.Location = new System.Drawing.Point(102, 440);
            this.receiptsListGroup.Name = "receiptsListGroup";
            this.receiptsListGroup.Size = new System.Drawing.Size(1027, 200);
            this.receiptsListGroup.TabIndex = 1;
            this.receiptsListGroup.TabStop = false;
            this.receiptsListGroup.Text = "📋 Receipts List";
            // 
            // dgvReceipts
            // 
            this.dgvReceipts.AllowUserToAddRows = false;
            this.dgvReceipts.AllowUserToDeleteRows = false;
            this.dgvReceipts.BackgroundColor = System.Drawing.Color.White;
            this.dgvReceipts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReceipts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvReceipts.ColumnHeadersHeight = 35;
            this.dgvReceipts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReceipts.EnableHeadersVisualStyles = false;
            this.dgvReceipts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvReceipts.Location = new System.Drawing.Point(3, 25);
            this.dgvReceipts.MultiSelect = false;
            this.dgvReceipts.Name = "dgvReceipts";
            this.dgvReceipts.ReadOnly = true;
            this.dgvReceipts.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvReceipts.RowHeadersVisible = false;
            this.dgvReceipts.RowHeadersWidth = 62;
            this.dgvReceipts.RowTemplate.Height = 30;
            this.dgvReceipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReceipts.Size = new System.Drawing.Size(1021, 172);
            this.dgvReceipts.TabIndex = 0;
            this.dgvReceipts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipts_CellContentClick);
            this.dgvReceipts.SelectionChanged += new System.EventHandler(this.DgvReceipts_SelectionChanged);
            this.dgvReceipts.Resize += new System.EventHandler(this.DgvReceipts_Resize);
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.actionsGroup);
            this.leftPanel.Controls.Add(this.receiptGroup);
            this.leftPanel.Location = new System.Drawing.Point(20, 20);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Padding = new System.Windows.Forms.Padding(20);
            this.leftPanel.Size = new System.Drawing.Size(1273, 400);
            this.leftPanel.TabIndex = 0;
            // 
            // actionsGroup
            // 
            this.actionsGroup.Controls.Add(this.btnPrint);
            this.actionsGroup.Controls.Add(this.btnClose);
            this.actionsGroup.Controls.Add(this.btnDelete);
            this.actionsGroup.Controls.Add(this.btnClear);
            this.actionsGroup.Controls.Add(this.btnSave);
            this.actionsGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.actionsGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.actionsGroup.Location = new System.Drawing.Point(20, 280);
            this.actionsGroup.Name = "actionsGroup";
            this.actionsGroup.Size = new System.Drawing.Size(987, 100);
            this.actionsGroup.TabIndex = 2;
            this.actionsGroup.TabStop = false;
            this.actionsGroup.Text = "⚡ Actions";
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(476, 35);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 40);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "🖨️ Print Receipt";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(635, 35);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 40);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "❌ Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(314, 35);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 40);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "🗑️ Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(161, 35);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 40);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "🗑️ Clear Form";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 35);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 Save Receipt";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // receiptGroup
            // 
            this.receiptGroup.Controls.Add(this.receivedByLabel);
            this.receiptGroup.Controls.Add(this.txtReceivedBy);
            this.receiptGroup.Controls.Add(this.descriptionLabel);
            this.receiptGroup.Controls.Add(this.txtDescription);
            this.receiptGroup.Controls.Add(this.invoiceRefLabel);
            this.receiptGroup.Controls.Add(this.cmbInvoiceReference);
            this.receiptGroup.Controls.Add(this.paymentMethodLabel);
            this.receiptGroup.Controls.Add(this.cmbPaymentMethod);
            this.receiptGroup.Controls.Add(this.paymentDetailsGroup);
            this.receiptGroup.Controls.Add(this.amountLabel);
            this.receiptGroup.Controls.Add(this.txtAmount);
            this.receiptGroup.Controls.Add(this.customerLabel);
            this.receiptGroup.Controls.Add(this.cmbCustomer);
            this.receiptGroup.Controls.Add(this.receiptDateLabel);
            this.receiptGroup.Controls.Add(this.dtpReceiptDate);
            this.receiptGroup.Controls.Add(this.receiptNumberLabel);
            this.receiptGroup.Controls.Add(this.txtReceiptNumber);
            this.receiptGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.receiptGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.receiptGroup.Location = new System.Drawing.Point(20, 20);
            this.receiptGroup.Name = "receiptGroup";
            this.receiptGroup.Size = new System.Drawing.Size(1230, 250);
            this.receiptGroup.TabIndex = 0;
            this.receiptGroup.TabStop = false;
            this.receiptGroup.Text = "📝 Receipt Information";
            // 
            // receivedByLabel
            // 
            this.receivedByLabel.AutoSize = true;
            this.receivedByLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.receivedByLabel.Location = new System.Drawing.Point(40, 218);
            this.receivedByLabel.Name = "receivedByLabel";
            this.receivedByLabel.Size = new System.Drawing.Size(73, 15);
            this.receivedByLabel.TabIndex = 15;
            this.receivedByLabel.Text = "Received By:";
            // 
            // txtReceivedBy
            // 
            this.txtReceivedBy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReceivedBy.Location = new System.Drawing.Point(120, 215);
            this.txtReceivedBy.Name = "txtReceivedBy";
            this.txtReceivedBy.Size = new System.Drawing.Size(200, 23);
            this.txtReceivedBy.TabIndex = 14;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.descriptionLabel.Location = new System.Drawing.Point(20, 150);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(70, 15);
            this.descriptionLabel.TabIndex = 13;
            this.descriptionLabel.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDescription.Location = new System.Drawing.Point(100, 147);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(250, 23);
            this.txtDescription.TabIndex = 12;
            // 
            // invoiceRefLabel
            // 
            this.invoiceRefLabel.AutoSize = true;
            this.invoiceRefLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.invoiceRefLabel.Location = new System.Drawing.Point(31, 189);
            this.invoiceRefLabel.Name = "invoiceRefLabel";
            this.invoiceRefLabel.Size = new System.Drawing.Size(103, 15);
            this.invoiceRefLabel.TabIndex = 11;
            this.invoiceRefLabel.Text = "Invoice Reference:";
            // 
            // cmbInvoiceReference
            // 
            this.cmbInvoiceReference.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvoiceReference.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbInvoiceReference.FormattingEnabled = true;
            this.cmbInvoiceReference.Location = new System.Drawing.Point(140, 186);
            this.cmbInvoiceReference.Name = "cmbInvoiceReference";
            this.cmbInvoiceReference.Size = new System.Drawing.Size(200, 23);
            this.cmbInvoiceReference.TabIndex = 10;
            // 
            // paymentMethodLabel
            // 
            this.paymentMethodLabel.AutoSize = true;
            this.paymentMethodLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.paymentMethodLabel.Location = new System.Drawing.Point(14, 110);
            this.paymentMethodLabel.Name = "paymentMethodLabel";
            this.paymentMethodLabel.Size = new System.Drawing.Size(102, 15);
            this.paymentMethodLabel.TabIndex = 9;
            this.paymentMethodLabel.Text = "Payment Method:";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(120, 107);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(150, 23);
            this.cmbPaymentMethod.TabIndex = 8;
            // 
            // paymentDetailsGroup
            // 
            this.paymentDetailsGroup.Controls.Add(this.lblBankName);
            this.paymentDetailsGroup.Controls.Add(this.txtBankName);
            this.paymentDetailsGroup.Controls.Add(this.lblAccountNumber);
            this.paymentDetailsGroup.Controls.Add(this.txtAccountNumber);
            this.paymentDetailsGroup.Controls.Add(this.lblChequeNumber);
            this.paymentDetailsGroup.Controls.Add(this.txtChequeNumber);
            this.paymentDetailsGroup.Controls.Add(this.lblChequeDate);
            this.paymentDetailsGroup.Controls.Add(this.dtpChequeDate);
            this.paymentDetailsGroup.Controls.Add(this.lblTransactionId);
            this.paymentDetailsGroup.Controls.Add(this.txtTransactionId);
            this.paymentDetailsGroup.Controls.Add(this.lblCardNumber);
            this.paymentDetailsGroup.Controls.Add(this.txtCardNumber);
            this.paymentDetailsGroup.Controls.Add(this.lblCardType);
            this.paymentDetailsGroup.Controls.Add(this.cmbCardType);
            this.paymentDetailsGroup.Controls.Add(this.lblMobileNumber);
            this.paymentDetailsGroup.Controls.Add(this.txtMobileNumber);
            this.paymentDetailsGroup.Controls.Add(this.lblPaymentReference);
            this.paymentDetailsGroup.Controls.Add(this.txtPaymentReference);
            this.paymentDetailsGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.paymentDetailsGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.paymentDetailsGroup.Location = new System.Drawing.Point(356, 110);
            this.paymentDetailsGroup.Name = "paymentDetailsGroup";
            this.paymentDetailsGroup.Size = new System.Drawing.Size(720, 157);
            this.paymentDetailsGroup.TabIndex = 10;
            this.paymentDetailsGroup.TabStop = false;
            this.paymentDetailsGroup.Text = "💳 Payment Details";
            this.paymentDetailsGroup.Visible = false;
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBankName.Location = new System.Drawing.Point(20, 25);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(71, 15);
            this.lblBankName.TabIndex = 0;
            this.lblBankName.Text = "Bank Name:";
            this.lblBankName.Visible = false;
            // 
            // txtBankName
            // 
            this.txtBankName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBankName.Location = new System.Drawing.Point(100, 22);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(150, 23);
            this.txtBankName.TabIndex = 1;
            this.txtBankName.Visible = false;
            // 
            // lblAccountNumber
            // 
            this.lblAccountNumber.AutoSize = true;
            this.lblAccountNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAccountNumber.Location = new System.Drawing.Point(270, 25);
            this.lblAccountNumber.Name = "lblAccountNumber";
            this.lblAccountNumber.Size = new System.Drawing.Size(102, 15);
            this.lblAccountNumber.TabIndex = 2;
            this.lblAccountNumber.Text = "Account Number:";
            this.lblAccountNumber.Visible = false;
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAccountNumber.Location = new System.Drawing.Point(370, 22);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(150, 23);
            this.txtAccountNumber.TabIndex = 3;
            this.txtAccountNumber.Visible = false;
            // 
            // lblChequeNumber
            // 
            this.lblChequeNumber.AutoSize = true;
            this.lblChequeNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChequeNumber.Location = new System.Drawing.Point(20, 55);
            this.lblChequeNumber.Name = "lblChequeNumber";
            this.lblChequeNumber.Size = new System.Drawing.Size(98, 15);
            this.lblChequeNumber.TabIndex = 2;
            this.lblChequeNumber.Text = "Cheque Number:";
            this.lblChequeNumber.Visible = false;
            // 
            // txtChequeNumber
            // 
            this.txtChequeNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtChequeNumber.Location = new System.Drawing.Point(120, 52);
            this.txtChequeNumber.Name = "txtChequeNumber";
            this.txtChequeNumber.Size = new System.Drawing.Size(120, 23);
            this.txtChequeNumber.TabIndex = 3;
            this.txtChequeNumber.Visible = false;
            // 
            // lblChequeDate
            // 
            this.lblChequeDate.AutoSize = true;
            this.lblChequeDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChequeDate.Location = new System.Drawing.Point(250, 55);
            this.lblChequeDate.Name = "lblChequeDate";
            this.lblChequeDate.Size = new System.Drawing.Size(78, 15);
            this.lblChequeDate.TabIndex = 4;
            this.lblChequeDate.Text = "Cheque Date:";
            this.lblChequeDate.Visible = false;
            // 
            // dtpChequeDate
            // 
            this.dtpChequeDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpChequeDate.Location = new System.Drawing.Point(330, 52);
            this.dtpChequeDate.Name = "dtpChequeDate";
            this.dtpChequeDate.Size = new System.Drawing.Size(120, 23);
            this.dtpChequeDate.TabIndex = 5;
            this.dtpChequeDate.Visible = false;
            // 
            // lblTransactionId
            // 
            this.lblTransactionId.AutoSize = true;
            this.lblTransactionId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTransactionId.Location = new System.Drawing.Point(460, 25);
            this.lblTransactionId.Name = "lblTransactionId";
            this.lblTransactionId.Size = new System.Drawing.Size(85, 15);
            this.lblTransactionId.TabIndex = 6;
            this.lblTransactionId.Text = "Transaction ID:";
            this.lblTransactionId.Visible = false;
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTransactionId.Location = new System.Drawing.Point(550, 22);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(150, 23);
            this.txtTransactionId.TabIndex = 7;
            this.txtTransactionId.Visible = false;
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardNumber.Location = new System.Drawing.Point(20, 55);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(82, 15);
            this.lblCardNumber.TabIndex = 8;
            this.lblCardNumber.Text = "Card Number:";
            this.lblCardNumber.Visible = false;
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCardNumber.Location = new System.Drawing.Point(100, 52);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(120, 23);
            this.txtCardNumber.TabIndex = 9;
            this.txtCardNumber.Visible = false;
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCardType.Location = new System.Drawing.Point(230, 55);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(63, 15);
            this.lblCardType.TabIndex = 10;
            this.lblCardType.Text = "Card Type:";
            this.lblCardType.Visible = false;
            // 
            // cmbCardType
            // 
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.Location = new System.Drawing.Point(300, 52);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.Size = new System.Drawing.Size(100, 23);
            this.cmbCardType.TabIndex = 11;
            this.cmbCardType.Visible = false;
            // 
            // lblMobileNumber
            // 
            this.lblMobileNumber.AutoSize = true;
            this.lblMobileNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMobileNumber.Location = new System.Drawing.Point(20, 85);
            this.lblMobileNumber.Name = "lblMobileNumber";
            this.lblMobileNumber.Size = new System.Drawing.Size(94, 15);
            this.lblMobileNumber.TabIndex = 12;
            this.lblMobileNumber.Text = "Mobile Number:";
            this.lblMobileNumber.Visible = false;
            // 
            // txtMobileNumber
            // 
            this.txtMobileNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMobileNumber.Location = new System.Drawing.Point(120, 82);
            this.txtMobileNumber.Name = "txtMobileNumber";
            this.txtMobileNumber.Size = new System.Drawing.Size(120, 23);
            this.txtMobileNumber.TabIndex = 13;
            this.txtMobileNumber.Visible = false;
            // 
            // lblPaymentReference
            // 
            this.lblPaymentReference.AutoSize = true;
            this.lblPaymentReference.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPaymentReference.Location = new System.Drawing.Point(250, 85);
            this.lblPaymentReference.Name = "lblPaymentReference";
            this.lblPaymentReference.Size = new System.Drawing.Size(112, 15);
            this.lblPaymentReference.TabIndex = 14;
            this.lblPaymentReference.Text = "Payment Reference:";
            this.lblPaymentReference.Visible = false;
            // 
            // txtPaymentReference
            // 
            this.txtPaymentReference.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPaymentReference.Location = new System.Drawing.Point(370, 82);
            this.txtPaymentReference.Name = "txtPaymentReference";
            this.txtPaymentReference.Size = new System.Drawing.Size(200, 23);
            this.txtPaymentReference.TabIndex = 15;
            this.txtPaymentReference.Visible = false;
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.amountLabel.Location = new System.Drawing.Point(400, 70);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(54, 15);
            this.amountLabel.TabIndex = 7;
            this.amountLabel.Text = "Amount:";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAmount.Location = new System.Drawing.Point(460, 67);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(120, 23);
            this.txtAmount.TabIndex = 6;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // customerLabel
            // 
            this.customerLabel.AutoSize = true;
            this.customerLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.customerLabel.Location = new System.Drawing.Point(20, 70);
            this.customerLabel.Name = "customerLabel";
            this.customerLabel.Size = new System.Drawing.Size(62, 15);
            this.customerLabel.TabIndex = 5;
            this.customerLabel.Text = "Customer:";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(90, 67);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(250, 23);
            this.cmbCustomer.TabIndex = 4;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.CmbCustomer_SelectedIndexChanged);
            // 
            // receiptDateLabel
            // 
            this.receiptDateLabel.AutoSize = true;
            this.receiptDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.receiptDateLabel.Location = new System.Drawing.Point(400, 30);
            this.receiptDateLabel.Name = "receiptDateLabel";
            this.receiptDateLabel.Size = new System.Drawing.Size(34, 15);
            this.receiptDateLabel.TabIndex = 3;
            this.receiptDateLabel.Text = "Date:";
            // 
            // dtpReceiptDate
            // 
            this.dtpReceiptDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReceiptDate.Location = new System.Drawing.Point(440, 27);
            this.dtpReceiptDate.Name = "dtpReceiptDate";
            this.dtpReceiptDate.Size = new System.Drawing.Size(120, 23);
            this.dtpReceiptDate.TabIndex = 2;
            // 
            // receiptNumberLabel
            // 
            this.receiptNumberLabel.AutoSize = true;
            this.receiptNumberLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.receiptNumberLabel.Location = new System.Drawing.Point(20, 30);
            this.receiptNumberLabel.Name = "receiptNumberLabel";
            this.receiptNumberLabel.Size = new System.Drawing.Size(96, 15);
            this.receiptNumberLabel.TabIndex = 1;
            this.receiptNumberLabel.Text = "Receipt Number:";
            // 
            // txtReceiptNumber
            // 
            this.txtReceiptNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReceiptNumber.Location = new System.Drawing.Point(110, 27);
            this.txtReceiptNumber.Name = "txtReceiptNumber";
            this.txtReceiptNumber.ReadOnly = true;
            this.txtReceiptNumber.Size = new System.Drawing.Size(150, 23);
            this.txtReceiptNumber.TabIndex = 0;
            // 
            // CustomerReceiptsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1284, 701);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1600, 900);
            this.MinimumSize = new System.Drawing.Size(1278, 670);
            this.Name = "CustomerReceiptsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Receipts";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.receiptsListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).EndInit();
            this.leftPanel.ResumeLayout(false);
            this.actionsGroup.ResumeLayout(false);
            this.receiptGroup.ResumeLayout(false);
            this.receiptGroup.PerformLayout();
            this.paymentDetailsGroup.ResumeLayout(false);
            this.paymentDetailsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.GroupBox receiptGroup;
        private System.Windows.Forms.Label receivedByLabel;
        private System.Windows.Forms.TextBox txtReceivedBy;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label invoiceRefLabel;
        private System.Windows.Forms.ComboBox cmbInvoiceReference;
        private System.Windows.Forms.Label paymentMethodLabel;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.GroupBox paymentDetailsGroup;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label lblAccountNumber;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Label lblChequeNumber;
        private System.Windows.Forms.TextBox txtChequeNumber;
        private System.Windows.Forms.Label lblChequeDate;
        private System.Windows.Forms.DateTimePicker dtpChequeDate;
        private System.Windows.Forms.Label lblTransactionId;
        private System.Windows.Forms.TextBox txtTransactionId;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.ComboBox cmbCardType;
        private System.Windows.Forms.Label lblMobileNumber;
        private System.Windows.Forms.TextBox txtMobileNumber;
        private System.Windows.Forms.Label lblPaymentReference;
        private System.Windows.Forms.TextBox txtPaymentReference;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label receiptDateLabel;
        private System.Windows.Forms.DateTimePicker dtpReceiptDate;
        private System.Windows.Forms.Label receiptNumberLabel;
        private System.Windows.Forms.TextBox txtReceiptNumber;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox receiptsListGroup;
        private System.Windows.Forms.DataGridView dgvReceipts;
    }
}