using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class StockAdjustmentForm
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
            this.adjustmentGroup = new System.Windows.Forms.GroupBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblAdjustmentType = new System.Windows.Forms.Label();
            this.cmbAdjustmentType = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblCurrentStock = new System.Windows.Forms.Label();
            this.txtPhysicalStock = new System.Windows.Forms.TextBox();
            this.lblDifference = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpAdjustmentDate = new System.Windows.Forms.DateTimePicker();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.adjustmentsGroup = new System.Windows.Forms.GroupBox();
            this.dgvAdjustments = new System.Windows.Forms.DataGridView();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.adjustmentGroup.SuspendLayout();
            this.actionsGroup.SuspendLayout();
            this.adjustmentsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjustments)).BeginInit();
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
            this.headerLabel.Size = new System.Drawing.Size(300, 37);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "📦 Stock Adjustment";
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
            this.contentPanel.Controls.Add(this.adjustmentGroup);
            this.contentPanel.Controls.Add(this.actionsGroup);
            this.contentPanel.Controls.Add(this.adjustmentsGroup);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 80);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.contentPanel.Size = new System.Drawing.Size(1200, 720);
            this.contentPanel.TabIndex = 1;
            // 
            // adjustmentGroup
            // 
            this.adjustmentGroup.Controls.Add(this.lblProduct);
            this.adjustmentGroup.Controls.Add(this.cmbProduct);
            this.adjustmentGroup.Controls.Add(this.lblAdjustmentType);
            this.adjustmentGroup.Controls.Add(this.cmbAdjustmentType);
            this.adjustmentGroup.Controls.Add(this.lblQuantity);
            this.adjustmentGroup.Controls.Add(this.txtQuantity);
            this.adjustmentGroup.Controls.Add(this.lblCurrentStock);
            this.adjustmentGroup.Controls.Add(this.txtPhysicalStock);
            this.adjustmentGroup.Controls.Add(this.lblDifference);
            this.adjustmentGroup.Controls.Add(this.txtNotes);
            this.adjustmentGroup.Controls.Add(this.lblReason);
            this.adjustmentGroup.Controls.Add(this.txtReason);
            this.adjustmentGroup.Controls.Add(this.lblDate);
            this.adjustmentGroup.Controls.Add(this.dtpAdjustmentDate);
            this.adjustmentGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adjustmentGroup.Location = new System.Drawing.Point(20, 20);
            this.adjustmentGroup.Name = "adjustmentGroup";
            this.adjustmentGroup.Size = new System.Drawing.Size(1160, 200);
            this.adjustmentGroup.TabIndex = 0;
            this.adjustmentGroup.TabStop = false;
            this.adjustmentGroup.Text = "📝 Adjustment Information";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(20, 30);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(60, 19);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product:";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(20, 55);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(300, 25);
            this.cmbProduct.TabIndex = 1;
            // 
            // lblAdjustmentType
            // 
            this.lblAdjustmentType.AutoSize = true;
            this.lblAdjustmentType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustmentType.Location = new System.Drawing.Point(350, 30);
            this.lblAdjustmentType.Name = "lblAdjustmentType";
            this.lblAdjustmentType.Size = new System.Drawing.Size(120, 19);
            this.lblAdjustmentType.TabIndex = 2;
            this.lblAdjustmentType.Text = "Adjustment Type:";
            // 
            // cmbAdjustmentType
            // 
            this.cmbAdjustmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdjustmentType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAdjustmentType.FormattingEnabled = true;
            this.cmbAdjustmentType.Location = new System.Drawing.Point(350, 55);
            this.cmbAdjustmentType.Name = "cmbAdjustmentType";
            this.cmbAdjustmentType.Size = new System.Drawing.Size(200, 25);
            this.cmbAdjustmentType.TabIndex = 3;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.Location = new System.Drawing.Point(580, 30);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(70, 19);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(580, 55);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(150, 26);
            this.txtQuantity.TabIndex = 5;
            // 
            // lblCurrentStock
            // 
            this.lblCurrentStock.AutoSize = true;
            this.lblCurrentStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentStock.Location = new System.Drawing.Point(20, 100);
            this.lblCurrentStock.Name = "lblCurrentStock";
            this.lblCurrentStock.Size = new System.Drawing.Size(100, 19);
            this.lblCurrentStock.TabIndex = 10;
            this.lblCurrentStock.Text = "Current Stock:";
            // 
            // txtPhysicalStock
            // 
            this.txtPhysicalStock.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhysicalStock.Location = new System.Drawing.Point(20, 125);
            this.txtPhysicalStock.Name = "txtPhysicalStock";
            this.txtPhysicalStock.Size = new System.Drawing.Size(150, 26);
            this.txtPhysicalStock.TabIndex = 11;
            this.txtPhysicalStock.TextChanged += new System.EventHandler(this.TxtPhysicalStock_TextChanged);
            // 
            // lblDifference
            // 
            this.lblDifference.AutoSize = true;
            this.lblDifference.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDifference.Location = new System.Drawing.Point(200, 100);
            this.lblDifference.Name = "lblDifference";
            this.lblDifference.Size = new System.Drawing.Size(80, 19);
            this.lblDifference.TabIndex = 12;
            this.lblDifference.Text = "Difference:";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(200, 125);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(300, 50);
            this.txtNotes.TabIndex = 13;
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReason.Location = new System.Drawing.Point(20, 100);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(60, 19);
            this.lblReason.TabIndex = 6;
            this.lblReason.Text = "Reason:";
            // 
            // txtReason
            // 
            this.txtReason.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Location = new System.Drawing.Point(20, 125);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReason.Size = new System.Drawing.Size(600, 50);
            this.txtReason.TabIndex = 7;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(650, 100);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(40, 19);
            this.lblDate.TabIndex = 8;
            this.lblDate.Text = "Date:";
            // 
            // dtpAdjustmentDate
            // 
            this.dtpAdjustmentDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAdjustmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAdjustmentDate.Location = new System.Drawing.Point(650, 125);
            this.dtpAdjustmentDate.Name = "dtpAdjustmentDate";
            this.dtpAdjustmentDate.Size = new System.Drawing.Size(150, 26);
            this.dtpAdjustmentDate.TabIndex = 9;
            // 
            // actionsGroup
            // 
            this.actionsGroup.Controls.Add(this.btnSave);
            this.actionsGroup.Controls.Add(this.btnClear);
            this.actionsGroup.Controls.Add(this.btnDelete);
            this.actionsGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionsGroup.Location = new System.Drawing.Point(20, 240);
            this.actionsGroup.Name = "actionsGroup";
            this.actionsGroup.Size = new System.Drawing.Size(1160, 80);
            this.actionsGroup.TabIndex = 1;
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
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(260, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "🗑️ Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // adjustmentsGroup
            // 
            this.adjustmentsGroup.Controls.Add(this.dgvAdjustments);
            this.adjustmentsGroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adjustmentsGroup.Location = new System.Drawing.Point(20, 340);
            this.adjustmentsGroup.Name = "adjustmentsGroup";
            this.adjustmentsGroup.Size = new System.Drawing.Size(1160, 350);
            this.adjustmentsGroup.TabIndex = 2;
            this.adjustmentsGroup.TabStop = false;
            this.adjustmentsGroup.Text = "📋 Stock Adjustments";
            // 
            // dgvAdjustments
            // 
            this.dgvAdjustments.AllowUserToAddRows = false;
            this.dgvAdjustments.AllowUserToDeleteRows = false;
            this.dgvAdjustments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdjustments.BackgroundColor = System.Drawing.Color.White;
            this.dgvAdjustments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAdjustments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdjustments.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAdjustments.Location = new System.Drawing.Point(20, 30);
            this.dgvAdjustments.MultiSelect = false;
            this.dgvAdjustments.Name = "dgvAdjustments";
            this.dgvAdjustments.ReadOnly = true;
            this.dgvAdjustments.RowHeadersVisible = false;
            this.dgvAdjustments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdjustments.Size = new System.Drawing.Size(1120, 300);
            this.dgvAdjustments.TabIndex = 0;
            this.dgvAdjustments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AdjustmentsGrid_CellClick);
            // 
            // StockAdjustmentForm
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
            this.Name = "StockAdjustmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Adjustment - Distribution Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.adjustmentGroup.ResumeLayout(false);
            this.adjustmentGroup.PerformLayout();
            this.actionsGroup.ResumeLayout(false);
            this.adjustmentsGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdjustments)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox adjustmentGroup;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblAdjustmentType;
        private System.Windows.Forms.ComboBox cmbAdjustmentType;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblCurrentStock;
        private System.Windows.Forms.TextBox txtPhysicalStock;
        private System.Windows.Forms.Label lblDifference;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpAdjustmentDate;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox adjustmentsGroup;
        private System.Windows.Forms.DataGridView dgvAdjustments;
    }
}
