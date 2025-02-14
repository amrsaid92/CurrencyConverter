using CurrencyConverter.Core.Utilities;

namespace CurrencyConverter.DomainEntities
{
    public class ConfigurationKeys
    {
        public static string AllowedHosts => ConfigurationManager.GetValue("AllowedHosts");

        public static class ConverterAPIs
        {
            public static class FrankforterAPI
            {
                public static string URL => ConfigurationManager.GetValue("ConverterAPIs:FrankforterAPI:URL");
            }
        }
        public static class JWT
        {
            public static string Secret => ConfigurationManager.GetValue("JWT:Secret");
            public static string Issuer => ConfigurationManager.GetValue("JWT:Issuer");
            public static string Audience => ConfigurationManager.GetValue("JWT:Audience");

        }
        public static class OpenTelemntry
        {
            public static string Url => ConfigurationManager.GetValue("OpenTelemntry:Url");
        }

        public static List<string> ExcludedCurrencies => ConfigurationManager.GetSection<List<string>>("ExcludedCurrencies");
    }
}
