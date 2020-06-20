using Microsoft.Extensions.Configuration;
using Profile.API.Common.Interfaces;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;
using System;
using Microsoft.Extensions.Hosting;

namespace Profile.API.Services
{
    /// <summary>
    /// Serilog service.
    /// </summary>
    public class SerilogService : ISerilogService
    {
        /// <inheritdoc/>
        public Logger SerilogConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isProduction = environment == Environments.Production;

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionType = isProduction ? "DockerConnection" : "DefaultConnection";
            var connectionString = configuration[$"ConnectionStrings:{connectionType}"];

            var serilogConfig =
                 new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.MSSqlServer(connectionString,
                                    sinkOptions: new SinkOptions
                                    {
                                        TableName = "Serilog",
                                        AutoCreateSqlTable = true
                                    })
                .CreateLogger();

            return serilogConfig;
        }
    }
}