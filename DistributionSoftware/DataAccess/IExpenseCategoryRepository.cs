using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IExpenseCategoryRepository
    {
        // CRUD Operations
        bool Create(ExpenseCategory category);
        bool Update(ExpenseCategory category);
        bool Delete(int categoryId);
        ExpenseCategory GetById(int categoryId);
        List<ExpenseCategory> GetAll();
        List<ExpenseCategory> GetActive();
        
        // Search and Filter
        List<ExpenseCategory> Search(string searchTerm);
        List<ExpenseCategory> GetByStatus(bool isActive);
        
        // Validation
        bool ExistsByName(string categoryName, int excludeId = 0);
        bool ExistsByCode(string categoryCode, int excludeId = 0);
    }
}
