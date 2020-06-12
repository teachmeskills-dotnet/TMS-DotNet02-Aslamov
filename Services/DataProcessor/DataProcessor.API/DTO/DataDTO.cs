using System;

namespace DataProcessor.API.DTO
{
    /// <summary>
    /// Sensor data for processing.
    /// </summary>
    public class DataDTO
    {
        /// <summary>
        /// Data identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Data value.
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// Data acquisition  date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Sensor identifier.
        /// </summary>
        public int SensorDeviceId { get; set; }

        /// <summary>
        /// Serial of sensor device.
        /// </summary>
        public string SensorDeviceSerial { get; set; }

        /// <summary>
        /// Type of sensor device.
        /// </summary>
        public string SensorDeviceType { get; set; }
    }
}