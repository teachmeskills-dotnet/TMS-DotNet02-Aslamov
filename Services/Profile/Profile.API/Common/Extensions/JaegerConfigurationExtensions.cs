using Jaeger;
using Jaeger.Samplers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using System.Reflection;

namespace Profile.API.Common.Extensions
{
    /// <summary>
    /// Define extensions to configure Jaeger tracer.
    /// </summary>
    public static class JaegerConfigurationExtensions
    {
        /// <summary>
        /// Add Jaeger service,
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns>Services with configured Jaeger.</returns>
        public static IServiceCollection AddJaegerService(this IServiceCollection services)
        {
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                //string serviceName = Assembly.GetEntryAssembly().GetName().Name;
                var serviceName = "profile";

                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(sample: true);

                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();

                if (!GlobalTracer.IsRegistered())
                {
                    GlobalTracer.Register(tracer);
                }

                return tracer;
            });

            return services;
        }
    }
}