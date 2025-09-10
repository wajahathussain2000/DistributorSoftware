using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface ISalesReturnService
    {
        int CreateSalesReturn(SalesReturn salesReturn);
        SalesReturn GetSalesReturnById(int returnId);
        List<SalesReturn> GetAllSalesReturns();
        bool UpdateSalesReturn(SalesReturn salesReturn);
        bool DeleteSalesReturn(int returnId);
        string GenerateNewReturnNumber();
        string GenerateReturnBarcode();
        List<SalesReturnItem> GetSalesReturnItems(int returnId);
        decimal CalculateTotalAmount(List<SalesReturnItem> items);
        decimal CalculateTaxAmount(List<SalesReturnItem> items);
        decimal CalculateDiscountAmount(List<SalesReturnItem> items);
        decimal CalculateSubTotal(List<SalesReturnItem> items);
        bool UpdateStockForSalesReturn(int returnId, int updatedByUserId);
    }
}