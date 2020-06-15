using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profile.API.Models;

namespace Profile.API.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Class for profile model configuration.
    /// </summary>
    public class ProfileModelEntityTypeConfiguration : IEntityTypeConfiguration<ProfileModel>
    {
        /// <summary>
        /// Configure profile model entity.
        /// </summary>
        /// <param name="builder">Profile model builder.</param>
        public void Configure(EntityTypeBuilder<ProfileModel> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.FirstName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(pm => pm.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(pm => pm.MiddleName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(pm => pm.Passport)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(pm => pm.BirthDate).IsRequired();
            builder.Property(pm => pm.Gender).HasMaxLength(20);
            builder.Property(pm => pm.Height).HasDefaultValue(170);
            builder.Property(pm => pm.Weight).HasDefaultValue(60);
        }
    }
}