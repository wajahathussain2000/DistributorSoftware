using System.Configuration;

namespace DistributionSoftware.Common
{
    public static class ConfigurationManager
    {
        public static string GetConnectionString(string name)
        {
            try
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[name];
                var result = connectionString != null ? connectionString.ConnectionString : null;
                System.Diagnostics.Debug.WriteLine($"GetConnectionString('{name}') = {(result ?? "null")}");
                return result;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting connection string '{name}': {ex.Message}");
                return null;
            }
        }

        public static string DistributionConnectionString 
        { 
            get { return GetConnectionString("DistributionConnection"); } 
        }
    }
}
