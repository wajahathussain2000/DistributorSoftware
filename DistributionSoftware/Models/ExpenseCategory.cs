using System;

namespace DistributionSoftware.Models
{
    public class ExpenseCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        // Navigation properties
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }

        // Computed properties
        public string StatusText => IsActive ? "Active" : "Inactive";
        public string FormattedCreatedDate => CreatedDate.ToString("dd/MM/yyyy HH:mm");
        public string FormattedModifiedDate => ModifiedDate?.ToString("dd/MM/yyyy HH:mm") ?? "Never";
    }
}
