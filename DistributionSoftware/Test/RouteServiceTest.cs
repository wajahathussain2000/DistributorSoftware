using System;
using System.Collections.Generic;
using DistributionSoftware.Business;
using DistributionSoftware.Models;

namespace DistributionSoftware.Test
{
    public class RouteServiceTest
    {
        public static void TestRouteService()
        {
            try
            {
                Console.WriteLine("Testing RouteService...");
                
                var routeService = new RouteService();
                
                // Test GetAll method
                Console.WriteLine("Testing GetAll method...");
                var routes = routeService.GetAll();
                Console.WriteLine($"Found {routes.Count} routes");
                
                foreach (var route in routes)
                {
                    Console.WriteLine($"- {route.RouteName} (ID: {route.RouteId}, Active: {route.Status})");
                }
                
                // Test GetActiveRoutes method
                Console.WriteLine("\nTesting GetActiveRoutes method...");
                var activeRoutes = routeService.GetActiveRoutes();
                Console.WriteLine($"Found {activeRoutes.Count} active routes");
                
                // Test SearchRoutes method
                Console.WriteLine("\nTesting SearchRoutes method...");
                var searchResults = routeService.SearchRoutes("City");
                Console.WriteLine($"Found {searchResults.Count} routes matching 'City'");
                
                Console.WriteLine("\nRouteService test completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing RouteService: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }
    }
}
