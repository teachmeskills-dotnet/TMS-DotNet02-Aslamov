using Microsoft.Extensions.Configuration;
using Sensor.API.Common.Interfaces;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.IO;

namespace Sensor.API.Services
{
    /// <summary>
    /// Serilog service.
    /// </summary>
    public class SerilogService : ISerilogService
    {
        /// <inheritdoc/>
        public Logger SerilogConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            var tableName = "Serilog";


            var serilogConfig =
                 new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.MSSqlServer(connectionString,
                                     tableName,
                                     autoCreateSqlTable: true)
                .CreateLogger();

            return serilogConfig;
        }
    }
}
