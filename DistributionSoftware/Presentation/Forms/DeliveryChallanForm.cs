using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class DeliveryChallanForm : Form
    {
        private IDeliveryChallanService _deliveryChallanService;
        private ISalesInvoiceRepository _salesInvoiceRepository;
        private IVehicleService _vehicleService;
        private IRouteService _routeService;
        private DeliveryChallan _currentChallan;
        private SalesInvoice _selectedSalesInvoice;
        private List<DeliveryChallanItem> _challanItems;

        public DeliveryChallanForm()
        {
            try
            {
                InitializeComponent();
                InitializeServices();
                InitializeChallan();
                LoadSalesInvoices();
                LoadVehicles();
                LoadRoutes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeServices()
        {
            _deliveryChallanService = new DeliveryChallanService();
            _salesInvoiceRepository = new SalesInvoiceRepository();
            _vehicleService = new VehicleService();
            _routeService = new RouteService();
        }

        private void InitializeChallan()
        {
            try
            {
                _currentChallan = new DeliveryChallan();
                _challanItems = new List<DeliveryChallanItem>();

                // Generate challan number and barcode
                _currentChallan.ChallanNo = _deliveryChallanService.GenerateChallanNumber();
                _currentChallan.BarcodeImage = _deliveryChallanService.GenerateChallanBarcode();

                // Update UI
                txtChallanNumber.Text = _currentChallan.ChallanNo;
                dtpChallanDate.Value = _currentChallan.ChallanDate;
                txtStatus.Text = _currentChallan.Status;

                // Display barcode
                DisplayBarcode(_currentChallan.BarcodeImage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing challan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSalesInvoices()
        {
            try
            {
                var salesInvoices = _salesInvoiceRepository.GetAllSalesInvoices();
                cmbSalesInvoice.DataSource = salesInvoices;
                cmbSalesInvoice.DisplayMember = "InvoiceNumber";
                cmbSalesInvoice.ValueMember = "SalesInvoiceId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayBarcode(string barcodeImageBase64)
        {
            try
            {
                if (!string.IsNullOrEmpty(barcodeImageBase64))
                {
                    var imageBytes = Convert.FromBase64String(barcodeImageBase64);
                    using (var memoryStream = new MemoryStream(imageBytes))
                    {
                        picBarcode.Image = Image.FromStream(memoryStream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying barcode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLoadSalesInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSalesInvoice.SelectedValue == null)
                {
                    MessageBox.Show("Please select a sales invoice.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var salesInvoiceId = (int)cmbSalesInvoice.SelectedValue;
                _selectedSalesInvoice = _salesInvoiceRepository.GetSalesInvoiceById(salesInvoiceId);

                if (_selectedSalesInvoice != null)
                {
                    // Load invoice details (items)
                    _selectedSalesInvoice.Items = _salesInvoiceRepository.GetSalesInvoiceDetails(salesInvoiceId);
                    
                    // Create delivery challan from sales invoice
                    _currentChallan = _deliveryChallanService.CreateFromSalesInvoice(salesInvoiceId);
                    
                    // Update UI with sales invoice data
                    txtCustomerName.Text = _currentChallan.CustomerName;
                    txtCustomerAddress.Text = _currentChallan.CustomerAddress;
                    
                    // Load items
                    LoadChallanItems();
                    CalculateTotals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChallanItems()
        {
            try
            {
                if (_currentChallan?.Items != null)
                {
                    _challanItems = _currentChallan.Items.ToList();
                    RefreshItemsGrid();
                }
                else
                {
                    _challanItems = new List<DeliveryChallanItem>();
                    RefreshItemsGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading challan items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshItemsGrid()
        {
            try
            {
                dgvItems.Rows.Clear();

                foreach (var item in _challanItems)
                {
                    dgvItems.Rows.Add(
                        item.ProductCode,
                        item.ProductName,
                        item.Quantity,
                        item.Unit,
                        item.UnitPrice.ToString("N2"),
                        item.TotalAmount.ToString("N2")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing items grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotals()
        {
            try
            {
                var totalAmount = _challanItems.Sum(item => item.TotalAmount);
                txtTotalAmount.Text = totalAmount.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating totals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    // Update challan with form data
                    UpdateChallanFromForm();

                    // Save challan
                    var challanId = _deliveryChallanService.CreateDeliveryChallan(_currentChallan);
                    
                    MessageBox.Show($"Delivery Challan saved successfully with ID: {challanId}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Reset form for new challan
                    InitializeChallan();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving delivery challan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                MessageBox.Show("Customer name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_challanItems == null || _challanItems.Count == 0)
            {
                MessageBox.Show("At least one item is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LoadVehicles()
        {
            try
            {
                if (_vehicleService == null)
                {
                    System.Diagnostics.Debug.WriteLine("VehicleService is null");
                    return;
                }

                var vehicles = _vehicleService.GetActiveVehicles();
                
                if (cmbVehicle != null)
                {
                    cmbVehicle.DataSource = vehicles;
                    cmbVehicle.DisplayMember = "DisplayText";
                    cmbVehicle.ValueMember = "VehicleId";
                    cmbVehicle.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading vehicles: {ex.Message}");
                MessageBox.Show("Unable to load vehicles. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoutes()
        {
            try
            {
                if (_routeService == null)
                {
                    System.Diagnostics.Debug.WriteLine("RouteService is null");
                    return;
                }

                var routes = _routeService.GetActiveRoutes();
                
                if (cmbRoute != null)
                {
                    cmbRoute.DataSource = routes;
                    cmbRoute.DisplayMember = "DisplayText";
                    cmbRoute.ValueMember = "RouteId";
                    cmbRoute.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading routes: {ex.Message}");
                MessageBox.Show("Unable to load routes. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateChallanFromForm()
        {
            try
            {
                _currentChallan.ChallanDate = dtpChallanDate.Value;
                _currentChallan.CustomerName = txtCustomerName.Text;
                _currentChallan.CustomerAddress = txtCustomerAddress.Text;
                
                // Update route information safely
                if (cmbRoute?.SelectedValue != null)
                {
                    try
                    {
                        int routeId = Convert.ToInt32(cmbRoute.SelectedValue);
                        _currentChallan.RouteId = routeId;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error setting route: {ex.Message}");
                        _currentChallan.RouteId = null;
                    }
                }
                else
                {
                    _currentChallan.RouteId = null;
                }
                
                // Update vehicle information safely
                if (cmbVehicle?.SelectedValue != null)
                {
                    try
                    {
                        int vehicleId = Convert.ToInt32(cmbVehicle.SelectedValue);
                        _currentChallan.VehicleId = vehicleId;
                        
                        if (_vehicleService != null)
                        {
                            var selectedVehicle = _vehicleService.GetVehicleById(vehicleId);
                            if (selectedVehicle != null)
                            {
                                _currentChallan.VehicleNo = selectedVehicle.VehicleNo ?? "";
                                _currentChallan.DriverName = selectedVehicle.DriverName ?? "";
                                _currentChallan.DriverPhone = selectedVehicle.DriverContact ?? "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error processing vehicle selection: {ex.Message}");
                        _currentChallan.VehicleId = null;
                        _currentChallan.VehicleNo = "";
                        _currentChallan.DriverName = "";
                        _currentChallan.DriverPhone = "";
                    }
                }
                else
                {
                    _currentChallan.VehicleId = null;
                    _currentChallan.VehicleNo = "";
                    _currentChallan.DriverName = "";
                    _currentChallan.DriverPhone = "";
                }
                
                _currentChallan.Remarks = txtRemarks.Text;
                _currentChallan.Status = "CONFIRMED";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating challan from form: {ex.Message}");
                MessageBox.Show("Unable to update challan details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtCustomerName.Clear();
            txtCustomerAddress.Clear();
            cmbVehicle.SelectedIndex = -1;
            txtDriverName.Clear();
            txtDriverPhone.Clear();
            txtRemarks.Clear();
            txtTotalAmount.Clear();
            dgvItems.Rows.Clear();
            cmbSalesInvoice.SelectedIndex = -1;
            _challanItems.Clear();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentChallan.ChallanId == 0)
                {
                    MessageBox.Show("Please save the delivery challan before printing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TODO: Implement print functionality
                MessageBox.Show("Print functionality will be implemented here.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing delivery challan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbVehicle?.SelectedValue != null && _vehicleService != null)
                {
                    try
                    {
                        int vehicleId = Convert.ToInt32(cmbVehicle.SelectedValue);
                        var selectedVehicle = _vehicleService.GetVehicleById(vehicleId);
                        if (selectedVehicle != null)
                        {
                            txtDriverName.Text = selectedVehicle.DriverName ?? "";
                            txtDriverPhone.Text = selectedVehicle.DriverContact ?? "";
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error processing vehicle selection: {ex.Message}");
                        txtDriverName.Clear();
                        txtDriverPhone.Clear();
                    }
                }
                else
                {
                    txtDriverName.Clear();
                    txtDriverPhone.Clear();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading vehicle details: {ex.Message}");
                MessageBox.Show("Unable to load vehicle details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                var vehicleMasterForm = new VehicleMasterForm();
                var result = vehicleMasterForm.ShowDialog();
                
                if (result == DialogResult.OK)
                {
                    // Reload vehicles after adding new one
                    LoadVehicles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening vehicle master form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to cancel? All unsaved changes will be lost.", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error canceling form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeliveryChallanForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Additional initialization if needed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeliveryChallanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Clean up resources if needed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error closing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
