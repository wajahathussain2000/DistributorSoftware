using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class SalesmanRepository : ISalesmanRepository
    {
        private readonly string _connectionString;

        public SalesmanRepository()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DistributionConnection"].ConnectionString;
        }

        public async Task<List<Salesman>> GetAllSalesmenAsync()
        {
            var salesmen = new List<Salesman>();
            
            try
            {
                // First ensure table exists
                await CreateSalesmanTableIfNotExistsAsync();
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT SalesmanId, SalesmanCode, SalesmanName, Email, Phone, 
                               Address, Territory, CommissionRate, IsActive, 
                               CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM Salesman 
                        ORDER BY SalesmanName";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            salesmen.Add(MapSalesmanFromReader(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all salesmen: {ex.Message}");
                // Return empty list if error occurs
            }
            
            return salesmen;
        }

        public async Task<List<Salesman>> GetActiveSalesmenAsync()
        {
            var salesmen = new List<Salesman>();
            
            try
            {
                await CreateSalesmanTableIfNotExistsAsync();
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT SalesmanId, SalesmanCode, SalesmanName, Email, Phone, 
                               Address, Territory, CommissionRate, IsActive, 
                               CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM Salesman 
                        WHERE IsActive = 1
                        ORDER BY SalesmanName";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            salesmen.Add(MapSalesmanFromReader(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting active salesmen: {ex.Message}");
            }
            
            return salesmen;
        }

        public async Task<Salesman> GetSalesmanByIdAsync(int salesmanId)
        {
            try
            {
                await CreateSalesmanTableIfNotExistsAsync();
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT SalesmanId, SalesmanCode, SalesmanName, Email, Phone, 
                               Address, Territory, CommissionRate, IsActive, 
                               CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM Salesman 
                        WHERE SalesmanId = @SalesmanId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanId", salesmanId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return MapSalesmanFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting salesman by ID: {ex.Message}");
            }
            
            return null;
        }

        public async Task<Salesman> GetSalesmanByCodeAsync(string salesmanCode)
        {
            try
            {
                await CreateSalesmanTableIfNotExistsAsync();
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        SELECT SalesmanId, SalesmanCode, SalesmanName, Email, Phone, 
                               Address, Territory, CommissionRate, IsActive, 
                               CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM Salesman 
                        WHERE SalesmanCode = @SalesmanCode";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanCode", salesmanCode);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return MapSalesmanFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting salesman by code: {ex.Message}");
            }
            
            return null;
        }

        public async Task<int> CreateSalesmanAsync(Salesman salesman)
        {
            try
            {
                await CreateSalesmanTableIfNotExistsAsync();
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        INSERT INTO Salesman (SalesmanCode, SalesmanName, Email, Phone, 
                                            Address, Territory, CommissionRate, IsActive, 
                                            CreatedDate, CreatedBy)
                        VALUES (@SalesmanCode, @SalesmanName, @Email, @Phone, 
                                @Address, @Territory, @CommissionRate, @IsActive, 
                                @CreatedDate, @CreatedBy);
                        SELECT SCOPE_IDENTITY();";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanCode", salesman.SalesmanCode);
                        command.Parameters.AddWithValue("@SalesmanName", salesman.SalesmanName);
                        command.Parameters.AddWithValue("@Email", salesman.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", salesman.Phone ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Address", salesman.Address ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Territory", salesman.Territory ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CommissionRate", salesman.CommissionRate);
                        command.Parameters.AddWithValue("@IsActive", salesman.IsActive);
                        command.Parameters.AddWithValue("@CreatedDate", salesman.CreatedDate);
                        command.Parameters.AddWithValue("@CreatedBy", salesman.CreatedBy);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating salesman: {ex.Message}");
                return 0;
            }
        }

        public async Task<bool> UpdateSalesmanAsync(Salesman salesman)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = @"
                        UPDATE Salesman 
                        SET SalesmanCode = @SalesmanCode, 
                            SalesmanName = @SalesmanName, 
                            Email = @Email, 
                            Phone = @Phone, 
                            Address = @Address, 
                            Territory = @Territory, 
                            CommissionRate = @CommissionRate, 
                            IsActive = @IsActive, 
                            ModifiedDate = @ModifiedDate, 
                            ModifiedBy = @ModifiedBy
                        WHERE SalesmanId = @SalesmanId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanId", salesman.SalesmanId);
                        command.Parameters.AddWithValue("@SalesmanCode", salesman.SalesmanCode);
                        command.Parameters.AddWithValue("@SalesmanName", salesman.SalesmanName);
                        command.Parameters.AddWithValue("@Email", salesman.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", salesman.Phone ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Address", salesman.Address ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Territory", salesman.Territory ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CommissionRate", salesman.CommissionRate);
                        command.Parameters.AddWithValue("@IsActive", salesman.IsActive);
                        command.Parameters.AddWithValue("@ModifiedDate", salesman.ModifiedDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", salesman.ModifiedBy ?? (object)DBNull.Value);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating salesman: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSalesmanAsync(int salesmanId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var query = "DELETE FROM Salesman WHERE SalesmanId = @SalesmanId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanId", salesmanId);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting salesman: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateSalesmanTableIfNotExistsAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    var createTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Salesman' AND xtype='U')
                        BEGIN
                            CREATE TABLE Salesman (
                                SalesmanId INT IDENTITY(1,1) PRIMARY KEY,
                                SalesmanCode NVARCHAR(20) NOT NULL UNIQUE,
                                SalesmanName NVARCHAR(100) NOT NULL,
                                Email NVARCHAR(100),
                                Phone NVARCHAR(20),
                                Address NVARCHAR(500),
                                Territory NVARCHAR(100),
                                CommissionRate DECIMAL(5,2) DEFAULT 0.00,
                                IsActive BIT DEFAULT 1,
                                CreatedDate DATETIME DEFAULT GETDATE(),
                                CreatedBy INT,
                                ModifiedDate DATETIME,
                                ModifiedBy INT
                            );
                            
                            -- Insert sample data if table is empty
                            IF NOT EXISTS (SELECT 1 FROM Salesman)
                            BEGIN
                                INSERT INTO Salesman (SalesmanCode, SalesmanName, Email, Phone, Address, Territory, CommissionRate, IsActive, CreatedBy)
                                VALUES 
                                ('SM001', 'John Smith', 'john.smith@company.com', '+1-555-0101', '123 Main St, New York, NY', 'North Region', 5.50, 1, 1),
                                ('SM002', 'Sarah Johnson', 'sarah.johnson@company.com', '+1-555-0102', '456 Oak Ave, Los Angeles, CA', 'West Region', 6.00, 1, 1),
                                ('SM003', 'Michael Brown', 'michael.brown@company.com', '+1-555-0103', '789 Pine St, Chicago, IL', 'Central Region', 5.75, 1, 1),
                                ('SM004', 'Emily Davis', 'emily.davis@company.com', '+1-555-0104', '321 Elm St, Houston, TX', 'South Region', 5.25, 1, 1),
                                ('SM005', 'David Wilson', 'david.wilson@company.com', '+1-555-0105', '654 Maple Dr, Phoenix, AZ', 'Southwest Region', 6.25, 1, 1),
                                ('SM006', 'Lisa Anderson', 'lisa.anderson@company.com', '+1-555-0106', '987 Cedar Ln, Philadelphia, PA', 'Northeast Region', 5.00, 1, 1),
                                ('SM007', 'Robert Taylor', 'robert.taylor@company.com', '+1-555-0107', '147 Birch St, San Antonio, TX', 'South Region', 5.80, 1, 1),
                                ('SM008', 'Jennifer Martinez', 'jennifer.martinez@company.com', '+1-555-0108', '258 Spruce Ave, San Diego, CA', 'West Region', 6.50, 1, 1),
                                ('SM009', 'Christopher Garcia', 'christopher.garcia@company.com', '+1-555-0109', '369 Willow Rd, Dallas, TX', 'Central Region', 5.30, 1, 1),
                                ('SM010', 'Amanda Rodriguez', 'amanda.rodriguez@company.com', '+1-555-0110', '741 Poplar St, San Jose, CA', 'West Region', 6.00, 1, 1);
                            END
                        END";
                    
                    using (var command = new SqlCommand(createTableQuery, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating salesman table: {ex.Message}");
                return false;
            }
        }

        private Salesman MapSalesmanFromReader(SqlDataReader reader)
        {
            return new Salesman
            {
                SalesmanId = reader.GetInt32("SalesmanId"),
                SalesmanCode = reader.GetString("SalesmanCode"),
                SalesmanName = reader.GetString("SalesmanName"),
                Email = reader.IsDBNull("Email") ? null : reader.GetString("Email"),
                Phone = reader.IsDBNull("Phone") ? null : reader.GetString("Phone"),
                Address = reader.IsDBNull("Address") ? null : reader.GetString("Address"),
                Territory = reader.IsDBNull("Territory") ? null : reader.GetString("Territory"),
                CommissionRate = reader.GetDecimal("CommissionRate"),
                IsActive = reader.GetBoolean("IsActive"),
                CreatedDate = reader.GetDateTime("CreatedDate"),
                CreatedBy = reader.GetInt32("CreatedBy"),
                ModifiedDate = reader.IsDBNull("ModifiedDate") ? (DateTime?)null : reader.GetDateTime("ModifiedDate"),
                ModifiedBy = reader.IsDBNull("ModifiedBy") ? (int?)null : reader.GetInt32("ModifiedBy")
            };
        }
    }
}
