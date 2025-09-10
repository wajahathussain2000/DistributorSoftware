using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class DeliveryChallanItemRepository : IDeliveryChallanItemRepository
    {
        private readonly string _connectionString;

        public DeliveryChallanItemRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public List<DeliveryChallanItem> GetByChallanId(int challanId)
        {
            var items = new List<DeliveryChallanItem>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT ChallanItemId, ChallanId, ProductId, ProductCode, ProductName, 
                               Quantity, Unit, UnitPrice, TotalAmount, Remarks, CreatedDate, CreatedBy
                        FROM DeliveryChallanItems 
                        WHERE ChallanId = @ChallanId
                        ORDER BY ChallanItemId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanId", challanId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(MapDeliveryChallanItem(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery challan items: {ex.Message}", ex);
            }

            return items;
        }

        public DeliveryChallanItem GetById(int challanItemId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT ChallanItemId, ChallanId, ProductId, ProductCode, ProductName, 
                               Quantity, Unit, UnitPrice, TotalAmount, Remarks, CreatedDate, CreatedBy
                        FROM DeliveryChallanItems 
                        WHERE ChallanItemId = @ChallanItemId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanItemId", challanItemId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapDeliveryChallanItem(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery challan item: {ex.Message}", ex);
            }

            return null;
        }

        public int Create(DeliveryChallanItem item)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        INSERT INTO DeliveryChallanItems 
                        (ChallanId, ProductId, ProductCode, ProductName, Quantity, Unit, 
                         UnitPrice, TotalAmount, Remarks, CreatedDate, CreatedBy)
                        VALUES 
                        (@ChallanId, @ProductId, @ProductCode, @ProductName, @Quantity, @Unit, 
                         @UnitPrice, @TotalAmount, @Remarks, @CreatedDate, @CreatedBy);
                        SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanId", item.ChallanId);
                        command.Parameters.AddWithValue("@ProductId", item.ProductId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductCode", item.ProductCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductName", item.ProductName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@Unit", item.Unit ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        command.Parameters.AddWithValue("@TotalAmount", item.TotalAmount);
                        command.Parameters.AddWithValue("@Remarks", item.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedDate", item.CreatedDate);
                        command.Parameters.AddWithValue("@CreatedBy", item.CreatedBy);

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating delivery challan item: {ex.Message}", ex);
            }
        }

        public bool Update(DeliveryChallanItem item)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        UPDATE DeliveryChallanItems 
                        SET ProductId = @ProductId, ProductCode = @ProductCode, ProductName = @ProductName, 
                            Quantity = @Quantity, Unit = @Unit, UnitPrice = @UnitPrice, 
                            TotalAmount = @TotalAmount, Remarks = @Remarks
                        WHERE ChallanItemId = @ChallanItemId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanItemId", item.ChallanItemId);
                        command.Parameters.AddWithValue("@ProductId", item.ProductId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductCode", item.ProductCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductName", item.ProductName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@Unit", item.Unit ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        command.Parameters.AddWithValue("@TotalAmount", item.TotalAmount);
                        command.Parameters.AddWithValue("@Remarks", item.Remarks ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating delivery challan item: {ex.Message}", ex);
            }
        }

        public bool Delete(int challanItemId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "DELETE FROM DeliveryChallanItems WHERE ChallanItemId = @ChallanItemId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanItemId", challanItemId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting delivery challan item: {ex.Message}", ex);
            }
        }

        public bool DeleteByChallanId(int challanId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "DELETE FROM DeliveryChallanItems WHERE ChallanId = @ChallanId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanId", challanId);
                        return command.ExecuteNonQuery() >= 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting delivery challan items: {ex.Message}", ex);
            }
        }

        private DeliveryChallanItem MapDeliveryChallanItem(SqlDataReader reader)
        {
            return new DeliveryChallanItem
            {
                ChallanItemId = Convert.ToInt32(reader["ChallanItemId"]),
                ChallanId = Convert.ToInt32(reader["ChallanId"]),
                ProductId = reader["ProductId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ProductId"]),
                ProductCode = reader["ProductCode"]?.ToString(),
                ProductName = reader["ProductName"]?.ToString(),
                Quantity = Convert.ToDecimal(reader["Quantity"]),
                Unit = reader["Unit"]?.ToString(),
                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                Remarks = reader["Remarks"]?.ToString(),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = Convert.ToInt32(reader["CreatedBy"])
            };
        }
    }
}
