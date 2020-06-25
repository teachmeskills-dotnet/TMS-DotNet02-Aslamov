using DataSource.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DataSource.Infrastructure
{
    /// <summary>
    /// Define extenstion methods to add services on infrastrucute layer.
    /// </summary>
    public static class InfrastructureDependentyInjection
    {
        /// <summary>
        /// Add services on infrastructure layer.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns>Services of infractructure layer.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSwaggerService();

            return services;
        }
    }
}