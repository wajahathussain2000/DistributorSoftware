using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            // Business logic validation
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            // Authenticate user through repository
            var user = await _userRepository.AuthenticateUserAsync(email, password);
            
            if (user != null && user.IsActive)
            {
                return user;
            }

            return null;
        }

        public async Task<bool> IsValidUserAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return await _userRepository.IsEmailExistsAsync(email);
        }

        public async Task<string> GetUserFullNameAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user != null)
            {
                return string.Format("{0} {1}", user.FirstName, user.LastName).Trim();
            }
            return string.Empty;
        }

        // New User Management Methods
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public List<User> GetAllUsers()
        {
            return GetAllUsersAsync().Result;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            // Business validation
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Email))
            {
                return false;
            }

            // Check if email already exists
            if (await _userRepository.IsEmailExistsAsync(user.Email))
            {
                return false;
            }

            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            // Business validation
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Email))
            {
                return false;
            }

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<bool> ChangeUserPasswordAsync(int userId, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                return false;
            }

            return await _userRepository.ChangeUserPasswordAsync(userId, newPassword);
        }

        public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
        {
            return await _userRepository.AssignRoleToUserAsync(userId, roleId);
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _userRepository.GetAllRolesAsync();
        }

        public async Task<List<Permission>> GetPermissionsByRoleAsync(int roleId)
        {
            return await _userRepository.GetPermissionsByRoleAsync(roleId);
        }
    }
}
