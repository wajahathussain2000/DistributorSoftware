using System;

namespace DistributionSoftware.Models
{
    public class Salesman
    {
        public int SalesmanId { get; set; }
        public string SalesmanCode { get; set; }
        public string SalesmanName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Territory { get; set; }
        public decimal CommissionRate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        // Calculated properties
        public string DisplayName => $"{SalesmanCode} - {SalesmanName}";
        public string StatusText => IsActive ? "Active" : "Inactive";
        public string CommissionRateText => $"{CommissionRate:F2}%";
    }
}
