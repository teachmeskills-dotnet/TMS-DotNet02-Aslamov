using Identity.API.Common.Constants;
using Identity.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace Identity.API.Common.Extensions
{
    /// <summary>
    /// Define runtime migrations.
    /// </summary>
    public class RuntimeMigrations
    {
        /// <summary>
        /// Implement runtime migration.
        /// </summary>
        /// <param name="serviceProvider">Services provider.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            try
            {
                var appContextService = serviceProvider.GetRequiredService<IdentityContext>();
                appContextService.Database.Migrate();

                Log.Information(InitializationConstants.MIGRATION_SUCCESS);
            }
            catch (Exception ex)
            {
                Log.Error(ex, InitializationConstants.MIGRATION_ERROR);
            }
        }
    }
}