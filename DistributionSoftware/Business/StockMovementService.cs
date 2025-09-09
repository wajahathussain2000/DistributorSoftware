using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Business
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IStockMovementRepository _stockMovementRepository;

        public StockMovementService()
        {
            _stockMovementRepository = new StockMovementRepository();
        }

        public StockMovementService(IStockMovementRepository stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
        }

        public List<StockMovement> GetStockMovements(DateTime? fromDate, DateTime? toDate, int? productId, int? warehouseId, string movementType, string referenceType)
        {
            try
            {
                return _stockMovementRepository.GetStockMovements(fromDate, toDate, productId, warehouseId, movementType, referenceType);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving stock movements: {ex.Message}", ex);
            }
        }

        public List<StockMovement> GetStockMovementsByProduct(int productId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                return _stockMovementRepository.GetStockMovementsByProduct(productId, fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving stock movements by product: {ex.Message}", ex);
            }
        }

        public List<StockMovement> GetStockMovementsByWarehouse(int warehouseId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                return _stockMovementRepository.GetStockMovementsByWarehouse(warehouseId, fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving stock movements by warehouse: {ex.Message}", ex);
            }
        }

        public List<StockMovement> GetStockMovementsByReference(string referenceType, int referenceId)
        {
            try
            {
                return _stockMovementRepository.GetStockMovementsByReference(referenceType, referenceId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving stock movements by reference: {ex.Message}", ex);
            }
        }

        public StockMovement GetStockMovementById(int movementId)
        {
            try
            {
                return _stockMovementRepository.GetStockMovementById(movementId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving stock movement: {ex.Message}", ex);
            }
        }

        public bool AddStockMovement(StockMovement movement)
        {
            try
            {
                ValidateStockMovement(movement);
                return _stockMovementRepository.AddStockMovement(movement);
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
                ValidateStockMovement(movement);
                return _stockMovementRepository.UpdateStockMovement(movement);
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
                return _stockMovementRepository.DeleteStockMovement(movementId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting stock movement: {ex.Message}", ex);
            }
        }

        public List<string> GetMovementTypes()
        {
            try
            {
                return _stockMovementRepository.GetMovementTypes();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving movement types: {ex.Message}", ex);
            }
        }

        public List<string> GetReferenceTypes()
        {
            try
            {
                return _stockMovementRepository.GetReferenceTypes();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving reference types: {ex.Message}", ex);
            }
        }

        public bool CreateSampleData()
        {
            try
            {
                return _stockMovementRepository.CreateSampleData();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating sample data: {ex.Message}", ex);
            }
        }

        public List<StockMovement> GetStockMovementReport(StockMovementReportFilter filter)
        {
            try
            {
                var movements = _stockMovementRepository.GetStockMovements(
                    filter.FromDate, 
                    filter.ToDate, 
                    filter.ProductId, 
                    filter.WarehouseId, 
                    filter.MovementType, 
                    filter.ReferenceType);

                // Apply additional filters
                if (!string.IsNullOrEmpty(filter.ProductName))
                {
                    movements = movements.Where(m => m.ProductName.ToLower().Contains(filter.ProductName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.WarehouseName))
                {
                    movements = movements.Where(m => m.WarehouseName.ToLower().Contains(filter.WarehouseName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.SupplierName))
                {
                    movements = movements.Where(m => !string.IsNullOrEmpty(m.SupplierName) && 
                        m.SupplierName.ToLower().Contains(filter.SupplierName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.CustomerName))
                {
                    movements = movements.Where(m => !string.IsNullOrEmpty(m.CustomerName) && 
                        m.CustomerName.ToLower().Contains(filter.CustomerName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.BatchNumber))
                {
                    movements = movements.Where(m => !string.IsNullOrEmpty(m.BatchNumber) && 
                        m.BatchNumber.ToLower().Contains(filter.BatchNumber.ToLower())).ToList();
                }

                if (filter.ExpiryDateFrom.HasValue)
                {
                    movements = movements.Where(m => m.ExpiryDate.HasValue && m.ExpiryDate >= filter.ExpiryDateFrom.Value).ToList();
                }

                if (filter.ExpiryDateTo.HasValue)
                {
                    movements = movements.Where(m => m.ExpiryDate.HasValue && m.ExpiryDate <= filter.ExpiryDateTo.Value).ToList();
                }

                if (!string.IsNullOrEmpty(filter.CreatedByUser))
                {
                    movements = movements.Where(m => !string.IsNullOrEmpty(m.CreatedByUser) && 
                        m.CreatedByUser.ToLower().Contains(filter.CreatedByUser.ToLower())).ToList();
                }

                return movements;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating stock movement report: {ex.Message}", ex);
            }
        }

        private void ValidateStockMovement(StockMovement movement)
        {
            if (movement == null)
                throw new ArgumentNullException(nameof(movement), "Stock movement cannot be null");

            if (movement.ProductId <= 0)
                throw new ArgumentException("Product ID must be greater than 0", nameof(movement.ProductId));

            if (movement.WarehouseId <= 0)
                throw new ArgumentException("Warehouse ID must be greater than 0", nameof(movement.WarehouseId));

            if (string.IsNullOrEmpty(movement.MovementType))
                throw new ArgumentException("Movement type is required", nameof(movement.MovementType));

            if (movement.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0", nameof(movement.Quantity));

            if (movement.MovementDate == DateTime.MinValue)
                throw new ArgumentException("Movement date is required", nameof(movement.MovementDate));

            var validMovementTypes = GetMovementTypes();
            if (!validMovementTypes.Contains(movement.MovementType.ToUpper()))
                throw new ArgumentException($"Invalid movement type: {movement.MovementType}", nameof(movement.MovementType));
        }
    }
}
