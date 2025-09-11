using System;

namespace DistributionSoftware.Models
{
    public class DeliveryScheduleItem
    {
        public int ScheduleItemId { get; set; }
        public int ScheduleId { get; set; }
        public int ChallanId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        
        // Navigation properties
        public virtual DeliverySchedule DeliverySchedule { get; set; }
        public virtual DeliveryChallan DeliveryChallan { get; set; }
        
        public DeliveryScheduleItem()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
