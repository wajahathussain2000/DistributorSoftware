using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IProductService
    {
        // CRUD Operations
        int CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);
        Product GetProductById(int productId);
        Product GetProductByCode(string productCode);
        List<Product> GetAllProducts();
        List<Product> GetActiveProducts();
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetProductsByBrand(int brandId);
        
        // Business Logic
        bool ValidateProduct(Product product);
        string[] GetValidationErrors(Product product);
        string GenerateProductCode();
        bool UpdateStockQuantity(int productId, decimal quantity, string operation);
        
        // Reports
        List<Product> GetProductReport(DateTime? startDate, DateTime? endDate, int? categoryId, int? brandId, bool? isActive);
        int GetProductCount(bool? isActive);
        decimal GetTotalStockValue();
        List<Product> GetLowStockProducts(decimal threshold);
    }
}
