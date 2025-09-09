using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly string _connectionString;

        public StockMovementRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public List<StockMovement> GetStockMovements(DateTime? fromDate, DateTime? toDate, int? productId, int? warehouseId, string movementType, string referenceType)
        {
            var movements = new List<StockMovement>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // First check if StockMovement table has any data
                    var checkQuery = "SELECT COUNT(*) FROM StockMovement";
                    using (var checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        connection.Open();
                        var count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        
                        if (count == 0)
                        {
                            // If no data in StockMovement table, return empty list
                            return movements;
                        }
                    }
                    connection.Close();

                    var query = @"
                        SELECT 
                            sm.MovementId,
                            sm.ProductId,
                            ISNULL(p.ProductName, 'Unknown Product') AS ProductName,
                            ISNULL(p.ProductCode, 'N/A') AS ProductCode,
                            sm.WarehouseId,
                            ISNULL(w.WarehouseName, 'Unknown Warehouse') AS WarehouseName,
                            sm.MovementType,
                            sm.Quantity,
                            sm.ReferenceType,
                            sm.ReferenceId,
                            sm.BatchNumber,
                            sm.ExpiryDate,
                            sm.MovementDate,
                            sm.CreatedBy,
                            ISNULL(u.FirstName + ' ' + u.LastName, 'System') AS CreatedByUser,
                            sm.Remarks,
                            CASE 
                                WHEN sm.ReferenceType = 'PURCHASE' THEN ISNULL(pid.UnitPrice, 0)
                                WHEN sm.ReferenceType = 'SALES' THEN ISNULL(sid.UnitPrice, 0)
                                ELSE 0
                            END AS UnitPrice,
                            CASE 
                                WHEN sm.ReferenceType = 'PURCHASE' THEN ISNULL(pid.TotalAmount, 0)
                                WHEN sm.ReferenceType = 'SALES' THEN ISNULL(sid.TotalAmount, 0)
                                ELSE 0
                            END AS TotalValue,
                            CASE 
                                WHEN sm.ReferenceType = 'PURCHASE' THEN s.SupplierName
                                ELSE NULL
                            END AS SupplierName,
                            CASE 
                                WHEN sm.ReferenceType = 'SALES' THEN c.ContactName
                                ELSE NULL
                            END AS CustomerName,
                            CASE 
                                WHEN sm.ReferenceType = 'PURCHASE' THEN pi.InvoiceNumber
                                WHEN sm.ReferenceType = 'SALES' THEN si.InvoiceNumber
                                WHEN sm.ReferenceType = 'RETURN' THEN pr.ReturnNo
                                ELSE NULL
                            END AS ReferenceNumber
                        FROM StockMovement sm
                        LEFT JOIN Products p ON sm.ProductId = p.ProductId
                        LEFT JOIN Warehouses w ON sm.WarehouseId = w.WarehouseId
                        LEFT JOIN Users u ON sm.CreatedBy = u.UserId
                        LEFT JOIN PurchaseInvoiceDetails pid ON sm.ReferenceType = 'PURCHASE' AND sm.ReferenceId = pid.DetailId
                        LEFT JOIN PurchaseInvoices pi ON pid.PurchaseInvoiceId = pi.PurchaseInvoiceId
                        LEFT JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                        LEFT JOIN SalesInvoiceDetails sid ON sm.ReferenceType = 'SALES' AND sm.ReferenceId = sid.DetailId
                        LEFT JOIN SalesInvoices si ON sid.SalesInvoiceId = si.SalesInvoiceId
                        LEFT JOIN Customers c ON si.CustomerId = c.CustomerId
                        LEFT JOIN PurchaseReturnItems pri ON sm.ReferenceType = 'RETURN' AND sm.ReferenceId = pri.ReturnItemId
                        LEFT JOIN PurchaseReturns pr ON pri.ReturnId = pr.ReturnId
                        WHERE 1=1";

                    var parameters = new List<SqlParameter>();

                    if (fromDate.HasValue)
                    {
                        query += " AND sm.MovementDate >= @FromDate";
                        parameters.Add(new SqlParameter("@FromDate", fromDate.Value));
                    }

                    if (toDate.HasValue)
                    {
                        query += " AND sm.MovementDate <= @ToDate";
                        parameters.Add(new SqlParameter("@ToDate", toDate.Value));
                    }

                    if (productId.HasValue)
                    {
                        query += " AND sm.ProductId = @ProductId";
                        parameters.Add(new SqlParameter("@ProductId", productId.Value));
                    }

                    if (warehouseId.HasValue)
                    {
                        query += " AND sm.WarehouseId = @WarehouseId";
                        parameters.Add(new SqlParameter("@WarehouseId", warehouseId.Value));
                    }

                    if (!string.IsNullOrEmpty(movementType))
                    {
                        query += " AND sm.MovementType = @MovementType";
                        parameters.Add(new SqlParameter("@MovementType", movementType));
                    }

                    if (!string.IsNullOrEmpty(referenceType))
                    {
                        query += " AND sm.ReferenceType = @ReferenceType";
                        parameters.Add(new SqlParameter("@ReferenceType", referenceType));
                    }

                    query += " ORDER BY sm.MovementDate DESC, sm.MovementId DESC";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                        connection.Open();
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                movements.Add(MapToStockMovement(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving stock movements: {ex.Message}", ex);
            }

            return movements;
        }

        public List<StockMovement> GetStockMovementsByProduct(int productId, DateTime? fromDate, DateTime? toDate)
        {
            return GetStockMovements(fromDate, toDate, productId, null, null, null);
        }

        public List<StockMovement> GetStockMovementsByWarehouse(int warehouseId, DateTime? fromDate, DateTime? toDate)
        {
            return GetStockMovements(fromDate, toDate, null, warehouseId, null, null);
        }

        public List<StockMovement> GetStockMovementsByReference(string referenceType, int referenceId)
        {
            var movements = new List<StockMovement>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT 
                            sm.MovementId,
                            sm.ProductId,
                            p.ProductName,
                            p.ProductCode,
                            sm.WarehouseId,
                            w.WarehouseName,
                            sm.MovementType,
                            sm.Quantity,
                            sm.ReferenceType,
                            sm.ReferenceId,
                            sm.BatchNumber,
                            sm.ExpiryDate,
                            sm.MovementDate,
                            sm.CreatedBy,
                            u.FirstName + ' ' + u.LastName AS CreatedByUser,
                            sm.Remarks
                        FROM StockMovement sm
                        INNER JOIN Products p ON sm.ProductId = p.ProductId
                        INNER JOIN Warehouses w ON sm.WarehouseId = w.WarehouseId
                        LEFT JOIN Users u ON sm.CreatedBy = u.UserId
                        WHERE sm.ReferenceType = @ReferenceType AND sm.ReferenceId = @ReferenceId
                        ORDER BY sm.MovementDate DESC";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@ReferenceType", referenceType));
                        command.Parameters.Add(new SqlParameter("@ReferenceId", referenceId));
                        connection.Open();
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                movements.Add(MapToStockMovement(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving stock movements by reference: {ex.Message}", ex);
            }

            return movements;
        }

        public StockMovement GetStockMovementById(int movementId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT 
                            sm.MovementId,
                            sm.ProductId,
                            p.ProductName,
                            p.ProductCode,
                            sm.WarehouseId,
                            w.WarehouseName,
                            sm.MovementType,
                            sm.Quantity,
                            sm.ReferenceType,
                            sm.ReferenceId,
                            sm.BatchNumber,
                            sm.ExpiryDate,
                            sm.MovementDate,
                            sm.CreatedBy,
                            u.FirstName + ' ' + u.LastName AS CreatedByUser,
                            sm.Remarks
                        FROM StockMovement sm
                        INNER JOIN Products p ON sm.ProductId = p.ProductId
                        INNER JOIN Warehouses w ON sm.WarehouseId = w.WarehouseId
                        LEFT JOIN Users u ON sm.CreatedBy = u.UserId
                        WHERE sm.MovementId = @MovementId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@MovementId", movementId));
                        connection.Open();
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapToStockMovement(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving stock movement: {ex.Message}", ex);
            }

            return null;
        }

        public bool AddStockMovement(StockMovement movement)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                        INSERT INTO StockMovement 
                        (ProductId, WarehouseId, MovementType, Quantity, ReferenceType, ReferenceId, 
                         BatchNumber, ExpiryDate, MovementDate, CreatedBy, Remarks)
                        VALUES 
                        (@ProductId, @WarehouseId, @MovementType, @Quantity, @ReferenceType, @ReferenceId,
                         @BatchNumber, @ExpiryDate, @MovementDate, @CreatedBy, @Remarks)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@ProductId", movement.ProductId));
                        command.Parameters.Add(new SqlParameter("@WarehouseId", movement.WarehouseId));
                        command.Parameters.Add(new SqlParameter("@MovementType", movement.MovementType));
                        command.Parameters.Add(new SqlParameter("@Quantity", movement.Quantity));
                        command.Parameters.Add(new SqlParameter("@ReferenceType", movement.ReferenceType ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@ReferenceId", movement.ReferenceId ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@BatchNumber", movement.BatchNumber ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@ExpiryDate", movement.ExpiryDate ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@MovementDate", movement.MovementDate));
                        command.Parameters.Add(new SqlParameter("@CreatedBy", movement.CreatedBy ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@Remarks", movement.Remarks ?? (object)DBNull.Value));

                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding stock movement: {ex.Message}", ex);
            }
        }

        public bool UpdateStockMovement(StockMovement movement)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                        UPDATE StockMovement 
                        SET ProductId = @ProductId, 
                            WarehouseId = @WarehouseId, 
                            MovementType = @MovementType, 
                            Quantity = @Quantity, 
                            ReferenceType = @ReferenceType, 
                            ReferenceId = @ReferenceId,
                            BatchNumber = @BatchNumber, 
                            ExpiryDate = @ExpiryDate, 
                            MovementDate = @MovementDate, 
                            Remarks = @Remarks
                        WHERE MovementId = @MovementId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@MovementId", movement.MovementId));
                        command.Parameters.Add(new SqlParameter("@ProductId", movement.ProductId));
                        command.Parameters.Add(new SqlParameter("@WarehouseId", movement.WarehouseId));
                        command.Parameters.Add(new SqlParameter("@MovementType", movement.MovementType));
                        command.Parameters.Add(new SqlParameter("@Quantity", movement.Quantity));
                        command.Parameters.Add(new SqlParameter("@ReferenceType", movement.ReferenceType ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@ReferenceId", movement.ReferenceId ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@BatchNumber", movement.BatchNumber ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@ExpiryDate", movement.ExpiryDate ?? (object)DBNull.Value));
                        command.Parameters.Add(new SqlParameter("@MovementDate", movement.MovementDate));
                        command.Parameters.Add(new SqlParameter("@Remarks", movement.Remarks ?? (object)DBNull.Value));

                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating stock movement: {ex.Message}", ex);
            }
        }

        public bool DeleteStockMovement(int movementId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "DELETE FROM StockMovement WHERE MovementId = @MovementId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@MovementId", movementId));
                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting stock movement: {ex.Message}", ex);
            }
        }

        public List<string> GetMovementTypes()
        {
            return new List<string> { "IN", "OUT", "ADJUSTMENT" };
        }

        public List<string> GetReferenceTypes()
        {
            return new List<string> { "PURCHASE", "SALES", "RETURN", "ADJUSTMENT", "TRANSFER" };
        }

        public bool CreateSampleData()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    // Check if we have any products and warehouses first
                    var checkQuery = @"
                        SELECT COUNT(*) FROM Products WHERE IsActive = 1;
                        SELECT COUNT(*) FROM Warehouses WHERE IsActive = 1;";
                    
                    connection.Open();
                    using (var command = new SqlCommand(checkQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        var productCount = 0;
                        var warehouseCount = 0;
                        
                        if (reader.Read())
                        {
                            productCount = Convert.ToInt32(reader[0]);
                        }
                        
                        if (reader.NextResult() && reader.Read())
                        {
                            warehouseCount = Convert.ToInt32(reader[0]);
                        }
                        
                        if (productCount == 0 || warehouseCount == 0)
                        {
                            return false; // No products or warehouses to create sample data
                        }
                    }

                    // Get first product and warehouse
                    var getDataQuery = @"
                        SELECT TOP 1 ProductId FROM Products WHERE IsActive = 1;
                        SELECT TOP 1 WarehouseId FROM Warehouses WHERE IsActive = 1;";
                    
                    int productId = 0, warehouseId = 0;
                    using (var command = new SqlCommand(getDataQuery, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            productId = Convert.ToInt32(reader[0]);
                        }
                        
                        if (reader.NextResult() && reader.Read())
                        {
                            warehouseId = Convert.ToInt32(reader[0]);
                        }
                    }

                    // Insert sample stock movements
                    var insertQuery = @"
                        INSERT INTO StockMovement (ProductId, WarehouseId, MovementType, Quantity, ReferenceType, MovementDate, CreatedBy, Remarks)
                        VALUES 
                        (@ProductId, @WarehouseId, 'IN', 100, 'ADJUSTMENT', @Date1, 1, 'Initial stock adjustment'),
                        (@ProductId, @WarehouseId, 'OUT', 25, 'ADJUSTMENT', @Date2, 1, 'Stock adjustment'),
                        (@ProductId, @WarehouseId, 'IN', 50, 'ADJUSTMENT', @Date3, 1, 'Stock replenishment')";

                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@ProductId", productId));
                        command.Parameters.Add(new SqlParameter("@WarehouseId", warehouseId));
                        command.Parameters.Add(new SqlParameter("@Date1", DateTime.Now.AddDays(-5)));
                        command.Parameters.Add(new SqlParameter("@Date2", DateTime.Now.AddDays(-3)));
                        command.Parameters.Add(new SqlParameter("@Date3", DateTime.Now.AddDays(-1)));
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating sample data: {ex.Message}", ex);
            }
        }

        private StockMovement MapToStockMovement(IDataReader reader)
        {
            return new StockMovement
            {
                MovementId = Convert.ToInt32(reader["MovementId"]),
                ProductId = Convert.ToInt32(reader["ProductId"]),
                ProductName = reader["ProductName"].ToString(),
                ProductCode = reader["ProductCode"].ToString(),
                WarehouseId = Convert.ToInt32(reader["WarehouseId"]),
                WarehouseName = reader["WarehouseName"].ToString(),
                MovementType = reader["MovementType"].ToString(),
                Quantity = Convert.ToDecimal(reader["Quantity"]),
                ReferenceType = reader["ReferenceType"]?.ToString(),
                ReferenceId = reader["ReferenceId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ReferenceId"]),
                ReferenceNumber = reader["ReferenceNumber"]?.ToString(),
                BatchNumber = reader["BatchNumber"]?.ToString(),
                ExpiryDate = reader["ExpiryDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ExpiryDate"]),
                MovementDate = Convert.ToDateTime(reader["MovementDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CreatedBy"]),
                CreatedByUser = reader["CreatedByUser"]?.ToString(),
                Remarks = reader["Remarks"]?.ToString(),
                UnitPrice = reader["UnitPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["UnitPrice"]),
                TotalValue = reader["TotalValue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalValue"]),
                SupplierName = reader["SupplierName"]?.ToString(),
                CustomerName = reader["CustomerName"]?.ToString()
            };
        }
    }
}
