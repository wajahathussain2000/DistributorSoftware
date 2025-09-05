using DistributionSoftware.Models;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace DistributionSoftware.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Authenticating user with email: {email}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Direct SQL query for authentication with plain text passwords
                    // Try different possible table names
                    var query = @"
                        SELECT UserId, Username, Email, PasswordHash, FirstName, LastName, 
                               IsActive, IsAdmin, LastLoginDate, CreatedDate, ModifiedDate
                        FROM Users 
                        WHERE Email = @Email AND PasswordHash = @Password AND IsActive = 1";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        
                        System.Diagnostics.Debug.WriteLine($"Executing query with email: {email}, password: {password}");
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            System.Diagnostics.Debug.WriteLine("Query executed, checking for results...");
                            
                            if (await reader.ReadAsync())
                            {
                                System.Diagnostics.Debug.WriteLine("User found in database!");
                                
                                // Check if user is valid (UserId should not be null)
                                if (!reader.IsDBNull(0))
                                {
                                    // Determine role name based on IsAdmin field
                                    bool isAdmin = reader.GetBoolean(7); // IsAdmin is at index 7
                                    string roleName = isAdmin ? "Admin" : "Manager"; // Default mapping
                                    System.Diagnostics.Debug.WriteLine($"User authentication - IsAdmin: {isAdmin}, RoleName: {roleName}");
                                    
                                    return new User
                                    {
                                        UserId = reader.GetInt32(0),
                                        Username = reader.GetString(1),
                                        Email = reader.GetString(2),
                                        PasswordHash = reader.GetString(3),
                                        FirstName = reader.GetString(4),
                                        LastName = reader.GetString(5),
                                        IsActive = reader.GetBoolean(6),
                                        CreatedDate = reader.GetDateTime(9), // CreatedDate is at index 9
                                        LastLoginDate = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8), // LastLoginDate is at index 8
                                        RoleId = isAdmin ? 1 : 2, // Admin = 1, Manager = 2
                                        RoleName = roleName
                                    };
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("No user found with provided credentials");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Authentication error: {0}", ex.Message));
            }
            return null;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Use stored procedure to get user by email
                    var query = "sp_GetUserByEmail";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new User
                                {
                                    UserId = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    PasswordHash = reader.GetString(3),
                                    FirstName = reader.GetString(4),
                                    LastName = reader.GetString(5),
                                    IsActive = reader.GetBoolean(6),
                                    CreatedDate = reader.GetDateTime(7),
                                    LastLoginDate = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8),
                                    RoleId = reader.GetInt32(9),
                                    RoleName = reader.GetString(10)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("GetUserByEmail error: {0}", ex.Message));
            }
            return null;
        }

        public async Task<bool> UpdateLastLoginAsync(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Use stored procedure to update last login
                    var query = "sp_UpdateLastLogin";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("UpdateLastLogin error: {0}", ex.Message));
            }
            return false;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Use stored procedure to check if email exists
                    var query = "sp_CheckEmailExists";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("IsEmailExists error: {0}", ex.Message));
            }
            return false;
        }

        public async Task<bool> TestDatabaseConnectionAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    System.Diagnostics.Debug.WriteLine("Database connection successful");
                    
                    // Test if Users table exists
                    var query = "SELECT COUNT(*) FROM Users";
                    using (var command = new SqlCommand(query, connection))
                    {
                        var count = await command.ExecuteScalarAsync();
                        System.Diagnostics.Debug.WriteLine($"Users table has {count} records");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database connection test failed: {ex.Message}");
                return false;
            }
        }


    }
}
