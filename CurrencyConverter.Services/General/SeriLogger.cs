using CurrencyConverter.Core.Utilities;
using Serilog;
using Serilog.Core;

namespace CurrencyConverter.Services.General
{
    public class SeriLogger : Core.Logging.ILogger
    {
        private readonly Logger _logger = new LoggerConfiguration()
           .ReadFrom.Configuration(ConfigurationManager.Configuration)
           .WriteTo.Seq(ConfigurationManager.GetValue("Serilog:WriteTo:0:Args:serverUrl"), messageHandler: new HttpClientHandler { UseProxy = false })
           .Enrich.FromLogContext()
           .Enrich.WithMachineName()
           .CreateLogger();
        public string Log(string message)
        {
            var errorId = GenerateLogKey();
            _logger.ForContext("ErrorId", errorId).Error($"API Error: {message}");
            return errorId;
        }

        public string Log(Exception ex)
        {
            var errorId = GenerateLogKey();
            _logger.ForContext("ErrorId", errorId).Error(ex, $"API Error: {ex.Message}");
            return errorId;
        }

        private string GenerateLogKey()
        {
            var date = DateTime.Now;
            return $"{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Second}";
        }
    }
}
