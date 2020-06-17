using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Class for account model configuration.
    /// </summary>
    public class AccountModelEntityTypeConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        /// <summary>
        /// Configure account model entity.
        /// </summary>
        /// <param name="builder">Account model builder.</param>
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.HasKey(sd => sd.Id);

            builder.Property(sd => sd.Email)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(sd => sd.Password)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(sd => sd.Username)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(sd => sd.Role).IsRequired();
            builder.Property(sd => sd.IsActive).IsRequired();
        }
    }
}