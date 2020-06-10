namespace Sensor.API.Models
{
    /// <summary>
    /// Sensor entity.
    /// </summary>
    public class Sensor
    {
        /// <summary>
        /// Sensor Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sensor serial number.
        /// </summary>
        public string Serial { get; set; }
    }
}