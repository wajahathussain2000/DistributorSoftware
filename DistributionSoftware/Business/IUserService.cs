using DistributionSoftware.Models;
using System.Threading.Tasks;

namespace DistributionSoftware.Business
{
    public interface IUserService
    {
        Task<User> AuthenticateUserAsync(string email, string password);
        Task<bool> IsValidUserAsync(string email);
        Task<string> GetUserFullNameAsync(string email);
    }
}
