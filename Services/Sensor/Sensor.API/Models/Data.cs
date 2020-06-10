using System;

namespace Sensor.API.Models
{
    /// <summary>
    /// Sensor data entity.
    /// </summary>
    public class Data
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
        /// Mark deleted data.
        /// </summary>
        public bool IsDeleted { get; set; }


        /// <summary>
        /// Sensor identifier.
        /// </summary>
        public int SensorId { get; set; }

        /// <summary>
        /// Sensor.
        /// </summary>
        public Sensor Sensor { get; set; }
    }
}
