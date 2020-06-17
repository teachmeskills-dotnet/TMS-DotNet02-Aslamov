using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Report.API.Infrastructure;
using System;

namespace Report.API.Common.Extensions
{
    /// <summary>
    /// Define scope factory for services initialization.
    /// </summary>
    public class InitialServicesScopeFactory
    {
        /// <summary>
        /// Build services factory.
        /// </summary>
        /// <param name="host">Application Host.</param>
        public static void Build(IHost host)
        {
            host = host ?? throw new ArgumentNullException(nameof(host));

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            RuntimeMigrations.Initialize(services);
            ReportContextSeed.Initialize(services);
        }
    }
}