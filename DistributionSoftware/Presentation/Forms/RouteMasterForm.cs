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
    public partial class RouteMasterForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private int currentRouteId = 0;
        private bool isEditMode = false;
        private IRouteService routeService;

        public RouteMasterForm()
        {
            InitializeComponent();
            InitializeConnection();
            InitializeServices();
            LoadRoutesGrid();
            
            // Subscribe to form load event to clear form after all controls are initialized
            this.Load += RouteMasterForm_Load;
        }

        private void RouteMasterForm_Load(object sender, EventArgs e)
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
                routeService = new RouteService();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing services: {ex.Message}");
                MessageBox.Show("Unable to initialize route services. Please restart the application.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoutesGrid()
        {
            try
            {
                if (routeService == null)
                {
                    System.Diagnostics.Debug.WriteLine("RouteService is null");
                    return;
                }

                var routes = routeService.GetAll();
                
                if (dgvRoutes != null)
                {
                    // Clear existing data
                    dgvRoutes.Rows.Clear();
                    
                    // Populate the grid manually using predefined columns
                    foreach (var route in routes)
                    {
                        dgvRoutes.Rows.Add(
                            route.RouteId,
                            route.RouteName,
                            route.StartLocation ?? "",
                            route.EndLocation ?? "",
                            route.Distance?.ToString("N2") ?? "",
                            route.EstimatedTime ?? "",
                            route.Status,
                            route.CreatedDate.ToString("dd/MM/yyyy")
                        );
                    }
                    
                    // Format the grid after data is loaded
                    FormatDataGrid();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading routes grid: {ex.Message}");
                MessageBox.Show("Unable to load route data. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGrid()
        {
            try
            {
                if (dgvRoutes?.Columns?.Count > 0)
                {
                    // The RouteId column (colRouteId) is already set to Visible = false in the Designer
                    // No need to hide it again

                    // Set column widths
                    dgvRoutes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvRoutes.Columns["colRouteName"].FillWeight = 25;
                    dgvRoutes.Columns["colStartLocation"].FillWeight = 20;
                    dgvRoutes.Columns["colEndLocation"].FillWeight = 20;
                    dgvRoutes.Columns["colDistance"].FillWeight = 10;
                    dgvRoutes.Columns["colEstimatedTime"].FillWeight = 10;
                    dgvRoutes.Columns["colStatus"].FillWeight = 10;
                    dgvRoutes.Columns["colCreatedDate"].FillWeight = 15;

                    // Set alternating row colors
                    dgvRoutes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error formatting data grid: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            try
            {
                currentRouteId = 0;
                isEditMode = false;
                
                txtRouteName.Clear();
                txtStartLocation.Clear();
                txtEndLocation.Clear();
                txtDistance.Clear();
                txtEstimatedTime.Clear();
                chkStatus.Checked = true;
                
                // Update button text
                btnSave.Text = "Save";
                btnDelete.Enabled = false;
                
                System.Diagnostics.Debug.WriteLine("Form cleared successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error clearing form: {ex.Message}");
            }
        }

        private void LoadRouteToForm(Route route)
        {
            try
            {
                if (route == null) return;

                currentRouteId = route.RouteId;
                isEditMode = true;
                
                txtRouteName.Text = route.RouteName;
                txtStartLocation.Text = route.StartLocation ?? "";
                txtEndLocation.Text = route.EndLocation ?? "";
                txtDistance.Text = route.Distance?.ToString() ?? "";
                txtEstimatedTime.Text = route.EstimatedTime ?? "";
                chkStatus.Checked = route.Status;
                
                // Update button text
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                
                System.Diagnostics.Debug.WriteLine($"Route loaded to form: {route.RouteName}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading route to form: {ex.Message}");
                MessageBox.Show("Error loading route data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtRouteName.Text))
            {
                MessageBox.Show("Route name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRouteName.Focus();
                return false;
            }

            if (txtRouteName.Text.Length > 100)
            {
                MessageBox.Show("Route name cannot exceed 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRouteName.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtStartLocation.Text) && txtStartLocation.Text.Length > 200)
            {
                MessageBox.Show("Start location cannot exceed 200 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStartLocation.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEndLocation.Text) && txtEndLocation.Text.Length > 200)
            {
                MessageBox.Show("End location cannot exceed 200 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndLocation.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtDistance.Text))
            {
                if (!decimal.TryParse(txtDistance.Text, out decimal distance) || distance < 0)
                {
                    MessageBox.Show("Distance must be a valid positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDistance.Focus();
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtEstimatedTime.Text) && txtEstimatedTime.Text.Length > 50)
            {
                MessageBox.Show("Estimated time cannot exceed 50 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEstimatedTime.Focus();
                return false;
            }

            return true;
        }

        private Route GetRouteFromForm()
        {
            return new Route
            {
                RouteId = currentRouteId,
                RouteName = txtRouteName.Text.Trim(),
                StartLocation = string.IsNullOrWhiteSpace(txtStartLocation.Text) ? null : txtStartLocation.Text.Trim(),
                EndLocation = string.IsNullOrWhiteSpace(txtEndLocation.Text) ? null : txtEndLocation.Text.Trim(),
                Distance = string.IsNullOrWhiteSpace(txtDistance.Text) ? (decimal?)null : decimal.Parse(txtDistance.Text),
                EstimatedTime = string.IsNullOrWhiteSpace(txtEstimatedTime.Text) ? null : txtEstimatedTime.Text.Trim(),
                Status = chkStatus.Checked,
                CreatedBy = UserSession.CurrentUser?.UserId ?? 1,
                CreatedDate = DateTime.Now
            };
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var route = GetRouteFromForm();

                if (isEditMode)
                {
                    routeService.Update(route);
                    MessageBox.Show("Route updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    routeService.Create(route);
                    MessageBox.Show("Route created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadRoutesGrid();
                ClearForm();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving route: {ex.Message}");
                MessageBox.Show($"Error saving route: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (currentRouteId == 0)
                {
                    MessageBox.Show("Please select a route to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this route?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    routeService.Delete(currentRouteId);
                    MessageBox.Show("Route deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRoutesGrid();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting route: {ex.Message}");
                MessageBox.Show($"Error deleting route: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var searchTerm = txtSearch.Text.Trim();
                var routes = routeService.SearchRoutes(searchTerm);
                
                // Clear existing data
                dgvRoutes.Rows.Clear();
                
                // Populate the grid manually using predefined columns
                foreach (var route in routes)
                {
                    dgvRoutes.Rows.Add(
                        route.RouteId,
                        route.RouteName,
                        route.StartLocation ?? "",
                        route.EndLocation ?? "",
                        route.Distance?.ToString("N2") ?? "",
                        route.EstimatedTime ?? "",
                        route.Status,
                        route.CreatedDate.ToString("dd/MM/yyyy")
                    );
                }
                
                FormatDataGrid();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching routes: {ex.Message}");
                MessageBox.Show("Error searching routes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvRoutes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    // Get the RouteId from the first column (which is hidden)
                    var routeId = Convert.ToInt32(dgvRoutes.Rows[e.RowIndex].Cells[0].Value);
                    var route = routeService.GetById(routeId);
                    
                    if (route != null)
                    {
                        LoadRouteToForm(route);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error selecting route: {ex.Message}");
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnSearch_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}

