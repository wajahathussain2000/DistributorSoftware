using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class DeliveryChallan
    {
        public int ChallanId { get; set; }
        public string ChallanNo { get; set; }
        public int? SalesInvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime ChallanDate { get; set; }
        public string VehicleNo { get; set; }
        public int? VehicleId { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public string Remarks { get; set; }
        public string BarcodeImage { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        
        // Navigation properties
        public virtual SalesInvoice SalesInvoice { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<DeliveryChallanItem> Items { get; set; }
        
        public DeliveryChallan()
        {
            Items = new List<DeliveryChallanItem>();
            ChallanDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            Status = "DRAFT";
        }
    }
}
