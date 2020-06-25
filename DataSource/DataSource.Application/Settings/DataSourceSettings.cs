using System.Globalization;

namespace DataSource.Application.Settings
{
    /// <summary>
    /// Data generator settings.
    /// </summary>
    public class DataSourceSettings
    {
        /// <summary>
        /// Authorization token.
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// Host address for posting data. 
        /// </summary>
        public string HostAddress { get; set; }

        /// <summary>
        /// Sensor serial number.
        /// </summary>
        public string SensorSerial { get; set; }

        /// <summary>
        /// Data type to generate.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Data generation time interval (seconds).
        /// </summary>
        public string GenerationTimeIntervalSeconds { get; set; }
    }
}