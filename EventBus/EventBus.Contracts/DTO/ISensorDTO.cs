using System;

namespace EventBus.Contracts.DTO
{
    /// <summary>
    /// Interface for sensor data trasfer object.
    /// </summary>
    public interface ISensorDTO
    {
        /// <summary>
        /// Sensor Identifier.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Sensor serial number.
        /// </summary>
        string Serial { get; set; }

        /// <summary>
        /// Sensor type identifier.
        /// </summary>
        int SensorTypeId { get; set; }

        /// <summary>
        /// Sensor type.
        /// </summary>
        string SensorType { get; set; }

        /// <summary>
        /// User profile identifier.
        /// </summary>
        Guid ProfileId {get; set;}
    }
}