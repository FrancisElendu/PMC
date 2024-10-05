using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMC.Infrastructure.Persistence;

namespace PMC.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<PrescriptionManagementDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            //services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            //services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        }
    }
}
