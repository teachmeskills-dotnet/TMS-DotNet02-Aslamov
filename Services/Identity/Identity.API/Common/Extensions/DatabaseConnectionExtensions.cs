using Identity.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.API.Common.Extensions
{
    /// <summary>
    /// Extension to extract the proper DB connection string based on current environment.
    /// </summary>
    public static class DatabaseConnectionExtensions
    {
        /// <summary>
        /// Choose connection string base on the currenty environment.
        /// </summary>
        /// <param name="environment">Applicaion environment.</param>
        /// <returns>Connection string.</returns>
        public static string ToDbConnectionString(this bool environment)
        {
            string result = environment switch
            {
                true => "DockerConnection",
                _ => "DefaultConnection",
            };
            return result;
        }

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
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}