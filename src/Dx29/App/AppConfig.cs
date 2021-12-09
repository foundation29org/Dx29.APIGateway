using System;

using Microsoft.Extensions.Configuration;

namespace Dx29
{
    static public class AppConfig
    {
#if DEBUG
        const string APPCONFIG_NAME = "appconfig-dev.json";
#else
        const string APPCONFIG_NAME = "appconfig.json";
#endif

        static private IConfigurationRoot _configuration = null;

        static AppConfig()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(APPCONFIG_NAME, optional: false, reloadOnChange: true).Build();
        }

        static public string StorageConnectionString => _configuration["StorageConnectionString"];

        static public string DatabaseName => _configuration["DatabaseName"];
        static public string DatabaseConnectionString => _configuration["DatabaseConnectionString"];

        static public object GetValue<T>(string name)
        {
            string str = _configuration[name];
            Type type = typeof(T);

            if (type == typeof(Int16)) return Convert.ToInt16(str);
            if (type == typeof(Int32)) return Convert.ToInt32(str);
            if (type == typeof(Int64)) return Convert.ToInt64(str);
            if (type == typeof(Double)) return Convert.ToDouble(str);
            if (type == typeof(String)) return str;

            throw new NotImplementedException();
        }

        static public string GetValue(string name)
        {
            return _configuration[name];
        }
    }
}
