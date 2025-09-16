using System;

namespace DistributionSoftware.Models
{
    public class VehicleUtilizationReportData
    {
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string DriverName { get; set; }
        public string DriverContact { get; set; }
        public string TransporterName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string Remarks { get; set; }

        // Utilization Statistics
        public int TotalSchedules { get; set; }
        public int ScheduledCount { get; set; }
        public int DispatchedCount { get; set; }
        public int InTransitCount { get; set; }
        public int DeliveredCount { get; set; }
        public int ReturnedCount { get; set; }
        public int CancelledCount { get; set; }

        // Time Utilization
        public decimal? TotalWorkingHours { get; set; }
        public decimal? TotalTripHours { get; set; }

        // Distance Utilization
        public decimal? TotalDistanceCovered { get; set; }
        public decimal? AverageDistancePerTrip { get; set; }

        // Route Utilization
        public int? UniqueRoutesUsed { get; set; }

        // Items and Customers
        public int? TotalItemsDelivered { get; set; }
        public int? TotalCustomersServed { get; set; }

        // Performance Metrics
        public decimal? OnTimeDeliveryPercentage { get; set; }

        // Utilization Rate
        public decimal? DailyUtilizationRate { get; set; }

        // Efficiency Metrics
        public decimal? AverageSpeedKmPerHour { get; set; }

        // First and Last Usage
        public DateTime? FirstUsageDate { get; set; }
        public DateTime? LastUsageDate { get; set; }
        public int? UsagePeriodDays { get; set; }

        // Status Description
        public string VehicleStatus { get; set; }

        // Utilization Category
        public string UtilizationCategory { get; set; }

        // Performance Category
        public string PerformanceCategory { get; set; }
    }
}
