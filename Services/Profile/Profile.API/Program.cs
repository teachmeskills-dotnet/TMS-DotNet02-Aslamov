using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Profile.API.Common.Constants;
using Profile.API.Common.Extensions;
using Profile.API.Common.Interfaces;
using Profile.API.Services;
using Serilog;

namespace Profile.API
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