using System;
using DistributionSoftware.Common;

namespace DistributionSoftware.Test
{
    public static class DebugTest
    {
        public static void RunDebugTest()
        {
            Console.WriteLine("=== DEBUG TEST START ===");
            
            DebugHelper.WriteLine("Testing debug output...");
            DebugHelper.WriteStep("1", "Testing step output");
            DebugHelper.WriteSuccess("Testing success output");
            DebugHelper.WriteError("Testing error output");
            DebugHelper.WriteInfo("Testing info output");
            
            Console.WriteLine("=== DEBUG TEST END ===");
            Console.WriteLine("Check Visual Studio Output window for debug messages");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
