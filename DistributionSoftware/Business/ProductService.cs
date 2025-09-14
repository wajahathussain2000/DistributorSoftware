using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #region CRUD Operations

        public int CreateProduct(Product product)
        {
            try
            {
                // Validate product
                if (!ValidateProduct(product))
                {
                    var errors = GetValidationErrors(product);
                    throw new ArgumentException($"Invalid product: {string.Join(", ", errors)}");
                }

                // Generate product code if not provided
                if (string.IsNullOrEmpty(product.ProductCode))
                {
                    product.ProductCode = GenerateProductCode();
                }

                // Set audit fields
                product.CreatedDate = DateTime.Now;
                product.CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1;

                return _productRepository.CreateProduct(product);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.CreateProduct", ex);
                throw;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                // Validate product
                if (!ValidateProduct(product))
                {
                    var errors = GetValidationErrors(product);
                    throw new ArgumentException($"Invalid product: {string.Join(", ", errors)}");
                }

                // Set audit fields
                product.ModifiedDate = DateTime.Now;
                product.ModifiedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1;

                return _productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.UpdateProduct", ex);
                throw;
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                return _productRepository.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.DeleteProduct", ex);
                throw;
            }
        }

        public Product GetProductById(int productId)
        {
            try
            {
                return _productRepository.GetProductById(productId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetProductById", ex);
                throw;
            }
        }

        public Product GetProductByCode(string productCode)
        {
            try
            {
                return _productRepository.GetProductByCode(productCode);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetProductByCode", ex);
                throw;
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return _productRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetAllProducts", ex);
                throw;
            }
        }

        public List<Product> GetActiveProducts()
        {
            try
            {
                return _productRepository.GetActiveProducts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetActiveProducts", ex);
                throw;
            }
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            try
            {
                return _productRepository.GetProductsByCategory(categoryId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetProductsByCategory", ex);
                throw;
            }
        }

        public List<Product> GetProductsByBrand(int brandId)
        {
            try
            {
                return _productRepository.GetProductsByBrand(brandId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetProductsByBrand", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic

        public bool ValidateProduct(Product product)
        {
            if (product == null) return false;
            
            if (string.IsNullOrWhiteSpace(product.ProductName)) return false;
            if (product.UnitPrice < 0) return false;
            if (product.StockQuantity < 0) return false;
            
            return true;
        }

        public string[] GetValidationErrors(Product product)
        {
            var errors = new List<string>();
            
            if (product == null)
            {
                errors.Add("Product is required");
                return errors.ToArray();
            }
            
            if (string.IsNullOrWhiteSpace(product.ProductName))
                errors.Add("Product name is required");
            
            if (product.UnitPrice < 0)
                errors.Add("Unit price cannot be negative");
            
            if (product.StockQuantity < 0)
                errors.Add("Stock quantity cannot be negative");
            
            return errors.ToArray();
        }

        public string GenerateProductCode()
        {
            try
            {
                var products = GetAllProducts();
                var maxCode = products.Where(p => !string.IsNullOrEmpty(p.ProductCode) && p.ProductCode.StartsWith("PRD"))
                                     .Select(p => p.ProductCode)
                                     .OrderByDescending(c => c)
                                     .FirstOrDefault();
                
                if (string.IsNullOrEmpty(maxCode))
                {
                    return "PRD001";
                }
                
                var number = int.Parse(maxCode.Substring(3)) + 1;
                return $"PRD{number:D3}";
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GenerateProductCode", ex);
                return $"PRD{DateTime.Now:yyyyMMddHHmmss}";
            }
        }

        public bool UpdateStockQuantity(int productId, decimal quantity, string operation)
        {
            try
            {
                var product = GetProductById(productId);
                if (product == null) return false;
                
                switch (operation.ToUpper())
                {
                    case "ADD":
                    case "IN":
                        product.StockQuantity += quantity;
                        break;
                    case "SUBTRACT":
                    case "OUT":
                        product.StockQuantity -= quantity;
                        break;
                    case "SET":
                        product.StockQuantity = quantity;
                        break;
                    default:
                        return false;
                }
                
                if (product.StockQuantity < 0) product.StockQuantity = 0;
                
                return UpdateProduct(product);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.UpdateStockQuantity", ex);
                throw;
            }
        }

        #endregion

        #region Reports

        public List<Product> GetProductReport(DateTime? startDate, DateTime? endDate, int? categoryId, int? brandId, bool? isActive)
        {
            try
            {
                return _productRepository.GetProductReport(startDate, endDate, categoryId, brandId, isActive);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetProductReport", ex);
                throw;
            }
        }

        public int GetProductCount(bool? isActive)
        {
            try
            {
                return _productRepository.GetProductCount(isActive);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetProductCount", ex);
                throw;
            }
        }

        public decimal GetTotalStockValue()
        {
            try
            {
                return _productRepository.GetTotalStockValue();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetTotalStockValue", ex);
                throw;
            }
        }

        public List<Product> GetLowStockProducts(decimal threshold)
        {
            try
            {
                return _productRepository.GetLowStockProducts(threshold);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ProductService.GetLowStockProducts", ex);
                throw;
            }
        }

        #endregion
    }
}
