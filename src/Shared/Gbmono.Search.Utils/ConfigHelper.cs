using System.Configuration;

namespace Gbmono.Search.Utils
{
    public class ConfigHelper
    {
        public static string GetSetting(string key, string defaultValue)
        {
            var result = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(result))
            {
                result = defaultValue;
            }
            return result;
        }
    }
}
