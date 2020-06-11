using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sensor.API.Common.Constants;
using Sensor.API.Common.Interfaces;
using Sensor.API.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.API.Infrastructure
{
    /// <summary>
    /// Seed sensor context with test data.
    /// </summary>
    public class SensorContextSeed
    {
        /// <summary>
        /// Fill database with initial date.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                var contextOptions = serviceProvider.GetRequiredService<DbContextOptions<SensorContext>>();

                using var sensorContext = new SensorContext(contextOptions);
                SeedAsync(sensorContext).GetAwaiter().GetResult();

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
        public static async Task SeedAsync(ISensorContext context)
        {
            if (!context.Types.Any())
            {
                await context.Types.AddRangeAsync(GetPreconfiguredSensorTypes());
                await context.SaveChangesAsync(new CancellationToken());
            }

            if (!context.Sensors.Any())
            {
                var types = await context.Types.ToListAsync();

                await context.Sensors.AddRangeAsync(GetPreconfiguredSensorDevices(types));
                await context.SaveChangesAsync(new CancellationToken());
            }

            if (!context.Records.Any())
            {
                var sensors = await context.Sensors.ToListAsync();

                await context.Records.AddRangeAsync(GetPreconfiguredSensorRecords(sensors));
                await context.SaveChangesAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// Get preconfigured sensor devices for application context seed.
        /// </summary>
        /// <returns>Sensor devices.</returns>
        public static ICollection<SensorDevice> GetPreconfiguredSensorDevices(ICollection<SensorType> sensorTypes)
        {
            if (sensorTypes.Count == 0 || sensorTypes.Count < 2)
            {
                return null;
            }

            return new List<SensorDevice>()
            {
                new SensorDevice()
                {
                    Serial = "123456789",
                    SensorTypeId = sensorTypes.ElementAt(0).Id,
                    Type = sensorTypes.ElementAt(0),
                },
                new SensorDevice()
                {
                    Serial = "987654321",
                    SensorTypeId = sensorTypes.ElementAt(1).Id,
                    Type = sensorTypes.ElementAt(1),
                },
            };
        }

        /// <summary>
        /// Get preconfigured sensor types for application context seed.
        /// </summary>
        /// <returns>Sensor types.</returns>
        public static ICollection<SensorType> GetPreconfiguredSensorTypes()
        {
            return new List<SensorType>()
            {
                new SensorType
                {
                    Type = "Temperature",
                },

                new SensorType
                {
                    Type = "Acoustic",
                },
            };
        }

        /// <summary>
        /// Get preconfigured sensor records for application context seed.
        /// </summary>
        /// <returns>Sensor records.</returns>
        public static ICollection<SensorRecord> GetPreconfiguredSensorRecords(ICollection<SensorDevice> sensorDevices)
        {
            if (sensorDevices.Count == 0 || sensorDevices.Count < 2)
            {
                return null;
            }

            return new List<SensorRecord>()
            {
                new SensorRecord()
                {
                    IsDeleted = false,
                    Date = DateTime.Parse("01-01-2018"),
                    SensorDeviceId = sensorDevices.ElementAt(0).Id,
                    SensorDevice = sensorDevices.ElementAt(0),
                    Value = new byte[] { 255, 254, 253, 242},
                },

                new SensorRecord()
                {
                    IsDeleted = false,
                    Date = DateTime.Parse("01-01-2019"),
                    SensorDeviceId = sensorDevices.ElementAt(1).Id,
                    SensorDevice = sensorDevices.ElementAt(1),
                    Value = new byte[] { 0, 1, 2, 3},
                },
            };
        }
    }
}