using DataProcessor.API.Common.Settings;
using DataProcessor.API.EventBus.Consumers;
using DataProcessor.API.EventBus.Produsers;
using EventBus.Contracts.Commands;
using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        /// <param name="configuration">Application configuration.</param>
        /// <param name="environment">Hosting environment.</param>
        /// <returns>Configured event bus service.</returns>
        public static IServiceCollection AddEventBusService(this IServiceCollection services,
                                                            IConfiguration configuration,
                                                            IHostEnvironment environment)
        {
            var eventBusSettingsSection = configuration.GetSection("EventBusSettings");
            var eventBusSettings = eventBusSettingsSection.Get<EventBusSettings>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProcessDataConsumer>();

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(context);

                    var hostName = environment.IsProduction() ? eventBusSettings.DockerHostName : eventBusSettings.HostName;
                    cfg.Host(hostName, eventBusSettings.VirtualHostName, host =>
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

                    var queueUri = new Uri(string.Concat("queue:", eventBusSettings.ReportsQueue));
                    EndpointConvention.Map<IRegisterReport>(queueUri);
                }));
            });

            services.AddMassTransitHostedService();

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            services.AddScoped(typeof(ICommandProducer<IRegisterReport,IReportDTO>),typeof(RegisterReportProducer));

            return services;
        }
    }
}