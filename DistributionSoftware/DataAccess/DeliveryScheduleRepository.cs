using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class DeliveryScheduleRepository : IDeliveryScheduleRepository
    {
        private readonly string connectionString;

        public DeliveryScheduleRepository()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public DeliverySchedule GetById(int scheduleId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetScheduleDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapDeliverySchedule(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery schedule: {ex.Message}", ex);
            }

            return null;
        }

        public List<DeliverySchedule> GetAll()
        {
            return GetPaged(1, int.MaxValue);
        }

        public List<DeliverySchedule> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return GetPaged(1, int.MaxValue, startDate, endDate);
        }

        public List<DeliverySchedule> GetByStatus(string status)
        {
            return GetPaged(1, int.MaxValue, null, null, status);
        }

        public List<DeliverySchedule> GetByVehicle(int vehicleId)
        {
            return GetPaged(1, int.MaxValue, null, null, null, vehicleId);
        }

        public List<DeliverySchedule> GetPaged(int pageNumber, int pageSize, DateTime? startDate = null, DateTime? endDate = null, string status = null, int? vehicleId = null)
        {
            var schedules = new List<DeliverySchedule>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetDeliverySchedules", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Status", status ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@VehicleId", vehicleId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PageNumber", pageNumber);
                        command.Parameters.AddWithValue("@PageSize", pageSize);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                schedules.Add(MapDeliverySchedule(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery schedules: {ex.Message}", ex);
            }

            return schedules;
        }

        public int Create(DeliverySchedule schedule)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_CreateDeliverySchedule", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ScheduleRef", schedule.ScheduleRef);
                        command.Parameters.AddWithValue("@ScheduledDateTime", schedule.ScheduledDateTime);
                        command.Parameters.AddWithValue("@VehicleId", schedule.VehicleId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@VehicleNo", schedule.VehicleNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@RouteId", schedule.RouteId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverName", schedule.DriverName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverContact", schedule.DriverContact ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Remarks", schedule.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", schedule.CreatedBy);
                        command.Parameters.AddWithValue("@ChallanIds", (object)DBNull.Value); // Will be handled separately

                        var scheduleIdParam = new SqlParameter("@ScheduleId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(scheduleIdParam);

                        command.ExecuteNonQuery();

                        return (int)scheduleIdParam.Value;
                    }
                }
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
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        UPDATE DeliverySchedules 
                        SET ScheduledDateTime = @ScheduledDateTime,
                            VehicleId = @VehicleId,
                            VehicleNo = @VehicleNo,
                            RouteId = @RouteId,
                            DriverName = @DriverName,
                            DriverContact = @DriverContact,
                            Remarks = @Remarks,
                            ModifiedDate = GETDATE(),
                            ModifiedBy = @ModifiedBy
                        WHERE ScheduleId = @ScheduleId", connection))
                    {
                        command.Parameters.AddWithValue("@ScheduleId", schedule.ScheduleId);
                        command.Parameters.AddWithValue("@ScheduledDateTime", schedule.ScheduledDateTime);
                        command.Parameters.AddWithValue("@VehicleId", schedule.VehicleId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@VehicleNo", schedule.VehicleNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@RouteId", schedule.RouteId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverName", schedule.DriverName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverContact", schedule.DriverContact ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Remarks", schedule.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", schedule.ModifiedBy ?? schedule.CreatedBy);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
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
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM DeliverySchedules WHERE ScheduleId = @ScheduleId", connection))
                    {
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting delivery schedule: {ex.Message}", ex);
            }
        }

        public bool UpdateStatus(int scheduleId, string newStatus, int performedBy, DateTime? dispatchDateTime = null, string driverName = null, string remarks = null)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_UpdateScheduleStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                        command.Parameters.AddWithValue("@NewStatus", newStatus);
                        command.Parameters.AddWithValue("@PerformedBy", performedBy);
                        command.Parameters.AddWithValue("@DispatchDateTime", dispatchDateTime ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverName", driverName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Remarks", remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@RowVersion", DBNull.Value); // Will be handled by stored procedure

                        var result = command.ExecuteScalar();
                        return result != null && (int)result == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating schedule status: {ex.Message}", ex);
            }
        }

        public List<DeliveryChallan> GetAvailableChallans(int? excludeScheduleId = null)
        {
            var challans = new List<DeliveryChallan>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetAvailableChallans", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ExcludeScheduleId", excludeScheduleId ?? (object)DBNull.Value);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                challans.Add(MapDeliveryChallan(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving available challans: {ex.Message}", ex);
            }

            return challans;
        }

        public List<DeliveryScheduleItem> GetScheduleItems(int scheduleId)
        {
            var items = new List<DeliveryScheduleItem>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        SELECT dsi.*, dc.ChallanNo, dc.CustomerName, dc.CustomerAddress, dc.ChallanDate, dc.Status
                        FROM DeliveryScheduleItems dsi
                        INNER JOIN DeliveryChallans dc ON dsi.ChallanId = dc.ChallanId
                        WHERE dsi.ScheduleId = @ScheduleId
                        ORDER BY dc.ChallanDate", connection))
                    {
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(MapDeliveryScheduleItem(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving schedule items: {ex.Message}", ex);
            }

            return items;
        }

        public bool AddChallanToSchedule(int scheduleId, int challanId, int createdBy)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        INSERT INTO DeliveryScheduleItems (ScheduleId, ChallanId, CreatedBy)
                        VALUES (@ScheduleId, @ChallanId, @CreatedBy)", connection))
                    {
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                        command.Parameters.AddWithValue("@ChallanId", challanId);
                        command.Parameters.AddWithValue("@CreatedBy", createdBy);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
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
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        DELETE FROM DeliveryScheduleItems 
                        WHERE ScheduleId = @ScheduleId AND ChallanId = @ChallanId", connection))
                    {
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                        command.Parameters.AddWithValue("@ChallanId", challanId);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing challan from schedule: {ex.Message}", ex);
            }
        }

        public List<DeliveryScheduleAttachment> GetScheduleAttachments(int scheduleId)
        {
            var attachments = new List<DeliveryScheduleAttachment>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        SELECT * FROM DeliveryScheduleAttachments 
                        WHERE ScheduleId = @ScheduleId 
                        ORDER BY CreatedDate DESC", connection))
                    {
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                attachments.Add(MapDeliveryScheduleAttachment(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving schedule attachments: {ex.Message}", ex);
            }

            return attachments;
        }

        public bool AddAttachment(int scheduleId, DeliveryScheduleAttachment attachment)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        INSERT INTO DeliveryScheduleAttachments 
                        (ScheduleId, FileName, FilePath, FileType, FileSize, AttachmentType, CreatedBy)
                        VALUES (@ScheduleId, @FileName, @FilePath, @FileType, @FileSize, @AttachmentType, @CreatedBy)", connection))
                    {
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                        command.Parameters.AddWithValue("@FileName", attachment.FileName);
                        command.Parameters.AddWithValue("@FilePath", attachment.FilePath);
                        command.Parameters.AddWithValue("@FileType", attachment.FileType ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@FileSize", attachment.FileSize ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@AttachmentType", attachment.AttachmentType ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", attachment.CreatedBy);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
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
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM DeliveryScheduleAttachments WHERE AttachmentId = @AttachmentId", connection))
                    {
                        command.Parameters.AddWithValue("@AttachmentId", attachmentId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing attachment: {ex.Message}", ex);
            }
        }

        public List<DeliveryScheduleHistory> GetScheduleHistory(int scheduleId)
        {
            var history = new List<DeliveryScheduleHistory>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        SELECT dsh.*, u.Username
                        FROM DeliveryScheduleHistory dsh
                        LEFT JOIN Users u ON dsh.ChangedBy = u.UserId
                        WHERE dsh.ScheduleId = @ScheduleId
                        ORDER BY dsh.ChangedAt DESC", connection))
                    {
                        command.Parameters.AddWithValue("@ScheduleId", scheduleId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                history.Add(MapDeliveryScheduleHistory(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving schedule history: {ex.Message}", ex);
            }

            return history;
        }

        public bool AddHistoryEntry(DeliveryScheduleHistory history)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        INSERT INTO DeliveryScheduleHistory 
                        (ScheduleId, ChangedBy, OldStatus, NewStatus, Remarks, DispatchDateTime, DriverName)
                        VALUES (@ScheduleId, @ChangedBy, @OldStatus, @NewStatus, @Remarks, @DispatchDateTime, @DriverName)", connection))
                    {
                        command.Parameters.AddWithValue("@ScheduleId", history.ScheduleId);
                        command.Parameters.AddWithValue("@ChangedBy", history.ChangedBy);
                        command.Parameters.AddWithValue("@OldStatus", history.OldStatus ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@NewStatus", history.NewStatus);
                        command.Parameters.AddWithValue("@Remarks", history.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DispatchDateTime", history.DispatchDateTime ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverName", history.DriverName ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding history entry: {ex.Message}", ex);
            }
        }

        public string GenerateScheduleRef()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        SELECT 'DS' + RIGHT('000000' + CAST(ISNULL(MAX(CAST(SUBSTRING(ScheduleRef, 3, 6) AS INT)), 0) + 1 AS VARCHAR(6)), 6)
                        FROM DeliverySchedules
                        WHERE ScheduleRef LIKE 'DS%'", connection))
                    {
                        var result = command.ExecuteScalar();
                        return result?.ToString() ?? "DS000001";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating schedule reference: {ex.Message}", ex);
            }
        }

        public bool IsChallanScheduled(int challanId, int? excludeScheduleId = null)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"
                        SELECT COUNT(1)
                        FROM DeliveryScheduleItems dsi
                        INNER JOIN DeliverySchedules ds ON dsi.ScheduleId = ds.ScheduleId
                        WHERE dsi.ChallanId = @ChallanId 
                        AND ds.Status IN ('Scheduled', 'Dispatched')
                        AND (@ExcludeScheduleId IS NULL OR ds.ScheduleId != @ExcludeScheduleId)", connection))
                    {
                        command.Parameters.AddWithValue("@ChallanId", challanId);
                        command.Parameters.AddWithValue("@ExcludeScheduleId", excludeScheduleId ?? (object)DBNull.Value);

                        return (int)command.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if challan is scheduled: {ex.Message}", ex);
            }
        }

        #region Helper Methods

        private bool HasColumn(SqlDataReader reader, string columnName)
        {
            try
            {
                return reader.GetOrdinal(columnName) >= 0;
            }
            catch
            {
                return false;
            }
        }

        private DeliverySchedule MapDeliverySchedule(SqlDataReader reader)
        {
            return new DeliverySchedule
            {
                ScheduleId = reader.GetInt32(reader.GetOrdinal("ScheduleId")),
                ScheduleRef = reader.GetString(reader.GetOrdinal("ScheduleRef")),
                ScheduledDateTime = reader.GetDateTime(reader.GetOrdinal("ScheduledDateTime")),
                VehicleId = reader.IsDBNull(reader.GetOrdinal("VehicleId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("VehicleId")),
                VehicleNo = reader.IsDBNull(reader.GetOrdinal("VehicleNo")) ? null : reader.GetString(reader.GetOrdinal("VehicleNo")),
                RouteId = reader.IsDBNull(reader.GetOrdinal("RouteId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RouteId")),
                DriverName = reader.IsDBNull(reader.GetOrdinal("DriverName")) ? null : reader.GetString(reader.GetOrdinal("DriverName")),
                DriverContact = reader.IsDBNull(reader.GetOrdinal("DriverContact")) ? null : reader.GetString(reader.GetOrdinal("DriverContact")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                DispatchDateTime = reader.IsDBNull(reader.GetOrdinal("DispatchDateTime")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DispatchDateTime")),
                DeliveredDateTime = reader.IsDBNull(reader.GetOrdinal("DeliveredDateTime")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DeliveredDateTime")),
                ReturnedDateTime = reader.IsDBNull(reader.GetOrdinal("ReturnedDateTime")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReturnedDateTime")),
                Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ModifiedBy")),
                VehicleType = HasColumn(reader, "VehicleType") ? (reader.IsDBNull(reader.GetOrdinal("VehicleType")) ? null : reader.GetString(reader.GetOrdinal("VehicleType"))) : null,
                RouteName = HasColumn(reader, "RouteName") ? (reader.IsDBNull(reader.GetOrdinal("RouteName")) ? null : reader.GetString(reader.GetOrdinal("RouteName"))) : null,
                TotalRecords = HasColumn(reader, "TotalRecords") ? (reader.IsDBNull(reader.GetOrdinal("TotalRecords")) ? 0 : reader.GetInt32(reader.GetOrdinal("TotalRecords"))) : 0
            };
        }

        private DeliveryChallan MapDeliveryChallan(SqlDataReader reader)
        {
            return new DeliveryChallan
            {
                ChallanId = reader.GetInt32(reader.GetOrdinal("ChallanId")),
                ChallanNo = reader.GetString(reader.GetOrdinal("ChallanNo")),
                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                CustomerAddress = reader.GetString(reader.GetOrdinal("CustomerAddress")),
                ChallanDate = reader.GetDateTime(reader.GetOrdinal("ChallanDate")),
                Status = reader.GetString(reader.GetOrdinal("Status"))
            };
        }

        private DeliveryScheduleItem MapDeliveryScheduleItem(SqlDataReader reader)
        {
            return new DeliveryScheduleItem
            {
                ScheduleItemId = reader.GetInt32(reader.GetOrdinal("ScheduleItemId")),
                ScheduleId = reader.GetInt32(reader.GetOrdinal("ScheduleId")),
                ChallanId = reader.GetInt32(reader.GetOrdinal("ChallanId")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy"))
            };
        }

        private DeliveryScheduleAttachment MapDeliveryScheduleAttachment(SqlDataReader reader)
        {
            return new DeliveryScheduleAttachment
            {
                AttachmentId = reader.GetInt32(reader.GetOrdinal("AttachmentId")),
                ScheduleId = reader.GetInt32(reader.GetOrdinal("ScheduleId")),
                FileName = reader.GetString(reader.GetOrdinal("FileName")),
                FilePath = reader.GetString(reader.GetOrdinal("FilePath")),
                FileType = reader.IsDBNull(reader.GetOrdinal("FileType")) ? null : reader.GetString(reader.GetOrdinal("FileType")),
                FileSize = reader.IsDBNull(reader.GetOrdinal("FileSize")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("FileSize")),
                AttachmentType = reader.IsDBNull(reader.GetOrdinal("AttachmentType")) ? null : reader.GetString(reader.GetOrdinal("AttachmentType")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy"))
            };
        }

        private DeliveryScheduleHistory MapDeliveryScheduleHistory(SqlDataReader reader)
        {
            return new DeliveryScheduleHistory
            {
                HistoryId = reader.GetInt32(reader.GetOrdinal("HistoryId")),
                ScheduleId = reader.GetInt32(reader.GetOrdinal("ScheduleId")),
                ChangedAt = reader.GetDateTime(reader.GetOrdinal("ChangedAt")),
                ChangedBy = reader.GetInt32(reader.GetOrdinal("ChangedBy")),
                OldStatus = reader.IsDBNull(reader.GetOrdinal("OldStatus")) ? null : reader.GetString(reader.GetOrdinal("OldStatus")),
                NewStatus = reader.GetString(reader.GetOrdinal("NewStatus")),
                Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                DispatchDateTime = reader.IsDBNull(reader.GetOrdinal("DispatchDateTime")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DispatchDateTime")),
                DriverName = reader.IsDBNull(reader.GetOrdinal("DriverName")) ? null : reader.GetString(reader.GetOrdinal("DriverName")),
                Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? null : reader.GetString(reader.GetOrdinal("Username"))
            };
        }

        #endregion
    }
}
