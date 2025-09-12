using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface ISalesmanTargetRepository
    {
        // Target Management
        int CreateSalesmanTarget(SalesmanTarget target);
        bool UpdateSalesmanTarget(SalesmanTarget target);
        bool DeleteSalesmanTarget(int targetId);
        SalesmanTarget GetSalesmanTargetById(int targetId);
        List<SalesmanTarget> GetAllSalesmanTargets();
        List<SalesmanTarget> GetSalesmanTargetsBySalesman(int salesmanId);
        List<SalesmanTarget> GetSalesmanTargetsByPeriod(DateTime startDate, DateTime endDate);
        List<SalesmanTarget> GetSalesmanTargetsByStatus(string status);
        List<SalesmanTarget> GetSalesmanTargetsByType(string targetType);
        
        // Achievement Management
        int CreateSalesmanTargetAchievement(SalesmanTargetAchievement achievement);
        bool UpdateSalesmanTargetAchievement(SalesmanTargetAchievement achievement);
        bool DeleteSalesmanTargetAchievement(int achievementId);
        SalesmanTargetAchievement GetSalesmanTargetAchievementById(int achievementId);
        List<SalesmanTargetAchievement> GetSalesmanTargetAchievements(int targetId);
        List<SalesmanTargetAchievement> GetSalesmanTargetAchievementsBySalesman(int salesmanId);
        List<SalesmanTargetAchievement> GetSalesmanTargetAchievementsByDate(DateTime date);
        List<SalesmanTargetAchievement> GetSalesmanTargetAchievementsByPeriod(DateTime startDate, DateTime endDate);
        
        // Performance Analysis
        List<SalesmanTarget> GetTopPerformers(int count, DateTime startDate, DateTime endDate);
        List<SalesmanTarget> GetUnderPerformers(int count, DateTime startDate, DateTime endDate);
        decimal GetAverageAchievementPercentage(int salesmanId, DateTime startDate, DateTime endDate);
        decimal GetTotalRevenueAchieved(int salesmanId, DateTime startDate, DateTime endDate);
        int GetTotalUnitsSold(int salesmanId, DateTime startDate, DateTime endDate);
        
        // Reporting
        List<SalesmanTarget> GetSalesmanTargetReport(DateTime startDate, DateTime endDate, int? salesmanId, string targetType, string status);
        List<SalesmanTargetAchievement> GetSalesmanAchievementReport(DateTime startDate, DateTime endDate, int? salesmanId, string status);
        
        // Validation
        bool ValidateSalesmanTarget(SalesmanTarget target);
        bool CheckTargetOverlap(int salesmanId, DateTime startDate, DateTime endDate, int? excludeTargetId = null);
        bool CheckSalesmanExists(int salesmanId);
        
        // Approval and Status Management
        bool ApproveSalesmanTarget(int targetId, int approvedBy, string approvalNotes);
        bool RejectSalesmanTarget(int targetId, int rejectedBy, string rejectionReason);
        bool ActivateSalesmanTarget(int targetId);
        bool CompleteSalesmanTarget(int targetId);
        
        // Achievement Verification
        bool VerifySalesmanAchievement(int achievementId, int verifiedBy, string verificationNotes);
        bool ApproveSalesmanAchievement(int achievementId, int approvedBy, string approvalNotes);
        
        // Bonus and Commission
        bool UpdateBonusAmount(int targetId, decimal bonusAmount);
        bool UpdateCommissionAmount(int targetId, decimal commissionAmount);
        bool SetBonusEligibility(int targetId, bool isEligible);
        bool SetCommissionEligibility(int targetId, bool isEligible);
        
        // Statistics
        int GetTargetCount(DateTime startDate, DateTime endDate);
        int GetAchievementCount(DateTime startDate, DateTime endDate);
        decimal GetTotalTargetRevenue(DateTime startDate, DateTime endDate);
        decimal GetTotalAchievedRevenue(DateTime startDate, DateTime endDate);
        decimal GetOverallAchievementPercentage(DateTime startDate, DateTime endDate);
    }
}


