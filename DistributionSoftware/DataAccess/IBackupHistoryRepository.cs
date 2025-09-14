using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IBackupHistoryRepository
    {
        int Create(BackupHistory backupHistory);
        bool Update(BackupHistory backupHistory);
        bool Delete(int historyId);
        BackupHistory GetById(int historyId);
        List<BackupHistory> GetAll();
        List<BackupHistory> GetByScheduleId(int scheduleId);
        List<BackupHistory> GetReport(DateTime? startDate, DateTime? endDate);
        int GetCount();
    }
}
