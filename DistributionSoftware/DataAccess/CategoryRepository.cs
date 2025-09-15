using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class CategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? ConfigurationManager.DistributionConnectionString;
        }

        public List<Category> GetActiveCategories()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT CategoryId, CategoryName FROM Categories WHERE IsActive = 1 ORDER BY CategoryName";
                    
                    var categories = new List<Category>();
                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                CategoryName = reader["CategoryName"].ToString()
                            });
                        }
                    }
                    return categories;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("CategoryRepository.GetActiveCategories", ex);
                throw;
            }
        }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
