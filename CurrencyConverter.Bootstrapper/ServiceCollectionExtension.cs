using CurrencyConverter.Core.BaseEntities;
using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.Logging;
using CurrencyConverter.DomainEntities;
using CurrencyConverter.Repositories.General;
using CurrencyConverter.RepositoryInterfaces.General;
using CurrencyConverter.ServiceInterfaces;
using CurrencyConverter.ServiceInterfaces.General;
using CurrencyConverter.Services;
using CurrencyConverter.Services.General;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Refit;

namespace CurrencyConverter.Bootstrapper
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesConnector(this IServiceCollection services)
        {
            services.AddTransient<IService<BaseEntityDto>, Service<BaseEntity, BaseEntityDto>>();
            services.AddScoped<IEntitiesContext, EntityContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ILogger, SeriLogger>();

            services.AddScoped<IRepository<BaseEntity>, EntityRepository<BaseEntity>>();
            services.AddSingleton<IAutoMapper, AutoMapperService>();

            services.AddScoped<ICurrencyConvertService, CurrencyConvertService>();
            services.AddScoped<IUserService, UserService>();

            services.AddRefitClient<IFrankfurterRefitClient>().ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(ConfigurationKeys.ConverterAPIs.FrankforterAPI.URL);
                // Below line to add authorization if required
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");
            })
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreaker());

            return services;
        }
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreaker()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 3, durationOfBreak: TimeSpan.FromSeconds(40));
        }
    }
}
