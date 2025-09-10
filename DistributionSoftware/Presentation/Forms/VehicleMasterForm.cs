using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class VehicleMasterForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private int currentVehicleId = 0;
        private bool isEditMode = false;
        private IVehicleService vehicleService;

        public VehicleMasterForm()
        {
            InitializeComponent();
            InitializeConnection();
            InitializeServices();
            LoadVehiclesGrid();
            
            // Subscribe to form load event to clear form after all controls are initialized
            this.Load += VehicleMasterForm_Load;
        }

        private void VehicleMasterForm_Load(object sender, EventArgs e)
        {
            // Clear form after all controls are fully initialized
            System.Diagnostics.Debug.WriteLine("Form Load event fired - clearing form now");
            ClearForm();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        private void InitializeServices()
        {
            try
            {
                var vehicleRepository = new VehicleRepository();
                vehicleService = new VehicleService(vehicleRepository);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing services: {ex.Message}");
                MessageBox.Show("Unable to initialize vehicle services. Please restart the application.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVehiclesGrid()
        {
            try
            {
                if (vehicleService == null)
                {
                    System.Diagnostics.Debug.WriteLine("VehicleService is null");
                    return;
                }

                var vehicles = vehicleService.GetAllVehicles();
                
                if (dgvVehicles != null)
                {
                    dgvVehicles.DataSource = vehicles;
                    
                    // Format the grid after data is loaded
                    FormatDataGrid();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading vehicles grid: {ex.Message}");
                MessageBox.Show("Unable to load vehicle data. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGrid()
        {
            try
            {
                if (dgvVehicles?.Columns?.Count > 0)
                {
                    // Hide the VehicleId column safely - try different possible names
                    string[] vehicleIdColumnNames = { "VehicleId", "colVehicleId", "ID" };
                    foreach (var columnName in vehicleIdColumnNames)
                    {
                        if (dgvVehicles.Columns.Contains(columnName))
                        {
                            dgvVehicles.Columns[columnName].Visible = false;
                            break;
                        }
                    }
                    
                    // Format CreatedDate column safely
                    var createdDateColumn = dgvVehicles.Columns["CreatedDate"];
                    if (createdDateColumn != null)
                    {
                        createdDateColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
                        createdDateColumn.Width = 80;
                    }
                    
                    // Set column widths safely
                    SetColumnWidthSafely("VehicleNo", 120);
                    SetColumnWidthSafely("VehicleType", 80);
                    SetColumnWidthSafely("DriverName", 120);
                    SetColumnWidthSafely("DriverContact", 100);
                    SetColumnWidthSafely("TransporterName", 120);
                    SetColumnWidthSafely("IsActive", 60);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error formatting data grid: {ex.Message}");
                // Don't show error to user, just log it
            }
        }

        private void SetColumnWidthSafely(string columnName, int width)
        {
            try
            {
                var column = dgvVehicles?.Columns?[columnName];
                if (column != null)
                {
                    column.Width = width;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error setting column width for {columnName}: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("ClearForm() method called");
                currentVehicleId = 0;
                isEditMode = false;

                txtVehicleNo.Clear();
                cmbVehicleType.SelectedIndex = -1;
                txtDriverName.Clear();
                txtDriverContact.Clear();
                txtTransporterName.Clear();
                txtRemarks.Clear();
                chkIsActive.Checked = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error clearing form: {ex.Message}");
            }
        }

        private void LoadVehicleToForm(Vehicle vehicle)
        {
            try
            {
                if (vehicle == null) return;

                currentVehicleId = vehicle.VehicleId;
                isEditMode = true;

                txtVehicleNo.Text = vehicle.VehicleNo ?? "";
                cmbVehicleType.Text = vehicle.VehicleType ?? "";
                txtDriverName.Text = vehicle.DriverName ?? "";
                txtDriverContact.Text = vehicle.DriverContact ?? "";
                txtTransporterName.Text = vehicle.TransporterName ?? "";
                txtRemarks.Text = vehicle.Remarks ?? "";
                chkIsActive.Checked = vehicle.IsActive;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading vehicle to form: {ex.Message}");
                MessageBox.Show("Unable to load vehicle details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Vehicle GetVehicleFromForm()
        {
            try
            {
                var vehicle = new Vehicle
                {
                    VehicleId = currentVehicleId,
                    VehicleNo = txtVehicleNo.Text.Trim(),
                    VehicleType = cmbVehicleType.SelectedItem?.ToString() ?? "",
                    DriverName = txtDriverName.Text.Trim(),
                    DriverContact = txtDriverContact.Text.Trim(),
                    TransporterName = txtTransporterName.Text.Trim(),
                    Remarks = txtRemarks.Text.Trim(),
                    IsActive = chkIsActive.Checked,
                    CreatedBy = UserSession.CurrentUser?.UserId ?? 1
                };

                if (isEditMode)
                {
                    vehicle.ModifiedBy = UserSession.CurrentUser?.UserId ?? 1;
                    vehicle.ModifiedDate = DateTime.Now;
                }

                return vehicle;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting vehicle from form: {ex.Message}");
                throw new Exception($"Error getting vehicle data: {ex.Message}", ex);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtVehicleNo.Text))
            {
                MessageBox.Show("Vehicle number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVehicleNo.Focus();
                return false;
            }

            if (cmbVehicleType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a vehicle type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbVehicleType.Focus();
                return false;
            }

            return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var vehicle = GetVehicleFromForm();
                
                // Validate using business service
                string validationError = vehicleService.ValidateVehicle(vehicle);
                if (!string.IsNullOrEmpty(validationError))
                {
                    MessageBox.Show(validationError, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool success;
                string message;

                if (isEditMode)
                {
                    success = vehicleService.UpdateVehicle(vehicle);
                    message = success ? "Vehicle updated successfully." : "Failed to update vehicle.";
                }
                else
                {
                    success = vehicleService.CreateVehicle(vehicle);
                    message = success ? "Vehicle created successfully." : "Failed to create vehicle.";
                }

                if (success)
                {
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVehiclesGrid();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving vehicle: {ex.Message}");
                MessageBox.Show($"Error saving vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentVehicleId == 0)
                {
                    MessageBox.Show("Please select a vehicle to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show(
                    "Are you sure you want to delete this vehicle?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool success = vehicleService.DeleteVehicle(currentVehicleId);
                    string message = success ? "Vehicle deleted successfully." : "Failed to delete vehicle.";

                    if (success)
                    {
                        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadVehiclesGrid();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting vehicle: {ex.Message}");
                MessageBox.Show($"Error deleting vehicle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (vehicleService == null || dgvVehicles == null)
                {
                    System.Diagnostics.Debug.WriteLine("VehicleService or DataGridView is null");
                    return;
                }

                string searchTerm = txtSearch?.Text?.Trim() ?? "";
                
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    LoadVehiclesGrid();
                }
                else
                {
                    var vehicles = vehicleService.SearchVehicles(searchTerm);
                    dgvVehicles.DataSource = vehicles;
                    FormatDataGrid();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching vehicles: {ex.Message}");
                MessageBox.Show("Unable to search vehicles. Please try again.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvVehicles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e?.RowIndex >= 0 && dgvVehicles?.Rows?.Count > e.RowIndex)
                {
                    // Try to get VehicleId from the hidden column first
                    DataGridViewCell vehicleIdCell = null;
                    
                    // Try different possible column names
                    string[] possibleColumnNames = { "VehicleId", "colVehicleId", "ID" };
                    
                    foreach (var columnName in possibleColumnNames)
                    {
                        if (dgvVehicles.Columns.Contains(columnName))
                        {
                            vehicleIdCell = dgvVehicles.Rows[e.RowIndex].Cells[columnName];
                            break;
                        }
                    }
                    
                    // If still not found, try to get it from the first column (index 0)
                    if (vehicleIdCell == null && dgvVehicles.Columns.Count > 0)
                    {
                        vehicleIdCell = dgvVehicles.Rows[e.RowIndex].Cells[0];
                    }
                    
                    if (vehicleIdCell?.Value != null && vehicleService != null)
                    {
                        var vehicleId = Convert.ToInt32(vehicleIdCell.Value);
                        var vehicle = vehicleService.GetVehicleById(vehicleId);
                        LoadVehicleToForm(vehicle);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading vehicle from grid: {ex.Message}");
                MessageBox.Show("Unable to load vehicle details. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Helper method to find controls recursively
        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Name == name)
                    return control;
                
                Control found = FindControlRecursive(control, name);
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
