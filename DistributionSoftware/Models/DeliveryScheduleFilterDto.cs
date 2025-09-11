using System;

namespace DistributionSoftware.Models
{
    public class DeliveryScheduleFilterDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? VehicleId { get; set; }
        public int? RouteId { get; set; }
        public string Status { get; set; }
        public int? DriverId { get; set; }
        public string ScheduleRef { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
