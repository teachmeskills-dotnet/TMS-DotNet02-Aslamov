using System;

namespace DataSource.Application.DTO
{
    /// <summary>
    /// Record data transfer object.
    /// </summary>
    public class RecordDTO
    {
        /// <summary>
        /// Data value.
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// Data acquisition  date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Serial of sensor device.
        /// </summary>
        public string SensorDeviceSerial { get; set; }

        ///<inheritdoc/>
        public string SensorDeviceType { get; set; }
    }
}