using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class StockMovementEntryForm
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.headerLabel = new System.Windows.Forms.Label();
            this.lblMovementType = new System.Windows.Forms.Label();
            this.cmbMovementType = new System.Windows.Forms.ComboBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblWarehouse = new System.Windows.Forms.Label();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblMovementDate = new System.Windows.Forms.Label();
            this.dtpMovementDate = new System.Windows.Forms.DateTimePicker();
            this.lblReferenceType = new System.Windows.Forms.Label();
            this.cmbReferenceType = new System.Windows.Forms.ComboBox();
            this.lblReferenceNumber = new System.Windows.Forms.Label();
            this.txtReferenceNumber = new System.Windows.Forms.TextBox();
            this.lblBatchNumber = new System.Windows.Forms.Label();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.lblRemarks);
            this.mainPanel.Controls.Add(this.txtRemarks);
            this.mainPanel.Controls.Add(this.lblExpiryDate);
            this.mainPanel.Controls.Add(this.dtpExpiryDate);
            this.mainPanel.Controls.Add(this.lblBatchNumber);
            this.mainPanel.Controls.Add(this.txtBatchNumber);
            this.mainPanel.Controls.Add(this.lblReferenceNumber);
            this.mainPanel.Controls.Add(this.txtReferenceNumber);
            this.mainPanel.Controls.Add(this.lblReferenceType);
            this.mainPanel.Controls.Add(this.cmbReferenceType);
            this.mainPanel.Controls.Add(this.lblMovementDate);
            this.mainPanel.Controls.Add(this.dtpMovementDate);
            this.mainPanel.Controls.Add(this.lblQuantity);
            this.mainPanel.Controls.Add(this.txtQuantity);
            this.mainPanel.Controls.Add(this.lblWarehouse);
            this.mainPanel.Controls.Add(this.cmbWarehouse);
            this.mainPanel.Controls.Add(this.lblProduct);
            this.mainPanel.Controls.Add(this.cmbProduct);
            this.mainPanel.Controls.Add(this.lblMovementType);
            this.mainPanel.Controls.Add(this.cmbMovementType);
            this.mainPanel.Controls.Add(this.headerLabel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(20);
            this.mainPanel.Size = new System.Drawing.Size(800, 600);
            this.mainPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.headerLabel.Location = new System.Drawing.Point(20, 20);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(220, 30);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Stock Movement Entry";
            // 
            // lblMovementType
            // 
            this.lblMovementType.AutoSize = true;
            this.lblMovementType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMovementType.Location = new System.Drawing.Point(20, 70);
            this.lblMovementType.Name = "lblMovementType";
            this.lblMovementType.Size = new System.Drawing.Size(100, 15);
            this.lblMovementType.TabIndex = 1;
            this.lblMovementType.Text = "Movement Type:";
            // 
            // cmbMovementType
            // 
            this.cmbMovementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMovementType.Location = new System.Drawing.Point(180, 67);
            this.cmbMovementType.Name = "cmbMovementType";
            this.cmbMovementType.Size = new System.Drawing.Size(200, 23);
            this.cmbMovementType.TabIndex = 2;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblProduct.Location = new System.Drawing.Point(20, 110);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(55, 15);
            this.lblProduct.TabIndex = 3;
            this.lblProduct.Text = "Product:";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.Location = new System.Drawing.Point(180, 107);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(200, 23);
            this.cmbProduct.TabIndex = 4;
            // 
            // lblWarehouse
            // 
            this.lblWarehouse.AutoSize = true;
            this.lblWarehouse.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblWarehouse.Location = new System.Drawing.Point(20, 150);
            this.lblWarehouse.Name = "lblWarehouse";
            this.lblWarehouse.Size = new System.Drawing.Size(70, 15);
            this.lblWarehouse.TabIndex = 5;
            this.lblWarehouse.Text = "Warehouse:";
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarehouse.Location = new System.Drawing.Point(180, 147);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(200, 23);
            this.cmbWarehouse.TabIndex = 6;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblQuantity.Location = new System.Drawing.Point(20, 190);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(60, 15);
            this.lblQuantity.TabIndex = 7;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(180, 187);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(200, 23);
            this.txtQuantity.TabIndex = 8;
            // 
            // lblMovementDate
            // 
            this.lblMovementDate.AutoSize = true;
            this.lblMovementDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMovementDate.Location = new System.Drawing.Point(20, 230);
            this.lblMovementDate.Name = "lblMovementDate";
            this.lblMovementDate.Size = new System.Drawing.Size(100, 15);
            this.lblMovementDate.TabIndex = 9;
            this.lblMovementDate.Text = "Movement Date:";
            // 
            // dtpMovementDate
            // 
            this.dtpMovementDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMovementDate.Location = new System.Drawing.Point(180, 227);
            this.dtpMovementDate.Name = "dtpMovementDate";
            this.dtpMovementDate.Size = new System.Drawing.Size(200, 23);
            this.dtpMovementDate.TabIndex = 10;
            this.dtpMovementDate.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // lblReferenceType
            // 
            this.lblReferenceType.AutoSize = true;
            this.lblReferenceType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReferenceType.Location = new System.Drawing.Point(20, 270);
            this.lblReferenceType.Name = "lblReferenceType";
            this.lblReferenceType.Size = new System.Drawing.Size(95, 15);
            this.lblReferenceType.TabIndex = 11;
            this.lblReferenceType.Text = "Reference Type:";
            // 
            // cmbReferenceType
            // 
            this.cmbReferenceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferenceType.Location = new System.Drawing.Point(180, 267);
            this.cmbReferenceType.Name = "cmbReferenceType";
            this.cmbReferenceType.Size = new System.Drawing.Size(200, 23);
            this.cmbReferenceType.TabIndex = 12;
            // 
            // lblReferenceNumber
            // 
            this.lblReferenceNumber.AutoSize = true;
            this.lblReferenceNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReferenceNumber.Location = new System.Drawing.Point(20, 310);
            this.lblReferenceNumber.Name = "lblReferenceNumber";
            this.lblReferenceNumber.Size = new System.Drawing.Size(110, 15);
            this.lblReferenceNumber.TabIndex = 13;
            this.lblReferenceNumber.Text = "Reference Number:";
            // 
            // txtReferenceNumber
            // 
            this.txtReferenceNumber.Location = new System.Drawing.Point(180, 307);
            this.txtReferenceNumber.Name = "txtReferenceNumber";
            this.txtReferenceNumber.Size = new System.Drawing.Size(200, 23);
            this.txtReferenceNumber.TabIndex = 14;
            // 
            // lblBatchNumber
            // 
            this.lblBatchNumber.AutoSize = true;
            this.lblBatchNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBatchNumber.Location = new System.Drawing.Point(20, 350);
            this.lblBatchNumber.Name = "lblBatchNumber";
            this.lblBatchNumber.Size = new System.Drawing.Size(90, 15);
            this.lblBatchNumber.TabIndex = 15;
            this.lblBatchNumber.Text = "Batch Number:";
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.Location = new System.Drawing.Point(180, 347);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(200, 23);
            this.txtBatchNumber.TabIndex = 16;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblExpiryDate.Location = new System.Drawing.Point(20, 390);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(80, 15);
            this.lblExpiryDate.TabIndex = 17;
            this.lblExpiryDate.Text = "Expiry Date:";
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiryDate.Location = new System.Drawing.Point(180, 387);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(200, 23);
            this.dtpExpiryDate.TabIndex = 18;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRemarks.Location = new System.Drawing.Point(20, 430);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(60, 15);
            this.lblRemarks.TabIndex = 19;
            this.lblRemarks.Text = "Remarks:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(180, 427);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.Size = new System.Drawing.Size(200, 60);
            this.txtRemarks.TabIndex = 20;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.buttonPanel.Controls.Add(this.btnSave);
            this.buttonPanel.Controls.Add(this.btnClear);
            this.buttonPanel.Controls.Add(this.btnClose);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 520);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(20);
            this.buttonPanel.Size = new System.Drawing.Size(800, 80);
            this.buttonPanel.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Movement";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(150, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 35);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear Form";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(260, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // StockMovementEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.buttonPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "StockMovementEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stock Movement Entry";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label lblMovementType;
        private System.Windows.Forms.ComboBox cmbMovementType;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblWarehouse;
        private System.Windows.Forms.ComboBox cmbWarehouse;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblMovementDate;
        private System.Windows.Forms.DateTimePicker dtpMovementDate;
        private System.Windows.Forms.Label lblReferenceType;
        private System.Windows.Forms.ComboBox cmbReferenceType;
        private System.Windows.Forms.Label lblReferenceNumber;
        private System.Windows.Forms.TextBox txtReferenceNumber;
        private System.Windows.Forms.Label lblBatchNumber;
        private System.Windows.Forms.TextBox txtBatchNumber;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
    }
}
