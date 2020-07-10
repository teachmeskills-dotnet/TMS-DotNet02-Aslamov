using Identity.API.Common.Constants;
using Identity.API.Common.Interfaces;
using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure
{
    /// <summary>
    /// Seed application context with test data.
    /// </summary>
    public class IdentityContextSeed
    {
        /// <summary>
        /// Fill database with initial date.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var contextOptions = serviceProvider.GetRequiredService<DbContextOptions<IdentityContext>>();

                using var identityContext = new IdentityContext(contextOptions);
                SeedAsync(identityContext).GetAwaiter().GetResult();

                Log.Information(InitializationConstants.SEED_SUCCEESS);
            }
            catch (Exception ex)
            {
                Log.Error(ex, InitializationConstants.SEED_ERROR);
            }
        }

        /// <summary>
        /// Seed application context.
        /// </summary>
        /// <param name="context">Application context.</param>
        public static async Task SeedAsync(IIdentityContext context)
        {
            if (!context.Accounts.Any())
            {
                await context.Accounts.AddRangeAsync(GetPreconfiguredAccountModels());
                await context.SaveChangesAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// Get preconfigured account models for application context seed.
        /// </summary>
        /// <returns>Account models.</returns>
        public static ICollection<AccountModel> GetPreconfiguredAccountModels()
        {
            return new List<AccountModel>()
            {
                new AccountModel()
                {
                    Id = Guid.Parse("4c704d36-7c29-4a33-7fda-08d811bd62ef"),
                    Email = "user@gmail.com",
                    Password = "passw@rd123",
                    Username = "user",
                    Role = 1, // User
                    IsActive = true,
                },

                new AccountModel()
                {
                    Id = Guid.Parse("892ee6da-d6af-4cf0-7fdb-08d811bd62ef"),
                    Email = "admin@gmail.com",
                    Password = "passw@rd123",
                    Username = "admin",
                    Role = 2, // Admin
                    IsActive = true,
                },
            };
        }
    }
}