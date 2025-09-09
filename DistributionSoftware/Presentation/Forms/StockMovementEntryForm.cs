using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.Models;
using DistributionSoftware.Common;
using System.Diagnostics;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class StockMovementEntryForm : Form
    {
        private IStockMovementService _stockMovementService;
        private List<Product> _products;
        private List<Warehouse> _warehouses;
        private List<User> _users;

        public StockMovementEntryForm()
        {
            try
            {
                Debug.WriteLine("StockMovementEntryForm: Initializing form");
                InitializeComponent();
                InitializeServices();
                LoadInitialData();
                Debug.WriteLine("StockMovementEntryForm: Form initialized successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"StockMovementEntryForm: Initialization error - {ex.Message}");
                MessageBox.Show($"Error initializing Stock Movement Entry Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeServices()
        {
            _stockMovementService = new StockMovementService();
        }


        private void LoadInitialData()
        {
            try
            {
                LoadMovementTypes();
                LoadProducts();
                LoadWarehouses();
                LoadReferenceTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMovementTypes()
        {
            try
            {
                var movementTypes = _stockMovementService.GetMovementTypes();
                cmbMovementType.Items.Clear();
                
                foreach (var type in movementTypes)
                {
                    cmbMovementType.Items.Add(type);
                }
                
                if (cmbMovementType.Items.Count > 0)
                {
                    cmbMovementType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading movement types: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.DistributionConnectionString))
                {
                    var query = "SELECT ProductId, ProductCode, ProductName FROM Products WHERE IsActive = 1 ORDER BY ProductName";
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            cmbProduct.Items.Clear();
                            
                            while (reader.Read())
                            {
                                var product = new Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductCode = reader["ProductCode"].ToString(),
                                    ProductName = reader["ProductName"].ToString()
                                };
                                
                                cmbProduct.Items.Add(product);
                            }
                        }
                    }
                }
                
                if (cmbProduct.Items.Count > 0)
                {
                    cmbProduct.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWarehouses()
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.DistributionConnectionString))
                {
                    var query = "SELECT WarehouseId, WarehouseName FROM Warehouses WHERE IsActive = 1 ORDER BY WarehouseName";
                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            cmbWarehouse.Items.Clear();
                            
                            while (reader.Read())
                            {
                                var warehouse = new Warehouse
                                {
                                    WarehouseId = Convert.ToInt32(reader["WarehouseId"]),
                                    WarehouseName = reader["WarehouseName"].ToString()
                                };
                                
                                cmbWarehouse.Items.Add(warehouse);
                            }
                        }
                    }
                }
                
                if (cmbWarehouse.Items.Count > 0)
                {
                    cmbWarehouse.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReferenceTypes()
        {
            try
            {
                var referenceTypes = _stockMovementService.GetReferenceTypes();
                cmbReferenceType.Items.Clear();
                
                foreach (var type in referenceTypes)
                {
                    cmbReferenceType.Items.Add(type);
                }
                
                if (cmbReferenceType.Items.Count > 0)
                {
                    cmbReferenceType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reference types: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbMovementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update reference type based on movement type
            if (cmbMovementType.SelectedItem != null)
            {
                var movementType = cmbMovementType.SelectedItem.ToString();
                
                switch (movementType)
                {
                    case "IN":
                        // For IN movements, common reference types are PURCHASE, ADJUSTMENT, TRANSFER
                        cmbReferenceType.Items.Clear();
                        cmbReferenceType.Items.AddRange(new[] { "PURCHASE", "ADJUSTMENT", "TRANSFER" });
                        break;
                    case "OUT":
                        // For OUT movements, common reference types are SALES, ADJUSTMENT, TRANSFER
                        cmbReferenceType.Items.Clear();
                        cmbReferenceType.Items.AddRange(new[] { "SALES", "ADJUSTMENT", "TRANSFER" });
                        break;
                    case "ADJUSTMENT":
                        // For ADJUSTMENT movements, only ADJUSTMENT reference type
                        cmbReferenceType.Items.Clear();
                        cmbReferenceType.Items.Add("ADJUSTMENT");
                        break;
                }
                
                if (cmbReferenceType.Items.Count > 0)
                {
                    cmbReferenceType.SelectedIndex = 0;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    var movement = CreateStockMovement();
                    
                    if (_stockMovementService.AddStockMovement(movement))
                    {
                        MessageBox.Show("Stock movement saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save stock movement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving stock movement: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (cmbMovementType.SelectedItem == null)
            {
                MessageBox.Show("Please select a movement type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMovementType.Focus();
                return false;
            }

            if (cmbProduct.SelectedItem == null)
            {
                MessageBox.Show("Please select a product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProduct.Focus();
                return false;
            }

            if (cmbWarehouse.SelectedItem == null)
            {
                MessageBox.Show("Please select a warehouse.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbWarehouse.Focus();
                return false;
            }

            if (!decimal.TryParse(txtQuantity.Text, out decimal quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            if (cmbReferenceType.SelectedItem == null)
            {
                MessageBox.Show("Please select a reference type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbReferenceType.Focus();
                return false;
            }

            return true;
        }

        private StockMovement CreateStockMovement()
        {
            var product = (Product)cmbProduct.SelectedItem;
            var warehouse = (Warehouse)cmbWarehouse.SelectedItem;

            return new StockMovement
            {
                ProductId = product.ProductId,
                WarehouseId = warehouse.WarehouseId,
                MovementType = cmbMovementType.SelectedItem.ToString(),
                Quantity = decimal.Parse(txtQuantity.Text),
                ReferenceType = cmbReferenceType.SelectedItem.ToString(),
                ReferenceId = null, // Will be set when linking to actual transactions
                BatchNumber = string.IsNullOrEmpty(txtBatchNumber.Text) ? null : txtBatchNumber.Text,
                ExpiryDate = dtpExpiryDate.Checked ? dtpExpiryDate.Value.Date : (DateTime?)null,
                MovementDate = dtpMovementDate.Value,
                CreatedBy = 1, // TODO: Get from current user session
                Remarks = string.IsNullOrEmpty(txtRemarks.Text) ? null : txtRemarks.Text
            };
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            cmbMovementType.SelectedIndex = 0;
            cmbProduct.SelectedIndex = 0;
            cmbWarehouse.SelectedIndex = 0;
            txtQuantity.Clear();
            dtpMovementDate.Value = DateTime.Today;
            cmbReferenceType.SelectedIndex = 0;
            txtReferenceNumber.Clear();
            txtBatchNumber.Clear();
            dtpExpiryDate.Checked = false;
            txtRemarks.Clear();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

    // Helper classes

    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }

        public override string ToString()
        {
            return WarehouseName;
        }
    }
}
