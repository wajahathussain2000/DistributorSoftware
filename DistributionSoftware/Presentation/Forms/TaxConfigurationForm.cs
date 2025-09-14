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
    public partial class TaxConfigurationForm : Form
    {
        private ITaxCategoryService _taxCategoryService;
        private ITaxRateService _taxRateService;
        private List<TaxCategory> _taxCategories;
        private List<TaxRate> _taxRates;

        public TaxConfigurationForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
        }

        private void InitializeServices()
        {
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var taxCategoryRepository = new TaxCategoryRepository(connectionString);
            var taxRateRepository = new TaxRateRepository(connectionString);
            
            _taxCategoryService = new TaxCategoryService(taxCategoryRepository);
            _taxRateService = new TaxRateService(taxRateRepository);
        }

        private void InitializeForm()
        {
            LoadTaxCategories();
            LoadTaxRates();
        }

        private void LoadTaxCategories()
        {
            try
            {
                _taxCategories = _taxCategoryService.GetAllTaxCategories();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tax categories: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTaxRates()
        {
            try
            {
                _taxRates = _taxRateService.GetAllTaxRates();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tax rates: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateGridColumns()
        {
            // UI method - will be implemented when designer files are created
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
    }
}