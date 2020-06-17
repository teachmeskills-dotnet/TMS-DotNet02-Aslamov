using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Report.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Report.API.Common.Interfaces
{
    /// <summary>
    /// Interface for report context.
    /// </summary>
    public interface IReportContext
    {
        /// <summary>
        /// Table of reports.
        /// </summary>
        DbSet<ReportModel> Reports { get; set; }

        /// <summary>
        /// Save changes in application context.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Operation result.</returns>
        Task<int> SaveChangesAsync(CancellationToken token);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <returns>Updated entity.</returns>
        EntityEntry Update(object entity);

        /// <summary>
        /// Remove entity.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <returns>Removed entity.</returns>
        EntityEntry Remove(object entity);
    }
}