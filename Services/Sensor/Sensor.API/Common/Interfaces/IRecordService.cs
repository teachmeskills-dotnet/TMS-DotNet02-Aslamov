using Sensor.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sensor.API.Common.Interfaces
{
    /// <summary>
    /// Interface to manage sensor records.
    /// </summary>
    public interface IRecordService
    {
        /// <summary>
        /// Create new sensor record.
        /// </summary>
        /// <param name="record">Sensor record object.</param>
        /// <returns>Record Identifier and operation status.</returns>
        Task<(int id, bool success)> RegisterNewRecordAsync(RecordDTO recordDTO);

        /// <summary>
        /// Get sensor record by Identifier.
        /// </summary>
        /// <param name="id">Sensor record identifier.</param>
        /// <returns>Sensor record object.</returns>
        Task<RecordDTO> GetRecordByIdAsync(int id);

        /// <summary>
        /// Get all records.
        /// </summary>
        /// <returns>Records collection.</returns>
        Task<ICollection<RecordDTO>> GetAllRecordsAsync(int? sensorId);

        /// <summary>
        /// Update record.
        /// </summary>
        /// <param name="record">Sensor record object.</param>
        /// <returns>Operation status.</returns>
        Task<bool> UpdateRecordAsync(RecordDTO recordDTO);

        /// <summary>
        /// Delete sensor record by Identifier.
        /// </summary>
        /// <param name="id">Record identifier.</param>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteRecordByIdAsync(int id);

        /// <summary>
        /// Delete all records.
        /// </summary>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteAllRecordsAsync();

        /// <summary>
        /// Delete all sensor records.
        /// </summary>
        /// <param name="sensorId">Sensor identifier.</param>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteAllRecordsBySensorIdAsync(int sensorId);
    }
}