using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
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
    }
}
