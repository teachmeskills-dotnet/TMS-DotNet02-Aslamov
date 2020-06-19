using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sensor.API.Infrastructure;

namespace Sensor.API.Common.Extensions
{
    /// <summary>
    /// Define extensions to configure application Db context.
    /// </summary>
    public static class DbContextConfigurationExtensions
    {
        /// <summary>
        /// Add application db context.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="configuration">Startup configuration.</param>
        /// <param name="environment">Web hosting environment.</param>
        /// <returns>Services with application context.</returns>
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
                                                                 IConfiguration configuration,
                                                                 IHostEnvironment environment)
        {
            var conntectionType = environment.IsProduction() ? "DockerConnection" : "DefaultConnection";

            var connectionString = configuration.GetConnectionString(conntectionType);
            services.AddDbContext<SensorContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}