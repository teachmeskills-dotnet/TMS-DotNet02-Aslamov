using DataSource.Application.DTO;
using DataSource.Application.Enums;

namespace DataSource.Application.Interfaces
{
    /// <summary>
    /// Define interface for sensor device.
    /// </summary>
    public interface ISensor
    {
        /// <summary>
        /// Sensor type.
        /// </summary>
        DataType DataType { get; set; }

        /// <summary>
        /// Sensor serial number.
        /// </summary>
        string Serial { get; set; }

        /// <summary>
        /// Get random data record.
        /// </summary>
        /// <returns>Data record.</returns>
        RecordDTO GetDataRecord();
    }
}