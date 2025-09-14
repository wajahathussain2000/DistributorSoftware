using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IBackupScheduleRepository
    {
        // CRUD Operations
        int Create(BackupSchedule backupSchedule);
        bool Update(BackupSchedule backupSchedule);
        bool Delete(int scheduleId);
        BackupSchedule GetById(int scheduleId);
        BackupSchedule GetByName(string scheduleName);
        List<BackupSchedule> GetAll();
        List<BackupSchedule> GetActive();
        
        // Reports
        List<BackupSchedule> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetCount(bool? isActive);
    }
}
