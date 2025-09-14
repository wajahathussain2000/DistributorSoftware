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
            LoadPricingRules();
            LoadDiscountRules();
        }

        private void LoadPricingRules()
        {
            try
            {
                _pricingRules = _pricingRuleService.GetAllPricingRules();
                // UI controls will be available when designer files are created
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
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading discount rules: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Switch to pricing mode - will be implemented when designer files are created
        }

        private void BtnDiscountMode_Click(object sender, EventArgs e)
        {
            _isPricingMode = false;
            // Switch to discount mode - will be implemented when designer files are created
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
    }
}