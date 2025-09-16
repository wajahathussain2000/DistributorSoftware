using System;

namespace DistributionSoftware.Models
{
    public class DispatchReportData
    {
        public int ScheduleId { get; set; }
        public string ScheduleRef { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public DateTime? DispatchDateTime { get; set; }
        public int? VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public int? RouteId { get; set; }
        public string DriverName { get; set; }
        public string DriverContact { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }

        // Vehicle Information
        public string VehicleType { get; set; }
        public string TransporterName { get; set; }
        public bool? VehicleIsActive { get; set; }

        // Route Information
        public string RouteName { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public decimal? Distance { get; set; }
        public string EstimatedTime { get; set; }
        public bool? RouteIsActive { get; set; }

        // Dispatch Analysis
        public string StatusDescription { get; set; }
        public int? DispatchDelayMinutes { get; set; }
        public string DispatchPerformance { get; set; }
        public int? ItemsCount { get; set; }
        public int? CustomersCount { get; set; }
        public int? ChallansCount { get; set; }
        public decimal? RouteEfficiency { get; set; }
        public int? DispatchAgeHours { get; set; }
        public int? ScheduleAgeDays { get; set; }
    }
}
