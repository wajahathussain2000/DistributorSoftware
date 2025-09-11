using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class DeliveryChallanRepository : IDeliveryChallanRepository
    {
        private readonly string _connectionString;

        public DeliveryChallanRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public DeliveryChallan GetById(int challanId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT ChallanId, ChallanNo, SalesInvoiceId, CustomerName, CustomerAddress, 
                               ChallanDate, VehicleNo, DriverName, DriverPhone, RouteId, Remarks, 
                               BarcodeImage, Status, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy
                        FROM DeliveryChallans 
                        WHERE ChallanId = @ChallanId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanId", challanId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapDeliveryChallan(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery challan: {ex.Message}", ex);
            }

            return null;
        }

        public List<DeliveryChallan> GetAll()
        {
            var challans = new List<DeliveryChallan>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT ChallanId, ChallanNo, SalesInvoiceId, CustomerName, CustomerAddress, 
                               ChallanDate, VehicleNo, DriverName, DriverPhone, Remarks, 
                               BarcodeImage, Status, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy
                        FROM DeliveryChallans 
                        ORDER BY ChallanDate DESC";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            challans.Add(MapDeliveryChallan(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery challans: {ex.Message}", ex);
            }

            return challans;
        }

        public List<DeliveryChallan> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var challans = new List<DeliveryChallan>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT ChallanId, ChallanNo, SalesInvoiceId, CustomerName, CustomerAddress, 
                               ChallanDate, VehicleNo, DriverName, DriverPhone, Remarks, 
                               BarcodeImage, Status, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy
                        FROM DeliveryChallans 
                        WHERE ChallanDate BETWEEN @StartDate AND @EndDate
                        ORDER BY ChallanDate DESC";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddTicks(-1));
                        
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
                throw new Exception($"Error retrieving delivery challans by date range: {ex.Message}", ex);
            }

            return challans;
        }

        public DeliveryChallan GetByChallanNo(string challanNo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT ChallanId, ChallanNo, SalesInvoiceId, CustomerName, CustomerAddress, 
                               ChallanDate, VehicleNo, DriverName, DriverPhone, Remarks, 
                               BarcodeImage, Status, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy
                        FROM DeliveryChallans 
                        WHERE ChallanNo = @ChallanNo";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanNo", challanNo);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapDeliveryChallan(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery challan by number: {ex.Message}", ex);
            }

            return null;
        }

        public int Create(DeliveryChallan challan)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        INSERT INTO DeliveryChallans 
                        (ChallanNo, SalesInvoiceId, CustomerName, CustomerAddress, ChallanDate, 
                         VehicleNo, DriverName, DriverPhone, RouteId, Remarks, BarcodeImage, Status, 
                         CreatedDate, CreatedBy)
                        VALUES 
                        (@ChallanNo, @SalesInvoiceId, @CustomerName, @CustomerAddress, @ChallanDate, 
                         @VehicleNo, @DriverName, @DriverPhone, @RouteId, @Remarks, @BarcodeImage, @Status, 
                         @CreatedDate, @CreatedBy);
                        SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanNo", challan.ChallanNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SalesInvoiceId", challan.SalesInvoiceId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerName", challan.CustomerName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerAddress", challan.CustomerAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ChallanDate", challan.ChallanDate);
                        command.Parameters.AddWithValue("@VehicleNo", challan.VehicleNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverName", challan.DriverName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverPhone", challan.DriverPhone ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@RouteId", challan.RouteId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Remarks", challan.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BarcodeImage", challan.BarcodeImage ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Status", challan.Status ?? "DRAFT");
                        command.Parameters.AddWithValue("@CreatedDate", challan.CreatedDate);
                        command.Parameters.AddWithValue("@CreatedBy", challan.CreatedBy);

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating delivery challan: {ex.Message}", ex);
            }
        }

        public bool Update(DeliveryChallan challan)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        UPDATE DeliveryChallans 
                        SET ChallanNo = @ChallanNo, SalesInvoiceId = @SalesInvoiceId, 
                            CustomerName = @CustomerName, CustomerAddress = @CustomerAddress, 
                            ChallanDate = @ChallanDate, VehicleNo = @VehicleNo, 
                            DriverName = @DriverName, DriverPhone = @DriverPhone, 
                            RouteId = @RouteId, Remarks = @Remarks, BarcodeImage = @BarcodeImage, 
                            Status = @Status, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy
                        WHERE ChallanId = @ChallanId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanId", challan.ChallanId);
                        command.Parameters.AddWithValue("@ChallanNo", challan.ChallanNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SalesInvoiceId", challan.SalesInvoiceId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerName", challan.CustomerName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerAddress", challan.CustomerAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ChallanDate", challan.ChallanDate);
                        command.Parameters.AddWithValue("@VehicleNo", challan.VehicleNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverName", challan.DriverName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DriverPhone", challan.DriverPhone ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@RouteId", challan.RouteId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Remarks", challan.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BarcodeImage", challan.BarcodeImage ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Status", challan.Status ?? "DRAFT");
                        command.Parameters.AddWithValue("@UpdatedDate", challan.UpdatedDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UpdatedBy", challan.UpdatedBy ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating delivery challan: {ex.Message}", ex);
            }
        }

        public bool Delete(int challanId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "DELETE FROM DeliveryChallans WHERE ChallanId = @ChallanId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanId", challanId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting delivery challan: {ex.Message}", ex);
            }
        }

        public string GenerateChallanNumber()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT ISNULL(MAX(CAST(SUBSTRING(ChallanNo, 3, LEN(ChallanNo) - 2) AS INT)), 0) + 1
                        FROM DeliveryChallans 
                        WHERE ChallanNo LIKE 'DC%'";

                    using (var command = new SqlCommand(query, connection))
                    {
                        var nextNumber = Convert.ToInt32(command.ExecuteScalar());
                        return $"DC{nextNumber:D6}";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating challan number: {ex.Message}", ex);
            }
        }

        public bool UpdateStatus(int challanId, string status)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        UPDATE DeliveryChallans 
                        SET Status = @Status, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy
                        WHERE ChallanId = @ChallanId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ChallanId", challanId);
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@UpdatedBy", 1); // TODO: Get from user session

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating challan status: {ex.Message}", ex);
            }
        }

        private DeliveryChallan MapDeliveryChallan(SqlDataReader reader)
        {
            return new DeliveryChallan
            {
                ChallanId = Convert.ToInt32(reader["ChallanId"]),
                ChallanNo = reader["ChallanNo"]?.ToString(),
                SalesInvoiceId = reader["SalesInvoiceId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["SalesInvoiceId"]),
                CustomerName = reader["CustomerName"]?.ToString(),
                CustomerAddress = reader["CustomerAddress"]?.ToString(),
                ChallanDate = Convert.ToDateTime(reader["ChallanDate"]),
                VehicleNo = reader["VehicleNo"]?.ToString(),
                DriverName = reader["DriverName"]?.ToString(),
                DriverPhone = reader["DriverPhone"]?.ToString(),
                RouteId = reader["RouteId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["RouteId"]),
                Remarks = reader["Remarks"]?.ToString(),
                BarcodeImage = reader["BarcodeImage"]?.ToString(),
                Status = reader["Status"]?.ToString(),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                UpdatedDate = reader["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedDate"]),
                UpdatedBy = reader["UpdatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["UpdatedBy"])
            };
        }
    }
}
