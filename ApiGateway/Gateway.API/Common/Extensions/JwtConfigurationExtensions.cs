using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Gateway.API.Common.Extensions
{
    /// <summary>
    /// Define extension methods for application JWT authentication.
    /// </summary>
    public static class JwtConfigurationExtensions
    {
        /// <summary>
        /// Add JWT authentication service to DI.
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