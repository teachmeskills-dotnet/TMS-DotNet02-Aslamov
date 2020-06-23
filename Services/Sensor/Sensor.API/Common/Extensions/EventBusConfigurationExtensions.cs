using DataProcessor.API.EventBus.Produsers;
using EventBus.Contracts.Commands;
using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        /// <returns>Configured event bus service.</returns>
        public static IServiceCollection AddEventBusService(this IServiceCollection services, IConfiguration section)
        {
            var eventBusSettingsSection = section.GetSection("EventBusSettings");
            var eventBusSettings = eventBusSettingsSection.Get<EventBusSettings>();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(eventBusSettings.HostName, eventBusSettings.VirtualHostName, host =>
                {
                    host.Username(eventBusSettings.UserName);
                    host.Password(eventBusSettings.Password);
                });

                var queueUri = new Uri(string.Concat(eventBusSettings.HostUri, "/", eventBusSettings.DataProcessingQueue));
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