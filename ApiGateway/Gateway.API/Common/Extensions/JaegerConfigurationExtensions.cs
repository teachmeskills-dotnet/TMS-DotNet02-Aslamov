﻿using Jaeger;
using Jaeger.Samplers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using System.Reflection;

namespace Gateway.API.Common.Extensions
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
                string serviceName = Assembly.GetEntryAssembly().GetName().Name;

                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(sample: true);

                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            services.AddOpenTracing();

            return services;
        }
    }
}