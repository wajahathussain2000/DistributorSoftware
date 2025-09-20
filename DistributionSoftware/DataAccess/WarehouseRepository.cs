using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DistributionSoftware.Common;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class WarehouseRepository
    {
        private readonly string _connectionString;

        public WarehouseRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? ConfigurationManager.DistributionConnectionString;
        }

        public List<Warehouse> GetActiveWarehouses()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT WarehouseId, WarehouseName FROM Warehouses WHERE IsActive = 1 ORDER BY WarehouseName";
                    
                    var warehouses = new List<Warehouse>();
                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            warehouses.Add(new Warehouse
                            {
                                WarehouseId = Convert.ToInt32(reader["WarehouseId"]),
                                WarehouseName = reader["WarehouseName"].ToString()
                            });
                        }
                    }
                    return warehouses;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("WarehouseRepository.GetActiveWarehouses", ex);
                throw;
            }
        }

        public List<Warehouse> GetAllWarehouses()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT WarehouseId, WarehouseName FROM Warehouses ORDER BY WarehouseName";
                    
                    var warehouses = new List<Warehouse>();
                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            warehouses.Add(new Warehouse
                            {
                                WarehouseId = Convert.ToInt32(reader["WarehouseId"]),
                                WarehouseName = reader["WarehouseName"].ToString()
                            });
                        }
                    }
                    return warehouses;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("WarehouseRepository.GetAllWarehouses", ex);
                throw;
            }
        }
    }
}
