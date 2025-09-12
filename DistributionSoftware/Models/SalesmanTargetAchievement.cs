using System;

namespace DistributionSoftware.Models
{
    public class SalesmanTargetAchievement
    {
        public int AchievementId { get; set; }
        public int TargetId { get; set; }
        public int SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        
        // Achievement Period
        public DateTime AchievementDate { get; set; }
        public string AchievementPeriod { get; set; } // Daily, Weekly, Monthly
        
        // Sales Achievement
        public decimal RevenueAchieved { get; set; }
        public int UnitsSold { get; set; }
        public int CustomersServed { get; set; }
        public int InvoicesGenerated { get; set; }
        
        // Product Category Achievement
        public string ProductCategory { get; set; }
        public decimal CategoryRevenueAchieved { get; set; }
        public int CategoryUnitsSold { get; set; }
        
        // Performance Metrics
        public decimal RevenueAchievementPercentage { get; set; }
        public decimal UnitAchievementPercentage { get; set; }
        public decimal CustomerAchievementPercentage { get; set; }
        public decimal InvoiceAchievementPercentage { get; set; }
        public decimal CategoryRevenueAchievementPercentage { get; set; }
        public decimal CategoryUnitAchievementPercentage { get; set; }
        
        // Variance from Target
        public decimal RevenueVariance { get; set; }
        public int UnitVariance { get; set; }
        public int CustomerVariance { get; set; }
        public int InvoiceVariance { get; set; }
        public decimal CategoryRevenueVariance { get; set; }
        public int CategoryUnitVariance { get; set; }
        
        // Achievement Details
        public string AchievementNotes { get; set; }
        public string Challenges { get; set; }
        public string MarketConditions { get; set; }
        public string CustomerFeedback { get; set; }
        
        // Status
        public string Status { get; set; } // RECORDED, VERIFIED, APPROVED
        public bool IsVerified { get; set; }
        public bool IsApproved { get; set; }
        
        // Verification and Approval
        public DateTime? VerifiedDate { get; set; }
        public int? VerifiedBy { get; set; }
        public string VerifiedByName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public string VerificationNotes { get; set; }
        public string ApprovalNotes { get; set; }
        
        // Audit Fields
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        
        public SalesmanTargetAchievement()
        {
            Status = "RECORDED";
            IsVerified = false;
            IsApproved = false;
        }
        
        // Calculated Properties
        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case "RECORDED": return "Recorded";
                    case "VERIFIED": return "Verified";
                    case "APPROVED": return "Approved";
                    default: return Status;
                }
            }
        }
        
        public decimal OverallAchievementPercentage
        {
            get
            {
                if (RevenueAchievementPercentage > 0)
                    return RevenueAchievementPercentage;
                else if (UnitAchievementPercentage > 0)
                    return UnitAchievementPercentage;
                else
                    return 0;
            }
        }
        
        public bool IsAchievementMet
        {
            get
            {
                return OverallAchievementPercentage >= 100;
            }
        }
        
        public string AchievementStatus
        {
            get
            {
                if (OverallAchievementPercentage >= 120)
                    return "Exceeded";
                else if (OverallAchievementPercentage >= 100)
                    return "Met";
                else if (OverallAchievementPercentage >= 80)
                    return "Near Target";
                else
                    return "Below Target";
            }
        }
    }
}


