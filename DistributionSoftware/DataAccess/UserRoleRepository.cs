using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly string _connectionString;

        public UserRoleRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? Common.ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public int Create(UserRole userRole)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO UserRoles (UserId, RoleId, AssignedDate, RevokedDate, IsActive) VALUES (@UserId, @RoleId, @AssignedDate, @RevokedDate, @IsActive); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userRole.UserId);
                    command.Parameters.AddWithValue("@RoleId", userRole.RoleId);
                    command.Parameters.AddWithValue("@AssignedDate", userRole.AssignedDate);
                    command.Parameters.AddWithValue("@RevokedDate", userRole.RevokedDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", userRole.IsActive);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(UserRole userRole)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE UserRoles SET UserId = @UserId, RoleId = @RoleId, AssignedDate = @AssignedDate, RevokedDate = @RevokedDate, IsActive = @IsActive WHERE UserRoleId = @UserRoleId", connection))
                {
                    command.Parameters.AddWithValue("@UserRoleId", userRole.UserRoleId);
                    command.Parameters.AddWithValue("@UserId", userRole.UserId);
                    command.Parameters.AddWithValue("@RoleId", userRole.RoleId);
                    command.Parameters.AddWithValue("@AssignedDate", userRole.AssignedDate);
                    command.Parameters.AddWithValue("@RevokedDate", userRole.RevokedDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", userRole.IsActive);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int userRoleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM UserRoles WHERE UserRoleId = @UserRoleId", connection))
                {
                    command.Parameters.AddWithValue("@UserRoleId", userRoleId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public UserRole GetById(int userRoleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM UserRoles WHERE UserRoleId = @UserRoleId", connection))
                {
                    command.Parameters.AddWithValue("@UserRoleId", userRoleId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapUserRole(reader);
                        return null;
                    }
                }
            }
        }

        public UserRole GetByUserAndRole(int userId, int roleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM UserRoles WHERE UserId = @UserId AND RoleId = @RoleId AND IsActive = 1", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapUserRole(reader);
                        return null;
                    }
                }
            }
        }

        public List<UserRole> GetAll()
        {
            var userRoles = new List<UserRole>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM UserRoles ORDER BY AssignedDate DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        userRoles.Add(MapUserRole(reader));
                }
            }
            return userRoles;
        }

        public List<UserRole> GetActive()
        {
            var userRoles = new List<UserRole>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM UserRoles WHERE IsActive = 1 ORDER BY AssignedDate DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        userRoles.Add(MapUserRole(reader));
                }
            }
            return userRoles;
        }

        public List<UserRole> GetByUserId(int userId)
        {
            var userRoles = new List<UserRole>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM UserRoles WHERE UserId = @UserId ORDER BY AssignedDate DESC", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            userRoles.Add(MapUserRole(reader));
                    }
                }
            }
            return userRoles;
        }

        public List<UserRole> GetByRoleId(int roleId)
        {
            var userRoles = new List<UserRole>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM UserRoles WHERE RoleId = @RoleId ORDER BY AssignedDate DESC", connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            userRoles.Add(MapUserRole(reader));
                    }
                }
            }
            return userRoles;
        }

        public List<UserRole> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            var userRoles = new List<UserRole>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM UserRoles WHERE 1=1";
                var command = new SqlCommand();

                if (startDate.HasValue)
                {
                    query += " AND AssignedDate >= @StartDate";
                    command.Parameters.AddWithValue("@StartDate", startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query += " AND AssignedDate <= @EndDate";
                    command.Parameters.AddWithValue("@EndDate", endDate.Value);
                }

                if (isActive.HasValue)
                {
                    query += " AND IsActive = @IsActive";
                    command.Parameters.AddWithValue("@IsActive", isActive.Value);
                }

                query += " ORDER BY AssignedDate DESC";
                command.CommandText = query;
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        userRoles.Add(MapUserRole(reader));
                }
            }
            return userRoles;
        }

        public int GetCount(bool? isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM UserRoles";
                var command = new SqlCommand();

                if (isActive.HasValue)
                {
                    query += " WHERE IsActive = @IsActive";
                    command.Parameters.AddWithValue("@IsActive", isActive.Value);
                }

                command.CommandText = query;
                command.Connection = connection;

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private UserRole MapUserRole(IDataReader reader)
        {
            return new UserRole
            {
                UserRoleId = Convert.ToInt32(reader["UserRoleId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                RoleId = Convert.ToInt32(reader["RoleId"]),
                AssignedDate = Convert.ToDateTime(reader["AssignedDate"]),
                RevokedDate = reader["RevokedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["RevokedDate"]),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };
        }
    }
}
