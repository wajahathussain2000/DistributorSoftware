using System;
using System.Collections.Generic;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class DeliveryScheduleService : IDeliveryScheduleService
    {
        private readonly IDeliveryScheduleRepository _deliveryScheduleRepository;
        private readonly IVehicleService _vehicleService;
        private readonly IRouteService _routeService;

        public DeliveryScheduleService()
        {
            _deliveryScheduleRepository = new DeliveryScheduleRepository();
            _vehicleService = new VehicleService();
            _routeService = new RouteService();
        }

        public DeliverySchedule GetById(int scheduleId)
        {
            try
            {
                return _deliveryScheduleRepository.GetById(scheduleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery schedule: {ex.Message}", ex);
            }
        }

        public List<DeliverySchedule> GetAll()
        {
            try
            {
                return _deliveryScheduleRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery schedules: {ex.Message}", ex);
            }
        }

        public List<DeliverySchedule> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _deliveryScheduleRepository.GetByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery schedules by date range: {ex.Message}", ex);
            }
        }

        public List<DeliverySchedule> GetByStatus(string status)
        {
            try
            {
                return _deliveryScheduleRepository.GetByStatus(status);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery schedules by status: {ex.Message}", ex);
            }
        }

        public List<DeliverySchedule> GetByVehicle(int vehicleId)
        {
            try
            {
                return _deliveryScheduleRepository.GetByVehicle(vehicleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery schedules by vehicle: {ex.Message}", ex);
            }
        }

        public List<DeliverySchedule> GetPaged(int pageNumber, int pageSize, DateTime? startDate = null, DateTime? endDate = null, string status = null, int? vehicleId = null)
        {
            try
            {
                return _deliveryScheduleRepository.GetPaged(pageNumber, pageSize, startDate, endDate, status, vehicleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving paged delivery schedules: {ex.Message}", ex);
            }
        }

        public int Create(DeliverySchedule schedule)
        {
            try
            {
                // Validate schedule
                if (!ValidateSchedule(schedule, out string errorMessage))
                {
                    throw new Exception(errorMessage);
                }

                // Set created by user
                schedule.CreatedBy = UserSession.CurrentUser?.UserId ?? 1;
                schedule.CreatedDate = DateTime.Now;

                // Generate schedule reference if not provided
                if (string.IsNullOrEmpty(schedule.ScheduleRef))
                {
                    schedule.ScheduleRef = GenerateScheduleRef();
                }

                return _deliveryScheduleRepository.Create(schedule);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating delivery schedule: {ex.Message}", ex);
            }
        }

        public bool Update(DeliverySchedule schedule)
        {
            try
            {
                // Validate schedule
                if (!ValidateSchedule(schedule, out string errorMessage))
                {
                    throw new Exception(errorMessage);
                }

                // Set modified by user
                schedule.ModifiedBy = UserSession.CurrentUser?.UserId ?? 1;
                schedule.ModifiedDate = DateTime.Now;

                return _deliveryScheduleRepository.Update(schedule);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating delivery schedule: {ex.Message}", ex);
            }
        }

        public bool Delete(int scheduleId)
        {
            try
            {
                // Check if schedule exists
                var schedule = _deliveryScheduleRepository.GetById(scheduleId);
                if (schedule == null)
                {
                    throw new Exception("Delivery schedule not found.");
                }

                // Only allow deletion of scheduled or cancelled schedules
                if (schedule.Status != "Scheduled" && schedule.Status != "Cancelled")
                {
                    throw new Exception("Cannot delete schedule with status: " + schedule.Status);
                }

                return _deliveryScheduleRepository.Delete(scheduleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting delivery schedule: {ex.Message}", ex);
            }
        }

        public bool DispatchSchedule(int scheduleId, int performedBy, string driverName, string remarks = null)
        {
            try
            {
                var schedule = _deliveryScheduleRepository.GetById(scheduleId);
                if (schedule == null)
                {
                    throw new Exception("Delivery schedule not found.");
                }

                if (schedule.Status != "Scheduled")
                {
                    throw new Exception("Only scheduled deliveries can be dispatched.");
                }

                // Validate driver information
                if (string.IsNullOrWhiteSpace(driverName))
                {
                    throw new Exception("Driver name is required for dispatch.");
                }

                // Validate vehicle is active
                if (schedule.VehicleId.HasValue)
                {
                    var vehicle = _vehicleService.GetVehicleById(schedule.VehicleId.Value);
                    if (vehicle == null || !vehicle.IsActive)
                    {
                        throw new Exception("Selected vehicle is not active.");
                    }
                }

                return _deliveryScheduleRepository.UpdateStatus(scheduleId, "Dispatched", performedBy, DateTime.Now, driverName, remarks);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error dispatching schedule: {ex.Message}", ex);
            }
        }

        public bool MarkDelivered(int scheduleId, int performedBy, string remarks = null)
        {
            try
            {
                var schedule = _deliveryScheduleRepository.GetById(scheduleId);
                if (schedule == null)
                {
                    throw new Exception("Delivery schedule not found.");
                }

                if (schedule.Status != "Dispatched")
                {
                    throw new Exception("Only dispatched deliveries can be marked as delivered.");
                }

                // TODO: Add validation for POD attachment requirement if configured

                return _deliveryScheduleRepository.UpdateStatus(scheduleId, "Delivered", performedBy, null, null, remarks);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error marking schedule as delivered: {ex.Message}", ex);
            }
        }

        public bool MarkReturned(int scheduleId, int performedBy, string remarks = null)
        {
            try
            {
                var schedule = _deliveryScheduleRepository.GetById(scheduleId);
                if (schedule == null)
                {
                    throw new Exception("Delivery schedule not found.");
                }

                if (schedule.Status != "Dispatched")
                {
                    throw new Exception("Only dispatched deliveries can be marked as returned.");
                }

                if (string.IsNullOrWhiteSpace(remarks))
                {
                    throw new Exception("Return reason is required.");
                }

                return _deliveryScheduleRepository.UpdateStatus(scheduleId, "Returned", performedBy, null, null, remarks);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error marking schedule as returned: {ex.Message}", ex);
            }
        }

        public bool CancelSchedule(int scheduleId, int performedBy, string remarks = null)
        {
            try
            {
                var schedule = _deliveryScheduleRepository.GetById(scheduleId);
                if (schedule == null)
                {
                    throw new Exception("Delivery schedule not found.");
                }

                if (schedule.Status != "Scheduled")
                {
                    throw new Exception("Only scheduled deliveries can be cancelled.");
                }

                return _deliveryScheduleRepository.UpdateStatus(scheduleId, "Cancelled", performedBy, null, null, remarks);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error cancelling schedule: {ex.Message}", ex);
            }
        }

        public bool ReopenSchedule(int scheduleId, int performedBy, string remarks = null)
        {
            try
            {
                var schedule = _deliveryScheduleRepository.GetById(scheduleId);
                if (schedule == null)
                {
                    throw new Exception("Delivery schedule not found.");
                }

                if (schedule.Status != "Delivered" && schedule.Status != "Returned")
                {
                    throw new Exception("Only delivered or returned schedules can be reopened.");
                }

                // TODO: Add permission check for Manager role

                return _deliveryScheduleRepository.UpdateStatus(scheduleId, "Scheduled", performedBy, null, null, remarks);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reopening schedule: {ex.Message}", ex);
            }
        }

        public bool ReassignVehicle(int scheduleId, int vehicleId, string driverName, string driverContact, int performedBy)
        {
            try
            {
                var schedule = _deliveryScheduleRepository.GetById(scheduleId);
                if (schedule == null)
                {
                    throw new Exception("Delivery schedule not found.");
                }

                if (schedule.Status != "Scheduled" && schedule.Status != "Dispatched")
                {
                    throw new Exception("Vehicle can only be reassigned for scheduled or dispatched deliveries.");
                }

                // Validate vehicle
                var vehicle = _vehicleService.GetVehicleById(vehicleId);
                if (vehicle == null || !vehicle.IsActive)
                {
                    throw new Exception("Selected vehicle is not active.");
                }

                // Update schedule with new vehicle and driver info
                schedule.VehicleId = vehicleId;
                schedule.VehicleNo = vehicle.VehicleNo;
                schedule.DriverName = driverName;
                schedule.DriverContact = driverContact;
                schedule.ModifiedBy = performedBy;
                schedule.ModifiedDate = DateTime.Now;

                return _deliveryScheduleRepository.Update(schedule);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reassigning vehicle: {ex.Message}", ex);
            }
        }

        public List<DeliveryChallan> GetAvailableChallans(int? excludeScheduleId = null)
        {
            try
            {
                return _deliveryScheduleRepository.GetAvailableChallans(excludeScheduleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving available challans: {ex.Message}", ex);
            }
        }

        public bool AddChallanToSchedule(int scheduleId, int challanId)
        {
            try
            {
                // Check if challan is already scheduled
                if (_deliveryScheduleRepository.IsChallanScheduled(challanId, scheduleId))
                {
                    throw new Exception("Challan is already scheduled in another active delivery.");
                }

                var createdBy = UserSession.CurrentUser?.UserId ?? 1;
                return _deliveryScheduleRepository.AddChallanToSchedule(scheduleId, challanId, createdBy);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding challan to schedule: {ex.Message}", ex);
            }
        }

        public bool RemoveChallanFromSchedule(int scheduleId, int challanId)
        {
            try
            {
                var schedule = _deliveryScheduleRepository.GetById(scheduleId);
                if (schedule == null)
                {
                    throw new Exception("Delivery schedule not found.");
                }

                if (schedule.Status != "Scheduled")
                {
                    throw new Exception("Challans can only be removed from scheduled deliveries.");
                }

                return _deliveryScheduleRepository.RemoveChallanFromSchedule(scheduleId, challanId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing challan from schedule: {ex.Message}", ex);
            }
        }

        public List<DeliveryScheduleItem> GetScheduleItems(int scheduleId)
        {
            try
            {
                return _deliveryScheduleRepository.GetScheduleItems(scheduleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving schedule items: {ex.Message}", ex);
            }
        }

        public List<DeliveryScheduleAttachment> GetScheduleAttachments(int scheduleId)
        {
            try
            {
                return _deliveryScheduleRepository.GetScheduleAttachments(scheduleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving schedule attachments: {ex.Message}", ex);
            }
        }

        public bool AddAttachment(int scheduleId, DeliveryScheduleAttachment attachment)
        {
            try
            {
                attachment.CreatedBy = UserSession.CurrentUser?.UserId ?? 1;
                return _deliveryScheduleRepository.AddAttachment(scheduleId, attachment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding attachment: {ex.Message}", ex);
            }
        }

        public bool RemoveAttachment(int attachmentId)
        {
            try
            {
                return _deliveryScheduleRepository.RemoveAttachment(attachmentId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing attachment: {ex.Message}", ex);
            }
        }

        public List<DeliveryScheduleHistory> GetScheduleHistory(int scheduleId)
        {
            try
            {
                return _deliveryScheduleRepository.GetScheduleHistory(scheduleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving schedule history: {ex.Message}", ex);
            }
        }

        public string GenerateScheduleRef()
        {
            try
            {
                return _deliveryScheduleRepository.GenerateScheduleRef();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating schedule reference: {ex.Message}", ex);
            }
        }

        public bool ValidateSchedule(DeliverySchedule schedule, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (schedule == null)
            {
                errorMessage = "Schedule cannot be null.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(schedule.ScheduleRef))
            {
                errorMessage = "Schedule reference is required.";
                return false;
            }

            if (schedule.ScheduledDateTime == DateTime.MinValue)
            {
                errorMessage = "Scheduled date and time is required.";
                return false;
            }

            if (schedule.ScheduledDateTime < DateTime.Now.AddHours(-1)) // Allow 1 hour buffer
            {
                errorMessage = "Scheduled date and time cannot be in the past.";
                return false;
            }

            if (schedule.VehicleId.HasValue)
            {
                var vehicle = _vehicleService.GetVehicleById(schedule.VehicleId.Value);
                if (vehicle == null)
                {
                    errorMessage = "Selected vehicle does not exist.";
                    return false;
                }

                if (!vehicle.IsActive)
                {
                    errorMessage = "Selected vehicle is not active.";
                    return false;
                }
            }

            if (schedule.RouteId.HasValue)
            {
                var route = _routeService.GetById(schedule.RouteId.Value);
                if (route == null)
                {
                    errorMessage = "Selected route does not exist.";
                    return false;
                }

                if (!route.Status)
                {
                    errorMessage = "Selected route is not active.";
                    return false;
                }
            }

            return true;
        }

        public bool IsChallanScheduled(int challanId, int? excludeScheduleId = null)
        {
            try
            {
                return _deliveryScheduleRepository.IsChallanScheduled(challanId, excludeScheduleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if challan is scheduled: {ex.Message}", ex);
            }
        }
    }
}
