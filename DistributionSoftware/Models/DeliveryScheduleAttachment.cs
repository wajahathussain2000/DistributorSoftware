using System;

namespace DistributionSoftware.Models
{
    public class DeliveryScheduleAttachment
    {
        public int AttachmentId { get; set; }
        public int ScheduleId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public int? FileSize { get; set; }
        public string AttachmentType { get; set; } // POD, MANIFEST, OTHER
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        
        // Navigation properties
        public virtual DeliverySchedule DeliverySchedule { get; set; }
        
        public DeliveryScheduleAttachment()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
