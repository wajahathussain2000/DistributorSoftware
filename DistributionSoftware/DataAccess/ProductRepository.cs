using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        p.ProductId, p.ProductCode, p.ProductName, p.ProductDescription,
                        p.CategoryId, c.CategoryName,
                        p.BrandId, b.BrandName,
                        p.UnitId, u.UnitName,
                        p.PurchasePrice, p.SalePrice, p.MRP,
                        p.StockQuantity as Quantity, p.ReservedQuantity, p.ReorderLevel,
                        p.Barcode, p.BatchNumber, p.ExpiryDate,
                        p.WarehouseId, w.WarehouseName,
                        p.IsActive, p.Remarks,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    LEFT JOIN Units u ON p.UnitId = u.UnitId
                    LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId
                    ORDER BY p.ProductName";

                using (var command = new SqlCommand(query, connection))
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

        public List<Product> GetActiveProducts()
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        p.ProductId, p.ProductCode, p.ProductName, p.ProductDescription,
                        p.CategoryId, c.CategoryName,
                        p.BrandId, b.BrandName,
                        p.UnitId, u.UnitName,
                        p.PurchasePrice, p.SalePrice, p.MRP,
                        p.StockQuantity as Quantity, p.ReservedQuantity, p.ReorderLevel,
                        p.Barcode, p.BatchNumber, p.ExpiryDate,
                        p.WarehouseId, w.WarehouseName,
                        p.IsActive, p.Remarks,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    LEFT JOIN Units u ON p.UnitId = u.UnitId
                    LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId
                    WHERE p.IsActive = 1
                    ORDER BY p.ProductName";

                using (var command = new SqlCommand(query, connection))
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

        public Product GetProductById(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        p.ProductId, p.ProductCode, p.ProductName, p.ProductDescription,
                        p.CategoryId, c.CategoryName,
                        p.BrandId, b.BrandName,
                        p.UnitId, u.UnitName,
                        p.PurchasePrice, p.SalePrice, p.MRP,
                        p.StockQuantity as Quantity, p.ReservedQuantity, p.ReorderLevel,
                        p.Barcode, p.BatchNumber, p.ExpiryDate,
                        p.WarehouseId, w.WarehouseName,
                        p.IsActive, p.Remarks,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    LEFT JOIN Units u ON p.UnitId = u.UnitId
                    LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId
                    WHERE p.ProductId = @ProductId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapProduct(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Product GetProductByCode(string productCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        p.ProductId, p.ProductCode, p.ProductName, p.ProductDescription,
                        p.CategoryId, c.CategoryName,
                        p.BrandId, b.BrandName,
                        p.UnitId, u.UnitName,
                        p.PurchasePrice, p.SalePrice, p.MRP,
                        p.StockQuantity as Quantity, p.ReservedQuantity, p.ReorderLevel,
                        p.Barcode, p.BatchNumber, p.ExpiryDate,
                        p.WarehouseId, w.WarehouseName,
                        p.IsActive, p.Remarks,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    LEFT JOIN Units u ON p.UnitId = u.UnitId
                    LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId
                    WHERE p.ProductCode = @ProductCode";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductCode", productCode);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapProduct(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        p.ProductId, p.ProductCode, p.ProductName, p.ProductDescription,
                        p.CategoryId, c.CategoryName,
                        p.BrandId, b.BrandName,
                        p.UnitId, u.UnitName,
                        p.PurchasePrice, p.SalePrice, p.MRP,
                        p.StockQuantity as Quantity, p.ReservedQuantity, p.ReorderLevel,
                        p.Barcode, p.BatchNumber, p.ExpiryDate,
                        p.WarehouseId, w.WarehouseName,
                        p.IsActive, p.Remarks,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    LEFT JOIN Units u ON p.UnitId = u.UnitId
                    LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId
                    WHERE p.CategoryId = @CategoryId AND p.IsActive = 1
                    ORDER BY p.ProductName";

                using (var command = new SqlCommand(query, connection))
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
            }
            return products;
        }

        public List<Product> GetProductsByBrand(int brandId)
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        p.ProductId, p.ProductCode, p.ProductName, p.ProductDescription,
                        p.CategoryId, c.CategoryName,
                        p.BrandId, b.BrandName,
                        p.UnitId, u.UnitName,
                        p.PurchasePrice, p.SalePrice, p.MRP,
                        p.StockQuantity as Quantity, p.ReservedQuantity, p.ReorderLevel,
                        p.Barcode, p.BatchNumber, p.ExpiryDate,
                        p.WarehouseId, w.WarehouseName,
                        p.IsActive, p.Remarks,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    LEFT JOIN Units u ON p.UnitId = u.UnitId
                    LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId
                    WHERE p.BrandId = @BrandId AND p.IsActive = 1
                    ORDER BY p.ProductName";

                using (var command = new SqlCommand(query, connection))
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
            }
            return products;
        }

        public List<Product> GetLowStockProducts(decimal threshold)
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        p.ProductId, p.ProductCode, p.ProductName, p.ProductDescription,
                        p.CategoryId, c.CategoryName,
                        p.BrandId, b.BrandName,
                        p.UnitId, u.UnitName,
                        p.PurchasePrice, p.SalePrice, p.MRP,
                        p.StockQuantity as Quantity, p.ReservedQuantity, p.ReorderLevel,
                        p.Barcode, p.BatchNumber, p.ExpiryDate,
                        p.WarehouseId, w.WarehouseName,
                        p.IsActive, p.Remarks,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    LEFT JOIN Units u ON p.UnitId = u.UnitId
                    LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId
                    WHERE p.StockQuantity <= @Threshold AND p.IsActive = 1
                    ORDER BY p.StockQuantity ASC";

                using (var command = new SqlCommand(query, connection))
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
            }
            return products;
        }

        public List<Product> GetOutOfStockProducts()
        {
            return GetLowStockProducts(0);
        }

        public bool CreateProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO Products (
                        ProductCode, ProductName, ProductDescription, CategoryId, BrandId, UnitId,
                        PurchasePrice, SalePrice, MRP, StockQuantity, ReservedQuantity, ReorderLevel,
                        Barcode, BatchNumber, ExpiryDate, WarehouseId, IsActive, Remarks,
                        CreatedBy, CreatedDate
                    ) VALUES (
                        @ProductCode, @ProductName, @ProductDescription, @CategoryId, @BrandId, @UnitId,
                        @PurchasePrice, @SalePrice, @MRP, @StockQuantity, @ReservedQuantity, @ReorderLevel,
                        @Barcode, @BatchNumber, @ExpiryDate, @WarehouseId, @IsActive, @Remarks,
                        @CreatedBy, @CreatedDate
                    )";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductCode", product.ProductCode);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    command.Parameters.AddWithValue("@BrandId", product.BrandId);
                    command.Parameters.AddWithValue("@UnitId", product.UnitId);
                    command.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                    command.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                    command.Parameters.AddWithValue("@MRP", product.MRP);
                    command.Parameters.AddWithValue("@StockQuantity", product.Quantity);
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

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE Products SET
                        ProductCode = @ProductCode,
                        ProductName = @ProductName,
                        ProductDescription = @ProductDescription,
                        CategoryId = @CategoryId,
                        BrandId = @BrandId,
                        UnitId = @UnitId,
                        PurchasePrice = @PurchasePrice,
                        SalePrice = @SalePrice,
                        MRP = @MRP,
                        StockQuantity = @StockQuantity,
                        ReservedQuantity = @ReservedQuantity,
                        ReorderLevel = @ReorderLevel,
                        Barcode = @Barcode,
                        BatchNumber = @BatchNumber,
                        ExpiryDate = @ExpiryDate,
                        WarehouseId = @WarehouseId,
                        IsActive = @IsActive,
                        Remarks = @Remarks,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate
                    WHERE ProductId = @ProductId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", product.ProductId);
                    command.Parameters.AddWithValue("@ProductCode", product.ProductCode);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    command.Parameters.AddWithValue("@BrandId", product.BrandId);
                    command.Parameters.AddWithValue("@UnitId", product.UnitId);
                    command.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                    command.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                    command.Parameters.AddWithValue("@MRP", product.MRP);
                    command.Parameters.AddWithValue("@StockQuantity", product.Quantity);
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

        public bool DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Products SET IsActive = 0 WHERE ProductId = @ProductId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateStock(int productId, decimal quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Products SET StockQuantity = @StockQuantity WHERE ProductId = @ProductId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@StockQuantity", quantity);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ReserveStock(int productId, decimal quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE Products SET
                        ReservedQuantity = ISNULL(ReservedQuantity, 0) + @Quantity
                    WHERE ProductId = @ProductId
                    AND (ISNULL(ReservedQuantity, 0) + @Quantity) <= StockQuantity";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ReleaseStock(int productId, decimal quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE Products SET
                        ReservedQuantity = ISNULL(ReservedQuantity, 0) - @Quantity
                    WHERE ProductId = @ProductId
                    AND ISNULL(ReservedQuantity, 0) >= @Quantity";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public decimal GetAvailableStock(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT StockQuantity - ISNULL(ReservedQuantity, 0) AS AvailableStock
                    FROM Products 
                    WHERE ProductId = @ProductId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);
                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        p.ProductId, p.ProductCode, p.ProductName, p.ProductDescription,
                        p.CategoryId, c.CategoryName,
                        p.BrandId, b.BrandName,
                        p.UnitId, u.UnitName,
                        p.PurchasePrice, p.SalePrice, p.MRP,
                        p.StockQuantity as Quantity, p.ReservedQuantity, p.ReorderLevel,
                        p.Barcode, p.BatchNumber, p.ExpiryDate,
                        p.WarehouseId, w.WarehouseName,
                        p.IsActive, p.Remarks,
                        p.CreatedBy, p.CreatedDate, p.ModifiedBy, p.ModifiedDate
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryId = c.CategoryId
                    LEFT JOIN Brands b ON p.BrandId = b.BrandId
                    LEFT JOIN Units u ON p.UnitId = u.UnitId
                    LEFT JOIN Warehouses w ON p.WarehouseId = w.WarehouseId
                    WHERE p.IsActive = 1 
                    AND (p.ProductName LIKE @SearchTerm 
                         OR p.ProductCode LIKE @SearchTerm 
                         OR p.Barcode LIKE @SearchTerm)
                    ORDER BY p.ProductName";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(MapProduct(reader));
                        }
                    }
                }
            }
            return products;
        }

        private Product MapProduct(SqlDataReader reader)
        {
            try
            {
                return new Product
                {
                    ProductId = SafeGetInt32(reader, "ProductId"),
                    ProductCode = SafeGetString(reader, "ProductCode"),
                    ProductName = SafeGetString(reader, "ProductName"),
                    ProductDescription = SafeGetString(reader, "ProductDescription"),
                    CategoryId = SafeGetInt32(reader, "CategoryId"),
                    CategoryName = SafeGetString(reader, "CategoryName"),
                    BrandId = SafeGetInt32(reader, "BrandId"),
                    BrandName = SafeGetString(reader, "BrandName"),
                    UnitId = SafeGetInt32(reader, "UnitId"),
                    UnitName = SafeGetString(reader, "UnitName"),
                    PurchasePrice = SafeGetDecimal(reader, "PurchasePrice"),
                    SalePrice = SafeGetDecimal(reader, "SalePrice"),
                    MRP = SafeGetDecimal(reader, "MRP"),
                    Quantity = SafeGetDecimal(reader, "Quantity"),
                    ReservedQuantity = SafeGetDecimal(reader, "ReservedQuantity"),
                    ReorderLevel = SafeGetDecimal(reader, "ReorderLevel"),
                    Barcode = SafeGetString(reader, "Barcode"),
                    BatchNumber = SafeGetString(reader, "BatchNumber"),
                    ExpiryDate = SafeGetDateTimeNullable(reader, "ExpiryDate"),
                    WarehouseId = SafeGetInt32(reader, "WarehouseId"),
                    WarehouseName = SafeGetString(reader, "WarehouseName"),
                    IsActive = SafeGetBoolean(reader, "IsActive"),
                    Remarks = SafeGetString(reader, "Remarks"),
                    CreatedBy = SafeGetInt32(reader, "CreatedBy"),
                    CreatedDate = SafeGetDateTime(reader, "CreatedDate"),
                    ModifiedBy = SafeGetInt32Nullable(reader, "ModifiedBy"),
                    ModifiedDate = SafeGetDateTimeNullable(reader, "ModifiedDate")
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error mapping product: {ex.Message}");
                throw new Exception($"Error mapping product data: {ex.Message}", ex);
            }
        }

        // Helper methods for safe data reading
        private string SafeGetString(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? null : value.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading string column '{columnName}': {ex.Message}");
                return null;
            }
        }

        private int SafeGetInt32(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? 0 : Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading int32 column '{columnName}': {ex.Message}");
                return 0;
            }
        }

        private int? SafeGetInt32Nullable(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? (int?)null : Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading int32 nullable column '{columnName}': {ex.Message}");
                return null;
            }
        }

        private decimal SafeGetDecimal(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? 0 : Convert.ToDecimal(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading decimal column '{columnName}': {ex.Message}");
                return 0;
            }
        }

        private bool SafeGetBoolean(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? false : Convert.ToBoolean(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading boolean column '{columnName}': {ex.Message}");
                return false;
            }
        }

        private DateTime SafeGetDateTime(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading datetime column '{columnName}': {ex.Message}");
                return DateTime.MinValue;
            }
        }

        private DateTime? SafeGetDateTimeNullable(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading datetime nullable column '{columnName}': {ex.Message}");
                return null;
            }
        }
    }
}
