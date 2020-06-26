using System.ComponentModel.DataAnnotations;

namespace DataSource.Application.DTO
{
    /// <summary>
    /// Settings data transfer object.
    /// </summary>
    public class SettingsDTO
    {
        /// <summary>
        /// Sensor serial number.
        /// </summary>
        [Required]
        public string SensorSerial { get; set; }

        /// <summary>
        /// Data type to generate.
        /// </summary>
        [Required]
        public string DataType { get; set; }

        /// <summary>
        /// Data generation time interval (seconds).
        /// </summary>
        [Required]
        public string GenerationTimeIntervalSeconds { get; set; }
    }
}