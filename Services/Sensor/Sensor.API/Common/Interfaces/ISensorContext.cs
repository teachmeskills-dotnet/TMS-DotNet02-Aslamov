using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sensor.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.API.Common.Interfaces
{
    /// <summary>
    /// Interface for sensor context.
    /// </summary>
    public interface ISensorContext
    {
        /// <summary>
        /// Table of sensor devices;
        /// </summary>
        DbSet<SensorDevice> Sensors { get; set; }

        /// <summary>
        /// Table of sensor records.
        /// </summary>
        DbSet<SensorRecord> Records { get; set; }

        /// <summary>
        /// Table of sensor/data types.
        /// </summary>
        DbSet<SensorType> Types { get; set; }

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