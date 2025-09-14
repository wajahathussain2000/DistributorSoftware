using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class BackupHistoryService : IBackupHistoryService
    {
        private readonly IBackupHistoryRepository _backupHistoryRepository;

        public BackupHistoryService(IBackupHistoryRepository backupHistoryRepository)
        {
            _backupHistoryRepository = backupHistoryRepository;
        }

        public int CreateBackupHistory(BackupHistory backupHistory)
        {
            try
            {
                if (!ValidateBackupHistory(backupHistory))
                    return 0;

                return _backupHistoryRepository.Create(backupHistory);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupHistoryService.CreateBackupHistory", ex);
                return 0;
            }
        }

        public bool UpdateBackupHistory(BackupHistory backupHistory)
        {
            try
            {
                if (!ValidateBackupHistory(backupHistory))
                    return false;

                return _backupHistoryRepository.Update(backupHistory);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupHistoryService.UpdateBackupHistory", ex);
                return false;
            }
        }

        public bool DeleteBackupHistory(int historyId)
        {
            try
            {
                return _backupHistoryRepository.Delete(historyId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupHistoryService.DeleteBackupHistory", ex);
                return false;
            }
        }

        public BackupHistory GetBackupHistoryById(int historyId)
        {
            try
            {
                return _backupHistoryRepository.GetById(historyId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupHistoryService.GetBackupHistoryById", ex);
                return null;
            }
        }

        public List<BackupHistory> GetAllBackupHistories()
        {
            try
            {
                return _backupHistoryRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupHistoryService.GetAllBackupHistories", ex);
                return new List<BackupHistory>();
            }
        }

        public List<BackupHistory> GetBackupHistoriesBySchedule(int scheduleId)
        {
            try
            {
                return _backupHistoryRepository.GetByScheduleId(scheduleId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupHistoryService.GetBackupHistoriesBySchedule", ex);
                return new List<BackupHistory>();
            }
        }

        public bool ValidateBackupHistory(BackupHistory backupHistory)
        {
            if (backupHistory == null)
                return false;

            if (backupHistory.BackupScheduleId <= 0)
                return false;

            if (string.IsNullOrEmpty(backupHistory.BackupFileName))
                return false;

            if (backupHistory.BackupSize <= 0)
                return false;

            return true;
        }

        public string[] GetValidationErrors(BackupHistory backupHistory)
        {
            var errors = new List<string>();

            if (backupHistory == null)
            {
                errors.Add("Backup history cannot be null");
                return errors.ToArray();
            }

            if (backupHistory.BackupScheduleId <= 0)
                errors.Add("Backup schedule ID is required");

            if (string.IsNullOrEmpty(backupHistory.BackupFileName))
                errors.Add("Backup file name is required");

            if (backupHistory.BackupSize <= 0)
                errors.Add("Backup size must be greater than 0");

            return errors.ToArray();
        }

        public List<BackupHistory> GetBackupHistoryReport(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                return _backupHistoryRepository.GetReport(startDate, endDate);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupHistoryService.GetBackupHistoryReport", ex);
                return new List<BackupHistory>();
            }
        }

        public int GetBackupHistoryCount()
        {
            try
            {
                return _backupHistoryRepository.GetCount();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupHistoryService.GetBackupHistoryCount", ex);
                return 0;
            }
        }
    }
}
