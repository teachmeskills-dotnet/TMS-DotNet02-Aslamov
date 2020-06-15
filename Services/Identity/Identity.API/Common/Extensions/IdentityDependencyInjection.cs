using Identity.API.Common.Interfaces;
using Identity.API.Infrastructure;
using Identity.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Identity.API.Common.Extensions
{
    /// <summary>
    /// Extenstion to add services.
    /// </summary>
    public static class IdentityDependencyInjection
    {
        /// <summary>
        /// Add scoped services.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns>Services.</returns>
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityContext, IdentityContext>();

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
    }
}