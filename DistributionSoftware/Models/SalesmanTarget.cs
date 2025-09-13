using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class SalesmanTarget
    {
        public int TargetId { get; set; }
        public int SalesmanId { get; set; }
        public string SalesmanName { get; set; }
        public string SalesmanCode { get; set; }
        public string Department { get; set; }
        public string Territory { get; set; }
        public string Region { get; set; }
        
        // Target Details
        public string TargetType { get; set; } // MONTHLY, QUARTERLY, YEARLY
        public DateTime TargetPeriodStart { get; set; }
        public DateTime TargetPeriodEnd { get; set; }
        public string TargetPeriodName { get; set; } // e.g., "January 2024", "Q1 2024", "2024"
        
        // Sales Targets
        public decimal RevenueTarget { get; set; }
        public int UnitTarget { get; set; }
        public int CustomerTarget { get; set; }
        public int InvoiceTarget { get; set; }
        
        // Product Category Targets
        public string ProductCategory { get; set; }
        public decimal CategoryRevenueTarget { get; set; }
        public int CategoryUnitTarget { get; set; }
        
        // Achievement Tracking
        public decimal ActualRevenue { get; set; }
        public int ActualUnits { get; set; }
        public int ActualCustomers { get; set; }
        public int ActualInvoices { get; set; }
        public decimal ActualCategoryRevenue { get; set; }
        public int ActualCategoryUnits { get; set; }
        
        // Performance Metrics
        public decimal RevenueAchievementPercentage { get; set; }
        public decimal UnitAchievementPercentage { get; set; }
        public decimal CustomerAchievementPercentage { get; set; }
        public decimal InvoiceAchievementPercentage { get; set; }
        public decimal CategoryRevenueAchievementPercentage { get; set; }
        public decimal CategoryUnitAchievementPercentage { get; set; }
        
        // Variance Analysis
        public decimal RevenueVariance { get; set; }
        public int UnitVariance { get; set; }
        public int CustomerVariance { get; set; }
        public int InvoiceVariance { get; set; }
        public decimal CategoryRevenueVariance { get; set; }
        public int CategoryUnitVariance { get; set; }
        
        // Status and Approval
        public string Status { get; set; } // DRAFT, ACTIVE, COMPLETED, CANCELLED
        public string PerformanceRating { get; set; } // EXCELLENT, GOOD, AVERAGE, POOR
        public string ManagerComments { get; set; }
        public string SalesmanComments { get; set; }
        public string MarketConditions { get; set; }
        public string Challenges { get; set; }
        
        // Incentive and Bonus
        public decimal BonusAmount { get; set; }
        public decimal CommissionAmount { get; set; }
        public bool IsBonusEligible { get; set; }
        public bool IsCommissionEligible { get; set; }
        
        // Audit Fields
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        
        // Navigation Properties
        public List<SalesmanTargetAchievement> Achievements { get; set; }
        
        public SalesmanTarget()
        {
            Achievements = new List<SalesmanTargetAchievement>();
            Status = "DRAFT";
            PerformanceRating = "AVERAGE";
            IsBonusEligible = false;
            IsCommissionEligible = false;
        }
        
        // Calculated Properties
        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case "DRAFT": return "Draft";
                    case "ACTIVE": return "Active";
                    case "COMPLETED": return "Completed";
                    case "CANCELLED": return "Cancelled";
                    default: return Status;
                }
            }
        }
        
        public string PerformanceRatingText
        {
            get
            {
                switch (PerformanceRating)
                {
                    case "EXCELLENT": return "Excellent";
                    case "GOOD": return "Good";
                    case "AVERAGE": return "Average";
                    case "POOR": return "Poor";
                    default: return PerformanceRating;
                }
            }
        }
        
        public string TargetTypeText
        {
            get
            {
                switch (TargetType)
                {
                    case "MONTHLY": return "Monthly";
                    case "QUARTERLY": return "Quarterly";
                    case "YEARLY": return "Yearly";
                    default: return TargetType;
                }
            }
        }
        
        public decimal OverallAchievementPercentage { get; set; }
        
        public bool IsTargetMet
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


