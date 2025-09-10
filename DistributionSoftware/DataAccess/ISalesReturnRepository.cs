using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface ISalesReturnRepository
    {
        List<SalesReturn> GetAll();
        SalesReturn GetById(int returnId);
        SalesReturn GetByReturnNumber(string returnNumber);
        List<SalesReturn> GetByCustomerId(int customerId);
        List<SalesReturn> GetByDateRange(DateTime startDate, DateTime endDate);
        int Create(SalesReturn salesReturn);
        bool Update(SalesReturn salesReturn);
        bool Delete(int returnId);
        string GenerateReturnNumber();
        List<SalesReturnItem> GetReturnItems(int returnId);
        bool CreateReturnItem(SalesReturnItem returnItem);
        bool UpdateReturnItem(SalesReturnItem returnItem);
        bool DeleteReturnItem(int returnItemId);
        bool DeleteReturnItemsByReturnId(int returnId);
        bool UpdateStockForSalesReturn(int returnId, int updatedByUserId);
    }
}