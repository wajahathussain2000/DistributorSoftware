using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class PricingDiscountSetupForm : Form
    {
        private IPricingRuleService _pricingRuleService;
        private IDiscountRuleService _discountRuleService;
        private IProductService _productService;
        private ICustomerService _customerService;
        private List<PricingRule> _pricingRules;
        private List<DiscountRule> _discountRules;
        private bool _isPricingMode = true;

        public PricingDiscountSetupForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
        }

        private void InitializeServices()
        {
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var pricingRuleRepository = new PricingRuleRepository(connectionString);
            var discountRuleRepository = new DiscountRuleRepository(connectionString);
            var productRepository = new ProductRepository(connectionString);
            var customerRepository = new CustomerRepository(connectionString);
            
            _pricingRuleService = new PricingRuleService(pricingRuleRepository);
            _discountRuleService = new DiscountRuleService(discountRuleRepository);
            _productService = new ProductService(productRepository);
            _customerService = new CustomerService(customerRepository);
        }

        private void InitializeForm()
        {
            LoadProducts();
            LoadCustomers();
            LoadPricingRules();
            LoadDiscountRules();
            
            // Ensure data grids are properly positioned
            EnsureDataGridsVisible();
            
            // Initialize form state
            SwitchToPricingMode(); // Default to pricing mode
        }

        private void EnsureDataGridsVisible()
        {
            try
            {
                // Position data grids on the RIGHT side of the form
                int formWidth = this.ClientSize.Width;
                int gridWidth = formWidth - 350; // Leave space for left panel (350px)
                int gridHeight = this.ClientSize.Height - 120; // Leave space for buttons
                
                if (dgvPricingRules != null)
                {
                    dgvPricingRules.Location = new Point(350, 80);
                    dgvPricingRules.Size = new Size(gridWidth, gridHeight);
                    dgvPricingRules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
                    dgvPricingRules.BringToFront();
                    
                    // Enable horizontal scrolling
                    dgvPricingRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgvPricingRules.ScrollBars = ScrollBars.Both;
                }

                if (dgvDiscountRules != null)
                {
                    dgvDiscountRules.Location = new Point(350, 80);
                    dgvDiscountRules.Size = new Size(gridWidth, gridHeight);
                    dgvDiscountRules.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
                    dgvDiscountRules.BringToFront();
                    
                    // Enable horizontal scrolling
                    dgvDiscountRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgvDiscountRules.ScrollBars = ScrollBars.Both;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error positioning data grids: {ex.Message}");
            }
        }

        private void LoadPricingRules()
        {
            try
            {
                _pricingRules = _pricingRuleService.GetAllPricingRules();
                BindPricingRulesToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pricing rules: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDiscountRules()
        {
            try
            {
                _discountRules = _discountRuleService.GetAllDiscountRules();
                BindDiscountRulesToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading discount rules: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = _productService.GetActiveProducts();
                cmbProduct.Items.Clear();
                
                // Add "All Products" option
                cmbProduct.Items.Add(new { ProductId = (int?)null, ProductName = "All Products", ProductCode = "" });
                
                // Add products from database with null safety checks
                foreach (var product in products)
                {
                    if (product != null && product.ProductId > 0)
                    {
                        cmbProduct.Items.Add(new { 
                            ProductId = (int?)product.ProductId, 
                            ProductName = product.ProductName ?? "Unnamed Product", 
                            ProductCode = product.ProductCode ?? ""
                        });
                    }
                }
                
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ProductId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerService.GetActiveCustomers();
                cmbCustomer.Items.Clear();
                
                // Add "All Customers" option
                cmbCustomer.Items.Add(new { CustomerId = (int?)null, CustomerName = "All Customers", CustomerCode = "" });
                
                // Add customers from database
                foreach (var customer in customers)
                {
                    cmbCustomer.Items.Add(new { 
                        CustomerId = (int?)customer.CustomerId, 
                        CustomerName = customer.CustomerName ?? "", 
                        CustomerCode = customer.CustomerCode ?? ""
                    });
                }
                
                cmbCustomer.DisplayMember = "CustomerName";
                cmbCustomer.ValueMember = "CustomerId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindPricingRulesToGrid()
        {
            try
            {
                if (dgvPricingRules != null)
                {
                    // Setup columns if not already done
                    SetupPricingRulesGridColumns();
                    
                    dgvPricingRules.Rows.Clear();
                    
                    foreach (var rule in _pricingRules)
                    {
                        var productName = rule.ProductId.HasValue ? 
                            _productService.GetProductById(rule.ProductId.Value)?.ProductName ?? "Unknown Product" : 
                            "All Products";
                        
                        var customerName = rule.CustomerId.HasValue ? 
                            _customerService.GetCustomerById(rule.CustomerId.Value)?.CustomerName ?? "Unknown Customer" : 
                            "All Customers";

                        dgvPricingRules.Rows.Add(
                            rule.PricingRuleId,
                            rule.RuleName,
                            rule.Description,
                            productName,
                            customerName,
                            rule.PricingType,
                            rule.BaseValue,
                            rule.MinQuantity,
                            rule.MaxQuantity,
                            rule.Priority,
                            rule.IsActive ? "Yes" : "No",
                            rule.EffectiveFrom?.ToString("yyyy-MM-dd"),
                            rule.EffectiveTo?.ToString("yyyy-MM-dd")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error binding pricing rules to grid: {ex.Message}");
            }
        }

        private void BindDiscountRulesToGrid()
        {
            try
            {
                if (dgvDiscountRules != null)
                {
                    // Setup columns if not already done
                    SetupDiscountRulesGridColumns();
                    
                    dgvDiscountRules.Rows.Clear();
                    
                    foreach (var rule in _discountRules)
                    {
                        var productName = rule.ProductId.HasValue ? 
                            _productService.GetProductById(rule.ProductId.Value)?.ProductName ?? "Unknown Product" : 
                            "All Products";
                        
                        var customerName = rule.CustomerId.HasValue ? 
                            _customerService.GetCustomerById(rule.CustomerId.Value)?.CustomerName ?? "Unknown Customer" : 
                            "All Customers";

                        dgvDiscountRules.Rows.Add(
                            rule.DiscountRuleId,
                            rule.RuleName,
                            rule.Description,
                            productName,
                            customerName,
                            rule.DiscountType,
                            rule.DiscountValue,
                            rule.MinQuantity,
                            rule.MaxQuantity,
                            rule.MinOrderAmount,
                            rule.MaxDiscountAmount,
                            rule.Priority,
                            rule.IsActive ? "Yes" : "No",
                            rule.IsPromotional ? "Yes" : "No",
                            rule.EffectiveFrom?.ToString("yyyy-MM-dd"),
                            rule.EffectiveTo?.ToString("yyyy-MM-dd")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error binding discount rules to grid: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save functionality - will be implemented when designer files are created
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // New functionality - will be implemented when designer files are created
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Edit functionality - will be implemented when designer files are created
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete functionality - will be implemented when designer files are created
        }

        // Event handlers for designer compatibility
        private void BtnNew_Click(object sender, EventArgs e)
        {
            btnNew_Click(sender, e);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            btnCancel_Click(sender, e);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            btnDelete_Click(sender, e);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnPricingMode_Click(object sender, EventArgs e)
        {
            _isPricingMode = true;
            SwitchToPricingMode();
        }

        private void BtnDiscountMode_Click(object sender, EventArgs e)
        {
            _isPricingMode = false;
            SwitchToDiscountMode();
        }

        private void SwitchToPricingMode()
        {
            // Update button colors
            btnPricingMode.BackColor = Color.Green;
            btnDiscountMode.BackColor = Color.Gray;
            
            // Show/hide relevant controls
            lblBaseValue.Text = "Base Value:";
            cmbPricingType.Visible = true;
            cmbDiscountType.Visible = false;
            
            // Show pricing rules grid and hide discount rules grid
            if (dgvPricingRules != null) dgvPricingRules.Visible = true;
            if (dgvDiscountRules != null) dgvDiscountRules.Visible = false;
            
            // Refresh pricing rules data
            BindPricingRulesToGrid();
        }

        private void SwitchToDiscountMode()
        {
            // Update button colors
            btnPricingMode.BackColor = Color.Gray;
            btnDiscountMode.BackColor = Color.Purple;
            
            // Show/hide relevant controls
            lblBaseValue.Text = "Discount Value:";
            cmbPricingType.Visible = false;
            cmbDiscountType.Visible = true;
            
            // Show discount rules grid and hide pricing rules grid
            if (dgvPricingRules != null) dgvPricingRules.Visible = false;
            if (dgvDiscountRules != null) dgvDiscountRules.Visible = true;
            
            // Refresh discount rules data
            BindDiscountRulesToGrid();
        }

        private void DgvPricingRules_SelectionChanged(object sender, EventArgs e)
        {
            // Handle pricing rules selection - will be implemented when designer files are created
        }

        private void DgvDiscountRules_SelectionChanged(object sender, EventArgs e)
        {
            // Handle discount rules selection - will be implemented when designer files are created
        }

        private void DgvPricingRules_DataBindingComplete(object sender, EventArgs e)
        {
            // Handle pricing rules data binding complete - will be implemented when designer files are created
        }

        private void DgvDiscountRules_DataBindingComplete(object sender, EventArgs e)
        {
            // Handle discount rules data binding complete - will be implemented when designer files are created
        }

        private void SetupPricingRulesGridColumns()
        {
            if (dgvPricingRules.Columns.Count == 0)
            {
                dgvPricingRules.Columns.Add("ID", "ID");
                dgvPricingRules.Columns.Add("RuleName", "Rule Name");
                dgvPricingRules.Columns.Add("Description", "Description");
                dgvPricingRules.Columns.Add("Product", "Product");
                dgvPricingRules.Columns.Add("Customer", "Customer");
                dgvPricingRules.Columns.Add("PricingType", "Pricing Type");
                dgvPricingRules.Columns.Add("BaseValue", "Base Value");
                dgvPricingRules.Columns.Add("MinQuantity", "Min Qty");
                dgvPricingRules.Columns.Add("MaxQuantity", "Max Qty");
                dgvPricingRules.Columns.Add("Priority", "Priority");
                dgvPricingRules.Columns.Add("IsActive", "Active");
                dgvPricingRules.Columns.Add("EffectiveFrom", "From Date");
                dgvPricingRules.Columns.Add("EffectiveTo", "To Date");

                // Hide ID column
                dgvPricingRules.Columns["ID"].Visible = false;
                
                // Set column widths - wider for better readability with horizontal scroll
                dgvPricingRules.Columns["RuleName"].Width = 150;
                dgvPricingRules.Columns["Description"].Width = 200;
                dgvPricingRules.Columns["Product"].Width = 120;
                dgvPricingRules.Columns["Customer"].Width = 120;
                dgvPricingRules.Columns["PricingType"].Width = 130;
                dgvPricingRules.Columns["BaseValue"].Width = 100;
                dgvPricingRules.Columns["MinQuantity"].Width = 90;
                dgvPricingRules.Columns["MaxQuantity"].Width = 90;
                dgvPricingRules.Columns["Priority"].Width = 80;
                dgvPricingRules.Columns["IsActive"].Width = 60;
                dgvPricingRules.Columns["EffectiveFrom"].Width = 100;
                dgvPricingRules.Columns["EffectiveTo"].Width = 100;
            }
        }

        private void SetupDiscountRulesGridColumns()
        {
            if (dgvDiscountRules.Columns.Count == 0)
            {
                dgvDiscountRules.Columns.Add("ID", "ID");
                dgvDiscountRules.Columns.Add("RuleName", "Rule Name");
                dgvDiscountRules.Columns.Add("Description", "Description");
                dgvDiscountRules.Columns.Add("Product", "Product");
                dgvDiscountRules.Columns.Add("Customer", "Customer");
                dgvDiscountRules.Columns.Add("DiscountType", "Discount Type");
                dgvDiscountRules.Columns.Add("DiscountValue", "Discount Value");
                dgvDiscountRules.Columns.Add("MinQuantity", "Min Qty");
                dgvDiscountRules.Columns.Add("MaxQuantity", "Max Qty");
                dgvDiscountRules.Columns.Add("MinOrderAmount", "Min Order");
                dgvDiscountRules.Columns.Add("MaxDiscountAmount", "Max Discount");
                dgvDiscountRules.Columns.Add("Priority", "Priority");
                dgvDiscountRules.Columns.Add("IsActive", "Active");
                dgvDiscountRules.Columns.Add("IsPromotional", "Promotional");
                dgvDiscountRules.Columns.Add("EffectiveFrom", "From Date");
                dgvDiscountRules.Columns.Add("EffectiveTo", "To Date");

                // Hide ID column
                dgvDiscountRules.Columns["ID"].Visible = false;
                
                // Set column widths - wider for better readability with horizontal scroll
                dgvDiscountRules.Columns["RuleName"].Width = 150;
                dgvDiscountRules.Columns["Description"].Width = 200;
                dgvDiscountRules.Columns["Product"].Width = 120;
                dgvDiscountRules.Columns["Customer"].Width = 120;
                dgvDiscountRules.Columns["DiscountType"].Width = 130;
                dgvDiscountRules.Columns["DiscountValue"].Width = 100;
                dgvDiscountRules.Columns["MinQuantity"].Width = 90;
                dgvDiscountRules.Columns["MaxQuantity"].Width = 90;
                dgvDiscountRules.Columns["MinOrderAmount"].Width = 100;
                dgvDiscountRules.Columns["MaxDiscountAmount"].Width = 100;
                dgvDiscountRules.Columns["Priority"].Width = 80;
                dgvDiscountRules.Columns["IsActive"].Width = 60;
                dgvDiscountRules.Columns["IsPromotional"].Width = 80;
                dgvDiscountRules.Columns["EffectiveFrom"].Width = 100;
                dgvDiscountRules.Columns["EffectiveTo"].Width = 100;
            }
        }
    }
}