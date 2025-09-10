using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly string _connectionString;

        public VehicleRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public List<Vehicle> GetAllVehicles()
        {
            var vehicles = new List<Vehicle>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        VehicleId, VehicleNo, VehicleType, DriverName, DriverContact,
                        TransporterName, IsActive, CreatedDate, CreatedBy, 
                        ModifiedDate, ModifiedBy, Remarks
                    FROM VehicleMaster
                    ORDER BY VehicleNo";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vehicles.Add(MapVehicle(reader));
                    }
                }
            }
            return vehicles;
        }

        public List<Vehicle> GetActiveVehicles()
        {
            var vehicles = new List<Vehicle>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        VehicleId, VehicleNo, VehicleType, DriverName, DriverContact,
                        TransporterName, IsActive, CreatedDate, CreatedBy, 
                        ModifiedDate, ModifiedBy, Remarks
                    FROM VehicleMaster
                    WHERE IsActive = 1
                    ORDER BY VehicleNo";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vehicles.Add(MapVehicle(reader));
                    }
                }
            }
            return vehicles;
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        VehicleId, VehicleNo, VehicleType, DriverName, DriverContact,
                        TransporterName, IsActive, CreatedDate, CreatedBy, 
                        ModifiedDate, ModifiedBy, Remarks
                    FROM VehicleMaster
                    WHERE VehicleId = @VehicleId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleId", vehicleId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapVehicle(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Vehicle GetVehicleByNumber(string vehicleNo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        VehicleId, VehicleNo, VehicleType, DriverName, DriverContact,
                        TransporterName, IsActive, CreatedDate, CreatedBy, 
                        ModifiedDate, ModifiedBy, Remarks
                    FROM VehicleMaster
                    WHERE VehicleNo = @VehicleNo";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleNo", vehicleNo);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapVehicle(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Vehicle> SearchVehicles(string searchTerm)
        {
            var vehicles = new List<Vehicle>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        VehicleId, VehicleNo, VehicleType, DriverName, DriverContact,
                        TransporterName, IsActive, CreatedDate, CreatedBy, 
                        ModifiedDate, ModifiedBy, Remarks
                    FROM VehicleMaster
                    WHERE (VehicleNo LIKE @SearchTerm 
                           OR VehicleType LIKE @SearchTerm 
                           OR DriverName LIKE @SearchTerm
                           OR DriverContact LIKE @SearchTerm
                           OR TransporterName LIKE @SearchTerm)
                    ORDER BY VehicleNo";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vehicles.Add(MapVehicle(reader));
                        }
                    }
                }
            }
            return vehicles;
        }

        public bool CreateVehicle(Vehicle vehicle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO VehicleMaster (
                        VehicleNo, VehicleType, DriverName, DriverContact,
                        TransporterName, IsActive, CreatedDate, CreatedBy, Remarks
                    ) VALUES (
                        @VehicleNo, @VehicleType, @DriverName, @DriverContact,
                        @TransporterName, @IsActive, @CreatedDate, @CreatedBy, @Remarks
                    )";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleNo", vehicle.VehicleNo);
                    command.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DriverName", vehicle.DriverName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DriverContact", vehicle.DriverContact ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TransporterName", vehicle.TransporterName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", vehicle.IsActive);
                    command.Parameters.AddWithValue("@CreatedDate", vehicle.CreatedDate);
                    command.Parameters.AddWithValue("@CreatedBy", vehicle.CreatedBy);
                    command.Parameters.AddWithValue("@Remarks", vehicle.Remarks ?? (object)DBNull.Value);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE VehicleMaster SET
                        VehicleNo = @VehicleNo,
                        VehicleType = @VehicleType,
                        DriverName = @DriverName,
                        DriverContact = @DriverContact,
                        TransporterName = @TransporterName,
                        IsActive = @IsActive,
                        ModifiedDate = @ModifiedDate,
                        ModifiedBy = @ModifiedBy,
                        Remarks = @Remarks
                    WHERE VehicleId = @VehicleId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                    command.Parameters.AddWithValue("@VehicleNo", vehicle.VehicleNo);
                    command.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DriverName", vehicle.DriverName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DriverContact", vehicle.DriverContact ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TransporterName", vehicle.TransporterName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", vehicle.IsActive);
                    command.Parameters.AddWithValue("@ModifiedDate", vehicle.ModifiedDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ModifiedBy", vehicle.ModifiedBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Remarks", vehicle.Remarks ?? (object)DBNull.Value);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteVehicle(int vehicleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE VehicleMaster SET IsActive = 0 WHERE VehicleId = @VehicleId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleId", vehicleId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool IsVehicleNumberExists(string vehicleNo, int excludeVehicleId = 0)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM VehicleMaster WHERE VehicleNo = @VehicleNo AND VehicleId != @ExcludeVehicleId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleNo", vehicleNo);
                    command.Parameters.AddWithValue("@ExcludeVehicleId", excludeVehicleId);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        private Vehicle MapVehicle(SqlDataReader reader)
        {
            try
            {
                return new Vehicle
                {
                    VehicleId = SafeGetInt32(reader, "VehicleId"),
                    VehicleNo = SafeGetString(reader, "VehicleNo"),
                    VehicleType = SafeGetString(reader, "VehicleType"),
                    DriverName = SafeGetString(reader, "DriverName"),
                    DriverContact = SafeGetString(reader, "DriverContact"),
                    TransporterName = SafeGetString(reader, "TransporterName"),
                    IsActive = SafeGetBoolean(reader, "IsActive"),
                    CreatedDate = SafeGetDateTime(reader, "CreatedDate"),
                    CreatedBy = SafeGetInt32(reader, "CreatedBy"),
                    ModifiedDate = SafeGetDateTimeNullable(reader, "ModifiedDate"),
                    ModifiedBy = SafeGetInt32Nullable(reader, "ModifiedBy"),
                    Remarks = SafeGetString(reader, "Remarks")
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error mapping vehicle: {ex.Message}");
                throw new Exception($"Error mapping vehicle data: {ex.Message}", ex);
            }
        }

        // Helper methods for safe data reading
        private string SafeGetString(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? null : value.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading string column '{columnName}': {ex.Message}");
                return null;
            }
        }

        private int SafeGetInt32(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? 0 : Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading int32 column '{columnName}': {ex.Message}");
                return 0;
            }
        }

        private int? SafeGetInt32Nullable(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? (int?)null : Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading int32 nullable column '{columnName}': {ex.Message}");
                return null;
            }
        }

        private bool SafeGetBoolean(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? false : Convert.ToBoolean(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading boolean column '{columnName}': {ex.Message}");
                return false;
            }
        }

        private DateTime SafeGetDateTime(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading datetime column '{columnName}': {ex.Message}");
                return DateTime.MinValue;
            }
        }

        private DateTime? SafeGetDateTimeNullable(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading datetime nullable column '{columnName}': {ex.Message}");
                return null;
            }
        }
    }
}
