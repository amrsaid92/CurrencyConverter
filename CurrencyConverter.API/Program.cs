using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CurrencyConverter.Bootstrapper;
using CurrencyConverter.DomainEntities;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        CurrencyConverter.Core.Utilities.ConfigurationManager.Initialize(builder.Environment.EnvironmentName);
        // Add services to the container.

        builder.Logging.AddOpenTelementryLogging();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API - V1", Version = "v1.0" });
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API - V2", Version = "v2.0" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        builder.Services.AddServicesConnector();
        builder.Services.AddOpenTelementryConnector();

        // Add JWT authorization
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.UseSecurityTokenValidators = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                RequireAudience = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = ConfigurationKeys.JWT.Issuer,
                ValidAudiences = new[] { $"{ConfigurationKeys.JWT.Audience}", "Any" },
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationKeys.JWT.Secret))
            };
        });

        // Implement Output cache
        builder.Services.AddOutputCache(options =>
        {
            options.AddBasePolicy(policy =>
            {
                policy.Expire(TimeSpan.FromDays(1));
                policy.SetCacheKeyPrefix("Currency");
                policy.SetVaryByQuery("*");
            });
        });

        // Implement API versioning
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version")
            );
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // Implement Rate Limiter
        builder.Services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("RateLimitPolicy", opt =>
            {
                opt.Window = TimeSpan.FromMinutes(1);
                opt.PermitLimit = 4;
            });

            // Customize Rejection Response (Return 429 instead of 503)
            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
            };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"CurrencyConverter.{description.GroupName.ToUpperInvariant()}");
                }

            });
        }

        app.UseHttpsRedirection();
        app.UseOutputCache();
        app.UseRateLimiter();
        app.UseAuthorization();

        app.MapControllers().RequireRateLimiting("RateLimitPolicy");
        app.MapHealthChecks("/health", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.Run();
    }
}