using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Report.API.Common.Interfaces;
using Report.API.Infrastructure;
using Report.API.Services;
using Serilog;
using System;

namespace Report.API.Common.Extensions
{
    /// <summary>
    /// Extenstion to add services.
    /// </summary>
    public static class ReportDependencyInjection
    {
        /// <summary>
        /// Add scoped services.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns>Services.</returns>
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IReportContext, ReportContext>();
            services.AddScoped<IReportService, ReportService>();

            return services;
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
                    Title = "iCare Report API",
                    Version = "v1",
                    Description = "The Report Microservice HHTP API. This is a Data-Driven/CRUD microservice."
                });
            });
        }
    }
}