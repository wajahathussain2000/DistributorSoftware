using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DistributionSoftware.DataAccess
{
    /// <summary>
    /// Repository for permission-related data access operations
    /// </summary>
    public class PermissionRepository : IPermissionRepository
    {
        private readonly string _connectionString;

        public PermissionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Permission Management

        public async Task<IEnumerable<Permission>> GetAllActivePermissionsAsync()
        {
            var permissions = new List<Permission>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "sp_GetAllActivePermissions";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                permissions.Add(new Permission
                                {
                                    PermissionId = Convert.ToInt32(reader[0]),
                                    PermissionName = reader[1].ToString(),
                                    PermissionCode = reader[2].ToString(),
                                    Description = reader.IsDBNull(3) ? string.Empty : reader[3].ToString(),
                                    Module = reader[4].ToString(),
                                    IsActive = reader.GetBoolean(5),
                                    CreatedDate = reader.GetDateTime(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllActivePermissions error: {ex.Message}");
            }
            
            return permissions;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByModuleAsync(string module)
        {
            var permissions = new List<Permission>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "sp_GetPermissionsByModule";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Module", module);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                permissions.Add(new Permission
                                {
                                    PermissionId = Convert.ToInt32(reader[0]),
                                    PermissionName = reader[1].ToString(),
                                    PermissionCode = reader[2].ToString(),
                                    Description = reader.IsDBNull(3) ? string.Empty : reader[3].ToString(),
                                    Module = reader[4].ToString(),
                                    IsActive = reader.GetBoolean(5),
                                    CreatedDate = reader.GetDateTime(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetPermissionsByModule error: {ex.Message}");
            }
            
            return permissions;
        }

        public async Task<Permission> GetPermissionByCodeAsync(string permissionCode)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "sp_GetPermissionByCode";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PermissionCode", permissionCode);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Permission
                                {
                                    PermissionId = Convert.ToInt32(reader[0]),
                                    PermissionName = reader[1].ToString(),
                                    PermissionCode = reader[2].ToString(),
                                    Description = reader.IsDBNull(3) ? string.Empty : reader[3].ToString(),
                                    Module = reader[4].ToString(),
                                    IsActive = reader.GetBoolean(5),
                                    CreatedDate = reader.GetDateTime(6)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetPermissionByCode error: {ex.Message}");
            }
            
            return null;
        }

        public async Task<bool> CreatePermissionAsync(Permission permission)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "sp_CreatePermission";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PermissionName", permission.PermissionName);
                        command.Parameters.AddWithValue("@PermissionCode", permission.PermissionCode);
                        command.Parameters.AddWithValue("@Description", (object)permission.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Module", permission.Module);
                        command.Parameters.AddWithValue("@IsActive", permission.IsActive);
                        command.Parameters.AddWithValue("@CreatedDate", permission.CreatedDate);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CreatePermission error: {ex.Message}");
            }
            
            return false;
        }

        public async Task<bool> UpdatePermissionAsync(Permission permission)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "sp_UpdatePermission";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PermissionId", permission.PermissionId);
                        command.Parameters.AddWithValue("@PermissionName", permission.PermissionName);
                        command.Parameters.AddWithValue("@PermissionCode", permission.PermissionCode);
                        command.Parameters.AddWithValue("@Description", (object)permission.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Module", permission.Module);
                        command.Parameters.AddWithValue("@IsActive", permission.IsActive);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdatePermission error: {ex.Message}");
            }
            
            return false;
        }

        public async Task<bool> DeactivatePermissionAsync(int permissionId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "sp_DeactivatePermission";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PermissionId", permissionId);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeactivatePermission error: {ex.Message}");
            }
            
            return false;
        }

        #endregion

        #region Role Permission Management

        public async Task<IEnumerable<Permission>> GetPermissionsByRoleAsync(int roleId)
        {
            var permissions = new List<Permission>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "sp_GetPermissionsByRole";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
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
                                    Description = reader.IsDBNull(3) ? string.Empty : reader[3].ToString(),
                                    Module = reader[4].ToString(),
                                    IsActive = reader.GetBoolean(5),
                                    CreatedDate = reader.GetDateTime(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetPermissionsByRole error: {ex.Message}");
            }
            
            return permissions;
        }

        public async Task<bool> AssignPermissionToRoleAsync(int roleId, int permissionId, bool isGranted)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "sp_AssignPermissionToRole";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoleId", roleId);
                        command.Parameters.AddWithValue("@PermissionId", permissionId);
                        command.Parameters.AddWithValue("@IsGranted", isGranted);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AssignPermissionToRole error: {ex.Message}");
            }
            
            return false;
        }

        public async Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "sp_RemovePermissionFromRole";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RoleId", roleId);
                        command.Parameters.AddWithValue("@PermissionId", permissionId);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RemovePermissionFromRole error: {ex.Message}");
            }
            
            return false;
        }

        #endregion
    }
}
