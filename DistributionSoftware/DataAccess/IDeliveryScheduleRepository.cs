using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IDeliveryScheduleRepository
    {
        DeliverySchedule GetById(int scheduleId);
        List<DeliverySchedule> GetAll();
        List<DeliverySchedule> GetByDateRange(DateTime startDate, DateTime endDate);
        List<DeliverySchedule> GetByStatus(string status);
        List<DeliverySchedule> GetByVehicle(int vehicleId);
        List<DeliverySchedule> GetPaged(int pageNumber, int pageSize, DateTime? startDate = null, DateTime? endDate = null, string status = null, int? vehicleId = null);
        int Create(DeliverySchedule schedule);
        bool Update(DeliverySchedule schedule);
        bool Delete(int scheduleId);
        bool UpdateStatus(int scheduleId, string newStatus, int performedBy, DateTime? dispatchDateTime = null, string driverName = null, string remarks = null);
        List<DeliveryChallan> GetAvailableChallans(int? excludeScheduleId = null);
        List<DeliveryScheduleItem> GetScheduleItems(int scheduleId);
        bool AddChallanToSchedule(int scheduleId, int challanId, int createdBy);
        bool RemoveChallanFromSchedule(int scheduleId, int challanId);
        List<DeliveryScheduleAttachment> GetScheduleAttachments(int scheduleId);
        bool AddAttachment(int scheduleId, DeliveryScheduleAttachment attachment);
        bool RemoveAttachment(int attachmentId);
        List<DeliveryScheduleHistory> GetScheduleHistory(int scheduleId);
        bool AddHistoryEntry(DeliveryScheduleHistory history);
        string GenerateScheduleRef();
        bool IsChallanScheduled(int challanId, int? excludeScheduleId = null);
    }
}
