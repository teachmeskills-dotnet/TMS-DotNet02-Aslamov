using EventBus.Contracts.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sensor.API.DTO
{
    /// <summary>
    /// Sensor data trasfer object.
    /// </summary>
    public class SensorDTO : ISensorDTO
    {
        /// <summary>
        /// Sensor Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sensor serial number.
        /// </summary>
        [Required]
        public string Serial { get; set; }

        /// <summary>
        /// Sensor type identifier.
        /// </summary>
        public int SensorTypeId { get; set; }

        /// <summary>
        /// Sensor type.
        /// </summary>
        [Required]
        public string SensorType { get; set; }

        /// <summary>
        /// User profile identifier.
        /// </summary>
        [Required]
        public Guid ProfileId {get; set;}
    }
}