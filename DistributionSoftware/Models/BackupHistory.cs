using System;

namespace DistributionSoftware.Models
{
    public class BackupHistory
    {
        public int BackupHistoryId { get; set; }
        public int BackupScheduleId { get; set; }
        public string BackupFileName { get; set; }
        public string BackupPath { get; set; }
        
        /// <summary>
        /// Alias for BackupPath for backward compatibility
        /// </summary>
        public string BackupFilePath { get => BackupPath; set => BackupPath = value; }
        public long BackupSize { get; set; }
        public DateTime BackupDate { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        
        // Additional properties for compatibility
        public bool IsManualBackup { get; set; }
        public string InitiatedBy { get; set; }
        public string Notes { get; set; }
    }
}