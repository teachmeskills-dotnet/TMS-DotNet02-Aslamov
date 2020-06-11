namespace Sensor.API.Models
{
    /// <summary>
    /// Sensor entity.
    /// </summary>
    public class SensorDevice
    {
        /// <summary>
        /// Sensor Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sensor serial number.
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        /// Sendor type identifier.
        /// </summary>
        public int SensorTypeId { get; set; }

        /// <summary>
        /// Sensor type.
        /// </summary>
        public SensorType SensorType { get; set; }
    }
}