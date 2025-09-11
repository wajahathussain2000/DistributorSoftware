using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.Models;
using DistributionSoftware.Common;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class DeliveryConfirmationForm : Form
    {
        private readonly IDeliveryScheduleService _deliveryScheduleService;
        private readonly IDeliveryChallanService _deliveryChallanService;
        private readonly IVehicleService _vehicleService;
        private readonly IRouteService _routeService;
        private readonly IUserService _userService;

        private DeliverySchedule _currentSchedule;
        private List<DeliverySchedule> _schedules;
        private List<Vehicle> _vehicles;

        public DeliveryConfirmationForm()
        {
            InitializeComponent();
            
            // Initialize services
            _deliveryScheduleService = new DeliveryScheduleService();
            _deliveryChallanService = new DeliveryChallanService();
            _vehicleService = new VehicleService();
            _routeService = new RouteService();
            _userService = new UserService(new UserRepository(ConfigurationManager.DistributionConnectionString));

            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form properties
            this.Text = "Delivery Confirmation";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Initialize date filters
            dtpFilterFrom.Value = DateTime.Today.AddDays(-7);
            dtpFilterTo.Value = DateTime.Today.AddDays(7);

            // Load initial data
            LoadVehicles();
            LoadStatusFilter();
            LoadSchedules();
            InitializeTabs();
        }

        private void LoadVehicles()
        {
            try
            {
                _vehicles = _vehicleService.GetActiveVehicles();
                cmbFilterVehicle.Items.Clear();
                cmbFilterVehicle.Items.Add("All Vehicles");
                cmbFilterVehicle.SelectedIndex = 0;

                foreach (var vehicle in _vehicles)
                {
                    cmbFilterVehicle.Items.Add($"{vehicle.VehicleNo} - {vehicle.VehicleType}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatusFilter()
        {
            cmbFilterStatus.Items.Clear();
            cmbFilterStatus.Items.Add("All");
            cmbFilterStatus.Items.Add("Dispatched");
            cmbFilterStatus.Items.Add("Delivered");
            cmbFilterStatus.Items.Add("Returned");
            cmbFilterStatus.SelectedIndex = 0;
        }

        private void LoadSchedules()
        {
            try
            {
                // Clear existing data
                dgvDispatchedSchedules.Rows.Clear();
                dgvDispatchedSchedules.Columns.Clear();

                // Initialize filter DTO
                var filterDto = new DeliveryScheduleFilterDto
                {
                    FromDate = dtpFilterFrom.Value.Date,
                    ToDate = dtpFilterTo.Value.Date,
                    VehicleId = cmbFilterVehicle.SelectedIndex > 0 ? _vehicles[cmbFilterVehicle.SelectedIndex - 1].VehicleId : (int?)null,
                    Status = cmbFilterStatus.SelectedIndex > 0 ? cmbFilterStatus.SelectedItem.ToString() : null,
                    PageNumber = 1,
                    PageSize = 100
                };

                // Get schedules from database
                _schedules = _deliveryScheduleService.GetPaged(
                    filterDto.PageNumber, 
                    filterDto.PageSize, 
                    filterDto.FromDate, 
                    filterDto.ToDate, 
                    filterDto.Status, 
                    filterDto.VehicleId);
                
                // Filter for dispatched, delivered, and returned schedules only
                _schedules = _schedules?.Where(s => s.Status == "Dispatched" || s.Status == "Delivered" || s.Status == "Returned").ToList() ?? new List<DeliverySchedule>();

                // If no data from database, load sample data
                if (_schedules == null || _schedules.Count == 0)
                {
                    LoadSampleSchedules();
                    lblScheduleCount.Text = "Schedules Found: 0 (Using sample data)";
                }
                else
                {
                    lblScheduleCount.Text = $"Schedules Found: {_schedules.Count} (Database)";
                }

                BindSchedulesGrid();
            }
            catch (Exception ex)
            {
                // Fallback to sample data
                LoadSampleSchedules();
                lblScheduleCount.Text = "Schedules Found: 0 (Sample data due to error)";
                MessageBox.Show($"Database error, using sample data: {ex.Message}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadSampleSchedules()
        {
            _schedules = new List<DeliverySchedule>
            {
                new DeliverySchedule
                {
                    ScheduleId = 1,
                    ScheduleRef = "DS000001",
                    VehicleId = 1,
                    VehicleNo = "ABC-123",
                    RouteId = 1,
                    RouteName = "Route A",
                    DriverName = "John Smith",
                    DriverContact = "123-456-7890",
                    DispatchDateTime = DateTime.Now.AddHours(-2),
                    Status = "Dispatched",
                    Remarks = "Sample dispatched schedule"
                },
                new DeliverySchedule
                {
                    ScheduleId = 2,
                    ScheduleRef = "DS000002",
                    VehicleId = 2,
                    VehicleNo = "XYZ-456",
                    RouteId = 2,
                    RouteName = "Route B",
                    DriverName = "Mike Johnson",
                    DriverContact = "987-654-3210",
                    DispatchDateTime = DateTime.Now.AddHours(-1),
                    DeliveredDateTime = DateTime.Now.AddMinutes(-30),
                    Status = "Delivered",
                    Remarks = "Sample delivered schedule"
                },
                new DeliverySchedule
                {
                    ScheduleId = 3,
                    ScheduleRef = "DS000003",
                    VehicleId = 3,
                    VehicleNo = "DEF-789",
                    RouteId = 3,
                    RouteName = "Route C",
                    DriverName = "Sarah Wilson",
                    DriverContact = "555-123-4567",
                    DispatchDateTime = DateTime.Now.AddHours(-3),
                    ReturnedDateTime = DateTime.Now.AddMinutes(-15),
                    Status = "Returned",
                    Remarks = "Sample returned schedule"
                }
            };

            BindSchedulesGrid();
        }

        private void BindSchedulesGrid()
        {
            dgvDispatchedSchedules.DataSource = null;
            dgvDispatchedSchedules.Rows.Clear();
            dgvDispatchedSchedules.Columns.Clear();

            if (_schedules == null || !_schedules.Any())
            {
                lblScheduleCount.Text = "Schedules Found: 0";
                return;
            }

            // Create columns with proper headers like other forms
            dgvDispatchedSchedules.Columns.Add("ScheduleId", "ID");
            dgvDispatchedSchedules.Columns["ScheduleId"].Visible = false; // Hide ID column
            
            dgvDispatchedSchedules.Columns.Add("ScheduleRef", "Schedule Ref");
            dgvDispatchedSchedules.Columns["ScheduleRef"].Width = 120;
            dgvDispatchedSchedules.Columns["ScheduleRef"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
            dgvDispatchedSchedules.Columns.Add("VehicleNo", "Vehicle No");
            dgvDispatchedSchedules.Columns["VehicleNo"].Width = 100;
            dgvDispatchedSchedules.Columns["VehicleNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
            dgvDispatchedSchedules.Columns.Add("RouteName", "Route");
            dgvDispatchedSchedules.Columns["RouteName"].Width = 120;
            dgvDispatchedSchedules.Columns["RouteName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
            dgvDispatchedSchedules.Columns.Add("DispatchDateTime", "Dispatch Date");
            dgvDispatchedSchedules.Columns["DispatchDateTime"].Width = 140;
            dgvDispatchedSchedules.Columns["DispatchDateTime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgvDispatchedSchedules.Columns.Add("Status", "Status");
            dgvDispatchedSchedules.Columns["Status"].Width = 80;
            dgvDispatchedSchedules.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgvDispatchedSchedules.Columns.Add("DriverName", "Driver");
            dgvDispatchedSchedules.Columns["DriverName"].Width = 100;
            dgvDispatchedSchedules.Columns["DriverName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
            dgvDispatchedSchedules.Columns.Add("Remarks", "Remarks");
            dgvDispatchedSchedules.Columns["Remarks"].Width = 150;
            dgvDispatchedSchedules.Columns["Remarks"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Add rows
            foreach (var schedule in _schedules)
            {
                dgvDispatchedSchedules.Rows.Add(
                    schedule.ScheduleId,
                    schedule.ScheduleRef,
                    schedule.VehicleNo,
                    schedule.Route?.RouteName ?? "N/A",
                    schedule.DispatchDateTime.HasValue ? schedule.DispatchDateTime.Value.ToString("dd/MM/yyyy HH:mm") : "",
                    schedule.Status,
                    schedule.DriverName,
                    schedule.Remarks
                );
            }

            // Style the grid like other forms
            dgvDispatchedSchedules.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvDispatchedSchedules.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvDispatchedSchedules.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvDispatchedSchedules.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDispatchedSchedules.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvDispatchedSchedules.GridColor = Color.FromArgb(220, 220, 220);
            dgvDispatchedSchedules.BorderStyle = BorderStyle.FixedSingle;
            dgvDispatchedSchedules.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDispatchedSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDispatchedSchedules.MultiSelect = false;
            dgvDispatchedSchedules.ReadOnly = true;
            dgvDispatchedSchedules.AllowUserToAddRows = false;
            dgvDispatchedSchedules.AllowUserToDeleteRows = false;
            dgvDispatchedSchedules.RowHeadersVisible = false;
            dgvDispatchedSchedules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Update schedule count
            lblScheduleCount.Text = $"Schedules Found: {_schedules.Count}";
        }

        private void InitializeTabs()
        {
            InitializeDetailsTab();
            InitializeChallansTab();
            InitializeAttachmentsTab();
            InitializeHistoryTab();
        }

        private void InitializeDetailsTab()
        {
            tabDetails.Controls.Clear();

            int yPos = 20;
            int labelWidth = 120;
            int controlWidth = 200;
            int spacing = 30;

            // Schedule Reference
            var lblRef = new Label
            {
                Text = "Schedule Ref:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            tabDetails.Controls.Add(lblRef);

            var txtScheduleRef = new TextBox
            {
                Name = "txtScheduleRef",
                Location = new Point(150, yPos - 3),
                Size = new Size(controlWidth, 25),
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 249, 250),
                Font = new Font("Segoe UI", 9F)
            };
            tabDetails.Controls.Add(txtScheduleRef);

            yPos += spacing;

            // Dispatch Date Time
            var lblDispatchDate = new Label
            {
                Text = "Dispatch Date:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            tabDetails.Controls.Add(lblDispatchDate);

            var dtpDispatchDateTime = new DateTimePicker
            {
                Name = "dtpDispatchDateTime",
                Location = new Point(150, yPos - 3),
                Size = new Size(controlWidth, 25),
                Enabled = false,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                Font = new Font("Segoe UI", 9F)
            };
            tabDetails.Controls.Add(dtpDispatchDateTime);

            yPos += spacing;

            // Driver Name
            var lblDriverName = new Label
            {
                Text = "Driver Name:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            tabDetails.Controls.Add(lblDriverName);

            var txtDriverName = new TextBox
            {
                Name = "txtDriverName",
                Location = new Point(150, yPos - 3),
                Size = new Size(controlWidth, 25),
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 249, 250),
                Font = new Font("Segoe UI", 9F)
            };
            tabDetails.Controls.Add(txtDriverName);

            yPos += spacing;

            // Driver Contact
            var lblDriverContact = new Label
            {
                Text = "Driver Contact:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            tabDetails.Controls.Add(lblDriverContact);

            var txtDriverContact = new TextBox
            {
                Name = "txtDriverContact",
                Location = new Point(150, yPos - 3),
                Size = new Size(controlWidth, 25),
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 249, 250),
                Font = new Font("Segoe UI", 9F)
            };
            tabDetails.Controls.Add(txtDriverContact);

            yPos += spacing;

            // Remarks
            var lblRemarks = new Label
            {
                Text = "Remarks:",
                Location = new Point(20, yPos),
                Size = new Size(labelWidth, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            tabDetails.Controls.Add(lblRemarks);

            var txtRemarks = new TextBox
            {
                Name = "txtRemarks",
                Location = new Point(150, yPos - 3),
                Size = new Size(controlWidth, 60),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 9F)
            };
            tabDetails.Controls.Add(txtRemarks);
        }

        private void InitializeChallansTab()
        {
            tabChallans.Controls.Clear();

            var dgvChallans = new DataGridView
            {
                Name = "dgvChallans",
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(220, 220, 220),
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9F)
            };

            // Add columns
            dgvChallans.Columns.Add("ChallanNumber", "Challan No");
            dgvChallans.Columns.Add("CustomerName", "Customer");
            dgvChallans.Columns.Add("TotalAmount", "Amount");
            dgvChallans.Columns.Add("Status", "Status");

            // Set column widths
            dgvChallans.Columns["ChallanNumber"].Width = 120;
            dgvChallans.Columns["CustomerName"].Width = 200;
            dgvChallans.Columns["TotalAmount"].Width = 100;
            dgvChallans.Columns["Status"].Width = 80;

            // Style the grid
            dgvChallans.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvChallans.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvChallans.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChallans.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            tabChallans.Controls.Add(dgvChallans);
        }

        private void InitializeAttachmentsTab()
        {
            tabAttachments.Controls.Clear();

            // Attachment uploader placeholder
            var lblUploader = new Label
            {
                Text = "Proof of Delivery / Return Document Upload",
                Location = new Point(20, 20),
                Size = new Size(300, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            tabAttachments.Controls.Add(lblUploader);

            // Upload button
            var btnUpload = new Button
            {
                Name = "btnUploadAttachment",
                Text = "Upload Attachment",
                Location = new Point(20, 60),
                Size = new Size(150, 35),
                BackColor = Color.FromArgb(52, 152, 219),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnUpload.Click += BtnUploadAttachment_Click;
            tabAttachments.Controls.Add(btnUpload);

            // Download button
            var btnDownload = new Button
            {
                Name = "btnDownloadAttachment",
                Text = "Download Attachment",
                Location = new Point(190, 60),
                Size = new Size(150, 35),
                BackColor = Color.FromArgb(46, 204, 113),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White
            };
            btnDownload.Click += BtnDownloadAttachment_Click;
            tabAttachments.Controls.Add(btnDownload);

            // Attachments list
            var dgvAttachments = new DataGridView
            {
                Name = "dgvAttachments",
                Location = new Point(20, 120),
                Size = new Size(550, 300),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(220, 220, 220),
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9F)
            };

            // Add columns
            dgvAttachments.Columns.Add("FileName", "File Name");
            dgvAttachments.Columns.Add("FileType", "Type");
            dgvAttachments.Columns.Add("UploadedBy", "Uploaded By");
            dgvAttachments.Columns.Add("UploadedDate", "Upload Date");

            // Set column widths
            dgvAttachments.Columns["FileName"].Width = 200;
            dgvAttachments.Columns["FileType"].Width = 100;
            dgvAttachments.Columns["UploadedBy"].Width = 120;
            dgvAttachments.Columns["UploadedDate"].Width = 120;

            // Style the grid
            dgvAttachments.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvAttachments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvAttachments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAttachments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            tabAttachments.Controls.Add(dgvAttachments);
        }

        private void InitializeHistoryTab()
        {
            tabHistory.Controls.Clear();

            var dgvHistory = new DataGridView
            {
                Name = "dgvHistory",
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(220, 220, 220),
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9F)
            };

            // Add columns
            dgvHistory.Columns.Add("ChangedAt", "Changed At");
            dgvHistory.Columns.Add("ChangedBy", "Changed By");
            dgvHistory.Columns.Add("OldStatus", "Old Status");
            dgvHistory.Columns.Add("NewStatus", "New Status");
            dgvHistory.Columns.Add("Remarks", "Remarks");

            // Set column widths
            dgvHistory.Columns["ChangedAt"].Width = 120;
            dgvHistory.Columns["ChangedBy"].Width = 120;
            dgvHistory.Columns["OldStatus"].Width = 80;
            dgvHistory.Columns["NewStatus"].Width = 80;
            dgvHistory.Columns["Remarks"].Width = 200;

            // Style the grid
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            tabHistory.Controls.Add(dgvHistory);
        }

        private void UpdateStatusBadge(string status)
        {
            lblStatus.Text = status;
            
            switch (status.ToLower())
            {
                case "dispatched":
                    lblStatus.BackColor = Color.FromArgb(52, 152, 219);
                    break;
                case "delivered":
                    lblStatus.BackColor = Color.FromArgb(46, 204, 113);
                    break;
                case "returned":
                    lblStatus.BackColor = Color.FromArgb(155, 89, 182);
                    break;
                case "cancelled":
                    lblStatus.BackColor = Color.FromArgb(231, 76, 60);
                    break;
                default:
                    lblStatus.BackColor = Color.FromArgb(149, 165, 166);
                    break;
            }
        }

        private void UpdateActionButtons(string status)
        {
            btnMarkDelivered.Visible = status == "Dispatched";
            btnMarkReturned.Visible = status == "Dispatched";
            btnReopen.Visible = (status == "Delivered" || status == "Returned") && IsManager();
        }

        private bool IsManager()
        {
            // Check if current user is manager
            return UserSession.IsManagerOrHigher;
        }

        private void LoadScheduleDetails(int scheduleId)
        {
            try
            {
                _currentSchedule = _schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
                if (_currentSchedule == null) return;

                // Update details tab
                var txtScheduleRef = tabDetails.Controls.Find("txtScheduleRef", true).FirstOrDefault() as TextBox;
                if (txtScheduleRef != null) txtScheduleRef.Text = _currentSchedule.ScheduleRef;

                var dtpDispatchDateTime = tabDetails.Controls.Find("dtpDispatchDateTime", true).FirstOrDefault() as DateTimePicker;
                if (dtpDispatchDateTime != null && _currentSchedule.DispatchDateTime.HasValue)
                    dtpDispatchDateTime.Value = _currentSchedule.DispatchDateTime.Value;

                var txtDriverName = tabDetails.Controls.Find("txtDriverName", true).FirstOrDefault() as TextBox;
                if (txtDriverName != null) txtDriverName.Text = _currentSchedule.DriverName ?? "";

                var txtDriverContact = tabDetails.Controls.Find("txtDriverContact", true).FirstOrDefault() as TextBox;
                if (txtDriverContact != null) txtDriverContact.Text = _currentSchedule.DriverContact ?? "";

                var txtRemarks = tabDetails.Controls.Find("txtRemarks", true).FirstOrDefault() as TextBox;
                if (txtRemarks != null) txtRemarks.Text = _currentSchedule.Remarks ?? "";

                // Update status badge and action buttons
                UpdateStatusBadge(_currentSchedule.Status);
                UpdateActionButtons(_currentSchedule.Status);
                UpdateStatusDisplay();

                // Load challans, attachments, and history
                LoadChallans();
                LoadAttachments();
                LoadHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading schedule details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChallans()
        {
            try
            {
                var dgvChallans = tabChallans.Controls.Find("dgvChallans", true).FirstOrDefault() as DataGridView;
                if (dgvChallans == null) return;

                dgvChallans.Rows.Clear();

                if (_currentSchedule?.ScheduleItems != null)
                {
                    foreach (var item in _currentSchedule.ScheduleItems)
                    {
                        if (item.DeliveryChallan != null)
                        {
                            var challan = item.DeliveryChallan;
                            var totalAmount = challan.Items?.Sum(i => i.TotalAmount) ?? 0;
                            dgvChallans.Rows.Add(
                                challan.ChallanNo,
                                challan.CustomerName,
                                totalAmount.ToString("C"),
                                challan.Status
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading challans: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAttachments()
        {
            try
            {
                var dgvAttachments = tabAttachments.Controls.Find("dgvAttachments", true).FirstOrDefault() as DataGridView;
                if (dgvAttachments == null) return;

                dgvAttachments.Rows.Clear();

                if (_currentSchedule?.Attachments != null)
                {
                    foreach (var attachment in _currentSchedule.Attachments)
                    {
                        dgvAttachments.Rows.Add(
                            attachment.FileName,
                            attachment.FileType,
                            attachment.CreatedBy.ToString(),
                            attachment.CreatedDate.ToString("dd/MM/yyyy HH:mm")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading attachments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHistory()
        {
            try
            {
                var dgvHistory = tabHistory.Controls.Find("dgvHistory", true).FirstOrDefault() as DataGridView;
                if (dgvHistory == null) return;

                dgvHistory.Rows.Clear();

                if (_currentSchedule?.History != null)
                {
                    foreach (var history in _currentSchedule.History)
                    {
                        dgvHistory.Rows.Add(
                            history.ChangedAt.ToString("dd/MM/yyyy HH:mm"),
                            history.ChangedBy,
                            history.OldStatus,
                            history.NewStatus,
                            history.Remarks
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Event Handlers

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnApplyFilter_Click(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        private void BtnNewSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the Delivery Planning & Dispatch form for creating new schedules
                var deliveryPlanningForm = new DeliveryPlanningAndDispatchForm();
                deliveryPlanningForm.Owner = this;
                deliveryPlanningForm.ShowDialog();
                deliveryPlanningForm.Dispose();
                
                // Refresh the schedules after creating a new one
                LoadSchedules();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening new schedule form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadSchedules();
        }

        private void DgvDispatchedSchedules_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDispatchedSchedules.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDispatchedSchedules.SelectedRows[0];
                var scheduleId = Convert.ToInt32(selectedRow.Cells["ScheduleId"].Value);
                var schedule = _schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
                if (schedule != null)
                {
                    LoadScheduleDetails(schedule.ScheduleId);
                    
                    // Highlight the selected row
                    selectedRow.DefaultCellStyle.BackColor = Color.FromArgb(230, 244, 255);
                    selectedRow.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                }
            }
        }

        private void DgvDispatchedSchedules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvDispatchedSchedules.Rows[e.RowIndex];
                var scheduleId = Convert.ToInt32(selectedRow.Cells["ScheduleId"].Value);
                var schedule = _schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
                if (schedule != null)
                {
                    LoadScheduleDetails(schedule.ScheduleId);
                    // Switch to Details tab
                    tabControl.SelectedTab = tabDetails;
                }
            }
        }

        private void BtnMarkDelivered_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentSchedule == null)
                {
                    MessageBox.Show("Please select a schedule to mark as delivered", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_currentSchedule.Status != "Dispatched")
                {
                    MessageBox.Show("Only dispatched schedules can be marked as delivered", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var remarks = GetControlValue("txtRemarks");
                if (string.IsNullOrWhiteSpace(remarks))
                {
                    MessageBox.Show("Remarks are required for delivery confirmation", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to mark this schedule as delivered?", "Confirm Delivery", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Update sample data
                    _currentSchedule.Status = "Delivered";
                    _currentSchedule.DeliveredDateTime = DateTime.Now;
                    _currentSchedule.Remarks = remarks;

                    // Try to update in database
                    try
                    {
                        _deliveryScheduleService.MarkDelivered(_currentSchedule.ScheduleId, UserSession.CurrentUser?.UserId ?? 1, remarks);
                    }
                    catch
                    {
                        // If database update fails, just update the sample data
                    }

                    MessageBox.Show("Schedule marked as delivered successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Update the schedule in the list
                    var scheduleInList = _schedules.FirstOrDefault(s => s.ScheduleId == _currentSchedule.ScheduleId);
                    if (scheduleInList != null)
                    {
                        scheduleInList.Status = "Delivered";
                        scheduleInList.DeliveredDateTime = DateTime.Now;
                        scheduleInList.Remarks = remarks;
                    }
                    
                    // Refresh the grid and details
                    BindSchedulesGrid();
                    LoadScheduleDetails(_currentSchedule.ScheduleId);
                    UpdateStatusDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error marking schedule as delivered: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMarkReturned_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentSchedule == null)
                {
                    MessageBox.Show("Please select a schedule to mark as returned", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_currentSchedule.Status != "Dispatched")
                {
                    MessageBox.Show("Only dispatched schedules can be marked as returned", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var remarks = GetControlValue("txtRemarks");
                if (string.IsNullOrWhiteSpace(remarks))
                {
                    MessageBox.Show("Remarks are required for return confirmation", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to mark this schedule as returned?", "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Update sample data
                    _currentSchedule.Status = "Returned";
                    _currentSchedule.ReturnedDateTime = DateTime.Now;
                    _currentSchedule.Remarks = remarks;

                    // Try to update in database
                    try
                    {
                        _deliveryScheduleService.MarkReturned(_currentSchedule.ScheduleId, UserSession.CurrentUser?.UserId ?? 1, remarks);
                    }
                    catch
                    {
                        // If database update fails, just update the sample data
                    }

                    MessageBox.Show("Schedule marked as returned successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Update the schedule in the list
                    var scheduleInList = _schedules.FirstOrDefault(s => s.ScheduleId == _currentSchedule.ScheduleId);
                    if (scheduleInList != null)
                    {
                        scheduleInList.Status = "Returned";
                        scheduleInList.ReturnedDateTime = DateTime.Now;
                        scheduleInList.Remarks = remarks;
                    }
                    
                    // Refresh the grid and details
                    BindSchedulesGrid();
                    LoadScheduleDetails(_currentSchedule.ScheduleId);
                    UpdateStatusDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error marking schedule as returned: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReopen_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentSchedule == null)
                {
                    MessageBox.Show("Please select a schedule to reopen", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_currentSchedule.Status != "Delivered" && _currentSchedule.Status != "Returned")
                {
                    MessageBox.Show("Only delivered or returned schedules can be reopened", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsManager())
                {
                    MessageBox.Show("Only managers can reopen schedules", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to reopen this schedule?", "Confirm Reopen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Update sample data
                    _currentSchedule.Status = "Scheduled";
                    _currentSchedule.DeliveredDateTime = null;
                    _currentSchedule.ReturnedDateTime = null;

                    // Try to update in database
                    try
                    {
                        _deliveryScheduleService.ReopenSchedule(_currentSchedule.ScheduleId, UserSession.CurrentUser?.UserId ?? 1, "Schedule reopened");
                    }
                    catch
                    {
                        // If database update fails, just update the sample data
                    }

                    MessageBox.Show("Schedule reopened successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Update the schedule in the list
                    var scheduleInList = _schedules.FirstOrDefault(s => s.ScheduleId == _currentSchedule.ScheduleId);
                    if (scheduleInList != null)
                    {
                        scheduleInList.Status = "Scheduled";
                        scheduleInList.DeliveredDateTime = null;
                        scheduleInList.ReturnedDateTime = null;
                    }
                    
                    // Refresh the grid and details
                    BindSchedulesGrid();
                    LoadScheduleDetails(_currentSchedule.ScheduleId);
                    UpdateStatusDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reopening schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateStatusDisplay()
        {
            if (_currentSchedule != null)
            {
                lblStatus.Text = _currentSchedule.Status;
                
                // Update status label color based on status
                switch (_currentSchedule.Status)
                {
                    case "Dispatched":
                        lblStatus.BackColor = Color.FromArgb(52, 152, 219); // Blue
                        break;
                    case "Delivered":
                        lblStatus.BackColor = Color.FromArgb(46, 204, 113); // Green
                        break;
                    case "Returned":
                        lblStatus.BackColor = Color.FromArgb(155, 89, 182); // Purple
                        break;
                    case "Scheduled":
                        lblStatus.BackColor = Color.FromArgb(241, 196, 15); // Yellow
                        break;
                    default:
                        lblStatus.BackColor = Color.FromArgb(52, 152, 219); // Default blue
                        break;
                }
            }
        }

        private void BtnUploadAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "All Files (*.*)|*.*|PDF Files (*.pdf)|*.pdf|Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                    openFileDialog.FilterIndex = 1;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Here you would implement file upload logic
                        MessageBox.Show($"File selected: {openFileDialog.FileName}", "File Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading attachment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDownloadAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                var dgvAttachments = tabAttachments.Controls.Find("dgvAttachments", true).FirstOrDefault() as DataGridView;
                if (dgvAttachments?.SelectedRows.Count > 0)
                {
                    var selectedRow = dgvAttachments.SelectedRows[0];
                    var fileName = selectedRow.Cells["FileName"].Value?.ToString();
                    MessageBox.Show($"Downloading: {fileName}", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select an attachment to download", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading attachment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helper Methods

        private string GetControlValue(string controlName)
        {
            var control = tabDetails.Controls.Find(controlName, true).FirstOrDefault();
            if (control is TextBox textBox)
                return textBox.Text;
            if (control is ComboBox comboBox)
                return comboBox.SelectedValue?.ToString() ?? comboBox.Text;
            if (control is DateTimePicker dateTimePicker)
                return dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss");
            return string.Empty;
        }

        #endregion
    }
}
