﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Profile.API.Common.Constants;
using Profile.API.Common.Interfaces;
using Profile.API.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Profile.API.Infrastructure
{
    /// <summary>
    /// Seed profile context with test data.
    /// </summary>
    public class ProfileContextSeed
    {
        /// <summary>
        /// Fill database with initial date.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var contextOptions = serviceProvider.GetRequiredService<DbContextOptions<ProfileContext>>();

                using var profileContext = new ProfileContext(contextOptions);
                SeedAsync(profileContext).GetAwaiter().GetResult();

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
        public static async Task SeedAsync(IProfileContext context)
        {
            if (!context.Profiles.Any())
            {
                await context.Profiles.AddRangeAsync(GetPreconfiguredProfileModels());
                await context.SaveChangesAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// Get preconfigured profile models for application context seed.
        /// </summary>
        /// <returns>Profile models.</returns>
        public static ICollection<ProfileModel> GetPreconfiguredProfileModels()
        {
            return new List<ProfileModel>()
            {
                new ProfileModel()
                {
                    Id = new Guid(),
                    FirstName = "FirstName_One",
                    LastName = "LastName_One",
                    MiddleName = "MiddleName_One",
                    BirthDate = DateTime.Parse("01-01-1984"),
                    Gender = "Male",
                    Height = 180,
                    Weight = 80,
                    AccountId = Guid.Parse("4c704d36-7c29-4a33-7fda-08d811bd62ef"),
                },

                new ProfileModel()
                {
                    Id = new Guid(),
                    FirstName = "FirstName_Two",
                    LastName = "LastName_Two",
                    MiddleName = "MiddleName_Two",
                    BirthDate = DateTime.Parse("01-01-1974"),
                    Gender = "Female",
                    Height = 165,
                    Weight = 50,
                    AccountId = Guid.Parse("892ee6da-d6af-4cf0-7fdb-08d811bd62ef"),
                },
            };
        }
    }
}