using Gateway.API.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostEnvironment Environment { get; }

        public Startup(IHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot(Configuration);
            services.AddJwtService(Configuration);

            services.AddOpenTracing();
            services.AddJaegerService(Configuration, Environment);

            services.AddCors();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(options => options.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod());

            app.UseAuthentication();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc");

                endpoints.MapHealthChecks("/hc/ready", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("ready"),
                });

                endpoints.MapHealthChecks("/hc/live", new HealthCheckOptions()
                {
                    Predicate = (_) => false
                });
            });

            app.UseOcelot().Wait();
        }
    }
}