using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IExpenseRepository
    {
        // CRUD Operations
        int Create(Expense expense);
        Expense GetById(int expenseId);
        List<Expense> GetAll();
        bool Update(Expense expense);
        bool Delete(int expenseId);
        
        // Search and Filter Operations
        List<Expense> Search(string searchTerm);
        List<Expense> GetByCategory(int categoryId);
        List<Expense> GetByStatus(string status);
        List<Expense> GetByDateRange(DateTime startDate, DateTime endDate);
        List<Expense> GetByAmountRange(decimal minAmount, decimal maxAmount);
        
        // Utility Operations
        bool IsExpenseCodeExists(string expenseCode);
        bool IsBarcodeExists(string barcode);
        string GetNextExpenseCode();
        string GenerateBarcode();
        
        // Image Operations
        bool SaveImage(int expenseId, byte[] imageData, string imagePath);
        byte[] GetImageData(int expenseId);
        string GetImagePath(int expenseId);
    }
}
