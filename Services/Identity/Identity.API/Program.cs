using Identity.API.Common.Constants;
using Identity.API.Common.Extensions;
using Identity.API.Common.Interfaces;
using Identity.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Identity.API
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