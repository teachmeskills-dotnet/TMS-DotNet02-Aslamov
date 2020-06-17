using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Profile.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Profile.API.Common.Interfaces
{
    /// <summary>
    /// Interface for profile context.
    /// </summary>
    public interface IProfileContext
    {
        /// <summary>
        /// Table of profiles.
        /// </summary>
        DbSet<ProfileModel> Profiles { get; set; }

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