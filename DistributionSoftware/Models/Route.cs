using System;

namespace DistributionSoftware.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public decimal? Distance { get; set; }
        public string EstimatedTime { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public Route()
        {
            Status = true;
            CreatedDate = DateTime.Now;
        }

        // Computed property for display purposes
        public string DisplayText => $"{RouteName ?? "N/A"} ({StartLocation ?? "N/A"} - {EndLocation ?? "N/A"})";
        
        // Computed property for status display
        public string StatusText => Status ? "Active" : "Inactive";
    }
}

