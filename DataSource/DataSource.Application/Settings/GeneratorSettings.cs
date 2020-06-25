using System.Globalization;

namespace DataSource.Application.Settings
{
    /// <summary>
    /// Data generator settings.
    /// </summary>
    public class GeneratorSettings
    {
        /// <summary>
        /// Generator serial number.
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        /// Data type to generate.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Data generation time interval.
        /// </summary>
        public string GenerationTimeInterval { get; set; }
    }
}
