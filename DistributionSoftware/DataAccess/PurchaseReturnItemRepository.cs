using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DistributionSoftware.DataAccess
{
    public class PurchaseReturnItemRepository : IPurchaseReturnItemRepository
    {
        private readonly string _connectionString;

        public PurchaseReturnItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<PurchaseReturnItem>> GetByReturnIdAsync(int returnId)
        {
            var items = new List<PurchaseReturnItem>();
            
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.GetByReturnIdAsync: Getting items for purchase return {returnId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT pri.*, p.ProductName, p.ProductCode
                        FROM PurchaseReturnItems pri
                        INNER JOIN Products p ON pri.ProductId = p.ProductId
                        WHERE pri.ReturnId = @ReturnId
                        ORDER BY pri.ReturnItemId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReturnId", returnId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new PurchaseReturnItem
                                {
                                    ReturnItemId = reader.GetInt32(reader.GetOrdinal("ReturnItemId")),
                                    ReturnId = reader.GetInt32(reader.GetOrdinal("ReturnId")),
                                    ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                    ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),
                                    Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                                    UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                    LineTotal = reader.GetDecimal(reader.GetOrdinal("LineTotal")),
                                    BatchNo = reader.IsDBNull(reader.GetOrdinal("BatchNo")) ? null : reader.GetString(reader.GetOrdinal("BatchNo")),
                                    ExpiryDate = reader.IsDBNull(reader.GetOrdinal("ExpiryDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ExpiryDate")),
                                };
                                
                                items.Add(item);
                            }
                        }
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnItemRepository.GetByReturnIdAsync: Successfully retrieved {items.Count} items");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.GetByReturnIdAsync: Error - {ex.Message}");
                throw;
            }
            
            return items;
        }

        public async Task<PurchaseReturnItem> GetByIdAsync(int purchaseReturnItemId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.GetByIdAsync: Getting item with ID {purchaseReturnItemId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT pri.*, p.ProductName, p.ProductCode
                        FROM PurchaseReturnItems pri
                        INNER JOIN Products p ON pri.ProductId = p.ProductId
                        WHERE pri.ReturnItemId = @ReturnItemId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReturnItemId", purchaseReturnItemId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var item = new PurchaseReturnItem
                                {
                                    ReturnItemId = reader.GetInt32(reader.GetOrdinal("ReturnItemId")),
                                    ReturnId = reader.GetInt32(reader.GetOrdinal("ReturnId")),
                                    ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                    ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),
                                    Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                                    UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                    LineTotal = reader.GetDecimal(reader.GetOrdinal("LineTotal")),
                                    BatchNo = reader.IsDBNull(reader.GetOrdinal("BatchNo")) ? null : reader.GetString(reader.GetOrdinal("BatchNo")),
                                    ExpiryDate = reader.IsDBNull(reader.GetOrdinal("ExpiryDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ExpiryDate")),
                                };
                                
                                Debug.WriteLine($"PurchaseReturnItemRepository.GetByIdAsync: Successfully retrieved item {item.ProductName}");
                                return item;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.GetByIdAsync: Error - {ex.Message}");
                throw;
            }
            
            return null;
        }

        public async Task<int> CreateAsync(PurchaseReturnItem item)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.CreateAsync: Creating item for product {item.ProductName}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        INSERT INTO PurchaseReturnItems (ReturnId, ProductId, Quantity, UnitPrice, 
                                                       BatchNo, ExpiryDate)
                        VALUES (@ReturnId, @ProductId, @Quantity, @UnitPrice, 
                                @BatchNo, @ExpiryDate);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReturnId", item.ReturnId);
                        command.Parameters.AddWithValue("@ProductId", item.ProductId);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        command.Parameters.AddWithValue("@BatchNo", item.BatchNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExpiryDate", item.ExpiryDate ?? (object)DBNull.Value);
                        
                        var result = await command.ExecuteScalarAsync();
                        var itemId = Convert.ToInt32(result);
                        
                        Debug.WriteLine($"PurchaseReturnItemRepository.CreateAsync: Successfully created item with ID {itemId}");
                        return itemId;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.CreateAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(PurchaseReturnItem item)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.UpdateAsync: Updating item {item.ProductName}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        UPDATE PurchaseReturnItems 
                        SET ProductId = @ProductId, Quantity = @Quantity, UnitPrice = @UnitPrice, 
                            BatchNo = @BatchNo, ExpiryDate = @ExpiryDate
                        WHERE ReturnItemId = @ReturnItemId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReturnItemId", item.ReturnItemId);
                        command.Parameters.AddWithValue("@ProductId", item.ProductId);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        command.Parameters.AddWithValue("@BatchNo", item.BatchNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExpiryDate", item.ExpiryDate ?? (object)DBNull.Value);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        var success = rowsAffected > 0;
                        
                        Debug.WriteLine($"PurchaseReturnItemRepository.UpdateAsync: Update {(success ? "successful" : "failed")}");
                        return success;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.UpdateAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int purchaseReturnItemId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.DeleteAsync: Deleting item with ID {purchaseReturnItemId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "DELETE FROM PurchaseReturnItems WHERE ReturnItemId = @ReturnItemId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReturnItemId", purchaseReturnItemId);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        var success = rowsAffected > 0;
                        
                        Debug.WriteLine($"PurchaseReturnItemRepository.DeleteAsync: Delete {(success ? "successful" : "failed")}");
                        return success;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.DeleteAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteByReturnIdAsync(int returnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.DeleteByReturnIdAsync: Deleting all items for purchase return {returnId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "DELETE FROM PurchaseReturnItems WHERE ReturnId = @ReturnId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReturnId", returnId);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        var success = rowsAffected >= 0; // Allow 0 rows affected (no items to delete)
                        
                        Debug.WriteLine($"PurchaseReturnItemRepository.DeleteByReturnIdAsync: Delete {(success ? "successful" : "failed")}");
                        return success;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.DeleteByReturnIdAsync: Error - {ex.Message}");
                throw;
            }
        }

        public Task<decimal> CalculateLineTotalAsync(int productId, decimal quantity, decimal unitPrice)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.CalculateLineTotalAsync: Calculating line total for product {productId}");
                
                var lineTotal = quantity * unitPrice;
                
                Debug.WriteLine($"PurchaseReturnItemRepository.CalculateLineTotalAsync: Calculated line total {lineTotal}");
                return Task.FromResult(lineTotal);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.CalculateLineTotalAsync: Error - {ex.Message}");
                throw;
            }
        }
    }
}