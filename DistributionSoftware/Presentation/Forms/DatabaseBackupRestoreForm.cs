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
    public partial class DatabaseBackupRestoreForm : Form
    {
        private IBackupScheduleService _backupScheduleService;
        private IBackupHistoryService _backupHistoryService;
        private List<BackupSchedule> _backupSchedules;
        private List<BackupHistory> _backupHistory;

        public DatabaseBackupRestoreForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
        }

        private void InitializeServices()
        {
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var backupScheduleRepository = new BackupScheduleRepository(connectionString);
            var backupHistoryRepository = new BackupHistoryRepository(connectionString);
            
            _backupScheduleService = new BackupScheduleService(backupScheduleRepository);
            _backupHistoryService = new BackupHistoryService(backupHistoryRepository);
        }

        private void InitializeForm()
        {
            LoadBackupSchedules();
            LoadBackupHistory();
        }

        private void LoadBackupSchedules()
        {
            try
            {
                _backupSchedules = _backupScheduleService.GetAllBackupSchedules();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading backup schedules: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBackupHistory()
        {
            try
            {
                _backupHistory = _backupHistoryService.GetAllBackupHistories();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading backup history: {ex.Message}", "Error", 
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
    }
}