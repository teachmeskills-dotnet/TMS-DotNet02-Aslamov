namespace DataSource.API.Settings
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Authorization token.
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// Host address for posting data in Development environment. 
        /// </summary>
        public string DefaultHostAddress { get; set; }

        /// <summary>
        /// Host address for posting data in Production environment. 
        /// </summary>
        public string DockerHostAddress { get; set; }

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