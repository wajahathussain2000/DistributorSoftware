using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? ConfigurationManager.DistributionConnectionString;
        }

        #region CRUD Operations

        public int CreateProduct(Product product)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"INSERT INTO Products (ProductCode, ProductName, ProductDescription, CategoryId, BrandId, UnitId, 
                               PurchasePrice, SalePrice, UnitPrice, MRP, Quantity, StockQuantity, ReservedQuantity, ReorderLevel, 
                               Barcode, BatchNumber, ExpiryDate, WarehouseId, IsActive, Remarks, CreatedBy, CreatedDate)
                               VALUES (@ProductCode, @ProductName, @ProductDescription, @CategoryId, @BrandId, @UnitId, 
                               @PurchasePrice, @SalePrice, @UnitPrice, @MRP, @Quantity, @StockQuantity, @ReservedQuantity, @ReorderLevel, 
                               @Barcode, @BatchNumber, @ExpiryDate, @WarehouseId, @IsActive, @Remarks, @CreatedBy, @CreatedDate);
                               SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductCode", product.ProductCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductName", product.ProductName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                        command.Parameters.AddWithValue("@BrandId", product.BrandId);
                        command.Parameters.AddWithValue("@UnitId", product.UnitId);
                        command.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                        command.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                        command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                        command.Parameters.AddWithValue("@MRP", product.MRP);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        command.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                        command.Parameters.AddWithValue("@ReservedQuantity", product.ReservedQuantity);
                        command.Parameters.AddWithValue("@ReorderLevel", product.ReorderLevel);
                        command.Parameters.AddWithValue("@Barcode", product.Barcode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BatchNumber", product.BatchNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExpiryDate", product.ExpiryDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@WarehouseId", product.WarehouseId);
                        command.Parameters.AddWithValue("@IsActive", product.IsActive);
                        command.Parameters.AddWithValue("@Remarks", product.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", product.CreatedBy);
                        command.Parameters.AddWithValue("@CreatedDate", product.CreatedDate);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.CreateProduct", ex);
                throw;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"UPDATE Products SET 
                               ProductCode = @ProductCode, ProductName = @ProductName, ProductDescription = @ProductDescription, 
                               CategoryId = @CategoryId, BrandId = @BrandId, UnitId = @UnitId, 
                               PurchasePrice = @PurchasePrice, SalePrice = @SalePrice, UnitPrice = @UnitPrice, MRP = @MRP, 
                               Quantity = @Quantity, StockQuantity = @StockQuantity, ReservedQuantity = @ReservedQuantity, 
                               ReorderLevel = @ReorderLevel, Barcode = @Barcode, BatchNumber = @BatchNumber, 
                               ExpiryDate = @ExpiryDate, WarehouseId = @WarehouseId, IsActive = @IsActive, 
                               Remarks = @Remarks, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate
                               WHERE ProductId = @ProductId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", product.ProductId);
                        command.Parameters.AddWithValue("@ProductCode", product.ProductCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductName", product.ProductName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                        command.Parameters.AddWithValue("@BrandId", product.BrandId);
                        command.Parameters.AddWithValue("@UnitId", product.UnitId);
                        command.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                        command.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                        command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                        command.Parameters.AddWithValue("@MRP", product.MRP);
                        command.Parameters.AddWithValue("@Quantity", product.Quantity);
                        command.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                        command.Parameters.AddWithValue("@ReservedQuantity", product.ReservedQuantity);
                        command.Parameters.AddWithValue("@ReorderLevel", product.ReorderLevel);
                        command.Parameters.AddWithValue("@Barcode", product.Barcode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BatchNumber", product.BatchNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExpiryDate", product.ExpiryDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@WarehouseId", product.WarehouseId);
                        command.Parameters.AddWithValue("@IsActive", product.IsActive);
                        command.Parameters.AddWithValue("@Remarks", product.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", product.ModifiedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedDate", product.ModifiedDate ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.UpdateProduct", ex);
                throw;
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "DELETE FROM Products WHERE ProductId = @ProductId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.DeleteProduct", ex);
                throw;
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM Products WHERE ProductId = @ProductId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapProduct(reader);
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetProductById", ex);
                throw;
            }
        }

        public Product GetById(int productId)
        {
            return GetProductById(productId);
        }

        public Product GetProductByCode(string productCode)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM Products WHERE ProductCode = @ProductCode";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductCode", productCode);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapProduct(reader);
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetProductByCode", ex);
                throw;
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM Products ORDER BY ProductName";

                    var products = new List<Product>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(MapProduct(reader));
                            }
                        }
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetAllProducts", ex);
                throw;
            }
        }

        public List<Product> GetActiveProducts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM Products WHERE IsActive = 1 ORDER BY ProductName";

                    var products = new List<Product>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(MapProduct(reader));
                            }
                        }
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetActiveProducts", ex);
                throw;
            }
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM Products WHERE CategoryId = @CategoryId ORDER BY ProductName";

                    var products = new List<Product>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", categoryId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(MapProduct(reader));
                            }
                        }
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetProductsByCategory", ex);
                throw;
            }
        }

        public List<Product> GetProductsByBrand(int brandId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM Products WHERE BrandId = @BrandId ORDER BY ProductName";

                    var products = new List<Product>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@BrandId", brandId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(MapProduct(reader));
                            }
                        }
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetProductsByBrand", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic

        public bool UpdateStockQuantity(int productId, decimal quantity, string operation)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql;
                    
                    switch (operation.ToUpper())
                    {
                        case "ADD":
                        case "IN":
                            sql = "UPDATE Products SET StockQuantity = StockQuantity + @Quantity WHERE ProductId = @ProductId";
                            break;
                        case "SUBTRACT":
                        case "OUT":
                            sql = "UPDATE Products SET StockQuantity = StockQuantity - @Quantity WHERE ProductId = @ProductId";
                            break;
                        case "SET":
                            sql = "UPDATE Products SET StockQuantity = @Quantity WHERE ProductId = @ProductId";
                            break;
                        default:
                            return false;
                    }

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.UpdateStockQuantity", ex);
                throw;
            }
        }

        #endregion

        #region Reports

        public List<Product> GetProductReport(DateTime? startDate, DateTime? endDate, int? categoryId, int? brandId, bool? isActive)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM Products WHERE 1=1";
                    var parameters = new List<SqlParameter>();

                    if (startDate.HasValue)
                    {
                        sql += " AND CreatedDate >= @StartDate";
                        parameters.Add(new SqlParameter("@StartDate", startDate.Value));
                    }

                    if (endDate.HasValue)
                    {
                        sql += " AND CreatedDate <= @EndDate";
                        parameters.Add(new SqlParameter("@EndDate", endDate.Value));
                    }

                    if (categoryId.HasValue)
                    {
                        sql += " AND CategoryId = @CategoryId";
                        parameters.Add(new SqlParameter("@CategoryId", categoryId.Value));
                    }

                    if (brandId.HasValue)
                    {
                        sql += " AND BrandId = @BrandId";
                        parameters.Add(new SqlParameter("@BrandId", brandId.Value));
                    }

                    if (isActive.HasValue)
                    {
                        sql += " AND IsActive = @IsActive";
                        parameters.Add(new SqlParameter("@IsActive", isActive.Value));
                    }

                    sql += " ORDER BY ProductName";

                    var products = new List<Product>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(param);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(MapProduct(reader));
                            }
                        }
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetProductReport", ex);
                throw;
            }
        }

        public int GetProductCount(bool? isActive)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT COUNT(*) FROM Products";
                    
                    if (isActive.HasValue)
                    {
                        sql += " WHERE IsActive = @IsActive";
                    }

                    using (var command = new SqlCommand(sql, connection))
                    {
                        if (isActive.HasValue)
                        {
                            command.Parameters.AddWithValue("@IsActive", isActive.Value);
                        }
                        
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetProductCount", ex);
                throw;
            }
        }

        public decimal GetTotalStockValue()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT SUM(StockQuantity * UnitPrice) FROM Products WHERE IsActive = 1";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        var result = command.ExecuteScalar();
                        return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetTotalStockValue", ex);
                throw;
            }
        }

        public List<Product> GetLowStockProducts(decimal threshold)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM Products WHERE IsActive = 1 AND StockQuantity <= @Threshold ORDER BY StockQuantity ASC";

                    var products = new List<Product>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Threshold", threshold);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(MapProduct(reader));
                            }
                        }
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetLowStockProducts", ex);
                throw;
            }
        }

        public List<LowStockReportData> GetLowStockReportData(int? productId, int? categoryId, DateTime? reportDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"
                        SELECT 
                            p.ProductId,
                            p.ProductCode,
                            p.ProductName,
                            ISNULL(c.CategoryName, ISNULL(p.Category, 'N/A')) AS CategoryName,
                            ISNULL(b.BrandName, 'N/A') AS BrandName,
                            ISNULL(u.UnitName, 'N/A') AS UnitName,
                            p.UnitPrice,
                            p.StockQuantity,
                            ISNULL(w.WarehouseName, 'Main Warehouse') AS WarehouseName,
                            p.ReorderLevel,
                            ISNULL(p.BatchNumber, 'N/A') AS BatchNumber,
                            p.ExpiryDate,
                            CASE 
                                WHEN p.StockQuantity <= 0 THEN 'Out of Stock'
                                WHEN p.StockQuantity <= p.ReorderLevel THEN 'Low Stock'
                                ELSE 'Normal'
                            END AS StockStatus,
                            CASE 
                                WHEN p.StockQuantity <= 0 THEN 'Critical'
                                WHEN p.StockQuantity <= p.ReorderLevel THEN 'Warning'
                                ELSE 'Normal'
                            END AS Priority,
                            p.ModifiedDate AS LastUpdated
                        FROM Products p 
                        LEFT JOIN Categories c ON p.CategoryId = c.CategoryId AND c.IsActive = 1 
                        LEFT JOIN Brands b ON p.BrandId = b.BrandId AND b.IsActive = 1 
                        LEFT JOIN Units u ON p.UnitId = u.UnitId AND u.IsActive = 1 
                        LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId AND w.IsActive = 1 
                        WHERE p.IsActive = 1 
                        AND p.StockQuantity <= p.ReorderLevel";

                    var conditions = new List<string>();
                    var parameters = new List<SqlParameter>();

                    if (productId.HasValue)
                    {
                        conditions.Add("p.ProductId = @ProductId");
                        parameters.Add(new SqlParameter("@ProductId", productId.Value));
                    }

                    if (categoryId.HasValue)
                    {
                        conditions.Add("p.CategoryId = @CategoryId");
                        parameters.Add(new SqlParameter("@CategoryId", categoryId.Value));
                    }

                    if (conditions.Count > 0)
                    {
                        sql += " AND " + string.Join(" AND ", conditions);
                    }

                    sql += " ORDER BY p.StockQuantity ASC, p.ProductName";

                    var reportData = new List<LowStockReportData>();
                    using (var command = new SqlCommand(sql, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.Add(param);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reportData.Add(new LowStockReportData
                                {
                                    ProductId = reader["ProductId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProductId"]),
                                    ProductCode = reader["ProductCode"] == DBNull.Value ? "" : reader["ProductCode"].ToString(),
                                    ProductName = reader["ProductName"] == DBNull.Value ? "" : reader["ProductName"].ToString(),
                                    CategoryName = reader["CategoryName"] == DBNull.Value ? "" : reader["CategoryName"].ToString(),
                                    BrandName = reader["BrandName"] == DBNull.Value ? "" : reader["BrandName"].ToString(),
                                    UnitName = reader["UnitName"] == DBNull.Value ? "" : reader["UnitName"].ToString(),
                                    UnitPrice = reader["UnitPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["UnitPrice"]),
                                    StockQuantity = reader["StockQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["StockQuantity"]),
                                    WarehouseName = reader["WarehouseName"] == DBNull.Value ? "" : reader["WarehouseName"].ToString(),
                                    ReorderLevel = reader["ReorderLevel"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ReorderLevel"]),
                                    BatchNumber = reader["BatchNumber"] == DBNull.Value ? "" : reader["BatchNumber"].ToString(),
                                    ExpiryDate = reader["ExpiryDate"] == DBNull.Value ? null : (DateTime?)reader["ExpiryDate"],
                                    StockStatus = reader["StockStatus"] == DBNull.Value ? "" : reader["StockStatus"].ToString(),
                                    Priority = reader["Priority"] == DBNull.Value ? "" : reader["Priority"].ToString(),
                                    LastUpdated = reader["LastUpdated"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["LastUpdated"])
                                });
                            }
                        }
                    }
                    return reportData;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductRepository.GetLowStockReportData", ex);
                throw;
            }
        }

        #endregion

        #region Helper Methods

        private Product MapProduct(SqlDataReader reader)
        {
            return new Product
            {
                ProductId = reader["ProductId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ProductId"]),
                ProductCode = reader["ProductCode"] == DBNull.Value ? null : reader["ProductCode"].ToString(),
                ProductName = reader["ProductName"] == DBNull.Value ? null : reader["ProductName"].ToString(),
                ProductDescription = reader["ProductDescription"] == DBNull.Value ? null : reader["ProductDescription"].ToString(),
                CategoryId = reader["CategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CategoryId"]),
                BrandId = reader["BrandId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["BrandId"]),
                UnitId = reader["UnitId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UnitId"]),
                PurchasePrice = reader["PurchasePrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PurchasePrice"]),
                SalePrice = reader["SalePrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SalePrice"]),
                UnitPrice = reader["UnitPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["UnitPrice"]),
                MRP = reader["MRP"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["MRP"]),
                Quantity = reader["Quantity"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Quantity"]),
                StockQuantity = reader["StockQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["StockQuantity"]),
                ReservedQuantity = reader["ReservedQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ReservedQuantity"]),
                ReorderLevel = reader["ReorderLevel"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["ReorderLevel"]),
                Barcode = reader["Barcode"] == DBNull.Value ? null : reader["Barcode"].ToString(),
                BatchNumber = reader["BatchNumber"] == DBNull.Value ? null : reader["BatchNumber"].ToString(),
                ExpiryDate = reader["ExpiryDate"] == DBNull.Value ? null : (DateTime?)reader["ExpiryDate"],
                WarehouseId = reader["WarehouseId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["WarehouseId"]),
                IsActive = reader["IsActive"] == DBNull.Value ? true : Convert.ToBoolean(reader["IsActive"]),
                Remarks = reader["Remarks"] == DBNull.Value ? null : reader["Remarks"].ToString(),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"]),
                CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedDate"]),
                ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? null : (int?)reader["ModifiedBy"],
                ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? null : (DateTime?)reader["ModifiedDate"]
            };
        }

        #endregion
    }
}