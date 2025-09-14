using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class DiscountRuleRepository : IDiscountRuleRepository
    {
        private readonly string _connectionString;

        public DiscountRuleRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? ConfigurationManager.DistributionConnectionString;
        }

        #region CRUD Operations

        public int CreateDiscountRule(DiscountRule discountRule)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"INSERT INTO DiscountRules (RuleName, Description, ProductId, CategoryId, CustomerId, CustomerCategoryId, 
                               DiscountType, DiscountValue, MinQuantity, MaxQuantity, MinOrderAmount, MaxDiscountAmount, Priority, 
                               IsActive, IsPromotional, EffectiveFrom, EffectiveTo, UsageLimitPerCustomer, TotalUsageLimit, 
                               CurrentUsageCount, CreatedBy, CreatedDate)
                               VALUES (@RuleName, @Description, @ProductId, @CategoryId, @CustomerId, @CustomerCategoryId, 
                               @DiscountType, @DiscountValue, @MinQuantity, @MaxQuantity, @MinOrderAmount, @MaxDiscountAmount, @Priority, 
                               @IsActive, @IsPromotional, @EffectiveFrom, @EffectiveTo, @UsageLimitPerCustomer, @TotalUsageLimit, 
                               @CurrentUsageCount, @CreatedBy, @CreatedDate);
                               SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@RuleName", discountRule.RuleName);
                        command.Parameters.AddWithValue("@Description", discountRule.Description ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductId", discountRule.ProductId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", discountRule.CategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerId", discountRule.CustomerId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerCategoryId", discountRule.CustomerCategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DiscountType", discountRule.DiscountType);
                        command.Parameters.AddWithValue("@DiscountValue", discountRule.DiscountValue);
                        command.Parameters.AddWithValue("@MinQuantity", discountRule.MinQuantity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MaxQuantity", discountRule.MaxQuantity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MinOrderAmount", discountRule.MinOrderAmount ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MaxDiscountAmount", discountRule.MaxDiscountAmount ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Priority", discountRule.Priority);
                        command.Parameters.AddWithValue("@IsActive", discountRule.IsActive);
                        command.Parameters.AddWithValue("@IsPromotional", discountRule.IsPromotional);
                        command.Parameters.AddWithValue("@EffectiveFrom", discountRule.EffectiveFrom ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EffectiveTo", discountRule.EffectiveTo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UsageLimitPerCustomer", discountRule.UsageLimitPerCustomer ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TotalUsageLimit", discountRule.TotalUsageLimit ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CurrentUsageCount", discountRule.CurrentUsageCount);
                        command.Parameters.AddWithValue("@CreatedBy", discountRule.CreatedBy);
                        command.Parameters.AddWithValue("@CreatedDate", discountRule.CreatedDate);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.CreateDiscountRule", ex);
                throw;
            }
        }

        public bool UpdateDiscountRule(DiscountRule discountRule)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"UPDATE DiscountRules SET 
                               RuleName = @RuleName, Description = @Description, ProductId = @ProductId, CategoryId = @CategoryId, 
                               CustomerId = @CustomerId, CustomerCategoryId = @CustomerCategoryId, DiscountType = @DiscountType, 
                               DiscountValue = @DiscountValue, MinQuantity = @MinQuantity, MaxQuantity = @MaxQuantity, 
                               MinOrderAmount = @MinOrderAmount, MaxDiscountAmount = @MaxDiscountAmount, Priority = @Priority, 
                               IsActive = @IsActive, IsPromotional = @IsPromotional, EffectiveFrom = @EffectiveFrom, 
                               EffectiveTo = @EffectiveTo, UsageLimitPerCustomer = @UsageLimitPerCustomer, 
                               TotalUsageLimit = @TotalUsageLimit, CurrentUsageCount = @CurrentUsageCount,
                               ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate
                               WHERE DiscountRuleId = @DiscountRuleId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DiscountRuleId", discountRule.DiscountRuleId);
                        command.Parameters.AddWithValue("@RuleName", discountRule.RuleName);
                        command.Parameters.AddWithValue("@Description", discountRule.Description ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductId", discountRule.ProductId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", discountRule.CategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerId", discountRule.CustomerId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerCategoryId", discountRule.CustomerCategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DiscountType", discountRule.DiscountType);
                        command.Parameters.AddWithValue("@DiscountValue", discountRule.DiscountValue);
                        command.Parameters.AddWithValue("@MinQuantity", discountRule.MinQuantity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MaxQuantity", discountRule.MaxQuantity ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MinOrderAmount", discountRule.MinOrderAmount ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MaxDiscountAmount", discountRule.MaxDiscountAmount ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Priority", discountRule.Priority);
                        command.Parameters.AddWithValue("@IsActive", discountRule.IsActive);
                        command.Parameters.AddWithValue("@IsPromotional", discountRule.IsPromotional);
                        command.Parameters.AddWithValue("@EffectiveFrom", discountRule.EffectiveFrom ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EffectiveTo", discountRule.EffectiveTo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UsageLimitPerCustomer", discountRule.UsageLimitPerCustomer ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TotalUsageLimit", discountRule.TotalUsageLimit ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CurrentUsageCount", discountRule.CurrentUsageCount);
                        command.Parameters.AddWithValue("@ModifiedBy", discountRule.ModifiedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedDate", discountRule.ModifiedDate ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.UpdateDiscountRule", ex);
                throw;
            }
        }

        public bool DeleteDiscountRule(int discountRuleId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "DELETE FROM DiscountRules WHERE DiscountRuleId = @DiscountRuleId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DiscountRuleId", discountRuleId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.DeleteDiscountRule", ex);
                throw;
            }
        }

        public DiscountRule GetDiscountRuleById(int discountRuleId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT dr.*, p.ProductName, c.CustomerName 
                               FROM DiscountRules dr
                               LEFT JOIN Products p ON dr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON dr.CustomerId = c.CustomerId
                               WHERE dr.DiscountRuleId = @DiscountRuleId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DiscountRuleId", discountRuleId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapDiscountRule(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.GetDiscountRuleById", ex);
                throw;
            }

            return null;
        }

        public List<DiscountRule> GetAllDiscountRules()
        {
            var discountRules = new List<DiscountRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT dr.*, p.ProductName, c.CustomerName 
                               FROM DiscountRules dr
                               LEFT JOIN Products p ON dr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON dr.CustomerId = c.CustomerId
                               ORDER BY dr.Priority, dr.RuleName";

                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            discountRules.Add(MapDiscountRule(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.GetAllDiscountRules", ex);
                throw;
            }

            return discountRules;
        }

        public List<DiscountRule> GetActiveDiscountRules()
        {
            var discountRules = new List<DiscountRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT dr.*, p.ProductName, c.CustomerName 
                               FROM DiscountRules dr
                               LEFT JOIN Products p ON dr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON dr.CustomerId = c.CustomerId
                               WHERE dr.IsActive = 1
                               ORDER BY dr.Priority, dr.RuleName";

                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            discountRules.Add(MapDiscountRule(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.GetActiveDiscountRules", ex);
                throw;
            }

            return discountRules;
        }

        public List<DiscountRule> GetActive()
        {
            return GetActiveDiscountRules();
        }

        #endregion

        #region Business Logic

        public List<DiscountRule> GetDiscountRulesForProduct(int productId)
        {
            var discountRules = new List<DiscountRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT dr.*, p.ProductName, c.CustomerName 
                               FROM DiscountRules dr
                               LEFT JOIN Products p ON dr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON dr.CustomerId = c.CustomerId
                               WHERE dr.ProductId = @ProductId AND dr.IsActive = 1
                               ORDER BY dr.Priority";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                discountRules.Add(MapDiscountRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.GetDiscountRulesForProduct", ex);
                throw;
            }

            return discountRules;
        }

        public List<DiscountRule> GetDiscountRulesForCategory(int categoryId)
        {
            var discountRules = new List<DiscountRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT dr.*, p.ProductName, c.CustomerName 
                               FROM DiscountRules dr
                               LEFT JOIN Products p ON dr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON dr.CustomerId = c.CustomerId
                               WHERE dr.CategoryId = @CategoryId AND dr.IsActive = 1
                               ORDER BY dr.Priority";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", categoryId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                discountRules.Add(MapDiscountRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.GetDiscountRulesForCategory", ex);
                throw;
            }

            return discountRules;
        }

        public List<DiscountRule> GetDiscountRulesForCustomer(int customerId)
        {
            var discountRules = new List<DiscountRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT dr.*, p.ProductName, c.CustomerName 
                               FROM DiscountRules dr
                               LEFT JOIN Products p ON dr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON dr.CustomerId = c.CustomerId
                               WHERE dr.CustomerId = @CustomerId AND dr.IsActive = 1
                               ORDER BY dr.Priority";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                discountRules.Add(MapDiscountRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.GetDiscountRulesForCustomer", ex);
                throw;
            }

            return discountRules;
        }

        public List<DiscountRule> GetDiscountRulesForCustomerCategory(int customerCategoryId)
        {
            var discountRules = new List<DiscountRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT dr.*, p.ProductName, c.CustomerName 
                               FROM DiscountRules dr
                               LEFT JOIN Products p ON dr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON dr.CustomerId = c.CustomerId
                               WHERE dr.CustomerCategoryId = @CustomerCategoryId AND dr.IsActive = 1
                               ORDER BY dr.Priority";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerCategoryId", customerCategoryId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                discountRules.Add(MapDiscountRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.GetDiscountRulesForCustomerCategory", ex);
                throw;
            }

            return discountRules;
        }

        public DiscountRule GetBestDiscountRule(int? productId, int? categoryId, int? customerId, int? customerCategoryId, decimal quantity = 1, decimal orderAmount = 0)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT TOP 1 dr.*, p.ProductName, c.CustomerName 
                               FROM DiscountRules dr
                               LEFT JOIN Products p ON dr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON dr.CustomerId = c.CustomerId
                               WHERE dr.IsActive = 1
                               AND (dr.EffectiveFrom IS NULL OR dr.EffectiveFrom <= GETDATE())
                               AND (dr.EffectiveTo IS NULL OR dr.EffectiveTo >= GETDATE())
                               AND (
                                   (dr.ProductId = @ProductId AND @ProductId IS NOT NULL) OR
                                   (dr.CategoryId = @CategoryId AND @CategoryId IS NOT NULL) OR
                                   (dr.CustomerId = @CustomerId AND @CustomerId IS NOT NULL) OR
                                   (dr.CustomerCategoryId = @CustomerCategoryId AND @CustomerCategoryId IS NOT NULL) OR
                                   (dr.ProductId IS NULL AND dr.CategoryId IS NULL AND dr.CustomerId IS NULL AND dr.CustomerCategoryId IS NULL)
                               )
                               AND (
                                   dr.MinQuantity IS NULL OR dr.MinQuantity <= @Quantity
                               )
                               AND (
                                   dr.MaxQuantity IS NULL OR dr.MaxQuantity >= @Quantity
                               )
                               AND (
                                   dr.MinOrderAmount IS NULL OR dr.MinOrderAmount <= @OrderAmount
                               )
                               ORDER BY dr.Priority ASC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", categoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerId", customerId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerCategoryId", customerCategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@OrderAmount", orderAmount);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapDiscountRule(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.GetBestDiscountRule", ex);
                throw;
            }

            return null;
        }

        #endregion

        #region Reports

        public List<DiscountRule> GetDiscountRuleReport(DateTime? startDate, DateTime? endDate, int? productId, int? categoryId, bool? isActive)
        {
            var discountRules = new List<DiscountRule>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT dr.*, p.ProductName, c.CustomerName 
                               FROM DiscountRules dr
                               LEFT JOIN Products p ON dr.ProductId = p.ProductId
                               LEFT JOIN Customers c ON dr.CustomerId = c.CustomerId
                               WHERE 1=1";

                    var parameters = new List<SqlParameter>();

                    if (startDate.HasValue)
                    {
                        sql += " AND dr.CreatedDate >= @StartDate";
                        parameters.Add(new SqlParameter("@StartDate", startDate.Value));
                    }

                    if (endDate.HasValue)
                    {
                        sql += " AND dr.CreatedDate <= @EndDate";
                        parameters.Add(new SqlParameter("@EndDate", endDate.Value));
                    }

                    if (productId.HasValue)
                    {
                        sql += " AND dr.ProductId = @ProductId";
                        parameters.Add(new SqlParameter("@ProductId", productId.Value));
                    }

                    if (categoryId.HasValue)
                    {
                        sql += " AND dr.CategoryId = @CategoryId";
                        parameters.Add(new SqlParameter("@CategoryId", categoryId.Value));
                    }

                    if (isActive.HasValue)
                    {
                        sql += " AND dr.IsActive = @IsActive";
                        parameters.Add(new SqlParameter("@IsActive", isActive.Value));
                    }

                    sql += " ORDER BY dr.Priority, dr.RuleName";

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
                                discountRules.Add(MapDiscountRule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("DiscountRuleRepository.GetDiscountRuleReport", ex);
                throw;
            }

            return discountRules;
        }

        public int GetDiscountRuleCount(bool? isActive)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT COUNT(*) FROM DiscountRules";
                    
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
                DebugHelper.WriteException("DiscountRuleRepository.GetDiscountRuleCount", ex);
                throw;
            }
        }

        #endregion

        #region Helper Methods

        private DiscountRule MapDiscountRule(SqlDataReader reader)
        {
            return new DiscountRule
            {
                DiscountRuleId = Convert.ToInt32(reader["DiscountRuleId"]),
                RuleName = reader["RuleName"].ToString(),
                Description = reader["Description"]?.ToString(),
                ProductId = reader["ProductId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ProductId"]),
                CategoryId = reader["CategoryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CategoryId"]),
                CustomerId = reader["CustomerId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CustomerId"]),
                CustomerCategoryId = reader["CustomerCategoryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CustomerCategoryId"]),
                DiscountType = reader["DiscountType"].ToString(),
                DiscountValue = Convert.ToDecimal(reader["DiscountValue"]),
                MinQuantity = reader["MinQuantity"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["MinQuantity"]),
                MaxQuantity = reader["MaxQuantity"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["MaxQuantity"]),
                MinOrderAmount = reader["MinOrderAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["MinOrderAmount"]),
                MaxDiscountAmount = reader["MaxDiscountAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["MaxDiscountAmount"]),
                Priority = Convert.ToInt32(reader["Priority"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                IsPromotional = Convert.ToBoolean(reader["IsPromotional"]),
                EffectiveFrom = reader["EffectiveFrom"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["EffectiveFrom"]),
                EffectiveTo = reader["EffectiveTo"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["EffectiveTo"]),
                UsageLimitPerCustomer = reader["UsageLimitPerCustomer"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["UsageLimitPerCustomer"]),
                TotalUsageLimit = reader["TotalUsageLimit"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TotalUsageLimit"]),
                CurrentUsageCount = Convert.ToInt32(reader["CurrentUsageCount"]),
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
