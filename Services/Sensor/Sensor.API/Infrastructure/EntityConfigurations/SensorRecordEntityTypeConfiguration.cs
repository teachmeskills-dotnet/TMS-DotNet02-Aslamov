using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sensor.API.Models;

namespace Sensor.API.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Class for sensor record entity configuration.
    /// </summary>
    public class SensorRecordEntityTypeConfiguration : IEntityTypeConfiguration<SensorRecord>
    {
        /// <summary>
        /// Configure sensor type entity.
        /// </summary>
        /// <param name="builder">Sensor record builder.</param>
        public void Configure(EntityTypeBuilder<SensorRecord> builder)
        {
            builder.HasKey(sr => sr.Id);

            builder.Property(sr => sr.Value).IsRequired();
            builder.Property(sr => sr.Date).IsRequired();
            builder.Property(sr => sr.IsDeleted).IsRequired();

            builder.HasOne(sr => sr.SensorDevice)
                .WithMany()
                .HasForeignKey(sr => sr.SensorDeviceId);
        }
    }
}