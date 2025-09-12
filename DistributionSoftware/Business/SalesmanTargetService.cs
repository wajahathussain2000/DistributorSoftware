using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Business
{
    public class SalesmanTargetService : ISalesmanTargetService
    {
        private readonly ISalesmanTargetRepository _salesmanTargetRepository;

        public SalesmanTargetService()
        {
            _salesmanTargetRepository = new SalesmanTargetRepository();
        }

        public SalesmanTargetService(ISalesmanTargetRepository salesmanTargetRepository)
        {
            _salesmanTargetRepository = salesmanTargetRepository;
        }

        #region Target Management

        public int CreateSalesmanTarget(SalesmanTarget target)
        {
            try
            {
                // Validate target
                if (!ValidateSalesmanTarget(target))
                {
                    throw new ArgumentException("Invalid salesman target data");
                }

                // Check for overlapping targets
                if (CheckTargetOverlap(target.SalesmanId, target.TargetPeriodStart, target.TargetPeriodEnd))
                {
                    throw new InvalidOperationException("Target period overlaps with existing target for this salesman");
                }

                // Check if salesman exists
                if (!CheckSalesmanExists(target.SalesmanId))
                {
                    throw new ArgumentException("Salesman does not exist");
                }

                // Set default values
                target.CreatedDate = DateTime.Now;
                target.Status = "DRAFT";
                target.PerformanceRating = "AVERAGE";

                // Calculate performance metrics
                target = CalculateTargetPerformance(target);

                return _salesmanTargetRepository.CreateSalesmanTarget(target);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating salesman target: {ex.Message}", ex);
            }
        }

        public bool UpdateSalesmanTarget(SalesmanTarget target)
        {
            try
            {
                // Validate target
                if (!ValidateSalesmanTarget(target))
                {
                    throw new ArgumentException("Invalid salesman target data");
                }

                // Check for overlapping targets (excluding current target)
                if (CheckTargetOverlap(target.SalesmanId, target.TargetPeriodStart, target.TargetPeriodEnd, target.TargetId))
                {
                    throw new InvalidOperationException("Target period overlaps with existing target for this salesman");
                }

                // Calculate performance metrics
                target = CalculateTargetPerformance(target);

                return _salesmanTargetRepository.UpdateSalesmanTarget(target);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating salesman target: {ex.Message}", ex);
            }
        }

        public bool DeleteSalesmanTarget(int targetId)
        {
            try
            {
                var target = GetSalesmanTargetById(targetId);
                if (target == null)
                {
                    throw new ArgumentException("Salesman target not found");
                }

                if (target.Status == "ACTIVE")
                {
                    throw new InvalidOperationException("Cannot delete active target. Please complete or cancel it first.");
                }

                return _salesmanTargetRepository.DeleteSalesmanTarget(targetId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting salesman target: {ex.Message}", ex);
            }
        }

        public SalesmanTarget GetSalesmanTargetById(int targetId)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetById(targetId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman target: {ex.Message}", ex);
            }
        }

        public List<SalesmanTarget> GetAllSalesmanTargets()
        {
            try
            {
                return _salesmanTargetRepository.GetAllSalesmanTargets();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all salesman targets: {ex.Message}", ex);
            }
        }

        public List<SalesmanTarget> GetSalesmanTargetsBySalesman(int salesmanId)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetsBySalesman(salesmanId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman targets by salesman: {ex.Message}", ex);
            }
        }

        public List<SalesmanTarget> GetSalesmanTargetsByPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetsByPeriod(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman targets by period: {ex.Message}", ex);
            }
        }

        public List<SalesmanTarget> GetSalesmanTargetsByStatus(string status)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetsByStatus(status);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman targets by status: {ex.Message}", ex);
            }
        }

        public List<SalesmanTarget> GetSalesmanTargetsByType(string targetType)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetsByType(targetType);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman targets by type: {ex.Message}", ex);
            }
        }

        #endregion

        #region Achievement Management

        public int CreateSalesmanTargetAchievement(SalesmanTargetAchievement achievement)
        {
            try
            {
                // Validate achievement
                if (achievement.TargetId <= 0)
                {
                    throw new ArgumentException("Invalid target ID");
                }

                if (achievement.SalesmanId <= 0)
                {
                    throw new ArgumentException("Invalid salesman ID");
                }

                // Get the target to validate against
                var target = GetSalesmanTargetById(achievement.TargetId);
                if (target == null)
                {
                    throw new ArgumentException("Target not found");
                }

                // Calculate achievement performance
                achievement = CalculateAchievementPerformance(achievement, target);

                // Set default values
                achievement.CreatedDate = DateTime.Now;
                achievement.Status = "RECORDED";

                return _salesmanTargetRepository.CreateSalesmanTargetAchievement(achievement);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating salesman target achievement: {ex.Message}", ex);
            }
        }

        public bool UpdateSalesmanTargetAchievement(SalesmanTargetAchievement achievement)
        {
            try
            {
                // Get the target to validate against
                var target = GetSalesmanTargetById(achievement.TargetId);
                if (target == null)
                {
                    throw new ArgumentException("Target not found");
                }

                // Calculate achievement performance
                achievement = CalculateAchievementPerformance(achievement, target);

                return _salesmanTargetRepository.UpdateSalesmanTargetAchievement(achievement);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating salesman target achievement: {ex.Message}", ex);
            }
        }

        public bool DeleteSalesmanTargetAchievement(int achievementId)
        {
            try
            {
                return _salesmanTargetRepository.DeleteSalesmanTargetAchievement(achievementId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting salesman target achievement: {ex.Message}", ex);
            }
        }

        public SalesmanTargetAchievement GetSalesmanTargetAchievementById(int achievementId)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetAchievementById(achievementId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman target achievement: {ex.Message}", ex);
            }
        }

        public List<SalesmanTargetAchievement> GetSalesmanTargetAchievements(int targetId)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetAchievements(targetId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman target achievements: {ex.Message}", ex);
            }
        }

        public List<SalesmanTargetAchievement> GetSalesmanTargetAchievementsBySalesman(int salesmanId)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetAchievementsBySalesman(salesmanId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman target achievements by salesman: {ex.Message}", ex);
            }
        }

        public List<SalesmanTargetAchievement> GetSalesmanTargetAchievementsByDate(DateTime date)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetAchievementsByDate(date);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman target achievements by date: {ex.Message}", ex);
            }
        }

        public List<SalesmanTargetAchievement> GetSalesmanTargetAchievementsByPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetAchievementsByPeriod(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman target achievements by period: {ex.Message}", ex);
            }
        }

        #endregion

        #region Performance Analysis

        public List<SalesmanTarget> GetTopPerformers(int count, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetTopPerformers(count, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting top performers: {ex.Message}", ex);
            }
        }

        public List<SalesmanTarget> GetUnderPerformers(int count, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetUnderPerformers(count, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting under performers: {ex.Message}", ex);
            }
        }

        public decimal GetAverageAchievementPercentage(int salesmanId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetAverageAchievementPercentage(salesmanId, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting average achievement percentage: {ex.Message}", ex);
            }
        }

        public decimal GetTotalRevenueAchieved(int salesmanId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetTotalRevenueAchieved(salesmanId, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total revenue achieved: {ex.Message}", ex);
            }
        }

        public int GetTotalUnitsSold(int salesmanId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetTotalUnitsSold(salesmanId, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total units sold: {ex.Message}", ex);
            }
        }

        #endregion

        #region Reporting

        public List<SalesmanTarget> GetSalesmanTargetReport(DateTime startDate, DateTime endDate, int? salesmanId, string targetType, string status)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanTargetReport(startDate, endDate, salesmanId, targetType, status);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman target report: {ex.Message}", ex);
            }
        }

        public List<SalesmanTargetAchievement> GetSalesmanAchievementReport(DateTime startDate, DateTime endDate, int? salesmanId, string status)
        {
            try
            {
                return _salesmanTargetRepository.GetSalesmanAchievementReport(startDate, endDate, salesmanId, status);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman achievement report: {ex.Message}", ex);
            }
        }

        #endregion

        #region Validation

        public bool ValidateSalesmanTarget(SalesmanTarget target)
        {
            return _salesmanTargetRepository.ValidateSalesmanTarget(target);
        }

        public bool CheckTargetOverlap(int salesmanId, DateTime startDate, DateTime endDate, int? excludeTargetId = null)
        {
            return _salesmanTargetRepository.CheckTargetOverlap(salesmanId, startDate, endDate, excludeTargetId);
        }

        public bool CheckSalesmanExists(int salesmanId)
        {
            return _salesmanTargetRepository.CheckSalesmanExists(salesmanId);
        }

        #endregion

        #region Approval and Status Management

        public bool ApproveSalesmanTarget(int targetId, int approvedBy, string approvalNotes)
        {
            try
            {
                return _salesmanTargetRepository.ApproveSalesmanTarget(targetId, approvedBy, approvalNotes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error approving salesman target: {ex.Message}", ex);
            }
        }

        public bool RejectSalesmanTarget(int targetId, int rejectedBy, string rejectionReason)
        {
            try
            {
                return _salesmanTargetRepository.RejectSalesmanTarget(targetId, rejectedBy, rejectionReason);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error rejecting salesman target: {ex.Message}", ex);
            }
        }

        public bool ActivateSalesmanTarget(int targetId)
        {
            try
            {
                return _salesmanTargetRepository.ActivateSalesmanTarget(targetId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error activating salesman target: {ex.Message}", ex);
            }
        }

        public bool CompleteSalesmanTarget(int targetId)
        {
            try
            {
                return _salesmanTargetRepository.CompleteSalesmanTarget(targetId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error completing salesman target: {ex.Message}", ex);
            }
        }

        #endregion

        #region Achievement Verification

        public bool VerifySalesmanAchievement(int achievementId, int verifiedBy, string verificationNotes)
        {
            try
            {
                return _salesmanTargetRepository.VerifySalesmanAchievement(achievementId, verifiedBy, verificationNotes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error verifying salesman achievement: {ex.Message}", ex);
            }
        }

        public bool ApproveSalesmanAchievement(int achievementId, int approvedBy, string approvalNotes)
        {
            try
            {
                return _salesmanTargetRepository.ApproveSalesmanAchievement(achievementId, approvedBy, approvalNotes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error approving salesman achievement: {ex.Message}", ex);
            }
        }

        #endregion

        #region Bonus and Commission

        public bool UpdateBonusAmount(int targetId, decimal bonusAmount)
        {
            try
            {
                return _salesmanTargetRepository.UpdateBonusAmount(targetId, bonusAmount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating bonus amount: {ex.Message}", ex);
            }
        }

        public bool UpdateCommissionAmount(int targetId, decimal commissionAmount)
        {
            try
            {
                return _salesmanTargetRepository.UpdateCommissionAmount(targetId, commissionAmount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating commission amount: {ex.Message}", ex);
            }
        }

        public bool SetBonusEligibility(int targetId, bool isEligible)
        {
            try
            {
                return _salesmanTargetRepository.SetBonusEligibility(targetId, isEligible);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error setting bonus eligibility: {ex.Message}", ex);
            }
        }

        public bool SetCommissionEligibility(int targetId, bool isEligible)
        {
            try
            {
                return _salesmanTargetRepository.SetCommissionEligibility(targetId, isEligible);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error setting commission eligibility: {ex.Message}", ex);
            }
        }

        #endregion

        #region Statistics

        public int GetTargetCount(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetTargetCount(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting target count: {ex.Message}", ex);
            }
        }

        public int GetAchievementCount(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetAchievementCount(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting achievement count: {ex.Message}", ex);
            }
        }

        public decimal GetTotalTargetRevenue(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetTotalTargetRevenue(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total target revenue: {ex.Message}", ex);
            }
        }

        public decimal GetTotalAchievedRevenue(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetTotalAchievedRevenue(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total achieved revenue: {ex.Message}", ex);
            }
        }

        public decimal GetOverallAchievementPercentage(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _salesmanTargetRepository.GetOverallAchievementPercentage(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting overall achievement percentage: {ex.Message}", ex);
            }
        }

        #endregion

        #region Business Logic Methods

        public SalesmanTarget CalculateTargetPerformance(SalesmanTarget target)
        {
            try
            {
                // Calculate achievement percentages
                if (target.RevenueTarget > 0)
                {
                    target.RevenueAchievementPercentage = (target.ActualRevenue / target.RevenueTarget) * 100;
                    target.RevenueVariance = target.ActualRevenue - target.RevenueTarget;
                }

                if (target.UnitTarget > 0)
                {
                    target.UnitAchievementPercentage = (target.ActualUnits / (decimal)target.UnitTarget) * 100;
                    target.UnitVariance = target.ActualUnits - target.UnitTarget;
                }

                if (target.CustomerTarget > 0)
                {
                    target.CustomerAchievementPercentage = (target.ActualCustomers / (decimal)target.CustomerTarget) * 100;
                    target.CustomerVariance = target.ActualCustomers - target.CustomerTarget;
                }

                if (target.InvoiceTarget > 0)
                {
                    target.InvoiceAchievementPercentage = (target.ActualInvoices / (decimal)target.InvoiceTarget) * 100;
                    target.InvoiceVariance = target.ActualInvoices - target.InvoiceTarget;
                }

                if (target.CategoryRevenueTarget > 0)
                {
                    target.CategoryRevenueAchievementPercentage = (target.ActualCategoryRevenue / target.CategoryRevenueTarget) * 100;
                    target.CategoryRevenueVariance = target.ActualCategoryRevenue - target.CategoryRevenueTarget;
                }

                if (target.CategoryUnitTarget > 0)
                {
                    target.CategoryUnitAchievementPercentage = (target.ActualCategoryUnits / (decimal)target.CategoryUnitTarget) * 100;
                    target.CategoryUnitVariance = target.ActualCategoryUnits - target.CategoryUnitTarget;
                }

                // Determine performance rating
                var overallPercentage = target.OverallAchievementPercentage;
                if (overallPercentage >= 120)
                    target.PerformanceRating = "EXCELLENT";
                else if (overallPercentage >= 100)
                    target.PerformanceRating = "GOOD";
                else if (overallPercentage >= 80)
                    target.PerformanceRating = "AVERAGE";
                else
                    target.PerformanceRating = "POOR";

                return target;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calculating target performance: {ex.Message}", ex);
            }
        }

        public SalesmanTargetAchievement CalculateAchievementPerformance(SalesmanTargetAchievement achievement, SalesmanTarget target)
        {
            try
            {
                // Calculate achievement percentages based on target
                if (target.RevenueTarget > 0)
                {
                    achievement.RevenueAchievementPercentage = (achievement.RevenueAchieved / target.RevenueTarget) * 100;
                    achievement.RevenueVariance = achievement.RevenueAchieved - target.RevenueTarget;
                }

                if (target.UnitTarget > 0)
                {
                    achievement.UnitAchievementPercentage = (achievement.UnitsSold / (decimal)target.UnitTarget) * 100;
                    achievement.UnitVariance = achievement.UnitsSold - target.UnitTarget;
                }

                if (target.CustomerTarget > 0)
                {
                    achievement.CustomerAchievementPercentage = (achievement.CustomersServed / (decimal)target.CustomerTarget) * 100;
                    achievement.CustomerVariance = achievement.CustomersServed - target.CustomerTarget;
                }

                if (target.InvoiceTarget > 0)
                {
                    achievement.InvoiceAchievementPercentage = (achievement.InvoicesGenerated / (decimal)target.InvoiceTarget) * 100;
                    achievement.InvoiceVariance = achievement.InvoicesGenerated - target.InvoiceTarget;
                }

                if (target.CategoryRevenueTarget > 0)
                {
                    achievement.CategoryRevenueAchievementPercentage = (achievement.CategoryRevenueAchieved / target.CategoryRevenueTarget) * 100;
                    achievement.CategoryRevenueVariance = achievement.CategoryRevenueAchieved - target.CategoryRevenueTarget;
                }

                if (target.CategoryUnitTarget > 0)
                {
                    achievement.CategoryUnitAchievementPercentage = (achievement.CategoryUnitsSold / (decimal)target.CategoryUnitTarget) * 100;
                    achievement.CategoryUnitVariance = achievement.CategoryUnitsSold - target.CategoryUnitTarget;
                }

                return achievement;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calculating achievement performance: {ex.Message}", ex);
            }
        }

        public List<SalesmanTarget> GetSalesmanPerformanceSummary(int salesmanId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var targets = GetSalesmanTargetsBySalesman(salesmanId);
                return targets.Where(t => t.TargetPeriodStart >= startDate && t.TargetPeriodEnd <= endDate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman performance summary: {ex.Message}", ex);
            }
        }

        public List<SalesmanTarget> GetTeamPerformanceSummary(DateTime startDate, DateTime endDate)
        {
            try
            {
                var targets = GetSalesmanTargetsByPeriod(startDate, endDate);
                return targets.OrderByDescending(t => t.OverallAchievementPercentage).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting team performance summary: {ex.Message}", ex);
            }
        }

        public bool UpdateTargetAchievement(int targetId)
        {
            try
            {
                var target = GetSalesmanTargetById(targetId);
                if (target == null)
                {
                    throw new ArgumentException("Target not found");
                }

                // Get all achievements for this target
                var achievements = GetSalesmanTargetAchievements(targetId);

                // Calculate total achievements
                target.ActualRevenue = achievements.Sum(a => a.RevenueAchieved);
                target.ActualUnits = achievements.Sum(a => a.UnitsSold);
                target.ActualCustomers = achievements.Sum(a => a.CustomersServed);
                target.ActualInvoices = achievements.Sum(a => a.InvoicesGenerated);
                target.ActualCategoryRevenue = achievements.Sum(a => a.CategoryRevenueAchieved);
                target.ActualCategoryUnits = achievements.Sum(a => a.CategoryUnitsSold);

                // Recalculate performance
                target = CalculateTargetPerformance(target);

                return UpdateSalesmanTarget(target);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating target achievement: {ex.Message}", ex);
            }
        }

        // Additional business logic methods will be implemented as needed
        public bool GenerateMonthlyTargets(int salesmanId, DateTime month)
        {
            // Implementation for generating monthly targets
            return false;
        }

        public bool GenerateQuarterlyTargets(int salesmanId, DateTime quarter)
        {
            // Implementation for generating quarterly targets
            return false;
        }

        public bool GenerateYearlyTargets(int salesmanId, DateTime year)
        {
            // Implementation for generating yearly targets
            return false;
        }

        public List<SalesmanTarget> GetTargetsDueForReview()
        {
            // Implementation for getting targets due for review
            return new List<SalesmanTarget>();
        }

        public List<SalesmanTarget> GetTargetsNearDeadline(int daysBeforeDeadline)
        {
            // Implementation for getting targets near deadline
            return new List<SalesmanTarget>();
        }

        public bool SendTargetReminder(int targetId)
        {
            // Implementation for sending target reminders
            return false;
        }

        public bool SendAchievementReminder(int achievementId)
        {
            // Implementation for sending achievement reminders
            return false;
        }

        #endregion
    }
}
