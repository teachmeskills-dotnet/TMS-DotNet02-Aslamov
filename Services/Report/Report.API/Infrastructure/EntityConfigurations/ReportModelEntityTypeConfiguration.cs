using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Report.API.Models;

namespace Report.API.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Class for report model configuration.
    /// </summary>
    public class ReportModelEntityTypeConfiguration : IEntityTypeConfiguration<ReportModel>
    {
        /// <summary>
        /// Configure report model entity.
        /// </summary>
        /// <param name="builder">Report model builder.</param>
        public void Configure(EntityTypeBuilder<ReportModel> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.SensorDeviceId).IsRequired();
            builder.Property(pm => pm.Date).IsRequired();

            builder.Property(pm => pm.DataType)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(pm => pm.HealthStatus)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(pm => pm.HealthDescription).IsRequired();
            builder.Property(pm => pm.Diseases);
            builder.Property(pm => pm.Accuracy).IsRequired();
        }
    }
}