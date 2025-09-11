using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class DeliverySchedule
    {
        public int ScheduleId { get; set; }
        public string ScheduleRef { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public int? VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public int? RouteId { get; set; }
        public string DriverName { get; set; }
        public string DriverContact { get; set; }
        public string Status { get; set; }
        public DateTime? DispatchDateTime { get; set; }
        public DateTime? DeliveredDateTime { get; set; }
        public DateTime? ReturnedDateTime { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public byte[] RowVersion { get; set; }
        
        // Additional properties for display
        public string VehicleType { get; set; }
        public string RouteName { get; set; }
        public int TotalRecords { get; set; }
        
        // Navigation properties
        public virtual Vehicle Vehicle { get; set; }
        public virtual Route Route { get; set; }
        public virtual ICollection<DeliveryScheduleItem> ScheduleItems { get; set; }
        public virtual ICollection<DeliveryScheduleAttachment> Attachments { get; set; }
        public virtual ICollection<DeliveryScheduleHistory> History { get; set; }
        
        public DeliverySchedule()
        {
            ScheduleItems = new List<DeliveryScheduleItem>();
            Attachments = new List<DeliveryScheduleAttachment>();
            History = new List<DeliveryScheduleHistory>();
            CreatedDate = DateTime.Now;
            Status = "Scheduled";
        }
        
        // Computed properties
        public string StatusDisplay => GetStatusDisplay();
        public string VehicleDisplay => $"{VehicleNo ?? "N/A"} - {DriverName ?? "N/A"}";
        public string RouteDisplay => RouteName ?? "N/A";
        
        private string GetStatusDisplay()
        {
            switch (Status)
            {
                case "Scheduled":
                    return "Scheduled";
                case "Dispatched":
                    return "Dispatched";
                case "Delivered":
                    return "Delivered";
                case "Returned":
                    return "Returned";
                case "Cancelled":
                    return "Cancelled";
                default:
                    return Status;
            }
        }
    }
}
