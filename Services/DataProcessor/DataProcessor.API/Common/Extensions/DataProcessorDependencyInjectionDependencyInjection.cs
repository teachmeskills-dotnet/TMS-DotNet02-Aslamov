using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using DataProcessor.API.Common.Mapping;
using DataProcessor.API.Common.Interfaces;
using DataProcessor.API.Services;
using Microsoft.OpenApi.Models;

namespace DataProcessor.API.Common.Extensions
{
    /// <summary>
    /// Extension to add services.
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

        /// <summary>
        /// Add Swagger Service.
        /// </summary>
        /// <param name="services">DI container</param>
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "iCare DataProcessor API",
                    Version = "v1",
                    Description = "The DataProcessor Microservice HHTP API. This is a Data-Driven/CRUD microservice."
                });
            });
        }
    }
}