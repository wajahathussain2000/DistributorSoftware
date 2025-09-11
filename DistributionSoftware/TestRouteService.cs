using System;
using DistributionSoftware.Business;
using DistributionSoftware.Test;

namespace DistributionSoftware
{
    class TestRouteService
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== RouteService Test ===");
            RouteServiceTest.TestRouteService();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
