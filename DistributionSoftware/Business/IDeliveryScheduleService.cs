using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IDeliveryScheduleService
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
        bool DispatchSchedule(int scheduleId, int performedBy, string driverName, string remarks = null);
        bool MarkDelivered(int scheduleId, int performedBy, string remarks = null);
        bool MarkReturned(int scheduleId, int performedBy, string remarks = null);
        bool CancelSchedule(int scheduleId, int performedBy, string remarks = null);
        bool ReopenSchedule(int scheduleId, int performedBy, string remarks = null);
        bool ReassignVehicle(int scheduleId, int vehicleId, string driverName, string driverContact, int performedBy);
        List<DeliveryChallan> GetAvailableChallans(int? excludeScheduleId = null);
        bool AddChallanToSchedule(int scheduleId, int challanId);
        bool RemoveChallanFromSchedule(int scheduleId, int challanId);
        List<DeliveryScheduleItem> GetScheduleItems(int scheduleId);
        List<DeliveryScheduleAttachment> GetScheduleAttachments(int scheduleId);
        bool AddAttachment(int scheduleId, DeliveryScheduleAttachment attachment);
        bool RemoveAttachment(int attachmentId);
        List<DeliveryScheduleHistory> GetScheduleHistory(int scheduleId);
        string GenerateScheduleRef();
        bool ValidateSchedule(DeliverySchedule schedule, out string errorMessage);
        bool IsChallanScheduled(int challanId, int? excludeScheduleId = null);
    }
}
