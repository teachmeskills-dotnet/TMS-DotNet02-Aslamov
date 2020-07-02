using Identity.API.Common.Interfaces;
using Identity.API.Infrastructure.EntityConfigurations;
using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure
{
    /// <summary>
    /// Identity application context.
    /// </summary>
    public class IdentityContext : DbContext, IIdentityContext
    {
        /// <inheritdoc/>
        public DbSet<AccountModel> Accounts { get; set; }

        /// <summary>
        /// Constructor of identity context.
        /// </summary>
        /// <param name="options"></param>
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        /// <summary>
        /// Configure models.
        /// </summary>
        /// <param name="builder">Models configurator.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AccountModelEntityTypeConfiguration());
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