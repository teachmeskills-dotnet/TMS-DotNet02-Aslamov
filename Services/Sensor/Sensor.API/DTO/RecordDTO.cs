using System;
using System.ComponentModel.DataAnnotations;

namespace Sensor.API.DTO
{
    /// <summary>
    /// Record data transfer object.
    /// </summary>
    public class RecordDTO
    {
        /// <summary>
        /// Data identifier.
        /// </summary>
        public int Id { get; set; }

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
        /// Mark deleted data.
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Sensor identifier.
        /// </summary>
        public int SensorDeviceId { get; set; }

        /// <summary>
        /// Serial of sensor device.
        /// </summary>
        [Required]
        public string SensorDeviceSerial { get; set; }
    }
}