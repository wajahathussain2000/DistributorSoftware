using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class ReorderLevelService : IReorderLevelService
    {
        private readonly IReorderLevelRepository _reorderLevelRepository;

        public ReorderLevelService()
        {
            _reorderLevelRepository = new ReorderLevelRepository();
        }

        public ReorderLevelService(IReorderLevelRepository reorderLevelRepository)
        {
            _reorderLevelRepository = reorderLevelRepository;
        }

        #region CRUD Operations

        public int CreateReorderLevel(ReorderLevel reorderLevel)
        {
            try
            {
                // Validate reorder level
                if (!ValidateReorderLevel(reorderLevel))
                {
                    var errors = GetValidationErrors(reorderLevel);
                    throw new ArgumentException($"Invalid reorder level: {string.Join(", ", errors)}");
                }

                // Check if reorder level already exists for this product
                var existingReorderLevel = _reorderLevelRepository.GetReorderLevelByProductId(reorderLevel.ProductId);
                if (existingReorderLevel != null)
                {
                    throw new InvalidOperationException("Reorder level already exists for this product. Please update the existing one instead.");
                }

                // Set audit fields
                reorderLevel.CreatedDate = DateTime.Now;
                reorderLevel.CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1;
                reorderLevel.CreatedByName = UserSession.IsLoggedIn ? UserSession.GetDisplayName() : "System User";

                return _reorderLevelRepository.CreateReorderLevel(reorderLevel);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.CreateReorderLevel", ex);
                throw;
            }
        }

        public bool UpdateReorderLevel(ReorderLevel reorderLevel)
        {
            try
            {
                // Validate reorder level
                if (!ValidateReorderLevel(reorderLevel))
                {
                    var errors = GetValidationErrors(reorderLevel);
                    throw new ArgumentException($"Invalid reorder level: {string.Join(", ", errors)}");
                }

                // Set audit fields
                reorderLevel.ModifiedDate = DateTime.Now;
                reorderLevel.ModifiedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1;
                reorderLevel.ModifiedByName = UserSession.IsLoggedIn ? UserSession.GetDisplayName() : "System User";

                return _reorderLevelRepository.UpdateReorderLevel(reorderLevel);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.UpdateReorderLevel", ex);
                throw;
            }
        }

        public bool DeleteReorderLevel(int reorderLevelId)
        {
            try
            {
                return _reorderLevelRepository.DeleteReorderLevel(reorderLevelId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.DeleteReorderLevel", ex);
                throw;
            }
        }

        public ReorderLevel GetReorderLevelById(int reorderLevelId)
        {
            try
            {
                return _reorderLevelRepository.GetReorderLevelById(reorderLevelId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetReorderLevelById", ex);
                throw;
            }
        }

        public ReorderLevel GetReorderLevelByProductId(int productId)
        {
            try
            {
                return _reorderLevelRepository.GetReorderLevelByProductId(productId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetReorderLevelByProductId", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetAllReorderLevels()
        {
            try
            {
                return _reorderLevelRepository.GetAllReorderLevels();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetAllReorderLevels", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetActiveReorderLevels()
        {
            try
            {
                return _reorderLevelRepository.GetActiveReorderLevels();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetActiveReorderLevels", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetReorderLevelsByProduct(int productId)
        {
            try
            {
                return _reorderLevelRepository.GetReorderLevelsByProduct(productId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetReorderLevelsByProduct", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic

        public List<ReorderLevel> GetReorderLevelsBelowMinimum()
        {
            try
            {
                return _reorderLevelRepository.GetReorderLevelsBelowMinimum();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetReorderLevelsBelowMinimum", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetReorderLevelsAboveMaximum()
        {
            try
            {
                return _reorderLevelRepository.GetReorderLevelsAboveMaximum();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetReorderLevelsAboveMaximum", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetReorderLevelsNeedingAlert()
        {
            try
            {
                return _reorderLevelRepository.GetReorderLevelsNeedingAlert();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetReorderLevelsNeedingAlert", ex);
                throw;
            }
        }

        public bool ProcessReorderAlerts()
        {
            try
            {
                var reorderLevelsNeedingAlert = GetReorderLevelsNeedingAlert();
                bool allAlertsProcessed = true;

                foreach (var reorderLevel in reorderLevelsNeedingAlert)
                {
                    try
                    {
                        // Update last alert date
                        _reorderLevelRepository.UpdateLastAlertDate(reorderLevel.ReorderLevelId, DateTime.Now);
                        
                        // Log the alert
                        DebugHelper.WriteInfo($"Reorder Alert: Product {reorderLevel.Product?.ProductName} (ID: {reorderLevel.ProductId}) - Current Stock: {reorderLevel.Product?.StockQuantity}, Minimum Level: {reorderLevel.MinimumLevel}");
                    }
                    catch (Exception ex)
                    {
                        DebugHelper.WriteException($"ReorderLevelService.ProcessReorderAlerts - Product {reorderLevel.ProductId}", ex);
                        allAlertsProcessed = false;
                    }
                }

                return allAlertsProcessed;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.ProcessReorderAlerts", ex);
                throw;
            }
        }

        public bool UpdateLastAlertDate(int reorderLevelId)
        {
            try
            {
                return _reorderLevelRepository.UpdateLastAlertDate(reorderLevelId, DateTime.Now);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.UpdateLastAlertDate", ex);
                throw;
            }
        }

        #endregion

        #region Validation

        public bool ValidateReorderLevel(ReorderLevel reorderLevel)
        {
            if (reorderLevel == null) return false;
            return reorderLevel.IsValid();
        }

        public string[] GetValidationErrors(ReorderLevel reorderLevel)
        {
            if (reorderLevel == null) return new[] { "Reorder level is required" };
            return reorderLevel.GetValidationErrors();
        }

        #endregion

        #region Reports

        public List<ReorderLevel> GetReorderLevelReport(DateTime? startDate, DateTime? endDate, int? productId, bool? isActive)
        {
            try
            {
                return _reorderLevelRepository.GetReorderLevelReport(startDate, endDate, productId, isActive);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetReorderLevelReport", ex);
                throw;
            }
        }

        public int GetReorderLevelCount(bool? isActive)
        {
            try
            {
                return _reorderLevelRepository.GetReorderLevelCount(isActive);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetReorderLevelCount", ex);
                throw;
            }
        }

        public decimal GetTotalReorderValue()
        {
            try
            {
                return _reorderLevelRepository.GetTotalReorderValue();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetTotalReorderValue", ex);
                throw;
            }
        }

        #endregion

        #region Automation

        public bool AutoCreateReorderLevel(int productId, decimal currentStock)
        {
            try
            {
                // Check if reorder level already exists
                var existingReorderLevel = GetReorderLevelByProductId(productId);
                if (existingReorderLevel != null)
                {
                    return false; // Already exists
                }

                // Create suggested reorder level based on current stock
                var suggestedReorderLevel = new ReorderLevel
                {
                    ProductId = productId,
                    MinimumLevel = Math.Max(0, currentStock * 0.2m), // 20% of current stock
                    MaximumLevel = currentStock * 2, // Double current stock
                    ReorderQuantity = currentStock, // Reorder current stock amount
                    IsActive = true,
                    AlertEnabled = true
                };

                CreateReorderLevel(suggestedReorderLevel);
                return true;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.AutoCreateReorderLevel", ex);
                throw;
            }
        }

        public List<ReorderLevel> GetSuggestedReorderLevels()
        {
            try
            {
                // This would typically analyze sales history and suggest optimal reorder levels
                // For now, return products that don't have reorder levels set
                var allProducts = new ProductService().GetAllProducts();
                var productsWithoutReorderLevels = new List<ReorderLevel>();

                foreach (var product in allProducts)
                {
                    var existingReorderLevel = GetReorderLevelByProductId(product.ProductId);
                    if (existingReorderLevel == null)
                    {
                        productsWithoutReorderLevels.Add(new ReorderLevel
                        {
                            ProductId = product.ProductId,
                            Product = product,
                            MinimumLevel = Math.Max(0, product.StockQuantity * 0.2m),
                            MaximumLevel = product.StockQuantity * 2,
                            ReorderQuantity = product.StockQuantity,
                            IsActive = true,
                            AlertEnabled = true
                        });
                    }
                }

                return productsWithoutReorderLevels;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelService.GetSuggestedReorderLevels", ex);
                throw;
            }
        }

        #endregion
    }
}
