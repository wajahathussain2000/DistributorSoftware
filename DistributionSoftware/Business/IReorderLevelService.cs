using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IReorderLevelService
    {
        // CRUD Operations
        int CreateReorderLevel(ReorderLevel reorderLevel);
        bool UpdateReorderLevel(ReorderLevel reorderLevel);
        bool DeleteReorderLevel(int reorderLevelId);
        ReorderLevel GetReorderLevelById(int reorderLevelId);
        ReorderLevel GetReorderLevelByProductId(int productId);
        List<ReorderLevel> GetAllReorderLevels();
        List<ReorderLevel> GetActiveReorderLevels();
        List<ReorderLevel> GetReorderLevelsByProduct(int productId);
        
        // Business Logic
        List<ReorderLevel> GetReorderLevelsBelowMinimum();
        List<ReorderLevel> GetReorderLevelsAboveMaximum();
        List<ReorderLevel> GetReorderLevelsNeedingAlert();
        bool ProcessReorderAlerts();
        bool UpdateLastAlertDate(int reorderLevelId);
        
        // Validation
        bool ValidateReorderLevel(ReorderLevel reorderLevel);
        string[] GetValidationErrors(ReorderLevel reorderLevel);
        
        // Reports
        List<ReorderLevel> GetReorderLevelReport(DateTime? startDate, DateTime? endDate, int? productId, bool? isActive);
        int GetReorderLevelCount(bool? isActive);
        decimal GetTotalReorderValue();
        
        // Automation
        bool AutoCreateReorderLevel(int productId, decimal currentStock);
        List<ReorderLevel> GetSuggestedReorderLevels();
    }
}
