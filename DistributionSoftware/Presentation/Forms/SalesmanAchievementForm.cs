using System;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SalesmanAchievementForm : Form
    {
        private readonly SalesmanTarget _target;
        private readonly ISalesmanTargetService _salesmanTargetService;
        private SalesmanTargetAchievement _achievement;

        public SalesmanAchievementForm(SalesmanTarget target, ISalesmanTargetService salesmanTargetService)
        {
            InitializeComponent();
            _target = target;
            _salesmanTargetService = salesmanTargetService;
            _achievement = new SalesmanTargetAchievement
            {
                TargetId = target.TargetId,
                SalesmanId = target.SalesmanId,
                AchievementDate = DateTime.Now,
                AchievementPeriod = "DAILY",
                CreatedBy = Common.UserSession.CurrentUserId,
                CreatedDate = DateTime.Now
            };
            
            LoadTargetInfo();
        }

        private void LoadTargetInfo()
        {
            lblTargetInfo.Text = $"Target: {_target.SalesmanName} - {_target.TargetPeriodName}";
            lblRevenueTarget.Text = $"Revenue Target: {_target.RevenueTarget:N2}";
            lblUnitTarget.Text = $"Unit Target: {_target.UnitTarget}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    PrepareAchievement();
                    _salesmanTargetService.CreateSalesmanTargetAchievement(_achievement);
                    MessageBox.Show("Achievement recorded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving achievement: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (!decimal.TryParse(txtRevenueAchieved.Text, out decimal revenue) || revenue < 0)
            {
                MessageBox.Show("Please enter a valid revenue amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRevenueAchieved.Focus();
                return false;
            }
            
            return true;
        }

        private void PrepareAchievement()
        {
            _achievement.RevenueAchieved = decimal.Parse(txtRevenueAchieved.Text);
            _achievement.UnitsSold = string.IsNullOrWhiteSpace(txtUnitsSold.Text) ? 0 : int.Parse(txtUnitsSold.Text);
            _achievement.CustomersServed = string.IsNullOrWhiteSpace(txtCustomersServed.Text) ? 0 : int.Parse(txtCustomersServed.Text);
            _achievement.InvoicesGenerated = string.IsNullOrWhiteSpace(txtInvoicesGenerated.Text) ? 0 : int.Parse(txtInvoicesGenerated.Text);
            _achievement.AchievementNotes = txtAchievementNotes.Text.Trim();
            _achievement.Challenges = txtChallenges.Text.Trim();
            _achievement.MarketConditions = txtMarketConditions.Text.Trim();
            _achievement.CustomerFeedback = txtCustomerFeedback.Text.Trim();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    partial class SalesmanAchievementForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTargetInfo;
        private System.Windows.Forms.Label lblRevenueTarget;
        private System.Windows.Forms.Label lblUnitTarget;
        private System.Windows.Forms.TextBox txtRevenueAchieved;
        private System.Windows.Forms.Label lblRevenueAchieved;
        private System.Windows.Forms.TextBox txtUnitsSold;
        private System.Windows.Forms.Label lblUnitsSold;
        private System.Windows.Forms.TextBox txtCustomersServed;
        private System.Windows.Forms.Label lblCustomersServed;
        private System.Windows.Forms.TextBox txtInvoicesGenerated;
        private System.Windows.Forms.Label lblInvoicesGenerated;
        private System.Windows.Forms.TextBox txtAchievementNotes;
        private System.Windows.Forms.Label lblAchievementNotes;
        private System.Windows.Forms.TextBox txtChallenges;
        private System.Windows.Forms.Label lblChallenges;
        private System.Windows.Forms.TextBox txtMarketConditions;
        private System.Windows.Forms.Label lblMarketConditions;
        private System.Windows.Forms.TextBox txtCustomerFeedback;
        private System.Windows.Forms.Label lblCustomerFeedback;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTargetInfo = new System.Windows.Forms.Label();
            this.lblRevenueTarget = new System.Windows.Forms.Label();
            this.lblUnitTarget = new System.Windows.Forms.Label();
            this.txtRevenueAchieved = new System.Windows.Forms.TextBox();
            this.lblRevenueAchieved = new System.Windows.Forms.Label();
            this.txtUnitsSold = new System.Windows.Forms.TextBox();
            this.lblUnitsSold = new System.Windows.Forms.Label();
            this.txtCustomersServed = new System.Windows.Forms.TextBox();
            this.lblCustomersServed = new System.Windows.Forms.Label();
            this.txtInvoicesGenerated = new System.Windows.Forms.TextBox();
            this.lblInvoicesGenerated = new System.Windows.Forms.Label();
            this.txtAchievementNotes = new System.Windows.Forms.TextBox();
            this.lblAchievementNotes = new System.Windows.Forms.Label();
            this.txtChallenges = new System.Windows.Forms.TextBox();
            this.lblChallenges = new System.Windows.Forms.Label();
            this.txtMarketConditions = new System.Windows.Forms.TextBox();
            this.lblMarketConditions = new System.Windows.Forms.Label();
            this.txtCustomerFeedback = new System.Windows.Forms.TextBox();
            this.lblCustomerFeedback = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Form setup
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SalesmanAchievementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Achievement";

            // Target Info
            this.lblTargetInfo.AutoSize = true;
            this.lblTargetInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTargetInfo.Location = new System.Drawing.Point(20, 20);
            this.lblTargetInfo.Name = "lblTargetInfo";
            this.lblTargetInfo.Size = new System.Drawing.Size(100, 19);
            this.lblTargetInfo.Text = "Target Info";

            this.lblRevenueTarget.AutoSize = true;
            this.lblRevenueTarget.Location = new System.Drawing.Point(20, 50);
            this.lblRevenueTarget.Name = "lblRevenueTarget";
            this.lblRevenueTarget.Size = new System.Drawing.Size(100, 16);
            this.lblRevenueTarget.Text = "Revenue Target:";

            this.lblUnitTarget.AutoSize = true;
            this.lblUnitTarget.Location = new System.Drawing.Point(250, 50);
            this.lblUnitTarget.Name = "lblUnitTarget";
            this.lblUnitTarget.Size = new System.Drawing.Size(80, 16);
            this.lblUnitTarget.Text = "Unit Target:";

            // Revenue Achieved
            this.lblRevenueAchieved.AutoSize = true;
            this.lblRevenueAchieved.Location = new System.Drawing.Point(20, 90);
            this.lblRevenueAchieved.Name = "lblRevenueAchieved";
            this.lblRevenueAchieved.Size = new System.Drawing.Size(120, 16);
            this.lblRevenueAchieved.Text = "Revenue Achieved: *";
            this.txtRevenueAchieved.Location = new System.Drawing.Point(150, 87);
            this.txtRevenueAchieved.Name = "txtRevenueAchieved";
            this.txtRevenueAchieved.Size = new System.Drawing.Size(120, 22);
            this.txtRevenueAchieved.TabIndex = 0;

            // Units Sold
            this.lblUnitsSold.AutoSize = true;
            this.lblUnitsSold.Location = new System.Drawing.Point(300, 90);
            this.lblUnitsSold.Name = "lblUnitsSold";
            this.lblUnitsSold.Size = new System.Drawing.Size(80, 16);
            this.lblUnitsSold.Text = "Units Sold:";
            this.txtUnitsSold.Location = new System.Drawing.Point(390, 87);
            this.txtUnitsSold.Name = "txtUnitsSold";
            this.txtUnitsSold.Size = new System.Drawing.Size(80, 22);
            this.txtUnitsSold.TabIndex = 1;

            // Customers Served
            this.lblCustomersServed.AutoSize = true;
            this.lblCustomersServed.Location = new System.Drawing.Point(20, 130);
            this.lblCustomersServed.Name = "lblCustomersServed";
            this.lblCustomersServed.Size = new System.Drawing.Size(120, 16);
            this.lblCustomersServed.Text = "Customers Served:";
            this.txtCustomersServed.Location = new System.Drawing.Point(150, 127);
            this.txtCustomersServed.Name = "txtCustomersServed";
            this.txtCustomersServed.Size = new System.Drawing.Size(80, 22);
            this.txtCustomersServed.TabIndex = 2;

            // Invoices Generated
            this.lblInvoicesGenerated.AutoSize = true;
            this.lblInvoicesGenerated.Location = new System.Drawing.Point(250, 130);
            this.lblInvoicesGenerated.Name = "lblInvoicesGenerated";
            this.lblInvoicesGenerated.Size = new System.Drawing.Size(120, 16);
            this.lblInvoicesGenerated.Text = "Invoices Generated:";
            this.txtInvoicesGenerated.Location = new System.Drawing.Point(380, 127);
            this.txtInvoicesGenerated.Name = "txtInvoicesGenerated";
            this.txtInvoicesGenerated.Size = new System.Drawing.Size(80, 22);
            this.txtInvoicesGenerated.TabIndex = 3;

            // Achievement Notes
            this.lblAchievementNotes.AutoSize = true;
            this.lblAchievementNotes.Location = new System.Drawing.Point(20, 170);
            this.lblAchievementNotes.Name = "lblAchievementNotes";
            this.lblAchievementNotes.Size = new System.Drawing.Size(120, 16);
            this.lblAchievementNotes.Text = "Achievement Notes:";
            this.txtAchievementNotes.Location = new System.Drawing.Point(20, 190);
            this.txtAchievementNotes.Multiline = true;
            this.txtAchievementNotes.Name = "txtAchievementNotes";
            this.txtAchievementNotes.Size = new System.Drawing.Size(450, 40);
            this.txtAchievementNotes.TabIndex = 4;

            // Challenges
            this.lblChallenges.AutoSize = true;
            this.lblChallenges.Location = new System.Drawing.Point(20, 240);
            this.lblChallenges.Name = "lblChallenges";
            this.lblChallenges.Size = new System.Drawing.Size(80, 16);
            this.lblChallenges.Text = "Challenges:";
            this.txtChallenges.Location = new System.Drawing.Point(20, 260);
            this.txtChallenges.Multiline = true;
            this.txtChallenges.Name = "txtChallenges";
            this.txtChallenges.Size = new System.Drawing.Size(450, 30);
            this.txtChallenges.TabIndex = 5;

            // Market Conditions
            this.lblMarketConditions.AutoSize = true;
            this.lblMarketConditions.Location = new System.Drawing.Point(20, 300);
            this.lblMarketConditions.Name = "lblMarketConditions";
            this.lblMarketConditions.Size = new System.Drawing.Size(120, 16);
            this.lblMarketConditions.Text = "Market Conditions:";
            this.txtMarketConditions.Location = new System.Drawing.Point(20, 320);
            this.txtMarketConditions.Multiline = true;
            this.txtMarketConditions.Name = "txtMarketConditions";
            this.txtMarketConditions.Size = new System.Drawing.Size(450, 30);
            this.txtMarketConditions.TabIndex = 6;

            // Customer Feedback
            this.lblCustomerFeedback.AutoSize = true;
            this.lblCustomerFeedback.Location = new System.Drawing.Point(20, 360);
            this.lblCustomerFeedback.Name = "lblCustomerFeedback";
            this.lblCustomerFeedback.Size = new System.Drawing.Size(130, 16);
            this.lblCustomerFeedback.Text = "Customer Feedback:";
            this.txtCustomerFeedback.Location = new System.Drawing.Point(20, 380);
            this.txtCustomerFeedback.Multiline = true;
            this.txtCustomerFeedback.Name = "txtCustomerFeedback";
            this.txtCustomerFeedback.Size = new System.Drawing.Size(450, 30);
            this.txtCustomerFeedback.TabIndex = 7;

            // Buttons
            this.btnSave.Location = new System.Drawing.Point(300, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.Location = new System.Drawing.Point(390, 420);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.Controls.Add(this.lblTargetInfo);
            this.Controls.Add(this.lblRevenueTarget);
            this.Controls.Add(this.lblUnitTarget);
            this.Controls.Add(this.txtRevenueAchieved);
            this.Controls.Add(this.lblRevenueAchieved);
            this.Controls.Add(this.txtUnitsSold);
            this.Controls.Add(this.lblUnitsSold);
            this.Controls.Add(this.txtCustomersServed);
            this.Controls.Add(this.lblCustomersServed);
            this.Controls.Add(this.txtInvoicesGenerated);
            this.Controls.Add(this.lblInvoicesGenerated);
            this.Controls.Add(this.txtAchievementNotes);
            this.Controls.Add(this.lblAchievementNotes);
            this.Controls.Add(this.txtChallenges);
            this.Controls.Add(this.lblChallenges);
            this.Controls.Add(this.txtMarketConditions);
            this.Controls.Add(this.lblMarketConditions);
            this.Controls.Add(this.txtCustomerFeedback);
            this.Controls.Add(this.lblCustomerFeedback);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}


