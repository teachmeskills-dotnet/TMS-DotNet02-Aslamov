using GreenPipes;
using MassTransit;
using MassTransit.OpenTracing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Report.API.EventBus.Consumers;
using Repport.API.Common.Settings;

namespace Report.API.Common.Extensions
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
                x.AddConsumer<RegisterReportConsumer>();

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(context);

                    var hostName = environment.IsProduction() ? eventBusSettings.DockerHostName : eventBusSettings.HostName;
                    cfg.Host(hostName, eventBusSettings.VirtualHostName, host =>
                    {
                        host.Username(eventBusSettings.UserName);
                        host.Password(eventBusSettings.Password);
                    });

                    cfg.PropagateOpenTracingContext();

                    cfg.ReceiveEndpoint(eventBusSettings.ReportsQueue, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));

                        ep.ConfigureConsumer<RegisterReportConsumer>(context);
                    });
                }));
            });

            services.AddMassTransitHostedService();

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            return services;
        }
    }
}