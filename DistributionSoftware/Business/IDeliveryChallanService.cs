using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IDeliveryChallanService
    {
        DeliveryChallan GetById(int challanId);
        List<DeliveryChallan> GetAll();
        List<DeliveryChallan> GetByDateRange(DateTime startDate, DateTime endDate);
        DeliveryChallan GetByChallanNo(string challanNo);
        int CreateDeliveryChallan(DeliveryChallan challan);
        bool UpdateDeliveryChallan(DeliveryChallan challan);
        bool DeleteDeliveryChallan(int challanId);
        string GenerateChallanNumber();
        string GenerateChallanBarcode();
        bool UpdateStatus(int challanId, string status);
        DeliveryChallan CreateFromSalesInvoice(int salesInvoiceId);
    }
}
