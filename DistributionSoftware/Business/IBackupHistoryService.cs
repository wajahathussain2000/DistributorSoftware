using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IBackupHistoryService
    {
        // CRUD Operations
        int CreateBackupHistory(BackupHistory backupHistory);
        bool UpdateBackupHistory(BackupHistory backupHistory);
        bool DeleteBackupHistory(int historyId);
        BackupHistory GetBackupHistoryById(int historyId);
        List<BackupHistory> GetAllBackupHistories();
        List<BackupHistory> GetBackupHistoriesBySchedule(int scheduleId);
        
        // Business Logic
        bool ValidateBackupHistory(BackupHistory backupHistory);
        string[] GetValidationErrors(BackupHistory backupHistory);
        
        // Reports
        List<BackupHistory> GetBackupHistoryReport(DateTime? startDate, DateTime? endDate);
        int GetBackupHistoryCount();
    }
}
