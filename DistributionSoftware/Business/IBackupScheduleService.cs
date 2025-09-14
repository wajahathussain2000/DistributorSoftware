using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IBackupScheduleService
    {
        // CRUD Operations
        int CreateBackupSchedule(BackupSchedule backupSchedule);
        bool UpdateBackupSchedule(BackupSchedule backupSchedule);
        bool DeleteBackupSchedule(int scheduleId);
        BackupSchedule GetBackupScheduleById(int scheduleId);
        List<BackupSchedule> GetAllBackupSchedules();
        List<BackupSchedule> GetActiveBackupSchedules();
        
        // Business Logic
        bool ValidateBackupSchedule(BackupSchedule backupSchedule);
        string[] GetValidationErrors(BackupSchedule backupSchedule);
        bool IsScheduleNameExists(string scheduleName, int? excludeId = null);
        
        // Reports
        List<BackupSchedule> GetBackupScheduleReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetBackupScheduleCount(bool? isActive);
    }
}
