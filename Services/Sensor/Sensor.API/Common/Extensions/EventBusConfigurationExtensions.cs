using DataProcessor.API.EventBus.Produsers;
using EventBus.Contracts.Commands;
using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using MassTransit;
using MassTransit.OpenTracing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sensor.API.Common.Settings;
using System;

namespace Sensor.API.Common.Extensions
{
    /// <summary>
    /// Define extensions to configure event bus.
    /// </summary>
    public static class EventBusConfigurationExtensions
    {
        /// <summary>
        /// Add event bus service (RabbitMQ-based).
        /// </summary>
        /// <param name="services">DI Container.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <param name="environment">Hosting environment.</param>
        /// <returns>Configured event bus service.</returns>
        public static IServiceCollection AddEventBusService(this IServiceCollection services, 
                                                            IConfiguration configuration,
                                                            IHostEnvironment environment)
        {
            var eventBusSettingsSection = configuration.GetSection("EventBusSettings");
            var eventBusSettings = eventBusSettingsSection.Get<EventBusSettings>();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var hostName = environment.IsProduction() ? eventBusSettings.DockerHostName : eventBusSettings.HostName;
                cfg.Host(hostName, eventBusSettings.VirtualHostName, host =>
                {
                    host.Username(eventBusSettings.UserName);
                    host.Password(eventBusSettings.Password);
                });

                cfg.PropagateOpenTracingContext();

                var queueUri = new Uri(string.Concat("queue:", eventBusSettings.DataProcessingQueue));
                EndpointConvention.Map<IProcessData>(queueUri);
            });

            services.AddSingleton(busControl);

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            services.AddScoped(typeof(ICommandProducer<IProcessData, IRecordDTO>), typeof(ProcessDataProducer));

            return services;
        }
    }
}