using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DistributionSoftware.DataAccess
{
    public class PurchaseReturnRepository : IPurchaseReturnRepository
    {
        private readonly string _connectionString;

        public PurchaseReturnRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<PurchaseReturn> GetByIdAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetByIdAsync: Getting purchase return with ID {purchaseReturnId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT pr.*, s.SupplierName, pi.InvoiceNumber as ReferencePurchaseNumber
                        FROM PurchaseReturns pr
                        LEFT JOIN Suppliers s ON pr.SupplierId = s.SupplierId
                        LEFT JOIN PurchaseInvoices pi ON pr.ReferencePurchaseId = pi.PurchaseInvoiceId
                        WHERE pr.PurchaseReturnId = @PurchaseReturnId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var purchaseReturn = new PurchaseReturn
                                {
                                    PurchaseReturnId = Convert.ToInt32(reader["PurchaseReturnId"]),
                                    ReturnNumber = reader["ReturnNo"].ToString(),
                                    Barcode = reader["Barcode"].ToString(),
                                    SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                    SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName")) ? null : reader["SupplierName"].ToString(),
                                    ReferencePurchaseId = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseId")) ? (int?)null : Convert.ToInt32(reader["ReferencePurchaseId"]),
                                    ReferencePurchaseNumber = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseNumber")) ? null : reader["ReferencePurchaseNumber"].ToString(),
                                    ReturnDate = reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                                    TaxAdjust = reader.GetDecimal(reader.GetOrdinal("TaxAdjust")),
                                    DiscountAdjust = reader.GetDecimal(reader.GetOrdinal("DiscountAdjust")),
                                    FreightAdjust = reader.GetDecimal(reader.GetOrdinal("FreightAdjust")),
                                    NetReturnAmount = reader.GetDecimal(reader.GetOrdinal("NetReturnAmount")),
                                    Reason = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : reader["Reason"].ToString(),
                                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                                    ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                                };
                                
                                Debug.WriteLine($"PurchaseReturnRepository.GetByIdAsync: Successfully retrieved purchase return {purchaseReturn.ReturnNumber}");
                                return purchaseReturn;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetByIdAsync: Error - {ex.Message}");
                throw;
            }
            
            return null;
        }

        public async Task<PurchaseReturn> GetByReturnNumberAsync(string returnNumber)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetByReturnNumberAsync: Getting purchase return with number {returnNumber}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT pr.*, s.SupplierName, pi.InvoiceNumber as ReferencePurchaseNumber
                        FROM PurchaseReturns pr
                        LEFT JOIN Suppliers s ON pr.SupplierId = s.SupplierId
                        LEFT JOIN PurchaseInvoices pi ON pr.ReferencePurchaseId = pi.PurchaseInvoiceId
                        WHERE pr.ReturnNo = @ReturnNumber";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReturnNumber", returnNumber);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var purchaseReturn = new PurchaseReturn
                                {
                                    PurchaseReturnId = Convert.ToInt32(reader["PurchaseReturnId"]),
                                    ReturnNumber = reader["ReturnNo"].ToString(),
                                    Barcode = reader["Barcode"].ToString(),
                                    SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                    SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName")) ? null : reader["SupplierName"].ToString(),
                                    ReferencePurchaseId = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseId")) ? (int?)null : Convert.ToInt32(reader["ReferencePurchaseId"]),
                                    ReferencePurchaseNumber = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseNumber")) ? null : reader["ReferencePurchaseNumber"].ToString(),
                                    ReturnDate = reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                                    TaxAdjust = reader.GetDecimal(reader.GetOrdinal("TaxAdjust")),
                                    DiscountAdjust = reader.GetDecimal(reader.GetOrdinal("DiscountAdjust")),
                                    FreightAdjust = reader.GetDecimal(reader.GetOrdinal("FreightAdjust")),
                                    NetReturnAmount = reader.GetDecimal(reader.GetOrdinal("NetReturnAmount")),
                                    Reason = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : reader["Reason"].ToString(),
                                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                                    ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                                };
                                
                                Debug.WriteLine($"PurchaseReturnRepository.GetByReturnNumberAsync: Successfully retrieved purchase return {purchaseReturn.ReturnNumber}");
                                return purchaseReturn;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetByReturnNumberAsync: Error - {ex.Message}");
                throw;
            }
            
            return null;
        }

        public async Task<List<PurchaseReturn>> GetAllAsync()
        {
            var purchaseReturns = new List<PurchaseReturn>();
            
            try
            {
                Debug.WriteLine("PurchaseReturnRepository.GetAllAsync: Getting all purchase returns");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT pr.*, s.SupplierName, pi.InvoiceNumber as ReferencePurchaseNumber
                        FROM PurchaseReturns pr
                        LEFT JOIN Suppliers s ON pr.SupplierId = s.SupplierId
                        LEFT JOIN PurchaseInvoices pi ON pr.ReferencePurchaseId = pi.PurchaseInvoiceId
                        ORDER BY pr.CreatedDate DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var purchaseReturn = new PurchaseReturn
                            {
                                PurchaseReturnId = Convert.ToInt32(reader["PurchaseReturnId"]),
                                ReturnNumber = reader["ReturnNo"].ToString(),
                                Barcode = reader["Barcode"].ToString(),
                                SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName")) ? null : reader["SupplierName"].ToString(),
                                ReferencePurchaseId = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseId")) ? (int?)null : Convert.ToInt32(reader["ReferencePurchaseId"]),
                                ReferencePurchaseNumber = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseNumber")) ? null : reader["ReferencePurchaseNumber"].ToString(),
                                ReturnDate = reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                                TaxAdjust = reader.GetDecimal(reader.GetOrdinal("TaxAdjust")),
                                DiscountAdjust = reader.GetDecimal(reader.GetOrdinal("DiscountAdjust")),
                                FreightAdjust = reader.GetDecimal(reader.GetOrdinal("FreightAdjust")),
                                NetReturnAmount = reader.GetDecimal(reader.GetOrdinal("NetReturnAmount")),
                                Reason = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : reader["Reason"].ToString(),
                                CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                            };
                            
                            purchaseReturns.Add(purchaseReturn);
                        }
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnRepository.GetAllAsync: Successfully retrieved {purchaseReturns.Count} purchase returns");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetAllAsync: Error - {ex.Message}");
                throw;
            }
            
            return purchaseReturns;
        }

        public async Task<List<PurchaseReturn>> GetBySupplierIdAsync(int supplierId)
        {
            var purchaseReturns = new List<PurchaseReturn>();
            
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetBySupplierIdAsync: Getting purchase returns for supplier {supplierId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT pr.*, s.SupplierName, pi.InvoiceNumber as ReferencePurchaseNumber
                        FROM PurchaseReturns pr
                        LEFT JOIN Suppliers s ON pr.SupplierId = s.SupplierId
                        LEFT JOIN PurchaseInvoices pi ON pr.ReferencePurchaseId = pi.PurchaseInvoiceId
                        WHERE pr.SupplierId = @SupplierId
                        ORDER BY pr.CreatedDate DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var purchaseReturn = new PurchaseReturn
                                {
                                    PurchaseReturnId = Convert.ToInt32(reader["PurchaseReturnId"]),
                                    ReturnNumber = reader["ReturnNo"].ToString(),
                                    Barcode = reader["Barcode"].ToString(),
                                    SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                    SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName")) ? null : reader["SupplierName"].ToString(),
                                    ReferencePurchaseId = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseId")) ? (int?)null : Convert.ToInt32(reader["ReferencePurchaseId"]),
                                    ReferencePurchaseNumber = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseNumber")) ? null : reader["ReferencePurchaseNumber"].ToString(),
                                    ReturnDate = reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                                    TaxAdjust = reader.GetDecimal(reader.GetOrdinal("TaxAdjust")),
                                    DiscountAdjust = reader.GetDecimal(reader.GetOrdinal("DiscountAdjust")),
                                    FreightAdjust = reader.GetDecimal(reader.GetOrdinal("FreightAdjust")),
                                    NetReturnAmount = reader.GetDecimal(reader.GetOrdinal("NetReturnAmount")),
                                    Reason = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : reader["Reason"].ToString(),
                                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                                    ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                                };
                                
                                purchaseReturns.Add(purchaseReturn);
                            }
                        }
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnRepository.GetBySupplierIdAsync: Successfully retrieved {purchaseReturns.Count} purchase returns for supplier {supplierId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetBySupplierIdAsync: Error - {ex.Message}");
                throw;
            }
            
            return purchaseReturns;
        }

        public async Task<List<PurchaseReturn>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var purchaseReturns = new List<PurchaseReturn>();
            
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetByDateRangeAsync: Getting purchase returns from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT pr.*, s.SupplierName, pi.InvoiceNumber as ReferencePurchaseNumber
                        FROM PurchaseReturns pr
                        LEFT JOIN Suppliers s ON pr.SupplierId = s.SupplierId
                        LEFT JOIN PurchaseInvoices pi ON pr.ReferencePurchaseId = pi.PurchaseInvoiceId
                        WHERE pr.ReturnDate BETWEEN @StartDate AND @EndDate
                        ORDER BY pr.ReturnDate DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddTicks(-1));
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var purchaseReturn = new PurchaseReturn
                                {
                                    PurchaseReturnId = Convert.ToInt32(reader["PurchaseReturnId"]),
                                    ReturnNumber = reader["ReturnNo"].ToString(),
                                    Barcode = reader["Barcode"].ToString(),
                                    SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                    SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName")) ? null : reader["SupplierName"].ToString(),
                                    ReferencePurchaseId = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseId")) ? (int?)null : Convert.ToInt32(reader["ReferencePurchaseId"]),
                                    ReferencePurchaseNumber = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseNumber")) ? null : reader["ReferencePurchaseNumber"].ToString(),
                                    ReturnDate = reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                                    TaxAdjust = reader.GetDecimal(reader.GetOrdinal("TaxAdjust")),
                                    DiscountAdjust = reader.GetDecimal(reader.GetOrdinal("DiscountAdjust")),
                                    FreightAdjust = reader.GetDecimal(reader.GetOrdinal("FreightAdjust")),
                                    NetReturnAmount = reader.GetDecimal(reader.GetOrdinal("NetReturnAmount")),
                                    Reason = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : reader["Reason"].ToString(),
                                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                                    ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                                };
                                
                                purchaseReturns.Add(purchaseReturn);
                            }
                        }
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnRepository.GetByDateRangeAsync: Successfully retrieved {purchaseReturns.Count} purchase returns");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetByDateRangeAsync: Error - {ex.Message}");
                throw;
            }
            
            return purchaseReturns;
        }

        public async Task<List<PurchaseReturn>> GetByStatusAsync(string status)
        {
            var purchaseReturns = new List<PurchaseReturn>();
            
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetByStatusAsync: Getting purchase returns with status {status}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT pr.*, s.SupplierName, pi.InvoiceNumber as ReferencePurchaseNumber
                        FROM PurchaseReturns pr
                        LEFT JOIN Suppliers s ON pr.SupplierId = s.SupplierId
                        LEFT JOIN PurchaseInvoices pi ON pr.ReferencePurchaseId = pi.PurchaseInvoiceId
                        WHERE 1=1
                        ORDER BY pr.CreatedDate DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var purchaseReturn = new PurchaseReturn
                                {
                                    PurchaseReturnId = Convert.ToInt32(reader["PurchaseReturnId"]),
                                    ReturnNumber = reader["ReturnNo"].ToString(),
                                    Barcode = reader["Barcode"].ToString(),
                                    SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                    SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName")) ? null : reader["SupplierName"].ToString(),
                                    ReferencePurchaseId = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseId")) ? (int?)null : Convert.ToInt32(reader["ReferencePurchaseId"]),
                                    ReferencePurchaseNumber = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseNumber")) ? null : reader["ReferencePurchaseNumber"].ToString(),
                                    ReturnDate = reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                                    TaxAdjust = reader.GetDecimal(reader.GetOrdinal("TaxAdjust")),
                                    DiscountAdjust = reader.GetDecimal(reader.GetOrdinal("DiscountAdjust")),
                                    FreightAdjust = reader.GetDecimal(reader.GetOrdinal("FreightAdjust")),
                                    NetReturnAmount = reader.GetDecimal(reader.GetOrdinal("NetReturnAmount")),
                                    Reason = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : reader["Reason"].ToString(),
                                    CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                                    ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                                };
                                
                                purchaseReturns.Add(purchaseReturn);
                            }
                        }
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnRepository.GetByStatusAsync: Successfully retrieved {purchaseReturns.Count} purchase returns with status {status}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetByStatusAsync: Error - {ex.Message}");
                throw;
            }
            
            return purchaseReturns;
        }

        public async Task<string> GetNextReturnNumberAsync()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnRepository.GetNextReturnNumberAsync: Getting next return number");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    using (var command = new SqlCommand("sp_GetNextPurchaseReturnNumber", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        
                        var result = await command.ExecuteScalarAsync();
                        var returnNumber = result?.ToString();
                        
                        Debug.WriteLine($"PurchaseReturnRepository.GetNextReturnNumberAsync: Generated return number {returnNumber}");
                        return returnNumber;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.GetNextReturnNumberAsync: Error - {ex.Message}");
                // Fallback to manual generation
                var today = DateTime.Now.ToString("yyyyMMdd");
                var fallbackNumber = $"PR-{today}-00001";
                Debug.WriteLine($"PurchaseReturnRepository.GetNextReturnNumberAsync: Using fallback number {fallbackNumber}");
                return fallbackNumber;
            }
        }

        public async Task<int> CreateAsync(PurchaseReturn purchaseReturn)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.CreateAsync: Creating purchase return {purchaseReturn.ReturnNumber}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        INSERT INTO PurchaseReturns (ReturnNo, ReturnBarcode, SupplierId, ReferencePurchaseId, ReturnDate, 
                                                   TaxAdjust, DiscountAdjust, FreightAdjust, NetReturnAmount, Reason, 
                                                   CreatedBy, CreatedDate)
                        VALUES (@ReturnNumber, @Barcode, @SupplierId, @ReferencePurchaseId, @ReturnDate, 
                                @TaxAdjust, @DiscountAdjust, @FreightAdjust, @NetReturnAmount, @Reason, 
                                @CreatedBy, @CreatedDate);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReturnNumber", purchaseReturn.ReturnNumber);
                        command.Parameters.AddWithValue("@Barcode", purchaseReturn.Barcode);
                        command.Parameters.AddWithValue("@SupplierId", purchaseReturn.SupplierId);
                        command.Parameters.AddWithValue("@ReferencePurchaseId", purchaseReturn.ReferencePurchaseId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ReturnDate", purchaseReturn.ReturnDate);
                        command.Parameters.AddWithValue("@TaxAdjust", purchaseReturn.TaxAdjust);
                        command.Parameters.AddWithValue("@DiscountAdjust", purchaseReturn.DiscountAdjust);
                        command.Parameters.AddWithValue("@FreightAdjust", purchaseReturn.FreightAdjust);
                        command.Parameters.AddWithValue("@NetReturnAmount", purchaseReturn.NetReturnAmount);
                        command.Parameters.AddWithValue("@Reason", purchaseReturn.Reason ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", purchaseReturn.CreatedBy);
                        command.Parameters.AddWithValue("@CreatedDate", purchaseReturn.CreatedDate);
                        
                        var result = await command.ExecuteScalarAsync();
                        var purchaseReturnId = Convert.ToInt32(result);
                        
                        Debug.WriteLine($"PurchaseReturnRepository.CreateAsync: Successfully created purchase return with ID {purchaseReturnId}");
                        return purchaseReturnId;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.CreateAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(PurchaseReturn purchaseReturn)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.UpdateAsync: Updating purchase return {purchaseReturn.ReturnNumber}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        UPDATE PurchaseReturns 
                        SET SupplierId = @SupplierId, ReferencePurchaseId = @ReferencePurchaseId, ReturnDate = @ReturnDate,
                            TaxAdjust = @TaxAdjust, DiscountAdjust = @DiscountAdjust, FreightAdjust = @FreightAdjust,
                            NetReturnAmount = @NetReturnAmount, Reason = @Reason,
                            ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate
                        WHERE PurchaseReturnId = @PurchaseReturnId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturn.PurchaseReturnId);
                        command.Parameters.AddWithValue("@SupplierId", purchaseReturn.SupplierId);
                        command.Parameters.AddWithValue("@ReferencePurchaseId", purchaseReturn.ReferencePurchaseId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ReturnDate", purchaseReturn.ReturnDate);
                        command.Parameters.AddWithValue("@TaxAdjust", purchaseReturn.TaxAdjust);
                        command.Parameters.AddWithValue("@DiscountAdjust", purchaseReturn.DiscountAdjust);
                        command.Parameters.AddWithValue("@FreightAdjust", purchaseReturn.FreightAdjust);
                        command.Parameters.AddWithValue("@NetReturnAmount", purchaseReturn.NetReturnAmount);
                        command.Parameters.AddWithValue("@Reason", purchaseReturn.Reason ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", purchaseReturn.ModifiedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedDate", purchaseReturn.ModifiedDate ?? (object)DBNull.Value);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        var success = rowsAffected > 0;
                        
                        Debug.WriteLine($"PurchaseReturnRepository.UpdateAsync: Update {(success ? "successful" : "failed")}");
                        return success;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.UpdateAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.DeleteAsync: Deleting purchase return with ID {purchaseReturnId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "DELETE FROM PurchaseReturns WHERE PurchaseReturnId = @PurchaseReturnId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        var success = rowsAffected > 0;
                        
                        Debug.WriteLine($"PurchaseReturnRepository.DeleteAsync: Delete {(success ? "successful" : "failed")}");
                        return success;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.DeleteAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> PostPurchaseReturnAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.PostPurchaseReturnAsync: Posting purchase return with ID {purchaseReturnId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Update status to Posted
                            var updateQuery = @"
                                UPDATE PurchaseReturns 
                                SET ModifiedDate = @ModifiedDate
                                WHERE PurchaseReturnId = @PurchaseReturnId";
                            
                            using (var command = new SqlCommand(updateQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                                command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                                
                                var rowsAffected = await command.ExecuteNonQueryAsync();
                                if (rowsAffected == 0)
                                {
                                    throw new InvalidOperationException("Purchase return not found or already posted");
                                }
                            }
                            
                            // Update inventory (reduce stock)
                            var inventoryQuery = @"
                                UPDATE p 
                                SET p.StockQuantity = p.StockQuantity - pri.Quantity
                                FROM Products p
                                INNER JOIN PurchaseReturnItems pri ON p.ProductId = pri.ProductId
                                WHERE pri.PurchaseReturnId = @PurchaseReturnId";
                            
                            using (var command = new SqlCommand(inventoryQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                                await command.ExecuteNonQueryAsync();
                            }
                            
                            // Insert ledger credit entry (simplified)
                            var ledgerQuery = @"
                                INSERT INTO SupplierTransactions (SupplierId, TransactionType, Amount, Description, TransactionDate, ReferenceId, ReferenceType)
                                SELECT @SupplierId, 'Credit', @NetAmount, 'Purchase Return - ' + @ReturnNumber, @TransactionDate, @PurchaseReturnId, 'PurchaseReturn'
                                FROM PurchaseReturns 
                                WHERE PurchaseReturnId = @PurchaseReturnId";
                            
                            using (var command = new SqlCommand(ledgerQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                                command.Parameters.AddWithValue("@SupplierId", await GetSupplierIdAsync(purchaseReturnId, connection, transaction));
                                command.Parameters.AddWithValue("@NetAmount", await GetNetAmountAsync(purchaseReturnId, connection, transaction));
                                command.Parameters.AddWithValue("@ReturnNumber", await GetReturnNumberAsync(purchaseReturnId, connection, transaction));
                                command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                                
                                await command.ExecuteNonQueryAsync();
                            }
                            
                            transaction.Commit();
                            Debug.WriteLine($"PurchaseReturnRepository.PostPurchaseReturnAsync: Successfully posted purchase return with ID {purchaseReturnId}");
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.PostPurchaseReturnAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CancelPurchaseReturnAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.CancelPurchaseReturnAsync: Cancelling purchase return with ID {purchaseReturnId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        UPDATE PurchaseReturns 
                        SET ModifiedDate = @ModifiedDate
                        WHERE PurchaseReturnId = @PurchaseReturnId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                        command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        var success = rowsAffected > 0;
                        
                        Debug.WriteLine($"PurchaseReturnRepository.CancelPurchaseReturnAsync: Cancel {(success ? "successful" : "failed")}");
                        return success;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.CancelPurchaseReturnAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<decimal> CalculateNetReturnAmountAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnRepository.CalculateNetReturnAmountAsync: Calculating net amount for purchase return {purchaseReturnId}");
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    using (var command = new SqlCommand("sp_CalculatePurchaseReturnAmount", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                        
                        var result = await command.ExecuteScalarAsync();
                        var netAmount = Convert.ToDecimal(result);
                        
                        Debug.WriteLine($"PurchaseReturnRepository.CalculateNetReturnAmountAsync: Calculated net amount {netAmount}");
                        return netAmount;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnRepository.CalculateNetReturnAmountAsync: Error - {ex.Message}");
                throw;
            }
        }

        private async Task<int> GetSupplierIdAsync(int purchaseReturnId, SqlConnection connection, SqlTransaction transaction)
        {
            var query = "SELECT SupplierId FROM PurchaseReturns WHERE PurchaseReturnId = @PurchaseReturnId";
            using (var command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        private async Task<decimal> GetNetAmountAsync(int purchaseReturnId, SqlConnection connection, SqlTransaction transaction)
        {
            var query = "SELECT NetReturnAmount FROM PurchaseReturns WHERE PurchaseReturnId = @PurchaseReturnId";
            using (var command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                return Convert.ToDecimal(await command.ExecuteScalarAsync());
            }
        }

        private async Task<string> GetReturnNumberAsync(int purchaseReturnId, SqlConnection connection, SqlTransaction transaction)
        {
            var query = "SELECT ReturnNo FROM PurchaseReturns WHERE PurchaseReturnId = @PurchaseReturnId";
            using (var command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@PurchaseReturnId", purchaseReturnId);
                return await command.ExecuteScalarAsync() as string;
            }
        }
    }
}