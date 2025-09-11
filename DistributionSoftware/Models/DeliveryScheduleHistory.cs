using System;

namespace DistributionSoftware.Models
{
    public class DeliveryScheduleHistory
    {
        public int HistoryId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime ChangedAt { get; set; }
        public int ChangedBy { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string Remarks { get; set; }
        public DateTime? DispatchDateTime { get; set; }
        public string DriverName { get; set; }
        
        // Additional properties for display
        public string Username { get; set; }
        
        // Navigation properties
        public virtual DeliverySchedule DeliverySchedule { get; set; }
        
        public DeliveryScheduleHistory()
        {
            ChangedAt = DateTime.Now;
        }
    }
}
