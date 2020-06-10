using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Sensor.API.Common.Mapping;

namespace Sensor.API.Common.Extensions
{
    /// <summary>
    /// Extenstion to add services.
    /// </summary>
    public static class ServiceExtensions
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
    }
}