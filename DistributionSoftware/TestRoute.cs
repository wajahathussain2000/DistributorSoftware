using System;
using System.Configuration;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Business;

namespace DistributionSoftware
{
    class TestRoute
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Testing Route Operations...");
                
                // Test RouteService
                var routeService = new RouteService();
                
                // Test GetAll
                Console.WriteLine("1. Testing GetAll...");
                var routes = routeService.GetAll();
                Console.WriteLine($"   Found {routes.Count} routes");
                
                // Test Create
                Console.WriteLine("2. Testing Create...");
                var newRoute = new Route
                {
                    RouteName = "Test Route " + DateTime.Now.Ticks,
                    StartLocation = "Test Start Location",
                    EndLocation = "Test End Location",
                    Distance = 15.5m,
                    EstimatedTime = "45 minutes",
                    Status = true,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now
                };
                
                int newRouteId = routeService.Create(newRoute);
                Console.WriteLine($"   Created route with ID: {newRouteId}");
                
                // Test GetById
                Console.WriteLine("3. Testing GetById...");
                var retrievedRoute = routeService.GetById(newRouteId);
                Console.WriteLine($"   Retrieved route: {retrievedRoute.RouteName}");
                
                // Test Update
                Console.WriteLine("4. Testing Update...");
                retrievedRoute.RouteName = "Updated Test Route";
                retrievedRoute.Status = false;
                bool updateResult = routeService.Update(retrievedRoute);
                Console.WriteLine($"   Update result: {updateResult}");
                
                // Test Delete
                Console.WriteLine("5. Testing Delete...");
                bool deleteResult = routeService.Delete(newRouteId);
                Console.WriteLine($"   Delete result: {deleteResult}");
                
                Console.WriteLine("All tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
