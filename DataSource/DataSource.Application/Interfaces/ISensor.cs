using DataSource.Application.DTO;

namespace DataSource.Application.Interfaces
{
    /// <summary>
    /// Define interface for sensor device.
    /// </summary>
    public interface ISensor
    {
        /// <summary>
        /// Get random data record.
        /// </summary>
        /// <returns>Data record.</returns>
        RecordDTO GetDataRecord();
    }
}