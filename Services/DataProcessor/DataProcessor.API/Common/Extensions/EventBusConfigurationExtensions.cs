using DataProcessor.API.Common.Settings;
using DataProcessor.API.EventBus.Consumers;
using EventBus.Contracts.Commands;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DataProcessor.API.Common.Extensions
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

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProcessDataConsumer>();

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(context);

                    cfg.Host(eventBusSettings.HostName, eventBusSettings.VirtualHostName, host =>
                    {
                        host.Username(eventBusSettings.UserName);
                        host.Password(eventBusSettings.Password);
                    });

                    cfg.ReceiveEndpoint(eventBusSettings.DataProcessingQueue, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));

                        ep.ConfigureConsumer<ProcessDataConsumer>(context);
                    });
                }));
            });

            var queueUri = new Uri(string.Concat("rabbitmq://localhost","/", eventBusSettings.ReportsQueue));
            EndpointConvention.Map<IRegisterReport>(queueUri);

            services.AddMassTransitHostedService();

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            return services;
        }
    }
}