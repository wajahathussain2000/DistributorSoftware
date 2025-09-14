using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class TaxCategoryRepository : ITaxCategoryRepository
    {
        private readonly string _connectionString;

        public TaxCategoryRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? Common.ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public int Create(TaxCategory taxCategory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO TaxCategories (TaxCategoryName, Description, IsActive, CreatedDate, CreatedBy) VALUES (@TaxCategoryName, @Description, @IsActive, @CreatedDate, @CreatedBy); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@TaxCategoryName", taxCategory.TaxCategoryName);
                    command.Parameters.AddWithValue("@Description", taxCategory.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", taxCategory.IsActive);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@CreatedBy", taxCategory.CreatedBy);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(TaxCategory taxCategory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE TaxCategories SET TaxCategoryName = @TaxCategoryName, Description = @Description, IsActive = @IsActive, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy WHERE TaxCategoryId = @TaxCategoryId", connection))
                {
                    command.Parameters.AddWithValue("@TaxCategoryId", taxCategory.TaxCategoryId);
                    command.Parameters.AddWithValue("@TaxCategoryName", taxCategory.TaxCategoryName);
                    command.Parameters.AddWithValue("@Description", taxCategory.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", taxCategory.IsActive);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ModifiedBy", taxCategory.ModifiedBy);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int taxCategoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM TaxCategories WHERE TaxCategoryId = @TaxCategoryId", connection))
                {
                    command.Parameters.AddWithValue("@TaxCategoryId", taxCategoryId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public TaxCategory GetById(int taxCategoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM TaxCategories WHERE TaxCategoryId = @TaxCategoryId", connection))
                {
                    command.Parameters.AddWithValue("@TaxCategoryId", taxCategoryId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapTaxCategory(reader);
                        return null;
                    }
                }
            }
        }

        public TaxCategory GetByCode(string taxCategoryCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM TaxCategories WHERE TaxCategoryName = @TaxCategoryName", connection))
                {
                    command.Parameters.AddWithValue("@TaxCategoryName", taxCategoryCode);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapTaxCategory(reader);
                        return null;
                    }
                }
            }
        }

        public List<TaxCategory> GetAll()
        {
            var taxCategories = new List<TaxCategory>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM TaxCategories ORDER BY TaxCategoryName", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        taxCategories.Add(MapTaxCategory(reader));
                }
            }
            return taxCategories;
        }

        public List<TaxCategory> GetActive()
        {
            var taxCategories = new List<TaxCategory>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM TaxCategories WHERE IsActive = 1 ORDER BY TaxCategoryName", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        taxCategories.Add(MapTaxCategory(reader));
                }
            }
            return taxCategories;
        }

        public List<TaxCategory> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            var taxCategories = new List<TaxCategory>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM TaxCategories WHERE 1=1";
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

                query += " ORDER BY TaxCategoryName";
                command.CommandText = query;
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        taxCategories.Add(MapTaxCategory(reader));
                }
            }
            return taxCategories;
        }

        public int GetCount(bool? isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM TaxCategories";
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

        private TaxCategory MapTaxCategory(IDataReader reader)
        {
            return new TaxCategory
            {
                TaxCategoryId = Convert.ToInt32(reader["TaxCategoryId"]),
                TaxCategoryName = reader["TaxCategoryName"].ToString(),
                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"])
            };
        }
    }
}