using System;
using System.ComponentModel.DataAnnotations;

namespace DataProcessor.API.DTO
{
    /// <summary>
    /// Sensor data for processing.
    /// </summary>
    public class DataDTO
    {
        /// <summary>
        /// Data value.
        /// </summary>
        [Required]
        public byte[] Value { get; set; }

        /// <summary>
        /// Data acquisition  date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Sensor identifier.
        /// </summary>
        public int SensorDeviceId { get; set; }

        /// <summary>
        /// Serial of sensor device.
        /// </summary>
        [Required]
        public string SensorDeviceSerial { get; set; }

        /// <summary>
        /// Type of sensor device.
        /// </summary>
        [Required]
        public string SensorDeviceType { get; set; }
    }
}