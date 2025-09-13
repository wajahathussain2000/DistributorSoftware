using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SalesmanTargetForm : Form
    {
        private ISalesmanTargetService _salesmanTargetService;
        private List<SalesmanTarget> _targets;
        private SalesmanTarget _currentTarget;
        private bool _isEditMode = false;
        private bool _isNewMode = false;

        public SalesmanTargetForm()
        {
            InitializeComponent();
            _salesmanTargetService = new SalesmanTargetService();
            _targets = new List<SalesmanTarget>();
        }

        private void SalesmanTargetForm_Load(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("SalesmanTargetForm_Load: Starting form initialization");
                
                // Ensure form is properly enabled and focused
                this.Enabled = true;
                this.Visible = true;
                this.BringToFront();
                this.Activate();
                this.Focus();
                this.Refresh();

                // Initialize form asynchronously to prevent UI blocking
                this.BeginInvoke(new Action(() =>
                {
                    Console.WriteLine("BeginInvoke: Starting InitializeForm");
                    InitializeForm();
                    Console.WriteLine("BeginInvoke: InitializeForm completed");
                    
                    // Test if DataGridView is properly initialized
                    Console.WriteLine($"DataGridView initialized: {dgvTargets != null}");
                    Console.WriteLine($"DataGridView enabled: {dgvTargets?.Enabled}");
                    Console.WriteLine($"DataGridView visible: {dgvTargets?.Visible}");
                    Console.WriteLine($"DataGridView row count: {dgvTargets?.Rows?.Count ?? 0}");
                    
                    // Test if DataGridView is ready
                    Console.WriteLine($"DataGridView ready for events: {dgvTargets != null}");
                    
                    // Force a test click event
                    if (dgvTargets?.Rows?.Count > 0)
                    {
                        Console.WriteLine("Testing manual event trigger...");
                        dgvTargets_CellClick(dgvTargets, new DataGridViewCellEventArgs(0, 0));
                    }
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SalesmanTargetForm_Load: {ex.Message}");
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeForm()
        {
            try
            {
                // Load dropdown data synchronously first
                LoadDropdownData();

                // Load targets asynchronously
                this.BeginInvoke(new Action(() =>
                {
                    LoadTargets();
                }));

                // Set initial form mode - start in view mode to allow DataGridView interaction
                _isNewMode = false;
                _isEditMode = false;
                SetFormMode(false);
                
                // Set default values for new targets
                SetDefaultValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDropdownData()
        {
            try
            {
                // Load Salesmen
                LoadSalesmen();

                // Load Target Types
                LoadTargetTypes();

                // Load Status options
                LoadStatusOptions();

                // Load Rating options
                LoadRatingOptions();

                // Load Period data
                LoadPeriodData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dropdown data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSalesmen()
        {
            try
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DistributionConnection"].ConnectionString;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // First ensure Salesman table exists and has data
                    var createTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Salesman' AND xtype='U')
                        BEGIN
                            CREATE TABLE Salesman (
                                SalesmanId INT IDENTITY(1,1) PRIMARY KEY,
                                SalesmanCode NVARCHAR(20) NOT NULL UNIQUE,
                                SalesmanName NVARCHAR(100) NOT NULL,
                                Email NVARCHAR(100),
                                Phone NVARCHAR(20),
                                Address NVARCHAR(500),
                                Territory NVARCHAR(100),
                                CommissionRate DECIMAL(5,2) DEFAULT 0.00,
                                IsActive BIT DEFAULT 1,
                                CreatedDate DATETIME DEFAULT GETDATE(),
                                CreatedBy INT,
                                ModifiedDate DATETIME,
                                ModifiedBy INT
                            );
                            
                            -- Insert sample data if table is empty
                            IF NOT EXISTS (SELECT 1 FROM Salesman)
                            BEGIN
                                INSERT INTO Salesman (SalesmanCode, SalesmanName, Email, Phone, Address, Territory, CommissionRate, IsActive, CreatedBy)
                                VALUES 
                                ('SM001', 'John Smith', 'john.smith@company.com', '+1-555-0101', '123 Main St, New York, NY', 'North Region', 5.50, 1, 1),
                                ('SM002', 'Sarah Johnson', 'sarah.johnson@company.com', '+1-555-0102', '456 Oak Ave, Los Angeles, CA', 'West Region', 6.00, 1, 1),
                                ('SM003', 'Michael Brown', 'michael.brown@company.com', '+1-555-0103', '789 Pine St, Chicago, IL', 'Central Region', 5.75, 1, 1),
                                ('SM004', 'Emily Davis', 'emily.davis@company.com', '+1-555-0104', '321 Elm St, Houston, TX', 'South Region', 5.25, 1, 1),
                                ('SM005', 'David Wilson', 'david.wilson@company.com', '+1-555-0105', '654 Maple Dr, Phoenix, AZ', 'Southwest Region', 6.25, 1, 1),
                                ('SM006', 'Lisa Anderson', 'lisa.anderson@company.com', '+1-555-0106', '987 Cedar Ln, Philadelphia, PA', 'Northeast Region', 5.00, 1, 1),
                                ('SM007', 'Robert Taylor', 'robert.taylor@company.com', '+1-555-0107', '147 Birch St, San Antonio, TX', 'South Region', 5.80, 1, 1),
                                ('SM008', 'Jennifer Martinez', 'jennifer.martinez@company.com', '+1-555-0108', '258 Spruce Ave, San Diego, CA', 'West Region', 6.50, 1, 1),
                                ('SM009', 'Christopher Garcia', 'christopher.garcia@company.com', '+1-555-0109', '369 Willow Rd, Dallas, TX', 'Central Region', 5.30, 1, 1),
                                ('SM010', 'Amanda Rodriguez', 'amanda.rodriguez@company.com', '+1-555-0110', '741 Poplar St, San Jose, CA', 'West Region', 6.00, 1, 1);
                            END
                        END";
                    
                    using (var createCommand = new SqlCommand(createTableQuery, connection))
                    {
                        createCommand.ExecuteNonQuery();
                    }
                    
                    // Now load salesmen from the Salesman table
                    var query = @"
                        SELECT SalesmanId, SalesmanCode, SalesmanName, Territory
                        FROM Salesman 
                        WHERE IsActive = 1
                        ORDER BY SalesmanName";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(command.ExecuteReader());
                        
                        cmbSalesman.DataSource = dataTable;
                        cmbSalesman.DisplayMember = "SalesmanName";
                        cmbSalesman.ValueMember = "SalesmanId";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading salesmen: {ex.Message}");
                
                // Provide default salesman options if database fails
                var defaultSalesmen = new List<object>
                {
                    new { SalesmanId = 1, SalesmanCode = "SM001", SalesmanName = "John Smith", Territory = "North Region" },
                    new { SalesmanId = 2, SalesmanCode = "SM002", SalesmanName = "Sarah Johnson", Territory = "West Region" },
                    new { SalesmanId = 3, SalesmanCode = "SM003", SalesmanName = "Michael Brown", Territory = "Central Region" }
                };
                
                cmbSalesman.DataSource = defaultSalesmen;
                cmbSalesman.DisplayMember = "SalesmanName";
                cmbSalesman.ValueMember = "SalesmanId";
            }
        }

        private void LoadTargetTypes()
        {
            try
            {
                var targetTypes = new List<object>
                {
                    new { Value = "MONTHLY", Text = "Monthly" },
                    new { Value = "QUARTERLY", Text = "Quarterly" },
                    new { Value = "YEARLY", Text = "Yearly" },
                    new { Value = "WEEKLY", Text = "Weekly" },
                    new { Value = "DAILY", Text = "Daily" }
                };

                cmbTargetType.DataSource = targetTypes;
                cmbTargetType.DisplayMember = "Text";
                cmbTargetType.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading target types: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatusOptions()
        {
            try
            {
                var statusOptions = new List<object>
                {
                    new { Value = "DRAFT", Text = "Draft" },
                    new { Value = "PENDING", Text = "Pending Approval" },
                    new { Value = "ACTIVE", Text = "Active" },
                    new { Value = "COMPLETED", Text = "Completed" },
                    new { Value = "CANCELLED", Text = "Cancelled" }
                };

                cmbStatus.DataSource = statusOptions;
                cmbStatus.DisplayMember = "Text";
                cmbStatus.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading status options: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRatingOptions()
        {
            try
            {
                var ratingOptions = new List<object>
                {
                    new { Value = "EXCELLENT", Text = "Excellent" },
                    new { Value = "GOOD", Text = "Good" },
                    new { Value = "AVERAGE", Text = "Average" },
                    new { Value = "POOR", Text = "Poor" }
                };

                cmbRating.DataSource = ratingOptions;
                cmbRating.DisplayMember = "Text";
                cmbRating.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rating options: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPeriodData()
        {
            try
            {
                // Load days
                var days = Enumerable.Range(1, 31).Select(d => new { Value = d, Text = d.ToString() }).ToList();
                cmbPeriodStartDay.DataSource = days;
                cmbPeriodStartDay.DisplayMember = "Text";
                cmbPeriodStartDay.ValueMember = "Value";

                cmbPeriodEndDay.DataSource = days.ToList();
                cmbPeriodEndDay.DisplayMember = "Text";
                cmbPeriodEndDay.ValueMember = "Value";

                // Load months
                var months = new List<object>
                {
                    new { Value = 1, Text = "Jan" },
                    new { Value = 2, Text = "Feb" },
                    new { Value = 3, Text = "Mar" },
                    new { Value = 4, Text = "Apr" },
                    new { Value = 5, Text = "May" },
                    new { Value = 6, Text = "Jun" },
                    new { Value = 7, Text = "Jul" },
                    new { Value = 8, Text = "Aug" },
                    new { Value = 9, Text = "Sep" },
                    new { Value = 10, Text = "Oct" },
                    new { Value = 11, Text = "Nov" },
                    new { Value = 12, Text = "Dec" }
                };

                cmbPeriodStartMonth.DataSource = months;
                cmbPeriodStartMonth.DisplayMember = "Text";
                cmbPeriodStartMonth.ValueMember = "Value";
                cmbPeriodStartMonth.SelectedIndexChanged += CmbPeriodStartMonth_SelectedIndexChanged;

                cmbPeriodEndMonth.DataSource = months.ToList();
                cmbPeriodEndMonth.DisplayMember = "Text";
                cmbPeriodEndMonth.ValueMember = "Value";
                cmbPeriodEndMonth.SelectedIndexChanged += CmbPeriodEndMonth_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading period data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTargets()
        {
            try
            {
                _targets = _salesmanTargetService.GetAllSalesmanTargets();
                BindTargetsToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading targets: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindTargetsToGrid()
        {
            try
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("TargetId", typeof(int));
                dataTable.Columns.Add("Salesman", typeof(string));
                dataTable.Columns.Add("Type", typeof(string));
                dataTable.Columns.Add("Period", typeof(string));
                dataTable.Columns.Add("Revenue Target", typeof(decimal));
                dataTable.Columns.Add("Actual Revenue", typeof(decimal));
                dataTable.Columns.Add("Achievement %", typeof(decimal));
                dataTable.Columns.Add("Bonus Amount", typeof(decimal));
                dataTable.Columns.Add("Commission", typeof(decimal));
                dataTable.Columns.Add("Status", typeof(string));

                foreach (var target in _targets)
                {
                    dataTable.Rows.Add(
                        target.TargetId,
                        target.SalesmanName ?? "Unknown",
                        target.TargetType,
                        target.TargetPeriodName,
                        target.RevenueTarget,
                        target.ActualRevenue,
                        target.OverallAchievementPercentage, // Use OverallAchievementPercentage from our query
                        target.BonusAmount,
                        target.CommissionAmount,
                        target.StatusText
                    );
                }

                dgvTargets.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error binding targets to grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFormMode(bool isEditMode)
        {
            try
            {
                _isEditMode = isEditMode;

                // Enable/disable controls based on mode
                bool enableControls = isEditMode || _isNewMode;

                // Target Information controls
                cmbSalesman.Enabled = enableControls;
                cmbTargetType.Enabled = enableControls;
                cmbPeriodStartDay.Enabled = enableControls;
                cmbPeriodStartMonth.Enabled = enableControls;
                cmbPeriodEndDay.Enabled = enableControls;
                cmbPeriodEndMonth.Enabled = enableControls;
                txtPeriodName.Enabled = enableControls;

                // Target Values controls
                txtRevenueTarget.Enabled = enableControls;
                txtUnitTarget.Enabled = enableControls;
                txtCustomerTarget.Enabled = enableControls;
                txtInvoiceTarget.Enabled = enableControls;
                txtProductCategory.Enabled = enableControls;
                txtCategoryRevenue.Enabled = enableControls;
                txtCategoryUnit.Enabled = enableControls;

                // Status Comments controls
                cmbStatus.Enabled = enableControls;
                cmbRating.Enabled = enableControls;
                chkBonusEligible.Enabled = enableControls;
                chkCommissionEligible.Enabled = enableControls;
                txtBonusAmount.Enabled = enableControls;
                txtCommissionAmount.Enabled = enableControls;
                txtManagerComments.Enabled = enableControls;
                txtSalesmanComments.Enabled = enableControls;
                txtMarketConditions.Enabled = enableControls;

                // Button states
                btnNew.Enabled = !isEditMode && !_isNewMode;
                btnEdit.Enabled = !isEditMode && !_isNewMode && dgvTargets.SelectedRows.Count > 0;
                btnDelete.Enabled = !isEditMode && !_isNewMode && dgvTargets.SelectedRows.Count > 0;
                btnSave.Enabled = isEditMode || _isNewMode;
                btnCancel.Enabled = isEditMode || _isNewMode;
                btnAdd.Enabled = !isEditMode && !_isNewMode;
                btnRefresh.Enabled = !isEditMode && !_isNewMode;
                btnExport.Enabled = !isEditMode && !_isNewMode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting form mode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultValues()
        {
            try
            {
                // Set default period dates
                if (cmbPeriodStartDay != null && cmbPeriodStartDay.Items.Count > 0)
                    cmbPeriodStartDay.SelectedIndex = 0; // First day
                if (cmbPeriodStartMonth != null && cmbPeriodStartMonth.Items.Count > 0)
                    cmbPeriodStartMonth.SelectedIndex = DateTime.Now.Month - 1; // Current month
                if (cmbPeriodEndDay != null && cmbPeriodEndDay.Items.Count > 0)
                    cmbPeriodEndDay.SelectedIndex = Math.Min(30, cmbPeriodEndDay.Items.Count - 1); // Last day of month
                if (cmbPeriodEndMonth != null && cmbPeriodEndMonth.Items.Count > 0)
                    cmbPeriodEndMonth.SelectedIndex = DateTime.Now.Month - 1; // Current month
                
                // Set default status
                if (cmbStatus != null && cmbStatus.Items.Count > 0)
                    cmbStatus.SelectedIndex = 0; // First status (Draft)
                
                // Set default rating
                if (cmbRating != null && cmbRating.Items.Count > 0)
                    cmbRating.SelectedIndex = 0; // First rating (Excellent)
                
                // Set default target type
                if (cmbTargetType != null && cmbTargetType.Items.Count > 0)
                    cmbTargetType.SelectedIndex = 0; // First type (Monthly)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting default values: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            try
            {
                if (cmbSalesman != null) cmbSalesman.SelectedIndex = -1;
                if (cmbTargetType != null) cmbTargetType.SelectedIndex = -1;
                if (cmbPeriodStartDay != null) cmbPeriodStartDay.SelectedIndex = -1;
                if (cmbPeriodStartMonth != null) cmbPeriodStartMonth.SelectedIndex = -1;
                if (cmbPeriodEndDay != null) cmbPeriodEndDay.SelectedIndex = -1;
                if (cmbPeriodEndMonth != null) cmbPeriodEndMonth.SelectedIndex = -1;
                if (txtPeriodName != null) txtPeriodName.Clear();
                if (txtRevenueTarget != null) txtRevenueTarget.Clear();
                if (txtUnitTarget != null) txtUnitTarget.Clear();
                if (txtCustomerTarget != null) txtCustomerTarget.Clear();
                if (txtInvoiceTarget != null) txtInvoiceTarget.Clear();
                if (txtProductCategory != null) txtProductCategory.Clear();
                if (txtCategoryRevenue != null) txtCategoryRevenue.Clear();
                if (txtCategoryUnit != null) txtCategoryUnit.Clear();
                if (cmbStatus != null) cmbStatus.SelectedIndex = -1;
                if (cmbRating != null) cmbRating.SelectedIndex = -1;
                if (chkBonusEligible != null) chkBonusEligible.Checked = false;
                if (chkCommissionEligible != null) chkCommissionEligible.Checked = false;
                if (txtBonusAmount != null) txtBonusAmount.Clear();
                if (txtCommissionAmount != null) txtCommissionAmount.Clear();
                if (txtManagerComments != null) txtManagerComments.Clear();
                if (txtSalesmanComments != null) txtSalesmanComments.Clear();
                if (txtMarketConditions != null) txtMarketConditions.Clear();

                _currentTarget = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateForm(SalesmanTarget target)
        {
            try
            {
                if (target == null) 
                {
                    Console.WriteLine("PopulateForm: target is null");
                    return;
                }

                Console.WriteLine($"PopulateForm: TargetId={target.TargetId}, RevenueTarget={target.RevenueTarget}");
                _currentTarget = target;

                // Target Information
                if (cmbSalesman != null) cmbSalesman.SelectedValue = target.SalesmanId;
                if (cmbTargetType != null) cmbTargetType.SelectedValue = target.TargetType;
                if (txtPeriodName != null) txtPeriodName.Text = target.TargetPeriodName;

                // Period dates
                if (target.TargetPeriodStart != DateTime.MinValue)
                {
                    if (cmbPeriodStartDay != null) cmbPeriodStartDay.SelectedValue = target.TargetPeriodStart.Day;
                    if (cmbPeriodStartMonth != null) cmbPeriodStartMonth.SelectedValue = target.TargetPeriodStart.Month;
                }
                if (target.TargetPeriodEnd != DateTime.MinValue)
                {
                    if (cmbPeriodEndDay != null) cmbPeriodEndDay.SelectedValue = target.TargetPeriodEnd.Day;
                    if (cmbPeriodEndMonth != null) cmbPeriodEndMonth.SelectedValue = target.TargetPeriodEnd.Month;
                }

                // Target Values
                Console.WriteLine($"Setting RevenueTarget: {target.RevenueTarget}");
                if (txtRevenueTarget != null) 
                {
                    txtRevenueTarget.Text = target.RevenueTarget.ToString();
                    Console.WriteLine($"txtRevenueTarget set to: {txtRevenueTarget.Text}");
                }
                else
                {
                    Console.WriteLine("txtRevenueTarget is null!");
                }
                
                if (txtUnitTarget != null) txtUnitTarget.Text = target.UnitTarget.ToString();
                if (txtCustomerTarget != null) txtCustomerTarget.Text = target.CustomerTarget.ToString();
                if (txtInvoiceTarget != null) txtInvoiceTarget.Text = target.InvoiceTarget.ToString();
                if (txtProductCategory != null) txtProductCategory.Text = target.ProductCategory ?? "";
                if (txtCategoryRevenue != null) txtCategoryRevenue.Text = target.CategoryRevenueTarget.ToString();
                if (txtCategoryUnit != null) txtCategoryUnit.Text = target.CategoryUnitTarget.ToString();

                // Status Comments
                if (cmbStatus != null) cmbStatus.SelectedValue = target.Status;
                if (cmbRating != null) cmbRating.SelectedValue = target.PerformanceRating;
                if (chkBonusEligible != null) chkBonusEligible.Checked = target.IsBonusEligible;
                if (chkCommissionEligible != null) chkCommissionEligible.Checked = target.IsCommissionEligible;
                if (txtBonusAmount != null) txtBonusAmount.Text = target.BonusAmount.ToString();
                if (txtCommissionAmount != null) txtCommissionAmount.Text = target.CommissionAmount.ToString();
                if (txtManagerComments != null) txtManagerComments.Text = target.ManagerComments ?? "";
                if (txtSalesmanComments != null) txtSalesmanComments.Text = target.SalesmanComments ?? "";
                if (txtMarketConditions != null) txtMarketConditions.Text = target.MarketConditions ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private SalesmanTarget GetFormData()
        {
            try
            {
                var target = _currentTarget ?? new SalesmanTarget();

                // Target Information
                if (cmbSalesman.SelectedValue != null)
                {
                    if (cmbSalesman.SelectedValue is DataRowView rowView)
                    {
                        target.SalesmanId = Convert.ToInt32(rowView["SalesmanId"]);
                    }
                    else
                    {
                        target.SalesmanId = Convert.ToInt32(cmbSalesman.SelectedValue);
                    }
                }
                if (cmbTargetType.SelectedValue != null)
                    target.TargetType = cmbTargetType.SelectedValue.ToString();
                target.TargetPeriodName = txtPeriodName.Text;

                // Period dates
                if (cmbPeriodStartDay.SelectedValue != null && cmbPeriodStartMonth.SelectedValue != null)
                {
                    try
                    {
                        var year = DateTime.Now.Year;
                        var month = Convert.ToInt32(cmbPeriodStartMonth.SelectedValue);
                        var day = Convert.ToInt32(cmbPeriodStartDay.SelectedValue);
                        
                        // Validate the date and adjust if necessary
                        var daysInMonth = DateTime.DaysInMonth(year, month);
                        if (day > daysInMonth)
                        {
                            day = daysInMonth; // Use last day of month
                        }
                        
                        target.TargetPeriodStart = new DateTime(year, month, day);
                    }
                    catch (Exception)
                    {
                        // If date creation fails, use default
                        target.TargetPeriodStart = DateTime.Now.Date;
                    }
                }
                else
                {
                    // Set default period start if not selected
                    target.TargetPeriodStart = DateTime.Now.Date;
                }
                
                if (cmbPeriodEndDay.SelectedValue != null && cmbPeriodEndMonth.SelectedValue != null)
                {
                    try
                    {
                        var year = DateTime.Now.Year;
                        var month = Convert.ToInt32(cmbPeriodEndMonth.SelectedValue);
                        var day = Convert.ToInt32(cmbPeriodEndDay.SelectedValue);
                        
                        // Validate the date and adjust if necessary
                        var daysInMonth = DateTime.DaysInMonth(year, month);
                        if (day > daysInMonth)
                        {
                            day = daysInMonth; // Use last day of month
                        }
                        
                        target.TargetPeriodEnd = new DateTime(year, month, day);
                    }
                    catch (Exception)
                    {
                        // If date creation fails, use default
                        target.TargetPeriodEnd = DateTime.Now.Date.AddMonths(1).AddDays(-1);
                    }
                }
                else
                {
                    // Set default period end if not selected (end of current month)
                    target.TargetPeriodEnd = DateTime.Now.Date.AddMonths(1).AddDays(-1);
                }

                // Target Values
                if (decimal.TryParse(txtRevenueTarget.Text, out decimal revenueTarget))
                    target.RevenueTarget = revenueTarget;
                if (int.TryParse(txtUnitTarget.Text, out int unitTarget))
                    target.UnitTarget = unitTarget;
                if (int.TryParse(txtCustomerTarget.Text, out int customerTarget))
                    target.CustomerTarget = customerTarget;
                if (int.TryParse(txtInvoiceTarget.Text, out int invoiceTarget))
                    target.InvoiceTarget = invoiceTarget;
                target.ProductCategory = txtProductCategory.Text;
                if (decimal.TryParse(txtCategoryRevenue.Text, out decimal categoryRevenue))
                    target.CategoryRevenueTarget = categoryRevenue;
                if (int.TryParse(txtCategoryUnit.Text, out int categoryUnit))
                    target.CategoryUnitTarget = categoryUnit;

                // Status Comments
                if (cmbStatus.SelectedValue != null)
                    target.Status = cmbStatus.SelectedValue.ToString();
                if (cmbRating.SelectedValue != null)
                    target.PerformanceRating = cmbRating.SelectedValue.ToString();
                target.IsBonusEligible = chkBonusEligible.Checked;
                target.IsCommissionEligible = chkCommissionEligible.Checked;
                if (decimal.TryParse(txtBonusAmount.Text, out decimal bonusAmount))
                    target.BonusAmount = bonusAmount;
                if (decimal.TryParse(txtCommissionAmount.Text, out decimal commissionAmount))
                    target.CommissionAmount = commissionAmount;
                target.ManagerComments = txtManagerComments.Text;
                target.SalesmanComments = txtSalesmanComments.Text;
                target.MarketConditions = txtMarketConditions.Text;

                // Set created/modified info
                if (target.TargetId == 0)
                {
                    target.CreatedBy = UserSession.CurrentUserId;
                    target.CreatedDate = DateTime.Now;
                }
                else
                {
                    target.ModifiedBy = UserSession.CurrentUserId;
                    target.ModifiedDate = DateTime.Now;
                }

                return target;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting form data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        #region Event Handlers

        private void dgvTargets_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine($"SelectionChanged: SelectedRows={dgvTargets.SelectedRows.Count}, EditMode={_isEditMode}, NewMode={_isNewMode}");
                
                if (dgvTargets.SelectedRows.Count > 0 && !_isEditMode && !_isNewMode)
                {
                    var selectedRow = dgvTargets.SelectedRows[0];
                    var targetId = Convert.ToInt32(selectedRow.Cells["TargetId"].Value);
                    var target = _targets.FirstOrDefault(t => t.TargetId == targetId);
                    
                    Console.WriteLine($"Found target: {target != null}, TargetId: {targetId}");
                    
                    if (target != null)
                    {
                        PopulateForm(target);
                        Console.WriteLine("Form populated successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SelectionChanged: {ex.Message}");
                MessageBox.Show($"Error handling selection change: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTargets_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvTargets.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvTargets.SelectedRows[0];
                    if (selectedRow.Cells["TargetId"].Value != null)
                    {
                        int targetId = Convert.ToInt32(selectedRow.Cells["TargetId"].Value);
                        var target = _targets.FirstOrDefault(t => t.TargetId == targetId);
                        
                        if (target != null)
                        {
                            PopulateForm(target);
                            SetFormMode(true); // Enable edit mode
                            MessageBox.Show("Target loaded for editing. Make your changes and click Save.", "Edit Mode", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a target to edit.", "No Selection", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading target for editing: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTargets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Console.WriteLine($"Cell clicked: Row={e.RowIndex}, Column={e.ColumnIndex}");
                
                // Only handle clicks on rows (not headers)
                if (e.RowIndex >= 0 && !_isEditMode && !_isNewMode)
                {
                    var selectedRow = dgvTargets.Rows[e.RowIndex];
                    var targetId = Convert.ToInt32(selectedRow.Cells["TargetId"].Value);
                    var target = _targets.FirstOrDefault(t => t.TargetId == targetId);
                    
                    Console.WriteLine($"Selected TargetId: {targetId}, Target found: {target != null}");
                    
                    if (target != null)
                    {
                        PopulateForm(target);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in cell click: {ex.Message}");
                MessageBox.Show($"Error handling cell click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ClearForm();
                _isNewMode = true;
                _isEditMode = false;
                SetFormMode(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating new target: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTargets.SelectedRows.Count > 0)
                {
                    _isNewMode = false;
                    _isEditMode = true;
                    SetFormMode(true);
                }
                else
                {
                    MessageBox.Show("Please select a target to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing target: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTargets.SelectedRows.Count > 0)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this target?", "Confirm Delete", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        var selectedRow = dgvTargets.SelectedRows[0];
                        var targetId = Convert.ToInt32(selectedRow.Cells["TargetId"].Value);
                        
                        if (_salesmanTargetService.DeleteSalesmanTarget(targetId))
                        {
                            MessageBox.Show("Target deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTargets();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete target.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a target to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting target: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var target = GetFormData();
                if (target == null) return;

                // Validate required fields
                if (target.SalesmanId <= 0)
                {
                    MessageBox.Show("Please select a salesman.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(target.TargetType))
                {
                    MessageBox.Show("Please select a target type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (target.RevenueTarget <= 0 && target.UnitTarget <= 0)
                {
                    MessageBox.Show("Please enter at least one target value (Revenue or Unit).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Additional validation for period dates
                if (target.TargetPeriodStart >= target.TargetPeriodEnd)
                {
                    MessageBox.Show("Period start date must be before period end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success;
                if (_isNewMode)
                {
                    var newTargetId = _salesmanTargetService.CreateSalesmanTarget(target);
                    success = newTargetId > 0;
                }
                else
                {
                    success = _salesmanTargetService.UpdateSalesmanTarget(target);
                }

                if (success)
                {
                    MessageBox.Show("Target saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTargets();
                    _isNewMode = false;
                    _isEditMode = false;
                    SetFormMode(false);
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to save target.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving target: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                _isNewMode = false;
                _isEditMode = false;
                SetFormMode(false);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error canceling operation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a target is selected
                if (dgvTargets.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a target to add achievement.", "No Selection", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the selected target
                var selectedRow = dgvTargets.SelectedRows[0];
                var targetId = Convert.ToInt32(selectedRow.Cells["TargetId"].Value);
                
                // Find the target object
                var target = _targets.FirstOrDefault(t => t.TargetId == targetId);
                if (target == null)
                {
                    MessageBox.Show("Selected target not found.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Open achievement form
                using (var achievementForm = new SalesmanAchievementForm(target, _salesmanTargetService))
                {
                    if (achievementForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the targets list to show updated achievement data
                        LoadTargets();
                        MessageBox.Show("Achievement added successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding achievement: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadTargets();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Export functionality can be implemented here
                MessageBox.Show("Export functionality will be implemented.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbPeriodStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPeriodStartMonth.SelectedValue != null)
                {
                    var month = Convert.ToInt32(cmbPeriodStartMonth.SelectedValue);
                    var year = DateTime.Now.Year;
                    var daysInMonth = DateTime.DaysInMonth(year, month);
                    
                    // Update the days dropdown to only show valid days
                    var days = Enumerable.Range(1, daysInMonth).Select(d => new { Value = d, Text = d.ToString() }).ToList();
                    cmbPeriodStartDay.DataSource = days;
                    cmbPeriodStartDay.DisplayMember = "Text";
                    cmbPeriodStartDay.ValueMember = "Value";
                    
                    // Adjust selected day if it's now invalid
                    if (cmbPeriodStartDay.SelectedValue != null)
                    {
                        var selectedDay = Convert.ToInt32(cmbPeriodStartDay.SelectedValue);
                        if (selectedDay > daysInMonth)
                        {
                            cmbPeriodStartDay.SelectedIndex = daysInMonth - 1; // Select last valid day
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Silently handle errors to avoid disrupting user experience
            }
        }

        private void CmbPeriodEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPeriodEndMonth.SelectedValue != null)
                {
                    var month = Convert.ToInt32(cmbPeriodEndMonth.SelectedValue);
                    var year = DateTime.Now.Year;
                    var daysInMonth = DateTime.DaysInMonth(year, month);
                    
                    // Update the days dropdown to only show valid days
                    var days = Enumerable.Range(1, daysInMonth).Select(d => new { Value = d, Text = d.ToString() }).ToList();
                    cmbPeriodEndDay.DataSource = days;
                    cmbPeriodEndDay.DisplayMember = "Text";
                    cmbPeriodEndDay.ValueMember = "Value";
                    
                    // Adjust selected day if it's now invalid
                    if (cmbPeriodEndDay.SelectedValue != null)
                    {
                        var selectedDay = Convert.ToInt32(cmbPeriodEndDay.SelectedValue);
                        if (selectedDay > daysInMonth)
                        {
                            cmbPeriodEndDay.SelectedIndex = daysInMonth - 1; // Select last valid day
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Silently handle errors to avoid disrupting user experience
            }
        }

        #endregion
    }
}
