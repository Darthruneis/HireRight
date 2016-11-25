using System.Configuration;

namespace SDK
{
    public static class ConfigurationConstants
    {
        public static string ApiUrl { get; }

        static ConfigurationConstants()
        {
            ApiUrl = ConfigurationManager.AppSettings["APIURL"];

            if (string.IsNullOrWhiteSpace(ApiUrl))
                throw new ConfigurationErrorsException("Api Url information missing from configuration file!");
        }
    }
}