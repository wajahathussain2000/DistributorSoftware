using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        public RoleRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? Common.ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public int Create(Role role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Roles (RoleName, Description, IsActive, CreatedDate, CreatedBy) VALUES (@RoleName, @Description, @IsActive, @CreatedDate, @CreatedBy); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@RoleName", role.RoleName);
                    command.Parameters.AddWithValue("@Description", role.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", role.IsActive);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@CreatedBy", role.CreatedBy);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(Role role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Roles SET RoleName = @RoleName, Description = @Description, IsActive = @IsActive, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy WHERE RoleId = @RoleId", connection))
                {
                    command.Parameters.AddWithValue("@RoleId", role.RoleId);
                    command.Parameters.AddWithValue("@RoleName", role.RoleName);
                    command.Parameters.AddWithValue("@Description", role.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", role.IsActive);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ModifiedBy", role.ModifiedBy);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int roleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Roles WHERE RoleId = @RoleId", connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public Role GetById(int roleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Roles WHERE RoleId = @RoleId", connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapRole(reader);
                        return null;
                    }
                }
            }
        }

        public Role GetByName(string roleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Roles WHERE RoleName = @RoleName", connection))
                {
                    command.Parameters.AddWithValue("@RoleName", roleName);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapRole(reader);
                        return null;
                    }
                }
            }
        }

        public List<Role> GetAll()
        {
            var roles = new List<Role>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Roles ORDER BY RoleName", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        roles.Add(MapRole(reader));
                }
            }
            return roles;
        }

        public List<Role> GetActive()
        {
            var roles = new List<Role>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Roles WHERE IsActive = 1 ORDER BY RoleName", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        roles.Add(MapRole(reader));
                }
            }
            return roles;
        }

        public List<Role> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            var roles = new List<Role>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Roles WHERE 1=1";
                var command = new SqlCommand();

                if (startDate.HasValue)
                {
                    query += " AND CreatedDate >= @StartDate";
                    command.Parameters.AddWithValue("@StartDate", startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query += " AND CreatedDate <= @EndDate";
                    command.Parameters.AddWithValue("@EndDate", endDate.Value);
                }

                if (isActive.HasValue)
                {
                    query += " AND IsActive = @IsActive";
                    command.Parameters.AddWithValue("@IsActive", isActive.Value);
                }

                query += " ORDER BY RoleName";
                command.CommandText = query;
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        roles.Add(MapRole(reader));
                }
            }
            return roles;
        }

        public int GetCount(bool? isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM Roles";
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

        private Role MapRole(IDataReader reader)
        {
            return new Role
            {
                RoleId = Convert.ToInt32(reader["RoleId"]),
                RoleName = reader["RoleName"].ToString(),
                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"])
            };
        }
    }
}
