using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class PurchaseReturnForm
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
            this.headerGroup = new System.Windows.Forms.GroupBox();
            this.returnNoLabel = new System.Windows.Forms.Label();
            this.txtReturnNumber = new System.Windows.Forms.TextBox();
            this.barcodeLabel = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.supplierLabel = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.refPurchaseLabel = new System.Windows.Forms.Label();
            this.cmbReferencePurchase = new System.Windows.Forms.ComboBox();
            this.returnDateLabel = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.taxAdjustLabel = new System.Windows.Forms.Label();
            this.txtTaxAdjust = new System.Windows.Forms.TextBox();
            this.discountAdjustLabel = new System.Windows.Forms.Label();
            this.txtDiscountAdjust = new System.Windows.Forms.TextBox();
            this.remarksLabel = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.itemsGroup = new System.Windows.Forms.GroupBox();
            this.dgvReturnItems = new System.Windows.Forms.DataGridView();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.headerGroup.SuspendLayout();
            this.itemsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnItems)).BeginInit();
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
            this.headerPanel.Size = new System.Drawing.Size(1200, 80);
            this.headerPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(20, 20);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(250, 37);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "🔄 Purchase Return";
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(1120, 20);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(40, 40);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "✕";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.Controls.Add(this.headerGroup);
            this.contentPanel.Controls.Add(this.itemsGroup);
            this.contentPanel.Controls.Add(this.actionsGroup);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 80);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentPanel.Size = new System.Drawing.Size(1200, 720);
            this.contentPanel.TabIndex = 1;
            // 
            // headerGroup
            // 
            this.headerGroup.Controls.Add(this.returnNoLabel);
            this.headerGroup.Controls.Add(this.txtReturnNumber);
            this.headerGroup.Controls.Add(this.barcodeLabel);
            this.headerGroup.Controls.Add(this.txtBarcode);
            this.headerGroup.Controls.Add(this.supplierLabel);
            this.headerGroup.Controls.Add(this.cmbSupplier);
            this.headerGroup.Controls.Add(this.refPurchaseLabel);
            this.headerGroup.Controls.Add(this.cmbReferencePurchase);
            this.headerGroup.Controls.Add(this.returnDateLabel);
            this.headerGroup.Controls.Add(this.dtpReturnDate);
            this.headerGroup.Controls.Add(this.taxAdjustLabel);
            this.headerGroup.Controls.Add(this.txtTaxAdjust);
            this.headerGroup.Controls.Add(this.discountAdjustLabel);
            this.headerGroup.Controls.Add(this.txtDiscountAdjust);
            this.headerGroup.Controls.Add(this.remarksLabel);
            this.headerGroup.Controls.Add(this.txtRemarks);
            this.headerGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerGroup.Location = new System.Drawing.Point(20, 20);
            this.headerGroup.Name = "headerGroup";
            this.headerGroup.Size = new System.Drawing.Size(900, 200);
            this.headerGroup.TabIndex = 0;
            this.headerGroup.TabStop = false;
            this.headerGroup.Text = "📋 Purchase Return Header";
            // 
            // returnNoLabel
            // 
            this.returnNoLabel.AutoSize = true;
            this.returnNoLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returnNoLabel.Location = new System.Drawing.Point(20, 30);
            this.returnNoLabel.Name = "returnNoLabel";
            this.returnNoLabel.Size = new System.Drawing.Size(80, 19);
            this.returnNoLabel.TabIndex = 0;
            this.returnNoLabel.Text = "Return No:";
            // 
            // txtReturnNumber
            // 
            this.txtReturnNumber.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnNumber.Location = new System.Drawing.Point(120, 28);
            this.txtReturnNumber.Name = "txtReturnNumber";
            this.txtReturnNumber.ReadOnly = true;
            this.txtReturnNumber.Size = new System.Drawing.Size(150, 25);
            this.txtReturnNumber.TabIndex = 1;
            // 
            // barcodeLabel
            // 
            this.barcodeLabel.AutoSize = true;
            this.barcodeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barcodeLabel.Location = new System.Drawing.Point(300, 30);
            this.barcodeLabel.Name = "barcodeLabel";
            this.barcodeLabel.Size = new System.Drawing.Size(70, 19);
            this.barcodeLabel.TabIndex = 2;
            this.barcodeLabel.Text = "Barcode:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(380, 28);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.ReadOnly = true;
            this.txtBarcode.Size = new System.Drawing.Size(150, 25);
            this.txtBarcode.TabIndex = 3;
            // 
            // supplierLabel
            // 
            this.supplierLabel.AutoSize = true;
            this.supplierLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.supplierLabel.Location = new System.Drawing.Point(20, 70);
            this.supplierLabel.Name = "supplierLabel";
            this.supplierLabel.Size = new System.Drawing.Size(70, 19);
            this.supplierLabel.TabIndex = 4;
            this.supplierLabel.Text = "Supplier:";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier.Location = new System.Drawing.Point(120, 68);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(200, 25);
            this.cmbSupplier.TabIndex = 5;
            // 
            // refPurchaseLabel
            // 
            this.refPurchaseLabel.AutoSize = true;
            this.refPurchaseLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refPurchaseLabel.Location = new System.Drawing.Point(350, 70);
            this.refPurchaseLabel.Name = "refPurchaseLabel";
            this.refPurchaseLabel.Size = new System.Drawing.Size(150, 19);
            this.refPurchaseLabel.TabIndex = 6;
            this.refPurchaseLabel.Text = "Reference Purchase:";
            // 
            // cmbReferencePurchase
            // 
            this.cmbReferencePurchase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferencePurchase.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReferencePurchase.Location = new System.Drawing.Point(500, 68);
            this.cmbReferencePurchase.Name = "cmbReferencePurchase";
            this.cmbReferencePurchase.Size = new System.Drawing.Size(200, 25);
            this.cmbReferencePurchase.TabIndex = 7;
            this.cmbReferencePurchase.SelectedIndexChanged += new System.EventHandler(this.CmbReferencePurchase_SelectedIndexChanged);
            // 
            // returnDateLabel
            // 
            this.returnDateLabel.AutoSize = true;
            this.returnDateLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returnDateLabel.Location = new System.Drawing.Point(20, 110);
            this.returnDateLabel.Name = "returnDateLabel";
            this.returnDateLabel.Size = new System.Drawing.Size(90, 19);
            this.returnDateLabel.TabIndex = 8;
            this.returnDateLabel.Text = "Return Date:";
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturnDate.Location = new System.Drawing.Point(120, 108);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(150, 25);
            this.dtpReturnDate.TabIndex = 9;
            // 
            // taxAdjustLabel
            // 
            this.taxAdjustLabel.AutoSize = true;
            this.taxAdjustLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taxAdjustLabel.Location = new System.Drawing.Point(300, 110);
            this.taxAdjustLabel.Name = "taxAdjustLabel";
            this.taxAdjustLabel.Size = new System.Drawing.Size(90, 19);
            this.taxAdjustLabel.TabIndex = 10;
            this.taxAdjustLabel.Text = "Tax Adjust:";
            // 
            // txtTaxAdjust
            // 
            this.txtTaxAdjust.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxAdjust.Location = new System.Drawing.Point(380, 108);
            this.txtTaxAdjust.Name = "txtTaxAdjust";
            this.txtTaxAdjust.Size = new System.Drawing.Size(100, 25);
            this.txtTaxAdjust.TabIndex = 11;
            this.txtTaxAdjust.Text = "0.00";
            // 
            // discountAdjustLabel
            // 
            this.discountAdjustLabel.AutoSize = true;
            this.discountAdjustLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discountAdjustLabel.Location = new System.Drawing.Point(500, 110);
            this.discountAdjustLabel.Name = "discountAdjustLabel";
            this.discountAdjustLabel.Size = new System.Drawing.Size(120, 19);
            this.discountAdjustLabel.TabIndex = 12;
            this.discountAdjustLabel.Text = "Discount Adjust:";
            // 
            // txtDiscountAdjust
            // 
            this.txtDiscountAdjust.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountAdjust.Location = new System.Drawing.Point(650, 108);
            this.txtDiscountAdjust.Name = "txtDiscountAdjust";
            this.txtDiscountAdjust.Size = new System.Drawing.Size(100, 25);
            this.txtDiscountAdjust.TabIndex = 13;
            this.txtDiscountAdjust.Text = "0.00";
            // 
            // remarksLabel
            // 
            this.remarksLabel.AutoSize = true;
            this.remarksLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remarksLabel.Location = new System.Drawing.Point(20, 150);
            this.remarksLabel.Name = "remarksLabel";
            this.remarksLabel.Size = new System.Drawing.Size(70, 19);
            this.remarksLabel.TabIndex = 14;
            this.remarksLabel.Text = "Remarks:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(120, 148);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(630, 25);
            this.txtRemarks.TabIndex = 15;
            // 
            // itemsGroup
            // 
            this.itemsGroup.Controls.Add(this.dgvReturnItems);
            this.itemsGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemsGroup.Location = new System.Drawing.Point(20, 240);
            this.itemsGroup.Name = "itemsGroup";
            this.itemsGroup.Size = new System.Drawing.Size(900, 300);
            this.itemsGroup.TabIndex = 1;
            this.itemsGroup.TabStop = false;
            this.itemsGroup.Text = "📦 Return Items";
            // 
            // dgvReturnItems
            // 
            this.dgvReturnItems.AllowUserToAddRows = false;
            this.dgvReturnItems.AllowUserToDeleteRows = false;
            this.dgvReturnItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReturnItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvReturnItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvReturnItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReturnItems.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvReturnItems.Location = new System.Drawing.Point(20, 30);
            this.dgvReturnItems.MultiSelect = false;
            this.dgvReturnItems.Name = "dgvReturnItems";
            this.dgvReturnItems.RowHeadersVisible = false;
            this.dgvReturnItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReturnItems.Size = new System.Drawing.Size(860, 250);
            this.dgvReturnItems.TabIndex = 0;
            // 
            // actionsGroup
            // 
            this.actionsGroup.Controls.Add(this.btnSave);
            this.actionsGroup.Controls.Add(this.btnClear);
            this.actionsGroup.Controls.Add(this.btnClose);
            this.actionsGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionsGroup.Location = new System.Drawing.Point(20, 560);
            this.actionsGroup.Name = "actionsGroup";
            this.actionsGroup.Size = new System.Drawing.Size(900, 80);
            this.actionsGroup.TabIndex = 2;
            this.actionsGroup.TabStop = false;
            this.actionsGroup.Text = "⚡ Actions";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(140, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "🗑️ Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(260, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "❌ Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // PurchaseReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "PurchaseReturnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Return - Distribution Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.headerGroup.ResumeLayout(false);
            this.headerGroup.PerformLayout();
            this.itemsGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnItems)).EndInit();
            this.actionsGroup.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox headerGroup;
        private System.Windows.Forms.Label returnNoLabel;
        private System.Windows.Forms.TextBox txtReturnNumber;
        private System.Windows.Forms.Label barcodeLabel;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label supplierLabel;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label refPurchaseLabel;
        private System.Windows.Forms.ComboBox cmbReferencePurchase;
        private System.Windows.Forms.Label returnDateLabel;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.Label taxAdjustLabel;
        private System.Windows.Forms.TextBox txtTaxAdjust;
        private System.Windows.Forms.Label discountAdjustLabel;
        private System.Windows.Forms.TextBox txtDiscountAdjust;
        private System.Windows.Forms.Label remarksLabel;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.GroupBox itemsGroup;
        private System.Windows.Forms.DataGridView dgvReturnItems;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
    }
}
