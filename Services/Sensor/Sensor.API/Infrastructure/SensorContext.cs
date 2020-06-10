using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sensor.API.Infrastructure.EntityConfigurations;
using Sensor.API.Models;

namespace Sensor.API.Infrastructure
{
    /// <summary>
    /// Sensor domain context.
    /// </summary>
    public class SensorContext : DbContext
    {
        /// <summary>
        /// Table for sensor devices;
        /// </summary>
        public DbSet<SensorDevice> Sensors { get; set; }

        /// <summary>
        /// Table for sensor records.
        /// </summary>
        public DbSet<SensorRecord> Records { get; set; }

        /// <summary>
        /// Table for sensor/data types.
        /// </summary>
        public DbSet<SensorType> Types { get; set; }

        /// <summary>
        /// Constructor of sensor context.
        /// </summary>
        /// <param name="options"></param>
        public SensorContext(DbContextOptions<SensorContext> options) : base(options) 
        {
            Database.Migrate();
        }

        /// <summary>
        /// Configure models.
        /// </summary>
        /// <param name="builder">Models configurator.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SensorDeviceEntityTypeConfiguration());
            builder.ApplyConfiguration(new SensorTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new SensorRecordEntityTypeConfiguration());
        }
    }
}