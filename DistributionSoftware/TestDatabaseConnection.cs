using System;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware
{
    class TestDatabaseConnection
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Database Connection Test ===");
            
            try
            {
                string connectionString = ConfigurationManager.DistributionConnectionString;
                Console.WriteLine($"Connection String: {connectionString}");
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("✓ Database connection successful!");
                    
                    // Test RouteMaster table
                    var query = @"
                        SELECT RouteID, RouteName, StartLocation, EndLocation, Distance, 
                               EstimatedTime, IsActive, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                        FROM RouteMaster 
                        ORDER BY RouteName";
                    
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("\n=== RouteMaster Data ===");
                        int count = 0;
                        while (reader.Read())
                        {
                            count++;
                            Console.WriteLine($"Route {count}: {reader["RouteName"]} (ID: {reader["RouteID"]}, Active: {reader["IsActive"]})");
                        }
                        Console.WriteLine($"\nTotal routes found: {count}");
                    }
                    
                    // Test INSERT operation
                    Console.WriteLine("\n=== Testing INSERT Operation ===");
                    var insertQuery = @"
                        INSERT INTO RouteMaster (RouteName, StartLocation, EndLocation, Distance, 
                                               EstimatedTime, IsActive, CreatedDate, CreatedBy)
                        OUTPUT INSERTED.RouteID
                        VALUES (@RouteName, @StartLocation, @EndLocation, @Distance, 
                                @EstimatedTime, @IsActive, @CreatedDate, @CreatedBy)";
                    
                    using (var insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@RouteName", "Test Route " + DateTime.Now.Ticks);
                        insertCommand.Parameters.AddWithValue("@StartLocation", "Test Start");
                        insertCommand.Parameters.AddWithValue("@EndLocation", "Test End");
                        insertCommand.Parameters.AddWithValue("@Distance", 10.5m);
                        insertCommand.Parameters.AddWithValue("@EstimatedTime", "30 minutes");
                        insertCommand.Parameters.AddWithValue("@IsActive", true);
                        insertCommand.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        insertCommand.Parameters.AddWithValue("@CreatedBy", 1);
                        
                        var newRouteId = (int)insertCommand.ExecuteScalar();
                        Console.WriteLine($"✓ Test route created successfully with ID: {newRouteId}");
                        
                        // Clean up test data
                        var deleteQuery = "DELETE FROM RouteMaster WHERE RouteID = @RouteId";
                        using (var deleteCommand = new SqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@RouteId", newRouteId);
                            deleteCommand.ExecuteNonQuery();
                            Console.WriteLine("✓ Test route deleted successfully");
                        }
                    }
                }
                
                Console.WriteLine("\n=== All tests passed! ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
