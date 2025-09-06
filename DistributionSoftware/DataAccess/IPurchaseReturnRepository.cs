using DistributionSoftware.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.DataAccess
{
    public interface IPurchaseReturnRepository
    {
        Task<PurchaseReturn> GetByIdAsync(int purchaseReturnId);
        Task<PurchaseReturn> GetByReturnNumberAsync(string returnNumber);
        Task<List<PurchaseReturn>> GetAllAsync();
        Task<List<PurchaseReturn>> GetBySupplierIdAsync(int supplierId);
        Task<List<PurchaseReturn>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<PurchaseReturn>> GetByStatusAsync(string status);
        Task<string> GetNextReturnNumberAsync();
        Task<int> CreateAsync(PurchaseReturn purchaseReturn);
        Task<bool> UpdateAsync(PurchaseReturn purchaseReturn);
        Task<bool> DeleteAsync(int purchaseReturnId);
        Task<bool> PostPurchaseReturnAsync(int purchaseReturnId);
        Task<bool> CancelPurchaseReturnAsync(int purchaseReturnId);
        Task<decimal> CalculateNetReturnAmountAsync(int purchaseReturnId);
    }
}
