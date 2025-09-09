using System;
using System.Collections.Generic;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;

namespace DistributionSoftware.Business
{
    public interface IStockMovementService
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
        List<StockMovement> GetStockMovementReport(StockMovementReportFilter filter);
        bool CreateSampleData();
    }

    public class StockMovementReportFilter
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string MovementType { get; set; }
        public string ReferenceType { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDateFrom { get; set; }
        public DateTime? ExpiryDateTo { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByUser { get; set; }
    }
}
