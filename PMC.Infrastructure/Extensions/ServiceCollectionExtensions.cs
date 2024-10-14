using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMC.Application.Service;
using PMC.Infrastructure.Persistence;
using PMC.Infrastructure.Seeder;
using PMC.Infrastructure.Service;
using Polly;

namespace PMC.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<PrescriptionManagementDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
        }

        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddHttpClient("ApiHttpClientConfig", client =>
            {
                client.BaseAddress = new Uri("https://api.example.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(60);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy())
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                MaxConnectionsPerServer = 10
            });

            // Register ApiService
            services.AddTransient<IApiService, ApiService>();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => r.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (result, timespan, retryCount, context) =>
                    {
                        // Log retry attempt
                    });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => (int)r.StatusCode >= 500)
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30),
                    onBreak: (result, breakDelay) =>
                    {
                        // Log circuit breaker activation
                    },
                    onReset: () =>
                    {
                        // Log circuit breaker reset
                    },
                    onHalfOpen: () =>
                    {
                        // Log circuit breaker half-open state
                    });
        }
    }
}
