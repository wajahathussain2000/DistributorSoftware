using System.Collections.Generic;
using System.Threading.Tasks;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface ISalesmanRepository
    {
        Task<List<Salesman>> GetAllSalesmenAsync();
        Task<List<Salesman>> GetActiveSalesmenAsync();
        Task<Salesman> GetSalesmanByIdAsync(int salesmanId);
        Task<Salesman> GetSalesmanByCodeAsync(string salesmanCode);
        Task<int> CreateSalesmanAsync(Salesman salesman);
        Task<bool> UpdateSalesmanAsync(Salesman salesman);
        Task<bool> DeleteSalesmanAsync(int salesmanId);
        Task<bool> CreateSalesmanTableIfNotExistsAsync();
    }
}
