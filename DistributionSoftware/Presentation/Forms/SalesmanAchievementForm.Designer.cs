namespace DistributionSoftware.Presentation.Forms
{
    partial class SalesmanAchievementForm
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grpTargetInfo = new System.Windows.Forms.GroupBox();
            this.lblTargetInfo = new System.Windows.Forms.Label();
            this.lblRevenueTarget = new System.Windows.Forms.Label();
            this.lblUnitTarget = new System.Windows.Forms.Label();
            this.grpAchievementData = new System.Windows.Forms.GroupBox();
            this.txtActualRevenue = new System.Windows.Forms.TextBox();
            this.lblActualRevenue = new System.Windows.Forms.Label();
            this.txtActualUnits = new System.Windows.Forms.TextBox();
            this.lblActualUnits = new System.Windows.Forms.Label();
            this.txtActualCustomers = new System.Windows.Forms.TextBox();
            this.lblActualCustomers = new System.Windows.Forms.Label();
            this.txtActualInvoices = new System.Windows.Forms.TextBox();
            this.lblActualInvoices = new System.Windows.Forms.Label();
            this.dtpAchievementDate = new System.Windows.Forms.DateTimePicker();
            this.lblAchievementDate = new System.Windows.Forms.Label();
            this.cmbAchievementPeriod = new System.Windows.Forms.ComboBox();
            this.lblAchievementPeriod = new System.Windows.Forms.Label();
            this.grpPerformanceMetrics = new System.Windows.Forms.GroupBox();
            this.lblRevenueAchievement = new System.Windows.Forms.Label();
            this.lblUnitAchievement = new System.Windows.Forms.Label();
            this.lblCustomerAchievement = new System.Windows.Forms.Label();
            this.lblInvoiceAchievement = new System.Windows.Forms.Label();
            this.lblOverallAchievement = new System.Windows.Forms.Label();
            this.grpComments = new System.Windows.Forms.GroupBox();
            this.txtAchievementNotes = new System.Windows.Forms.TextBox();
            this.lblAchievementNotes = new System.Windows.Forms.Label();
            this.txtChallenges = new System.Windows.Forms.TextBox();
            this.lblChallenges = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.grpTargetInfo.SuspendLayout();
            this.grpAchievementData.SuspendLayout();
            this.grpPerformanceMetrics.SuspendLayout();
            this.grpComments.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Salesman Achievement Tracking";
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.grpComments);
            this.pnlMain.Controls.Add(this.grpPerformanceMetrics);
            this.pnlMain.Controls.Add(this.grpAchievementData);
            this.pnlMain.Controls.Add(this.grpTargetInfo);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 60);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(800, 540);
            this.pnlMain.TabIndex = 1;
            // 
            // grpTargetInfo
            // 
            this.grpTargetInfo.Controls.Add(this.lblUnitTarget);
            this.grpTargetInfo.Controls.Add(this.lblRevenueTarget);
            this.grpTargetInfo.Controls.Add(this.lblTargetInfo);
            this.grpTargetInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTargetInfo.Location = new System.Drawing.Point(20, 20);
            this.grpTargetInfo.Name = "grpTargetInfo";
            this.grpTargetInfo.Size = new System.Drawing.Size(760, 80);
            this.grpTargetInfo.TabIndex = 0;
            this.grpTargetInfo.TabStop = false;
            this.grpTargetInfo.Text = "Target Information";
            // 
            // lblTargetInfo
            // 
            this.lblTargetInfo.AutoSize = true;
            this.lblTargetInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetInfo.Location = new System.Drawing.Point(15, 25);
            this.lblTargetInfo.Name = "lblTargetInfo";
            this.lblTargetInfo.Size = new System.Drawing.Size(200, 19);
            this.lblTargetInfo.TabIndex = 0;
            this.lblTargetInfo.Text = "Target: [Salesman] - [Period]";
            // 
            // lblRevenueTarget
            // 
            this.lblRevenueTarget.AutoSize = true;
            this.lblRevenueTarget.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRevenueTarget.Location = new System.Drawing.Point(15, 50);
            this.lblRevenueTarget.Name = "lblRevenueTarget";
            this.lblRevenueTarget.Size = new System.Drawing.Size(150, 19);
            this.lblRevenueTarget.TabIndex = 1;
            this.lblRevenueTarget.Text = "Revenue Target: $0.00";
            // 
            // lblUnitTarget
            // 
            this.lblUnitTarget.AutoSize = true;
            this.lblUnitTarget.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitTarget.Location = new System.Drawing.Point(300, 50);
            this.lblUnitTarget.Name = "lblUnitTarget";
            this.lblUnitTarget.Size = new System.Drawing.Size(120, 19);
            this.lblUnitTarget.TabIndex = 2;
            this.lblUnitTarget.Text = "Unit Target: 0";
            // 
            // grpAchievementData
            // 
            this.grpAchievementData.Controls.Add(this.cmbAchievementPeriod);
            this.grpAchievementData.Controls.Add(this.lblAchievementPeriod);
            this.grpAchievementData.Controls.Add(this.dtpAchievementDate);
            this.grpAchievementData.Controls.Add(this.lblAchievementDate);
            this.grpAchievementData.Controls.Add(this.txtActualInvoices);
            this.grpAchievementData.Controls.Add(this.lblActualInvoices);
            this.grpAchievementData.Controls.Add(this.txtActualCustomers);
            this.grpAchievementData.Controls.Add(this.lblActualCustomers);
            this.grpAchievementData.Controls.Add(this.txtActualUnits);
            this.grpAchievementData.Controls.Add(this.lblActualUnits);
            this.grpAchievementData.Controls.Add(this.txtActualRevenue);
            this.grpAchievementData.Controls.Add(this.lblActualRevenue);
            this.grpAchievementData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAchievementData.Location = new System.Drawing.Point(20, 120);
            this.grpAchievementData.Name = "grpAchievementData";
            this.grpAchievementData.Size = new System.Drawing.Size(760, 200);
            this.grpAchievementData.TabIndex = 1;
            this.grpAchievementData.TabStop = false;
            this.grpAchievementData.Text = "Achievement Data Entry";
            // 
            // txtActualRevenue
            // 
            this.txtActualRevenue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActualRevenue.Location = new System.Drawing.Point(150, 30);
            this.txtActualRevenue.Name = "txtActualRevenue";
            this.txtActualRevenue.Size = new System.Drawing.Size(150, 23);
            this.txtActualRevenue.TabIndex = 1;
            this.txtActualRevenue.TextChanged += new System.EventHandler(this.txtActualRevenue_TextChanged);
            // 
            // lblActualRevenue
            // 
            this.lblActualRevenue.AutoSize = true;
            this.lblActualRevenue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualRevenue.Location = new System.Drawing.Point(15, 33);
            this.lblActualRevenue.Name = "lblActualRevenue";
            this.lblActualRevenue.Size = new System.Drawing.Size(90, 15);
            this.lblActualRevenue.TabIndex = 0;
            this.lblActualRevenue.Text = "Actual Revenue:";
            // 
            // txtActualUnits
            // 
            this.txtActualUnits.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActualUnits.Location = new System.Drawing.Point(150, 65);
            this.txtActualUnits.Name = "txtActualUnits";
            this.txtActualUnits.Size = new System.Drawing.Size(150, 23);
            this.txtActualUnits.TabIndex = 3;
            this.txtActualUnits.TextChanged += new System.EventHandler(this.txtActualUnits_TextChanged);
            // 
            // lblActualUnits
            // 
            this.lblActualUnits.AutoSize = true;
            this.lblActualUnits.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualUnits.Location = new System.Drawing.Point(15, 68);
            this.lblActualUnits.Name = "lblActualUnits";
            this.lblActualUnits.Size = new System.Drawing.Size(80, 15);
            this.lblActualUnits.TabIndex = 2;
            this.lblActualUnits.Text = "Actual Units:";
            // 
            // txtActualCustomers
            // 
            this.txtActualCustomers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActualCustomers.Location = new System.Drawing.Point(150, 100);
            this.txtActualCustomers.Name = "txtActualCustomers";
            this.txtActualCustomers.Size = new System.Drawing.Size(150, 23);
            this.txtActualCustomers.TabIndex = 5;
            this.txtActualCustomers.TextChanged += new System.EventHandler(this.txtActualCustomers_TextChanged);
            // 
            // lblActualCustomers
            // 
            this.lblActualCustomers.AutoSize = true;
            this.lblActualCustomers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualCustomers.Location = new System.Drawing.Point(15, 103);
            this.lblActualCustomers.Name = "lblActualCustomers";
            this.lblActualCustomers.Size = new System.Drawing.Size(110, 15);
            this.lblActualCustomers.TabIndex = 4;
            this.lblActualCustomers.Text = "Actual Customers:";
            // 
            // txtActualInvoices
            // 
            this.txtActualInvoices.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActualInvoices.Location = new System.Drawing.Point(150, 135);
            this.txtActualInvoices.Name = "txtActualInvoices";
            this.txtActualInvoices.Size = new System.Drawing.Size(150, 23);
            this.txtActualInvoices.TabIndex = 7;
            this.txtActualInvoices.TextChanged += new System.EventHandler(this.txtActualInvoices_TextChanged);
            // 
            // lblActualInvoices
            // 
            this.lblActualInvoices.AutoSize = true;
            this.lblActualInvoices.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualInvoices.Location = new System.Drawing.Point(15, 138);
            this.lblActualInvoices.Name = "lblActualInvoices";
            this.lblActualInvoices.Size = new System.Drawing.Size(100, 15);
            this.lblActualInvoices.TabIndex = 6;
            this.lblActualInvoices.Text = "Actual Invoices:";
            // 
            // dtpAchievementDate
            // 
            this.dtpAchievementDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAchievementDate.Location = new System.Drawing.Point(450, 30);
            this.dtpAchievementDate.Name = "dtpAchievementDate";
            this.dtpAchievementDate.Size = new System.Drawing.Size(200, 23);
            this.dtpAchievementDate.TabIndex = 9;
            // 
            // lblAchievementDate
            // 
            this.lblAchievementDate.AutoSize = true;
            this.lblAchievementDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAchievementDate.Location = new System.Drawing.Point(350, 33);
            this.lblAchievementDate.Name = "lblAchievementDate";
            this.lblAchievementDate.Size = new System.Drawing.Size(100, 15);
            this.lblAchievementDate.TabIndex = 8;
            this.lblAchievementDate.Text = "Achievement Date:";
            // 
            // cmbAchievementPeriod
            // 
            this.cmbAchievementPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAchievementPeriod.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAchievementPeriod.FormattingEnabled = true;
            this.cmbAchievementPeriod.Location = new System.Drawing.Point(450, 65);
            this.cmbAchievementPeriod.Name = "cmbAchievementPeriod";
            this.cmbAchievementPeriod.Size = new System.Drawing.Size(200, 23);
            this.cmbAchievementPeriod.TabIndex = 11;
            // 
            // lblAchievementPeriod
            // 
            this.lblAchievementPeriod.AutoSize = true;
            this.lblAchievementPeriod.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAchievementPeriod.Location = new System.Drawing.Point(350, 68);
            this.lblAchievementPeriod.Name = "lblAchievementPeriod";
            this.lblAchievementPeriod.Size = new System.Drawing.Size(110, 15);
            this.lblAchievementPeriod.TabIndex = 10;
            this.lblAchievementPeriod.Text = "Achievement Period:";
            // 
            // grpPerformanceMetrics
            // 
            this.grpPerformanceMetrics.Controls.Add(this.lblOverallAchievement);
            this.grpPerformanceMetrics.Controls.Add(this.lblInvoiceAchievement);
            this.grpPerformanceMetrics.Controls.Add(this.lblCustomerAchievement);
            this.grpPerformanceMetrics.Controls.Add(this.lblUnitAchievement);
            this.grpPerformanceMetrics.Controls.Add(this.lblRevenueAchievement);
            this.grpPerformanceMetrics.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPerformanceMetrics.Location = new System.Drawing.Point(20, 340);
            this.grpPerformanceMetrics.Name = "grpPerformanceMetrics";
            this.grpPerformanceMetrics.Size = new System.Drawing.Size(760, 120);
            this.grpPerformanceMetrics.TabIndex = 2;
            this.grpPerformanceMetrics.TabStop = false;
            this.grpPerformanceMetrics.Text = "Performance Metrics (Auto-Calculated)";
            // 
            // lblRevenueAchievement
            // 
            this.lblRevenueAchievement.AutoSize = true;
            this.lblRevenueAchievement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRevenueAchievement.Location = new System.Drawing.Point(15, 25);
            this.lblRevenueAchievement.Name = "lblRevenueAchievement";
            this.lblRevenueAchievement.Size = new System.Drawing.Size(150, 15);
            this.lblRevenueAchievement.TabIndex = 0;
            this.lblRevenueAchievement.Text = "Revenue Achievement: 0%";
            // 
            // lblUnitAchievement
            // 
            this.lblUnitAchievement.AutoSize = true;
            this.lblUnitAchievement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitAchievement.Location = new System.Drawing.Point(15, 50);
            this.lblUnitAchievement.Name = "lblUnitAchievement";
            this.lblUnitAchievement.Size = new System.Drawing.Size(130, 15);
            this.lblUnitAchievement.TabIndex = 1;
            this.lblUnitAchievement.Text = "Unit Achievement: 0%";
            // 
            // lblCustomerAchievement
            // 
            this.lblCustomerAchievement.AutoSize = true;
            this.lblCustomerAchievement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerAchievement.Location = new System.Drawing.Point(350, 25);
            this.lblCustomerAchievement.Name = "lblCustomerAchievement";
            this.lblCustomerAchievement.Size = new System.Drawing.Size(160, 15);
            this.lblCustomerAchievement.TabIndex = 2;
            this.lblCustomerAchievement.Text = "Customer Achievement: 0%";
            // 
            // lblInvoiceAchievement
            // 
            this.lblInvoiceAchievement.AutoSize = true;
            this.lblInvoiceAchievement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceAchievement.Location = new System.Drawing.Point(350, 50);
            this.lblInvoiceAchievement.Name = "lblInvoiceAchievement";
            this.lblInvoiceAchievement.Size = new System.Drawing.Size(150, 15);
            this.lblInvoiceAchievement.TabIndex = 3;
            this.lblInvoiceAchievement.Text = "Invoice Achievement: 0%";
            // 
            // lblOverallAchievement
            // 
            this.lblOverallAchievement.AutoSize = true;
            this.lblOverallAchievement.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverallAchievement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblOverallAchievement.Location = new System.Drawing.Point(15, 85);
            this.lblOverallAchievement.Name = "lblOverallAchievement";
            this.lblOverallAchievement.Size = new System.Drawing.Size(180, 19);
            this.lblOverallAchievement.TabIndex = 4;
            this.lblOverallAchievement.Text = "Overall Achievement: 0%";
            // 
            // grpComments
            // 
            this.grpComments.Controls.Add(this.txtChallenges);
            this.grpComments.Controls.Add(this.lblChallenges);
            this.grpComments.Controls.Add(this.txtAchievementNotes);
            this.grpComments.Controls.Add(this.lblAchievementNotes);
            this.grpComments.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpComments.Location = new System.Drawing.Point(20, 480);
            this.grpComments.Name = "grpComments";
            this.grpComments.Size = new System.Drawing.Size(760, 120);
            this.grpComments.TabIndex = 3;
            this.grpComments.TabStop = false;
            this.grpComments.Text = "Comments & Notes";
            // 
            // txtAchievementNotes
            // 
            this.txtAchievementNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAchievementNotes.Location = new System.Drawing.Point(15, 45);
            this.txtAchievementNotes.Multiline = true;
            this.txtAchievementNotes.Name = "txtAchievementNotes";
            this.txtAchievementNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAchievementNotes.Size = new System.Drawing.Size(350, 60);
            this.txtAchievementNotes.TabIndex = 1;
            // 
            // lblAchievementNotes
            // 
            this.lblAchievementNotes.AutoSize = true;
            this.lblAchievementNotes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAchievementNotes.Location = new System.Drawing.Point(15, 25);
            this.lblAchievementNotes.Name = "lblAchievementNotes";
            this.lblAchievementNotes.Size = new System.Drawing.Size(120, 15);
            this.lblAchievementNotes.TabIndex = 0;
            this.lblAchievementNotes.Text = "Achievement Notes:";
            // 
            // txtChallenges
            // 
            this.txtChallenges.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChallenges.Location = new System.Drawing.Point(395, 45);
            this.txtChallenges.Multiline = true;
            this.txtChallenges.Name = "txtChallenges";
            this.txtChallenges.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChallenges.Size = new System.Drawing.Size(350, 60);
            this.txtChallenges.TabIndex = 3;
            // 
            // lblChallenges
            // 
            this.lblChallenges.AutoSize = true;
            this.lblChallenges.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChallenges.Location = new System.Drawing.Point(395, 25);
            this.lblChallenges.Name = "lblChallenges";
            this.lblChallenges.Size = new System.Drawing.Size(70, 15);
            this.lblChallenges.TabIndex = 2;
            this.lblChallenges.Text = "Challenges:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 600);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(800, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(620, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(710, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SalesmanAchievementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 660);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "SalesmanAchievementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Salesman Achievement Tracking";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;this.Load += new System.EventHandler(this.SalesmanAchievementForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.grpTargetInfo.ResumeLayout(false);
            this.grpTargetInfo.PerformLayout();
            this.grpAchievementData.ResumeLayout(false);
            this.grpAchievementData.PerformLayout();
            this.grpPerformanceMetrics.ResumeLayout(false);
            this.grpPerformanceMetrics.PerformLayout();
            this.grpComments.ResumeLayout(false);
            this.grpComments.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox grpTargetInfo;
        private System.Windows.Forms.Label lblTargetInfo;
        private System.Windows.Forms.Label lblRevenueTarget;
        private System.Windows.Forms.Label lblUnitTarget;
        private System.Windows.Forms.GroupBox grpAchievementData;
        private System.Windows.Forms.TextBox txtActualRevenue;
        private System.Windows.Forms.Label lblActualRevenue;
        private System.Windows.Forms.TextBox txtActualUnits;
        private System.Windows.Forms.Label lblActualUnits;
        private System.Windows.Forms.TextBox txtActualCustomers;
        private System.Windows.Forms.Label lblActualCustomers;
        private System.Windows.Forms.TextBox txtActualInvoices;
        private System.Windows.Forms.Label lblActualInvoices;
        private System.Windows.Forms.DateTimePicker dtpAchievementDate;
        private System.Windows.Forms.Label lblAchievementDate;
        private System.Windows.Forms.ComboBox cmbAchievementPeriod;
        private System.Windows.Forms.Label lblAchievementPeriod;
        private System.Windows.Forms.GroupBox grpPerformanceMetrics;
        private System.Windows.Forms.Label lblRevenueAchievement;
        private System.Windows.Forms.Label lblUnitAchievement;
        private System.Windows.Forms.Label lblCustomerAchievement;
        private System.Windows.Forms.Label lblInvoiceAchievement;
        private System.Windows.Forms.Label lblOverallAchievement;
        private System.Windows.Forms.GroupBox grpComments;
        private System.Windows.Forms.TextBox txtAchievementNotes;
        private System.Windows.Forms.Label lblAchievementNotes;
        private System.Windows.Forms.TextBox txtChallenges;
        private System.Windows.Forms.Label lblChallenges;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}

