using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class BackupScheduleService : IBackupScheduleService
    {
        private readonly IBackupScheduleRepository _backupScheduleRepository;

        public BackupScheduleService(IBackupScheduleRepository backupScheduleRepository)
        {
            _backupScheduleRepository = backupScheduleRepository;
        }

        public int CreateBackupSchedule(BackupSchedule backupSchedule)
        {
            try
            {
                if (!ValidateBackupSchedule(backupSchedule))
                    throw new InvalidOperationException("Backup schedule validation failed");

                return _backupScheduleRepository.Create(backupSchedule);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupScheduleService", ex);
                throw;
            }
        }

        public bool UpdateBackupSchedule(BackupSchedule backupSchedule)
        {
            try
            {
                if (!ValidateBackupSchedule(backupSchedule))
                    throw new InvalidOperationException("Backup schedule validation failed");

                return _backupScheduleRepository.Update(backupSchedule);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupScheduleService", ex);
                throw;
            }
        }

        public bool DeleteBackupSchedule(int scheduleId)
        {
            try
            {
                return _backupScheduleRepository.Delete(scheduleId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupScheduleService", ex);
                throw;
            }
        }

        public BackupSchedule GetBackupScheduleById(int scheduleId)
        {
            try
            {
                return _backupScheduleRepository.GetById(scheduleId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupScheduleService", ex);
                throw;
            }
        }

        public List<BackupSchedule> GetAllBackupSchedules()
        {
            try
            {
                return _backupScheduleRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupScheduleService", ex);
                throw;
            }
        }

        public List<BackupSchedule> GetActiveBackupSchedules()
        {
            try
            {
                return _backupScheduleRepository.GetActive();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupScheduleService", ex);
                throw;
            }
        }

        public bool ValidateBackupSchedule(BackupSchedule backupSchedule)
        {
            return GetValidationErrors(backupSchedule).Length == 0;
        }

        public string[] GetValidationErrors(BackupSchedule backupSchedule)
        {
            var errors = new List<string>();

            if (backupSchedule == null)
            {
                errors.Add("Backup schedule cannot be null");
                return errors.ToArray();
            }

            if (string.IsNullOrWhiteSpace(backupSchedule.ScheduleName))
                errors.Add("Schedule name is required");

            if (string.IsNullOrWhiteSpace(backupSchedule.BackupType))
                errors.Add("Backup type is required");

            if (string.IsNullOrWhiteSpace(backupSchedule.Frequency))
                errors.Add("Frequency is required");

            if (string.IsNullOrWhiteSpace(backupSchedule.BackupLocation))
                errors.Add("Backup location is required");

            if (backupSchedule.ScheduleName != null && backupSchedule.ScheduleName.Length > 100)
                errors.Add("Schedule name cannot exceed 100 characters");

            if (backupSchedule.BackupType != null && backupSchedule.BackupType.Length > 50)
                errors.Add("Backup type cannot exceed 50 characters");

            if (backupSchedule.Frequency != null && backupSchedule.Frequency.Length > 50)
                errors.Add("Frequency cannot exceed 50 characters");

            if (backupSchedule.BackupLocation != null && backupSchedule.BackupLocation.Length > 500)
                errors.Add("Backup location cannot exceed 500 characters");

            return errors.ToArray();
        }

        public bool IsScheduleNameExists(string scheduleName, int? excludeId = null)
        {
            try
            {
                var existingSchedule = _backupScheduleRepository.GetByName(scheduleName);
                return existingSchedule != null && existingSchedule.ScheduleId != excludeId;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupScheduleService", ex);
                return false;
            }
        }

        public List<BackupSchedule> GetBackupScheduleReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            try
            {
                return _backupScheduleRepository.GetReport(startDate, endDate, isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupScheduleService", ex);
                throw;
            }
        }

        public int GetBackupScheduleCount(bool? isActive)
        {
            try
            {
                return _backupScheduleRepository.GetCount(isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BackupScheduleService", ex);
                throw;
            }
        }
    }
}
