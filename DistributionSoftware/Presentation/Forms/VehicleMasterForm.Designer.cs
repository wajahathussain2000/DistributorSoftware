using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class VehicleMasterForm
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
            this.vehicleGroup = new System.Windows.Forms.GroupBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtTransporterName = new System.Windows.Forms.TextBox();
            this.lblTransporterName = new System.Windows.Forms.Label();
            this.txtDriverContact = new System.Windows.Forms.TextBox();
            this.lblDriverContact = new System.Windows.Forms.Label();
            this.txtDriverName = new System.Windows.Forms.TextBox();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.cmbVehicleType = new System.Windows.Forms.ComboBox();
            this.lblVehicleType = new System.Windows.Forms.Label();
            this.txtVehicleNo = new System.Windows.Forms.TextBox();
            this.lblVehicleNo = new System.Windows.Forms.Label();
            this.vehicleListGroup = new System.Windows.Forms.GroupBox();
            this.dgvVehicles = new System.Windows.Forms.DataGridView();
            this.colVehicleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehicleNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehicleType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDriverName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDriverContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransporterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colCreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.searchGroup = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.vehicleGroup.SuspendLayout();
            this.vehicleListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).BeginInit();
            this.actionsGroup.SuspendLayout();
            this.searchGroup.SuspendLayout();
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
            this.headerPanel.Size = new System.Drawing.Size(1000, 50);
            this.headerPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(15, 12);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(150, 25);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Vehicle Master";
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(950, 10);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(40, 30);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "×";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.searchGroup);
            this.contentPanel.Controls.Add(this.vehicleListGroup);
            this.contentPanel.Controls.Add(this.actionsGroup);
            this.contentPanel.Controls.Add(this.vehicleGroup);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 50);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(10);
            this.contentPanel.Size = new System.Drawing.Size(1000, 550);
            this.contentPanel.TabIndex = 1;
            // 
            // vehicleGroup
            // 
            this.vehicleGroup.Controls.Add(this.chkIsActive);
            this.vehicleGroup.Controls.Add(this.txtRemarks);
            this.vehicleGroup.Controls.Add(this.lblRemarks);
            this.vehicleGroup.Controls.Add(this.txtTransporterName);
            this.vehicleGroup.Controls.Add(this.lblTransporterName);
            this.vehicleGroup.Controls.Add(this.txtDriverContact);
            this.vehicleGroup.Controls.Add(this.lblDriverContact);
            this.vehicleGroup.Controls.Add(this.txtDriverName);
            this.vehicleGroup.Controls.Add(this.lblDriverName);
            this.vehicleGroup.Controls.Add(this.cmbVehicleType);
            this.vehicleGroup.Controls.Add(this.lblVehicleType);
            this.vehicleGroup.Controls.Add(this.txtVehicleNo);
            this.vehicleGroup.Controls.Add(this.lblVehicleNo);
            this.vehicleGroup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.vehicleGroup.Location = new System.Drawing.Point(10, 10);
            this.vehicleGroup.Name = "vehicleGroup";
            this.vehicleGroup.Size = new System.Drawing.Size(480, 280);
            this.vehicleGroup.TabIndex = 0;
            this.vehicleGroup.TabStop = false;
            this.vehicleGroup.Text = "Vehicle Information";
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkIsActive.Location = new System.Drawing.Point(20, 240);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(75, 23);
            this.chkIsActive.TabIndex = 12;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRemarks.Location = new System.Drawing.Point(120, 200);
            this.txtRemarks.MaxLength = 500;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.Size = new System.Drawing.Size(340, 30);
            this.txtRemarks.TabIndex = 11;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRemarks.Location = new System.Drawing.Point(20, 203);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(65, 19);
            this.lblRemarks.TabIndex = 10;
            this.lblRemarks.Text = "Remarks:";
            // 
            // txtTransporterName
            // 
            this.txtTransporterName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTransporterName.Location = new System.Drawing.Point(120, 160);
            this.txtTransporterName.MaxLength = 100;
            this.txtTransporterName.Name = "txtTransporterName";
            this.txtTransporterName.Size = new System.Drawing.Size(340, 25);
            this.txtTransporterName.TabIndex = 9;
            // 
            // lblTransporterName
            // 
            this.lblTransporterName.AutoSize = true;
            this.lblTransporterName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTransporterName.Location = new System.Drawing.Point(20, 163);
            this.lblTransporterName.Name = "lblTransporterName";
            this.lblTransporterName.Size = new System.Drawing.Size(120, 19);
            this.lblTransporterName.TabIndex = 8;
            this.lblTransporterName.Text = "Transporter Name:";
            // 
            // txtDriverContact
            // 
            this.txtDriverContact.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDriverContact.Location = new System.Drawing.Point(120, 120);
            this.txtDriverContact.MaxLength = 20;
            this.txtDriverContact.Name = "txtDriverContact";
            this.txtDriverContact.Size = new System.Drawing.Size(340, 25);
            this.txtDriverContact.TabIndex = 7;
            // 
            // lblDriverContact
            // 
            this.lblDriverContact.AutoSize = true;
            this.lblDriverContact.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDriverContact.Location = new System.Drawing.Point(20, 123);
            this.lblDriverContact.Name = "lblDriverContact";
            this.lblDriverContact.Size = new System.Drawing.Size(105, 19);
            this.lblDriverContact.TabIndex = 6;
            this.lblDriverContact.Text = "Driver Contact:";
            // 
            // txtDriverName
            // 
            this.txtDriverName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDriverName.Location = new System.Drawing.Point(120, 80);
            this.txtDriverName.MaxLength = 100;
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(340, 25);
            this.txtDriverName.TabIndex = 5;
            // 
            // lblDriverName
            // 
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDriverName.Location = new System.Drawing.Point(20, 83);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.Size = new System.Drawing.Size(90, 19);
            this.lblDriverName.TabIndex = 4;
            this.lblDriverName.Text = "Driver Name:";
            // 
            // cmbVehicleType
            // 
            this.cmbVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbVehicleType.FormattingEnabled = true;
            this.cmbVehicleType.Items.AddRange(new object[] {
            "Truck",
            "Van",
            "Bike",
            "Car",
            "Bus",
            "Other"});
            this.cmbVehicleType.Location = new System.Drawing.Point(120, 40);
            this.cmbVehicleType.Name = "cmbVehicleType";
            this.cmbVehicleType.Size = new System.Drawing.Size(200, 25);
            this.cmbVehicleType.TabIndex = 3;
            // 
            // lblVehicleType
            // 
            this.lblVehicleType.AutoSize = true;
            this.lblVehicleType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVehicleType.Location = new System.Drawing.Point(20, 43);
            this.lblVehicleType.Name = "lblVehicleType";
            this.lblVehicleType.Size = new System.Drawing.Size(85, 19);
            this.lblVehicleType.TabIndex = 2;
            this.lblVehicleType.Text = "Vehicle Type:";
            // 
            // txtVehicleNo
            // 
            this.txtVehicleNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtVehicleNo.Location = new System.Drawing.Point(120, 0);
            this.txtVehicleNo.MaxLength = 50;
            this.txtVehicleNo.Name = "txtVehicleNo";
            this.txtVehicleNo.Size = new System.Drawing.Size(200, 25);
            this.txtVehicleNo.TabIndex = 1;
            // 
            // lblVehicleNo
            // 
            this.lblVehicleNo.AutoSize = true;
            this.lblVehicleNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVehicleNo.Location = new System.Drawing.Point(20, 3);
            this.lblVehicleNo.Name = "lblVehicleNo";
            this.lblVehicleNo.Size = new System.Drawing.Size(80, 19);
            this.lblVehicleNo.TabIndex = 0;
            this.lblVehicleNo.Text = "Vehicle No:";
            // 
            // vehicleListGroup
            // 
            this.vehicleListGroup.Controls.Add(this.dgvVehicles);
            this.vehicleListGroup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.vehicleListGroup.Location = new System.Drawing.Point(500, 10);
            this.vehicleListGroup.Name = "vehicleListGroup";
            this.vehicleListGroup.Size = new System.Drawing.Size(490, 400);
            this.vehicleListGroup.TabIndex = 1;
            this.vehicleListGroup.TabStop = false;
            this.vehicleListGroup.Text = "Vehicle List";
            // 
            // dgvVehicles
            // 
            this.dgvVehicles.AllowUserToAddRows = false;
            this.dgvVehicles.AllowUserToDeleteRows = false;
            this.dgvVehicles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVehicles.BackgroundColor = System.Drawing.Color.White;
            this.dgvVehicles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVehicles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVehicleId,
            this.colVehicleNo,
            this.colVehicleType,
            this.colDriverName,
            this.colDriverContact,
            this.colTransporterName,
            this.colIsActive,
            this.colCreatedDate});
            this.dgvVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVehicles.Location = new System.Drawing.Point(3, 23);
            this.dgvVehicles.MultiSelect = false;
            this.dgvVehicles.Name = "dgvVehicles";
            this.dgvVehicles.ReadOnly = true;
            this.dgvVehicles.RowHeadersVisible = false;
            this.dgvVehicles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVehicles.Size = new System.Drawing.Size(484, 374);
            this.dgvVehicles.TabIndex = 0;
            this.dgvVehicles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVehicles_CellClick);
            // 
            // colVehicleId
            // 
            this.colVehicleId.DataPropertyName = "VehicleId";
            this.colVehicleId.HeaderText = "ID";
            this.colVehicleId.Name = "colVehicleId";
            this.colVehicleId.ReadOnly = true;
            this.colVehicleId.Visible = false;
            // 
            // colVehicleNo
            // 
            this.colVehicleNo.DataPropertyName = "VehicleNo";
            this.colVehicleNo.HeaderText = "Vehicle No";
            this.colVehicleNo.Name = "colVehicleNo";
            this.colVehicleNo.ReadOnly = true;
            // 
            // colVehicleType
            // 
            this.colVehicleType.DataPropertyName = "VehicleType";
            this.colVehicleType.HeaderText = "Type";
            this.colVehicleType.Name = "colVehicleType";
            this.colVehicleType.ReadOnly = true;
            // 
            // colDriverName
            // 
            this.colDriverName.DataPropertyName = "DriverName";
            this.colDriverName.HeaderText = "Driver";
            this.colDriverName.Name = "colDriverName";
            this.colDriverName.ReadOnly = true;
            // 
            // colDriverContact
            // 
            this.colDriverContact.DataPropertyName = "DriverContact";
            this.colDriverContact.HeaderText = "Contact";
            this.colDriverContact.Name = "colDriverContact";
            this.colDriverContact.ReadOnly = true;
            // 
            // colTransporterName
            // 
            this.colTransporterName.DataPropertyName = "TransporterName";
            this.colTransporterName.HeaderText = "Transporter";
            this.colTransporterName.Name = "colTransporterName";
            this.colTransporterName.ReadOnly = true;
            // 
            // colIsActive
            // 
            this.colIsActive.DataPropertyName = "IsActive";
            this.colIsActive.HeaderText = "Active";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.ReadOnly = true;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.DataPropertyName = "CreatedDate";
            this.colCreatedDate.HeaderText = "Created";
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.ReadOnly = true;
            // 
            // actionsGroup
            // 
            this.actionsGroup.Controls.Add(this.btnSave);
            this.actionsGroup.Controls.Add(this.btnClear);
            this.actionsGroup.Controls.Add(this.btnDelete);
            this.actionsGroup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.actionsGroup.Location = new System.Drawing.Point(10, 300);
            this.actionsGroup.Name = "actionsGroup";
            this.actionsGroup.Size = new System.Drawing.Size(480, 60);
            this.actionsGroup.TabIndex = 2;
            this.actionsGroup.TabStop = false;
            this.actionsGroup.Text = "Actions";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(140, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(260, 20);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // searchGroup
            // 
            this.searchGroup.Controls.Add(this.btnSearch);
            this.searchGroup.Controls.Add(this.txtSearch);
            this.searchGroup.Controls.Add(this.lblSearch);
            this.searchGroup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.searchGroup.Location = new System.Drawing.Point(500, 420);
            this.searchGroup.Name = "searchGroup";
            this.searchGroup.Size = new System.Drawing.Size(490, 60);
            this.searchGroup.TabIndex = 3;
            this.searchGroup.TabStop = false;
            this.searchGroup.Text = "Search";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(400, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(80, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearch_KeyPress);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.Location = new System.Drawing.Point(20, 23);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(50, 19);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search:";
            // 
            // VehicleMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VehicleMasterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehicle Master";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.vehicleGroup.ResumeLayout(false);
            this.vehicleGroup.PerformLayout();
            this.vehicleListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).EndInit();
            this.actionsGroup.ResumeLayout(false);
            this.searchGroup.ResumeLayout(false);
            this.searchGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox vehicleGroup;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtTransporterName;
        private System.Windows.Forms.Label lblTransporterName;
        private System.Windows.Forms.TextBox txtDriverContact;
        private System.Windows.Forms.Label lblDriverContact;
        private System.Windows.Forms.TextBox txtDriverName;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.ComboBox cmbVehicleType;
        private System.Windows.Forms.Label lblVehicleType;
        private System.Windows.Forms.TextBox txtVehicleNo;
        private System.Windows.Forms.Label lblVehicleNo;
        private System.Windows.Forms.GroupBox vehicleListGroup;
        private System.Windows.Forms.DataGridView dgvVehicles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehicleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehicleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehicleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDriverName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDriverContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransporterName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreatedDate;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox searchGroup;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
    }
}
