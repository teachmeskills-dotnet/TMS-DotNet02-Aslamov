using Sensor.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sensor.API.Common.Interfaces
{
    /// <summary>
    /// Interface to manage sensor records.
    /// </summary>
    interface IRecordService
    {
        /// <summary>
        /// Create new sensor record.
        /// </summary>
        /// <param name="record">Sensor record object.</param>
        /// <returns>Record Identifier and operation status.</returns>
        Task<(int id, bool success)> CreateNewRecordAsync(RecordDTO record);

        /// <summary>
        /// Get sensor record by identifier.
        /// </summary>
        /// <param name="id">Sensor record identifier.</param>
        /// <returns>Sensor record object.</returns>
        Task<RecordDTO> GetRecordByIdAsync(int id);

        /// <summary>
        /// Get all records.
        /// </summary>
        /// <returns>Records collection.</returns>
        Task<ICollection<RecordDTO>> GetAllRecordsAsync();

        /// <summary>
        /// Update record.
        /// </summary>
        /// <param name="record">Sensor record object.</param>
        /// <returns>Operation status.</returns>
        Task<bool> UpdateRecordAsync(SensorDTO record);

        /// <summary>
        /// Delete sensor record application.
        /// </summary>
        /// <param name="id">Record identifier.</param>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteRecordAsync(int id);

        /// <summary>
        /// Delete all records application.
        /// </summary>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteAllRecordsAsync();
    }
}