using System.Configuration;

namespace DistributionSoftware.Common
{
    public static class ConfigurationManager
    {
        public static string GetConnectionString(string name)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[name];
            return connectionString != null ? connectionString.ConnectionString : null;
        }

        public static string DistributionConnectionString 
        { 
            get { return GetConnectionString("DistributionConnection"); } 
        }
    }
}
