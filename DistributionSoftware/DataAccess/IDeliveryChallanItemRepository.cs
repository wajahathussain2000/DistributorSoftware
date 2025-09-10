using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IDeliveryChallanItemRepository
    {
        List<DeliveryChallanItem> GetByChallanId(int challanId);
        DeliveryChallanItem GetById(int challanItemId);
        int Create(DeliveryChallanItem item);
        bool Update(DeliveryChallanItem item);
        bool Delete(int challanItemId);
        bool DeleteByChallanId(int challanId);
    }
}
