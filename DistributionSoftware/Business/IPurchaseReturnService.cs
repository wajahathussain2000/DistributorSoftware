using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.Business
{
    public interface IPurchaseReturnService
    {
        Task<PurchaseReturn> GetPurchaseReturnByIdAsync(int purchaseReturnId);
        Task<PurchaseReturn> GetPurchaseReturnByNumberAsync(string returnNumber);
        Task<List<PurchaseReturn>> GetAllPurchaseReturnsAsync();
        Task<List<PurchaseReturn>> GetPurchaseReturnsBySupplierAsync(int supplierId);
        Task<List<PurchaseReturn>> GetPurchaseReturnsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<string> GenerateNextReturnNumberAsync();
        Task<string> GenerateBarcodeAsync(string returnNumber);
        Task<int> CreatePurchaseReturnAsync(PurchaseReturn purchaseReturn);
        Task<bool> UpdatePurchaseReturnAsync(PurchaseReturn purchaseReturn);
        Task<bool> DeletePurchaseReturnAsync(int purchaseReturnId);
        Task<bool> SaveDraftAsync(PurchaseReturn purchaseReturn);
        Task<bool> PostPurchaseReturnAsync(int purchaseReturnId);
        Task<bool> CancelPurchaseReturnAsync(int purchaseReturnId);
        Task<List<PurchaseReturnItem>> GetProductsFromInvoiceAsync(int invoiceId);
        Task<bool> ValidateReturnQuantityAsync(int productId, int invoiceId, decimal returnQuantity);
        Task<decimal> GetActualAvailableStockAsync(int productId);
        Task<decimal> CalculateNetReturnAmountAsync(PurchaseReturn purchaseReturn);
        Task<bool> ValidatePurchaseReturnAsync(PurchaseReturn purchaseReturn);
        Task<List<PurchaseReturnItem>> GetPurchaseReturnItemsAsync(int purchaseReturnId);
        Task<bool> AddPurchaseReturnItemAsync(PurchaseReturnItem item);
        Task<bool> UpdatePurchaseReturnItemAsync(PurchaseReturnItem item);
        Task<bool> RemovePurchaseReturnItemAsync(int itemId);
        Task<bool> RemoveAllPurchaseReturnItemsAsync(int purchaseReturnId);
    }
}
