using Microsoft.Extensions.Configuration;

namespace CurrencyConverter.Core.Utilities
{
    public class ConfigurationManager
    {
        public static IConfigurationRoot Configuration;
        public static void Initialize(string environment)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: true)
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.{environment}.json"), optional: true)
                .Build();
        }

        public static string GetValue(string key) => Configuration != null ? Configuration[key] : string.Empty;
        public static T GetSection<T>(string key) => Configuration.GetSection(key).Get<T>();
        public static string GetConnectionString(string key) => Configuration != null ? Configuration.GetConnectionString(key) : string.Empty;

    }
}
