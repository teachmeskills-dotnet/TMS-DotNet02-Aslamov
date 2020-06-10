namespace Sensor.API.Models
{
    /// <summary>
    /// Sensor type entity.
    /// </summary>
    public class SensorType
    {
        /// <summary>
        /// Sensor type identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sensor type.
        /// </summary>
        public string Type { get; set; }
    }
}