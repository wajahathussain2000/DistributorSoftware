using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Business
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService()
        {
            _expenseRepository = new ExpenseRepository();
        }

        public int CreateExpense(Expense expense)
        {
            // Validate expense
            if (!ValidateExpense(expense))
            {
                throw new ArgumentException("Invalid expense data");
            }

            // Generate codes if not provided
            if (string.IsNullOrEmpty(expense.ExpenseCode))
            {
                expense.ExpenseCode = GenerateExpenseCode();
            }

            if (string.IsNullOrEmpty(expense.Barcode))
            {
                expense.Barcode = GenerateBarcode();
            }

            // Set default values
            if (string.IsNullOrEmpty(expense.Status))
            {
                expense.Status = "PENDING";
            }

            if (expense.CreatedDate == DateTime.MinValue)
            {
                expense.CreatedDate = DateTime.Now;
            }

            return _expenseRepository.Create(expense);
        }

        public Expense GetExpenseById(int expenseId)
        {
            return _expenseRepository.GetById(expenseId);
        }

        public List<Expense> GetAllExpenses()
        {
            return _expenseRepository.GetAll();
        }

        public bool UpdateExpense(Expense expense)
        {
            if (!ValidateExpense(expense))
            {
                throw new ArgumentException("Invalid expense data");
            }

            expense.ModifiedDate = DateTime.Now;
            return _expenseRepository.Update(expense);
        }

        public bool DeleteExpense(int expenseId)
        {
            return _expenseRepository.Delete(expenseId);
        }

        public List<Expense> SearchExpenses(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAllExpenses();
            }

            return _expenseRepository.Search(searchTerm);
        }

        public List<Expense> GetExpensesByCategory(int categoryId)
        {
            return _expenseRepository.GetByCategory(categoryId);
        }

        public List<Expense> GetExpensesByStatus(string status)
        {
            return _expenseRepository.GetByStatus(status);
        }

        public List<Expense> GetExpensesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _expenseRepository.GetByDateRange(startDate, endDate);
        }

        public List<Expense> GetExpensesByAmountRange(decimal minAmount, decimal maxAmount)
        {
            return _expenseRepository.GetByAmountRange(minAmount, maxAmount);
        }

        public string GenerateExpenseCode()
        {
            string newCode;
            do
            {
                newCode = _expenseRepository.GetNextExpenseCode();
            } while (_expenseRepository.IsExpenseCodeExists(newCode));

            return newCode;
        }

        public string GenerateBarcode()
        {
            string newBarcode;
            do
            {
                newBarcode = _expenseRepository.GenerateBarcode();
            } while (_expenseRepository.IsBarcodeExists(newBarcode));

            return newBarcode;
        }

        public bool ValidateExpense(Expense expense)
        {
            if (expense == null)
                return false;

            // Required fields validation
            if (expense.CategoryId <= 0)
                return false;

            if (expense.Amount <= 0)
                return false;

            if (expense.ExpenseDate == DateTime.MinValue)
                return false;

            if (string.IsNullOrWhiteSpace(expense.Description))
                return false;

            // Business rules validation
            if (expense.Amount > 10000) // Maximum amount limit
                return false;

            if (expense.ExpenseDate > DateTime.Today)
                return false;

            if (expense.ExpenseDate < DateTime.Today.AddDays(-30))
                return false;

            if (expense.Description.Length < 10)
                return false;

            return true;
        }

        public bool ApproveExpense(int expenseId, int approvedBy)
        {
            var expense = GetExpenseById(expenseId);
            if (expense == null || expense.Status != "PENDING")
                return false;

            expense.Status = "APPROVED";
            expense.ApprovedBy = approvedBy;
            expense.ApprovedDate = DateTime.Now;
            expense.ModifiedDate = DateTime.Now;
            expense.ModifiedBy = approvedBy;

            return UpdateExpense(expense);
        }

        public bool RejectExpense(int expenseId, int rejectedBy, string reason)
        {
            var expense = GetExpenseById(expenseId);
            if (expense == null || expense.Status != "PENDING")
                return false;

            expense.Status = "REJECTED";
            expense.Remarks = reason;
            expense.ModifiedDate = DateTime.Now;
            expense.ModifiedBy = rejectedBy;

            return UpdateExpense(expense);
        }

        public bool SaveExpenseImage(int expenseId, byte[] imageData, string imagePath)
        {
            return _expenseRepository.SaveImage(expenseId, imageData, imagePath);
        }

        public byte[] GetExpenseImage(int expenseId)
        {
            return _expenseRepository.GetImageData(expenseId);
        }

        public string GetExpenseImagePath(int expenseId)
        {
            return _expenseRepository.GetImagePath(expenseId);
        }

        public bool IsExpenseCodeUnique(string expenseCode)
        {
            return !_expenseRepository.IsExpenseCodeExists(expenseCode);
        }

        public bool IsBarcodeUnique(string barcode)
        {
            return !_expenseRepository.IsBarcodeExists(barcode);
        }

        public List<Expense> GetExpensesForApproval()
        {
            return GetExpensesByStatus("PENDING");
        }

        public List<Expense> GetExpensesByUser(int userId)
        {
            return GetAllExpenses().Where(e => e.CreatedBy == userId).ToList();
        }
    }
}
