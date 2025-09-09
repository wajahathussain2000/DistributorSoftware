using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        List<Product> GetActiveProducts();
        Product GetProductById(int productId);
        Product GetProductByCode(string productCode);
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetProductsByBrand(int brandId);
        List<Product> GetLowStockProducts(decimal threshold);
        List<Product> GetOutOfStockProducts();
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);
        bool UpdateStock(int productId, decimal quantity);
        bool ReserveStock(int productId, decimal quantity);
        bool ReleaseStock(int productId, decimal quantity);
        decimal GetAvailableStock(int productId);
        List<Product> SearchProducts(string searchTerm);
    }
}
