using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Sensor.API.Common.Interfaces;
using Sensor.API.Common.Mapping;
using Sensor.API.Infrastructure;
using Sensor.API.Services;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

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
            services.AddScoped<ISensorContext, SensorContext>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IRecordService, RecordService>();

            return services;
        }

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
                    Title = "iCare Sensor API", 
                    Version = "v1",
                    Description = "The Sensor Microservice HHTP API. This is a Data-Driven/CRUD microservice."
                });
            });
        }

        /// <summary>
        /// Add Serilog Service.
        /// </summary>
        /// <param name="services">DI container.</param>
        public static IServiceCollection AddSerilogService(this IServiceCollection services)
        {
            ISerilogService serilogConfiguration = new SerilogService();
            Log.Logger = serilogConfiguration.SerilogConfiguration();

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            return services.AddSingleton(Log.Logger);
        }
    }
}