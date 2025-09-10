using System;

namespace DistributionSoftware.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        public string DriverName { get; set; }
        public string DriverContact { get; set; }
        public string TransporterName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string Remarks { get; set; }

        public Vehicle()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }

        // Computed property for display purposes
        public string DisplayText => $"{VehicleNo ?? "N/A"} - {DriverName ?? "N/A"}";
    }
}
