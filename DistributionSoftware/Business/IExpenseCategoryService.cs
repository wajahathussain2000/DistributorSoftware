using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IExpenseCategoryService
    {
        // CRUD Operations
        bool CreateExpenseCategory(ExpenseCategory category);
        bool UpdateExpenseCategory(ExpenseCategory category);
        bool DeleteExpenseCategory(int categoryId);
        ExpenseCategory GetExpenseCategoryById(int categoryId);
        List<ExpenseCategory> GetAllExpenseCategories();
        List<ExpenseCategory> GetActiveExpenseCategories();
        
        // Search and Filter
        List<ExpenseCategory> SearchExpenseCategories(string searchTerm);
        List<ExpenseCategory> GetExpenseCategoriesByStatus(bool isActive);
        
        // Validation
        string ValidateExpenseCategory(ExpenseCategory category);
        bool IsCategoryNameExists(string categoryName, int excludeId = 0);
        bool IsCategoryCodeExists(string categoryCode, int excludeId = 0);
        
        // Utility
        string GenerateCategoryCode(string categoryName);
    }
}
