using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Profile.API.Common.Interfaces;
using Profile.API.Infrastructure.EntityConfigurations;
using Profile.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Profile.API.Infrastructure
{
    /// <summary>
    /// Profile application context.
    /// </summary>
    public class ProfileContext : DbContext, IProfileContext
    {
        /// <inheritdoc/>
        public DbSet<ProfileModel> Profiles { get; set; }

        /// <summary>
        /// Constructor of profile context.
        /// </summary>
        /// <param name="options"></param>
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {
            // Commented due to the runtime migrations enable.
            Database.Migrate();
        }

        /// <summary>
        /// Configure profile models.
        /// </summary>
        /// <param name="builder">Models configurator.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProfileModelEntityTypeConfiguration());
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