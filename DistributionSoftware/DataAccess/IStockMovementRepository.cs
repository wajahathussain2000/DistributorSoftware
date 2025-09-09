using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IStockMovementRepository
    {
        List<StockMovement> GetStockMovements(DateTime? fromDate, DateTime? toDate, int? productId, int? warehouseId, string movementType, string referenceType);
        List<StockMovement> GetStockMovementsByProduct(int productId, DateTime? fromDate, DateTime? toDate);
        List<StockMovement> GetStockMovementsByWarehouse(int warehouseId, DateTime? fromDate, DateTime? toDate);
        List<StockMovement> GetStockMovementsByReference(string referenceType, int referenceId);
        StockMovement GetStockMovementById(int movementId);
        bool AddStockMovement(StockMovement movement);
        bool UpdateStockMovement(StockMovement movement);
        bool DeleteStockMovement(int movementId);
        List<string> GetMovementTypes();
        List<string> GetReferenceTypes();
        bool CreateSampleData();
    }
}
