using System;
using System.Diagnostics;

namespace DistributionSoftware.Common
{
    public static class DebugHelper
    {
        public static void WriteLine(string message)
        {
            // Write to Debug Output (only visible in debug mode)
            Debug.WriteLine(message);
            
            // Write to Console Output (visible in Visual Studio Output window)
            Console.WriteLine($"[DEBUG] {message}");
            
            // Write to Trace Output (visible in Output window)
            Trace.WriteLine(message);
        }

        public static void WriteException(string context, Exception ex)
        {
            var message = $"❌ EXCEPTION in {context}: {ex.Message}";
            WriteLine(message);
            WriteLine($"Stack Trace: {ex.StackTrace}");
        }

        public static void WriteStep(string step, string message)
        {
            WriteLine($"Step {step}: {message}");
        }

        public static void WriteSuccess(string message)
        {
            WriteLine($"✅ {message}");
        }

        public static void WriteError(string message)
        {
            WriteLine($"❌ {message}");
        }

        public static void WriteInfo(string message)
        {
            WriteLine($"🔍 {message}");
        }

        public static void Break()
        {
            // Debugger breakpoint disabled - just log the event
            WriteLine("🔍 Debugger breakpoint reached (disabled for production)");
        }
    }
}
