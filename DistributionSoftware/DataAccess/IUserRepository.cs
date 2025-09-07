using DistributionSoftware.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.DataAccess
{
    public interface IUserRepository
    {
        // Authentication methods
        Task<User> AuthenticateUserAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UpdateLastLoginAsync(int userId);
        Task<bool> IsEmailExistsAsync(string email);
        
        // User management methods
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> ChangeUserPasswordAsync(int userId, string passwordHash);
        Task<bool> AssignRoleToUserAsync(int userId, int roleId);
        Task<List<Role>> GetAllRolesAsync();
        Task<List<Permission>> GetPermissionsByRoleAsync(int roleId);
    }
}
