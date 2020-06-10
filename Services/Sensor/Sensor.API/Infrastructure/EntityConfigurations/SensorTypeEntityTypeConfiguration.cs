using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sensor.API.Models;

namespace Sensor.API.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Class for sensor type entity configuration.
    /// </summary>
    public class SensorTypeEntityTypeConfiguration : IEntityTypeConfiguration<SensorType>
    {
        /// <summary>
        /// Configure sensor type entity.
        /// </summary>
        /// <param name="builder">Sensor type builder.</param>
        public void Configure(EntityTypeBuilder<SensorType> builder)
        {
            builder.HasKey(st => st.Id);

            builder.Property(st => st.Type)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}