using DataSource.API.Settings;
using DataSource.Application.Interfaces;
using DataSource.Application.Settings;
using DataSource.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataSource.API.Extensions
{
    /// <summary>
    /// Define extensions for data source configuration
    /// </summary>
    public static class DataSourceExtensions
    {
        /// <summary>
        /// Extension methos to add service for data generation.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <param name="environment">Application environment.</param>
        /// <returns>Application services.</returns>
        public static IServiceCollection AddDataSourceService(this IServiceCollection services,
                                                                 IConfiguration configuration, 
                                                                 IHostEnvironment environment)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            var hostAddress = environment.IsProduction() ? appSettings.DockerHostAddress : appSettings.DefaultHostAddress;

            var settings = new DataSourceSettings
            {
                AuthToken = appSettings.AuthToken,
                HostAddress = hostAddress,
                DataType = appSettings.DataType,
                GenerationTimeIntervalSeconds = appSettings.GenerationTimeIntervalSeconds,
                SensorSerial = appSettings.SensorSerial,
            };

            services.AddSingleton<IDataSourceService>(app => new DataSourceService(settings));

            var provider = services.BuildServiceProvider();
            var myDataSourceService = provider.GetService<IDataSourceService>();

            return services;
        }
    }
}