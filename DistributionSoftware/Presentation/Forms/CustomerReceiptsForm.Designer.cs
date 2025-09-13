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
            this.contentPanel = new System.Windows.Forms.Panel();
            this.receiptGroup = new System.Windows.Forms.GroupBox();
            this.receivedByLabel = new System.Windows.Forms.Label();
            this.txtReceivedBy = new System.Windows.Forms.TextBox();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.invoiceRefLabel = new System.Windows.Forms.Label();
            this.cmbInvoiceReference = new System.Windows.Forms.ComboBox();
            this.paymentMethodLabel = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.amountLabel = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.customerLabel = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.receiptDateLabel = new System.Windows.Forms.Label();
            this.dtpReceiptDate = new System.Windows.Forms.DateTimePicker();
            this.receiptNumberLabel = new System.Windows.Forms.Label();
            this.txtReceiptNumber = new System.Windows.Forms.TextBox();
            this.receiptsListGroup = new System.Windows.Forms.GroupBox();
            this.dgvReceipts = new System.Windows.Forms.DataGridView();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.receiptGroup.SuspendLayout();
            this.receiptsListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).BeginInit();
            this.actionsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Controls.Add(this.closeBtn);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1200, 60);
            this.headerPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(20, 15);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(250, 30);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Customer Receipts";
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(1150, 15);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(30, 30);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "×";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.receiptGroup);
            this.contentPanel.Controls.Add(this.receiptsListGroup);
            this.contentPanel.Controls.Add(this.actionsGroup);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 60);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentPanel.Size = new System.Drawing.Size(1200, 740);
            this.contentPanel.TabIndex = 1;
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
            this.receiptGroup.Controls.Add(this.amountLabel);
            this.receiptGroup.Controls.Add(this.txtAmount);
            this.receiptGroup.Controls.Add(this.customerLabel);
            this.receiptGroup.Controls.Add(this.cmbCustomer);
            this.receiptGroup.Controls.Add(this.receiptDateLabel);
            this.receiptGroup.Controls.Add(this.dtpReceiptDate);
            this.receiptGroup.Controls.Add(this.receiptNumberLabel);
            this.receiptGroup.Controls.Add(this.txtReceiptNumber);
            this.receiptGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.receiptGroup.Location = new System.Drawing.Point(20, 20);
            this.receiptGroup.Name = "receiptGroup";
            this.receiptGroup.Size = new System.Drawing.Size(1160, 200);
            this.receiptGroup.TabIndex = 0;
            this.receiptGroup.TabStop = false;
            this.receiptGroup.Text = "Receipt Information";
            // 
            // receivedByLabel
            // 
            this.receivedByLabel.AutoSize = true;
            this.receivedByLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.receivedByLabel.Location = new System.Drawing.Point(600, 150);
            this.receivedByLabel.Name = "receivedByLabel";
            this.receivedByLabel.Size = new System.Drawing.Size(75, 15);
            this.receivedByLabel.TabIndex = 15;
            this.receivedByLabel.Text = "Received By:";
            // 
            // txtReceivedBy
            // 
            this.txtReceivedBy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReceivedBy.Location = new System.Drawing.Point(680, 147);
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
            this.txtDescription.Size = new System.Drawing.Size(300, 23);
            this.txtDescription.TabIndex = 12;
            // 
            // invoiceRefLabel
            // 
            this.invoiceRefLabel.AutoSize = true;
            this.invoiceRefLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.invoiceRefLabel.Location = new System.Drawing.Point(600, 110);
            this.invoiceRefLabel.Name = "invoiceRefLabel";
            this.invoiceRefLabel.Size = new System.Drawing.Size(95, 15);
            this.invoiceRefLabel.TabIndex = 11;
            this.invoiceRefLabel.Text = "Invoice Reference:";
            // 
            // cmbInvoiceReference
            // 
            this.cmbInvoiceReference.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInvoiceReference.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbInvoiceReference.FormattingEnabled = true;
            this.cmbInvoiceReference.Location = new System.Drawing.Point(700, 107);
            this.cmbInvoiceReference.Name = "cmbInvoiceReference";
            this.cmbInvoiceReference.Size = new System.Drawing.Size(200, 23);
            this.cmbInvoiceReference.TabIndex = 10;
            // 
            // paymentMethodLabel
            // 
            this.paymentMethodLabel.AutoSize = true;
            this.paymentMethodLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.paymentMethodLabel.Location = new System.Drawing.Point(20, 110);
            this.paymentMethodLabel.Name = "paymentMethodLabel";
            this.paymentMethodLabel.Size = new System.Drawing.Size(95, 15);
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
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.amountLabel.Location = new System.Drawing.Point(600, 70);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(51, 15);
            this.amountLabel.TabIndex = 7;
            this.amountLabel.Text = "Amount:";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAmount.Location = new System.Drawing.Point(660, 67);
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
            this.customerLabel.Size = new System.Drawing.Size(60, 15);
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
            this.cmbCustomer.Size = new System.Drawing.Size(300, 23);
            this.cmbCustomer.TabIndex = 4;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.CmbCustomer_SelectedIndexChanged);
            // 
            // receiptDateLabel
            // 
            this.receiptDateLabel.AutoSize = true;
            this.receiptDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.receiptDateLabel.Location = new System.Drawing.Point(600, 30);
            this.receiptDateLabel.Name = "receiptDateLabel";
            this.receiptDateLabel.Size = new System.Drawing.Size(35, 15);
            this.receiptDateLabel.TabIndex = 3;
            this.receiptDateLabel.Text = "Date:";
            // 
            // dtpReceiptDate
            // 
            this.dtpReceiptDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReceiptDate.Location = new System.Drawing.Point(640, 27);
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
            this.receiptNumberLabel.Size = new System.Drawing.Size(85, 15);
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
            // receiptsListGroup
            // 
            this.receiptsListGroup.Controls.Add(this.dgvReceipts);
            this.receiptsListGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.receiptsListGroup.Location = new System.Drawing.Point(20, 240);
            this.receiptsListGroup.Name = "receiptsListGroup";
            this.receiptsListGroup.Size = new System.Drawing.Size(1160, 300);
            this.receiptsListGroup.TabIndex = 1;
            this.receiptsListGroup.TabStop = false;
            this.receiptsListGroup.Text = "Receipts List";
            // 
            // dgvReceipts
            // 
            this.dgvReceipts.AllowUserToAddRows = false;
            this.dgvReceipts.AllowUserToDeleteRows = false;
            this.dgvReceipts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReceipts.BackgroundColor = System.Drawing.Color.White;
            this.dgvReceipts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReceipts.Location = new System.Drawing.Point(3, 23);
            this.dgvReceipts.MultiSelect = false;
            this.dgvReceipts.Name = "dgvReceipts";
            this.dgvReceipts.ReadOnly = true;
            this.dgvReceipts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReceipts.Size = new System.Drawing.Size(1154, 274);
            this.dgvReceipts.TabIndex = 0;
            this.dgvReceipts.SelectionChanged += new System.EventHandler(this.DgvReceipts_SelectionChanged);
            // 
            // actionsGroup
            // 
            this.actionsGroup.Controls.Add(this.btnPrint);
            this.actionsGroup.Controls.Add(this.btnClose);
            this.actionsGroup.Controls.Add(this.btnDelete);
            this.actionsGroup.Controls.Add(this.btnClear);
            this.actionsGroup.Controls.Add(this.btnSave);
            this.actionsGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.actionsGroup.Location = new System.Drawing.Point(20, 560);
            this.actionsGroup.Name = "actionsGroup";
            this.actionsGroup.Size = new System.Drawing.Size(1160, 80);
            this.actionsGroup.TabIndex = 2;
            this.actionsGroup.TabStop = false;
            this.actionsGroup.Text = "Actions";
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(400, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 35);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print Receipt";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(520, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(300, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(200, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(100, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Receipt";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // CustomerReceiptsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerReceiptsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Receipts";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.receiptGroup.ResumeLayout(false);
            this.receiptGroup.PerformLayout();
            this.receiptsListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipts)).EndInit();
            this.actionsGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox receiptGroup;
        private System.Windows.Forms.Label receivedByLabel;
        private System.Windows.Forms.TextBox txtReceivedBy;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label invoiceRefLabel;
        private System.Windows.Forms.ComboBox cmbInvoiceReference;
        private System.Windows.Forms.Label paymentMethodLabel;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label receiptDateLabel;
        private System.Windows.Forms.DateTimePicker dtpReceiptDate;
        private System.Windows.Forms.Label receiptNumberLabel;
        private System.Windows.Forms.TextBox txtReceiptNumber;
        private System.Windows.Forms.GroupBox receiptsListGroup;
        private System.Windows.Forms.DataGridView dgvReceipts;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
    }
}
