using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Report.API.Common.Constants;
using Report.API.Infrastructure;
using Serilog;
using System;

namespace Report.API.Common.Extensions
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
                var appContextService = serviceProvider.GetRequiredService<ReportContext>();
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