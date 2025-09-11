using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class RouteMasterForm
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
            this.searchGroup = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.routeListGroup = new System.Windows.Forms.GroupBox();
            this.dgvRoutes = new System.Windows.Forms.DataGridView();
            this.colRouteId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRouteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstimatedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colCreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionsGroup = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.routeGroup = new System.Windows.Forms.GroupBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.txtEstimatedTime = new System.Windows.Forms.TextBox();
            this.lblEstimatedTime = new System.Windows.Forms.Label();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.lblDistance = new System.Windows.Forms.Label();
            this.txtEndLocation = new System.Windows.Forms.TextBox();
            this.lblEndLocation = new System.Windows.Forms.Label();
            this.txtStartLocation = new System.Windows.Forms.TextBox();
            this.lblStartLocation = new System.Windows.Forms.Label();
            this.txtRouteName = new System.Windows.Forms.TextBox();
            this.lblRouteName = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.searchGroup.SuspendLayout();
            this.routeListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoutes)).BeginInit();
            this.actionsGroup.SuspendLayout();
            this.routeGroup.SuspendLayout();
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
            this.headerPanel.Size = new System.Drawing.Size(1200, 50);
            this.headerPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(15, 12);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(130, 25);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Route Master";
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(1150, 10);
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
            this.contentPanel.Controls.Add(this.routeListGroup);
            this.contentPanel.Controls.Add(this.actionsGroup);
            this.contentPanel.Controls.Add(this.routeGroup);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 50);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(10);
            this.contentPanel.Size = new System.Drawing.Size(1200, 650);
            this.contentPanel.TabIndex = 1;
            // 
            // searchGroup
            // 
            this.searchGroup.Controls.Add(this.btnSearch);
            this.searchGroup.Controls.Add(this.txtSearch);
            this.searchGroup.Controls.Add(this.lblSearch);
            this.searchGroup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.searchGroup.Location = new System.Drawing.Point(10, 10);
            this.searchGroup.Name = "searchGroup";
            this.searchGroup.Size = new System.Drawing.Size(580, 60);
            this.searchGroup.TabIndex = 0;
            this.searchGroup.TabStop = false;
            this.searchGroup.Text = "Search Routes";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(450, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(80, 22);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(350, 23);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearch_KeyPress);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.Location = new System.Drawing.Point(15, 25);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(42, 15);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search:";
            // 
            // routeListGroup
            // 
            this.routeListGroup.Controls.Add(this.dgvRoutes);
            this.routeListGroup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.routeListGroup.Location = new System.Drawing.Point(10, 80);
            this.routeListGroup.Name = "routeListGroup";
            this.routeListGroup.Size = new System.Drawing.Size(580, 300);
            this.routeListGroup.TabIndex = 1;
            this.routeListGroup.TabStop = false;
            this.routeListGroup.Text = "Routes List";
            // 
            // dgvRoutes
            // 
            this.dgvRoutes.AllowUserToAddRows = false;
            this.dgvRoutes.AllowUserToDeleteRows = false;
            this.dgvRoutes.BackgroundColor = System.Drawing.Color.White;
            this.dgvRoutes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoutes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRouteId,
            this.colRouteName,
            this.colStartLocation,
            this.colEndLocation,
            this.colDistance,
            this.colEstimatedTime,
            this.colStatus,
            this.colCreatedDate});
            this.dgvRoutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoutes.Location = new System.Drawing.Point(3, 19);
            this.dgvRoutes.MultiSelect = false;
            this.dgvRoutes.Name = "dgvRoutes";
            this.dgvRoutes.ReadOnly = true;
            this.dgvRoutes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoutes.Size = new System.Drawing.Size(574, 278);
            this.dgvRoutes.TabIndex = 0;
            this.dgvRoutes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvRoutes_CellClick);
            // 
            // colRouteId
            // 
            this.colRouteId.HeaderText = "Route ID";
            this.colRouteId.Name = "colRouteId";
            this.colRouteId.ReadOnly = true;
            this.colRouteId.Visible = false;
            // 
            // colRouteName
            // 
            this.colRouteName.HeaderText = "Route Name";
            this.colRouteName.Name = "colRouteName";
            this.colRouteName.ReadOnly = true;
            // 
            // colStartLocation
            // 
            this.colStartLocation.HeaderText = "Start Location";
            this.colStartLocation.Name = "colStartLocation";
            this.colStartLocation.ReadOnly = true;
            // 
            // colEndLocation
            // 
            this.colEndLocation.HeaderText = "End Location";
            this.colEndLocation.Name = "colEndLocation";
            this.colEndLocation.ReadOnly = true;
            // 
            // colDistance
            // 
            this.colDistance.HeaderText = "Distance";
            this.colDistance.Name = "colDistance";
            this.colDistance.ReadOnly = true;
            // 
            // colEstimatedTime
            // 
            this.colEstimatedTime.HeaderText = "Estimated Time";
            this.colEstimatedTime.Name = "colEstimatedTime";
            this.colEstimatedTime.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colCreatedDate
            // 
            this.colCreatedDate.HeaderText = "Created Date";
            this.colCreatedDate.Name = "colCreatedDate";
            this.colCreatedDate.ReadOnly = true;
            // 
            // actionsGroup
            // 
            this.actionsGroup.Controls.Add(this.btnSave);
            this.actionsGroup.Controls.Add(this.btnClear);
            this.actionsGroup.Controls.Add(this.btnDelete);
            this.actionsGroup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.actionsGroup.Location = new System.Drawing.Point(10, 390);
            this.actionsGroup.Name = "actionsGroup";
            this.actionsGroup.Size = new System.Drawing.Size(580, 60);
            this.actionsGroup.TabIndex = 2;
            this.actionsGroup.TabStop = false;
            this.actionsGroup.Text = "Actions";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(15, 20);
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
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(130, 20);
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
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(245, 20);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // routeGroup
            // 
            this.routeGroup.Controls.Add(this.chkStatus);
            this.routeGroup.Controls.Add(this.txtEstimatedTime);
            this.routeGroup.Controls.Add(this.lblEstimatedTime);
            this.routeGroup.Controls.Add(this.txtDistance);
            this.routeGroup.Controls.Add(this.lblDistance);
            this.routeGroup.Controls.Add(this.txtEndLocation);
            this.routeGroup.Controls.Add(this.lblEndLocation);
            this.routeGroup.Controls.Add(this.txtStartLocation);
            this.routeGroup.Controls.Add(this.lblStartLocation);
            this.routeGroup.Controls.Add(this.txtRouteName);
            this.routeGroup.Controls.Add(this.lblRouteName);
            this.routeGroup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.routeGroup.Location = new System.Drawing.Point(610, 10);
            this.routeGroup.Name = "routeGroup";
            this.routeGroup.Size = new System.Drawing.Size(580, 440);
            this.routeGroup.TabIndex = 3;
            this.routeGroup.TabStop = false;
            this.routeGroup.Text = "Route Information";
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Checked = true;
            this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkStatus.Location = new System.Drawing.Point(20, 350);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(60, 19);
            this.chkStatus.TabIndex = 10;
            this.chkStatus.Text = "Active";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // txtEstimatedTime
            // 
            this.txtEstimatedTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEstimatedTime.Location = new System.Drawing.Point(20, 320);
            this.txtEstimatedTime.Name = "txtEstimatedTime";
            this.txtEstimatedTime.Size = new System.Drawing.Size(200, 23);
            this.txtEstimatedTime.TabIndex = 9;
            // 
            // lblEstimatedTime
            // 
            this.lblEstimatedTime.AutoSize = true;
            this.lblEstimatedTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEstimatedTime.Location = new System.Drawing.Point(20, 300);
            this.lblEstimatedTime.Name = "lblEstimatedTime";
            this.lblEstimatedTime.Size = new System.Drawing.Size(88, 15);
            this.lblEstimatedTime.TabIndex = 8;
            this.lblEstimatedTime.Text = "Estimated Time:";
            // 
            // txtDistance
            // 
            this.txtDistance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDistance.Location = new System.Drawing.Point(20, 270);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new System.Drawing.Size(200, 23);
            this.txtDistance.TabIndex = 7;
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDistance.Location = new System.Drawing.Point(20, 250);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(55, 15);
            this.lblDistance.TabIndex = 6;
            this.lblDistance.Text = "Distance:";
            // 
            // txtEndLocation
            // 
            this.txtEndLocation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEndLocation.Location = new System.Drawing.Point(20, 220);
            this.txtEndLocation.Name = "txtEndLocation";
            this.txtEndLocation.Size = new System.Drawing.Size(540, 23);
            this.txtEndLocation.TabIndex = 5;
            // 
            // lblEndLocation
            // 
            this.lblEndLocation.AutoSize = true;
            this.lblEndLocation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEndLocation.Location = new System.Drawing.Point(20, 200);
            this.lblEndLocation.Name = "lblEndLocation";
            this.lblEndLocation.Size = new System.Drawing.Size(78, 15);
            this.lblEndLocation.TabIndex = 4;
            this.lblEndLocation.Text = "End Location:";
            // 
            // txtStartLocation
            // 
            this.txtStartLocation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtStartLocation.Location = new System.Drawing.Point(20, 170);
            this.txtStartLocation.Name = "txtStartLocation";
            this.txtStartLocation.Size = new System.Drawing.Size(540, 23);
            this.txtStartLocation.TabIndex = 3;
            // 
            // lblStartLocation
            // 
            this.lblStartLocation.AutoSize = true;
            this.lblStartLocation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStartLocation.Location = new System.Drawing.Point(20, 150);
            this.lblStartLocation.Name = "lblStartLocation";
            this.lblStartLocation.Size = new System.Drawing.Size(84, 15);
            this.lblStartLocation.TabIndex = 2;
            this.lblStartLocation.Text = "Start Location:";
            // 
            // txtRouteName
            // 
            this.txtRouteName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRouteName.Location = new System.Drawing.Point(20, 50);
            this.txtRouteName.Name = "txtRouteName";
            this.txtRouteName.Size = new System.Drawing.Size(540, 23);
            this.txtRouteName.TabIndex = 1;
            // 
            // lblRouteName
            // 
            this.lblRouteName.AutoSize = true;
            this.lblRouteName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRouteName.Location = new System.Drawing.Point(20, 30);
            this.lblRouteName.Name = "lblRouteName";
            this.lblRouteName.Size = new System.Drawing.Size(78, 15);
            this.lblRouteName.TabIndex = 0;
            this.lblRouteName.Text = "Route Name:";
            // 
            // RouteMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RouteMasterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Route Master";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.searchGroup.ResumeLayout(false);
            this.searchGroup.PerformLayout();
            this.routeListGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoutes)).EndInit();
            this.actionsGroup.ResumeLayout(false);
            this.routeGroup.ResumeLayout(false);
            this.routeGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox searchGroup;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.GroupBox routeListGroup;
        private System.Windows.Forms.DataGridView dgvRoutes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRouteId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRouteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstimatedTime;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreatedDate;
        private System.Windows.Forms.GroupBox actionsGroup;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox routeGroup;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.TextBox txtEstimatedTime;
        private System.Windows.Forms.Label lblEstimatedTime;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.TextBox txtEndLocation;
        private System.Windows.Forms.Label lblEndLocation;
        private System.Windows.Forms.TextBox txtStartLocation;
        private System.Windows.Forms.Label lblStartLocation;
        private System.Windows.Forms.TextBox txtRouteName;
        private System.Windows.Forms.Label lblRouteName;
    }
}

