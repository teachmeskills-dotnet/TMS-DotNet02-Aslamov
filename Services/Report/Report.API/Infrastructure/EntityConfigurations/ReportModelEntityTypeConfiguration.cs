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

            builder.Property(r => r.RecordId).IsRequired();
            builder.Property(r => r.Date).IsRequired();

            builder.Property(r => r.DataType)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.HealthStatus)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(r => r.HealthDescription).IsRequired();
            builder.Property(r => r.Diseases);
            builder.Property(r => r.Accuracy).IsRequired();
        }
    }
}