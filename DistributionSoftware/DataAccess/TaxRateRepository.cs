using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class TaxRateRepository : ITaxRateRepository
    {
        private readonly string _connectionString;

        public TaxRateRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? Common.ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public int Create(TaxRate taxRate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO TaxRates (TaxCategoryId, TaxRateCode, TaxRateName, TaxPercentage, Description, IsActive, IsSystemRate, EffectiveFrom, EffectiveTo, IsCompound, IsInclusive, TaxAccountId, TaxPayableAccountId, CreatedDate, CreatedBy) VALUES (@TaxCategoryId, @TaxRateCode, @TaxRateName, @TaxPercentage, @Description, @IsActive, @IsSystemRate, @EffectiveFrom, @EffectiveTo, @IsCompound, @IsInclusive, @TaxAccountId, @TaxPayableAccountId, @CreatedDate, @CreatedBy); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@TaxCategoryId", taxRate.TaxCategoryId);
                    command.Parameters.AddWithValue("@TaxRateCode", taxRate.TaxRateCode ?? string.Empty);
                    command.Parameters.AddWithValue("@TaxRateName", taxRate.TaxRateName ?? string.Empty);
                    command.Parameters.AddWithValue("@TaxPercentage", taxRate.TaxPercentage);
                    command.Parameters.AddWithValue("@Description", taxRate.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", taxRate.IsActive);
                    command.Parameters.AddWithValue("@IsSystemRate", taxRate.IsSystemRate);
                    command.Parameters.AddWithValue("@EffectiveFrom", taxRate.EffectiveFrom);
                    command.Parameters.AddWithValue("@EffectiveTo", taxRate.EffectiveTo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsCompound", taxRate.IsCompound);
                    command.Parameters.AddWithValue("@IsInclusive", taxRate.IsInclusive);
                    command.Parameters.AddWithValue("@TaxAccountId", taxRate.TaxAccountId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TaxPayableAccountId", taxRate.TaxPayableAccountId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedDate", taxRate.CreatedDate);
                    command.Parameters.AddWithValue("@CreatedBy", taxRate.CreatedBy);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(TaxRate taxRate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE TaxRates SET TaxCategoryId = @TaxCategoryId, TaxRateCode = @TaxRateCode, TaxRateName = @TaxRateName, TaxPercentage = @TaxPercentage, Description = @Description, IsActive = @IsActive, IsSystemRate = @IsSystemRate, EffectiveFrom = @EffectiveFrom, EffectiveTo = @EffectiveTo, IsCompound = @IsCompound, IsInclusive = @IsInclusive, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy WHERE TaxRateId = @TaxRateId", connection))
                {
                    command.Parameters.AddWithValue("@TaxRateId", taxRate.TaxRateId);
                    command.Parameters.AddWithValue("@TaxCategoryId", taxRate.TaxCategoryId);
                    command.Parameters.AddWithValue("@TaxRateCode", taxRate.TaxRateCode ?? string.Empty);
                    command.Parameters.AddWithValue("@TaxRateName", taxRate.TaxRateName ?? string.Empty);
                    command.Parameters.AddWithValue("@TaxPercentage", taxRate.TaxPercentage);
                    command.Parameters.AddWithValue("@Description", taxRate.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", taxRate.IsActive);
                    command.Parameters.AddWithValue("@IsSystemRate", taxRate.IsSystemRate);
                    command.Parameters.AddWithValue("@EffectiveFrom", taxRate.EffectiveFrom);
                    command.Parameters.AddWithValue("@EffectiveTo", taxRate.EffectiveTo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsCompound", taxRate.IsCompound);
                    command.Parameters.AddWithValue("@IsInclusive", taxRate.IsInclusive);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ModifiedBy", taxRate.ModifiedBy);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int taxRateId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM TaxRates WHERE TaxRateId = @TaxRateId", connection))
                {
                    command.Parameters.AddWithValue("@TaxRateId", taxRateId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public TaxRate GetById(int taxRateId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM TaxRates WHERE TaxRateId = @TaxRateId", connection))
                {
                    command.Parameters.AddWithValue("@TaxRateId", taxRateId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapTaxRate(reader);
                        return null;
                    }
                }
            }
        }

        public TaxRate GetByCode(string taxRateCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM TaxRates WHERE TaxRateCode = @TaxRateCode", connection))
                {
                    command.Parameters.AddWithValue("@TaxRateCode", taxRateCode);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapTaxRate(reader);
                        return null;
                    }
                }
            }
        }

        public List<TaxRate> GetAll()
        {
            var taxRates = new List<TaxRate>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM TaxRates ORDER BY TaxCategoryId, EffectiveFrom DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        taxRates.Add(MapTaxRate(reader));
                }
            }
            return taxRates;
        }

        public List<TaxRate> GetActive()
        {
            var taxRates = new List<TaxRate>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM TaxRates WHERE IsActive = 1 ORDER BY TaxCategoryId, EffectiveFrom DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        taxRates.Add(MapTaxRate(reader));
                }
            }
            return taxRates;
        }

        public List<TaxRate> GetByTaxCategoryId(int taxCategoryId)
        {
            var taxRates = new List<TaxRate>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM TaxRates WHERE TaxCategoryId = @TaxCategoryId ORDER BY EffectiveFrom DESC", connection))
                {
                    command.Parameters.AddWithValue("@TaxCategoryId", taxCategoryId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            taxRates.Add(MapTaxRate(reader));
                    }
                }
            }
            return taxRates;
        }

        public TaxRate GetEffectiveRate(int taxCategoryId, DateTime effectiveDate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT TOP 1 * FROM TaxRates WHERE TaxCategoryId = @TaxCategoryId AND IsActive = 1 AND EffectiveFrom <= @EffectiveFrom AND (EffectiveTo IS NULL OR EffectiveTo >= @EffectiveFrom) ORDER BY EffectiveFrom DESC", connection))
                {
                    command.Parameters.AddWithValue("@TaxCategoryId", taxCategoryId);
                    command.Parameters.AddWithValue("@EffectiveFrom", effectiveDate);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapTaxRate(reader);
                        return null;
                    }
                }
            }
        }

        public TaxRate GetCurrentRate(int taxCategoryId)
        {
            return GetEffectiveRate(taxCategoryId, DateTime.Now);
        }

        public List<TaxRate> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            var taxRates = new List<TaxRate>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM TaxRates WHERE 1=1";
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

                query += " ORDER BY TaxCategoryId, EffectiveFrom DESC";
                command.CommandText = query;
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        taxRates.Add(MapTaxRate(reader));
                }
            }
            return taxRates;
        }

        public int GetCount(bool? isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM TaxRates";
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

        private TaxRate MapTaxRate(IDataReader reader)
        {
            return new TaxRate
            {
                TaxRateId = Convert.ToInt32(reader["TaxRateId"]),
                TaxCategoryId = Convert.ToInt32(reader["TaxCategoryId"]),
                TaxRateCode = reader["TaxRateCode"]?.ToString() ?? string.Empty,
                TaxRateName = reader["TaxRateName"]?.ToString() ?? string.Empty,
                TaxPercentage = Convert.ToDecimal(reader["TaxPercentage"]),
                Description = reader["Description"]?.ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                IsSystemRate = Convert.ToBoolean(reader["IsSystemRate"]),
                EffectiveFrom = reader["EffectiveFrom"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["EffectiveFrom"]),
                EffectiveTo = reader["EffectiveTo"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["EffectiveTo"]),
                IsCompound = Convert.ToBoolean(reader["IsCompound"]),
                IsInclusive = Convert.ToBoolean(reader["IsInclusive"]),
                TaxAccountId = reader["TaxAccountId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TaxAccountId"]),
                TaxPayableAccountId = reader["TaxPayableAccountId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TaxPayableAccountId"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"]),
                ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifiedBy"])
            };
        }

        public List<TaxRate> GetByCategoryId(int taxCategoryId)
        {
            return GetByTaxCategoryId(taxCategoryId);
        }

        public bool IsRateExists(int taxCategoryId, decimal rate, int? excludeId = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM TaxRates WHERE TaxCategoryId = @TaxCategoryId AND TaxPercentage = @Rate";
                if (excludeId.HasValue)
                {
                    query += " AND TaxRateId != @ExcludeId";
                }

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaxCategoryId", taxCategoryId);
                    command.Parameters.AddWithValue("@Rate", rate);
                    if (excludeId.HasValue)
                    {
                        command.Parameters.AddWithValue("@ExcludeId", excludeId.Value);
                    }

                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }
    }
}
