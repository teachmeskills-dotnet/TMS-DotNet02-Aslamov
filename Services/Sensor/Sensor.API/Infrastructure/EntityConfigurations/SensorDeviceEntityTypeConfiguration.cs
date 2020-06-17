using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sensor.API.Models;

namespace Sensor.API.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Class for sensor device configuration.
    /// </summary>
    public class SensorDeviceEntityTypeConfiguration : IEntityTypeConfiguration<SensorDevice>
    {
        /// <summary>
        /// Configure sensor device entity.
        /// </summary>
        /// <param name="builder">Sensor device builder.</param>
        public void Configure(EntityTypeBuilder<SensorDevice> builder)
        {
            builder.HasKey(sd => sd.Id);

            builder.Property(sd => sd.Serial)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(sd => sd.SensorType)
                .WithMany()
                .HasForeignKey(sd => sd.SensorTypeId);
        }
    }
}