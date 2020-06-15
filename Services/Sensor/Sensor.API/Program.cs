using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sensor.API.Common.Constants;
using Sensor.API.Common.Extensions;
using Sensor.API.Common.Interfaces;
using Sensor.API.Services;
using Serilog;

namespace Sensor.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ISerilogService serilogConfiguration = new SerilogService();
            Log.Logger = serilogConfiguration.SerilogConfiguration();

            try
            {
                Log.Information(InitializationConstants.WEB_HOST_STARTING);
                var host = CreateHostBuilder(args).Build();

                InitialServicesScopeFactory.Build(host);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, InitializationConstants.WEB_HOST_TERMINATED);
            }
            finally
            {
                Log.Information(InitializationConstants.WEB_HOST_STOPPED);
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}