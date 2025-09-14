using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class ReorderLevelRepository : IReorderLevelRepository
    {
        private readonly string _connectionString;

        public ReorderLevelRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public ReorderLevelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreateReorderLevel(ReorderLevel reorderLevel)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"INSERT INTO ReorderLevels 
                               (ProductId, MinimumLevel, MaximumLevel, ReorderQuantity, 
                                IsActive, AlertEnabled, LastAlertDate, CreatedBy, CreatedByName, CreatedDate)
                               VALUES 
                               (@ProductId, @MinimumLevel, @MaximumLevel, @ReorderQuantity, 
                                @IsActive, @AlertEnabled, @LastAlertDate, @CreatedBy, @CreatedByName, @CreatedDate);
                               SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", reorderLevel.ProductId);
                        command.Parameters.AddWithValue("@MinimumLevel", reorderLevel.MinimumLevel);
                        command.Parameters.AddWithValue("@MaximumLevel", reorderLevel.MaximumLevel);
                        command.Parameters.AddWithValue("@ReorderQuantity", reorderLevel.ReorderQuantity);
                        command.Parameters.AddWithValue("@IsActive", reorderLevel.IsActive);
                        command.Parameters.AddWithValue("@AlertEnabled", reorderLevel.AlertEnabled);
                        command.Parameters.AddWithValue("@LastAlertDate", reorderLevel.LastAlertDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", reorderLevel.CreatedBy);
                        command.Parameters.AddWithValue("@CreatedByName", reorderLevel.CreatedByName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedDate", reorderLevel.CreatedDate);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.CreateReorderLevel", ex);
                throw;
            }
        }

        public bool UpdateReorderLevel(ReorderLevel reorderLevel)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"UPDATE ReorderLevels SET 
                               ProductId = @ProductId,
                               MinimumLevel = @MinimumLevel,
                               MaximumLevel = @MaximumLevel,
                               ReorderQuantity = @ReorderQuantity,
                               IsActive = @IsActive,
                               AlertEnabled = @AlertEnabled,
                               LastAlertDate = @LastAlertDate,
                               ModifiedBy = @ModifiedBy,
                               ModifiedByName = @ModifiedByName,
                               ModifiedDate = @ModifiedDate
                               WHERE ReorderLevelId = @ReorderLevelId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReorderLevelId", reorderLevel.ReorderLevelId);
                        command.Parameters.AddWithValue("@ProductId", reorderLevel.ProductId);
                        command.Parameters.AddWithValue("@MinimumLevel", reorderLevel.MinimumLevel);
                        command.Parameters.AddWithValue("@MaximumLevel", reorderLevel.MaximumLevel);
                        command.Parameters.AddWithValue("@ReorderQuantity", reorderLevel.ReorderQuantity);
                        command.Parameters.AddWithValue("@IsActive", reorderLevel.IsActive);
                        command.Parameters.AddWithValue("@AlertEnabled", reorderLevel.AlertEnabled);
                        command.Parameters.AddWithValue("@LastAlertDate", reorderLevel.LastAlertDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", reorderLevel.ModifiedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedByName", reorderLevel.ModifiedByName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedDate", reorderLevel.ModifiedDate ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.UpdateReorderLevel", ex);
                throw;
            }
        }

        public bool DeleteReorderLevel(int reorderLevelId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "DELETE FROM ReorderLevels WHERE ReorderLevelId = @ReorderLevelId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReorderLevelId", reorderLevelId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.DeleteReorderLevel", ex);
                throw;
            }
        }

        public ReorderLevel GetReorderLevelById(int reorderLevelId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rl.*, p.ProductName, p.ProductCode, p.StockQuantity
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               WHERE rl.ReorderLevelId = @ReorderLevelId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReorderLevelId", reorderLevelId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReorderLevel(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetReorderLevelById", ex);
                throw;
            }
        }

        public ReorderLevel GetReorderLevelByProductId(int productId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rl.*, p.ProductName, p.ProductCode, p.StockQuantity
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               WHERE rl.ProductId = @ProductId AND rl.IsActive = 1";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReorderLevel(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetReorderLevelByProductId", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetAllReorderLevels()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rl.*, p.ProductName, p.ProductCode, p.StockQuantity
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               ORDER BY p.ProductName";

                    var reorderLevels = new List<ReorderLevel>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reorderLevels.Add(MapReorderLevel(reader));
                            }
                        }
                    }
                    return reorderLevels;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetAllReorderLevels", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetActiveReorderLevels()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rl.*, p.ProductName, p.ProductCode, p.StockQuantity
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               WHERE rl.IsActive = 1
                               ORDER BY p.ProductName";

                    var reorderLevels = new List<ReorderLevel>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reorderLevels.Add(MapReorderLevel(reader));
                            }
                        }
                    }
                    return reorderLevels;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetActiveReorderLevels", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetReorderLevelsByProduct(int productId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rl.*, p.ProductName, p.ProductCode, p.StockQuantity
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               WHERE rl.ProductId = @ProductId
                               ORDER BY rl.CreatedDate DESC";

                    var reorderLevels = new List<ReorderLevel>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reorderLevels.Add(MapReorderLevel(reader));
                            }
                        }
                    }
                    return reorderLevels;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetReorderLevelsByProduct", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetReorderLevelsBelowMinimum()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rl.*, p.ProductName, p.ProductCode, p.StockQuantity
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               WHERE rl.IsActive = 1 AND rl.AlertEnabled = 1 
                               AND p.StockQuantity <= rl.MinimumLevel
                               ORDER BY (rl.MinimumLevel - p.StockQuantity) DESC";

                    var reorderLevels = new List<ReorderLevel>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reorderLevels.Add(MapReorderLevel(reader));
                            }
                        }
                    }
                    return reorderLevels;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetReorderLevelsBelowMinimum", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetReorderLevelsAboveMaximum()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rl.*, p.ProductName, p.ProductCode, p.StockQuantity
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               WHERE rl.IsActive = 1 AND rl.AlertEnabled = 1 
                               AND p.StockQuantity >= rl.MaximumLevel
                               ORDER BY (p.StockQuantity - rl.MaximumLevel) DESC";

                    var reorderLevels = new List<ReorderLevel>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reorderLevels.Add(MapReorderLevel(reader));
                            }
                        }
                    }
                    return reorderLevels;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetReorderLevelsAboveMaximum", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetReorderLevelsNeedingAlert()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rl.*, p.ProductName, p.ProductCode, p.StockQuantity
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               WHERE rl.IsActive = 1 AND rl.AlertEnabled = 1 
                               AND (p.StockQuantity <= rl.MinimumLevel OR p.StockQuantity >= rl.MaximumLevel)
                               AND (rl.LastAlertDate IS NULL OR DATEDIFF(day, rl.LastAlertDate, GETDATE()) >= 1)
                               ORDER BY p.ProductName";

                    var reorderLevels = new List<ReorderLevel>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reorderLevels.Add(MapReorderLevel(reader));
                            }
                        }
                    }
                    return reorderLevels;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetReorderLevelsNeedingAlert", ex);
                throw;
            }
        }

        public bool UpdateLastAlertDate(int reorderLevelId, DateTime alertDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "UPDATE ReorderLevels SET LastAlertDate = @AlertDate WHERE ReorderLevelId = @ReorderLevelId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReorderLevelId", reorderLevelId);
                        command.Parameters.AddWithValue("@AlertDate", alertDate);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.UpdateLastAlertDate", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetReorderLevelReport(DateTime? startDate, DateTime? endDate, int? productId, bool? isActive)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rl.*, p.ProductName, p.ProductCode, p.StockQuantity
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               WHERE 1=1";

                    var parameters = new List<SqlParameter>();

                    if (startDate.HasValue)
                    {
                        sql += " AND rl.CreatedDate >= @StartDate";
                        parameters.Add(new SqlParameter("@StartDate", startDate.Value));
                    }

                    if (endDate.HasValue)
                    {
                        sql += " AND rl.CreatedDate <= @EndDate";
                        parameters.Add(new SqlParameter("@EndDate", endDate.Value));
                    }

                    if (productId.HasValue)
                    {
                        sql += " AND rl.ProductId = @ProductId";
                        parameters.Add(new SqlParameter("@ProductId", productId.Value));
                    }

                    if (isActive.HasValue)
                    {
                        sql += " AND rl.IsActive = @IsActive";
                        parameters.Add(new SqlParameter("@IsActive", isActive.Value));
                    }

                    sql += " ORDER BY p.ProductName";

                    var reorderLevels = new List<ReorderLevel>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reorderLevels.Add(MapReorderLevel(reader));
                            }
                        }
                    }
                    return reorderLevels;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetReorderLevelReport", ex);
                throw;
            }
        }

        public int GetReorderLevelCount(bool? isActive)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT COUNT(*) FROM ReorderLevels";
                    
                    if (isActive.HasValue)
                    {
                        sql += " WHERE IsActive = @IsActive";
                    }

                    using (var command = new SqlCommand(sql, connection))
                    {
                        if (isActive.HasValue)
                        {
                            command.Parameters.AddWithValue("@IsActive", isActive.Value);
                        }
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetReorderLevelCount", ex);
                throw;
            }
        }

        public decimal GetTotalReorderValue()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT ISNULL(SUM(rl.ReorderQuantity * p.PurchasePrice), 0)
                               FROM ReorderLevels rl
                               LEFT JOIN Products p ON rl.ProductId = p.ProductId
                               WHERE rl.IsActive = 1";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        return Convert.ToDecimal(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelRepository.GetTotalReorderValue", ex);
                throw;
            }
        }

        private ReorderLevel MapReorderLevel(SqlDataReader reader)
        {
            return new ReorderLevel
            {
                ReorderLevelId = Convert.ToInt32(reader["ReorderLevelId"]),
                ProductId = Convert.ToInt32(reader["ProductId"]),
                MinimumLevel = Convert.ToDecimal(reader["MinimumLevel"]),
                MaximumLevel = Convert.ToDecimal(reader["MaximumLevel"]),
                ReorderQuantity = Convert.ToDecimal(reader["ReorderQuantity"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                AlertEnabled = Convert.ToBoolean(reader["AlertEnabled"]),
                LastAlertDate = reader["LastAlertDate"] == DBNull.Value ? null : (DateTime?)reader["LastAlertDate"],
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                CreatedByName = reader["CreatedByName"] == DBNull.Value ? null : reader["CreatedByName"].ToString(),
                ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? null : (DateTime?)reader["ModifiedDate"],
                ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? null : (int?)reader["ModifiedBy"],
                ModifiedByName = reader["ModifiedByName"] == DBNull.Value ? null : reader["ModifiedByName"].ToString(),
                Product = new Product
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = reader["ProductName"] == DBNull.Value ? null : reader["ProductName"].ToString(),
                    ProductCode = reader["ProductCode"] == DBNull.Value ? null : reader["ProductCode"].ToString(),
                    StockQuantity = reader["StockQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["StockQuantity"])
                }
            };
        }
    }
}
