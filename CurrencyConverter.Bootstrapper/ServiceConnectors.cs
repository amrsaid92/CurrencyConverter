using CurrencyConverter.DomainEntities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CurrencyConverter.Bootstrapper
{
    public static class ServiceConnectors
    {
        public static IServiceCollection AddOpenTelementryConnector(this IServiceCollection services)
        {
            var assemblyName = System.Reflection.Assembly.GetEntryAssembly()?.GetName();
            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddAttributes(new List<KeyValuePair<string, object>>
                { new KeyValuePair<string, object>("Application", "ConverterTelementry") })
                .AddService(assemblyName?.Name ?? string.Empty, assemblyName?.Version?.ToString()))
                .WithMetrics(x =>
                {
                    x.AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();
                    x.AddMeter("Microsoft.AspNetCore.Hosting", "System.Net.Http", assemblyName?.Name ?? string.Empty);
                })
                .WithTracing(x =>
                {
                    x.AddSource(assemblyName?.Name ?? string.Empty)
                    .AddAspNetCoreInstrumentation(o =>
                    {

                        o.EnrichWithHttpRequest = (activity, httpRequest) =>
                        {
                            var clientIp = httpRequest.HttpContext.Connection.RemoteIpAddress?.ToString();
                            if (!string.IsNullOrWhiteSpace(clientIp))
                                activity.SetTag("http.request.client.IP", clientIp);

                            var authHeader = httpRequest.Headers["Authorization"];
                            if (!string.IsNullOrWhiteSpace(authHeader))
                            {
                                var jwtToken = authHeader[0].Remove(0, 7);
                                var handler = new JwtSecurityTokenHandler();
                                var token = handler.ReadJwtToken(jwtToken);
                                activity.SetTag("http.request.client.email",
                                    token.Claims.FirstOrDefault(itm => itm.Type == ClaimTypes.NameIdentifier)?.Value);
                            }
                        };
                    })
                    .AddHttpClientInstrumentation();
                }).UseOtlpExporter(OtlpExportProtocol.HttpProtobuf, new Uri(ConfigurationKeys.OpenTelemntry.Url));
            services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);
            return services;
        }
        public static ILoggingBuilder AddOpenTelementryLogging(this ILoggingBuilder builder)
        {
            builder.AddOpenTelemetry(x =>
            {
                x.IncludeScopes = true;
                x.IncludeFormattedMessage = true;
            });
            return builder;
        }
    }
}
