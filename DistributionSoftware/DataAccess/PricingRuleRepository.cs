using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class PricingRuleRepository : IPricingRuleRepository
    {
        private readonly string _connectionString;

        public PricingRuleRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? ConfigurationManager.DistributionConnectionString;
        }

        #region CRUD Operations

        public int CreatePricingRule(PricingRule pricingRule)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"INSERT INTO PricingRules (RuleName, Description, ProductId, CategoryId, CustomerId, CustomerCategoryId, 
                               PricingType, BaseValue, MinQuantity, MaxQuantity, Priority, IsActive, EffectiveFrom, EffectiveTo, 
                               CreatedBy, CreatedDate)
                               VALUES (@RuleName, @Description, @ProductId, @CategoryId, @CustomerId, @CustomerCategoryId, 
                               @PricingType, @BaseValue, @MinQuantity, @MaxQuantity, @Priority, @IsActive, @EffectiveFrom, @EffectiveTo, 
                               @CreatedBy, @CreatedDate);
                               SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@RuleName", pricingRule.RuleName);
                        command.Parameters.AddWithValue("@Description", pricingRule.Description ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductId", pricingRule.ProductId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", pricingRule.CategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerId", pricingRule.CustomerId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerCategoryId", pricingRule.CustomerCategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PricingType", pricingRule.PricingType);
                        command.Parameters.AddWithValue("@BaseValue", pricingRule.BaseValue);
                        command.Parameters.AddWithValue("@MinQuantity", pricingRule.MinQuantity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MaxQuantity", pricingRule.MaxQuantity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Priority", pricingRule.Priority);
                        command.Parameters.AddWithValue("@IsActive", pricingRule.IsActive);
                        command.Parameters.AddWithValue("@EffectiveFrom", pricingRule.EffectiveFrom ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EffectiveTo", pricingRule.EffectiveTo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", pricingRule.CreatedBy);
                        command.Parameters.AddWithValue("@CreatedDate", pricingRule.CreatedDate);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.CreatePricingRule", ex);
                throw;
            }
        }

        public bool UpdatePricingRule(PricingRule pricingRule)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"UPDATE PricingRules SET 
                               RuleName = @RuleName, Description = @Description, ProductId = @ProductId, CategoryId = @CategoryId, 
                               CustomerId = @CustomerId, CustomerCategoryId = @CustomerCategoryId, PricingType = @PricingType, 
                               BaseValue = @BaseValue, MinQuantity = @MinQuantity, MaxQuantity = @MaxQuantity, Priority = @Priority, 
                               IsActive = @IsActive, EffectiveFrom = @EffectiveFrom, EffectiveTo = @EffectiveTo, 
                               ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate
                               WHERE PricingRuleId = @PricingRuleId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PricingRuleId", pricingRule.PricingRuleId);
                        command.Parameters.AddWithValue("@RuleName", pricingRule.RuleName);
                        command.Parameters.AddWithValue("@Description", pricingRule.Description ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductId", pricingRule.ProductId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", pricingRule.CategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerId", pricingRule.CustomerId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerCategoryId", pricingRule.CustomerCategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PricingType", pricingRule.PricingType);
                        command.Parameters.AddWithValue("@BaseValue", pricingRule.BaseValue);
                        command.Parameters.AddWithValue("@MinQuantity", pricingRule.MinQuantity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MaxQuantity", pricingRule.MaxQuantity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Priority", pricingRule.Priority);
                        command.Parameters.AddWithValue("@IsActive", pricingRule.IsActive);
                        command.Parameters.AddWithValue("@EffectiveFrom", pricingRule.EffectiveFrom ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EffectiveTo", pricingRule.EffectiveTo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", pricingRule.ModifiedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedDate", pricingRule.ModifiedDate ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.UpdatePricingRule", ex);
                throw;
            }
        }

        public bool DeletePricingRule(int pricingRuleId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "DELETE FROM PricingRules WHERE PricingRuleId = @PricingRuleId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PricingRuleId", pricingRuleId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.DeletePricingRule", ex);
                throw;
            }
        }

        public PricingRule GetPricingRuleById(int pricingRuleId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pr.*, p.ProductName, c.CustomerName 
                               FROM PricingRules pr
                               LEFT JOIN Products p ON pr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON pr.CustomerId = c.CustomerId
                               WHERE pr.PricingRuleId = @PricingRuleId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PricingRuleId", pricingRuleId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapPricingRule(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.GetPricingRuleById", ex);
                throw;
            }

            return null;
        }

        public List<PricingRule> GetAllPricingRules()
        {
            var pricingRules = new List<PricingRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pr.*, p.ProductName, c.CustomerName 
                               FROM PricingRules pr
                               LEFT JOIN Products p ON pr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON pr.CustomerId = c.CustomerId
                               ORDER BY pr.Priority, pr.RuleName";

                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pricingRules.Add(MapPricingRule(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.GetAllPricingRules", ex);
                throw;
            }

            return pricingRules;
        }

        public List<PricingRule> GetActivePricingRules()
        {
            var pricingRules = new List<PricingRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pr.*, p.ProductName, c.CustomerName 
                               FROM PricingRules pr
                               LEFT JOIN Products p ON pr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON pr.CustomerId = c.CustomerId
                               WHERE pr.IsActive = 1
                               ORDER BY pr.Priority, pr.RuleName";

                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pricingRules.Add(MapPricingRule(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.GetActivePricingRules", ex);
                throw;
            }

            return pricingRules;
        }

        public List<PricingRule> GetActive()
        {
            return GetActivePricingRules();
        }

        #endregion

        #region Business Logic

        public List<PricingRule> GetPricingRulesForProduct(int productId)
        {
            var pricingRules = new List<PricingRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pr.*, p.ProductName, c.CustomerName 
                               FROM PricingRules pr
                               LEFT JOIN Products p ON pr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON pr.CustomerId = c.CustomerId
                               WHERE pr.ProductId = @ProductId AND pr.IsActive = 1
                               ORDER BY pr.Priority";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pricingRules.Add(MapPricingRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.GetPricingRulesForProduct", ex);
                throw;
            }

            return pricingRules;
        }

        public List<PricingRule> GetPricingRulesForCategory(int categoryId)
        {
            var pricingRules = new List<PricingRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pr.*, p.ProductName, c.CustomerName 
                               FROM PricingRules pr
                               LEFT JOIN Products p ON pr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON pr.CustomerId = c.CustomerId
                               WHERE pr.CategoryId = @CategoryId AND pr.IsActive = 1
                               ORDER BY pr.Priority";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", categoryId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pricingRules.Add(MapPricingRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.GetPricingRulesForCategory", ex);
                throw;
            }

            return pricingRules;
        }

        public List<PricingRule> GetPricingRulesForCustomer(int customerId)
        {
            var pricingRules = new List<PricingRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pr.*, p.ProductName, c.CustomerName 
                               FROM PricingRules pr
                               LEFT JOIN Products p ON pr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON pr.CustomerId = c.CustomerId
                               WHERE pr.CustomerId = @CustomerId AND pr.IsActive = 1
                               ORDER BY pr.Priority";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pricingRules.Add(MapPricingRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.GetPricingRulesForCustomer", ex);
                throw;
            }

            return pricingRules;
        }

        public List<PricingRule> GetPricingRulesForCustomerCategory(int customerCategoryId)
        {
            var pricingRules = new List<PricingRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pr.*, p.ProductName, c.CustomerName 
                               FROM PricingRules pr
                               LEFT JOIN Products p ON pr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON pr.CustomerId = c.CustomerId
                               WHERE pr.CustomerCategoryId = @CustomerCategoryId AND pr.IsActive = 1
                               ORDER BY pr.Priority";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerCategoryId", customerCategoryId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pricingRules.Add(MapPricingRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.GetPricingRulesForCustomerCategory", ex);
                throw;
            }

            return pricingRules;
        }

        public PricingRule GetBestPricingRule(int? productId, int? categoryId, int? customerId, int? customerCategoryId, decimal quantity = 1)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT TOP 1 pr.*, p.ProductName, c.CustomerName 
                               FROM PricingRules pr
                               LEFT JOIN Products p ON pr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON pr.CustomerId = c.CustomerId
                               WHERE pr.IsActive = 1
                               AND (pr.EffectiveFrom IS NULL OR pr.EffectiveFrom <= GETDATE())
                               AND (pr.EffectiveTo IS NULL OR pr.EffectiveTo >= GETDATE())
                               AND (
                                   (pr.ProductId = @ProductId AND @ProductId IS NOT NULL) OR
                                   (pr.CategoryId = @CategoryId AND @CategoryId IS NOT NULL) OR
                                   (pr.CustomerId = @CustomerId AND @CustomerId IS NOT NULL) OR
                                   (pr.CustomerCategoryId = @CustomerCategoryId AND @CustomerCategoryId IS NOT NULL) OR
                                   (pr.ProductId IS NULL AND pr.CategoryId IS NULL AND pr.CustomerId IS NULL AND pr.CustomerCategoryId IS NULL)
                               )
                               AND (
                                   pr.MinQuantity IS NULL OR pr.MinQuantity <= @Quantity
                               )
                               AND (
                                   pr.MaxQuantity IS NULL OR pr.MaxQuantity >= @Quantity
                               )
                               ORDER BY pr.Priority ASC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", categoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerId", customerId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerCategoryId", customerCategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Quantity", quantity);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapPricingRule(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.GetBestPricingRule", ex);
                throw;
            }

            return null;
        }

        #endregion

        #region Reports

        public List<PricingRule> GetPricingRuleReport(DateTime? startDate, DateTime? endDate, int? productId, int? categoryId, bool? isActive)
        {
            var pricingRules = new List<PricingRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pr.*, p.ProductName, c.CustomerName 
                               FROM PricingRules pr
                               LEFT JOIN Products p ON pr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON pr.CustomerId = c.CustomerId
                               WHERE 1=1";

                    var parameters = new List<SqlParameter>();

                    if (startDate.HasValue)
                    {
                        sql += " AND pr.CreatedDate >= @StartDate";
                        parameters.Add(new SqlParameter("@StartDate", startDate.Value));
                    }

                    if (endDate.HasValue)
                    {
                        sql += " AND pr.CreatedDate <= @EndDate";
                        parameters.Add(new SqlParameter("@EndDate", endDate.Value));
                    }

                    if (productId.HasValue)
                    {
                        sql += " AND pr.ProductId = @ProductId";
                        parameters.Add(new SqlParameter("@ProductId", productId.Value));
                    }

                    if (categoryId.HasValue)
                    {
                        sql += " AND pr.CategoryId = @CategoryId";
                        parameters.Add(new SqlParameter("@CategoryId", categoryId.Value));
                    }

                    if (isActive.HasValue)
                    {
                        sql += " AND pr.IsActive = @IsActive";
                        parameters.Add(new SqlParameter("@IsActive", isActive.Value));
                    }

                    sql += " ORDER BY pr.Priority, pr.RuleName";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(param);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pricingRules.Add(MapPricingRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PricingRuleRepository.GetPricingRuleReport", ex);
                throw;
            }

            return pricingRules;
        }

        public int GetPricingRuleCount(bool? isActive)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT COUNT(*) FROM PricingRules";
                    
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
                DebugHelper.WriteException("PricingRuleRepository.GetPricingRuleCount", ex);
                throw;
            }
        }

        #endregion

        #region Helper Methods

        private PricingRule MapPricingRule(SqlDataReader reader)
        {
            return new PricingRule
            {
                PricingRuleId = Convert.ToInt32(reader["PricingRuleId"]),
                RuleName = reader["RuleName"].ToString(),
                Description = reader["Description"]?.ToString(),
                ProductId = reader["ProductId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ProductId"]),
                CategoryId = reader["CategoryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CategoryId"]),
                CustomerId = reader["CustomerId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CustomerId"]),
                CustomerCategoryId = reader["CustomerCategoryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CustomerCategoryId"]),
                PricingType = reader["PricingType"].ToString(),
                BaseValue = Convert.ToDecimal(reader["BaseValue"]),
                MinQuantity = reader["MinQuantity"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["MinQuantity"]),
                MaxQuantity = reader["MaxQuantity"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["MaxQuantity"]),
                Priority = Convert.ToInt32(reader["Priority"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                EffectiveFrom = reader["EffectiveFrom"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["EffectiveFrom"]),
                EffectiveTo = reader["EffectiveTo"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["EffectiveTo"]),
                CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                Product = reader["ProductName"] != DBNull.Value ? new Product { ProductName = reader["ProductName"].ToString() } : null,
                Customer = reader["CustomerName"] != DBNull.Value ? new Customer { CustomerName = reader["CustomerName"].ToString() } : null
            };
        }


        #endregion
    }
}
