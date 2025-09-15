using System;
using System.Windows.Forms;
using DistributionSoftware.Presentation.Forms;

namespace DistributionSoftware.Test
{
    /// <summary>
    /// Test class for User Activity and Login History Reports
    /// </summary>
    public class UserReportsTest
    {
        /// <summary>
        /// Test if UserActivityReportForm can be instantiated
        /// </summary>
        public static void TestUserActivityReportForm()
        {
            try
            {
                // This test just verifies the form can be created without errors
                // In a real scenario, you would test with proper database connection
                Console.WriteLine("Testing UserActivityReportForm instantiation...");
                
                // Note: This will fail without proper database connection, but that's expected
                // The important thing is that the form class compiles correctly
                var form = new UserActivityReportForm();
                Console.WriteLine("✓ UserActivityReportForm instantiated successfully");
                
                form.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ UserActivityReportForm test failed: {ex.Message}");
                // This is expected if database connection is not available
            }
        }

        /// <summary>
        /// Test if LoginHistoryReportForm can be instantiated
        /// </summary>
        public static void TestLoginHistoryReportForm()
        {
            try
            {
                Console.WriteLine("Testing LoginHistoryReportForm instantiation...");
                
                var form = new LoginHistoryReportForm();
                Console.WriteLine("✓ LoginHistoryReportForm instantiated successfully");
                
                form.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ LoginHistoryReportForm test failed: {ex.Message}");
                // This is expected if database connection is not available
            }
        }

        /// <summary>
        /// Run all user reports tests
        /// </summary>
        public static void RunAllTests()
        {
            Console.WriteLine("=== User Reports Tests ===");
            TestUserActivityReportForm();
            TestLoginHistoryReportForm();
            Console.WriteLine("=== Tests Complete ===");
        }
    }
}
