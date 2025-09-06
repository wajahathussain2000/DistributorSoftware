using DistributionSoftware.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.DataAccess
{
    public interface IPurchaseReturnItemRepository
    {
        Task<List<PurchaseReturnItem>> GetByPurchaseReturnIdAsync(int purchaseReturnId);
        Task<PurchaseReturnItem> GetByIdAsync(int purchaseReturnItemId);
        Task<int> CreateAsync(PurchaseReturnItem item);
        Task<bool> UpdateAsync(PurchaseReturnItem item);
        Task<bool> DeleteAsync(int purchaseReturnItemId);
        Task<bool> DeleteByPurchaseReturnIdAsync(int purchaseReturnId);
        Task<decimal> CalculateLineTotalAsync(int productId, decimal quantity, decimal unitPrice);
    }
}
