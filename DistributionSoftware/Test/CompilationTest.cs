using System;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Test
{
    class CompilationTest
    {
        static void Main(string[] args)
        {
            try
            {
                // Test if our classes can be instantiated
                string connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
                var userRepository = new UserRepository(connectionString);
                var userService = new UserService(userRepository);
                
                // Test if our models can be created
                var user = new User
                {
                    Username = "test",
                    Email = "test@test.com",
                    FirstName = "Test",
                    LastName = "User",
                    IsActive = true,
                    IsAdmin = false
                };
                
                var role = new Role
                {
                    RoleName = "Test Role",
                    Description = "Test Description",
                    IsActive = true
                };
                
                var permission = new Permission
                {
                    PermissionName = "Test Permission",
                    PermissionCode = "TEST_CODE",
                    Description = "Test Description",
                    Module = "Test Module",
                    IsActive = true
                };
                
                Console.WriteLine("✅ All classes compile successfully!");
                Console.WriteLine($"User: {user.FullName}");
                Console.WriteLine($"Role: {role.RoleName}");
                Console.WriteLine($"Permission: {permission.PermissionName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Compilation error: {ex.Message}");
            }
        }
    }
}
