using Identity.API.Common.Interfaces;
using Identity.API.Infrastructure;
using Identity.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Text;

namespace Identity.API.Common.Extensions
{
    /// <summary>
    /// Extenstion to add services.
    /// </summary>
    public static class IdentityDependencyInjection
    {
        /// <summary>
        /// Add scoped services.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns>Services.</returns>
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityContext, IdentityContext>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }

        /// <summary>
        /// Add Serilog Service.
        /// </summary>
        /// <param name="services">DI container.</param>
        public static IServiceCollection AddSerilogService(this IServiceCollection services)
        {
            ISerilogService serilogConfiguration = new SerilogService();
            Log.Logger = serilogConfiguration.SerilogConfiguration();

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            return services.AddSingleton(Log.Logger);
        }

        /// <summary>
        /// Add JWT-based authentication.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="secret">Secret key.</param>
        public static void AddJwtService(this IServiceCollection services, string secret)
        {
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false; // "false" -- only for debug.
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}