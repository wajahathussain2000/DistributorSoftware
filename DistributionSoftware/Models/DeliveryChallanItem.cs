using System;

namespace DistributionSoftware.Models
{
    public class DeliveryChallanItem
    {
        public int ChallanItemId { get; set; }
        public int ChallanId { get; set; }
        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        
        // Navigation properties
        public virtual DeliveryChallan DeliveryChallan { get; set; }
        public virtual Product Product { get; set; }
        
        public DeliveryChallanItem()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
