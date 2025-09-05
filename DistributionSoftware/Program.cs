using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DistributionSoftware.Presentation.Forms;

namespace DistributionSoftware
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Use the normal login flow
            Application.Run(new LoginForm());
            // Uncomment the line below to test the dashboard directly (bypasses login)
            // Application.Run(new AdminDashboardRedesigned());
        }
    }
}
