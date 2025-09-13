using System;
using System.Globalization;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SalesmanAchievementForm : Form
    {
        private readonly SalesmanTarget _target;
        private readonly ISalesmanTargetService _salesmanTargetService;
        private SalesmanTargetAchievement _achievement;
        private bool _isEditMode = false;

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
                Status = "DRAFT",
                CreatedBy = Common.UserSession.CurrentUserId,
                CreatedDate = DateTime.Now
            };
        }

        private void SalesmanAchievementForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTargetInfo();
                LoadAchievementPeriods();
                SetDefaultValues();
                CalculatePerformanceMetrics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTargetInfo()
        {
            lblTargetInfo.Text = $"Target: {_target.SalesmanName} - {_target.TargetPeriodName}";
            lblRevenueTarget.Text = $"Revenue Target: {_target.RevenueTarget:N2}";
            lblUnitTarget.Text = $"Unit Target: {_target.UnitTarget}";
        }

        private void LoadAchievementPeriods()
        {
            var periods = new[]
            {
                new { Value = "DAILY", Text = "Daily" },
                new { Value = "WEEKLY", Text = "Weekly" },
                new { Value = "MONTHLY", Text = "Monthly" },
                new { Value = "QUARTERLY", Text = "Quarterly" },
                new { Value = "YEARLY", Text = "Yearly" }
            };

            cmbAchievementPeriod.DataSource = periods;
            cmbAchievementPeriod.DisplayMember = "Text";
            cmbAchievementPeriod.ValueMember = "Value";
            cmbAchievementPeriod.SelectedValue = "DAILY";
        }

        private void SetDefaultValues()
        {
            dtpAchievementDate.Value = DateTime.Now;
            txtActualRevenue.Text = "0";
            txtActualUnits.Text = "0";
            txtActualCustomers.Text = "0";
            txtActualInvoices.Text = "0";
            txtAchievementNotes.Text = "";
            txtChallenges.Text = "";
        }

        private void txtActualRevenue_TextChanged(object sender, EventArgs e)
        {
            CalculatePerformanceMetrics();
        }

        private void txtActualUnits_TextChanged(object sender, EventArgs e)
        {
            CalculatePerformanceMetrics();
        }

        private void txtActualCustomers_TextChanged(object sender, EventArgs e)
        {
            CalculatePerformanceMetrics();
        }

        private void txtActualInvoices_TextChanged(object sender, EventArgs e)
        {
            CalculatePerformanceMetrics();
        }

        private void CalculatePerformanceMetrics()
        {
            try
            {
                // Parse actual values
                decimal actualRevenue = ParseDecimal(txtActualRevenue.Text);
                int actualUnits = ParseInt(txtActualUnits.Text);
                int actualCustomers = ParseInt(txtActualCustomers.Text);
                int actualInvoices = ParseInt(txtActualInvoices.Text);

                // Calculate achievement percentages
                decimal revenueAchievement = _target.RevenueTarget > 0 ? (actualRevenue / _target.RevenueTarget) * 100 : 0;
                decimal unitAchievement = _target.UnitTarget > 0 ? ((decimal)actualUnits / _target.UnitTarget) * 100 : 0;
                decimal customerAchievement = _target.CustomerTarget > 0 ? ((decimal)actualCustomers / _target.CustomerTarget) * 100 : 0;
                decimal invoiceAchievement = _target.InvoiceTarget > 0 ? ((decimal)actualInvoices / _target.InvoiceTarget) * 100 : 0;

                // Calculate overall achievement (weighted average)
                decimal overallAchievement = (revenueAchievement + unitAchievement + customerAchievement + invoiceAchievement) / 4;

                // Update labels
                lblRevenueAchievement.Text = $"Revenue Achievement: {revenueAchievement:F1}%";
                lblUnitAchievement.Text = $"Unit Achievement: {unitAchievement:F1}%";
                lblCustomerAchievement.Text = $"Customer Achievement: {customerAchievement:F1}%";
                lblInvoiceAchievement.Text = $"Invoice Achievement: {invoiceAchievement:F1}%";
                lblOverallAchievement.Text = $"Overall Achievement: {overallAchievement:F1}%";

                // Color coding for overall achievement
                if (overallAchievement >= 100)
                {
                    lblOverallAchievement.ForeColor = System.Drawing.Color.Green;
                }
                else if (overallAchievement >= 80)
                {
                    lblOverallAchievement.ForeColor = System.Drawing.Color.Orange;
                }
                else
                {
                    lblOverallAchievement.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating performance metrics: {ex.Message}");
            }
        }

        private decimal ParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return 0;
            if (decimal.TryParse(value, NumberStyles.Currency | NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
                return result;
            return 0;
        }

        private int ParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return 0;
            if (int.TryParse(value, out int result))
                return result;
            return 0;
        }

        private bool ValidateForm()
        {
            var errors = new System.Collections.Generic.List<string>();

            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtActualRevenue.Text))
                errors.Add("Actual Revenue is required");

            if (string.IsNullOrWhiteSpace(txtActualUnits.Text))
                errors.Add("Actual Units is required");

            if (string.IsNullOrWhiteSpace(txtActualCustomers.Text))
                errors.Add("Actual Customers is required");

            if (string.IsNullOrWhiteSpace(txtActualInvoices.Text))
                errors.Add("Actual Invoices is required");

            // Validate numeric values
            if (!decimal.TryParse(txtActualRevenue.Text, out _))
                errors.Add("Actual Revenue must be a valid number");

            if (!int.TryParse(txtActualUnits.Text, out _))
                errors.Add("Actual Units must be a valid number");

            if (!int.TryParse(txtActualCustomers.Text, out _))
                errors.Add("Actual Customers must be a valid number");

            if (!int.TryParse(txtActualInvoices.Text, out _))
                errors.Add("Actual Invoices must be a valid number");

            // Validate date
            if (dtpAchievementDate.Value > DateTime.Now)
                errors.Add("Achievement Date cannot be in the future");

            if (errors.Count > 0)
            {
                MessageBox.Show($"Please fix the following errors:\n\n• {string.Join("\n• ", errors)}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void PrepareAchievement()
        {
            _achievement.ActualRevenue = ParseDecimal(txtActualRevenue.Text);
            _achievement.ActualUnits = ParseInt(txtActualUnits.Text);
            _achievement.ActualCustomers = ParseInt(txtActualCustomers.Text);
            _achievement.ActualInvoices = ParseInt(txtActualInvoices.Text);
            _achievement.AchievementDate = dtpAchievementDate.Value;
            _achievement.AchievementPeriod = cmbAchievementPeriod.SelectedValue?.ToString() ?? "DAILY";
            _achievement.AchievementNotes = txtAchievementNotes.Text;
            _achievement.Challenges = txtChallenges.Text;
            _achievement.Status = "SUBMITTED";
            _achievement.ModifiedDate = DateTime.Now;
            _achievement.ModifiedBy = Common.UserSession.CurrentUserId;

            // Calculate achievement percentages
            _achievement.RevenueAchievementPercentage = _target.RevenueTarget > 0 ? 
                (_achievement.ActualRevenue / _target.RevenueTarget) * 100 : 0;
            _achievement.UnitAchievementPercentage = _target.UnitTarget > 0 ? 
                ((decimal)_achievement.ActualUnits / _target.UnitTarget) * 100 : 0;
            _achievement.CustomerAchievementPercentage = _target.CustomerTarget > 0 ? 
                ((decimal)_achievement.ActualCustomers / _target.CustomerTarget) * 100 : 0;
            _achievement.InvoiceAchievementPercentage = _target.InvoiceTarget > 0 ? 
                ((decimal)_achievement.ActualInvoices / _target.InvoiceTarget) * 100 : 0;

            // Calculate overall achievement
            _achievement.OverallAchievementPercentage = (_achievement.RevenueAchievementPercentage + 
                _achievement.UnitAchievementPercentage + _achievement.CustomerAchievementPercentage + 
                _achievement.InvoiceAchievementPercentage) / 4;

            // Determine if target was met
            _achievement.IsAchievementMet = _achievement.OverallAchievementPercentage >= 100;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (ValidateForm())
                {
                    PrepareAchievement();
                    
                    
                    if (_isEditMode)
                    {
                        var success = _salesmanTargetService.UpdateSalesmanTargetAchievement(_achievement);
                        MessageBox.Show("Achievement updated successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        var achievementId = _salesmanTargetService.CreateSalesmanTargetAchievement(_achievement);
                        
                        if (achievementId > 0)
                        {
                            MessageBox.Show("Achievement recorded successfully!", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to save achievement. AchievementId = 0", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving achievement: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // Public method to load existing achievement for editing
        public void LoadAchievement(SalesmanTargetAchievement achievement)
        {
            _achievement = achievement;
            _isEditMode = true;

            // Populate form with existing data
            txtActualRevenue.Text = achievement.ActualRevenue.ToString("N2");
            txtActualUnits.Text = achievement.ActualUnits.ToString();
            txtActualCustomers.Text = achievement.ActualCustomers.ToString();
            txtActualInvoices.Text = achievement.ActualInvoices.ToString();
            dtpAchievementDate.Value = achievement.AchievementDate;
            cmbAchievementPeriod.SelectedValue = achievement.AchievementPeriod;
            txtAchievementNotes.Text = achievement.AchievementNotes ?? "";
            txtChallenges.Text = achievement.Challenges ?? "";

            // Update button text
            btnSave.Text = "Update";
            this.Text = "Edit Salesman Achievement";

            // Recalculate metrics
            CalculatePerformanceMetrics();
        }
    }
}