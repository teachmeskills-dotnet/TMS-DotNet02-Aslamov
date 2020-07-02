using Sensor.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.API.Common.Interfaces
{
    /// <summary>
    /// Interface to manage sensors.
    /// </summary>
    public interface ISensorService
    {
        /// <summary>
        /// Register new sensor.
        /// </summary>
        /// <param name="sensor">Sensor object.</param>
        /// <returns>Sensor Id and operation status.</returns>
        Task<(int id, bool success)> RegisterNewSensorAsync(SensorDTO sensor);

        /// <summary>
        /// Get sensor by identifier.
        /// </summary>
        /// <param name="id">Sensor identifier.</param>
        /// <returns>Sensor object.</returns>
        Task<SensorDTO> GetSensorByIdAsync(int id);

        /// <summary>
        /// Get all registered sensors.
        /// </summary>
        /// <returns>Sensors collection.</returns>
        Task<ICollection<SensorDTO>> GetAllSensorsAsync();

        /// <summary>
        /// Get all registered sensors of specific profile.
        /// </summary>
        /// <param name="profileId">User profile identifier.</param>
        /// <returns>Sensors collection.</returns>
        Task<ICollection<SensorDTO>> GetAllSensorsByProfileIdAsync(Guid profileId);

        /// <summary>
        /// Update sensor information.
        /// </summary>
        /// <param name="sensor">Sensor object.</param>
        /// <returns>Operation status.</returns>
        Task<bool> UpdateSensorAsync(SensorDTO sensor);

        /// <summary>
        /// Delete sensor from application.
        /// </summary>
        /// <param name="id">Sensor identifier.</param>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteSensorByIdAsync(int id);
    }
}