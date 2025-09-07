using DistributionSoftware.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.DataAccess
{
    public interface IPurchaseReturnItemRepository
    {
        Task<List<PurchaseReturnItem>> GetByReturnIdAsync(int returnId);
        Task<PurchaseReturnItem> GetByIdAsync(int returnItemId);
        Task<int> CreateAsync(PurchaseReturnItem item);
        Task<bool> UpdateAsync(PurchaseReturnItem item);
        Task<bool> DeleteAsync(int returnItemId);
        Task<bool> DeleteByReturnIdAsync(int returnId);
        Task<decimal> CalculateLineTotalAsync(int productId, decimal quantity, decimal unitPrice);
    }
}
