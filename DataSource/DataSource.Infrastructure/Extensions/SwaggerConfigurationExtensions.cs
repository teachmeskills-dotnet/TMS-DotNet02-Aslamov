using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DataSource.Infrastructure.Extensions
{
    /// <summary>
    /// Define extension methods to configure services of infrastructure layer.
    /// </summary>
    public static class SwaggerConfigurationExtensions
    {
        /// <summary>
        /// Add Swagger Service.
        /// </summary>
        /// <param name="services">DI container.</param>
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "iCare Data Source API",
                    Version = "v1",
                    Description = "The Data Source Microservice HHTP API. This is a Data-Driven/CRUD microservice."
                });
            });
        }
    }
}