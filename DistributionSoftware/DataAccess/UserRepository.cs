using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
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
                        
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            
                            if (await reader.ReadAsync())
                            {
                                
                                // Check if user is valid (UserId should not be null)
                                if (!reader.IsDBNull(0))
                                {
                                    // Determine role name based on IsAdmin field
                                    bool isAdmin = Convert.ToBoolean(reader[7]); // IsAdmin is at index 7
                                    string roleName = isAdmin ? "Admin" : "Manager"; // Default mapping
                                    
                                    return new User
                                    {
                                        UserId = Convert.ToInt32(reader[0]),
                                        Username = reader[1].ToString(),
                                        Email = reader[2].ToString(),
                                        PasswordHash = reader[3].ToString(),
                                        FirstName = reader[4].ToString(),
                                        LastName = reader[5].ToString(),
                                        IsActive = Convert.ToBoolean(reader[6]),
                                        CreatedDate = reader.GetDateTime(9), // CreatedDate is at index 9
                                        LastLoginDate = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8), // LastLoginDate is at index 8
                                        RoleId = isAdmin ? 1 : 2, // Admin = 1, Manager = 2
                                        RoleName = roleName
                                    };
                                }
                            }
                            else
                            {
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
                                    UserId = Convert.ToInt32(reader[0]),
                                    Username = reader[1].ToString(),
                                    Email = reader[2].ToString(),
                                    PasswordHash = reader[3].ToString(),
                                    FirstName = reader[4].ToString(),
                                    LastName = reader[5].ToString(),
                                    IsActive = Convert.ToBoolean(reader[6]),
                                    CreatedDate = reader.GetDateTime(7),
                                    LastLoginDate = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8),
                                    RoleId = Convert.ToInt32(reader[9]),
                                    RoleName = reader[10].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
                    
                    // Test if Users table exists
                    var query = "SELECT COUNT(*) FROM Users";
                    using (var command = new SqlCommand(query, connection))
                    {
                        var count = await command.ExecuteScalarAsync();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // New User Management Methods
        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = new List<User>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT u.UserId, u.Username, u.Email, u.FirstName, u.LastName, 
                               u.IsActive, u.IsAdmin, u.LastLoginDate, u.CreatedDate, u.ModifiedDate,
                               r.RoleId, r.RoleName
                        FROM Users u
                        LEFT JOIN UserRoles ur ON u.UserId = ur.UserId
                        LEFT JOIN Roles r ON ur.RoleId = r.RoleId
                        ORDER BY u.CreatedDate DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                users.Add(new User
                                {
                                    UserId = Convert.ToInt32(reader[0]),
                                    Username = reader[1].ToString(),
                                    Email = reader[2].ToString(),
                                    FirstName = reader.IsDBNull(3) ? "" : reader[3].ToString(),
                                    LastName = reader.IsDBNull(4) ? "" : reader[4].ToString(),
                                    IsActive = Convert.ToBoolean(reader[5]),
                                    IsAdmin = Convert.ToBoolean(reader[6]),
                                    LastLoginDate = reader.IsDBNull(7) ? null : (DateTime?)reader.GetDateTime(7),
                                    CreatedDate = reader.GetDateTime(8),
                                    ModifiedDate = reader.IsDBNull(9) ? null : (DateTime?)reader.GetDateTime(9),
                                    RoleId = reader.IsDBNull(10) ? 0 : Convert.ToInt32(reader[10]),
                                    RoleName = reader.IsDBNull(11) ? "No Role" : reader[11].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return users;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            var roles = new List<Role>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "SELECT RoleId, RoleName, Description, IsActive, CreatedDate FROM Roles WHERE IsActive = 1 ORDER BY RoleName";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                roles.Add(new Role
                                {
                                    RoleId = Convert.ToInt32(reader[0]),
                                    RoleName = reader[1].ToString(),
                                    Description = reader.IsDBNull(2) ? "" : reader[2].ToString(),
                                    IsActive = Convert.ToBoolean(reader[3]),
                                    CreatedDate = reader.GetDateTime(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return roles;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        INSERT INTO Users (Username, Email, PasswordHash, FirstName, LastName, IsActive, IsAdmin, CreatedDate)
                        VALUES (@Username, @Email, @PasswordHash, @FirstName, @LastName, @IsActive, @IsAdmin, @CreatedDate);
                        SELECT SCOPE_IDENTITY();";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                        command.Parameters.AddWithValue("@FirstName", user.FirstName ?? "");
                        command.Parameters.AddWithValue("@LastName", user.LastName ?? "");
                        command.Parameters.AddWithValue("@IsActive", user.IsActive);
                        command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        
                        var userId = await command.ExecuteScalarAsync();
                        
                        // Assign role if specified
                        if (user.RoleId > 0)
                        {
                            await AssignRoleToUserAsync(Convert.ToInt32(userId), user.RoleId);
                        }
                        
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return false;
        }

        public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // First remove existing role assignment
                    var deleteQuery = "DELETE FROM UserRoles WHERE UserId = @UserId";
                    using (var deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@UserId", userId);
                        await deleteCommand.ExecuteNonQueryAsync();
                    }
                    
                    // Then assign new role
                    var insertQuery = "INSERT INTO UserRoles (UserId, RoleId, CreatedDate) VALUES (@UserId, @RoleId, @CreatedDate)";
                    using (var insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@UserId", userId);
                        insertCommand.Parameters.AddWithValue("@RoleId", roleId);
                        insertCommand.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        
                        var rowsAffected = await insertCommand.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return false;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT u.UserId, u.Username, u.Email, u.FirstName, u.LastName, 
                               u.IsActive, u.IsAdmin, u.LastLoginDate, u.CreatedDate, u.ModifiedDate,
                               r.RoleId, r.RoleName
                        FROM Users u
                        LEFT JOIN UserRoles ur ON u.UserId = ur.UserId
                        LEFT JOIN Roles r ON ur.RoleId = r.RoleId
                        WHERE u.UserId = @UserId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new User
                                {
                                    UserId = Convert.ToInt32(reader[0]),
                                    Username = reader[1].ToString(),
                                    Email = reader[2].ToString(),
                                    FirstName = reader.IsDBNull(3) ? "" : reader[3].ToString(),
                                    LastName = reader.IsDBNull(4) ? "" : reader[4].ToString(),
                                    IsActive = Convert.ToBoolean(reader[5]),
                                    IsAdmin = Convert.ToBoolean(reader[6]),
                                    LastLoginDate = reader.IsDBNull(7) ? null : (DateTime?)reader.GetDateTime(7),
                                    CreatedDate = reader.GetDateTime(8),
                                    ModifiedDate = reader.IsDBNull(9) ? null : (DateTime?)reader.GetDateTime(9),
                                    RoleId = reader.IsDBNull(10) ? 0 : Convert.ToInt32(reader[10]),
                                    RoleName = reader.IsDBNull(11) ? "No Role" : reader[11].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return null;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        UPDATE Users 
                        SET Username = @Username, Email = @Email, FirstName = @FirstName, 
                            LastName = @LastName, IsActive = @IsActive, IsAdmin = @IsAdmin, 
                            ModifiedDate = @ModifiedDate
                        WHERE UserId = @UserId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", user.UserId);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@FirstName", user.FirstName ?? "");
                        command.Parameters.AddWithValue("@LastName", user.LastName ?? "");
                        command.Parameters.AddWithValue("@IsActive", user.IsActive);
                        command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                        command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        
                        // Update role if specified
                        if (user.RoleId > 0)
                        {
                            await AssignRoleToUserAsync(user.UserId, user.RoleId);
                        }
                        
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Soft delete - set IsActive to false
                    var query = "UPDATE Users SET IsActive = 0, ModifiedDate = @ModifiedDate WHERE UserId = @UserId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return false;
        }

        public async Task<bool> ChangeUserPasswordAsync(int userId, string passwordHash)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "UPDATE Users SET PasswordHash = @PasswordHash, ModifiedDate = @ModifiedDate WHERE UserId = @UserId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return false;
        }

        public async Task<List<Permission>> GetPermissionsByRoleAsync(int roleId)
        {
            var permissions = new List<Permission>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT p.PermissionId, p.PermissionName, p.PermissionCode, p.Description, p.Module, p.IsActive, p.CreatedDate
                        FROM Permissions p
                        INNER JOIN RolePermissions rp ON p.PermissionId = rp.PermissionId
                        WHERE rp.RoleId = @RoleId AND p.IsActive = 1
                        ORDER BY p.Module, p.PermissionName";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoleId", roleId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                permissions.Add(new Permission
                                {
                                    PermissionId = Convert.ToInt32(reader[0]),
                                    PermissionName = reader[1].ToString(),
                                    PermissionCode = reader[2].ToString(),
                                    Description = reader.IsDBNull(3) ? "" : reader[3].ToString(),
                                    Module = reader.IsDBNull(4) ? "" : reader[4].ToString(),
                                    IsActive = Convert.ToBoolean(reader[5]),
                                    CreatedDate = reader.GetDateTime(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return permissions;
        }

    }
}
