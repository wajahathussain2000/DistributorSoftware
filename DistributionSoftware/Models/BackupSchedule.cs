using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents database backup schedules
    /// </summary>
    public class BackupSchedule
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the backup schedule
        /// </summary>
        public int BackupScheduleId { get; set; }
        
        /// <summary>
        /// Schedule ID (alias for BackupScheduleId)
        /// </summary>
        public int ScheduleId { get => BackupScheduleId; set => BackupScheduleId = value; }
        
        /// <summary>
        /// Schedule name
        /// </summary>
        public string ScheduleName { get; set; }
        
        /// <summary>
        /// Description of the backup schedule
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Backup type (FULL, INCREMENTAL, DIFFERENTIAL)
        /// </summary>
        public string BackupType { get; set; }
        
        /// <summary>
        /// Schedule frequency (DAILY, WEEKLY, MONTHLY)
        /// </summary>
        public string Frequency { get; set; }
        
        /// <summary>
        /// Day of week for weekly backups (1=Sunday, 7=Saturday)
        /// </summary>
        public int? DayOfWeek { get; set; }
        
        /// <summary>
        /// Day of month for monthly backups (1-31)
        /// </summary>
        public int? DayOfMonth { get; set; }
        
        /// <summary>
        /// Backup time (HH:MM format)
        /// </summary>
        public TimeSpan BackupTime { get; set; }
        
        /// <summary>
        /// Backup file path
        /// </summary>
        public string BackupPath { get; set; }
        
        /// <summary>
        /// Whether to compress the backup
        /// </summary>
        public bool CompressBackup { get; set; }
        
        /// <summary>
        /// Whether to encrypt the backup
        /// </summary>
        public bool EncryptBackup { get; set; }
        
        /// <summary>
        /// Encryption password (encrypted)
        /// </summary>
        public string EncryptionPassword { get; set; }
        
        /// <summary>
        /// Number of backup files to retain
        /// </summary>
        public int RetentionDays { get; set; }
        
        /// <summary>
        /// Whether this schedule is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Whether to notify on backup completion
        /// </summary>
        public bool NotifyOnCompletion { get; set; }
        
        /// <summary>
        /// Email addresses for notifications (comma-separated)
        /// </summary>
        public string NotificationEmails { get; set; }
        
        /// <summary>
        /// Last backup execution date
        /// </summary>
        public DateTime? LastExecutionDate { get; set; }
        
        /// <summary>
        /// Next scheduled backup date
        /// </summary>
        public DateTime? NextExecutionDate { get; set; }
        
        /// <summary>
        /// Last run date (alias for LastExecutionDate)
        /// </summary>
        public DateTime? LastRunDate { get => LastExecutionDate; set => LastExecutionDate = value; }
        
        /// <summary>
        /// Next run date (alias for NextExecutionDate)
        /// </summary>
        public DateTime? NextRunDate { get => NextExecutionDate; set => NextExecutionDate = value; }
        
        /// <summary>
        /// Backup location (alias for BackupPath)
        /// </summary>
        public string BackupLocation { get => BackupPath; set => BackupPath = value; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the schedule was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the schedule
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the schedule
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the schedule was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the schedule
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the schedule
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public BackupSchedule()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            BackupType = "FULL";
            Frequency = "DAILY";
            BackupTime = new TimeSpan(2, 0, 0); // 2:00 AM
            CompressBackup = true;
            EncryptBackup = false;
            RetentionDays = 30;
            NotifyOnCompletion = false;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the backup schedule
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(ScheduleName)) return false;
            if (string.IsNullOrWhiteSpace(BackupType)) return false;
            if (string.IsNullOrWhiteSpace(Frequency)) return false;
            if (string.IsNullOrWhiteSpace(BackupPath)) return false;
            if (RetentionDays < 1) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(ScheduleName))
                errors.Add("Schedule name is required");
            
            if (string.IsNullOrWhiteSpace(BackupType))
                errors.Add("Backup type is required");
            
            if (string.IsNullOrWhiteSpace(Frequency))
                errors.Add("Frequency is required");
            
            if (string.IsNullOrWhiteSpace(BackupPath))
                errors.Add("Backup path is required");
            
            if (RetentionDays < 1)
                errors.Add("Retention days must be at least 1");
            
            if (Frequency == "WEEKLY" && (!DayOfWeek.HasValue || DayOfWeek < 1 || DayOfWeek > 7))
                errors.Add("Day of week is required for weekly backups");
            
            if (Frequency == "MONTHLY" && (!DayOfMonth.HasValue || DayOfMonth < 1 || DayOfMonth > 31))
                errors.Add("Day of month is required for monthly backups");
            
            if (EncryptBackup && string.IsNullOrWhiteSpace(EncryptionPassword))
                errors.Add("Encryption password is required when encryption is enabled");
            
            return errors.ToArray();
        }
        
        /// <summary>
        /// Calculates the next execution date based on the schedule
        /// </summary>
        public void CalculateNextExecutionDate()
        {
            var now = DateTime.Now;
            var nextDate = now.Date.Add(BackupTime);
            
            switch (Frequency)
            {
                case "DAILY":
                    if (nextDate <= now)
                        nextDate = nextDate.AddDays(1);
                    break;
                
                case "WEEKLY":
                    if (DayOfWeek.HasValue)
                    {
                        var daysUntilTarget = ((int)DayOfWeek.Value - (int)now.DayOfWeek + 7) % 7;
                        nextDate = now.Date.AddDays(daysUntilTarget).Add(BackupTime);
                        if (nextDate <= now)
                            nextDate = nextDate.AddDays(7);
                    }
                    break;
                
                case "MONTHLY":
                    if (DayOfMonth.HasValue)
                    {
                        var targetDay = Math.Min(DayOfMonth.Value, DateTime.DaysInMonth(now.Year, now.Month));
                        nextDate = new DateTime(now.Year, now.Month, targetDay).Add(BackupTime);
                        if (nextDate <= now)
                        {
                            var nextMonth = now.Month == 12 ? 1 : now.Month + 1;
                            var nextYear = now.Month == 12 ? now.Year + 1 : now.Year;
                            targetDay = Math.Min(DayOfMonth.Value, DateTime.DaysInMonth(nextYear, nextMonth));
                            nextDate = new DateTime(nextYear, nextMonth, targetDay).Add(BackupTime);
                        }
                    }
                    break;
            }
            
            NextExecutionDate = nextDate;
        }
        
        /// <summary>
        /// Checks if the schedule should run now
        /// </summary>
        /// <returns>True if should run</returns>
        public bool ShouldRunNow()
        {
            if (!IsActive) return false;
            if (!NextExecutionDate.HasValue) return false;
            
            return DateTime.Now >= NextExecutionDate.Value;
        }
        
        /// <summary>
        /// Gets the backup file name for this schedule
        /// </summary>
        /// <returns>Backup file name</returns>
        public string GetBackupFileName()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var extension = CompressBackup ? ".bak" : ".sql";
            return $"{ScheduleName}_{timestamp}{extension}";
        }
        
        #endregion
    }
}
