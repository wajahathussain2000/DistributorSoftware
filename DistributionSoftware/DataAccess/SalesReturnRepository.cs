using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class SalesReturnRepository : ISalesReturnRepository
    {
        private readonly string _connectionString;

        public SalesReturnRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public List<SalesReturn> GetAll()
        {
            var salesReturns = new List<SalesReturn>();
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    SELECT sr.*, c.ContactName as CustomerName, c.Phone as CustomerPhone, c.Address as CustomerAddress,
                           si.InvoiceNumber as ReferenceInvoiceNumber
                    FROM SalesReturns sr
                    INNER JOIN Customers c ON sr.CustomerId = c.CustomerId
                    LEFT JOIN SalesInvoices si ON sr.ReferenceSalesInvoiceId = si.InvoiceId
                    ORDER BY sr.ReturnDate DESC, sr.ReturnId DESC";
                
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        salesReturns.Add(MapSalesReturn(reader));
                    }
                }
            }
            
            return salesReturns;
        }

        public SalesReturn GetById(int returnId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    SELECT sr.*, c.ContactName as CustomerName, c.Phone as CustomerPhone, c.Address as CustomerAddress
                    FROM SalesReturns sr
                    INNER JOIN Customers c ON sr.CustomerId = c.CustomerId
                    WHERE sr.ReturnId = @ReturnId";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", returnId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapSalesReturn(reader);
                        }
                    }
                }
            }
            
            return null;
        }

        public SalesReturn GetByReturnNumber(string returnNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    SELECT sr.*, c.ContactName as CustomerName, c.Phone as CustomerPhone, c.Address as CustomerAddress
                    FROM SalesReturns sr
                    INNER JOIN Customers c ON sr.CustomerId = c.CustomerId
                    WHERE sr.ReturnNumber = @ReturnNumber";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnNumber", returnNumber);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapSalesReturn(reader);
                        }
                    }
                }
            }
            
            return null;
        }

        public List<SalesReturn> GetByCustomerId(int customerId)
        {
            var salesReturns = new List<SalesReturn>();
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    SELECT sr.*, c.ContactName as CustomerName, c.Phone as CustomerPhone, c.Address as CustomerAddress
                    FROM SalesReturns sr
                    INNER JOIN Customers c ON sr.CustomerId = c.CustomerId
                    WHERE sr.CustomerId = @CustomerId
                    ORDER BY sr.ReturnDate DESC, sr.ReturnId DESC";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesReturns.Add(MapSalesReturn(reader));
                        }
                    }
                }
            }
            
            return salesReturns;
        }

        public List<SalesReturn> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var salesReturns = new List<SalesReturn>();
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    SELECT sr.*, c.ContactName as CustomerName, c.Phone as CustomerPhone, c.Address as CustomerAddress
                    FROM SalesReturns sr
                    INNER JOIN Customers c ON sr.CustomerId = c.CustomerId
                    WHERE sr.ReturnDate BETWEEN @StartDate AND @EndDate
                    ORDER BY sr.ReturnDate DESC, sr.ReturnId DESC";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate.Date);
                    command.Parameters.AddWithValue("@EndDate", endDate.Date);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salesReturns.Add(MapSalesReturn(reader));
                        }
                    }
                }
            }
            
            return salesReturns;
        }

        public int Create(SalesReturn salesReturn)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    INSERT INTO SalesReturns (
                        ReturnNumber, ReturnBarcode, CustomerId, ReferenceSalesInvoiceId,
                        ReturnDate, Reason, SubTotal, TaxAmount, DiscountAmount, TotalAmount,
                        Status, Remarks, CreatedDate, CreatedBy
                    )
                    VALUES (
                        @ReturnNumber, @ReturnBarcode, @CustomerId, @ReferenceSalesInvoiceId,
                        @ReturnDate, @Reason, @SubTotal, @TaxAmount, @DiscountAmount, @TotalAmount,
                        @Status, @Remarks, @CreatedDate, @CreatedBy
                    );
                    SELECT SCOPE_IDENTITY();";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnNumber", salesReturn.ReturnNumber);
                    command.Parameters.AddWithValue("@ReturnBarcode", (object)salesReturn.ReturnBarcode ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerId", salesReturn.CustomerId);
                    command.Parameters.AddWithValue("@ReferenceSalesInvoiceId", (object)salesReturn.ReferenceSalesInvoiceId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ReturnDate", salesReturn.ReturnDate);
                    command.Parameters.AddWithValue("@Reason", (object)salesReturn.Reason ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SubTotal", salesReturn.SubTotal);
                    command.Parameters.AddWithValue("@TaxAmount", salesReturn.TaxAmount);
                    command.Parameters.AddWithValue("@DiscountAmount", salesReturn.DiscountAmount);
                    command.Parameters.AddWithValue("@TotalAmount", salesReturn.TotalAmount);
                    command.Parameters.AddWithValue("@Status", salesReturn.Status);
                    command.Parameters.AddWithValue("@Remarks", (object)salesReturn.Remarks ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedDate", salesReturn.CreatedDate);
                    command.Parameters.AddWithValue("@CreatedBy", (object)salesReturn.CreatedBy ?? DBNull.Value);
                    
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        public bool Update(SalesReturn salesReturn)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    UPDATE SalesReturns SET
                        ReturnNumber = @ReturnNumber,
                        ReturnBarcode = @ReturnBarcode,
                        CustomerId = @CustomerId,
                        ReferenceSalesInvoiceId = @ReferenceSalesInvoiceId,
                        ReturnDate = @ReturnDate,
                        Reason = @Reason,
                        SubTotal = @SubTotal,
                        TaxAmount = @TaxAmount,
                        DiscountAmount = @DiscountAmount,
                        TotalAmount = @TotalAmount,
                        Status = @Status,
                        Remarks = @Remarks,
                        ModifiedDate = @ModifiedDate,
                        ModifiedBy = @ModifiedBy
                    WHERE ReturnId = @ReturnId";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", salesReturn.ReturnId);
                    command.Parameters.AddWithValue("@ReturnNumber", salesReturn.ReturnNumber);
                    command.Parameters.AddWithValue("@ReturnBarcode", (object)salesReturn.ReturnBarcode ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerId", salesReturn.CustomerId);
                    command.Parameters.AddWithValue("@ReferenceSalesInvoiceId", (object)salesReturn.ReferenceSalesInvoiceId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ReturnDate", salesReturn.ReturnDate);
                    command.Parameters.AddWithValue("@Reason", (object)salesReturn.Reason ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SubTotal", salesReturn.SubTotal);
                    command.Parameters.AddWithValue("@TaxAmount", salesReturn.TaxAmount);
                    command.Parameters.AddWithValue("@DiscountAmount", salesReturn.DiscountAmount);
                    command.Parameters.AddWithValue("@TotalAmount", salesReturn.TotalAmount);
                    command.Parameters.AddWithValue("@Status", salesReturn.Status);
                    command.Parameters.AddWithValue("@Remarks", (object)salesReturn.Remarks ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ModifiedDate", (object)salesReturn.ModifiedDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ModifiedBy", (object)salesReturn.ModifiedBy ?? DBNull.Value);
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int returnId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = "DELETE FROM SalesReturns WHERE ReturnId = @ReturnId";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", returnId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public string GenerateReturnNumber()
        {
            // Generate a simple return number based on current date and time
            return $"SR{DateTime.Now:yyyyMMddHHmmss}";
        }

        public List<SalesReturnItem> GetReturnItems(int returnId)
        {
            var returnItems = new List<SalesReturnItem>();
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    SELECT sri.*, p.ProductCode, p.ProductName, p.SalePrice
                    FROM SalesReturnItems sri
                    INNER JOIN Products p ON sri.ProductId = p.ProductId
                    WHERE sri.ReturnId = @ReturnId
                    ORDER BY sri.ReturnItemId";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", returnId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnItems.Add(MapSalesReturnItem(reader));
                        }
                    }
                }
            }
            
            return returnItems;
        }

        public bool CreateReturnItem(SalesReturnItem returnItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    INSERT INTO SalesReturnItems (
                        ReturnId, ProductId, OriginalInvoiceItemId, Quantity, UnitPrice, TaxPercentage, TaxAmount,
                        DiscountPercentage, DiscountAmount, TotalAmount, Remarks, StockUpdated
                    )
                    VALUES (
                        @ReturnId, @ProductId, @OriginalInvoiceItemId, @Quantity, @UnitPrice, @TaxPercentage, @TaxAmount,
                        @DiscountPercentage, @DiscountAmount, @TotalAmount, @Remarks, @StockUpdated
                    )";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", returnItem.ReturnId);
                    command.Parameters.AddWithValue("@ProductId", returnItem.ProductId);
                    command.Parameters.AddWithValue("@OriginalInvoiceItemId", (object)returnItem.OriginalInvoiceItemId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Quantity", returnItem.Quantity);
                    command.Parameters.AddWithValue("@UnitPrice", returnItem.UnitPrice);
                    command.Parameters.AddWithValue("@TaxPercentage", (object)returnItem.TaxPercentage ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TaxAmount", returnItem.TaxAmount);
                    command.Parameters.AddWithValue("@DiscountPercentage", (object)returnItem.DiscountPercentage ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DiscountAmount", returnItem.DiscountAmount);
                    command.Parameters.AddWithValue("@TotalAmount", returnItem.TotalAmount);
                    command.Parameters.AddWithValue("@Remarks", (object)returnItem.Remarks ?? DBNull.Value);
                    command.Parameters.AddWithValue("@StockUpdated", returnItem.StockUpdated);
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateReturnItem(SalesReturnItem returnItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = @"
                    UPDATE SalesReturnItems SET
                        Quantity = @Quantity,
                        UnitPrice = @UnitPrice,
                        TaxPercentage = @TaxPercentage,
                        TaxAmount = @TaxAmount,
                        DiscountPercentage = @DiscountPercentage,
                        DiscountAmount = @DiscountAmount,
                        TotalAmount = @TotalAmount,
                        Remarks = @Remarks
                    WHERE ReturnItemId = @ReturnItemId";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnItemId", returnItem.ReturnItemId);
                    command.Parameters.AddWithValue("@Quantity", returnItem.Quantity);
                    command.Parameters.AddWithValue("@UnitPrice", returnItem.UnitPrice);
                    command.Parameters.AddWithValue("@TaxPercentage", (object)returnItem.TaxPercentage ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TaxAmount", returnItem.TaxAmount);
                    command.Parameters.AddWithValue("@DiscountPercentage", (object)returnItem.DiscountPercentage ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DiscountAmount", returnItem.DiscountAmount);
                    command.Parameters.AddWithValue("@TotalAmount", returnItem.TotalAmount);
                    command.Parameters.AddWithValue("@Remarks", (object)returnItem.Remarks ?? DBNull.Value);
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteReturnItem(int returnItemId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = "DELETE FROM SalesReturnItems WHERE ReturnItemId = @ReturnItemId";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnItemId", returnItemId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteReturnItemsByReturnId(int returnId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                var query = "DELETE FROM SalesReturnItems WHERE ReturnId = @ReturnId";
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", returnId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateStockForSalesReturn(int returnId, int updatedByUserId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update stock for each returned item
                        var updateStockQuery = @"
                            UPDATE p
                            SET p.Quantity = p.Quantity + sri.Quantity,
                                p.ModifiedDate = GETDATE(),
                                p.ModifiedBy = @UpdatedBy
                            FROM Products p
                            INNER JOIN SalesReturnItems sri ON p.ProductId = sri.ProductId
                            WHERE sri.ReturnId = @ReturnId AND sri.StockUpdated = 0";
                        
                        using (var command = new SqlCommand(updateStockQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ReturnId", returnId);
                            command.Parameters.AddWithValue("@UpdatedBy", updatedByUserId);
                            command.ExecuteNonQuery();
                        }
                        
                        // Mark items as stock updated
                        var markUpdatedQuery = @"
                            UPDATE SalesReturnItems
                            SET StockUpdated = 1,
                                StockUpdatedDate = GETDATE(),
                                StockUpdatedBy = @UpdatedBy
                            WHERE ReturnId = @ReturnId AND StockUpdated = 0";
                        
                        using (var command = new SqlCommand(markUpdatedQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ReturnId", returnId);
                            command.Parameters.AddWithValue("@UpdatedBy", updatedByUserId);
                            command.ExecuteNonQuery();
                        }
                        
                        // Update return status
                        var updateStatusQuery = @"
                            UPDATE SalesReturns
                            SET Status = 'PROCESSED',
                                ProcessedDate = GETDATE(),
                                ProcessedBy = @UpdatedBy
                            WHERE ReturnId = @ReturnId";
                        
                        using (var command = new SqlCommand(updateStatusQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@ReturnId", returnId);
                            command.Parameters.AddWithValue("@UpdatedBy", updatedByUserId);
                            command.ExecuteNonQuery();
                        }
                        
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        private SalesReturn MapSalesReturn(SqlDataReader reader)
        {
            return new SalesReturn
            {
                ReturnId = Convert.ToInt32(reader["ReturnId"]),
                ReturnNumber = reader["ReturnNumber"].ToString(),
                ReturnBarcode = reader["ReturnBarcode"]?.ToString(),
                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                ReferenceSalesInvoiceId = reader["ReferenceSalesInvoiceId"] as int?,
                ReturnDate = Convert.ToDateTime(reader["ReturnDate"]),
                Reason = reader["Reason"]?.ToString(),
                SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                Status = reader["Status"].ToString(),
                Remarks = reader["Remarks"]?.ToString(),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] as int?,
                ModifiedDate = reader["ModifiedDate"] as DateTime?,
                ModifiedBy = reader["ModifiedBy"] as int?,
                Customer = new Customer
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    ContactName = reader["CustomerName"].ToString(),
                    Phone = reader["CustomerPhone"]?.ToString(),
                    Address = reader["CustomerAddress"]?.ToString()
                }
            };
        }

        private SalesReturnItem MapSalesReturnItem(SqlDataReader reader)
        {
            return new SalesReturnItem
            {
                ReturnItemId = Convert.ToInt32(reader["ReturnItemId"]),
                ReturnId = Convert.ToInt32(reader["ReturnId"]),
                ProductId = Convert.ToInt32(reader["ProductId"]),
                Quantity = Convert.ToDecimal(reader["Quantity"]),
                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                TaxPercentage = reader["TaxPercentage"] as decimal?,
                TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                DiscountPercentage = reader["DiscountPercentage"] as decimal?,
                DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                Remarks = reader["Remarks"]?.ToString(),
                Product = new Product
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductCode = reader["ProductCode"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    SalePrice = Convert.ToDecimal(reader["SalePrice"])
                }
            };
        }
    }
}