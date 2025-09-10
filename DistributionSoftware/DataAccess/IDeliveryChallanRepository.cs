using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IDeliveryChallanRepository
    {
        DeliveryChallan GetById(int challanId);
        List<DeliveryChallan> GetAll();
        List<DeliveryChallan> GetByDateRange(DateTime startDate, DateTime endDate);
        DeliveryChallan GetByChallanNo(string challanNo);
        int Create(DeliveryChallan challan);
        bool Update(DeliveryChallan challan);
        bool Delete(int challanId);
        string GenerateChallanNumber();
        bool UpdateStatus(int challanId, string status);
    }
}
