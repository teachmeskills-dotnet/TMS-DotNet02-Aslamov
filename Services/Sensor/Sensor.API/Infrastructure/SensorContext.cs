using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sensor.API.Common.Interfaces;
using Sensor.API.Infrastructure.EntityConfigurations;
using Sensor.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.API.Infrastructure
{
    /// <summary>
    /// Sensor application context.
    /// </summary>
    public class SensorContext : DbContext, ISensorContext
    {
        /// <inheritdoc/>
        public DbSet<SensorDevice> Sensors { get; set; }

        /// <inheritdoc/>
        public DbSet<SensorRecord> Records { get; set; }

        /// <inheritdoc/>
        public DbSet<SensorType> Types { get; set; }

        /// <summary>
        /// Constructor of sensor context.
        /// </summary>
        /// <param name="options"></param>
        public SensorContext(DbContextOptions<SensorContext> options) : base(options) 
        {
            // Commented due to runtime migrations enable.
            //Database.Migrate();
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

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public override EntityEntry Update(object entity)
        {
            return base.Update(entity);
        }

        /// <inheritdoc/>
        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }
    }
}