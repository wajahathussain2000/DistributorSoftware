using System;

namespace DistributionSoftware.Models
{
    public class DeliveryScheduleReportData
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
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

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

        // Salesman Information
        public int? SalesmanId { get; set; }
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public string SalesmanEmail { get; set; }
        public string SalesmanPhone { get; set; }
        public string SalesmanTerritory { get; set; }
        public decimal? CommissionRate { get; set; }

        // Schedule Analysis
        public string StatusDescription { get; set; }
        public int? DispatchDelayMinutes { get; set; }
        public int? DeliveryTimeMinutes { get; set; }
        public int? ReturnTimeMinutes { get; set; }
        public int? ScheduleAgeDays { get; set; }
        public int? ItemsCount { get; set; }
        public int? CustomersCount { get; set; }
        public string DeliveryPerformance { get; set; }
        public decimal? RouteEfficiency { get; set; }
        public decimal? VehicleUtilizationHours { get; set; }
    }
}
