using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IExpenseCategoryRepository _repository;

        public ExpenseCategoryService()
        {
            _repository = new ExpenseCategoryRepository();
        }

        public ExpenseCategoryService(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

        public bool CreateExpenseCategory(ExpenseCategory category)
        {
            try
            {
                // Validate category
                var validationError = ValidateExpenseCategory(category);
                if (!string.IsNullOrEmpty(validationError))
                {
                    throw new Exception(validationError);
                }

                // Set audit fields
                category.CreatedDate = DateTime.Now;
                category.CreatedBy = UserSession.CurrentUser?.UserId ?? 1;

                // Always generate category code for new categories
                category.CategoryCode = GenerateCategoryCode(category.CategoryName);

                return _repository.Create(category);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating expense category: {ex.Message}");
                throw;
            }
        }

        public bool UpdateExpenseCategory(ExpenseCategory category)
        {
            try
            {
                // Validate category
                var validationError = ValidateExpenseCategory(category);
                if (!string.IsNullOrEmpty(validationError))
                {
                    throw new Exception(validationError);
                }

                // Set audit fields
                category.ModifiedDate = DateTime.Now;
                category.ModifiedBy = UserSession.CurrentUser?.UserId ?? 1;

                return _repository.Update(category);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating expense category: {ex.Message}");
                throw;
            }
        }

        public bool DeleteExpenseCategory(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                {
                    throw new Exception("Invalid category ID");
                }

                return _repository.Delete(categoryId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting expense category: {ex.Message}");
                throw;
            }
        }

        public ExpenseCategory GetExpenseCategoryById(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                {
                    return null;
                }

                return _repository.GetById(categoryId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting expense category by ID: {ex.Message}");
                return null;
            }
        }

        public List<ExpenseCategory> GetAllExpenseCategories()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all expense categories: {ex.Message}");
                return new List<ExpenseCategory>();
            }
        }

        public List<ExpenseCategory> GetActiveExpenseCategories()
        {
            try
            {
                return _repository.GetActive();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting active expense categories: {ex.Message}");
                return new List<ExpenseCategory>();
            }
        }

        public List<ExpenseCategory> SearchExpenseCategories(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return GetAllExpenseCategories();
                }

                return _repository.Search(searchTerm.Trim());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching expense categories: {ex.Message}");
                return new List<ExpenseCategory>();
            }
        }

        public List<ExpenseCategory> GetExpenseCategoriesByStatus(bool isActive)
        {
            try
            {
                return _repository.GetByStatus(isActive);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting expense categories by status: {ex.Message}");
                return new List<ExpenseCategory>();
            }
        }

        public string ValidateExpenseCategory(ExpenseCategory category)
        {
            if (category == null)
            {
                return "Category cannot be null";
            }

            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return "Category name is required";
            }

            if (category.CategoryName.Length > 100)
            {
                return "Category name cannot exceed 100 characters";
            }

            if (!string.IsNullOrWhiteSpace(category.CategoryCode) && category.CategoryCode.Length > 20)
            {
                return "Category code cannot exceed 20 characters";
            }

            if (!string.IsNullOrWhiteSpace(category.Description) && category.Description.Length > 255)
            {
                return "Description cannot exceed 255 characters";
            }

            // Check for duplicate category name
            if (IsCategoryNameExists(category.CategoryName, category.CategoryId))
            {
                return "Category name already exists";
            }

            // Check for duplicate category code (if provided)
            if (!string.IsNullOrWhiteSpace(category.CategoryCode) && IsCategoryCodeExists(category.CategoryCode, category.CategoryId))
            {
                return "Category code already exists";
            }

            return string.Empty;
        }

        public bool IsCategoryNameExists(string categoryName, int excludeId = 0)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoryName))
                {
                    return false;
                }

                return _repository.ExistsByName(categoryName.Trim(), excludeId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking category name exists: {ex.Message}");
                return false;
            }
        }

        public bool IsCategoryCodeExists(string categoryCode, int excludeId = 0)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoryCode))
                {
                    return false;
                }

                return _repository.ExistsByCode(categoryCode.Trim(), excludeId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking category code exists: {ex.Message}");
                return false;
            }
        }

        public string GenerateCategoryCode(string categoryName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoryName))
                {
                    return string.Empty;
                }

                // Take first 3 characters and make uppercase
                var baseCode = categoryName.Substring(0, Math.Min(3, categoryName.Length)).ToUpper();
                
                // Add a number suffix if needed to ensure uniqueness
                var code = baseCode;
                var counter = 1;
                
                while (IsCategoryCodeExists(code))
                {
                    code = $"{baseCode}{counter:D2}";
                    counter++;
                }

                return code;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating category code: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
