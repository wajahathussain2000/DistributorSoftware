using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly string _connectionString;

        public ExpenseRepository()
        {
            _connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
        }

        public int Create(Expense expense)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        INSERT INTO [dbo].[Expenses] 
                        ([ExpenseCode], [Barcode], [CategoryId], [ExpenseDate], [Amount], [Description], 
                         [ReferenceNumber], [PaymentMethod], [Status], [Remarks], 
                         [CreatedBy], [ModifiedBy])
                        VALUES 
                        (@ExpenseCode, @Barcode, @CategoryId, @ExpenseDate, @Amount, @Description, 
                         @ReferenceNumber, @PaymentMethod, @Status, @Remarks, 
                         @CreatedBy, @ModifiedBy);
                        SELECT SCOPE_IDENTITY();";

                    command.Parameters.AddWithValue("@ExpenseCode", expense.ExpenseCode);
                    command.Parameters.AddWithValue("@Barcode", expense.Barcode);
                    command.Parameters.AddWithValue("@CategoryId", expense.CategoryId);
                    command.Parameters.AddWithValue("@ExpenseDate", expense.ExpenseDate.Date);
                    command.Parameters.AddWithValue("@Amount", expense.Amount);
                    command.Parameters.AddWithValue("@Description", (object)expense.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ReferenceNumber", (object)expense.ReferenceNumber ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PaymentMethod", (object)expense.PaymentMethod ?? DBNull.Value);
                    // Image parameters removed - we only need barcode images
                    command.Parameters.AddWithValue("@Status", expense.Status);
                    command.Parameters.AddWithValue("@Remarks", (object)expense.Remarks ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", (object)expense.CreatedBy ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ModifiedBy", (object)expense.ModifiedBy ?? DBNull.Value);

                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        public Expense GetById(int expenseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT e.*, ec.CategoryName, 
                               u1.Username as CreatedByName, u2.Username as ModifiedByName, u3.Username as ApprovedByName
                        FROM [dbo].[Expenses] e
                        LEFT JOIN [dbo].[ExpenseCategories] ec ON e.CategoryId = ec.CategoryId
                        LEFT JOIN [dbo].[Users] u1 ON e.CreatedBy = u1.UserId
                        LEFT JOIN [dbo].[Users] u2 ON e.ModifiedBy = u2.UserId
                        LEFT JOIN [dbo].[Users] u3 ON e.ApprovedBy = u3.UserId
                        WHERE e.ExpenseId = @ExpenseId";

                    command.Parameters.AddWithValue("@ExpenseId", expenseId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapExpenseFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Expense> GetAll()
        {
            var expenses = new List<Expense>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT e.*, ec.CategoryName, 
                               u1.Username as CreatedByName, u2.Username as ModifiedByName, u3.Username as ApprovedByName
                        FROM [dbo].[Expenses] e
                        LEFT JOIN [dbo].[ExpenseCategories] ec ON e.CategoryId = ec.CategoryId
                        LEFT JOIN [dbo].[Users] u1 ON e.CreatedBy = u1.UserId
                        LEFT JOIN [dbo].[Users] u2 ON e.ModifiedBy = u2.UserId
                        LEFT JOIN [dbo].[Users] u3 ON e.ApprovedBy = u3.UserId
                        ORDER BY e.CreatedDate DESC";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(MapExpenseFromReader(reader));
                        }
                    }
                }
            }
            return expenses;
        }

        public bool Update(Expense expense)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        UPDATE [dbo].[Expenses] 
                        SET [ExpenseCode] = @ExpenseCode, [Barcode] = @Barcode, [CategoryId] = @CategoryId,
                            [ExpenseDate] = @ExpenseDate, [Amount] = @Amount, [Description] = @Description,
                            [ReferenceNumber] = @ReferenceNumber, [PaymentMethod] = @PaymentMethod,
                            [ImagePath] = @ImagePath, [ImageData] = @ImageData, [Status] = @Status,
                            [ApprovedBy] = @ApprovedBy, [ApprovedDate] = @ApprovedDate, [Remarks] = @Remarks,
                            [ModifiedDate] = GETDATE(), [ModifiedBy] = @ModifiedBy
                        WHERE [ExpenseId] = @ExpenseId";

                    command.Parameters.AddWithValue("@ExpenseId", expense.ExpenseId);
                    command.Parameters.AddWithValue("@ExpenseCode", expense.ExpenseCode);
                    command.Parameters.AddWithValue("@Barcode", expense.Barcode);
                    command.Parameters.AddWithValue("@CategoryId", expense.CategoryId);
                    command.Parameters.AddWithValue("@ExpenseDate", expense.ExpenseDate.Date);
                    command.Parameters.AddWithValue("@Amount", expense.Amount);
                    command.Parameters.AddWithValue("@Description", (object)expense.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ReferenceNumber", (object)expense.ReferenceNumber ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PaymentMethod", (object)expense.PaymentMethod ?? DBNull.Value);
                    // Image parameters removed - we only need barcode images
                    command.Parameters.AddWithValue("@Status", expense.Status);
                    command.Parameters.AddWithValue("@ApprovedBy", (object)expense.ApprovedBy ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ApprovedDate", (object)expense.ApprovedDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Remarks", (object)expense.Remarks ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ModifiedBy", (object)expense.ModifiedBy ?? DBNull.Value);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int expenseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM [dbo].[Expenses] WHERE [ExpenseId] = @ExpenseId";
                    command.Parameters.AddWithValue("@ExpenseId", expenseId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Expense> Search(string searchTerm)
        {
            var expenses = new List<Expense>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT e.*, ec.CategoryName, 
                               u1.Username as CreatedByName, u2.Username as ModifiedByName, u3.Username as ApprovedByName
                        FROM [dbo].[Expenses] e
                        LEFT JOIN [dbo].[ExpenseCategories] ec ON e.CategoryId = ec.CategoryId
                        LEFT JOIN [dbo].[Users] u1 ON e.CreatedBy = u1.UserId
                        LEFT JOIN [dbo].[Users] u2 ON e.ModifiedBy = u2.UserId
                        LEFT JOIN [dbo].[Users] u3 ON e.ApprovedBy = u3.UserId
                        WHERE LOWER(e.Description) LIKE LOWER(@SearchTerm) 
                           OR LOWER(e.ReferenceNumber) LIKE LOWER(@SearchTerm)
                           OR LOWER(e.ExpenseCode) LIKE LOWER(@SearchTerm)
                           OR LOWER(e.Barcode) LIKE LOWER(@SearchTerm)
                        ORDER BY e.CreatedDate DESC";

                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(MapExpenseFromReader(reader));
                        }
                    }
                }
            }
            return expenses;
        }

        public List<Expense> GetByCategory(int categoryId)
        {
            var expenses = new List<Expense>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT e.*, ec.CategoryName, 
                               u1.Username as CreatedByName, u2.Username as ModifiedByName, u3.Username as ApprovedByName
                        FROM [dbo].[Expenses] e
                        LEFT JOIN [dbo].[ExpenseCategories] ec ON e.CategoryId = ec.CategoryId
                        LEFT JOIN [dbo].[Users] u1 ON e.CreatedBy = u1.UserId
                        LEFT JOIN [dbo].[Users] u2 ON e.ModifiedBy = u2.UserId
                        LEFT JOIN [dbo].[Users] u3 ON e.ApprovedBy = u3.UserId
                        WHERE e.CategoryId = @CategoryId
                        ORDER BY e.CreatedDate DESC";

                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(MapExpenseFromReader(reader));
                        }
                    }
                }
            }
            return expenses;
        }

        public List<Expense> GetByStatus(string status)
        {
            var expenses = new List<Expense>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT e.*, ec.CategoryName, 
                               u1.Username as CreatedByName, u2.Username as ModifiedByName, u3.Username as ApprovedByName
                        FROM [dbo].[Expenses] e
                        LEFT JOIN [dbo].[ExpenseCategories] ec ON e.CategoryId = ec.CategoryId
                        LEFT JOIN [dbo].[Users] u1 ON e.CreatedBy = u1.UserId
                        LEFT JOIN [dbo].[Users] u2 ON e.ModifiedBy = u2.UserId
                        LEFT JOIN [dbo].[Users] u3 ON e.ApprovedBy = u3.UserId
                        WHERE e.Status = @Status
                        ORDER BY e.CreatedDate DESC";

                    command.Parameters.AddWithValue("@Status", status);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(MapExpenseFromReader(reader));
                        }
                    }
                }
            }
            return expenses;
        }

        public List<Expense> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var expenses = new List<Expense>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT e.*, ec.CategoryName, 
                               u1.Username as CreatedByName, u2.Username as ModifiedByName, u3.Username as ApprovedByName
                        FROM [dbo].[Expenses] e
                        LEFT JOIN [dbo].[ExpenseCategories] ec ON e.CategoryId = ec.CategoryId
                        LEFT JOIN [dbo].[Users] u1 ON e.CreatedBy = u1.UserId
                        LEFT JOIN [dbo].[Users] u2 ON e.ModifiedBy = u2.UserId
                        LEFT JOIN [dbo].[Users] u3 ON e.ApprovedBy = u3.UserId
                        WHERE e.ExpenseDate BETWEEN @StartDate AND @EndDate
                        ORDER BY e.CreatedDate DESC";

                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(MapExpenseFromReader(reader));
                        }
                    }
                }
            }
            return expenses;
        }

        public List<Expense> GetByAmountRange(decimal minAmount, decimal maxAmount)
        {
            var expenses = new List<Expense>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT e.*, ec.CategoryName, 
                               u1.Username as CreatedByName, u2.Username as ModifiedByName, u3.Username as ApprovedByName
                        FROM [dbo].[Expenses] e
                        LEFT JOIN [dbo].[ExpenseCategories] ec ON e.CategoryId = ec.CategoryId
                        LEFT JOIN [dbo].[Users] u1 ON e.CreatedBy = u1.UserId
                        LEFT JOIN [dbo].[Users] u2 ON e.ModifiedBy = u2.UserId
                        LEFT JOIN [dbo].[Users] u3 ON e.ApprovedBy = u3.UserId
                        WHERE e.Amount BETWEEN @MinAmount AND @MaxAmount
                        ORDER BY e.CreatedDate DESC";

                    command.Parameters.AddWithValue("@MinAmount", minAmount);
                    command.Parameters.AddWithValue("@MaxAmount", maxAmount);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(MapExpenseFromReader(reader));
                        }
                    }
                }
            }
            return expenses;
        }

        public bool IsExpenseCodeExists(string expenseCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM [dbo].[Expenses] WHERE [ExpenseCode] = @ExpenseCode";
                    command.Parameters.AddWithValue("@ExpenseCode", expenseCode);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public bool IsBarcodeExists(string barcode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM [dbo].[Expenses] WHERE [Barcode] = @Barcode";
                    command.Parameters.AddWithValue("@Barcode", barcode);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public string GetNextExpenseCode()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT TOP 1 [ExpenseCode] 
                        FROM [dbo].[Expenses] 
                        WHERE [ExpenseCode] LIKE 'EXP-' + CAST(YEAR(GETDATE()) AS VARCHAR(4)) + '-%'
                        ORDER BY [ExpenseCode] DESC";

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        var lastCode = result.ToString();
                        var parts = lastCode.Split('-');
                        if (parts.Length == 3)
                        {
                            var year = parts[1];
                            var number = int.Parse(parts[2]);
                            return $"EXP-{year}-{(number + 1):D4}";
                        }
                    }
                    return $"EXP-{DateTime.Now.Year}-0001";
                }
            }
        }

        public string GenerateBarcode()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"EXP{timestamp}{random}";
        }

        public bool SaveImage(int expenseId, byte[] imageData, string imagePath)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        UPDATE [dbo].[Expenses] 
                        SET [ImageData] = @ImageData, [ImagePath] = @ImagePath, [ModifiedDate] = GETDATE()
                        WHERE [ExpenseId] = @ExpenseId";

                    command.Parameters.AddWithValue("@ExpenseId", expenseId);
                    command.Parameters.AddWithValue("@ImageData", (object)imageData ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public byte[] GetImageData(int expenseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [ImageData] FROM [dbo].[Expenses] WHERE [ExpenseId] = @ExpenseId";
                    command.Parameters.AddWithValue("@ExpenseId", expenseId);

                    var result = command.ExecuteScalar();
                    return result as byte[];
                }
            }
        }

        public string GetImagePath(int expenseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [ImagePath] FROM [dbo].[Expenses] WHERE [ExpenseId] = @ExpenseId";
                    command.Parameters.AddWithValue("@ExpenseId", expenseId);

                    var result = command.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        private Expense MapExpenseFromReader(SqlDataReader reader)
        {
            return new Expense
            {
                ExpenseId = reader.GetInt32(reader.GetOrdinal("ExpenseId")),
                ExpenseCode = reader.GetString(reader.GetOrdinal("ExpenseCode")),
                Barcode = reader.GetString(reader.GetOrdinal("Barcode")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                CategoryName = reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? null : reader.GetString(reader.GetOrdinal("CategoryName")),
                ExpenseDate = reader.GetDateTime(reader.GetOrdinal("ExpenseDate")),
                Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                ReferenceNumber = reader.IsDBNull(reader.GetOrdinal("ReferenceNumber")) ? null : reader.GetString(reader.GetOrdinal("ReferenceNumber")),
                PaymentMethod = reader.IsDBNull(reader.GetOrdinal("PaymentMethod")) ? null : reader.GetString(reader.GetOrdinal("PaymentMethod")),
                // Image properties removed - we only need barcode images
                Status = reader.GetString(reader.GetOrdinal("Status")),
                ApprovedBy = reader.IsDBNull(reader.GetOrdinal("ApprovedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ApprovedBy")),
                ApprovedByName = reader.IsDBNull(reader.GetOrdinal("ApprovedByName")) ? null : reader.GetString(reader.GetOrdinal("ApprovedByName")),
                ApprovedDate = reader.IsDBNull(reader.GetOrdinal("ApprovedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovedDate")),
                Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                CreatedByName = reader.IsDBNull(reader.GetOrdinal("CreatedByName")) ? null : reader.GetString(reader.GetOrdinal("CreatedByName")),
                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ModifiedBy")),
                ModifiedByName = reader.IsDBNull(reader.GetOrdinal("ModifiedByName")) ? null : reader.GetString(reader.GetOrdinal("ModifiedByName"))
            };
        }
    }
}
