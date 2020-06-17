using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Report.API.Common.Interfaces;
using Report.API.Infrastructure.EntityConfigurations;
using Report.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Report.API.Infrastructure
{
    /// <summary>
    /// Report application context.
    /// </summary>
    public class ReportContext : DbContext, IReportContext
    {
        /// <inheritdoc/>
        public DbSet<ReportModel> Reports { get; set; }

        /// <summary>
        /// Constructor of report context.
        /// </summary>
        /// <param name="options"></param>
        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {
            // Commented due to the runtime migrations enable.
            //Database.Migrate();
        }

        /// <summary>
        /// Configure report models.
        /// </summary>
        /// <param name="builder">Models configurator.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ReportModelEntityTypeConfiguration());
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