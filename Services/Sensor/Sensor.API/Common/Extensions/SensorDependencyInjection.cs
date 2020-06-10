using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Sensor.API.Common.Interfaces;
using Sensor.API.Common.Mapping;
using Sensor.API.Infrastructure;
using Sensor.API.Services;

namespace Sensor.API.Common.Extensions
{
    /// <summary>
    /// Extenstion to add services.
    /// </summary>
    public static class SensorDependencyInjection
    {
        /// <summary>
        /// Add Automapper service.
        /// </summary>
        /// <param name="services">DI container/</param>
        /// <param name="configuration">Configuration</param>
        /// <returns>Services.</returns>
        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SensorProfile());
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
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<ISensorContext, SensorContext>();
            return services;
        }
    }
}