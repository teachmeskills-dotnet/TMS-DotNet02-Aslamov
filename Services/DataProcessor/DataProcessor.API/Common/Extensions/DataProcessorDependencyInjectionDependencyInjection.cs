using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using DataProcessor.API.Common.Mapping;
using DataProcessor.API.Common.Interfaces;
using DataProcessor.API.Services;

namespace DataProcessor.API.Common.Extensions
{
    /// <summary>
    /// Extenstion to add services.
    /// </summary>
    public static class DataProcessorDependencyInjection
    {
        /// <summary>
        /// Add Automapper service.
        /// </summary>
        /// <param name="services">DI container/</param>
        /// <returns>Services.</returns>
        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DataProcessorProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        /// <summary>
        /// Add scoped services.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns>Services.</returns>
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IDataProcessorService, DataProcessorService>();

            return services;
        }
    }
}