using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Presentation.Forms
{
    partial class DeliveryConfirmationForm
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblFilterFrom = new System.Windows.Forms.Label();
            this.dtpFilterFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFilterTo = new System.Windows.Forms.Label();
            this.dtpFilterTo = new System.Windows.Forms.DateTimePicker();
            this.lblFilterVehicle = new System.Windows.Forms.Label();
            this.cmbFilterVehicle = new System.Windows.Forms.ComboBox();
            this.lblFilterStatus = new System.Windows.Forms.Label();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.btnNewSchedule = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblScheduleCount = new System.Windows.Forms.Label();
            this.dgvDispatchedSchedules = new System.Windows.Forms.DataGridView();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDetails = new System.Windows.Forms.TabPage();
            this.tabChallans = new System.Windows.Forms.TabPage();
            this.tabAttachments = new System.Windows.Forms.TabPage();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnMarkDelivered = new System.Windows.Forms.Button();
            this.btnMarkReturned = new System.Windows.Forms.Button();
            this.btnReopen = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDispatchedSchedules)).BeginInit();
            this.pnlDetails.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlRight);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.btnClose);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lblScheduleCount);
            this.pnlLeft.Controls.Add(this.dgvDispatchedSchedules);
            this.pnlLeft.Controls.Add(this.pnlFilter);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 40);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(700, 660);
            this.pnlLeft.TabIndex = 1;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.pnlActions);
            this.pnlRight.Controls.Add(this.pnlDetails);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(700, 40);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(500, 660);
            this.pnlRight.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Delivery Confirmation";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1150, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(950, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(120, 30);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Dispatched";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlFilter.Controls.Add(this.btnRefresh);
            this.pnlFilter.Controls.Add(this.btnNewSchedule);
            this.pnlFilter.Controls.Add(this.btnApplyFilter);
            this.pnlFilter.Controls.Add(this.cmbFilterStatus);
            this.pnlFilter.Controls.Add(this.lblFilterStatus);
            this.pnlFilter.Controls.Add(this.cmbFilterVehicle);
            this.pnlFilter.Controls.Add(this.lblFilterVehicle);
            this.pnlFilter.Controls.Add(this.dtpFilterTo);
            this.pnlFilter.Controls.Add(this.lblFilterTo);
            this.pnlFilter.Controls.Add(this.dtpFilterFrom);
            this.pnlFilter.Controls.Add(this.lblFilterFrom);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(600, 80);
            this.pnlFilter.TabIndex = 0;
            // 
            // lblFilterFrom
            // 
            this.lblFilterFrom.AutoSize = true;
            this.lblFilterFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterFrom.Location = new System.Drawing.Point(20, 15);
            this.lblFilterFrom.Name = "lblFilterFrom";
            this.lblFilterFrom.Size = new System.Drawing.Size(40, 15);
            this.lblFilterFrom.TabIndex = 0;
            this.lblFilterFrom.Text = "From:";
            // 
            // dtpFilterFrom
            // 
            this.dtpFilterFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFilterFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFilterFrom.Location = new System.Drawing.Point(70, 12);
            this.dtpFilterFrom.Name = "dtpFilterFrom";
            this.dtpFilterFrom.Size = new System.Drawing.Size(100, 23);
            this.dtpFilterFrom.TabIndex = 1;
            // 
            // lblFilterTo
            // 
            this.lblFilterTo.AutoSize = true;
            this.lblFilterTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterTo.Location = new System.Drawing.Point(190, 15);
            this.lblFilterTo.Name = "lblFilterTo";
            this.lblFilterTo.Size = new System.Drawing.Size(25, 15);
            this.lblFilterTo.TabIndex = 2;
            this.lblFilterTo.Text = "To:";
            // 
            // dtpFilterTo
            // 
            this.dtpFilterTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFilterTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFilterTo.Location = new System.Drawing.Point(220, 12);
            this.dtpFilterTo.Name = "dtpFilterTo";
            this.dtpFilterTo.Size = new System.Drawing.Size(100, 23);
            this.dtpFilterTo.TabIndex = 3;
            // 
            // lblFilterVehicle
            // 
            this.lblFilterVehicle.AutoSize = true;
            this.lblFilterVehicle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterVehicle.Location = new System.Drawing.Point(20, 45);
            this.lblFilterVehicle.Name = "lblFilterVehicle";
            this.lblFilterVehicle.Size = new System.Drawing.Size(50, 15);
            this.lblFilterVehicle.TabIndex = 4;
            this.lblFilterVehicle.Text = "Vehicle:";
            // 
            // cmbFilterVehicle
            // 
            this.cmbFilterVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterVehicle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFilterVehicle.FormattingEnabled = true;
            this.cmbFilterVehicle.Location = new System.Drawing.Point(70, 42);
            this.cmbFilterVehicle.Name = "cmbFilterVehicle";
            this.cmbFilterVehicle.Size = new System.Drawing.Size(120, 23);
            this.cmbFilterVehicle.TabIndex = 5;
            // 
            // lblFilterStatus
            // 
            this.lblFilterStatus.AutoSize = true;
            this.lblFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterStatus.Location = new System.Drawing.Point(210, 45);
            this.lblFilterStatus.Name = "lblFilterStatus";
            this.lblFilterStatus.Size = new System.Drawing.Size(45, 15);
            this.lblFilterStatus.TabIndex = 6;
            this.lblFilterStatus.Text = "Status:";
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Location = new System.Drawing.Point(260, 42);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(120, 23);
            this.cmbFilterStatus.TabIndex = 7;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnApplyFilter.FlatAppearance.BorderSize = 0;
            this.btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnApplyFilter.ForeColor = System.Drawing.Color.White;
            this.btnApplyFilter.Location = new System.Drawing.Point(400, 40);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(80, 30);
            this.btnApplyFilter.TabIndex = 8;
            this.btnApplyFilter.Text = "Apply Filter";
            this.btnApplyFilter.UseVisualStyleBackColor = false;
            this.btnApplyFilter.Click += new System.EventHandler(this.BtnApplyFilter_Click);
            // 
            // btnNewSchedule
            // 
            this.btnNewSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNewSchedule.FlatAppearance.BorderSize = 0;
            this.btnNewSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewSchedule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewSchedule.ForeColor = System.Drawing.Color.White;
            this.btnNewSchedule.Location = new System.Drawing.Point(500, 40);
            this.btnNewSchedule.Name = "btnNewSchedule";
            this.btnNewSchedule.Size = new System.Drawing.Size(100, 30);
            this.btnNewSchedule.TabIndex = 9;
            this.btnNewSchedule.Text = "New Schedule";
            this.btnNewSchedule.UseVisualStyleBackColor = false;
            this.btnNewSchedule.Click += new System.EventHandler(this.BtnNewSchedule_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(610, 40);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblScheduleCount
            // 
            this.lblScheduleCount.AutoSize = true;
            this.lblScheduleCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblScheduleCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblScheduleCount.Location = new System.Drawing.Point(20, 90);
            this.lblScheduleCount.Name = "lblScheduleCount";
            this.lblScheduleCount.Size = new System.Drawing.Size(120, 15);
            this.lblScheduleCount.TabIndex = 11;
            this.lblScheduleCount.Text = "Schedules Found: 0";
            // 
            // dgvDispatchedSchedules
            // 
            this.dgvDispatchedSchedules.AllowUserToAddRows = false;
            this.dgvDispatchedSchedules.AllowUserToDeleteRows = false;
            this.dgvDispatchedSchedules.BackgroundColor = System.Drawing.Color.White;
            this.dgvDispatchedSchedules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDispatchedSchedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDispatchedSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDispatchedSchedules.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.dgvDispatchedSchedules.Location = new System.Drawing.Point(0, 110);
            this.dgvDispatchedSchedules.MultiSelect = false;
            this.dgvDispatchedSchedules.Name = "dgvDispatchedSchedules";
            this.dgvDispatchedSchedules.ReadOnly = true;
            this.dgvDispatchedSchedules.RowHeadersVisible = false;
            this.dgvDispatchedSchedules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDispatchedSchedules.Size = new System.Drawing.Size(700, 550);
            this.dgvDispatchedSchedules.TabIndex = 1;
            this.dgvDispatchedSchedules.SelectionChanged += new System.EventHandler(this.DgvDispatchedSchedules_SelectionChanged);
            this.dgvDispatchedSchedules.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDispatchedSchedules_CellDoubleClick);
            // 
            // pnlDetails
            // 
            this.pnlDetails.Controls.Add(this.tabControl);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(500, 560);
            this.pnlDetails.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDetails);
            this.tabControl.Controls.Add(this.tabChallans);
            this.tabControl.Controls.Add(this.tabAttachments);
            this.tabControl.Controls.Add(this.tabHistory);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(500, 560);
            this.tabControl.TabIndex = 0;
            // 
            // tabDetails
            // 
            this.tabDetails.Location = new System.Drawing.Point(4, 24);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetails.Size = new System.Drawing.Size(492, 532);
            this.tabDetails.TabIndex = 0;
            this.tabDetails.Text = "Details";
            this.tabDetails.UseVisualStyleBackColor = true;
            // 
            // tabChallans
            // 
            this.tabChallans.Location = new System.Drawing.Point(4, 24);
            this.tabChallans.Name = "tabChallans";
            this.tabChallans.Padding = new System.Windows.Forms.Padding(3);
            this.tabChallans.Size = new System.Drawing.Size(492, 532);
            this.tabChallans.TabIndex = 1;
            this.tabChallans.Text = "Challans";
            this.tabChallans.UseVisualStyleBackColor = true;
            // 
            // tabAttachments
            // 
            this.tabAttachments.Location = new System.Drawing.Point(4, 24);
            this.tabAttachments.Name = "tabAttachments";
            this.tabAttachments.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttachments.Size = new System.Drawing.Size(492, 532);
            this.tabAttachments.TabIndex = 2;
            this.tabAttachments.Text = "Attachments";
            this.tabAttachments.UseVisualStyleBackColor = true;
            // 
            // tabHistory
            // 
            this.tabHistory.Location = new System.Drawing.Point(4, 24);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(492, 532);
            this.tabHistory.TabIndex = 3;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // pnlActions
            // 
            this.pnlActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlActions.Controls.Add(this.btnCancel);
            this.pnlActions.Controls.Add(this.btnReopen);
            this.pnlActions.Controls.Add(this.btnMarkReturned);
            this.pnlActions.Controls.Add(this.btnMarkDelivered);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 560);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(500, 100);
            this.pnlActions.TabIndex = 1;
            // 
            // btnMarkDelivered
            // 
            this.btnMarkDelivered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnMarkDelivered.FlatAppearance.BorderSize = 0;
            this.btnMarkDelivered.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkDelivered.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMarkDelivered.ForeColor = System.Drawing.Color.White;
            this.btnMarkDelivered.Location = new System.Drawing.Point(20, 20);
            this.btnMarkDelivered.Name = "btnMarkDelivered";
            this.btnMarkDelivered.Size = new System.Drawing.Size(120, 35);
            this.btnMarkDelivered.TabIndex = 0;
            this.btnMarkDelivered.Text = "Mark Delivered";
            this.btnMarkDelivered.UseVisualStyleBackColor = false;
            this.btnMarkDelivered.Visible = false;
            this.btnMarkDelivered.Click += new System.EventHandler(this.BtnMarkDelivered_Click);
            // 
            // btnMarkReturned
            // 
            this.btnMarkReturned.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnMarkReturned.FlatAppearance.BorderSize = 0;
            this.btnMarkReturned.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkReturned.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMarkReturned.ForeColor = System.Drawing.Color.White;
            this.btnMarkReturned.Location = new System.Drawing.Point(160, 20);
            this.btnMarkReturned.Name = "btnMarkReturned";
            this.btnMarkReturned.Size = new System.Drawing.Size(120, 35);
            this.btnMarkReturned.TabIndex = 1;
            this.btnMarkReturned.Text = "Mark Returned";
            this.btnMarkReturned.UseVisualStyleBackColor = false;
            this.btnMarkReturned.Visible = false;
            this.btnMarkReturned.Click += new System.EventHandler(this.BtnMarkReturned_Click);
            // 
            // btnReopen
            // 
            this.btnReopen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnReopen.FlatAppearance.BorderSize = 0;
            this.btnReopen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReopen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReopen.ForeColor = System.Drawing.Color.White;
            this.btnReopen.Location = new System.Drawing.Point(300, 20);
            this.btnReopen.Name = "btnReopen";
            this.btnReopen.Size = new System.Drawing.Size(120, 35);
            this.btnReopen.TabIndex = 2;
            this.btnReopen.Text = "Reopen";
            this.btnReopen.UseVisualStyleBackColor = false;
            this.btnReopen.Visible = false;
            this.btnReopen.Click += new System.EventHandler(this.BtnReopen_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(440, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // DeliveryConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DeliveryConfirmationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delivery Confirmation";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDispatchedSchedules)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblFilterFrom;
        private System.Windows.Forms.DateTimePicker dtpFilterFrom;
        private System.Windows.Forms.Label lblFilterTo;
        private System.Windows.Forms.DateTimePicker dtpFilterTo;
        private System.Windows.Forms.Label lblFilterVehicle;
        private System.Windows.Forms.ComboBox cmbFilterVehicle;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnNewSchedule;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblScheduleCount;
        private System.Windows.Forms.DataGridView dgvDispatchedSchedules;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDetails;
        private System.Windows.Forms.TabPage tabChallans;
        private System.Windows.Forms.TabPage tabAttachments;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnMarkDelivered;
        private System.Windows.Forms.Button btnMarkReturned;
        private System.Windows.Forms.Button btnReopen;
        private System.Windows.Forms.Button btnCancel;
    }
}
