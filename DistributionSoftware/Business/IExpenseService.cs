using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IExpenseService
    {
        // CRUD Operations
        int CreateExpense(Expense expense);
        Expense GetExpenseById(int expenseId);
        List<Expense> GetAllExpenses();
        bool UpdateExpense(Expense expense);
        bool DeleteExpense(int expenseId);
        
        // Search and Filter Operations
        List<Expense> SearchExpenses(string searchTerm);
        List<Expense> GetExpensesByCategory(int categoryId);
        List<Expense> GetExpensesByStatus(string status);
        List<Expense> GetExpensesByDateRange(DateTime startDate, DateTime endDate);
        List<Expense> GetExpensesByAmountRange(decimal minAmount, decimal maxAmount);
        
        // Business Logic Operations
        string GenerateExpenseCode();
        string GenerateBarcode();
        bool ValidateExpense(Expense expense);
        bool ApproveExpense(int expenseId, int approvedBy);
        bool RejectExpense(int expenseId, int rejectedBy, string reason);
        
        // Image Operations
        bool SaveExpenseImage(int expenseId, byte[] imageData, string imagePath);
        byte[] GetExpenseImage(int expenseId);
        string GetExpenseImagePath(int expenseId);
        
        // Utility Operations
        bool IsExpenseCodeUnique(string expenseCode);
        bool IsBarcodeUnique(string barcode);
        List<Expense> GetExpensesForApproval();
        List<Expense> GetExpensesByUser(int userId);
    }
}
