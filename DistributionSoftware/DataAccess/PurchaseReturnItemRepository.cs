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

        public async Task<List<PurchaseReturnItem>> GetByPurchaseReturnIdAsync(int purchaseReturnId)
        {
            var items = new List<PurchaseReturnItem>();
            
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.GetByPurchaseReturnIdAsync: Getting items for purchase return {purchaseReturnId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT pri.*, p.ProductName, p.ProductCode
                        FROM PurchaseReturnItems pri
                        INNER JOIN Products p ON pri.ProductId = p.ProductId
                        WHERE pri.PurchaseReturnId = @PurchaseReturnId
                        ORDER BY pri.PurchaseReturnItemId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var item = new PurchaseReturnItem
                                {
                                    PurchaseReturnItemId = reader.GetInt32("PurchaseReturnItemId"),
                                    PurchaseReturnId = reader.GetInt32("PurchaseReturnId"),
                                    ProductId = reader.GetInt32("ProductId"),
                                    ProductName = reader.GetString("ProductName"),
                                    ProductCode = reader.GetString("ProductCode"),
                                    Quantity = reader.GetDecimal("Quantity"),
                                    UnitPrice = reader.GetDecimal("UnitPrice"),
                                    LineTotal = reader.GetDecimal("LineTotal"),
                                    BatchNumber = reader.IsDBNull("BatchNumber") ? null : reader.GetString("BatchNumber"),
                                    ExpiryDate = reader.IsDBNull("ExpiryDate") ? (DateTime?)null : reader.GetDateTime("ExpiryDate"),
                                    Notes = reader.IsDBNull("Notes") ? null : reader.GetString("Notes")
                                };
                                
                                items.Add(item);
                            }
                        }
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnItemRepository.GetByPurchaseReturnIdAsync: Successfully retrieved {items.Count} items");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.GetByPurchaseReturnIdAsync: Error - {ex.Message}");
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
                        WHERE pri.PurchaseReturnItemId = @PurchaseReturnItemId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnItemId", purchaseReturnItemId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var item = new PurchaseReturnItem
                                {
                                    PurchaseReturnItemId = reader.GetInt32("PurchaseReturnItemId"),
                                    PurchaseReturnId = reader.GetInt32("PurchaseReturnId"),
                                    ProductId = reader.GetInt32("ProductId"),
                                    ProductName = reader.GetString("ProductName"),
                                    ProductCode = reader.GetString("ProductCode"),
                                    Quantity = reader.GetDecimal("Quantity"),
                                    UnitPrice = reader.GetDecimal("UnitPrice"),
                                    LineTotal = reader.GetDecimal("LineTotal"),
                                    BatchNumber = reader.IsDBNull("BatchNumber") ? null : reader.GetString("BatchNumber"),
                                    ExpiryDate = reader.IsDBNull("ExpiryDate") ? (DateTime?)null : reader.GetDateTime("ExpiryDate"),
                                    Notes = reader.IsDBNull("Notes") ? null : reader.GetString("Notes")
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
                        INSERT INTO PurchaseReturnItems (PurchaseReturnId, ProductId, Quantity, UnitPrice, LineTotal, 
                                                       BatchNumber, ExpiryDate, Notes)
                        VALUES (@PurchaseReturnId, @ProductId, @Quantity, @UnitPrice, @LineTotal, 
                                @BatchNumber, @ExpiryDate, @Notes);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnId", item.PurchaseReturnId);
                        command.Parameters.AddWithValue("@ProductId", item.ProductId);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        command.Parameters.AddWithValue("@LineTotal", item.LineTotal);
                        command.Parameters.AddWithValue("@BatchNumber", item.BatchNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExpiryDate", item.ExpiryDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Notes", item.Notes ?? (object)DBNull.Value);
                        
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
                            LineTotal = @LineTotal, BatchNumber = @BatchNumber, ExpiryDate = @ExpiryDate, 
                            Notes = @Notes
                        WHERE PurchaseReturnItemId = @PurchaseReturnItemId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnItemId", item.PurchaseReturnItemId);
                        command.Parameters.AddWithValue("@ProductId", item.ProductId);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        command.Parameters.AddWithValue("@LineTotal", item.LineTotal);
                        command.Parameters.AddWithValue("@BatchNumber", item.BatchNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExpiryDate", item.ExpiryDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Notes", item.Notes ?? (object)DBNull.Value);
                        
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
                    
                    var query = "DELETE FROM PurchaseReturnItems WHERE PurchaseReturnItemId = @PurchaseReturnItemId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnItemId", purchaseReturnItemId);
                        
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

        public async Task<bool> DeleteByPurchaseReturnIdAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.DeleteByPurchaseReturnIdAsync: Deleting all items for purchase return {purchaseReturnId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "DELETE FROM PurchaseReturnItems WHERE PurchaseReturnId = @PurchaseReturnId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        var success = rowsAffected >= 0; // Allow 0 rows affected (no items to delete)
                        
                        Debug.WriteLine($"PurchaseReturnItemRepository.DeleteByPurchaseReturnIdAsync: Delete {(success ? "successful" : "failed")}");
                        return success;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.DeleteByPurchaseReturnIdAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<decimal> CalculateLineTotalAsync(int productId, decimal quantity, decimal unitPrice)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.CalculateLineTotalAsync: Calculating line total for product {productId}");
                
                var lineTotal = quantity * unitPrice;
                
                Debug.WriteLine($"PurchaseReturnItemRepository.CalculateLineTotalAsync: Calculated line total {lineTotal}");
                return lineTotal;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnItemRepository.CalculateLineTotalAsync: Error - {ex.Message}");
                throw;
            }
        }
    }
}
