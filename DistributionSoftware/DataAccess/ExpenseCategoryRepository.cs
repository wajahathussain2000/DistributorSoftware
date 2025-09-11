using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly string _connectionString;

        public ExpenseCategoryRepository()
        {
            _connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
        }

        public ExpenseCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Create(ExpenseCategory category)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        INSERT INTO ExpenseCategories (CategoryName, CategoryCode, Description, IsActive, CreatedDate, CreatedBy)
                        VALUES (@CategoryName, @CategoryCode, @Description, @IsActive, @CreatedDate, @CreatedBy)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                        command.Parameters.AddWithValue("@CategoryCode", (object)category.CategoryCode ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", (object)category.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", category.IsActive);
                        command.Parameters.AddWithValue("@CreatedDate", category.CreatedDate);
                        command.Parameters.AddWithValue("@CreatedBy", (object)category.CreatedBy ?? DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating expense category: {ex.Message}");
                return false;
            }
        }

        public bool Update(ExpenseCategory category)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        UPDATE ExpenseCategories 
                        SET CategoryName = @CategoryName, 
                            CategoryCode = @CategoryCode, 
                            Description = @Description, 
                            IsActive = @IsActive, 
                            ModifiedDate = @ModifiedDate, 
                            ModifiedBy = @ModifiedBy
                        WHERE CategoryId = @CategoryId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                        command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                        command.Parameters.AddWithValue("@CategoryCode", (object)category.CategoryCode ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", (object)category.Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IsActive", category.IsActive);
                        command.Parameters.AddWithValue("@ModifiedDate", category.ModifiedDate ?? DateTime.Now);
                        command.Parameters.AddWithValue("@ModifiedBy", (object)category.ModifiedBy ?? DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating expense category: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int categoryId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "UPDATE ExpenseCategories SET IsActive = 0 WHERE CategoryId = @CategoryId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", categoryId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting expense category: {ex.Message}");
                return false;
            }
        }

        public ExpenseCategory GetById(int categoryId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT CategoryId, CategoryName, CategoryCode, Description, IsActive, 
                               CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM ExpenseCategories 
                        WHERE CategoryId = @CategoryId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", categoryId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToExpenseCategory(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting expense category by ID: {ex.Message}");
            }
            return null;
        }

        public List<ExpenseCategory> GetAll()
        {
            var categories = new List<ExpenseCategory>();
            try
            {
                System.Diagnostics.Debug.WriteLine($"Connection string: {_connectionString}");
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    System.Diagnostics.Debug.WriteLine("Database connection opened successfully");
                    var query = @"
                        SELECT CategoryId, CategoryName, CategoryCode, Description, IsActive, 
                               CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM ExpenseCategories 
                        ORDER BY CategoryName";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        int rowCount = 0;
                        while (reader.Read())
                        {
                            try
                            {
                                System.Diagnostics.Debug.WriteLine($"Processing row {rowCount + 1}");
                                var category = MapReaderToExpenseCategory(reader);
                                categories.Add(category);
                                System.Diagnostics.Debug.WriteLine($"Successfully mapped category: {category.CategoryName}");
                                rowCount++;
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error mapping row {rowCount + 1}: {ex.Message}");
                                throw;
                            }
                        }
                        System.Diagnostics.Debug.WriteLine($"Successfully processed {rowCount} rows");
                    }
                    System.Diagnostics.Debug.WriteLine($"Retrieved {categories.Count} categories from database");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting all expense categories: {ex.Message}");
            }
            return categories;
        }

        public List<ExpenseCategory> GetActive()
        {
            var categories = new List<ExpenseCategory>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT CategoryId, CategoryName, CategoryCode, Description, IsActive, 
                               CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM ExpenseCategories 
                        WHERE IsActive = 1
                        ORDER BY CategoryName";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(MapReaderToExpenseCategory(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting active expense categories: {ex.Message}");
            }
            return categories;
        }

        public List<ExpenseCategory> Search(string searchTerm)
        {
            var categories = new List<ExpenseCategory>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT CategoryId, CategoryName, CategoryCode, Description, IsActive, 
                               CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM ExpenseCategories 
                        WHERE (LOWER(CategoryName) LIKE LOWER(@SearchTerm) OR 
                               LOWER(CategoryCode) LIKE LOWER(@SearchTerm) OR 
                               LOWER(Description) LIKE LOWER(@SearchTerm))
                        ORDER BY CategoryName";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categories.Add(MapReaderToExpenseCategory(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching expense categories: {ex.Message}");
            }
            return categories;
        }

        public List<ExpenseCategory> GetByStatus(bool isActive)
        {
            var categories = new List<ExpenseCategory>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT CategoryId, CategoryName, CategoryCode, Description, IsActive, 
                               CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM ExpenseCategories 
                        WHERE IsActive = @IsActive
                        ORDER BY CategoryName";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IsActive", isActive);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categories.Add(MapReaderToExpenseCategory(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting expense categories by status: {ex.Message}");
            }
            return categories;
        }

        public bool ExistsByName(string categoryName, int excludeId = 0)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT COUNT(1) FROM ExpenseCategories WHERE CategoryName = @CategoryName AND CategoryId != @ExcludeId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryName", categoryName);
                        command.Parameters.AddWithValue("@ExcludeId", excludeId);
                        return (int)command.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking category name exists: {ex.Message}");
                return false;
            }
        }

        public bool ExistsByCode(string categoryCode, int excludeId = 0)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT COUNT(1) FROM ExpenseCategories WHERE CategoryCode = @CategoryCode AND CategoryId != @ExcludeId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryCode", categoryCode);
                        command.Parameters.AddWithValue("@ExcludeId", excludeId);
                        return (int)command.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking category code exists: {ex.Message}");
                return false;
            }
        }

        private ExpenseCategory MapReaderToExpenseCategory(SqlDataReader reader)
        {
            return new ExpenseCategory
            {
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                CategoryCode = reader.IsDBNull(reader.GetOrdinal("CategoryCode")) ? null : reader.GetString(reader.GetOrdinal("CategoryCode")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")), // CreatedBy is NOT NULL in database
                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ModifiedBy"))
            };
        }
    }
}
