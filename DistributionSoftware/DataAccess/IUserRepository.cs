using DistributionSoftware.Models;
using System.Threading.Tasks;

namespace DistributionSoftware.DataAccess
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UpdateLastLoginAsync(int userId);
        Task<bool> IsEmailExistsAsync(string email);
    }
}
